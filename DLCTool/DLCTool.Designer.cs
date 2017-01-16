namespace DLCTool
{
    partial class DLCTool
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.B_Open = new System.Windows.Forms.Button();
            this.TB_Directory = new System.Windows.Forms.TextBox();
            this.CLB_DLC = new System.Windows.Forms.CheckedListBox();
            this.TB_TitleDesc = new System.Windows.Forms.TextBox();
            this.PB_LargeIcon = new System.Windows.Forms.PictureBox();
            this.PB_SmallIcon = new System.Windows.Forms.PictureBox();
            this.B_Rebuild = new System.Windows.Forms.Button();
            this.L_CTRM = new System.Windows.Forms.Label();
            this.MTB_ProductCode = new System.Windows.Forms.MaskedTextBox();
            this.L_dash = new System.Windows.Forms.Label();
            this.MTB_ContentIndex = new System.Windows.Forms.MaskedTextBox();
            this.L_Task = new System.Windows.Forms.Label();
            this.PB_Prog = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.PB_LargeIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_SmallIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // B_Open
            // 
            this.B_Open.Location = new System.Drawing.Point(373, 11);
            this.B_Open.Name = "B_Open";
            this.B_Open.Size = new System.Drawing.Size(107, 23);
            this.B_Open.TabIndex = 0;
            this.B_Open.Text = "Open";
            this.B_Open.UseVisualStyleBackColor = true;
            this.B_Open.Click += new System.EventHandler(this.B_Open_Click);
            // 
            // TB_Directory
            // 
            this.TB_Directory.Location = new System.Drawing.Point(12, 13);
            this.TB_Directory.Name = "TB_Directory";
            this.TB_Directory.ReadOnly = true;
            this.TB_Directory.Size = new System.Drawing.Size(355, 20);
            this.TB_Directory.TabIndex = 1;
            // 
            // CLB_DLC
            // 
            this.CLB_DLC.FormattingEnabled = true;
            this.CLB_DLC.Location = new System.Drawing.Point(12, 39);
            this.CLB_DLC.Name = "CLB_DLC";
            this.CLB_DLC.Size = new System.Drawing.Size(172, 229);
            this.CLB_DLC.TabIndex = 2;
            this.CLB_DLC.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CLB_DLC_ItemCheck);
            this.CLB_DLC.SelectedIndexChanged += new System.EventHandler(this.CLB_DLC_SelectedIndexChanged);
            // 
            // TB_TitleDesc
            // 
            this.TB_TitleDesc.Location = new System.Drawing.Point(186, 93);
            this.TB_TitleDesc.Multiline = true;
            this.TB_TitleDesc.Name = "TB_TitleDesc";
            this.TB_TitleDesc.ReadOnly = true;
            this.TB_TitleDesc.Size = new System.Drawing.Size(181, 175);
            this.TB_TitleDesc.TabIndex = 3;
            // 
            // PB_LargeIcon
            // 
            this.PB_LargeIcon.Location = new System.Drawing.Point(186, 39);
            this.PB_LargeIcon.Name = "PB_LargeIcon";
            this.PB_LargeIcon.Size = new System.Drawing.Size(48, 48);
            this.PB_LargeIcon.TabIndex = 4;
            this.PB_LargeIcon.TabStop = false;
            // 
            // PB_SmallIcon
            // 
            this.PB_SmallIcon.Location = new System.Drawing.Point(237, 63);
            this.PB_SmallIcon.Name = "PB_SmallIcon";
            this.PB_SmallIcon.Size = new System.Drawing.Size(24, 24);
            this.PB_SmallIcon.TabIndex = 5;
            this.PB_SmallIcon.TabStop = false;
            // 
            // B_Rebuild
            // 
            this.B_Rebuild.Enabled = false;
            this.B_Rebuild.Location = new System.Drawing.Point(373, 40);
            this.B_Rebuild.Name = "B_Rebuild";
            this.B_Rebuild.Size = new System.Drawing.Size(107, 23);
            this.B_Rebuild.TabIndex = 6;
            this.B_Rebuild.Text = "Rebuild";
            this.B_Rebuild.UseVisualStyleBackColor = true;
            this.B_Rebuild.Click += new System.EventHandler(this.B_Rebuild_Click);
            // 
            // L_CTRM
            // 
            this.L_CTRM.AutoSize = true;
            this.L_CTRM.Location = new System.Drawing.Point(263, 74);
            this.L_CTRM.Name = "L_CTRM";
            this.L_CTRM.Size = new System.Drawing.Size(44, 13);
            this.L_CTRM.TabIndex = 7;
            this.L_CTRM.Text = "CTR-M-";
            // 
            // MTB_ProductCode
            // 
            this.MTB_ProductCode.Location = new System.Drawing.Point(304, 71);
            this.MTB_ProductCode.Mask = "AAAA";
            this.MTB_ProductCode.Name = "MTB_ProductCode";
            this.MTB_ProductCode.Size = new System.Drawing.Size(36, 20);
            this.MTB_ProductCode.TabIndex = 8;
            // 
            // L_dash
            // 
            this.L_dash.AutoSize = true;
            this.L_dash.Location = new System.Drawing.Point(340, 74);
            this.L_dash.Name = "L_dash";
            this.L_dash.Size = new System.Drawing.Size(10, 13);
            this.L_dash.TabIndex = 9;
            this.L_dash.Text = "-";
            // 
            // MTB_ContentIndex
            // 
            this.MTB_ContentIndex.Location = new System.Drawing.Point(348, 71);
            this.MTB_ContentIndex.Mask = "00";
            this.MTB_ContentIndex.Name = "MTB_ContentIndex";
            this.MTB_ContentIndex.Size = new System.Drawing.Size(19, 20);
            this.MTB_ContentIndex.TabIndex = 10;
            // 
            // L_Task
            // 
            this.L_Task.AutoSize = true;
            this.L_Task.Location = new System.Drawing.Point(373, 74);
            this.L_Task.Name = "L_Task";
            this.L_Task.Size = new System.Drawing.Size(0, 13);
            this.L_Task.TabIndex = 11;
            this.L_Task.Visible = false;
            // 
            // PB_Prog
            // 
            this.PB_Prog.Location = new System.Drawing.Point(373, 93);
            this.PB_Prog.Name = "PB_Prog";
            this.PB_Prog.Size = new System.Drawing.Size(107, 16);
            this.PB_Prog.TabIndex = 12;
            // 
            // DLCTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 278);
            this.Controls.Add(this.PB_Prog);
            this.Controls.Add(this.L_Task);
            this.Controls.Add(this.MTB_ContentIndex);
            this.Controls.Add(this.L_dash);
            this.Controls.Add(this.MTB_ProductCode);
            this.Controls.Add(this.L_CTRM);
            this.Controls.Add(this.B_Rebuild);
            this.Controls.Add(this.PB_SmallIcon);
            this.Controls.Add(this.PB_LargeIcon);
            this.Controls.Add(this.TB_TitleDesc);
            this.Controls.Add(this.CLB_DLC);
            this.Controls.Add(this.TB_Directory);
            this.Controls.Add(this.B_Open);
            this.Name = "DLCTool";
            this.Text = "3DS DLC Tool";
            ((System.ComponentModel.ISupportInitialize)(this.PB_LargeIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_SmallIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_Open;
        private System.Windows.Forms.TextBox TB_Directory;
        private System.Windows.Forms.CheckedListBox CLB_DLC;
        private System.Windows.Forms.TextBox TB_TitleDesc;
        private System.Windows.Forms.PictureBox PB_LargeIcon;
        private System.Windows.Forms.PictureBox PB_SmallIcon;
        private System.Windows.Forms.Button B_Rebuild;
        private System.Windows.Forms.Label L_CTRM;
        private System.Windows.Forms.MaskedTextBox MTB_ProductCode;
        private System.Windows.Forms.Label L_dash;
        private System.Windows.Forms.MaskedTextBox MTB_ContentIndex;
        private System.Windows.Forms.Label L_Task;
        private System.Windows.Forms.ProgressBar PB_Prog;
    }
}

