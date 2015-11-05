namespace AltasoftDaily.UserInterface.WindowsForms
{
    partial class SingleOrderForm
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
            this.panel1 = new MetroFramework.Controls.MetroPanel();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.btnAdd = new MetroFramework.Controls.MetroButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxCashDeskSymbol = new MetroFramework.Controls.MetroComboBox();
            this.lblAmount = new MetroFramework.Controls.MetroLabel();
            this.tbxAmount = new MetroFramework.Controls.MetroTextBox();
            this.lblCashDeskSymbol = new MetroFramework.Controls.MetroLabel();
            this.lblPurpose = new MetroFramework.Controls.MetroLabel();
            this.tbxPurpose = new MetroFramework.Controls.MetroTextBox();
            this.comboBox7 = new MetroFramework.Controls.MetroComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbxDocNum = new MetroFramework.Controls.MetroTextBox();
            this.lblDocNum = new MetroFramework.Controls.MetroLabel();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new MetroFramework.Controls.MetroLabel();
            this.cbxCcy = new MetroFramework.Controls.MetroComboBox();
            this.lblCcy = new MetroFramework.Controls.MetroLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbxCashDesk = new MetroFramework.Controls.MetroComboBox();
            this.tbxCashDesk = new MetroFramework.Controls.MetroTextBox();
            this.btnCashDeskBrowse = new MetroFramework.Controls.MetroButton();
            this.button4 = new MetroFramework.Controls.MetroButton();
            this.tbxCashDeskName = new MetroFramework.Controls.MetroTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbxBranch = new MetroFramework.Controls.MetroComboBox();
            this.tbxAccountNumber = new MetroFramework.Controls.MetroTextBox();
            this.btnAccountBrowse = new MetroFramework.Controls.MetroButton();
            this.button6 = new MetroFramework.Controls.MetroButton();
            this.tbxAccountName = new MetroFramework.Controls.MetroTextBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.HorizontalScrollbarBarColor = true;
            this.panel1.HorizontalScrollbarHighlightOnWheel = false;
            this.panel1.HorizontalScrollbarSize = 10;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 315);
            this.panel1.TabIndex = 1;
            this.panel1.VerticalScrollbarBarColor = true;
            this.panel1.VerticalScrollbarHighlightOnWheel = false;
            this.panel1.VerticalScrollbarSize = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(695, 283);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "გაუქმება";
            this.btnCancel.UseSelectable = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(601, 283);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 23);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "დამატება";
            this.btnAdd.UseSelectable = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(12, 178);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(764, 99);
            this.tableLayoutPanel3.TabIndex = 12;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel4);
            this.groupBox4.Controls.Add(this.comboBox7);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(758, 91);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.73434F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.26566F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel4.Controls.Add(this.cbxCashDeskSymbol, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblAmount, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tbxAmount, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblCashDeskSymbol, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblPurpose, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tbxPurpose, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(752, 72);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // cbxCashDeskSymbol
            // 
            this.cbxCashDeskSymbol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxCashDeskSymbol.Enabled = false;
            this.cbxCashDeskSymbol.FormattingEnabled = true;
            this.cbxCashDeskSymbol.ItemHeight = 23;
            this.cbxCashDeskSymbol.Items.AddRange(new object[] {
            "120"});
            this.cbxCashDeskSymbol.Location = new System.Drawing.Point(647, 3);
            this.cbxCashDeskSymbol.Name = "cbxCashDeskSymbol";
            this.cbxCashDeskSymbol.Size = new System.Drawing.Size(88, 29);
            this.cbxCashDeskSymbol.TabIndex = 3;
            this.cbxCashDeskSymbol.UseSelectable = true;
            // 
            // lblAmount
            // 
            this.lblAmount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(25, 8);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(51, 19);
            this.lblAmount.TabIndex = 0;
            this.lblAmount.Text = "თანხა";
            // 
            // tbxAmount
            // 
            this.tbxAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxAmount.BackColor = System.Drawing.Color.White;
            this.tbxAmount.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbxAmount.Lines = new string[0];
            this.tbxAmount.Location = new System.Drawing.Point(104, 3);
            this.tbxAmount.MaxLength = 32767;
            this.tbxAmount.Name = "tbxAmount";
            this.tbxAmount.PasswordChar = '\0';
            this.tbxAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxAmount.SelectedText = "";
            this.tbxAmount.Size = new System.Drawing.Size(135, 29);
            this.tbxAmount.TabIndex = 1;
            this.tbxAmount.UseSelectable = true;
            // 
            // lblCashDeskSymbol
            // 
            this.lblCashDeskSymbol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCashDeskSymbol.AutoSize = true;
            this.lblCashDeskSymbol.Location = new System.Drawing.Point(515, 8);
            this.lblCashDeskSymbol.Name = "lblCashDeskSymbol";
            this.lblCashDeskSymbol.Size = new System.Drawing.Size(113, 19);
            this.lblCashDeskSymbol.TabIndex = 2;
            this.lblCashDeskSymbol.Text = "სალაროს სიმბოლო";
            // 
            // lblPurpose
            // 
            this.lblPurpose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPurpose.AutoSize = true;
            this.lblPurpose.Location = new System.Drawing.Point(3, 44);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Size = new System.Drawing.Size(95, 19);
            this.lblPurpose.TabIndex = 4;
            this.lblPurpose.Text = "დანიშნულება";
            // 
            // tbxPurpose
            // 
            this.tbxPurpose.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxPurpose.Enabled = false;
            this.tbxPurpose.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbxPurpose.Lines = new string[0];
            this.tbxPurpose.Location = new System.Drawing.Point(104, 39);
            this.tbxPurpose.MaxLength = 32767;
            this.tbxPurpose.Name = "tbxPurpose";
            this.tbxPurpose.PasswordChar = '\0';
            this.tbxPurpose.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxPurpose.SelectedText = "";
            this.tbxPurpose.Size = new System.Drawing.Size(405, 29);
            this.tbxPurpose.TabIndex = 1;
            this.tbxPurpose.UseSelectable = true;
            // 
            // comboBox7
            // 
            this.comboBox7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.ItemHeight = 23;
            this.comboBox7.Location = new System.Drawing.Point(-9, -188);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(121, 29);
            this.comboBox7.TabIndex = 0;
            this.comboBox7.UseSelectable = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.46939F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.53061F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tableLayoutPanel1.Controls.Add(this.tbxDocNum, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDocNum, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpDate, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDate, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxCcy, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCcy, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(764, 35);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // tbxDocNum
            // 
            this.tbxDocNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxDocNum.Enabled = false;
            this.tbxDocNum.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbxDocNum.Lines = new string[0];
            this.tbxDocNum.Location = new System.Drawing.Point(95, 3);
            this.tbxDocNum.MaxLength = 32767;
            this.tbxDocNum.Name = "tbxDocNum";
            this.tbxDocNum.PasswordChar = '\0';
            this.tbxDocNum.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxDocNum.SelectedText = "";
            this.tbxDocNum.Size = new System.Drawing.Size(92, 29);
            this.tbxDocNum.TabIndex = 5;
            this.tbxDocNum.UseSelectable = true;
            // 
            // lblDocNum
            // 
            this.lblDocNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDocNum.AutoSize = true;
            this.lblDocNum.Location = new System.Drawing.Point(5, 8);
            this.lblDocNum.Name = "lblDocNum";
            this.lblDocNum.Size = new System.Drawing.Size(81, 19);
            this.lblDocNum.TabIndex = 2;
            this.lblDocNum.Text = "საბუთის #";
            // 
            // dtpDate
            // 
            this.dtpDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Enabled = false;
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(628, 3);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(119, 29);
            this.dtpDate.TabIndex = 4;
            this.dtpDate.Value = new System.DateTime(2015, 12, 31, 0, 0, 0, 0);
            // 
            // lblDate
            // 
            this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(539, 8);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(63, 19);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "თარიღი";
            // 
            // cbxCcy
            // 
            this.cbxCcy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxCcy.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbxCcy.Enabled = false;
            this.cbxCcy.FormattingEnabled = true;
            this.cbxCcy.ItemHeight = 23;
            this.cbxCcy.Items.AddRange(new object[] {
            "GEL"});
            this.cbxCcy.Location = new System.Drawing.Point(415, 3);
            this.cbxCcy.Name = "cbxCcy";
            this.cbxCcy.Size = new System.Drawing.Size(69, 29);
            this.cbxCcy.TabIndex = 3;
            this.cbxCcy.UseSelectable = true;
            // 
            // lblCcy
            // 
            this.lblCcy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCcy.AutoSize = true;
            this.lblCcy.Location = new System.Drawing.Point(342, 8);
            this.lblCcy.Name = "lblCcy";
            this.lblCcy.Size = new System.Drawing.Size(64, 19);
            this.lblCcy.TabIndex = 2;
            this.lblCcy.Text = "ვალუტა";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 53);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.42017F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.57983F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(764, 119);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel1);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(758, 54);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "სალაროს ანგარიში";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cbxCashDesk);
            this.flowLayoutPanel1.Controls.Add(this.tbxCashDesk);
            this.flowLayoutPanel1.Controls.Add(this.btnCashDeskBrowse);
            this.flowLayoutPanel1.Controls.Add(this.button4);
            this.flowLayoutPanel1.Controls.Add(this.tbxCashDeskName);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(752, 35);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // cbxCashDesk
            // 
            this.cbxCashDesk.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbxCashDesk.Enabled = false;
            this.cbxCashDesk.FormattingEnabled = true;
            this.cbxCashDesk.ItemHeight = 23;
            this.cbxCashDesk.Items.AddRange(new object[] {
            "HEAD",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cbxCashDesk.Location = new System.Drawing.Point(3, 3);
            this.cbxCashDesk.Name = "cbxCashDesk";
            this.cbxCashDesk.Size = new System.Drawing.Size(107, 29);
            this.cbxCashDesk.TabIndex = 0;
            this.cbxCashDesk.UseSelectable = true;
            // 
            // tbxCashDesk
            // 
            this.tbxCashDesk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxCashDesk.Enabled = false;
            this.tbxCashDesk.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbxCashDesk.Lines = new string[0];
            this.tbxCashDesk.Location = new System.Drawing.Point(116, 3);
            this.tbxCashDesk.MaxLength = 32767;
            this.tbxCashDesk.Name = "tbxCashDesk";
            this.tbxCashDesk.PasswordChar = '\0';
            this.tbxCashDesk.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxCashDesk.SelectedText = "";
            this.tbxCashDesk.Size = new System.Drawing.Size(144, 29);
            this.tbxCashDesk.TabIndex = 1;
            this.tbxCashDesk.UseSelectable = true;
            // 
            // btnCashDeskBrowse
            // 
            this.btnCashDeskBrowse.Enabled = false;
            this.btnCashDeskBrowse.Location = new System.Drawing.Point(266, 3);
            this.btnCashDeskBrowse.Name = "btnCashDeskBrowse";
            this.btnCashDeskBrowse.Size = new System.Drawing.Size(29, 29);
            this.btnCashDeskBrowse.TabIndex = 2;
            this.btnCashDeskBrowse.Text = "...";
            this.btnCashDeskBrowse.UseSelectable = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(301, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(29, 29);
            this.button4.TabIndex = 3;
            this.button4.Text = "V";
            this.button4.UseSelectable = true;
            // 
            // tbxCashDeskName
            // 
            this.tbxCashDeskName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxCashDeskName.Enabled = false;
            this.tbxCashDeskName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbxCashDeskName.Lines = new string[] {
        "სალარო"};
            this.tbxCashDeskName.Location = new System.Drawing.Point(336, 3);
            this.tbxCashDeskName.MaxLength = 32767;
            this.tbxCashDeskName.Name = "tbxCashDeskName";
            this.tbxCashDeskName.PasswordChar = '\0';
            this.tbxCashDeskName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxCashDeskName.SelectedText = "";
            this.tbxCashDeskName.Size = new System.Drawing.Size(407, 29);
            this.tbxCashDeskName.TabIndex = 1;
            this.tbxCashDeskName.Text = "სალარო";
            this.tbxCashDeskName.UseSelectable = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(3, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(758, 53);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "კლიენტის ანგარიში";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.cbxBranch);
            this.flowLayoutPanel2.Controls.Add(this.tbxAccountNumber);
            this.flowLayoutPanel2.Controls.Add(this.btnAccountBrowse);
            this.flowLayoutPanel2.Controls.Add(this.button6);
            this.flowLayoutPanel2.Controls.Add(this.tbxAccountName);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(752, 34);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // cbxBranch
            // 
            this.cbxBranch.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbxBranch.Enabled = false;
            this.cbxBranch.FormattingEnabled = true;
            this.cbxBranch.ItemHeight = 23;
            this.cbxBranch.Items.AddRange(new object[] {
            "HEAD",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cbxBranch.Location = new System.Drawing.Point(3, 3);
            this.cbxBranch.Name = "cbxBranch";
            this.cbxBranch.Size = new System.Drawing.Size(107, 29);
            this.cbxBranch.TabIndex = 0;
            this.cbxBranch.UseSelectable = true;
            // 
            // tbxAccountNumber
            // 
            this.tbxAccountNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxAccountNumber.Enabled = false;
            this.tbxAccountNumber.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbxAccountNumber.Lines = new string[0];
            this.tbxAccountNumber.Location = new System.Drawing.Point(116, 3);
            this.tbxAccountNumber.MaxLength = 32767;
            this.tbxAccountNumber.Name = "tbxAccountNumber";
            this.tbxAccountNumber.PasswordChar = '\0';
            this.tbxAccountNumber.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxAccountNumber.SelectedText = "";
            this.tbxAccountNumber.Size = new System.Drawing.Size(144, 29);
            this.tbxAccountNumber.TabIndex = 1;
            this.tbxAccountNumber.UseSelectable = true;
            // 
            // btnAccountBrowse
            // 
            this.btnAccountBrowse.Enabled = false;
            this.btnAccountBrowse.Location = new System.Drawing.Point(266, 3);
            this.btnAccountBrowse.Name = "btnAccountBrowse";
            this.btnAccountBrowse.Size = new System.Drawing.Size(29, 29);
            this.btnAccountBrowse.TabIndex = 2;
            this.btnAccountBrowse.Text = "...";
            this.btnAccountBrowse.UseSelectable = true;
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(301, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(29, 29);
            this.button6.TabIndex = 3;
            this.button6.Text = "V";
            this.button6.UseSelectable = true;
            // 
            // tbxAccountName
            // 
            this.tbxAccountName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxAccountName.Enabled = false;
            this.tbxAccountName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbxAccountName.Lines = new string[0];
            this.tbxAccountName.Location = new System.Drawing.Point(336, 3);
            this.tbxAccountName.MaxLength = 32767;
            this.tbxAccountName.Name = "tbxAccountName";
            this.tbxAccountName.PasswordChar = '\0';
            this.tbxAccountName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbxAccountName.SelectedText = "";
            this.tbxAccountName.Size = new System.Drawing.Size(407, 29);
            this.tbxAccountName.TabIndex = 1;
            this.tbxAccountName.UseSelectable = true;
            // 
            // SingleOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 395);
            this.Controls.Add(this.panel1);
            this.Name = "SingleOrderForm";
            this.Text = "სალაროს შემოსავლის ორდერის დამატება";
            this.Load += new System.EventHandler(this.Home_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private MetroFramework.Controls.MetroLabel lblAmount;
        private MetroFramework.Controls.MetroTextBox tbxAmount;
        private MetroFramework.Controls.MetroLabel lblCashDeskSymbol;
        private MetroFramework.Controls.MetroLabel lblPurpose;
        private MetroFramework.Controls.MetroTextBox tbxPurpose;
        private MetroFramework.Controls.MetroComboBox comboBox7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroTextBox tbxDocNum;
        private MetroFramework.Controls.MetroLabel lblDocNum;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private MetroFramework.Controls.MetroLabel lblDate;
        private MetroFramework.Controls.MetroComboBox cbxCcy;
        private MetroFramework.Controls.MetroLabel lblCcy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MetroFramework.Controls.MetroComboBox cbxCashDesk;
        private MetroFramework.Controls.MetroTextBox tbxCashDesk;
        private MetroFramework.Controls.MetroButton btnCashDeskBrowse;
        private MetroFramework.Controls.MetroButton button4;
        private MetroFramework.Controls.MetroTextBox tbxCashDeskName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private MetroFramework.Controls.MetroComboBox cbxBranch;
        private MetroFramework.Controls.MetroTextBox tbxAccountNumber;
        private MetroFramework.Controls.MetroButton btnAccountBrowse;
        private MetroFramework.Controls.MetroButton button6;
        private MetroFramework.Controls.MetroTextBox tbxAccountName;
        private MetroFramework.Controls.MetroPanel panel1;
        private MetroFramework.Controls.MetroComboBox cbxCashDeskSymbol;
        private MetroFramework.Controls.MetroButton btnCancel;
        private MetroFramework.Controls.MetroButton btnAdd;
    }
}