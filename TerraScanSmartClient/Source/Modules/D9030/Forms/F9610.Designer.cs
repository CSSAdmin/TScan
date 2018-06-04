namespace D9030
{
	partial class F9610
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9610));
            this.FindPanelPanel = new System.Windows.Forms.Panel();
            this.FindTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.FindLabel = new System.Windows.Forms.Label();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.RecordCountLabel = new System.Windows.Forms.Label();
            this.ItemTextBox = new System.Windows.Forms.Label();
            this.HelpLink = new System.Windows.Forms.LinkLabel();
            this.GridviewPanel = new System.Windows.Forms.Panel();
            this.FindListDataGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MasterNameVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.PreviewButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CloseButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FindButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FindPanelPanel.SuspendLayout();
            this.GridviewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FindListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FindPanelPanel
            // 
            this.FindPanelPanel.BackColor = System.Drawing.Color.White;
            this.FindPanelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FindPanelPanel.Controls.Add(this.FindTextBox);
            this.FindPanelPanel.Controls.Add(this.FindLabel);
            this.FindPanelPanel.Location = new System.Drawing.Point(12, 12);
            this.FindPanelPanel.Name = "FindPanelPanel";
            this.FindPanelPanel.Size = new System.Drawing.Size(336, 38);
            this.FindPanelPanel.TabIndex = 0;
            this.FindPanelPanel.TabStop = true;
            // 
            // FindTextBox
            // 
            this.FindTextBox.AllowClick = true;
            this.FindTextBox.AllowNegativeSign = false;
            this.FindTextBox.ApplyCFGFormat = false;
            this.FindTextBox.ApplyCurrencyFormat = false;
            this.FindTextBox.ApplyFocusColor = true;
            this.FindTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.FindTextBox.ApplyNegativeStandard = true;
            this.FindTextBox.ApplyParentFocusColor = true;
            this.FindTextBox.ApplyTimeFormat = false;
            this.FindTextBox.BackColor = System.Drawing.Color.White;
            this.FindTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FindTextBox.CFromatWihoutSymbol = false;
            this.FindTextBox.CheckForEmpty = false;
            this.FindTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FindTextBox.Digits = -1;
            this.FindTextBox.EmptyDecimalValue = false;
            this.FindTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.FindTextBox.ForeColor = System.Drawing.Color.Black;
            this.FindTextBox.IsEditable = false;
            this.FindTextBox.IsQueryableFileld = false;
            this.FindTextBox.Location = new System.Drawing.Point(14, 15);
            this.FindTextBox.LockKeyPress = false;
            this.FindTextBox.MaxLength = 50;
            this.FindTextBox.Name = "FindTextBox";
            this.FindTextBox.PersistDefaultColor = false;
            this.FindTextBox.Precision = 2;
            this.FindTextBox.QueryingFileldName = "";
            this.FindTextBox.SetColorFlag = false;
            this.FindTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FindTextBox.Size = new System.Drawing.Size(314, 16);
            this.FindTextBox.SpecialCharacter = "%";
            this.FindTextBox.TabIndex = 1;
            this.FindTextBox.TextCustomFormat = "$#,##0.00";
            this.FindTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.FindTextBox.WholeInteger = false;
            this.FindTextBox.TextChanged += new System.EventHandler(this.FindTextBox_TextChanged);
            this.FindTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindTextBox_KeyDown);
            // 
            // FindLabel
            // 
            this.FindLabel.AutoSize = true;
            this.FindLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FindLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.FindLabel.Location = new System.Drawing.Point(1, -1);
            this.FindLabel.Name = "FindLabel";
            this.FindLabel.Size = new System.Drawing.Size(33, 14);
            this.FindLabel.TabIndex = 0;
            this.FindLabel.Text = "Find:";
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(12, 335);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(334, 2);
            this.FormLinePanel.TabIndex = 115;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(9, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 116;
            this.label1.Text = "9610";
            // 
            // RecordCountLabel
            // 
            this.RecordCountLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordCountLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.RecordCountLabel.Location = new System.Drawing.Point(155, 342);
            this.RecordCountLabel.Name = "RecordCountLabel";
            this.RecordCountLabel.Size = new System.Drawing.Size(193, 15);
            this.RecordCountLabel.TabIndex = 8;
            this.RecordCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ItemTextBox
            // 
            this.ItemTextBox.AutoSize = true;
            this.ItemTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ItemTextBox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemTextBox.ForeColor = System.Drawing.Color.Maroon;
            this.ItemTextBox.Location = new System.Drawing.Point(117, 350);
            this.ItemTextBox.Name = "ItemTextBox";
            this.ItemTextBox.Size = new System.Drawing.Size(0, 14);
            this.ItemTextBox.TabIndex = 119;
            this.ItemTextBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ItemTextBox.Visible = false;
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpLink.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLink.Location = new System.Drawing.Point(142, 341);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(56, 15);
            this.HelpLink.TabIndex = 7;
            this.HelpLink.TabStop = true;
            this.HelpLink.Text = "Help";
            this.HelpLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // GridviewPanel
            // 
            this.GridviewPanel.BackColor = System.Drawing.Color.White;
            this.GridviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridviewPanel.Controls.Add(this.FindListDataGridView);
            this.GridviewPanel.Location = new System.Drawing.Point(12, 49);
            this.GridviewPanel.Name = "GridviewPanel";
            this.GridviewPanel.Size = new System.Drawing.Size(336, 239);
            this.GridviewPanel.TabIndex = 2;
            this.GridviewPanel.TabStop = true;
            // 
            // FindListDataGridView
            // 
            this.FindListDataGridView.AllowCellClick = true;
            this.FindListDataGridView.AllowDoubleClick = true;
            this.FindListDataGridView.AllowEmptyRows = true;
            this.FindListDataGridView.AllowEnterKey = false;
            this.FindListDataGridView.AllowSorting = true;
            this.FindListDataGridView.AllowUserToAddRows = false;
            this.FindListDataGridView.AllowUserToDeleteRows = false;
            this.FindListDataGridView.AllowUserToResizeColumns = false;
            this.FindListDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.FindListDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.FindListDataGridView.ApplyStandardBehaviour = false;
            this.FindListDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.FindListDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FindListDataGridView.ClearCurrentCellOnLeave = false;
            this.FindListDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FindListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.FindListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FindListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item,
            this.Key});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.FindListDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.FindListDataGridView.DefaultRowIndex = -1;
            this.FindListDataGridView.DeselectCurrentCell = false;
            this.FindListDataGridView.DeselectSpecifiedRow = -1;
            this.FindListDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.FindListDataGridView.EnableBinding = false;
            this.FindListDataGridView.EnableHeadersVisualStyles = false;
            this.FindListDataGridView.GridColor = System.Drawing.Color.Black;
            this.FindListDataGridView.GridContentSelected = false;
            this.FindListDataGridView.IsEditableGrid = false;
            this.FindListDataGridView.IsMultiSelect = false;
            this.FindListDataGridView.IsSorted = true;
            this.FindListDataGridView.Location = new System.Drawing.Point(-1, -1);
            this.FindListDataGridView.MultiSelect = false;
            this.FindListDataGridView.Name = "FindListDataGridView";
            this.FindListDataGridView.NumRowsVisible = 10;
            this.FindListDataGridView.PrimaryKeyColumnName = "";
            this.FindListDataGridView.RemainSortFields = false;
            this.FindListDataGridView.RemoveDefaultSelection = false;
            this.FindListDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FindListDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.FindListDataGridView.RowHeadersWidth = 20;
            this.FindListDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.FindListDataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.FindListDataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.FindListDataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.FindListDataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.FindListDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FindListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FindListDataGridView.Size = new System.Drawing.Size(335, 240);
            this.FindListDataGridView.StandardTab = true;
            this.FindListDataGridView.TabIndex = 3;
            this.FindListDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FindListDataGridView_CellDoubleClick);
            this.FindListDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.FindListDataGridView_CellFormatting);
            this.FindListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FindListDataGridView_CellClick);
            this.FindListDataGridView.SelectionChanged += new System.EventHandler(this.FindListDataGridView_SelectionChanged);
            this.FindListDataGridView.VisibleChanged += new System.EventHandler(this.FindListDataGridView_VisibleChanged);
            // 
            // Item
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Item.DefaultCellStyle = dataGridViewCellStyle3;
            this.Item.HeaderText = "Results";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Item.Width = 298;
            // 
            // Key
            // 
            this.Key.HeaderText = "KeyID";
            this.Key.Name = "Key";
            this.Key.Visible = false;
            // 
            // MasterNameVerticalScroll
            // 
            this.MasterNameVerticalScroll.Enabled = false;
            this.MasterNameVerticalScroll.Location = new System.Drawing.Point(330, 50);
            this.MasterNameVerticalScroll.Name = "MasterNameVerticalScroll";
            this.MasterNameVerticalScroll.Size = new System.Drawing.Size(17, 238);
            this.MasterNameVerticalScroll.TabIndex = 122;
            // 
            // PreviewButton
            // 
            this.PreviewButton.ActualPermission = false;
            this.PreviewButton.ApplyDisableBehaviour = false;
            this.PreviewButton.AutoSize = true;
            this.PreviewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.PreviewButton.BorderColor = System.Drawing.Color.Wheat;
            this.PreviewButton.CommentPriority = false;
            this.PreviewButton.EnableAutoPrint = false;
            this.PreviewButton.FilterStatus = false;
            this.PreviewButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.PreviewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreviewButton.FocusRectangleEnabled = true;
            this.PreviewButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviewButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PreviewButton.ImageSelected = false;
            this.PreviewButton.Location = new System.Drawing.Point(136, 293);
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.NewPadding = 5;
            this.PreviewButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.PreviewButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.PreviewButton.Size = new System.Drawing.Size(90, 35);
            this.PreviewButton.StatusIndicator = false;
            this.PreviewButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PreviewButton.StatusOffText = null;
            this.PreviewButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.PreviewButton.StatusOnText = null;
            this.PreviewButton.TabIndex = 5;
            this.PreviewButton.Tag = "Preview";
            this.PreviewButton.Text = "Preview";
            this.PreviewButton.UseVisualStyleBackColor = false;
            this.PreviewButton.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.ActualPermission = false;
            this.CloseButton.ApplyDisableBehaviour = false;
            this.CloseButton.AutoSize = true;
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
            this.CloseButton.Location = new System.Drawing.Point(258, 294);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.NewPadding = 5;
            this.CloseButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.CloseButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CloseButton.Size = new System.Drawing.Size(90, 35);
            this.CloseButton.StatusIndicator = false;
            this.CloseButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CloseButton.StatusOffText = null;
            this.CloseButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CloseButton.StatusOnText = null;
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Tag = "Close";
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // FindButton
            // 
            this.FindButton.ActualPermission = false;
            this.FindButton.ApplyDisableBehaviour = false;
            this.FindButton.AutoSize = true;
            this.FindButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.FindButton.BorderColor = System.Drawing.Color.Wheat;
            this.FindButton.CommentPriority = false;
            this.FindButton.EnableAutoPrint = false;
            this.FindButton.FilterStatus = false;
            this.FindButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.FindButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindButton.FocusRectangleEnabled = true;
            this.FindButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FindButton.ImageSelected = false;
            this.FindButton.Location = new System.Drawing.Point(12, 294);
            this.FindButton.Name = "FindButton";
            this.FindButton.NewPadding = 5;
            this.FindButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.FindButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.FindButton.Size = new System.Drawing.Size(90, 35);
            this.FindButton.StatusIndicator = false;
            this.FindButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FindButton.StatusOffText = null;
            this.FindButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.FindButton.StatusOnText = null;
            this.FindButton.TabIndex = 4;
            this.FindButton.Text = "Find";
            this.FindButton.UseVisualStyleBackColor = false;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // F9610
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(360, 362);
            this.Controls.Add(this.MasterNameVerticalScroll);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.ItemTextBox);
            this.Controls.Add(this.RecordCountLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.PreviewButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.GridviewPanel);
            this.Controls.Add(this.FindPanelPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F9610";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "9610";
            this.Text = "TerraScan T2 - Quick Find";
            this.Load += new System.EventHandler(this.F9610_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F9610_FormClosing);
            this.FindPanelPanel.ResumeLayout(false);
            this.FindPanelPanel.PerformLayout();
            this.GridviewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FindListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Panel FindPanelPanel;
        private System.Windows.Forms.Label FindLabel;
        private TerraScan.UI.Controls.TerraScanTextBox FindTextBox;
        private TerraScan.UI.Controls.TerraScanButton FindButton;
        private TerraScan.UI.Controls.TerraScanButton CloseButton;
        private TerraScan.UI.Controls.TerraScanButton PreviewButton;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label RecordCountLabel;
        private System.Windows.Forms.Label ItemTextBox;
        private System.Windows.Forms.LinkLabel HelpLink;
        private System.Windows.Forms.Panel GridviewPanel;  
        private TerraScan.UI.Controls.TerraScanDataGridView FindListDataGridView;
        private System.Windows.Forms.VScrollBar MasterNameVerticalScroll;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
	}
}