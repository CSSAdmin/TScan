namespace D9000
{
    partial class F9015
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9015));
            this.headerLabel = new System.Windows.Forms.Label();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.ResultGridView = new TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid();
            this.formIDLabel = new System.Windows.Forms.Label();
            this.k = new System.Windows.Forms.Timer(this.components);
            this.sqlQueryTextBox = new System.Windows.Forms.TextBox();
            this.SqlCategoryCombo = new TerraScan.UI.Controls.TerraScanComboBox();
            this.SqlLibraryCombo = new TerraScan.UI.Controls.TerraScanComboBox();
            this.sqlSaveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SaveMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SaveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.sqlRunButton = new TerraScan.UI.Controls.TerraScanButton();
            this.PreviewButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ExcelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ResultPreviewDialog = new Infragistics.Win.Printing.UltraPrintPreviewDialog(this.components);
            this.ResultPrintDocument = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(this.components);
            this.ExcelExporter = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.resultPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SaveMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(80)))), ((int)(((byte)(129)))));
            this.headerLabel.Location = new System.Drawing.Point(712, 12);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(130, 22);
            this.headerLabel.TabIndex = 30;
            this.headerLabel.Text = "SQL Support";
            // 
            // resultPanel
            // 
            this.resultPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resultPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resultPanel.Controls.Add(this.ResultGridView);
            this.resultPanel.Location = new System.Drawing.Point(18, 226);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(787, 405);
            this.resultPanel.TabIndex = 32;
            // 
            // ResultGridView
            // 
            this.ResultGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance1.BorderColor = System.Drawing.Color.Black;
            appearance1.FontData.BoldAsString = "True";
            appearance1.FontData.Name = "Arial";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.TextHAlignAsString = "Left";
            appearance1.TextVAlignAsString = "Middle";
            this.ResultGridView.DisplayLayout.Appearance = appearance1;
            this.ResultGridView.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            appearance2.BackColor = System.Drawing.Color.DimGray;
            ultraGridBand1.Override.HeaderAppearance = appearance2;
            this.ResultGridView.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ResultGridView.DisplayLayout.GroupByBox.Hidden = true;
            this.ResultGridView.DisplayLayout.InterBandSpacing = 10;
            this.ResultGridView.DisplayLayout.MaxColScrollRegions = 1;
            this.ResultGridView.DisplayLayout.MaxRowScrollRegions = 1;
            this.ResultGridView.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ResultGridView.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinBand;
            this.ResultGridView.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            this.ResultGridView.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.ResultGridView.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ResultGridView.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ResultGridView.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.True;
            this.ResultGridView.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.ResultGridView.DisplayLayout.Override.CardAreaAppearance = appearance3;
            appearance4.BorderColor = System.Drawing.Color.Black;
            this.ResultGridView.DisplayLayout.Override.CellAppearance = appearance4;
            this.ResultGridView.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect;
            this.ResultGridView.DisplayLayout.Override.ColumnSizingArea = Infragistics.Win.UltraWinGrid.ColumnSizingArea.EntireColumn;
            this.ResultGridView.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            this.ResultGridView.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            this.ResultGridView.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Combo;
            this.ResultGridView.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.StartsWith;
            this.ResultGridView.DisplayLayout.Override.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)(((((((((((((((((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.NotEquals)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.LessThan)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.LessThanOrEqualTo)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.GreaterThan)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.GreaterThanOrEqualTo)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Like)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Match)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.NotLike)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.DoesNotMatch)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.StartsWith)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.DoesNotStartWith)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.EndsWith)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.DoesNotEndWith)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.DoesNotContain)
                        | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Reserved)));
            this.ResultGridView.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(190)))), ((int)(((byte)(140)))));
            this.ResultGridView.DisplayLayout.Override.FilterRowAppearance = appearance5;
            appearance6.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.ResultGridView.DisplayLayout.Override.FilterRowPromptAppearance = appearance6;
            this.ResultGridView.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            this.ResultGridView.DisplayLayout.Override.FixedRowIndicator = Infragistics.Win.UltraWinGrid.FixedRowIndicator.Button;
            this.ResultGridView.DisplayLayout.Override.FixedRowSortOrder = Infragistics.Win.UltraWinGrid.FixedRowSortOrder.FixOrder;
            this.ResultGridView.DisplayLayout.Override.FixedRowStyle = Infragistics.Win.UltraWinGrid.FixedRowStyle.Top;
            this.ResultGridView.DisplayLayout.Override.GroupBySummaryDisplayStyle = Infragistics.Win.UltraWinGrid.GroupBySummaryDisplayStyle.SummaryCellsAlwaysBelowDescription;
            appearance7.BackColor = System.Drawing.Color.Gray;
            appearance7.BackColor2 = System.Drawing.Color.Gray;
            appearance7.ForeColor = System.Drawing.Color.White;
            appearance7.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            appearance7.TextHAlignAsString = "Left";
            appearance7.TextTrimming = Infragistics.Win.TextTrimming.EllipsisPath;
            appearance7.TextVAlignAsString = "Middle";
            appearance7.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ResultGridView.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.ResultGridView.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            appearance8.TextHAlignAsString = "Left";
            appearance8.TextVAlignAsString = "Middle";
            this.ResultGridView.DisplayLayout.Override.MergedCellAppearance = appearance8;
            this.ResultGridView.DisplayLayout.Override.MergedCellStyle = Infragistics.Win.UltraWinGrid.MergedCellStyle.Never;
            appearance9.BackColor = System.Drawing.Color.LightGray;
            this.ResultGridView.DisplayLayout.Override.RowAlternateAppearance = appearance9;
            appearance10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(195)))), ((int)(((byte)(198)))));
            this.ResultGridView.DisplayLayout.Override.RowSelectorAppearance = appearance10;
            this.ResultGridView.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.ResultGridView.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed;
            appearance11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            appearance11.ForeColor = System.Drawing.Color.White;
            this.ResultGridView.DisplayLayout.Override.SelectedCellAppearance = appearance11;
            this.ResultGridView.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.ResultGridView.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.ResultGridView.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FixedRows;
            appearance12.BackColor = System.Drawing.Color.Black;
            this.ResultGridView.DisplayLayout.Override.SpecialRowSeparatorAppearance = appearance12;
            this.ResultGridView.DisplayLayout.Override.SpecialRowSeparatorHeight = 4;
            this.ResultGridView.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed;
            appearance13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(156)))));
            appearance13.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.ResultGridView.DisplayLayout.Override.SummaryValueAppearance = appearance13;
            this.ResultGridView.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Hide;
            this.ResultGridView.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ResultGridView.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.ResultGridView.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Both;
            this.ResultGridView.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.ResultGridView.DisplayLayout.UseFixedHeaders = true;
            this.ResultGridView.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.ResultGridView.Location = new System.Drawing.Point(-1, -1);
            this.ResultGridView.Name = "ResultGridView";
            this.ResultGridView.Size = new System.Drawing.Size(787, 405);
            this.ResultGridView.TabIndex = 2;
            this.ResultGridView.BeforeColumnChooserDisplayed += new Infragistics.Win.UltraWinGrid.BeforeColumnChooserDisplayedEventHandler(this.ResultGridView_BeforeColumnChooserDisplayed);
            this.ResultGridView.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ResultGridView_InitializeLayout);
            // 
            // formIDLabel
            // 
            this.formIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.formIDLabel.AutoSize = true;
            this.formIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.formIDLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.formIDLabel.Location = new System.Drawing.Point(14, 643);
            this.formIDLabel.Name = "formIDLabel";
            this.formIDLabel.Size = new System.Drawing.Size(35, 15);
            this.formIDLabel.TabIndex = 111;
            this.formIDLabel.Text = "9015";
            // 
            // k
            // 
            this.k.Enabled = true;
            // 
            // sqlQueryTextBox
            // 
            this.sqlQueryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sqlQueryTextBox.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqlQueryTextBox.Location = new System.Drawing.Point(2, 0);
            this.sqlQueryTextBox.MaxLength = 1000;
            this.sqlQueryTextBox.Multiline = true;
            this.sqlQueryTextBox.Name = "sqlQueryTextBox";
            this.sqlQueryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sqlQueryTextBox.Size = new System.Drawing.Size(782, 100);
            this.sqlQueryTextBox.TabIndex = 3;
            this.sqlQueryTextBox.TextChanged += new System.EventHandler(this.SqlQueryTextBox_TextChanged);
            this.sqlQueryTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SqlQueryTextBox_KeyDown);
            // 
            // SqlCategoryCombo
            // 
            this.SqlCategoryCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SqlCategoryCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SqlCategoryCombo.DropDownHeight = 212;
            this.SqlCategoryCombo.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.SqlCategoryCombo.FormattingEnabled = true;
            this.SqlCategoryCombo.IntegralHeight = false;
            this.SqlCategoryCombo.Location = new System.Drawing.Point(18, 70);
            this.SqlCategoryCombo.Name = "SqlCategoryCombo";
            this.SqlCategoryCombo.Size = new System.Drawing.Size(254, 24);
            this.SqlCategoryCombo.TabIndex = 0;
            this.SqlCategoryCombo.Validating += new System.ComponentModel.CancelEventHandler(this.SqlCategoryCombo_Validating);
            this.SqlCategoryCombo.SelectionChangeCommitted += new System.EventHandler(this.SqlCategoryCombo_SelectionChangeCommitted);
            // 
            // SqlLibraryCombo
            // 
            this.SqlLibraryCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SqlLibraryCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            ////this.SqlLibraryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;  
            this.SqlLibraryCombo.DropDownHeight = 212;
            this.SqlLibraryCombo.DropDownWidth = 397;
            this.SqlLibraryCombo.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.SqlLibraryCombo.FormattingEnabled = true;
            this.SqlLibraryCombo.IntegralHeight = false;
            this.SqlLibraryCombo.Location = new System.Drawing.Point(296, 70);
            this.SqlLibraryCombo.Name = "SqlLibraryCombo";
            this.SqlLibraryCombo.Size = new System.Drawing.Size(416, 24);
            this.SqlLibraryCombo.TabIndex = 1;
            this.SqlLibraryCombo.Validating += new System.ComponentModel.CancelEventHandler(this.SqlLibraryCombo_Validating);
            this.SqlLibraryCombo.SelectionChangeCommitted += new System.EventHandler(this.SqlLibraryCombo_SelectionChangeCommitted);
            // 
            // sqlSaveButton
            // 
            this.sqlSaveButton.ActualPermission = false;
            this.sqlSaveButton.ApplyDisableBehaviour = true;
            this.sqlSaveButton.BackColor = System.Drawing.Color.SeaGreen;
            this.sqlSaveButton.BorderColor = System.Drawing.Color.Wheat;
            this.sqlSaveButton.CommentPriority = false;
            this.sqlSaveButton.EnableAutoPrint = false;
            this.sqlSaveButton.FilterStatus = false;
            this.sqlSaveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.sqlSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sqlSaveButton.FocusRectangleEnabled = true;
            this.sqlSaveButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqlSaveButton.ForeColor = System.Drawing.Color.White;
            this.sqlSaveButton.ImageSelected = false;
            this.sqlSaveButton.Location = new System.Drawing.Point(18, 11);
            this.sqlSaveButton.Name = "sqlSaveButton";
            this.sqlSaveButton.NewPadding = 5;
            this.sqlSaveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.sqlSaveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.None;
            this.sqlSaveButton.Size = new System.Drawing.Size(93, 29);
            this.sqlSaveButton.StatusIndicator = false;
            this.sqlSaveButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sqlSaveButton.StatusOffText = null;
            this.sqlSaveButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.sqlSaveButton.StatusOnText = null;
            this.sqlSaveButton.TabIndex = 0;
            this.sqlSaveButton.TabStop = false;
            this.sqlSaveButton.Text = "Save";
            this.sqlSaveButton.UseVisualStyleBackColor = false;
            this.sqlSaveButton.Click += new System.EventHandler(this.SqlSaveButton_Click);
            // 
            // PreviousButton
            // 
            this.PreviousButton.AutoEllipsis = true;
            this.PreviousButton.BackColor = System.Drawing.Color.White;
            this.PreviousButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PreviousButton.Enabled = false;
            this.PreviousButton.FlatAppearance.BorderSize = 0;
            this.PreviousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreviousButton.Font = new System.Drawing.Font("Arial", 3.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviousButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PreviousButton.Image = ((System.Drawing.Image)(resources.GetObject("PreviousButton.Image")));
            this.PreviousButton.Location = new System.Drawing.Point(752, 46);
            this.PreviousButton.Margin = new System.Windows.Forms.Padding(0);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(33, 35);
            this.PreviousButton.TabIndex = 117;
            this.PreviousButton.TabStop = false;
            this.PreviousButton.Tag = "Previous";
            this.PreviousButton.UseVisualStyleBackColor = false;
            this.PreviousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.AutoEllipsis = true;
            this.NextButton.BackColor = System.Drawing.Color.White;
            this.NextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NextButton.Enabled = false;
            this.NextButton.FlatAppearance.BorderSize = 0;
            this.NextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextButton.Font = new System.Drawing.Font("Arial", 3.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.NextButton.Image = ((System.Drawing.Image)(resources.GetObject("NextButton.Image")));
            this.NextButton.Location = new System.Drawing.Point(791, 46);
            this.NextButton.Margin = new System.Windows.Forms.Padding(0);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(33, 35);
            this.NextButton.TabIndex = 6;
            this.NextButton.TabStop = false;
            this.NextButton.Tag = "Next";
            this.NextButton.UseVisualStyleBackColor = false;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.sqlQueryTextBox);
            this.panel1.Location = new System.Drawing.Point(18, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 102);
            this.panel1.TabIndex = 2;
            this.panel1.TabStop = true;
            // 
            // SaveMenuStrip
            // 
            this.SaveMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveMenu});
            this.SaveMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.SaveMenuStrip.Name = "SaveMenuStrip";
            this.SaveMenuStrip.Size = new System.Drawing.Size(864, 24);
            this.SaveMenuStrip.TabIndex = 164;
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
            // sqlRunButton
            // 
            this.sqlRunButton.ActualPermission = false;
            this.sqlRunButton.ApplyDisableBehaviour = true;
            this.sqlRunButton.AutoEllipsis = true;
            this.sqlRunButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sqlRunButton.BorderColor = System.Drawing.Color.Wheat;
            this.sqlRunButton.CommentPriority = false;
            this.sqlRunButton.EnableAutoPrint = false;
            this.sqlRunButton.Enabled = false;
            this.sqlRunButton.FilterStatus = false;
            this.sqlRunButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.sqlRunButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sqlRunButton.FocusRectangleEnabled = true;
            this.sqlRunButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqlRunButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sqlRunButton.ImageSelected = false;
            this.sqlRunButton.Location = new System.Drawing.Point(117, 11);
            this.sqlRunButton.Name = "sqlRunButton";
            this.sqlRunButton.NewPadding = 5;
            this.sqlRunButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.sqlRunButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.None;
            this.sqlRunButton.Size = new System.Drawing.Size(93, 29);
            this.sqlRunButton.StatusIndicator = false;
            this.sqlRunButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sqlRunButton.StatusOffText = null;
            this.sqlRunButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.sqlRunButton.StatusOnText = null;
            this.sqlRunButton.TabIndex = 165;
            this.sqlRunButton.TabStop = false;
            this.sqlRunButton.Text = "Run";
            this.sqlRunButton.UseVisualStyleBackColor = false;
            this.sqlRunButton.Click += new System.EventHandler(this.SqlRunButton_Click);
            // 
            // PreviewButton
            // 
            this.PreviewButton.ActualPermission = false;
            this.PreviewButton.ApplyDisableBehaviour = false;
            this.PreviewButton.AutoEllipsis = true;
            this.PreviewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(150)))), ((int)(((byte)(94)))));
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
            this.PreviewButton.Location = new System.Drawing.Point(216, 11);
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.NewPadding = 5;
            this.PreviewButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.PreviewButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.Print;
            this.PreviewButton.Size = new System.Drawing.Size(93, 29);
            this.PreviewButton.StatusIndicator = false;
            this.PreviewButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PreviewButton.StatusOffText = null;
            this.PreviewButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.PreviewButton.StatusOnText = null;
            this.PreviewButton.TabIndex = 166;
            this.PreviewButton.TabStop = false;
            this.PreviewButton.Text = "Preview";
            this.PreviewButton.UseVisualStyleBackColor = false;
            this.PreviewButton.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // ExcelButton
            // 
            this.ExcelButton.ActualPermission = false;
            this.ExcelButton.ApplyDisableBehaviour = false;
            this.ExcelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ExcelButton.BorderColor = System.Drawing.Color.Wheat;
            this.ExcelButton.CommentPriority = false;
            this.ExcelButton.EnableAutoPrint = false;
            this.ExcelButton.FilterStatus = false;
            this.ExcelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ExcelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExcelButton.FocusRectangleEnabled = true;
            this.ExcelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExcelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ExcelButton.ImageSelected = false;
            this.ExcelButton.Location = new System.Drawing.Point(315, 11);
            this.ExcelButton.Name = "ExcelButton";
            this.ExcelButton.NewPadding = 5;
            this.ExcelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.ExcelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ExcelButton.Size = new System.Drawing.Size(93, 29);
            this.ExcelButton.StatusIndicator = false;
            this.ExcelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ExcelButton.StatusOffText = null;
            this.ExcelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ExcelButton.StatusOnText = null;
            this.ExcelButton.TabIndex = 167;
            this.ExcelButton.TabStop = false;
            this.ExcelButton.Text = "Excel";
            this.ExcelButton.UseVisualStyleBackColor = false;
            this.ExcelButton.Click += new System.EventHandler(this.ExcelButton_Click);
            // 
            // ResultPreviewDialog
            // 
            this.ResultPreviewDialog.AutoSize = true;
            this.ResultPreviewDialog.Document = this.ResultPrintDocument;
            this.ResultPreviewDialog.Name = "ultraPrintPreviewDialog1";
            this.ResultPreviewDialog.Load += new System.EventHandler(this.ResultPreviewDialog_Load);
            // 
            // ResultPrintDocument
            // 
            this.ResultPrintDocument.DocumentName = "Preview";
            this.ResultPrintDocument.Grid = this.ResultGridView;
            this.ResultPrintDocument.Header.TextCenter = "";
            this.ResultPrintDocument.Header.TextLeft = "";
            // 
            // ExcelExporter
            // 
            this.ExcelExporter.FileLimitBehaviour = Infragistics.Win.UltraWinGrid.ExcelExport.FileLimitBehaviour.TruncateData;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.panel5.Location = new System.Drawing.Point(18, 637);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(825, 2);
            this.panel5.TabIndex = 168;
            // 
            // F9015
            // 
            this.AccessibleName = "SQL Support";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.ExcelButton);
            this.Controls.Add(this.PreviewButton);
            this.Controls.Add(this.sqlRunButton);
            this.Controls.Add(this.SaveMenuStrip);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.PreviousButton);
            this.Controls.Add(this.sqlSaveButton);
            this.Controls.Add(this.SqlLibraryCombo);
            this.Controls.Add(this.SqlCategoryCombo);
            this.Controls.Add(this.formIDLabel);
            this.Controls.Add(this.resultPanel);
            this.Controls.Add(this.headerLabel);
            this.MinimumSize = new System.Drawing.Size(862, 710);
            this.Name = "F9015";
            this.Size = new System.Drawing.Size(864, 710);
            this.Tag = "9015";
            this.Load += new System.EventHandler(this.F9015_Load);
            this.resultPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResultGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.SaveMenuStrip.ResumeLayout(false);
            this.SaveMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.Label formIDLabel;
        private System.Windows.Forms.Timer k;
        private System.Windows.Forms.TextBox sqlQueryTextBox;
        private TerraScan.UI.Controls.TerraScanComboBox SqlCategoryCombo;
        private TerraScan.UI.Controls.TerraScanComboBox SqlLibraryCombo;
        private TerraScan.UI.Controls.TerraScanButton  sqlSaveButton;
        protected internal System.Windows.Forms.Button PreviousButton;
        protected internal System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip SaveMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SaveMenu;
        private TerraScan.UI.Controls.TerraScanButton sqlRunButton;
        private TerraScan.UI.Controls.TerraScanButton PreviewButton;
        private TerraScan.UI.Controls.TerraScanButton ExcelButton;
        private TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid ResultGridView;
        private Infragistics.Win.Printing.UltraPrintPreviewDialog ResultPreviewDialog;
        private Infragistics.Win.UltraWinGrid.UltraGridPrintDocument ResultPrintDocument;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ExcelExporter;
        private System.Windows.Forms.Panel panel5;
    }
}
