namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    partial class EnforcementForm
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
            this.cbxShowPassive = new System.Windows.Forms.CheckBox();
            this.tbxSearchString = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cbxClosedLoans = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.tbxSearchString);
            this.splitContainer1.Size = new System.Drawing.Size(779, 453);
            this.splitContainer1.SplitterDistance = 41;
            // 
            // splitContainer2
            // 
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.cbxShowPassive);
            this.splitContainer2.Panel2.Controls.Add(this.cbxClosedLoans);
            this.splitContainer2.Size = new System.Drawing.Size(779, 408);
            this.splitContainer2.SplitterDistance = 368;
            // 
            // pbxRefresh
            // 
            this.pbxRefresh.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxOrders;
            this.pbxRefresh.Tag = "რედაქტირება";
            this.pbxRefresh.Click += new System.EventHandler(this.pbxRefresh_Click);
            // 
            // pbxFilter
            // 
            this.pbxFilter.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxEditOrder;
            this.pbxFilter.Tag = "მორიგება";
            this.pbxFilter.Click += new System.EventHandler(this.pbxFilter_Click);
            // 
            // pbxExport
            // 
            this.pbxExport.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxGuarantors;
            this.pbxExport.Tag = "თავდებები";
            this.pbxExport.Click += new System.EventHandler(this.pbxExport_Click);
            // 
            // cbxShowPassive
            // 
            this.cbxShowPassive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxShowPassive.AutoSize = true;
            this.cbxShowPassive.Location = new System.Drawing.Point(631, 8);
            this.cbxShowPassive.Name = "cbxShowPassive";
            this.cbxShowPassive.Size = new System.Drawing.Size(140, 17);
            this.cbxShowPassive.TabIndex = 2;
            this.cbxShowPassive.Text = "დახურული სესხები";
            this.cbxShowPassive.UseVisualStyleBackColor = true;
            this.cbxShowPassive.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tbxSearchString
            // 
            this.tbxSearchString.Location = new System.Drawing.Point(500, 14);
            this.tbxSearchString.Name = "tbxSearchString";
            this.tbxSearchString.Size = new System.Drawing.Size(190, 20);
            this.tbxSearchString.TabIndex = 1;
            this.tbxSearchString.TextChanged += new System.EventHandler(this.tbxSearchString_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(696, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "ძებნა";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbxClosedLoans
            // 
            this.cbxClosedLoans.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxClosedLoans.AutoSize = true;
            this.cbxClosedLoans.Location = new System.Drawing.Point(317, 8);
            this.cbxClosedLoans.Name = "cbxClosedLoans";
            this.cbxClosedLoans.Size = new System.Drawing.Size(167, 17);
            this.cbxClosedLoans.TabIndex = 3;
            this.cbxClosedLoans.Text = "დასრულებული საქმეები";
            this.cbxClosedLoans.UseVisualStyleBackColor = true;
            this.cbxClosedLoans.CheckedChanged += new System.EventHandler(this.cbxClosedLoans_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxExport;
            this.pictureBox1.Location = new System.Drawing.Point(117, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "ექსპორტი";
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // EnforcementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 533);
            this.Name = "EnforcementForm";
            this.Text = "პრობლემური სესხები";
            this.Load += new System.EventHandler(this.EnforcementForm_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbxShowPassive;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbxSearchString;
        private System.Windows.Forms.CheckBox cbxClosedLoans;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}