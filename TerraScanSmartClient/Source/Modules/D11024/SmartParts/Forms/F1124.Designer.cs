namespace D11024
{
    partial class F1124
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
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateID", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateName", 1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rollyear", 2);
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinScrollBar.ScrollBarLook scrollBarLook1 = new Infragistics.Win.UltraWinScrollBar.ScrollBarLook();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1124));
            this.MiscTemplatesGridView = new TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid();
            this.AcceptMiscTemplateButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CancelMiscTemplateButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.MiscTemplateLinePanel = new System.Windows.Forms.Panel();
            this.GridPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.MiscTemplatesGridView)).BeginInit();
            this.GridPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MiscTemplatesGridView
            // 
            this.MiscTemplatesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance17.BackColor = System.Drawing.Color.White;
            appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance17.BorderColor = System.Drawing.Color.Black;
            appearance17.FontData.BoldAsString = "True";
            appearance17.FontData.Name = "Arial";
            appearance17.FontData.SizeInPoints = 8F;
            appearance17.TextHAlignAsString = "Left";
            appearance17.TextVAlignAsString = "Middle";
            this.MiscTemplatesGridView.DisplayLayout.Appearance = appearance17;
            this.MiscTemplatesGridView.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            appearance4.TextHAlignAsString = "Left";
            ultraGridColumn2.CellAppearance = appearance4;
            appearance1.TextHAlignAsString = "Center";
            ultraGridColumn2.Header.Appearance = appearance1;
            ultraGridColumn2.Header.Caption = "Template Name";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 325;
            ultraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            appearance5.TextHAlignAsString = "Right";
            ultraGridColumn3.CellAppearance = appearance5;
            appearance3.TextHAlignAsString = "Center";
            ultraGridColumn3.Header.Appearance = appearance3;
            ultraGridColumn3.Header.Caption = "Roll Year";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 55;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3});
            ultraGridBand1.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Edit;
            ultraGridBand1.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.StartsWith;
            ultraGridBand1.Override.FilterOperatorDropDownItems = Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.None;
            ultraGridBand1.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.Hidden;
            appearance18.BackColor = System.Drawing.Color.DimGray;
            ultraGridBand1.Override.HeaderAppearance = appearance18;
            appearance2.BackColor = System.Drawing.Color.LightGray;
            ultraGridBand1.Override.RowAlternateAppearance = appearance2;
            this.MiscTemplatesGridView.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.MiscTemplatesGridView.DisplayLayout.GroupByBox.Hidden = true;
            this.MiscTemplatesGridView.DisplayLayout.InterBandSpacing = 10;
            this.MiscTemplatesGridView.DisplayLayout.MaxColScrollRegions = 1;
            this.MiscTemplatesGridView.DisplayLayout.MaxRowScrollRegions = 1;
            this.MiscTemplatesGridView.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.MiscTemplatesGridView.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.MiscTemplatesGridView.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.None;
            this.MiscTemplatesGridView.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.MiscTemplatesGridView.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.MiscTemplatesGridView.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.MiscTemplatesGridView.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.MiscTemplatesGridView.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance19.BackColor = System.Drawing.Color.Transparent;
            this.MiscTemplatesGridView.DisplayLayout.Override.CardAreaAppearance = appearance19;
            appearance20.BorderColor = System.Drawing.Color.Black;
            this.MiscTemplatesGridView.DisplayLayout.Override.CellAppearance = appearance20;
            this.MiscTemplatesGridView.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect;
            appearance21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.MiscTemplatesGridView.DisplayLayout.Override.FilterCellAppearanceActive = appearance21;
            this.MiscTemplatesGridView.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Cell;
            appearance22.FontData.BoldAsString = "True";
            appearance22.FontData.Name = "Arial";
            appearance22.FontData.SizeInPoints = 8F;
            appearance22.ForeColor = System.Drawing.Color.Black;
            this.MiscTemplatesGridView.DisplayLayout.Override.FilteredInCellAppearance = appearance22;
            this.MiscTemplatesGridView.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            this.MiscTemplatesGridView.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Combo;
            this.MiscTemplatesGridView.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.StartsWith;
            this.MiscTemplatesGridView.DisplayLayout.Override.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)(((((((((((((((((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.NotEquals)
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
            this.MiscTemplatesGridView.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            appearance23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(204)))), ((int)(((byte)(167)))));
            appearance23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.MiscTemplatesGridView.DisplayLayout.Override.FilterRowAppearance = appearance23;
            appearance24.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.MiscTemplatesGridView.DisplayLayout.Override.FilterRowPromptAppearance = appearance24;
            this.MiscTemplatesGridView.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            this.MiscTemplatesGridView.DisplayLayout.Override.FixedRowIndicator = Infragistics.Win.UltraWinGrid.FixedRowIndicator.None;
            this.MiscTemplatesGridView.DisplayLayout.Override.FixedRowSortOrder = Infragistics.Win.UltraWinGrid.FixedRowSortOrder.FixOrder;
            this.MiscTemplatesGridView.DisplayLayout.Override.FixedRowStyle = Infragistics.Win.UltraWinGrid.FixedRowStyle.Top;
            this.MiscTemplatesGridView.DisplayLayout.Override.GroupBySummaryDisplayStyle = Infragistics.Win.UltraWinGrid.GroupBySummaryDisplayStyle.SummaryCellsAlwaysBelowDescription;
            appearance25.BackColor = System.Drawing.Color.Gray;
            appearance25.BackColor2 = System.Drawing.Color.Gray;
            appearance25.ForeColor = System.Drawing.Color.White;
            appearance25.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            appearance25.TextHAlignAsString = "Left";
            appearance25.TextTrimming = Infragistics.Win.TextTrimming.EllipsisPath;
            appearance25.TextVAlignAsString = "Middle";
            appearance25.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.MiscTemplatesGridView.DisplayLayout.Override.HeaderAppearance = appearance25;
            this.MiscTemplatesGridView.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            appearance26.TextHAlignAsString = "Left";
            appearance26.TextVAlignAsString = "Middle";
            this.MiscTemplatesGridView.DisplayLayout.Override.MergedCellAppearance = appearance26;
            this.MiscTemplatesGridView.DisplayLayout.Override.MergedCellStyle = Infragistics.Win.UltraWinGrid.MergedCellStyle.Never;
            appearance27.BackColor = System.Drawing.Color.LightGray;
            this.MiscTemplatesGridView.DisplayLayout.Override.RowAlternateAppearance = appearance27;
            appearance6.BackColor = System.Drawing.Color.LightGray;
            this.MiscTemplatesGridView.DisplayLayout.Override.RowAppearance = appearance6;
            appearance28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(195)))), ((int)(((byte)(198)))));
            this.MiscTemplatesGridView.DisplayLayout.Override.RowSelectorAppearance = appearance28;
            this.MiscTemplatesGridView.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
            this.MiscTemplatesGridView.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed;
            appearance29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            appearance29.ForeColor = System.Drawing.Color.White;
            this.MiscTemplatesGridView.DisplayLayout.Override.SelectedCellAppearance = appearance29;
            this.MiscTemplatesGridView.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.MiscTemplatesGridView.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.MiscTemplatesGridView.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FixedRows;
            appearance30.BackColor = System.Drawing.Color.Black;
            this.MiscTemplatesGridView.DisplayLayout.Override.SpecialRowSeparatorAppearance = appearance30;
            this.MiscTemplatesGridView.DisplayLayout.Override.SpecialRowSeparatorHeight = 4;
            this.MiscTemplatesGridView.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed;
            appearance31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(194)))), ((int)(((byte)(218)))));
            appearance31.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.MiscTemplatesGridView.DisplayLayout.Override.SummaryValueAppearance = appearance31;
            this.MiscTemplatesGridView.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Hide;
            this.MiscTemplatesGridView.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.MiscTemplatesGridView.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            appearance32.BackColor = System.Drawing.Color.DarkGray;
            scrollBarLook1.Appearance = appearance32;
            this.MiscTemplatesGridView.DisplayLayout.ScrollBarLook = scrollBarLook1;
            this.MiscTemplatesGridView.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;
            this.MiscTemplatesGridView.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.MiscTemplatesGridView.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.MiscTemplatesGridView.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.MiscTemplatesGridView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiscTemplatesGridView.Location = new System.Drawing.Point(-1, -1);
            this.MiscTemplatesGridView.Name = "MiscTemplatesGridView";
            this.MiscTemplatesGridView.Size = new System.Drawing.Size(500, 134);
            this.MiscTemplatesGridView.StyleSetName = "135";
            this.MiscTemplatesGridView.TabIndex = 199;
            this.MiscTemplatesGridView.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.MiscTemplatesGridView.DoubleClickCell += new Infragistics.Win.UltraWinGrid.DoubleClickCellEventHandler(this.MiscTemplatesGridView_DoubleClickCell);
            // 
            // AcceptMiscTemplateButton
            // 
            this.AcceptMiscTemplateButton.ActualPermission = false;
            this.AcceptMiscTemplateButton.ApplyDisableBehaviour = false;
            this.AcceptMiscTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AcceptMiscTemplateButton.BorderColor = System.Drawing.Color.Wheat;
            this.AcceptMiscTemplateButton.CommentPriority = false;
            this.AcceptMiscTemplateButton.EnableAutoPrint = false;
            this.AcceptMiscTemplateButton.Enabled = false;
            this.AcceptMiscTemplateButton.FilterStatus = false;
            this.AcceptMiscTemplateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AcceptMiscTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AcceptMiscTemplateButton.FocusRectangleEnabled = true;
            this.AcceptMiscTemplateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptMiscTemplateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AcceptMiscTemplateButton.ImageSelected = false;
            this.AcceptMiscTemplateButton.Location = new System.Drawing.Point(102, 169);
            this.AcceptMiscTemplateButton.Name = "AcceptMiscTemplateButton";
            this.AcceptMiscTemplateButton.NewPadding = 5;
            this.AcceptMiscTemplateButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.AcceptMiscTemplateButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AcceptMiscTemplateButton.Size = new System.Drawing.Size(98, 28);
            this.AcceptMiscTemplateButton.StatusIndicator = false;
            this.AcceptMiscTemplateButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AcceptMiscTemplateButton.StatusOffText = null;
            this.AcceptMiscTemplateButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AcceptMiscTemplateButton.StatusOnText = null;
            this.AcceptMiscTemplateButton.TabIndex = 200;
            this.AcceptMiscTemplateButton.TabStop = false;
            this.AcceptMiscTemplateButton.Text = "Accept";
            this.AcceptMiscTemplateButton.UseVisualStyleBackColor = false;
            this.AcceptMiscTemplateButton.Click += new System.EventHandler(this.AcceptTemplateButton_Click);
            // 
            // CancelMiscTemplateButton
            // 
            this.CancelMiscTemplateButton.ActualPermission = false;
            this.CancelMiscTemplateButton.ApplyDisableBehaviour = false;
            this.CancelMiscTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelMiscTemplateButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelMiscTemplateButton.CommentPriority = false;
            this.CancelMiscTemplateButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelMiscTemplateButton.EnableAutoPrint = false;
            this.CancelMiscTemplateButton.FilterStatus = false;
            this.CancelMiscTemplateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelMiscTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelMiscTemplateButton.FocusRectangleEnabled = true;
            this.CancelMiscTemplateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelMiscTemplateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelMiscTemplateButton.ImageSelected = false;
            this.CancelMiscTemplateButton.Location = new System.Drawing.Point(380, 170);
            this.CancelMiscTemplateButton.Name = "CancelMiscTemplateButton";
            this.CancelMiscTemplateButton.NewPadding = 5;
            this.CancelMiscTemplateButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CancelMiscTemplateButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelMiscTemplateButton.Size = new System.Drawing.Size(98, 28);
            this.CancelMiscTemplateButton.StatusIndicator = false;
            this.CancelMiscTemplateButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelMiscTemplateButton.StatusOffText = null;
            this.CancelMiscTemplateButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelMiscTemplateButton.StatusOnText = null;
            this.CancelMiscTemplateButton.TabIndex = 201;
            this.CancelMiscTemplateButton.TabStop = false;
            this.CancelMiscTemplateButton.Text = "Cancel";
            this.CancelMiscTemplateButton.UseVisualStyleBackColor = false;
            this.CancelMiscTemplateButton.Click += new System.EventHandler(this.CancelMiscTemplateButton_Click);
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(25, 212);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(34, 15);
            this.FormIDLabel.TabIndex = 203;
            this.FormIDLabel.Text = "1124";
            // 
            // MiscTemplateLinePanel
            // 
            this.MiscTemplateLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.MiscTemplateLinePanel.Location = new System.Drawing.Point(23, 207);
            this.MiscTemplateLinePanel.Name = "MiscTemplateLinePanel";
            this.MiscTemplateLinePanel.Size = new System.Drawing.Size(500, 3);
            this.MiscTemplateLinePanel.TabIndex = 202;
            // 
            // GridPanel
            // 
            this.GridPanel.BackColor = System.Drawing.Color.White;
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.MiscTemplatesGridView);
            this.GridPanel.Location = new System.Drawing.Point(23, 23);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(500, 134);
            this.GridPanel.TabIndex = 204;
            this.GridPanel.TabStop = true;
            // 
            // F1124
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(544, 231);
            this.Controls.Add(this.AcceptMiscTemplateButton);
            this.Controls.Add(this.CancelMiscTemplateButton);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.MiscTemplateLinePanel);
            this.Controls.Add(this.GridPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F1124";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "1124";
            this.Text = "TerraScan T2 - Select Journal Entry Template";
            this.Load += new System.EventHandler(this.F1124_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MiscTemplatesGridView)).EndInit();
            this.GridPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid MiscTemplatesGridView;
        private TerraScan.UI.Controls.TerraScanButton AcceptMiscTemplateButton;
        private TerraScan.UI.Controls.TerraScanButton CancelMiscTemplateButton;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.Panel MiscTemplateLinePanel;
        private System.Windows.Forms.Panel GridPanel;
    }
}