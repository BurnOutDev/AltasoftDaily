namespace AltasoftDaily.UserInterface.WindowsForms
{
    partial class CustomerForm
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
            this.tabMain = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.gbxInspection = new System.Windows.Forms.GroupBox();
            this.dtpRegDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.lblRegDate = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.cbxCity = new System.Windows.Forms.ComboBox();
            this.gbxDocuments = new System.Windows.Forms.GroupBox();
            this.gridDocuments = new System.Windows.Forms.DataGridView();
            this.gbxClient = new System.Windows.Forms.GroupBox();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.cbxSubType = new System.Windows.Forms.ComboBox();
            this.lblSubType = new System.Windows.Forms.Label();
            this.rdoFemale = new System.Windows.Forms.RadioButton();
            this.rdoMale = new System.Windows.Forms.RadioButton();
            this.lblFathersName = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.tbxFathersName = new System.Windows.Forms.TextBox();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.tbxLastName = new System.Windows.Forms.TextBox();
            this.cbxIndustry = new System.Windows.Forms.ComboBox();
            this.cbxSegment = new System.Windows.Forms.ComboBox();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.lblIndustry = new System.Windows.Forms.Label();
            this.lblSegment = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cbxIsInsider = new System.Windows.Forms.CheckBox();
            this.chkIsPermanent = new System.Windows.Forms.CheckBox();
            this.cbxCitizenship = new System.Windows.Forms.ComboBox();
            this.cbxStatus = new System.Windows.Forms.ComboBox();
            this.tbxValidThru = new System.Windows.Forms.TextBox();
            this.tbxIssueDate = new System.Windows.Forms.TextBox();
            this.lblValidThu = new System.Windows.Forms.Label();
            this.lblIssueDate = new System.Windows.Forms.Label();
            this.lblIsPermanent = new System.Windows.Forms.Label();
            this.lblCitizenship = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.lblIsInsider = new System.Windows.Forms.Label();
            this.cbxAddressBusiness = new System.Windows.Forms.ComboBox();
            this.cbxAddressFact = new System.Windows.Forms.ComboBox();
            this.cbxAddress = new System.Windows.Forms.ComboBox();
            this.tbxAddressBusiness = new System.Windows.Forms.TextBox();
            this.tbxAddressFact = new System.Windows.Forms.TextBox();
            this.tbxAddress = new System.Windows.Forms.TextBox();
            this.lblAddressBusiness = new System.Windows.Forms.Label();
            this.lblAddressFact = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.tbxHome = new System.Windows.Forms.TextBox();
            this.tbxMobile = new System.Windows.Forms.TextBox();
            this.lblHome = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.gbxInspection.SuspendLayout();
            this.gbxDocuments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).BeginInit();
            this.gbxClient.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.groupBox1);
            this.tabMain.Controls.Add(this.gbxInspection);
            this.tabMain.Controls.Add(this.gbxDocuments);
            this.tabMain.Controls.Add(this.gbxClient);
            this.tabMain.Controls.Add(this.button1);
            this.tabMain.Controls.Add(this.cbxIsInsider);
            this.tabMain.Controls.Add(this.chkIsPermanent);
            this.tabMain.Controls.Add(this.cbxCitizenship);
            this.tabMain.Controls.Add(this.cbxStatus);
            this.tabMain.Controls.Add(this.tbxValidThru);
            this.tabMain.Controls.Add(this.tbxIssueDate);
            this.tabMain.Controls.Add(this.lblValidThu);
            this.tabMain.Controls.Add(this.lblIsInsider);
            this.tabMain.Controls.Add(this.lblIssueDate);
            this.tabMain.Controls.Add(this.lblIsPermanent);
            this.tabMain.Controls.Add(this.lblCitizenship);
            this.tabMain.Controls.Add(this.lblStatus);
            this.tabMain.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(614, 433);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "მთავარი";
            this.tabMain.UseVisualStyleBackColor = true;
            this.tabMain.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Location = new System.Drawing.Point(6, 357);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 100);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ინფორმაციის ხარისხი";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 21);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(172, 45);
            this.trackBar1.TabIndex = 0;
            // 
            // gbxInspection
            // 
            this.gbxInspection.Controls.Add(this.dtpRegDate);
            this.gbxInspection.Controls.Add(this.label2);
            this.gbxInspection.Controls.Add(this.textBox2);
            this.gbxInspection.Controls.Add(this.label3);
            this.gbxInspection.Controls.Add(this.textBox3);
            this.gbxInspection.Controls.Add(this.lblRegDate);
            this.gbxInspection.Controls.Add(this.lblCity);
            this.gbxInspection.Controls.Add(this.cbxCity);
            this.gbxInspection.Location = new System.Drawing.Point(6, 278);
            this.gbxInspection.Name = "gbxInspection";
            this.gbxInspection.Size = new System.Drawing.Size(602, 73);
            this.gbxInspection.TabIndex = 13;
            this.gbxInspection.TabStop = false;
            this.gbxInspection.Text = "საგადასახადო ინსპექცია";
            // 
            // dtpRegDate
            // 
            this.dtpRegDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRegDate.Location = new System.Drawing.Point(468, 15);
            this.dtpRegDate.Name = "dtpRegDate";
            this.dtpRegDate.Size = new System.Drawing.Size(127, 22);
            this.dtpRegDate.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "რეგისტრაციის ორგანო";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.textBox2.Location = new System.Drawing.Point(146, 15);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(198, 22);
            this.textBox2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "საგადასახადო კოდი";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.textBox3.Location = new System.Drawing.Point(146, 43);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(198, 22);
            this.textBox3.TabIndex = 7;
            // 
            // lblRegDate
            // 
            this.lblRegDate.AutoSize = true;
            this.lblRegDate.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblRegDate.Location = new System.Drawing.Point(350, 18);
            this.lblRegDate.Name = "lblRegDate";
            this.lblRegDate.Size = new System.Drawing.Size(78, 14);
            this.lblRegDate.TabIndex = 4;
            this.lblRegDate.Text = "რეგ. თარიღი";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblCity.Location = new System.Drawing.Point(348, 46);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(44, 14);
            this.lblCity.TabIndex = 4;
            this.lblCity.Text = "ქალაქი";
            // 
            // cbxCity
            // 
            this.cbxCity.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.cbxCity.FormattingEnabled = true;
            this.cbxCity.Items.AddRange(new object[] {
            "მამრ",
            "მდედრ"});
            this.cbxCity.Location = new System.Drawing.Point(433, 43);
            this.cbxCity.Name = "cbxCity";
            this.cbxCity.Size = new System.Drawing.Size(162, 22);
            this.cbxCity.TabIndex = 8;
            // 
            // gbxDocuments
            // 
            this.gbxDocuments.Controls.Add(this.gridDocuments);
            this.gbxDocuments.Location = new System.Drawing.Point(6, 169);
            this.gbxDocuments.Name = "gbxDocuments";
            this.gbxDocuments.Size = new System.Drawing.Size(602, 103);
            this.gbxDocuments.TabIndex = 12;
            this.gbxDocuments.TabStop = false;
            this.gbxDocuments.Text = "პასპორტები";
            // 
            // gridDocuments
            // 
            this.gridDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDocuments.Location = new System.Drawing.Point(3, 18);
            this.gridDocuments.Name = "gridDocuments";
            this.gridDocuments.ReadOnly = true;
            this.gridDocuments.Size = new System.Drawing.Size(596, 82);
            this.gridDocuments.TabIndex = 0;
            this.gridDocuments.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridDocuments_CellMouseDoubleClick);
            // 
            // gbxClient
            // 
            this.gbxClient.Controls.Add(this.dtpBirthDate);
            this.gbxClient.Controls.Add(this.lblBirthDate);
            this.gbxClient.Controls.Add(this.cbxSubType);
            this.gbxClient.Controls.Add(this.lblSubType);
            this.gbxClient.Controls.Add(this.rdoFemale);
            this.gbxClient.Controls.Add(this.rdoMale);
            this.gbxClient.Controls.Add(this.lblFathersName);
            this.gbxClient.Controls.Add(this.lblName);
            this.gbxClient.Controls.Add(this.lblLastName);
            this.gbxClient.Controls.Add(this.tbxFathersName);
            this.gbxClient.Controls.Add(this.tbxName);
            this.gbxClient.Controls.Add(this.tbxLastName);
            this.gbxClient.Controls.Add(this.cbxIndustry);
            this.gbxClient.Controls.Add(this.cbxSegment);
            this.gbxClient.Controls.Add(this.cbxType);
            this.gbxClient.Controls.Add(this.lblIndustry);
            this.gbxClient.Controls.Add(this.lblSegment);
            this.gbxClient.Controls.Add(this.lblType);
            this.gbxClient.Controls.Add(this.lblGender);
            this.gbxClient.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.gbxClient.Location = new System.Drawing.Point(6, 6);
            this.gbxClient.Name = "gbxClient";
            this.gbxClient.Size = new System.Drawing.Size(602, 157);
            this.gbxClient.TabIndex = 11;
            this.gbxClient.TabStop = false;
            this.gbxClient.Text = "ფიზიკური პირი";
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthDate.Location = new System.Drawing.Point(423, 49);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(173, 22);
            this.dtpBirthDate.TabIndex = 15;
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblBirthDate.Location = new System.Drawing.Point(305, 52);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(112, 14);
            this.lblBirthDate.TabIndex = 12;
            this.lblBirthDate.Text = "დაბადების თარიღი";
            // 
            // cbxSubType
            // 
            this.cbxSubType.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.cbxSubType.FormattingEnabled = true;
            this.cbxSubType.Items.AddRange(new object[] {
            "მამრ",
            "მდედრ"});
            this.cbxSubType.Location = new System.Drawing.Point(388, 77);
            this.cbxSubType.Name = "cbxSubType";
            this.cbxSubType.Size = new System.Drawing.Size(208, 22);
            this.cbxSubType.TabIndex = 14;
            // 
            // lblSubType
            // 
            this.lblSubType.AutoSize = true;
            this.lblSubType.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblSubType.Location = new System.Drawing.Point(303, 80);
            this.lblSubType.Name = "lblSubType";
            this.lblSubType.Size = new System.Drawing.Size(49, 14);
            this.lblSubType.TabIndex = 13;
            this.lblSubType.Text = "ქვეტიპი";
            // 
            // rdoFemale
            // 
            this.rdoFemale.AutoSize = true;
            this.rdoFemale.Location = new System.Drawing.Point(186, 134);
            this.rdoFemale.Name = "rdoFemale";
            this.rdoFemale.Size = new System.Drawing.Size(100, 18);
            this.rdoFemale.TabIndex = 10;
            this.rdoFemale.TabStop = true;
            this.rdoFemale.Text = "მდედრობითი";
            this.rdoFemale.UseVisualStyleBackColor = true;
            // 
            // rdoMale
            // 
            this.rdoMale.AutoSize = true;
            this.rdoMale.Location = new System.Drawing.Point(91, 134);
            this.rdoMale.Name = "rdoMale";
            this.rdoMale.Size = new System.Drawing.Size(87, 18);
            this.rdoMale.TabIndex = 9;
            this.rdoMale.TabStop = true;
            this.rdoMale.Text = "მამრობითი";
            this.rdoMale.UseVisualStyleBackColor = true;
            // 
            // lblFathersName
            // 
            this.lblFathersName.AutoSize = true;
            this.lblFathersName.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblFathersName.Location = new System.Drawing.Point(6, 52);
            this.lblFathersName.Name = "lblFathersName";
            this.lblFathersName.Size = new System.Drawing.Size(79, 14);
            this.lblFathersName.TabIndex = 4;
            this.lblFathersName.Text = "მამის სახელი";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblName.Location = new System.Drawing.Point(6, 24);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(47, 14);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "სახელი";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblLastName.Location = new System.Drawing.Point(305, 24);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(39, 14);
            this.lblLastName.TabIndex = 3;
            this.lblLastName.Text = "გვარი";
            // 
            // tbxFathersName
            // 
            this.tbxFathersName.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxFathersName.Location = new System.Drawing.Point(91, 49);
            this.tbxFathersName.Name = "tbxFathersName";
            this.tbxFathersName.Size = new System.Drawing.Size(208, 22);
            this.tbxFathersName.TabIndex = 7;
            // 
            // tbxName
            // 
            this.tbxName.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxName.Location = new System.Drawing.Point(91, 21);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(208, 22);
            this.tbxName.TabIndex = 7;
            // 
            // tbxLastName
            // 
            this.tbxLastName.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxLastName.Location = new System.Drawing.Point(387, 21);
            this.tbxLastName.Name = "tbxLastName";
            this.tbxLastName.Size = new System.Drawing.Size(208, 22);
            this.tbxLastName.TabIndex = 6;
            this.tbxLastName.Text = "ღო";
            // 
            // cbxIndustry
            // 
            this.cbxIndustry.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.cbxIndustry.FormattingEnabled = true;
            this.cbxIndustry.Items.AddRange(new object[] {
            "მამრ",
            "მდედრ"});
            this.cbxIndustry.Location = new System.Drawing.Point(387, 105);
            this.cbxIndustry.Name = "cbxIndustry";
            this.cbxIndustry.Size = new System.Drawing.Size(208, 22);
            this.cbxIndustry.TabIndex = 8;
            // 
            // cbxSegment
            // 
            this.cbxSegment.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.cbxSegment.FormattingEnabled = true;
            this.cbxSegment.Items.AddRange(new object[] {
            "მამრ",
            "მდედრ"});
            this.cbxSegment.Location = new System.Drawing.Point(91, 105);
            this.cbxSegment.Name = "cbxSegment";
            this.cbxSegment.Size = new System.Drawing.Size(208, 22);
            this.cbxSegment.TabIndex = 8;
            // 
            // cbxType
            // 
            this.cbxType.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Items.AddRange(new object[] {
            "NonEntrepreneur",
            "SoleProprietor",
            "Entrepreneur",
            "Taxpayer"});
            this.cbxType.Location = new System.Drawing.Point(91, 77);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(208, 22);
            this.cbxType.TabIndex = 8;
            // 
            // lblIndustry
            // 
            this.lblIndustry.AutoSize = true;
            this.lblIndustry.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblIndustry.Location = new System.Drawing.Point(302, 108);
            this.lblIndustry.Name = "lblIndustry";
            this.lblIndustry.Size = new System.Drawing.Size(42, 14);
            this.lblIndustry.TabIndex = 4;
            this.lblIndustry.Text = "დარგი";
            // 
            // lblSegment
            // 
            this.lblSegment.AutoSize = true;
            this.lblSegment.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblSegment.Location = new System.Drawing.Point(6, 108);
            this.lblSegment.Name = "lblSegment";
            this.lblSegment.Size = new System.Drawing.Size(57, 14);
            this.lblSegment.TabIndex = 4;
            this.lblSegment.Text = "სეგმენტი";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblType.Location = new System.Drawing.Point(6, 80);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(32, 14);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "ტიპი";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblGender.Location = new System.Drawing.Point(6, 136);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(36, 14);
            this.lblGender.TabIndex = 4;
            this.lblGender.Text = "სქესი";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.button1.Location = new System.Drawing.Point(407, 502);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 24);
            this.button1.TabIndex = 10;
            this.button1.Text = "ნახვა...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbxIsInsider
            // 
            this.cbxIsInsider.AutoSize = true;
            this.cbxIsInsider.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.cbxIsInsider.Location = new System.Drawing.Point(238, 534);
            this.cbxIsInsider.Name = "cbxIsInsider";
            this.cbxIsInsider.Size = new System.Drawing.Size(15, 14);
            this.cbxIsInsider.TabIndex = 9;
            this.cbxIsInsider.UseVisualStyleBackColor = true;
            // 
            // chkIsPermanent
            // 
            this.chkIsPermanent.AutoSize = true;
            this.chkIsPermanent.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.chkIsPermanent.Location = new System.Drawing.Point(563, 485);
            this.chkIsPermanent.Name = "chkIsPermanent";
            this.chkIsPermanent.Size = new System.Drawing.Size(15, 14);
            this.chkIsPermanent.TabIndex = 9;
            this.chkIsPermanent.UseVisualStyleBackColor = true;
            // 
            // cbxCitizenship
            // 
            this.cbxCitizenship.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.cbxCitizenship.FormattingEnabled = true;
            this.cbxCitizenship.Items.AddRange(new object[] {
            "მამრ",
            "მდედრ"});
            this.cbxCitizenship.Location = new System.Drawing.Point(485, 550);
            this.cbxCitizenship.Name = "cbxCitizenship";
            this.cbxCitizenship.Size = new System.Drawing.Size(96, 22);
            this.cbxCitizenship.TabIndex = 8;
            this.cbxCitizenship.SelectedIndexChanged += new System.EventHandler(this.comboBox7_SelectedIndexChanged);
            // 
            // cbxStatus
            // 
            this.cbxStatus.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.cbxStatus.FormattingEnabled = true;
            this.cbxStatus.Items.AddRange(new object[] {
            "მამრ",
            "მდედრ"});
            this.cbxStatus.Location = new System.Drawing.Point(407, 549);
            this.cbxStatus.Name = "cbxStatus";
            this.cbxStatus.Size = new System.Drawing.Size(72, 22);
            this.cbxStatus.TabIndex = 8;
            // 
            // tbxValidThru
            // 
            this.tbxValidThru.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxValidThru.Location = new System.Drawing.Point(471, 482);
            this.tbxValidThru.Name = "tbxValidThru";
            this.tbxValidThru.Size = new System.Drawing.Size(72, 22);
            this.tbxValidThru.TabIndex = 5;
            // 
            // tbxIssueDate
            // 
            this.tbxIssueDate.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxIssueDate.Location = new System.Drawing.Point(381, 482);
            this.tbxIssueDate.Name = "tbxIssueDate";
            this.tbxIssueDate.Size = new System.Drawing.Size(84, 22);
            this.tbxIssueDate.TabIndex = 5;
            // 
            // lblValidThu
            // 
            this.lblValidThu.AutoSize = true;
            this.lblValidThu.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblValidThu.Location = new System.Drawing.Point(467, 461);
            this.lblValidThu.Name = "lblValidThu";
            this.lblValidThu.Size = new System.Drawing.Size(64, 14);
            this.lblValidThu.TabIndex = 4;
            this.lblValidThu.Text = "მოქმ. ვადა";
            // 
            // lblIssueDate
            // 
            this.lblIssueDate.AutoSize = true;
            this.lblIssueDate.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblIssueDate.Location = new System.Drawing.Point(377, 460);
            this.lblIssueDate.Name = "lblIssueDate";
            this.lblIssueDate.Size = new System.Drawing.Size(72, 14);
            this.lblIssueDate.TabIndex = 4;
            this.lblIssueDate.Text = "გაც.თარიღი";
            this.lblIssueDate.Click += new System.EventHandler(this.label15_Click);
            // 
            // lblIsPermanent
            // 
            this.lblIsPermanent.AutoSize = true;
            this.lblIsPermanent.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblIsPermanent.Location = new System.Drawing.Point(548, 461);
            this.lblIsPermanent.Name = "lblIsPermanent";
            this.lblIsPermanent.Size = new System.Drawing.Size(44, 14);
            this.lblIsPermanent.TabIndex = 4;
            this.lblIsPermanent.Text = "უვადო";
            // 
            // lblCitizenship
            // 
            this.lblCitizenship.AutoSize = true;
            this.lblCitizenship.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblCitizenship.Location = new System.Drawing.Point(481, 527);
            this.lblCitizenship.Name = "lblCitizenship";
            this.lblCitizenship.Size = new System.Drawing.Size(79, 14);
            this.lblCitizenship.TabIndex = 4;
            this.lblCitizenship.Text = "მოქალაქეობა";
            this.lblCitizenship.Click += new System.EventHandler(this.label14_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblStatus.Location = new System.Drawing.Point(403, 529);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(54, 14);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "სტატუსი";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbxHome);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.tbxMobile);
            this.tabPage2.Controls.Add(this.lblHome);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.lblMobile);
            this.tabPage2.Controls.Add(this.cbxAddressBusiness);
            this.tabPage2.Controls.Add(this.cbxAddressFact);
            this.tabPage2.Controls.Add(this.cbxAddress);
            this.tabPage2.Controls.Add(this.tbxAddressBusiness);
            this.tabPage2.Controls.Add(this.tbxAddressFact);
            this.tabPage2.Controls.Add(this.tbxAddress);
            this.tabPage2.Controls.Add(this.lblAddressBusiness);
            this.tabPage2.Controls.Add(this.lblAddressFact);
            this.tabPage2.Controls.Add(this.lblAddress);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(614, 433);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "რეკვიზიტები";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(20, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(622, 459);
            this.tabControl1.TabIndex = 2;
            // 
            // lblIsInsider
            // 
            this.lblIsInsider.AutoSize = true;
            this.lblIsInsider.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblIsInsider.Location = new System.Drawing.Point(156, 530);
            this.lblIsInsider.Name = "lblIsInsider";
            this.lblIsInsider.Size = new System.Drawing.Size(66, 14);
            this.lblIsInsider.TabIndex = 4;
            this.lblIsInsider.Text = "ინსაიდერი";
            this.lblIsInsider.Click += new System.EventHandler(this.label15_Click);
            // 
            // cbxAddressBusiness
            // 
            this.cbxAddressBusiness.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.cbxAddressBusiness.FormattingEnabled = true;
            this.cbxAddressBusiness.Location = new System.Drawing.Point(7, 139);
            this.cbxAddressBusiness.Name = "cbxAddressBusiness";
            this.cbxAddressBusiness.Size = new System.Drawing.Size(106, 22);
            this.cbxAddressBusiness.TabIndex = 15;
            // 
            // cbxAddressFact
            // 
            this.cbxAddressFact.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.cbxAddressFact.FormattingEnabled = true;
            this.cbxAddressFact.Location = new System.Drawing.Point(7, 25);
            this.cbxAddressFact.Name = "cbxAddressFact";
            this.cbxAddressFact.Size = new System.Drawing.Size(106, 22);
            this.cbxAddressFact.TabIndex = 16;
            // 
            // cbxAddress
            // 
            this.cbxAddress.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.cbxAddress.FormattingEnabled = true;
            this.cbxAddress.Location = new System.Drawing.Point(7, 82);
            this.cbxAddress.Name = "cbxAddress";
            this.cbxAddress.Size = new System.Drawing.Size(106, 22);
            this.cbxAddress.TabIndex = 17;
            // 
            // tbxAddressBusiness
            // 
            this.tbxAddressBusiness.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxAddressBusiness.Location = new System.Drawing.Point(119, 139);
            this.tbxAddressBusiness.Name = "tbxAddressBusiness";
            this.tbxAddressBusiness.Size = new System.Drawing.Size(236, 22);
            this.tbxAddressBusiness.TabIndex = 12;
            // 
            // tbxAddressFact
            // 
            this.tbxAddressFact.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxAddressFact.Location = new System.Drawing.Point(119, 25);
            this.tbxAddressFact.Name = "tbxAddressFact";
            this.tbxAddressFact.Size = new System.Drawing.Size(236, 22);
            this.tbxAddressFact.TabIndex = 13;
            // 
            // tbxAddress
            // 
            this.tbxAddress.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxAddress.Location = new System.Drawing.Point(119, 82);
            this.tbxAddress.Name = "tbxAddress";
            this.tbxAddress.Size = new System.Drawing.Size(236, 22);
            this.tbxAddress.TabIndex = 14;
            // 
            // lblAddressBusiness
            // 
            this.lblAddressBusiness.AutoSize = true;
            this.lblAddressBusiness.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblAddressBusiness.Location = new System.Drawing.Point(3, 117);
            this.lblAddressBusiness.Name = "lblAddressBusiness";
            this.lblAddressBusiness.Size = new System.Drawing.Size(118, 14);
            this.lblAddressBusiness.TabIndex = 9;
            this.lblAddressBusiness.Text = "ბიზნესის მისამართი";
            // 
            // lblAddressFact
            // 
            this.lblAddressFact.AutoSize = true;
            this.lblAddressFact.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblAddressFact.Location = new System.Drawing.Point(3, 3);
            this.lblAddressFact.Name = "lblAddressFact";
            this.lblAddressFact.Size = new System.Drawing.Size(123, 14);
            this.lblAddressFact.TabIndex = 10;
            this.lblAddressFact.Text = "ფაქტიური მისამართი";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblAddress.Location = new System.Drawing.Point(3, 60);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(137, 14);
            this.lblAddress.TabIndex = 11;
            this.lblAddress.Text = "იურიდიული მისამართი";
            // 
            // tbxHome
            // 
            this.tbxHome.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxHome.Location = new System.Drawing.Point(192, 225);
            this.tbxHome.Name = "tbxHome";
            this.tbxHome.Size = new System.Drawing.Size(167, 22);
            this.tbxHome.TabIndex = 20;
            // 
            // tbxMobile
            // 
            this.tbxMobile.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.tbxMobile.Location = new System.Drawing.Point(7, 225);
            this.tbxMobile.Name = "tbxMobile";
            this.tbxMobile.Size = new System.Drawing.Size(179, 22);
            this.tbxMobile.TabIndex = 21;
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblHome.Location = new System.Drawing.Point(189, 208);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(110, 14);
            this.lblHome.TabIndex = 18;
            this.lblHome.Text = "სახლის ტელეფონი";
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.lblMobile.Location = new System.Drawing.Point(3, 208);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(94, 14);
            this.lblMobile.TabIndex = 19;
            this.lblMobile.Text = "მობ. ტელეფონი";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.label1.Location = new System.Drawing.Point(361, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 14);
            this.label1.TabIndex = 19;
            this.label1.Text = "ელ. ფოსტა";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Sylfaen", 8.25F);
            this.textBox1.Location = new System.Drawing.Point(365, 225);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(179, 22);
            this.textBox1.TabIndex = 21;
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 539);
            this.Controls.Add(this.tabControl1);
            this.Name = "CustomerForm";
            this.Text = "კლიენტი ";
            this.Load += new System.EventHandler(this.CustomersForm_Load);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.gbxInspection.ResumeLayout(false);
            this.gbxInspection.PerformLayout();
            this.gbxDocuments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).EndInit();
            this.gbxClient.ResumeLayout(false);
            this.gbxClient.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbxIsInsider;
        private System.Windows.Forms.CheckBox chkIsPermanent;
        private System.Windows.Forms.ComboBox cbxCitizenship;
        private System.Windows.Forms.ComboBox cbxStatus;
        private System.Windows.Forms.TextBox tbxValidThru;
        private System.Windows.Forms.TextBox tbxIssueDate;
        private System.Windows.Forms.TextBox tbxLastName;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Label lblValidThu;
        private System.Windows.Forms.Label lblIssueDate;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblIsPermanent;
        private System.Windows.Forms.Label lblCitizenship;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblRegDate;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.GroupBox gbxInspection;
        private System.Windows.Forms.DateTimePicker dtpRegDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.ComboBox cbxCity;
        private System.Windows.Forms.GroupBox gbxDocuments;
        private System.Windows.Forms.DataGridView gridDocuments;
        private System.Windows.Forms.GroupBox gbxClient;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.ComboBox cbxSubType;
        private System.Windows.Forms.Label lblSubType;
        private System.Windows.Forms.RadioButton rdoFemale;
        private System.Windows.Forms.RadioButton rdoMale;
        private System.Windows.Forms.Label lblFathersName;
        private System.Windows.Forms.TextBox tbxFathersName;
        private System.Windows.Forms.ComboBox cbxIndustry;
        private System.Windows.Forms.ComboBox cbxSegment;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.Label lblIndustry;
        private System.Windows.Forms.Label lblSegment;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblIsInsider;
        private System.Windows.Forms.TextBox tbxHome;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tbxMobile;
        private System.Windows.Forms.Label lblHome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.ComboBox cbxAddressBusiness;
        private System.Windows.Forms.ComboBox cbxAddressFact;
        private System.Windows.Forms.ComboBox cbxAddress;
        private System.Windows.Forms.TextBox tbxAddressBusiness;
        private System.Windows.Forms.TextBox tbxAddressFact;
        private System.Windows.Forms.TextBox tbxAddress;
        private System.Windows.Forms.Label lblAddressBusiness;
        private System.Windows.Forms.Label lblAddressFact;
        private System.Windows.Forms.Label lblAddress;

    }
}