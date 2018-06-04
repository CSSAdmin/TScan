namespace D20050
{
    partial class F2201
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F2201));
            this.ScheduleSearchGridPanel = new System.Windows.Forms.Panel();
            this.ScheduleSearchDataGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.PersonalPropertyCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecordCountLabel = new System.Windows.Forms.Label();
            this.CodePanel = new System.Windows.Forms.Panel();
            this.CodeTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CodeLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DescriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.CancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.RemoveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ClearButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SearchButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AcceptButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ScheduleSearchGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleSearchDataGridView)).BeginInit();
            this.CodePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScheduleSearchGridPanel
            // 
            this.ScheduleSearchGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScheduleSearchGridPanel.Controls.Add(this.ScheduleSearchDataGridView);
            this.ScheduleSearchGridPanel.Location = new System.Drawing.Point(20, 55);
            this.ScheduleSearchGridPanel.Name = "ScheduleSearchGridPanel";
            this.ScheduleSearchGridPanel.Size = new System.Drawing.Size(631, 134);
            this.ScheduleSearchGridPanel.TabIndex = 5;
            this.ScheduleSearchGridPanel.TabStop = true;
            // 
            // ScheduleSearchDataGridView
            // 
            this.ScheduleSearchDataGridView.AllowCellClick = true;
            this.ScheduleSearchDataGridView.AllowDoubleClick = true;
            this.ScheduleSearchDataGridView.AllowEmptyRows = true;
            this.ScheduleSearchDataGridView.AllowEnterKey = false;
            this.ScheduleSearchDataGridView.AllowSorting = true;
            this.ScheduleSearchDataGridView.AllowUserToAddRows = false;
            this.ScheduleSearchDataGridView.AllowUserToDeleteRows = false;
            this.ScheduleSearchDataGridView.AllowUserToResizeColumns = false;
            this.ScheduleSearchDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.ScheduleSearchDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ScheduleSearchDataGridView.ApplyStandardBehaviour = false;
            this.ScheduleSearchDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.ScheduleSearchDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScheduleSearchDataGridView.ClearCurrentCellOnLeave = false;
            this.ScheduleSearchDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ScheduleSearchDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ScheduleSearchDataGridView.ColumnHeadersHeight = 24;
            this.ScheduleSearchDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ScheduleSearchDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PersonalPropertyCode,
            this.Description});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ScheduleSearchDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ScheduleSearchDataGridView.DefaultRowIndex = -1;
            this.ScheduleSearchDataGridView.DeselectCurrentCell = false;
            this.ScheduleSearchDataGridView.DeselectSpecifiedRow = -1;
            this.ScheduleSearchDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ScheduleSearchDataGridView.EnableBinding = true;
            this.ScheduleSearchDataGridView.EnableHeadersVisualStyles = false;
            this.ScheduleSearchDataGridView.GridColor = System.Drawing.Color.Black;
            this.ScheduleSearchDataGridView.GridContentSelected = false;
            this.ScheduleSearchDataGridView.IsEditableGrid = true;
            this.ScheduleSearchDataGridView.IsMultiSelect = false;
            this.ScheduleSearchDataGridView.IsSorted = true;
            this.ScheduleSearchDataGridView.Location = new System.Drawing.Point(-1, -1);
            this.ScheduleSearchDataGridView.MultiSelect = false;
            this.ScheduleSearchDataGridView.Name = "ScheduleSearchDataGridView";
            this.ScheduleSearchDataGridView.NumRowsVisible = 5;
            this.ScheduleSearchDataGridView.PrimaryKeyColumnName = "";
            this.ScheduleSearchDataGridView.RemainSortFields = false;
            this.ScheduleSearchDataGridView.RemoveDefaultSelection = false;
            this.ScheduleSearchDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ScheduleSearchDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ScheduleSearchDataGridView.RowHeadersWidth = 20;
            this.ScheduleSearchDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ScheduleSearchDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ScheduleSearchDataGridView.Size = new System.Drawing.Size(631, 134);
            this.ScheduleSearchDataGridView.TabIndex = 10;
            this.ScheduleSearchDataGridView.RowHeaderCellChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.ScheduleSearchDataGridView_RowHeaderCellChanged);
            this.ScheduleSearchDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScheduleSearchDataGridView_CellDoubleClick);
            // 
            // PersonalPropertyCode
            // 
            this.PersonalPropertyCode.HeaderText = "Code";
            this.PersonalPropertyCode.Name = "PersonalPropertyCode";
            this.PersonalPropertyCode.ReadOnly = true;
            this.PersonalPropertyCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // RecordCountLabel
            // 
            this.RecordCountLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordCountLabel.Location = new System.Drawing.Point(493, 255);
            this.RecordCountLabel.Name = "RecordCountLabel";
            this.RecordCountLabel.Size = new System.Drawing.Size(157, 15);
            this.RecordCountLabel.TabIndex = 121;
            this.RecordCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CodePanel
            // 
            this.CodePanel.BackColor = System.Drawing.Color.White;
            this.CodePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CodePanel.Controls.Add(this.CodeTextBox);
            this.CodePanel.Controls.Add(this.CodeLabel);
            this.CodePanel.Location = new System.Drawing.Point(20, 18);
            this.CodePanel.Name = "CodePanel";
            this.CodePanel.Size = new System.Drawing.Size(120, 38);
            this.CodePanel.TabIndex = 1;
            this.CodePanel.TabStop = true;
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
            this.CodeTextBox.Location = new System.Drawing.Point(16, 16);
            this.CodeTextBox.LockKeyPress = false;
            this.CodeTextBox.MaxLength = 10;
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.PersistDefaultColor = false;
            this.CodeTextBox.Precision = 2;
            this.CodeTextBox.QueryingFileldName = "";
            this.CodeTextBox.SetColorFlag = false;
            this.CodeTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CodeTextBox.Size = new System.Drawing.Size(97, 16);
            this.CodeTextBox.SpecialCharacter = "%";
            this.CodeTextBox.TabIndex = 2;
            this.CodeTextBox.TextCustomFormat = "$#,##0.00";
            this.CodeTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CodeTextBox.WholeInteger = false;
            this.CodeTextBox.TextChanged += new System.EventHandler(this.CodeTextBox_TextChanged);
            this.CodeTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CodeTextBox_KeyUp);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DescriptionTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(139, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 38);
            this.panel1.TabIndex = 3;
            this.panel1.TabStop = true;
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
            this.DescriptionTextBox.Location = new System.Drawing.Point(16, 16);
            this.DescriptionTextBox.LockKeyPress = false;
            this.DescriptionTextBox.MaxLength = 50;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.PersistDefaultColor = false;
            this.DescriptionTextBox.Precision = 2;
            this.DescriptionTextBox.QueryingFileldName = "";
            this.DescriptionTextBox.SetColorFlag = false;
            this.DescriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescriptionTextBox.Size = new System.Drawing.Size(472, 16);
            this.DescriptionTextBox.SpecialCharacter = "%";
            this.DescriptionTextBox.TabIndex = 4;
            this.DescriptionTextBox.TextCustomFormat = "$#,##0.00";
            this.DescriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescriptionTextBox.WholeInteger = false;
            this.DescriptionTextBox.TextChanged += new System.EventHandler(this.DescriptionTextBox_TextChanged);
            this.DescriptionTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CodeTextBox_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 14);
            this.label1.TabIndex = 62;
            this.label1.Text = "Description:";
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(20, 252);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(630, 2);
            this.FormLinePanel.TabIndex = 178;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(20, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "2201";
            // 
            // CancelButton
            // 
            this.CancelButton.ActualPermission = false;
            this.CancelButton.ApplyDisableBehaviour = false;
            this.CancelButton.AutoSize = true;
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelButton.CommentPriority = false;
            this.CancelButton.EnableAutoPrint = false;
            this.CancelButton.FilterStatus = false;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.FocusRectangleEnabled = true;
            this.CancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelButton.ImageSelected = false;
            this.CancelButton.Location = new System.Drawing.Point(552, 208);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NewPadding = 5;
            this.CancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.CancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelButton.Size = new System.Drawing.Size(98, 28);
            this.CancelButton.StatusIndicator = false;
            this.CancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelButton.StatusOffText = null;
            this.CancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelButton.StatusOnText = null;
            this.CancelButton.TabIndex = 9;
            this.CancelButton.TabStop = false;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.ActualPermission = false;
            this.RemoveButton.ApplyDisableBehaviour = false;
            this.RemoveButton.AutoSize = true;
            this.RemoveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.RemoveButton.BorderColor = System.Drawing.Color.Wheat;
            this.RemoveButton.CommentPriority = false;
            this.RemoveButton.EnableAutoPrint = false;
            this.RemoveButton.FilterStatus = false;
            this.RemoveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveButton.FocusRectangleEnabled = true;
            this.RemoveButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.RemoveButton.ImageSelected = false;
            this.RemoveButton.Location = new System.Drawing.Point(421, 208);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.NewPadding = 5;
            this.RemoveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.RemoveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.RemoveButton.Size = new System.Drawing.Size(98, 28);
            this.RemoveButton.StatusIndicator = false;
            this.RemoveButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RemoveButton.StatusOffText = null;
            this.RemoveButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.RemoveButton.StatusOnText = null;
            this.RemoveButton.TabIndex = 8;
            this.RemoveButton.TabStop = false;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = false;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
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
            this.ClearButton.Location = new System.Drawing.Point(287, 208);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.NewPadding = 5;
            this.ClearButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.ClearButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ClearButton.Size = new System.Drawing.Size(98, 28);
            this.ClearButton.StatusIndicator = false;
            this.ClearButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClearButton.StatusOffText = null;
            this.ClearButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ClearButton.StatusOnText = null;
            this.ClearButton.TabIndex = 7;
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
            this.SearchButton.Location = new System.Drawing.Point(153, 208);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.NewPadding = 5;
            this.SearchButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.SearchButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SearchButton.Size = new System.Drawing.Size(98, 28);
            this.SearchButton.StatusIndicator = false;
            this.SearchButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SearchButton.StatusOffText = null;
            this.SearchButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SearchButton.StatusOnText = null;
            this.SearchButton.TabIndex = 6;
            this.SearchButton.TabStop = false;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // AcceptButton
            // 
            this.AcceptButton.ActualPermission = false;
            this.AcceptButton.ApplyDisableBehaviour = false;
            this.AcceptButton.AutoSize = true;
            this.AcceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AcceptButton.BorderColor = System.Drawing.Color.Wheat;
            this.AcceptButton.CommentPriority = false;
            this.AcceptButton.EnableAutoPrint = false;
            this.AcceptButton.FilterStatus = false;
            this.AcceptButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AcceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AcceptButton.FocusRectangleEnabled = true;
            this.AcceptButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AcceptButton.ImageSelected = false;
            this.AcceptButton.Location = new System.Drawing.Point(20, 208);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.NewPadding = 5;
            this.AcceptButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.AcceptButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AcceptButton.Size = new System.Drawing.Size(98, 28);
            this.AcceptButton.StatusIndicator = false;
            this.AcceptButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AcceptButton.StatusOffText = null;
            this.AcceptButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AcceptButton.StatusOnText = null;
            this.AcceptButton.TabIndex = 5;
            this.AcceptButton.TabStop = false;
            this.AcceptButton.Text = "Accept";
            this.AcceptButton.UseVisualStyleBackColor = false;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // F2201
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(676, 272);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.AcceptButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CodePanel);
            this.Controls.Add(this.ScheduleSearchGridPanel);
            this.Controls.Add(this.RecordCountLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F2201";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "2201";
            this.Text = "TerraScan T2 - Personal Property Code Search";
            this.Load += new System.EventHandler(this.F2201_Load);
            this.ScheduleSearchGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleSearchDataGridView)).EndInit();
            this.CodePanel.ResumeLayout(false);
            this.CodePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ScheduleSearchGridPanel;
        private TerraScan.UI.Controls.TerraScanDataGridView ScheduleSearchDataGridView;
        private System.Windows.Forms.Panel CodePanel;
        private TerraScan.UI.Controls.TerraScanTextBox CodeTextBox;
        private System.Windows.Forms.Label CodeLabel;
        private System.Windows.Forms.Panel panel1;
        private TerraScan.UI.Controls.TerraScanTextBox DescriptionTextBox;
        private System.Windows.Forms.Label label1;
        private TerraScan.UI.Controls.TerraScanButton AcceptButton;
        private TerraScan.UI.Controls.TerraScanButton SearchButton;
        private TerraScan.UI.Controls.TerraScanButton ClearButton;
        private TerraScan.UI.Controls.TerraScanButton RemoveButton;
        private TerraScan.UI.Controls.TerraScanButton CancelButton;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label RecordCountLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PersonalPropertyCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}