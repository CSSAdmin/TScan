namespace D8102
{
    partial class F8104
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8104));
            this.GridPanel = new System.Windows.Forms.Panel();
            this.TvDetailsGridVscrollBar = new System.Windows.Forms.VScrollBar();
            this.TVDetailsGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detail = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LengthPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LengthTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DetailPanel = new System.Windows.Forms.Panel();
            this.DetailTypeComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.CommentPanel = new System.Windows.Forms.Panel();
            this.CommentTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.AddButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DistrictInfoSecIndicatorPictureBox = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SanitaryPipeInspToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.GridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TVDetailsGridView)).BeginInit();
            this.LengthPanel.SuspendLayout();
            this.DetailPanel.SuspendLayout();
            this.CommentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DistrictInfoSecIndicatorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GridPanel
            // 
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.TvDetailsGridVscrollBar);
            this.GridPanel.Controls.Add(this.TVDetailsGridView);
            this.GridPanel.Location = new System.Drawing.Point(0, 24);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(768, 197);
            this.GridPanel.TabIndex = 5;
            this.GridPanel.TabStop = true;
            // 
            // TvDetailsGridVscrollBar
            // 
            this.TvDetailsGridVscrollBar.Enabled = false;
            this.TvDetailsGridVscrollBar.Location = new System.Drawing.Point(749, 0);
            this.TvDetailsGridVscrollBar.Name = "TvDetailsGridVscrollBar";
            this.TvDetailsGridVscrollBar.Size = new System.Drawing.Size(16, 194);
            this.TvDetailsGridVscrollBar.TabIndex = 1004;
            // 
            // TVDetailsGridView
            // 
            this.TVDetailsGridView.AllowCellClick = true;
            this.TVDetailsGridView.AllowDoubleClick = true;
            this.TVDetailsGridView.AllowEmptyRows = true;
            this.TVDetailsGridView.AllowEnterKey = false;
            this.TVDetailsGridView.AllowSorting = true;
            this.TVDetailsGridView.AllowUserToAddRows = false;
            this.TVDetailsGridView.AllowUserToDeleteRows = false;
            this.TVDetailsGridView.AllowUserToResizeColumns = false;
            this.TVDetailsGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.TVDetailsGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.TVDetailsGridView.ApplyStandardBehaviour = false;
            this.TVDetailsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TVDetailsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TVDetailsGridView.ClearCurrentCellOnLeave = false;
            this.TVDetailsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TVDetailsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.TVDetailsGridView.ColumnHeadersHeight = 21;
            this.TVDetailsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TVDetailsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Length,
            this.Detail,
            this.Comment,
            this.DetailId,
            this.EventId});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TVDetailsGridView.DefaultCellStyle = dataGridViewCellStyle11;
            this.TVDetailsGridView.DefaultRowIndex = -1;
            this.TVDetailsGridView.DeselectCurrentCell = false;
            this.TVDetailsGridView.DeselectSpecifiedRow = -1;
            this.TVDetailsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.TVDetailsGridView.EnableBinding = true;
            this.TVDetailsGridView.EnableHeadersVisualStyles = false;
            this.TVDetailsGridView.GridColor = System.Drawing.Color.Black;
            this.TVDetailsGridView.GridContentSelected = false;
            this.TVDetailsGridView.IsEditableGrid = true;
            this.TVDetailsGridView.IsSorted = false;
            this.TVDetailsGridView.Location = new System.Drawing.Point(-1, -1);
            this.TVDetailsGridView.MultiSelect = false;
            this.TVDetailsGridView.Name = "TVDetailsGridView";
            this.TVDetailsGridView.NumRowsVisible = 8;
            this.TVDetailsGridView.PrimaryKeyColumnName = "";
            this.TVDetailsGridView.RemainSortFields = true;
            this.TVDetailsGridView.RemoveDefaultSelection = false;
            this.TVDetailsGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TVDetailsGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.TVDetailsGridView.RowHeadersWidth = 20;
            this.TVDetailsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.TVDetailsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TVDetailsGridView.Size = new System.Drawing.Size(766, 197);
            this.TVDetailsGridView.StandardTab = true;
            this.TVDetailsGridView.TabIndex = 1;
            this.TVDetailsGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TVDetailsGridView_KeyDown);
            this.TVDetailsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TVDetailsGridView_CellClick);
            this.TVDetailsGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.TVDetailsGridView_DataBindingComplete);
            this.TVDetailsGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.TVDetailsGridView_CellParsing);
            this.TVDetailsGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.TVDetailsGridView_CellValueChanged);
            // 
            // Length
            // 
            this.Length.DataPropertyName = "Length";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Length.DefaultCellStyle = dataGridViewCellStyle9;
            this.Length.HeaderText = "Length";
            this.Length.MaxInputLength = 9;
            this.Length.Name = "Length";
            this.Length.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Detail
            // 
            this.Detail.DataPropertyName = "DetailTypeID";
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.Detail.DefaultCellStyle = dataGridViewCellStyle10;
            this.Detail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Detail.HeaderText = "Detail";
            this.Detail.Name = "Detail";
            this.Detail.Width = 180;
            // 
            // Comment
            // 
            this.Comment.DataPropertyName = "Comment";
            this.Comment.HeaderText = "Comment";
            this.Comment.MaxInputLength = 100;
            this.Comment.Name = "Comment";
            this.Comment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Comment.Width = 449;
            // 
            // DetailId
            // 
            this.DetailId.HeaderText = "DetailId";
            this.DetailId.Name = "DetailId";
            this.DetailId.ReadOnly = true;
            this.DetailId.Visible = false;
            // 
            // EventId
            // 
            this.EventId.HeaderText = "EventId";
            this.EventId.Name = "EventId";
            this.EventId.ReadOnly = true;
            this.EventId.Visible = false;
            // 
            // LengthPanel
            // 
            this.LengthPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LengthPanel.Controls.Add(this.panel2);
            this.LengthPanel.Controls.Add(this.LengthTextBox);
            this.LengthPanel.Location = new System.Drawing.Point(0, 0);
            this.LengthPanel.Name = "LengthPanel";
            this.LengthPanel.Size = new System.Drawing.Size(120, 25);
            this.LengthPanel.TabIndex = 1;
            this.LengthPanel.TabStop = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(-1, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(20, 25);
            this.panel2.TabIndex = 111;
            // 
            // LengthTextBox
            // 
            this.LengthTextBox.AllowClick = true;
            this.LengthTextBox.AllowNegativeSign = false;
            this.LengthTextBox.ApplyCFGFormat = false;
            this.LengthTextBox.ApplyCurrencyFormat = false;
            this.LengthTextBox.ApplyFocusColor = true;
            this.LengthTextBox.ApplyNegativeStandard = true;
            this.LengthTextBox.ApplyParentFocusColor = true;
            this.LengthTextBox.ApplyTimeFormat = false;
            this.LengthTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.LengthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LengthTextBox.CFromatWihoutSymbol = false;
            this.LengthTextBox.CheckForEmpty = false;
            this.LengthTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.LengthTextBox.Digits = -1;
            this.LengthTextBox.EmptyDecimalValue = false;
            this.LengthTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LengthTextBox.ForeColor = System.Drawing.Color.Black;
            this.LengthTextBox.IsEditable = true;
            this.LengthTextBox.IsQueryableFileld = true;
            this.LengthTextBox.Location = new System.Drawing.Point(21, 4);
            this.LengthTextBox.LockKeyPress = false;
            this.LengthTextBox.MaxLength = 9;
            this.LengthTextBox.Name = "LengthTextBox";
            this.LengthTextBox.PersistDefaultColor = false;
            this.LengthTextBox.Precision = 2;
            this.LengthTextBox.QueryingFileldName = "";
            this.LengthTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.LengthTextBox.Size = new System.Drawing.Size(94, 15);
            this.LengthTextBox.SpecialCharacter = "%";
            this.LengthTextBox.TabIndex = 1;
            this.LengthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LengthTextBox.TextCustomFormat = "$#,##0.00";
            this.LengthTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.LengthTextBox.WholeInteger = false;
            this.LengthTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // DetailPanel
            // 
            this.DetailPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DetailPanel.Controls.Add(this.DetailTypeComboBox);
            this.DetailPanel.Location = new System.Drawing.Point(119, 0);
            this.DetailPanel.Name = "DetailPanel";
            this.DetailPanel.Size = new System.Drawing.Size(181, 25);
            this.DetailPanel.TabIndex = 2;
            this.DetailPanel.TabStop = true;
            // 
            // DetailTypeComboBox
            // 
            this.DetailTypeComboBox.BackColor = System.Drawing.Color.White;
            this.DetailTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DetailTypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DetailTypeComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetailTypeComboBox.FormattingEnabled = true;
            this.DetailTypeComboBox.Location = new System.Drawing.Point(3, 1);
            this.DetailTypeComboBox.Name = "DetailTypeComboBox";
            this.DetailTypeComboBox.Size = new System.Drawing.Size(175, 24);
            this.DetailTypeComboBox.TabIndex = 1;
            this.DetailTypeComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // CommentPanel
            // 
            this.CommentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CommentPanel.Controls.Add(this.CommentTextBox);
            this.CommentPanel.Controls.Add(this.AddButton);
            this.CommentPanel.Location = new System.Drawing.Point(299, 0);
            this.CommentPanel.Name = "CommentPanel";
            this.CommentPanel.Size = new System.Drawing.Size(469, 25);
            this.CommentPanel.TabIndex = 3;
            this.CommentPanel.TabStop = true;
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
            this.CommentTextBox.Location = new System.Drawing.Point(3, 4);
            this.CommentTextBox.LockKeyPress = false;
            this.CommentTextBox.MaxLength = 100;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.PersistDefaultColor = false;
            this.CommentTextBox.Precision = 2;
            this.CommentTextBox.QueryingFileldName = "";
            this.CommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CommentTextBox.Size = new System.Drawing.Size(377, 16);
            this.CommentTextBox.SpecialCharacter = "%";
            this.CommentTextBox.TabIndex = 1;
            this.CommentTextBox.TextCustomFormat = "$#,##0.00";
            this.CommentTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CommentTextBox.WholeInteger = false;
            this.CommentTextBox.Leave += new System.EventHandler(this.CommentTextBox_Leave);
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
            this.AddButton.Location = new System.Drawing.Point(386, -1);
            this.AddButton.Name = "AddButton";
            this.AddButton.NewPadding = 5;
            this.AddButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.AddButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.None;
            this.AddButton.Size = new System.Drawing.Size(82, 25);
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
            // DistrictInfoSecIndicatorPictureBox
            // 
            this.DistrictInfoSecIndicatorPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("DistrictInfoSecIndicatorPictureBox.Image")));
            this.DistrictInfoSecIndicatorPictureBox.Location = new System.Drawing.Point(761, 0);
            this.DistrictInfoSecIndicatorPictureBox.Name = "DistrictInfoSecIndicatorPictureBox";
            this.DistrictInfoSecIndicatorPictureBox.Size = new System.Drawing.Size(42, 241);
            this.DistrictInfoSecIndicatorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DistrictInfoSecIndicatorPictureBox.TabIndex = 108;
            this.DistrictInfoSecIndicatorPictureBox.TabStop = false;
            this.DistrictInfoSecIndicatorPictureBox.Click += new System.EventHandler(this.DistrictInfoSecIndicatorPictureBox_Click);
            this.DistrictInfoSecIndicatorPictureBox.MouseEnter += new System.EventHandler(this.DistrictInfoSecIndicatorPictureBox_MouseEnter);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(19, 220);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(749, 21);
            this.panel4.TabIndex = 109;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 220);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 21);
            this.panel1.TabIndex = 110;
            // 
            // F8104
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.CommentPanel);
            this.Controls.Add(this.DetailPanel);
            this.Controls.Add(this.LengthPanel);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.DistrictInfoSecIndicatorPictureBox);
            this.Name = "F8104";
            this.ParentFormId = 8104;
            this.Size = new System.Drawing.Size(804, 241);
            this.Tag = "";
            this.Load += new System.EventHandler(this.F8104_Load);
            this.GridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TVDetailsGridView)).EndInit();
            this.LengthPanel.ResumeLayout(false);
            this.LengthPanel.PerformLayout();
            this.DetailPanel.ResumeLayout(false);
            this.CommentPanel.ResumeLayout(false);
            this.CommentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DistrictInfoSecIndicatorPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel GridPanel;
        private TerraScan.UI.Controls.TerraScanDataGridView TVDetailsGridView;
        private System.Windows.Forms.Panel LengthPanel;
        private TerraScan.UI.Controls.TerraScanTextBox LengthTextBox;
        private System.Windows.Forms.Panel DetailPanel;
        private TerraScan.UI.Controls.TerraScanComboBox DetailTypeComboBox;
        private System.Windows.Forms.Panel CommentPanel;
        private TerraScan.UI.Controls.TerraScanTextBox CommentTextBox;
        private TerraScan.UI.Controls.TerraScanButton AddButton;
        private System.Windows.Forms.PictureBox DistrictInfoSecIndicatorPictureBox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.VScrollBar TvDetailsGridVscrollBar;
        private System.Windows.Forms.ToolTip SanitaryPipeInspToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
        private System.Windows.Forms.DataGridViewComboBoxColumn Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailId;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventId;
    }
}
