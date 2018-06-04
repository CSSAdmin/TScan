namespace D8000
{
    partial class F8002
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8002));
            this.ActiveRecordVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.RecordMatchLabel = new System.Windows.Forms.Label();
            this.ScrollPanel = new System.Windows.Forms.Panel();
            this.DistrictLinePanel = new System.Windows.Forms.Panel();
            this.CloseButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ManagementButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ActiveWorkRecordDataGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.AcceptActiveRecordButton = new TerraScan.UI.Controls.TerraScanButton();
            this.WOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WODate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WOType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScrollPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActiveWorkRecordDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ActiveRecordVerticalScroll
            // 
            this.ActiveRecordVerticalScroll.Location = new System.Drawing.Point(0, -1);
            this.ActiveRecordVerticalScroll.Name = "ActiveRecordVerticalScroll";
            this.ActiveRecordVerticalScroll.Size = new System.Drawing.Size(17, 238);
            this.ActiveRecordVerticalScroll.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(13, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 108;
            this.label1.Text = "8002";
            // 
            // RecordMatchLabel
            // 
            this.RecordMatchLabel.AutoSize = true;
            this.RecordMatchLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordMatchLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.RecordMatchLabel.Location = new System.Drawing.Point(444, 295);
            this.RecordMatchLabel.Name = "RecordMatchLabel";
            this.RecordMatchLabel.Size = new System.Drawing.Size(0, 15);
            this.RecordMatchLabel.TabIndex = 113;
            // 
            // ScrollPanel
            // 
            this.ScrollPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScrollPanel.Controls.Add(this.ActiveRecordVerticalScroll);
            this.ScrollPanel.Location = new System.Drawing.Point(561, 7);
            this.ScrollPanel.Name = "ScrollPanel";
            this.ScrollPanel.Size = new System.Drawing.Size(18, 239);
            this.ScrollPanel.TabIndex = 114;
            this.ScrollPanel.TabStop = true;
            // 
            // DistrictLinePanel
            // 
            this.DistrictLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.DistrictLinePanel.Location = new System.Drawing.Point(11, 289);
            this.DistrictLinePanel.Name = "DistrictLinePanel";
            this.DistrictLinePanel.Size = new System.Drawing.Size(568, 2);
            this.DistrictLinePanel.TabIndex = 115;
            // 
            // CloseButton
            // 
            this.CloseButton.ActualPermission = false;
            this.CloseButton.ApplyDisableBehaviour = false;
            this.CloseButton.AutoSize = true;
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CloseButton.BorderColor = System.Drawing.Color.Wheat;
            this.CloseButton.CommentPriority = false;
            this.CloseButton.EnableAutoPrint = false;
            this.CloseButton.FilterStatus = false;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.FocusRectangleEnabled = true;
            this.CloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.ImageSelected = false;
            this.CloseButton.Location = new System.Drawing.Point(468, 252);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.NewPadding = 5;
            this.CloseButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.CloseButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CloseButton.Size = new System.Drawing.Size(110, 30);
            this.CloseButton.StatusIndicator = false;
            this.CloseButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CloseButton.StatusOffText = null;
            this.CloseButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CloseButton.StatusOnText = null;
            this.CloseButton.TabIndex = 3;
            this.CloseButton.TabStop = false;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ManagementButton
            // 
            this.ManagementButton.ActualPermission = false;
            this.ManagementButton.ApplyDisableBehaviour = false;
            this.ManagementButton.AutoSize = true;
            this.ManagementButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ManagementButton.BorderColor = System.Drawing.Color.Wheat;
            this.ManagementButton.CommentPriority = false;
            this.ManagementButton.EnableAutoPrint = false;
            this.ManagementButton.FilterStatus = false;
            this.ManagementButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ManagementButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ManagementButton.FocusRectangleEnabled = true;
            this.ManagementButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManagementButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ManagementButton.ImageSelected = false;
            this.ManagementButton.Location = new System.Drawing.Point(128, 252);
            this.ManagementButton.Name = "ManagementButton";
            this.ManagementButton.NewPadding = 5;
            this.ManagementButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.ManagementButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ManagementButton.Size = new System.Drawing.Size(110, 30);
            this.ManagementButton.StatusIndicator = false;
            this.ManagementButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ManagementButton.StatusOffText = null;
            this.ManagementButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ManagementButton.StatusOnText = null;
            this.ManagementButton.TabIndex = 2;
            this.ManagementButton.TabStop = false;
            this.ManagementButton.Text = "Management";
            this.ManagementButton.UseVisualStyleBackColor = false;
            this.ManagementButton.Click += new System.EventHandler(this.ManagementButton_Click);
            // 
            // ActiveWorkRecordDataGridView
            // 
            this.ActiveWorkRecordDataGridView.AllowCellClick = true;
            this.ActiveWorkRecordDataGridView.AllowDoubleClick = true;
            this.ActiveWorkRecordDataGridView.AllowEmptyRows = true;
            this.ActiveWorkRecordDataGridView.AllowEnterKey = true;
            this.ActiveWorkRecordDataGridView.AllowSorting = true;
            this.ActiveWorkRecordDataGridView.AllowUserToAddRows = false;
            this.ActiveWorkRecordDataGridView.AllowUserToDeleteRows = false;
            this.ActiveWorkRecordDataGridView.AllowUserToResizeColumns = false;
            this.ActiveWorkRecordDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.ActiveWorkRecordDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ActiveWorkRecordDataGridView.ApplyStandardBehaviour = false;
            this.ActiveWorkRecordDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.ActiveWorkRecordDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ActiveWorkRecordDataGridView.ClearCurrentCellOnLeave = false;
            this.ActiveWorkRecordDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ActiveWorkRecordDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ActiveWorkRecordDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ActiveWorkRecordDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WOID,
            this.WODate,
            this.WOType,
            this.Comments});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ActiveWorkRecordDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.ActiveWorkRecordDataGridView.DefaultRowIndex = -1;
            this.ActiveWorkRecordDataGridView.DeselectCurrentCell = false;
            this.ActiveWorkRecordDataGridView.DeselectSpecifiedRow = -1;
            this.ActiveWorkRecordDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ActiveWorkRecordDataGridView.EnableBinding = false;
            this.ActiveWorkRecordDataGridView.EnableHeadersVisualStyles = false;
            this.ActiveWorkRecordDataGridView.GridColor = System.Drawing.Color.Black;
            this.ActiveWorkRecordDataGridView.GridContentSelected = false;
            this.ActiveWorkRecordDataGridView.IsEditableGrid = false;
            this.ActiveWorkRecordDataGridView.IsSorted = false;
            this.ActiveWorkRecordDataGridView.Location = new System.Drawing.Point(11, 7);
            this.ActiveWorkRecordDataGridView.MultiSelect = false;
            this.ActiveWorkRecordDataGridView.Name = "ActiveWorkRecordDataGridView";
            this.ActiveWorkRecordDataGridView.NumRowsVisible = 10;
            this.ActiveWorkRecordDataGridView.PrimaryKeyColumnName = "";
            this.ActiveWorkRecordDataGridView.RemainSortFields = false;
            this.ActiveWorkRecordDataGridView.RemoveDefaultSelection = false;
            this.ActiveWorkRecordDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ActiveWorkRecordDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.ActiveWorkRecordDataGridView.RowHeadersWidth = 20;
            this.ActiveWorkRecordDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ActiveWorkRecordDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ActiveWorkRecordDataGridView.Size = new System.Drawing.Size(568, 239);
            this.ActiveWorkRecordDataGridView.TabIndex = 8;
            this.ActiveWorkRecordDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ActiveWorkRecordDataGridView_KeyDown);
            this.ActiveWorkRecordDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ActiveWorkRecordDataGridView_CellDoubleClick);
            this.ActiveWorkRecordDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ActiveWorkRecordDataGridView_CellDoubleClick);
            // 
            // AcceptActiveRecordButton
            // 
            this.AcceptActiveRecordButton.ActualPermission = false;
            this.AcceptActiveRecordButton.ApplyDisableBehaviour = false;
            this.AcceptActiveRecordButton.AutoSize = true;
            this.AcceptActiveRecordButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AcceptActiveRecordButton.BorderColor = System.Drawing.Color.Wheat;
            this.AcceptActiveRecordButton.CommentPriority = false;
            this.AcceptActiveRecordButton.EnableAutoPrint = false;
            this.AcceptActiveRecordButton.FilterStatus = false;
            this.AcceptActiveRecordButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AcceptActiveRecordButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AcceptActiveRecordButton.FocusRectangleEnabled = true;
            this.AcceptActiveRecordButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptActiveRecordButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AcceptActiveRecordButton.ImageSelected = false;
            this.AcceptActiveRecordButton.Location = new System.Drawing.Point(12, 252);
            this.AcceptActiveRecordButton.Name = "AcceptActiveRecordButton";
            this.AcceptActiveRecordButton.NewPadding = 5;
            this.AcceptActiveRecordButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.AcceptActiveRecordButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AcceptActiveRecordButton.Size = new System.Drawing.Size(110, 30);
            this.AcceptActiveRecordButton.StatusIndicator = false;
            this.AcceptActiveRecordButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AcceptActiveRecordButton.StatusOffText = null;
            this.AcceptActiveRecordButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AcceptActiveRecordButton.StatusOnText = null;
            this.AcceptActiveRecordButton.TabIndex = 1;
            this.AcceptActiveRecordButton.TabStop = false;
            this.AcceptActiveRecordButton.Text = "Accept";
            this.AcceptActiveRecordButton.UseVisualStyleBackColor = false;
            this.AcceptActiveRecordButton.Click += new System.EventHandler(this.AcceptActiveRecordButton_Click);
            // 
            // WOID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.WOID.DefaultCellStyle = dataGridViewCellStyle3;
            this.WOID.HeaderText = "WOID";
            this.WOID.Name = "WOID";
            this.WOID.ReadOnly = true;
            this.WOID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.WOID.Width = 65;
            // 
            // WODate
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.WODate.DefaultCellStyle = dataGridViewCellStyle4;
            this.WODate.HeaderText = "Date";
            this.WODate.Name = "WODate";
            this.WODate.ReadOnly = true;
            this.WODate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.WODate.Width = 80;
            // 
            // WOType
            // 
            this.WOType.HeaderText = "Type";
            this.WOType.Name = "WOType";
            this.WOType.ReadOnly = true;
            this.WOType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.WOType.Width = 166;
            // 
            // Comments
            // 
            this.Comments.HeaderText = "Comments";
            this.Comments.Name = "Comments";
            this.Comments.ReadOnly = true;
            this.Comments.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Comments.Width = 220;
            // 
            // F8002
            // 
            this.AcceptButton = this.AcceptActiveRecordButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(590, 316);
            this.Controls.Add(this.AcceptActiveRecordButton);
            this.Controls.Add(this.DistrictLinePanel);
            this.Controls.Add(this.ScrollPanel);
            this.Controls.Add(this.RecordMatchLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ManagementButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ActiveWorkRecordDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F8002";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - Active Work Record";
            this.Load += new System.EventHandler(this.F8002_Load);
            this.ScrollPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ActiveWorkRecordDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanDataGridView ActiveWorkRecordDataGridView;
        private System.Windows.Forms.VScrollBar ActiveRecordVerticalScroll;
        private System.Windows.Forms.Label label1;
        private TerraScan.UI.Controls.TerraScanButton ManagementButton;
        private TerraScan.UI.Controls.TerraScanButton CloseButton;
        private System.Windows.Forms.Label RecordMatchLabel;
        private System.Windows.Forms.Panel ScrollPanel;
        private System.Windows.Forms.Panel DistrictLinePanel;
        private TerraScan.UI.Controls.TerraScanButton AcceptActiveRecordButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn WOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WODate;
        private System.Windows.Forms.DataGridViewTextBoxColumn WOType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comments;
    }
}