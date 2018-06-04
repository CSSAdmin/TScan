namespace D90005
{
    partial class F95005
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F95005));
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
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinScrollBar.ScrollBarLook scrollBarLook1 = new Infragistics.Win.UltraWinScrollBar.ScrollBarLook();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            this.ReferenceDataPictureBox = new System.Windows.Forms.PictureBox();
            this.ReferenceDataPanel = new System.Windows.Forms.Panel();
            this.ReferenceDataGrid = new TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid();
            this.ReferenceDataFormSliceToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ReferenceDataPictureBox)).BeginInit();
            this.ReferenceDataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReferenceDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ReferenceDataPictureBox
            // 
            this.ReferenceDataPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ReferenceDataPictureBox.Image")));
            this.ReferenceDataPictureBox.Location = new System.Drawing.Point(761, 0);
            this.ReferenceDataPictureBox.Name = "ReferenceDataPictureBox";
            this.ReferenceDataPictureBox.Size = new System.Drawing.Size(42, 153);
            this.ReferenceDataPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ReferenceDataPictureBox.TabIndex = 62;
            this.ReferenceDataPictureBox.TabStop = false;
            this.ReferenceDataPictureBox.Click += new System.EventHandler(this.ReferenceDataPictureBox_Click);
            this.ReferenceDataPictureBox.MouseHover += new System.EventHandler(this.ReferenceDataPictureBox_MouseHover);
            // 
            // ReferenceDataPanel
            // 
            this.ReferenceDataPanel.BackColor = System.Drawing.Color.White;
            this.ReferenceDataPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReferenceDataPanel.Controls.Add(this.ReferenceDataGrid);
            this.ReferenceDataPanel.Location = new System.Drawing.Point(0, 0);
            this.ReferenceDataPanel.Name = "ReferenceDataPanel";
            this.ReferenceDataPanel.Size = new System.Drawing.Size(768, 153);
            this.ReferenceDataPanel.TabIndex = 0;
            this.ReferenceDataPanel.TabStop = true;
            // 
            // ReferenceDataGrid
            // 
            this.ReferenceDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
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
            this.ReferenceDataGrid.DisplayLayout.Appearance = appearance1;
            this.ReferenceDataGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            appearance2.BackColor = System.Drawing.Color.Gray;
            appearance2.ForeColor = System.Drawing.Color.White;
            ultraGridBand1.Override.HeaderAppearance = appearance2;
            this.ReferenceDataGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance3.BackColor = System.Drawing.Color.LightGray;
            appearance3.BorderColor = System.Drawing.Color.Black;
            this.ReferenceDataGrid.DisplayLayout.EmptyRowSettings.CellAppearance = appearance3;
            appearance4.BackColor = System.Drawing.Color.LightGray;
            this.ReferenceDataGrid.DisplayLayout.EmptyRowSettings.RowAppearance = appearance4;
            this.ReferenceDataGrid.DisplayLayout.EmptyRowSettings.Style = Infragistics.Win.UltraWinGrid.EmptyRowStyle.AlignWithDataRows;
            this.ReferenceDataGrid.DisplayLayout.GroupByBox.Hidden = true;
            this.ReferenceDataGrid.DisplayLayout.InterBandSpacing = 10;
            this.ReferenceDataGrid.DisplayLayout.MaxColScrollRegions = 1;
            this.ReferenceDataGrid.DisplayLayout.MaxRowScrollRegions = 1;
            this.ReferenceDataGrid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
            this.ReferenceDataGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinBand;
            this.ReferenceDataGrid.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.None;
            this.ReferenceDataGrid.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.ReferenceDataGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.True;
            this.ReferenceDataGrid.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ReferenceDataGrid.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.ReferenceDataGrid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.ReferenceDataGrid.DisplayLayout.Override.CardAreaAppearance = appearance5;
            appearance6.BorderColor = System.Drawing.Color.Black;
            this.ReferenceDataGrid.DisplayLayout.Override.CellAppearance = appearance6;
            this.ReferenceDataGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            appearance7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ReferenceDataGrid.DisplayLayout.Override.EditCellAppearance = appearance7;
            appearance8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.ReferenceDataGrid.DisplayLayout.Override.FilterCellAppearanceActive = appearance8;
            this.ReferenceDataGrid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Cell;
            appearance9.FontData.BoldAsString = "True";
            appearance9.FontData.Name = "Arial";
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.ForeColor = System.Drawing.Color.Black;
            this.ReferenceDataGrid.DisplayLayout.Override.FilteredInCellAppearance = appearance9;
            this.ReferenceDataGrid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            this.ReferenceDataGrid.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Combo;
            this.ReferenceDataGrid.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.StartsWith;
            this.ReferenceDataGrid.DisplayLayout.Override.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)(((((((((((((((((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.NotEquals)
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
            this.ReferenceDataGrid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            appearance10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(204)))), ((int)(((byte)(167)))));
            appearance10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.ReferenceDataGrid.DisplayLayout.Override.FilterRowAppearance = appearance10;
            appearance11.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.ReferenceDataGrid.DisplayLayout.Override.FilterRowPromptAppearance = appearance11;
            this.ReferenceDataGrid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            this.ReferenceDataGrid.DisplayLayout.Override.FixedRowIndicator = Infragistics.Win.UltraWinGrid.FixedRowIndicator.None;
            this.ReferenceDataGrid.DisplayLayout.Override.FixedRowSortOrder = Infragistics.Win.UltraWinGrid.FixedRowSortOrder.FixOrder;
            this.ReferenceDataGrid.DisplayLayout.Override.FixedRowStyle = Infragistics.Win.UltraWinGrid.FixedRowStyle.Top;
            this.ReferenceDataGrid.DisplayLayout.Override.GroupBySummaryDisplayStyle = Infragistics.Win.UltraWinGrid.GroupBySummaryDisplayStyle.SummaryCellsAlwaysBelowDescription;
            appearance12.BackColor = System.Drawing.Color.Gray;
            appearance12.BackColor2 = System.Drawing.Color.Gray;
            appearance12.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            appearance12.ForeColor = System.Drawing.Color.White;
            appearance12.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            appearance12.TextHAlignAsString = "Left";
            appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisPath;
            appearance12.TextVAlignAsString = "Middle";
            appearance12.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ReferenceDataGrid.DisplayLayout.Override.HeaderAppearance = appearance12;
            this.ReferenceDataGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            appearance13.TextHAlignAsString = "Left";
            appearance13.TextVAlignAsString = "Middle";
            this.ReferenceDataGrid.DisplayLayout.Override.MergedCellAppearance = appearance13;
            this.ReferenceDataGrid.DisplayLayout.Override.MergedCellStyle = Infragistics.Win.UltraWinGrid.MergedCellStyle.Never;
            appearance14.BackColor = System.Drawing.Color.LightGray;
            this.ReferenceDataGrid.DisplayLayout.Override.RowAlternateAppearance = appearance14;
            appearance15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(195)))), ((int)(((byte)(198)))));
            this.ReferenceDataGrid.DisplayLayout.Override.RowSelectorAppearance = appearance15;
            this.ReferenceDataGrid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.ReferenceDataGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed;
            appearance16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            appearance16.ForeColor = System.Drawing.Color.White;
            this.ReferenceDataGrid.DisplayLayout.Override.SelectedCellAppearance = appearance16;
            this.ReferenceDataGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.ReferenceDataGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.ReferenceDataGrid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FixedRows;
            appearance17.BackColor = System.Drawing.Color.Black;
            this.ReferenceDataGrid.DisplayLayout.Override.SpecialRowSeparatorAppearance = appearance17;
            this.ReferenceDataGrid.DisplayLayout.Override.SpecialRowSeparatorHeight = 4;
            this.ReferenceDataGrid.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.None;
            appearance18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(156)))));
            appearance18.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.ReferenceDataGrid.DisplayLayout.Override.SummaryValueAppearance = appearance18;
            this.ReferenceDataGrid.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.ReferenceDataGrid.DisplayLayout.RowConnectorColor = System.Drawing.Color.Gray;
            this.ReferenceDataGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            appearance19.BackColor = System.Drawing.Color.DarkGray;
            scrollBarLook1.Appearance = appearance19;
            this.ReferenceDataGrid.DisplayLayout.ScrollBarLook = scrollBarLook1;
            this.ReferenceDataGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Both;
            this.ReferenceDataGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ReferenceDataGrid.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
            this.ReferenceDataGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.ReferenceDataGrid.Location = new System.Drawing.Point(-1, -1);
            this.ReferenceDataGrid.Name = "ReferenceDataGrid";
            this.ReferenceDataGrid.Size = new System.Drawing.Size(768, 154);
            this.ReferenceDataGrid.TabIndex = 1;
            this.ReferenceDataGrid.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChangeOrLostFocus;
            this.ReferenceDataGrid.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ReferenceDataGrid.BeforeEnterEditMode += new System.ComponentModel.CancelEventHandler(this.ReferenceDataGrid_BeforeEnterEditMode);
            this.ReferenceDataGrid.Error += new Infragistics.Win.UltraWinGrid.ErrorEventHandler(this.ReferenceDataGrid_Error);
            this.ReferenceDataGrid.Enter += new System.EventHandler(this.ReferenceDataGrid_Enter);
            this.ReferenceDataGrid.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.ReferenceDataGrid_BeforeRowsDeleted);
            this.ReferenceDataGrid.InitializeTemplateAddRow += new Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventHandler(this.ReferenceDataGrid_InitializeTemplateAddRow);
            this.ReferenceDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReferenceDataGrid_KeyDown);
            this.ReferenceDataGrid.BeforeRowDeactivate += new System.ComponentModel.CancelEventHandler(this.ReferenceDataGrid_BeforeRowDeactivate);
            this.ReferenceDataGrid.BeforeCellCancelUpdate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.ReferenceDataGrid_BeforeCellCancelUpdate);
            this.ReferenceDataGrid.AfterRowsDeleted += new System.EventHandler(this.ReferenceDataGrid_AfterRowsDeleted);
            this.ReferenceDataGrid.CellDataError += new Infragistics.Win.UltraWinGrid.CellDataErrorEventHandler(this.ReferenceDataGrid_CellDataError);
            this.ReferenceDataGrid.BeforeExitEditMode += new Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventHandler(this.ReferenceDataGrid_BeforeExitEditMode);
            this.ReferenceDataGrid.AfterRowInsert += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.ReferenceDataGrid_AfterRowInsert);
            this.ReferenceDataGrid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ReferenceDataGrid_InitializeLayout);
            this.ReferenceDataGrid.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ReferenceDataGrid_CellChange);
            this.ReferenceDataGrid.BeforeColPosChanged += new Infragistics.Win.UltraWinGrid.BeforeColPosChangedEventHandler(this.ReferenceDataGrid_BeforeColPosChanged);
            // 
            // F95005
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ReferenceDataPanel);
            this.Controls.Add(this.ReferenceDataPictureBox);
            this.Name = "F95005";
            this.Size = new System.Drawing.Size(804, 153);
            this.Tag = "95005";
            this.Load += new System.EventHandler(this.F95005_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReferenceDataPictureBox)).EndInit();
            this.ReferenceDataPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReferenceDataGrid)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.PictureBox ReferenceDataPictureBox;
        private System.Windows.Forms.Panel ReferenceDataPanel;
        private System.Windows.Forms.ToolTip ReferenceDataFormSliceToolTip;
        public TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid ReferenceDataGrid;
    }
}
