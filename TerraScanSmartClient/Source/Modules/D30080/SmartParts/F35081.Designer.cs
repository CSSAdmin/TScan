namespace D30080
{
    partial class F35081
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
            this.CentralAssessedGrid = new TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.LowerPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.terraScanTextBox3 = new TerraScan.UI.Controls.TerraScanTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SchoolBondTotalTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SchoolGeneralTotalTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CompanyNumberPanel = new System.Windows.Forms.Panel();
            this.CountyGeneralTotalTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CompanyLabel = new System.Windows.Forms.Label();
            this.CentralAssessedPictureBox = new System.Windows.Forms.PictureBox();
            this.CentralAssessedOwnerToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CentralAssessedGrid)).BeginInit();
            this.LowerPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.CompanyNumberPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CentralAssessedPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.White;
            this.MainPanel.Controls.Add(this.CentralAssessedGrid);
            this.MainPanel.Controls.Add(this.BottomPanel);
            this.MainPanel.Controls.Add(this.LowerPanel);
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(765, 175);
            this.MainPanel.TabIndex = 1;
            this.MainPanel.TabStop = true;
            // 
            // CentralAssessedGrid
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            appearance1.BorderColor = System.Drawing.Color.Black;
            appearance1.FontData.BoldAsString = "True";
            appearance1.FontData.Name = "Arial";
            appearance1.FontData.SizeInPoints = 8F;
            this.CentralAssessedGrid.DisplayLayout.Appearance = appearance1;
            this.CentralAssessedGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridBand1.MinRows = 5;
            ultraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            ultraGridBand1.Override.AllowGroupMoving = Infragistics.Win.UltraWinGrid.AllowGroupMoving.NotAllowed;
            appearance2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            ultraGridBand1.Override.HeaderAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            ultraGridBand1.Override.RowSelectorHeaderAppearance = appearance3;
            ultraGridBand1.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.CentralAssessedGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance4.BackColor = System.Drawing.Color.LightGray;
            appearance4.BorderColor = System.Drawing.Color.Black;
            this.CentralAssessedGrid.DisplayLayout.EmptyRowSettings.CellAppearance = appearance4;
            this.CentralAssessedGrid.DisplayLayout.MaxColScrollRegions = 1;
            this.CentralAssessedGrid.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(133)))));
            appearance5.ForeColor = System.Drawing.Color.White;
            this.CentralAssessedGrid.DisplayLayout.Override.ActiveRowAppearance = appearance5;
            this.CentralAssessedGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinBand;
            this.CentralAssessedGrid.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            appearance6.BorderColor = System.Drawing.Color.Black;
            this.CentralAssessedGrid.DisplayLayout.Override.CellAppearance = appearance6;
            appearance7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.CentralAssessedGrid.DisplayLayout.Override.EditCellAppearance = appearance7;
            appearance8.FontData.BoldAsString = "True";
            appearance8.FontData.Name = "Arial";
            appearance8.FontData.SizeInPoints = 8F;
            appearance8.ForeColor = System.Drawing.Color.Black;
            this.CentralAssessedGrid.DisplayLayout.Override.FilteredInCellAppearance = appearance8;
            appearance9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            appearance9.ForeColor = System.Drawing.Color.White;
            this.CentralAssessedGrid.DisplayLayout.Override.FixedHeaderAppearance = appearance9;
            appearance10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            appearance10.FontData.BoldAsString = "True";
            appearance10.FontData.Name = "Arial";
            appearance10.FontData.SizeInPoints = 10F;
            appearance10.ForeColor = System.Drawing.Color.White;
            this.CentralAssessedGrid.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.CentralAssessedGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.CentralAssessedGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.Standard;
            appearance11.ForeColor = System.Drawing.Color.White;
            this.CentralAssessedGrid.DisplayLayout.Override.HotTrackHeaderAppearance = appearance11;
            appearance12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.CentralAssessedGrid.DisplayLayout.Override.RowAlternateAppearance = appearance12;
            appearance13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.CentralAssessedGrid.DisplayLayout.Override.RowSelectorAppearance = appearance13;
            this.CentralAssessedGrid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
            this.CentralAssessedGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            appearance14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this.CentralAssessedGrid.DisplayLayout.Override.SelectedCellAppearance = appearance14;
            this.CentralAssessedGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.CentralAssessedGrid.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.CentralAssessedGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.SingleAutoDrag;
            appearance15.BackColor = System.Drawing.Color.White;
            this.CentralAssessedGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance15;
            this.CentralAssessedGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.CentralAssessedGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Horizontal;
            this.CentralAssessedGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.CentralAssessedGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CentralAssessedGrid.Location = new System.Drawing.Point(-1, 0);
            this.CentralAssessedGrid.Name = "CentralAssessedGrid";
            this.CentralAssessedGrid.Size = new System.Drawing.Size(767, 106);
            this.CentralAssessedGrid.TabIndex = 1;
            this.CentralAssessedGrid.Tag = "35081";
            this.CentralAssessedGrid.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.CentralAssessedGrid.Error += new Infragistics.Win.UltraWinGrid.ErrorEventHandler(this.CentralAssessedGrid_Error);
            this.CentralAssessedGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CentralAssessedGrid_MouseDown);
            this.CentralAssessedGrid.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.CentralAssessedGrid_AfterCellUpdate);
            this.CentralAssessedGrid.Enter += new System.EventHandler(this.CentralAssessedGrid_Enter);
            this.CentralAssessedGrid.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.CentralAssessedGrid_BeforeRowsDeleted);
            this.CentralAssessedGrid.BeforeCellDeactivate += new System.ComponentModel.CancelEventHandler(this.CentralAssessedGrid_BeforeCellDeactivate);
            this.CentralAssessedGrid.InitializeTemplateAddRow += new Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventHandler(this.CentralAssessedGrid_InitializeTemplateAddRow);
            this.CentralAssessedGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CentralAssessedGrid_KeyDown);
            this.CentralAssessedGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CentralAssessedGrid_MouseClick);
            this.CentralAssessedGrid.TextChanged += new System.EventHandler(this.CentralAssessedGrid_TextChanged);
            this.CentralAssessedGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CentralAssessedGrid_KeyPress);
            this.CentralAssessedGrid.CellDataError += new Infragistics.Win.UltraWinGrid.CellDataErrorEventHandler(this.CentralAssessedGrid_CellDataError);
            this.CentralAssessedGrid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.CentralAssessedGrid_InitializeLayout);
            this.CentralAssessedGrid.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.CentralAssessedGrid_CellChange);
            this.CentralAssessedGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CentralAssessedGrid_KeyUp);
            this.CentralAssessedGrid.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.CentralAssessedGrid_AfterSelectChange);
            // 
            // BottomPanel
            // 
            this.BottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(103)))));
            this.BottomPanel.Location = new System.Drawing.Point(0, 107);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(769, 31);
            this.BottomPanel.TabIndex = 2;
            // 
            // LowerPanel
            // 
            this.LowerPanel.BackColor = System.Drawing.Color.Transparent;
            this.LowerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LowerPanel.Controls.Add(this.panel3);
            this.LowerPanel.Controls.Add(this.panel2);
            this.LowerPanel.Controls.Add(this.panel1);
            this.LowerPanel.Controls.Add(this.CompanyNumberPanel);
            this.LowerPanel.Location = new System.Drawing.Point(0, 136);
            this.LowerPanel.Name = "LowerPanel";
            this.LowerPanel.Size = new System.Drawing.Size(769, 37);
            this.LowerPanel.TabIndex = 166;
            this.LowerPanel.TabStop = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.terraScanTextBox3);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(566, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(201, 37);
            this.panel3.TabIndex = 8;
            this.panel3.TabStop = true;
            // 
            // terraScanTextBox3
            // 
            this.terraScanTextBox3.AllowClick = true;
            this.terraScanTextBox3.AllowNegativeSign = false;
            this.terraScanTextBox3.ApplyCFGFormat = false;
            this.terraScanTextBox3.ApplyCurrencyFormat = false;
            this.terraScanTextBox3.ApplyFocusColor = true;
            this.terraScanTextBox3.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.terraScanTextBox3.ApplyNegativeStandard = true;
            this.terraScanTextBox3.ApplyParentFocusColor = true;
            this.terraScanTextBox3.ApplyTimeFormat = false;
            this.terraScanTextBox3.BackColor = System.Drawing.Color.White;
            this.terraScanTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.terraScanTextBox3.CFromatWihoutSymbol = false;
            this.terraScanTextBox3.CheckForEmpty = false;
            this.terraScanTextBox3.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.terraScanTextBox3.Digits = -1;
            this.terraScanTextBox3.EmptyDecimalValue = false;
            this.terraScanTextBox3.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.terraScanTextBox3.ForeColor = System.Drawing.Color.Gray;
            this.terraScanTextBox3.IsEditable = false;
            this.terraScanTextBox3.IsQueryableFileld = true;
            this.terraScanTextBox3.Location = new System.Drawing.Point(16, 16);
            this.terraScanTextBox3.LockKeyPress = true;
            this.terraScanTextBox3.MaxLength = 50;
            this.terraScanTextBox3.Name = "terraScanTextBox3";
            this.terraScanTextBox3.PersistDefaultColor = false;
            this.terraScanTextBox3.Precision = 2;
            this.terraScanTextBox3.QueryingFileldName = "";
            this.terraScanTextBox3.ReadOnly = true;
            this.terraScanTextBox3.SetColorFlag = true;
            this.terraScanTextBox3.SetFocusColor = System.Drawing.Color.White;
            this.terraScanTextBox3.Size = new System.Drawing.Size(178, 16);
            this.terraScanTextBox3.SpecialCharacter = "%";
            this.terraScanTextBox3.TabIndex = 1;
            this.terraScanTextBox3.TabStop = false;
            this.terraScanTextBox3.TextCustomFormat = "";
            this.terraScanTextBox3.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.terraScanTextBox3.WholeInteger = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(-1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Schools With Bonds Total:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.SchoolBondTotalTextBox);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(377, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(190, 37);
            this.panel2.TabIndex = 7;
            this.panel2.TabStop = true;
            // 
            // SchoolBondTotalTextBox
            // 
            this.SchoolBondTotalTextBox.AllowClick = true;
            this.SchoolBondTotalTextBox.AllowNegativeSign = false;
            this.SchoolBondTotalTextBox.ApplyCFGFormat = false;
            this.SchoolBondTotalTextBox.ApplyCurrencyFormat = false;
            this.SchoolBondTotalTextBox.ApplyFocusColor = true;
            this.SchoolBondTotalTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.SchoolBondTotalTextBox.ApplyNegativeStandard = true;
            this.SchoolBondTotalTextBox.ApplyParentFocusColor = true;
            this.SchoolBondTotalTextBox.ApplyTimeFormat = false;
            this.SchoolBondTotalTextBox.BackColor = System.Drawing.Color.White;
            this.SchoolBondTotalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SchoolBondTotalTextBox.CFromatWihoutSymbol = false;
            this.SchoolBondTotalTextBox.CheckForEmpty = false;
            this.SchoolBondTotalTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SchoolBondTotalTextBox.Digits = -1;
            this.SchoolBondTotalTextBox.EmptyDecimalValue = false;
            this.SchoolBondTotalTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.SchoolBondTotalTextBox.ForeColor = System.Drawing.Color.Gray;
            this.SchoolBondTotalTextBox.IsEditable = false;
            this.SchoolBondTotalTextBox.IsQueryableFileld = true;
            this.SchoolBondTotalTextBox.Location = new System.Drawing.Point(16, 16);
            this.SchoolBondTotalTextBox.LockKeyPress = true;
            this.SchoolBondTotalTextBox.MaxLength = 50;
            this.SchoolBondTotalTextBox.Name = "SchoolBondTotalTextBox";
            this.SchoolBondTotalTextBox.PersistDefaultColor = false;
            this.SchoolBondTotalTextBox.Precision = 2;
            this.SchoolBondTotalTextBox.QueryingFileldName = "";
            this.SchoolBondTotalTextBox.ReadOnly = true;
            this.SchoolBondTotalTextBox.SetColorFlag = false;
            this.SchoolBondTotalTextBox.SetFocusColor = System.Drawing.Color.Empty;
            this.SchoolBondTotalTextBox.Size = new System.Drawing.Size(168, 16);
            this.SchoolBondTotalTextBox.SpecialCharacter = "%";
            this.SchoolBondTotalTextBox.TabIndex = 1;
            this.SchoolBondTotalTextBox.TabStop = false;
            this.SchoolBondTotalTextBox.TextCustomFormat = "";
            this.SchoolBondTotalTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.SchoolBondTotalTextBox.WholeInteger = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(-1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "School Bond Total:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.SchoolGeneralTotalTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(188, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 37);
            this.panel1.TabIndex = 6;
            this.panel1.TabStop = true;
            // 
            // SchoolGeneralTotalTextBox
            // 
            this.SchoolGeneralTotalTextBox.AllowClick = true;
            this.SchoolGeneralTotalTextBox.AllowNegativeSign = false;
            this.SchoolGeneralTotalTextBox.ApplyCFGFormat = false;
            this.SchoolGeneralTotalTextBox.ApplyCurrencyFormat = false;
            this.SchoolGeneralTotalTextBox.ApplyFocusColor = true;
            this.SchoolGeneralTotalTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.SchoolGeneralTotalTextBox.ApplyNegativeStandard = true;
            this.SchoolGeneralTotalTextBox.ApplyParentFocusColor = true;
            this.SchoolGeneralTotalTextBox.ApplyTimeFormat = false;
            this.SchoolGeneralTotalTextBox.BackColor = System.Drawing.Color.White;
            this.SchoolGeneralTotalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SchoolGeneralTotalTextBox.CFromatWihoutSymbol = false;
            this.SchoolGeneralTotalTextBox.CheckForEmpty = false;
            this.SchoolGeneralTotalTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SchoolGeneralTotalTextBox.Digits = -1;
            this.SchoolGeneralTotalTextBox.EmptyDecimalValue = false;
            this.SchoolGeneralTotalTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.SchoolGeneralTotalTextBox.ForeColor = System.Drawing.Color.Gray;
            this.SchoolGeneralTotalTextBox.IsEditable = false;
            this.SchoolGeneralTotalTextBox.IsQueryableFileld = true;
            this.SchoolGeneralTotalTextBox.Location = new System.Drawing.Point(16, 16);
            this.SchoolGeneralTotalTextBox.LockKeyPress = true;
            this.SchoolGeneralTotalTextBox.MaxLength = 50;
            this.SchoolGeneralTotalTextBox.Name = "SchoolGeneralTotalTextBox";
            this.SchoolGeneralTotalTextBox.PersistDefaultColor = false;
            this.SchoolGeneralTotalTextBox.Precision = 2;
            this.SchoolGeneralTotalTextBox.QueryingFileldName = "";
            this.SchoolGeneralTotalTextBox.ReadOnly = true;
            this.SchoolGeneralTotalTextBox.SetColorFlag = false;
            this.SchoolGeneralTotalTextBox.SetFocusColor = System.Drawing.Color.Empty;
            this.SchoolGeneralTotalTextBox.Size = new System.Drawing.Size(168, 16);
            this.SchoolGeneralTotalTextBox.SpecialCharacter = "%";
            this.SchoolGeneralTotalTextBox.TabIndex = 1;
            this.SchoolGeneralTotalTextBox.TabStop = false;
            this.SchoolGeneralTotalTextBox.TextCustomFormat = "";
            this.SchoolGeneralTotalTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.SchoolGeneralTotalTextBox.WholeInteger = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(-1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "School General Total:";
            // 
            // CompanyNumberPanel
            // 
            this.CompanyNumberPanel.BackColor = System.Drawing.Color.Transparent;
            this.CompanyNumberPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CompanyNumberPanel.Controls.Add(this.CountyGeneralTotalTextBox);
            this.CompanyNumberPanel.Controls.Add(this.CompanyLabel);
            this.CompanyNumberPanel.Location = new System.Drawing.Point(-1, -1);
            this.CompanyNumberPanel.Name = "CompanyNumberPanel";
            this.CompanyNumberPanel.Size = new System.Drawing.Size(190, 37);
            this.CompanyNumberPanel.TabIndex = 5;
            this.CompanyNumberPanel.TabStop = true;
            // 
            // CountyGeneralTotalTextBox
            // 
            this.CountyGeneralTotalTextBox.AllowClick = true;
            this.CountyGeneralTotalTextBox.AllowNegativeSign = false;
            this.CountyGeneralTotalTextBox.ApplyCFGFormat = false;
            this.CountyGeneralTotalTextBox.ApplyCurrencyFormat = false;
            this.CountyGeneralTotalTextBox.ApplyFocusColor = true;
            this.CountyGeneralTotalTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.CountyGeneralTotalTextBox.ApplyNegativeStandard = true;
            this.CountyGeneralTotalTextBox.ApplyParentFocusColor = true;
            this.CountyGeneralTotalTextBox.ApplyTimeFormat = false;
            this.CountyGeneralTotalTextBox.BackColor = System.Drawing.Color.White;
            this.CountyGeneralTotalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CountyGeneralTotalTextBox.CFromatWihoutSymbol = false;
            this.CountyGeneralTotalTextBox.CheckForEmpty = false;
            this.CountyGeneralTotalTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CountyGeneralTotalTextBox.Digits = -1;
            this.CountyGeneralTotalTextBox.EmptyDecimalValue = false;
            this.CountyGeneralTotalTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.CountyGeneralTotalTextBox.ForeColor = System.Drawing.Color.Gray;
            this.CountyGeneralTotalTextBox.IsEditable = false;
            this.CountyGeneralTotalTextBox.IsQueryableFileld = true;
            this.CountyGeneralTotalTextBox.Location = new System.Drawing.Point(16, 16);
            this.CountyGeneralTotalTextBox.LockKeyPress = true;
            this.CountyGeneralTotalTextBox.MaxLength = 50;
            this.CountyGeneralTotalTextBox.Name = "CountyGeneralTotalTextBox";
            this.CountyGeneralTotalTextBox.PersistDefaultColor = false;
            this.CountyGeneralTotalTextBox.Precision = 2;
            this.CountyGeneralTotalTextBox.QueryingFileldName = "";
            this.CountyGeneralTotalTextBox.ReadOnly = true;
            this.CountyGeneralTotalTextBox.SetColorFlag = false;
            this.CountyGeneralTotalTextBox.SetFocusColor = System.Drawing.Color.Empty;
            this.CountyGeneralTotalTextBox.Size = new System.Drawing.Size(168, 16);
            this.CountyGeneralTotalTextBox.SpecialCharacter = "%";
            this.CountyGeneralTotalTextBox.TabIndex = 1;
            this.CountyGeneralTotalTextBox.TabStop = false;
            this.CountyGeneralTotalTextBox.TextCustomFormat = "";
            this.CountyGeneralTotalTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CountyGeneralTotalTextBox.WholeInteger = false;
            // 
            // CompanyLabel
            // 
            this.CompanyLabel.AutoSize = true;
            this.CompanyLabel.BackColor = System.Drawing.Color.Transparent;
            this.CompanyLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.CompanyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CompanyLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CompanyLabel.Location = new System.Drawing.Point(-1, 0);
            this.CompanyLabel.Name = "CompanyLabel";
            this.CompanyLabel.Size = new System.Drawing.Size(124, 14);
            this.CompanyLabel.TabIndex = 0;
            this.CompanyLabel.Text = "County General Total:";
            // 
            // CentralAssessedPictureBox
            // 
            this.CentralAssessedPictureBox.BackColor = System.Drawing.Color.White;
            this.CentralAssessedPictureBox.Location = new System.Drawing.Point(757, 0);
            this.CentralAssessedPictureBox.Name = "CentralAssessedPictureBox";
            this.CentralAssessedPictureBox.Size = new System.Drawing.Size(45, 173);
            this.CentralAssessedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CentralAssessedPictureBox.TabIndex = 165;
            this.CentralAssessedPictureBox.TabStop = false;
            this.CentralAssessedPictureBox.Click += new System.EventHandler(this.CentralAssessedPictureBox_Click);
            this.CentralAssessedPictureBox.MouseHover += new System.EventHandler(this.CentralAssessedPictureBox_MouseHover);
            // 
            // F35081
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.CentralAssessedPictureBox);
            this.Name = "F35081";
            this.Size = new System.Drawing.Size(802, 180);
            this.Load += new System.EventHandler(this.F35081_Load);
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CentralAssessedGrid)).EndInit();
            this.LowerPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.CompanyNumberPanel.ResumeLayout(false);
            this.CompanyNumberPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CentralAssessedPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid CentralAssessedGrid;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.PictureBox CentralAssessedPictureBox;
        private System.Windows.Forms.ToolTip CentralAssessedOwnerToolTip;
        private System.Windows.Forms.Panel LowerPanel;
        private System.Windows.Forms.Panel panel1;
        private TerraScan.UI.Controls.TerraScanTextBox SchoolGeneralTotalTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel CompanyNumberPanel;
        private TerraScan.UI.Controls.TerraScanTextBox CountyGeneralTotalTextBox;
        private System.Windows.Forms.Label CompanyLabel;
        private System.Windows.Forms.Panel panel3;
        private TerraScan.UI.Controls.TerraScanTextBox terraScanTextBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private TerraScan.UI.Controls.TerraScanTextBox SchoolBondTotalTextBox;
        private System.Windows.Forms.Label label2;

    }
}
