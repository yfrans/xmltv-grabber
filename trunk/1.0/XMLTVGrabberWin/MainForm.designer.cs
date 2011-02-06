namespace XMLTVGrabberWin
{
	partial class MainForm
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
			if (disposing && (components != null)) {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.chkLbChannels_HOT = new System.Windows.Forms.CheckedListBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnUpdChannels = new System.Windows.Forms.Button();
			this.btnCreateXMLTV = new System.Windows.Forms.Button();
			this.rtbLog = new System.Windows.Forms.RichTextBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.chkLbChannels_General = new System.Windows.Forms.CheckedListBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.chkLbChannels_YES = new System.Windows.Forms.CheckedListBox();
			this.txtXMLTVPath = new System.Windows.Forms.TextBox();
			this.btnBrowseXMLTVPath = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtIconsPath = new System.Windows.Forms.TextBox();
			this.btnBrowseIconPath = new System.Windows.Forms.Button();
			this.chkBoxUseXMLTV2MXF = new System.Windows.Forms.CheckBox();
			this.chkBoxDownloadIcons = new System.Windows.Forms.CheckBox();
			this.txtXMLTV2MXFPath = new System.Windows.Forms.TextBox();
			this.btnBrowseXMLTV2MXFPath = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnRunXMLTV2MXF = new System.Windows.Forms.Button();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// chkLbChannels_HOT
			// 
			this.chkLbChannels_HOT.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkLbChannels_HOT.Location = new System.Drawing.Point(3, 3);
			this.chkLbChannels_HOT.Name = "chkLbChannels_HOT";
			this.chkLbChannels_HOT.ScrollAlwaysVisible = true;
			this.chkLbChannels_HOT.Size = new System.Drawing.Size(314, 319);
			this.chkLbChannels_HOT.Sorted = true;
			this.chkLbChannels_HOT.TabIndex = 0;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(6, 183);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(108, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnUpdChannels
			// 
			this.btnUpdChannels.Location = new System.Drawing.Point(7, 19);
			this.btnUpdChannels.Name = "btnUpdChannels";
			this.btnUpdChannels.Size = new System.Drawing.Size(108, 23);
			this.btnUpdChannels.TabIndex = 2;
			this.btnUpdChannels.Text = "Update Channels";
			this.btnUpdChannels.UseVisualStyleBackColor = true;
			this.btnUpdChannels.Click += new System.EventHandler(this.btnUpdChannels_Click);
			// 
			// btnCreateXMLTV
			// 
			this.btnCreateXMLTV.Location = new System.Drawing.Point(6, 48);
			this.btnCreateXMLTV.Name = "btnCreateXMLTV";
			this.btnCreateXMLTV.Size = new System.Drawing.Size(109, 23);
			this.btnCreateXMLTV.TabIndex = 3;
			this.btnCreateXMLTV.Text = "Create XMLTV";
			this.btnCreateXMLTV.UseVisualStyleBackColor = true;
			this.btnCreateXMLTV.Click += new System.EventHandler(this.btnCreateXMLTV_Click);
			// 
			// rtbLog
			// 
			this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbLog.Location = new System.Drawing.Point(0, 0);
			this.rtbLog.Name = "rtbLog";
			this.rtbLog.ReadOnly = true;
			this.rtbLog.Size = new System.Drawing.Size(447, 100);
			this.rtbLog.TabIndex = 4;
			this.rtbLog.Text = "";
			this.rtbLog.WordWrap = false;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.rtbLog);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 351);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(447, 100);
			this.panel3.TabIndex = 6;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 451);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(447, 22);
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.panel1);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(327, 351);
			this.panel4.TabIndex = 8;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(328, 351);
			this.panel1.TabIndex = 4;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(328, 351);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.chkLbChannels_General);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(320, 325);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// chkLbChannels_General
			// 
			this.chkLbChannels_General.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkLbChannels_General.Location = new System.Drawing.Point(3, 3);
			this.chkLbChannels_General.Name = "chkLbChannels_General";
			this.chkLbChannels_General.ScrollAlwaysVisible = true;
			this.chkLbChannels_General.Size = new System.Drawing.Size(314, 319);
			this.chkLbChannels_General.Sorted = true;
			this.chkLbChannels_General.TabIndex = 1;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.chkLbChannels_HOT);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(320, 325);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "HOT";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.chkLbChannels_YES);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(320, 325);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "YES";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// chkLbChannels_YES
			// 
			this.chkLbChannels_YES.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkLbChannels_YES.Location = new System.Drawing.Point(3, 3);
			this.chkLbChannels_YES.Name = "chkLbChannels_YES";
			this.chkLbChannels_YES.ScrollAlwaysVisible = true;
			this.chkLbChannels_YES.Size = new System.Drawing.Size(314, 319);
			this.chkLbChannels_YES.Sorted = true;
			this.chkLbChannels_YES.TabIndex = 2;
			// 
			// txtXMLTVPath
			// 
			this.txtXMLTVPath.Enabled = false;
			this.txtXMLTVPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtXMLTVPath.Location = new System.Drawing.Point(6, 35);
			this.txtXMLTVPath.Name = "txtXMLTVPath";
			this.txtXMLTVPath.Size = new System.Drawing.Size(75, 18);
			this.txtXMLTVPath.TabIndex = 6;
			// 
			// btnBrowseXMLTVPath
			// 
			this.btnBrowseXMLTVPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowseXMLTVPath.Location = new System.Drawing.Point(87, 35);
			this.btnBrowseXMLTVPath.Name = "btnBrowseXMLTVPath";
			this.btnBrowseXMLTVPath.Size = new System.Drawing.Size(27, 18);
			this.btnBrowseXMLTVPath.TabIndex = 7;
			this.btnBrowseXMLTVPath.Text = "...";
			this.btnBrowseXMLTVPath.UseVisualStyleBackColor = true;
			this.btnBrowseXMLTVPath.Click += new System.EventHandler(this.btnBrowseXMLTVPath_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtIconsPath);
			this.groupBox1.Controls.Add(this.btnBrowseIconPath);
			this.groupBox1.Controls.Add(this.chkBoxUseXMLTV2MXF);
			this.groupBox1.Controls.Add(this.chkBoxDownloadIcons);
			this.groupBox1.Controls.Add(this.txtXMLTV2MXFPath);
			this.groupBox1.Controls.Add(this.btnBrowseXMLTV2MXFPath);
			this.groupBox1.Controls.Add(this.btnSave);
			this.groupBox1.Controls.Add(this.txtXMLTVPath);
			this.groupBox1.Controls.Add(this.btnBrowseXMLTVPath);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 136);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(120, 215);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Settings";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(6, 126);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(97, 12);
			this.label3.TabIndex = 16;
			this.label3.Text = "Channels Icons Folder";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(7, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 12);
			this.label2.TabIndex = 15;
			this.label2.Text = "XMLTV2MXF Path";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(7, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 12);
			this.label1.TabIndex = 14;
			this.label1.Text = "XMLTV Path";
			// 
			// txtIconsPath
			// 
			this.txtIconsPath.Enabled = false;
			this.txtIconsPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtIconsPath.Location = new System.Drawing.Point(6, 141);
			this.txtIconsPath.Name = "txtIconsPath";
			this.txtIconsPath.Size = new System.Drawing.Size(75, 18);
			this.txtIconsPath.TabIndex = 12;
			// 
			// btnBrowseIconPath
			// 
			this.btnBrowseIconPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowseIconPath.Location = new System.Drawing.Point(87, 141);
			this.btnBrowseIconPath.Name = "btnBrowseIconPath";
			this.btnBrowseIconPath.Size = new System.Drawing.Size(27, 18);
			this.btnBrowseIconPath.TabIndex = 13;
			this.btnBrowseIconPath.Text = "...";
			this.btnBrowseIconPath.UseVisualStyleBackColor = true;
			this.btnBrowseIconPath.Click += new System.EventHandler(this.btnBrowseIconPath_Click);
			// 
			// chkBoxUseXMLTV2MXF
			// 
			this.chkBoxUseXMLTV2MXF.AutoSize = true;
			this.chkBoxUseXMLTV2MXF.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkBoxUseXMLTV2MXF.Location = new System.Drawing.Point(6, 98);
			this.chkBoxUseXMLTV2MXF.Name = "chkBoxUseXMLTV2MXF";
			this.chkBoxUseXMLTV2MXF.Size = new System.Drawing.Size(101, 16);
			this.chkBoxUseXMLTV2MXF.TabIndex = 11;
			this.chkBoxUseXMLTV2MXF.Text = "Use XMLTV2MXF";
			this.chkBoxUseXMLTV2MXF.UseVisualStyleBackColor = true;
			// 
			// chkBoxDownloadIcons
			// 
			this.chkBoxDownloadIcons.AutoSize = true;
			this.chkBoxDownloadIcons.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkBoxDownloadIcons.Location = new System.Drawing.Point(6, 161);
			this.chkBoxDownloadIcons.Name = "chkBoxDownloadIcons";
			this.chkBoxDownloadIcons.Size = new System.Drawing.Size(90, 16);
			this.chkBoxDownloadIcons.TabIndex = 10;
			this.chkBoxDownloadIcons.Text = "Download icons";
			this.chkBoxDownloadIcons.UseVisualStyleBackColor = true;
			// 
			// txtXMLTV2MXFPath
			// 
			this.txtXMLTV2MXFPath.Enabled = false;
			this.txtXMLTV2MXFPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtXMLTV2MXFPath.Location = new System.Drawing.Point(6, 78);
			this.txtXMLTV2MXFPath.Name = "txtXMLTV2MXFPath";
			this.txtXMLTV2MXFPath.Size = new System.Drawing.Size(75, 18);
			this.txtXMLTV2MXFPath.TabIndex = 8;
			// 
			// btnBrowseXMLTV2MXFPath
			// 
			this.btnBrowseXMLTV2MXFPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowseXMLTV2MXFPath.Location = new System.Drawing.Point(87, 78);
			this.btnBrowseXMLTV2MXFPath.Name = "btnBrowseXMLTV2MXFPath";
			this.btnBrowseXMLTV2MXFPath.Size = new System.Drawing.Size(27, 18);
			this.btnBrowseXMLTV2MXFPath.TabIndex = 9;
			this.btnBrowseXMLTV2MXFPath.Text = "...";
			this.btnBrowseXMLTV2MXFPath.UseVisualStyleBackColor = true;
			this.btnBrowseXMLTV2MXFPath.Click += new System.EventHandler(this.btnBrowseXMLTV2MXFPath_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.groupBox1);
			this.panel2.Controls.Add(this.groupBox2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(327, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(120, 351);
			this.panel2.TabIndex = 9;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnRunXMLTV2MXF);
			this.groupBox2.Controls.Add(this.btnCreateXMLTV);
			this.groupBox2.Controls.Add(this.btnUpdChannels);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(120, 136);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Actions";
			// 
			// btnRunXMLTV2MXF
			// 
			this.btnRunXMLTV2MXF.Location = new System.Drawing.Point(6, 77);
			this.btnRunXMLTV2MXF.Name = "btnRunXMLTV2MXF";
			this.btnRunXMLTV2MXF.Size = new System.Drawing.Size(109, 23);
			this.btnRunXMLTV2MXF.TabIndex = 4;
			this.btnRunXMLTV2MXF.Text = "Run XMLTV2MXF";
			this.btnRunXMLTV2MXF.UseVisualStyleBackColor = true;
			this.btnRunXMLTV2MXF.Click += new System.EventHandler(this.btnRunXMLTV2MXF_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(447, 473);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "XMLTV Grabber";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckedListBox chkLbChannels_HOT;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnUpdChannels;
		private System.Windows.Forms.Button btnCreateXMLTV;
		private System.Windows.Forms.RichTextBox rtbLog;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.CheckedListBox chkLbChannels_General;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.CheckedListBox chkLbChannels_YES;
		private System.Windows.Forms.TextBox txtXMLTVPath;
		private System.Windows.Forms.Button btnBrowseXMLTVPath;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkBoxUseXMLTV2MXF;
		private System.Windows.Forms.CheckBox chkBoxDownloadIcons;
		private System.Windows.Forms.TextBox txtXMLTV2MXFPath;
		private System.Windows.Forms.Button btnBrowseXMLTV2MXFPath;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtIconsPath;
		private System.Windows.Forms.Button btnBrowseIconPath;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnRunXMLTV2MXF;

	}
}

