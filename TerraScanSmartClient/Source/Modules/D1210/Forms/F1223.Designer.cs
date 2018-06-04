namespace D1210
{
    partial class F1223
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1223));
            this.VoidPanel = new System.Windows.Forms.Panel();
            this.VoidButton = new System.Windows.Forms.Button();
            this.VoidTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.VoidLabel = new System.Windows.Forms.Label();
            this.CLDetailPanel = new System.Windows.Forms.Panel();
            this.CheckDatePanel = new System.Windows.Forms.Panel();
            this.CheckDateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CheckDateLabel = new System.Windows.Forms.Label();
            this.CheckAmountPanel = new System.Windows.Forms.Panel();
            this.CheckAmountLabel = new System.Windows.Forms.Label();
            this.CheckAmountTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.PayableToPanel = new System.Windows.Forms.Panel();
            this.PayableToTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.PayableToLabel = new System.Windows.Forms.Label();
            this.CLIDPanel = new System.Windows.Forms.Panel();
            this.CLIDLabel = new System.Windows.Forms.Label();
            this.CLIDTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.SaveCLButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CancelCLButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DistrictLinePanel = new System.Windows.Forms.Panel();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.VoidCheckBox = new System.Windows.Forms.CheckBox();
            this.VoidMonthCalendar = new TerraScan.UI.Controls.TerraScanMonthCalender();
            this.VoidCheckMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SaveMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VoidPanel.SuspendLayout();
            this.CLDetailPanel.SuspendLayout();
            this.CheckDatePanel.SuspendLayout();
            this.CheckAmountPanel.SuspendLayout();
            this.PayableToPanel.SuspendLayout();
            this.CLIDPanel.SuspendLayout();
            this.VoidCheckMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // VoidPanel
            // 
            this.VoidPanel.BackColor = System.Drawing.Color.Transparent;
            this.VoidPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VoidPanel.Controls.Add(this.VoidButton);
            this.VoidPanel.Controls.Add(this.VoidTextBox);
            this.VoidPanel.Controls.Add(this.VoidLabel);
            this.VoidPanel.Location = new System.Drawing.Point(10, 123);
            this.VoidPanel.Name = "VoidPanel";
            this.VoidPanel.Size = new System.Drawing.Size(330, 37);
            this.VoidPanel.TabIndex = 1;
            this.VoidPanel.TabStop = true;
            // 
            // VoidButton
            // 
            this.VoidButton.FlatAppearance.BorderSize = 0;
            this.VoidButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VoidButton.Image = ((System.Drawing.Image)(resources.GetObject("VoidButton.Image")));
            this.VoidButton.Location = new System.Drawing.Point(295, 11);
            this.VoidButton.Name = "VoidButton";
            this.VoidButton.Size = new System.Drawing.Size(20, 21);
            this.VoidButton.TabIndex = 1;
            this.VoidButton.Tag = "VoidTextBox";
            this.VoidButton.UseVisualStyleBackColor = true;
            this.VoidButton.Click += new System.EventHandler(this.VoidButton_Click);
            // 
            // VoidTextBox
            // 
            this.VoidTextBox.AllowClick = true;
            this.VoidTextBox.AllowNegativeSign = false;
            this.VoidTextBox.ApplyCFGFormat = false;
            this.VoidTextBox.ApplyCurrencyFormat = false;
            this.VoidTextBox.ApplyFocusColor = true;
            this.VoidTextBox.ApplyNegativeStandard = true;
            this.VoidTextBox.ApplyParentFocusColor = true;
            this.VoidTextBox.ApplyTimeFormat = false;
            this.VoidTextBox.BackColor = System.Drawing.Color.White;
            this.VoidTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.VoidTextBox.CFromatWihoutSymbol = false;
            this.VoidTextBox.CheckForEmpty = false;
            this.VoidTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.VoidTextBox.Digits = -1;
            this.VoidTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.VoidTextBox.ForeColor = System.Drawing.Color.Black;
            this.VoidTextBox.IsEditable = false;
            this.VoidTextBox.IsQueryableFileld = false;
            this.VoidTextBox.Location = new System.Drawing.Point(8, 16);
            this.VoidTextBox.LockKeyPress = false;
            this.VoidTextBox.MaxLength = 10;
            this.VoidTextBox.Name = "VoidTextBox";
            this.VoidTextBox.PersistDefaultColor = false;
            this.VoidTextBox.Precision = 2;
            this.VoidTextBox.QueryingFileldName = "";
            this.VoidTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.VoidTextBox.Size = new System.Drawing.Size(281, 16);
            this.VoidTextBox.SpecialCharacter = "%";
            this.VoidTextBox.TabIndex = 0;
            this.VoidTextBox.Tag = "";
            this.VoidTextBox.Text = "12/12/1212";
            this.VoidTextBox.TextCustomFormat = "$#,##0.00";
            this.VoidTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            // 
            // VoidLabel
            // 
            this.VoidLabel.AutoSize = true;
            this.VoidLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.VoidLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.VoidLabel.Location = new System.Drawing.Point(0, 0);
            this.VoidLabel.Name = "VoidLabel";
            this.VoidLabel.Size = new System.Drawing.Size(68, 14);
            this.VoidLabel.TabIndex = 0;
            this.VoidLabel.Text = "Void As Of:";
            // 
            // CLDetailPanel
            // 
            this.CLDetailPanel.Controls.Add(this.CheckDatePanel);
            this.CLDetailPanel.Controls.Add(this.CheckAmountPanel);
            this.CLDetailPanel.Controls.Add(this.PayableToPanel);
            this.CLDetailPanel.Controls.Add(this.CLIDPanel);
            this.CLDetailPanel.Location = new System.Drawing.Point(10, 10);
            this.CLDetailPanel.Name = "CLDetailPanel";
            this.CLDetailPanel.Size = new System.Drawing.Size(330, 73);
            this.CLDetailPanel.TabIndex = 124;
            // 
            // CheckDatePanel
            // 
            this.CheckDatePanel.BackColor = System.Drawing.Color.Transparent;
            this.CheckDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CheckDatePanel.Controls.Add(this.CheckDateTextBox);
            this.CheckDatePanel.Controls.Add(this.CheckDateLabel);
            this.CheckDatePanel.Location = new System.Drawing.Point(0, 36);
            this.CheckDatePanel.Name = "CheckDatePanel";
            this.CheckDatePanel.Size = new System.Drawing.Size(113, 37);
            this.CheckDatePanel.TabIndex = 6;
            // 
            // CheckDateTextBox
            // 
            this.CheckDateTextBox.AllowClick = true;
            this.CheckDateTextBox.AllowNegativeSign = false;
            this.CheckDateTextBox.ApplyCFGFormat = false;
            this.CheckDateTextBox.ApplyCurrencyFormat = false;
            this.CheckDateTextBox.ApplyFocusColor = true;
            this.CheckDateTextBox.ApplyNegativeStandard = true;
            this.CheckDateTextBox.ApplyParentFocusColor = true;
            this.CheckDateTextBox.ApplyTimeFormat = false;
            this.CheckDateTextBox.BackColor = System.Drawing.Color.White;
            this.CheckDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CheckDateTextBox.CFromatWihoutSymbol = false;
            this.CheckDateTextBox.CheckForEmpty = false;
            this.CheckDateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CheckDateTextBox.Digits = -1;
            this.CheckDateTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.CheckDateTextBox.ForeColor = System.Drawing.Color.Black;
            this.CheckDateTextBox.IsEditable = false;
            this.CheckDateTextBox.IsQueryableFileld = false;
            this.CheckDateTextBox.Location = new System.Drawing.Point(8, 16);
            this.CheckDateTextBox.LockKeyPress = true;
            this.CheckDateTextBox.MaxLength = 75;
            this.CheckDateTextBox.Name = "CheckDateTextBox";
            this.CheckDateTextBox.PersistDefaultColor = false;
            this.CheckDateTextBox.Precision = 2;
            this.CheckDateTextBox.QueryingFileldName = "";
            this.CheckDateTextBox.ReadOnly = true;
            this.CheckDateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CheckDateTextBox.Size = new System.Drawing.Size(100, 16);
            this.CheckDateTextBox.SpecialCharacter = "%";
            this.CheckDateTextBox.TabIndex = 1;
            this.CheckDateTextBox.TabStop = false;
            this.CheckDateTextBox.Tag = "";
            this.CheckDateTextBox.Text = "12/12/1212";
            this.CheckDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CheckDateTextBox.TextCustomFormat = "$#,##0.00";
            this.CheckDateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            // 
            // CheckDateLabel
            // 
            this.CheckDateLabel.AutoSize = true;
            this.CheckDateLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.CheckDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CheckDateLabel.Location = new System.Drawing.Point(0, 0);
            this.CheckDateLabel.Name = "CheckDateLabel";
            this.CheckDateLabel.Size = new System.Drawing.Size(72, 14);
            this.CheckDateLabel.TabIndex = 0;
            this.CheckDateLabel.Text = "Check Date:";
            // 
            // CheckAmountPanel
            // 
            this.CheckAmountPanel.BackColor = System.Drawing.Color.Transparent;
            this.CheckAmountPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CheckAmountPanel.Controls.Add(this.CheckAmountLabel);
            this.CheckAmountPanel.Controls.Add(this.CheckAmountTextBox);
            this.CheckAmountPanel.Location = new System.Drawing.Point(112, 36);
            this.CheckAmountPanel.Name = "CheckAmountPanel";
            this.CheckAmountPanel.Size = new System.Drawing.Size(218, 37);
            this.CheckAmountPanel.TabIndex = 3;
            // 
            // CheckAmountLabel
            // 
            this.CheckAmountLabel.AutoSize = true;
            this.CheckAmountLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.CheckAmountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CheckAmountLabel.Location = new System.Drawing.Point(0, 0);
            this.CheckAmountLabel.Name = "CheckAmountLabel";
            this.CheckAmountLabel.Size = new System.Drawing.Size(92, 14);
            this.CheckAmountLabel.TabIndex = 0;
            this.CheckAmountLabel.Text = "Check Amount:";
            // 
            // CheckAmountTextBox
            // 
            this.CheckAmountTextBox.AcceptsReturn = true;
            this.CheckAmountTextBox.AllowClick = true;
            this.CheckAmountTextBox.AllowNegativeSign = false;
            this.CheckAmountTextBox.ApplyCFGFormat = false;
            this.CheckAmountTextBox.ApplyCurrencyFormat = true;
            this.CheckAmountTextBox.ApplyFocusColor = true;
            this.CheckAmountTextBox.ApplyNegativeStandard = true;
            this.CheckAmountTextBox.ApplyParentFocusColor = true;
            this.CheckAmountTextBox.ApplyTimeFormat = false;
            this.CheckAmountTextBox.BackColor = System.Drawing.Color.White;
            this.CheckAmountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CheckAmountTextBox.CFromatWihoutSymbol = false;
            this.CheckAmountTextBox.CheckForEmpty = false;
            this.CheckAmountTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CheckAmountTextBox.Digits = -1;
            this.CheckAmountTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.CheckAmountTextBox.ForeColor = System.Drawing.Color.Black;
            this.CheckAmountTextBox.IsEditable = false;
            this.CheckAmountTextBox.IsQueryableFileld = false;
            this.CheckAmountTextBox.Location = new System.Drawing.Point(8, 16);
            this.CheckAmountTextBox.LockKeyPress = true;
            this.CheckAmountTextBox.MaxLength = 4;
            this.CheckAmountTextBox.Name = "CheckAmountTextBox";
            this.CheckAmountTextBox.PersistDefaultColor = false;
            this.CheckAmountTextBox.Precision = 2;
            this.CheckAmountTextBox.QueryingFileldName = "";
            this.CheckAmountTextBox.ReadOnly = true;
            this.CheckAmountTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CheckAmountTextBox.Size = new System.Drawing.Size(205, 16);
            this.CheckAmountTextBox.SpecialCharacter = "$";
            this.CheckAmountTextBox.TabIndex = 1;
            this.CheckAmountTextBox.TabStop = false;
            this.CheckAmountTextBox.Tag = "";
            this.CheckAmountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CheckAmountTextBox.TextCustomFormat = "$ #,##0.00";
            this.CheckAmountTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            // 
            // PayableToPanel
            // 
            this.PayableToPanel.BackColor = System.Drawing.Color.Transparent;
            this.PayableToPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PayableToPanel.Controls.Add(this.PayableToTextBox);
            this.PayableToPanel.Controls.Add(this.PayableToLabel);
            this.PayableToPanel.Location = new System.Drawing.Point(90, 0);
            this.PayableToPanel.Name = "PayableToPanel";
            this.PayableToPanel.Size = new System.Drawing.Size(240, 37);
            this.PayableToPanel.TabIndex = 2;
            // 
            // PayableToTextBox
            // 
            this.PayableToTextBox.AllowClick = true;
            this.PayableToTextBox.AllowNegativeSign = false;
            this.PayableToTextBox.ApplyCFGFormat = false;
            this.PayableToTextBox.ApplyCurrencyFormat = false;
            this.PayableToTextBox.ApplyFocusColor = true;
            this.PayableToTextBox.ApplyNegativeStandard = true;
            this.PayableToTextBox.ApplyParentFocusColor = true;
            this.PayableToTextBox.ApplyTimeFormat = false;
            this.PayableToTextBox.BackColor = System.Drawing.Color.White;
            this.PayableToTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PayableToTextBox.CFromatWihoutSymbol = false;
            this.PayableToTextBox.CheckForEmpty = false;
            this.PayableToTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PayableToTextBox.Digits = -1;
            this.PayableToTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.PayableToTextBox.ForeColor = System.Drawing.Color.Black;
            this.PayableToTextBox.IsEditable = false;
            this.PayableToTextBox.IsQueryableFileld = false;
            this.PayableToTextBox.Location = new System.Drawing.Point(8, 16);
            this.PayableToTextBox.LockKeyPress = true;
            this.PayableToTextBox.MaxLength = 75;
            this.PayableToTextBox.Name = "PayableToTextBox";
            this.PayableToTextBox.PersistDefaultColor = false;
            this.PayableToTextBox.Precision = 2;
            this.PayableToTextBox.QueryingFileldName = "";
            this.PayableToTextBox.ReadOnly = true;
            this.PayableToTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.PayableToTextBox.Size = new System.Drawing.Size(227, 16);
            this.PayableToTextBox.SpecialCharacter = "%";
            this.PayableToTextBox.TabIndex = 1;
            this.PayableToTextBox.TabStop = false;
            this.PayableToTextBox.Tag = "";
            this.PayableToTextBox.Text = "Jafsdfasdfasdf";
            this.PayableToTextBox.TextCustomFormat = "$#,##0.00";
            this.PayableToTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            // 
            // PayableToLabel
            // 
            this.PayableToLabel.AutoSize = true;
            this.PayableToLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.PayableToLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.PayableToLabel.Location = new System.Drawing.Point(0, 0);
            this.PayableToLabel.Name = "PayableToLabel";
            this.PayableToLabel.Size = new System.Drawing.Size(69, 14);
            this.PayableToLabel.TabIndex = 0;
            this.PayableToLabel.Text = "Payable To:";
            // 
            // CLIDPanel
            // 
            this.CLIDPanel.BackColor = System.Drawing.Color.Transparent;
            this.CLIDPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CLIDPanel.Controls.Add(this.CLIDLabel);
            this.CLIDPanel.Controls.Add(this.CLIDTextBox);
            this.CLIDPanel.Location = new System.Drawing.Point(0, 0);
            this.CLIDPanel.Name = "CLIDPanel";
            this.CLIDPanel.Size = new System.Drawing.Size(91, 37);
            this.CLIDPanel.TabIndex = 1;
            // 
            // CLIDLabel
            // 
            this.CLIDLabel.AutoSize = true;
            this.CLIDLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.CLIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CLIDLabel.Location = new System.Drawing.Point(0, 0);
            this.CLIDLabel.Name = "CLIDLabel";
            this.CLIDLabel.Size = new System.Drawing.Size(35, 14);
            this.CLIDLabel.TabIndex = 0;
            this.CLIDLabel.Text = "CLID:";
            // 
            // CLIDTextBox
            // 
            this.CLIDTextBox.AcceptsReturn = true;
            this.CLIDTextBox.AllowClick = true;
            this.CLIDTextBox.AllowNegativeSign = false;
            this.CLIDTextBox.ApplyCFGFormat = false;
            this.CLIDTextBox.ApplyCurrencyFormat = false;
            this.CLIDTextBox.ApplyFocusColor = true;
            this.CLIDTextBox.ApplyNegativeStandard = true;
            this.CLIDTextBox.ApplyParentFocusColor = true;
            this.CLIDTextBox.ApplyTimeFormat = false;
            this.CLIDTextBox.BackColor = System.Drawing.Color.White;
            this.CLIDTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CLIDTextBox.CFromatWihoutSymbol = false;
            this.CLIDTextBox.CheckForEmpty = false;
            this.CLIDTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CLIDTextBox.Digits = -1;
            this.CLIDTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.CLIDTextBox.ForeColor = System.Drawing.Color.Black;
            this.CLIDTextBox.IsEditable = false;
            this.CLIDTextBox.IsQueryableFileld = false;
            this.CLIDTextBox.Location = new System.Drawing.Point(8, 16);
            this.CLIDTextBox.LockKeyPress = true;
            this.CLIDTextBox.MaxLength = 4;
            this.CLIDTextBox.Name = "CLIDTextBox";
            this.CLIDTextBox.PersistDefaultColor = false;
            this.CLIDTextBox.Precision = 2;
            this.CLIDTextBox.QueryingFileldName = "";
            this.CLIDTextBox.ReadOnly = true;
            this.CLIDTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CLIDTextBox.Size = new System.Drawing.Size(69, 16);
            this.CLIDTextBox.SpecialCharacter = "%";
            this.CLIDTextBox.TabIndex = 1;
            this.CLIDTextBox.TabStop = false;
            this.CLIDTextBox.Tag = "";
            this.CLIDTextBox.Text = "51231";
            this.CLIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CLIDTextBox.TextCustomFormat = "$#,##0.00";
            this.CLIDTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Numeric;
            // 
            // SaveCLButton
            // 
            this.SaveCLButton.ActualPermission = false;
            this.SaveCLButton.ApplyDisableBehaviour = false;
            this.SaveCLButton.AutoSize = true;
            this.SaveCLButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SaveCLButton.BorderColor = System.Drawing.Color.Wheat;
            this.SaveCLButton.CommentPriority = false;
            this.SaveCLButton.EnableAutoPrint = false;
            this.SaveCLButton.FilterStatus = false;
            this.SaveCLButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveCLButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveCLButton.FocusRectangleEnabled = true;
            this.SaveCLButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveCLButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SaveCLButton.ImageSelected = false;
            this.SaveCLButton.Location = new System.Drawing.Point(10, 167);
            this.SaveCLButton.Name = "SaveCLButton";
            this.SaveCLButton.NewPadding = 5;
            this.SaveCLButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.SaveCLButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SaveCLButton.Size = new System.Drawing.Size(110, 30);
            this.SaveCLButton.StatusIndicator = false;
            this.SaveCLButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SaveCLButton.StatusOffText = null;
            this.SaveCLButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SaveCLButton.StatusOnText = null;
            this.SaveCLButton.TabIndex = 126;
            this.SaveCLButton.TabStop = false;
            this.SaveCLButton.Text = "Save";
            this.SaveCLButton.UseVisualStyleBackColor = false;
            this.SaveCLButton.Click += new System.EventHandler(this.SaveCLButton_Click);
            // 
            // CancelCLButton
            // 
            this.CancelCLButton.ActualPermission = false;
            this.CancelCLButton.ApplyDisableBehaviour = false;
            this.CancelCLButton.AutoSize = true;
            this.CancelCLButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelCLButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelCLButton.CommentPriority = false;
            this.CancelCLButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelCLButton.EnableAutoPrint = false;
            this.CancelCLButton.FilterStatus = false;
            this.CancelCLButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelCLButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelCLButton.FocusRectangleEnabled = true;
            this.CancelCLButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelCLButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelCLButton.ImageSelected = false;
            this.CancelCLButton.Location = new System.Drawing.Point(230, 167);
            this.CancelCLButton.Name = "CancelCLButton";
            this.CancelCLButton.NewPadding = 5;
            this.CancelCLButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CancelCLButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelCLButton.Size = new System.Drawing.Size(110, 30);
            this.CancelCLButton.StatusIndicator = false;
            this.CancelCLButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelCLButton.StatusOffText = null;
            this.CancelCLButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelCLButton.StatusOnText = null;
            this.CancelCLButton.TabIndex = 127;
            this.CancelCLButton.TabStop = false;
            this.CancelCLButton.Text = "Cancel";
            this.CancelCLButton.UseVisualStyleBackColor = false;
            // 
            // DistrictLinePanel
            // 
            this.DistrictLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.DistrictLinePanel.Location = new System.Drawing.Point(10, 204);
            this.DistrictLinePanel.Name = "DistrictLinePanel";
            this.DistrictLinePanel.Size = new System.Drawing.Size(330, 2);
            this.DistrictLinePanel.TabIndex = 128;
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(11, 208);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(35, 15);
            this.FormIDLabel.TabIndex = 129;
            this.FormIDLabel.Text = "1223";
            // 
            // VoidCheckBox
            // 
            this.VoidCheckBox.Font = new System.Drawing.Font("Arial", 9F);
            this.VoidCheckBox.ForeColor = System.Drawing.Color.Black;
            this.VoidCheckBox.Location = new System.Drawing.Point(10, 103);
            this.VoidCheckBox.Name = "VoidCheckBox";
            this.VoidCheckBox.Size = new System.Drawing.Size(100, 20);
            this.VoidCheckBox.TabIndex = 0;
            this.VoidCheckBox.Text = "Check Is Void";
            this.VoidCheckBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.VoidCheckBox.UseCompatibleTextRendering = true;
            this.VoidCheckBox.UseVisualStyleBackColor = false;
            this.VoidCheckBox.CheckedChanged += new System.EventHandler(this.VoidCheckBox_CheckedChanged);
            // 
            // VoidMonthCalendar
            // 
            this.VoidMonthCalendar.FocusRemovedFrom = false;
            this.VoidMonthCalendar.Location = new System.Drawing.Point(127, 0);
            this.VoidMonthCalendar.MaxDate = new System.DateTime(2074, 12, 31, 0, 0, 0, 0);
            this.VoidMonthCalendar.MaxSelectionCount = 1;
            this.VoidMonthCalendar.Name = "VoidMonthCalendar";
            this.VoidMonthCalendar.TabIndex = 162;
            this.VoidMonthCalendar.Visible = false;
            this.VoidMonthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.VoidMonthCalendar_DateSelected);
            this.VoidMonthCalendar.Validating += new System.ComponentModel.CancelEventHandler(this.VoidMonthCalendar_Validating);
            this.VoidMonthCalendar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VoidMonthCalendar_KeyDown);
            // 
            // VoidCheckMenuStrip
            // 
            this.VoidCheckMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveMenuToolStripMenuItem});
            this.VoidCheckMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.VoidCheckMenuStrip.Name = "VoidCheckMenuStrip";
            this.VoidCheckMenuStrip.Size = new System.Drawing.Size(348, 24);
            this.VoidCheckMenuStrip.TabIndex = 163;
            this.VoidCheckMenuStrip.Text = "menuStrip1";
            this.VoidCheckMenuStrip.Visible = false;
            // 
            // SaveMenuToolStripMenuItem
            // 
            this.SaveMenuToolStripMenuItem.Name = "SaveMenuToolStripMenuItem";
            this.SaveMenuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveMenuToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.SaveMenuToolStripMenuItem.Text = "SaveMenu";
            this.SaveMenuToolStripMenuItem.Visible = false;
            // 
            // F1223
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(350, 240);
            this.Controls.Add(this.VoidCheckMenuStrip);
            this.Controls.Add(this.VoidMonthCalendar);
            this.Controls.Add(this.VoidPanel);
            this.Controls.Add(this.CLDetailPanel);
            this.Controls.Add(this.SaveCLButton);
            this.Controls.Add(this.CancelCLButton);
            this.Controls.Add(this.DistrictLinePanel);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.VoidCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(356, 265);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(356, 265);
            this.Name = "F1223";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - Void Check";
            this.Load += new System.EventHandler(this.F1223_Load);
            this.VoidPanel.ResumeLayout(false);
            this.VoidPanel.PerformLayout();
            this.CLDetailPanel.ResumeLayout(false);
            this.CheckDatePanel.ResumeLayout(false);
            this.CheckDatePanel.PerformLayout();
            this.CheckAmountPanel.ResumeLayout(false);
            this.CheckAmountPanel.PerformLayout();
            this.PayableToPanel.ResumeLayout(false);
            this.PayableToPanel.PerformLayout();
            this.CLIDPanel.ResumeLayout(false);
            this.CLIDPanel.PerformLayout();
            this.VoidCheckMenuStrip.ResumeLayout(false);
            this.VoidCheckMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel VoidPanel;
        private System.Windows.Forms.Button VoidButton;
        private TerraScan.UI.Controls.TerraScanTextBox VoidTextBox;
        private System.Windows.Forms.Label VoidLabel;
        private System.Windows.Forms.Panel CLDetailPanel;
        private System.Windows.Forms.Panel CheckDatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox CheckDateTextBox;
        private System.Windows.Forms.Label CheckDateLabel;
        private System.Windows.Forms.Panel CheckAmountPanel;
        private System.Windows.Forms.Label CheckAmountLabel;
        private TerraScan.UI.Controls.TerraScanTextBox CheckAmountTextBox;
        private System.Windows.Forms.Panel PayableToPanel;
        private TerraScan.UI.Controls.TerraScanTextBox PayableToTextBox;
        private System.Windows.Forms.Label PayableToLabel;
        private System.Windows.Forms.Panel CLIDPanel;
        private System.Windows.Forms.Label CLIDLabel;
        private TerraScan.UI.Controls.TerraScanTextBox CLIDTextBox;
        private TerraScan.UI.Controls.TerraScanButton SaveCLButton;
        private TerraScan.UI.Controls.TerraScanButton CancelCLButton;
        private System.Windows.Forms.Panel DistrictLinePanel;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.CheckBox VoidCheckBox;
        private TerraScan.UI.Controls.TerraScanMonthCalender VoidMonthCalendar;
        private System.Windows.Forms.MenuStrip VoidCheckMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SaveMenuToolStripMenuItem;
    }
}