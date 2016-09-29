namespace AltasoftDaily.UserInterface.WindowsForms
{
    partial class ChangePassword
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbxNewPassword = new MetroFramework.Controls.MetroTextBox();
            this.tbxOldPassword = new MetroFramework.Controls.MetroTextBox();
            this.tbxNewPasswordRe = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // tbxNewPassword
            // 
            this.tbxNewPassword.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbxNewPassword.Lines = new string[0];
            this.tbxNewPassword.Location = new System.Drawing.Point(3, 38);
            this.tbxNewPassword.MaxLength = 32767;
            this.tbxNewPassword.Name = "tbxNewPassword";
            this.tbxNewPassword.PasswordChar = '●';
            this.tbxNewPassword.PromptText = "ახალი პაროლი";
            this.tbxNewPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxNewPassword.SelectedText = "";
            this.tbxNewPassword.Size = new System.Drawing.Size(215, 29);
            this.tbxNewPassword.TabIndex = 1;
            this.tbxNewPassword.UseSelectable = true;
            // 
            // tbxOldPassword
            // 
            this.tbxOldPassword.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbxOldPassword.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbxOldPassword.Lines = new string[0];
            this.tbxOldPassword.Location = new System.Drawing.Point(3, 3);
            this.tbxOldPassword.MaxLength = 32767;
            this.tbxOldPassword.Name = "tbxOldPassword";
            this.tbxOldPassword.PasswordChar = '\0';
            this.tbxOldPassword.PromptText = "ძველი პაროლი";
            this.tbxOldPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxOldPassword.SelectedText = "";
            this.tbxOldPassword.Size = new System.Drawing.Size(215, 29);
            this.tbxOldPassword.TabIndex = 0;
            this.tbxOldPassword.UseSelectable = true;
            // 
            // tbxNewPasswordRe
            // 
            this.tbxNewPasswordRe.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbxNewPasswordRe.Lines = new string[0];
            this.tbxNewPasswordRe.Location = new System.Drawing.Point(3, 73);
            this.tbxNewPasswordRe.MaxLength = 32767;
            this.tbxNewPasswordRe.Name = "tbxNewPasswordRe";
            this.tbxNewPasswordRe.PasswordChar = '●';
            this.tbxNewPasswordRe.PromptText = "გაიმეორეთ";
            this.tbxNewPasswordRe.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxNewPasswordRe.SelectedText = "";
            this.tbxNewPasswordRe.Size = new System.Drawing.Size(215, 29);
            this.tbxNewPasswordRe.TabIndex = 2;
            this.tbxNewPasswordRe.UseSelectable = true;
            // 
            // ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxNewPasswordRe);
            this.Controls.Add(this.tbxNewPassword);
            this.Controls.Add(this.tbxOldPassword);
            this.Name = "ChangePassword";
            this.Size = new System.Drawing.Size(221, 105);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox tbxNewPassword;
        private MetroFramework.Controls.MetroTextBox tbxOldPassword;
        private MetroFramework.Controls.MetroTextBox tbxNewPasswordRe;



    }
}
