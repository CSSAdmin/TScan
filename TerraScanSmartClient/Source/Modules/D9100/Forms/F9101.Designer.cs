namespace D9100
{
    partial class F9101
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9101));
            this.LastNamePanel = new System.Windows.Forms.Panel();
            this.LastNameTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.LastNameLabel = new System.Windows.Forms.Label();
            this.FirstNamePanel = new System.Windows.Forms.Panel();
            this.FirstNameTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.FirstNameLabel = new System.Windows.Forms.Label();
            this.AddressPanel = new System.Windows.Forms.Panel();
            this.AddressTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.MasterNameVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.RecordCountLabel = new System.Windows.Forms.Label();
            this.ScrollPanel = new System.Windows.Forms.Panel();
            this.MasterNameCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ClearButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SearchButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AcceptMasterNameButton = new TerraScan.UI.Controls.TerraScanButton();
            this.MasterNameLinkLabel = new TerraScan.UI.Controls.TerraScanLinkLabel();
            this.MasterNameDataGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OwnerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HelpLink = new System.Windows.Forms.LinkLabel();
            this.LastNamePanel.SuspendLayout();
            this.FirstNamePanel.SuspendLayout();
            this.AddressPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MasterNameDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // LastNamePanel
            // 
            this.LastNamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastNamePanel.Controls.Add(this.LastNameTextBox);
            this.LastNamePanel.Controls.Add(this.LastNameLabel);
            this.LastNamePanel.Location = new System.Drawing.Point(12, 12);
            this.LastNamePanel.Name = "LastNamePanel";
            this.LastNamePanel.Size = new System.Drawing.Size(171, 38);
            this.LastNamePanel.TabIndex = 1;
            this.LastNamePanel.TabStop = true;
            // 
            // LastNameTextBox
            // 
            this.LastNameTextBox.AllowClick = true;
            this.LastNameTextBox.AllowNegativeSign = false;
            this.LastNameTextBox.ApplyCFGFormat = false;
            this.LastNameTextBox.ApplyCurrencyFormat = false;
            this.LastNameTextBox.ApplyFocusColor = true;
            this.LastNameTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.LastNameTextBox.ApplyNegativeStandard = true;
            this.LastNameTextBox.ApplyParentFocusColor = true;
            this.LastNameTextBox.ApplyTimeFormat = false;
            this.LastNameTextBox.BackColor = System.Drawing.Color.White;
            this.LastNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LastNameTextBox.CFromatWihoutSymbol = false;
            this.LastNameTextBox.CheckForEmpty = false;
            this.LastNameTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.LastNameTextBox.Digits = -1;
            this.LastNameTextBox.EmptyDecimalValue = false;
            this.LastNameTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.LastNameTextBox.ForeColor = System.Drawing.Color.Black;
            this.LastNameTextBox.IsEditable = false;
            this.LastNameTextBox.IsQueryableFileld = false;
            this.LastNameTextBox.Location = new System.Drawing.Point(14, 16);
            this.LastNameTextBox.LockKeyPress = false;
            this.LastNameTextBox.MaxLength = 50;
            this.LastNameTextBox.Name = "LastNameTextBox";
            this.LastNameTextBox.PersistDefaultColor = false;
            this.LastNameTextBox.Precision = 2;
            this.LastNameTextBox.QueryingFileldName = "";
            this.LastNameTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.LastNameTextBox.Size = new System.Drawing.Size(149, 16);
            this.LastNameTextBox.SpecialCharacter = "%";
            this.LastNameTextBox.TabIndex = 2;
            this.LastNameTextBox.TextCustomFormat = "$#,##0.00";
            this.LastNameTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.LastNameTextBox.WholeInteger = false;
            this.LastNameTextBox.TextChanged += new System.EventHandler(this.EditText);
            // 
            // LastNameLabel
            // 
            this.LastNameLabel.AutoSize = true;
            this.LastNameLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LastNameLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.LastNameLabel.Location = new System.Drawing.Point(0, 0);
            this.LastNameLabel.Name = "LastNameLabel";
            this.LastNameLabel.Size = new System.Drawing.Size(68, 14);
            this.LastNameLabel.TabIndex = 62;
            this.LastNameLabel.Text = "Last Name:";
            // 
            // FirstNamePanel
            // 
            this.FirstNamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FirstNamePanel.Controls.Add(this.FirstNameTextBox);
            this.FirstNamePanel.Controls.Add(this.FirstNameLabel);
            this.FirstNamePanel.Location = new System.Drawing.Point(182, 12);
            this.FirstNamePanel.Name = "FirstNamePanel";
            this.FirstNamePanel.Size = new System.Drawing.Size(164, 38);
            this.FirstNamePanel.TabIndex = 3;
            this.FirstNamePanel.TabStop = true;
            // 
            // FirstNameTextBox
            // 
            this.FirstNameTextBox.AllowClick = true;
            this.FirstNameTextBox.AllowNegativeSign = false;
            this.FirstNameTextBox.ApplyCFGFormat = false;
            this.FirstNameTextBox.ApplyCurrencyFormat = false;
            this.FirstNameTextBox.ApplyFocusColor = true;
            this.FirstNameTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.FirstNameTextBox.ApplyNegativeStandard = true;
            this.FirstNameTextBox.ApplyParentFocusColor = true;
            this.FirstNameTextBox.ApplyTimeFormat = false;
            this.FirstNameTextBox.BackColor = System.Drawing.Color.White;
            this.FirstNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FirstNameTextBox.CFromatWihoutSymbol = false;
            this.FirstNameTextBox.CheckForEmpty = false;
            this.FirstNameTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FirstNameTextBox.Digits = -1;
            this.FirstNameTextBox.EmptyDecimalValue = false;
            this.FirstNameTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.FirstNameTextBox.ForeColor = System.Drawing.Color.Black;
            this.FirstNameTextBox.IsEditable = false;
            this.FirstNameTextBox.IsQueryableFileld = false;
            this.FirstNameTextBox.Location = new System.Drawing.Point(12, 16);
            this.FirstNameTextBox.LockKeyPress = false;
            this.FirstNameTextBox.MaxLength = 50;
            this.FirstNameTextBox.Name = "FirstNameTextBox";
            this.FirstNameTextBox.PersistDefaultColor = false;
            this.FirstNameTextBox.Precision = 2;
            this.FirstNameTextBox.QueryingFileldName = "";
            this.FirstNameTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FirstNameTextBox.Size = new System.Drawing.Size(143, 16);
            this.FirstNameTextBox.SpecialCharacter = "%";
            this.FirstNameTextBox.TabIndex = 4;
            this.FirstNameTextBox.TextCustomFormat = "$#,##0.00";
            this.FirstNameTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.FirstNameTextBox.WholeInteger = false;
            this.FirstNameTextBox.TextChanged += new System.EventHandler(this.EditText);
            // 
            // FirstNameLabel
            // 
            this.FirstNameLabel.AutoSize = true;
            this.FirstNameLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FirstNameLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.FirstNameLabel.Location = new System.Drawing.Point(0, 0);
            this.FirstNameLabel.Name = "FirstNameLabel";
            this.FirstNameLabel.Size = new System.Drawing.Size(69, 14);
            this.FirstNameLabel.TabIndex = 62;
            this.FirstNameLabel.Text = "First Name:";
            // 
            // AddressPanel
            // 
            this.AddressPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddressPanel.Controls.Add(this.AddressTextBox);
            this.AddressPanel.Controls.Add(this.AddressLabel);
            this.AddressPanel.Location = new System.Drawing.Point(344, 12);
            this.AddressPanel.Name = "AddressPanel";
            this.AddressPanel.Size = new System.Drawing.Size(238, 38);
            this.AddressPanel.TabIndex = 5;
            this.AddressPanel.TabStop = true;
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.AllowClick = true;
            this.AddressTextBox.AllowNegativeSign = false;
            this.AddressTextBox.ApplyCFGFormat = false;
            this.AddressTextBox.ApplyCurrencyFormat = false;
            this.AddressTextBox.ApplyFocusColor = true;
            this.AddressTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.AddressTextBox.ApplyNegativeStandard = true;
            this.AddressTextBox.ApplyParentFocusColor = true;
            this.AddressTextBox.ApplyTimeFormat = false;
            this.AddressTextBox.BackColor = System.Drawing.Color.White;
            this.AddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AddressTextBox.CFromatWihoutSymbol = false;
            this.AddressTextBox.CheckForEmpty = false;
            this.AddressTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AddressTextBox.Digits = -1;
            this.AddressTextBox.EmptyDecimalValue = false;
            this.AddressTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.AddressTextBox.ForeColor = System.Drawing.Color.Black;
            this.AddressTextBox.IsEditable = false;
            this.AddressTextBox.IsQueryableFileld = false;
            this.AddressTextBox.Location = new System.Drawing.Point(18, 16);
            this.AddressTextBox.LockKeyPress = false;
            this.AddressTextBox.MaxLength = 101;
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.PersistDefaultColor = false;
            this.AddressTextBox.Precision = 2;
            this.AddressTextBox.QueryingFileldName = "";
            this.AddressTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.AddressTextBox.Size = new System.Drawing.Size(212, 16);
            this.AddressTextBox.SpecialCharacter = "%";
            this.AddressTextBox.TabIndex = 6;
            this.AddressTextBox.TextCustomFormat = "$#,##0.00";
            this.AddressTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.AddressTextBox.WholeInteger = false;
            this.AddressTextBox.TextChanged += new System.EventHandler(this.EditText);
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AddressLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.AddressLabel.Location = new System.Drawing.Point(0, 0);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(58, 14);
            this.AddressLabel.TabIndex = 62;
            this.AddressLabel.Text = "Address:";
            // 
            // MasterNameVerticalScroll
            // 
            this.MasterNameVerticalScroll.Location = new System.Drawing.Point(563, 50);
            this.MasterNameVerticalScroll.Name = "MasterNameVerticalScroll";
            this.MasterNameVerticalScroll.Size = new System.Drawing.Size(17, 127);
            this.MasterNameVerticalScroll.TabIndex = 6;
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.Black;
            this.FormLinePanel.Location = new System.Drawing.Point(12, 225);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(575, 2);
            this.FormLinePanel.TabIndex = 105;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(12, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 107;
            this.label1.Text = "9101";
            // 
            // RecordCountLabel
            // 
            this.RecordCountLabel.AutoSize = true;
            this.RecordCountLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordCountLabel.Location = new System.Drawing.Point(376, 230);
            this.RecordCountLabel.Name = "RecordCountLabel";
            this.RecordCountLabel.Size = new System.Drawing.Size(0, 15);
            this.RecordCountLabel.TabIndex = 118;
            // 
            // ScrollPanel
            // 
            this.ScrollPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScrollPanel.Location = new System.Drawing.Point(563, 49);
            this.ScrollPanel.Name = "ScrollPanel";
            this.ScrollPanel.Size = new System.Drawing.Size(19, 129);
            this.ScrollPanel.TabIndex = 119;
            // 
            // MasterNameCancelButton
            // 
            this.MasterNameCancelButton.ActualPermission = false;
            this.MasterNameCancelButton.ApplyDisableBehaviour = false;
            this.MasterNameCancelButton.AutoSize = true;
            this.MasterNameCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.MasterNameCancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.MasterNameCancelButton.CommentPriority = false;
            this.MasterNameCancelButton.EnableAutoPrint = false;
            this.MasterNameCancelButton.FilterStatus = false;
            this.MasterNameCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MasterNameCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MasterNameCancelButton.FocusRectangleEnabled = true;
            this.MasterNameCancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MasterNameCancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MasterNameCancelButton.ImageSelected = false;
            this.MasterNameCancelButton.Location = new System.Drawing.Point(471, 189);
            this.MasterNameCancelButton.Name = "MasterNameCancelButton";
            this.MasterNameCancelButton.NewPadding = 5;
            this.MasterNameCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.MasterNameCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.MasterNameCancelButton.Size = new System.Drawing.Size(110, 30);
            this.MasterNameCancelButton.StatusIndicator = false;
            this.MasterNameCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MasterNameCancelButton.StatusOffText = null;
            this.MasterNameCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.MasterNameCancelButton.StatusOnText = null;
            this.MasterNameCancelButton.TabIndex = 117;
            this.MasterNameCancelButton.TabStop = false;
            this.MasterNameCancelButton.Text = "Cancel";
            this.MasterNameCancelButton.UseVisualStyleBackColor = false;
            this.MasterNameCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.ActualPermission = false;
            this.ClearButton.ApplyDisableBehaviour = false;
            this.ClearButton.AutoSize = true;
            this.ClearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ClearButton.BorderColor = System.Drawing.Color.Wheat;
            this.ClearButton.CommentPriority = false;
            this.ClearButton.EnableAutoPrint = false;
            this.ClearButton.FilterStatus = false;
            this.ClearButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearButton.FocusRectangleEnabled = true;
            this.ClearButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClearButton.ImageSelected = false;
            this.ClearButton.Location = new System.Drawing.Point(318, 189);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.NewPadding = 5;
            this.ClearButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.ClearButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ClearButton.Size = new System.Drawing.Size(110, 30);
            this.ClearButton.StatusIndicator = false;
            this.ClearButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClearButton.StatusOffText = null;
            this.ClearButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ClearButton.StatusOnText = null;
            this.ClearButton.TabIndex = 116;
            this.ClearButton.TabStop = false;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.ActualPermission = false;
            this.SearchButton.ApplyDisableBehaviour = false;
            this.SearchButton.AutoSize = true;
            this.SearchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SearchButton.BorderColor = System.Drawing.Color.Wheat;
            this.SearchButton.CommentPriority = false;
            this.SearchButton.EnableAutoPrint = false;
            this.SearchButton.FilterStatus = false;
            this.SearchButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchButton.FocusRectangleEnabled = true;
            this.SearchButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SearchButton.ImageSelected = false;
            this.SearchButton.Location = new System.Drawing.Point(173, 189);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.NewPadding = 5;
            this.SearchButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.SearchButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SearchButton.Size = new System.Drawing.Size(110, 30);
            this.SearchButton.StatusIndicator = false;
            this.SearchButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SearchButton.StatusOffText = null;
            this.SearchButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SearchButton.StatusOnText = null;
            this.SearchButton.TabIndex = 115;
            this.SearchButton.TabStop = false;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // AcceptMasterNameButton
            // 
            this.AcceptMasterNameButton.ActualPermission = false;
            this.AcceptMasterNameButton.ApplyDisableBehaviour = false;
            this.AcceptMasterNameButton.AutoSize = true;
            this.AcceptMasterNameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AcceptMasterNameButton.BorderColor = System.Drawing.Color.Wheat;
            this.AcceptMasterNameButton.CommentPriority = false;
            this.AcceptMasterNameButton.EnableAutoPrint = false;
            this.AcceptMasterNameButton.FilterStatus = false;
            this.AcceptMasterNameButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AcceptMasterNameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AcceptMasterNameButton.FocusRectangleEnabled = true;
            this.AcceptMasterNameButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptMasterNameButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AcceptMasterNameButton.ImageSelected = false;
            this.AcceptMasterNameButton.Location = new System.Drawing.Point(12, 189);
            this.AcceptMasterNameButton.Name = "AcceptMasterNameButton";
            this.AcceptMasterNameButton.NewPadding = 5;
            this.AcceptMasterNameButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.AcceptMasterNameButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AcceptMasterNameButton.Size = new System.Drawing.Size(110, 30);
            this.AcceptMasterNameButton.StatusIndicator = false;
            this.AcceptMasterNameButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AcceptMasterNameButton.StatusOffText = null;
            this.AcceptMasterNameButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AcceptMasterNameButton.StatusOnText = null;
            this.AcceptMasterNameButton.TabIndex = 101;
            this.AcceptMasterNameButton.TabStop = false;
            this.AcceptMasterNameButton.Text = "Accept";
            this.AcceptMasterNameButton.UseVisualStyleBackColor = false;
            this.AcceptMasterNameButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // MasterNameLinkLabel
            // 
            this.MasterNameLinkLabel.AutoSize = true;
            this.MasterNameLinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MasterNameLinkLabel.FormDllName = null;
            this.MasterNameLinkLabel.FormId = 0;
            this.MasterNameLinkLabel.Location = new System.Drawing.Point(212, 230);
            this.MasterNameLinkLabel.MenuName = null;
            this.MasterNameLinkLabel.Name = "MasterNameLinkLabel";
            this.MasterNameLinkLabel.PermissionOpen = 0;
            this.MasterNameLinkLabel.Size = new System.Drawing.Size(115, 15);
            this.MasterNameLinkLabel.TabIndex = 8;
            this.MasterNameLinkLabel.TabStop = true;
            this.MasterNameLinkLabel.Text = "Master Name Form";
            this.MasterNameLinkLabel.TextCustomFormat = "#,##0.00";
            this.MasterNameLinkLabel.ValidateType = TerraScan.UI.Controls.TerraScanLinkLabel.ControlValidationType.Text;
            this.MasterNameLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MasterNameLinkLabel_LinkClicked);
            // 
            // MasterNameDataGridView
            // 
            this.MasterNameDataGridView.AllowCellClick = true;
            this.MasterNameDataGridView.AllowDoubleClick = true;
            this.MasterNameDataGridView.AllowEmptyRows = true;
            this.MasterNameDataGridView.AllowEnterKey = false;
            this.MasterNameDataGridView.AllowSorting = true;
            this.MasterNameDataGridView.AllowUserToAddRows = false;
            this.MasterNameDataGridView.AllowUserToDeleteRows = false;
            this.MasterNameDataGridView.AllowUserToResizeColumns = false;
            this.MasterNameDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.MasterNameDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.MasterNameDataGridView.ApplyStandardBehaviour = false;
            this.MasterNameDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.MasterNameDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MasterNameDataGridView.ClearCurrentCellOnLeave = false;
            this.MasterNameDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MasterNameDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MasterNameDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MasterNameDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LastName,
            this.FirstName,
            this.Address,
            this.OwnerID,
            this.sa});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MasterNameDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.MasterNameDataGridView.DefaultRowIndex = -1;
            this.MasterNameDataGridView.DeselectCurrentCell = false;
            this.MasterNameDataGridView.DeselectSpecifiedRow = -1;
            this.MasterNameDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.MasterNameDataGridView.EnableBinding = false;
            this.MasterNameDataGridView.EnableHeadersVisualStyles = false;
            this.MasterNameDataGridView.GridColor = System.Drawing.Color.Black;
            this.MasterNameDataGridView.GridContentSelected = false;
            this.MasterNameDataGridView.IsEditableGrid = false;
            this.MasterNameDataGridView.IsMultiSelect = false;
            this.MasterNameDataGridView.IsSorted = false;
            this.MasterNameDataGridView.Location = new System.Drawing.Point(12, 49);
            this.MasterNameDataGridView.MultiSelect = false;
            this.MasterNameDataGridView.Name = "MasterNameDataGridView";
            this.MasterNameDataGridView.NumRowsVisible = 5;
            this.MasterNameDataGridView.PrimaryKeyColumnName = "";
            this.MasterNameDataGridView.RemainSortFields = false;
            this.MasterNameDataGridView.RemoveDefaultSelection = false;
            this.MasterNameDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MasterNameDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.MasterNameDataGridView.RowHeadersWidth = 20;
            this.MasterNameDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.MasterNameDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MasterNameDataGridView.Size = new System.Drawing.Size(568, 129);
            this.MasterNameDataGridView.StandardTab = true;
            this.MasterNameDataGridView.TabIndex = 7;
            this.MasterNameDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MasterNameDataGridView_CellDoubleClick);
            // 
            // LastName
            // 
            this.LastName.HeaderText = "Last Name";
            this.LastName.Name = "LastName";
            this.LastName.ReadOnly = true;
            this.LastName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.LastName.Width = 150;
            // 
            // FirstName
            // 
            this.FirstName.HeaderText = "First Name";
            this.FirstName.Name = "FirstName";
            this.FirstName.ReadOnly = true;
            this.FirstName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.FirstName.Width = 150;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Address.Width = 150;
            // 
            // OwnerID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.OwnerID.DefaultCellStyle = dataGridViewCellStyle3;
            this.OwnerID.HeaderText = "OwnerID";
            this.OwnerID.Name = "OwnerID";
            this.OwnerID.ReadOnly = true;
            this.OwnerID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.OwnerID.Width = 81;
            // 
            // sa
            // 
            this.sa.HeaderText = "";
            this.sa.Name = "sa";
            this.sa.ReadOnly = true;
            this.sa.Visible = false;
            this.sa.Width = 20;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(561, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 1);
            this.panel1.TabIndex = 142;
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpLink.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLink.Location = new System.Drawing.Point(526, 230);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(56, 15);
            this.HelpLink.TabIndex = 144;
            this.HelpLink.TabStop = true;
            this.HelpLink.Text = "Help";
            this.HelpLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // F9101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(592, 250);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MasterNameVerticalScroll);
            this.Controls.Add(this.ScrollPanel);
            this.Controls.Add(this.RecordCountLabel);
            this.Controls.Add(this.MasterNameCancelButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.AcceptMasterNameButton);
            this.Controls.Add(this.MasterNameLinkLabel);
            this.Controls.Add(this.AddressPanel);
            this.Controls.Add(this.FirstNamePanel);
            this.Controls.Add(this.LastNamePanel);
            this.Controls.Add(this.MasterNameDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F9101";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "9101";
            this.Text = "TerraScan - Master Name Search";
            this.Load += new System.EventHandler(this.F9101_Load);
            this.LastNamePanel.ResumeLayout(false);
            this.LastNamePanel.PerformLayout();
            this.FirstNamePanel.ResumeLayout(false);
            this.FirstNamePanel.PerformLayout();
            this.AddressPanel.ResumeLayout(false);
            this.AddressPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MasterNameDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel LastNamePanel;
        private TerraScan.UI.Controls.TerraScanTextBox LastNameTextBox;
        private System.Windows.Forms.Label LastNameLabel;
        private System.Windows.Forms.Panel FirstNamePanel;
        private TerraScan.UI.Controls.TerraScanTextBox FirstNameTextBox;
        private System.Windows.Forms.Label FirstNameLabel;
        private System.Windows.Forms.Panel AddressPanel;
        private TerraScan.UI.Controls.TerraScanTextBox AddressTextBox;
        private System.Windows.Forms.Label AddressLabel;
        private TerraScan.UI.Controls.TerraScanDataGridView MasterNameDataGridView;
        private System.Windows.Forms.VScrollBar MasterNameVerticalScroll;
        private TerraScan.UI.Controls.TerraScanLinkLabel MasterNameLinkLabel;
        private TerraScan.UI.Controls.TerraScanButton AcceptMasterNameButton;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label label1;
        private TerraScan.UI.Controls.TerraScanButton MasterNameCancelButton;
        private TerraScan.UI.Controls.TerraScanButton ClearButton;
        private TerraScan.UI.Controls.TerraScanButton SearchButton;
        private System.Windows.Forms.Label RecordCountLabel;
        private System.Windows.Forms.Panel ScrollPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn OwnerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn sa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel HelpLink;
    }
}