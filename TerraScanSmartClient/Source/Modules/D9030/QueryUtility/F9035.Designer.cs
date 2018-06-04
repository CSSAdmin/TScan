namespace D9030
{
    partial class F9035
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.UltraWinEditors.EditorButton editorButton1 = new Infragistics.Win.UltraWinEditors.EditorButton();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9035));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.FormatTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.FormulaTextBox = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StatementIDLable = new System.Windows.Forms.Label();
            this.FieldNameTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TypeCombo = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.RightRadioButton = new System.Windows.Forms.RadioButton();
            this.CenterRadioButton = new System.Windows.Forms.RadioButton();
            this.LeftRadioButton = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.HelpLink = new System.Windows.Forms.LinkLabel();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.F9035OKButton = new TerraScan.UI.Controls.TerraScanButton();
            this.F9035CancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AddNewFieldMenuStrip = new System.Windows.Forms.MenuStrip();
            this.HelpStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewColCalcManager = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(this.components);
            this.MainPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormulaTextBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TypeCombo)).BeginInit();
            this.panel5.SuspendLayout();
            this.AddNewFieldMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewColCalcManager)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPanel.Controls.Add(this.panel4);
            this.MainPanel.Controls.Add(this.panel3);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Controls.Add(this.panel2);
            this.MainPanel.Location = new System.Drawing.Point(12, 12);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(283, 194);
            this.MainPanel.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.FormatTextBox);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(-5, 114);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(288, 39);
            this.panel4.TabIndex = 41;
            // 
            // FormatTextBox
            // 
            this.FormatTextBox.AllowClick = true;
            this.FormatTextBox.AllowNegativeSign = false;
            this.FormatTextBox.ApplyCFGFormat = false;
            this.FormatTextBox.ApplyCurrencyFormat = false;
            this.FormatTextBox.ApplyFocusColor = true;
            this.FormatTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.FormatTextBox.ApplyNegativeStandard = true;
            this.FormatTextBox.ApplyParentFocusColor = true;
            this.FormatTextBox.ApplyTimeFormat = false;
            this.FormatTextBox.BackColor = System.Drawing.Color.White;
            this.FormatTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FormatTextBox.CFromatWihoutSymbol = false;
            this.FormatTextBox.CheckForEmpty = false;
            this.FormatTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FormatTextBox.Digits = -1;
            this.FormatTextBox.EmptyDecimalValue = false;
            this.FormatTextBox.Font = new System.Drawing.Font("Arial", 8.59F, System.Drawing.FontStyle.Bold);
            this.FormatTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormatTextBox.IsEditable = false;
            this.FormatTextBox.IsQueryableFileld = true;
            this.FormatTextBox.Location = new System.Drawing.Point(38, 16);
            this.FormatTextBox.LockKeyPress = true;
            this.FormatTextBox.MaxLength = 25;
            this.FormatTextBox.Name = "FormatTextBox";
            this.FormatTextBox.PersistDefaultColor = false;
            this.FormatTextBox.Precision = 2;
            this.FormatTextBox.QueryingFileldName = "";
            this.FormatTextBox.ReadOnly = true;
            this.FormatTextBox.SetColorFlag = false;
            this.FormatTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FormatTextBox.Size = new System.Drawing.Size(227, 14);
            this.FormatTextBox.SpecialCharacter = "%";
            this.FormatTextBox.TabIndex = 38;
            this.FormatTextBox.TabStop = false;
            this.FormatTextBox.TextCustomFormat = "$#,##0.00";
            this.FormatTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.FormatTextBox.WholeInteger = false;
            this.FormatTextBox.TextChanged += new System.EventHandler(this.FormatTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "Format :";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.FormulaTextBox);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(-1, 75);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(288, 40);
            this.panel3.TabIndex = 40;
            // 
            // FormulaTextBox
            // 
            this.FormulaTextBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            editorButton1.Text = "...";
            this.FormulaTextBox.ButtonsRight.Add(editorButton1);
            this.FormulaTextBox.Font = new System.Drawing.Font("Arial", 8.59F, System.Drawing.FontStyle.Bold);
            this.FormulaTextBox.Location = new System.Drawing.Point(35, 14);
            this.FormulaTextBox.MaxLength = 75;
            this.FormulaTextBox.Name = "FormulaTextBox";
            this.FormulaTextBox.Size = new System.Drawing.Size(227, 18);
            this.FormulaTextBox.TabIndex = 2;
            this.FormulaTextBox.TextChanged += new System.EventHandler(this.FormulaTextBox_TextChanged);
            this.FormulaTextBox.EditorButtonClick += new Infragistics.Win.UltraWinEditors.EditorButtonEventHandler(this.FormulaTextBox_EditorButtonClick);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Formula:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.StatementIDLable);
            this.panel1.Controls.Add(this.FieldNameTextBox);
            this.panel1.Location = new System.Drawing.Point(-5, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 40);
            this.panel1.TabIndex = 38;
            // 
            // StatementIDLable
            // 
            this.StatementIDLable.BackColor = System.Drawing.Color.Transparent;
            this.StatementIDLable.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatementIDLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.StatementIDLable.Location = new System.Drawing.Point(3, -1);
            this.StatementIDLable.Name = "StatementIDLable";
            this.StatementIDLable.Size = new System.Drawing.Size(73, 14);
            this.StatementIDLable.TabIndex = 1;
            this.StatementIDLable.Text = "Field Name:";
            // 
            // FieldNameTextBox
            // 
            this.FieldNameTextBox.AllowClick = true;
            this.FieldNameTextBox.AllowNegativeSign = false;
            this.FieldNameTextBox.ApplyCFGFormat = false;
            this.FieldNameTextBox.ApplyCurrencyFormat = false;
            this.FieldNameTextBox.ApplyFocusColor = true;
            this.FieldNameTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.FieldNameTextBox.ApplyNegativeStandard = true;
            this.FieldNameTextBox.ApplyParentFocusColor = true;
            this.FieldNameTextBox.ApplyTimeFormat = false;
            this.FieldNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FieldNameTextBox.CFromatWihoutSymbol = false;
            this.FieldNameTextBox.CheckForEmpty = false;
            this.FieldNameTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNameTextBox.Digits = -1;
            this.FieldNameTextBox.EmptyDecimalValue = false;
            this.FieldNameTextBox.Font = new System.Drawing.Font("Arial", 8.59F, System.Drawing.FontStyle.Bold);
            this.FieldNameTextBox.IsEditable = true;
            this.FieldNameTextBox.IsQueryableFileld = true;
            this.FieldNameTextBox.Location = new System.Drawing.Point(39, 15);
            this.FieldNameTextBox.LockKeyPress = false;
            this.FieldNameTextBox.MaxLength = 75;
            this.FieldNameTextBox.Name = "FieldNameTextBox";
            this.FieldNameTextBox.PersistDefaultColor = false;
            this.FieldNameTextBox.Precision = 2;
            this.FieldNameTextBox.QueryingFileldName = "";
            this.FieldNameTextBox.SetColorFlag = false;
            this.FieldNameTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FieldNameTextBox.Size = new System.Drawing.Size(227, 14);
            this.FieldNameTextBox.SpecialCharacter = "%";
            this.FieldNameTextBox.TabIndex = 37;
            this.FieldNameTextBox.TextCustomFormat = "$#,##0.00";
            this.FieldNameTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.FieldNameTextBox.WholeInteger = false;
            this.FieldNameTextBox.TextChanged += new System.EventHandler(this.FieldNameTextBox_TextChanged);
            this.FieldNameTextBox.Leave += new System.EventHandler(this.FieldNameTextBox_Leave);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.TypeCombo);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(-2, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(288, 40);
            this.panel2.TabIndex = 39;
            // 
            // TypeCombo
            // 
            this.TypeCombo.DropDownListWidth = 250;
            this.TypeCombo.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            this.TypeCombo.Font = new System.Drawing.Font("Arial", 8.59F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeCombo.Location = new System.Drawing.Point(36, 8);
            this.TypeCombo.Name = "TypeCombo";
            this.TypeCombo.Size = new System.Drawing.Size(227, 22);
            this.TypeCombo.TabIndex = 2;
            this.TypeCombo.SelectionChangeCommitted += new System.EventHandler(this.TypeCombo_SelectionChangeCommitted);
            this.TypeCombo.Leave += new System.EventHandler(this.TypeCombo_Leave);
            this.TypeCombo.Enter += new System.EventHandler(this.TypeCombo_Enter);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label1.Location = new System.Drawing.Point(0, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type:";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.RightRadioButton);
            this.panel5.Controls.Add(this.CenterRadioButton);
            this.panel5.Controls.Add(this.LeftRadioButton);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Location = new System.Drawing.Point(12, 165);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(283, 41);
            this.panel5.TabIndex = 42;
            // 
            // RightRadioButton
            // 
            this.RightRadioButton.AutoSize = true;
            this.RightRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightRadioButton.Location = new System.Drawing.Point(208, 15);
            this.RightRadioButton.Name = "RightRadioButton";
            this.RightRadioButton.Size = new System.Drawing.Size(54, 19);
            this.RightRadioButton.TabIndex = 4;
            this.RightRadioButton.TabStop = true;
            this.RightRadioButton.Text = "Right";
            this.RightRadioButton.UseVisualStyleBackColor = true;
            this.RightRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // CenterRadioButton
            // 
            this.CenterRadioButton.AutoSize = true;
            this.CenterRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CenterRadioButton.Location = new System.Drawing.Point(119, 15);
            this.CenterRadioButton.Name = "CenterRadioButton";
            this.CenterRadioButton.Size = new System.Drawing.Size(63, 19);
            this.CenterRadioButton.TabIndex = 3;
            this.CenterRadioButton.TabStop = true;
            this.CenterRadioButton.Text = "Center";
            this.CenterRadioButton.UseVisualStyleBackColor = true;
            this.CenterRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // LeftRadioButton
            // 
            this.LeftRadioButton.AutoSize = true;
            this.LeftRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftRadioButton.Location = new System.Drawing.Point(18, 15);
            this.LeftRadioButton.Name = "LeftRadioButton";
            this.LeftRadioButton.Size = new System.Drawing.Size(95, 19);
            this.LeftRadioButton.TabIndex = 2;
            this.LeftRadioButton.TabStop = true;
            this.LeftRadioButton.Text = "Left(Default)";
            this.LeftRadioButton.UseVisualStyleBackColor = true;
            this.LeftRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label4.Location = new System.Drawing.Point(-1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Alignment:";
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AccessibleDescription = "0";
            this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(10, 262);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(35, 15);
            this.FormIDLabel.TabIndex = 208;
            this.FormIDLabel.Text = "9035";
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpLink.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLink.Location = new System.Drawing.Point(189, 261);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(106, 15);
            this.HelpLink.TabIndex = 207;
            this.HelpLink.TabStop = true;
            this.HelpLink.Text = "Help";
            this.HelpLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.HelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(12, 256);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(283, 2);
            this.FormLinePanel.TabIndex = 206;
            // 
            // F9035OKButton
            // 
            this.F9035OKButton.ActualPermission = false;
            this.F9035OKButton.ApplyDisableBehaviour = false;
            this.F9035OKButton.AutoSize = true;
            this.F9035OKButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.F9035OKButton.BorderColor = System.Drawing.Color.Wheat;
            this.F9035OKButton.CommentPriority = false;
            this.F9035OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.F9035OKButton.EnableAutoPrint = false;
            this.F9035OKButton.FilterStatus = false;
            this.F9035OKButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.F9035OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.F9035OKButton.FocusRectangleEnabled = true;
            this.F9035OKButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.F9035OKButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.F9035OKButton.ImageSelected = false;
            this.F9035OKButton.Location = new System.Drawing.Point(11, 216);
            this.F9035OKButton.Name = "F9035OKButton";
            this.F9035OKButton.NewPadding = 5;
            this.F9035OKButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.F9035OKButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.F9035OKButton.Size = new System.Drawing.Size(110, 30);
            this.F9035OKButton.StatusIndicator = false;
            this.F9035OKButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.F9035OKButton.StatusOffText = null;
            this.F9035OKButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.F9035OKButton.StatusOnText = null;
            this.F9035OKButton.TabIndex = 209;
            this.F9035OKButton.TabStop = false;
            this.F9035OKButton.Text = "OK";
            this.F9035OKButton.UseVisualStyleBackColor = false;
            this.F9035OKButton.Click += new System.EventHandler(this.F9035OKButton_Click);
            // 
            // F9035CancelButton
            // 
            this.F9035CancelButton.ActualPermission = false;
            this.F9035CancelButton.ApplyDisableBehaviour = false;
            this.F9035CancelButton.AutoSize = true;
            this.F9035CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.F9035CancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.F9035CancelButton.CommentPriority = false;
            this.F9035CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.F9035CancelButton.EnableAutoPrint = false;
            this.F9035CancelButton.FilterStatus = false;
            this.F9035CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.F9035CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.F9035CancelButton.FocusRectangleEnabled = true;
            this.F9035CancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.F9035CancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.F9035CancelButton.ImageSelected = false;
            this.F9035CancelButton.Location = new System.Drawing.Point(185, 216);
            this.F9035CancelButton.Name = "F9035CancelButton";
            this.F9035CancelButton.NewPadding = 5;
            this.F9035CancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.F9035CancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.F9035CancelButton.Size = new System.Drawing.Size(110, 30);
            this.F9035CancelButton.StatusIndicator = false;
            this.F9035CancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.F9035CancelButton.StatusOffText = null;
            this.F9035CancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.F9035CancelButton.StatusOnText = null;
            this.F9035CancelButton.TabIndex = 210;
            this.F9035CancelButton.TabStop = false;
            this.F9035CancelButton.Text = "Cancel";
            this.F9035CancelButton.UseVisualStyleBackColor = false;
            this.F9035CancelButton.Click += new System.EventHandler(this.F9035CancelButton_Click);
            // 
            // AddNewFieldMenuStrip
            // 
            this.AddNewFieldMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpStripMenuItem});
            this.AddNewFieldMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.AddNewFieldMenuStrip.Name = "AddNewFieldMenuStrip";
            this.AddNewFieldMenuStrip.Size = new System.Drawing.Size(309, 24);
            this.AddNewFieldMenuStrip.TabIndex = 211;
            this.AddNewFieldMenuStrip.Text = "AddNewFieldMenuStrip";
            this.AddNewFieldMenuStrip.Visible = false;
            // 
            // HelpStripMenuItem
            // 
            this.HelpStripMenuItem.Name = "HelpStripMenuItem";
            this.HelpStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.HelpStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.HelpStripMenuItem.Text = "HelpStripMenuItem";
            this.HelpStripMenuItem.Click += new System.EventHandler(this.HelpStripMenuItem_Click);
            // 
            // NewColCalcManager
            // 
            this.NewColCalcManager.ContainingControl = this;
            this.NewColCalcManager.FormulaSyntaxError += new Infragistics.Win.UltraWinCalcManager.FormulaSyntaxErrorEventHandler(this.NewColCalcManager_FormulaSyntaxError);
            // 
            // F9035
            // 
            this.AcceptButton = this.F9035OKButton;
            this.AccessibleName = "Add New Field";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.F9035CancelButton;
            this.ClientSize = new System.Drawing.Size(309, 290);
            this.Controls.Add(this.AddNewFieldMenuStrip);
            this.Controls.Add(this.F9035CancelButton);
            this.Controls.Add(this.F9035OKButton);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(315, 322);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(315, 322);
            this.Name = "F9035";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "9035";
            this.Text = "Add New Field";
            this.Load += new System.EventHandler(this.F9035_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F9035_FormClosing);
            this.MainPanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormulaTextBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TypeCombo)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.AddNewFieldMenuStrip.ResumeLayout(false);
            this.AddNewFieldMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewColCalcManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label StatementIDLable;
        private TerraScan.UI.Controls.TerraScanTextBox FieldNameTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor FormulaTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton RightRadioButton;
        private System.Windows.Forms.RadioButton CenterRadioButton;
        private System.Windows.Forms.RadioButton LeftRadioButton;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.LinkLabel HelpLink;
        private System.Windows.Forms.Panel FormLinePanel;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor TypeCombo;
        private TerraScan.UI.Controls.TerraScanTextBox FormatTextBox;
        private TerraScan.UI.Controls.TerraScanButton F9035OKButton;
        private TerraScan.UI.Controls.TerraScanButton F9035CancelButton;
        private System.Windows.Forms.MenuStrip AddNewFieldMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem HelpStripMenuItem;
        private Infragistics.Win.UltraWinCalcManager.UltraCalcManager NewColCalcManager;
    }
}