namespace D20050
{
    partial class F35051
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
            this.ScheduleLinePictureBox = new System.Windows.Forms.PictureBox();
            this.ScheduleLineItemsEntirePanel = new System.Windows.Forms.Panel();
            this.ScheduleLineItemGrid = new TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid();
            this.ScheduleLineItemSummaryPanel = new System.Windows.Forms.Panel();
            this.TotalLinesValuePanel = new System.Windows.Forms.Panel();
            this.TotalLinesValueLabel = new System.Windows.Forms.Label();
            this.TotalLinesLabelPanel = new System.Windows.Forms.Panel();
            this.TotalLinesLabel = new System.Windows.Forms.Label();
            this.TotalValueLabelPanel = new System.Windows.Forms.Panel();
            this.TotalValueLabel = new System.Windows.Forms.Label();
            this.TotalValueSumPanel = new System.Windows.Forms.Panel();
            this.TotalValueSumLabel = new System.Windows.Forms.Label();
            this.TotalCostLabelPanel = new System.Windows.Forms.Panel();
            this.TotalCostLabel = new System.Windows.Forms.Label();
            this.TotalCostValuePanel = new System.Windows.Forms.Panel();
            this.TotalCostValueLabel = new System.Windows.Forms.Label();
            this.ScheduleLineItemHeaderPanel = new System.Windows.Forms.Panel();
            this.ScheduleLineItemButtonsPanel = new System.Windows.Forms.Panel();
            this.MoveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DeleteButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ScheduleLineItemsToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleLinePictureBox)).BeginInit();
            this.ScheduleLineItemsEntirePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleLineItemGrid)).BeginInit();
            this.ScheduleLineItemSummaryPanel.SuspendLayout();
            this.TotalLinesValuePanel.SuspendLayout();
            this.TotalLinesLabelPanel.SuspendLayout();
            this.TotalValueLabelPanel.SuspendLayout();
            this.TotalValueSumPanel.SuspendLayout();
            this.TotalCostLabelPanel.SuspendLayout();
            this.TotalCostValuePanel.SuspendLayout();
            this.ScheduleLineItemHeaderPanel.SuspendLayout();
            this.ScheduleLineItemButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScheduleLinePictureBox
            // 
            this.ScheduleLinePictureBox.Location = new System.Drawing.Point(760, 0);
            this.ScheduleLinePictureBox.Name = "ScheduleLinePictureBox";
            this.ScheduleLinePictureBox.Size = new System.Drawing.Size(42, 161);
            this.ScheduleLinePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ScheduleLinePictureBox.TabIndex = 121;
            this.ScheduleLinePictureBox.TabStop = false;
            this.ScheduleLinePictureBox.Click += new System.EventHandler(this.ScheduleLinePictureBox_Click);
            this.ScheduleLinePictureBox.MouseHover += new System.EventHandler(this.ScheduleLinePictureBox_MouseHover);
            // 
            // ScheduleLineItemsEntirePanel
            // 
            this.ScheduleLineItemsEntirePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.ScheduleLineItemsEntirePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScheduleLineItemsEntirePanel.Controls.Add(this.ScheduleLineItemGrid);
            this.ScheduleLineItemsEntirePanel.Controls.Add(this.ScheduleLineItemSummaryPanel);
            this.ScheduleLineItemsEntirePanel.Controls.Add(this.ScheduleLineItemHeaderPanel);
            this.ScheduleLineItemsEntirePanel.Location = new System.Drawing.Point(0, 0);
            this.ScheduleLineItemsEntirePanel.Name = "ScheduleLineItemsEntirePanel";
            this.ScheduleLineItemsEntirePanel.Size = new System.Drawing.Size(767, 161);
            this.ScheduleLineItemsEntirePanel.TabIndex = 0;
            // 
            // ScheduleLineItemGrid
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            appearance1.BorderColor = System.Drawing.Color.Black;
            appearance1.FontData.BoldAsString = "True";
            appearance1.FontData.Name = "Arial";
            appearance1.FontData.SizeInPoints = 8F;
            this.ScheduleLineItemGrid.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            appearance2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            ultraGridBand1.Override.HeaderAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            ultraGridBand1.Override.RowSelectorHeaderAppearance = appearance3;
            ultraGridBand1.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.ScheduleLineItemGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance4.BackColor = System.Drawing.Color.LightGray;
            appearance4.BorderColor = System.Drawing.Color.Black;
            this.ScheduleLineItemGrid.DisplayLayout.EmptyRowSettings.CellAppearance = appearance4;
            this.ScheduleLineItemGrid.DisplayLayout.MaxColScrollRegions = 1;
            this.ScheduleLineItemGrid.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(133)))));
            appearance5.ForeColor = System.Drawing.Color.White;
            this.ScheduleLineItemGrid.DisplayLayout.Override.ActiveRowAppearance = appearance5;
            this.ScheduleLineItemGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.ScheduleLineItemGrid.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.None;
            this.ScheduleLineItemGrid.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            appearance6.BorderColor = System.Drawing.Color.Black;
            this.ScheduleLineItemGrid.DisplayLayout.Override.CellAppearance = appearance6;
            appearance7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.ScheduleLineItemGrid.DisplayLayout.Override.EditCellAppearance = appearance7;
            appearance8.FontData.BoldAsString = "True";
            appearance8.FontData.Name = "Arial";
            appearance8.FontData.SizeInPoints = 8F;
            appearance8.ForeColor = System.Drawing.Color.Black;
            this.ScheduleLineItemGrid.DisplayLayout.Override.FilteredInCellAppearance = appearance8;
            appearance9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            appearance9.ForeColor = System.Drawing.Color.White;
            this.ScheduleLineItemGrid.DisplayLayout.Override.FixedHeaderAppearance = appearance9;
            appearance10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            appearance10.FontData.BoldAsString = "True";
            appearance10.FontData.Name = "Arial";
            appearance10.FontData.SizeInPoints = 10F;
            appearance10.ForeColor = System.Drawing.Color.White;
            this.ScheduleLineItemGrid.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.ScheduleLineItemGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.Standard;
            appearance11.ForeColor = System.Drawing.Color.White;
            this.ScheduleLineItemGrid.DisplayLayout.Override.HotTrackHeaderAppearance = appearance11;
            appearance12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ScheduleLineItemGrid.DisplayLayout.Override.RowAlternateAppearance = appearance12;
            appearance13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.ScheduleLineItemGrid.DisplayLayout.Override.RowSelectorAppearance = appearance13;
            this.ScheduleLineItemGrid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
            this.ScheduleLineItemGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            appearance14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this.ScheduleLineItemGrid.DisplayLayout.Override.SelectedCellAppearance = appearance14;
            this.ScheduleLineItemGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ScheduleLineItemGrid.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ScheduleLineItemGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            appearance15.BackColor = System.Drawing.Color.White;
            this.ScheduleLineItemGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance15;
            this.ScheduleLineItemGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ScheduleLineItemGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.None;
            this.ScheduleLineItemGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ScheduleLineItemGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScheduleLineItemGrid.Location = new System.Drawing.Point(-1, 43);
            this.ScheduleLineItemGrid.Name = "ScheduleLineItemGrid";
            this.ScheduleLineItemGrid.Size = new System.Drawing.Size(767, 90);
            this.ScheduleLineItemGrid.TabIndex = 5;
            this.ScheduleLineItemGrid.Tag = "35051";
            this.ScheduleLineItemGrid.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ScheduleLineItemGrid.AfterExitEditMode += new System.EventHandler(this.ScheduleLineItemGrid_AfterExitEditMode);
            this.ScheduleLineItemGrid.Error += new Infragistics.Win.UltraWinGrid.ErrorEventHandler(this.ScheduleLineItemGrid_Error);
            this.ScheduleLineItemGrid.Enter += new System.EventHandler(this.ScheduleLineItemGrid_Enter);
            this.ScheduleLineItemGrid.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.ScheduleLineItemGrid_BeforeRowsDeleted);
            this.ScheduleLineItemGrid.InitializeTemplateAddRow += new Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventHandler(this.ScheduleLineItemGrid_InitializeTemplateAddRow);
            this.ScheduleLineItemGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScheduleLineItemGrid_KeyDown);
            this.ScheduleLineItemGrid.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ScheduleLineItemGrid_InitializeRow);
            this.ScheduleLineItemGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScheduleLineItemGrid_MouseClick);
            this.ScheduleLineItemGrid.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.ScheduleLineItemGrid_AfterRowUpdate);
            this.ScheduleLineItemGrid.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.ScheduleLineItemGrid_BeforeCellActivate);
            this.ScheduleLineItemGrid.BeforeHeaderCheckStateChanged += new Infragistics.Win.UltraWinGrid.BeforeHeaderCheckStateChangedEventHandler(this.ScheduleLineItemGrid_BeforeHeaderCheckStateChanged);
            this.ScheduleLineItemGrid.BeforeExitEditMode += new Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventHandler(this.ScheduleLineItemGrid_BeforeExitEditMode);
            this.ScheduleLineItemGrid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ScheduleLineItemGrid_InitializeLayout);
            this.ScheduleLineItemGrid.AfterRowActivate += new System.EventHandler(this.ScheduleLineItemGrid_AfterRowActivate);
            this.ScheduleLineItemGrid.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ScheduleLineItemGrid_CellChange);
            // 
            // ScheduleLineItemSummaryPanel
            // 
            this.ScheduleLineItemSummaryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScheduleLineItemSummaryPanel.Controls.Add(this.TotalLinesValuePanel);
            this.ScheduleLineItemSummaryPanel.Controls.Add(this.TotalLinesLabelPanel);
            this.ScheduleLineItemSummaryPanel.Controls.Add(this.TotalValueLabelPanel);
            this.ScheduleLineItemSummaryPanel.Controls.Add(this.TotalValueSumPanel);
            this.ScheduleLineItemSummaryPanel.Controls.Add(this.TotalCostLabelPanel);
            this.ScheduleLineItemSummaryPanel.Controls.Add(this.TotalCostValuePanel);
            this.ScheduleLineItemSummaryPanel.Location = new System.Drawing.Point(-1, 131);
            this.ScheduleLineItemSummaryPanel.Name = "ScheduleLineItemSummaryPanel";
            this.ScheduleLineItemSummaryPanel.Size = new System.Drawing.Size(767, 29);
            this.ScheduleLineItemSummaryPanel.TabIndex = 6;
            // 
            // TotalLinesValuePanel
            // 
            this.TotalLinesValuePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalLinesValuePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalLinesValuePanel.Controls.Add(this.TotalLinesValueLabel);
            this.TotalLinesValuePanel.Location = new System.Drawing.Point(156, -2);
            this.TotalLinesValuePanel.Name = "TotalLinesValuePanel";
            this.TotalLinesValuePanel.Size = new System.Drawing.Size(45, 30);
            this.TotalLinesValuePanel.TabIndex = 8;
            // 
            // TotalLinesValueLabel
            // 
            this.TotalLinesValueLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalLinesValueLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalLinesValueLabel.ForeColor = System.Drawing.Color.White;
            this.TotalLinesValueLabel.Location = new System.Drawing.Point(4, 7);
            this.TotalLinesValueLabel.Name = "TotalLinesValueLabel";
            this.TotalLinesValueLabel.Size = new System.Drawing.Size(36, 16);
            this.TotalLinesValueLabel.TabIndex = 2;
            this.TotalLinesValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TotalLinesValueLabel.MouseHover += new System.EventHandler(this.DisplayLabelToolTip_MouseHover);
            // 
            // TotalLinesLabelPanel
            // 
            this.TotalLinesLabelPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalLinesLabelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalLinesLabelPanel.Controls.Add(this.TotalLinesLabel);
            this.TotalLinesLabelPanel.Location = new System.Drawing.Point(17, -2);
            this.TotalLinesLabelPanel.Name = "TotalLinesLabelPanel";
            this.TotalLinesLabelPanel.Size = new System.Drawing.Size(140, 30);
            this.TotalLinesLabelPanel.TabIndex = 7;
            // 
            // TotalLinesLabel
            // 
            this.TotalLinesLabel.AutoSize = true;
            this.TotalLinesLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalLinesLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalLinesLabel.ForeColor = System.Drawing.Color.White;
            this.TotalLinesLabel.Location = new System.Drawing.Point(10, 7);
            this.TotalLinesLabel.Name = "TotalLinesLabel";
            this.TotalLinesLabel.Size = new System.Drawing.Size(78, 16);
            this.TotalLinesLabel.TabIndex = 2;
            this.TotalLinesLabel.Text = "Total Lines";
            // 
            // TotalValueLabelPanel
            // 
            this.TotalValueLabelPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalValueLabelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalValueLabelPanel.Controls.Add(this.TotalValueLabel);
            this.TotalValueLabelPanel.Location = new System.Drawing.Point(575, -2);
            this.TotalValueLabelPanel.Name = "TotalValueLabelPanel";
            this.TotalValueLabelPanel.Size = new System.Drawing.Size(92, 30);
            this.TotalValueLabelPanel.TabIndex = 11;
            // 
            // TotalValueLabel
            // 
            this.TotalValueLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalValueLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalValueLabel.ForeColor = System.Drawing.Color.White;
            this.TotalValueLabel.Location = new System.Drawing.Point(10, 7);
            this.TotalValueLabel.Name = "TotalValueLabel";
            this.TotalValueLabel.Size = new System.Drawing.Size(81, 16);
            this.TotalValueLabel.TabIndex = 2;
            this.TotalValueLabel.Text = "Total Value";
            // 
            // TotalValueSumPanel
            // 
            this.TotalValueSumPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalValueSumPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalValueSumPanel.Controls.Add(this.TotalValueSumLabel);
            this.TotalValueSumPanel.Location = new System.Drawing.Point(666, -2);
            this.TotalValueSumPanel.Name = "TotalValueSumPanel";
            this.TotalValueSumPanel.Size = new System.Drawing.Size(100, 30);
            this.TotalValueSumPanel.TabIndex = 12;
            // 
            // TotalValueSumLabel
            // 
            this.TotalValueSumLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalValueSumLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalValueSumLabel.ForeColor = System.Drawing.Color.White;
            this.TotalValueSumLabel.Location = new System.Drawing.Point(13, 7);
            this.TotalValueSumLabel.Name = "TotalValueSumLabel";
            this.TotalValueSumLabel.Size = new System.Drawing.Size(65, 16);
            this.TotalValueSumLabel.TabIndex = 2;
            this.TotalValueSumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TotalValueSumLabel.MouseHover += new System.EventHandler(this.DisplayLabelToolTip_MouseHover);
            // 
            // TotalCostLabelPanel
            // 
            this.TotalCostLabelPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalCostLabelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalCostLabelPanel.Controls.Add(this.TotalCostLabel);
            this.TotalCostLabelPanel.Location = new System.Drawing.Point(393, -2);
            this.TotalCostLabelPanel.Name = "TotalCostLabelPanel";
            this.TotalCostLabelPanel.Size = new System.Drawing.Size(92, 30);
            this.TotalCostLabelPanel.TabIndex = 9;
            // 
            // TotalCostLabel
            // 
            this.TotalCostLabel.AutoSize = true;
            this.TotalCostLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalCostLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalCostLabel.ForeColor = System.Drawing.Color.White;
            this.TotalCostLabel.Location = new System.Drawing.Point(10, 7);
            this.TotalCostLabel.Name = "TotalCostLabel";
            this.TotalCostLabel.Size = new System.Drawing.Size(71, 16);
            this.TotalCostLabel.TabIndex = 2;
            this.TotalCostLabel.Text = "Total Cost";
            // 
            // TotalCostValuePanel
            // 
            this.TotalCostValuePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalCostValuePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalCostValuePanel.Controls.Add(this.TotalCostValueLabel);
            this.TotalCostValuePanel.Location = new System.Drawing.Point(484, -2);
            this.TotalCostValuePanel.Name = "TotalCostValuePanel";
            this.TotalCostValuePanel.Size = new System.Drawing.Size(92, 30);
            this.TotalCostValuePanel.TabIndex = 10;
            // 
            // TotalCostValueLabel
            // 
            this.TotalCostValueLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.TotalCostValueLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalCostValueLabel.ForeColor = System.Drawing.Color.White;
            this.TotalCostValueLabel.Location = new System.Drawing.Point(6, 7);
            this.TotalCostValueLabel.Name = "TotalCostValueLabel";
            this.TotalCostValueLabel.Size = new System.Drawing.Size(80, 16);
            this.TotalCostValueLabel.TabIndex = 2;
            this.TotalCostValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TotalCostValueLabel.MouseHover += new System.EventHandler(this.DisplayLabelToolTip_MouseHover);
            // 
            // ScheduleLineItemHeaderPanel
            // 
            this.ScheduleLineItemHeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.ScheduleLineItemHeaderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScheduleLineItemHeaderPanel.Controls.Add(this.ScheduleLineItemButtonsPanel);
            this.ScheduleLineItemHeaderPanel.Location = new System.Drawing.Point(17, -1);
            this.ScheduleLineItemHeaderPanel.Name = "ScheduleLineItemHeaderPanel";
            this.ScheduleLineItemHeaderPanel.Size = new System.Drawing.Size(749, 45);
            this.ScheduleLineItemHeaderPanel.TabIndex = 1;
            // 
            // ScheduleLineItemButtonsPanel
            // 
            this.ScheduleLineItemButtonsPanel.BackColor = System.Drawing.Color.Gray;
            this.ScheduleLineItemButtonsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScheduleLineItemButtonsPanel.Controls.Add(this.MoveButton);
            this.ScheduleLineItemButtonsPanel.Controls.Add(this.DeleteButton);
            this.ScheduleLineItemButtonsPanel.Location = new System.Drawing.Point(-1, 4);
            this.ScheduleLineItemButtonsPanel.Name = "ScheduleLineItemButtonsPanel";
            this.ScheduleLineItemButtonsPanel.Size = new System.Drawing.Size(244, 34);
            this.ScheduleLineItemButtonsPanel.TabIndex = 2;
            // 
            // MoveButton
            // 
            this.MoveButton.ActualPermission = false;
            this.MoveButton.ApplyDisableBehaviour = false;
            this.MoveButton.AutoEllipsis = true;
            this.MoveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.MoveButton.BorderColor = System.Drawing.Color.Wheat;
            this.MoveButton.CommentPriority = false;
            this.MoveButton.EnableAutoPrint = false;
            this.MoveButton.FilterStatus = false;
            this.MoveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MoveButton.FocusRectangleEnabled = true;
            this.MoveButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MoveButton.ImageSelected = false;
            this.MoveButton.Location = new System.Drawing.Point(16, 2);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.NewPadding = 5;
            this.MoveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.MoveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.MoveButton.Size = new System.Drawing.Size(98, 28);
            this.MoveButton.StatusIndicator = false;
            this.MoveButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MoveButton.StatusOffText = null;
            this.MoveButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.MoveButton.StatusOnText = null;
            this.MoveButton.TabIndex = 3;
            this.MoveButton.TabStop = false;
            this.MoveButton.Tag = "";
            this.MoveButton.Text = "Move";
            this.MoveButton.UseVisualStyleBackColor = false;
            this.MoveButton.Click += new System.EventHandler(this.MoveButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.ActualPermission = false;
            this.DeleteButton.ApplyDisableBehaviour = false;
            this.DeleteButton.AutoEllipsis = true;
            this.DeleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.DeleteButton.BorderColor = System.Drawing.Color.Wheat;
            this.DeleteButton.CommentPriority = false;
            this.DeleteButton.EnableAutoPrint = false;
            this.DeleteButton.FilterStatus = false;
            this.DeleteButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteButton.FocusRectangleEnabled = true;
            this.DeleteButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DeleteButton.ImageSelected = false;
            this.DeleteButton.Location = new System.Drawing.Point(130, 2);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.NewPadding = 5;
            this.DeleteButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.DeleteButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.DeleteButton.Size = new System.Drawing.Size(98, 28);
            this.DeleteButton.StatusIndicator = false;
            this.DeleteButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DeleteButton.StatusOffText = null;
            this.DeleteButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.DeleteButton.StatusOnText = null;
            this.DeleteButton.TabIndex = 4;
            this.DeleteButton.TabStop = false;
            this.DeleteButton.Tag = "DELETE";
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // F35051
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ScheduleLineItemsEntirePanel);
            this.Controls.Add(this.ScheduleLinePictureBox);
            this.Name = "F35051";
            this.Size = new System.Drawing.Size(804, 161);
            this.Load += new System.EventHandler(this.F35051_Load);
            this.Resize += new System.EventHandler(this.F35051_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleLinePictureBox)).EndInit();
            this.ScheduleLineItemsEntirePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleLineItemGrid)).EndInit();
            this.ScheduleLineItemSummaryPanel.ResumeLayout(false);
            this.TotalLinesValuePanel.ResumeLayout(false);
            this.TotalLinesLabelPanel.ResumeLayout(false);
            this.TotalLinesLabelPanel.PerformLayout();
            this.TotalValueLabelPanel.ResumeLayout(false);
            this.TotalValueSumPanel.ResumeLayout(false);
            this.TotalCostLabelPanel.ResumeLayout(false);
            this.TotalCostLabelPanel.PerformLayout();
            this.TotalCostValuePanel.ResumeLayout(false);
            this.ScheduleLineItemHeaderPanel.ResumeLayout(false);
            this.ScheduleLineItemButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.PictureBox ScheduleLinePictureBox;
        private System.Windows.Forms.Panel ScheduleLineItemsEntirePanel;
        private System.Windows.Forms.Panel ScheduleLineItemHeaderPanel;
        private System.Windows.Forms.Panel ScheduleLineItemButtonsPanel;
        private TerraScan.UI.Controls.TerraScanButton MoveButton;
        private TerraScan.UI.Controls.TerraScanButton DeleteButton;
        private System.Windows.Forms.Panel ScheduleLineItemSummaryPanel;
        private System.Windows.Forms.ToolTip ScheduleLineItemsToolTip;
        private TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid ScheduleLineItemGrid;
        private System.Windows.Forms.Panel TotalCostValuePanel;
        private System.Windows.Forms.Panel TotalLinesLabelPanel;
        private System.Windows.Forms.Label TotalLinesLabel;
        private System.Windows.Forms.Panel TotalValueLabelPanel;
        private System.Windows.Forms.Label TotalValueLabel;
        private System.Windows.Forms.Panel TotalValueSumPanel;
        private System.Windows.Forms.Label TotalValueSumLabel;
        private System.Windows.Forms.Panel TotalCostLabelPanel;
        private System.Windows.Forms.Label TotalCostLabel;
        private System.Windows.Forms.Label TotalCostValueLabel;
        private System.Windows.Forms.Panel TotalLinesValuePanel;
        private System.Windows.Forms.Label TotalLinesValueLabel;
    }
}
