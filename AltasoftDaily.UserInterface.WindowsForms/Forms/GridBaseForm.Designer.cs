namespace AltasoftDaily.UserInterface.WindowsForms
{
    partial class GridBaseForm
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
            this.lblSum = new System.Windows.Forms.Label();
            this.lblSumValue = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblCountValue = new System.Windows.Forms.Label();
            this.gridData = new ADGV.AdvancedDataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pbxRefresh = new System.Windows.Forms.PictureBox();
            this.pbxFilter = new System.Windows.Forms.PictureBox();
            this.pbxExport = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSum
            // 
            this.lblSum.AutoSize = true;
            this.lblSum.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSum.Location = new System.Drawing.Point(3, 2);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(47, 19);
            this.lblSum.TabIndex = 0;
            this.lblSum.Text = "ჯამი:";
            // 
            // lblSumValue
            // 
            this.lblSumValue.AutoSize = true;
            this.lblSumValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSumValue.Location = new System.Drawing.Point(56, 2);
            this.lblSumValue.Name = "lblSumValue";
            this.lblSumValue.Size = new System.Drawing.Size(0, 19);
            this.lblSumValue.TabIndex = 1;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCount.Location = new System.Drawing.Point(119, 2);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(95, 19);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "რაოდენობა:";
            // 
            // lblCountValue
            // 
            this.lblCountValue.AutoSize = true;
            this.lblCountValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCountValue.Location = new System.Drawing.Point(220, 2);
            this.lblCountValue.Name = "lblCountValue";
            this.lblCountValue.Size = new System.Drawing.Size(0, 19);
            this.lblCountValue.TabIndex = 1;
            // 
            // gridData
            // 
            this.gridData.AllowUserToAddRows = false;
            this.gridData.AllowUserToDeleteRows = false;
            this.gridData.AutoGenerateContextFilters = true;
            this.gridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridData.DateWithTime = false;
            this.gridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData.Location = new System.Drawing.Point(0, 0);
            this.gridData.Name = "gridData";
            this.gridData.Size = new System.Drawing.Size(690, 343);
            this.gridData.TabIndex = 2;
            this.gridData.TimeFilter = false;
            this.gridData.SortStringChanged += new System.EventHandler(this.gridData_SortStringChanged);
            this.gridData.FilterStringChanged += new System.EventHandler(this.gridData_FilterStringChanged);
            this.gridData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_CellDoubleClick);
            this.gridData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridData_ColumnHeaderMouseClick);
            this.gridData.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridData_ColumnHeaderMouseDoubleClick);
            this.gridData.SelectionChanged += new System.EventHandler(this.gridData_SelectionChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.Controls.Add(this.pbxRefresh);
            this.splitContainer1.Panel1.Controls.Add(this.pbxFilter);
            this.splitContainer1.Panel1.Controls.Add(this.pbxExport);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(690, 416);
            this.splitContainer1.SplitterDistance = 38;
            this.splitContainer1.TabIndex = 3;
            // 
            // pbxRefresh
            // 
            this.pbxRefresh.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxRefresh;
            this.pbxRefresh.Location = new System.Drawing.Point(3, 3);
            this.pbxRefresh.Name = "pbxRefresh";
            this.pbxRefresh.Size = new System.Drawing.Size(32, 32);
            this.pbxRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxRefresh.TabIndex = 0;
            this.pbxRefresh.TabStop = false;
            this.pbxRefresh.Tag = "განახლება";
            this.pbxRefresh.MouseHover += new System.EventHandler(this.pbx_MouseHover);
            // 
            // pbxFilter
            // 
            this.pbxFilter.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxFilter;
            this.pbxFilter.Location = new System.Drawing.Point(41, 3);
            this.pbxFilter.Name = "pbxFilter";
            this.pbxFilter.Size = new System.Drawing.Size(32, 32);
            this.pbxFilter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxFilter.TabIndex = 0;
            this.pbxFilter.TabStop = false;
            this.pbxFilter.Tag = "გაფილტრვა";
            // 
            // pbxExport
            // 
            this.pbxExport.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.pbxExport;
            this.pbxExport.Location = new System.Drawing.Point(79, 3);
            this.pbxExport.Name = "pbxExport";
            this.pbxExport.Size = new System.Drawing.Size(32, 32);
            this.pbxExport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxExport.TabIndex = 0;
            this.pbxExport.TabStop = false;
            this.pbxExport.Tag = "ექსპორტი";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gridData);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblCountValue);
            this.splitContainer2.Panel2.Controls.Add(this.lblSum);
            this.splitContainer2.Panel2.Controls.Add(this.lblCount);
            this.splitContainer2.Panel2.Controls.Add(this.lblSumValue);
            this.splitContainer2.Size = new System.Drawing.Size(690, 374);
            this.splitContainer2.SplitterDistance = 343;
            this.splitContainer2.TabIndex = 0;
            // 
            // GridBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 496);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GridBaseForm";
            this.Text = "GridBaseForm";
            this.Load += new System.EventHandler(this.GridBaseForm_Load);
            this.Shown += new System.EventHandler(this.GridBaseForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxExport)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label lblSum;
        protected System.Windows.Forms.Label lblSumValue;
        protected System.Windows.Forms.Label lblCount;
        protected System.Windows.Forms.Label lblCountValue;
        protected ADGV.AdvancedDataGridView gridData;
        protected System.Windows.Forms.SplitContainer splitContainer1;
        protected System.Windows.Forms.SplitContainer splitContainer2;
        public System.Windows.Forms.PictureBox pbxRefresh;
        public System.Windows.Forms.PictureBox pbxFilter;
        public System.Windows.Forms.PictureBox pbxExport;

    }
}