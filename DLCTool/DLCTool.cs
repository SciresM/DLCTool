using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using DLCTool.Properties;

using _3DSExplorer.Utils;
using PKHeX.WinForms;
using PKHeX.Core;
using CTR;

namespace DLCTool
{
    public partial class DLCTool : Form
    {
        private Content[] Contents;
        private ushort Version;
        private string root_dir;
        private string icon_dir;
        private uint unique_id;
        private volatile int threads;
        public DLCTool()
        {
            if (!File.Exists("makerom.exe"))
                File.WriteAllBytes("makerom.exe", Resources.makerom);
            if (!File.Exists("rom.rsf"))
                File.WriteAllBytes("rom.rsf", Resources.rom_rsf);
            if (!File.Exists("empty_romfs.bin"))
                File.WriteAllBytes("empty_romfs.bin", Resources.empty_romfs);
            InitializeComponent();
        }

        private void LoadDLCFolder(string folder)
        {
            Contents = null;
            Version = 0;
            root_dir = folder;
            B_Rebuild.Enabled = false;
            CLB_DLC.Items.Clear();
            var tmdfn = Path.Combine(root_dir, "tmd");
            if (!File.Exists(tmdfn))
            {
                WinFormsUtil.Error("Invalid DLC folder", "tmd does not exist.");
                return;
            }

            if (!File.Exists(Path.Combine(root_dir, "icon.bin")))
            {
                WinFormsUtil.Error("Invalid DLC folder", "icon.bin does not exist.");
            }

            var tmd = File.ReadAllBytes(tmdfn);

            Version = BigEndian.ToUInt16(tmd, 0x1DC);
            Contents = new Content[BigEndian.ToUInt16(tmd, 0x1DE)];
            unique_id = (BigEndian.ToUInt32(tmd, 0x190) >> 8) & 0xFFFFF;

            for (var i = 0; i < Contents.Length; i++)
            {
                Contents[i] = new Content
                {
                    ID = BigEndian.ToUInt32(tmd, 0xB04 + 0x30*i),
                    Index = BigEndian.ToUInt16(tmd, 0xB08 + 0x30*i)
                };
            }

            var metadata_dir = Path.Combine(root_dir, $"{Contents[0].ID.ToString("X8")}_rom");
            icon_dir = Path.Combine(metadata_dir, "icons");

            if (!Directory.Exists(metadata_dir))
            {
                WinFormsUtil.Error("Invalid DLC Folder", "Metadata romfs directory does not exist.");
            }
            
            var cin_fn = (new DirectoryInfo(metadata_dir)).GetFiles().FirstOrDefault(f => f.Name.StartsWith("ContentInfoArchive"));
            if (cin_fn == null)
            {
                WinFormsUtil.Error("Invalid DLC folder", "No Content Info Archive!");
                return;
            }

            var cin = File.ReadAllBytes(cin_fn.FullName);
            var shift_jis = Encoding.UTF8;

            for (var i = 0; i < Contents.Length; i++)
            {
                Contents[i].Name = Util.TrimFromZero(shift_jis.GetString(cin, 0x8 + 0xC8*i, 0x40));
                Contents[i].Description = Util.TrimFromZero(shift_jis.GetString(cin, 0x48 + 0xC8 * i, 0x80));
                var icon_ind = BitConverter.ToUInt32(cin, 0xC8 + 0xC8*i);
                if (icon_ind == 0)
                    icon_ind = 1;
                Contents[i].IconPath = Path.Combine(icon_dir, $"{icon_ind}.icn");
                CLB_DLC.Items.Add(Contents[i].Name, Directory.Exists(Path.Combine(root_dir, $"{Contents[i].ID.ToString("X8")}_rom")));
            }

            CLB_DLC.SelectedIndex = 0;

            TB_Directory.Text = root_dir;
            B_Rebuild.Enabled = true;

        }

        private void LoadIcons(string fn)
        {
            var icn = File.ReadAllBytes(fn);
            if (icn.Length != 0x1200)
                return;
            using (var ms = new MemoryStream(icn))
            {
                PB_LargeIcon.Image = ImageUtil.ReadImageFromStream(ms, 48, 48, ImageUtil.PixelFormat.RGB565);
                PB_SmallIcon.Image = ImageUtil.ReadImageFromStream(ms, 24, 24, ImageUtil.PixelFormat.RGB565);
       
            }
        }

        private void CLB_DLC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadIcons(Contents[CLB_DLC.SelectedIndex].IconPath);
            TB_TitleDesc.ResetText();
            TB_TitleDesc.AppendText(Contents[CLB_DLC.SelectedIndex].ID.ToString("X8"));
            TB_TitleDesc.AppendText(Environment.NewLine);
            TB_TitleDesc.AppendText(Contents[CLB_DLC.SelectedIndex].Name);
            TB_TitleDesc.AppendText(Environment.NewLine);
            TB_TitleDesc.AppendText(Contents[CLB_DLC.SelectedIndex].Description);
        }

        private void B_Open_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK)
                return;
            LoadDLCFolder(fbd.SelectedPath);
        }

        private void CLB_DLC_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0 && e.NewValue == CheckState.Unchecked)
            {
                WinFormsUtil.Error("Cannot disable metadata archive!");
                e.NewValue = CheckState.Checked;
            }
            var rom_path = Path.Combine(root_dir, $"{Contents[e.Index].ID.ToString("X8")}_rom");
            if (Directory.Exists(rom_path) || e.NewValue != CheckState.Checked) return;
            WinFormsUtil.Error($"Cannot enable content {Contents[e.Index].Name}.", $"Romfs folder {rom_path} does not exist.");
            e.NewValue = CheckState.Unchecked;
        }

        private void BuildCIA()
        {
            if (File.Exists("dlc.rsf"))
                File.Delete("dlc.rsf");

            var dlc_rsf = Resources.dlc_rsf;
            File.WriteAllBytes("dlc.rsf", Resources.dlc_rsf);
            var dlc_str = File.ReadAllText("dlc.rsf");
            dlc_str =
                dlc_str.Replace("{unique_id}", unique_id.ToString("X5"))
                    .Replace("{product_code}", MTB_ProductCode.Text.Replace("_", "A"))
                    .Replace("{content_index}", MTB_ContentIndex.Text.Replace("_", "0"));
            File.WriteAllText("dlc.rsf", dlc_str);

            File.Copy(Path.Combine(root_dir, "icon.bin"), "icon.bin");

            for (var i = 0; i < Contents.Length; i++)
            {
                SetTask($"Building Content {i+1}/{Contents.Length}...");
                var content = Contents[i];
                var romfs_path = "empty_romfs.bin";
                if (CLB_DLC.GetItemChecked(i))
                {
                    romfs_path = "romfs.bin";
                    var rom_path = Path.Combine(root_dir, $"{content.ID.ToString("X8")}_rom");
                    RomFS.BuildRomFS(romfs_path, rom_path, null, PB_Prog);
                }

                var cmd = $" -f ncch -target t -rsf dlc.rsf -romfs {romfs_path}";
                if (i == 0)
                    cmd += " -icon icon.bin";
                cmd += $" -o {content.ID.ToString("X8")}";

                Makerom(cmd);
                if (File.Exists("romfs.bin"))
                    File.Delete("romfs.bin");
            }

            var major = ((Version & 0xFC00) >> 10);
            var minor = ((Version & 0x3F0) >> 4);
            var micro = ((Version & 0xF) >> 0);
            SetTask("Building final cia...");
            var command = " -f cia -rsf rom.rsf -o dlc.cia -ckeyid 0";
            command += $" -major {major} -minor {minor} -micro {micro} -DSaveSize=0 -dlc";
            for (var i = 0; i < Contents.Length; i++)
                command += Contents[i].MakeromInclude;

            Makerom(command);
            if (File.Exists(Path.Combine(root_dir, "dlc.cia")))
                File.Delete(Path.Combine(root_dir, "dlc.cia"));
            File.Move("dlc.cia", Path.Combine(root_dir, "dlc.cia"));

            for (var i = 0; i < Contents.Length; i++)
            {
                File.Delete(Contents[i].ID.ToString("X8"));
            }
            File.Delete("dlc.rsf");
            File.Delete("icon.bin");
        }

        private void Makerom(string commands)
        {
            var p = new Process
            {
                StartInfo = {FileName = "makerom.exe", WindowStyle = ProcessWindowStyle.Hidden, Arguments = commands}
            };
            p.Start();
            p.WaitForExit();
        }
        
        private void SetTask(string task)
        {
            if (L_Task.InvokeRequired)
                L_Task.Invoke((Action) (() => L_Task.Text = task));
            else
                L_Task.Text = task;
        }

        private void B_Rebuild_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                threads++;
                SetTask("");
                Invoke((Action)(() => { B_Rebuild.Enabled = CLB_DLC.Enabled = MTB_ContentIndex.Enabled = MTB_ProductCode.Enabled = false; L_Task.Visible = true; }));
                BuildCIA();
                Invoke((Action)(() => { B_Rebuild.Enabled = CLB_DLC.Enabled = MTB_ContentIndex.Enabled = MTB_ProductCode.Enabled = true; L_Task.Visible = false; }));
                threads--;
                WinFormsUtil.Alert("Alert!", "Built DLC to " + Path.Combine(root_dir, "dlc.cia") + ".");
            }).Start();
        }
    }

    class Content
    {
        public uint ID;
        public ushort Index;
        public string Name;
        public string Description;
        public string IconPath;

        public string MakeromInclude => $" -i {ID.ToString("X8")}:0x{Index.ToString("X4")}:0x{ID.ToString("X8")}";
    }
}
