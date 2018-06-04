namespace D1200
{
    partial class F1201
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1201));
            this.BorderPanel = new System.Windows.Forms.Panel();
            this.PostingsAuditLink = new System.Windows.Forms.LinkLabel();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.PostingHistoryVScrollBar = new System.Windows.Forms.VScrollBar();
            this.PostingHistoryDataGrid = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.PostIdentifier = new System.Windows.Forms.DataGridViewLinkColumn();
            this.PostType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RollYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecvdThru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RanOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amountposted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReversePost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnPostID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViewGLButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ReverseGLPostButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ShowLastLabel = new System.Windows.Forms.Label();
            this.ShowOnly = new System.Windows.Forms.Label();
            this.ClosedDatelbl = new System.Windows.Forms.Label();
            this.ClosedDateLabel = new System.Windows.Forms.Label();
            this.formHeaderSmartPartdeckWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.CommentsdeckWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.CountyConfiglink = new System.Windows.Forms.LinkLabel();
            this.ShowOnlyCombo = new TerraScan.UI.Controls.TerraScanComboBox();
            this.ShowLastCombo = new TerraScan.UI.Controls.TerraScanComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.HelpLink = new TerraScan.SmartParts.HelpSmartPart();
            ((System.ComponentModel.ISupportInitialize)(this.PostingHistoryDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BorderPanel
            // 
            this.BorderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BorderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.BorderPanel.Location = new System.Drawing.Point(18, 637);
            this.BorderPanel.Name = "BorderPanel";
            this.BorderPanel.Size = new System.Drawing.Size(825, 2);
            this.BorderPanel.TabIndex = 22;
            // 
            // PostingsAuditLink
            // 
            this.PostingsAuditLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PostingsAuditLink.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostingsAuditLink.Location = new System.Drawing.Point(534, 643);
            this.PostingsAuditLink.Name = "PostingsAuditLink";
            this.PostingsAuditLink.Size = new System.Drawing.Size(301, 15);
            this.PostingsAuditLink.TabIndex = 9;
            this.PostingsAuditLink.TabStop = true;
            this.PostingsAuditLink.Text = "tTR_Postings [PostID]";
            this.PostingsAuditLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.PostingsAuditLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PostingsLink_LinkClicked);
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(14, 643);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(35, 15);
            this.FormIDLabel.TabIndex = 23;
            this.FormIDLabel.Text = "1201";
            // 
            // PostingHistoryVScrollBar
            // 
            this.PostingHistoryVScrollBar.Location = new System.Drawing.Point(777, 109);
            this.PostingHistoryVScrollBar.Name = "PostingHistoryVScrollBar";
            this.PostingHistoryVScrollBar.Size = new System.Drawing.Size(17, 457);
            this.PostingHistoryVScrollBar.TabIndex = 144;
            // 
            // PostingHistoryDataGrid
            // 
            this.PostingHistoryDataGrid.AccessibleName = "`";
            this.PostingHistoryDataGrid.AllowCellClick = true;
            this.PostingHistoryDataGrid.AllowDoubleClick = false;
            this.PostingHistoryDataGrid.AllowEmptyRows = true;
            this.PostingHistoryDataGrid.AllowEnterKey = false;
            this.PostingHistoryDataGrid.AllowSorting = true;
            this.PostingHistoryDataGrid.AllowUserToAddRows = false;
            this.PostingHistoryDataGrid.AllowUserToDeleteRows = false;
            this.PostingHistoryDataGrid.AllowUserToResizeColumns = false;
            this.PostingHistoryDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.PostingHistoryDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.PostingHistoryDataGrid.ApplyStandardBehaviour = false;
            this.PostingHistoryDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.PostingHistoryDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PostingHistoryDataGrid.ClearCurrentCellOnLeave = false;
            this.PostingHistoryDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PostingHistoryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.PostingHistoryDataGrid.ColumnHeadersHeight = 19;
            this.PostingHistoryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.PostingHistoryDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PostIdentifier,
            this.PostType,
            this.User,
            this.RollYear,
            this.RecvdThru,
            this.RanOn,
            this.Amountposted,
            this.ReversePost,
            this.UnPostID});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PostingHistoryDataGrid.DefaultCellStyle = dataGridViewCellStyle10;
            this.PostingHistoryDataGrid.DefaultRowIndex = 0;
            this.PostingHistoryDataGrid.DeselectCurrentCell = false;
            this.PostingHistoryDataGrid.DeselectSpecifiedRow = -1;
            this.PostingHistoryDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.PostingHistoryDataGrid.EnableBinding = false;
            this.PostingHistoryDataGrid.EnableHeadersVisualStyles = false;
            this.PostingHistoryDataGrid.GridColor = System.Drawing.Color.Black;
            this.PostingHistoryDataGrid.GridContentSelected = false;
            this.PostingHistoryDataGrid.IsEditableGrid = false;
            this.PostingHistoryDataGrid.IsSorted = false;
            this.PostingHistoryDataGrid.Location = new System.Drawing.Point(18, 108);
            this.PostingHistoryDataGrid.MultiSelect = false;
            this.PostingHistoryDataGrid.Name = "PostingHistoryDataGrid";
            this.PostingHistoryDataGrid.NumRowsVisible = 20;
            this.PostingHistoryDataGrid.PrimaryKeyColumnName = "";
            this.PostingHistoryDataGrid.RemainSortFields = false;
            this.PostingHistoryDataGrid.RemoveDefaultSelection = false;
            this.PostingHistoryDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PostingHistoryDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.PostingHistoryDataGrid.RowHeadersWidth = 20;
            this.PostingHistoryDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PostingHistoryDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PostingHistoryDataGrid.Size = new System.Drawing.Size(776, 459);
            this.PostingHistoryDataGrid.TabIndex = 7;
            this.PostingHistoryDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PostingHistoryDataGrid_CellClick);
            this.PostingHistoryDataGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.PostingHistoryDataGrid_RowEnter);
            this.PostingHistoryDataGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.PostingHistoryDataGrid_DataBindingComplete);
            this.PostingHistoryDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.PostingHistoryDataGrid_CellFormatting);
            this.PostingHistoryDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PostingHistoryDataGrid_CellContentClick);
            // 
            // PostIdentifier
            // 
            this.PostIdentifier.DataPropertyName = "PostID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PostIdentifier.DefaultCellStyle = dataGridViewCellStyle3;
            this.PostIdentifier.HeaderText = "Post ID";
            this.PostIdentifier.Name = "PostIdentifier";
            this.PostIdentifier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PostIdentifier.Width = 72;
            // 
            // PostType
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PostType.DefaultCellStyle = dataGridViewCellStyle4;
            this.PostType.HeaderText = "PostType";
            this.PostType.Name = "PostType";
            this.PostType.ReadOnly = true;
            this.PostType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PostType.Width = 141;
            // 
            // User
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.User.DefaultCellStyle = dataGridViewCellStyle5;
            this.User.HeaderText = "User";
            this.User.Name = "User";
            this.User.ReadOnly = true;
            this.User.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.User.Width = 90;
            // 
            // RollYear
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RollYear.DefaultCellStyle = dataGridViewCellStyle6;
            this.RollYear.HeaderText = "Roll Year";
            this.RollYear.Name = "RollYear";
            this.RollYear.ReadOnly = true;
            this.RollYear.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.RollYear.Width = 81;
            // 
            // RecvdThru
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RecvdThru.DefaultCellStyle = dataGridViewCellStyle7;
            this.RecvdThru.HeaderText = "Recv\'d Thru";
            this.RecvdThru.Name = "RecvdThru";
            this.RecvdThru.ReadOnly = true;
            this.RecvdThru.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // RanOn
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.Format = "G";
            dataGridViewCellStyle8.NullValue = null;
            this.RanOn.DefaultCellStyle = dataGridViewCellStyle8;
            this.RanOn.HeaderText = "Ran On";
            this.RanOn.Name = "RanOn";
            this.RanOn.ReadOnly = true;
            this.RanOn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.RanOn.Width = 136;
            // 
            // Amountposted
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "#,##0.00";
            dataGridViewCellStyle9.NullValue = null;
            this.Amountposted.DefaultCellStyle = dataGridViewCellStyle9;
            this.Amountposted.HeaderText = "Amount posted";
            this.Amountposted.Name = "Amountposted";
            this.Amountposted.ReadOnly = true;
            this.Amountposted.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Amountposted.Width = 118;
            // 
            // ReversePost
            // 
            this.ReversePost.DataPropertyName = "ReversePost";
            this.ReversePost.HeaderText = "";
            this.ReversePost.Name = "ReversePost";
            this.ReversePost.Visible = false;
            this.ReversePost.Width = 5;
            // 
            // UnPostID
            // 
            this.UnPostID.DataPropertyName = "UnPostID";
            this.UnPostID.HeaderText = "";
            this.UnPostID.Name = "UnPostID";
            this.UnPostID.Visible = false;
            // 
            // ViewGLButton
            // 
            this.ViewGLButton.ActualPermission = false;
            this.ViewGLButton.ApplyDisableBehaviour = false;
            this.ViewGLButton.AutoEllipsis = true;
            this.ViewGLButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(150)))), ((int)(((byte)(94)))));
            this.ViewGLButton.BorderColor = System.Drawing.Color.Wheat;
            this.ViewGLButton.CommentPriority = false;
            this.ViewGLButton.EnableAutoPrint = false;
            this.ViewGLButton.FilterStatus = false;
            this.ViewGLButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ViewGLButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewGLButton.FocusRectangleEnabled = true;
            this.ViewGLButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewGLButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ViewGLButton.ImageSelected = false;
            this.ViewGLButton.Location = new System.Drawing.Point(136, 11);
            this.ViewGLButton.Name = "ViewGLButton";
            this.ViewGLButton.NewPadding = 5;
            this.ViewGLButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.ViewGLButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.Print;
            this.ViewGLButton.Size = new System.Drawing.Size(98, 28);
            this.ViewGLButton.StatusIndicator = false;
            this.ViewGLButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ViewGLButton.StatusOffText = null;
            this.ViewGLButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ViewGLButton.StatusOnText = null;
            this.ViewGLButton.TabIndex = 1;
            this.ViewGLButton.TabStop = false;
            this.ViewGLButton.Text = "View GL";
            this.ViewGLButton.UseVisualStyleBackColor = false;
            this.ViewGLButton.Click += new System.EventHandler(this.ViewGLButton_Click);
            // 
            // ReverseGLPostButton
            // 
            this.ReverseGLPostButton.ActualPermission = false;
            this.ReverseGLPostButton.ApplyDisableBehaviour = false;
            this.ReverseGLPostButton.AutoEllipsis = true;
            this.ReverseGLPostButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ReverseGLPostButton.BorderColor = System.Drawing.Color.Wheat;
            this.ReverseGLPostButton.CommentPriority = false;
            this.ReverseGLPostButton.EnableAutoPrint = false;
            this.ReverseGLPostButton.FilterStatus = false;
            this.ReverseGLPostButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ReverseGLPostButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReverseGLPostButton.FocusRectangleEnabled = true;
            this.ReverseGLPostButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReverseGLPostButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ReverseGLPostButton.ImageSelected = false;
            this.ReverseGLPostButton.Location = new System.Drawing.Point(18, 11);
            this.ReverseGLPostButton.Name = "ReverseGLPostButton";
            this.ReverseGLPostButton.NewPadding = 5;
            this.ReverseGLPostButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.ReverseGLPostButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ReverseGLPostButton.Size = new System.Drawing.Size(111, 28);
            this.ReverseGLPostButton.StatusIndicator = false;
            this.ReverseGLPostButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ReverseGLPostButton.StatusOffText = null;
            this.ReverseGLPostButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ReverseGLPostButton.StatusOnText = null;
            this.ReverseGLPostButton.TabIndex = 0;
            this.ReverseGLPostButton.TabStop = false;
            this.ReverseGLPostButton.Text = "Reverse GL Post";
            this.ReverseGLPostButton.UseVisualStyleBackColor = false;
            this.ReverseGLPostButton.Click += new System.EventHandler(this.ReverseGLPostButton_Click);
            // 
            // ShowLastLabel
            // 
            this.ShowLastLabel.AutoSize = true;
            this.ShowLastLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ShowLastLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowLastLabel.ForeColor = System.Drawing.Color.Black;
            this.ShowLastLabel.Location = new System.Drawing.Point(349, 72);
            this.ShowLastLabel.Name = "ShowLastLabel";
            this.ShowLastLabel.Size = new System.Drawing.Size(74, 16);
            this.ShowLastLabel.TabIndex = 148;
            this.ShowLastLabel.Text = "Show Last";
            // 
            // ShowOnly
            // 
            this.ShowOnly.AutoSize = true;
            this.ShowOnly.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ShowOnly.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowOnly.ForeColor = System.Drawing.Color.Black;
            this.ShowOnly.Location = new System.Drawing.Point(581, 72);
            this.ShowOnly.Name = "ShowOnly";
            this.ShowOnly.Size = new System.Drawing.Size(77, 16);
            this.ShowOnly.TabIndex = 150;
            this.ShowOnly.Text = "Show Only";
            // 
            // ClosedDatelbl
            // 
            this.ClosedDatelbl.AutoSize = true;
            this.ClosedDatelbl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClosedDatelbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ClosedDatelbl.Location = new System.Drawing.Point(49, 71);
            this.ClosedDatelbl.Name = "ClosedDatelbl";
            this.ClosedDatelbl.Size = new System.Drawing.Size(108, 19);
            this.ClosedDatelbl.TabIndex = 152;
            this.ClosedDatelbl.Text = "Closed Date:";
            // 
            // ClosedDateLabel
            // 
            this.ClosedDateLabel.AutoSize = true;
            this.ClosedDateLabel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.ClosedDateLabel.ForeColor = System.Drawing.Color.Maroon;
            this.ClosedDateLabel.Location = new System.Drawing.Point(157, 69);
            this.ClosedDateLabel.Name = "ClosedDateLabel";
            this.ClosedDateLabel.Size = new System.Drawing.Size(0, 22);
            this.ClosedDateLabel.TabIndex = 153;
            // 
            // formHeaderSmartPartdeckWorkspace
            // 
            this.formHeaderSmartPartdeckWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.formHeaderSmartPartdeckWorkspace.Location = new System.Drawing.Point(537, 0);
            this.formHeaderSmartPartdeckWorkspace.Name = "formHeaderSmartPartdeckWorkspace";
            this.formHeaderSmartPartdeckWorkspace.Size = new System.Drawing.Size(327, 62);
            this.formHeaderSmartPartdeckWorkspace.TabIndex = 154;
            this.formHeaderSmartPartdeckWorkspace.Text = "FormHeaderSmartPart";
            // 
            // CommentsdeckWorkspace
            // 
            this.CommentsdeckWorkspace.Location = new System.Drawing.Point(238, 1);
            this.CommentsdeckWorkspace.Name = "CommentsdeckWorkspace";
            this.CommentsdeckWorkspace.Size = new System.Drawing.Size(248, 48);
            this.CommentsdeckWorkspace.TabIndex = 2;
            this.CommentsdeckWorkspace.TabStop = false;
            this.CommentsdeckWorkspace.Text = "deckWorkspace2";
            // 
            // CountyConfiglink
            // 
            this.CountyConfiglink.AutoSize = true;
            this.CountyConfiglink.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountyConfiglink.Location = new System.Drawing.Point(150, 88);
            this.CountyConfiglink.Name = "CountyConfiglink";
            this.CountyConfiglink.Size = new System.Drawing.Size(105, 11);
            this.CountyConfiglink.TabIndex = 4;
            this.CountyConfiglink.TabStop = true;
            this.CountyConfiglink.Text = "County Configuration";
            this.CountyConfiglink.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.CountyConfiglink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CountyConfiglink_LinkClicked);
            // 
            // ShowOnlyCombo
            // 
            this.ShowOnlyCombo.BackColor = System.Drawing.Color.White;
            this.ShowOnlyCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowOnlyCombo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowOnlyCombo.FormattingEnabled = true;
            this.ShowOnlyCombo.IntegralHeight = false;
            this.ShowOnlyCombo.Location = new System.Drawing.Point(658, 68);
            this.ShowOnlyCombo.Name = "ShowOnlyCombo";
            this.ShowOnlyCombo.Size = new System.Drawing.Size(163, 23);
            this.ShowOnlyCombo.TabIndex = 6;
            this.ShowOnlyCombo.SelectionChangeCommitted += new System.EventHandler(this.ShowOnlyCombo_SelectionChangeCommitted);
            // 
            // ShowLastCombo
            // 
            this.ShowLastCombo.BackColor = System.Drawing.Color.White;
            this.ShowLastCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowLastCombo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowLastCombo.FormattingEnabled = true;
            this.ShowLastCombo.IntegralHeight = false;
            this.ShowLastCombo.Location = new System.Drawing.Point(424, 68);
            this.ShowLastCombo.Name = "ShowLastCombo";
            this.ShowLastCombo.Size = new System.Drawing.Size(144, 23);
            this.ShowLastCombo.TabIndex = 5;
            this.ShowLastCombo.SelectionChangeCommitted += new System.EventHandler(this.ShowLastCombo_SelectionChangeCommitted);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(787, 108);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 460);
            this.pictureBox1.TabIndex = 159;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(775, 566);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 1);
            this.panel1.TabIndex = 160;
            // 
            // panel2
            // 
            this.panel2.AccessibleName = "`";
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(775, 108);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(20, 1);
            this.panel2.TabIndex = 161;
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.HelpLink.BackColor = System.Drawing.Color.White;
            this.HelpLink.FormId = "1201";
            this.HelpLink.Location = new System.Drawing.Point(396, 643);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(43, 27);
            this.HelpLink.TabIndex = 8;
            this.HelpLink.Tag = "1201";
            this.HelpLink.VisibleHelpButton = false;
            this.HelpLink.VisibleHelpLinkButton = true;
            // 
            // F1201
            // 
            this.AccessibleName = "Posting History";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ShowLastCombo);
            this.Controls.Add(this.CountyConfiglink);
            this.Controls.Add(this.CommentsdeckWorkspace);
            this.Controls.Add(this.formHeaderSmartPartdeckWorkspace);
            this.Controls.Add(this.ClosedDateLabel);
            this.Controls.Add(this.ClosedDatelbl);
            this.Controls.Add(this.ShowOnlyCombo);
            this.Controls.Add(this.ShowOnly);
            this.Controls.Add(this.ShowLastLabel);
            this.Controls.Add(this.ViewGLButton);
            this.Controls.Add(this.ReverseGLPostButton);
            this.Controls.Add(this.PostingHistoryVScrollBar);
            this.Controls.Add(this.BorderPanel);
            this.Controls.Add(this.PostingsAuditLink);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.PostingHistoryDataGrid);
            this.Controls.Add(this.pictureBox1);
            this.MinimumSize = new System.Drawing.Size(854, 665);
            this.Name = "F1201";
            this.Size = new System.Drawing.Size(864, 710);
            this.Tag = "1201";
            this.Load += new System.EventHandler(this.F1201_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PostingHistoryDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BorderPanel;
        private System.Windows.Forms.LinkLabel PostingsAuditLink;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.VScrollBar PostingHistoryVScrollBar;
        private TerraScan.UI.Controls.TerraScanDataGridView PostingHistoryDataGrid;
        private TerraScan.UI.Controls.TerraScanButton ViewGLButton;
        private TerraScan.UI.Controls.TerraScanButton ReverseGLPostButton;
        private System.Windows.Forms.Label ShowLastLabel;
        private System.Windows.Forms.Label ShowOnly;
        private System.Windows.Forms.Label ClosedDatelbl;
        private System.Windows.Forms.Label ClosedDateLabel;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace formHeaderSmartPartdeckWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace CommentsdeckWorkspace;
        private System.Windows.Forms.LinkLabel CountyConfiglink;
        private TerraScan.UI.Controls.TerraScanComboBox ShowOnlyCombo;
        private TerraScan.UI.Controls.TerraScanComboBox ShowLastCombo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewLinkColumn PostIdentifier;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostType;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn RollYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecvdThru;
        private System.Windows.Forms.DataGridViewTextBoxColumn RanOn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amountposted;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReversePost;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnPostID;
        private TerraScan.SmartParts.HelpSmartPart HelpLink;
    }
}
