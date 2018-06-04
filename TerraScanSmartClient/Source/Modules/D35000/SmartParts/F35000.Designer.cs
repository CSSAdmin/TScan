namespace D35000
{
    partial class F35000
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
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F35000));
            this.AppraisalGridpanel = new System.Windows.Forms.Panel();
            this.ObjectTypeUltraFormattedTextEditor = new Infragistics.Win.FormattedLinkLabel.UltraFormattedTextEditor();
            this.SliceTypeUltraFormattedTextEditor = new Infragistics.Win.FormattedLinkLabel.UltraFormattedTextEditor();
            this.AppraisalSummaryGrid = new TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid();
            this.TotalGridpanel = new System.Windows.Forms.Panel();
            this.TotalLabelValue = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.LinePanel = new System.Windows.Forms.Panel();
            this.AppraisalSummaryPictureBox = new System.Windows.Forms.PictureBox();
            this.AdjustmentTypesImage = new System.Windows.Forms.ImageList(this.components);
            this.AppraisalSummaryToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.AppraisalGridpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AppraisalSummaryGrid)).BeginInit();
            this.TotalGridpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AppraisalSummaryPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AppraisalGridpanel
            // 
            this.AppraisalGridpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AppraisalGridpanel.Controls.Add(this.ObjectTypeUltraFormattedTextEditor);
            this.AppraisalGridpanel.Controls.Add(this.SliceTypeUltraFormattedTextEditor);
            this.AppraisalGridpanel.Controls.Add(this.AppraisalSummaryGrid);
            this.AppraisalGridpanel.Location = new System.Drawing.Point(0, 0);
            this.AppraisalGridpanel.Name = "AppraisalGridpanel";
            this.AppraisalGridpanel.Size = new System.Drawing.Size(765, 343);
            this.AppraisalGridpanel.TabIndex = 0;
            // 
            // ObjectTypeUltraFormattedTextEditor
            // 
            appearance4.ForeColor = System.Drawing.Color.White;
            this.ObjectTypeUltraFormattedTextEditor.LinkAppearance = appearance4;
            this.ObjectTypeUltraFormattedTextEditor.Location = new System.Drawing.Point(595, 315);
            this.ObjectTypeUltraFormattedTextEditor.Name = "ObjectTypeUltraFormattedTextEditor";
            this.ObjectTypeUltraFormattedTextEditor.ScrollBarDisplayStyle = Infragistics.Win.UltraWinScrollBar.ScrollBarDisplayStyle.Never;
            this.ObjectTypeUltraFormattedTextEditor.Size = new System.Drawing.Size(130, 23);
            this.ObjectTypeUltraFormattedTextEditor.TabIndex = 2;
            this.ObjectTypeUltraFormattedTextEditor.TextSectionBreakMode = Infragistics.Win.FormattedLinkLabel.TextSectionBreakMode.OnlyWhenNecessary;
            this.ObjectTypeUltraFormattedTextEditor.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ObjectTypeUltraFormattedTextEditor.Value = "ultraFormattedTextEditor1";
            this.ObjectTypeUltraFormattedTextEditor.Visible = false;
            appearance2.ForeColor = System.Drawing.Color.White;
            this.ObjectTypeUltraFormattedTextEditor.VisitedLinkAppearance = appearance2;
            this.ObjectTypeUltraFormattedTextEditor.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ObjectTypeUltraFormattedTextEditor_LinkClicked);
            // 
            // SliceTypeUltraFormattedTextEditor
            // 
            this.SliceTypeUltraFormattedTextEditor.Location = new System.Drawing.Point(459, 315);
            this.SliceTypeUltraFormattedTextEditor.Name = "SliceTypeUltraFormattedTextEditor";
            this.SliceTypeUltraFormattedTextEditor.ScrollBarDisplayStyle = Infragistics.Win.UltraWinScrollBar.ScrollBarDisplayStyle.Never;
            this.SliceTypeUltraFormattedTextEditor.Size = new System.Drawing.Size(130, 23);
            this.SliceTypeUltraFormattedTextEditor.TabIndex = 1;
            this.SliceTypeUltraFormattedTextEditor.TextSectionBreakMode = Infragistics.Win.FormattedLinkLabel.TextSectionBreakMode.OnlyWhenNecessary;
            this.SliceTypeUltraFormattedTextEditor.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.SliceTypeUltraFormattedTextEditor.Value = "ultraFormattedTextEditor1";
            this.SliceTypeUltraFormattedTextEditor.Visible = false;
            appearance3.ForeColor = System.Drawing.Color.Blue;
            this.SliceTypeUltraFormattedTextEditor.VisitedLinkAppearance = appearance3;
            this.SliceTypeUltraFormattedTextEditor.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedTextEditor1_LinkClicked);
            // 
            // AppraisalSummaryGrid
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            appearance1.BorderColor = System.Drawing.Color.Black;
            appearance1.FontData.BoldAsString = "True";
            appearance1.FontData.Name = "Arial";
            appearance1.FontData.SizeInPoints = 8F;
            this.AppraisalSummaryGrid.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.ColHeadersVisible = false;
            ultraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            ultraGridBand1.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.AppraisalSummaryGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.AppraisalSummaryGrid.DisplayLayout.InterBandSpacing = 0;
            this.AppraisalSummaryGrid.DisplayLayout.MaxColScrollRegions = 1;
            this.AppraisalSummaryGrid.DisplayLayout.MaxRowScrollRegions = 1;
            this.AppraisalSummaryGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            this.AppraisalSummaryGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.AppraisalSummaryGrid.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.AppraisalSummaryGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.AppraisalSummaryGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.None;
            this.AppraisalSummaryGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.AppraisalSummaryGrid.Location = new System.Drawing.Point(-1, -1);
            this.AppraisalSummaryGrid.Name = "AppraisalSummaryGrid";
            this.AppraisalSummaryGrid.Size = new System.Drawing.Size(765, 383);
            this.AppraisalSummaryGrid.TabIndex = 0;
            this.AppraisalSummaryGrid.Tag = "35000";
            this.AppraisalSummaryGrid.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.AppraisalSummaryGrid.BeforeEnterEditMode += new System.ComponentModel.CancelEventHandler(this.AppraisalSummaryGrid_BeforeEnterEditMode);
            this.AppraisalSummaryGrid.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.AppraisalSummaryGrid_AfterCellUpdate);
            this.AppraisalSummaryGrid.Enter += new System.EventHandler(this.AppraisalSummaryGrid_Enter);
            this.AppraisalSummaryGrid.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.AppraisalSummaryGrid_ClickCell);
            this.AppraisalSummaryGrid.InitializeTemplateAddRow += new Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventHandler(this.AppraisalSummaryGrid_InitializeTemplateAddRow);
            this.AppraisalSummaryGrid.AfterCellActivate += new System.EventHandler(this.AppraisalSummaryGrid_AfterCellActivate);
            this.AppraisalSummaryGrid.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.AppraisalSummaryGrid_InitializeRow);
            this.AppraisalSummaryGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AppraisalSummaryGrid_MouseClick);
            this.AppraisalSummaryGrid.BeforeRowUpdate += new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.AppraisalSummaryGrid_BeforeRowUpdate);
            this.AppraisalSummaryGrid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.AppraisalSummaryGrid_InitializeLayout);
            this.AppraisalSummaryGrid.MouseLeaveElement += new Infragistics.Win.UIElementEventHandler(this.AppraisalSummaryGrid_MouseLeaveElement);
            this.AppraisalSummaryGrid.MouseEnterElement += new Infragistics.Win.UIElementEventHandler(this.AppraisalSummaryGrid_MouseEnterElement);
            this.AppraisalSummaryGrid.BeforePerformAction += new Infragistics.Win.UltraWinGrid.BeforeUltraGridPerformActionEventHandler(this.AppraisalSummaryGrid_BeforePerformAction);
            // 
            // TotalGridpanel
            // 
            this.TotalGridpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TotalGridpanel.Controls.Add(this.TotalLabelValue);
            this.TotalGridpanel.Controls.Add(this.TotalLabel);
            this.TotalGridpanel.Controls.Add(this.LinePanel);
            this.TotalGridpanel.Location = new System.Drawing.Point(0, 341);
            this.TotalGridpanel.Name = "TotalGridpanel";
            this.TotalGridpanel.Size = new System.Drawing.Size(764, 25);
            this.TotalGridpanel.TabIndex = 10;
            // 
            // TotalLabelValue
            // 
            this.TotalLabelValue.AllowClick = true;
            this.TotalLabelValue.AllowNegativeSign = true;
            this.TotalLabelValue.ApplyCFGFormat = false;
            this.TotalLabelValue.ApplyCurrencyFormat = false;
            this.TotalLabelValue.ApplyFocusColor = false;
            this.TotalLabelValue.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.TotalLabelValue.ApplyNegativeStandard = true;
            this.TotalLabelValue.ApplyParentFocusColor = true;
            this.TotalLabelValue.ApplyTimeFormat = false;
            this.TotalLabelValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TotalLabelValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TotalLabelValue.CFromatWihoutSymbol = false;
            this.TotalLabelValue.CheckForEmpty = false;
            this.TotalLabelValue.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TotalLabelValue.Digits = -1;
            this.TotalLabelValue.EmptyDecimalValue = false;
            this.TotalLabelValue.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.TotalLabelValue.ForeColor = System.Drawing.Color.White;
            this.TotalLabelValue.IsEditable = false;
            this.TotalLabelValue.IsQueryableFileld = true;
            this.TotalLabelValue.Location = new System.Drawing.Point(659, 2);
            this.TotalLabelValue.LockKeyPress = true;
            this.TotalLabelValue.MaxLength = 20;
            this.TotalLabelValue.Name = "TotalLabelValue";
            this.TotalLabelValue.PersistDefaultColor = false;
            this.TotalLabelValue.Precision = 2;
            this.TotalLabelValue.QueryingFileldName = "";
            this.TotalLabelValue.ReadOnly = true;
            this.TotalLabelValue.SetColorFlag = false;
            this.TotalLabelValue.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TotalLabelValue.Size = new System.Drawing.Size(102, 16);
            this.TotalLabelValue.SpecialCharacter = "%";
            this.TotalLabelValue.TabIndex = 3;
            this.TotalLabelValue.TabStop = false;
            this.TotalLabelValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TotalLabelValue.TextCustomFormat = "##,##0";
            this.TotalLabelValue.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Money;
            this.TotalLabelValue.WholeInteger = false;
            // 
            // TotalLabel
            // 
            this.TotalLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TotalLabel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalLabel.ForeColor = System.Drawing.Color.White;
            this.TotalLabel.Location = new System.Drawing.Point(559, 2);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(95, 16);
            this.TotalLabel.TabIndex = 3;
            this.TotalLabel.Text = "Object Total:";
            this.TotalLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LinePanel
            // 
            this.LinePanel.BackColor = System.Drawing.Color.Black;
            this.LinePanel.Location = new System.Drawing.Point(655, 0);
            this.LinePanel.Name = "LinePanel";
            this.LinePanel.Size = new System.Drawing.Size(1, 25);
            this.LinePanel.TabIndex = 10;
            // 
            // AppraisalSummaryPictureBox
            // 
            this.AppraisalSummaryPictureBox.Location = new System.Drawing.Point(757, 0);
            this.AppraisalSummaryPictureBox.Name = "AppraisalSummaryPictureBox";
            this.AppraisalSummaryPictureBox.Size = new System.Drawing.Size(42, 383);
            this.AppraisalSummaryPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AppraisalSummaryPictureBox.TabIndex = 119;
            this.AppraisalSummaryPictureBox.TabStop = false;
            this.AppraisalSummaryPictureBox.Click += new System.EventHandler(this.AppraisalSummaryPictureBox_Click);
            this.AppraisalSummaryPictureBox.MouseEnter += new System.EventHandler(this.AppraisalSummaryPictureBox_MouseEnter);
            // 
            // AdjustmentTypesImage
            // 
            this.AdjustmentTypesImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("AdjustmentTypesImage.ImageStream")));
            this.AdjustmentTypesImage.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AdjustmentTypesImage.Images.SetKeyName(0, "Plus.gif");
            this.AdjustmentTypesImage.Images.SetKeyName(1, "Minus.gif");
            this.AdjustmentTypesImage.Images.SetKeyName(2, "Dot.gif");
            this.AdjustmentTypesImage.Images.SetKeyName(3, "Down.gif");
            this.AdjustmentTypesImage.Images.SetKeyName(4, "Up.gif");
            this.AdjustmentTypesImage.Images.SetKeyName(5, "mulplicative.gif");
            // 
            // F35000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.AppraisalGridpanel);
            this.Controls.Add(this.TotalGridpanel);
            this.Controls.Add(this.AppraisalSummaryPictureBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "F35000";
            this.Size = new System.Drawing.Size(804, 383);
            this.Tag = "35000";
            this.Load += new System.EventHandler(this.F35000_Load);
            this.Resize += new System.EventHandler(this.F35000_Resize);
            this.AppraisalGridpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AppraisalSummaryGrid)).EndInit();
            this.TotalGridpanel.ResumeLayout(false);
            this.TotalGridpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AppraisalSummaryPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel AppraisalGridpanel;
        private System.Windows.Forms.Panel TotalGridpanel;
        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.Panel LinePanel;
        private System.Windows.Forms.PictureBox AppraisalSummaryPictureBox;
        private TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid AppraisalSummaryGrid;
        private System.Windows.Forms.ImageList AdjustmentTypesImage;
        private System.Windows.Forms.ToolTip AppraisalSummaryToolTip;
        private Infragistics.Win.FormattedLinkLabel.UltraFormattedTextEditor SliceTypeUltraFormattedTextEditor;
        private Infragistics.Win.FormattedLinkLabel.UltraFormattedTextEditor ObjectTypeUltraFormattedTextEditor;
        private TerraScan.UI.Controls.TerraScanTextBox TotalLabelValue;
        
    
        
    }
}
