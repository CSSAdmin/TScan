namespace D34110
{
    partial class F39110
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RollYearPanel = new System.Windows.Forms.Panel();
            this.RollYearTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.RollYearLabel = new System.Windows.Forms.Label();
            this.AgEqualPanel = new System.Windows.Forms.Panel();
            this.AgEqualLabel = new System.Windows.Forms.Label();
            this.AgEqualTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.NonAgEqualPanel = new System.Windows.Forms.Panel();
            this.NonAgEqualLabel = new System.Windows.Forms.Label();
            this.NonAgEqualTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CountyPanel = new System.Windows.Forms.Panel();
            this.CountyTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CountyLabel = new System.Windows.Forms.Label();
            this.CropPanel = new System.Windows.Forms.Panel();
            this.CropTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CropLabel = new System.Windows.Forms.Label();
            this.NonCropPanel = new System.Windows.Forms.Panel();
            this.NonCropTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.NonCropLabel = new System.Windows.Forms.Label();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.topDollarToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            this.RollYearPanel.SuspendLayout();
            this.AgEqualPanel.SuspendLayout();
            this.NonAgEqualPanel.SuspendLayout();
            this.CountyPanel.SuspendLayout();
            this.CropPanel.SuspendLayout();
            this.NonCropPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 145);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.RollYearPanel);
            this.panel2.Controls.Add(this.AgEqualPanel);
            this.panel2.Controls.Add(this.NonAgEqualPanel);
            this.panel2.Controls.Add(this.CountyPanel);
            this.panel2.Controls.Add(this.CropPanel);
            this.panel2.Controls.Add(this.NonCropPanel);
            this.panel2.Controls.Add(this.PictureBox);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(792, 145);
            this.panel2.TabIndex = 0;
            // 
            // RollYearPanel
            // 
            this.RollYearPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RollYearPanel.Controls.Add(this.RollYearTextBox);
            this.RollYearPanel.Controls.Add(this.RollYearLabel);
            this.RollYearPanel.Location = new System.Drawing.Point(0, 0);
            this.RollYearPanel.Name = "RollYearPanel";
            this.RollYearPanel.Size = new System.Drawing.Size(150, 41);
            this.RollYearPanel.TabIndex = 1;
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
            this.RollYearTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.RollYearTextBox.ForeColor = System.Drawing.Color.Black;
            this.RollYearTextBox.IsEditable = true;
            this.RollYearTextBox.IsQueryableFileld = false;
            this.RollYearTextBox.Location = new System.Drawing.Point(8, 19);
            this.RollYearTextBox.LockKeyPress = false;
            this.RollYearTextBox.MaxLength = 4;
            this.RollYearTextBox.Name = "RollYearTextBox";
            this.RollYearTextBox.PersistDefaultColor = false;
            this.RollYearTextBox.Precision = 2;
            this.RollYearTextBox.QueryingFileldName = "";
            this.RollYearTextBox.SetColorFlag = false;
            this.RollYearTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.RollYearTextBox.Size = new System.Drawing.Size(100, 16);
            this.RollYearTextBox.SpecialCharacter = "%";
            this.RollYearTextBox.TabIndex = 3;
            this.RollYearTextBox.TextCustomFormat = "$ #,##0.00";
            this.RollYearTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Smallint;
            this.RollYearTextBox.WholeInteger = false;
            this.RollYearTextBox.TextChanged += new System.EventHandler(this.Control_TextChanged);
            this.RollYearTextBox.Leave += new System.EventHandler(this.RollYearTextBox_Leave);
            // 
            // RollYearLabel
            // 
            this.RollYearLabel.AutoSize = true;
            this.RollYearLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RollYearLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.RollYearLabel.Location = new System.Drawing.Point(0, 0);
            this.RollYearLabel.Name = "RollYearLabel";
            this.RollYearLabel.Size = new System.Drawing.Size(57, 14);
            this.RollYearLabel.TabIndex = 1;
            this.RollYearLabel.Text = "Roll Year:";
            // 
            // AgEqualPanel
            // 
            this.AgEqualPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AgEqualPanel.Controls.Add(this.AgEqualLabel);
            this.AgEqualPanel.Controls.Add(this.AgEqualTextBox);
            this.AgEqualPanel.Location = new System.Drawing.Point(149, 0);
            this.AgEqualPanel.Name = "AgEqualPanel";
            this.AgEqualPanel.Size = new System.Drawing.Size(300, 41);
            this.AgEqualPanel.TabIndex = 2;
            // 
            // AgEqualLabel
            // 
            this.AgEqualLabel.AutoSize = true;
            this.AgEqualLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AgEqualLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.AgEqualLabel.Location = new System.Drawing.Point(0, 0);
            this.AgEqualLabel.Name = "AgEqualLabel";
            this.AgEqualLabel.Size = new System.Drawing.Size(120, 14);
            this.AgEqualLabel.TabIndex = 1;
            this.AgEqualLabel.Text = "Ag Equalization Rate:";
            // 
            // AgEqualTextBox
            // 
            this.AgEqualTextBox.AllowClick = true;
            this.AgEqualTextBox.AllowNegativeSign = false;
            this.AgEqualTextBox.ApplyCFGFormat = false;
            this.AgEqualTextBox.ApplyCurrencyFormat = true;
            this.AgEqualTextBox.ApplyFocusColor = true;
            this.AgEqualTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.AgEqualTextBox.ApplyNegativeStandard = false;
            this.AgEqualTextBox.ApplyParentFocusColor = true;
            this.AgEqualTextBox.ApplyTimeFormat = false;
            this.AgEqualTextBox.BackColor = System.Drawing.Color.White;
            this.AgEqualTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AgEqualTextBox.CFromatWihoutSymbol = false;
            this.AgEqualTextBox.CheckForEmpty = false;
            this.AgEqualTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AgEqualTextBox.Digits = 1;
            this.AgEqualTextBox.EmptyDecimalValue = true;
            this.AgEqualTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.AgEqualTextBox.ForeColor = System.Drawing.Color.Black;
            this.AgEqualTextBox.IsEditable = true;
            this.AgEqualTextBox.IsQueryableFileld = false;
            this.AgEqualTextBox.Location = new System.Drawing.Point(8, 19);
            this.AgEqualTextBox.LockKeyPress = false;
            this.AgEqualTextBox.MaxLength = 0;
            this.AgEqualTextBox.Name = "AgEqualTextBox";
            this.AgEqualTextBox.PersistDefaultColor = false;
            this.AgEqualTextBox.Precision = 4;
            this.AgEqualTextBox.QueryingFileldName = "";
            this.AgEqualTextBox.SetColorFlag = false;
            this.AgEqualTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.AgEqualTextBox.Size = new System.Drawing.Size(250, 16);
            this.AgEqualTextBox.SpecialCharacter = "%";
            this.AgEqualTextBox.TabIndex = 3;
            this.AgEqualTextBox.TextCustomFormat = "0.0000";
            this.AgEqualTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.AgEqualTextBox.WholeInteger = false;
            this.AgEqualTextBox.TextChanged += new System.EventHandler(this.Control_TextChanged);
            this.AgEqualTextBox.Leave += new System.EventHandler(this.AgEqualTextBox_Leave);
            // 
            // NonAgEqualPanel
            // 
            this.NonAgEqualPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NonAgEqualPanel.Controls.Add(this.NonAgEqualLabel);
            this.NonAgEqualPanel.Controls.Add(this.NonAgEqualTextBox);
            this.NonAgEqualPanel.Location = new System.Drawing.Point(448, 0);
            this.NonAgEqualPanel.Name = "NonAgEqualPanel";
            this.NonAgEqualPanel.Size = new System.Drawing.Size(300, 41);
            this.NonAgEqualPanel.TabIndex = 3;
            // 
            // NonAgEqualLabel
            // 
            this.NonAgEqualLabel.AutoSize = true;
            this.NonAgEqualLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NonAgEqualLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.NonAgEqualLabel.Location = new System.Drawing.Point(0, 0);
            this.NonAgEqualLabel.Name = "NonAgEqualLabel";
            this.NonAgEqualLabel.Size = new System.Drawing.Size(145, 14);
            this.NonAgEqualLabel.TabIndex = 1;
            this.NonAgEqualLabel.Text = "Non-Ag Equalization Rate:";
            // 
            // NonAgEqualTextBox
            // 
            this.NonAgEqualTextBox.AllowClick = true;
            this.NonAgEqualTextBox.AllowNegativeSign = false;
            this.NonAgEqualTextBox.ApplyCFGFormat = false;
            this.NonAgEqualTextBox.ApplyCurrencyFormat = true;
            this.NonAgEqualTextBox.ApplyFocusColor = true;
            this.NonAgEqualTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.NonAgEqualTextBox.ApplyNegativeStandard = false;
            this.NonAgEqualTextBox.ApplyParentFocusColor = true;
            this.NonAgEqualTextBox.ApplyTimeFormat = false;
            this.NonAgEqualTextBox.BackColor = System.Drawing.Color.White;
            this.NonAgEqualTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NonAgEqualTextBox.CFromatWihoutSymbol = false;
            this.NonAgEqualTextBox.CheckForEmpty = false;
            this.NonAgEqualTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NonAgEqualTextBox.Digits = 1;
            this.NonAgEqualTextBox.EmptyDecimalValue = true;
            this.NonAgEqualTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.NonAgEqualTextBox.ForeColor = System.Drawing.Color.Black;
            this.NonAgEqualTextBox.IsEditable = true;
            this.NonAgEqualTextBox.IsQueryableFileld = false;
            this.NonAgEqualTextBox.Location = new System.Drawing.Point(8, 19);
            this.NonAgEqualTextBox.LockKeyPress = false;
            this.NonAgEqualTextBox.MaxLength = 0;
            this.NonAgEqualTextBox.Name = "NonAgEqualTextBox";
            this.NonAgEqualTextBox.PersistDefaultColor = false;
            this.NonAgEqualTextBox.Precision = 4;
            this.NonAgEqualTextBox.QueryingFileldName = "";
            this.NonAgEqualTextBox.SetColorFlag = false;
            this.NonAgEqualTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.NonAgEqualTextBox.Size = new System.Drawing.Size(250, 16);
            this.NonAgEqualTextBox.SpecialCharacter = "%";
            this.NonAgEqualTextBox.TabIndex = 3;
            this.NonAgEqualTextBox.TextCustomFormat = "0.0000";
            this.NonAgEqualTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.NonAgEqualTextBox.WholeInteger = false;
            this.NonAgEqualTextBox.TextChanged += new System.EventHandler(this.Control_TextChanged);
            this.NonAgEqualTextBox.Leave += new System.EventHandler(this.NonAgEqualTextBox_Leave);
            // 
            // CountyPanel
            // 
            this.CountyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CountyPanel.Controls.Add(this.CountyTextBox);
            this.CountyPanel.Controls.Add(this.CountyLabel);
            this.CountyPanel.Location = new System.Drawing.Point(248, 40);
            this.CountyPanel.Name = "CountyPanel";
            this.CountyPanel.Size = new System.Drawing.Size(250, 41);
            this.CountyPanel.TabIndex = 5;
            // 
            // CountyTextBox
            // 
            this.CountyTextBox.AllowClick = true;
            this.CountyTextBox.AllowNegativeSign = false;
            this.CountyTextBox.ApplyCFGFormat = false;
            this.CountyTextBox.ApplyCurrencyFormat = true;
            this.CountyTextBox.ApplyFocusColor = true;
            this.CountyTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.CountyTextBox.ApplyNegativeStandard = false;
            this.CountyTextBox.ApplyParentFocusColor = true;
            this.CountyTextBox.ApplyTimeFormat = false;
            this.CountyTextBox.BackColor = System.Drawing.Color.White;
            this.CountyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CountyTextBox.CFromatWihoutSymbol = false;
            this.CountyTextBox.CheckForEmpty = false;
            this.CountyTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CountyTextBox.Digits = 1;
            this.CountyTextBox.EmptyDecimalValue = true;
            this.CountyTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.CountyTextBox.ForeColor = System.Drawing.Color.Black;
            this.CountyTextBox.IsEditable = false;
            this.CountyTextBox.IsQueryableFileld = true;
            this.CountyTextBox.Location = new System.Drawing.Point(8, 19);
            this.CountyTextBox.LockKeyPress = false;
            this.CountyTextBox.MaxLength = 0;
            this.CountyTextBox.Name = "CountyTextBox";
            this.CountyTextBox.PersistDefaultColor = false;
            this.CountyTextBox.Precision = 5;
            this.CountyTextBox.QueryingFileldName = "";
            this.CountyTextBox.SetColorFlag = false;
            this.CountyTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CountyTextBox.Size = new System.Drawing.Size(110, 16);
            this.CountyTextBox.SpecialCharacter = "%";
            this.CountyTextBox.TabIndex = 3;
            this.CountyTextBox.TextCustomFormat = "0.00000";
            this.CountyTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.CountyTextBox.WholeInteger = false;
            this.CountyTextBox.TextChanged += new System.EventHandler(this.CountyTextBox_TextChanged);
            this.CountyTextBox.Leave += new System.EventHandler(this.CountyTextBox_Leave);
            // 
            // CountyLabel
            // 
            this.CountyLabel.AutoSize = true;
            this.CountyLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CountyLabel.Location = new System.Drawing.Point(0, 0);
            this.CountyLabel.Name = "CountyLabel";
            this.CountyLabel.Size = new System.Drawing.Size(86, 14);
            this.CountyLabel.TabIndex = 1;
            this.CountyLabel.Text = "County Factor:";
            // 
            // CropPanel
            // 
            this.CropPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CropPanel.Controls.Add(this.CropTextBox);
            this.CropPanel.Controls.Add(this.CropLabel);
            this.CropPanel.Location = new System.Drawing.Point(0, 40);
            this.CropPanel.Name = "CropPanel";
            this.CropPanel.Size = new System.Drawing.Size(249, 41);
            this.CropPanel.TabIndex = 4;
            // 
            // CropTextBox
            // 
            this.CropTextBox.AllowClick = true;
            this.CropTextBox.AllowNegativeSign = false;
            this.CropTextBox.ApplyCFGFormat = false;
            this.CropTextBox.ApplyCurrencyFormat = false;
            this.CropTextBox.ApplyFocusColor = true;
            this.CropTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.CropTextBox.ApplyNegativeStandard = true;
            this.CropTextBox.ApplyParentFocusColor = true;
            this.CropTextBox.ApplyTimeFormat = false;
            this.CropTextBox.BackColor = System.Drawing.Color.White;
            this.CropTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CropTextBox.CFromatWihoutSymbol = false;
            this.CropTextBox.CheckForEmpty = false;
            this.CropTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CropTextBox.Digits = 11;
            this.CropTextBox.EmptyDecimalValue = false;
            this.CropTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.CropTextBox.ForeColor = System.Drawing.Color.Black;
            this.CropTextBox.IsEditable = true;
            this.CropTextBox.IsQueryableFileld = false;
            this.CropTextBox.Location = new System.Drawing.Point(8, 19);
            this.CropTextBox.LockKeyPress = false;
            this.CropTextBox.MaxLength = 0;
            this.CropTextBox.Name = "CropTextBox";
            this.CropTextBox.PersistDefaultColor = false;
            this.CropTextBox.Precision = 2;
            this.CropTextBox.QueryingFileldName = "";
            this.CropTextBox.SetColorFlag = false;
            this.CropTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CropTextBox.Size = new System.Drawing.Size(200, 16);
            this.CropTextBox.SpecialCharacter = "%";
            this.CropTextBox.TabIndex = 3;
            this.CropTextBox.TextCustomFormat = "$ #,##0.00";
            this.CropTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Money;
            this.CropTextBox.WholeInteger = false;
            this.CropTextBox.TextChanged += new System.EventHandler(this.CropTextBox_TextChanged);
            this.CropTextBox.Leave += new System.EventHandler(this.CropTextBox_Leave);
            // 
            // CropLabel
            // 
            this.CropLabel.AutoSize = true;
            this.CropLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CropLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CropLabel.Location = new System.Drawing.Point(0, 0);
            this.CropLabel.Name = "CropLabel";
            this.CropLabel.Size = new System.Drawing.Size(94, 14);
            this.CropLabel.TabIndex = 1;
            this.CropLabel.Text = "Crop Top Dollar:";
            // 
            // NonCropPanel
            // 
            this.NonCropPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NonCropPanel.Controls.Add(this.NonCropTextBox);
            this.NonCropPanel.Controls.Add(this.NonCropLabel);
            this.NonCropPanel.Location = new System.Drawing.Point(497, 40);
            this.NonCropPanel.Name = "NonCropPanel";
            this.NonCropPanel.Size = new System.Drawing.Size(251, 41);
            this.NonCropPanel.TabIndex = 9;
            // 
            // NonCropTextBox
            // 
            this.NonCropTextBox.AllowClick = true;
            this.NonCropTextBox.AllowNegativeSign = false;
            this.NonCropTextBox.ApplyCFGFormat = false;
            this.NonCropTextBox.ApplyCurrencyFormat = false;
            this.NonCropTextBox.ApplyFocusColor = true;
            this.NonCropTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.NonCropTextBox.ApplyNegativeStandard = true;
            this.NonCropTextBox.ApplyParentFocusColor = true;
            this.NonCropTextBox.ApplyTimeFormat = false;
            this.NonCropTextBox.BackColor = System.Drawing.Color.White;
            this.NonCropTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NonCropTextBox.CFromatWihoutSymbol = false;
            this.NonCropTextBox.CheckForEmpty = false;
            this.NonCropTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NonCropTextBox.Digits = 11;
            this.NonCropTextBox.EmptyDecimalValue = false;
            this.NonCropTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.NonCropTextBox.ForeColor = System.Drawing.Color.Gray;
            this.NonCropTextBox.IsEditable = false;
            this.NonCropTextBox.IsQueryableFileld = false;
            this.NonCropTextBox.Location = new System.Drawing.Point(8, 19);
            this.NonCropTextBox.LockKeyPress = true;
            this.NonCropTextBox.MaxLength = 0;
            this.NonCropTextBox.Name = "NonCropTextBox";
            this.NonCropTextBox.PersistDefaultColor = false;
            this.NonCropTextBox.Precision = 2;
            this.NonCropTextBox.QueryingFileldName = "";
            this.NonCropTextBox.ReadOnly = true;
            this.NonCropTextBox.SetColorFlag = false;
            this.NonCropTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.NonCropTextBox.Size = new System.Drawing.Size(200, 16);
            this.NonCropTextBox.SpecialCharacter = "%";
            this.NonCropTextBox.TabIndex = 3;
            this.NonCropTextBox.TabStop = false;
            this.NonCropTextBox.TextCustomFormat = "$ #,##0.00";
            this.NonCropTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Money;
            this.NonCropTextBox.WholeInteger = false;
            this.NonCropTextBox.TextChanged += new System.EventHandler(this.Control_TextChanged);
            // 
            // NonCropLabel
            // 
            this.NonCropLabel.AutoSize = true;
            this.NonCropLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NonCropLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.NonCropLabel.Location = new System.Drawing.Point(0, 0);
            this.NonCropLabel.Name = "NonCropLabel";
            this.NonCropLabel.Size = new System.Drawing.Size(119, 14);
            this.NonCropLabel.TabIndex = 1;
            this.NonCropLabel.Text = "Non-Crop Top Dollar:";
            // 
            // PictureBox
            // 
            this.PictureBox.BackColor = System.Drawing.Color.White;
            this.PictureBox.Location = new System.Drawing.Point(738, 0);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(42, 81);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox.TabIndex = 1;
            this.PictureBox.TabStop = false;
            this.PictureBox.Click += new System.EventHandler(this.PictureBox_Click);
            this.PictureBox.MouseHover += new System.EventHandler(this.PictureBox_Click);
            // 
            // F39110
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Name = "F39110";
            this.Size = new System.Drawing.Size(792, 150);
            this.Tag = "39110";
            this.Load += new System.EventHandler(this.F39110_Load);
            this.panel2.ResumeLayout(false);
            this.RollYearPanel.ResumeLayout(false);
            this.RollYearPanel.PerformLayout();
            this.AgEqualPanel.ResumeLayout(false);
            this.AgEqualPanel.PerformLayout();
            this.NonAgEqualPanel.ResumeLayout(false);
            this.NonAgEqualPanel.PerformLayout();
            this.CountyPanel.ResumeLayout(false);
            this.CountyPanel.PerformLayout();
            this.CropPanel.ResumeLayout(false);
            this.CropPanel.PerformLayout();
            this.NonCropPanel.ResumeLayout(false);
            this.NonCropPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Panel RollYearPanel;
        private System.Windows.Forms.Label RollYearLabel;
        private TerraScan.UI.Controls.TerraScanTextBox RollYearTextBox;
        private System.Windows.Forms.Panel AgEqualPanel;
        private System.Windows.Forms.Label AgEqualLabel;
        private TerraScan.UI.Controls.TerraScanTextBox AgEqualTextBox;
        private System.Windows.Forms.Panel NonAgEqualPanel;
        private System.Windows.Forms.Label NonAgEqualLabel;
        private TerraScan.UI.Controls.TerraScanTextBox NonAgEqualTextBox;
        private System.Windows.Forms.Panel CropPanel;
        private System.Windows.Forms.Label CropLabel;
        private TerraScan.UI.Controls.TerraScanTextBox CropTextBox;
        private System.Windows.Forms.Panel NonCropPanel;
        private System.Windows.Forms.Label NonCropLabel;
        private TerraScan.UI.Controls.TerraScanTextBox NonCropTextBox;
        private System.Windows.Forms.Panel CountyPanel;
        private System.Windows.Forms.Label CountyLabel;
        private TerraScan.UI.Controls.TerraScanTextBox CountyTextBox;

        #endregion
        private System.Windows.Forms.ToolTip topDollarToolTip;
    }
}
