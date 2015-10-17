namespace AltasoftDaily.UserInterface.WindowsForms
{
    partial class AuthenticationForm
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
            this.tbxUsername = new MetroFramework.Controls.MetroTextBox();
            this.tbxPassword = new MetroFramework.Controls.MetroTextBox();
            this.btnEnter = new MetroFramework.Controls.MetroButton();
            this.btnExit = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // tbxUsername
            // 
            this.tbxUsername.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbxUsername.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.tbxUsername.Lines = new string[0];
            this.tbxUsername.Location = new System.Drawing.Point(23, 95);
            this.tbxUsername.MaxLength = 32767;
            this.tbxUsername.Name = "tbxUsername";
            this.tbxUsername.PasswordChar = '\0';
            this.tbxUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxUsername.SelectedText = "";
            this.tbxUsername.Size = new System.Drawing.Size(215, 29);
            this.tbxUsername.TabIndex = 0;
            this.tbxUsername.UseSelectable = true;
            // 
            // tbxPassword
            // 
            this.tbxPassword.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbxPassword.Lines = new string[0];
            this.tbxPassword.Location = new System.Drawing.Point(23, 130);
            this.tbxPassword.MaxLength = 32767;
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '●';
            this.tbxPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxPassword.SelectedText = "";
            this.tbxPassword.Size = new System.Drawing.Size(215, 29);
            this.tbxPassword.TabIndex = 1;
            this.tbxPassword.UseSelectable = true;
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(23, 165);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(215, 29);
            this.btnEnter.TabIndex = 2;
            this.btnEnter.Text = "შესვლა";
            this.btnEnter.UseSelectable = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(23, 200);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(215, 29);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "გაუქმება";
            this.btnExit.UseSelectable = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // AuthenticationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 251);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.tbxPassword);
            this.Controls.Add(this.tbxUsername);
            this.Name = "AuthenticationForm";
            this.Text = "ავტენტიფიკაცია";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox tbxUsername;
        private MetroFramework.Controls.MetroTextBox tbxPassword;
        private MetroFramework.Controls.MetroButton btnEnter;
        private MetroFramework.Controls.MetroButton btnExit;
    }
}