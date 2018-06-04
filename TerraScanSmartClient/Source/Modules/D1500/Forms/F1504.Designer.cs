namespace D1500
{
    partial class F1504
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1504));
            this.LinePanel = new System.Windows.Forms.Panel();
            this.LineTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.LineLabel = new System.Windows.Forms.Label();
            this.ObjectPanel = new System.Windows.Forms.Panel();
            this.ObjectTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.ObjectLabel = new System.Windows.Forms.Label();
            this.BarsPanel = new System.Windows.Forms.Panel();
            this.BarsTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.BarsLabel = new System.Windows.Forms.Label();
            this.FunctionPanel = new System.Windows.Forms.Panel();
            this.FunctionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.FunctionLabel = new System.Windows.Forms.Label();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.DescriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.SubFundPanel = new System.Windows.Forms.Panel();
            this.SubFundCombo = new TerraScan.UI.Controls.TerraScanComboBox();
            this.ActiveLabel = new System.Windows.Forms.Label();
            this.RollYearPanel = new System.Windows.Forms.Panel();
            this.RollYearTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.RollYearLabel = new System.Windows.Forms.Label();
            this.CopyAccountCreateButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CopyAccounttCloseButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormNoLabel = new System.Windows.Forms.Label();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.LinePanel.SuspendLayout();
            this.ObjectPanel.SuspendLayout();
            this.BarsPanel.SuspendLayout();
            this.FunctionPanel.SuspendLayout();
            this.DescriptionPanel.SuspendLayout();
            this.SubFundPanel.SuspendLayout();
            this.RollYearPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LinePanel
            // 
            this.LinePanel.BackColor = System.Drawing.Color.Transparent;
            this.LinePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LinePanel.Controls.Add(this.LineTextBox);
            this.LinePanel.Controls.Add(this.LineLabel);
            this.LinePanel.Location = new System.Drawing.Point(406, 52);
            this.LinePanel.Name = "LinePanel";
            this.LinePanel.Size = new System.Drawing.Size(121, 43);
            this.LinePanel.TabIndex = 6;
            this.LinePanel.TabStop = true;
            // 
            // LineTextBox
            // 
            this.LineTextBox.AllowClick = true;
            this.LineTextBox.AllowNegativeSign = false;
            this.LineTextBox.ApplyCFGFormat = false;
            this.LineTextBox.ApplyCurrencyFormat = false;
            this.LineTextBox.ApplyFocusColor = true;
            this.LineTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.LineTextBox.ApplyNegativeStandard = true;
            this.LineTextBox.ApplyParentFocusColor = true;
            this.LineTextBox.ApplyTimeFormat = false;
            this.LineTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.LineTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.LineTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.LineTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LineTextBox.CFromatWihoutSymbol = false;
            this.LineTextBox.CheckForEmpty = true;
            this.LineTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.LineTextBox.Digits = -1;
            this.LineTextBox.EmptyDecimalValue = false;
            this.LineTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.LineTextBox.ForeColor = System.Drawing.Color.Black;
            this.LineTextBox.IsEditable = true;
            this.LineTextBox.IsQueryableFileld = true;
            this.LineTextBox.Location = new System.Drawing.Point(12, 17);
            this.LineTextBox.LockKeyPress = false;
            this.LineTextBox.MaxLength = 50;
            this.LineTextBox.Name = "LineTextBox";
            this.LineTextBox.PersistDefaultColor = false;
            this.LineTextBox.Precision = 2;
            this.LineTextBox.QueryingFileldName = "";
            this.LineTextBox.SetColorFlag = false;
            this.LineTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.LineTextBox.Size = new System.Drawing.Size(102, 16);
            this.LineTextBox.SpecialCharacter = "%";
            this.LineTextBox.TabIndex = 23;
            this.LineTextBox.TextCustomFormat = "0.00 %";
            this.LineTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.LineTextBox.WholeInteger = false;
            this.LineTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // LineLabel
            // 
            this.LineLabel.AutoSize = true;
            this.LineLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.LineLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.LineLabel.Location = new System.Drawing.Point(0, 0);
            this.LineLabel.Name = "LineLabel";
            this.LineLabel.Size = new System.Drawing.Size(34, 14);
            this.LineLabel.TabIndex = 0;
            this.LineLabel.Text = "Line:";
            this.LineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ObjectPanel
            // 
            this.ObjectPanel.BackColor = System.Drawing.Color.Transparent;
            this.ObjectPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ObjectPanel.Controls.Add(this.ObjectTextBox);
            this.ObjectPanel.Controls.Add(this.ObjectLabel);
            this.ObjectPanel.Location = new System.Drawing.Point(272, 52);
            this.ObjectPanel.Name = "ObjectPanel";
            this.ObjectPanel.Size = new System.Drawing.Size(135, 43);
            this.ObjectPanel.TabIndex = 5;
            this.ObjectPanel.TabStop = true;
            // 
            // ObjectTextBox
            // 
            this.ObjectTextBox.AllowClick = true;
            this.ObjectTextBox.AllowNegativeSign = false;
            this.ObjectTextBox.ApplyCFGFormat = false;
            this.ObjectTextBox.ApplyCurrencyFormat = false;
            this.ObjectTextBox.ApplyFocusColor = true;
            this.ObjectTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.ObjectTextBox.ApplyNegativeStandard = true;
            this.ObjectTextBox.ApplyParentFocusColor = true;
            this.ObjectTextBox.ApplyTimeFormat = false;
            this.ObjectTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ObjectTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ObjectTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.ObjectTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ObjectTextBox.CFromatWihoutSymbol = false;
            this.ObjectTextBox.CheckForEmpty = true;
            this.ObjectTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ObjectTextBox.Digits = -1;
            this.ObjectTextBox.EmptyDecimalValue = false;
            this.ObjectTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.ObjectTextBox.ForeColor = System.Drawing.Color.Black;
            this.ObjectTextBox.IsEditable = true;
            this.ObjectTextBox.IsQueryableFileld = true;
            this.ObjectTextBox.Location = new System.Drawing.Point(13, 17);
            this.ObjectTextBox.LockKeyPress = false;
            this.ObjectTextBox.MaxLength = 50;
            this.ObjectTextBox.Name = "ObjectTextBox";
            this.ObjectTextBox.PersistDefaultColor = false;
            this.ObjectTextBox.Precision = 2;
            this.ObjectTextBox.QueryingFileldName = "";
            this.ObjectTextBox.SetColorFlag = false;
            this.ObjectTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ObjectTextBox.Size = new System.Drawing.Size(116, 16);
            this.ObjectTextBox.SpecialCharacter = "%";
            this.ObjectTextBox.TabIndex = 23;
            this.ObjectTextBox.TextCustomFormat = "0.00 %";
            this.ObjectTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.ObjectTextBox.WholeInteger = false;
            this.ObjectTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // ObjectLabel
            // 
            this.ObjectLabel.AutoSize = true;
            this.ObjectLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.ObjectLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ObjectLabel.Location = new System.Drawing.Point(0, 0);
            this.ObjectLabel.Name = "ObjectLabel";
            this.ObjectLabel.Size = new System.Drawing.Size(45, 14);
            this.ObjectLabel.TabIndex = 0;
            this.ObjectLabel.Text = "Object:";
            this.ObjectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BarsPanel
            // 
            this.BarsPanel.BackColor = System.Drawing.Color.Transparent;
            this.BarsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BarsPanel.Controls.Add(this.BarsTextBox);
            this.BarsPanel.Controls.Add(this.BarsLabel);
            this.BarsPanel.Location = new System.Drawing.Point(144, 52);
            this.BarsPanel.Name = "BarsPanel";
            this.BarsPanel.Size = new System.Drawing.Size(129, 43);
            this.BarsPanel.TabIndex = 4;
            this.BarsPanel.TabStop = true;
            // 
            // BarsTextBox
            // 
            this.BarsTextBox.AllowClick = true;
            this.BarsTextBox.AllowNegativeSign = false;
            this.BarsTextBox.ApplyCFGFormat = false;
            this.BarsTextBox.ApplyCurrencyFormat = false;
            this.BarsTextBox.ApplyFocusColor = true;
            this.BarsTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.BarsTextBox.ApplyNegativeStandard = true;
            this.BarsTextBox.ApplyParentFocusColor = true;
            this.BarsTextBox.ApplyTimeFormat = false;
            this.BarsTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.BarsTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.BarsTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.BarsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BarsTextBox.CFromatWihoutSymbol = false;
            this.BarsTextBox.CheckForEmpty = true;
            this.BarsTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.BarsTextBox.Digits = -1;
            this.BarsTextBox.EmptyDecimalValue = false;
            this.BarsTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.BarsTextBox.ForeColor = System.Drawing.Color.Black;
            this.BarsTextBox.IsEditable = true;
            this.BarsTextBox.IsQueryableFileld = true;
            this.BarsTextBox.Location = new System.Drawing.Point(13, 18);
            this.BarsTextBox.LockKeyPress = false;
            this.BarsTextBox.MaxLength = 50;
            this.BarsTextBox.Name = "BarsTextBox";
            this.BarsTextBox.PersistDefaultColor = false;
            this.BarsTextBox.Precision = 2;
            this.BarsTextBox.QueryingFileldName = "";
            this.BarsTextBox.SetColorFlag = false;
            this.BarsTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.BarsTextBox.Size = new System.Drawing.Size(110, 16);
            this.BarsTextBox.SpecialCharacter = "%";
            this.BarsTextBox.TabIndex = 23;
            this.BarsTextBox.TextCustomFormat = "0.00 %";
            this.BarsTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.BarsTextBox.WholeInteger = false;
            this.BarsTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // BarsLabel
            // 
            this.BarsLabel.AutoSize = true;
            this.BarsLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.BarsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.BarsLabel.Location = new System.Drawing.Point(0, 0);
            this.BarsLabel.Name = "BarsLabel";
            this.BarsLabel.Size = new System.Drawing.Size(35, 14);
            this.BarsLabel.TabIndex = 0;
            this.BarsLabel.Text = "Bars:";
            this.BarsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FunctionPanel
            // 
            this.FunctionPanel.BackColor = System.Drawing.Color.Transparent;
            this.FunctionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FunctionPanel.Controls.Add(this.FunctionTextBox);
            this.FunctionPanel.Controls.Add(this.FunctionLabel);
            this.FunctionPanel.Location = new System.Drawing.Point(9, 52);
            this.FunctionPanel.Name = "FunctionPanel";
            this.FunctionPanel.Size = new System.Drawing.Size(136, 43);
            this.FunctionPanel.TabIndex = 3;
            this.FunctionPanel.TabStop = true;
            // 
            // FunctionTextBox
            // 
            this.FunctionTextBox.AllowClick = true;
            this.FunctionTextBox.AllowNegativeSign = false;
            this.FunctionTextBox.ApplyCFGFormat = false;
            this.FunctionTextBox.ApplyCurrencyFormat = false;
            this.FunctionTextBox.ApplyFocusColor = true;
            this.FunctionTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.FunctionTextBox.ApplyNegativeStandard = true;
            this.FunctionTextBox.ApplyParentFocusColor = true;
            this.FunctionTextBox.ApplyTimeFormat = false;
            this.FunctionTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.FunctionTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.FunctionTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.FunctionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FunctionTextBox.CFromatWihoutSymbol = false;
            this.FunctionTextBox.CheckForEmpty = true;
            this.FunctionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FunctionTextBox.Digits = -1;
            this.FunctionTextBox.EmptyDecimalValue = false;
            this.FunctionTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.FunctionTextBox.ForeColor = System.Drawing.Color.Black;
            this.FunctionTextBox.IsEditable = true;
            this.FunctionTextBox.IsQueryableFileld = true;
            this.FunctionTextBox.Location = new System.Drawing.Point(13, 18);
            this.FunctionTextBox.LockKeyPress = false;
            this.FunctionTextBox.MaxLength = 50;
            this.FunctionTextBox.Name = "FunctionTextBox";
            this.FunctionTextBox.PersistDefaultColor = false;
            this.FunctionTextBox.Precision = 2;
            this.FunctionTextBox.QueryingFileldName = "";
            this.FunctionTextBox.SetColorFlag = false;
            this.FunctionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FunctionTextBox.Size = new System.Drawing.Size(116, 16);
            this.FunctionTextBox.SpecialCharacter = "%";
            this.FunctionTextBox.TabIndex = 16;
            this.FunctionTextBox.TextCustomFormat = "0.00 %";
            this.FunctionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.FunctionTextBox.WholeInteger = false;
            this.FunctionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // FunctionLabel
            // 
            this.FunctionLabel.AutoSize = true;
            this.FunctionLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.FunctionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.FunctionLabel.Location = new System.Drawing.Point(1, 0);
            this.FunctionLabel.Name = "FunctionLabel";
            this.FunctionLabel.Size = new System.Drawing.Size(57, 14);
            this.FunctionLabel.TabIndex = 0;
            this.FunctionLabel.Text = "Function:";
            this.FunctionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.BackColor = System.Drawing.Color.Transparent;
            this.DescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionPanel.Controls.Add(this.DescriptionTextBox);
            this.DescriptionPanel.Controls.Add(this.DescriptionLabel);
            this.DescriptionPanel.Location = new System.Drawing.Point(272, 10);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(255, 43);
            this.DescriptionPanel.TabIndex = 2;
            this.DescriptionPanel.TabStop = true;
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.AllowClick = true;
            this.DescriptionTextBox.AllowNegativeSign = false;
            this.DescriptionTextBox.ApplyCFGFormat = false;
            this.DescriptionTextBox.ApplyCurrencyFormat = false;
            this.DescriptionTextBox.ApplyFocusColor = true;
            this.DescriptionTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.DescriptionTextBox.ApplyNegativeStandard = true;
            this.DescriptionTextBox.ApplyParentFocusColor = true;
            this.DescriptionTextBox.ApplyTimeFormat = false;
            this.DescriptionTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.DescriptionTextBox.BackColor = System.Drawing.Color.White;
            this.DescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DescriptionTextBox.CFromatWihoutSymbol = false;
            this.DescriptionTextBox.CheckForEmpty = false;
            this.DescriptionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DescriptionTextBox.Digits = -1;
            this.DescriptionTextBox.EmptyDecimalValue = false;
            this.DescriptionTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.DescriptionTextBox.ForeColor = System.Drawing.Color.Black;
            this.DescriptionTextBox.IsEditable = true;
            this.DescriptionTextBox.IsQueryableFileld = true;
            this.DescriptionTextBox.Location = new System.Drawing.Point(13, 18);
            this.DescriptionTextBox.LockKeyPress = false;
            this.DescriptionTextBox.MaxLength = 100;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.PersistDefaultColor = false;
            this.DescriptionTextBox.Precision = 2;
            this.DescriptionTextBox.QueryingFileldName = "";
            this.DescriptionTextBox.SetColorFlag = false;
            this.DescriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescriptionTextBox.Size = new System.Drawing.Size(236, 16);
            this.DescriptionTextBox.SpecialCharacter = "%";
            this.DescriptionTextBox.TabIndex = 9;
            this.DescriptionTextBox.Tag = "";
            this.DescriptionTextBox.TextCustomFormat = "$#,##0.00";
            this.DescriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescriptionTextBox.WholeInteger = false;
            this.DescriptionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.DescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DescriptionLabel.Location = new System.Drawing.Point(0, 0);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(73, 14);
            this.DescriptionLabel.TabIndex = 0;
            this.DescriptionLabel.Text = "Description:";
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SubFundPanel
            // 
            this.SubFundPanel.BackColor = System.Drawing.Color.Transparent;
            this.SubFundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SubFundPanel.Controls.Add(this.SubFundCombo);
            this.SubFundPanel.Controls.Add(this.ActiveLabel);
            this.SubFundPanel.Location = new System.Drawing.Point(120, 10);
            this.SubFundPanel.Name = "SubFundPanel";
            this.SubFundPanel.Size = new System.Drawing.Size(153, 43);
            this.SubFundPanel.TabIndex = 1;
            this.SubFundPanel.TabStop = true;
            // 
            // SubFundCombo
            // 
            this.SubFundCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SubFundCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SubFundCombo.BackColor = System.Drawing.SystemColors.Window;
            this.SubFundCombo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SubFundCombo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubFundCombo.FormattingEnabled = true;
            this.SubFundCombo.Location = new System.Drawing.Point(10, 15);
            this.SubFundCombo.Name = "SubFundCombo";
            this.SubFundCombo.Size = new System.Drawing.Size(138, 24);
            this.SubFundCombo.TabIndex = 5;
            this.SubFundCombo.Validating += new System.ComponentModel.CancelEventHandler(this.SubFundCombo_Validating);
            this.SubFundCombo.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // ActiveLabel
            // 
            this.ActiveLabel.AutoSize = true;
            this.ActiveLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.ActiveLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ActiveLabel.Location = new System.Drawing.Point(0, 0);
            this.ActiveLabel.Name = "ActiveLabel";
            this.ActiveLabel.Size = new System.Drawing.Size(58, 14);
            this.ActiveLabel.TabIndex = 0;
            this.ActiveLabel.Text = "SubFund:";
            this.ActiveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RollYearPanel
            // 
            this.RollYearPanel.BackColor = System.Drawing.Color.Transparent;
            this.RollYearPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RollYearPanel.Controls.Add(this.RollYearTextBox);
            this.RollYearPanel.Controls.Add(this.RollYearLabel);
            this.RollYearPanel.Location = new System.Drawing.Point(9, 10);
            this.RollYearPanel.Name = "RollYearPanel";
            this.RollYearPanel.Size = new System.Drawing.Size(112, 43);
            this.RollYearPanel.TabIndex = 0;
            this.RollYearPanel.TabStop = true;
            // 
            // RollYearTextBox
            // 
            this.RollYearTextBox.AllowClick = true;
            this.RollYearTextBox.AllowNegativeSign = false;
            this.RollYearTextBox.ApplyCFGFormat = false;
            this.RollYearTextBox.ApplyCurrencyFormat = false;
            this.RollYearTextBox.ApplyFocusColor = true;
            this.RollYearTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.RollYearTextBox.ApplyNegativeStandard = true;
            this.RollYearTextBox.ApplyParentFocusColor = true;
            this.RollYearTextBox.ApplyTimeFormat = false;
            this.RollYearTextBox.BackColor = System.Drawing.Color.White;
            this.RollYearTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RollYearTextBox.CFromatWihoutSymbol = false;
            this.RollYearTextBox.CheckForEmpty = false;
            this.RollYearTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RollYearTextBox.Digits = -1;
            this.RollYearTextBox.EmptyDecimalValue = false;
            this.RollYearTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.RollYearTextBox.ForeColor = System.Drawing.Color.Black;
            this.RollYearTextBox.IsEditable = false;
            this.RollYearTextBox.IsQueryableFileld = false;
            this.RollYearTextBox.Location = new System.Drawing.Point(14, 18);
            this.RollYearTextBox.LockKeyPress = false;
            this.RollYearTextBox.MaxLength = 4;
            this.RollYearTextBox.Name = "RollYearTextBox";
            this.RollYearTextBox.PersistDefaultColor = false;
            this.RollYearTextBox.Precision = 2;
            this.RollYearTextBox.QueryingFileldName = "";
            this.RollYearTextBox.SetColorFlag = false;
            this.RollYearTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.RollYearTextBox.Size = new System.Drawing.Size(90, 16);
            this.RollYearTextBox.SpecialCharacter = "%";
            this.RollYearTextBox.TabIndex = 7;
            this.RollYearTextBox.TextCustomFormat = "$#,##0.00";
            this.RollYearTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Year;
            this.RollYearTextBox.WholeInteger = false;
            this.RollYearTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // RollYearLabel
            // 
            this.RollYearLabel.AutoSize = true;
            this.RollYearLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.RollYearLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.RollYearLabel.Location = new System.Drawing.Point(1, 0);
            this.RollYearLabel.Name = "RollYearLabel";
            this.RollYearLabel.Size = new System.Drawing.Size(58, 14);
            this.RollYearLabel.TabIndex = 0;
            this.RollYearLabel.Text = "Roll Year:";
            this.RollYearLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CopyAccountCreateButton
            // 
            this.CopyAccountCreateButton.ActualPermission = false;
            this.CopyAccountCreateButton.ApplyDisableBehaviour = false;
            this.CopyAccountCreateButton.AutoSize = true;
            this.CopyAccountCreateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CopyAccountCreateButton.BorderColor = System.Drawing.Color.Wheat;
            this.CopyAccountCreateButton.CommentPriority = false;
            this.CopyAccountCreateButton.EnableAutoPrint = false;
            this.CopyAccountCreateButton.Enabled = false;
            this.CopyAccountCreateButton.FilterStatus = false;
            this.CopyAccountCreateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CopyAccountCreateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyAccountCreateButton.FocusRectangleEnabled = true;
            this.CopyAccountCreateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyAccountCreateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CopyAccountCreateButton.ImageSelected = false;
            this.CopyAccountCreateButton.Location = new System.Drawing.Point(9, 101);
            this.CopyAccountCreateButton.Name = "CopyAccountCreateButton";
            this.CopyAccountCreateButton.NewPadding = 5;
            this.CopyAccountCreateButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.CopyAccountCreateButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CopyAccountCreateButton.Size = new System.Drawing.Size(98, 28);
            this.CopyAccountCreateButton.StatusIndicator = false;
            this.CopyAccountCreateButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CopyAccountCreateButton.StatusOffText = null;
            this.CopyAccountCreateButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CopyAccountCreateButton.StatusOnText = null;
            this.CopyAccountCreateButton.TabIndex = 7;
            this.CopyAccountCreateButton.TabStop = false;
            this.CopyAccountCreateButton.Text = "Create";
            this.CopyAccountCreateButton.UseVisualStyleBackColor = false;
            this.CopyAccountCreateButton.Click += new System.EventHandler(this.CopyAccountCreateButton_Click);
            // 
            // CopyAccounttCloseButton
            // 
            this.CopyAccounttCloseButton.ActualPermission = false;
            this.CopyAccounttCloseButton.ApplyDisableBehaviour = false;
            this.CopyAccounttCloseButton.AutoSize = true;
            this.CopyAccounttCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CopyAccounttCloseButton.BorderColor = System.Drawing.Color.Wheat;
            this.CopyAccounttCloseButton.CommentPriority = false;
            this.CopyAccounttCloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CopyAccounttCloseButton.EnableAutoPrint = false;
            this.CopyAccounttCloseButton.FilterStatus = false;
            this.CopyAccounttCloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CopyAccounttCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyAccounttCloseButton.FocusRectangleEnabled = true;
            this.CopyAccounttCloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyAccounttCloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CopyAccounttCloseButton.ImageSelected = false;
            this.CopyAccounttCloseButton.Location = new System.Drawing.Point(429, 101);
            this.CopyAccounttCloseButton.Name = "CopyAccounttCloseButton";
            this.CopyAccounttCloseButton.NewPadding = 5;
            this.CopyAccounttCloseButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CopyAccounttCloseButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CopyAccounttCloseButton.Size = new System.Drawing.Size(98, 28);
            this.CopyAccounttCloseButton.StatusIndicator = false;
            this.CopyAccounttCloseButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CopyAccounttCloseButton.StatusOffText = null;
            this.CopyAccounttCloseButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CopyAccounttCloseButton.StatusOnText = null;
            this.CopyAccounttCloseButton.TabIndex = 8;
            this.CopyAccounttCloseButton.TabStop = false;
            this.CopyAccounttCloseButton.Text = "Close";
            this.CopyAccounttCloseButton.UseVisualStyleBackColor = false;
            this.CopyAccounttCloseButton.Click += new System.EventHandler(this.CopyAccounttCloseButton_Click);
            // 
            // FormNoLabel
            // 
            this.FormNoLabel.AutoSize = true;
            this.FormNoLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormNoLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.FormNoLabel.Location = new System.Drawing.Point(7, 138);
            this.FormNoLabel.Name = "FormNoLabel";
            this.FormNoLabel.Size = new System.Drawing.Size(35, 15);
            this.FormNoLabel.TabIndex = 9;
            this.FormNoLabel.Text = "1504";
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(9, 136);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(519, 2);
            this.FormLinePanel.TabIndex = 166;
            // 
            // F1504
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.CopyAccounttCloseButton;
            this.ClientSize = new System.Drawing.Size(536, 154);
            this.Controls.Add(this.FormNoLabel);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.CopyAccounttCloseButton);
            this.Controls.Add(this.CopyAccountCreateButton);
            this.Controls.Add(this.LinePanel);
            this.Controls.Add(this.ObjectPanel);
            this.Controls.Add(this.BarsPanel);
            this.Controls.Add(this.FunctionPanel);
            this.Controls.Add(this.DescriptionPanel);
            this.Controls.Add(this.SubFundPanel);
            this.Controls.Add(this.RollYearPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F1504";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - Copy Account";
            this.Load += new System.EventHandler(this.F1504_Load);
            this.LinePanel.ResumeLayout(false);
            this.LinePanel.PerformLayout();
            this.ObjectPanel.ResumeLayout(false);
            this.ObjectPanel.PerformLayout();
            this.BarsPanel.ResumeLayout(false);
            this.BarsPanel.PerformLayout();
            this.FunctionPanel.ResumeLayout(false);
            this.FunctionPanel.PerformLayout();
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.SubFundPanel.ResumeLayout(false);
            this.SubFundPanel.PerformLayout();
            this.RollYearPanel.ResumeLayout(false);
            this.RollYearPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel LinePanel;
        private System.Windows.Forms.Label LineLabel;
        private System.Windows.Forms.Panel ObjectPanel;
        private System.Windows.Forms.Label ObjectLabel;
        private System.Windows.Forms.Panel BarsPanel;
        private System.Windows.Forms.Label BarsLabel;
        private System.Windows.Forms.Panel FunctionPanel;
        private System.Windows.Forms.Label FunctionLabel;
        private System.Windows.Forms.Panel DescriptionPanel;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Panel SubFundPanel;
        private TerraScan.UI.Controls.TerraScanComboBox SubFundCombo;
        private System.Windows.Forms.Label ActiveLabel;
        private System.Windows.Forms.Panel RollYearPanel;
        private System.Windows.Forms.Label RollYearLabel;
        private TerraScan.UI.Controls.TerraScanButton CopyAccountCreateButton;
        private TerraScan.UI.Controls.TerraScanButton CopyAccounttCloseButton;
        private System.Windows.Forms.Label FormNoLabel;
        private System.Windows.Forms.Panel FormLinePanel;
        private TerraScan.UI.Controls.TerraScanTextBox RollYearTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox DescriptionTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox FunctionTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox ObjectTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox BarsTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox LineTextBox;
    }
}