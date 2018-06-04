namespace D1500
{
    partial class F1505
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1505));
            this.DistrictPanel = new System.Windows.Forms.Panel();
            this.DistrictTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DistrictLabel = new System.Windows.Forms.Label();
            this.DistrictTypepanel = new System.Windows.Forms.Panel();
            this.DistrictTypeCOmbo = new TerraScan.UI.Controls.TerraScanComboBox();
            this.DistrictTypelabel = new System.Windows.Forms.Label();
            this.RollYearpanel = new System.Windows.Forms.Panel();
            this.RollYearTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.RollYearlabel = new System.Windows.Forms.Label();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.DescriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.Descriptionlabel = new System.Windows.Forms.Label();
            this.Activepanel = new System.Windows.Forms.Panel();
            this.Activelabel = new System.Windows.Forms.Label();
            this.ActiveComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.ExciseRatepanel = new System.Windows.Forms.Panel();
            this.ExciseRatelabel = new System.Windows.Forms.Label();
            this.Exciselabel = new TerraScan.UI.Controls.TerraScanLinkLabel();
            this.ExciseRatePictureBox = new System.Windows.Forms.Button();
            this.CreateButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.DistrictPanel.SuspendLayout();
            this.DistrictTypepanel.SuspendLayout();
            this.RollYearpanel.SuspendLayout();
            this.DescriptionPanel.SuspendLayout();
            this.Activepanel.SuspendLayout();
            this.ExciseRatepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // DistrictPanel
            // 
            this.DistrictPanel.BackColor = System.Drawing.Color.White;
            this.DistrictPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DistrictPanel.Controls.Add(this.DistrictTextBox);
            this.DistrictPanel.Controls.Add(this.DistrictLabel);
            this.DistrictPanel.Location = new System.Drawing.Point(13, 10);
            this.DistrictPanel.Name = "DistrictPanel";
            this.DistrictPanel.Size = new System.Drawing.Size(100, 37);
            this.DistrictPanel.TabIndex = 2;
            this.DistrictPanel.TabStop = true;
            // 
            // DistrictTextBox
            // 
            this.DistrictTextBox.AllowClick = true;
            this.DistrictTextBox.AllowNegativeSign = false;
            this.DistrictTextBox.ApplyCFGFormat = false;
            this.DistrictTextBox.ApplyCurrencyFormat = false;
            this.DistrictTextBox.ApplyFocusColor = true;
            this.DistrictTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.DistrictTextBox.ApplyNegativeStandard = true;
            this.DistrictTextBox.ApplyParentFocusColor = true;
            this.DistrictTextBox.ApplyTimeFormat = false;
            this.DistrictTextBox.BackColor = System.Drawing.Color.White;
            this.DistrictTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DistrictTextBox.CFromatWihoutSymbol = false;
            this.DistrictTextBox.CheckForEmpty = false;
            this.DistrictTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DistrictTextBox.Digits = -1;
            this.DistrictTextBox.EmptyDecimalValue = false;
            this.DistrictTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.DistrictTextBox.ForeColor = System.Drawing.Color.Black;
            this.DistrictTextBox.IsEditable = false;
            this.DistrictTextBox.IsQueryableFileld = false;
            this.DistrictTextBox.Location = new System.Drawing.Point(18, 16);
            this.DistrictTextBox.LockKeyPress = false;
            this.DistrictTextBox.MaxLength = 50;
            this.DistrictTextBox.Name = "DistrictTextBox";
            this.DistrictTextBox.PersistDefaultColor = false;
            this.DistrictTextBox.Precision = 2;
            this.DistrictTextBox.QueryingFileldName = "";
            this.DistrictTextBox.SetColorFlag = false;
            this.DistrictTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DistrictTextBox.Size = new System.Drawing.Size(50, 16);
            this.DistrictTextBox.SpecialCharacter = "%";
            this.DistrictTextBox.TabIndex = 2;
            this.DistrictTextBox.TextCustomFormat = "$#,##0.00";
            this.DistrictTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DistrictTextBox.WholeInteger = false;
            this.DistrictTextBox.TextChanged += new System.EventHandler(this.DistrictTextBox_Leave);
            this.DistrictTextBox.Leave += new System.EventHandler(this.DistrictTextBox_Leave);
            // 
            // DistrictLabel
            // 
            this.DistrictLabel.AutoSize = true;
            this.DistrictLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DistrictLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistrictLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DistrictLabel.Location = new System.Drawing.Point(1, 1);
            this.DistrictLabel.Name = "DistrictLabel";
            this.DistrictLabel.Size = new System.Drawing.Size(49, 14);
            this.DistrictLabel.TabIndex = 62;
            this.DistrictLabel.Text = "District:";
            // 
            // DistrictTypepanel
            // 
            this.DistrictTypepanel.BackColor = System.Drawing.Color.White;
            this.DistrictTypepanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DistrictTypepanel.Controls.Add(this.DistrictTypeCOmbo);
            this.DistrictTypepanel.Controls.Add(this.DistrictTypelabel);
            this.DistrictTypepanel.Location = new System.Drawing.Point(112, 10);
            this.DistrictTypepanel.Name = "DistrictTypepanel";
            this.DistrictTypepanel.Size = new System.Drawing.Size(200, 37);
            this.DistrictTypepanel.TabIndex = 3;
            this.DistrictTypepanel.TabStop = true;
            // 
            // DistrictTypeCOmbo
            // 
            this.DistrictTypeCOmbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DistrictTypeCOmbo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DistrictTypeCOmbo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistrictTypeCOmbo.ForeColor = System.Drawing.Color.Black;
            this.DistrictTypeCOmbo.FormattingEnabled = true;
            this.DistrictTypeCOmbo.Location = new System.Drawing.Point(25, 13);
            this.DistrictTypeCOmbo.Name = "DistrictTypeCOmbo";
            this.DistrictTypeCOmbo.Size = new System.Drawing.Size(150, 24);
            this.DistrictTypeCOmbo.TabIndex = 15;
            this.DistrictTypeCOmbo.Tag = "";
            this.DistrictTypeCOmbo.Leave += new System.EventHandler(this.DistrictTextBox_Leave);
            // 
            // DistrictTypelabel
            // 
            this.DistrictTypelabel.AutoSize = true;
            this.DistrictTypelabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DistrictTypelabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistrictTypelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DistrictTypelabel.Location = new System.Drawing.Point(1, 1);
            this.DistrictTypelabel.Name = "DistrictTypelabel";
            this.DistrictTypelabel.Size = new System.Drawing.Size(78, 14);
            this.DistrictTypelabel.TabIndex = 62;
            this.DistrictTypelabel.Text = "District Type:";
            // 
            // RollYearpanel
            // 
            this.RollYearpanel.BackColor = System.Drawing.Color.White;
            this.RollYearpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RollYearpanel.Controls.Add(this.RollYearTextBox);
            this.RollYearpanel.Controls.Add(this.RollYearlabel);
            this.RollYearpanel.Location = new System.Drawing.Point(311, 10);
            this.RollYearpanel.Name = "RollYearpanel";
            this.RollYearpanel.Size = new System.Drawing.Size(100, 37);
            this.RollYearpanel.TabIndex = 4;
            this.RollYearpanel.TabStop = true;
            // 
            // RollYearTextBox
            // 
            this.RollYearTextBox.AllowClick = true;
            this.RollYearTextBox.AllowNegativeSign = false;
            this.RollYearTextBox.ApplyCFGFormat = false;
            this.RollYearTextBox.ApplyCurrencyFormat = false;
            this.RollYearTextBox.ApplyFocusColor = true;
            this.RollYearTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.RollYearTextBox.ApplyNegativeStandard = true;
            this.RollYearTextBox.ApplyParentFocusColor = true;
            this.RollYearTextBox.ApplyTimeFormat = false;
            this.RollYearTextBox.BackColor = System.Drawing.Color.White;
            this.RollYearTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RollYearTextBox.CFromatWihoutSymbol = false;
            this.RollYearTextBox.CheckForEmpty = false;
            this.RollYearTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RollYearTextBox.Digits = -1;
            this.RollYearTextBox.EmptyDecimalValue = false;
            this.RollYearTextBox.Enabled = false;
            this.RollYearTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.RollYearTextBox.ForeColor = System.Drawing.Color.Black;
            this.RollYearTextBox.IsEditable = false;
            this.RollYearTextBox.IsQueryableFileld = false;
            this.RollYearTextBox.Location = new System.Drawing.Point(18, 16);
            this.RollYearTextBox.LockKeyPress = false;
            this.RollYearTextBox.MaxLength = 50;
            this.RollYearTextBox.Name = "RollYearTextBox";
            this.RollYearTextBox.PersistDefaultColor = false;
            this.RollYearTextBox.Precision = 2;
            this.RollYearTextBox.QueryingFileldName = "";
            this.RollYearTextBox.SetColorFlag = false;
            this.RollYearTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.RollYearTextBox.Size = new System.Drawing.Size(50, 16);
            this.RollYearTextBox.SpecialCharacter = "%";
            this.RollYearTextBox.TabIndex = 2;
            this.RollYearTextBox.TextCustomFormat = "$#,##0.00";
            this.RollYearTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.RollYearTextBox.WholeInteger = false;
            this.RollYearTextBox.Leave += new System.EventHandler(this.DistrictTextBox_Leave);
            // 
            // RollYearlabel
            // 
            this.RollYearlabel.AutoSize = true;
            this.RollYearlabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RollYearlabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RollYearlabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.RollYearlabel.Location = new System.Drawing.Point(1, 1);
            this.RollYearlabel.Name = "RollYearlabel";
            this.RollYearlabel.Size = new System.Drawing.Size(57, 14);
            this.RollYearlabel.TabIndex = 62;
            this.RollYearlabel.Text = "Roll Year:";
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.BackColor = System.Drawing.Color.White;
            this.DescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionPanel.Controls.Add(this.DescriptionTextBox);
            this.DescriptionPanel.Controls.Add(this.Descriptionlabel);
            this.DescriptionPanel.Location = new System.Drawing.Point(13, 46);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(398, 37);
            this.DescriptionPanel.TabIndex = 5;
            this.DescriptionPanel.TabStop = true;
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.AllowClick = true;
            this.DescriptionTextBox.AllowNegativeSign = false;
            this.DescriptionTextBox.ApplyCFGFormat = false;
            this.DescriptionTextBox.ApplyCurrencyFormat = false;
            this.DescriptionTextBox.ApplyFocusColor = true;
            this.DescriptionTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.DescriptionTextBox.ApplyNegativeStandard = true;
            this.DescriptionTextBox.ApplyParentFocusColor = true;
            this.DescriptionTextBox.ApplyTimeFormat = false;
            this.DescriptionTextBox.BackColor = System.Drawing.Color.White;
            this.DescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DescriptionTextBox.CFromatWihoutSymbol = false;
            this.DescriptionTextBox.CheckForEmpty = false;
            this.DescriptionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DescriptionTextBox.Digits = -1;
            this.DescriptionTextBox.EmptyDecimalValue = false;
            this.DescriptionTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.DescriptionTextBox.ForeColor = System.Drawing.Color.Black;
            this.DescriptionTextBox.IsEditable = false;
            this.DescriptionTextBox.IsQueryableFileld = false;
            this.DescriptionTextBox.Location = new System.Drawing.Point(18, 16);
            this.DescriptionTextBox.LockKeyPress = false;
            this.DescriptionTextBox.MaxLength = 50;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.PersistDefaultColor = false;
            this.DescriptionTextBox.Precision = 2;
            this.DescriptionTextBox.QueryingFileldName = "";
            this.DescriptionTextBox.SetColorFlag = false;
            this.DescriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescriptionTextBox.Size = new System.Drawing.Size(350, 16);
            this.DescriptionTextBox.SpecialCharacter = "%";
            this.DescriptionTextBox.TabIndex = 2;
            this.DescriptionTextBox.TextCustomFormat = "$#,##0.00";
            this.DescriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescriptionTextBox.WholeInteger = false;
            this.DescriptionTextBox.Leave += new System.EventHandler(this.DistrictTextBox_Leave);
            // 
            // Descriptionlabel
            // 
            this.Descriptionlabel.AutoSize = true;
            this.Descriptionlabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Descriptionlabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descriptionlabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.Descriptionlabel.Location = new System.Drawing.Point(1, 1);
            this.Descriptionlabel.Name = "Descriptionlabel";
            this.Descriptionlabel.Size = new System.Drawing.Size(73, 14);
            this.Descriptionlabel.TabIndex = 62;
            this.Descriptionlabel.Text = "Description:";
            // 
            // Activepanel
            // 
            this.Activepanel.BackColor = System.Drawing.Color.White;
            this.Activepanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Activepanel.Controls.Add(this.Activelabel);
            this.Activepanel.Controls.Add(this.ActiveComboBox);
            this.Activepanel.Location = new System.Drawing.Point(13, 82);
            this.Activepanel.Name = "Activepanel";
            this.Activepanel.Size = new System.Drawing.Size(100, 37);
            this.Activepanel.TabIndex = 6;
            this.Activepanel.TabStop = true;
            // 
            // Activelabel
            // 
            this.Activelabel.AutoSize = true;
            this.Activelabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Activelabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Activelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.Activelabel.Location = new System.Drawing.Point(1, 1);
            this.Activelabel.Name = "Activelabel";
            this.Activelabel.Size = new System.Drawing.Size(44, 14);
            this.Activelabel.TabIndex = 62;
            this.Activelabel.Text = "Active:";
            // 
            // ActiveComboBox
            // 
            this.ActiveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActiveComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ActiveComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActiveComboBox.ForeColor = System.Drawing.Color.Black;
            this.ActiveComboBox.FormattingEnabled = true;
            this.ActiveComboBox.Location = new System.Drawing.Point(25, 13);
            this.ActiveComboBox.Name = "ActiveComboBox";
            this.ActiveComboBox.Size = new System.Drawing.Size(65, 24);
            this.ActiveComboBox.TabIndex = 7;
            this.ActiveComboBox.Tag = "";
            this.ActiveComboBox.SelectionChangeCommitted += new System.EventHandler(this.DistrictTextBox_Leave);
            // 
            // ExciseRatepanel
            // 
            this.ExciseRatepanel.BackColor = System.Drawing.Color.White;
            this.ExciseRatepanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExciseRatepanel.Controls.Add(this.ExciseRatelabel);
            this.ExciseRatepanel.Controls.Add(this.Exciselabel);
            this.ExciseRatepanel.Controls.Add(this.ExciseRatePictureBox);
            this.ExciseRatepanel.Location = new System.Drawing.Point(112, 82);
            this.ExciseRatepanel.Name = "ExciseRatepanel";
            this.ExciseRatepanel.Size = new System.Drawing.Size(299, 37);
            this.ExciseRatepanel.TabIndex = 7;
            this.ExciseRatepanel.TabStop = true;
            // 
            // ExciseRatelabel
            // 
            this.ExciseRatelabel.AutoSize = true;
            this.ExciseRatelabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ExciseRatelabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExciseRatelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ExciseRatelabel.Location = new System.Drawing.Point(1, 1);
            this.ExciseRatelabel.Name = "ExciseRatelabel";
            this.ExciseRatelabel.Size = new System.Drawing.Size(114, 14);
            this.ExciseRatelabel.TabIndex = 62;
            this.ExciseRatelabel.Text = "Excise Rate District:";
            // 
            // Exciselabel
            // 
            this.Exciselabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exciselabel.FormDllName = null;
            this.Exciselabel.FormId = 0;
            this.Exciselabel.Location = new System.Drawing.Point(16, 18);
            this.Exciselabel.MenuName = null;
            this.Exciselabel.Name = "Exciselabel";
            this.Exciselabel.PermissionOpen = 0;
            this.Exciselabel.Size = new System.Drawing.Size(200, 16);
            this.Exciselabel.TabIndex = 2;
            this.Exciselabel.TextCustomFormat = "#,##0.00";
            this.Exciselabel.ValidateType = TerraScan.UI.Controls.TerraScanLinkLabel.ControlValidationType.Text;
            this.Exciselabel.Click += new System.EventHandler(this.Exciselabel_Click);
            // 
            // ExciseRatePictureBox
            // 
            this.ExciseRatePictureBox.BackColor = System.Drawing.Color.White;
            this.ExciseRatePictureBox.FlatAppearance.BorderSize = 0;
            this.ExciseRatePictureBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExciseRatePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ExciseRatePictureBox.Image")));
            this.ExciseRatePictureBox.Location = new System.Drawing.Point(250, 9);
            this.ExciseRatePictureBox.Name = "ExciseRatePictureBox";
            this.ExciseRatePictureBox.Size = new System.Drawing.Size(22, 23);
            this.ExciseRatePictureBox.TabIndex = 3;
            this.ExciseRatePictureBox.UseVisualStyleBackColor = false;
            this.ExciseRatePictureBox.Click += new System.EventHandler(this.ExciseRatePictureBox_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.ActualPermission = false;
            this.CreateButton.ApplyDisableBehaviour = false;
            this.CreateButton.AutoSize = true;
            this.CreateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CreateButton.BorderColor = System.Drawing.Color.Wheat;
            this.CreateButton.CommentPriority = false;
            this.CreateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CreateButton.EnableAutoPrint = false;
            this.CreateButton.Enabled = false;
            this.CreateButton.FilterStatus = false;
            this.CreateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CreateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateButton.FocusRectangleEnabled = true;
            this.CreateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CreateButton.ImageSelected = false;
            this.CreateButton.Location = new System.Drawing.Point(13, 130);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.NewPadding = 5;
            this.CreateButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.CreateButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CreateButton.Size = new System.Drawing.Size(110, 30);
            this.CreateButton.StatusIndicator = false;
            this.CreateButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CreateButton.StatusOffText = null;
            this.CreateButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CreateButton.StatusOnText = null;
            this.CreateButton.TabIndex = 8;
            this.CreateButton.TabStop = false;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = false;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.ActualPermission = false;
            this.CancelButton.ApplyDisableBehaviour = false;
            this.CancelButton.AutoSize = true;
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelButton.CommentPriority = false;
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CancelButton.EnableAutoPrint = false;
            this.CancelButton.FilterStatus = false;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.FocusRectangleEnabled = true;
            this.CancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelButton.ImageSelected = false;
            this.CancelButton.Location = new System.Drawing.Point(301, 130);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NewPadding = 5;
            this.CancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.CancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelButton.Size = new System.Drawing.Size(110, 30);
            this.CancelButton.StatusIndicator = false;
            this.CancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelButton.StatusOffText = null;
            this.CancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelButton.StatusOnText = null;
            this.CancelButton.TabIndex = 9;
            this.CancelButton.TabStop = false;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(11, 166);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(400, 2);
            this.FormLinePanel.TabIndex = 166;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(12, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 167;
            this.label1.Text = "1505";
            // 
            // F1505
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(426, 192);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.ExciseRatepanel);
            this.Controls.Add(this.Activepanel);
            this.Controls.Add(this.DescriptionPanel);
            this.Controls.Add(this.RollYearpanel);
            this.Controls.Add(this.DistrictTypepanel);
            this.Controls.Add(this.DistrictPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F1505";
            this.Text = "TerraScan T2- District Copy";
            this.Load += new System.EventHandler(this.F1505_Load);
            this.DistrictPanel.ResumeLayout(false);
            this.DistrictPanel.PerformLayout();
            this.DistrictTypepanel.ResumeLayout(false);
            this.DistrictTypepanel.PerformLayout();
            this.RollYearpanel.ResumeLayout(false);
            this.RollYearpanel.PerformLayout();
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.Activepanel.ResumeLayout(false);
            this.Activepanel.PerformLayout();
            this.ExciseRatepanel.ResumeLayout(false);
            this.ExciseRatepanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DistrictPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DistrictTextBox;
        private System.Windows.Forms.Label DistrictLabel;
        private System.Windows.Forms.Panel DistrictTypepanel;
        private TerraScan.UI.Controls.TerraScanComboBox DistrictTypeCOmbo;
        private System.Windows.Forms.Label DistrictTypelabel;
        private System.Windows.Forms.Panel RollYearpanel;
        private TerraScan.UI.Controls.TerraScanTextBox RollYearTextBox;
        private System.Windows.Forms.Label RollYearlabel;
        private System.Windows.Forms.Panel DescriptionPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DescriptionTextBox;
        private System.Windows.Forms.Label Descriptionlabel;
        private System.Windows.Forms.Panel Activepanel;
        //private TerraScan.UI.Controls.TerraScanTextBox ActiveTextBox;
        private TerraScan.UI.Controls.TerraScanComboBox ActiveComboBox;
        private System.Windows.Forms.Label Activelabel;
        private System.Windows.Forms.Panel ExciseRatepanel;
        private System.Windows.Forms.Label ExciseRatelabel;
        private TerraScan.UI.Controls.TerraScanLinkLabel Exciselabel;
        private TerraScan.UI.Controls.TerraScanButton CreateButton;
        private TerraScan.UI.Controls.TerraScanButton CancelButton;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ExciseRatePictureBox;
    }
}