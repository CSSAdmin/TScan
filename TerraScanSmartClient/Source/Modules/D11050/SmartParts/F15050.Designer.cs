namespace D11050
{
    partial class F15050
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F15050));
            this.FromTemplatePanel = new System.Windows.Forms.Panel();
            this.FromTemplateComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.FromTemplateLabel = new System.Windows.Forms.Label();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.RemoveTemplateButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ApplyFeesButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SaveTemplateButton = new TerraScan.UI.Controls.TerraScanButton();
            this.IncludeRowsPanel = new System.Windows.Forms.Panel();
            this.IncludeRowsComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.IncludeRowsLabel = new System.Windows.Forms.Label();
            this.AmountPanel = new System.Windows.Forms.Panel();
            this.AmmountTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.AmountLabel = new System.Windows.Forms.Label();
            this.AccountPanel = new System.Windows.Forms.Panel();
            this.AccountTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.AccountSelectionButton = new System.Windows.Forms.Button();
            this.AccountLabel = new System.Windows.Forms.Label();
            this.FeeManagementPictureBox = new System.Windows.Forms.PictureBox();
            this.FeeToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.DescriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.FeeTypePanel = new System.Windows.Forms.Panel();
            this.FeeTypeComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.FeeTypeLabel = new System.Windows.Forms.Label();
            this.FromTemplatePanel.SuspendLayout();
            this.ButtonsPanel.SuspendLayout();
            this.IncludeRowsPanel.SuspendLayout();
            this.AmountPanel.SuspendLayout();
            this.AccountPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FeeManagementPictureBox)).BeginInit();
            this.DescriptionPanel.SuspendLayout();
            this.FeeTypePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FromTemplatePanel
            // 
            this.FromTemplatePanel.BackColor = System.Drawing.Color.Transparent;
            this.FromTemplatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FromTemplatePanel.Controls.Add(this.FromTemplateComboBox);
            this.FromTemplatePanel.Controls.Add(this.FromTemplateLabel);
            this.FromTemplatePanel.Location = new System.Drawing.Point(0, 0);
            this.FromTemplatePanel.Name = "FromTemplatePanel";
            this.FromTemplatePanel.Size = new System.Drawing.Size(300, 42);
            this.FromTemplatePanel.TabIndex = 0;
            this.FromTemplatePanel.TabStop = true;
            // 
            // FromTemplateComboBox
            // 
            this.FromTemplateComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.FromTemplateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FromTemplateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FromTemplateComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.FromTemplateComboBox.FormattingEnabled = true;
            this.FromTemplateComboBox.Items.AddRange(new object[] {
            "NSF Check Fee"});
            this.FromTemplateComboBox.Location = new System.Drawing.Point(13, 14);
            this.FromTemplateComboBox.MaxLength = 18;
            this.FromTemplateComboBox.Name = "FromTemplateComboBox";
            this.FromTemplateComboBox.Size = new System.Drawing.Size(274, 24);
            this.FromTemplateComboBox.TabIndex = 2;
            this.FromTemplateComboBox.SelectionChangeCommitted += new System.EventHandler(this.FromTemplateComboBox_SelectionChangeCommitted);
            this.FromTemplateComboBox.SelectedValueChanged += new System.EventHandler(this.FromTemplateComboBox_SelectionChangeCommitted);
            // 
            // FromTemplateLabel
            // 
            this.FromTemplateLabel.AutoSize = true;
            this.FromTemplateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromTemplateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.FromTemplateLabel.Location = new System.Drawing.Point(1, -1);
            this.FromTemplateLabel.Name = "FromTemplateLabel";
            this.FromTemplateLabel.Size = new System.Drawing.Size(94, 14);
            this.FromTemplateLabel.TabIndex = 1;
            this.FromTemplateLabel.Tag = "";
            this.FromTemplateLabel.Text = "From Template:";
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.BackColor = System.Drawing.Color.Silver;
            this.ButtonsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ButtonsPanel.Controls.Add(this.RemoveTemplateButton);
            this.ButtonsPanel.Controls.Add(this.ApplyFeesButton);
            this.ButtonsPanel.Controls.Add(this.SaveTemplateButton);
            this.ButtonsPanel.Location = new System.Drawing.Point(0, 82);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(751, 42);
            this.ButtonsPanel.TabIndex = 22;
            // 
            // RemoveTemplateButton
            // 
            this.RemoveTemplateButton.ActualPermission = false;
            this.RemoveTemplateButton.ApplyDisableBehaviour = false;
            this.RemoveTemplateButton.AutoEllipsis = true;
            this.RemoveTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.RemoveTemplateButton.BorderColor = System.Drawing.Color.Wheat;
            this.RemoveTemplateButton.CausesValidation = false;
            this.RemoveTemplateButton.CommentPriority = false;
            this.RemoveTemplateButton.EnableAutoPrint = false;
            this.RemoveTemplateButton.FilterStatus = false;
            this.RemoveTemplateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.RemoveTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveTemplateButton.FocusRectangleEnabled = true;
            this.RemoveTemplateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveTemplateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.RemoveTemplateButton.ImageSelected = false;
            this.RemoveTemplateButton.Location = new System.Drawing.Point(294, 6);
            this.RemoveTemplateButton.Name = "RemoveTemplateButton";
            this.RemoveTemplateButton.NewPadding = 5;
            this.RemoveTemplateButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.RemoveTemplateButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.RemoveTemplateButton.Size = new System.Drawing.Size(160, 28);
            this.RemoveTemplateButton.StatusIndicator = false;
            this.RemoveTemplateButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RemoveTemplateButton.StatusOffText = null;
            this.RemoveTemplateButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.RemoveTemplateButton.StatusOnText = null;
            this.RemoveTemplateButton.TabIndex = 20;
            this.RemoveTemplateButton.TabStop = false;
            this.RemoveTemplateButton.Text = "Remove Template";
            this.RemoveTemplateButton.UseVisualStyleBackColor = false;
            this.RemoveTemplateButton.Click += new System.EventHandler(this.RemoveTemplateButton_Click);
            // 
            // ApplyFeesButton
            // 
            this.ApplyFeesButton.ActualPermission = false;
            this.ApplyFeesButton.ApplyDisableBehaviour = false;
            this.ApplyFeesButton.AutoEllipsis = true;
            this.ApplyFeesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ApplyFeesButton.BorderColor = System.Drawing.Color.Wheat;
            this.ApplyFeesButton.CausesValidation = false;
            this.ApplyFeesButton.CommentPriority = false;
            this.ApplyFeesButton.EnableAutoPrint = false;
            this.ApplyFeesButton.FilterStatus = false;
            this.ApplyFeesButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ApplyFeesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApplyFeesButton.FocusRectangleEnabled = true;
            this.ApplyFeesButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyFeesButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ApplyFeesButton.ImageSelected = false;
            this.ApplyFeesButton.Location = new System.Drawing.Point(494, 5);
            this.ApplyFeesButton.Name = "ApplyFeesButton";
            this.ApplyFeesButton.NewPadding = 5;
            this.ApplyFeesButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.ApplyFeesButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ApplyFeesButton.Size = new System.Drawing.Size(160, 28);
            this.ApplyFeesButton.StatusIndicator = false;
            this.ApplyFeesButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ApplyFeesButton.StatusOffText = null;
            this.ApplyFeesButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ApplyFeesButton.StatusOnText = null;
            this.ApplyFeesButton.TabIndex = 21;
            this.ApplyFeesButton.TabStop = false;
            this.ApplyFeesButton.Text = "Apply Fees";
            this.ApplyFeesButton.UseVisualStyleBackColor = false;
            this.ApplyFeesButton.Click += new System.EventHandler(this.ApplyFeesButton_Click);
            // 
            // SaveTemplateButton
            // 
            this.SaveTemplateButton.ActualPermission = false;
            this.SaveTemplateButton.ApplyDisableBehaviour = false;
            this.SaveTemplateButton.AutoEllipsis = true;
            this.SaveTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SaveTemplateButton.BorderColor = System.Drawing.Color.Wheat;
            this.SaveTemplateButton.CausesValidation = false;
            this.SaveTemplateButton.CommentPriority = false;
            this.SaveTemplateButton.EnableAutoPrint = false;
            this.SaveTemplateButton.FilterStatus = false;
            this.SaveTemplateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveTemplateButton.FocusRectangleEnabled = true;
            this.SaveTemplateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveTemplateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SaveTemplateButton.ImageSelected = false;
            this.SaveTemplateButton.Location = new System.Drawing.Point(94, 5);
            this.SaveTemplateButton.Name = "SaveTemplateButton";
            this.SaveTemplateButton.NewPadding = 5;
            this.SaveTemplateButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.SaveTemplateButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SaveTemplateButton.Size = new System.Drawing.Size(160, 28);
            this.SaveTemplateButton.StatusIndicator = false;
            this.SaveTemplateButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SaveTemplateButton.StatusOffText = null;
            this.SaveTemplateButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SaveTemplateButton.StatusOnText = null;
            this.SaveTemplateButton.TabIndex = 19;
            this.SaveTemplateButton.TabStop = false;
            this.SaveTemplateButton.Text = "Save Template";
            this.SaveTemplateButton.UseVisualStyleBackColor = false;
            this.SaveTemplateButton.Click += new System.EventHandler(this.SaveTemplateButton_Click);
            // 
            // IncludeRowsPanel
            // 
            this.IncludeRowsPanel.BackColor = System.Drawing.Color.Transparent;
            this.IncludeRowsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IncludeRowsPanel.Controls.Add(this.IncludeRowsComboBox);
            this.IncludeRowsPanel.Controls.Add(this.IncludeRowsLabel);
            this.IncludeRowsPanel.Location = new System.Drawing.Point(524, 0);
            this.IncludeRowsPanel.Name = "IncludeRowsPanel";
            this.IncludeRowsPanel.Size = new System.Drawing.Size(227, 42);
            this.IncludeRowsPanel.TabIndex = 6;
            this.IncludeRowsPanel.TabStop = true;
            // 
            // IncludeRowsComboBox
            // 
            this.IncludeRowsComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.IncludeRowsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IncludeRowsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.IncludeRowsComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.IncludeRowsComboBox.FormattingEnabled = true;
            this.IncludeRowsComboBox.Items.AddRange(new object[] {
            "Only Current Row"});
            this.IncludeRowsComboBox.Location = new System.Drawing.Point(13, 14);
            this.IncludeRowsComboBox.MaxLength = 18;
            this.IncludeRowsComboBox.Name = "IncludeRowsComboBox";
            this.IncludeRowsComboBox.Size = new System.Drawing.Size(199, 24);
            this.IncludeRowsComboBox.TabIndex = 8;
            // 
            // IncludeRowsLabel
            // 
            this.IncludeRowsLabel.AutoSize = true;
            this.IncludeRowsLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IncludeRowsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.IncludeRowsLabel.Location = new System.Drawing.Point(1, -1);
            this.IncludeRowsLabel.Name = "IncludeRowsLabel";
            this.IncludeRowsLabel.Size = new System.Drawing.Size(84, 14);
            this.IncludeRowsLabel.TabIndex = 7;
            this.IncludeRowsLabel.Tag = "";
            this.IncludeRowsLabel.Text = "Include Rows:";
            // 
            // AmountPanel
            // 
            this.AmountPanel.BackColor = System.Drawing.Color.Transparent;
            this.AmountPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AmountPanel.Controls.Add(this.AmmountTextBox);
            this.AmountPanel.Controls.Add(this.AmountLabel);
            this.AmountPanel.Location = new System.Drawing.Point(299, 41);
            this.AmountPanel.Name = "AmountPanel";
            this.AmountPanel.Size = new System.Drawing.Size(135, 42);
            this.AmountPanel.TabIndex = 12;
            this.AmountPanel.TabStop = true;
            // 
            // AmmountTextBox
            // 
            this.AmmountTextBox.AllowClick = true;
            this.AmmountTextBox.AllowNegativeSign = false;
            this.AmmountTextBox.ApplyCFGFormat = true;
            this.AmmountTextBox.ApplyCurrencyFormat = true;
            this.AmmountTextBox.ApplyFocusColor = true;
            this.AmmountTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.AmmountTextBox.ApplyNegativeStandard = true;
            this.AmmountTextBox.ApplyParentFocusColor = true;
            this.AmmountTextBox.ApplyTimeFormat = false;
            this.AmmountTextBox.BackColor = System.Drawing.Color.White;
            this.AmmountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AmmountTextBox.CFromatWihoutSymbol = false;
            this.AmmountTextBox.CheckForEmpty = true;
            this.AmmountTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AmmountTextBox.Digits = 15;
            this.AmmountTextBox.EmptyDecimalValue = false;
            this.AmmountTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.AmmountTextBox.ForeColor = System.Drawing.Color.Black;
            this.AmmountTextBox.IsEditable = false;
            this.AmmountTextBox.IsQueryableFileld = true;
            this.AmmountTextBox.Location = new System.Drawing.Point(11, 18);
            this.AmmountTextBox.LockKeyPress = false;
            this.AmmountTextBox.MaxLength = 0;
            this.AmmountTextBox.Name = "AmmountTextBox";
            this.AmmountTextBox.PersistDefaultColor = false;
            this.AmmountTextBox.Precision = 2;
            this.AmmountTextBox.QueryingFileldName = "";
            this.AmmountTextBox.SetColorFlag = false;
            this.AmmountTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.AmmountTextBox.Size = new System.Drawing.Size(116, 16);
            this.AmmountTextBox.SpecialCharacter = "%";
            this.AmmountTextBox.TabIndex = 14;
            this.AmmountTextBox.Tag = "Irrigated Base";
            this.AmmountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AmmountTextBox.TextCustomFormat = "$ #,##0.00";
            this.AmmountTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.AmmountTextBox.WholeInteger = false;
            this.AmmountTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.AmmountTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.AmmountTextBox_Validating);
            // 
            // AmountLabel
            // 
            this.AmountLabel.AutoSize = true;
            this.AmountLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.AmountLabel.Location = new System.Drawing.Point(1, -1);
            this.AmountLabel.Name = "AmountLabel";
            this.AmountLabel.Size = new System.Drawing.Size(54, 14);
            this.AmountLabel.TabIndex = 13;
            this.AmountLabel.Tag = "";
            this.AmountLabel.Text = "Amount:";
            // 
            // AccountPanel
            // 
            this.AccountPanel.BackColor = System.Drawing.Color.Transparent;
            this.AccountPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AccountPanel.Controls.Add(this.AccountTextBox);
            this.AccountPanel.Controls.Add(this.AccountSelectionButton);
            this.AccountPanel.Controls.Add(this.AccountLabel);
            this.AccountPanel.Location = new System.Drawing.Point(433, 41);
            this.AccountPanel.Name = "AccountPanel";
            this.AccountPanel.Size = new System.Drawing.Size(318, 42);
            this.AccountPanel.TabIndex = 15;
            this.AccountPanel.TabStop = true;
            // 
            // AccountTextBox
            // 
            this.AccountTextBox.AllowClick = true;
            this.AccountTextBox.AllowNegativeSign = false;
            this.AccountTextBox.ApplyCFGFormat = true;
            this.AccountTextBox.ApplyCurrencyFormat = true;
            this.AccountTextBox.ApplyFocusColor = true;
            this.AccountTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.AccountTextBox.ApplyNegativeStandard = true;
            this.AccountTextBox.ApplyParentFocusColor = true;
            this.AccountTextBox.ApplyTimeFormat = false;
            this.AccountTextBox.BackColor = System.Drawing.Color.White;
            this.AccountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AccountTextBox.CFromatWihoutSymbol = false;
            this.AccountTextBox.CheckForEmpty = true;
            this.AccountTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AccountTextBox.Digits = -1;
            this.AccountTextBox.EmptyDecimalValue = false;
            this.AccountTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.AccountTextBox.ForeColor = System.Drawing.Color.Black;
            this.AccountTextBox.IsEditable = false;
            this.AccountTextBox.IsQueryableFileld = true;
            this.AccountTextBox.Location = new System.Drawing.Point(11, 18);
            this.AccountTextBox.LockKeyPress = true;
            this.AccountTextBox.MaxLength = 500;
            this.AccountTextBox.Name = "AccountTextBox";
            this.AccountTextBox.PersistDefaultColor = false;
            this.AccountTextBox.Precision = 2;
            this.AccountTextBox.QueryingFileldName = "";
            this.AccountTextBox.ReadOnly = true;
            this.AccountTextBox.SetColorFlag = false;
            this.AccountTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.AccountTextBox.Size = new System.Drawing.Size(271, 16);
            this.AccountTextBox.SpecialCharacter = "%";
            this.AccountTextBox.TabIndex = 17;
            this.AccountTextBox.TabStop = false;
            this.AccountTextBox.Tag = "";
            this.AccountTextBox.TextCustomFormat = "$ #,##0.00";
            this.AccountTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.AccountTextBox.WholeInteger = false;
            // 
            // AccountSelectionButton
            // 
            this.AccountSelectionButton.FlatAppearance.BorderSize = 0;
            this.AccountSelectionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AccountSelectionButton.Image = ((System.Drawing.Image)(resources.GetObject("AccountSelectionButton.Image")));
            this.AccountSelectionButton.Location = new System.Drawing.Point(288, 18);
            this.AccountSelectionButton.Name = "AccountSelectionButton";
            this.AccountSelectionButton.Size = new System.Drawing.Size(17, 16);
            this.AccountSelectionButton.TabIndex = 18;
            this.AccountSelectionButton.Tag = "AccountAdminFeeTextBox";
            this.AccountSelectionButton.UseVisualStyleBackColor = true;
            this.AccountSelectionButton.Click += new System.EventHandler(this.AccountSelectionButton_Click);
            // 
            // AccountLabel
            // 
            this.AccountLabel.AutoSize = true;
            this.AccountLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.AccountLabel.Location = new System.Drawing.Point(1, -1);
            this.AccountLabel.Name = "AccountLabel";
            this.AccountLabel.Size = new System.Drawing.Size(55, 14);
            this.AccountLabel.TabIndex = 16;
            this.AccountLabel.Tag = "";
            this.AccountLabel.Text = "Account:";
            // 
            // FeeManagementPictureBox
            // 
            this.FeeManagementPictureBox.Location = new System.Drawing.Point(742, 0);
            this.FeeManagementPictureBox.Name = "FeeManagementPictureBox";
            this.FeeManagementPictureBox.Size = new System.Drawing.Size(42, 124);
            this.FeeManagementPictureBox.TabIndex = 12;
            this.FeeManagementPictureBox.TabStop = false;
            this.FeeManagementPictureBox.Click += new System.EventHandler(this.FeeManagementPictureBox_Click);
            this.FeeManagementPictureBox.MouseHover += new System.EventHandler(this.FeeManagementPictureBox_MouseHover);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DescriptionLabel.Location = new System.Drawing.Point(1, -1);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(73, 14);
            this.DescriptionLabel.TabIndex = 10;
            this.DescriptionLabel.Tag = "";
            this.DescriptionLabel.Text = "Description:";
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.BackColor = System.Drawing.Color.Transparent;
            this.DescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionPanel.Controls.Add(this.DescriptionTextBox);
            this.DescriptionPanel.Controls.Add(this.DescriptionLabel);
            this.DescriptionPanel.Location = new System.Drawing.Point(0, 41);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(304, 42);
            this.DescriptionPanel.TabIndex = 9;
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
            this.DescriptionTextBox.BackColor = System.Drawing.SystemColors.Window;
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
            this.DescriptionTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionTextBox.ForeColor = System.Drawing.Color.Black;
            this.DescriptionTextBox.IsEditable = false;
            this.DescriptionTextBox.IsQueryableFileld = false;
            this.DescriptionTextBox.Location = new System.Drawing.Point(11, 18);
            this.DescriptionTextBox.LockKeyPress = false;
            this.DescriptionTextBox.MaxLength = 80;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.PersistDefaultColor = false;
            this.DescriptionTextBox.Precision = 2;
            this.DescriptionTextBox.QueryingFileldName = "";
            this.DescriptionTextBox.SetColorFlag = false;
            this.DescriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescriptionTextBox.Size = new System.Drawing.Size(273, 16);
            this.DescriptionTextBox.SpecialCharacter = "$";
            this.DescriptionTextBox.TabIndex = 11;
            this.DescriptionTextBox.TextCustomFormat = "$#,##0.00";
            this.DescriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescriptionTextBox.WholeInteger = false;
            this.DescriptionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // FeeTypePanel
            // 
            this.FeeTypePanel.BackColor = System.Drawing.Color.Transparent;
            this.FeeTypePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FeeTypePanel.Controls.Add(this.FeeTypeComboBox);
            this.FeeTypePanel.Controls.Add(this.FeeTypeLabel);
            this.FeeTypePanel.Location = new System.Drawing.Point(299, 0);
            this.FeeTypePanel.Name = "FeeTypePanel";
            this.FeeTypePanel.Size = new System.Drawing.Size(227, 42);
            this.FeeTypePanel.TabIndex = 3;
            this.FeeTypePanel.TabStop = true;
            // 
            // FeeTypeComboBox
            // 
            this.FeeTypeComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.FeeTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FeeTypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FeeTypeComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.FeeTypeComboBox.FormattingEnabled = true;
            this.FeeTypeComboBox.Location = new System.Drawing.Point(13, 14);
            this.FeeTypeComboBox.MaxLength = 18;
            this.FeeTypeComboBox.Name = "FeeTypeComboBox";
            this.FeeTypeComboBox.Size = new System.Drawing.Size(201, 24);
            this.FeeTypeComboBox.TabIndex = 5;
            this.FeeTypeComboBox.SelectionChangeCommitted += new System.EventHandler(this.FeeTypeComboBox_SelectionChangeCommitted);
            this.FeeTypeComboBox.SelectedValueChanged += new System.EventHandler(this.FeeTypeComboBox_SelectionChangeCommitted);
            // 
            // FeeTypeLabel
            // 
            this.FeeTypeLabel.AutoSize = true;
            this.FeeTypeLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FeeTypeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.FeeTypeLabel.Location = new System.Drawing.Point(1, -1);
            this.FeeTypeLabel.Name = "FeeTypeLabel";
            this.FeeTypeLabel.Size = new System.Drawing.Size(60, 14);
            this.FeeTypeLabel.TabIndex = 4;
            this.FeeTypeLabel.Tag = "";
            this.FeeTypeLabel.Text = "Fee Type:";
            // 
            // F15050
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.FeeTypePanel);
            this.Controls.Add(this.AccountPanel);
            this.Controls.Add(this.AmountPanel);
            this.Controls.Add(this.DescriptionPanel);
            this.Controls.Add(this.IncludeRowsPanel);
            this.Controls.Add(this.ButtonsPanel);
            this.Controls.Add(this.FromTemplatePanel);
            this.Controls.Add(this.FeeManagementPictureBox);
            this.Name = "F15050";
            this.Size = new System.Drawing.Size(804, 124);
            this.Tag = "15050";
            this.Load += new System.EventHandler(this.F15050_Load);
            this.FromTemplatePanel.ResumeLayout(false);
            this.FromTemplatePanel.PerformLayout();
            this.ButtonsPanel.ResumeLayout(false);
            this.IncludeRowsPanel.ResumeLayout(false);
            this.IncludeRowsPanel.PerformLayout();
            this.AmountPanel.ResumeLayout(false);
            this.AmountPanel.PerformLayout();
            this.AccountPanel.ResumeLayout(false);
            this.AccountPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FeeManagementPictureBox)).EndInit();
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.FeeTypePanel.ResumeLayout(false);
            this.FeeTypePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel FromTemplatePanel;
        private System.Windows.Forms.Label FromTemplateLabel;
        private TerraScan.UI.Controls.TerraScanComboBox FromTemplateComboBox;
        private System.Windows.Forms.Panel ButtonsPanel;
        private System.Windows.Forms.Panel IncludeRowsPanel;
        private System.Windows.Forms.Label IncludeRowsLabel;
        private TerraScan.UI.Controls.TerraScanComboBox IncludeRowsComboBox;
        private TerraScan.UI.Controls.TerraScanButton ApplyFeesButton;
        private TerraScan.UI.Controls.TerraScanButton SaveTemplateButton;
        private System.Windows.Forms.Panel AmountPanel;
        private System.Windows.Forms.Panel AccountPanel;
        private System.Windows.Forms.Label AmountLabel;
        private System.Windows.Forms.Label AccountLabel;
        private System.Windows.Forms.PictureBox FeeManagementPictureBox;
        private System.Windows.Forms.ToolTip FeeToolTip;
        private System.Windows.Forms.Button AccountSelectionButton;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Panel DescriptionPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DescriptionTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox AmmountTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox AccountTextBox;
        private System.Windows.Forms.Panel FeeTypePanel;
        private TerraScan.UI.Controls.TerraScanComboBox FeeTypeComboBox;
        private System.Windows.Forms.Label FeeTypeLabel;
        private TerraScan.UI.Controls.TerraScanButton RemoveTemplateButton;
    }
}
