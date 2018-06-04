namespace D9000
{
    partial class F9016
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9016));
            this.SqlCategoryCombo = new TerraScan.UI.Controls.TerraScanComboBox();
            this.SaveSQLButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CancelSQLButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SaveMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SaveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sqlQueryTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.sqlDescriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DeleteSQLButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DescipritonPanel = new System.Windows.Forms.Panel();
            this.DescriptionVScroll = new System.Windows.Forms.VScrollBar();
            this.DescriptionGrid = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.Descript = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SQLID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.DescipritonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DescriptionGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SqlCategoryCombo
            // 
            this.SqlCategoryCombo.AllowDrop = true;
            this.SqlCategoryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SqlCategoryCombo.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.SqlCategoryCombo.FormattingEnabled = true;
            this.SqlCategoryCombo.Location = new System.Drawing.Point(12, 15);
            this.SqlCategoryCombo.Name = "SqlCategoryCombo";
            this.SqlCategoryCombo.Size = new System.Drawing.Size(232, 24);
            this.SqlCategoryCombo.TabIndex = 1;
            this.SqlCategoryCombo.SelectionChangeCommitted += new System.EventHandler(this.SqlCategoryCombo_SelectionChangeCommitted);
            // 
            // SaveSQLButton
            // 
            this.SaveSQLButton.ActualPermission = false;
            this.SaveSQLButton.ApplyDisableBehaviour = false;
            this.SaveSQLButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SaveSQLButton.BorderColor = System.Drawing.Color.Wheat;
            this.SaveSQLButton.CommentPriority = false;
            this.SaveSQLButton.EnableAutoPrint = false;
            this.SaveSQLButton.FilterStatus = false;
            this.SaveSQLButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveSQLButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveSQLButton.FocusRectangleEnabled = true;
            this.SaveSQLButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveSQLButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SaveSQLButton.ImageSelected = false;
            this.SaveSQLButton.Location = new System.Drawing.Point(171, 390);
            this.SaveSQLButton.Name = "SaveSQLButton";
            this.SaveSQLButton.NewPadding = 5;
            this.SaveSQLButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.SaveSQLButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SaveSQLButton.Size = new System.Drawing.Size(91, 28);
            this.SaveSQLButton.StatusIndicator = false;
            this.SaveSQLButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SaveSQLButton.StatusOffText = null;
            this.SaveSQLButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SaveSQLButton.StatusOnText = null;
            this.SaveSQLButton.TabIndex = 162;
            this.SaveSQLButton.TabStop = false;
            this.SaveSQLButton.Text = "Save";
            this.SaveSQLButton.UseVisualStyleBackColor = false;
            this.SaveSQLButton.Click += new System.EventHandler(this.SaveSQLButton_Click);
            // 
            // CancelSQLButton
            // 
            this.CancelSQLButton.ActualPermission = false;
            this.CancelSQLButton.ApplyDisableBehaviour = false;
            this.CancelSQLButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelSQLButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelSQLButton.CommentPriority = false;
            this.CancelSQLButton.EnableAutoPrint = false;
            this.CancelSQLButton.FilterStatus = false;
            this.CancelSQLButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelSQLButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelSQLButton.FocusRectangleEnabled = true;
            this.CancelSQLButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelSQLButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelSQLButton.ImageSelected = false;
            this.CancelSQLButton.Location = new System.Drawing.Point(369, 390);
            this.CancelSQLButton.Name = "CancelSQLButton";
            this.CancelSQLButton.NewPadding = 5;
            this.CancelSQLButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CancelSQLButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelSQLButton.Size = new System.Drawing.Size(91, 28);
            this.CancelSQLButton.StatusIndicator = false;
            this.CancelSQLButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelSQLButton.StatusOffText = null;
            this.CancelSQLButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelSQLButton.StatusOnText = null;
            this.CancelSQLButton.TabIndex = 161;
            this.CancelSQLButton.TabStop = false;
            this.CancelSQLButton.Text = "Cancel";
            this.CancelSQLButton.UseVisualStyleBackColor = false;
            this.CancelSQLButton.Click += new System.EventHandler(this.CancelSQLButton_Click);
            // 
            // SaveMenuStrip
            // 
            this.SaveMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveMenu});
            this.SaveMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.SaveMenuStrip.Name = "SaveMenuStrip";
            this.SaveMenuStrip.Size = new System.Drawing.Size(538, 24);
            this.SaveMenuStrip.TabIndex = 163;
            this.SaveMenuStrip.Text = "menuStrip1";
            this.SaveMenuStrip.Visible = false;
            // 
            // SaveMenu
            // 
            this.SaveMenu.Name = "SaveMenu";
            this.SaveMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveMenu.Size = new System.Drawing.Size(43, 20);
            this.SaveMenu.Text = "Save";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(9, 409);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 167;
            this.label2.Text = "9016";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.panel5.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.panel5.Location = new System.Drawing.Point(13, 403);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(152, 2);
            this.panel5.TabIndex = 166;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.sqlQueryTextBox);
            this.panel1.Location = new System.Drawing.Point(12, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 101);
            this.panel1.TabIndex = 2;
            // 
            // sqlQueryTextBox
            // 
            this.sqlQueryTextBox.AllowClick = true;
            this.sqlQueryTextBox.AllowNegativeSign = false;
            this.sqlQueryTextBox.ApplyCFGFormat = false;
            this.sqlQueryTextBox.ApplyCurrencyFormat = false;
            this.sqlQueryTextBox.ApplyFocusColor = true;
            this.sqlQueryTextBox.ApplyNegativeStandard = true;
            this.sqlQueryTextBox.ApplyParentFocusColor = true;
            this.sqlQueryTextBox.ApplyTimeFormat = false;
            this.sqlQueryTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.sqlQueryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sqlQueryTextBox.CFromatWihoutSymbol = false;
            this.sqlQueryTextBox.CheckForEmpty = false;
            this.sqlQueryTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sqlQueryTextBox.Digits = -1;
            this.sqlQueryTextBox.EmptyDecimalValue = false;
            this.sqlQueryTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.sqlQueryTextBox.ForeColor = System.Drawing.Color.Black;
            this.sqlQueryTextBox.IsEditable = false;
            this.sqlQueryTextBox.IsQueryableFileld = false;
            this.sqlQueryTextBox.Location = new System.Drawing.Point(0, 0);
            this.sqlQueryTextBox.LockKeyPress = false;
            this.sqlQueryTextBox.MaxLength = 1000;
            this.sqlQueryTextBox.Multiline = true;
            this.sqlQueryTextBox.Name = "sqlQueryTextBox";
            this.sqlQueryTextBox.PersistDefaultColor = false;
            this.sqlQueryTextBox.Precision = 2;
            this.sqlQueryTextBox.QueryingFileldName = "";
            this.sqlQueryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sqlQueryTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.sqlQueryTextBox.Size = new System.Drawing.Size(446, 100);
            this.sqlQueryTextBox.SpecialCharacter = "";
            this.sqlQueryTextBox.TabIndex = 3;
            this.sqlQueryTextBox.TextCustomFormat = "";
            this.sqlQueryTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.sqlQueryTextBox.WholeInteger = false;
            this.sqlQueryTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SqlQueryTextBox_KeyPress);
            // 
            // sqlDescriptionTextBox
            // 
            this.sqlDescriptionTextBox.AllowClick = true;
            this.sqlDescriptionTextBox.AllowNegativeSign = false;
            this.sqlDescriptionTextBox.ApplyCFGFormat = false;
            this.sqlDescriptionTextBox.ApplyCurrencyFormat = false;
            this.sqlDescriptionTextBox.ApplyFocusColor = true;
            this.sqlDescriptionTextBox.ApplyNegativeStandard = true;
            this.sqlDescriptionTextBox.ApplyParentFocusColor = true;
            this.sqlDescriptionTextBox.ApplyTimeFormat = false;
            this.sqlDescriptionTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.sqlDescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sqlDescriptionTextBox.CFromatWihoutSymbol = false;
            this.sqlDescriptionTextBox.CheckForEmpty = false;
            this.sqlDescriptionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sqlDescriptionTextBox.Digits = -1;
            this.sqlDescriptionTextBox.EmptyDecimalValue = false;
            this.sqlDescriptionTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.sqlDescriptionTextBox.ForeColor = System.Drawing.Color.Black;
            this.sqlDescriptionTextBox.IsEditable = false;
            this.sqlDescriptionTextBox.IsQueryableFileld = false;
            this.sqlDescriptionTextBox.Location = new System.Drawing.Point(12, 42);
            this.sqlDescriptionTextBox.LockKeyPress = false;
            this.sqlDescriptionTextBox.MaxLength = 200;
            this.sqlDescriptionTextBox.Name = "sqlDescriptionTextBox";
            this.sqlDescriptionTextBox.PersistDefaultColor = false;
            this.sqlDescriptionTextBox.Precision = 2;
            this.sqlDescriptionTextBox.QueryingFileldName = "";
            this.sqlDescriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.sqlDescriptionTextBox.Size = new System.Drawing.Size(447, 23);
            this.sqlDescriptionTextBox.SpecialCharacter = "";
            this.sqlDescriptionTextBox.TabIndex = 2;
            this.sqlDescriptionTextBox.TextCustomFormat = "";
            this.sqlDescriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.sqlDescriptionTextBox.WholeInteger = false;
            // 
            // DeleteSQLButton
            // 
            this.DeleteSQLButton.ActualPermission = false;
            this.DeleteSQLButton.ApplyDisableBehaviour = false;
            this.DeleteSQLButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.DeleteSQLButton.BorderColor = System.Drawing.Color.Wheat;
            this.DeleteSQLButton.CommentPriority = false;
            this.DeleteSQLButton.EnableAutoPrint = false;
            this.DeleteSQLButton.FilterStatus = false;
            this.DeleteSQLButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.DeleteSQLButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteSQLButton.FocusRectangleEnabled = true;
            this.DeleteSQLButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteSQLButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DeleteSQLButton.ImageSelected = false;
            this.DeleteSQLButton.Location = new System.Drawing.Point(270, 390);
            this.DeleteSQLButton.Name = "DeleteSQLButton";
            this.DeleteSQLButton.NewPadding = 5;
            this.DeleteSQLButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Delete;
            this.DeleteSQLButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.DeleteSQLButton.Size = new System.Drawing.Size(91, 28);
            this.DeleteSQLButton.StatusIndicator = false;
            this.DeleteSQLButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DeleteSQLButton.StatusOffText = null;
            this.DeleteSQLButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.DeleteSQLButton.StatusOnText = null;
            this.DeleteSQLButton.TabIndex = 174;
            this.DeleteSQLButton.TabStop = false;
            this.DeleteSQLButton.Text = "Delete";
            this.DeleteSQLButton.UseVisualStyleBackColor = false;
            this.DeleteSQLButton.Click += new System.EventHandler(this.DeleteSQLButton_Click);
            // 
            // DescipritonPanel
            // 
            this.DescipritonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescipritonPanel.Controls.Add(this.DescriptionVScroll);
            this.DescipritonPanel.Controls.Add(this.DescriptionGrid);
            this.DescipritonPanel.Location = new System.Drawing.Point(12, 175);
            this.DescipritonPanel.Name = "DescipritonPanel";
            this.DescipritonPanel.Size = new System.Drawing.Size(447, 199);
            this.DescipritonPanel.TabIndex = 4;
            // 
            // DescriptionVScroll
            // 
            this.DescriptionVScroll.Enabled = false;
            this.DescriptionVScroll.Location = new System.Drawing.Point(429, 0);
            this.DescriptionVScroll.Name = "DescriptionVScroll";
            this.DescriptionVScroll.Size = new System.Drawing.Size(16, 199);
            this.DescriptionVScroll.TabIndex = 1005;
            // 
            // DescriptionGrid
            // 
            this.DescriptionGrid.AllowCellClick = true;
            this.DescriptionGrid.AllowDoubleClick = false;
            this.DescriptionGrid.AllowEmptyRows = false;
            this.DescriptionGrid.AllowEnterKey = false;
            this.DescriptionGrid.AllowSorting = false;
            this.DescriptionGrid.AllowUserToAddRows = false;
            this.DescriptionGrid.AllowUserToDeleteRows = false;
            this.DescriptionGrid.AllowUserToResizeColumns = false;
            this.DescriptionGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DescriptionGrid.ApplyStandardBehaviour = false;
            this.DescriptionGrid.BackgroundColor = System.Drawing.Color.White;
            this.DescriptionGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DescriptionGrid.ClearCurrentCellOnLeave = true;
            this.DescriptionGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DescriptionGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DescriptionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DescriptionGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Descript,
            this.SQLID});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DescriptionGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.DescriptionGrid.DefaultRowIndex = 0;
            this.DescriptionGrid.DeselectCurrentCell = false;
            this.DescriptionGrid.DeselectSpecifiedRow = -1;
            this.DescriptionGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DescriptionGrid.EnableBinding = true;
            this.DescriptionGrid.EnableHeadersVisualStyles = false;
            this.DescriptionGrid.GridColor = System.Drawing.Color.Black;
            this.DescriptionGrid.GridContentSelected = false;
            this.DescriptionGrid.IsEditableGrid = false;
            this.DescriptionGrid.IsSorted = false;
            this.DescriptionGrid.Location = new System.Drawing.Point(-1, -1);
            this.DescriptionGrid.MultiSelect = false;
            this.DescriptionGrid.Name = "DescriptionGrid";
            this.DescriptionGrid.NumRowsVisible = 8;
            this.DescriptionGrid.PrimaryKeyColumnName = "";
            this.DescriptionGrid.RemainSortFields = false;
            this.DescriptionGrid.RemoveDefaultSelection = true;
            this.DescriptionGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DescriptionGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DescriptionGrid.RowHeadersWidth = 20;
            this.DescriptionGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.DescriptionGrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DescriptionGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DescriptionGrid.Size = new System.Drawing.Size(447, 200);
            this.DescriptionGrid.TabIndex = 4;
            this.DescriptionGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DescriptionGrid_CellClick);
            // 
            // Descript
            // 
            this.Descript.HeaderText = "Description";
            this.Descript.Name = "Descript";
            this.Descript.ReadOnly = true;
            this.Descript.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Descript.Width = 410;
            // 
            // SQLID
            // 
            this.SQLID.HeaderText = "SQLID";
            this.SQLID.Name = "SQLID";
            this.SQLID.ReadOnly = true;
            this.SQLID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SQLID.Visible = false;
            // 
            // F9016
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(472, 431);
            this.Controls.Add(this.DescipritonPanel);
            this.Controls.Add(this.DeleteSQLButton);
            this.Controls.Add(this.sqlDescriptionTextBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.SaveMenuStrip);
            this.Controls.Add(this.SaveSQLButton);
            this.Controls.Add(this.SqlCategoryCombo);
            this.Controls.Add(this.CancelSQLButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F9016";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "9015";
            this.Text = "TerraScan T2 - Save SQL Support";
            this.Load += new System.EventHandler(this.QuerySave_Load);
            this.SaveMenuStrip.ResumeLayout(false);
            this.SaveMenuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.DescipritonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DescriptionGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanComboBox SqlCategoryCombo;
        private TerraScan.UI.Controls.TerraScanButton SaveSQLButton;
        private TerraScan.UI.Controls.TerraScanButton CancelSQLButton;
        private System.Windows.Forms.MenuStrip SaveMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SaveMenu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private TerraScan.UI.Controls.TerraScanTextBox sqlDescriptionTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox sqlQueryTextBox;
        private TerraScan.UI.Controls.TerraScanButton DeleteSQLButton;
        private System.Windows.Forms.Panel DescipritonPanel;
        private TerraScan.UI.Controls.TerraScanDataGridView DescriptionGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descript;
        private System.Windows.Forms.DataGridViewTextBoxColumn SQLID;
        private System.Windows.Forms.VScrollBar DescriptionVScroll;
    }
}