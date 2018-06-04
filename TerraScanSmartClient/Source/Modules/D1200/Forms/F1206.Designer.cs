namespace D1200
{
    partial class F1206
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1206));
            this.PostingErrorsLabel = new System.Windows.Forms.Label();
            this.formIdlabel = new System.Windows.Forms.Label();
            this.requeryFormIDLabel = new System.Windows.Forms.Label();
            this.queryUtilityLinePanel = new System.Windows.Forms.Panel();
            this.PostingErrorsDataGrid = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.Error = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Report = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ErrorTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempSortID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateAccountButton = new TerraScan.UI.Controls.TerraScanButton();
            this.PostingErrorsDataGridPanel = new System.Windows.Forms.Panel();
            this.ScrollPanel = new System.Windows.Forms.Panel();
            this.PostingErrorsVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.CreateSubFundButton = new TerraScan.UI.Controls.TerraScanButton();
            this.PostingErrorsCloseButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ReportText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.PostingErrorsDataGrid)).BeginInit();
            this.PostingErrorsDataGridPanel.SuspendLayout();
            this.ScrollPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PostingErrorsLabel
            // 
            this.PostingErrorsLabel.AutoSize = true;
            this.PostingErrorsLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PostingErrorsLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostingErrorsLabel.ForeColor = System.Drawing.Color.Black;
            this.PostingErrorsLabel.Location = new System.Drawing.Point(12, 4);
            this.PostingErrorsLabel.Name = "PostingErrorsLabel";
            this.PostingErrorsLabel.Size = new System.Drawing.Size(373, 16);
            this.PostingErrorsLabel.TabIndex = 63;
            this.PostingErrorsLabel.Text = "The following errors occurred during the Posting Process:";
            // 
            // formIdlabel
            // 
            this.formIdlabel.AutoSize = true;
            this.formIdlabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formIdlabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.formIdlabel.Location = new System.Drawing.Point(13, 202);
            this.formIdlabel.Name = "formIdlabel";
            this.formIdlabel.Size = new System.Drawing.Size(35, 15);
            this.formIdlabel.TabIndex = 148;
            this.formIdlabel.Text = "1206";
            // 
            // requeryFormIDLabel
            // 
            this.requeryFormIDLabel.AutoSize = true;
            this.requeryFormIDLabel.Location = new System.Drawing.Point(5, 199);
            this.requeryFormIDLabel.Name = "requeryFormIDLabel";
            this.requeryFormIDLabel.Size = new System.Drawing.Size(0, 13);
            this.requeryFormIDLabel.TabIndex = 147;
            // 
            // queryUtilityLinePanel
            // 
            this.queryUtilityLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.queryUtilityLinePanel.Location = new System.Drawing.Point(16, 197);
            this.queryUtilityLinePanel.Name = "queryUtilityLinePanel";
            this.queryUtilityLinePanel.Size = new System.Drawing.Size(397, 2);
            this.queryUtilityLinePanel.TabIndex = 146;
            // 
            // PostingErrorsDataGrid
            // 
            this.PostingErrorsDataGrid.AllowCellClick = true;
            this.PostingErrorsDataGrid.AllowDoubleClick = true;
            this.PostingErrorsDataGrid.AllowEmptyRows = true;
            this.PostingErrorsDataGrid.AllowEnterKey = false;
            this.PostingErrorsDataGrid.AllowSorting = true;
            this.PostingErrorsDataGrid.AllowUserToAddRows = false;
            this.PostingErrorsDataGrid.AllowUserToDeleteRows = false;
            this.PostingErrorsDataGrid.AllowUserToResizeColumns = false;
            this.PostingErrorsDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.PostingErrorsDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.PostingErrorsDataGrid.ApplyStandardBehaviour = false;
            this.PostingErrorsDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.PostingErrorsDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PostingErrorsDataGrid.ClearCurrentCellOnLeave = false;
            this.PostingErrorsDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PostingErrorsDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.PostingErrorsDataGrid.ColumnHeadersHeight = 24;
            this.PostingErrorsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.PostingErrorsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Error,
            this.Report,
            this.ErrorTypeID,
            this.TempSortID,
            this.ReportText});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PostingErrorsDataGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.PostingErrorsDataGrid.DefaultRowIndex = -1;
            this.PostingErrorsDataGrid.DeselectCurrentCell = false;
            this.PostingErrorsDataGrid.DeselectSpecifiedRow = -1;
            this.PostingErrorsDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.PostingErrorsDataGrid.EnableBinding = true;
            this.PostingErrorsDataGrid.EnableHeadersVisualStyles = false;
            this.PostingErrorsDataGrid.GridColor = System.Drawing.Color.Black;
            this.PostingErrorsDataGrid.GridContentSelected = false;
            this.PostingErrorsDataGrid.IsEditableGrid = false;
            this.PostingErrorsDataGrid.IsSorted = false;
            this.PostingErrorsDataGrid.Location = new System.Drawing.Point(-1, -1);
            this.PostingErrorsDataGrid.MultiSelect = false;
            this.PostingErrorsDataGrid.Name = "PostingErrorsDataGrid";
            this.PostingErrorsDataGrid.NumRowsVisible = 4;
            this.PostingErrorsDataGrid.PrimaryKeyColumnName = "TempSortID";
            this.PostingErrorsDataGrid.ReadOnly = true;
            this.PostingErrorsDataGrid.RemainSortFields = false;
            this.PostingErrorsDataGrid.RemoveDefaultSelection = false;
            this.PostingErrorsDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PostingErrorsDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.PostingErrorsDataGrid.RowHeadersWidth = 20;
            this.PostingErrorsDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PostingErrorsDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PostingErrorsDataGrid.Size = new System.Drawing.Size(399, 112);
            this.PostingErrorsDataGrid.StandardTab = true;
            this.PostingErrorsDataGrid.TabIndex = 154;
            this.PostingErrorsDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PostingErrorsDataGrid_CellClick);
            this.PostingErrorsDataGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.PostingErrorsDataGrid_RowEnter);
            this.PostingErrorsDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.PostingErrorsDataGrid_CellFormatting);
            this.PostingErrorsDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PostingErrorsDataGrid_CellContentClick);
            // 
            // Error
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Error.DefaultCellStyle = dataGridViewCellStyle3;
            this.Error.HeaderText = "Error";
            this.Error.Name = "Error";
            this.Error.ReadOnly = true;
            this.Error.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Error.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Error.Width = 268;
            // 
            // Report
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Report.DefaultCellStyle = dataGridViewCellStyle4;
            this.Report.HeaderText = "Link";
            this.Report.Name = "Report";
            this.Report.ReadOnly = true;
            this.Report.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Report.Width = 94;
            // 
            // ErrorTypeID
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorTypeID.DefaultCellStyle = dataGridViewCellStyle5;
            this.ErrorTypeID.HeaderText = "ErrorTypeID";
            this.ErrorTypeID.Name = "ErrorTypeID";
            this.ErrorTypeID.ReadOnly = true;
            this.ErrorTypeID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ErrorTypeID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ErrorTypeID.Visible = false;
            // 
            // TempSortID
            // 
            this.TempSortID.HeaderText = "TempSortID";
            this.TempSortID.Name = "TempSortID";
            this.TempSortID.ReadOnly = true;
            this.TempSortID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TempSortID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.TempSortID.Visible = false;
            // 
            // CreateAccountButton
            // 
            this.CreateAccountButton.ActualPermission = false;
            this.CreateAccountButton.ApplyDisableBehaviour = false;
            this.CreateAccountButton.AutoSize = true;
            this.CreateAccountButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CreateAccountButton.BorderColor = System.Drawing.Color.Wheat;
            this.CreateAccountButton.CommentPriority = false;
            this.CreateAccountButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CreateAccountButton.EnableAutoPrint = false;
            this.CreateAccountButton.FilterStatus = false;
            this.CreateAccountButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CreateAccountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateAccountButton.FocusRectangleEnabled = true;
            this.CreateAccountButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateAccountButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CreateAccountButton.ImageSelected = false;
            this.CreateAccountButton.Location = new System.Drawing.Point(32, 158);
            this.CreateAccountButton.Name = "CreateAccountButton";
            this.CreateAccountButton.NewPadding = 5;
            this.CreateAccountButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CreateAccountButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CreateAccountButton.Size = new System.Drawing.Size(110, 30);
            this.CreateAccountButton.StatusIndicator = false;
            this.CreateAccountButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CreateAccountButton.StatusOffText = null;
            this.CreateAccountButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CreateAccountButton.StatusOnText = null;
            this.CreateAccountButton.TabIndex = 151;
            this.CreateAccountButton.TabStop = false;
            this.CreateAccountButton.Text = "Create Accts";
            this.CreateAccountButton.UseVisualStyleBackColor = false;
            this.CreateAccountButton.Click += new System.EventHandler(this.CreateAccountButton_Click);
            // 
            // PostingErrorsDataGridPanel
            // 
            this.PostingErrorsDataGridPanel.BackColor = System.Drawing.Color.White;
            this.PostingErrorsDataGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PostingErrorsDataGridPanel.Controls.Add(this.ScrollPanel);
            this.PostingErrorsDataGridPanel.Controls.Add(this.PostingErrorsDataGrid);
            this.PostingErrorsDataGridPanel.Location = new System.Drawing.Point(17, 32);
            this.PostingErrorsDataGridPanel.Name = "PostingErrorsDataGridPanel";
            this.PostingErrorsDataGridPanel.Size = new System.Drawing.Size(401, 111);
            this.PostingErrorsDataGridPanel.TabIndex = 155;
            // 
            // ScrollPanel
            // 
            this.ScrollPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScrollPanel.Controls.Add(this.PostingErrorsVerticalScroll);
            this.ScrollPanel.Location = new System.Drawing.Point(380, -1);
            this.ScrollPanel.Name = "ScrollPanel";
            this.ScrollPanel.Size = new System.Drawing.Size(20, 112);
            this.ScrollPanel.TabIndex = 155;
            // 
            // PostingErrorsVerticalScroll
            // 
            this.PostingErrorsVerticalScroll.Enabled = false;
            this.PostingErrorsVerticalScroll.Location = new System.Drawing.Point(-1, -1);
            this.PostingErrorsVerticalScroll.Name = "PostingErrorsVerticalScroll";
            this.PostingErrorsVerticalScroll.Size = new System.Drawing.Size(20, 111);
            this.PostingErrorsVerticalScroll.TabIndex = 151;
            // 
            // CreateSubFundButton
            // 
            this.CreateSubFundButton.ActualPermission = false;
            this.CreateSubFundButton.ApplyDisableBehaviour = false;
            this.CreateSubFundButton.AutoSize = true;
            this.CreateSubFundButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CreateSubFundButton.BorderColor = System.Drawing.Color.Wheat;
            this.CreateSubFundButton.CommentPriority = false;
            this.CreateSubFundButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CreateSubFundButton.EnableAutoPrint = false;
            this.CreateSubFundButton.FilterStatus = false;
            this.CreateSubFundButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CreateSubFundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateSubFundButton.FocusRectangleEnabled = true;
            this.CreateSubFundButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateSubFundButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CreateSubFundButton.ImageSelected = false;
            this.CreateSubFundButton.Location = new System.Drawing.Point(163, 158);
            this.CreateSubFundButton.Name = "CreateSubFundButton";
            this.CreateSubFundButton.NewPadding = 5;
            this.CreateSubFundButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CreateSubFundButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CreateSubFundButton.Size = new System.Drawing.Size(110, 30);
            this.CreateSubFundButton.StatusIndicator = false;
            this.CreateSubFundButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CreateSubFundButton.StatusOffText = null;
            this.CreateSubFundButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CreateSubFundButton.StatusOnText = null;
            this.CreateSubFundButton.TabIndex = 156;
            this.CreateSubFundButton.TabStop = false;
            this.CreateSubFundButton.Text = "Create SubFund";
            this.CreateSubFundButton.UseVisualStyleBackColor = false;
            this.CreateSubFundButton.Click += new System.EventHandler(this.CreateAccountButton_Click);
            // 
            // PostingErrorsCloseButton
            // 
            this.PostingErrorsCloseButton.ActualPermission = false;
            this.PostingErrorsCloseButton.ApplyDisableBehaviour = false;
            this.PostingErrorsCloseButton.AutoSize = true;
            this.PostingErrorsCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.PostingErrorsCloseButton.BorderColor = System.Drawing.Color.Wheat;
            this.PostingErrorsCloseButton.CommentPriority = false;
            this.PostingErrorsCloseButton.EnableAutoPrint = false;
            this.PostingErrorsCloseButton.FilterStatus = false;
            this.PostingErrorsCloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.PostingErrorsCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PostingErrorsCloseButton.FocusRectangleEnabled = true;
            this.PostingErrorsCloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostingErrorsCloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PostingErrorsCloseButton.ImageSelected = false;
            this.PostingErrorsCloseButton.Location = new System.Drawing.Point(293, 158);
            this.PostingErrorsCloseButton.Name = "PostingErrorsCloseButton";
            this.PostingErrorsCloseButton.NewPadding = 5;
            this.PostingErrorsCloseButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.PostingErrorsCloseButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.PostingErrorsCloseButton.Size = new System.Drawing.Size(110, 30);
            this.PostingErrorsCloseButton.StatusIndicator = false;
            this.PostingErrorsCloseButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PostingErrorsCloseButton.StatusOffText = null;
            this.PostingErrorsCloseButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.PostingErrorsCloseButton.StatusOnText = null;
            this.PostingErrorsCloseButton.TabIndex = 157;
            this.PostingErrorsCloseButton.TabStop = false;
            this.PostingErrorsCloseButton.Text = "Close";
            this.PostingErrorsCloseButton.UseVisualStyleBackColor = false;
            this.PostingErrorsCloseButton.Click += new System.EventHandler(this.PostingErrorsCloselButton_Click);
            // 
            // ReportText
            // 
            this.ReportText.HeaderText = "ReportText";
            this.ReportText.Name = "ReportText";
            this.ReportText.ReadOnly = true;
            this.ReportText.Visible = false;
            // 
            // F1206
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(429, 226);
            this.Controls.Add(this.PostingErrorsCloseButton);
            this.Controls.Add(this.CreateSubFundButton);
            this.Controls.Add(this.PostingErrorsDataGridPanel);
            this.Controls.Add(this.CreateAccountButton);
            this.Controls.Add(this.formIdlabel);
            this.Controls.Add(this.requeryFormIDLabel);
            this.Controls.Add(this.queryUtilityLinePanel);
            this.Controls.Add(this.PostingErrorsLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(437, 260);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(437, 260);
            this.Name = "F1206";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "1206";
            this.Text = "TerraScan T2 - Posting Errors";
            this.Load += new System.EventHandler(this.F1206_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PostingErrorsDataGrid)).EndInit();
            this.PostingErrorsDataGridPanel.ResumeLayout(false);
            this.ScrollPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PostingErrorsLabel;
        private System.Windows.Forms.Label formIdlabel;
        private System.Windows.Forms.Label requeryFormIDLabel;
        private System.Windows.Forms.Panel queryUtilityLinePanel;
        private TerraScan.UI.Controls.TerraScanButton CreateAccountButton;
        private TerraScan.UI.Controls.TerraScanDataGridView PostingErrorsDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Error;
        private System.Windows.Forms.DataGridViewLinkColumn Report;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorTypeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempSortID;
        private System.Windows.Forms.Panel PostingErrorsDataGridPanel;
        private System.Windows.Forms.Panel ScrollPanel;
        private System.Windows.Forms.VScrollBar PostingErrorsVerticalScroll;
        private TerraScan.UI.Controls.TerraScanButton CreateSubFundButton;
        private TerraScan.UI.Controls.TerraScanButton PostingErrorsCloseButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportText;
    }
}