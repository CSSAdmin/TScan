namespace D1410
{
    partial class F1411
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1411));
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ParcelStmtLabel = new System.Windows.Forms.Label();
            this.ParcelStmtSearchPanel = new System.Windows.Forms.Panel();
            this.MasterNameVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.ParcelStmtSearchGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.ParcelStmtTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.ParcelTextBoxPanel = new System.Windows.Forms.Panel();
            this.MasterAcceptButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AcceptAllButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SearchButton = new TerraScan.UI.Controls.TerraScanButton();
            this.MasterCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.label1 = new System.Windows.Forms.Label();
            this.RecordCountLabel = new System.Windows.Forms.Label();
            this.StatementID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatementNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Taxpayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RollYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParcelStmtSearchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParcelStmtSearchGridView)).BeginInit();
            this.ParcelTextBoxPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(12, 280);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(555, 2);
            this.FormLinePanel.TabIndex = 5;
            this.FormLinePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.FormLinePanel_Paint);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.White;
            this.MainPanel.Location = new System.Drawing.Point(12, 12);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(555, 211);
            this.MainPanel.TabIndex = 5;
            // 
            // ParcelStmtLabel
            // 
            this.ParcelStmtLabel.AutoSize = true;
            this.ParcelStmtLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ParcelStmtLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ParcelStmtLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ParcelStmtLabel.Location = new System.Drawing.Point(1, 1);
            this.ParcelStmtLabel.Name = "ParcelStmtLabel";
            this.ParcelStmtLabel.Size = new System.Drawing.Size(119, 14);
            this.ParcelStmtLabel.TabIndex = 0;
            this.ParcelStmtLabel.Text = "Parcel or Statement:";
            // 
            // ParcelStmtSearchPanel
            // 
            this.ParcelStmtSearchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ParcelStmtSearchPanel.Controls.Add(this.MasterNameVerticalScroll);
            this.ParcelStmtSearchPanel.Controls.Add(this.ParcelStmtSearchGridView);
            this.ParcelStmtSearchPanel.Location = new System.Drawing.Point(12, 51);
            this.ParcelStmtSearchPanel.Name = "ParcelStmtSearchPanel";
            this.ParcelStmtSearchPanel.Size = new System.Drawing.Size(555, 178);
            this.ParcelStmtSearchPanel.TabIndex = 169;
            // 
            // MasterNameVerticalScroll
            // 
            this.MasterNameVerticalScroll.Location = new System.Drawing.Point(537, 0);
            this.MasterNameVerticalScroll.Name = "MasterNameVerticalScroll";
            this.MasterNameVerticalScroll.Size = new System.Drawing.Size(15, 176);
            this.MasterNameVerticalScroll.TabIndex = 7;
            // 
            // ParcelStmtSearchGridView
            // 
            this.ParcelStmtSearchGridView.AllowCellClick = true;
            this.ParcelStmtSearchGridView.AllowDoubleClick = true;
            this.ParcelStmtSearchGridView.AllowEmptyRows = true;
            this.ParcelStmtSearchGridView.AllowEnterKey = false;
            this.ParcelStmtSearchGridView.AllowSorting = true;
            this.ParcelStmtSearchGridView.AllowUserToAddRows = false;
            this.ParcelStmtSearchGridView.AllowUserToDeleteRows = false;
            this.ParcelStmtSearchGridView.AllowUserToResizeColumns = false;
            this.ParcelStmtSearchGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.ParcelStmtSearchGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ParcelStmtSearchGridView.ApplyStandardBehaviour = false;
            this.ParcelStmtSearchGridView.BackgroundColor = System.Drawing.Color.White;
            this.ParcelStmtSearchGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ParcelStmtSearchGridView.ClearCurrentCellOnLeave = false;
            this.ParcelStmtSearchGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.ParcelStmtSearchGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ParcelStmtSearchGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ParcelStmtSearchGridView.ColumnHeadersHeight = 24;
            this.ParcelStmtSearchGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ParcelStmtSearchGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StatementID,
            this.StatementNumber,
            this.Taxpayer,
            this.PostName,
            this.RollYear});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ParcelStmtSearchGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ParcelStmtSearchGridView.DefaultRowIndex = -1;
            this.ParcelStmtSearchGridView.DeselectCurrentCell = false;
            this.ParcelStmtSearchGridView.DeselectSpecifiedRow = -1;
            this.ParcelStmtSearchGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ParcelStmtSearchGridView.EnableBinding = false;
            this.ParcelStmtSearchGridView.EnableHeadersVisualStyles = false;
            this.ParcelStmtSearchGridView.GridColor = System.Drawing.Color.Black;
            this.ParcelStmtSearchGridView.GridContentSelected = false;
            this.ParcelStmtSearchGridView.IsEditableGrid = false;
            this.ParcelStmtSearchGridView.IsMultiSelect = true;
            this.ParcelStmtSearchGridView.IsSorted = false;
            this.ParcelStmtSearchGridView.Location = new System.Drawing.Point(-1, -1);
            this.ParcelStmtSearchGridView.Name = "ParcelStmtSearchGridView";
            this.ParcelStmtSearchGridView.NumRowsVisible = 7;
            this.ParcelStmtSearchGridView.PrimaryKeyColumnName = "";
            this.ParcelStmtSearchGridView.RemainSortFields = false;
            this.ParcelStmtSearchGridView.RemoveDefaultSelection = false;
            this.ParcelStmtSearchGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ParcelStmtSearchGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ParcelStmtSearchGridView.RowHeadersWidth = 20;
            this.ParcelStmtSearchGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ParcelStmtSearchGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ParcelStmtSearchGridView.ShowEditingIcon = false;
            this.ParcelStmtSearchGridView.Size = new System.Drawing.Size(554, 178);
            this.ParcelStmtSearchGridView.StandardTab = true;
            this.ParcelStmtSearchGridView.TabIndex = 0;
            // 
            // ParcelStmtTextBox
            // 
            this.ParcelStmtTextBox.AllowClick = true;
            this.ParcelStmtTextBox.AllowNegativeSign = false;
            this.ParcelStmtTextBox.ApplyCFGFormat = false;
            this.ParcelStmtTextBox.ApplyCurrencyFormat = false;
            this.ParcelStmtTextBox.ApplyFocusColor = true;
            this.ParcelStmtTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.ParcelStmtTextBox.ApplyNegativeStandard = true;
            this.ParcelStmtTextBox.ApplyParentFocusColor = true;
            this.ParcelStmtTextBox.ApplyTimeFormat = false;
            this.ParcelStmtTextBox.BackColor = System.Drawing.Color.White;
            this.ParcelStmtTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ParcelStmtTextBox.CFromatWihoutSymbol = false;
            this.ParcelStmtTextBox.CheckForEmpty = false;
            this.ParcelStmtTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ParcelStmtTextBox.Digits = -1;
            this.ParcelStmtTextBox.EmptyDecimalValue = false;
            this.ParcelStmtTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.ParcelStmtTextBox.ForeColor = System.Drawing.Color.Black;
            this.ParcelStmtTextBox.IsEditable = false;
            this.ParcelStmtTextBox.IsQueryableFileld = false;
            this.ParcelStmtTextBox.Location = new System.Drawing.Point(15, 16);
            this.ParcelStmtTextBox.LockKeyPress = false;
            this.ParcelStmtTextBox.MaxLength = 50;
            this.ParcelStmtTextBox.Name = "ParcelStmtTextBox";
            this.ParcelStmtTextBox.PersistDefaultColor = false;
            this.ParcelStmtTextBox.Precision = 2;
            this.ParcelStmtTextBox.QueryingFileldName = "";
            this.ParcelStmtTextBox.SetColorFlag = false;
            this.ParcelStmtTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ParcelStmtTextBox.Size = new System.Drawing.Size(450, 16);
            this.ParcelStmtTextBox.SpecialCharacter = "%";
            this.ParcelStmtTextBox.TabIndex = 3;
            this.ParcelStmtTextBox.TextCustomFormat = "$#,##0.00";
            this.ParcelStmtTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.ParcelStmtTextBox.WholeInteger = false;
            this.ParcelStmtTextBox.TextChanged += new System.EventHandler(this.ParcelStmtTextBox_TextChanged);
            // 
            // ParcelTextBoxPanel
            // 
            this.ParcelTextBoxPanel.BackColor = System.Drawing.Color.White;
            this.ParcelTextBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ParcelTextBoxPanel.Controls.Add(this.ParcelStmtTextBox);
            this.ParcelTextBoxPanel.Controls.Add(this.ParcelStmtLabel);
            this.ParcelTextBoxPanel.Location = new System.Drawing.Point(12, 12);
            this.ParcelTextBoxPanel.Name = "ParcelTextBoxPanel";
            this.ParcelTextBoxPanel.Size = new System.Drawing.Size(555, 40);
            this.ParcelTextBoxPanel.TabIndex = 5;
            // 
            // MasterAcceptButton
            // 
            this.MasterAcceptButton.ActualPermission = false;
            this.MasterAcceptButton.ApplyDisableBehaviour = false;
            this.MasterAcceptButton.AutoSize = true;
            this.MasterAcceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.MasterAcceptButton.BorderColor = System.Drawing.Color.Wheat;
            this.MasterAcceptButton.CommentPriority = false;
            this.MasterAcceptButton.EnableAutoPrint = false;
            this.MasterAcceptButton.FilterStatus = false;
            this.MasterAcceptButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MasterAcceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MasterAcceptButton.FocusRectangleEnabled = true;
            this.MasterAcceptButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MasterAcceptButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MasterAcceptButton.ImageSelected = false;
            this.MasterAcceptButton.Location = new System.Drawing.Point(12, 238);
            this.MasterAcceptButton.Name = "MasterAcceptButton";
            this.MasterAcceptButton.NewPadding = 5;
            this.MasterAcceptButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.MasterAcceptButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.MasterAcceptButton.Size = new System.Drawing.Size(110, 32);
            this.MasterAcceptButton.StatusIndicator = false;
            this.MasterAcceptButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MasterAcceptButton.StatusOffText = null;
            this.MasterAcceptButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.MasterAcceptButton.StatusOnText = null;
            this.MasterAcceptButton.TabIndex = 170;
            this.MasterAcceptButton.TabStop = false;
            this.MasterAcceptButton.Text = "Accept";
            this.MasterAcceptButton.UseVisualStyleBackColor = false;
            this.MasterAcceptButton.Click += new System.EventHandler(this.MasterAcceptButton_Click);
            // 
            // AcceptAllButton
            // 
            this.AcceptAllButton.ActualPermission = false;
            this.AcceptAllButton.ApplyDisableBehaviour = false;
            this.AcceptAllButton.AutoSize = true;
            this.AcceptAllButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AcceptAllButton.BorderColor = System.Drawing.Color.Wheat;
            this.AcceptAllButton.CommentPriority = false;
            this.AcceptAllButton.EnableAutoPrint = false;
            this.AcceptAllButton.FilterStatus = false;
            this.AcceptAllButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AcceptAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AcceptAllButton.FocusRectangleEnabled = true;
            this.AcceptAllButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptAllButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AcceptAllButton.ImageSelected = false;
            this.AcceptAllButton.Location = new System.Drawing.Point(160, 238);
            this.AcceptAllButton.Name = "AcceptAllButton";
            this.AcceptAllButton.NewPadding = 5;
            this.AcceptAllButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.AcceptAllButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AcceptAllButton.Size = new System.Drawing.Size(110, 32);
            this.AcceptAllButton.StatusIndicator = false;
            this.AcceptAllButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AcceptAllButton.StatusOffText = null;
            this.AcceptAllButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AcceptAllButton.StatusOnText = null;
            this.AcceptAllButton.TabIndex = 171;
            this.AcceptAllButton.TabStop = false;
            this.AcceptAllButton.Text = "Accept All";
            this.AcceptAllButton.UseVisualStyleBackColor = false;
            this.AcceptAllButton.Click += new System.EventHandler(this.AcceptAllButton_Click);
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
            this.SearchButton.Location = new System.Drawing.Point(309, 238);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.NewPadding = 5;
            this.SearchButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.SearchButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SearchButton.Size = new System.Drawing.Size(110, 32);
            this.SearchButton.StatusIndicator = false;
            this.SearchButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SearchButton.StatusOffText = null;
            this.SearchButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SearchButton.StatusOnText = null;
            this.SearchButton.TabIndex = 171;
            this.SearchButton.TabStop = false;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // MasterCancelButton
            // 
            this.MasterCancelButton.ActualPermission = false;
            this.MasterCancelButton.ApplyDisableBehaviour = false;
            this.MasterCancelButton.AutoSize = true;
            this.MasterCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.MasterCancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.MasterCancelButton.CommentPriority = false;
            this.MasterCancelButton.EnableAutoPrint = false;
            this.MasterCancelButton.FilterStatus = false;
            this.MasterCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MasterCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MasterCancelButton.FocusRectangleEnabled = true;
            this.MasterCancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MasterCancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MasterCancelButton.ImageSelected = false;
            this.MasterCancelButton.Location = new System.Drawing.Point(457, 238);
            this.MasterCancelButton.Name = "MasterCancelButton";
            this.MasterCancelButton.NewPadding = 5;
            this.MasterCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.MasterCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.MasterCancelButton.Size = new System.Drawing.Size(110, 32);
            this.MasterCancelButton.StatusIndicator = false;
            this.MasterCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MasterCancelButton.StatusOffText = null;
            this.MasterCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.MasterCancelButton.StatusOnText = null;
            this.MasterCancelButton.TabIndex = 172;
            this.MasterCancelButton.TabStop = false;
            this.MasterCancelButton.Text = "Cancel";
            this.MasterCancelButton.UseVisualStyleBackColor = false;
            this.MasterCancelButton.Click += new System.EventHandler(this.MasterCancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(9, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 173;
            this.label1.Text = "1411";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // RecordCountLabel
            // 
            this.RecordCountLabel.AutoSize = true;
            this.RecordCountLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordCountLabel.Location = new System.Drawing.Point(430, 283);
            this.RecordCountLabel.Name = "RecordCountLabel";
            this.RecordCountLabel.Size = new System.Drawing.Size(0, 15);
            this.RecordCountLabel.TabIndex = 174;
            // 
            // StatementID
            // 
            this.StatementID.HeaderText = "StatementID";
            this.StatementID.Name = "StatementID";
            this.StatementID.Visible = false;
            // 
            // StatementNumber
            // 
            this.StatementNumber.HeaderText = "Statement Number";
            this.StatementNumber.Name = "StatementNumber";
            this.StatementNumber.ReadOnly = true;
            this.StatementNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.StatementNumber.Width = 162;
            // 
            // Taxpayer
            // 
            this.Taxpayer.HeaderText = "Taxpayer";
            this.Taxpayer.Name = "Taxpayer";
            this.Taxpayer.ReadOnly = true;
            this.Taxpayer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Taxpayer.Width = 140;
            // 
            // PostName
            // 
            this.PostName.HeaderText = "Post Type";
            this.PostName.Name = "PostName";
            this.PostName.ReadOnly = true;
            this.PostName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PostName.Width = 115;
            // 
            // RollYear
            // 
            this.RollYear.HeaderText = "Roll Year";
            this.RollYear.Name = "RollYear";
            this.RollYear.ReadOnly = true;
            this.RollYear.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // F1411
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(579, 302);
            this.Controls.Add(this.RecordCountLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ParcelStmtSearchPanel);
            this.Controls.Add(this.MasterCancelButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.AcceptAllButton);
            this.Controls.Add(this.MasterAcceptButton);
            this.Controls.Add(this.ParcelTextBoxPanel);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F1411";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan – Statement Search";
            this.Load += new System.EventHandler(this.F1411_Load);
            this.ParcelStmtSearchPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ParcelStmtSearchGridView)).EndInit();
            this.ParcelTextBoxPanel.ResumeLayout(false);
            this.ParcelTextBoxPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        

        #endregion

        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel ParcelTextBoxPanel;
        private System.Windows.Forms.Label ParcelStmtLabel;
        private TerraScan.UI.Controls.TerraScanTextBox ParcelStmtTextBox;
        private System.Windows.Forms.Panel ParcelStmtSearchPanel;
        private TerraScan.UI.Controls.TerraScanButton MasterAcceptButton;
        private TerraScan.UI.Controls.TerraScanButton AcceptAllButton;
        private TerraScan.UI.Controls.TerraScanButton SearchButton;
        private TerraScan.UI.Controls.TerraScanButton MasterCancelButton;
        private TerraScan.UI.Controls.TerraScanDataGridView ParcelStmtSearchGridView;
        private System.Windows.Forms.VScrollBar MasterNameVerticalScroll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label RecordCountLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatementID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatementNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Taxpayer;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RollYear;   
    }
}