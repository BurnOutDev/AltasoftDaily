namespace AltasoftDaily.UserInterface.WindowsForms
{
    partial class StatsForm
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
            this.gridStats = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridStats)).BeginInit();
            this.SuspendLayout();
            // 
            // gridStats
            // 
            this.gridStats.AllowUserToAddRows = false;
            this.gridStats.AllowUserToDeleteRows = false;
            this.gridStats.AllowUserToOrderColumns = true;
            this.gridStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridStats.Location = new System.Drawing.Point(0, 0);
            this.gridStats.Name = "gridStats";
            this.gridStats.ReadOnly = true;
            this.gridStats.Size = new System.Drawing.Size(460, 198);
            this.gridStats.TabIndex = 0;
            // 
            // StatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 198);
            this.Controls.Add(this.gridStats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StatsForm";
            this.Text = "StatsForm";
            this.Load += new System.EventHandler(this.StatsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridStats)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridStats;
    }
}