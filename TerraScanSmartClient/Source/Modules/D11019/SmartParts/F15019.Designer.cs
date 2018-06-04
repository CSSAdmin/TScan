namespace D11019
{
    partial class F15019
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F15019));
            this.JournalEntrypanel = new System.Windows.Forms.Panel();
            this.ReceiptMonthCalender = new TerraScan.UI.Controls.TerraScanMonthCalender();
            this.TransferDatepanel = new System.Windows.Forms.Panel();
            this.TransferDateCalenderButton = new System.Windows.Forms.Button();
            this.TransferDateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TransferDateLabel = new System.Windows.Forms.Label();
            this.ReceiptNumberPanel = new System.Windows.Forms.Panel();
            this.ReceiptNumberTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.ReceiptNumberLabel = new System.Windows.Forms.Label();
            this.EnteredBypanel = new System.Windows.Forms.Panel();
            this.EnteredByTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.EnteredByLabel = new System.Windows.Forms.Label();
            this.Postedpanel = new System.Windows.Forms.Panel();
            this.PostedTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.PostedLabel = new System.Windows.Forms.Label();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FromAccountPanel = new System.Windows.Forms.Panel();
            this.FromTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.comboBox1 = new TerraScan.UI.Controls.TerraScanComboBox();
            this.FromAccountLabel = new System.Windows.Forms.Label();
            this.TransferDescriptionpanel = new System.Windows.Forms.Panel();
            this.TransferDescriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TransferDescriptionpanellabel = new System.Windows.Forms.Label();
            this.TransferAmountpanel = new System.Windows.Forms.Panel();
            this.TransferAmountTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TransferAmountLabel = new System.Windows.Forms.Label();
            this.StartNewpanel = new System.Windows.Forms.Panel();
            this.checkBoxPanel = new System.Windows.Forms.Panel();
            this.StartNewcheckBox = new TerraScan.UI.Controls.TerraScanCheckBox();
            this.startNewLabel = new System.Windows.Forms.Label();
            this.TransfersPictureBox = new System.Windows.Forms.PictureBox();
            this.JournalEntryToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ToTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.ToAccounttpanel = new System.Windows.Forms.Panel();
            this.comboBox2 = new TerraScan.UI.Controls.TerraScanComboBox();
            this.ToAccountLabel = new System.Windows.Forms.Label();
            this.JournalEntrypanel.SuspendLayout();
            this.TransferDatepanel.SuspendLayout();
            this.ReceiptNumberPanel.SuspendLayout();
            this.EnteredBypanel.SuspendLayout();
            this.Postedpanel.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.FromAccountPanel.SuspendLayout();
            this.TransferDescriptionpanel.SuspendLayout();
            this.TransferAmountpanel.SuspendLayout();
            this.StartNewpanel.SuspendLayout();
            this.checkBoxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TransfersPictureBox)).BeginInit();
            this.ToAccounttpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // JournalEntrypanel
            // 
            this.JournalEntrypanel.BackColor = System.Drawing.Color.White;
            this.JournalEntrypanel.Controls.Add(this.ToAccounttpanel);
            this.JournalEntrypanel.Controls.Add(this.ReceiptMonthCalender);
            this.JournalEntrypanel.Controls.Add(this.TransferDatepanel);
            this.JournalEntrypanel.Controls.Add(this.ReceiptNumberPanel);
            this.JournalEntrypanel.Controls.Add(this.EnteredBypanel);
            this.JournalEntrypanel.Controls.Add(this.Postedpanel);
            this.JournalEntrypanel.Controls.Add(this.FromAccountPanel);
            this.JournalEntrypanel.Controls.Add(this.TransferDescriptionpanel);
            this.JournalEntrypanel.Controls.Add(this.TransferAmountpanel);
            this.JournalEntrypanel.Controls.Add(this.StartNewpanel);
            this.JournalEntrypanel.Controls.Add(this.TransfersPictureBox);
            this.JournalEntrypanel.Location = new System.Drawing.Point(0, 0);
            this.JournalEntrypanel.Name = "JournalEntrypanel";
            this.JournalEntrypanel.Size = new System.Drawing.Size(805, 377);
            this.JournalEntrypanel.TabIndex = 1;
            // 
            // ReceiptMonthCalender
            // 
            this.ReceiptMonthCalender.ApplyDateChange = false;
            this.ReceiptMonthCalender.FocusRemovedFrom = false;
            this.ReceiptMonthCalender.Location = new System.Drawing.Point(569, 213);
            this.ReceiptMonthCalender.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            this.ReceiptMonthCalender.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.ReceiptMonthCalender.Name = "ReceiptMonthCalender";
            this.ReceiptMonthCalender.TabIndex = 20;
            this.ReceiptMonthCalender.Visible = false;
            this.ReceiptMonthCalender.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.ReceiptMonthCalender_DateSelected);
            this.ReceiptMonthCalender.Leave += new System.EventHandler(this.ReceiptMonthCalender_Leave);
            this.ReceiptMonthCalender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReceiptMonthCalender_KeyDown);
            // 
            // TransferDatepanel
            // 
            this.TransferDatepanel.BackColor = System.Drawing.Color.Transparent;
            this.TransferDatepanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TransferDatepanel.Controls.Add(this.TransferDateCalenderButton);
            this.TransferDatepanel.Controls.Add(this.TransferDateTextBox);
            this.TransferDatepanel.Controls.Add(this.TransferDateLabel);
            this.TransferDatepanel.Location = new System.Drawing.Point(625, 160);
            this.TransferDatepanel.Name = "TransferDatepanel";
            this.TransferDatepanel.Size = new System.Drawing.Size(142, 41);
            this.TransferDatepanel.TabIndex = 12;
            // 
            // TransferDateCalenderButton
            // 
            this.TransferDateCalenderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TransferDateCalenderButton.Image = ((System.Drawing.Image)(resources.GetObject("TransferDateCalenderButton.Image")));
            this.TransferDateCalenderButton.Location = new System.Drawing.Point(115, 14);
            this.TransferDateCalenderButton.Name = "TransferDateCalenderButton";
            this.TransferDateCalenderButton.Size = new System.Drawing.Size(19, 20);
            this.TransferDateCalenderButton.TabIndex = 13;
            this.TransferDateCalenderButton.Tag = "TransferDateTextBox";
            this.TransferDateCalenderButton.UseVisualStyleBackColor = true;
            this.TransferDateCalenderButton.Click += new System.EventHandler(this.TransferDateCalenderButton_Click);
            // 
            // TransferDateTextBox
            // 
            this.TransferDateTextBox.AllowClick = true;
            this.TransferDateTextBox.AllowNegativeSign = false;
            this.TransferDateTextBox.ApplyCFGFormat = false;
            this.TransferDateTextBox.ApplyCurrencyFormat = false;
            this.TransferDateTextBox.ApplyFocusColor = true;
            this.TransferDateTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.TransferDateTextBox.ApplyNegativeStandard = true;
            this.TransferDateTextBox.ApplyParentFocusColor = true;
            this.TransferDateTextBox.ApplyTimeFormat = false;
            this.TransferDateTextBox.BackColor = System.Drawing.Color.White;
            this.TransferDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TransferDateTextBox.CFromatWihoutSymbol = false;
            this.TransferDateTextBox.CheckForEmpty = false;
            this.TransferDateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TransferDateTextBox.Digits = -1;
            this.TransferDateTextBox.EmptyDecimalValue = false;
            this.TransferDateTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.TransferDateTextBox.ForeColor = System.Drawing.Color.Black;
            this.TransferDateTextBox.IsEditable = false;
            this.TransferDateTextBox.IsQueryableFileld = false;
            this.TransferDateTextBox.Location = new System.Drawing.Point(5, 18);
            this.TransferDateTextBox.LockKeyPress = true;
            this.TransferDateTextBox.MaxLength = 12;
            this.TransferDateTextBox.Name = "TransferDateTextBox";
            this.TransferDateTextBox.PersistDefaultColor = false;
            this.TransferDateTextBox.Precision = 2;
            this.TransferDateTextBox.QueryingFileldName = "";
            this.TransferDateTextBox.ReadOnly = true;
            this.TransferDateTextBox.SetColorFlag = false;
            this.TransferDateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.TransferDateTextBox.Size = new System.Drawing.Size(110, 16);
            this.TransferDateTextBox.SpecialCharacter = "%";
            this.TransferDateTextBox.TabIndex = 1;
            this.TransferDateTextBox.TabStop = false;
            this.TransferDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TransferDateTextBox.TextCustomFormat = "$#,##0.00";
            this.TransferDateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.TransferDateTextBox.WholeInteger = false;
            this.TransferDateTextBox.TextChanged += new System.EventHandler(this.TransferDateTextBox_TextChanged);
            this.TransferDateTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.TransferDateTextBox_Validating);
            // 
            // TransferDateLabel
            // 
            this.TransferDateLabel.AutoSize = true;
            this.TransferDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.TransferDateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.TransferDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TransferDateLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TransferDateLabel.Location = new System.Drawing.Point(1, -1);
            this.TransferDateLabel.Name = "TransferDateLabel";
            this.TransferDateLabel.Size = new System.Drawing.Size(84, 14);
            this.TransferDateLabel.TabIndex = 0;
            this.TransferDateLabel.Text = "Transfer Date:";
            // 
            // ReceiptNumberPanel
            // 
            this.ReceiptNumberPanel.BackColor = System.Drawing.Color.Transparent;
            this.ReceiptNumberPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReceiptNumberPanel.Controls.Add(this.ReceiptNumberTextBox);
            this.ReceiptNumberPanel.Controls.Add(this.ReceiptNumberLabel);
            this.ReceiptNumberPanel.Location = new System.Drawing.Point(371, 160);
            this.ReceiptNumberPanel.Name = "ReceiptNumberPanel";
            this.ReceiptNumberPanel.Size = new System.Drawing.Size(255, 41);
            this.ReceiptNumberPanel.TabIndex = 11;
            // 
            // ReceiptNumberTextBox
            // 
            this.ReceiptNumberTextBox.AllowClick = true;
            this.ReceiptNumberTextBox.AllowNegativeSign = false;
            this.ReceiptNumberTextBox.ApplyCFGFormat = false;
            this.ReceiptNumberTextBox.ApplyCurrencyFormat = false;
            this.ReceiptNumberTextBox.ApplyFocusColor = true;
            this.ReceiptNumberTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.ReceiptNumberTextBox.ApplyNegativeStandard = true;
            this.ReceiptNumberTextBox.ApplyParentFocusColor = true;
            this.ReceiptNumberTextBox.ApplyTimeFormat = false;
            this.ReceiptNumberTextBox.BackColor = System.Drawing.Color.White;
            this.ReceiptNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReceiptNumberTextBox.CFromatWihoutSymbol = false;
            this.ReceiptNumberTextBox.CheckForEmpty = false;
            this.ReceiptNumberTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ReceiptNumberTextBox.Digits = -1;
            this.ReceiptNumberTextBox.EmptyDecimalValue = false;
            this.ReceiptNumberTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.ReceiptNumberTextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.ReceiptNumberTextBox.IsEditable = false;
            this.ReceiptNumberTextBox.IsQueryableFileld = true;
            this.ReceiptNumberTextBox.Location = new System.Drawing.Point(17, 18);
            this.ReceiptNumberTextBox.LockKeyPress = true;
            this.ReceiptNumberTextBox.MaxLength = 50;
            this.ReceiptNumberTextBox.Name = "ReceiptNumberTextBox";
            this.ReceiptNumberTextBox.PersistDefaultColor = false;
            this.ReceiptNumberTextBox.Precision = 2;
            this.ReceiptNumberTextBox.QueryingFileldName = "";
            this.ReceiptNumberTextBox.ReadOnly = true;
            this.ReceiptNumberTextBox.SetColorFlag = false;
            this.ReceiptNumberTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ReceiptNumberTextBox.Size = new System.Drawing.Size(226, 16);
            this.ReceiptNumberTextBox.SpecialCharacter = "%";
            this.ReceiptNumberTextBox.TabIndex = 16;
            this.ReceiptNumberTextBox.TabStop = false;
            this.ReceiptNumberTextBox.TextCustomFormat = "$#,##0.00";
            this.ReceiptNumberTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.ReceiptNumberTextBox.WholeInteger = false;
            // 
            // ReceiptNumberLabel
            // 
            this.ReceiptNumberLabel.AutoSize = true;
            this.ReceiptNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.ReceiptNumberLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.ReceiptNumberLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ReceiptNumberLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ReceiptNumberLabel.Location = new System.Drawing.Point(1, -1);
            this.ReceiptNumberLabel.Name = "ReceiptNumberLabel";
            this.ReceiptNumberLabel.Size = new System.Drawing.Size(98, 14);
            this.ReceiptNumberLabel.TabIndex = 0;
            this.ReceiptNumberLabel.Text = "Receipt Number:";
            // 
            // EnteredBypanel
            // 
            this.EnteredBypanel.BackColor = System.Drawing.Color.Transparent;
            this.EnteredBypanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EnteredBypanel.Controls.Add(this.EnteredByTextBox);
            this.EnteredBypanel.Controls.Add(this.EnteredByLabel);
            this.EnteredBypanel.Location = new System.Drawing.Point(194, 160);
            this.EnteredBypanel.Name = "EnteredBypanel";
            this.EnteredBypanel.Size = new System.Drawing.Size(178, 41);
            this.EnteredBypanel.TabIndex = 10;
            // 
            // EnteredByTextBox
            // 
            this.EnteredByTextBox.AllowClick = true;
            this.EnteredByTextBox.AllowNegativeSign = false;
            this.EnteredByTextBox.ApplyCFGFormat = false;
            this.EnteredByTextBox.ApplyCurrencyFormat = false;
            this.EnteredByTextBox.ApplyFocusColor = true;
            this.EnteredByTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.EnteredByTextBox.ApplyNegativeStandard = true;
            this.EnteredByTextBox.ApplyParentFocusColor = true;
            this.EnteredByTextBox.ApplyTimeFormat = false;
            this.EnteredByTextBox.BackColor = System.Drawing.Color.White;
            this.EnteredByTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EnteredByTextBox.CFromatWihoutSymbol = false;
            this.EnteredByTextBox.CheckForEmpty = false;
            this.EnteredByTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnteredByTextBox.Digits = -1;
            this.EnteredByTextBox.EmptyDecimalValue = false;
            this.EnteredByTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.EnteredByTextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.EnteredByTextBox.IsEditable = false;
            this.EnteredByTextBox.IsQueryableFileld = true;
            this.EnteredByTextBox.Location = new System.Drawing.Point(6, 18);
            this.EnteredByTextBox.LockKeyPress = true;
            this.EnteredByTextBox.MaxLength = 20;
            this.EnteredByTextBox.Name = "EnteredByTextBox";
            this.EnteredByTextBox.PersistDefaultColor = false;
            this.EnteredByTextBox.Precision = 2;
            this.EnteredByTextBox.QueryingFileldName = "";
            this.EnteredByTextBox.ReadOnly = true;
            this.EnteredByTextBox.SetColorFlag = false;
            this.EnteredByTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.EnteredByTextBox.Size = new System.Drawing.Size(164, 16);
            this.EnteredByTextBox.SpecialCharacter = "%";
            this.EnteredByTextBox.TabIndex = 14;
            this.EnteredByTextBox.TabStop = false;
            this.EnteredByTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.EnteredByTextBox.TextCustomFormat = "$#,##0.00";
            this.EnteredByTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.EnteredByTextBox.WholeInteger = false;
            // 
            // EnteredByLabel
            // 
            this.EnteredByLabel.AutoSize = true;
            this.EnteredByLabel.BackColor = System.Drawing.Color.Transparent;
            this.EnteredByLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.EnteredByLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.EnteredByLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.EnteredByLabel.Location = new System.Drawing.Point(1, -1);
            this.EnteredByLabel.Name = "EnteredByLabel";
            this.EnteredByLabel.Size = new System.Drawing.Size(69, 14);
            this.EnteredByLabel.TabIndex = 0;
            this.EnteredByLabel.Text = "Entered By:";
            // 
            // Postedpanel
            // 
            this.Postedpanel.BackColor = System.Drawing.Color.Transparent;
            this.Postedpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Postedpanel.Controls.Add(this.PostedTextBox);
            this.Postedpanel.Controls.Add(this.PostedLabel);
            this.Postedpanel.Location = new System.Drawing.Point(0, 160);
            this.Postedpanel.Name = "Postedpanel";
            this.Postedpanel.Size = new System.Drawing.Size(195, 41);
            this.Postedpanel.TabIndex = 9;
            // 
            // PostedTextBox
            // 
            this.PostedTextBox.AllowClick = true;
            this.PostedTextBox.AllowNegativeSign = false;
            this.PostedTextBox.ApplyCFGFormat = false;
            this.PostedTextBox.ApplyCurrencyFormat = true;
            this.PostedTextBox.ApplyFocusColor = true;
            this.PostedTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.PostedTextBox.ApplyNegativeStandard = true;
            this.PostedTextBox.ApplyParentFocusColor = true;
            this.PostedTextBox.ApplyTimeFormat = false;
            this.PostedTextBox.BackColor = System.Drawing.Color.White;
            this.PostedTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PostedTextBox.CFromatWihoutSymbol = false;
            this.PostedTextBox.CheckForEmpty = false;
            this.PostedTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PostedTextBox.Digits = -1;
            this.PostedTextBox.EmptyDecimalValue = false;
            this.PostedTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.PostedTextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.PostedTextBox.IsEditable = false;
            this.PostedTextBox.IsQueryableFileld = true;
            this.PostedTextBox.Location = new System.Drawing.Point(7, 18);
            this.PostedTextBox.LockKeyPress = true;
            this.PostedTextBox.MaxLength = 35;
            this.PostedTextBox.Name = "PostedTextBox";
            this.PostedTextBox.PersistDefaultColor = false;
            this.PostedTextBox.Precision = 2;
            this.PostedTextBox.QueryingFileldName = "";
            this.PostedTextBox.ReadOnly = true;
            this.PostedTextBox.SetColorFlag = false;
            this.PostedTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.PostedTextBox.Size = new System.Drawing.Size(179, 16);
            this.PostedTextBox.SpecialCharacter = "%";
            this.PostedTextBox.TabIndex = 12;
            this.PostedTextBox.TabStop = false;
            this.PostedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PostedTextBox.TextCustomFormat = "$#,##0.00";
            this.PostedTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.PostedTextBox.WholeInteger = false;
            // 
            // PostedLabel
            // 
            this.PostedLabel.AutoSize = true;
            this.PostedLabel.BackColor = System.Drawing.Color.Transparent;
            this.PostedLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.PostedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.PostedLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PostedLabel.Location = new System.Drawing.Point(1, -1);
            this.PostedLabel.Name = "PostedLabel";
            this.PostedLabel.Size = new System.Drawing.Size(49, 14);
            this.PostedLabel.TabIndex = 0;
            this.PostedLabel.Text = "Posted:";
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(103, 70);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // FromAccountPanel
            // 
            this.FromAccountPanel.BackColor = System.Drawing.Color.Transparent;
            this.FromAccountPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FromAccountPanel.Controls.Add(this.FromTextBox);
            this.FromAccountPanel.Controls.Add(this.comboBox1);
            this.FromAccountPanel.Controls.Add(this.FromAccountLabel);
            this.FromAccountPanel.Location = new System.Drawing.Point(0, 80);
            this.FromAccountPanel.Name = "FromAccountPanel";
            this.FromAccountPanel.Size = new System.Drawing.Size(767, 41);
            this.FromAccountPanel.TabIndex = 7;
            // 
            // FromTextBox
            // 
            this.FromTextBox.AllowClick = true;
            this.FromTextBox.AllowNegativeSign = true;
            this.FromTextBox.ApplyCFGFormat = true;
            this.FromTextBox.ApplyCurrencyFormat = true;
            this.FromTextBox.ApplyFocusColor = true;
            this.FromTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.FromTextBox.ApplyNegativeStandard = true;
            this.FromTextBox.ApplyParentFocusColor = true;
            this.FromTextBox.ApplyTimeFormat = false;
            this.FromTextBox.BackColor = System.Drawing.Color.White;
            this.FromTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FromTextBox.CFromatWihoutSymbol = false;
            this.FromTextBox.CheckForEmpty = false;
            this.FromTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FromTextBox.Digits = -1;
            this.FromTextBox.EmptyDecimalValue = false;
            this.FromTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.FromTextBox.ForeColor = System.Drawing.Color.Black;
            this.FromTextBox.IsEditable = false;
            this.FromTextBox.IsQueryableFileld = true;
            this.FromTextBox.Location = new System.Drawing.Point(15, 18);
            this.FromTextBox.LockKeyPress = true;
            this.FromTextBox.MaxLength = 100;
            this.FromTextBox.Name = "FromTextBox";
            this.FromTextBox.PersistDefaultColor = false;
            this.FromTextBox.Precision = 2;
            this.FromTextBox.QueryingFileldName = "";
            this.FromTextBox.ReadOnly = true;
            this.FromTextBox.SetColorFlag = false;
            this.FromTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FromTextBox.Size = new System.Drawing.Size(751, 16);
            this.FromTextBox.SpecialCharacter = "%";
            this.FromTextBox.TabIndex = 8;
            this.FromTextBox.TabStop = false;
            this.FromTextBox.TextCustomFormat = "$ #,##0.00";
            this.FromTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.FromTextBox.WholeInteger = false;
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.ContextMenuStrip = this.contextMenu;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(738, 24);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Tag = "comboBox1";
            this.comboBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBox1_MouseClick);
            this.comboBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox1_DrawItem);
            this.comboBox1.Enter += new System.EventHandler(this.comboBox1_Enter);
            this.comboBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.comboBox1_MouseDown);
            this.comboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox1_KeyPress);
            this.comboBox1.Validated += new System.EventHandler(this.comboBox1_Validated);
            this.comboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
            this.comboBox1.TextUpdate += new System.EventHandler(this.comboBox1_TextUpdate);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // FromAccountLabel
            // 
            this.FromAccountLabel.AutoSize = true;
            this.FromAccountLabel.BackColor = System.Drawing.Color.Transparent;
            this.FromAccountLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.FromAccountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.FromAccountLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.FromAccountLabel.Location = new System.Drawing.Point(1, -1);
            this.FromAccountLabel.Name = "FromAccountLabel";
            this.FromAccountLabel.Size = new System.Drawing.Size(87, 14);
            this.FromAccountLabel.TabIndex = 0;
            this.FromAccountLabel.Text = "From Account:";
            // 
            // TransferDescriptionpanel
            // 
            this.TransferDescriptionpanel.BackColor = System.Drawing.Color.Transparent;
            this.TransferDescriptionpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TransferDescriptionpanel.Controls.Add(this.TransferDescriptionTextBox);
            this.TransferDescriptionpanel.Controls.Add(this.TransferDescriptionpanellabel);
            this.TransferDescriptionpanel.Location = new System.Drawing.Point(0, 40);
            this.TransferDescriptionpanel.Name = "TransferDescriptionpanel";
            this.TransferDescriptionpanel.Size = new System.Drawing.Size(625, 41);
            this.TransferDescriptionpanel.TabIndex = 5;
            // 
            // TransferDescriptionTextBox
            // 
            this.TransferDescriptionTextBox.AllowClick = true;
            this.TransferDescriptionTextBox.AllowNegativeSign = false;
            this.TransferDescriptionTextBox.ApplyCFGFormat = false;
            this.TransferDescriptionTextBox.ApplyCurrencyFormat = true;
            this.TransferDescriptionTextBox.ApplyFocusColor = true;
            this.TransferDescriptionTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.TransferDescriptionTextBox.ApplyNegativeStandard = true;
            this.TransferDescriptionTextBox.ApplyParentFocusColor = true;
            this.TransferDescriptionTextBox.ApplyTimeFormat = false;
            this.TransferDescriptionTextBox.BackColor = System.Drawing.Color.White;
            this.TransferDescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TransferDescriptionTextBox.CFromatWihoutSymbol = false;
            this.TransferDescriptionTextBox.CheckForEmpty = false;
            this.TransferDescriptionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TransferDescriptionTextBox.Digits = -1;
            this.TransferDescriptionTextBox.EmptyDecimalValue = false;
            this.TransferDescriptionTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.TransferDescriptionTextBox.ForeColor = System.Drawing.Color.Black;
            this.TransferDescriptionTextBox.IsEditable = false;
            this.TransferDescriptionTextBox.IsQueryableFileld = true;
            this.TransferDescriptionTextBox.Location = new System.Drawing.Point(33, 18);
            this.TransferDescriptionTextBox.LockKeyPress = true;
            this.TransferDescriptionTextBox.MaxLength = 100;
            this.TransferDescriptionTextBox.Name = "TransferDescriptionTextBox";
            this.TransferDescriptionTextBox.PersistDefaultColor = false;
            this.TransferDescriptionTextBox.Precision = 2;
            this.TransferDescriptionTextBox.QueryingFileldName = "";
            this.TransferDescriptionTextBox.ReadOnly = true;
            this.TransferDescriptionTextBox.SetColorFlag = false;
            this.TransferDescriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.TransferDescriptionTextBox.Size = new System.Drawing.Size(565, 16);
            this.TransferDescriptionTextBox.SpecialCharacter = "%";
            this.TransferDescriptionTextBox.TabIndex = 2;
            this.TransferDescriptionTextBox.TabStop = false;
            this.TransferDescriptionTextBox.TextCustomFormat = "$ #,##0.00";
            this.TransferDescriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.TransferDescriptionTextBox.WholeInteger = false;
            // 
            // TransferDescriptionpanellabel
            // 
            this.TransferDescriptionpanellabel.AutoSize = true;
            this.TransferDescriptionpanellabel.BackColor = System.Drawing.Color.Transparent;
            this.TransferDescriptionpanellabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.TransferDescriptionpanellabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TransferDescriptionpanellabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TransferDescriptionpanellabel.Location = new System.Drawing.Point(1, -1);
            this.TransferDescriptionpanellabel.Name = "TransferDescriptionpanellabel";
            this.TransferDescriptionpanellabel.Size = new System.Drawing.Size(123, 14);
            this.TransferDescriptionpanellabel.TabIndex = 0;
            this.TransferDescriptionpanellabel.Text = "Transfer Description:";
            // 
            // TransferAmountpanel
            // 
            this.TransferAmountpanel.BackColor = System.Drawing.Color.Transparent;
            this.TransferAmountpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TransferAmountpanel.Controls.Add(this.TransferAmountTextBox);
            this.TransferAmountpanel.Controls.Add(this.TransferAmountLabel);
            this.TransferAmountpanel.Location = new System.Drawing.Point(624, 40);
            this.TransferAmountpanel.Name = "TransferAmountpanel";
            this.TransferAmountpanel.Size = new System.Drawing.Size(143, 41);
            this.TransferAmountpanel.TabIndex = 6;
            // 
            // TransferAmountTextBox
            // 
            this.TransferAmountTextBox.AllowClick = true;
            this.TransferAmountTextBox.AllowNegativeSign = true;
            this.TransferAmountTextBox.ApplyCFGFormat = true;
            this.TransferAmountTextBox.ApplyCurrencyFormat = true;
            this.TransferAmountTextBox.ApplyFocusColor = true;
            this.TransferAmountTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.TransferAmountTextBox.ApplyNegativeStandard = true;
            this.TransferAmountTextBox.ApplyParentFocusColor = true;
            this.TransferAmountTextBox.ApplyTimeFormat = false;
            this.TransferAmountTextBox.BackColor = System.Drawing.Color.White;
            this.TransferAmountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TransferAmountTextBox.CFromatWihoutSymbol = false;
            this.TransferAmountTextBox.CheckForEmpty = false;
            this.TransferAmountTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TransferAmountTextBox.Digits = -1;
            this.TransferAmountTextBox.EmptyDecimalValue = false;
            this.TransferAmountTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.TransferAmountTextBox.ForeColor = System.Drawing.Color.Black;
            this.TransferAmountTextBox.IsEditable = false;
            this.TransferAmountTextBox.IsQueryableFileld = true;
            this.TransferAmountTextBox.Location = new System.Drawing.Point(5, 18);
            this.TransferAmountTextBox.LockKeyPress = true;
            this.TransferAmountTextBox.MaxLength = 25;
            this.TransferAmountTextBox.Name = "TransferAmountTextBox";
            this.TransferAmountTextBox.PersistDefaultColor = false;
            this.TransferAmountTextBox.Precision = 2;
            this.TransferAmountTextBox.QueryingFileldName = "";
            this.TransferAmountTextBox.ReadOnly = true;
            this.TransferAmountTextBox.SetColorFlag = false;
            this.TransferAmountTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.TransferAmountTextBox.Size = new System.Drawing.Size(130, 16);
            this.TransferAmountTextBox.SpecialCharacter = "%";
            this.TransferAmountTextBox.TabIndex = 4;
            this.TransferAmountTextBox.TabStop = false;
            this.TransferAmountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TransferAmountTextBox.TextCustomFormat = "$ #,##0.00";
            this.TransferAmountTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.TransferAmountTextBox.WholeInteger = false;
            // 
            // TransferAmountLabel
            // 
            this.TransferAmountLabel.AutoSize = true;
            this.TransferAmountLabel.BackColor = System.Drawing.Color.Transparent;
            this.TransferAmountLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.TransferAmountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TransferAmountLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TransferAmountLabel.Location = new System.Drawing.Point(1, -1);
            this.TransferAmountLabel.Name = "TransferAmountLabel";
            this.TransferAmountLabel.Size = new System.Drawing.Size(104, 14);
            this.TransferAmountLabel.TabIndex = 0;
            this.TransferAmountLabel.Text = "Transfer Amount:";
            // 
            // StartNewpanel
            // 
            this.StartNewpanel.BackColor = System.Drawing.Color.Gray;
            this.StartNewpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartNewpanel.Controls.Add(this.checkBoxPanel);
            this.StartNewpanel.Controls.Add(this.startNewLabel);
            this.StartNewpanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.StartNewpanel.Location = new System.Drawing.Point(0, 0);
            this.StartNewpanel.Name = "StartNewpanel";
            this.StartNewpanel.Size = new System.Drawing.Size(767, 41);
            this.StartNewpanel.TabIndex = 0;
            // 
            // checkBoxPanel
            // 
            this.checkBoxPanel.BackColor = System.Drawing.Color.Gray;
            this.checkBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkBoxPanel.Controls.Add(this.StartNewcheckBox);
            this.checkBoxPanel.Location = new System.Drawing.Point(17, 11);
            this.checkBoxPanel.Name = "checkBoxPanel";
            this.checkBoxPanel.Size = new System.Drawing.Size(14, 14);
            this.checkBoxPanel.TabIndex = 26;
            // 
            // StartNewcheckBox
            // 
            this.StartNewcheckBox.AutoSize = true;
            this.StartNewcheckBox.BackColor = System.Drawing.Color.White;
            this.StartNewcheckBox.ForeColor = System.Drawing.Color.Black;
            this.StartNewcheckBox.Location = new System.Drawing.Point(0, 0);
            this.StartNewcheckBox.Name = "StartNewcheckBox";
            this.StartNewcheckBox.Size = new System.Drawing.Size(15, 14);
            this.StartNewcheckBox.TabIndex = 3;
            this.StartNewcheckBox.UseVisualStyleBackColor = true;
            this.StartNewcheckBox.CheckStateChanged += new System.EventHandler(this.StartNewcheckBox_CheckStateChanged);
            this.StartNewcheckBox.CheckedChanged += new System.EventHandler(this.StartNewcheckBox_CheckedChanged);
            // 
            // startNewLabel
            // 
            this.startNewLabel.AutoSize = true;
            this.startNewLabel.BackColor = System.Drawing.Color.Transparent;
            this.startNewLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.startNewLabel.ForeColor = System.Drawing.Color.Black;
            this.startNewLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.startNewLabel.Location = new System.Drawing.Point(36, 12);
            this.startNewLabel.Name = "startNewLabel";
            this.startNewLabel.Size = new System.Drawing.Size(195, 14);
            this.startNewLabel.TabIndex = 4;
            this.startNewLabel.Text = "Start New with current To Account";
            // 
            // TransfersPictureBox
            // 
            this.TransfersPictureBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TransfersPictureBox.Location = new System.Drawing.Point(760, 0);
            this.TransfersPictureBox.Name = "TransfersPictureBox";
            this.TransfersPictureBox.Size = new System.Drawing.Size(42, 201);
            this.TransfersPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TransfersPictureBox.TabIndex = 25;
            this.TransfersPictureBox.TabStop = false;
            this.TransfersPictureBox.Click += new System.EventHandler(this.TransfersPictureBox_Click);
            this.TransfersPictureBox.MouseEnter += new System.EventHandler(this.TransfersPictureBox_MouseEnter);
            // 
            // ToTextBox
            // 
            this.ToTextBox.AllowClick = true;
            this.ToTextBox.AllowNegativeSign = false;
            this.ToTextBox.ApplyCFGFormat = false;
            this.ToTextBox.ApplyCurrencyFormat = true;
            this.ToTextBox.ApplyFocusColor = true;
            this.ToTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.ToTextBox.ApplyNegativeStandard = true;
            this.ToTextBox.ApplyParentFocusColor = true;
            this.ToTextBox.ApplyTimeFormat = false;
            this.ToTextBox.BackColor = System.Drawing.Color.White;
            this.ToTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ToTextBox.CFromatWihoutSymbol = false;
            this.ToTextBox.CheckForEmpty = false;
            this.ToTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ToTextBox.Digits = -1;
            this.ToTextBox.EmptyDecimalValue = false;
            this.ToTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.ToTextBox.ForeColor = System.Drawing.Color.Black;
            this.ToTextBox.IsEditable = false;
            this.ToTextBox.IsQueryableFileld = true;
            this.ToTextBox.Location = new System.Drawing.Point(15, 18);
            this.ToTextBox.LockKeyPress = true;
            this.ToTextBox.MaxLength = 100;
            this.ToTextBox.Name = "ToTextBox";
            this.ToTextBox.PersistDefaultColor = false;
            this.ToTextBox.Precision = 2;
            this.ToTextBox.QueryingFileldName = "";
            this.ToTextBox.ReadOnly = true;
            this.ToTextBox.SetColorFlag = false;
            this.ToTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ToTextBox.Size = new System.Drawing.Size(751, 16);
            this.ToTextBox.SpecialCharacter = "%";
            this.ToTextBox.TabIndex = 11;
            this.ToTextBox.TabStop = false;
            this.ToTextBox.TextCustomFormat = "$ #,##0.00";
            this.ToTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.ToTextBox.WholeInteger = false;
            // 
            // ToAccounttpanel
            // 
            this.ToAccounttpanel.BackColor = System.Drawing.Color.Transparent;
            this.ToAccounttpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ToAccounttpanel.Controls.Add(this.ToTextBox);
            this.ToAccounttpanel.Controls.Add(this.comboBox2);
            this.ToAccounttpanel.Controls.Add(this.ToAccountLabel);
            this.ToAccounttpanel.Location = new System.Drawing.Point(0, 120);
            this.ToAccounttpanel.Name = "ToAccounttpanel";
            this.ToAccounttpanel.Size = new System.Drawing.Size(767, 41);
            this.ToAccounttpanel.TabIndex = 26;
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox2.BackColor = System.Drawing.Color.White;
            this.comboBox2.ContextMenuStrip = this.contextMenu;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox2.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(15, 12);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(738, 24);
            this.comboBox2.TabIndex = 10;
            this.comboBox2.Tag = "comboBox2";
            this.comboBox2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.comboBox2_MouseDoubleClick);
            this.comboBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBox2_MouseClick);
            this.comboBox2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox2_DrawItem);
            this.comboBox2.Leave += new System.EventHandler(this.comboBox2_Leave);
            this.comboBox2.MouseEnter += new System.EventHandler(this.comboBox2_MouseEnter);
            this.comboBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.comboBox2_MouseDown);
            this.comboBox2.Validated += new System.EventHandler(this.comboBox2_Validated);
            this.comboBox2.TextUpdate += new System.EventHandler(this.comboBox2_TextUpdate);
            this.comboBox2.Click += new System.EventHandler(this.comboBox2_Click);
            // 
            // ToAccountLabel
            // 
            this.ToAccountLabel.AutoSize = true;
            this.ToAccountLabel.BackColor = System.Drawing.Color.Transparent;
            this.ToAccountLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.ToAccountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ToAccountLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ToAccountLabel.Location = new System.Drawing.Point(1, -1);
            this.ToAccountLabel.Name = "ToAccountLabel";
            this.ToAccountLabel.Size = new System.Drawing.Size(71, 14);
            this.ToAccountLabel.TabIndex = 0;
            this.ToAccountLabel.Text = "To Account:";
            // 
            // F15019
            // 
            this.AccessibleName = "JournalEntry";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.JournalEntrypanel);
            this.Name = "F15019";
            this.Size = new System.Drawing.Size(825, 377);
            this.Tag = "15019";
            this.Load += new System.EventHandler(this.F15019_Load);
            this.JournalEntrypanel.ResumeLayout(false);
            this.TransferDatepanel.ResumeLayout(false);
            this.TransferDatepanel.PerformLayout();
            this.ReceiptNumberPanel.ResumeLayout(false);
            this.ReceiptNumberPanel.PerformLayout();
            this.EnteredBypanel.ResumeLayout(false);
            this.EnteredBypanel.PerformLayout();
            this.Postedpanel.ResumeLayout(false);
            this.Postedpanel.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.FromAccountPanel.ResumeLayout(false);
            this.FromAccountPanel.PerformLayout();
            this.TransferDescriptionpanel.ResumeLayout(false);
            this.TransferDescriptionpanel.PerformLayout();
            this.TransferAmountpanel.ResumeLayout(false);
            this.TransferAmountpanel.PerformLayout();
            this.StartNewpanel.ResumeLayout(false);
            this.StartNewpanel.PerformLayout();
            this.checkBoxPanel.ResumeLayout(false);
            this.checkBoxPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TransfersPictureBox)).EndInit();
            this.ToAccounttpanel.ResumeLayout(false);
            this.ToAccounttpanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel JournalEntrypanel;
        private System.Windows.Forms.Panel TransferAmountpanel;
        private System.Windows.Forms.Label TransferAmountLabel;
        private System.Windows.Forms.Panel TransferDescriptionpanel;
        private System.Windows.Forms.Label TransferDescriptionpanellabel;
        private System.Windows.Forms.Panel FromAccountPanel;
        private System.Windows.Forms.Label FromAccountLabel;
        private System.Windows.Forms.Panel Postedpanel;
        private System.Windows.Forms.Label PostedLabel;
        private System.Windows.Forms.Panel EnteredBypanel;
        private System.Windows.Forms.Label EnteredByLabel;
        private System.Windows.Forms.Panel ReceiptNumberPanel;
        private System.Windows.Forms.Label ReceiptNumberLabel;
        private System.Windows.Forms.Panel TransferDatepanel;
        private System.Windows.Forms.Label TransferDateLabel;
        private System.Windows.Forms.PictureBox TransfersPictureBox;
        private TerraScan.UI.Controls.TerraScanTextBox TransferAmountTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox TransferDescriptionTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox PostedTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox EnteredByTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox ReceiptNumberTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox TransferDateTextBox;
        private System.Windows.Forms.ToolTip JournalEntryToolTip;
        private System.Windows.Forms.Button TransferDateCalenderButton;
        private TerraScan.UI.Controls.TerraScanMonthCalender ReceiptMonthCalender;
        private System.Windows.Forms.Panel StartNewpanel;
        private TerraScan.UI.Controls.TerraScanCheckBox StartNewcheckBox;
        private TerraScan.UI.Controls.TerraScanComboBox  comboBox1;
        private System.Windows.Forms.Panel checkBoxPanel;
        private System.Windows.Forms.Label startNewLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private TerraScan.UI.Controls.TerraScanTextBox FromTextBox;
        private System.Windows.Forms.Panel ToAccounttpanel;
        private TerraScan.UI.Controls.TerraScanTextBox ToTextBox;
        private TerraScan.UI.Controls.TerraScanComboBox comboBox2;
        private System.Windows.Forms.Label ToAccountLabel;   
               
    }
}
