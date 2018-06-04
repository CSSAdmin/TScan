namespace D90101
{
    partial class F95101
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("KeyID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ItemName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TableName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FieldName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Old");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("New");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UpdateTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AuditType");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F95101));
            this.AuditTrailDataPanel = new System.Windows.Forms.Panel();
            this.AuditTrailDataGrid = new TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid();
            this.AuditTrailPictureBox = new System.Windows.Forms.PictureBox();
            this.AuditTrailFormSliceToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.AuditTrailDataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AuditTrailDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AuditTrailPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AuditTrailDataPanel
            // 
            this.AuditTrailDataPanel.BackColor = System.Drawing.Color.White;
            this.AuditTrailDataPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AuditTrailDataPanel.Controls.Add(this.AuditTrailDataGrid);
            this.AuditTrailDataPanel.Location = new System.Drawing.Point(0, 0);
            this.AuditTrailDataPanel.Name = "AuditTrailDataPanel";
            this.AuditTrailDataPanel.Size = new System.Drawing.Size(768, 164);
            this.AuditTrailDataPanel.TabIndex = 1;
            this.AuditTrailDataPanel.TabStop = true;
            // 
            // AuditTrailDataGrid
            // 
            this.AuditTrailDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
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
            this.AuditTrailDataGrid.DisplayLayout.Appearance = appearance1;
            this.AuditTrailDataGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridColumn1.Header.Fixed = true;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn2.Header.Fixed = true;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.Header.Fixed = true;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn4.Header.Fixed = true;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.Header.Fixed = true;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn6.Header.Fixed = true;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.MaskInput = "{LOC}mm/dd/yyyy hh:mm:ss tt";
            ultraGridColumn7.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTimeWithoutDropDown;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10});
            appearance2.BackColor = System.Drawing.Color.DimGray;
            ultraGridBand1.Override.HeaderAppearance = appearance2;
            this.AuditTrailDataGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance3.BackColor = System.Drawing.Color.LightGray;
            appearance3.BorderColor = System.Drawing.Color.Black;
            this.AuditTrailDataGrid.DisplayLayout.EmptyRowSettings.CellAppearance = appearance3;
            appearance4.BackColor = System.Drawing.Color.LightGray;
            this.AuditTrailDataGrid.DisplayLayout.EmptyRowSettings.RowAppearance = appearance4;
            this.AuditTrailDataGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
            this.AuditTrailDataGrid.DisplayLayout.EmptyRowSettings.Style = Infragistics.Win.UltraWinGrid.EmptyRowStyle.AlignWithDataRows;
            this.AuditTrailDataGrid.DisplayLayout.GroupByBox.Hidden = true;
            this.AuditTrailDataGrid.DisplayLayout.InterBandSpacing = 10;
            this.AuditTrailDataGrid.DisplayLayout.MaxColScrollRegions = 1;
            this.AuditTrailDataGrid.DisplayLayout.MaxRowScrollRegions = 1;
            this.AuditTrailDataGrid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.AuditTrailDataGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinBand;
            this.AuditTrailDataGrid.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.None;
            this.AuditTrailDataGrid.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.AuditTrailDataGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.AuditTrailDataGrid.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.AuditTrailDataGrid.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.AuditTrailDataGrid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.AuditTrailDataGrid.DisplayLayout.Override.CardAreaAppearance = appearance5;
            appearance6.BorderColor = System.Drawing.Color.Black;
            this.AuditTrailDataGrid.DisplayLayout.Override.CellAppearance = appearance6;
            this.AuditTrailDataGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            appearance7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.AuditTrailDataGrid.DisplayLayout.Override.EditCellAppearance = appearance7;
            appearance8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.AuditTrailDataGrid.DisplayLayout.Override.FilterCellAppearanceActive = appearance8;
            this.AuditTrailDataGrid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Cell;
            appearance9.FontData.BoldAsString = "True";
            appearance9.FontData.Name = "Arial";
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.ForeColor = System.Drawing.Color.Black;
            this.AuditTrailDataGrid.DisplayLayout.Override.FilteredInCellAppearance = appearance9;
            this.AuditTrailDataGrid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            this.AuditTrailDataGrid.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Combo;
            this.AuditTrailDataGrid.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.StartsWith;
            this.AuditTrailDataGrid.DisplayLayout.Override.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)(((((((((((((((((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.NotEquals)
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
            this.AuditTrailDataGrid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            appearance10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(204)))), ((int)(((byte)(167)))));
            appearance10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.AuditTrailDataGrid.DisplayLayout.Override.FilterRowAppearance = appearance10;
            appearance11.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.AuditTrailDataGrid.DisplayLayout.Override.FilterRowPromptAppearance = appearance11;
            this.AuditTrailDataGrid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            this.AuditTrailDataGrid.DisplayLayout.Override.FixedRowIndicator = Infragistics.Win.UltraWinGrid.FixedRowIndicator.None;
            this.AuditTrailDataGrid.DisplayLayout.Override.FixedRowSortOrder = Infragistics.Win.UltraWinGrid.FixedRowSortOrder.FixOrder;
            this.AuditTrailDataGrid.DisplayLayout.Override.FixedRowStyle = Infragistics.Win.UltraWinGrid.FixedRowStyle.Top;
            this.AuditTrailDataGrid.DisplayLayout.Override.GroupBySummaryDisplayStyle = Infragistics.Win.UltraWinGrid.GroupBySummaryDisplayStyle.SummaryCellsAlwaysBelowDescription;
            appearance12.BackColor = System.Drawing.Color.Gray;
            appearance12.BackColor2 = System.Drawing.Color.Gray;
            appearance12.ForeColor = System.Drawing.Color.White;
            appearance12.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            appearance12.TextHAlignAsString = "Left";
            appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisPath;
            appearance12.TextVAlignAsString = "Middle";
            appearance12.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.AuditTrailDataGrid.DisplayLayout.Override.HeaderAppearance = appearance12;
            appearance13.TextHAlignAsString = "Left";
            appearance13.TextVAlignAsString = "Middle";
            this.AuditTrailDataGrid.DisplayLayout.Override.MergedCellAppearance = appearance13;
            this.AuditTrailDataGrid.DisplayLayout.Override.MergedCellStyle = Infragistics.Win.UltraWinGrid.MergedCellStyle.Never;
            appearance14.BackColor = System.Drawing.Color.LightGray;
            this.AuditTrailDataGrid.DisplayLayout.Override.RowAlternateAppearance = appearance14;
            appearance15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(195)))), ((int)(((byte)(198)))));
            this.AuditTrailDataGrid.DisplayLayout.Override.RowSelectorAppearance = appearance15;
            this.AuditTrailDataGrid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
            this.AuditTrailDataGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed;
            appearance16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            appearance16.ForeColor = System.Drawing.Color.White;
            this.AuditTrailDataGrid.DisplayLayout.Override.SelectedCellAppearance = appearance16;
            this.AuditTrailDataGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.AuditTrailDataGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.AuditTrailDataGrid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FixedRows;
            appearance17.BackColor = System.Drawing.Color.Black;
            this.AuditTrailDataGrid.DisplayLayout.Override.SpecialRowSeparatorAppearance = appearance17;
            this.AuditTrailDataGrid.DisplayLayout.Override.SpecialRowSeparatorHeight = 4;
            this.AuditTrailDataGrid.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.None;
            appearance18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(156)))));
            appearance18.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.AuditTrailDataGrid.DisplayLayout.Override.SummaryValueAppearance = appearance18;
            this.AuditTrailDataGrid.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Hide;
            this.AuditTrailDataGrid.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.AuditTrailDataGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            appearance19.BackColor = System.Drawing.Color.DarkGray;
            scrollBarLook1.Appearance = appearance19;
            this.AuditTrailDataGrid.DisplayLayout.ScrollBarLook = scrollBarLook1;
            this.AuditTrailDataGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;
            this.AuditTrailDataGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.AuditTrailDataGrid.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.AuditTrailDataGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.AuditTrailDataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuditTrailDataGrid.Location = new System.Drawing.Point(-1, -2);
            this.AuditTrailDataGrid.Name = "AuditTrailDataGrid";
            this.AuditTrailDataGrid.Size = new System.Drawing.Size(769, 164);
            this.AuditTrailDataGrid.TabIndex = 1;
            this.AuditTrailDataGrid.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChangeOrLostFocus;
            this.AuditTrailDataGrid.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // AuditTrailPictureBox
            // 
            this.AuditTrailPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("AuditTrailPictureBox.Image")));
            this.AuditTrailPictureBox.Location = new System.Drawing.Point(761, 0);
            this.AuditTrailPictureBox.Name = "AuditTrailPictureBox";
            this.AuditTrailPictureBox.Size = new System.Drawing.Size(42, 164);
            this.AuditTrailPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AuditTrailPictureBox.TabIndex = 63;
            this.AuditTrailPictureBox.TabStop = false;
            this.AuditTrailPictureBox.Click += new System.EventHandler(this.AuditTrailPictureBox_Click);
            this.AuditTrailPictureBox.MouseHover += new System.EventHandler(this.AuditTrailPictureBox_MouseHover);
            // 
            // F95101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.AuditTrailDataPanel);
            this.Controls.Add(this.AuditTrailPictureBox);
            this.Name = "F95101";
            this.Size = new System.Drawing.Size(804, 164);
            this.Tag = "95101";
            this.Load += new System.EventHandler(this.F95101_Load);
            this.AuditTrailDataPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AuditTrailDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AuditTrailPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel AuditTrailDataPanel;
        private TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid AuditTrailDataGrid;
        private System.Windows.Forms.PictureBox AuditTrailPictureBox;
        private System.Windows.Forms.ToolTip AuditTrailFormSliceToolTip;


    }
}
