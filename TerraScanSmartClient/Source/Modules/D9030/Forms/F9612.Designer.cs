namespace D9030
{
    partial class F9612
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9612));
            this.RecordHistoryGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.HistoryPanel = new System.Windows.Forms.Panel();
            this.LayoutMgmtVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.CloseButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AcceptRecordButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.HelpLink = new TerraScan.SmartParts.HelpSmartPart();
            this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Task = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parameter4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parameter3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parameter2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parameter1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.RecordHistoryGridView)).BeginInit();
            this.HistoryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RecordHistoryGridView
            // 
            this.RecordHistoryGridView.AllowCellClick = true;
            this.RecordHistoryGridView.AllowDoubleClick = true;
            this.RecordHistoryGridView.AllowEmptyRows = true;
            this.RecordHistoryGridView.AllowEnterKey = false;
            this.RecordHistoryGridView.AllowSorting = false;
            this.RecordHistoryGridView.AllowUserToAddRows = false;
            this.RecordHistoryGridView.AllowUserToDeleteRows = false;
            this.RecordHistoryGridView.AllowUserToResizeColumns = false;
            this.RecordHistoryGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.RecordHistoryGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.RecordHistoryGridView.ApplyStandardBehaviour = false;
            this.RecordHistoryGridView.BackgroundColor = System.Drawing.Color.White;
            this.RecordHistoryGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RecordHistoryGridView.ClearCurrentCellOnLeave = false;
            this.RecordHistoryGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RecordHistoryGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.RecordHistoryGridView.ColumnHeadersHeight = 22;
            this.RecordHistoryGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.RecordHistoryGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FormNumber,
            this.Parameter1,
            this.Parameter2,
            this.Parameter3,
            this.Parameter4,
            this.UserId,
            this.Task,
            this.DateTime});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RecordHistoryGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.RecordHistoryGridView.DefaultRowIndex = -1;
            this.RecordHistoryGridView.DeselectCurrentCell = false;
            this.RecordHistoryGridView.DeselectSpecifiedRow = -1;
            this.RecordHistoryGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.RecordHistoryGridView.EnableBinding = true;
            this.RecordHistoryGridView.EnableHeadersVisualStyles = false;
            this.RecordHistoryGridView.GridColor = System.Drawing.Color.Black;
            this.RecordHistoryGridView.GridContentSelected = false;
            this.RecordHistoryGridView.IsEditableGrid = false;
            this.RecordHistoryGridView.IsMultiSelect = false;
            this.RecordHistoryGridView.IsSorted = false;
            this.RecordHistoryGridView.Location = new System.Drawing.Point(-1, -1);
            this.RecordHistoryGridView.MultiSelect = false;
            this.RecordHistoryGridView.Name = "RecordHistoryGridView";
            this.RecordHistoryGridView.NumRowsVisible = 0;
            this.RecordHistoryGridView.PrimaryKeyColumnName = "";
            this.RecordHistoryGridView.RemainSortFields = false;
            this.RecordHistoryGridView.RemoveDefaultSelection = false;
            this.RecordHistoryGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RecordHistoryGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.RecordHistoryGridView.RowHeadersWidth = 20;
            this.RecordHistoryGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.RecordHistoryGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RecordHistoryGridView.Size = new System.Drawing.Size(450, 462);
            this.RecordHistoryGridView.TabIndex = 13;
            this.RecordHistoryGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RecordHistoryGridView_CellDoubleClick);
            this.RecordHistoryGridView.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.RecordHistoryGridView_CellToolTipTextNeeded);
            this.RecordHistoryGridView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RecordHistoryGridView_KeyUp);
            // 
            // HistoryPanel
            // 
            this.HistoryPanel.BackColor = System.Drawing.Color.White;
            this.HistoryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HistoryPanel.Controls.Add(this.LayoutMgmtVerticalScroll);
            this.HistoryPanel.Controls.Add(this.RecordHistoryGridView);
            this.HistoryPanel.Controls.Add(this.CloseButton);
            this.HistoryPanel.Controls.Add(this.AcceptRecordButton);
            this.HistoryPanel.Location = new System.Drawing.Point(6, 12);
            this.HistoryPanel.Name = "HistoryPanel";
            this.HistoryPanel.Size = new System.Drawing.Size(452, 462);
            this.HistoryPanel.TabIndex = 14;
            // 
            // LayoutMgmtVerticalScroll
            // 
            this.LayoutMgmtVerticalScroll.Enabled = false;
            this.LayoutMgmtVerticalScroll.Location = new System.Drawing.Point(433, 0);
            this.LayoutMgmtVerticalScroll.Name = "LayoutMgmtVerticalScroll";
            this.LayoutMgmtVerticalScroll.Size = new System.Drawing.Size(16, 460);
            this.LayoutMgmtVerticalScroll.TabIndex = 164;
            // 
            // CloseButton
            // 
            this.CloseButton.ActualPermission = false;
            this.CloseButton.ApplyDisableBehaviour = false;
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CloseButton.BorderColor = System.Drawing.Color.Wheat;
            this.CloseButton.CommentPriority = false;
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.EnableAutoPrint = false;
            this.CloseButton.FilterStatus = false;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.FocusRectangleEnabled = true;
            this.CloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.ImageSelected = false;
            this.CloseButton.Location = new System.Drawing.Point(142, 3);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.NewPadding = 5;
            this.CloseButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.CloseButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CloseButton.Size = new System.Drawing.Size(110, 30);
            this.CloseButton.StatusIndicator = false;
            this.CloseButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CloseButton.StatusOffText = null;
            this.CloseButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CloseButton.StatusOnText = null;
            this.CloseButton.TabIndex = 217;
            this.CloseButton.TabStop = false;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // AcceptRecordButton
            // 
            this.AcceptRecordButton.ActualPermission = false;
            this.AcceptRecordButton.ApplyDisableBehaviour = false;
            this.AcceptRecordButton.AutoSize = true;
            this.AcceptRecordButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AcceptRecordButton.BorderColor = System.Drawing.Color.Wheat;
            this.AcceptRecordButton.CommentPriority = false;
            this.AcceptRecordButton.EnableAutoPrint = false;
            this.AcceptRecordButton.FilterStatus = false;
            this.AcceptRecordButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AcceptRecordButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AcceptRecordButton.FocusRectangleEnabled = true;
            this.AcceptRecordButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptRecordButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AcceptRecordButton.ImageSelected = false;
            this.AcceptRecordButton.Location = new System.Drawing.Point(5, 3);
            this.AcceptRecordButton.Name = "AcceptRecordButton";
            this.AcceptRecordButton.NewPadding = 5;
            this.AcceptRecordButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.AcceptRecordButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AcceptRecordButton.Size = new System.Drawing.Size(110, 30);
            this.AcceptRecordButton.StatusIndicator = false;
            this.AcceptRecordButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AcceptRecordButton.StatusOffText = null;
            this.AcceptRecordButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AcceptRecordButton.StatusOnText = null;
            this.AcceptRecordButton.TabIndex = 218;
            this.AcceptRecordButton.TabStop = false;
            this.AcceptRecordButton.Text = "Accept";
            this.AcceptRecordButton.UseVisualStyleBackColor = false;
            this.AcceptRecordButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(6, 482);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(452, 2);
            this.FormLinePanel.TabIndex = 164;
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AccessibleDescription = "0";
            this.FormIDLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(7, 487);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(35, 15);
            this.FormIDLabel.TabIndex = 206;
            this.FormIDLabel.Text = "9612";
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.HelpLink.BackColor = System.Drawing.Color.White;
            this.HelpLink.FormId = "9612";
            this.HelpLink.Location = new System.Drawing.Point(215, 486);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(43, 27);
            this.HelpLink.TabIndex = 207;
            this.HelpLink.Tag = "9612";
            this.HelpLink.VisibleHelpButton = false;
            this.HelpLink.VisibleHelpLinkButton = true;
            // 
            // DateTime
            // 
            this.DateTime.HeaderText = "DateTime";
            this.DateTime.Name = "DateTime";
            this.DateTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DateTime.Visible = false;
            // 
            // Task
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Task.DefaultCellStyle = dataGridViewCellStyle3;
            this.Task.HeaderText = "Task";
            this.Task.Name = "Task";
            this.Task.ReadOnly = true;
            this.Task.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Task.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Task.Width = 413;
            // 
            // UserId
            // 
            this.UserId.HeaderText = "UserId";
            this.UserId.Name = "UserId";
            this.UserId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.UserId.Visible = false;
            // 
            // Parameter4
            // 
            this.Parameter4.HeaderText = "Parameter4";
            this.Parameter4.Name = "Parameter4";
            this.Parameter4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Parameter4.Visible = false;
            // 
            // Parameter3
            // 
            this.Parameter3.HeaderText = "Parameter3";
            this.Parameter3.Name = "Parameter3";
            this.Parameter3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Parameter3.Visible = false;
            // 
            // Parameter2
            // 
            this.Parameter2.HeaderText = "Parameter2";
            this.Parameter2.Name = "Parameter2";
            this.Parameter2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Parameter2.Visible = false;
            // 
            // Parameter1
            // 
            this.Parameter1.HeaderText = "Parameter1";
            this.Parameter1.Name = "Parameter1";
            this.Parameter1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Parameter1.Visible = false;
            // 
            // FormNumber
            // 
            this.FormNumber.HeaderText = "FormNumber";
            this.FormNumber.Name = "FormNumber";
            this.FormNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FormNumber.Visible = false;
            // 
            // F9612
            // 
            this.AccessibleName = "Task Queue";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(465, 505);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.HistoryPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F9612";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "9612";
            this.Text = "TerraScan T2 - Task History";
            this.Load += new System.EventHandler(this.F9612_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.F9612_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.RecordHistoryGridView)).EndInit();
            this.HistoryPanel.ResumeLayout(false);
            this.HistoryPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      

        

        #endregion

        private TerraScan.UI.Controls.TerraScanDataGridView RecordHistoryGridView;
        private System.Windows.Forms.Panel HistoryPanel;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.VScrollBar LayoutMgmtVerticalScroll;
        private TerraScan.UI.Controls.TerraScanButton CloseButton;
        private TerraScan.UI.Controls.TerraScanButton AcceptRecordButton;
        private TerraScan.SmartParts.HelpSmartPart HelpLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter4;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Task;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTime;
    }
}