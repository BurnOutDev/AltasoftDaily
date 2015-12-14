namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    partial class ChangePasswordForm
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
            this.btnOk = new MetroFramework.Controls.MetroButton();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.ctrlChangePassword = new AltasoftDaily.UserInterface.WindowsForms.ChangePassword();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(25, 174);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(215, 29);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "შეცვლა";
            this.btnOk.UseSelectable = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(25, 209);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(215, 29);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "გაუქმება";
            this.btnCancel.UseSelectable = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ctrlChangePassword
            // 
            this.ctrlChangePassword.Location = new System.Drawing.Point(22, 63);
            this.ctrlChangePassword.Name = "ctrlChangePassword";
            this.ctrlChangePassword.PasswordNew = "";
            this.ctrlChangePassword.PasswordNewRe = "";
            this.ctrlChangePassword.PasswordOld = "";
            this.ctrlChangePassword.Size = new System.Drawing.Size(221, 105);
            this.ctrlChangePassword.TabIndex = 0;
            this.ctrlChangePassword.UseSelectable = true;
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 262);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.ctrlChangePassword);
            this.Name = "ChangePasswordForm";
            this.Text = "პაროლის შეცვლა";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Load += new System.EventHandler(this.ChangePasswordForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ChangePassword ctrlChangePassword;
        private MetroFramework.Controls.MetroButton btnOk;
        private MetroFramework.Controls.MetroButton btnCancel;

    }
}