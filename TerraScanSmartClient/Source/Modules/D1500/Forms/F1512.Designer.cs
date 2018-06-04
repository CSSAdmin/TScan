namespace D1500
{
    partial class F1512
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1512));
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.DistrictNumberLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.DescTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DistrictNumberPanel = new System.Windows.Forms.Panel();
            this.DistrictNoTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.RollYearPanel = new System.Windows.Forms.Panel();
            this.RollYearTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.RollYeatLabel = new System.Windows.Forms.Label();
            this.RecordCountLabel = new System.Windows.Forms.Label();
            this.DistrictSlectionVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DistrictSelectionDataGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.DistrictCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DistrictClearButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DistrictSearchButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DistrictAcceptButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DistrictManagementLinkLabel = new TerraScan.UI.Controls.TerraScanLinkLabel();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.District = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RollYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescriptionPanel.SuspendLayout();
            this.DistrictNumberPanel.SuspendLayout();
            this.RollYearPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DistrictSelectionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(12, 235);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(528, 2);
            this.FormLinePanel.TabIndex = 116;
            // 
            // DistrictNumberLabel
            // 
            this.DistrictNumberLabel.AutoSize = true;
            this.DistrictNumberLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DistrictNumberLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistrictNumberLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DistrictNumberLabel.Location = new System.Drawing.Point(1, 1);
            this.DistrictNumberLabel.Name = "DistrictNumberLabel";
            this.DistrictNumberLabel.Size = new System.Drawing.Size(96, 14);
            this.DistrictNumberLabel.TabIndex = 62;
            this.DistrictNumberLabel.Text = "District Number:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(15, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 117;
            this.label1.Text = "1512";
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
            // DescriptionPanel
            // 
            this.DescriptionPanel.BackColor = System.Drawing.Color.White;
            this.DescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionPanel.Controls.Add(this.DescTextBox);
            this.DescriptionPanel.Controls.Add(this.DescriptionLabel);
            this.DescriptionPanel.Location = new System.Drawing.Point(122, 12);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(311, 37);
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
            this.DescTextBox.Location = new System.Drawing.Point(14, 16);
            this.DescTextBox.LockKeyPress = false;
            this.DescTextBox.MaxLength = 100;
            this.DescTextBox.Name = "DescTextBox";
            this.DescTextBox.PersistDefaultColor = false;
            this.DescTextBox.Precision = 2;
            this.DescTextBox.QueryingFileldName = "";
            this.DescTextBox.SetColorFlag = false;
            this.DescTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescTextBox.Size = new System.Drawing.Size(191, 16);
            this.DescTextBox.SpecialCharacter = "%";
            this.DescTextBox.TabIndex = 6;
            this.DescTextBox.TextCustomFormat = "$#,##0.00";
            this.DescTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescTextBox.WholeInteger = false;
            this.DescTextBox.TextChanged += new System.EventHandler(this.Editext);
            // 
            // DistrictNumberPanel
            // 
            this.DistrictNumberPanel.BackColor = System.Drawing.Color.White;
            this.DistrictNumberPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DistrictNumberPanel.Controls.Add(this.DistrictNoTextBox);
            this.DistrictNumberPanel.Controls.Add(this.DistrictNumberLabel);
            this.DistrictNumberPanel.Location = new System.Drawing.Point(12, 12);
            this.DistrictNumberPanel.Name = "DistrictNumberPanel";
            this.DistrictNumberPanel.Size = new System.Drawing.Size(111, 37);
            this.DistrictNumberPanel.TabIndex = 3;
            this.DistrictNumberPanel.TabStop = true;
            // 
            // DistrictNoTextBox
            // 
            this.DistrictNoTextBox.AllowClick = true;
            this.DistrictNoTextBox.AllowNegativeSign = false;
            this.DistrictNoTextBox.ApplyCFGFormat = false;
            this.DistrictNoTextBox.ApplyCurrencyFormat = false;
            this.DistrictNoTextBox.ApplyFocusColor = true;
            this.DistrictNoTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.DistrictNoTextBox.ApplyNegativeStandard = true;
            this.DistrictNoTextBox.ApplyParentFocusColor = true;
            this.DistrictNoTextBox.ApplyTimeFormat = false;
            this.DistrictNoTextBox.BackColor = System.Drawing.Color.White;
            this.DistrictNoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DistrictNoTextBox.CFromatWihoutSymbol = false;
            this.DistrictNoTextBox.CheckForEmpty = false;
            this.DistrictNoTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DistrictNoTextBox.Digits = -1;
            this.DistrictNoTextBox.EmptyDecimalValue = false;
            this.DistrictNoTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.DistrictNoTextBox.ForeColor = System.Drawing.Color.Black;
            this.DistrictNoTextBox.IsEditable = false;
            this.DistrictNoTextBox.IsQueryableFileld = false;
            this.DistrictNoTextBox.Location = new System.Drawing.Point(16, 16);
            this.DistrictNoTextBox.LockKeyPress = false;
            this.DistrictNoTextBox.MaxLength = 50;
            this.DistrictNoTextBox.Name = "DistrictNoTextBox";
            this.DistrictNoTextBox.PersistDefaultColor = false;
            this.DistrictNoTextBox.Precision = 2;
            this.DistrictNoTextBox.QueryingFileldName = "";
            this.DistrictNoTextBox.SetColorFlag = false;
            this.DistrictNoTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DistrictNoTextBox.Size = new System.Drawing.Size(79, 16);
            this.DistrictNoTextBox.SpecialCharacter = "%";
            this.DistrictNoTextBox.TabIndex = 4;
            this.DistrictNoTextBox.TextCustomFormat = "$#,##0.00";
            this.DistrictNoTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DistrictNoTextBox.WholeInteger = false;
            this.DistrictNoTextBox.TextChanged += new System.EventHandler(this.Editext);
            // 
            // RollYearPanel
            // 
            this.RollYearPanel.BackColor = System.Drawing.Color.White;
            this.RollYearPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RollYearPanel.Controls.Add(this.RollYearTextBox);
            this.RollYearPanel.Controls.Add(this.RollYeatLabel);
            this.RollYearPanel.Location = new System.Drawing.Point(432, 12);
            this.RollYearPanel.Name = "RollYearPanel";
            this.RollYearPanel.Size = new System.Drawing.Size(108, 37);
            this.RollYearPanel.TabIndex = 7;
            this.RollYearPanel.TabStop = true;
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
            this.RollYearTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.RollYearTextBox.ForeColor = System.Drawing.Color.Black;
            this.RollYearTextBox.IsEditable = false;
            this.RollYearTextBox.IsQueryableFileld = false;
            this.RollYearTextBox.Location = new System.Drawing.Point(12, 16);
            this.RollYearTextBox.LockKeyPress = false;
            this.RollYearTextBox.MaxLength = 4;
            this.RollYearTextBox.Name = "RollYearTextBox";
            this.RollYearTextBox.PersistDefaultColor = false;
            this.RollYearTextBox.Precision = 2;
            this.RollYearTextBox.QueryingFileldName = "";
            this.RollYearTextBox.SetColorFlag = false;
            this.RollYearTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.RollYearTextBox.Size = new System.Drawing.Size(83, 16);
            this.RollYearTextBox.SpecialCharacter = "%";
            this.RollYearTextBox.TabIndex = 8;
            this.RollYearTextBox.TextCustomFormat = "$#,##0.00";
            this.RollYearTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Numeric;
            this.RollYearTextBox.WholeInteger = false;
            this.RollYearTextBox.TextChanged += new System.EventHandler(this.Editext);
            // 
            // RollYeatLabel
            // 
            this.RollYeatLabel.AutoSize = true;
            this.RollYeatLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RollYeatLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RollYeatLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.RollYeatLabel.Location = new System.Drawing.Point(1, 1);
            this.RollYeatLabel.Name = "RollYeatLabel";
            this.RollYeatLabel.Size = new System.Drawing.Size(57, 14);
            this.RollYeatLabel.TabIndex = 62;
            this.RollYeatLabel.Text = "Roll Year:";
            // 
            // RecordCountLabel
            // 
            this.RecordCountLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordCountLabel.Location = new System.Drawing.Point(345, 241);
            this.RecordCountLabel.Name = "RecordCountLabel";
            this.RecordCountLabel.Size = new System.Drawing.Size(197, 15);
            this.RecordCountLabel.TabIndex = 120;
            this.RecordCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DistrictSlectionVerticalScroll
            // 
            this.DistrictSlectionVerticalScroll.Location = new System.Drawing.Point(523, 49);
            this.DistrictSlectionVerticalScroll.Name = "DistrictSlectionVerticalScroll";
            this.DistrictSlectionVerticalScroll.Size = new System.Drawing.Size(16, 132);
            this.DistrictSlectionVerticalScroll.TabIndex = 145;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DistrictSelectionDataGridView);
            this.panel1.Location = new System.Drawing.Point(12, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 134);
            this.panel1.TabIndex = 9;
            this.panel1.TabStop = true;
            // 
            // DistrictSelectionDataGridView
            // 
            this.DistrictSelectionDataGridView.AllowCellClick = true;
            this.DistrictSelectionDataGridView.AllowDoubleClick = true;
            this.DistrictSelectionDataGridView.AllowEmptyRows = true;
            this.DistrictSelectionDataGridView.AllowEnterKey = false;
            this.DistrictSelectionDataGridView.AllowSorting = true;
            this.DistrictSelectionDataGridView.AllowUserToAddRows = false;
            this.DistrictSelectionDataGridView.AllowUserToDeleteRows = false;
            this.DistrictSelectionDataGridView.AllowUserToResizeColumns = false;
            this.DistrictSelectionDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.DistrictSelectionDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DistrictSelectionDataGridView.ApplyStandardBehaviour = false;
            this.DistrictSelectionDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.DistrictSelectionDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DistrictSelectionDataGridView.ClearCurrentCellOnLeave = false;
            this.DistrictSelectionDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DistrictSelectionDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DistrictSelectionDataGridView.ColumnHeadersHeight = 24;
            this.DistrictSelectionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DistrictSelectionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.District,
            this.Description,
            this.RollYear});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DistrictSelectionDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.DistrictSelectionDataGridView.DefaultRowIndex = -1;
            this.DistrictSelectionDataGridView.DeselectCurrentCell = false;
            this.DistrictSelectionDataGridView.DeselectSpecifiedRow = -1;
            this.DistrictSelectionDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DistrictSelectionDataGridView.EnableBinding = true;
            this.DistrictSelectionDataGridView.EnableHeadersVisualStyles = false;
            this.DistrictSelectionDataGridView.GridColor = System.Drawing.Color.Black;
            this.DistrictSelectionDataGridView.GridContentSelected = false;
            this.DistrictSelectionDataGridView.IsEditableGrid = false;
            this.DistrictSelectionDataGridView.IsMultiSelect = false;
            this.DistrictSelectionDataGridView.IsSorted = false;
            this.DistrictSelectionDataGridView.Location = new System.Drawing.Point(-1, -1);
            this.DistrictSelectionDataGridView.MultiSelect = false;
            this.DistrictSelectionDataGridView.Name = "DistrictSelectionDataGridView";
            this.DistrictSelectionDataGridView.NumRowsVisible = 5;
            this.DistrictSelectionDataGridView.PrimaryKeyColumnName = "";
            this.DistrictSelectionDataGridView.RemainSortFields = false;
            this.DistrictSelectionDataGridView.RemoveDefaultSelection = false;
            this.DistrictSelectionDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DistrictSelectionDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.DistrictSelectionDataGridView.RowHeadersWidth = 20;
            this.DistrictSelectionDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DistrictSelectionDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DistrictSelectionDataGridView.Size = new System.Drawing.Size(527, 134);
            this.DistrictSelectionDataGridView.TabIndex = 10;
            this.DistrictSelectionDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DistrictSelectionDataGridView_CellDoubleClick);
            // 
            // DistrictCancelButton
            // 
            this.DistrictCancelButton.ActualPermission = false;
            this.DistrictCancelButton.ApplyDisableBehaviour = false;
            this.DistrictCancelButton.AutoSize = true;
            this.DistrictCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.DistrictCancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.DistrictCancelButton.CommentPriority = false;
            this.DistrictCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.DistrictCancelButton.EnableAutoPrint = false;
            this.DistrictCancelButton.FilterStatus = false;
            this.DistrictCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.DistrictCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DistrictCancelButton.FocusRectangleEnabled = true;
            this.DistrictCancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistrictCancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DistrictCancelButton.ImageSelected = false;
            this.DistrictCancelButton.Location = new System.Drawing.Point(430, 192);
            this.DistrictCancelButton.Name = "DistrictCancelButton";
            this.DistrictCancelButton.NewPadding = 5;
            this.DistrictCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.DistrictCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.DistrictCancelButton.Size = new System.Drawing.Size(110, 30);
            this.DistrictCancelButton.StatusIndicator = false;
            this.DistrictCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DistrictCancelButton.StatusOffText = null;
            this.DistrictCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.DistrictCancelButton.StatusOnText = null;
            this.DistrictCancelButton.TabIndex = 14;
            this.DistrictCancelButton.TabStop = false;
            this.DistrictCancelButton.Text = "Cancel";
            this.DistrictCancelButton.UseVisualStyleBackColor = false;
            this.DistrictCancelButton.Click += new System.EventHandler(this.DistrictCancelButton_Click);
            // 
            // DistrictClearButton
            // 
            this.DistrictClearButton.ActualPermission = false;
            this.DistrictClearButton.ApplyDisableBehaviour = false;
            this.DistrictClearButton.AutoSize = true;
            this.DistrictClearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.DistrictClearButton.BorderColor = System.Drawing.Color.Wheat;
            this.DistrictClearButton.CommentPriority = false;
            this.DistrictClearButton.EnableAutoPrint = false;
            this.DistrictClearButton.FilterStatus = false;
            this.DistrictClearButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.DistrictClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DistrictClearButton.FocusRectangleEnabled = true;
            this.DistrictClearButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistrictClearButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DistrictClearButton.ImageSelected = false;
            this.DistrictClearButton.Location = new System.Drawing.Point(291, 192);
            this.DistrictClearButton.Name = "DistrictClearButton";
            this.DistrictClearButton.NewPadding = 5;
            this.DistrictClearButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.DistrictClearButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.DistrictClearButton.Size = new System.Drawing.Size(110, 30);
            this.DistrictClearButton.StatusIndicator = false;
            this.DistrictClearButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DistrictClearButton.StatusOffText = null;
            this.DistrictClearButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.DistrictClearButton.StatusOnText = null;
            this.DistrictClearButton.TabIndex = 13;
            this.DistrictClearButton.TabStop = false;
            this.DistrictClearButton.Text = "Clear";
            this.DistrictClearButton.UseVisualStyleBackColor = false;
            this.DistrictClearButton.Click += new System.EventHandler(this.DistrictClearButton_Click);
            // 
            // DistrictSearchButton
            // 
            this.DistrictSearchButton.ActualPermission = false;
            this.DistrictSearchButton.ApplyDisableBehaviour = false;
            this.DistrictSearchButton.AutoSize = true;
            this.DistrictSearchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.DistrictSearchButton.BorderColor = System.Drawing.Color.Wheat;
            this.DistrictSearchButton.CommentPriority = false;
            this.DistrictSearchButton.EnableAutoPrint = false;
            this.DistrictSearchButton.FilterStatus = false;
            this.DistrictSearchButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.DistrictSearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DistrictSearchButton.FocusRectangleEnabled = true;
            this.DistrictSearchButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistrictSearchButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DistrictSearchButton.ImageSelected = false;
            this.DistrictSearchButton.Location = new System.Drawing.Point(152, 192);
            this.DistrictSearchButton.Name = "DistrictSearchButton";
            this.DistrictSearchButton.NewPadding = 5;
            this.DistrictSearchButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.DistrictSearchButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.DistrictSearchButton.Size = new System.Drawing.Size(110, 30);
            this.DistrictSearchButton.StatusIndicator = false;
            this.DistrictSearchButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DistrictSearchButton.StatusOffText = null;
            this.DistrictSearchButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.DistrictSearchButton.StatusOnText = null;
            this.DistrictSearchButton.TabIndex = 12;
            this.DistrictSearchButton.TabStop = false;
            this.DistrictSearchButton.Text = "Search";
            this.DistrictSearchButton.UseVisualStyleBackColor = false;
            this.DistrictSearchButton.Click += new System.EventHandler(this.DistrictSearchButton_Click);
            // 
            // DistrictAcceptButton
            // 
            this.DistrictAcceptButton.ActualPermission = false;
            this.DistrictAcceptButton.ApplyDisableBehaviour = false;
            this.DistrictAcceptButton.AutoSize = true;
            this.DistrictAcceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.DistrictAcceptButton.BorderColor = System.Drawing.Color.Wheat;
            this.DistrictAcceptButton.CommentPriority = false;
            this.DistrictAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.DistrictAcceptButton.EnableAutoPrint = false;
            this.DistrictAcceptButton.FilterStatus = false;
            this.DistrictAcceptButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.DistrictAcceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DistrictAcceptButton.FocusRectangleEnabled = true;
            this.DistrictAcceptButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistrictAcceptButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DistrictAcceptButton.ImageSelected = false;
            this.DistrictAcceptButton.Location = new System.Drawing.Point(13, 192);
            this.DistrictAcceptButton.Name = "DistrictAcceptButton";
            this.DistrictAcceptButton.NewPadding = 5;
            this.DistrictAcceptButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.DistrictAcceptButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.DistrictAcceptButton.Size = new System.Drawing.Size(110, 30);
            this.DistrictAcceptButton.StatusIndicator = false;
            this.DistrictAcceptButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DistrictAcceptButton.StatusOffText = null;
            this.DistrictAcceptButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.DistrictAcceptButton.StatusOnText = null;
            this.DistrictAcceptButton.TabIndex = 11;
            this.DistrictAcceptButton.TabStop = false;
            this.DistrictAcceptButton.Text = "Accept";
            this.DistrictAcceptButton.UseVisualStyleBackColor = false;
            this.DistrictAcceptButton.Click += new System.EventHandler(this.DistrictAcceptButton_Click);
            // 
            // DistrictManagementLinkLabel
            // 
            this.DistrictManagementLinkLabel.AutoSize = true;
            this.DistrictManagementLinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistrictManagementLinkLabel.FormDllName = null;
            this.DistrictManagementLinkLabel.FormId = 0;
            this.DistrictManagementLinkLabel.Location = new System.Drawing.Point(184, 241);
            this.DistrictManagementLinkLabel.MenuName = null;
            this.DistrictManagementLinkLabel.Name = "DistrictManagementLinkLabel";
            this.DistrictManagementLinkLabel.PermissionOpen = 0;
            this.DistrictManagementLinkLabel.Size = new System.Drawing.Size(125, 15);
            this.DistrictManagementLinkLabel.TabIndex = 15;
            this.DistrictManagementLinkLabel.TabStop = true;
            this.DistrictManagementLinkLabel.Text = "District Management";
            this.DistrictManagementLinkLabel.TextCustomFormat = "#,##0.00";
            this.DistrictManagementLinkLabel.ValidateType = TerraScan.UI.Controls.TerraScanLinkLabel.ControlValidationType.Text;
            this.DistrictManagementLinkLabel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.DistrictManagementLinkLabel_PreviewKeyDown);
            this.DistrictManagementLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DistrictManagementLinkLabel_LinkClicked);
            // 
            // ID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ID.DefaultCellStyle = dataGridViewCellStyle3;
            this.ID.HeaderText = "   ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ID.Visible = false;
            this.ID.Width = 70;
            // 
            // District
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.District.DefaultCellStyle = dataGridViewCellStyle4;
            this.District.HeaderText = " District";
            this.District.Name = "District";
            this.District.ReadOnly = true;
            this.District.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.District.Width = 91;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Description.Width = 310;
            // 
            // RollYear
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RollYear.DefaultCellStyle = dataGridViewCellStyle5;
            this.RollYear.HeaderText = "   Year";
            this.RollYear.Name = "RollYear";
            this.RollYear.ReadOnly = true;
            this.RollYear.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.RollYear.Width = 90;
            // 
            // F1512
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(554, 263);
            this.Controls.Add(this.RollYearPanel);
            this.Controls.Add(this.DistrictSlectionVerticalScroll);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DistrictCancelButton);
            this.Controls.Add(this.DistrictClearButton);
            this.Controls.Add(this.DistrictSearchButton);
            this.Controls.Add(this.DistrictAcceptButton);
            this.Controls.Add(this.RecordCountLabel);
            this.Controls.Add(this.DistrictManagementLinkLabel);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DescriptionPanel);
            this.Controls.Add(this.DistrictNumberPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(560, 293);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(560, 291);
            this.Name = "F1512";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - District Selection";
            this.Load += new System.EventHandler(this.F1512_Load);
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.DistrictNumberPanel.ResumeLayout(false);
            this.DistrictNumberPanel.PerformLayout();
            this.RollYearPanel.ResumeLayout(false);
            this.RollYearPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DistrictSelectionDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanLinkLabel DistrictManagementLinkLabel;
        private TerraScan.UI.Controls.TerraScanTextBox DistrictNoTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox DescTextBox;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label DistrictNumberLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Panel DescriptionPanel;
        private System.Windows.Forms.Panel DistrictNumberPanel;
        private System.Windows.Forms.Panel RollYearPanel;
        private TerraScan.UI.Controls.TerraScanTextBox RollYearTextBox;
        private System.Windows.Forms.Label RollYeatLabel;
        private System.Windows.Forms.Label RecordCountLabel;
        private TerraScan.UI.Controls.TerraScanButton DistrictCancelButton;
        private TerraScan.UI.Controls.TerraScanButton DistrictClearButton;
        private TerraScan.UI.Controls.TerraScanButton DistrictSearchButton;
        private TerraScan.UI.Controls.TerraScanButton DistrictAcceptButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn DistrictID;
        private System.Windows.Forms.VScrollBar DistrictSlectionVerticalScroll;
        private System.Windows.Forms.Panel panel1;
        private TerraScan.UI.Controls.TerraScanDataGridView DistrictSelectionDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn District;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn RollYear;
    }
}