namespace D36010
{
    partial class F36012
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F36012));
            this.MISelectionGridViewPanel = new System.Windows.Forms.Panel();
            this.MISelectionGridVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.MISelectionGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MIChoiceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MICodeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiscSelectionCloseButton = new TerraScan.UI.Controls.TerraScanButton();
            this.MiscSelectionSaveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.MISelectionFormLabel = new System.Windows.Forms.Label();
            this.MISelectionGridViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MISelectionGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // MISelectionGridViewPanel
            // 
            this.MISelectionGridViewPanel.BackColor = System.Drawing.Color.White;
            this.MISelectionGridViewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MISelectionGridViewPanel.Controls.Add(this.MISelectionGridVerticalScroll);
            this.MISelectionGridViewPanel.Controls.Add(this.MISelectionGridView);
            this.MISelectionGridViewPanel.Location = new System.Drawing.Point(0, 0);
            this.MISelectionGridViewPanel.Name = "MISelectionGridViewPanel";
            this.MISelectionGridViewPanel.Size = new System.Drawing.Size(362, 242);
            this.MISelectionGridViewPanel.TabIndex = 162;
            this.MISelectionGridViewPanel.TabStop = true;
            // 
            // MISelectionGridVerticalScroll
            // 
            this.MISelectionGridVerticalScroll.Enabled = false;
            this.MISelectionGridVerticalScroll.Location = new System.Drawing.Point(344, -1);
            this.MISelectionGridVerticalScroll.Name = "MISelectionGridVerticalScroll";
            this.MISelectionGridVerticalScroll.Size = new System.Drawing.Size(15, 242);
            this.MISelectionGridVerticalScroll.TabIndex = 163;
            // 
            // MISelectionGridView
            // 
            this.MISelectionGridView.AllowCellClick = true;
            this.MISelectionGridView.AllowDoubleClick = true;
            this.MISelectionGridView.AllowEmptyRows = true;
            this.MISelectionGridView.AllowEnterKey = false;
            this.MISelectionGridView.AllowSorting = true;
            this.MISelectionGridView.AllowUserToAddRows = false;
            this.MISelectionGridView.AllowUserToDeleteRows = false;
            this.MISelectionGridView.AllowUserToResizeColumns = false;
            this.MISelectionGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.MISelectionGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.MISelectionGridView.ApplyStandardBehaviour = false;
            this.MISelectionGridView.BackgroundColor = System.Drawing.Color.White;
            this.MISelectionGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MISelectionGridView.ClearCurrentCellOnLeave = true;
            this.MISelectionGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MISelectionGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MISelectionGridView.ColumnHeadersHeight = 22;
            this.MISelectionGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.MISelectionGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.ItemValue,
            this.MIChoiceID,
            this.MICodeID,
            this.FieldNum});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MISelectionGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.MISelectionGridView.DefaultRowIndex = 0;
            this.MISelectionGridView.DeselectCurrentCell = false;
            this.MISelectionGridView.DeselectSpecifiedRow = -1;
            this.MISelectionGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.MISelectionGridView.EnableBinding = true;
            this.MISelectionGridView.EnableHeadersVisualStyles = false;
            this.MISelectionGridView.GridColor = System.Drawing.Color.Black;
            this.MISelectionGridView.GridContentSelected = false;
            this.MISelectionGridView.IsEditableGrid = true;
            this.MISelectionGridView.IsMultiSelect = false;
            this.MISelectionGridView.IsSorted = false;
            this.MISelectionGridView.Location = new System.Drawing.Point(-1, -1);
            this.MISelectionGridView.MultiSelect = false;
            this.MISelectionGridView.Name = "MISelectionGridView";
            this.MISelectionGridView.NumRowsVisible = 10;
            this.MISelectionGridView.PrimaryKeyColumnName = "";
            this.MISelectionGridView.RemainSortFields = true;
            this.MISelectionGridView.RemoveDefaultSelection = true;
            this.MISelectionGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MISelectionGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.MISelectionGridView.RowHeadersWidth = 20;
            this.MISelectionGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.MISelectionGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MISelectionGridView.Size = new System.Drawing.Size(362, 242);
            this.MISelectionGridView.TabIndex = 9;
            this.MISelectionGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.MISelectionGridView_CellValueChanged);
            this.MISelectionGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.MISelectionGridView_CellBeginEdit);
            this.MISelectionGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.MISelectionGridView_RowEnter);
            this.MISelectionGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.MISelectionGridView_CellParsing);
            this.MISelectionGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.MISelectionGridView_CellFormatting);
            this.MISelectionGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.MISelectionGridView_EditingControlShowing);
            this.MISelectionGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MISelectionGridView_KeyDown);
            this.MISelectionGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MISelectionGridView_RowHeaderMouseClick);
            // 
            // ItemName
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.MaxInputLength = 20;
            this.ItemName.Name = "ItemName";
            this.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ItemName.Width = 165;
            // 
            // ItemValue
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemValue.DefaultCellStyle = dataGridViewCellStyle4;
            this.ItemValue.HeaderText = "Item Value";
            this.ItemValue.MaxInputLength = 19;
            this.ItemValue.Name = "ItemValue";
            this.ItemValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ItemValue.Width = 160;
            // 
            // MIChoiceID
            // 
            this.MIChoiceID.HeaderText = "MIChoiceID";
            this.MIChoiceID.Name = "MIChoiceID";
            this.MIChoiceID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MIChoiceID.Visible = false;
            // 
            // MICodeID
            // 
            this.MICodeID.HeaderText = "MICodeID";
            this.MICodeID.Name = "MICodeID";
            this.MICodeID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MICodeID.Visible = false;
            // 
            // FieldNum
            // 
            this.FieldNum.HeaderText = "FieldNum";
            this.FieldNum.Name = "FieldNum";
            this.FieldNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FieldNum.Visible = false;
            // 
            // MiscSelectionCloseButton
            // 
            this.MiscSelectionCloseButton.ActualPermission = false;
            this.MiscSelectionCloseButton.ApplyDisableBehaviour = false;
            this.MiscSelectionCloseButton.AutoSize = true;
            this.MiscSelectionCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.MiscSelectionCloseButton.BorderColor = System.Drawing.Color.Wheat;
            this.MiscSelectionCloseButton.CommentPriority = false;
            this.MiscSelectionCloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MiscSelectionCloseButton.EnableAutoPrint = false;
            this.MiscSelectionCloseButton.FilterStatus = false;
            this.MiscSelectionCloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MiscSelectionCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MiscSelectionCloseButton.FocusRectangleEnabled = true;
            this.MiscSelectionCloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiscSelectionCloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MiscSelectionCloseButton.ImageSelected = false;
            this.MiscSelectionCloseButton.Location = new System.Drawing.Point(180, 250);
            this.MiscSelectionCloseButton.Name = "MiscSelectionCloseButton";
            this.MiscSelectionCloseButton.NewPadding = 5;
            this.MiscSelectionCloseButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.MiscSelectionCloseButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.MiscSelectionCloseButton.Size = new System.Drawing.Size(110, 30);
            this.MiscSelectionCloseButton.StatusIndicator = false;
            this.MiscSelectionCloseButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MiscSelectionCloseButton.StatusOffText = null;
            this.MiscSelectionCloseButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.MiscSelectionCloseButton.StatusOnText = null;
            this.MiscSelectionCloseButton.TabIndex = 164;
            this.MiscSelectionCloseButton.TabStop = false;
            this.MiscSelectionCloseButton.Text = "Close";
            this.MiscSelectionCloseButton.UseVisualStyleBackColor = false;
            this.MiscSelectionCloseButton.Click += new System.EventHandler(this.MiscSelectionCloseButton_Click);
            // 
            // MiscSelectionSaveButton
            // 
            this.MiscSelectionSaveButton.ActualPermission = false;
            this.MiscSelectionSaveButton.ApplyDisableBehaviour = false;
            this.MiscSelectionSaveButton.AutoSize = true;
            this.MiscSelectionSaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.MiscSelectionSaveButton.BorderColor = System.Drawing.Color.Wheat;
            this.MiscSelectionSaveButton.CommentPriority = false;
            this.MiscSelectionSaveButton.EnableAutoPrint = false;
            this.MiscSelectionSaveButton.FilterStatus = false;
            this.MiscSelectionSaveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MiscSelectionSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MiscSelectionSaveButton.FocusRectangleEnabled = true;
            this.MiscSelectionSaveButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiscSelectionSaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MiscSelectionSaveButton.ImageSelected = false;
            this.MiscSelectionSaveButton.Location = new System.Drawing.Point(38, 250);
            this.MiscSelectionSaveButton.Name = "MiscSelectionSaveButton";
            this.MiscSelectionSaveButton.NewPadding = 5;
            this.MiscSelectionSaveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.MiscSelectionSaveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.MiscSelectionSaveButton.Size = new System.Drawing.Size(110, 30);
            this.MiscSelectionSaveButton.StatusIndicator = false;
            this.MiscSelectionSaveButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MiscSelectionSaveButton.StatusOffText = null;
            this.MiscSelectionSaveButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.MiscSelectionSaveButton.StatusOnText = null;
            this.MiscSelectionSaveButton.TabIndex = 163;
            this.MiscSelectionSaveButton.TabStop = false;
            this.MiscSelectionSaveButton.Text = "Save";
            this.MiscSelectionSaveButton.UseVisualStyleBackColor = false;
            this.MiscSelectionSaveButton.Click += new System.EventHandler(this.MiscSelectionSaveButton_Click);
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(7, 291);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(348, 2);
            this.FormLinePanel.TabIndex = 165;
            // 
            // MISelectionFormLabel
            // 
            this.MISelectionFormLabel.AutoSize = true;
            this.MISelectionFormLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MISelectionFormLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.MISelectionFormLabel.Location = new System.Drawing.Point(9, 296);
            this.MISelectionFormLabel.Name = "MISelectionFormLabel";
            this.MISelectionFormLabel.Size = new System.Drawing.Size(42, 15);
            this.MISelectionFormLabel.TabIndex = 166;
            this.MISelectionFormLabel.Text = "36012";
            // 
            // F36012
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.MiscSelectionCloseButton;
            this.ClientSize = new System.Drawing.Size(362, 320);
            this.Controls.Add(this.MISelectionFormLabel);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.MiscSelectionCloseButton);
            this.Controls.Add(this.MiscSelectionSaveButton);
            this.Controls.Add(this.MISelectionGridViewPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(368, 352);
            this.MinimumSize = new System.Drawing.Size(368, 352);
            this.Name = "F36012";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "36012";
            this.Text = "MI Selections";
            this.Load += new System.EventHandler(this.F36012_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F36012_FormClosing);
            this.MISelectionGridViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MISelectionGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MISelectionGridViewPanel;
        private System.Windows.Forms.VScrollBar MISelectionGridVerticalScroll;
        private TerraScan.UI.Controls.TerraScanDataGridView MISelectionGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn MIChoiceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MICodeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemValue;
        private TerraScan.UI.Controls.TerraScanButton MiscSelectionCloseButton;
        private TerraScan.UI.Controls.TerraScanButton MiscSelectionSaveButton;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label MISelectionFormLabel;

    }
}