namespace D9500
{
    partial class F9104
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9104));
            this.DescTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.FundTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.FundLabel = new System.Windows.Forms.Label();
            this.RecordCountLabel = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.FundSearchButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FundAcceptButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FundManagementLinkLabel = new TerraScan.UI.Controls.TerraScanLinkLabel();
            this.FundClearButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FundCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.FundPanel = new System.Windows.Forms.Panel();
            this.FundSlectionVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.FundSelectionDataGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.FundID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fund = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescriptionPanel.SuspendLayout();
            this.FundPanel.SuspendLayout();
            this.GridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FundSelectionDataGridView)).BeginInit();
            this.SuspendLayout();
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
            this.DescTextBox.Location = new System.Drawing.Point(13, 16);
            this.DescTextBox.LockKeyPress = false;
            this.DescTextBox.MaxLength = 50;
            this.DescTextBox.Name = "DescTextBox";
            this.DescTextBox.PersistDefaultColor = false;
            this.DescTextBox.Precision = 2;
            this.DescTextBox.QueryingFileldName = "";
            this.DescTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescTextBox.Size = new System.Drawing.Size(323, 16);
            this.DescTextBox.SpecialCharacter = "%";
            this.DescTextBox.TabIndex = 4;
            this.DescTextBox.TextCustomFormat = "$#,##0.00";
            this.DescTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescTextBox.TextChanged += new System.EventHandler(this.AllFundTextBoxs_TextChanged);
            // 
            // FundTextBox
            // 
            this.FundTextBox.AllowClick = true;
            this.FundTextBox.AllowNegativeSign = false;
            this.FundTextBox.ApplyCFGFormat = false;
            this.FundTextBox.ApplyCurrencyFormat = false;
            this.FundTextBox.ApplyFocusColor = true;
            this.FundTextBox.ApplyNegativeStandard = true;
            this.FundTextBox.ApplyParentFocusColor = true;
            this.FundTextBox.ApplyTimeFormat = false;
            this.FundTextBox.BackColor = System.Drawing.Color.White;
            this.FundTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FundTextBox.CFromatWihoutSymbol = false;
            this.FundTextBox.CheckForEmpty = false;
            this.FundTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FundTextBox.Digits = -1;
            this.FundTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.FundTextBox.ForeColor = System.Drawing.Color.Black;
            this.FundTextBox.IsEditable = false;
            this.FundTextBox.IsQueryableFileld = false;
            this.FundTextBox.Location = new System.Drawing.Point(18, 16);
            this.FundTextBox.LockKeyPress = false;
            this.FundTextBox.MaxLength = 5;
            this.FundTextBox.Name = "FundTextBox";
            this.FundTextBox.PersistDefaultColor = false;
            this.FundTextBox.Precision = 2;
            this.FundTextBox.QueryingFileldName = "";
            this.FundTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FundTextBox.Size = new System.Drawing.Size(161, 16);
            this.FundTextBox.SpecialCharacter = "%";
            this.FundTextBox.TabIndex = 2;
            this.FundTextBox.TextCustomFormat = "$#,##0.00";
            this.FundTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Numeric;
            this.FundTextBox.TextChanged += new System.EventHandler(this.AllFundTextBoxs_TextChanged);
            // 
            // FundLabel
            // 
            this.FundLabel.AutoSize = true;
            this.FundLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FundLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.FundLabel.Location = new System.Drawing.Point(1, 1);
            this.FundLabel.Name = "FundLabel";
            this.FundLabel.Size = new System.Drawing.Size(37, 14);
            this.FundLabel.TabIndex = 62;
            this.FundLabel.Text = "Fund:";
            // 
            // RecordCountLabel
            // 
            this.RecordCountLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordCountLabel.Location = new System.Drawing.Point(346, 236);
            this.RecordCountLabel.Name = "RecordCountLabel";
            this.RecordCountLabel.Size = new System.Drawing.Size(193, 15);
            this.RecordCountLabel.TabIndex = 167;
            this.RecordCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // FundSearchButton
            // 
            this.FundSearchButton.ActualPermission = false;
            this.FundSearchButton.ApplyDisableBehaviour = false;
            this.FundSearchButton.AutoSize = true;
            this.FundSearchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.FundSearchButton.BorderColor = System.Drawing.Color.Wheat;
            this.FundSearchButton.CommentPriority = false;
            this.FundSearchButton.EnableAutoPrint = false;
            this.FundSearchButton.FilterStatus = false;
            this.FundSearchButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.FundSearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FundSearchButton.FocusRectangleEnabled = true;
            this.FundSearchButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundSearchButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FundSearchButton.ImageSelected = false;
            this.FundSearchButton.Location = new System.Drawing.Point(152, 190);
            this.FundSearchButton.Name = "FundSearchButton";
            this.FundSearchButton.NewPadding = 5;
            this.FundSearchButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.FundSearchButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.FundSearchButton.Size = new System.Drawing.Size(110, 30);
            this.FundSearchButton.StatusIndicator = false;
            this.FundSearchButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FundSearchButton.StatusOffText = null;
            this.FundSearchButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.FundSearchButton.StatusOnText = null;
            this.FundSearchButton.TabIndex = 7;
            this.FundSearchButton.TabStop = false;
            this.FundSearchButton.Text = "Search";
            this.FundSearchButton.UseVisualStyleBackColor = false;
            this.FundSearchButton.Click += new System.EventHandler(this.FundSearchButton_Click);
            // 
            // FundAcceptButton
            // 
            this.FundAcceptButton.ActualPermission = false;
            this.FundAcceptButton.ApplyDisableBehaviour = false;
            this.FundAcceptButton.AutoSize = true;
            this.FundAcceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.FundAcceptButton.BorderColor = System.Drawing.Color.Wheat;
            this.FundAcceptButton.CommentPriority = false;
            this.FundAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.FundAcceptButton.EnableAutoPrint = false;
            this.FundAcceptButton.FilterStatus = false;
            this.FundAcceptButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.FundAcceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FundAcceptButton.FocusRectangleEnabled = true;
            this.FundAcceptButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundAcceptButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FundAcceptButton.ImageSelected = false;
            this.FundAcceptButton.Location = new System.Drawing.Point(13, 190);
            this.FundAcceptButton.Name = "FundAcceptButton";
            this.FundAcceptButton.NewPadding = 5;
            this.FundAcceptButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.FundAcceptButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.FundAcceptButton.Size = new System.Drawing.Size(110, 30);
            this.FundAcceptButton.StatusIndicator = false;
            this.FundAcceptButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FundAcceptButton.StatusOffText = null;
            this.FundAcceptButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.FundAcceptButton.StatusOnText = null;
            this.FundAcceptButton.TabIndex = 6;
            this.FundAcceptButton.TabStop = false;
            this.FundAcceptButton.Text = "Accept";
            this.FundAcceptButton.UseVisualStyleBackColor = false;
            this.FundAcceptButton.Click += new System.EventHandler(this.FundAcceptButton_Click);
            // 
            // FundManagementLinkLabel
            // 
            this.FundManagementLinkLabel.AutoSize = true;
            this.FundManagementLinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundManagementLinkLabel.FormDllName = null;
            this.FundManagementLinkLabel.FormId = 0;
            this.FundManagementLinkLabel.Location = new System.Drawing.Point(223, 237);
            this.FundManagementLinkLabel.MenuName = null;
            this.FundManagementLinkLabel.Name = "FundManagementLinkLabel";
            this.FundManagementLinkLabel.PermissionOpen = 0;
            this.FundManagementLinkLabel.Size = new System.Drawing.Size(111, 15);
            this.FundManagementLinkLabel.TabIndex = 10;
            this.FundManagementLinkLabel.TabStop = true;
            this.FundManagementLinkLabel.Text = "Fund Management";
            this.FundManagementLinkLabel.TextCustomFormat = "#,##0.00";
            this.FundManagementLinkLabel.ValidateType = TerraScan.UI.Controls.TerraScanLinkLabel.ControlValidationType.Text;
            this.FundManagementLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FundManagementLinkLabel_LinkClicked);
            // 
            // FundClearButton
            // 
            this.FundClearButton.ActualPermission = false;
            this.FundClearButton.ApplyDisableBehaviour = false;
            this.FundClearButton.AutoSize = true;
            this.FundClearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.FundClearButton.BorderColor = System.Drawing.Color.Wheat;
            this.FundClearButton.CommentPriority = false;
            this.FundClearButton.EnableAutoPrint = false;
            this.FundClearButton.FilterStatus = false;
            this.FundClearButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.FundClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FundClearButton.FocusRectangleEnabled = true;
            this.FundClearButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundClearButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FundClearButton.ImageSelected = false;
            this.FundClearButton.Location = new System.Drawing.Point(291, 190);
            this.FundClearButton.Name = "FundClearButton";
            this.FundClearButton.NewPadding = 5;
            this.FundClearButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.FundClearButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.FundClearButton.Size = new System.Drawing.Size(110, 30);
            this.FundClearButton.StatusIndicator = false;
            this.FundClearButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FundClearButton.StatusOffText = null;
            this.FundClearButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.FundClearButton.StatusOnText = null;
            this.FundClearButton.TabIndex = 8;
            this.FundClearButton.TabStop = false;
            this.FundClearButton.Text = "Clear";
            this.FundClearButton.UseVisualStyleBackColor = false;
            this.FundClearButton.Click += new System.EventHandler(this.FundClearButton_Click);
            // 
            // FundCancelButton
            // 
            this.FundCancelButton.ActualPermission = false;
            this.FundCancelButton.ApplyDisableBehaviour = false;
            this.FundCancelButton.AutoSize = true;
            this.FundCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.FundCancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.FundCancelButton.CommentPriority = false;
            this.FundCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.FundCancelButton.EnableAutoPrint = false;
            this.FundCancelButton.FilterStatus = false;
            this.FundCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.FundCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FundCancelButton.FocusRectangleEnabled = true;
            this.FundCancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundCancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FundCancelButton.ImageSelected = false;
            this.FundCancelButton.Location = new System.Drawing.Point(431, 190);
            this.FundCancelButton.Name = "FundCancelButton";
            this.FundCancelButton.NewPadding = 5;
            this.FundCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.FundCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.FundCancelButton.Size = new System.Drawing.Size(110, 30);
            this.FundCancelButton.StatusIndicator = false;
            this.FundCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FundCancelButton.StatusOffText = null;
            this.FundCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.FundCancelButton.StatusOnText = null;
            this.FundCancelButton.TabIndex = 9;
            this.FundCancelButton.TabStop = false;
            this.FundCancelButton.Text = "Cancel";
            this.FundCancelButton.UseVisualStyleBackColor = false;
            this.FundCancelButton.Click += new System.EventHandler(this.FundCancelButton_Click);
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(14, 230);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(527, 2);
            this.FormLinePanel.TabIndex = 165;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(14, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 166;
            this.label1.Text = "9104";
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.BackColor = System.Drawing.Color.White;
            this.DescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionPanel.Controls.Add(this.DescTextBox);
            this.DescriptionPanel.Controls.Add(this.DescriptionLabel);
            this.DescriptionPanel.Location = new System.Drawing.Point(198, 10);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(343, 37);
            this.DescriptionPanel.TabIndex = 3;
            this.DescriptionPanel.TabStop = true;
            // 
            // FundPanel
            // 
            this.FundPanel.BackColor = System.Drawing.Color.White;
            this.FundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FundPanel.Controls.Add(this.FundTextBox);
            this.FundPanel.Controls.Add(this.FundLabel);
            this.FundPanel.Location = new System.Drawing.Point(13, 10);
            this.FundPanel.Name = "FundPanel";
            this.FundPanel.Size = new System.Drawing.Size(186, 37);
            this.FundPanel.TabIndex = 1;
            this.FundPanel.TabStop = true;
            // 
            // FundSlectionVerticalScroll
            // 
            this.FundSlectionVerticalScroll.Enabled = false;
            this.FundSlectionVerticalScroll.Location = new System.Drawing.Point(523, 47);
            this.FundSlectionVerticalScroll.Name = "FundSlectionVerticalScroll";
            this.FundSlectionVerticalScroll.Size = new System.Drawing.Size(16, 132);
            this.FundSlectionVerticalScroll.TabIndex = 172;
            // 
            // GridPanel
            // 
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.FundSelectionDataGridView);
            this.GridPanel.Location = new System.Drawing.Point(13, 46);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(528, 134);
            this.GridPanel.TabIndex = 173;
            // 
            // FundSelectionDataGridView
            // 
            this.FundSelectionDataGridView.AllowCellClick = true;
            this.FundSelectionDataGridView.AllowDoubleClick = true;
            this.FundSelectionDataGridView.AllowEmptyRows = true;
            this.FundSelectionDataGridView.AllowEnterKey = false;
            this.FundSelectionDataGridView.AllowSorting = true;
            this.FundSelectionDataGridView.AllowUserToAddRows = false;
            this.FundSelectionDataGridView.AllowUserToDeleteRows = false;
            this.FundSelectionDataGridView.AllowUserToResizeColumns = false;
            this.FundSelectionDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.FundSelectionDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.FundSelectionDataGridView.ApplyStandardBehaviour = false;
            this.FundSelectionDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.FundSelectionDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FundSelectionDataGridView.ClearCurrentCellOnLeave = false;
            this.FundSelectionDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FundSelectionDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.FundSelectionDataGridView.ColumnHeadersHeight = 24;
            this.FundSelectionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.FundSelectionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FundID,
            this.Fund,
            this.Description});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.FundSelectionDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.FundSelectionDataGridView.DefaultRowIndex = -1;
            this.FundSelectionDataGridView.DeselectCurrentCell = false;
            this.FundSelectionDataGridView.DeselectSpecifiedRow = -1;
            this.FundSelectionDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.FundSelectionDataGridView.EnableBinding = true;
            this.FundSelectionDataGridView.EnableHeadersVisualStyles = false;
            this.FundSelectionDataGridView.GridColor = System.Drawing.Color.Black;
            this.FundSelectionDataGridView.GridContentSelected = false;
            this.FundSelectionDataGridView.IsEditableGrid = false;
            this.FundSelectionDataGridView.IsSorted = false;
            this.FundSelectionDataGridView.Location = new System.Drawing.Point(-1, -1);
            this.FundSelectionDataGridView.MultiSelect = false;
            this.FundSelectionDataGridView.Name = "FundSelectionDataGridView";
            this.FundSelectionDataGridView.NumRowsVisible = 5;
            this.FundSelectionDataGridView.PrimaryKeyColumnName = "FundID";
            this.FundSelectionDataGridView.ReadOnly = true;
            this.FundSelectionDataGridView.RemainSortFields = false;
            this.FundSelectionDataGridView.RemoveDefaultSelection = false;
            this.FundSelectionDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FundSelectionDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.FundSelectionDataGridView.RowHeadersWidth = 20;
            this.FundSelectionDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.FundSelectionDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FundSelectionDataGridView.Size = new System.Drawing.Size(527, 134);
            this.FundSelectionDataGridView.StandardTab = true;
            this.FundSelectionDataGridView.TabIndex = 5;
            this.FundSelectionDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FundSelectionDataGridView_CellDoubleClick);
            this.FundSelectionDataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.FundSelectionDataGridView_RowHeaderMouseDoubleClick);
            // 
            // FundID
            // 
            this.FundID.DataPropertyName = "FundID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FundID.DefaultCellStyle = dataGridViewCellStyle3;
            this.FundID.HeaderText = "FundID";
            this.FundID.Name = "FundID";
            this.FundID.ReadOnly = true;
            this.FundID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.FundID.Visible = false;
            this.FundID.Width = 161;
            // 
            // Fund
            // 
            this.Fund.DataPropertyName = "Fund";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fund.DefaultCellStyle = dataGridViewCellStyle4;
            this.Fund.HeaderText = "Fund";
            this.Fund.Name = "Fund";
            this.Fund.ReadOnly = true;
            this.Fund.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Fund.Width = 187;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Description.DefaultCellStyle = dataGridViewCellStyle5;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Description.Width = 303;
            // 
            // F9104
            // 
            this.AcceptButton = this.FundSearchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.FundCancelButton;
            this.ClientSize = new System.Drawing.Size(554, 259);
            this.Controls.Add(this.FundSlectionVerticalScroll);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.RecordCountLabel);
            this.Controls.Add(this.FundSearchButton);
            this.Controls.Add(this.FundAcceptButton);
            this.Controls.Add(this.FundManagementLinkLabel);
            this.Controls.Add(this.FundClearButton);
            this.Controls.Add(this.FundCancelButton);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DescriptionPanel);
            this.Controls.Add(this.FundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(560, 291);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(560, 291);
            this.Name = "F9104";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - Fund Selection";
            this.Load += new System.EventHandler(this.F9104_Load);
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.FundPanel.ResumeLayout(false);
            this.FundPanel.PerformLayout();
            this.GridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FundSelectionDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanTextBox DescTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox FundTextBox;
        private System.Windows.Forms.Label FundLabel;
        private System.Windows.Forms.Label RecordCountLabel;
        private System.Windows.Forms.Label DescriptionLabel;
        private TerraScan.UI.Controls.TerraScanButton FundSearchButton;
        private TerraScan.UI.Controls.TerraScanButton FundAcceptButton;
        private TerraScan.UI.Controls.TerraScanLinkLabel FundManagementLinkLabel;
        private TerraScan.UI.Controls.TerraScanButton FundClearButton;
        private TerraScan.UI.Controls.TerraScanButton FundCancelButton;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel DescriptionPanel;
        private System.Windows.Forms.Panel FundPanel;
        private System.Windows.Forms.VScrollBar FundSlectionVerticalScroll;
        private System.Windows.Forms.Panel GridPanel;
        private TerraScan.UI.Controls.TerraScanDataGridView FundSelectionDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn FundID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fund;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}