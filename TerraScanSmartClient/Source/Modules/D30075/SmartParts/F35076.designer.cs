namespace D30075
{
    partial class F35076
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.StateAssessedGrid = new TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.PersonalPropertyTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.RealPropertyTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TotalValueTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.TotalValueLabel = new System.Windows.Forms.Label();
            this.StateAssessedOwnerToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.StateAssessedPictureBox = new System.Windows.Forms.PictureBox();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StateAssessedGrid)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StateAssessedPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.White;
            this.MainPanel.Controls.Add(this.StateAssessedGrid);
            this.MainPanel.Controls.Add(this.BottomPanel);
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(765, 137);
            this.MainPanel.TabIndex = 0;
            // 
            // StateAssessedGrid
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            appearance1.BorderColor = System.Drawing.Color.Black;
            appearance1.FontData.BoldAsString = "True";
            appearance1.FontData.Name = "Arial";
            appearance1.FontData.SizeInPoints = 8F;
            this.StateAssessedGrid.DisplayLayout.Appearance = appearance1;
            this.StateAssessedGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridBand1.MinRows = 5;
            ultraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            ultraGridBand1.Override.AllowGroupMoving = Infragistics.Win.UltraWinGrid.AllowGroupMoving.NotAllowed;
            appearance2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            ultraGridBand1.Override.HeaderAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            ultraGridBand1.Override.RowSelectorHeaderAppearance = appearance3;
            ultraGridBand1.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.StateAssessedGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance4.BackColor = System.Drawing.Color.LightGray;
            appearance4.BorderColor = System.Drawing.Color.Black;
            this.StateAssessedGrid.DisplayLayout.EmptyRowSettings.CellAppearance = appearance4;
            this.StateAssessedGrid.DisplayLayout.MaxColScrollRegions = 1;
            this.StateAssessedGrid.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(133)))));
            appearance5.ForeColor = System.Drawing.Color.White;
            this.StateAssessedGrid.DisplayLayout.Override.ActiveRowAppearance = appearance5;
            this.StateAssessedGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinBand;
            this.StateAssessedGrid.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            appearance6.BorderColor = System.Drawing.Color.Black;
            this.StateAssessedGrid.DisplayLayout.Override.CellAppearance = appearance6;
            appearance7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.StateAssessedGrid.DisplayLayout.Override.EditCellAppearance = appearance7;
            appearance8.FontData.BoldAsString = "True";
            appearance8.FontData.Name = "Arial";
            appearance8.FontData.SizeInPoints = 8F;
            appearance8.ForeColor = System.Drawing.Color.Black;
            this.StateAssessedGrid.DisplayLayout.Override.FilteredInCellAppearance = appearance8;
            appearance9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            appearance9.ForeColor = System.Drawing.Color.White;
            this.StateAssessedGrid.DisplayLayout.Override.FixedHeaderAppearance = appearance9;
            appearance10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            appearance10.FontData.BoldAsString = "True";
            appearance10.FontData.Name = "Arial";
            appearance10.FontData.SizeInPoints = 10F;
            appearance10.ForeColor = System.Drawing.Color.White;
            this.StateAssessedGrid.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.StateAssessedGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.StateAssessedGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.Standard;
            appearance11.ForeColor = System.Drawing.Color.White;
            this.StateAssessedGrid.DisplayLayout.Override.HotTrackHeaderAppearance = appearance11;
            appearance12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.StateAssessedGrid.DisplayLayout.Override.RowAlternateAppearance = appearance12;
            appearance13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.StateAssessedGrid.DisplayLayout.Override.RowSelectorAppearance = appearance13;
            this.StateAssessedGrid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
            this.StateAssessedGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            appearance14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this.StateAssessedGrid.DisplayLayout.Override.SelectedCellAppearance = appearance14;
            this.StateAssessedGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.StateAssessedGrid.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.StateAssessedGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.None;
            appearance15.BackColor = System.Drawing.Color.White;
            this.StateAssessedGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance15;
            this.StateAssessedGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.StateAssessedGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.None;
            this.StateAssessedGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.StateAssessedGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateAssessedGrid.Location = new System.Drawing.Point(-1, 0);
            this.StateAssessedGrid.Name = "StateAssessedGrid";
            this.StateAssessedGrid.Size = new System.Drawing.Size(767, 106);
            this.StateAssessedGrid.TabIndex = 1;
            this.StateAssessedGrid.Tag = "35050";
            this.StateAssessedGrid.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            //this.StateAssessedGrid.AfterExitEditMode += new System.EventHandler(this.StateAssessedGrid_AfterExitEditMode);
            this.StateAssessedGrid.Error += new Infragistics.Win.UltraWinGrid.ErrorEventHandler(this.StateAssessedGrid_Error);
            this.StateAssessedGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StateAssessedGrid_MouseDown);
            this.StateAssessedGrid.AfterColPosChanged += new Infragistics.Win.UltraWinGrid.AfterColPosChangedEventHandler(this.StateAssessedGrid_AfterColPosChanged);
            this.StateAssessedGrid.Enter += new System.EventHandler(this.StateAssessedGrid_Enter);
            this.StateAssessedGrid.BeforeCellDeactivate += new System.ComponentModel.CancelEventHandler(this.StateAssessedGrid_BeforeCellDeactivate);
            this.StateAssessedGrid.InitializeTemplateAddRow += new Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventHandler(this.StateAssessedGrid_InitializeTemplateAddRow);
            this.StateAssessedGrid.Click += new System.EventHandler(this.StateAssessedGrid_Click);
            this.StateAssessedGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StateAssessedGrid_KeyDown);
            this.StateAssessedGrid.BeforeRowDeactivate += new System.ComponentModel.CancelEventHandler(this.StateAssessedGrid_BeforeRowDeactivate);
            this.StateAssessedGrid.AfterCellActivate += new System.EventHandler(this.StateAssessedGrid_AfterCellActivate);
            this.StateAssessedGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StateAssessedGrid_MouseClick);
            this.StateAssessedGrid.TextChanged += new System.EventHandler(this.StateAssessedGrid_TextChanged);
            this.StateAssessedGrid.CellDataError += new Infragistics.Win.UltraWinGrid.CellDataErrorEventHandler(this.StateAssessedGrid_CellDataError);
            this.StateAssessedGrid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.StateAssessedGrid_InitializeLayout);
            this.StateAssessedGrid.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.StateAssessedGrid_CellChange);
            this.StateAssessedGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.StateAssessedGrid_KeyUp);
            this.StateAssessedGrid.BeforeColPosChanged += new Infragistics.Win.UltraWinGrid.BeforeColPosChangedEventHandler(this.StateAssessedGrid_BeforeColPosChanged);
            // 
            // BottomPanel
            // 
            this.BottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.BottomPanel.Controls.Add(this.PersonalPropertyTextBox);
            this.BottomPanel.Controls.Add(this.RealPropertyTextBox);
            this.BottomPanel.Controls.Add(this.TotalValueTextBox);
            this.BottomPanel.Controls.Add(this.panel6);
            this.BottomPanel.Controls.Add(this.TotalValueLabel);
            this.BottomPanel.Location = new System.Drawing.Point(-1, 106);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(769, 31);
            this.BottomPanel.TabIndex = 2;
            // 
            // PersonalPropertyTextBox
            // 
            this.PersonalPropertyTextBox.AllowClick = true;
            this.PersonalPropertyTextBox.AllowNegativeSign = false;
            this.PersonalPropertyTextBox.ApplyCFGFormat = true;
            this.PersonalPropertyTextBox.ApplyCurrencyFormat = true;
            this.PersonalPropertyTextBox.ApplyFocusColor = true;
            this.PersonalPropertyTextBox.ApplyNegativeForeColor = System.Drawing.Color.DarkGray;
            this.PersonalPropertyTextBox.ApplyNegativeStandard = true;
            this.PersonalPropertyTextBox.ApplyParentFocusColor = true;
            this.PersonalPropertyTextBox.ApplyTimeFormat = false;
            this.PersonalPropertyTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.PersonalPropertyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PersonalPropertyTextBox.CFromatWihoutSymbol = false;
            this.PersonalPropertyTextBox.CheckForEmpty = false;
            this.PersonalPropertyTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.PersonalPropertyTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PersonalPropertyTextBox.Digits = -1;
            this.PersonalPropertyTextBox.EmptyDecimalValue = false;
            this.PersonalPropertyTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.PersonalPropertyTextBox.ForeColor = System.Drawing.Color.White;
            this.PersonalPropertyTextBox.IsEditable = false;
            this.PersonalPropertyTextBox.IsQueryableFileld = true;
            this.PersonalPropertyTextBox.Location = new System.Drawing.Point(207, 6);
            this.PersonalPropertyTextBox.LockKeyPress = true;
            this.PersonalPropertyTextBox.MaxLength = 50;
            this.PersonalPropertyTextBox.Name = "PersonalPropertyTextBox";
            this.PersonalPropertyTextBox.PersistDefaultColor = false;
            this.PersonalPropertyTextBox.Precision = 2;
            this.PersonalPropertyTextBox.QueryingFileldName = "";
            this.PersonalPropertyTextBox.ReadOnly = true;
            this.PersonalPropertyTextBox.SetColorFlag = false;
            this.PersonalPropertyTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.PersonalPropertyTextBox.Size = new System.Drawing.Size(150, 16);
            this.PersonalPropertyTextBox.SpecialCharacter = "%";
            this.PersonalPropertyTextBox.TabIndex = 125;
            this.PersonalPropertyTextBox.TabStop = false;
            this.PersonalPropertyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PersonalPropertyTextBox.TextCustomFormat = "$#,##0";
            this.PersonalPropertyTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.PersonalPropertyTextBox.WholeInteger = false;
            this.PersonalPropertyTextBox.Click += new System.EventHandler(this.PersonalPropertyTextBox_Click);
            this.PersonalPropertyTextBox.Enter += new System.EventHandler(this.PersonalPropertyTextBox_Enter);
            // 
            // RealPropertyTextBox
            // 
            this.RealPropertyTextBox.AllowClick = true;
            this.RealPropertyTextBox.AllowNegativeSign = false;
            this.RealPropertyTextBox.ApplyCFGFormat = true;
            this.RealPropertyTextBox.ApplyCurrencyFormat = true;
            this.RealPropertyTextBox.ApplyFocusColor = true;
            this.RealPropertyTextBox.ApplyNegativeForeColor = System.Drawing.Color.DarkGray;
            this.RealPropertyTextBox.ApplyNegativeStandard = true;
            this.RealPropertyTextBox.ApplyParentFocusColor = true;
            this.RealPropertyTextBox.ApplyTimeFormat = false;
            this.RealPropertyTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.RealPropertyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RealPropertyTextBox.CFromatWihoutSymbol = false;
            this.RealPropertyTextBox.CheckForEmpty = false;
            this.RealPropertyTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.RealPropertyTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RealPropertyTextBox.Digits = -1;
            this.RealPropertyTextBox.EmptyDecimalValue = false;
            this.RealPropertyTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.RealPropertyTextBox.ForeColor = System.Drawing.Color.White;
            this.RealPropertyTextBox.IsEditable = false;
            this.RealPropertyTextBox.IsQueryableFileld = true;
            this.RealPropertyTextBox.Location = new System.Drawing.Point(357, 6);
            this.RealPropertyTextBox.LockKeyPress = true;
            this.RealPropertyTextBox.MaxLength = 50;
            this.RealPropertyTextBox.Name = "RealPropertyTextBox";
            this.RealPropertyTextBox.PersistDefaultColor = false;
            this.RealPropertyTextBox.Precision = 2;
            this.RealPropertyTextBox.QueryingFileldName = "";
            this.RealPropertyTextBox.ReadOnly = true;
            this.RealPropertyTextBox.SetColorFlag = false;
            this.RealPropertyTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.RealPropertyTextBox.Size = new System.Drawing.Size(150, 16);
            this.RealPropertyTextBox.SpecialCharacter = "%";
            this.RealPropertyTextBox.TabIndex = 124;
            this.RealPropertyTextBox.TabStop = false;
            this.RealPropertyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.RealPropertyTextBox.TextCustomFormat = "$#,##0";
            this.RealPropertyTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.RealPropertyTextBox.WholeInteger = false;
            this.RealPropertyTextBox.Click += new System.EventHandler(this.RealPropertyTextBox_Click);
            this.RealPropertyTextBox.Enter += new System.EventHandler(this.RealPropertyTextBox_Enter);
            // 
            // TotalValueTextBox
            // 
            this.TotalValueTextBox.AllowClick = true;
            this.TotalValueTextBox.AllowNegativeSign = false;
            this.TotalValueTextBox.ApplyCFGFormat = true;
            this.TotalValueTextBox.ApplyCurrencyFormat = true;
            this.TotalValueTextBox.ApplyFocusColor = true;
            this.TotalValueTextBox.ApplyNegativeForeColor = System.Drawing.Color.DarkGray;
            this.TotalValueTextBox.ApplyNegativeStandard = true;
            this.TotalValueTextBox.ApplyParentFocusColor = true;
            this.TotalValueTextBox.ApplyTimeFormat = false;
            this.TotalValueTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalValueTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TotalValueTextBox.CFromatWihoutSymbol = false;
            this.TotalValueTextBox.CheckForEmpty = false;
            this.TotalValueTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.TotalValueTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TotalValueTextBox.Digits = -1;
            this.TotalValueTextBox.EmptyDecimalValue = false;
            this.TotalValueTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.TotalValueTextBox.ForeColor = System.Drawing.Color.White;
            this.TotalValueTextBox.IsEditable = false;
            this.TotalValueTextBox.IsQueryableFileld = true;
            this.TotalValueTextBox.Location = new System.Drawing.Point(679, 6);
            this.TotalValueTextBox.LockKeyPress = true;
            this.TotalValueTextBox.MaxLength = 50;
            this.TotalValueTextBox.Name = "TotalValueTextBox";
            this.TotalValueTextBox.PersistDefaultColor = false;
            this.TotalValueTextBox.Precision = 2;
            this.TotalValueTextBox.QueryingFileldName = "";
            this.TotalValueTextBox.ReadOnly = true;
            this.TotalValueTextBox.SetColorFlag = false;
            this.TotalValueTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalValueTextBox.Size = new System.Drawing.Size(80, 16);
            this.TotalValueTextBox.SpecialCharacter = "%";
            this.TotalValueTextBox.TabIndex = 4;
            this.TotalValueTextBox.TabStop = false;
            this.TotalValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TotalValueTextBox.TextCustomFormat = "$#,##0";
            this.TotalValueTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.TotalValueTextBox.WholeInteger = false;
            this.TotalValueTextBox.Click += new System.EventHandler(this.TotalValueTextBox_Click);
            this.TotalValueTextBox.Enter += new System.EventHandler(this.TotalValueTextBox_Enter);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Black;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(765, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(2, 30);
            this.panel6.TabIndex = 123;
            // 
            // TotalValueLabel
            // 
            this.TotalValueLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalValueLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalValueLabel.ForeColor = System.Drawing.Color.White;
            this.TotalValueLabel.Location = new System.Drawing.Point(7, 6);
            this.TotalValueLabel.Name = "TotalValueLabel";
            this.TotalValueLabel.Size = new System.Drawing.Size(207, 16);
            this.TotalValueLabel.TabIndex = 3;
            this.TotalValueLabel.Text = "Totals:";
            this.TotalValueLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // StateAssessedPictureBox
            // 
            this.StateAssessedPictureBox.BackColor = System.Drawing.Color.White;
            this.StateAssessedPictureBox.Location = new System.Drawing.Point(751, 0);
            this.StateAssessedPictureBox.Name = "StateAssessedPictureBox";
            this.StateAssessedPictureBox.Size = new System.Drawing.Size(50, 137);
            this.StateAssessedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StateAssessedPictureBox.TabIndex = 163;
            this.StateAssessedPictureBox.TabStop = false;
            this.StateAssessedPictureBox.Click += new System.EventHandler(this.StateAssessedPictureBox_Click);
            this.StateAssessedPictureBox.MouseHover += new System.EventHandler(this.StateAssessedPictureBox_MouseHover);
            // 
            // F35076
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.StateAssessedPictureBox);
            this.Name = "F35076";
            this.Size = new System.Drawing.Size(804, 138);
            this.Load += new System.EventHandler(this.F35076_Load);
            this.Resize += new System.EventHandler(this.F35076_Resize);
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StateAssessedGrid)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StateAssessedPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid StateAssessedGrid;
        private System.Windows.Forms.ToolTip StateAssessedOwnerToolTip;
        private System.Windows.Forms.PictureBox StateAssessedPictureBox;
        private System.Windows.Forms.Panel BottomPanel;
        private TerraScan.UI.Controls.TerraScanTextBox TotalValueTextBox;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label TotalValueLabel;
        private TerraScan.UI.Controls.TerraScanTextBox PersonalPropertyTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox RealPropertyTextBox;
    }
}
