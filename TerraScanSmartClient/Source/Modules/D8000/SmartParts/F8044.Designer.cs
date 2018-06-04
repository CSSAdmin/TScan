namespace D8000
{
    partial class F8044
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8044));
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.QntyPanel = new System.Windows.Forms.Panel();
            this.QntyTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.UserPanel = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.UserComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.CommentPanel = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.CommentTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.AddButton = new TerraScan.UI.Controls.TerraScanButton();
            this.PartPanel = new System.Windows.Forms.Panel();
            this.PartComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.InspectionDetailsGridVscrollBar = new System.Windows.Forms.VScrollBar();
            this.MaterialsGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.User = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Part = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Qnty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DistrictInfoSecIndicatorPictureBox = new System.Windows.Forms.PictureBox();
            this.InspectionDetailsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.HeaderPanel.SuspendLayout();
            this.QntyPanel.SuspendLayout();
            this.UserPanel.SuspendLayout();
            this.CommentPanel.SuspendLayout();
            this.PartPanel.SuspendLayout();
            this.GridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialsGridView)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DistrictInfoSecIndicatorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Controls.Add(this.QntyPanel);
            this.HeaderPanel.Controls.Add(this.UserPanel);
            this.HeaderPanel.Controls.Add(this.CommentPanel);
            this.HeaderPanel.Controls.Add(this.PartPanel);
            this.HeaderPanel.Location = new System.Drawing.Point(-1, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(769, 25);
            this.HeaderPanel.TabIndex = 1;
            // 
            // QntyPanel
            // 
            this.QntyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.QntyPanel.Controls.Add(this.QntyTextBox);
            this.QntyPanel.Location = new System.Drawing.Point(357, 0);
            this.QntyPanel.Name = "QntyPanel";
            this.QntyPanel.Size = new System.Drawing.Size(94, 25);
            this.QntyPanel.TabIndex = 3;
            // 
            // QntyTextBox
            // 
            this.QntyTextBox.AllowClick = true;
            this.QntyTextBox.AllowNegativeSign = false;
            this.QntyTextBox.ApplyCFGFormat = false;
            this.QntyTextBox.ApplyCurrencyFormat = false;
            this.QntyTextBox.ApplyFocusColor = true;
            this.QntyTextBox.ApplyNegativeStandard = true;
            this.QntyTextBox.ApplyParentFocusColor = true;
            this.QntyTextBox.ApplyTimeFormat = false;
            this.QntyTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.QntyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.QntyTextBox.CFromatWihoutSymbol = false;
            this.QntyTextBox.CheckForEmpty = false;
            this.QntyTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.QntyTextBox.Digits = -1;
            this.QntyTextBox.EmptyDecimalValue = false;
            this.QntyTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.QntyTextBox.ForeColor = System.Drawing.Color.Black;
            this.QntyTextBox.IsEditable = true;
            this.QntyTextBox.IsQueryableFileld = true;
            this.QntyTextBox.Location = new System.Drawing.Point(4, 4);
            this.QntyTextBox.LockKeyPress = false;
            this.QntyTextBox.MaxLength = 3;
            this.QntyTextBox.Name = "QntyTextBox";
            this.QntyTextBox.PersistDefaultColor = false;
            this.QntyTextBox.Precision = 2;
            this.QntyTextBox.QueryingFileldName = "";
            this.QntyTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.QntyTextBox.Size = new System.Drawing.Size(84, 16);
            this.QntyTextBox.SpecialCharacter = "%";
            this.QntyTextBox.TabIndex = 1;
            this.QntyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.QntyTextBox.TextCustomFormat = "$#,##0.00";
            this.QntyTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Numeric;
            this.QntyTextBox.WholeInteger = false;
            this.QntyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // UserPanel
            // 
            this.UserPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserPanel.Controls.Add(this.panel6);
            this.UserPanel.Controls.Add(this.UserComboBox);
            this.UserPanel.Location = new System.Drawing.Point(1, 0);
            this.UserPanel.Name = "UserPanel";
            this.UserPanel.Size = new System.Drawing.Size(146, 25);
            this.UserPanel.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Silver;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(-1, -1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(20, 25);
            this.panel6.TabIndex = 112;
            // 
            // UserComboBox
            // 
            this.UserComboBox.BackColor = System.Drawing.Color.White;
            this.UserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UserComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UserComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserComboBox.FormattingEnabled = true;
            this.UserComboBox.Location = new System.Drawing.Point(23, 1);
            this.UserComboBox.Name = "UserComboBox";
            this.UserComboBox.Size = new System.Drawing.Size(120, 24);
            this.UserComboBox.TabIndex = 1;
            this.UserComboBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserComboBox_MouseMove);
            this.UserComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // CommentPanel
            // 
            this.CommentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CommentPanel.Controls.Add(this.panel5);
            this.CommentPanel.Controls.Add(this.CommentTextBox);
            this.CommentPanel.Controls.Add(this.AddButton);
            this.CommentPanel.Location = new System.Drawing.Point(450, 0);
            this.CommentPanel.Name = "CommentPanel";
            this.CommentPanel.Size = new System.Drawing.Size(319, 25);
            this.CommentPanel.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Silver;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(298, -1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(20, 25);
            this.panel5.TabIndex = 119;
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.AllowClick = true;
            this.CommentTextBox.AllowNegativeSign = false;
            this.CommentTextBox.ApplyCFGFormat = false;
            this.CommentTextBox.ApplyCurrencyFormat = false;
            this.CommentTextBox.ApplyFocusColor = true;
            this.CommentTextBox.ApplyNegativeStandard = true;
            this.CommentTextBox.ApplyParentFocusColor = true;
            this.CommentTextBox.ApplyTimeFormat = false;
            this.CommentTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.CommentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommentTextBox.CFromatWihoutSymbol = false;
            this.CommentTextBox.CheckForEmpty = false;
            this.CommentTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CommentTextBox.Digits = -1;
            this.CommentTextBox.EmptyDecimalValue = false;
            this.CommentTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.CommentTextBox.ForeColor = System.Drawing.Color.Black;
            this.CommentTextBox.IsEditable = true;
            this.CommentTextBox.IsQueryableFileld = true;
            this.CommentTextBox.Location = new System.Drawing.Point(4, 4);
            this.CommentTextBox.LockKeyPress = false;
            this.CommentTextBox.MaxLength = 500;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.PersistDefaultColor = false;
            this.CommentTextBox.Precision = 2;
            this.CommentTextBox.QueryingFileldName = "";
            this.CommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CommentTextBox.Size = new System.Drawing.Size(226, 16);
            this.CommentTextBox.SpecialCharacter = "%";
            this.CommentTextBox.TabIndex = 1;
            this.CommentTextBox.TextCustomFormat = "$#,##0.00";
            this.CommentTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CommentTextBox.WholeInteger = false;
            this.CommentTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // AddButton
            // 
            this.AddButton.ActualPermission = false;
            this.AddButton.ApplyDisableBehaviour = true;
            this.AddButton.AutoEllipsis = true;
            this.AddButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(102)))));
            this.AddButton.BorderColor = System.Drawing.Color.Wheat;
            this.AddButton.CommentPriority = false;
            this.AddButton.EnableAutoPrint = false;
            this.AddButton.FilterStatus = false;
            this.AddButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddButton.FocusRectangleEnabled = true;
            this.AddButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AddButton.ImageSelected = false;
            this.AddButton.Location = new System.Drawing.Point(236, -1);
            this.AddButton.Name = "AddButton";
            this.AddButton.NewPadding = 5;
            this.AddButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.AddButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.None;
            this.AddButton.Size = new System.Drawing.Size(63, 25);
            this.AddButton.StatusIndicator = false;
            this.AddButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AddButton.StatusOffText = null;
            this.AddButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AddButton.StatusOnText = null;
            this.AddButton.TabIndex = 5;
            this.AddButton.TabStop = false;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // PartPanel
            // 
            this.PartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PartPanel.Controls.Add(this.PartComboBox);
            this.PartPanel.Location = new System.Drawing.Point(146, 0);
            this.PartPanel.Name = "PartPanel";
            this.PartPanel.Size = new System.Drawing.Size(212, 25);
            this.PartPanel.TabIndex = 2;
            // 
            // PartComboBox
            // 
            this.PartComboBox.BackColor = System.Drawing.Color.White;
            this.PartComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PartComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PartComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartComboBox.FormattingEnabled = true;
            this.PartComboBox.Location = new System.Drawing.Point(3, 1);
            this.PartComboBox.Name = "PartComboBox";
            this.PartComboBox.Size = new System.Drawing.Size(206, 24);
            this.PartComboBox.TabIndex = 1;
            this.PartComboBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserComboBox_MouseMove);
            this.PartComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // GridPanel
            // 
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.InspectionDetailsGridVscrollBar);
            this.GridPanel.Controls.Add(this.MaterialsGridView);
            this.GridPanel.Location = new System.Drawing.Point(0, 24);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(768, 109);
            this.GridPanel.TabIndex = 6;
            this.GridPanel.TabStop = true;
            // 
            // InspectionDetailsGridVscrollBar
            // 
            this.InspectionDetailsGridVscrollBar.Enabled = false;
            this.InspectionDetailsGridVscrollBar.Location = new System.Drawing.Point(749, 0);
            this.InspectionDetailsGridVscrollBar.Name = "InspectionDetailsGridVscrollBar";
            this.InspectionDetailsGridVscrollBar.Size = new System.Drawing.Size(16, 106);
            this.InspectionDetailsGridVscrollBar.TabIndex = 1004;
            // 
            // MaterialsGridView
            // 
            this.MaterialsGridView.AllowCellClick = true;
            this.MaterialsGridView.AllowDoubleClick = true;
            this.MaterialsGridView.AllowEmptyRows = true;
            this.MaterialsGridView.AllowEnterKey = false;
            this.MaterialsGridView.AllowSorting = true;
            this.MaterialsGridView.AllowUserToAddRows = false;
            this.MaterialsGridView.AllowUserToDeleteRows = false;
            this.MaterialsGridView.AllowUserToResizeColumns = false;
            this.MaterialsGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.MaterialsGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.MaterialsGridView.ApplyStandardBehaviour = false;
            this.MaterialsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.MaterialsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MaterialsGridView.ClearCurrentCellOnLeave = false;
            this.MaterialsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MaterialsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MaterialsGridView.ColumnHeadersHeight = 21;
            this.MaterialsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.MaterialsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.User,
            this.Part,
            this.Qnty,
            this.Comment,
            this.MaterialID,
            this.EventID,
            this.WOID});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MaterialsGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.MaterialsGridView.DefaultRowIndex = -1;
            this.MaterialsGridView.DeselectCurrentCell = false;
            this.MaterialsGridView.DeselectSpecifiedRow = -1;
            this.MaterialsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.MaterialsGridView.EnableBinding = true;
            this.MaterialsGridView.EnableHeadersVisualStyles = false;
            this.MaterialsGridView.GridColor = System.Drawing.Color.Black;
            this.MaterialsGridView.GridContentSelected = false;
            this.MaterialsGridView.IsEditableGrid = true;
            this.MaterialsGridView.IsSorted = false;
            this.MaterialsGridView.Location = new System.Drawing.Point(-1, -1);
            this.MaterialsGridView.MultiSelect = false;
            this.MaterialsGridView.Name = "MaterialsGridView";
            this.MaterialsGridView.NumRowsVisible = 4;
            this.MaterialsGridView.PrimaryKeyColumnName = "";
            this.MaterialsGridView.RemainSortFields = true;
            this.MaterialsGridView.RemoveDefaultSelection = false;
            this.MaterialsGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MaterialsGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.MaterialsGridView.RowHeadersWidth = 20;
            this.MaterialsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.MaterialsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MaterialsGridView.Size = new System.Drawing.Size(766, 109);
            this.MaterialsGridView.StandardTab = true;
            this.MaterialsGridView.TabIndex = 1;
            this.MaterialsGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.MaterialsGridView_CellLeave);
            this.MaterialsGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaterialsGridView_KeyDown);
            this.MaterialsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MaterialsGridView_CellClick);
            this.MaterialsGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.MaterialsGridView_DataBindingComplete);
            this.MaterialsGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.MaterialsGridView_CellParsing);
            this.MaterialsGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.MaterialsGridView_CellEndEdit);
            this.MaterialsGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.MaterialsGridView_CellValueChanged);
            this.MaterialsGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.MaterialsGridView_DataError);
            // 
            // User
            // 
            this.User.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.User.HeaderText = "User";
            this.User.Name = "User";
            this.User.Width = 126;
            // 
            // Part
            // 
            this.Part.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Part.HeaderText = "Part";
            this.Part.Name = "Part";
            this.Part.Width = 211;
            // 
            // Qnty
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qnty.DefaultCellStyle = dataGridViewCellStyle3;
            this.Qnty.HeaderText = "Qnty";
            this.Qnty.MaxInputLength = 3;
            this.Qnty.Name = "Qnty";
            this.Qnty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Qnty.Width = 93;
            // 
            // Comment
            // 
            this.Comment.DataPropertyName = "Comment";
            this.Comment.HeaderText = "Comment";
            this.Comment.MaxInputLength = 500;
            this.Comment.Name = "Comment";
            this.Comment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Comment.Width = 299;
            // 
            // MaterialID
            // 
            this.MaterialID.HeaderText = "MaterialID";
            this.MaterialID.Name = "MaterialID";
            this.MaterialID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MaterialID.Visible = false;
            // 
            // EventID
            // 
            this.EventID.HeaderText = "EventID";
            this.EventID.Name = "EventID";
            this.EventID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EventID.Visible = false;
            // 
            // WOID
            // 
            this.WOID.HeaderText = "WOID";
            this.WOID.Name = "WOID";
            this.WOID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WOID.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Location = new System.Drawing.Point(19, 132);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(749, 21);
            this.panel4.TabIndex = 115;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(728, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(20, 25);
            this.panel3.TabIndex = 119;
            // 
            // DistrictInfoSecIndicatorPictureBox
            // 
            this.DistrictInfoSecIndicatorPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("DistrictInfoSecIndicatorPictureBox.Image")));
            this.DistrictInfoSecIndicatorPictureBox.Location = new System.Drawing.Point(761, 0);
            this.DistrictInfoSecIndicatorPictureBox.Name = "DistrictInfoSecIndicatorPictureBox";
            this.DistrictInfoSecIndicatorPictureBox.Size = new System.Drawing.Size(42, 153);
            this.DistrictInfoSecIndicatorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DistrictInfoSecIndicatorPictureBox.TabIndex = 117;
            this.DistrictInfoSecIndicatorPictureBox.TabStop = false;
            this.DistrictInfoSecIndicatorPictureBox.Click += new System.EventHandler(this.DistrictInfoSecIndicatorPictureBox_Click);
            this.DistrictInfoSecIndicatorPictureBox.MouseEnter += new System.EventHandler(this.DistrictInfoSecIndicatorPictureBox_MouseEnter);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 132);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 21);
            this.panel1.TabIndex = 118;
            // 
            // F8044
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.HeaderPanel);
            this.Controls.Add(this.DistrictInfoSecIndicatorPictureBox);
            this.Name = "F8044";
            this.ParentFormId = 8044;
            this.Size = new System.Drawing.Size(804, 153);
            this.Tag = "8044";
            this.Load += new System.EventHandler(this.F8044_Load);
            this.HeaderPanel.ResumeLayout(false);
            this.QntyPanel.ResumeLayout(false);
            this.QntyPanel.PerformLayout();
            this.UserPanel.ResumeLayout(false);
            this.CommentPanel.ResumeLayout(false);
            this.CommentPanel.PerformLayout();
            this.PartPanel.ResumeLayout(false);
            this.GridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MaterialsGridView)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DistrictInfoSecIndicatorPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Panel QntyPanel;
        private System.Windows.Forms.Panel UserPanel;
        private System.Windows.Forms.Panel panel6;
        private TerraScan.UI.Controls.TerraScanComboBox UserComboBox;
        private System.Windows.Forms.Panel CommentPanel;
        private TerraScan.UI.Controls.TerraScanTextBox CommentTextBox;
        private TerraScan.UI.Controls.TerraScanButton AddButton;
        private System.Windows.Forms.Panel PartPanel;
        private TerraScan.UI.Controls.TerraScanComboBox PartComboBox;
        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.VScrollBar InspectionDetailsGridVscrollBar;
        private TerraScan.UI.Controls.TerraScanDataGridView MaterialsGridView;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox DistrictInfoSecIndicatorPictureBox;
        private TerraScan.UI.Controls.TerraScanTextBox QntyTextBox;
        private System.Windows.Forms.ToolTip InspectionDetailsToolTip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewComboBoxColumn User;
        private System.Windows.Forms.DataGridViewComboBoxColumn Part;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qnty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WOID;
    }
}
