namespace AltasoftDaily.UserInterface.WindowsForms
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ინკასატორიToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.დეილიToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ინკასატორიToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(512, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ინკასატორიToolStripMenuItem
            // 
            this.ინკასატორიToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.დეილიToolStripMenuItem});
            this.ინკასატორიToolStripMenuItem.Name = "ინკასატორიToolStripMenuItem";
            this.ინკასატორიToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.ინკასატორიToolStripMenuItem.Text = "ინკასატორი";
            // 
            // დეილიToolStripMenuItem
            // 
            this.დეილიToolStripMenuItem.Name = "დეილიToolStripMenuItem";
            this.დეილიToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.დეილიToolStripMenuItem.Text = "დეილი";
            this.დეილიToolStripMenuItem.Click += new System.EventHandler(this.TmiDaily_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 389);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "AltasoftDaily";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ინკასატორიToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem დეილიToolStripMenuItem;
    }
}