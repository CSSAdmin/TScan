namespace D21000
{
    partial class F2101
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F2101));
            this.DescTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CodeTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CodeLabel = new System.Windows.Forms.Label();
            this.RecordCountLabel = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.LocationSearchButton = new TerraScan.UI.Controls.TerraScanButton();
            this.LocationAcceptButton = new TerraScan.UI.Controls.TerraScanButton();
            this.LocationManagementLinkLabel = new TerraScan.UI.Controls.TerraScanLinkLabel();
            this.LocationClearButton = new TerraScan.UI.Controls.TerraScanButton();
            this.LocationCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.CodePanel = new System.Windows.Forms.Panel();
            this.LocationSelectionVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.LocationSelectionDataGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.LocationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescriptionPanel.SuspendLayout();
            this.CodePanel.SuspendLayout();
            this.GridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocationSelectionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DescTextBox
            // 
            this.DescTextBox.AllowClick = true;
            this.DescTextBox.AllowNegativeSign = false;
            this.DescTextBox.ApplyCFGFormat = false;
            this.DescTextBox.ApplyCurrencyFormat = false;
            this.DescTextBox.ApplyFocusColor = true;
            this.DescTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
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
            this.DescTextBox.EmptyDecimalValue = false;
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
            this.DescTextBox.SetColorFlag = false;
            this.DescTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescTextBox.Size = new System.Drawing.Size(323, 16);
            this.DescTextBox.SpecialCharacter = "%";
            this.DescTextBox.TabIndex = 4;
            this.DescTextBox.TextCustomFormat = "$#,##0.00";
            this.DescTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescTextBox.WholeInteger = false;
            this.DescTextBox.TextChanged += new System.EventHandler(this.Editext);
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.AllowClick = true;
            this.CodeTextBox.AllowNegativeSign = false;
            this.CodeTextBox.ApplyCFGFormat = false;
            this.CodeTextBox.ApplyCurrencyFormat = false;
            this.CodeTextBox.ApplyFocusColor = true;
            this.CodeTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.CodeTextBox.ApplyNegativeStandard = true;
            this.CodeTextBox.ApplyParentFocusColor = true;
            this.CodeTextBox.ApplyTimeFormat = false;
            this.CodeTextBox.BackColor = System.Drawing.Color.White;
            this.CodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CodeTextBox.CFromatWihoutSymbol = false;
            this.CodeTextBox.CheckForEmpty = false;
            this.CodeTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CodeTextBox.Digits = -1;
            this.CodeTextBox.EmptyDecimalValue = false;
            this.CodeTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.CodeTextBox.ForeColor = System.Drawing.Color.Black;
            this.CodeTextBox.IsEditable = false;
            this.CodeTextBox.IsQueryableFileld = false;
            this.CodeTextBox.Location = new System.Drawing.Point(18, 16);
            this.CodeTextBox.LockKeyPress = false;
            this.CodeTextBox.MaxLength = 50;
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.PersistDefaultColor = false;
            this.CodeTextBox.Precision = 2;
            this.CodeTextBox.QueryingFileldName = "";
            this.CodeTextBox.SetColorFlag = false;
            this.CodeTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CodeTextBox.Size = new System.Drawing.Size(161, 16);
            this.CodeTextBox.SpecialCharacter = "%";
            this.CodeTextBox.TabIndex = 2;
            this.CodeTextBox.TextCustomFormat = "$#,##0.00";
            this.CodeTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CodeTextBox.WholeInteger = false;
            this.CodeTextBox.TextChanged += new System.EventHandler(this.Editext);
            // 
            // CodeLabel
            // 
            this.CodeLabel.AutoSize = true;
            this.CodeLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CodeLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CodeLabel.Location = new System.Drawing.Point(1, 1);
            this.CodeLabel.Name = "CodeLabel";
            this.CodeLabel.Size = new System.Drawing.Size(39, 14);
            this.CodeLabel.TabIndex = 62;
            this.CodeLabel.Text = "Code:";
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
            // LocationSearchButton
            // 
            this.LocationSearchButton.ActualPermission = false;
            this.LocationSearchButton.ApplyDisableBehaviour = false;
            this.LocationSearchButton.AutoSize = true;
            this.LocationSearchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.LocationSearchButton.BorderColor = System.Drawing.Color.Wheat;
            this.LocationSearchButton.CommentPriority = false;
            this.LocationSearchButton.EnableAutoPrint = false;
            this.LocationSearchButton.FilterStatus = false;
            this.LocationSearchButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.LocationSearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LocationSearchButton.FocusRectangleEnabled = true;
            this.LocationSearchButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationSearchButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LocationSearchButton.ImageSelected = false;
            this.LocationSearchButton.Location = new System.Drawing.Point(152, 190);
            this.LocationSearchButton.Name = "LocationSearchButton";
            this.LocationSearchButton.NewPadding = 5;
            this.LocationSearchButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.LocationSearchButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.LocationSearchButton.Size = new System.Drawing.Size(110, 30);
            this.LocationSearchButton.StatusIndicator = false;
            this.LocationSearchButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LocationSearchButton.StatusOffText = null;
            this.LocationSearchButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.LocationSearchButton.StatusOnText = null;
            this.LocationSearchButton.TabIndex = 8;
            this.LocationSearchButton.TabStop = false;
            this.LocationSearchButton.Text = "Search";
            this.LocationSearchButton.UseVisualStyleBackColor = false;
            this.LocationSearchButton.Click += new System.EventHandler(this.LocationSearchButton_Click);
            // 
            // LocationAcceptButton
            // 
            this.LocationAcceptButton.ActualPermission = false;
            this.LocationAcceptButton.ApplyDisableBehaviour = false;
            this.LocationAcceptButton.AutoSize = true;
            this.LocationAcceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.LocationAcceptButton.BorderColor = System.Drawing.Color.Wheat;
            this.LocationAcceptButton.CommentPriority = false;
            this.LocationAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.LocationAcceptButton.EnableAutoPrint = false;
            this.LocationAcceptButton.FilterStatus = false;
            this.LocationAcceptButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.LocationAcceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LocationAcceptButton.FocusRectangleEnabled = true;
            this.LocationAcceptButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationAcceptButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LocationAcceptButton.ImageSelected = false;
            this.LocationAcceptButton.Location = new System.Drawing.Point(13, 190);
            this.LocationAcceptButton.Name = "LocationAcceptButton";
            this.LocationAcceptButton.NewPadding = 5;
            this.LocationAcceptButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.LocationAcceptButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.LocationAcceptButton.Size = new System.Drawing.Size(110, 30);
            this.LocationAcceptButton.StatusIndicator = false;
            this.LocationAcceptButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LocationAcceptButton.StatusOffText = null;
            this.LocationAcceptButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.LocationAcceptButton.StatusOnText = null;
            this.LocationAcceptButton.TabIndex = 7;
            this.LocationAcceptButton.TabStop = false;
            this.LocationAcceptButton.Text = "Accept";
            this.LocationAcceptButton.UseVisualStyleBackColor = false;
            this.LocationAcceptButton.Click += new System.EventHandler(this.LocationAcceptButton_Click);
            // 
            // LocationManagementLinkLabel
            // 
            this.LocationManagementLinkLabel.AutoSize = true;
            this.LocationManagementLinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationManagementLinkLabel.FormDllName = null;
            this.LocationManagementLinkLabel.FormId = 0;
            this.LocationManagementLinkLabel.Location = new System.Drawing.Point(215, 237);
            this.LocationManagementLinkLabel.MenuName = null;
            this.LocationManagementLinkLabel.Name = "LocationManagementLinkLabel";
            this.LocationManagementLinkLabel.PermissionOpen = 0;
            this.LocationManagementLinkLabel.Size = new System.Drawing.Size(133, 15);
            this.LocationManagementLinkLabel.TabIndex = 11;
            this.LocationManagementLinkLabel.TabStop = true;
            this.LocationManagementLinkLabel.Text = "Location Management";
            this.LocationManagementLinkLabel.TextCustomFormat = "#,##0.00";
            this.LocationManagementLinkLabel.ValidateType = TerraScan.UI.Controls.TerraScanLinkLabel.ControlValidationType.Text;
            this.LocationManagementLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LocationManagementLinkLabel_LinkClicked);
            // 
            // LocationClearButton
            // 
            this.LocationClearButton.ActualPermission = false;
            this.LocationClearButton.ApplyDisableBehaviour = false;
            this.LocationClearButton.AutoSize = true;
            this.LocationClearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.LocationClearButton.BorderColor = System.Drawing.Color.Wheat;
            this.LocationClearButton.CommentPriority = false;
            this.LocationClearButton.EnableAutoPrint = false;
            this.LocationClearButton.FilterStatus = false;
            this.LocationClearButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.LocationClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LocationClearButton.FocusRectangleEnabled = true;
            this.LocationClearButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationClearButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LocationClearButton.ImageSelected = false;
            this.LocationClearButton.Location = new System.Drawing.Point(291, 190);
            this.LocationClearButton.Name = "LocationClearButton";
            this.LocationClearButton.NewPadding = 5;
            this.LocationClearButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.LocationClearButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.LocationClearButton.Size = new System.Drawing.Size(110, 30);
            this.LocationClearButton.StatusIndicator = false;
            this.LocationClearButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LocationClearButton.StatusOffText = null;
            this.LocationClearButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.LocationClearButton.StatusOnText = null;
            this.LocationClearButton.TabIndex = 9;
            this.LocationClearButton.TabStop = false;
            this.LocationClearButton.Text = "Clear";
            this.LocationClearButton.UseVisualStyleBackColor = false;
            this.LocationClearButton.Click += new System.EventHandler(this.LocationClearButton_Click);
            // 
            // LocationCancelButton
            // 
            this.LocationCancelButton.ActualPermission = false;
            this.LocationCancelButton.ApplyDisableBehaviour = false;
            this.LocationCancelButton.AutoSize = true;
            this.LocationCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.LocationCancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.LocationCancelButton.CommentPriority = false;
            this.LocationCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.LocationCancelButton.EnableAutoPrint = false;
            this.LocationCancelButton.FilterStatus = false;
            this.LocationCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.LocationCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LocationCancelButton.FocusRectangleEnabled = true;
            this.LocationCancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationCancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LocationCancelButton.ImageSelected = false;
            this.LocationCancelButton.Location = new System.Drawing.Point(431, 190);
            this.LocationCancelButton.Name = "LocationCancelButton";
            this.LocationCancelButton.NewPadding = 5;
            this.LocationCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.LocationCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.LocationCancelButton.Size = new System.Drawing.Size(110, 30);
            this.LocationCancelButton.StatusIndicator = false;
            this.LocationCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LocationCancelButton.StatusOffText = null;
            this.LocationCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.LocationCancelButton.StatusOnText = null;
            this.LocationCancelButton.TabIndex = 10;
            this.LocationCancelButton.TabStop = false;
            this.LocationCancelButton.Text = "Cancel";
            this.LocationCancelButton.UseVisualStyleBackColor = false;
            this.LocationCancelButton.Click += new System.EventHandler(this.LocationCancelButton_Click);
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
            this.label1.Text = "2101";
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
            // CodePanel
            // 
            this.CodePanel.BackColor = System.Drawing.Color.White;
            this.CodePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CodePanel.Controls.Add(this.CodeTextBox);
            this.CodePanel.Controls.Add(this.CodeLabel);
            this.CodePanel.Location = new System.Drawing.Point(13, 10);
            this.CodePanel.Name = "CodePanel";
            this.CodePanel.Size = new System.Drawing.Size(186, 37);
            this.CodePanel.TabIndex = 1;
            this.CodePanel.TabStop = true;
            // 
            // LocationSelectionVerticalScroll
            // 
            this.LocationSelectionVerticalScroll.Enabled = false;
            this.LocationSelectionVerticalScroll.Location = new System.Drawing.Point(523, 47);
            this.LocationSelectionVerticalScroll.Name = "LocationSelectionVerticalScroll";
            this.LocationSelectionVerticalScroll.Size = new System.Drawing.Size(16, 132);
            this.LocationSelectionVerticalScroll.TabIndex = 172;
            // 
            // GridPanel
            // 
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.LocationSelectionDataGridView);
            this.GridPanel.Location = new System.Drawing.Point(13, 46);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(528, 134);
            this.GridPanel.TabIndex = 5;
            // 
            // LocationSelectionDataGridView
            // 
            this.LocationSelectionDataGridView.AllowCellClick = true;
            this.LocationSelectionDataGridView.AllowDoubleClick = true;
            this.LocationSelectionDataGridView.AllowEmptyRows = true;
            this.LocationSelectionDataGridView.AllowEnterKey = false;
            this.LocationSelectionDataGridView.AllowSorting = true;
            this.LocationSelectionDataGridView.AllowUserToAddRows = false;
            this.LocationSelectionDataGridView.AllowUserToDeleteRows = false;
            this.LocationSelectionDataGridView.AllowUserToResizeColumns = false;
            this.LocationSelectionDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.LocationSelectionDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.LocationSelectionDataGridView.ApplyStandardBehaviour = false;
            this.LocationSelectionDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.LocationSelectionDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LocationSelectionDataGridView.ClearCurrentCellOnLeave = false;
            this.LocationSelectionDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LocationSelectionDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.LocationSelectionDataGridView.ColumnHeadersHeight = 24;
            this.LocationSelectionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.LocationSelectionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LocationID,
            this.Code,
            this.Description});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.LocationSelectionDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.LocationSelectionDataGridView.DefaultRowIndex = -1;
            this.LocationSelectionDataGridView.DeselectCurrentCell = false;
            this.LocationSelectionDataGridView.DeselectSpecifiedRow = -1;
            this.LocationSelectionDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.LocationSelectionDataGridView.EnableBinding = true;
            this.LocationSelectionDataGridView.EnableHeadersVisualStyles = false;
            this.LocationSelectionDataGridView.GridColor = System.Drawing.Color.Black;
            this.LocationSelectionDataGridView.GridContentSelected = false;
            this.LocationSelectionDataGridView.IsEditableGrid = false;
            this.LocationSelectionDataGridView.IsMultiSelect = false;
            this.LocationSelectionDataGridView.IsSorted = false;
            this.LocationSelectionDataGridView.Location = new System.Drawing.Point(-1, -1);
            this.LocationSelectionDataGridView.MultiSelect = false;
            this.LocationSelectionDataGridView.Name = "LocationSelectionDataGridView";
            this.LocationSelectionDataGridView.NumRowsVisible = 5;
            this.LocationSelectionDataGridView.PrimaryKeyColumnName = "FundID";
            this.LocationSelectionDataGridView.ReadOnly = true;
            this.LocationSelectionDataGridView.RemainSortFields = false;
            this.LocationSelectionDataGridView.RemoveDefaultSelection = false;
            this.LocationSelectionDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LocationSelectionDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.LocationSelectionDataGridView.RowHeadersWidth = 20;
            this.LocationSelectionDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.LocationSelectionDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LocationSelectionDataGridView.Size = new System.Drawing.Size(527, 134);
            this.LocationSelectionDataGridView.StandardTab = true;
            this.LocationSelectionDataGridView.TabIndex = 6;
            this.LocationSelectionDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.LocationSelectionDataGridView_CellDoubleClick);
            this.LocationSelectionDataGridView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.LocationManagementLinkLabel_PreviewKeyDown);
            this.LocationSelectionDataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.LocationSelectionDataGridView_RowHeaderMouseClick);
            // 
            // LocationID
            // 
            this.LocationID.DataPropertyName = "LocationID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationID.DefaultCellStyle = dataGridViewCellStyle3;
            this.LocationID.HeaderText = "LocationID";
            this.LocationID.Name = "LocationID";
            this.LocationID.ReadOnly = true;
            this.LocationID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.LocationID.Visible = false;
            this.LocationID.Width = 161;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Code.DefaultCellStyle = dataGridViewCellStyle4;
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Code.Width = 187;
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
            // F2101
            // 
            this.AcceptButton = this.LocationSearchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.LocationCancelButton;
            this.ClientSize = new System.Drawing.Size(554, 263);
            this.Controls.Add(this.LocationSelectionVerticalScroll);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.RecordCountLabel);
            this.Controls.Add(this.LocationSearchButton);
            this.Controls.Add(this.LocationAcceptButton);
            this.Controls.Add(this.LocationManagementLinkLabel);
            this.Controls.Add(this.LocationClearButton);
            this.Controls.Add(this.LocationCancelButton);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DescriptionPanel);
            this.Controls.Add(this.CodePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(560, 291);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(560, 291);
            this.Name = "F2101";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terrascan T2 - Location Selection";
            this.Load += new System.EventHandler(this.F2101_Load);
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.CodePanel.ResumeLayout(false);
            this.CodePanel.PerformLayout();
            this.GridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LocationSelectionDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanTextBox DescTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox CodeTextBox;
        private System.Windows.Forms.Label CodeLabel;
        private System.Windows.Forms.Label RecordCountLabel;
        private System.Windows.Forms.Label DescriptionLabel;
        private TerraScan.UI.Controls.TerraScanButton LocationSearchButton;
        private TerraScan.UI.Controls.TerraScanButton LocationAcceptButton;
        private TerraScan.UI.Controls.TerraScanLinkLabel LocationManagementLinkLabel;
        private TerraScan.UI.Controls.TerraScanButton LocationClearButton;
        private TerraScan.UI.Controls.TerraScanButton LocationCancelButton;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel DescriptionPanel;
        private System.Windows.Forms.Panel CodePanel;
        private System.Windows.Forms.VScrollBar LocationSelectionVerticalScroll;
        private System.Windows.Forms.Panel GridPanel;
        private TerraScan.UI.Controls.TerraScanDataGridView LocationSelectionDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}