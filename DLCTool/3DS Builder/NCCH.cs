using System;

namespace CTR
{
    public class NCCH
    {
        public Header header;
        public ExeFS exefs;
        public RomFS romfs;
        public Exheader exheader;
        public byte[] logo;
        public byte[] plainregion;
        public const uint MEDIA_UNIT_SIZE = 0x200;
        public bool isManual = false;
        public byte[] Manual;

        public class Header
        {
            public byte[] Signature; //Size: 0x100
            public uint Magic;
            public uint Size;
            public ulong TitleId;
            public ushort MakerCode;
            public ushort FormatVersion;
            //public uint padding0;
            public ulong ProgramId;
            //public byte[0x10] padding1;
            public byte[] LogoHash; // Size: 0x20
            public byte[] ProductCode; // Size: 0x10
            public byte[] ExheaderHash; // Size: 0x20
            public uint ExheaderSize;
            //public uint padding2;
            public byte[] Flags; // Size: 8
            public uint PlainRegionOffset;
            public uint PlainRegionSize;
            public uint LogoOffset;
            public uint LogoSize;
            public uint ExefsOffset;
            public uint ExefsSize;
            public uint ExefsSuperBlockSize;
            //public uint padding4;
            public uint RomfsOffset;
            public uint RomfsSize;
            public uint RomfsSuperBlockSize;
            //public uint padding5;
            public byte[] ExefsHash; // Size: 0x20
            public byte[] RomfsHash; // Size: 0x20

            public byte[] Data;

            public void BuildHeader()
            {
                Data = new byte[0x200];
                Array.Copy(Signature, Data, 0x100);
                Array.Copy(BitConverter.GetBytes(Magic), 0, Data, 0x100, 4);
                Array.Copy(BitConverter.GetBytes(Size), 0, Data, 0x104, 4);
                Array.Copy(BitConverter.GetBytes(TitleId), 0, Data, 0x108, 8);
                Array.Copy(BitConverter.GetBytes(MakerCode), 0, Data, 0x110, 2);
                Array.Copy(BitConverter.GetBytes(FormatVersion), 0, Data, 0x112, 2);
                //4 Byte Padding
                Array.Copy(BitConverter.GetBytes(ProgramId), 0, Data, 0x118, 8);
                //0x10 Byte Padding
                Array.Copy(LogoHash, 0, Data, 0x130, 0x20);
                Array.Copy(ProductCode, 0, Data, 0x150, 0x10);
                Array.Copy(ExheaderHash, 0, Data, 0x160, 0x20);
                Array.Copy(BitConverter.GetBytes(ExheaderSize), 0, Data, 0x180, 4);
                //4 Byte Padding
                Array.Copy(Flags, 0, Data, 0x188, 0x8);
                uint ofs = 0x190;
                foreach (uint val in new uint[] { PlainRegionOffset, PlainRegionSize, LogoOffset, LogoSize, ExefsOffset, ExefsSize, ExefsSuperBlockSize, 0, RomfsOffset, RomfsSize, RomfsSuperBlockSize, 0 })
                {
                    Array.Copy(BitConverter.GetBytes(val), 0, Data, ofs, 4);
                    ofs += 4;
                }
                Array.Copy(ExefsHash, 0, Data, 0x1C0, 0x20);
                Array.Copy(RomfsHash, 0, Data, 0x1E0, 0x20);
            }
        }

        public NCCH() { }

        public NCCH(byte[] ncch)
        {
            this.header = new Header();
            this.header.Signature = new byte[0x100];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(ncch))
            {
                ms.Seek(0x100, System.IO.SeekOrigin.Begin);
                using (System.IO.BinaryReader br = new System.IO.BinaryReader(ms))
                {
                    this.header.Magic = br.ReadUInt32();
                    this.header.Size = br.ReadUInt32();
                    this.header.TitleId = br.ReadUInt64();
                    this.header.MakerCode = br.ReadUInt16();
                    this.header.FormatVersion = br.ReadUInt16();
                    br.ReadUInt32(); // padding0
                    this.header.ProgramId = br.ReadUInt64();
                    br.ReadBytes(0x10); // padding1
                    this.header.LogoHash = br.ReadBytes(0x20);
                    this.header.ProductCode = br.ReadBytes(0x10);
                    this.header.ExheaderHash = br.ReadBytes(0x20);
                    this.header.ExheaderSize = br.ReadUInt32();
                    br.ReadUInt32(); //padding2
                    this.header.Flags = br.ReadBytes(0x8);
                    this.header.PlainRegionOffset = br.ReadUInt32();
                    this.header.PlainRegionSize = br.ReadUInt32();
                    this.header.LogoOffset = br.ReadUInt32();
                    this.header.LogoSize = br.ReadUInt32();
                    this.header.ExefsOffset = br.ReadUInt32();
                    this.header.ExefsSize = br.ReadUInt32();
                    this.header.ExefsSuperBlockSize = br.ReadUInt32();
                    br.ReadUInt32(); //padding3
                    this.header.RomfsOffset = br.ReadUInt32();
                    this.header.RomfsSize = br.ReadUInt32();
                    this.header.RomfsSuperBlockSize = br.ReadUInt32();
                    br.ReadUInt32(); //padding4
                    this.header.ExefsHash = br.ReadBytes(0x20);
                    this.header.RomfsHash = br.ReadBytes(0x20);
                }
            }
            this.header.Data = new byte[0x200];
            Array.Copy(ncch, this.header.Data, 0x200);
            this.isManual = true;
            this.Manual = ncch;
        }
    }
}