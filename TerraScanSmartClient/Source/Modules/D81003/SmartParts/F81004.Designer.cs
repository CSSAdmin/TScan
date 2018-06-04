namespace D81003
{
    partial class F81004
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
            this.SelectionGridPanel = new System.Windows.Forms.Panel();
            this.SummaryPanel = new System.Windows.Forms.Panel();
            this.TotalFeePanel = new System.Windows.Forms.Panel();
            this.TotalFeeLabel = new System.Windows.Forms.Label();
            this.UnitFeePanel = new System.Windows.Forms.Panel();
            this.TotalFeeTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.SelectionGrid = new TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid();
            this.UltraFeeCalcManager = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(this.components);
            this.SelectionPictureBox = new System.Windows.Forms.PictureBox();
            this.SelectionFormSliceToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SelectionGridPanel.SuspendLayout();
            this.SummaryPanel.SuspendLayout();
            this.TotalFeePanel.SuspendLayout();
            this.UnitFeePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectionGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraFeeCalcManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectionPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectionGridPanel
            // 
            this.SelectionGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectionGridPanel.Controls.Add(this.SummaryPanel);
            this.SelectionGridPanel.Controls.Add(this.SelectionGrid);
            this.SelectionGridPanel.Location = new System.Drawing.Point(0, 0);
            this.SelectionGridPanel.Name = "SelectionGridPanel";
            this.SelectionGridPanel.Size = new System.Drawing.Size(759, 339);
            this.SelectionGridPanel.TabIndex = 0;
            this.SelectionGridPanel.Tag = "81004";
            // 
            // SummaryPanel
            // 
            this.SummaryPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.SummaryPanel.Controls.Add(this.TotalFeePanel);
            this.SummaryPanel.Controls.Add(this.UnitFeePanel);
            this.SummaryPanel.Location = new System.Drawing.Point(0, 309);
            this.SummaryPanel.Name = "SummaryPanel";
            this.SummaryPanel.Size = new System.Drawing.Size(767, 28);
            this.SummaryPanel.TabIndex = 4;
            // 
            // TotalFeePanel
            // 
            this.TotalFeePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalFeePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalFeePanel.Controls.Add(this.TotalFeeLabel);
            this.TotalFeePanel.Location = new System.Drawing.Point(566, -1);
            this.TotalFeePanel.Name = "TotalFeePanel";
            this.TotalFeePanel.Size = new System.Drawing.Size(99, 33);
            this.TotalFeePanel.TabIndex = 13;
            // 
            // TotalFeeLabel
            // 
            this.TotalFeeLabel.AutoSize = true;
            this.TotalFeeLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalFeeLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalFeeLabel.ForeColor = System.Drawing.Color.White;
            this.TotalFeeLabel.Location = new System.Drawing.Point(18, 5);
            this.TotalFeeLabel.Name = "TotalFeeLabel";
            this.TotalFeeLabel.Size = new System.Drawing.Size(68, 16);
            this.TotalFeeLabel.TabIndex = 0;
            this.TotalFeeLabel.Text = "Total Fee";
            // 
            // UnitFeePanel
            // 
            this.UnitFeePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.UnitFeePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UnitFeePanel.Controls.Add(this.TotalFeeTextBox);
            this.UnitFeePanel.Location = new System.Drawing.Point(664, -1);
            this.UnitFeePanel.Name = "UnitFeePanel";
            this.UnitFeePanel.Size = new System.Drawing.Size(100, 33);
            this.UnitFeePanel.TabIndex = 12;
            // 
            // TotalFeeTextBox
            // 
            this.TotalFeeTextBox.AllowClick = true;
            this.TotalFeeTextBox.AllowNegativeSign = false;
            this.TotalFeeTextBox.ApplyCFGFormat = false;
            this.TotalFeeTextBox.ApplyCurrencyFormat = true;
            this.TotalFeeTextBox.ApplyFocusColor = true;
            this.TotalFeeTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.TotalFeeTextBox.ApplyNegativeStandard = true;
            this.TotalFeeTextBox.ApplyParentFocusColor = true;
            this.TotalFeeTextBox.ApplyTimeFormat = false;
            this.TotalFeeTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalFeeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TotalFeeTextBox.CFromatWihoutSymbol = false;
            this.TotalFeeTextBox.CheckForEmpty = false;
            this.TotalFeeTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TotalFeeTextBox.Digits = 16;
            this.TotalFeeTextBox.EmptyDecimalValue = false;
            this.TotalFeeTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.TotalFeeTextBox.ForeColor = System.Drawing.Color.White;
            this.TotalFeeTextBox.IsEditable = false;
            this.TotalFeeTextBox.IsQueryableFileld = true;
            this.TotalFeeTextBox.Location = new System.Drawing.Point(4, 5);
            this.TotalFeeTextBox.LockKeyPress = true;
            this.TotalFeeTextBox.MaxLength = 50;
            this.TotalFeeTextBox.Name = "TotalFeeTextBox";
            this.TotalFeeTextBox.PersistDefaultColor = false;
            this.TotalFeeTextBox.Precision = 2;
            this.TotalFeeTextBox.QueryingFileldName = "";
            this.TotalFeeTextBox.ReadOnly = true;
            this.TotalFeeTextBox.SetColorFlag = false;
            this.TotalFeeTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalFeeTextBox.Size = new System.Drawing.Size(90, 16);
            this.TotalFeeTextBox.SpecialCharacter = "%";
            this.TotalFeeTextBox.TabIndex = 12;
            this.TotalFeeTextBox.TabStop = false;
            this.TotalFeeTextBox.TextCustomFormat = "#,##0.00";
            this.TotalFeeTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.TotalFeeTextBox.WholeInteger = false;
            // 
            // SelectionGrid
            // 
            this.SelectionGrid.CalcManager = this.UltraFeeCalcManager;
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            appearance1.BorderColor = System.Drawing.Color.Black;
            appearance1.FontData.BoldAsString = "True";
            appearance1.FontData.Name = "Arial";
            appearance1.FontData.SizeInPoints = 8F;
            this.SelectionGrid.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.ColHeadersVisible = false;
            ultraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            ultraGridBand1.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.SelectionGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.SelectionGrid.DisplayLayout.MaxColScrollRegions = 1;
            this.SelectionGrid.DisplayLayout.MaxRowScrollRegions = 1;
            this.SelectionGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            this.SelectionGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.SelectionGrid.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.SelectionGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.SelectionGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.None;
            this.SelectionGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.SelectionGrid.Location = new System.Drawing.Point(-1, -1);
            this.SelectionGrid.Name = "SelectionGrid";
            this.SelectionGrid.Size = new System.Drawing.Size(765, 310);
            this.SelectionGrid.TabIndex = 1;
            this.SelectionGrid.Tag = "81004";
            this.SelectionGrid.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.SelectionGrid.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.SelectionGrid_InitializeRow);
            this.SelectionGrid.BeforeEnterEditMode += new System.ComponentModel.CancelEventHandler(this.SelectionGrid_BeforeEnterEditMode);
            this.SelectionGrid.InitializeTemplateAddRow += new Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventHandler(this.SelectionGrid_InitializeTemplateAddRow);
            this.SelectionGrid.AfterExitEditMode += new System.EventHandler(this.SelectionGrid_AfterExitEditMode);
            this.SelectionGrid.Click += new System.EventHandler(this.SelectionGrid_Click);
            this.SelectionGrid.AfterCellActivate += new System.EventHandler(this.SelectionGrid_AfterCellActivate);
            this.SelectionGrid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.SelectionGrid_InitializeLayout);
            this.SelectionGrid.BeforeExitEditMode += new Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventHandler(this.SelectionGrid_BeforeExitEditMode);
            this.SelectionGrid.CellDataError += new Infragistics.Win.UltraWinGrid.CellDataErrorEventHandler(this.SelectionGrid_CellDataError);
            this.SelectionGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SelectionGrid_MouseClick);
            this.SelectionGrid.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.SelectionGrid_CellChange);
            this.SelectionGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SelectionGrid_KeyPress);
            this.SelectionGrid.Enter += new System.EventHandler(this.SelectionGrid_Enter);
            // 
            // UltraFeeCalcManager
            // 
            this.UltraFeeCalcManager.ContainingControl = this;
            this.UltraFeeCalcManager.FormulaCircularityError += new Infragistics.Win.UltraWinCalcManager.FormulaCircularityErrorEventHandler(this.UltraFeeCalcManager_FormulaCircularityError);
            this.UltraFeeCalcManager.FormulaSyntaxError += new Infragistics.Win.UltraWinCalcManager.FormulaSyntaxErrorEventHandler(this.UltraFeeCalcManager_FormulaSyntaxError);
            // 
            // SelectionPictureBox
            // 
            this.SelectionPictureBox.Location = new System.Drawing.Point(752, 0);
            this.SelectionPictureBox.Name = "SelectionPictureBox";
            this.SelectionPictureBox.Size = new System.Drawing.Size(35, 335);
            this.SelectionPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SelectionPictureBox.TabIndex = 120;
            this.SelectionPictureBox.TabStop = false;
            this.SelectionPictureBox.Click += new System.EventHandler(this.SelectionPictureBox_Click);
            this.SelectionPictureBox.MouseHover += new System.EventHandler(this.SelectionPictureBox_MouseHover);
            // 
            // F81004
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.SelectionGridPanel);
            this.Controls.Add(this.SelectionPictureBox);
            this.Name = "F81004";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(804, 343);
            this.Tag = "81004";
            this.Load += new System.EventHandler(this.F81004_Load);
            this.Resize += new System.EventHandler(this.F81004_Resize);
            this.SelectionGridPanel.ResumeLayout(false);
            this.SummaryPanel.ResumeLayout(false);
            this.TotalFeePanel.ResumeLayout(false);
            this.TotalFeePanel.PerformLayout();
            this.UnitFeePanel.ResumeLayout(false);
            this.UnitFeePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectionGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UltraFeeCalcManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectionPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SelectionGridPanel;
        private System.Windows.Forms.PictureBox SelectionPictureBox;
        private TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid SelectionGrid;
        private System.Windows.Forms.ToolTip SelectionFormSliceToolTip;
        private Infragistics.Win.UltraWinCalcManager.UltraCalcManager UltraFeeCalcManager;
        private System.Windows.Forms.Panel SummaryPanel;
        private System.Windows.Forms.Panel TotalFeePanel;
        private System.Windows.Forms.Label TotalFeeLabel;
        private System.Windows.Forms.Panel UnitFeePanel;
        private TerraScan.UI.Controls.TerraScanTextBox TotalFeeTextBox;
    }
}
