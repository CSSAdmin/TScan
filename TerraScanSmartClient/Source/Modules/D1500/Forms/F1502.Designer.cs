namespace D1500
{
    partial class F1502
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1502));
            this.AccountElementMgmtPanel = new System.Windows.Forms.Panel();
            this.SearchButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ClearButton = new TerraScan.UI.Controls.TerraScanButton();
            this.LinePanel = new System.Windows.Forms.Panel();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.FunctionPanel = new System.Windows.Forms.Panel();
            this.FunctionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.FunctionLabel = new System.Windows.Forms.Label();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.DescTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.TypePanel = new System.Windows.Forms.Panel();
            this.TypeComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.RecordCountLabel = new System.Windows.Forms.Label();
            this.AccountMgmtCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AccountMgmtCloseButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AccountMgmtSaveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AccountMgmtAcceptButton = new TerraScan.UI.Controls.TerraScanButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AccountMgmtVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.AccountMgmtDataGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.FunctionValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SemiAnnualCode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FunctionKeyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionsMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AccountElementMgmtPanel.SuspendLayout();
            this.FunctionPanel.SuspendLayout();
            this.DescriptionPanel.SuspendLayout();
            this.TypePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AccountMgmtDataGridView)).BeginInit();
            this.FunctionsMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccountElementMgmtPanel
            // 
            this.AccountElementMgmtPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AccountElementMgmtPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AccountElementMgmtPanel.Controls.Add(this.SearchButton);
            this.AccountElementMgmtPanel.Controls.Add(this.ClearButton);
            this.AccountElementMgmtPanel.Controls.Add(this.LinePanel);
            this.AccountElementMgmtPanel.Controls.Add(this.SearchLabel);
            this.AccountElementMgmtPanel.Location = new System.Drawing.Point(7, 7);
            this.AccountElementMgmtPanel.Name = "AccountElementMgmtPanel";
            this.AccountElementMgmtPanel.Size = new System.Drawing.Size(537, 44);
            this.AccountElementMgmtPanel.TabIndex = 18;
            // 
            // SearchButton
            // 
            this.SearchButton.ActualPermission = false;
            this.SearchButton.ApplyDisableBehaviour = false;
            this.SearchButton.AutoEllipsis = true;
            this.SearchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SearchButton.BorderColor = System.Drawing.Color.Wheat;
            this.SearchButton.CausesValidation = false;
            this.SearchButton.CommentPriority = false;
            this.SearchButton.EnableAutoPrint = false;
            this.SearchButton.FilterStatus = false;
            this.SearchButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchButton.FocusRectangleEnabled = true;
            this.SearchButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SearchButton.ImageSelected = false;
            this.SearchButton.Location = new System.Drawing.Point(430, 7);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.NewPadding = 5;
            this.SearchButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.SearchButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SearchButton.Size = new System.Drawing.Size(98, 28);
            this.SearchButton.StatusIndicator = false;
            this.SearchButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SearchButton.StatusOffText = null;
            this.SearchButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SearchButton.StatusOnText = null;
            this.SearchButton.TabIndex = 2;
            this.SearchButton.TabStop = false;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.ActualPermission = false;
            this.ClearButton.ApplyDisableBehaviour = false;
            this.ClearButton.AutoEllipsis = true;
            this.ClearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ClearButton.BorderColor = System.Drawing.Color.Wheat;
            this.ClearButton.CausesValidation = false;
            this.ClearButton.CommentPriority = false;
            this.ClearButton.EnableAutoPrint = false;
            this.ClearButton.FilterStatus = false;
            this.ClearButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearButton.FocusRectangleEnabled = true;
            this.ClearButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClearButton.ImageSelected = false;
            this.ClearButton.Location = new System.Drawing.Point(327, 7);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.NewPadding = 5;
            this.ClearButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.ClearButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ClearButton.Size = new System.Drawing.Size(98, 28);
            this.ClearButton.StatusIndicator = false;
            this.ClearButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClearButton.StatusOffText = null;
            this.ClearButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ClearButton.StatusOnText = null;
            this.ClearButton.TabIndex = 1;
            this.ClearButton.TabStop = false;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // LinePanel
            // 
            this.LinePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LinePanel.BackColor = System.Drawing.Color.Silver;
            this.LinePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LinePanel.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.LinePanel.Location = new System.Drawing.Point(103, 20);
            this.LinePanel.Name = "LinePanel";
            this.LinePanel.Size = new System.Drawing.Size(217, 2);
            this.LinePanel.TabIndex = 20;
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(80)))), ((int)(((byte)(129)))));
            this.SearchLabel.Location = new System.Drawing.Point(16, 9);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(81, 24);
            this.SearchLabel.TabIndex = 0;
            this.SearchLabel.Text = "Search";
            // 
            // FunctionPanel
            // 
            this.FunctionPanel.BackColor = System.Drawing.Color.White;
            this.FunctionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FunctionPanel.Controls.Add(this.FunctionTextBox);
            this.FunctionPanel.Controls.Add(this.FunctionLabel);
            this.FunctionPanel.Location = new System.Drawing.Point(7, 50);
            this.FunctionPanel.Name = "FunctionPanel";
            this.FunctionPanel.Size = new System.Drawing.Size(88, 37);
            this.FunctionPanel.TabIndex = 3;
            this.FunctionPanel.TabStop = true;
            // 
            // FunctionTextBox
            // 
            this.FunctionTextBox.AllowClick = true;
            this.FunctionTextBox.AllowNegativeSign = false;
            this.FunctionTextBox.ApplyCFGFormat = false;
            this.FunctionTextBox.ApplyCurrencyFormat = false;
            this.FunctionTextBox.ApplyFocusColor = true;
            this.FunctionTextBox.ApplyNegativeStandard = true;
            this.FunctionTextBox.ApplyParentFocusColor = true;
            this.FunctionTextBox.ApplyTimeFormat = false;
            this.FunctionTextBox.BackColor = System.Drawing.Color.White;
            this.FunctionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FunctionTextBox.CFromatWihoutSymbol = false;
            this.FunctionTextBox.CheckForEmpty = false;
            this.FunctionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextBox.Digits = -1;
            this.FunctionTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.FunctionTextBox.ForeColor = System.Drawing.Color.Black;
            this.FunctionTextBox.IsEditable = false;
            this.FunctionTextBox.IsQueryableFileld = false;
            this.FunctionTextBox.Location = new System.Drawing.Point(4, 16);
            this.FunctionTextBox.LockKeyPress = false;
            this.FunctionTextBox.MaxLength = 50;
            this.FunctionTextBox.Name = "FunctionTextBox";
            this.FunctionTextBox.PersistDefaultColor = false;
            this.FunctionTextBox.Precision = 2;
            this.FunctionTextBox.QueryingFileldName = "";
            this.FunctionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FunctionTextBox.Size = new System.Drawing.Size(78, 16);
            this.FunctionTextBox.SpecialCharacter = "%";
            this.FunctionTextBox.TabIndex = 4;
            this.FunctionTextBox.TextCustomFormat = "$#,##0.00";
            this.FunctionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.FunctionTextBox.WholeInteger = false;
            this.FunctionTextBox.TextChanged += new System.EventHandler(this.Editext);
            // 
            // FunctionLabel
            // 
            this.FunctionLabel.AutoSize = true;
            this.FunctionLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FunctionLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FunctionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.FunctionLabel.Location = new System.Drawing.Point(1, 1);
            this.FunctionLabel.Name = "FunctionLabel";
            this.FunctionLabel.Size = new System.Drawing.Size(57, 14);
            this.FunctionLabel.TabIndex = 62;
            this.FunctionLabel.Text = "Function:";
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.BackColor = System.Drawing.Color.White;
            this.DescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionPanel.Controls.Add(this.DescTextBox);
            this.DescriptionPanel.Controls.Add(this.DescriptionLabel);
            this.DescriptionPanel.Location = new System.Drawing.Point(94, 50);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(283, 37);
            this.DescriptionPanel.TabIndex = 5;
            this.DescriptionPanel.TabStop = true;
            // 
            // DescTextBox
            // 
            this.DescTextBox.AllowClick = true;
            this.DescTextBox.AllowNegativeSign = false;
            this.DescTextBox.ApplyCFGFormat = false;
            this.DescTextBox.ApplyCurrencyFormat = false;
            this.DescTextBox.ApplyFocusColor = true;
            this.DescTextBox.ApplyNegativeStandard = true;
            this.DescTextBox.ApplyParentFocusColor = true;
            this.DescTextBox.ApplyTimeFormat = false;
            this.DescTextBox.BackColor = System.Drawing.Color.White;
            this.DescTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DescTextBox.CFromatWihoutSymbol = false;
            this.DescTextBox.CheckForEmpty = false;
            this.DescTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DescTextBox.Digits = -1;
            this.DescTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.DescTextBox.ForeColor = System.Drawing.Color.Black;
            this.DescTextBox.IsEditable = false;
            this.DescTextBox.IsQueryableFileld = false;
            this.DescTextBox.Location = new System.Drawing.Point(14, 16);
            this.DescTextBox.LockKeyPress = false;
            this.DescTextBox.MaxLength = 50;
            this.DescTextBox.Name = "DescTextBox";
            this.DescTextBox.PersistDefaultColor = false;
            this.DescTextBox.Precision = 2;
            this.DescTextBox.QueryingFileldName = "";
            this.DescTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescTextBox.Size = new System.Drawing.Size(262, 16);
            this.DescTextBox.SpecialCharacter = "%";
            this.DescTextBox.TabIndex = 6;
            this.DescTextBox.TextCustomFormat = "$#,##0.00";
            this.DescTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescTextBox.WholeInteger = false;
            this.DescTextBox.TextChanged += new System.EventHandler(this.Editext);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DescriptionLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DescriptionLabel.Location = new System.Drawing.Point(1, 1);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(73, 14);
            this.DescriptionLabel.TabIndex = 62;
            this.DescriptionLabel.Text = "Description:";
            // 
            // TypePanel
            // 
            this.TypePanel.BackColor = System.Drawing.Color.White;
            this.TypePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TypePanel.Controls.Add(this.TypeComboBox);
            this.TypePanel.Controls.Add(this.TypeLabel);
            this.TypePanel.Location = new System.Drawing.Point(376, 50);
            this.TypePanel.Name = "TypePanel";
            this.TypePanel.Size = new System.Drawing.Size(168, 37);
            this.TypePanel.TabIndex = 7;
            this.TypePanel.TabStop = true;
            // 
            // TypeComboBox
            // 
            this.TypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TypeComboBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.TypeComboBox.FormattingEnabled = true;
            this.TypeComboBox.Location = new System.Drawing.Point(36, 9);
            this.TypeComboBox.Name = "TypeComboBox";
            this.TypeComboBox.Size = new System.Drawing.Size(125, 24);
            this.TypeComboBox.TabIndex = 8;
            this.TypeComboBox.SelectedValueChanged += new System.EventHandler(this.TypeComboBox_SelectedValueChanged);
            // 
            // TypeLabel
            // 
            this.TypeLabel.AutoSize = true;
            this.TypeLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TypeLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TypeLabel.Location = new System.Drawing.Point(1, 1);
            this.TypeLabel.Name = "TypeLabel";
            this.TypeLabel.Size = new System.Drawing.Size(37, 14);
            this.TypeLabel.TabIndex = 62;
            this.TypeLabel.Text = "Type:";
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(7, 366);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(537, 2);
            this.FormLinePanel.TabIndex = 148;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(9, 371);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 153;
            this.label1.Text = "1502";
            // 
            // RecordCountLabel
            // 
            this.RecordCountLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordCountLabel.Location = new System.Drawing.Point(346, 371);
            this.RecordCountLabel.Name = "RecordCountLabel";
            this.RecordCountLabel.Size = new System.Drawing.Size(197, 15);
            this.RecordCountLabel.TabIndex = 154;
            this.RecordCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AccountMgmtCancelButton
            // 
            this.AccountMgmtCancelButton.ActualPermission = false;
            this.AccountMgmtCancelButton.ApplyDisableBehaviour = false;
            this.AccountMgmtCancelButton.AutoSize = true;
            this.AccountMgmtCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AccountMgmtCancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.AccountMgmtCancelButton.CommentPriority = false;
            this.AccountMgmtCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AccountMgmtCancelButton.EnableAutoPrint = false;
            this.AccountMgmtCancelButton.FilterStatus = false;
            this.AccountMgmtCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AccountMgmtCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AccountMgmtCancelButton.FocusRectangleEnabled = true;
            this.AccountMgmtCancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountMgmtCancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AccountMgmtCancelButton.ImageSelected = false;
            this.AccountMgmtCancelButton.Location = new System.Drawing.Point(149, 332);
            this.AccountMgmtCancelButton.Name = "AccountMgmtCancelButton";
            this.AccountMgmtCancelButton.NewPadding = 5;
            this.AccountMgmtCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.AccountMgmtCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AccountMgmtCancelButton.Size = new System.Drawing.Size(110, 30);
            this.AccountMgmtCancelButton.StatusIndicator = false;
            this.AccountMgmtCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AccountMgmtCancelButton.StatusOffText = null;
            this.AccountMgmtCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AccountMgmtCancelButton.StatusOnText = null;
            this.AccountMgmtCancelButton.TabIndex = 11;
            this.AccountMgmtCancelButton.TabStop = false;
            this.AccountMgmtCancelButton.Text = "Cancel";
            this.AccountMgmtCancelButton.UseVisualStyleBackColor = false;
            this.AccountMgmtCancelButton.Click += new System.EventHandler(this.AccountMgmtCancelButton_Click);
            // 
            // AccountMgmtCloseButton
            // 
            this.AccountMgmtCloseButton.ActualPermission = false;
            this.AccountMgmtCloseButton.ApplyDisableBehaviour = false;
            this.AccountMgmtCloseButton.AutoSize = true;
            this.AccountMgmtCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AccountMgmtCloseButton.BorderColor = System.Drawing.Color.Wheat;
            this.AccountMgmtCloseButton.CommentPriority = false;
            this.AccountMgmtCloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AccountMgmtCloseButton.EnableAutoPrint = false;
            this.AccountMgmtCloseButton.FilterStatus = false;
            this.AccountMgmtCloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AccountMgmtCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AccountMgmtCloseButton.FocusRectangleEnabled = true;
            this.AccountMgmtCloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountMgmtCloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AccountMgmtCloseButton.ImageSelected = false;
            this.AccountMgmtCloseButton.Location = new System.Drawing.Point(291, 332);
            this.AccountMgmtCloseButton.Name = "AccountMgmtCloseButton";
            this.AccountMgmtCloseButton.NewPadding = 5;
            this.AccountMgmtCloseButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.AccountMgmtCloseButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AccountMgmtCloseButton.Size = new System.Drawing.Size(110, 30);
            this.AccountMgmtCloseButton.StatusIndicator = false;
            this.AccountMgmtCloseButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AccountMgmtCloseButton.StatusOffText = null;
            this.AccountMgmtCloseButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AccountMgmtCloseButton.StatusOnText = null;
            this.AccountMgmtCloseButton.TabIndex = 12;
            this.AccountMgmtCloseButton.TabStop = false;
            this.AccountMgmtCloseButton.Text = "Close";
            this.AccountMgmtCloseButton.UseVisualStyleBackColor = false;
            this.AccountMgmtCloseButton.Click += new System.EventHandler(this.AccountMgmtCloseButton_Click);
            // 
            // AccountMgmtSaveButton
            // 
            this.AccountMgmtSaveButton.ActualPermission = false;
            this.AccountMgmtSaveButton.ApplyDisableBehaviour = false;
            this.AccountMgmtSaveButton.AutoSize = true;
            this.AccountMgmtSaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AccountMgmtSaveButton.BorderColor = System.Drawing.Color.Wheat;
            this.AccountMgmtSaveButton.CommentPriority = false;
            this.AccountMgmtSaveButton.EnableAutoPrint = false;
            this.AccountMgmtSaveButton.FilterStatus = false;
            this.AccountMgmtSaveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AccountMgmtSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AccountMgmtSaveButton.FocusRectangleEnabled = true;
            this.AccountMgmtSaveButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountMgmtSaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AccountMgmtSaveButton.ImageSelected = false;
            this.AccountMgmtSaveButton.Location = new System.Drawing.Point(7, 332);
            this.AccountMgmtSaveButton.Name = "AccountMgmtSaveButton";
            this.AccountMgmtSaveButton.NewPadding = 5;
            this.AccountMgmtSaveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.AccountMgmtSaveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AccountMgmtSaveButton.Size = new System.Drawing.Size(110, 30);
            this.AccountMgmtSaveButton.StatusIndicator = false;
            this.AccountMgmtSaveButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AccountMgmtSaveButton.StatusOffText = null;
            this.AccountMgmtSaveButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AccountMgmtSaveButton.StatusOnText = null;
            this.AccountMgmtSaveButton.TabIndex = 10;
            this.AccountMgmtSaveButton.TabStop = false;
            this.AccountMgmtSaveButton.Text = "Save";
            this.AccountMgmtSaveButton.UseVisualStyleBackColor = false;
            this.AccountMgmtSaveButton.Click += new System.EventHandler(this.AccountMgmtSaveButton_Click);
            // 
            // AccountMgmtAcceptButton
            // 
            this.AccountMgmtAcceptButton.ActualPermission = false;
            this.AccountMgmtAcceptButton.ApplyDisableBehaviour = false;
            this.AccountMgmtAcceptButton.AutoSize = true;
            this.AccountMgmtAcceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AccountMgmtAcceptButton.BorderColor = System.Drawing.Color.Wheat;
            this.AccountMgmtAcceptButton.CommentPriority = false;
            this.AccountMgmtAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AccountMgmtAcceptButton.EnableAutoPrint = false;
            this.AccountMgmtAcceptButton.FilterStatus = false;
            this.AccountMgmtAcceptButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AccountMgmtAcceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AccountMgmtAcceptButton.FocusRectangleEnabled = true;
            this.AccountMgmtAcceptButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountMgmtAcceptButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AccountMgmtAcceptButton.ImageSelected = false;
            this.AccountMgmtAcceptButton.Location = new System.Drawing.Point(434, 332);
            this.AccountMgmtAcceptButton.Name = "AccountMgmtAcceptButton";
            this.AccountMgmtAcceptButton.NewPadding = 5;
            this.AccountMgmtAcceptButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.AccountMgmtAcceptButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AccountMgmtAcceptButton.Size = new System.Drawing.Size(110, 30);
            this.AccountMgmtAcceptButton.StatusIndicator = false;
            this.AccountMgmtAcceptButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AccountMgmtAcceptButton.StatusOffText = null;
            this.AccountMgmtAcceptButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AccountMgmtAcceptButton.StatusOnText = null;
            this.AccountMgmtAcceptButton.TabIndex = 13;
            this.AccountMgmtAcceptButton.TabStop = false;
            this.AccountMgmtAcceptButton.Text = "Accept";
            this.AccountMgmtAcceptButton.UseVisualStyleBackColor = false;
            this.AccountMgmtAcceptButton.Click += new System.EventHandler(this.AccountMgmtAcceptButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.AccountMgmtVerticalScroll);
            this.panel1.Controls.Add(this.AccountMgmtDataGridView);
            this.panel1.Location = new System.Drawing.Point(7, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(537, 242);
            this.panel1.TabIndex = 161;
            this.panel1.TabStop = true;
            // 
            // AccountMgmtVerticalScroll
            // 
            this.AccountMgmtVerticalScroll.Enabled = false;
            this.AccountMgmtVerticalScroll.Location = new System.Drawing.Point(519, -1);
            this.AccountMgmtVerticalScroll.Name = "AccountMgmtVerticalScroll";
            this.AccountMgmtVerticalScroll.Size = new System.Drawing.Size(15, 242);
            this.AccountMgmtVerticalScroll.TabIndex = 163;
            // 
            // AccountMgmtDataGridView
            // 
            this.AccountMgmtDataGridView.AllowCellClick = true;
            this.AccountMgmtDataGridView.AllowDoubleClick = true;
            this.AccountMgmtDataGridView.AllowEmptyRows = true;
            this.AccountMgmtDataGridView.AllowEnterKey = false;
            this.AccountMgmtDataGridView.AllowSorting = true;
            this.AccountMgmtDataGridView.AllowUserToAddRows = false;
            this.AccountMgmtDataGridView.AllowUserToDeleteRows = false;
            this.AccountMgmtDataGridView.AllowUserToResizeColumns = false;
            this.AccountMgmtDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.AccountMgmtDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.AccountMgmtDataGridView.ApplyStandardBehaviour = false;
            this.AccountMgmtDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.AccountMgmtDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AccountMgmtDataGridView.ClearCurrentCellOnLeave = true;
            this.AccountMgmtDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AccountMgmtDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.AccountMgmtDataGridView.ColumnHeadersHeight = 22;
            this.AccountMgmtDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.AccountMgmtDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FunctionValue,
            this.Description,
            this.SemiAnnualCode,
            this.FunctionKeyID});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AccountMgmtDataGridView.DefaultCellStyle = dataGridViewCellStyle7;
            this.AccountMgmtDataGridView.DefaultRowIndex = 0;
            this.AccountMgmtDataGridView.DeselectCurrentCell = false;
            this.AccountMgmtDataGridView.DeselectSpecifiedRow = -1;
            this.AccountMgmtDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.AccountMgmtDataGridView.EnableBinding = true;
            this.AccountMgmtDataGridView.EnableHeadersVisualStyles = false;
            this.AccountMgmtDataGridView.GridColor = System.Drawing.Color.Black;
            this.AccountMgmtDataGridView.GridContentSelected = false;
            this.AccountMgmtDataGridView.IsEditableGrid = true;
            this.AccountMgmtDataGridView.IsSorted = false;
            this.AccountMgmtDataGridView.Location = new System.Drawing.Point(-1, -1);
            this.AccountMgmtDataGridView.MultiSelect = false;
            this.AccountMgmtDataGridView.Name = "AccountMgmtDataGridView";
            this.AccountMgmtDataGridView.NumRowsVisible = 10;
            this.AccountMgmtDataGridView.PrimaryKeyColumnName = "";
            this.AccountMgmtDataGridView.RemainSortFields = true;
            this.AccountMgmtDataGridView.RemoveDefaultSelection = true;
            this.AccountMgmtDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AccountMgmtDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.AccountMgmtDataGridView.RowHeadersWidth = 20;
            this.AccountMgmtDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.AccountMgmtDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AccountMgmtDataGridView.Size = new System.Drawing.Size(537, 242);
            this.AccountMgmtDataGridView.TabIndex = 9;
            this.AccountMgmtDataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.AccountMgmtDataGridView_CellBeginEdit);
            this.AccountMgmtDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AccountMgmtDataGridView_KeyDown);
            this.AccountMgmtDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AccountMgmtDataGridView_CellClick);
            this.AccountMgmtDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.AccountMgmtDataGridView_RowEnter);
            this.AccountMgmtDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AccountMgmtDataGridView_CellDoubleClick);
            this.AccountMgmtDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AccountMgmtDataGridView_ColumnHeaderMouseClick);
            this.AccountMgmtDataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AccountMgmtDataGridView_RowHeaderMouseClick);
            this.AccountMgmtDataGridView.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.AccountMgmtDataGridView_CellMouseEnter);
            this.AccountMgmtDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.AccountMgmtDataGridView_CellEndEdit);
            this.AccountMgmtDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.AccountMgmtDataGridView_CellValueChanged);
            this.AccountMgmtDataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AccountMgmtDataGridView_RowHeaderMouseDoubleClick);
            this.AccountMgmtDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.AccountMgmtDataGridView_EditingControlShowing);
            // 
            // FunctionValue
            // 
            this.FunctionValue.DataPropertyName = "FunctionValue";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FunctionValue.DefaultCellStyle = dataGridViewCellStyle3;
            this.FunctionValue.HeaderText = "Function";
            this.FunctionValue.MaxInputLength = 50;
            this.FunctionValue.Name = "FunctionValue";
            this.FunctionValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FunctionValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FunctionValue.Width = 90;
            // 
            // Description
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Description.DefaultCellStyle = dataGridViewCellStyle4;
            this.Description.HeaderText = "Description";
            this.Description.MaxInputLength = 50;
            this.Description.Name = "Description";
            this.Description.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Description.Width = 250;
            // 
            // SemiAnnualCode
            // 
            this.SemiAnnualCode.DataPropertyName = "SemiAnnualCode";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SemiAnnualCode.DefaultCellStyle = dataGridViewCellStyle5;
            this.SemiAnnualCode.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.SemiAnnualCode.DisplayStyleForCurrentCellOnly = true;
            this.SemiAnnualCode.HeaderText = "Type";
            this.SemiAnnualCode.Name = "SemiAnnualCode";
            this.SemiAnnualCode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SemiAnnualCode.Width = 160;
            // 
            // FunctionKeyID
            // 
            this.FunctionKeyID.DataPropertyName = "FunctionKeyID";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FunctionKeyID.DefaultCellStyle = dataGridViewCellStyle6;
            this.FunctionKeyID.HeaderText = "FunctionKeyID";
            this.FunctionKeyID.MaxInputLength = 50;
            this.FunctionKeyID.Name = "FunctionKeyID";
            this.FunctionKeyID.ReadOnly = true;
            this.FunctionKeyID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FunctionKeyID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FunctionKeyID.Visible = false;
            // 
            // FunctionsMenuStrip
            // 
            this.FunctionsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToolStripMenuItem});
            this.FunctionsMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.FunctionsMenuStrip.Name = "FunctionsMenuStrip";
            this.FunctionsMenuStrip.Size = new System.Drawing.Size(552, 24);
            this.FunctionsMenuStrip.TabIndex = 163;
            this.FunctionsMenuStrip.Text = "FunctionsMenuStrip";
            this.FunctionsMenuStrip.Visible = false;
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(133, 20);
            this.SaveToolStripMenuItem.Text = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // F1502
            // 
            this.AcceptButton = this.SearchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(552, 388);
            this.Controls.Add(this.FunctionsMenuStrip);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RecordCountLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AccountMgmtCancelButton);
            this.Controls.Add(this.AccountMgmtCloseButton);
            this.Controls.Add(this.AccountMgmtSaveButton);
            this.Controls.Add(this.AccountMgmtAcceptButton);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.TypePanel);
            this.Controls.Add(this.DescriptionPanel);
            this.Controls.Add(this.FunctionPanel);
            this.Controls.Add(this.AccountElementMgmtPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.FunctionsMenuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(558, 420);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(558, 420);
            this.Name = "F1502";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - Functions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F1502_FormClosing);
            this.Load += new System.EventHandler(this.F1502_Load);
            this.AccountElementMgmtPanel.ResumeLayout(false);
            this.AccountElementMgmtPanel.PerformLayout();
            this.FunctionPanel.ResumeLayout(false);
            this.FunctionPanel.PerformLayout();
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.TypePanel.ResumeLayout(false);
            this.TypePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AccountMgmtDataGridView)).EndInit();
            this.FunctionsMenuStrip.ResumeLayout(false);
            this.FunctionsMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel AccountElementMgmtPanel;
        private System.Windows.Forms.Panel LinePanel;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.Panel FunctionPanel;
        private TerraScan.UI.Controls.TerraScanTextBox FunctionTextBox;
        private System.Windows.Forms.Label FunctionLabel;
        private System.Windows.Forms.Panel DescriptionPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DescTextBox;
        private System.Windows.Forms.Label DescriptionLabel;
        private TerraScan.UI.Controls.TerraScanButton ClearButton;
        private TerraScan.UI.Controls.TerraScanButton SearchButton;
        private System.Windows.Forms.Panel TypePanel;
        private System.Windows.Forms.Label TypeLabel;
        private TerraScan.UI.Controls.TerraScanComboBox TypeComboBox;
        private System.Windows.Forms.Panel FormLinePanel;
        private TerraScan.UI.Controls.TerraScanButton AccountMgmtAcceptButton;
        private TerraScan.UI.Controls.TerraScanButton AccountMgmtSaveButton;
        private TerraScan.UI.Controls.TerraScanButton AccountMgmtCloseButton;
        private TerraScan.UI.Controls.TerraScanButton AccountMgmtCancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label RecordCountLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.VScrollBar AccountMgmtVerticalScroll;
        private TerraScan.UI.Controls.TerraScanDataGridView AccountMgmtDataGridView;
        private System.Windows.Forms.MenuStrip FunctionsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewComboBoxColumn SemiAnnualCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionKeyID;
    }
}