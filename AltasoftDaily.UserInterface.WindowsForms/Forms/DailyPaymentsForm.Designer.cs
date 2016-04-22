namespace AltasoftDaily.UserInterface.WindowsForms
{
    partial class DailyPaymentsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DailyPaymentsForm));
            this.pbxSave = new System.Windows.Forms.PictureBox();
            this.pbxOrders = new System.Windows.Forms.PictureBox();
            this.pbxStats = new System.Windows.Forms.PictureBox();
            this.pbxUpload = new System.Windows.Forms.PictureBox();
            this.pbxGuarantors = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.cbxPorts = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGuarantors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox3);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox2);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.pbxGuarantors);
            this.splitContainer1.Panel1.Controls.Add(this.pbxUpload);
            this.splitContainer1.Panel1.Controls.Add(this.pbxStats);
            this.splitContainer1.Panel1.Controls.Add(this.pbxOrders);
            this.splitContainer1.Panel1.Controls.Add(this.pbxSave);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer2
            // 
            // 
            // pbxFilter
            // 
            this.pbxFilter.Click += new System.EventHandler(this.pbxFilter_Click);
            // 
            // pbxExport
            // 
            this.pbxExport.Click += new System.EventHandler(this.pbxExport_Click);
            // 
            // pbxSave
            // 
            this.pbxSave.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxSave;
            this.pbxSave.Location = new System.Drawing.Point(117, 3);
            this.pbxSave.Name = "pbxSave";
            this.pbxSave.Size = new System.Drawing.Size(32, 32);
            this.pbxSave.TabIndex = 1;
            this.pbxSave.TabStop = false;
            this.pbxSave.Tag = "შენახვა";
            this.pbxSave.Click += new System.EventHandler(this.pbxSave_Click);
            // 
            // pbxOrders
            // 
            this.pbxOrders.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxOrders;
            this.pbxOrders.Location = new System.Drawing.Point(155, 3);
            this.pbxOrders.Name = "pbxOrders";
            this.pbxOrders.Size = new System.Drawing.Size(32, 32);
            this.pbxOrders.TabIndex = 2;
            this.pbxOrders.TabStop = false;
            this.pbxOrders.Tag = "საშემოსავლო";
            this.pbxOrders.Click += new System.EventHandler(this.pbxOrders_Click);
            // 
            // pbxStats
            // 
            this.pbxStats.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxStats;
            this.pbxStats.Location = new System.Drawing.Point(193, 3);
            this.pbxStats.Name = "pbxStats";
            this.pbxStats.Size = new System.Drawing.Size(32, 32);
            this.pbxStats.TabIndex = 3;
            this.pbxStats.TabStop = false;
            this.pbxStats.Tag = "სტატისტიკა";
            this.pbxStats.Click += new System.EventHandler(this.pbxStats_Click);
            // 
            // pbxUpload
            // 
            this.pbxUpload.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxUpload;
            this.pbxUpload.Location = new System.Drawing.Point(231, 3);
            this.pbxUpload.Name = "pbxUpload";
            this.pbxUpload.Size = new System.Drawing.Size(32, 32);
            this.pbxUpload.TabIndex = 4;
            this.pbxUpload.TabStop = false;
            this.pbxUpload.Tag = "ატვირთვა";
            this.pbxUpload.Click += new System.EventHandler(this.pbxUpload_Click);
            // 
            // pbxGuarantors
            // 
            this.pbxGuarantors.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxGuarantors;
            this.pbxGuarantors.InitialImage = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxGuarantors;
            this.pbxGuarantors.Location = new System.Drawing.Point(269, 3);
            this.pbxGuarantors.Name = "pbxGuarantors";
            this.pbxGuarantors.Size = new System.Drawing.Size(32, 32);
            this.pbxGuarantors.TabIndex = 5;
            this.pbxGuarantors.TabStop = false;
            this.pbxGuarantors.Tag = "თავდებები";
            this.pbxGuarantors.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.ECR_Import_32;
            this.pictureBox1.InitialImage = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxGuarantors;
            this.pictureBox1.Location = new System.Drawing.Point(307, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "აპარატში ჩაწერა";
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.ECR_Export_32;
            this.pictureBox2.InitialImage = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxGuarantors;
            this.pictureBox2.Location = new System.Drawing.Point(345, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "აპარატიდან წაკითხვა";
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.InitialImage = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxGuarantors;
            this.pictureBox3.Location = new System.Drawing.Point(383, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Tag = "Z რეპორტი";
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox2_Click_1);
            // 
            // cbxPorts
            // 
            this.cbxPorts.FormattingEnabled = true;
            this.cbxPorts.Location = new System.Drawing.Point(628, 33);
            this.cbxPorts.Name = "cbxPorts";
            this.cbxPorts.Size = new System.Drawing.Size(82, 21);
            this.cbxPorts.TabIndex = 4;
            // 
            // DailyPaymentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 496);
            this.Controls.Add(this.cbxPorts);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Name = "DailyPaymentsForm";
            this.Text = "დღევანდელი გადახდები";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GG_FormClosing);
            this.Load += new System.EventHandler(this.GG_Load);
            this.Shown += new System.EventHandler(this.GG_Shown);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.Controls.SetChildIndex(this.cbxPorts, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGuarantors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxSave;
        private System.Windows.Forms.PictureBox pbxOrders;
        private System.Windows.Forms.PictureBox pbxStats;
        private System.Windows.Forms.PictureBox pbxUpload;
        private System.Windows.Forms.PictureBox pbxGuarantors;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox cbxPorts;

    }
}