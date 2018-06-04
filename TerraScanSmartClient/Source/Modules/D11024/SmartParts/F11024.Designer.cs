namespace D11024
{
    partial class F11024
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FromAccountID", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FromAccount", 1);
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ToAccountID", 2);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ToAccount", 3);
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description", 4);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TransferAmount", 5);
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
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
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinScrollBar.ScrollBarLook scrollBarLook1 = new Infragistics.Win.UltraWinScrollBar.ScrollBarLook();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F11024));
            this.JournalEntryGridPanel = new System.Windows.Forms.Panel();
            this.JournalEntryGrid = new TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid();
            this.ReceiptItemGridVscrollBar = new System.Windows.Forms.VScrollBar();
            this.HiddenButton = new TerraScan.UI.Controls.TerraScanButton();
            this.TransferDatePanel = new System.Windows.Forms.Panel();
            this.ReceiptDateCalenderButton = new System.Windows.Forms.Button();
            this.TransferDateLabel = new System.Windows.Forms.Label();
            this.TransferDateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TransferDescriptionPanel = new System.Windows.Forms.Panel();
            this.TransferDescriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TransferDescriptionLabel = new System.Windows.Forms.Label();
            this.MultilpejournalEntryPictureBox = new System.Windows.Forms.PictureBox();
            this.NewButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FromTemplateButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SaveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormHeaderDeckWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.ReceiptMonthCalender = new TerraScan.UI.Controls.TerraScanMonthCalender();
            //Added for TSBG - 11024 Multiple Journal Entry - wrong form number passed to Help SP 
            this.HelplinkLabel = new System.Windows.Forms.LinkLabel();
            this.BottomFormPanel = new System.Windows.Forms.Panel();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.JournalEntryGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JournalEntryGrid)).BeginInit();
            this.TransferDatePanel.SuspendLayout();
            this.TransferDescriptionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MultilpejournalEntryPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // JournalEntryGridPanel
            // 
            this.JournalEntryGridPanel.BackColor = System.Drawing.Color.Silver;
            this.JournalEntryGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JournalEntryGridPanel.Controls.Add(this.JournalEntryGrid);
            this.JournalEntryGridPanel.Controls.Add(this.ReceiptItemGridVscrollBar);
            this.JournalEntryGridPanel.Controls.Add(this.HiddenButton);
            this.JournalEntryGridPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.JournalEntryGridPanel.Location = new System.Drawing.Point(48, 127);
            this.JournalEntryGridPanel.Name = "JournalEntryGridPanel";
            this.JournalEntryGridPanel.Size = new System.Drawing.Size(768, 312);
            this.JournalEntryGridPanel.TabIndex = 6;
            this.JournalEntryGridPanel.TabStop = true;
            // 
            // JournalEntryGrid
            // 
            this.JournalEntryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance1.BorderColor = System.Drawing.Color.Black;
            appearance1.FontData.BoldAsString = "True";
            appearance1.FontData.Name = "Arial";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColorDisabled = System.Drawing.Color.White;
            appearance1.TextHAlignAsString = "Left";
            appearance1.TextVAlignAsString = "Middle";
            this.JournalEntryGrid.DisplayLayout.Appearance = appearance1;
            this.JournalEntryGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            appearance23.TextHAlignAsString = "Left";
            ultraGridColumn2.CellAppearance = appearance23;
            appearance20.TextHAlignAsString = "Center";
            ultraGridColumn2.Header.Appearance = appearance20;
            ultraGridColumn2.Header.Caption = "From Account";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 300;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            appearance24.TextHAlignAsString = "Left";
            ultraGridColumn4.CellAppearance = appearance24;
            appearance21.TextHAlignAsString = "Center";
            ultraGridColumn4.Header.Appearance = appearance21;
            ultraGridColumn4.Header.Caption = "To Account";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 300;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Hidden = true;
            appearance26.TextHAlignAsString = "Right";
            ultraGridColumn6.CellAppearance = appearance26;
            appearance22.TextHAlignAsString = "Center";
            ultraGridColumn6.Header.Appearance = appearance22;
            ultraGridColumn6.Header.Caption = "Transfer Amount";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6});
            appearance2.BackColor = System.Drawing.Color.Gray;
            appearance2.ForeColor = System.Drawing.Color.White;
            ultraGridBand1.Override.HeaderAppearance = appearance2;
            this.JournalEntryGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance3.BackColor = System.Drawing.Color.LightGray;
            appearance3.BorderColor = System.Drawing.Color.Black;
            this.JournalEntryGrid.DisplayLayout.EmptyRowSettings.CellAppearance = appearance3;
            appearance4.BackColor = System.Drawing.Color.LightGray;
            this.JournalEntryGrid.DisplayLayout.EmptyRowSettings.RowAppearance = appearance4;
            this.JournalEntryGrid.DisplayLayout.EmptyRowSettings.Style = Infragistics.Win.UltraWinGrid.EmptyRowStyle.AlignWithDataRows;
            this.JournalEntryGrid.DisplayLayout.GroupByBox.Hidden = true;
            this.JournalEntryGrid.DisplayLayout.InterBandSpacing = 10;
            this.JournalEntryGrid.DisplayLayout.MaxColScrollRegions = 1;
            this.JournalEntryGrid.DisplayLayout.MaxRowScrollRegions = 1;
            this.JournalEntryGrid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
            this.JournalEntryGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinBand;
            this.JournalEntryGrid.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.None;
            this.JournalEntryGrid.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.JournalEntryGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.True;
            this.JournalEntryGrid.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            this.JournalEntryGrid.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.JournalEntryGrid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.JournalEntryGrid.DisplayLayout.Override.CardAreaAppearance = appearance5;
            appearance6.BorderColor = System.Drawing.Color.Black;
            this.JournalEntryGrid.DisplayLayout.Override.CellAppearance = appearance6;
            this.JournalEntryGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            appearance7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.JournalEntryGrid.DisplayLayout.Override.EditCellAppearance = appearance7;
            appearance8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.JournalEntryGrid.DisplayLayout.Override.FilterCellAppearanceActive = appearance8;
            this.JournalEntryGrid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Cell;
            appearance9.FontData.BoldAsString = "True";
            appearance9.FontData.Name = "Arial";
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.ForeColor = System.Drawing.Color.Black;
            this.JournalEntryGrid.DisplayLayout.Override.FilteredInCellAppearance = appearance9;
            this.JournalEntryGrid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            this.JournalEntryGrid.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Combo;
            this.JournalEntryGrid.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.StartsWith;
            this.JournalEntryGrid.DisplayLayout.Override.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)(((((((((((((((((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.NotEquals)
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
            this.JournalEntryGrid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            appearance10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(204)))), ((int)(((byte)(167)))));
            appearance10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.JournalEntryGrid.DisplayLayout.Override.FilterRowAppearance = appearance10;
            appearance11.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.JournalEntryGrid.DisplayLayout.Override.FilterRowPromptAppearance = appearance11;
            this.JournalEntryGrid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            this.JournalEntryGrid.DisplayLayout.Override.FixedRowIndicator = Infragistics.Win.UltraWinGrid.FixedRowIndicator.None;
            this.JournalEntryGrid.DisplayLayout.Override.FixedRowSortOrder = Infragistics.Win.UltraWinGrid.FixedRowSortOrder.FixOrder;
            this.JournalEntryGrid.DisplayLayout.Override.FixedRowStyle = Infragistics.Win.UltraWinGrid.FixedRowStyle.Top;
            this.JournalEntryGrid.DisplayLayout.Override.GroupBySummaryDisplayStyle = Infragistics.Win.UltraWinGrid.GroupBySummaryDisplayStyle.SummaryCellsAlwaysBelowDescription;
            appearance12.BackColor = System.Drawing.Color.Gray;
            appearance12.BackColor2 = System.Drawing.Color.Gray;
            appearance12.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            appearance12.ForeColor = System.Drawing.Color.White;
            appearance12.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            appearance12.TextHAlignAsString = "Left";
            appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisPath;
            appearance12.TextVAlignAsString = "Middle";
            appearance12.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.JournalEntryGrid.DisplayLayout.Override.HeaderAppearance = appearance12;
            appearance13.TextHAlignAsString = "Left";
            appearance13.TextVAlignAsString = "Middle";
            this.JournalEntryGrid.DisplayLayout.Override.MergedCellAppearance = appearance13;
            this.JournalEntryGrid.DisplayLayout.Override.MergedCellStyle = Infragistics.Win.UltraWinGrid.MergedCellStyle.Never;
            appearance14.BackColor = System.Drawing.Color.LightGray;
            this.JournalEntryGrid.DisplayLayout.Override.RowAlternateAppearance = appearance14;
            appearance15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(195)))), ((int)(((byte)(198)))));
            this.JournalEntryGrid.DisplayLayout.Override.RowSelectorAppearance = appearance15;
            this.JournalEntryGrid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
            this.JournalEntryGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed;
            appearance16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            appearance16.ForeColor = System.Drawing.Color.White;
            this.JournalEntryGrid.DisplayLayout.Override.SelectedCellAppearance = appearance16;
            this.JournalEntryGrid.DisplayLayout.Override.SelectedRowAppearance = appearance16;
            this.JournalEntryGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.JournalEntryGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.JournalEntryGrid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FixedRows;
            appearance17.BackColor = System.Drawing.Color.Black;
            this.JournalEntryGrid.DisplayLayout.Override.SpecialRowSeparatorAppearance = appearance17;
            this.JournalEntryGrid.DisplayLayout.Override.SpecialRowSeparatorHeight = 4;
            this.JournalEntryGrid.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.None;
            appearance18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(156)))));
            appearance18.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.JournalEntryGrid.DisplayLayout.Override.SummaryValueAppearance = appearance18;
            this.JournalEntryGrid.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.JournalEntryGrid.DisplayLayout.RowConnectorColor = System.Drawing.Color.Gray;
            this.JournalEntryGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            appearance19.BackColor = System.Drawing.Color.DarkGray;
            scrollBarLook1.Appearance = appearance19;
            this.JournalEntryGrid.DisplayLayout.ScrollBarLook = scrollBarLook1;
            this.JournalEntryGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;
            this.JournalEntryGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.JournalEntryGrid.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
            this.JournalEntryGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.JournalEntryGrid.Location = new System.Drawing.Point(-1, -1);
            this.JournalEntryGrid.Name = "JournalEntryGrid";
            this.JournalEntryGrid.Size = new System.Drawing.Size(768, 312);
            this.JournalEntryGrid.TabIndex = 5;
            this.JournalEntryGrid.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChangeOrLostFocus;
            this.JournalEntryGrid.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.JournalEntryGrid.AfterExitEditMode += new System.EventHandler(this.JournalEntryGrid_AfterExitEditMode);
            this.JournalEntryGrid.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.JournalEntryGrid_BeforeRowsDeleted);
            this.JournalEntryGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.JournalEntryGrid_KeyDown);
            this.JournalEntryGrid.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.JournalEntryGrid_InitializeRow);
            this.JournalEntryGrid.AfterRowsDeleted += new System.EventHandler(this.JournalEntryGrid_AfterRowsDeleted);
            this.JournalEntryGrid.BeforeExitEditMode += new Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventHandler(this.JournalEntryGrid_BeforeExitEditMode);
            this.JournalEntryGrid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.JournalEntryGrid_InitializeLayout);
            this.JournalEntryGrid.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.JournalEntryGrid_CellChange);
            // 
            // ReceiptItemGridVscrollBar
            // 
            this.ReceiptItemGridVscrollBar.Enabled = false;
            this.ReceiptItemGridVscrollBar.Location = new System.Drawing.Point(713, 0);
            this.ReceiptItemGridVscrollBar.Name = "ReceiptItemGridVscrollBar";
            this.ReceiptItemGridVscrollBar.Size = new System.Drawing.Size(16, 272);
            this.ReceiptItemGridVscrollBar.TabIndex = 6;
            // 
            // HiddenButton
            // 
            this.HiddenButton.ActualPermission = false;
            this.HiddenButton.ApplyDisableBehaviour = false;
            this.HiddenButton.AutoEllipsis = true;
            this.HiddenButton.BackColor = System.Drawing.Color.Silver;
            this.HiddenButton.BorderColor = System.Drawing.Color.Wheat;
            this.HiddenButton.CommentPriority = false;
            this.HiddenButton.EnableAutoPrint = false;
            this.HiddenButton.FilterStatus = false;
            this.HiddenButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.HiddenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HiddenButton.FocusRectangleEnabled = true;
            this.HiddenButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HiddenButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.HiddenButton.ImageSelected = false;
            this.HiddenButton.Location = new System.Drawing.Point(485, 74);
            this.HiddenButton.Name = "HiddenButton";
            this.HiddenButton.NewPadding = 5;
            this.HiddenButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.HiddenButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.None;
            this.HiddenButton.Size = new System.Drawing.Size(10, 10);
            this.HiddenButton.StatusIndicator = false;
            this.HiddenButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.HiddenButton.StatusOffText = null;
            this.HiddenButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.HiddenButton.StatusOnText = null;
            this.HiddenButton.TabIndex = 5;
            this.HiddenButton.TabStop = false;
            this.HiddenButton.UseVisualStyleBackColor = false;
            this.HiddenButton.Visible = false;
            // 
            // TransferDatePanel
            // 
            this.TransferDatePanel.BackColor = System.Drawing.Color.Transparent;
            this.TransferDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TransferDatePanel.Controls.Add(this.ReceiptDateCalenderButton);
            this.TransferDatePanel.Controls.Add(this.TransferDateLabel);
            this.TransferDatePanel.Controls.Add(this.TransferDateTextBox);
            this.TransferDatePanel.Location = new System.Drawing.Point(648, 88);
            this.TransferDatePanel.Name = "TransferDatePanel";
            this.TransferDatePanel.Size = new System.Drawing.Size(168, 40);
            this.TransferDatePanel.TabIndex = 1;
            this.TransferDatePanel.TabStop = true;
            // 
            // ReceiptDateCalenderButton
            // 
            this.ReceiptDateCalenderButton.FlatAppearance.BorderSize = 0;
            this.ReceiptDateCalenderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReceiptDateCalenderButton.Image = ((System.Drawing.Image)(resources.GetObject("ReceiptDateCalenderButton.Image")));
            this.ReceiptDateCalenderButton.Location = new System.Drawing.Point(140, 11);
            this.ReceiptDateCalenderButton.Name = "ReceiptDateCalenderButton";
            this.ReceiptDateCalenderButton.Size = new System.Drawing.Size(19, 21);
            this.ReceiptDateCalenderButton.TabIndex = 3;
            this.ReceiptDateCalenderButton.Tag = "ReceiptDateTextBox";
            this.ReceiptDateCalenderButton.UseVisualStyleBackColor = true;
            this.ReceiptDateCalenderButton.Click += new System.EventHandler(this.ReceiptDateCalenderButton_Click);
            // 
            // TransferDateLabel
            // 
            this.TransferDateLabel.AutoSize = true;
            this.TransferDateLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.TransferDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TransferDateLabel.Location = new System.Drawing.Point(3, 0);
            this.TransferDateLabel.Name = "TransferDateLabel";
            this.TransferDateLabel.Size = new System.Drawing.Size(84, 14);
            this.TransferDateLabel.TabIndex = 2;
            this.TransferDateLabel.Text = "Transfer Date:";
            this.TransferDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TransferDateTextBox
            // 
            this.TransferDateTextBox.AllowClick = true;
            this.TransferDateTextBox.AllowNegativeSign = false;
            this.TransferDateTextBox.ApplyCFGFormat = false;
            this.TransferDateTextBox.ApplyCurrencyFormat = false;
            this.TransferDateTextBox.ApplyFocusColor = true;
            this.TransferDateTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.TransferDateTextBox.ApplyNegativeStandard = true;
            this.TransferDateTextBox.ApplyParentFocusColor = true;
            this.TransferDateTextBox.ApplyTimeFormat = false;
            this.TransferDateTextBox.BackColor = System.Drawing.Color.White;
            this.TransferDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TransferDateTextBox.CFromatWihoutSymbol = false;
            this.TransferDateTextBox.CheckForEmpty = true;
            this.TransferDateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TransferDateTextBox.Digits = -1;
            this.TransferDateTextBox.EmptyDecimalValue = false;
            this.TransferDateTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.TransferDateTextBox.ForeColor = System.Drawing.Color.Black;
            this.TransferDateTextBox.IsEditable = false;
            this.TransferDateTextBox.IsQueryableFileld = true;
            this.TransferDateTextBox.Location = new System.Drawing.Point(28, 16);
            this.TransferDateTextBox.LockKeyPress = false;
            this.TransferDateTextBox.MaxLength = 10;
            this.TransferDateTextBox.Name = "TransferDateTextBox";
            this.TransferDateTextBox.PersistDefaultColor = false;
            this.TransferDateTextBox.Precision = 2;
            this.TransferDateTextBox.QueryingFileldName = "";
            this.TransferDateTextBox.SetColorFlag = false;
            this.TransferDateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.TransferDateTextBox.Size = new System.Drawing.Size(100, 16);
            this.TransferDateTextBox.SpecialCharacter = "%";
            this.TransferDateTextBox.TabIndex = 2;
            this.TransferDateTextBox.Tag = "";
            this.TransferDateTextBox.TextCustomFormat = "$#,##0.00";
            this.TransferDateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.TransferDateTextBox.WholeInteger = false;
            // 
            // TransferDescriptionPanel
            // 
            this.TransferDescriptionPanel.BackColor = System.Drawing.Color.White;
            this.TransferDescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TransferDescriptionPanel.Controls.Add(this.TransferDescriptionTextBox);
            this.TransferDescriptionPanel.Controls.Add(this.TransferDescriptionLabel);
            this.TransferDescriptionPanel.ImeMode = System.Windows.Forms.ImeMode.On;
            this.TransferDescriptionPanel.Location = new System.Drawing.Point(48, 88);
            this.TransferDescriptionPanel.Name = "TransferDescriptionPanel";
            this.TransferDescriptionPanel.Size = new System.Drawing.Size(601, 40);
            this.TransferDescriptionPanel.TabIndex = 0;
            this.TransferDescriptionPanel.TabStop = true;
            // 
            // TransferDescriptionTextBox
            // 
            this.TransferDescriptionTextBox.AllowClick = true;
            this.TransferDescriptionTextBox.AllowNegativeSign = false;
            this.TransferDescriptionTextBox.ApplyCFGFormat = false;
            this.TransferDescriptionTextBox.ApplyCurrencyFormat = false;
            this.TransferDescriptionTextBox.ApplyFocusColor = true;
            this.TransferDescriptionTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.TransferDescriptionTextBox.ApplyNegativeStandard = true;
            this.TransferDescriptionTextBox.ApplyParentFocusColor = true;
            this.TransferDescriptionTextBox.ApplyTimeFormat = false;
            this.TransferDescriptionTextBox.BackColor = System.Drawing.Color.White;
            this.TransferDescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TransferDescriptionTextBox.CFromatWihoutSymbol = false;
            this.TransferDescriptionTextBox.CheckForEmpty = false;
            this.TransferDescriptionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TransferDescriptionTextBox.Digits = -1;
            this.TransferDescriptionTextBox.EmptyDecimalValue = false;
            this.TransferDescriptionTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.TransferDescriptionTextBox.ForeColor = System.Drawing.Color.Black;
            this.TransferDescriptionTextBox.IsEditable = false;
            this.TransferDescriptionTextBox.IsQueryableFileld = false;
            this.TransferDescriptionTextBox.Location = new System.Drawing.Point(20, 16);
            this.TransferDescriptionTextBox.LockKeyPress = false;
            this.TransferDescriptionTextBox.MaxLength = 100;
            this.TransferDescriptionTextBox.Name = "TransferDescriptionTextBox";
            this.TransferDescriptionTextBox.PersistDefaultColor = false;
            this.TransferDescriptionTextBox.Precision = 2;
            this.TransferDescriptionTextBox.QueryingFileldName = "";
            this.TransferDescriptionTextBox.SetColorFlag = false;
            this.TransferDescriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.TransferDescriptionTextBox.Size = new System.Drawing.Size(552, 16);
            this.TransferDescriptionTextBox.SpecialCharacter = "%";
            this.TransferDescriptionTextBox.TabIndex = 1;
            this.TransferDescriptionTextBox.TextCustomFormat = "";
            this.TransferDescriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.TransferDescriptionTextBox.WholeInteger = false;
            // 
            // TransferDescriptionLabel
            // 
            this.TransferDescriptionLabel.AutoSize = true;
            this.TransferDescriptionLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.TransferDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TransferDescriptionLabel.Location = new System.Drawing.Point(0, 0);
            this.TransferDescriptionLabel.Name = "TransferDescriptionLabel";
            this.TransferDescriptionLabel.Size = new System.Drawing.Size(123, 14);
            this.TransferDescriptionLabel.TabIndex = 1;
            this.TransferDescriptionLabel.Text = "Transfer Description:";
            this.TransferDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MultilpejournalEntryPictureBox
            // 
            this.MultilpejournalEntryPictureBox.Location = new System.Drawing.Point(810, 88);
            this.MultilpejournalEntryPictureBox.Name = "MultilpejournalEntryPictureBox";
            this.MultilpejournalEntryPictureBox.Size = new System.Drawing.Size(42, 351);
            this.MultilpejournalEntryPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MultilpejournalEntryPictureBox.TabIndex = 161;
            this.MultilpejournalEntryPictureBox.TabStop = false;
            // 
            // NewButton
            // 
            this.NewButton.ActualPermission = false;
            this.NewButton.ApplyDisableBehaviour = false;
            this.NewButton.AutoSize = true;
            this.NewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.NewButton.BorderColor = System.Drawing.Color.Wheat;
            this.NewButton.CommentPriority = false;
            this.NewButton.EnableAutoPrint = false;
            this.NewButton.FilterStatus = false;
            this.NewButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.NewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewButton.FocusRectangleEnabled = true;
            this.NewButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.NewButton.ImageSelected = false;
            this.NewButton.Location = new System.Drawing.Point(48, 35);
            this.NewButton.Name = "NewButton";
            this.NewButton.NewPadding = 5;
            this.NewButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.NewButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.NewButton.Size = new System.Drawing.Size(100, 28);
            this.NewButton.StatusIndicator = false;
            this.NewButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NewButton.StatusOffText = null;
            this.NewButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.NewButton.StatusOnText = null;
            this.NewButton.TabIndex = 162;
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = false;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // FromTemplateButton
            // 
            this.FromTemplateButton.ActualPermission = false;
            this.FromTemplateButton.ApplyDisableBehaviour = false;
            this.FromTemplateButton.AutoSize = true;
            this.FromTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.FromTemplateButton.BorderColor = System.Drawing.Color.Wheat;
            this.FromTemplateButton.CommentPriority = false;
            this.FromTemplateButton.EnableAutoPrint = false;
            this.FromTemplateButton.FilterStatus = false;
            this.FromTemplateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.FromTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FromTemplateButton.FocusRectangleEnabled = true;
            this.FromTemplateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromTemplateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FromTemplateButton.ImageSelected = false;
            this.FromTemplateButton.Location = new System.Drawing.Point(48, 461);
            this.FromTemplateButton.Name = "FromTemplateButton";
            this.FromTemplateButton.NewPadding = 5;
            this.FromTemplateButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.FromTemplateButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.FromTemplateButton.Size = new System.Drawing.Size(120, 28);
            this.FromTemplateButton.StatusIndicator = false;
            this.FromTemplateButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FromTemplateButton.StatusOffText = null;
            this.FromTemplateButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.FromTemplateButton.StatusOnText = null;
            this.FromTemplateButton.TabIndex = 165;
            this.FromTemplateButton.Text = "From Template";
            this.FromTemplateButton.UseVisualStyleBackColor = false;
            this.FromTemplateButton.Click += new System.EventHandler(this.FromTemplateButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.ActualPermission = false;
            this.SaveButton.ApplyDisableBehaviour = false;
            this.SaveButton.AutoSize = true;
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SaveButton.BorderColor = System.Drawing.Color.Wheat;
            this.SaveButton.CommentPriority = false;
            this.SaveButton.EnableAutoPrint = false;
            this.SaveButton.FilterStatus = false;
            this.SaveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.FocusRectangleEnabled = true;
            this.SaveButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SaveButton.ImageSelected = false;
            this.SaveButton.Location = new System.Drawing.Point(154, 35);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.NewPadding = 5;
            this.SaveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Save;
            this.SaveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SaveButton.Size = new System.Drawing.Size(100, 28);
            this.SaveButton.StatusIndicator = false;
            this.SaveButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SaveButton.StatusOffText = null;
            this.SaveButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SaveButton.StatusOnText = null;
            this.SaveButton.TabIndex = 166;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.ActualPermission = false;
            this.CancelButton.ApplyDisableBehaviour = false;
            this.CancelButton.AutoSize = true;
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelButton.CommentPriority = false;
            this.CancelButton.EnableAutoPrint = false;
            this.CancelButton.FilterStatus = false;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.FocusRectangleEnabled = true;
            this.CancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelButton.ImageSelected = false;
            this.CancelButton.Location = new System.Drawing.Point(260, 35);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NewPadding = 5;
            this.CancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.CancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelButton.Size = new System.Drawing.Size(100, 28);
            this.CancelButton.StatusIndicator = false;
            this.CancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelButton.StatusOffText = null;
            this.CancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelButton.StatusOnText = null;
            this.CancelButton.TabIndex = 167;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // FormHeaderDeckWorkspace
            // 
            this.FormHeaderDeckWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FormHeaderDeckWorkspace.Location = new System.Drawing.Point(736, 7);
            this.FormHeaderDeckWorkspace.Name = "FormHeaderDeckWorkspace";
            this.FormHeaderDeckWorkspace.Size = new System.Drawing.Size(231, 62);
            this.FormHeaderDeckWorkspace.TabIndex = 206;
            this.FormHeaderDeckWorkspace.Text = "FormHeaderSmartPart";
            // 
            // ReceiptMonthCalender
            // 
            this.ReceiptMonthCalender.ApplyDateChange = false;
            this.ReceiptMonthCalender.FocusRemovedFrom = false;
            this.ReceiptMonthCalender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReceiptMonthCalender.Location = new System.Drawing.Point(570, 176);
            this.ReceiptMonthCalender.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            this.ReceiptMonthCalender.MaxSelectionCount = 1;
            this.ReceiptMonthCalender.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.ReceiptMonthCalender.Name = "ReceiptMonthCalender";
            this.ReceiptMonthCalender.TabIndex = 1051;
            this.ReceiptMonthCalender.Visible = false;
            this.ReceiptMonthCalender.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.ReceiptMonthCalender_DateSelected);
            this.ReceiptMonthCalender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReceiptMonthCalender_KeyDown);
            // 
            // HelplinkLabel Added for TSBG - 11024 Multiple Journal Entry - wrong form number passed to Help SP 
            // 
            this.HelplinkLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.HelplinkLabel.AutoSize = true;
            this.HelplinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelplinkLabel.Location = new System.Drawing.Point(449, 604);
            this.HelplinkLabel.Name = "HelplinkLabel";
            this.HelplinkLabel.Size = new System.Drawing.Size(32, 15);
            this.HelplinkLabel.TabIndex = 125;
            this.HelplinkLabel.TabStop = true;
            this.HelplinkLabel.Text = "Help";
            this.HelplinkLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.HelplinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelplinkLabel_LinkClicked);
            // 
            // BottomFormPanel
            // 
            this.BottomFormPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomFormPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.BottomFormPanel.Location = new System.Drawing.Point(48, 588);
            this.BottomFormPanel.Name = "BottomFormPanel";
            this.BottomFormPanel.Size = new System.Drawing.Size(800, 5);
            this.BottomFormPanel.TabIndex = 1054;
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AccessibleDescription = "0";
            this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(55, 597);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(41, 15);
            this.FormIDLabel.TabIndex = 1052;
            this.FormIDLabel.Text = "11024";
            // 
            // F11024
            // 
            this.AccessibleName = "Multiple Journal Entry";//Added for TSBG - 11024 Multiple Journal Entry - wrong form number passed to Help SP 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.BottomFormPanel);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.ReceiptMonthCalender);
            this.Controls.Add(this.FormHeaderDeckWorkspace);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.FromTemplateButton);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.TransferDatePanel);
            this.Controls.Add(this.TransferDescriptionPanel);
            this.Controls.Add(this.JournalEntryGridPanel);
            this.Controls.Add(this.MultilpejournalEntryPictureBox);
            this.Controls.Add(this.HelplinkLabel);
            this.Name = "F11024";
            this.Size = new System.Drawing.Size(966, 672);
            this.Tag = "11024";
            this.Load += new System.EventHandler(this.F11024_Load);
            this.JournalEntryGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.JournalEntryGrid)).EndInit();
            this.TransferDatePanel.ResumeLayout(false);
            this.TransferDatePanel.PerformLayout();
            this.TransferDescriptionPanel.ResumeLayout(false);
            this.TransferDescriptionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MultilpejournalEntryPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel JournalEntryGridPanel;
        private TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid JournalEntryGrid;
        private System.Windows.Forms.VScrollBar ReceiptItemGridVscrollBar;
        private TerraScan.UI.Controls.TerraScanButton HiddenButton;
        private System.Windows.Forms.Panel TransferDatePanel;
        private System.Windows.Forms.Label TransferDateLabel;
        private TerraScan.UI.Controls.TerraScanTextBox TransferDateTextBox;
        private System.Windows.Forms.Panel TransferDescriptionPanel;
        private TerraScan.UI.Controls.TerraScanTextBox TransferDescriptionTextBox;
        private System.Windows.Forms.Label TransferDescriptionLabel;
        private System.Windows.Forms.PictureBox MultilpejournalEntryPictureBox;
        private TerraScan.UI.Controls.TerraScanButton NewButton;
        private TerraScan.UI.Controls.TerraScanButton FromTemplateButton;
        private TerraScan.UI.Controls.TerraScanButton SaveButton;
        private TerraScan.UI.Controls.TerraScanButton CancelButton;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FormHeaderDeckWorkspace;
        private System.Windows.Forms.Button ReceiptDateCalenderButton;
        private TerraScan.UI.Controls.TerraScanMonthCalender ReceiptMonthCalender;
        //Added for TSBG - 11024 Multiple Journal Entry - wrong form number passed to Help SP 
        private System.Windows.Forms.LinkLabel HelplinkLabel;
        private System.Windows.Forms.Panel BottomFormPanel;
        private System.Windows.Forms.Label FormIDLabel;
    }
}