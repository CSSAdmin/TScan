namespace D9030
{
    partial class F9045
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9045));
            this.StateCodePanel = new System.Windows.Forms.Panel();
            this.StateCodeLabel = new System.Windows.Forms.Label();
            this.StateCodeTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.FormLabel = new System.Windows.Forms.Label();
            this.StateCodeDataGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.KeyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.stateVSscrollBar = new System.Windows.Forms.VScrollBar();
            this.AcceptMasterNameButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SearchButton = new TerraScan.UI.Controls.TerraScanButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StateCodePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StateCodeDataGridView)).BeginInit();
            this.GridPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StateCodePanel
            // 
            this.StateCodePanel.BackColor = System.Drawing.SystemColors.Window;
            this.StateCodePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StateCodePanel.Controls.Add(this.StateCodeLabel);
            this.StateCodePanel.Controls.Add(this.StateCodeTextBox);
            this.StateCodePanel.Location = new System.Drawing.Point(12, 13);
            this.StateCodePanel.Name = "StateCodePanel";
            this.StateCodePanel.Size = new System.Drawing.Size(372, 41);
            this.StateCodePanel.TabIndex = 0;
            // 
            // StateCodeLabel
            // 
            this.StateCodeLabel.AutoSize = true;
            this.StateCodeLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateCodeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.StateCodeLabel.Location = new System.Drawing.Point(0, 0);
            this.StateCodeLabel.Name = "StateCodeLabel";
            this.StateCodeLabel.Size = new System.Drawing.Size(70, 14);
            this.StateCodeLabel.TabIndex = 1;
            this.StateCodeLabel.Text = "State Code:";
            // 
            // StateCodeTextBox
            // 
            this.StateCodeTextBox.AllowClick = true;
            this.StateCodeTextBox.AllowNegativeSign = false;
            this.StateCodeTextBox.ApplyCFGFormat = false;
            this.StateCodeTextBox.ApplyCurrencyFormat = false;
            this.StateCodeTextBox.ApplyFocusColor = false;
            this.StateCodeTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.StateCodeTextBox.ApplyNegativeStandard = true;
            this.StateCodeTextBox.ApplyParentFocusColor = true;
            this.StateCodeTextBox.ApplyTimeFormat = false;
            this.StateCodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StateCodeTextBox.CFromatWihoutSymbol = false;
            this.StateCodeTextBox.CheckForEmpty = false;
            this.StateCodeTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.StateCodeTextBox.Digits = -1;
            this.StateCodeTextBox.EmptyDecimalValue = false;
            this.StateCodeTextBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateCodeTextBox.IsEditable = false;
            this.StateCodeTextBox.IsQueryableFileld = false;
            this.StateCodeTextBox.Location = new System.Drawing.Point(9, 20);
            this.StateCodeTextBox.LockKeyPress = false;
            this.StateCodeTextBox.MaxLength = 50;
            this.StateCodeTextBox.Name = "StateCodeTextBox";
            this.StateCodeTextBox.PersistDefaultColor = false;
            this.StateCodeTextBox.Precision = 2;
            this.StateCodeTextBox.QueryingFileldName = "";
            this.StateCodeTextBox.SetColorFlag = false;
            this.StateCodeTextBox.SetFocusColor = System.Drawing.Color.Empty;
            this.StateCodeTextBox.Size = new System.Drawing.Size(342, 13);
            this.StateCodeTextBox.SpecialCharacter = "%";
            this.StateCodeTextBox.TabIndex = 2;
            this.StateCodeTextBox.TextCustomFormat = "$ #,##0.00";
            this.StateCodeTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.StateCodeTextBox.WholeInteger = false;
            // 
            // FormLabel
            // 
            this.FormLabel.AutoSize = true;
            this.FormLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.FormLabel.Location = new System.Drawing.Point(9, 263);
            this.FormLabel.Name = "FormLabel";
            this.FormLabel.Size = new System.Drawing.Size(35, 15);
            this.FormLabel.TabIndex = 120;
            this.FormLabel.Text = "9045";
            // 
            // StateCodeDataGridView
            // 
            this.StateCodeDataGridView.AllowCellClick = true;
            this.StateCodeDataGridView.AllowDoubleClick = true;
            this.StateCodeDataGridView.AllowEmptyRows = true;
            this.StateCodeDataGridView.AllowEnterKey = false;
            this.StateCodeDataGridView.AllowSorting = true;
            this.StateCodeDataGridView.AllowUserToAddRows = false;
            this.StateCodeDataGridView.AllowUserToDeleteRows = false;
            this.StateCodeDataGridView.AllowUserToResizeColumns = false;
            this.StateCodeDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.StateCodeDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.StateCodeDataGridView.ApplyStandardBehaviour = false;
            this.StateCodeDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.StateCodeDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StateCodeDataGridView.ClearCurrentCellOnLeave = false;
            this.StateCodeDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.StateCodeDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.StateCodeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StateCodeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KeyID,
            this.StateCode});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.StateCodeDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.StateCodeDataGridView.DefaultRowIndex = -1;
            this.StateCodeDataGridView.DeselectCurrentCell = false;
            this.StateCodeDataGridView.DeselectSpecifiedRow = -1;
            this.StateCodeDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.StateCodeDataGridView.EnableBinding = false;
            this.StateCodeDataGridView.EnableHeadersVisualStyles = false;
            this.StateCodeDataGridView.GridColor = System.Drawing.Color.Black;
            this.StateCodeDataGridView.GridContentSelected = false;
            this.StateCodeDataGridView.IsEditableGrid = false;
            this.StateCodeDataGridView.IsMultiSelect = false;
            this.StateCodeDataGridView.IsSorted = false;
            this.StateCodeDataGridView.Location = new System.Drawing.Point(-2, -2);
            this.StateCodeDataGridView.MultiSelect = false;
            this.StateCodeDataGridView.Name = "StateCodeDataGridView";
            this.StateCodeDataGridView.NumRowsVisible = 6;
            this.StateCodeDataGridView.PrimaryKeyColumnName = "";
            this.StateCodeDataGridView.RemainSortFields = false;
            this.StateCodeDataGridView.RemoveDefaultSelection = false;
            this.StateCodeDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.StateCodeDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.StateCodeDataGridView.RowHeadersWidth = 20;
            this.StateCodeDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.StateCodeDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StateCodeDataGridView.Size = new System.Drawing.Size(372, 152);
            this.StateCodeDataGridView.StandardTab = true;
            this.StateCodeDataGridView.TabIndex = 9;
            this.StateCodeDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StateCodeDataGridView_CellDoubleClick);
            this.StateCodeDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StateCodeDataGridView_CellClick);
            // 
            // KeyID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KeyID.DefaultCellStyle = dataGridViewCellStyle3;
            this.KeyID.HeaderText = "KeyID";
            this.KeyID.Name = "KeyID";
            this.KeyID.ReadOnly = true;
            this.KeyID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.KeyID.Visible = false;
            this.KeyID.Width = 81;
            // 
            // StateCode
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.StateCode.DefaultCellStyle = dataGridViewCellStyle4;
            this.StateCode.HeaderText = "State Codes";
            this.StateCode.MaxInputLength = 150;
            this.StateCode.Name = "StateCode";
            this.StateCode.ReadOnly = true;
            this.StateCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.StateCode.Width = 337;
            // 
            // GridPanel
            // 
            this.GridPanel.BackColor = System.Drawing.SystemColors.Window;
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.stateVSscrollBar);
            this.GridPanel.Controls.Add(this.StateCodeDataGridView);
            this.GridPanel.Location = new System.Drawing.Point(12, 53);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(372, 148);
            this.GridPanel.TabIndex = 0;
            // 
            // stateVSscrollBar
            // 
            this.stateVSscrollBar.Enabled = false;
            this.stateVSscrollBar.Location = new System.Drawing.Point(354, 0);
            this.stateVSscrollBar.Name = "stateVSscrollBar";
            this.stateVSscrollBar.Size = new System.Drawing.Size(16, 146);
            this.stateVSscrollBar.TabIndex = 9;
            // 
            // AcceptMasterNameButton
            // 
            this.AcceptMasterNameButton.ActualPermission = false;
            this.AcceptMasterNameButton.ApplyDisableBehaviour = false;
            this.AcceptMasterNameButton.AutoSize = true;
            this.AcceptMasterNameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AcceptMasterNameButton.BorderColor = System.Drawing.Color.Wheat;
            this.AcceptMasterNameButton.CommentPriority = false;
            this.AcceptMasterNameButton.EnableAutoPrint = false;
            this.AcceptMasterNameButton.Enabled = false;
            this.AcceptMasterNameButton.FilterStatus = false;
            this.AcceptMasterNameButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AcceptMasterNameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AcceptMasterNameButton.FocusRectangleEnabled = true;
            this.AcceptMasterNameButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptMasterNameButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AcceptMasterNameButton.ImageSelected = false;
            this.AcceptMasterNameButton.Location = new System.Drawing.Point(12, 214);
            this.AcceptMasterNameButton.Name = "AcceptMasterNameButton";
            this.AcceptMasterNameButton.NewPadding = 5;
            this.AcceptMasterNameButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.AcceptMasterNameButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AcceptMasterNameButton.Size = new System.Drawing.Size(110, 30);
            this.AcceptMasterNameButton.StatusIndicator = false;
            this.AcceptMasterNameButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AcceptMasterNameButton.StatusOffText = null;
            this.AcceptMasterNameButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AcceptMasterNameButton.StatusOnText = null;
            this.AcceptMasterNameButton.TabIndex = 121;
            this.AcceptMasterNameButton.TabStop = false;
            this.AcceptMasterNameButton.Text = "Accept";
            this.AcceptMasterNameButton.UseVisualStyleBackColor = false;
            this.AcceptMasterNameButton.Click += new System.EventHandler(this.AcceptMasterNameButton_Click);
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
            this.CancelButton.Location = new System.Drawing.Point(274, 214);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NewPadding = 5;
            this.CancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.CancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelButton.Size = new System.Drawing.Size(110, 30);
            this.CancelButton.StatusIndicator = false;
            this.CancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelButton.StatusOffText = null;
            this.CancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelButton.StatusOnText = null;
            this.CancelButton.TabIndex = 122;
            this.CancelButton.TabStop = false;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
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
            this.SearchButton.Location = new System.Drawing.Point(144, 214);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.NewPadding = 5;
            this.SearchButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.SearchButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SearchButton.Size = new System.Drawing.Size(110, 30);
            this.SearchButton.StatusIndicator = false;
            this.SearchButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SearchButton.StatusOffText = null;
            this.SearchButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SearchButton.StatusOnText = null;
            this.SearchButton.TabIndex = 123;
            this.SearchButton.TabStop = false;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(12, 258);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 2);
            this.panel1.TabIndex = 119;
            // 
            // F9045
            // 
            this.AcceptButton = this.SearchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(396, 287);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AcceptMasterNameButton);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.StateCodePanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.FormLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F9045";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "State Code Search";
            this.Load += new System.EventHandler(this.F9045_Load);
            this.StateCodePanel.ResumeLayout(false);
            this.StateCodePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StateCodeDataGridView)).EndInit();
            this.GridPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel StateCodePanel;
        private System.Windows.Forms.Label StateCodeLabel;
        private TerraScan.UI.Controls.TerraScanTextBox StateCodeTextBox;
        private TerraScan.UI.Controls.TerraScanDataGridView StateCodeDataGridView;
        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.VScrollBar stateVSscrollBar;
        private System.Windows.Forms.Label FormLabel;
        private TerraScan.UI.Controls.TerraScanButton AcceptMasterNameButton;
        private TerraScan.UI.Controls.TerraScanButton CancelButton;
        private TerraScan.UI.Controls.TerraScanButton SearchButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateCode;   
    }
}