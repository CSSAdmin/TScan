namespace D9030
{
    partial class F9039
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinScrollBar.ScrollBarLook scrollBarLook1 = new Infragistics.Win.UltraWinScrollBar.ScrollBarLook();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9039));
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("QueryColumn");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ProcessButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.ClearButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CloseButton = new TerraScan.UI.Controls.TerraScanButton();
            this.QueryEngineGrid = new Infragistics.Win.UltraWinGrid.UltraGridColumnChooser();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.QueryUpdateGridVScroll = new System.Windows.Forms.VScrollBar();
            this.QueryUpdateGrid = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.CnSImage = new System.Windows.Forms.ImageList(this.components);
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.HelpLink = new System.Windows.Forms.LinkLabel();
            this.QueryEngineColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FormulaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImageColumn = new TerraScan.UI.Controls.TerraScanTextAndImageColumn();
            this.FormulaValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.QueryEngineGrid)).BeginInit();
            this.GridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QueryUpdateGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // ProcessButton
            // 
            this.ProcessButton.ActualPermission = false;
            this.ProcessButton.ApplyDisableBehaviour = false;
            this.ProcessButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ProcessButton.BorderColor = System.Drawing.Color.Wheat;
            this.ProcessButton.CommentPriority = false;
            this.ProcessButton.EnableAutoPrint = false;
            this.ProcessButton.Enabled = false;
            this.ProcessButton.FilterStatus = false;
            this.ProcessButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ProcessButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProcessButton.FocusRectangleEnabled = true;
            this.ProcessButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ProcessButton.ImageSelected = false;
            this.ProcessButton.Location = new System.Drawing.Point(20, 147);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.NewPadding = 5;
            this.ProcessButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.ProcessButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ProcessButton.Size = new System.Drawing.Size(110, 30);
            this.ProcessButton.StatusIndicator = false;
            this.ProcessButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ProcessButton.StatusOffText = null;
            this.ProcessButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ProcessButton.StatusOnText = null;
            this.ProcessButton.TabIndex = 2;
            this.ProcessButton.TabStop = false;
            this.ProcessButton.Text = "Process";
            this.ProcessButton.UseVisualStyleBackColor = false;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(19, 192);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(634, 2);
            this.FormLinePanel.TabIndex = 212;
            // 
            // ClearButton
            // 
            this.ClearButton.ActualPermission = false;
            this.ClearButton.ApplyDisableBehaviour = false;
            this.ClearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ClearButton.BorderColor = System.Drawing.Color.Wheat;
            this.ClearButton.CommentPriority = false;
            this.ClearButton.EnableAutoPrint = false;
            this.ClearButton.Enabled = false;
            this.ClearButton.FilterStatus = false;
            this.ClearButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearButton.FocusRectangleEnabled = true;
            this.ClearButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClearButton.ImageSelected = false;
            this.ClearButton.Location = new System.Drawing.Point(265, 147);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.NewPadding = 5;
            this.ClearButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.ClearButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ClearButton.Size = new System.Drawing.Size(110, 30);
            this.ClearButton.StatusIndicator = false;
            this.ClearButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClearButton.StatusOffText = null;
            this.ClearButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ClearButton.StatusOnText = null;
            this.ClearButton.TabIndex = 215;
            this.ClearButton.TabStop = false;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
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
            this.CloseButton.Location = new System.Drawing.Point(544, 147);
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
            this.CloseButton.TabIndex = 216;
            this.CloseButton.TabStop = false;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // QueryEngineGrid
            // 
            this.QueryEngineGrid.ColumnDisplayOrder = Infragistics.Win.UltraWinGrid.ColumnDisplayOrder.SameAsGrid;
            appearance1.BackColor = System.Drawing.Color.White;
            this.QueryEngineGrid.DisplayLayout.Appearance = appearance1;
            this.QueryEngineGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            this.QueryEngineGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.QueryEngineGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.QueryEngineGrid.DisplayLayout.MaxColScrollRegions = 1;
            this.QueryEngineGrid.DisplayLayout.MaxRowScrollRegions = 1;
            this.QueryEngineGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.QueryEngineGrid.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.None;
            this.QueryEngineGrid.DisplayLayout.Override.AllowRowLayoutCellSizing = Infragistics.Win.UltraWinGrid.RowLayoutSizing.None;
            this.QueryEngineGrid.DisplayLayout.Override.AllowRowLayoutLabelSizing = Infragistics.Win.UltraWinGrid.RowLayoutSizing.None;
            this.QueryEngineGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.QueryEngineGrid.DisplayLayout.Override.CellPadding = 2;
            this.QueryEngineGrid.DisplayLayout.Override.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            this.QueryEngineGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            this.QueryEngineGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.QueryEngineGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed;
            this.QueryEngineGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.QueryEngineGrid.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.QueryEngineGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.QueryEngineGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None;
            appearance2.BackColor = System.Drawing.Color.White;
            scrollBarLook1.Appearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            scrollBarLook1.AppearanceVertical = appearance3;
            scrollBarLook1.ScrollBarArrowStyle = Infragistics.Win.UltraWinScrollBar.ScrollBarArrowStyle.None;
            this.QueryEngineGrid.DisplayLayout.ScrollBarLook = scrollBarLook1;
            this.QueryEngineGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;
            this.QueryEngineGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.QueryEngineGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.QueryEngineGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QueryEngineGrid.ForeColor = System.Drawing.Color.White;
            this.QueryEngineGrid.Location = new System.Drawing.Point(153, 204);
            this.QueryEngineGrid.Name = "QueryEngineGrid";
            this.QueryEngineGrid.Size = new System.Drawing.Size(10, 11);
            this.QueryEngineGrid.StyleLibraryName = "";
            this.QueryEngineGrid.StyleSetName = "";
            this.QueryEngineGrid.TabIndex = 218;
            this.QueryEngineGrid.Text = "ultraGridColumnChooser1";
            this.QueryEngineGrid.Visible = false;
            // 
            // GridPanel
            // 
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.QueryUpdateGridVScroll);
            this.GridPanel.Controls.Add(this.QueryUpdateGrid);
            this.GridPanel.Location = new System.Drawing.Point(19, 17);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(634, 111);
            this.GridPanel.TabIndex = 219;
            this.GridPanel.TabStop = true;
            // 
            // QueryUpdateGridVScroll
            // 
            this.QueryUpdateGridVScroll.Enabled = false;
            this.QueryUpdateGridVScroll.Location = new System.Drawing.Point(615, 0);
            this.QueryUpdateGridVScroll.Name = "QueryUpdateGridVScroll";
            this.QueryUpdateGridVScroll.Size = new System.Drawing.Size(16, 110);
            this.QueryUpdateGridVScroll.TabIndex = 1005;
            // 
            // QueryUpdateGrid
            // 
            this.QueryUpdateGrid.AllowCellClick = true;
            this.QueryUpdateGrid.AllowDoubleClick = true;
            this.QueryUpdateGrid.AllowEmptyRows = true;
            this.QueryUpdateGrid.AllowEnterKey = false;
            this.QueryUpdateGrid.AllowSorting = true;
            this.QueryUpdateGrid.AllowUserToAddRows = false;
            this.QueryUpdateGrid.AllowUserToDeleteRows = false;
            this.QueryUpdateGrid.AllowUserToResizeColumns = false;
            this.QueryUpdateGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.QueryUpdateGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.QueryUpdateGrid.ApplyStandardBehaviour = false;
            this.QueryUpdateGrid.BackgroundColor = System.Drawing.Color.White;
            this.QueryUpdateGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.QueryUpdateGrid.ClearCurrentCellOnLeave = true;
            this.QueryUpdateGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.QueryUpdateGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.QueryUpdateGrid.ColumnHeadersHeight = 22;
            this.QueryUpdateGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.QueryUpdateGrid.ColumnHeadersVisible = false;
            this.QueryUpdateGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QueryEngineColumn,
            this.FormulaColumn,
            this.ImageColumn,
            this.FormulaValue});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.QueryUpdateGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.QueryUpdateGrid.DefaultRowIndex = 0;
            this.QueryUpdateGrid.DeselectCurrentCell = false;
            this.QueryUpdateGrid.DeselectSpecifiedRow = -1;
            this.QueryUpdateGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.QueryUpdateGrid.EnableBinding = true;
            this.QueryUpdateGrid.EnableHeadersVisualStyles = false;
            this.QueryUpdateGrid.GridColor = System.Drawing.Color.Black;
            this.QueryUpdateGrid.GridContentSelected = false;
            this.QueryUpdateGrid.IsEditableGrid = true;
            this.QueryUpdateGrid.IsMultiSelect = false;
            this.QueryUpdateGrid.IsSorted = false;
            this.QueryUpdateGrid.Location = new System.Drawing.Point(-1, -1);
            this.QueryUpdateGrid.MultiSelect = false;
            this.QueryUpdateGrid.Name = "QueryUpdateGrid";
            this.QueryUpdateGrid.NumRowsVisible = 5;
            this.QueryUpdateGrid.PrimaryKeyColumnName = "";
            this.QueryUpdateGrid.RemainSortFields = true;
            this.QueryUpdateGrid.RemoveDefaultSelection = true;
            this.QueryUpdateGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.QueryUpdateGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.QueryUpdateGrid.RowHeadersWidth = 20;
            this.QueryUpdateGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.QueryUpdateGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.QueryUpdateGrid.Size = new System.Drawing.Size(634, 111);
            this.QueryUpdateGrid.TabIndex = 10;
            this.QueryUpdateGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.QueryUpdateGrid_CellMouseClick);
            this.QueryUpdateGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryUpdateGrid_RowEnter);
            this.QueryUpdateGrid.MouseHover += new System.EventHandler(this.QueryUpdateGrid_MouseHover);
            this.QueryUpdateGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryUpdateGrid_CellEndEdit);
            this.QueryUpdateGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryUpdateGrid_CellClick);
            this.QueryUpdateGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.QueryUpdateGrid_EditingControlShowing);
            this.QueryUpdateGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QueryUpdateGrid_KeyDown);
            // 
            // CnSImage
            // 
            this.CnSImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("CnSImage.ImageStream")));
            this.CnSImage.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CnSImage.Images.SetKeyName(0, "C.jpg");
            this.CnSImage.Images.SetKeyName(1, "S.jpg");
            this.CnSImage.Images.SetKeyName(2, "");
            // 
            // ultraDataSource1
            // 
            this.ultraDataSource1.Band.Columns.AddRange(new object[] {
            ultraDataColumn1});
            this.ultraDataSource1.Band.Key = "QueryColumn";
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AccessibleDescription = "0";
            this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(16, 197);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(35, 15);
            this.FormIDLabel.TabIndex = 221;
            this.FormIDLabel.Text = "9039";
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpLink.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLink.Location = new System.Drawing.Point(296, 197);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(56, 15);
            this.HelpLink.TabIndex = 220;
            this.HelpLink.TabStop = true;
            this.HelpLink.Text = "Help";
            this.HelpLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // QueryEngineColumn
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QueryEngineColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.QueryEngineColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.QueryEngineColumn.DisplayStyleForCurrentCellOnly = true;
            this.QueryEngineColumn.HeaderText = "QueryEngineColumn";
            this.QueryEngineColumn.Name = "QueryEngineColumn";
            this.QueryEngineColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.QueryEngineColumn.ToolTipText = "$hi$";
            this.QueryEngineColumn.Width = 161;
            // 
            // FormulaColumn
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Gray;
            this.FormulaColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.FormulaColumn.HeaderText = "FormulaColumn";
            this.FormulaColumn.MaxInputLength = 50;
            this.FormulaColumn.Name = "FormulaColumn";
            this.FormulaColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FormulaColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FormulaColumn.Width = 405;
            // 
            // ImageColumn
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.ImageColumn.HeaderText = "ImageColumn";
            this.ImageColumn.Image = null;
            this.ImageColumn.Name = "ImageColumn";
            this.ImageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ImageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ImageColumn.Width = 30;
            // 
            // FormulaValue
            // 
            this.FormulaValue.HeaderText = "FormulaValue";
            this.FormulaValue.Name = "FormulaValue";
            this.FormulaValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FormulaValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FormulaValue.Visible = false;
            // 
            // F9039
            // 
            this.AccessibleName = "Query Engine Update";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(672, 224);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.QueryEngineGrid);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.ProcessButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F9039";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "9039";
            this.Text = "Query Engine Update";
            this.Load += new System.EventHandler(this.F9039_Load);
            ((System.ComponentModel.ISupportInitialize)(this.QueryEngineGrid)).EndInit();
            this.GridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.QueryUpdateGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanButton ProcessButton;
        private System.Windows.Forms.Panel FormLinePanel;
        private TerraScan.UI.Controls.TerraScanButton ClearButton;
        private TerraScan.UI.Controls.TerraScanButton CloseButton;
        private Infragistics.Win.UltraWinGrid.UltraGridColumnChooser QueryEngineGrid;
        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.ImageList CnSImage;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
        private TerraScan.UI.Controls.TerraScanDataGridView QueryUpdateGrid;
        private System.Windows.Forms.VScrollBar QueryUpdateGridVScroll;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.LinkLabel HelpLink;
        private System.Windows.Forms.DataGridViewComboBoxColumn QueryEngineColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormulaColumn;
        private TerraScan.UI.Controls.TerraScanTextAndImageColumn ImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormulaValue;
    }
}