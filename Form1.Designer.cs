namespace OCRComparer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpHome = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTestAllOCRs = new System.Windows.Forms.Button();
            this.clbAllOCRs = new System.Windows.Forms.CheckedListBox();
            this.progressBarImagePreprocessing = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlGT = new System.Windows.Forms.Panel();
            this.lblGT = new System.Windows.Forms.Label();
            this.rtbGT = new System.Windows.Forms.RichTextBox();
            this.pbOriginalPreview = new System.Windows.Forms.PictureBox();
            this.tpStatistics = new System.Windows.Forms.TabPage();
            this.tcMain.SuspendLayout();
            this.tpHome.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlGT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginalPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tcMain.Controls.Add(this.tpHome);
            this.tcMain.Controls.Add(this.tpStatistics);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1204, 729);
            this.tcMain.TabIndex = 1;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tpHome
            // 
            this.tpHome.Controls.Add(this.panel2);
            this.tpHome.Controls.Add(this.progressBarImagePreprocessing);
            this.tpHome.Controls.Add(this.panel1);
            this.tpHome.Location = new System.Drawing.Point(47, 4);
            this.tpHome.Name = "tpHome";
            this.tpHome.Size = new System.Drawing.Size(1153, 721);
            this.tpHome.TabIndex = 0;
            this.tpHome.Text = "Home";
            this.tpHome.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnTestAllOCRs);
            this.panel2.Controls.Add(this.clbAllOCRs);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1153, 171);
            this.panel2.TabIndex = 8;
            // 
            // btnTestAllOCRs
            // 
            this.btnTestAllOCRs.Location = new System.Drawing.Point(0, 0);
            this.btnTestAllOCRs.Name = "btnTestAllOCRs";
            this.btnTestAllOCRs.Size = new System.Drawing.Size(227, 168);
            this.btnTestAllOCRs.TabIndex = 7;
            this.btnTestAllOCRs.Text = "Test selected!";
            this.btnTestAllOCRs.UseVisualStyleBackColor = true;
            this.btnTestAllOCRs.Click += new System.EventHandler(this.btnTestAllOCRs_Click);
            // 
            // clbAllOCRs
            // 
            this.clbAllOCRs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbAllOCRs.FormattingEnabled = true;
            this.clbAllOCRs.Items.AddRange(new object[] { "Google", "Tesseract", "OCR Space", "NewOcr" });
            this.clbAllOCRs.Location = new System.Drawing.Point(233, 3);
            this.clbAllOCRs.Name = "clbAllOCRs";
            this.clbAllOCRs.Size = new System.Drawing.Size(321, 156);
            this.clbAllOCRs.TabIndex = 6;
            // 
            // progressBarImagePreprocessing
            // 
            this.progressBarImagePreprocessing.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBarImagePreprocessing.Location = new System.Drawing.Point(0, 0);
            this.progressBarImagePreprocessing.Name = "progressBarImagePreprocessing";
            this.progressBarImagePreprocessing.Size = new System.Drawing.Size(1153, 42);
            this.progressBarImagePreprocessing.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pnlGT);
            this.panel1.Controls.Add(this.pbOriginalPreview);
            this.panel1.Location = new System.Drawing.Point(0, 213);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1166, 508);
            this.panel1.TabIndex = 5;
            // 
            // pnlGT
            // 
            this.pnlGT.AutoScroll = true;
            this.pnlGT.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlGT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGT.Controls.Add(this.lblGT);
            this.pnlGT.Controls.Add(this.rtbGT);
            this.pnlGT.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlGT.Location = new System.Drawing.Point(570, 0);
            this.pnlGT.Name = "pnlGT";
            this.pnlGT.Size = new System.Drawing.Size(596, 508);
            this.pnlGT.TabIndex = 4;
            // 
            // lblGT
            // 
            this.lblGT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGT.AutoSize = true;
            this.lblGT.Location = new System.Drawing.Point(215, 9);
            this.lblGT.Name = "lblGT";
            this.lblGT.Size = new System.Drawing.Size(199, 37);
            this.lblGT.TabIndex = 1;
            this.lblGT.Text = "Ground truth";
            // 
            // rtbGT
            // 
            this.rtbGT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbGT.Location = new System.Drawing.Point(0, 49);
            this.rtbGT.Name = "rtbGT";
            this.rtbGT.ReadOnly = true;
            this.rtbGT.Size = new System.Drawing.Size(594, 457);
            this.rtbGT.TabIndex = 0;
            this.rtbGT.Text = "";
            this.rtbGT.DoubleClick += new System.EventHandler(this.rtbGT_DoubleClick);
            // 
            // pbOriginalPreview
            // 
            this.pbOriginalPreview.BackColor = System.Drawing.Color.Silver;
            this.pbOriginalPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbOriginalPreview.InitialImage = null;
            this.pbOriginalPreview.Location = new System.Drawing.Point(0, 0);
            this.pbOriginalPreview.Name = "pbOriginalPreview";
            this.pbOriginalPreview.Size = new System.Drawing.Size(1166, 508);
            this.pbOriginalPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOriginalPreview.TabIndex = 1;
            this.pbOriginalPreview.TabStop = false;
            this.pbOriginalPreview.WaitOnLoad = true;
            this.pbOriginalPreview.DoubleClick += new System.EventHandler(this.pbOriginalPreview_DoubleClick);
            // 
            // tpStatistics
            // 
            this.tpStatistics.AutoScroll = true;
            this.tpStatistics.Location = new System.Drawing.Point(47, 4);
            this.tpStatistics.Name = "tpStatistics";
            this.tpStatistics.Size = new System.Drawing.Size(1153, 721);
            this.tpStatistics.TabIndex = 7;
            this.tpStatistics.Text = "Statistics";
            this.tpStatistics.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 729);
            this.Controls.Add(this.tcMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1220, 768);
            this.Name = "MainForm";
            this.Text = "OCR Comparer";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tcMain.ResumeLayout(false);
            this.tpHome.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlGT.ResumeLayout(false);
            this.pnlGT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginalPreview)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpHome;
        private System.Windows.Forms.PictureBox pbOriginalPreview;
        private System.Windows.Forms.Panel pnlGT;
        private System.Windows.Forms.ProgressBar progressBarImagePreprocessing;
        private System.Windows.Forms.TabPage tpStatistics;
        private System.Windows.Forms.RichTextBox rtbGT;
        private System.Windows.Forms.Label lblGT;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTestAllOCRs;
        private System.Windows.Forms.CheckedListBox clbAllOCRs;
        private System.Windows.Forms.Panel panel2;
    }
}

