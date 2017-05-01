namespace AltasoftDaily.UserInterface.WindowsForms
{
    partial class ChoosePrinterForm
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
            this.cbxChoosePrinter = new MetroFramework.Controls.MetroComboBox();
            this.btnOk = new MetroFramework.Controls.MetroButton();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // cbxChoosePrinter
            // 
            this.cbxChoosePrinter.FormattingEnabled = true;
            this.cbxChoosePrinter.ItemHeight = 23;
            this.cbxChoosePrinter.Location = new System.Drawing.Point(23, 72);
            this.cbxChoosePrinter.Name = "cbxChoosePrinter";
            this.cbxChoosePrinter.Size = new System.Drawing.Size(396, 29);
            this.cbxChoosePrinter.TabIndex = 0;
            this.cbxChoosePrinter.UseSelectable = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(23, 119);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(186, 29);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "არჩევა";
            this.btnOk.UseSelectable = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(233, 119);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(186, 29);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "გაუქმება";
            this.btnCancel.UseSelectable = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ChoosePrinterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 171);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbxChoosePrinter);
            this.Name = "ChoosePrinterForm";
            this.Text = "აირჩიეთ პრინტერი";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox cbxChoosePrinter;
        private MetroFramework.Controls.MetroButton btnOk;
        private MetroFramework.Controls.MetroButton btnCancel;
    }
}