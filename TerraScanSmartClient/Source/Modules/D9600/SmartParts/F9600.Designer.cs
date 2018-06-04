namespace D9600
{
    partial class F9600
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9600));
            this.panel5 = new System.Windows.Forms.Panel();
            this.formIDLabel = new System.Windows.Forms.Label();
            this.SearchResultGrid = new TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid();
            this.SearchResultPictureBox = new System.Windows.Forms.PictureBox();
            this.SearchMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SearchMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportExcelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SearchButton = new TerraScan.UI.Controls.TerraScanButton();
            this.PreviewButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SearchTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.ExcelExporter = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.SearchPreviewDialog = new Infragistics.Win.Printing.UltraPrintPreviewDialog(this.components);
            this.SearchPrintDocument = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(this.components);
            this.ScrollBarPanel = new System.Windows.Forms.Panel();
            this.T2SearchPanel = new System.Windows.Forms.Panel();
            this.SearchDataSource = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.formHeaderSmartPartdeckWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SearchEngineToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.HelpLink = new TerraScan.SmartParts.HelpSmartPart();
            this.RecordsCountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SearchResultGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchResultPictureBox)).BeginInit();
            this.SearchMenuStrip.SuspendLayout();
            this.T2SearchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchDataSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.panel5.Location = new System.Drawing.Point(19, 637);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(807, 2);
            this.panel5.TabIndex = 117;
            // 
            // formIDLabel
            // 
            this.formIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.formIDLabel.AutoSize = true;
            this.formIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.formIDLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.formIDLabel.Location = new System.Drawing.Point(16, 643);
            this.formIDLabel.Name = "formIDLabel";
            this.formIDLabel.Size = new System.Drawing.Size(35, 15);
            this.formIDLabel.TabIndex = 116;
            this.formIDLabel.Text = "9600";
            // 
            // SearchResultGrid
            // 
            this.SearchResultGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance1.BorderColor = System.Drawing.Color.Black;
            appearance1.FontData.BoldAsString = "True";
            appearance1.FontData.Name = "Arial";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.TextHAlignAsString = "Left";
            appearance1.TextVAlignAsString = "Middle";
            this.SearchResultGrid.DisplayLayout.Appearance = appearance1;
            this.SearchResultGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            appearance2.BackColor = System.Drawing.Color.Gray;
            ultraGridBand1.Override.HeaderAppearance = appearance2;
            this.SearchResultGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.SearchResultGrid.DisplayLayout.GroupByBox.Hidden = true;
            this.SearchResultGrid.DisplayLayout.InterBandSpacing = 10;
            this.SearchResultGrid.DisplayLayout.MaxRowScrollRegions = 1;
            this.SearchResultGrid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.SearchResultGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.SearchResultGrid.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.None;
            this.SearchResultGrid.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.SearchResultGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.SearchResultGrid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.SearchResultGrid.DisplayLayout.Override.CardAreaAppearance = appearance3;
            appearance4.BorderColor = System.Drawing.Color.Black;
            this.SearchResultGrid.DisplayLayout.Override.CellAppearance = appearance4;
            this.SearchResultGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect;
            this.SearchResultGrid.DisplayLayout.Override.ColumnSizingArea = Infragistics.Win.UltraWinGrid.ColumnSizingArea.HeadersOnly;
            appearance5.BackColor = System.Drawing.Color.Gray;
            appearance5.BackColor2 = System.Drawing.Color.Gray;
            appearance5.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            appearance5.ForeColor = System.Drawing.Color.White;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            appearance5.TextHAlignAsString = "Left";
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisPath;
            appearance5.TextVAlignAsString = "Middle";
            appearance5.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.SearchResultGrid.DisplayLayout.Override.HeaderAppearance = appearance5;
            this.SearchResultGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance6.TextHAlignAsString = "Left";
            appearance6.TextVAlignAsString = "Middle";
            this.SearchResultGrid.DisplayLayout.Override.MergedCellAppearance = appearance6;
            this.SearchResultGrid.DisplayLayout.Override.MergedCellStyle = Infragistics.Win.UltraWinGrid.MergedCellStyle.Never;
            appearance7.BackColor = System.Drawing.Color.LightGray;
            this.SearchResultGrid.DisplayLayout.Override.RowAlternateAppearance = appearance7;
            appearance8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(195)))), ((int)(((byte)(198)))));
            this.SearchResultGrid.DisplayLayout.Override.RowSelectorAppearance = appearance8;
            this.SearchResultGrid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
            this.SearchResultGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed;
            appearance9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            appearance9.ForeColor = System.Drawing.Color.White;
            this.SearchResultGrid.DisplayLayout.Override.SelectedCellAppearance = appearance9;
            this.SearchResultGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.SearchResultGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.SingleAutoDrag;
            this.SearchResultGrid.DisplayLayout.RowConnectorColor = System.Drawing.Color.Gray;
            this.SearchResultGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.SearchResultGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;
            this.SearchResultGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.SearchResultGrid.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.SearchResultGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.SearchResultGrid.Location = new System.Drawing.Point(-1, -1);
            this.SearchResultGrid.Name = "SearchResultGrid";
            this.SearchResultGrid.Size = new System.Drawing.Size(772, 532);
            this.SearchResultGrid.TabIndex = 1;
            this.SearchResultGrid.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.SearchResultGrid_InitializeRow);
            this.SearchResultGrid.Click += new System.EventHandler(this.SearchResultGrid_Click);
            this.SearchResultGrid.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.SearchResultGrid_AfterSelectChange);
            this.SearchResultGrid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.SearchResultGrid_InitializeLayout);
            this.SearchResultGrid.MouseLeaveElement += new Infragistics.Win.UIElementEventHandler(this.SearchResultGrid_MouseLeaveElement);
            this.SearchResultGrid.DoubleClickCell += new Infragistics.Win.UltraWinGrid.DoubleClickCellEventHandler(this.SearchResultGrid_DoubleClickCell);
            this.SearchResultGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchResultGrid_KeyPress);
            this.SearchResultGrid.MouseEnterElement += new Infragistics.Win.UIElementEventHandler(this.SearchResultGrid_MouseEnterElement);
            this.SearchResultGrid.AfterSortChange += new Infragistics.Win.UltraWinGrid.BandEventHandler(this.SearchResultGrid_AfterSortChange);
            // 
            // SearchResultPictureBox
            // 
            this.SearchResultPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.SearchResultPictureBox.ErrorImage = null;
            this.SearchResultPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("SearchResultPictureBox.Image")));
            this.SearchResultPictureBox.Location = new System.Drawing.Point(784, 99);
            this.SearchResultPictureBox.Name = "SearchResultPictureBox";
            this.SearchResultPictureBox.Size = new System.Drawing.Size(42, 532);
            this.SearchResultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SearchResultPictureBox.TabIndex = 173;
            this.SearchResultPictureBox.TabStop = false;
            // 
            // SearchMenuStrip
            // 
            this.SearchMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SearchMenu});
            this.SearchMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.SearchMenuStrip.Name = "SearchMenuStrip";
            this.SearchMenuStrip.Size = new System.Drawing.Size(864, 24);
            this.SearchMenuStrip.TabIndex = 174;
            this.SearchMenuStrip.Text = "menuStrip1";
            this.SearchMenuStrip.Visible = false;
            // 
            // SearchMenu
            // 
            this.SearchMenu.Name = "SearchMenu";
            this.SearchMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SearchMenu.Size = new System.Drawing.Size(52, 20);
            this.SearchMenu.Text = "Search";
            this.SearchMenu.Click += new System.EventHandler(this.SearchMenu_Click);
            // 
            // ExportExcelButton
            // 
            this.ExportExcelButton.ActualPermission = false;
            this.ExportExcelButton.ApplyDisableBehaviour = false;
            this.ExportExcelButton.AutoEllipsis = true;
            this.ExportExcelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ExportExcelButton.BorderColor = System.Drawing.Color.Wheat;
            this.ExportExcelButton.CommentPriority = false;
            this.ExportExcelButton.EnableAutoPrint = false;
            this.ExportExcelButton.FilterStatus = false;
            this.ExportExcelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ExportExcelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportExcelButton.FocusRectangleEnabled = true;
            this.ExportExcelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportExcelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ExportExcelButton.ImageSelected = false;
            this.ExportExcelButton.Location = new System.Drawing.Point(488, 17);
            this.ExportExcelButton.Name = "ExportExcelButton";
            this.ExportExcelButton.NewPadding = 5;
            this.ExportExcelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.ExportExcelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ExportExcelButton.Size = new System.Drawing.Size(110, 30);
            this.ExportExcelButton.StatusIndicator = false;
            this.ExportExcelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ExportExcelButton.StatusOffText = null;
            this.ExportExcelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ExportExcelButton.StatusOnText = null;
            this.ExportExcelButton.TabIndex = 8;
            this.ExportExcelButton.TabStop = false;
            this.ExportExcelButton.Tag = "";
            this.ExportExcelButton.Text = "To Excel";
            this.ExportExcelButton.UseVisualStyleBackColor = false;
            this.ExportExcelButton.Click += new System.EventHandler(this.ExportExcelButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.ActualPermission = false;
            this.SearchButton.ApplyDisableBehaviour = false;
            this.SearchButton.AutoEllipsis = true;
            this.SearchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SearchButton.BorderColor = System.Drawing.Color.Wheat;
            this.SearchButton.CommentPriority = false;
            this.SearchButton.EnableAutoPrint = false;
            this.SearchButton.Enabled = false;
            this.SearchButton.FilterStatus = false;
            this.SearchButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchButton.FocusRectangleEnabled = true;
            this.SearchButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SearchButton.ImageSelected = false;
            this.SearchButton.Location = new System.Drawing.Point(243, 17);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.NewPadding = 5;
            this.SearchButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.SearchButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SearchButton.Size = new System.Drawing.Size(110, 30);
            this.SearchButton.StatusIndicator = false;
            this.SearchButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SearchButton.StatusOffText = null;
            this.SearchButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SearchButton.StatusOnText = null;
            this.SearchButton.TabIndex = 7;
            this.SearchButton.TabStop = false;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
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
            this.PreviewButton.Location = new System.Drawing.Point(366, 17);
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.NewPadding = 5;
            this.PreviewButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.PreviewButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.Print;
            this.PreviewButton.Size = new System.Drawing.Size(110, 30);
            this.PreviewButton.StatusIndicator = false;
            this.PreviewButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PreviewButton.StatusOffText = null;
            this.PreviewButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.PreviewButton.StatusOnText = null;
            this.PreviewButton.TabIndex = 3;
            this.PreviewButton.TabStop = false;
            this.PreviewButton.Text = "Preview";
            this.PreviewButton.UseVisualStyleBackColor = false;
            this.PreviewButton.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.AllowClick = true;
            this.SearchTextBox.AllowNegativeSign = false;
            this.SearchTextBox.ApplyCFGFormat = false;
            this.SearchTextBox.ApplyCurrencyFormat = false;
            this.SearchTextBox.ApplyFocusColor = true;
            this.SearchTextBox.ApplyNegativeStandard = true;
            this.SearchTextBox.ApplyParentFocusColor = true;
            this.SearchTextBox.ApplyTimeFormat = false;
            this.SearchTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTextBox.CFromatWihoutSymbol = false;
            this.SearchTextBox.CheckForEmpty = false;
            this.SearchTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SearchTextBox.Digits = -1;
            this.SearchTextBox.EmptyDecimalValue = false;
            this.SearchTextBox.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.SearchTextBox.ForeColor = System.Drawing.Color.Black;
            this.SearchTextBox.IsEditable = true;
            this.SearchTextBox.IsQueryableFileld = true;
            this.SearchTextBox.Location = new System.Drawing.Point(18, 17);
            this.SearchTextBox.LockKeyPress = false;
            this.SearchTextBox.MaxLength = 50;
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.PersistDefaultColor = false;
            this.SearchTextBox.Precision = 2;
            this.SearchTextBox.QueryingFileldName = "";
            this.SearchTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.SearchTextBox.Size = new System.Drawing.Size(219, 29);
            this.SearchTextBox.SpecialCharacter = "%";
            this.SearchTextBox.TabIndex = 0;
            this.SearchTextBox.TextCustomFormat = "0.0";
            this.SearchTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.SearchTextBox.WholeInteger = false;
            this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            this.SearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBox_KeyDown);
            // 
            // ExcelExporter
            // 
            this.ExcelExporter.FileLimitBehaviour = Infragistics.Win.UltraWinGrid.ExcelExport.FileLimitBehaviour.TruncateData;
            // 
            // SearchPreviewDialog
            // 
            this.SearchPreviewDialog.AutoSize = true;
            this.SearchPreviewDialog.Document = this.SearchPrintDocument;
            this.SearchPreviewDialog.Name = "ultraPrintPreviewDialog1";
            this.SearchPreviewDialog.Load += new System.EventHandler(this.SearchPreviewDialog_Load);
            // 
            // SearchPrintDocument
            // 
            this.SearchPrintDocument.DocumentName = "Preview";
            this.SearchPrintDocument.Grid = this.SearchResultGrid;
            // 
            // ScrollBarPanel
            // 
            this.ScrollBarPanel.BackColor = System.Drawing.Color.Gray;
            this.ScrollBarPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScrollBarPanel.Location = new System.Drawing.Point(800, -1);
            this.ScrollBarPanel.Name = "ScrollBarPanel";
            this.ScrollBarPanel.Size = new System.Drawing.Size(18, 25);
            this.ScrollBarPanel.TabIndex = 168;
            // 
            // T2SearchPanel
            // 
            this.T2SearchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.T2SearchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.T2SearchPanel.Controls.Add(this.ScrollBarPanel);
            this.T2SearchPanel.Controls.Add(this.SearchResultGrid);
            this.T2SearchPanel.Location = new System.Drawing.Point(18, 99);
            this.T2SearchPanel.Name = "T2SearchPanel";
            this.T2SearchPanel.Size = new System.Drawing.Size(773, 532);
            this.T2SearchPanel.TabIndex = 118;
            // 
            // SearchDataSource
            // 
            this.SearchDataSource.AllowAdd = false;
            this.SearchDataSource.AllowDelete = false;
            // 
            // formHeaderSmartPartdeckWorkspace
            // 
            this.formHeaderSmartPartdeckWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.formHeaderSmartPartdeckWorkspace.Location = new System.Drawing.Point(652, 0);
            this.formHeaderSmartPartdeckWorkspace.Name = "formHeaderSmartPartdeckWorkspace";
            this.formHeaderSmartPartdeckWorkspace.Size = new System.Drawing.Size(200, 48);
            this.formHeaderSmartPartdeckWorkspace.TabIndex = 175;
            this.formHeaderSmartPartdeckWorkspace.TabStop = false;
            this.formHeaderSmartPartdeckWorkspace.Text = "FormHeaderSmartPart";
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.HelpLink.BackColor = System.Drawing.Color.White;
            this.HelpLink.FormId = "9600";
            this.HelpLink.Location = new System.Drawing.Point(396, 643);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(43, 27);
            this.HelpLink.TabIndex = 176;
            this.HelpLink.Tag = "9600";
            this.HelpLink.VisibleHelpButton = false;
            this.HelpLink.VisibleHelpLinkButton = true;
            // 
            // RecordsCountLabel
            // 
            this.RecordsCountLabel.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordsCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.RecordsCountLabel.Location = new System.Drawing.Point(643, 62);
            this.RecordsCountLabel.Name = "RecordsCountLabel";
            this.RecordsCountLabel.Size = new System.Drawing.Size(182, 23);
            this.RecordsCountLabel.TabIndex = 216;
            this.RecordsCountLabel.Text = "Rows";
            this.RecordsCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // F9600
            // 
            this.AccessibleName = "Search Engine";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.RecordsCountLabel);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.formHeaderSmartPartdeckWorkspace);
            this.Controls.Add(this.SearchMenuStrip);
            this.Controls.Add(this.T2SearchPanel);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.formIDLabel);
            this.Controls.Add(this.ExportExcelButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.PreviewButton);
            this.Controls.Add(this.SearchTextBox);
            this.Controls.Add(this.SearchResultPictureBox);
            this.MinimumSize = new System.Drawing.Size(862, 710);
            this.Name = "F9600";
            this.Size = new System.Drawing.Size(864, 710);
            this.Tag = "9600";
            this.Load += new System.EventHandler(this.F9600_Load);
            this.Resize += new System.EventHandler(this.F9600_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.F9600_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.SearchResultGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchResultPictureBox)).EndInit();
            this.SearchMenuStrip.ResumeLayout(false);
            this.SearchMenuStrip.PerformLayout();
            this.T2SearchPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SearchDataSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanTextBox SearchTextBox;
        private TerraScan.UI.Controls.TerraScanButton PreviewButton;
        private TerraScan.UI.Controls.TerraScanButton SearchButton;
        private TerraScan.UI.Controls.TerraScanButton ExportExcelButton;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label formIDLabel;
        private TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid SearchResultGrid;
        private System.Windows.Forms.PictureBox SearchResultPictureBox;
        private System.Windows.Forms.MenuStrip SearchMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SearchMenu;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ExcelExporter;
        private Infragistics.Win.Printing.UltraPrintPreviewDialog SearchPreviewDialog;
        private Infragistics.Win.UltraWinGrid.UltraGridPrintDocument SearchPrintDocument;
        private System.Windows.Forms.Panel ScrollBarPanel;
        private System.Windows.Forms.Panel T2SearchPanel;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource SearchDataSource;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace formHeaderSmartPartdeckWorkspace;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.ToolTip SearchEngineToolTip;
        private TerraScan.SmartParts.HelpSmartPart HelpLink;
        private System.Windows.Forms.Label RecordsCountLabel;        
    }
}
