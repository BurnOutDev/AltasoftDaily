namespace AltasoftDaily.UserInterface.WindowsForms
{
    partial class DailyForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridDaily = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSumName = new System.Windows.Forms.Label();
            this.lblPmtSumName = new System.Windows.Forms.Label();
            this.lblSum = new System.Windows.Forms.Label();
            this.lblPmtSum = new System.Windows.Forms.Label();
            this.lblPmtCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCountName = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.tsDailyForm = new System.Windows.Forms.ToolStrip();
            this.tsBtnGenerateTaxOrders = new System.Windows.Forms.ToolStripButton();
            this.tsBtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExport = new System.Windows.Forms.ToolStripButton();
            this.tsBtnStats = new System.Windows.Forms.ToolStripButton();
            this.tsBtnSubmit = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDaily)).BeginInit();
            this.panel1.SuspendLayout();
            this.tsDailyForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gridDaily, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tsDailyForm, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(798, 428);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Click += new System.EventHandler(this.btnStats_Click_1);
            // 
            // gridDaily
            // 
            this.gridDaily.AllowUserToAddRows = false;
            this.gridDaily.AllowUserToDeleteRows = false;
            this.gridDaily.AllowUserToOrderColumns = true;
            this.gridDaily.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDaily.Location = new System.Drawing.Point(3, 28);
            this.gridDaily.Name = "gridDaily";
            this.gridDaily.Size = new System.Drawing.Size(792, 362);
            this.gridDaily.TabIndex = 0;
            this.gridDaily.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDaily_CellDoubleClick);
            this.gridDaily.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDaily_CellEndEdit);
            this.gridDaily.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridDaily_ColumnHeaderMouseClick);
            this.gridDaily.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridDaily_ColumnHeaderMouseDoubleClick);
            this.gridDaily.SelectionChanged += new System.EventHandler(this.gridDaily_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSumName);
            this.panel1.Controls.Add(this.lblPmtSumName);
            this.panel1.Controls.Add(this.lblSum);
            this.panel1.Controls.Add(this.lblPmtSum);
            this.panel1.Controls.Add(this.lblPmtCount);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblCountName);
            this.panel1.Controls.Add(this.lblCount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 396);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 29);
            this.panel1.TabIndex = 1;
            // 
            // lblSumName
            // 
            this.lblSumName.AutoSize = true;
            this.lblSumName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSumName.Location = new System.Drawing.Point(413, 0);
            this.lblSumName.Name = "lblSumName";
            this.lblSumName.Size = new System.Drawing.Size(45, 21);
            this.lblSumName.TabIndex = 2;
            this.lblSumName.Text = "Sum:";
            // 
            // lblPmtSumName
            // 
            this.lblPmtSumName.AutoSize = true;
            this.lblPmtSumName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPmtSumName.Location = new System.Drawing.Point(3, 0);
            this.lblPmtSumName.Name = "lblPmtSumName";
            this.lblPmtSumName.Size = new System.Drawing.Size(108, 21);
            this.lblPmtSumName.TabIndex = 2;
            this.lblPmtSumName.Text = "Payment sum:";
            // 
            // lblSum
            // 
            this.lblSum.AutoSize = true;
            this.lblSum.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSum.Location = new System.Drawing.Point(464, 0);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(0, 21);
            this.lblSum.TabIndex = 2;
            // 
            // lblPmtSum
            // 
            this.lblPmtSum.AutoSize = true;
            this.lblPmtSum.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPmtSum.Location = new System.Drawing.Point(116, 0);
            this.lblPmtSum.Name = "lblPmtSum";
            this.lblPmtSum.Size = new System.Drawing.Size(0, 21);
            this.lblPmtSum.TabIndex = 2;
            // 
            // lblPmtCount
            // 
            this.lblPmtCount.AutoSize = true;
            this.lblPmtCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPmtCount.Location = new System.Drawing.Point(326, 0);
            this.lblPmtCount.Name = "lblPmtCount";
            this.lblPmtCount.Size = new System.Drawing.Size(0, 21);
            this.lblPmtCount.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(537, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Count:";
            // 
            // lblCountName
            // 
            this.lblCountName.AutoSize = true;
            this.lblCountName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCountName.Location = new System.Drawing.Point(204, 0);
            this.lblCountName.Name = "lblCountName";
            this.lblCountName.Size = new System.Drawing.Size(117, 21);
            this.lblCountName.TabIndex = 2;
            this.lblCountName.Text = "Payment count:";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCount.Location = new System.Drawing.Point(598, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 21);
            this.lblCount.TabIndex = 2;
            // 
            // tsDailyForm
            // 
            this.tsDailyForm.AutoSize = false;
            this.tsDailyForm.BackColor = System.Drawing.Color.White;
            this.tsDailyForm.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsDailyForm.ImageScalingSize = new System.Drawing.Size(20, 16);
            this.tsDailyForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnGenerateTaxOrders,
            this.tsBtnSave,
            this.tsBtnExport,
            this.tsBtnStats,
            this.tsBtnSubmit});
            this.tsDailyForm.Location = new System.Drawing.Point(0, 0);
            this.tsDailyForm.Name = "tsDailyForm";
            this.tsDailyForm.Size = new System.Drawing.Size(798, 25);
            this.tsDailyForm.TabIndex = 2;
            this.tsDailyForm.Text = "toolStrip1";
            // 
            // tsBtnGenerateTaxOrders
            // 
            this.tsBtnGenerateTaxOrders.AutoSize = false;
            this.tsBtnGenerateTaxOrders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnGenerateTaxOrders.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.GenerateOrders;
            this.tsBtnGenerateTaxOrders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnGenerateTaxOrders.Name = "tsBtnGenerateTaxOrders";
            this.tsBtnGenerateTaxOrders.Size = new System.Drawing.Size(23, 22);
            this.tsBtnGenerateTaxOrders.Text = "სშო გენერაცია";
            this.tsBtnGenerateTaxOrders.ToolTipText = "სშო გენერაცია";
            this.tsBtnGenerateTaxOrders.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // tsBtnSave
            // 
            this.tsBtnSave.AutoSize = false;
            this.tsBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSave.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.Save;
            this.tsBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSave.Name = "tsBtnSave";
            this.tsBtnSave.Size = new System.Drawing.Size(23, 22);
            this.tsBtnSave.Text = "შენახვა";
            this.tsBtnSave.ToolTipText = "შენახვა";
            this.tsBtnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // tsBtnExport
            // 
            this.tsBtnExport.AutoSize = false;
            this.tsBtnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnExport.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.ExportXLS;
            this.tsBtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnExport.Name = "tsBtnExport";
            this.tsBtnExport.Size = new System.Drawing.Size(23, 22);
            this.tsBtnExport.Text = "ექსპორტი";
            this.tsBtnExport.ToolTipText = "ექსპორტი";
            this.tsBtnExport.Click += new System.EventHandler(this.btnStats_Click_1);
            // 
            // tsBtnStats
            // 
            this.tsBtnStats.AutoSize = false;
            this.tsBtnStats.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnStats.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.Statistics;
            this.tsBtnStats.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnStats.Name = "tsBtnStats";
            this.tsBtnStats.Size = new System.Drawing.Size(23, 22);
            this.tsBtnStats.Text = "სტატისტიკა";
            this.tsBtnStats.ToolTipText = "სტატისტიკა";
            this.tsBtnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // tsBtnSubmit
            // 
            this.tsBtnSubmit.AutoSize = false;
            this.tsBtnSubmit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSubmit.Image = global::AltasoftDaily.UserInterface.WindowsForms.Properties.Resources.SubmitOrders;
            this.tsBtnSubmit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSubmit.Name = "tsBtnSubmit";
            this.tsBtnSubmit.Size = new System.Drawing.Size(23, 22);
            this.tsBtnSubmit.Text = "ატვირთვა";
            this.tsBtnSubmit.ToolTipText = "ატვირთვა";
            this.tsBtnSubmit.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // DailyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 508);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DailyForm";
            this.Text = "Daily";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DailyForm_FormClosing);
            this.Load += new System.EventHandler(this.DailyForm_Load);
            this.Shown += new System.EventHandler(this.DailyForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDaily)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsDailyForm.ResumeLayout(false);
            this.tsDailyForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView gridDaily;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSum;
        private System.Windows.Forms.Label lblPmtCount;
        private System.Windows.Forms.ToolStrip tsDailyForm;
        private System.Windows.Forms.ToolStripButton tsBtnGenerateTaxOrders;
        private System.Windows.Forms.ToolStripButton tsBtnSave;
        private System.Windows.Forms.ToolStripButton tsBtnExport;
        private System.Windows.Forms.ToolStripButton tsBtnStats;
        private System.Windows.Forms.Label lblSumName;
        private System.Windows.Forms.Label lblCountName;
        private System.Windows.Forms.ToolStripButton tsBtnSubmit;
        private System.Windows.Forms.Label lblPmtSumName;
        private System.Windows.Forms.Label lblPmtSum;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label2;
    }
}

