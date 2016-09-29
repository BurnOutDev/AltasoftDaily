namespace AltasoftDaily.UserInterface.WindowsForms
{
    partial class CollateralForm
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
            this.tbxType = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblAgreement = new System.Windows.Forms.Label();
            this.tbxAgreement = new System.Windows.Forms.TextBox();
            this.lblOwner = new System.Windows.Forms.Label();
            this.tbxOwner = new System.Windows.Forms.TextBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.tbxBranch = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbxIdNumber = new System.Windows.Forms.TextBox();
            this.tbxAddressLegal = new System.Windows.Forms.TextBox();
            this.lblIdNumber = new System.Windows.Forms.Label();
            this.lblAddressLegal = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridRelations = new System.Windows.Forms.DataGridView();
            this.lblPhone = new System.Windows.Forms.Label();
            this.tbxPhone = new System.Windows.Forms.TextBox();
            this.lblMobile = new System.Windows.Forms.Label();
            this.tbxMobile = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRelations)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxType
            // 
            this.tbxType.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxType.Location = new System.Drawing.Point(27, 77);
            this.tbxType.Name = "tbxType";
            this.tbxType.Size = new System.Drawing.Size(143, 22);
            this.tbxType.TabIndex = 23;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblType.Location = new System.Drawing.Point(24, 60);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(76, 14);
            this.lblType.TabIndex = 22;
            this.lblType.Text = "გირაოს ტიპი";
            // 
            // lblAgreement
            // 
            this.lblAgreement.AutoSize = true;
            this.lblAgreement.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblAgreement.Location = new System.Drawing.Point(173, 60);
            this.lblAgreement.Name = "lblAgreement";
            this.lblAgreement.Size = new System.Drawing.Size(92, 14);
            this.lblAgreement.TabIndex = 22;
            this.lblAgreement.Text = "ხელშეკრულება";
            // 
            // tbxAgreement
            // 
            this.tbxAgreement.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxAgreement.Location = new System.Drawing.Point(176, 77);
            this.tbxAgreement.Name = "tbxAgreement";
            this.tbxAgreement.Size = new System.Drawing.Size(143, 22);
            this.tbxAgreement.TabIndex = 23;
            // 
            // lblOwner
            // 
            this.lblOwner.AutoSize = true;
            this.lblOwner.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblOwner.Location = new System.Drawing.Point(24, 102);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new System.Drawing.Size(67, 14);
            this.lblOwner.TabIndex = 22;
            this.lblOwner.Text = "მესაკუთრე";
            // 
            // tbxOwner
            // 
            this.tbxOwner.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxOwner.Location = new System.Drawing.Point(27, 119);
            this.tbxOwner.Name = "tbxOwner";
            this.tbxOwner.Size = new System.Drawing.Size(143, 22);
            this.tbxOwner.TabIndex = 23;
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblBranch.Location = new System.Drawing.Point(173, 102);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(60, 14);
            this.lblBranch.TabIndex = 22;
            this.lblBranch.Text = "ფილიალი";
            // 
            // tbxBranch
            // 
            this.tbxBranch.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxBranch.Location = new System.Drawing.Point(176, 119);
            this.tbxBranch.Name = "tbxBranch";
            this.tbxBranch.Size = new System.Drawing.Size(143, 22);
            this.tbxBranch.TabIndex = 23;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(20, 147);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(531, 318);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbxMobile);
            this.tabPage1.Controls.Add(this.tbxPhone);
            this.tabPage1.Controls.Add(this.tbxIdNumber);
            this.tabPage1.Controls.Add(this.tbxAddressLegal);
            this.tabPage1.Controls.Add(this.lblMobile);
            this.tabPage1.Controls.Add(this.lblPhone);
            this.tabPage1.Controls.Add(this.lblIdNumber);
            this.tabPage1.Controls.Add(this.lblAddressLegal);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(523, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "გირაო";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbxIdNumber
            // 
            this.tbxIdNumber.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxIdNumber.Location = new System.Drawing.Point(150, 34);
            this.tbxIdNumber.Name = "tbxIdNumber";
            this.tbxIdNumber.Size = new System.Drawing.Size(236, 22);
            this.tbxIdNumber.TabIndex = 19;
            // 
            // tbxAddressLegal
            // 
            this.tbxAddressLegal.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxAddressLegal.Location = new System.Drawing.Point(150, 6);
            this.tbxAddressLegal.Name = "tbxAddressLegal";
            this.tbxAddressLegal.Size = new System.Drawing.Size(236, 22);
            this.tbxAddressLegal.TabIndex = 19;
            // 
            // lblIdNumber
            // 
            this.lblIdNumber.AutoSize = true;
            this.lblIdNumber.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblIdNumber.Location = new System.Drawing.Point(7, 37);
            this.lblIdNumber.Name = "lblIdNumber";
            this.lblIdNumber.Size = new System.Drawing.Size(139, 14);
            this.lblIdNumber.TabIndex = 18;
            this.lblIdNumber.Text = "საიდ. კოდი/პირ. ნომერი";
            // 
            // lblAddressLegal
            // 
            this.lblAddressLegal.AutoSize = true;
            this.lblAddressLegal.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblAddressLegal.Location = new System.Drawing.Point(7, 9);
            this.lblAddressLegal.Name = "lblAddressLegal";
            this.lblAddressLegal.Size = new System.Drawing.Size(137, 14);
            this.lblAddressLegal.TabIndex = 18;
            this.lblAddressLegal.Text = "იურიდიული მისამართი";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridRelations);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(523, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "კავშირები";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridRelations
            // 
            this.gridRelations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRelations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRelations.Location = new System.Drawing.Point(3, 3);
            this.gridRelations.Name = "gridRelations";
            this.gridRelations.Size = new System.Drawing.Size(517, 286);
            this.gridRelations.TabIndex = 0;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblPhone.Location = new System.Drawing.Point(9, 65);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(80, 14);
            this.lblPhone.TabIndex = 18;
            this.lblPhone.Text = "ტელ. ნომერი";
            // 
            // tbxPhone
            // 
            this.tbxPhone.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxPhone.Location = new System.Drawing.Point(150, 62);
            this.tbxPhone.Name = "tbxPhone";
            this.tbxPhone.Size = new System.Drawing.Size(236, 22);
            this.tbxPhone.TabIndex = 19;
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblMobile.Location = new System.Drawing.Point(9, 93);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(76, 14);
            this.lblMobile.TabIndex = 18;
            this.lblMobile.Text = "მობ. ნომერი";
            // 
            // tbxMobile
            // 
            this.tbxMobile.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxMobile.Location = new System.Drawing.Point(150, 90);
            this.tbxMobile.Name = "tbxMobile";
            this.tbxMobile.Size = new System.Drawing.Size(236, 22);
            this.tbxMobile.TabIndex = 19;
            // 
            // CollateralForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 485);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tbxBranch);
            this.Controls.Add(this.tbxAgreement);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.lblAgreement);
            this.Controls.Add(this.tbxOwner);
            this.Controls.Add(this.tbxType);
            this.Controls.Add(this.lblOwner);
            this.Controls.Add(this.lblType);
            this.Name = "CollateralForm";
            this.Text = "გირაო - ნახვა";
            this.Load += new System.EventHandler(this.CollateralForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRelations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblAgreement;
        private System.Windows.Forms.TextBox tbxAgreement;
        private System.Windows.Forms.Label lblOwner;
        private System.Windows.Forms.TextBox tbxOwner;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.TextBox tbxBranch;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbxIdNumber;
        private System.Windows.Forms.TextBox tbxAddressLegal;
        private System.Windows.Forms.Label lblIdNumber;
        private System.Windows.Forms.Label lblAddressLegal;
        private System.Windows.Forms.DataGridView gridRelations;
        private System.Windows.Forms.TextBox tbxMobile;
        private System.Windows.Forms.TextBox tbxPhone;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.Label lblPhone;
    }
}