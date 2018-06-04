namespace D20003
{
    partial class F25011
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F25011));
            this.StreetListingPictureBox = new System.Windows.Forms.PictureBox();
            this.StreetListMgmtToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.HeaderMaintenancePanel = new System.Windows.Forms.Panel();
            this.SuffixPanel = new System.Windows.Forms.Panel();
            this.SuffixComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.SuffixComboBoxTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.SuffixLabel = new System.Windows.Forms.Label();
            this.DirectionalPanel = new System.Windows.Forms.Panel();
            this.DirectionalComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.DirectionalLabel = new System.Windows.Forms.Label();
            this.CityPanel = new System.Windows.Forms.Panel();
            this.CityComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.CityComboxTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CityLabel = new System.Windows.Forms.Label();
            this.ZipCodePanel = new System.Windows.Forms.Panel();
            this.ZipCodeTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.ZipCodeLabel = new System.Windows.Forms.Label();
            this.StreetNamePanel = new System.Windows.Forms.Panel();
            this.StreetNameTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.StreetNameLabel = new System.Windows.Forms.Label();
            this.FullStreetNamePanel = new System.Windows.Forms.Panel();
            this.FullStreetNameTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.FullStreetNameLabel = new System.Windows.Forms.Label();
            this.StreetMaintenanceSecPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.StreetListingPictureBox)).BeginInit();
            this.HeaderMaintenancePanel.SuspendLayout();
            this.SuffixPanel.SuspendLayout();
            this.DirectionalPanel.SuspendLayout();
            this.CityPanel.SuspendLayout();
            this.ZipCodePanel.SuspendLayout();
            this.StreetNamePanel.SuspendLayout();
            this.FullStreetNamePanel.SuspendLayout();
            this.StreetMaintenanceSecPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StreetListingPictureBox
            // 
            this.StreetListingPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("StreetListingPictureBox.Image")));
            this.StreetListingPictureBox.Location = new System.Drawing.Point(761, 0);
            this.StreetListingPictureBox.Name = "StreetListingPictureBox";
            this.StreetListingPictureBox.Size = new System.Drawing.Size(42, 80);
            this.StreetListingPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StreetListingPictureBox.TabIndex = 61;
            this.StreetListingPictureBox.TabStop = false;
            this.StreetListingPictureBox.Click += new System.EventHandler(this.StreetListingPictureBox_Click);
            this.StreetListingPictureBox.MouseEnter += new System.EventHandler(this.StreetListingPictureBox_MouseEnter);
            // 
            // HeaderMaintenancePanel
            // 
            this.HeaderMaintenancePanel.BackColor = System.Drawing.Color.White;
            this.HeaderMaintenancePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HeaderMaintenancePanel.Controls.Add(this.SuffixPanel);
            this.HeaderMaintenancePanel.Controls.Add(this.DirectionalPanel);
            this.HeaderMaintenancePanel.Controls.Add(this.CityPanel);
            this.HeaderMaintenancePanel.Controls.Add(this.ZipCodePanel);
            this.HeaderMaintenancePanel.Controls.Add(this.StreetNamePanel);
            this.HeaderMaintenancePanel.Controls.Add(this.FullStreetNamePanel);
            this.HeaderMaintenancePanel.Location = new System.Drawing.Point(-1, -1);
            this.HeaderMaintenancePanel.Name = "HeaderMaintenancePanel";
            this.HeaderMaintenancePanel.Size = new System.Drawing.Size(768, 80);
            this.HeaderMaintenancePanel.TabIndex = 2;
            this.HeaderMaintenancePanel.TabStop = true;
            // 
            // SuffixPanel
            // 
            this.SuffixPanel.BackColor = System.Drawing.Color.White;
            this.SuffixPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SuffixPanel.Controls.Add(this.SuffixComboBox);
            this.SuffixPanel.Controls.Add(this.SuffixComboBoxTextBox);
            this.SuffixPanel.Controls.Add(this.SuffixLabel);
            this.SuffixPanel.Location = new System.Drawing.Point(403, 38);
            this.SuffixPanel.Name = "SuffixPanel";
            this.SuffixPanel.Size = new System.Drawing.Size(157, 41);
            this.SuffixPanel.TabIndex = 9;
            this.SuffixPanel.TabStop = true;
            // 
            // SuffixComboBox
            // 
            this.SuffixComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SuffixComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SuffixComboBox.BackColor = System.Drawing.Color.White;
            this.SuffixComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SuffixComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.SuffixComboBox.FormattingEnabled = true;
            this.SuffixComboBox.Location = new System.Drawing.Point(17, 12);
            this.SuffixComboBox.Name = "SuffixComboBox";
            this.SuffixComboBox.Size = new System.Drawing.Size(128, 24);
            this.SuffixComboBox.TabIndex = 10;
            this.SuffixComboBox.TextChanged += new System.EventHandler(this.SuffixComboBox_TextChanged);
            // 
            // SuffixComboBoxTextBox
            // 
            this.SuffixComboBoxTextBox.AllowClick = true;
            this.SuffixComboBoxTextBox.AllowNegativeSign = false;
            this.SuffixComboBoxTextBox.ApplyCFGFormat = false;
            this.SuffixComboBoxTextBox.ApplyCurrencyFormat = false;
            this.SuffixComboBoxTextBox.ApplyFocusColor = true;
            this.SuffixComboBoxTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.SuffixComboBoxTextBox.ApplyNegativeStandard = true;
            this.SuffixComboBoxTextBox.ApplyParentFocusColor = true;
            this.SuffixComboBoxTextBox.ApplyTimeFormat = false;
            this.SuffixComboBoxTextBox.BackColor = System.Drawing.Color.White;
            this.SuffixComboBoxTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SuffixComboBoxTextBox.CFromatWihoutSymbol = false;
            this.SuffixComboBoxTextBox.CheckForEmpty = false;
            this.SuffixComboBoxTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SuffixComboBoxTextBox.Digits = -1;
            this.SuffixComboBoxTextBox.EmptyDecimalValue = false;
            this.SuffixComboBoxTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.SuffixComboBoxTextBox.IsEditable = false;
            this.SuffixComboBoxTextBox.IsQueryableFileld = true;
            this.SuffixComboBoxTextBox.Location = new System.Drawing.Point(6, 16);
            this.SuffixComboBoxTextBox.LockKeyPress = false;
            this.SuffixComboBoxTextBox.MaxLength = 50;
            this.SuffixComboBoxTextBox.Name = "SuffixComboBoxTextBox";
            this.SuffixComboBoxTextBox.PersistDefaultColor = false;
            this.SuffixComboBoxTextBox.Precision = 2;
            this.SuffixComboBoxTextBox.QueryingFileldName = "";
            this.SuffixComboBoxTextBox.SetColorFlag = false;
            this.SuffixComboBoxTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.SuffixComboBoxTextBox.Size = new System.Drawing.Size(117, 16);
            this.SuffixComboBoxTextBox.SpecialCharacter = "%";
            this.SuffixComboBoxTextBox.TabIndex = 4;
            this.SuffixComboBoxTextBox.TabStop = false;
            this.SuffixComboBoxTextBox.TextCustomFormat = "$#,##0.00";
            this.SuffixComboBoxTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.SuffixComboBoxTextBox.Visible = false;
            this.SuffixComboBoxTextBox.WholeInteger = false;
            // 
            // SuffixLabel
            // 
            this.SuffixLabel.AutoSize = true;
            this.SuffixLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SuffixLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.SuffixLabel.Location = new System.Drawing.Point(1, -1);
            this.SuffixLabel.Name = "SuffixLabel";
            this.SuffixLabel.Size = new System.Drawing.Size(41, 14);
            this.SuffixLabel.TabIndex = 1;
            this.SuffixLabel.Text = "Suffix:";
            // 
            // DirectionalPanel
            // 
            this.DirectionalPanel.BackColor = System.Drawing.Color.White;
            this.DirectionalPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DirectionalPanel.Controls.Add(this.DirectionalComboBox);
            this.DirectionalPanel.Controls.Add(this.DirectionalLabel);
            this.DirectionalPanel.Location = new System.Drawing.Point(240, 38);
            this.DirectionalPanel.Name = "DirectionalPanel";
            this.DirectionalPanel.Size = new System.Drawing.Size(164, 41);
            this.DirectionalPanel.TabIndex = 7;
            this.DirectionalPanel.TabStop = true;
            // 
            // DirectionalComboBox
            // 
            this.DirectionalComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.DirectionalComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DirectionalComboBox.BackColor = System.Drawing.Color.White;
            this.DirectionalComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DirectionalComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.DirectionalComboBox.FormattingEnabled = true;
            this.DirectionalComboBox.Location = new System.Drawing.Point(17, 13);
            this.DirectionalComboBox.Name = "DirectionalComboBox";
            this.DirectionalComboBox.Size = new System.Drawing.Size(139, 24);
            this.DirectionalComboBox.TabIndex = 11;
            this.DirectionalComboBox.TextChanged += new System.EventHandler(this.DirectionalComboBox_TextChanged);
            // 
            // DirectionalLabel
            // 
            this.DirectionalLabel.AutoSize = true;
            this.DirectionalLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectionalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DirectionalLabel.Location = new System.Drawing.Point(1, -1);
            this.DirectionalLabel.Name = "DirectionalLabel";
            this.DirectionalLabel.Size = new System.Drawing.Size(68, 14);
            this.DirectionalLabel.TabIndex = 1;
            this.DirectionalLabel.Text = "Directional:";
            // 
            // CityPanel
            // 
            this.CityPanel.BackColor = System.Drawing.Color.White;
            this.CityPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CityPanel.Controls.Add(this.CityComboBox);
            this.CityPanel.Controls.Add(this.CityComboxTextBox);
            this.CityPanel.Controls.Add(this.CityLabel);
            this.CityPanel.Location = new System.Drawing.Point(487, -1);
            this.CityPanel.Name = "CityPanel";
            this.CityPanel.Size = new System.Drawing.Size(280, 40);
            this.CityPanel.TabIndex = 3;
            this.CityPanel.TabStop = true;
            // 
            // CityComboBox
            // 
            this.CityComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CityComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CityComboBox.BackColor = System.Drawing.Color.White;
            this.CityComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CityComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.CityComboBox.FormattingEnabled = true;
            this.CityComboBox.Location = new System.Drawing.Point(17, 12);
            this.CityComboBox.Name = "CityComboBox";
            this.CityComboBox.Size = new System.Drawing.Size(251, 24);
            this.CityComboBox.TabIndex = 4;
            this.CityComboBox.TextChanged += new System.EventHandler(this.CityComboBox_TextChanged);
            // 
            // CityComboxTextBox
            // 
            this.CityComboxTextBox.AllowClick = true;
            this.CityComboxTextBox.AllowNegativeSign = false;
            this.CityComboxTextBox.ApplyCFGFormat = false;
            this.CityComboxTextBox.ApplyCurrencyFormat = false;
            this.CityComboxTextBox.ApplyFocusColor = true;
            this.CityComboxTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.CityComboxTextBox.ApplyNegativeStandard = true;
            this.CityComboxTextBox.ApplyParentFocusColor = true;
            this.CityComboxTextBox.ApplyTimeFormat = false;
            this.CityComboxTextBox.BackColor = System.Drawing.Color.White;
            this.CityComboxTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CityComboxTextBox.CFromatWihoutSymbol = false;
            this.CityComboxTextBox.CheckForEmpty = false;
            this.CityComboxTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CityComboxTextBox.Digits = -1;
            this.CityComboxTextBox.EmptyDecimalValue = false;
            this.CityComboxTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.CityComboxTextBox.IsEditable = false;
            this.CityComboxTextBox.IsQueryableFileld = true;
            this.CityComboxTextBox.Location = new System.Drawing.Point(17, 16);
            this.CityComboxTextBox.LockKeyPress = false;
            this.CityComboxTextBox.MaxLength = 50;
            this.CityComboxTextBox.Name = "CityComboxTextBox";
            this.CityComboxTextBox.PersistDefaultColor = false;
            this.CityComboxTextBox.Precision = 2;
            this.CityComboxTextBox.QueryingFileldName = "";
            this.CityComboxTextBox.SetColorFlag = false;
            this.CityComboxTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CityComboxTextBox.Size = new System.Drawing.Size(231, 16);
            this.CityComboxTextBox.SpecialCharacter = "%";
            this.CityComboxTextBox.TabIndex = 4;
            this.CityComboxTextBox.TabStop = false;
            this.CityComboxTextBox.TextCustomFormat = "$#,##0.00";
            this.CityComboxTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CityComboxTextBox.Visible = false;
            this.CityComboxTextBox.WholeInteger = false;
            // 
            // CityLabel
            // 
            this.CityLabel.AutoSize = true;
            this.CityLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CityLabel.Location = new System.Drawing.Point(1, -1);
            this.CityLabel.Name = "CityLabel";
            this.CityLabel.Size = new System.Drawing.Size(31, 14);
            this.CityLabel.TabIndex = 1;
            this.CityLabel.Text = "City:";
            // 
            // ZipCodePanel
            // 
            this.ZipCodePanel.BackColor = System.Drawing.Color.White;
            this.ZipCodePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZipCodePanel.Controls.Add(this.ZipCodeTextBox);
            this.ZipCodePanel.Controls.Add(this.ZipCodeLabel);
            this.ZipCodePanel.Location = new System.Drawing.Point(559, 38);
            this.ZipCodePanel.Name = "ZipCodePanel";
            this.ZipCodePanel.Size = new System.Drawing.Size(208, 41);
            this.ZipCodePanel.TabIndex = 11;
            this.ZipCodePanel.TabStop = true;
            // 
            // ZipCodeTextBox
            // 
            this.ZipCodeTextBox.AllowClick = true;
            this.ZipCodeTextBox.AllowNegativeSign = false;
            this.ZipCodeTextBox.ApplyCFGFormat = false;
            this.ZipCodeTextBox.ApplyCurrencyFormat = false;
            this.ZipCodeTextBox.ApplyFocusColor = true;
            this.ZipCodeTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.ZipCodeTextBox.ApplyNegativeStandard = true;
            this.ZipCodeTextBox.ApplyParentFocusColor = true;
            this.ZipCodeTextBox.ApplyTimeFormat = false;
            this.ZipCodeTextBox.BackColor = System.Drawing.Color.White;
            this.ZipCodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ZipCodeTextBox.CFromatWihoutSymbol = false;
            this.ZipCodeTextBox.CheckForEmpty = false;
            this.ZipCodeTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ZipCodeTextBox.Digits = -1;
            this.ZipCodeTextBox.EmptyDecimalValue = false;
            this.ZipCodeTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.ZipCodeTextBox.ForeColor = System.Drawing.Color.Black;
            this.ZipCodeTextBox.IsEditable = false;
            this.ZipCodeTextBox.IsQueryableFileld = false;
            this.ZipCodeTextBox.Location = new System.Drawing.Point(17, 16);
            this.ZipCodeTextBox.LockKeyPress = false;
            this.ZipCodeTextBox.MaxLength = 50;
            this.ZipCodeTextBox.Name = "ZipCodeTextBox";
            this.ZipCodeTextBox.PersistDefaultColor = false;
            this.ZipCodeTextBox.Precision = 2;
            this.ZipCodeTextBox.QueryingFileldName = "";
            this.ZipCodeTextBox.SetColorFlag = false;
            this.ZipCodeTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ZipCodeTextBox.Size = new System.Drawing.Size(186, 16);
            this.ZipCodeTextBox.SpecialCharacter = "%";
            this.ZipCodeTextBox.TabIndex = 12;
            this.ZipCodeTextBox.TextCustomFormat = "$#,##0.00";
            this.ZipCodeTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.ZipCodeTextBox.WholeInteger = false;
            this.ZipCodeTextBox.TextChanged += new System.EventHandler(this.ZipCodeTextBox_TextChanged);
            // 
            // ZipCodeLabel
            // 
            this.ZipCodeLabel.AutoSize = true;
            this.ZipCodeLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ZipCodeLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZipCodeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ZipCodeLabel.Location = new System.Drawing.Point(1, 1);
            this.ZipCodeLabel.Name = "ZipCodeLabel";
            this.ZipCodeLabel.Size = new System.Drawing.Size(27, 14);
            this.ZipCodeLabel.TabIndex = 62;
            this.ZipCodeLabel.Text = "Zip:";
            // 
            // StreetNamePanel
            // 
            this.StreetNamePanel.BackColor = System.Drawing.Color.White;
            this.StreetNamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StreetNamePanel.Controls.Add(this.StreetNameTextBox);
            this.StreetNamePanel.Controls.Add(this.StreetNameLabel);
            this.StreetNamePanel.Location = new System.Drawing.Point(-1, 38);
            this.StreetNamePanel.Name = "StreetNamePanel";
            this.StreetNamePanel.Size = new System.Drawing.Size(242, 41);
            this.StreetNamePanel.TabIndex = 5;
            this.StreetNamePanel.TabStop = true;
            // 
            // StreetNameTextBox
            // 
            this.StreetNameTextBox.AllowClick = true;
            this.StreetNameTextBox.AllowNegativeSign = false;
            this.StreetNameTextBox.ApplyCFGFormat = false;
            this.StreetNameTextBox.ApplyCurrencyFormat = false;
            this.StreetNameTextBox.ApplyFocusColor = true;
            this.StreetNameTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.StreetNameTextBox.ApplyNegativeStandard = true;
            this.StreetNameTextBox.ApplyParentFocusColor = true;
            this.StreetNameTextBox.ApplyTimeFormat = false;
            this.StreetNameTextBox.BackColor = System.Drawing.Color.White;
            this.StreetNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StreetNameTextBox.CFromatWihoutSymbol = false;
            this.StreetNameTextBox.CheckForEmpty = false;
            this.StreetNameTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.StreetNameTextBox.Digits = -1;
            this.StreetNameTextBox.EmptyDecimalValue = false;
            this.StreetNameTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.StreetNameTextBox.ForeColor = System.Drawing.Color.Black;
            this.StreetNameTextBox.IsEditable = false;
            this.StreetNameTextBox.IsQueryableFileld = false;
            this.StreetNameTextBox.Location = new System.Drawing.Point(17, 16);
            this.StreetNameTextBox.LockKeyPress = false;
            this.StreetNameTextBox.MaxLength = 50;
            this.StreetNameTextBox.Name = "StreetNameTextBox";
            this.StreetNameTextBox.PersistDefaultColor = false;
            this.StreetNameTextBox.Precision = 2;
            this.StreetNameTextBox.QueryingFileldName = "";
            this.StreetNameTextBox.SetColorFlag = false;
            this.StreetNameTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.StreetNameTextBox.Size = new System.Drawing.Size(220, 16);
            this.StreetNameTextBox.SpecialCharacter = "%";
            this.StreetNameTextBox.TabIndex = 6;
            this.StreetNameTextBox.TextCustomFormat = "$#,##0.00";
            this.StreetNameTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.StreetNameTextBox.WholeInteger = false;
            this.StreetNameTextBox.TextChanged += new System.EventHandler(this.StreetNameTextBox_TextChanged);
            // 
            // StreetNameLabel
            // 
            this.StreetNameLabel.AutoSize = true;
            this.StreetNameLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.StreetNameLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StreetNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.StreetNameLabel.Location = new System.Drawing.Point(1, 1);
            this.StreetNameLabel.Name = "StreetNameLabel";
            this.StreetNameLabel.Size = new System.Drawing.Size(78, 14);
            this.StreetNameLabel.TabIndex = 62;
            this.StreetNameLabel.Text = "Street Name:";
            // 
            // FullStreetNamePanel
            // 
            this.FullStreetNamePanel.BackColor = System.Drawing.Color.White;
            this.FullStreetNamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FullStreetNamePanel.Controls.Add(this.FullStreetNameTextBox);
            this.FullStreetNamePanel.Controls.Add(this.FullStreetNameLabel);
            this.FullStreetNamePanel.Location = new System.Drawing.Point(-1, -1);
            this.FullStreetNamePanel.Name = "FullStreetNamePanel";
            this.FullStreetNamePanel.Size = new System.Drawing.Size(490, 40);
            this.FullStreetNamePanel.TabIndex = 1;
            this.FullStreetNamePanel.TabStop = true;
            // 
            // FullStreetNameTextBox
            // 
            this.FullStreetNameTextBox.AllowClick = true;
            this.FullStreetNameTextBox.AllowNegativeSign = false;
            this.FullStreetNameTextBox.ApplyCFGFormat = false;
            this.FullStreetNameTextBox.ApplyCurrencyFormat = false;
            this.FullStreetNameTextBox.ApplyFocusColor = true;
            this.FullStreetNameTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.FullStreetNameTextBox.ApplyNegativeStandard = true;
            this.FullStreetNameTextBox.ApplyParentFocusColor = true;
            this.FullStreetNameTextBox.ApplyTimeFormat = false;
            this.FullStreetNameTextBox.BackColor = System.Drawing.Color.White;
            this.FullStreetNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FullStreetNameTextBox.CFromatWihoutSymbol = false;
            this.FullStreetNameTextBox.CheckForEmpty = false;
            this.FullStreetNameTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FullStreetNameTextBox.Digits = -1;
            this.FullStreetNameTextBox.EmptyDecimalValue = false;
            this.FullStreetNameTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.FullStreetNameTextBox.ForeColor = System.Drawing.Color.Black;
            this.FullStreetNameTextBox.IsEditable = false;
            this.FullStreetNameTextBox.IsQueryableFileld = false;
            this.FullStreetNameTextBox.Location = new System.Drawing.Point(17, 16);
            this.FullStreetNameTextBox.LockKeyPress = false;
            this.FullStreetNameTextBox.MaxLength = 50;
            this.FullStreetNameTextBox.Name = "FullStreetNameTextBox";
            this.FullStreetNameTextBox.PersistDefaultColor = false;
            this.FullStreetNameTextBox.Precision = 2;
            this.FullStreetNameTextBox.QueryingFileldName = "";
            this.FullStreetNameTextBox.SetColorFlag = false;
            this.FullStreetNameTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FullStreetNameTextBox.Size = new System.Drawing.Size(467, 16);
            this.FullStreetNameTextBox.SpecialCharacter = "%";
            this.FullStreetNameTextBox.TabIndex = 2;
            this.FullStreetNameTextBox.TextCustomFormat = "$#,##0.00";
            this.FullStreetNameTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.FullStreetNameTextBox.WholeInteger = false;
            this.FullStreetNameTextBox.TextChanged += new System.EventHandler(this.FullStreetNameTextBox_TextChanged);
            // 
            // FullStreetNameLabel
            // 
            this.FullStreetNameLabel.AutoSize = true;
            this.FullStreetNameLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FullStreetNameLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FullStreetNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.FullStreetNameLabel.Location = new System.Drawing.Point(1, 1);
            this.FullStreetNameLabel.Name = "FullStreetNameLabel";
            this.FullStreetNameLabel.Size = new System.Drawing.Size(100, 14);
            this.FullStreetNameLabel.TabIndex = 62;
            this.FullStreetNameLabel.Text = "Full Street Name:";
            // 
            // StreetMaintenanceSecPanel
            // 
            this.StreetMaintenanceSecPanel.BackColor = System.Drawing.Color.White;
            this.StreetMaintenanceSecPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StreetMaintenanceSecPanel.Controls.Add(this.HeaderMaintenancePanel);
            this.StreetMaintenanceSecPanel.Location = new System.Drawing.Point(0, 0);
            this.StreetMaintenanceSecPanel.Name = "StreetMaintenanceSecPanel";
            this.StreetMaintenanceSecPanel.Size = new System.Drawing.Size(768, 80);
            this.StreetMaintenanceSecPanel.TabIndex = 2;
            this.StreetMaintenanceSecPanel.TabStop = true;
            // 
            // F25011
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.StreetMaintenanceSecPanel);
            this.Controls.Add(this.StreetListingPictureBox);
            this.MaximumSize = new System.Drawing.Size(804, 81);
            this.MinimumSize = new System.Drawing.Size(804, 81);
            this.Name = "F25011";
            this.Size = new System.Drawing.Size(804, 81);
            this.Tag = "25011";
            this.Load += new System.EventHandler(this.F25011_Load);
            ((System.ComponentModel.ISupportInitialize)(this.StreetListingPictureBox)).EndInit();
            this.HeaderMaintenancePanel.ResumeLayout(false);
            this.SuffixPanel.ResumeLayout(false);
            this.SuffixPanel.PerformLayout();
            this.DirectionalPanel.ResumeLayout(false);
            this.DirectionalPanel.PerformLayout();
            this.CityPanel.ResumeLayout(false);
            this.CityPanel.PerformLayout();
            this.ZipCodePanel.ResumeLayout(false);
            this.ZipCodePanel.PerformLayout();
            this.StreetNamePanel.ResumeLayout(false);
            this.StreetNamePanel.PerformLayout();
            this.FullStreetNamePanel.ResumeLayout(false);
            this.FullStreetNamePanel.PerformLayout();
            this.StreetMaintenanceSecPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox StreetListingPictureBox;
        private System.Windows.Forms.ToolTip StreetListMgmtToolTip;
        private System.Windows.Forms.Panel HeaderMaintenancePanel;
        private System.Windows.Forms.Panel SuffixPanel;
        private TerraScan.UI.Controls.TerraScanComboBox SuffixComboBox;
        private TerraScan.UI.Controls.TerraScanTextBox SuffixComboBoxTextBox;
        private System.Windows.Forms.Label SuffixLabel;
        private System.Windows.Forms.Panel DirectionalPanel;
        private System.Windows.Forms.Label DirectionalLabel;
        private System.Windows.Forms.Panel CityPanel;
        private TerraScan.UI.Controls.TerraScanComboBox CityComboBox;
        private TerraScan.UI.Controls.TerraScanTextBox CityComboxTextBox;
        private System.Windows.Forms.Label CityLabel;
        private System.Windows.Forms.Panel ZipCodePanel;
        private TerraScan.UI.Controls.TerraScanTextBox ZipCodeTextBox;
        private System.Windows.Forms.Label ZipCodeLabel;
        private System.Windows.Forms.Panel StreetNamePanel;
        private TerraScan.UI.Controls.TerraScanTextBox StreetNameTextBox;
        private System.Windows.Forms.Label StreetNameLabel;
        private System.Windows.Forms.Panel FullStreetNamePanel;
        private TerraScan.UI.Controls.TerraScanTextBox FullStreetNameTextBox;
        private System.Windows.Forms.Label FullStreetNameLabel;
        private System.Windows.Forms.Panel StreetMaintenanceSecPanel;
        private TerraScan.UI.Controls.TerraScanComboBox DirectionalComboBox;
    }
}
