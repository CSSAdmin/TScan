namespace D8056
{
    partial class F8056
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8056));
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ActionTypeComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ComponentTypeComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.CommentPanel = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.CommentTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.AddButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DetailPanel = new System.Windows.Forms.Panel();
            this.ConditionTypeComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.InspectionDetailsGridVscrollBar = new System.Windows.Forms.VScrollBar();
            this.InspectionDetailsGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.Component = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Condition = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InspectionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DistrictInfoSecIndicatorPictureBox = new System.Windows.Forms.PictureBox();
            this.InspectionDetailsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.HeaderPanel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.CommentPanel.SuspendLayout();
            this.DetailPanel.SuspendLayout();
            this.GridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InspectionDetailsGridView)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DistrictInfoSecIndicatorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Controls.Add(this.panel5);
            this.HeaderPanel.Controls.Add(this.panel3);
            this.HeaderPanel.Controls.Add(this.CommentPanel);
            this.HeaderPanel.Controls.Add(this.DetailPanel);
            this.HeaderPanel.Location = new System.Drawing.Point(-1, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(769, 25);
            this.HeaderPanel.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.ActionTypeComboBox);
            this.panel5.Location = new System.Drawing.Point(307, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(144, 25);
            this.panel5.TabIndex = 3;
            // 
            // ActionTypeComboBox
            // 
            this.ActionTypeComboBox.BackColor = System.Drawing.Color.White;
            this.ActionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionTypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ActionTypeComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActionTypeComboBox.ForeColor = System.Drawing.Color.Black;
            this.ActionTypeComboBox.FormattingEnabled = true;
            this.ActionTypeComboBox.Location = new System.Drawing.Point(3, 1);
            this.ActionTypeComboBox.Name = "ActionTypeComboBox";
            this.ActionTypeComboBox.Size = new System.Drawing.Size(138, 24);
            this.ActionTypeComboBox.TabIndex = 1005;
            this.ActionTypeComboBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ActionTypeComboBox_MouseMove);
            this.ActionTypeComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.ComponentTypeComboBox);
            this.panel3.Location = new System.Drawing.Point(1, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(164, 25);
            this.panel3.TabIndex = 1;
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
            // ComponentTypeComboBox
            // 
            this.ComponentTypeComboBox.BackColor = System.Drawing.Color.White;
            this.ComponentTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComponentTypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComponentTypeComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComponentTypeComboBox.FormattingEnabled = true;
            this.ComponentTypeComboBox.Location = new System.Drawing.Point(23, 1);
            this.ComponentTypeComboBox.Name = "ComponentTypeComboBox";
            this.ComponentTypeComboBox.Size = new System.Drawing.Size(138, 24);
            this.ComponentTypeComboBox.TabIndex = 1;
            this.ComponentTypeComboBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ComboToolTip);
            this.ComponentTypeComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // CommentPanel
            // 
            this.CommentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CommentPanel.Controls.Add(this.panel7);
            this.CommentPanel.Controls.Add(this.CommentTextBox);
            this.CommentPanel.Controls.Add(this.AddButton);
            this.CommentPanel.Location = new System.Drawing.Point(450, 0);
            this.CommentPanel.Name = "CommentPanel";
            this.CommentPanel.Size = new System.Drawing.Size(319, 25);
            this.CommentPanel.TabIndex = 4;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Silver;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Location = new System.Drawing.Point(298, -1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(20, 25);
            this.panel7.TabIndex = 115;
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
            this.CommentTextBox.MaxLength = 100;
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
            this.AddButton.TabIndex = 4;
            this.AddButton.TabStop = false;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DetailPanel
            // 
            this.DetailPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DetailPanel.Controls.Add(this.ConditionTypeComboBox);
            this.DetailPanel.Location = new System.Drawing.Point(164, 0);
            this.DetailPanel.Name = "DetailPanel";
            this.DetailPanel.Size = new System.Drawing.Size(144, 25);
            this.DetailPanel.TabIndex = 2;
            // 
            // ConditionTypeComboBox
            // 
            this.ConditionTypeComboBox.BackColor = System.Drawing.Color.White;
            this.ConditionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConditionTypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConditionTypeComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConditionTypeComboBox.ForeColor = System.Drawing.Color.Black;
            this.ConditionTypeComboBox.FormattingEnabled = true;
            this.ConditionTypeComboBox.Location = new System.Drawing.Point(3, 1);
            this.ConditionTypeComboBox.Name = "ConditionTypeComboBox";
            this.ConditionTypeComboBox.Size = new System.Drawing.Size(138, 24);
            this.ConditionTypeComboBox.TabIndex = 1;
            this.ConditionTypeComboBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ConditionTypeComboBox_MouseMove);
            this.ConditionTypeComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // GridPanel
            // 
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.InspectionDetailsGridVscrollBar);
            this.GridPanel.Controls.Add(this.InspectionDetailsGridView);
            this.GridPanel.Location = new System.Drawing.Point(0, 24);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(768, 197);
            this.GridPanel.TabIndex = 105;
            this.GridPanel.TabStop = true;
            // 
            // InspectionDetailsGridVscrollBar
            // 
            this.InspectionDetailsGridVscrollBar.Enabled = false;
            this.InspectionDetailsGridVscrollBar.Location = new System.Drawing.Point(749, 0);
            this.InspectionDetailsGridVscrollBar.Name = "InspectionDetailsGridVscrollBar";
            this.InspectionDetailsGridVscrollBar.Size = new System.Drawing.Size(16, 194);
            this.InspectionDetailsGridVscrollBar.TabIndex = 1004;
            // 
            // InspectionDetailsGridView
            // 
            this.InspectionDetailsGridView.AllowCellClick = true;
            this.InspectionDetailsGridView.AllowDoubleClick = true;
            this.InspectionDetailsGridView.AllowEmptyRows = true;
            this.InspectionDetailsGridView.AllowEnterKey = false;
            this.InspectionDetailsGridView.AllowSorting = true;
            this.InspectionDetailsGridView.AllowUserToAddRows = false;
            this.InspectionDetailsGridView.AllowUserToDeleteRows = false;
            this.InspectionDetailsGridView.AllowUserToResizeColumns = false;
            this.InspectionDetailsGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.InspectionDetailsGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.InspectionDetailsGridView.ApplyStandardBehaviour = false;
            this.InspectionDetailsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.InspectionDetailsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InspectionDetailsGridView.ClearCurrentCellOnLeave = false;
            this.InspectionDetailsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InspectionDetailsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.InspectionDetailsGridView.ColumnHeadersHeight = 21;
            this.InspectionDetailsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.InspectionDetailsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Component,
            this.Condition,
            this.Action,
            this.Comment,
            this.InspectionID,
            this.EventId});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.InspectionDetailsGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.InspectionDetailsGridView.DefaultRowIndex = -1;
            this.InspectionDetailsGridView.DeselectCurrentCell = false;
            this.InspectionDetailsGridView.DeselectSpecifiedRow = -1;
            this.InspectionDetailsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.InspectionDetailsGridView.EnableBinding = true;
            this.InspectionDetailsGridView.EnableHeadersVisualStyles = false;
            this.InspectionDetailsGridView.GridColor = System.Drawing.Color.Black;
            this.InspectionDetailsGridView.GridContentSelected = false;
            this.InspectionDetailsGridView.IsEditableGrid = true;
            this.InspectionDetailsGridView.IsSorted = false;
            this.InspectionDetailsGridView.Location = new System.Drawing.Point(-1, -1);
            this.InspectionDetailsGridView.MultiSelect = false;
            this.InspectionDetailsGridView.Name = "InspectionDetailsGridView";
            this.InspectionDetailsGridView.NumRowsVisible = 8;
            this.InspectionDetailsGridView.PrimaryKeyColumnName = "";
            this.InspectionDetailsGridView.RemainSortFields = true;
            this.InspectionDetailsGridView.RemoveDefaultSelection = false;
            this.InspectionDetailsGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InspectionDetailsGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.InspectionDetailsGridView.RowHeadersWidth = 20;
            this.InspectionDetailsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.InspectionDetailsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InspectionDetailsGridView.Size = new System.Drawing.Size(766, 197);
            this.InspectionDetailsGridView.StandardTab = true;
            this.InspectionDetailsGridView.TabIndex = 4;
            this.InspectionDetailsGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InspectionDetailsGridView_KeyDown);
            this.InspectionDetailsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InspectionDetailsGridView_CellClick);
            this.InspectionDetailsGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.InspectionDetailsGridView_DataBindingComplete);
            this.InspectionDetailsGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.InspectionDetailsGridView_CellValueChanged);
            // 
            // Component
            // 
            this.Component.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Component.HeaderText = "Component";
            this.Component.Name = "Component";
            this.Component.Width = 144;
            // 
            // Condition
            // 
            this.Condition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Condition.HeaderText = "Condition";
            this.Condition.Name = "Condition";
            this.Condition.Width = 143;
            // 
            // Action
            // 
            this.Action.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Action.HeaderText = "Action";
            this.Action.Name = "Action";
            this.Action.Width = 143;
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
            // InspectionID
            // 
            this.InspectionID.HeaderText = "InspectionID";
            this.InspectionID.Name = "InspectionID";
            this.InspectionID.Visible = false;
            // 
            // EventId
            // 
            this.EventId.HeaderText = "EventId";
            this.EventId.Name = "EventId";
            this.EventId.ReadOnly = true;
            this.EventId.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(0, 220);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(20, 21);
            this.panel2.TabIndex = 114;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Location = new System.Drawing.Point(19, 220);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(749, 21);
            this.panel4.TabIndex = 113;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(728, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 21);
            this.panel1.TabIndex = 115;
            // 
            // DistrictInfoSecIndicatorPictureBox
            // 
            this.DistrictInfoSecIndicatorPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("DistrictInfoSecIndicatorPictureBox.Image")));
            this.DistrictInfoSecIndicatorPictureBox.Location = new System.Drawing.Point(761, 0);
            this.DistrictInfoSecIndicatorPictureBox.Name = "DistrictInfoSecIndicatorPictureBox";
            this.DistrictInfoSecIndicatorPictureBox.Size = new System.Drawing.Size(42, 240);
            this.DistrictInfoSecIndicatorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DistrictInfoSecIndicatorPictureBox.TabIndex = 115;
            this.DistrictInfoSecIndicatorPictureBox.TabStop = false;
            this.DistrictInfoSecIndicatorPictureBox.Click += new System.EventHandler(this.DistrictInfoSecIndicatorPictureBox_Click);
            this.DistrictInfoSecIndicatorPictureBox.MouseEnter += new System.EventHandler(this.DistrictInfoSecIndicatorPictureBox_MouseEnter);
            // 
            // F8056
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.HeaderPanel);
            this.Controls.Add(this.DistrictInfoSecIndicatorPictureBox);
            this.Name = "F8056";
            this.ParentFormId = 8056;
            this.Size = new System.Drawing.Size(804, 241);
            this.Tag = "";
            this.Load += new System.EventHandler(this.F8056_Load);
            this.HeaderPanel.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.CommentPanel.ResumeLayout(false);
            this.CommentPanel.PerformLayout();
            this.DetailPanel.ResumeLayout(false);
            this.GridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InspectionDetailsGridView)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DistrictInfoSecIndicatorPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.VScrollBar InspectionDetailsGridVscrollBar;
        private TerraScan.UI.Controls.TerraScanDataGridView InspectionDetailsGridView;
        private System.Windows.Forms.Panel CommentPanel;
        private TerraScan.UI.Controls.TerraScanTextBox CommentTextBox;
        private TerraScan.UI.Controls.TerraScanButton AddButton;
        private System.Windows.Forms.Panel DetailPanel;
        private TerraScan.UI.Controls.TerraScanComboBox ConditionTypeComboBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private TerraScan.UI.Controls.TerraScanComboBox ComponentTypeComboBox;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox DistrictInfoSecIndicatorPictureBox;
        private System.Windows.Forms.ToolTip InspectionDetailsToolTip;
        private System.Windows.Forms.DataGridViewComboBoxColumn Component;
        private System.Windows.Forms.DataGridViewComboBoxColumn Condition;
        private System.Windows.Forms.DataGridViewComboBoxColumn Action;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn InspectionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventId;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel1;
        private TerraScan.UI.Controls.TerraScanComboBox ActionTypeComboBox;
    }
}
