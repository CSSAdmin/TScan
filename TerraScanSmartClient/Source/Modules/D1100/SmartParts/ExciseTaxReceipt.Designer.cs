namespace D1100
{
    partial class ExciseTaxReceipt
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExciseTaxReceipt));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ErrorGridPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ErrorGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeeAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorAmountTotalTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.terraScanTextBox1 = new TerraScan.UI.Controls.TerraScanTextBox();
            this.terraScanTextBox2 = new TerraScan.UI.Controls.TerraScanTextBox();
            this.PaymentsTotalTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.BalanceAmountTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.ValidEntriesTotalTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.BalanceLabel = new System.Windows.Forms.Label();
            this.PaymentTotalLabel = new System.Windows.Forms.Label();
            this.TotalDueLabel = new System.Windows.Forms.Label();
            this.CancelReceiptButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SaveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.NewButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AuditLinkLabel = new System.Windows.Forms.LinkLabel();
            this.PaymentPictureBox = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.paymentEngineUserControl1 = new TerraScan.PaymentEngine.PaymentEngineUserControl();
            this.CommentButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AttachmentButton = new TerraScan.UI.Controls.TerraScanButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.ErrorGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentPictureBox)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ErrorGridPanel);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 109);
            this.panel1.TabIndex = 13;
            this.panel1.TabStop = true;
            // 
            // ErrorGridPanel
            // 
            this.ErrorGridPanel.BackColor = System.Drawing.Color.Silver;
            this.ErrorGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ErrorGridPanel.Controls.Add(this.label3);
            this.ErrorGridPanel.Controls.Add(this.label2);
            this.ErrorGridPanel.Controls.Add(this.label1);
            this.ErrorGridPanel.Controls.Add(this.ErrorGridView);
            this.ErrorGridPanel.Controls.Add(this.ErrorAmountTotalTextBox);
            this.ErrorGridPanel.Controls.Add(this.terraScanTextBox1);
            this.ErrorGridPanel.Controls.Add(this.terraScanTextBox2);
            this.ErrorGridPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ErrorGridPanel.Location = new System.Drawing.Point(0, 0);
            this.ErrorGridPanel.Name = "ErrorGridPanel";
            this.ErrorGridPanel.Size = new System.Drawing.Size(616, 109);
            this.ErrorGridPanel.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 120;
            this.label3.Text = "Fee Total:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(220, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 119;
            this.label2.Text = "Fee Total:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(428, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fee Total:";
            // 
            // ErrorGridView
            // 
            this.ErrorGridView.AllowCellClick = true;
            this.ErrorGridView.AllowDoubleClick = false;
            this.ErrorGridView.AllowEmptyRows = true;
            this.ErrorGridView.AllowEnterKey = false;
            this.ErrorGridView.AllowSorting = false;
            this.ErrorGridView.AllowUserToAddRows = false;
            this.ErrorGridView.AllowUserToDeleteRows = false;
            this.ErrorGridView.AllowUserToResizeColumns = false;
            this.ErrorGridView.AllowUserToResizeRows = false;
            this.ErrorGridView.ApplyStandardBehaviour = false;
            this.ErrorGridView.BackgroundColor = System.Drawing.Color.White;
            this.ErrorGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ErrorGridView.ClearCurrentCellOnLeave = true;
            this.ErrorGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ErrorGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.ErrorGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ErrorGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item,
            this.StateRate,
            this.StateAmount,
            this.LocalRate,
            this.LocalAmount,
            this.FeeType,
            this.FeeAmount});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ErrorGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.ErrorGridView.DefaultRowIndex = 0;
            this.ErrorGridView.DeselectCurrentCell = false;
            this.ErrorGridView.DeselectSpecifiedRow = -1;
            this.ErrorGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ErrorGridView.EnableBinding = true;
            this.ErrorGridView.EnableHeadersVisualStyles = false;
            this.ErrorGridView.GridColor = System.Drawing.Color.Black;
            this.ErrorGridView.GridContentSelected = false;
            this.ErrorGridView.IsEditableGrid = false;
            this.ErrorGridView.IsMultiSelect = false;
            this.ErrorGridView.IsSorted = false;
            this.ErrorGridView.Location = new System.Drawing.Point(-2, -2);
            this.ErrorGridView.MultiSelect = false;
            this.ErrorGridView.Name = "ErrorGridView";
            this.ErrorGridView.NumRowsVisible = 5;
            this.ErrorGridView.PrimaryKeyColumnName = "";
            this.ErrorGridView.RemainSortFields = false;
            this.ErrorGridView.RemoveDefaultSelection = false;
            this.ErrorGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ErrorGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.ErrorGridView.RowHeadersVisible = false;
            this.ErrorGridView.RowHeadersWidth = 20;
            this.ErrorGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ErrorGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ErrorGridView.Size = new System.Drawing.Size(617, 91);
            this.ErrorGridView.TabIndex = 0;
            // 
            // Item
            // 
            this.Item.HeaderText = "Item";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Item.Width = 74;
            // 
            // StateRate
            // 
            this.StateRate.HeaderText = "State Rate";
            this.StateRate.Name = "StateRate";
            this.StateRate.ReadOnly = true;
            this.StateRate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.StateRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StateRate.Width = 77;
            // 
            // StateAmount
            // 
            this.StateAmount.HeaderText = "State Amount";
            this.StateAmount.Name = "StateAmount";
            this.StateAmount.ReadOnly = true;
            this.StateAmount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.StateAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StateAmount.Width = 95;
            // 
            // LocalRate
            // 
            this.LocalRate.HeaderText = "Local Rate";
            this.LocalRate.Name = "LocalRate";
            this.LocalRate.ReadOnly = true;
            this.LocalRate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.LocalRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LocalRate.Width = 80;
            // 
            // LocalAmount
            // 
            this.LocalAmount.HeaderText = "Local Amount";
            this.LocalAmount.Name = "LocalAmount";
            this.LocalAmount.ReadOnly = true;
            this.LocalAmount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.LocalAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FeeType
            // 
            this.FeeType.HeaderText = "Fee Type";
            this.FeeType.Name = "FeeType";
            this.FeeType.ReadOnly = true;
            this.FeeType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FeeType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FeeType.Width = 90;
            // 
            // FeeAmount
            // 
            this.FeeAmount.HeaderText = "Fee Amount";
            this.FeeAmount.Name = "FeeAmount";
            this.FeeAmount.ReadOnly = true;
            this.FeeAmount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FeeAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ErrorAmountTotalTextBox
            // 
            this.ErrorAmountTotalTextBox.AllowClick = true;
            this.ErrorAmountTotalTextBox.AllowNegativeSign = false;
            this.ErrorAmountTotalTextBox.ApplyCFGFormat = false;
            this.ErrorAmountTotalTextBox.ApplyCurrencyFormat = false;
            this.ErrorAmountTotalTextBox.ApplyFocusColor = true;
            this.ErrorAmountTotalTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.ErrorAmountTotalTextBox.ApplyNegativeStandard = true;
            this.ErrorAmountTotalTextBox.ApplyParentFocusColor = false;
            this.ErrorAmountTotalTextBox.ApplyTimeFormat = false;
            this.ErrorAmountTotalTextBox.BackColor = System.Drawing.Color.Silver;
            this.ErrorAmountTotalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ErrorAmountTotalTextBox.CFromatWihoutSymbol = false;
            this.ErrorAmountTotalTextBox.CheckForEmpty = false;
            this.ErrorAmountTotalTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ErrorAmountTotalTextBox.Digits = -1;
            this.ErrorAmountTotalTextBox.EmptyDecimalValue = false;
            this.ErrorAmountTotalTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorAmountTotalTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ErrorAmountTotalTextBox.IsEditable = false;
            this.ErrorAmountTotalTextBox.IsQueryableFileld = false;
            this.ErrorAmountTotalTextBox.Location = new System.Drawing.Point(514, 88);
            this.ErrorAmountTotalTextBox.LockKeyPress = true;
            this.ErrorAmountTotalTextBox.Name = "ErrorAmountTotalTextBox";
            this.ErrorAmountTotalTextBox.PersistDefaultColor = false;
            this.ErrorAmountTotalTextBox.Precision = 2;
            this.ErrorAmountTotalTextBox.QueryingFileldName = "";
            this.ErrorAmountTotalTextBox.ReadOnly = true;
            this.ErrorAmountTotalTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ErrorAmountTotalTextBox.Size = new System.Drawing.Size(101, 20);
            this.ErrorAmountTotalTextBox.SpecialCharacter = "%";
            this.ErrorAmountTotalTextBox.TabIndex = 106;
            this.ErrorAmountTotalTextBox.TabStop = false;
            this.ErrorAmountTotalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ErrorAmountTotalTextBox.TextCustomFormat = "$ #,##0.00";
            this.ErrorAmountTotalTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.ErrorAmountTotalTextBox.WholeInteger = false;
            // 
            // terraScanTextBox1
            // 
            this.terraScanTextBox1.AllowClick = true;
            this.terraScanTextBox1.AllowNegativeSign = false;
            this.terraScanTextBox1.ApplyCFGFormat = false;
            this.terraScanTextBox1.ApplyCurrencyFormat = false;
            this.terraScanTextBox1.ApplyFocusColor = true;
            this.terraScanTextBox1.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.terraScanTextBox1.ApplyNegativeStandard = true;
            this.terraScanTextBox1.ApplyParentFocusColor = false;
            this.terraScanTextBox1.ApplyTimeFormat = false;
            this.terraScanTextBox1.BackColor = System.Drawing.Color.Silver;
            this.terraScanTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.terraScanTextBox1.CFromatWihoutSymbol = false;
            this.terraScanTextBox1.CheckForEmpty = false;
            this.terraScanTextBox1.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.terraScanTextBox1.Digits = -1;
            this.terraScanTextBox1.EmptyDecimalValue = false;
            this.terraScanTextBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.terraScanTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.terraScanTextBox1.IsEditable = false;
            this.terraScanTextBox1.IsQueryableFileld = false;
            this.terraScanTextBox1.Location = new System.Drawing.Point(297, 88);
            this.terraScanTextBox1.LockKeyPress = true;
            this.terraScanTextBox1.Name = "terraScanTextBox1";
            this.terraScanTextBox1.PersistDefaultColor = false;
            this.terraScanTextBox1.Precision = 2;
            this.terraScanTextBox1.QueryingFileldName = "";
            this.terraScanTextBox1.ReadOnly = true;
            this.terraScanTextBox1.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.terraScanTextBox1.Size = new System.Drawing.Size(101, 20);
            this.terraScanTextBox1.SpecialCharacter = "%";
            this.terraScanTextBox1.TabIndex = 117;
            this.terraScanTextBox1.TabStop = false;
            this.terraScanTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.terraScanTextBox1.TextCustomFormat = "$ #,##0.00";
            this.terraScanTextBox1.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.terraScanTextBox1.WholeInteger = false;
            // 
            // terraScanTextBox2
            // 
            this.terraScanTextBox2.AllowClick = true;
            this.terraScanTextBox2.AllowNegativeSign = false;
            this.terraScanTextBox2.ApplyCFGFormat = false;
            this.terraScanTextBox2.ApplyCurrencyFormat = false;
            this.terraScanTextBox2.ApplyFocusColor = true;
            this.terraScanTextBox2.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.terraScanTextBox2.ApplyNegativeStandard = true;
            this.terraScanTextBox2.ApplyParentFocusColor = false;
            this.terraScanTextBox2.ApplyTimeFormat = false;
            this.terraScanTextBox2.BackColor = System.Drawing.Color.Silver;
            this.terraScanTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.terraScanTextBox2.CFromatWihoutSymbol = false;
            this.terraScanTextBox2.CheckForEmpty = false;
            this.terraScanTextBox2.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.terraScanTextBox2.Digits = -1;
            this.terraScanTextBox2.EmptyDecimalValue = false;
            this.terraScanTextBox2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.terraScanTextBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.terraScanTextBox2.IsEditable = false;
            this.terraScanTextBox2.IsQueryableFileld = false;
            this.terraScanTextBox2.Location = new System.Drawing.Point(96, 88);
            this.terraScanTextBox2.LockKeyPress = true;
            this.terraScanTextBox2.Name = "terraScanTextBox2";
            this.terraScanTextBox2.PersistDefaultColor = false;
            this.terraScanTextBox2.Precision = 2;
            this.terraScanTextBox2.QueryingFileldName = "";
            this.terraScanTextBox2.ReadOnly = true;
            this.terraScanTextBox2.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.terraScanTextBox2.Size = new System.Drawing.Size(101, 20);
            this.terraScanTextBox2.SpecialCharacter = "%";
            this.terraScanTextBox2.TabIndex = 118;
            this.terraScanTextBox2.TabStop = false;
            this.terraScanTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.terraScanTextBox2.TextCustomFormat = "$ #,##0.00";
            this.terraScanTextBox2.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.terraScanTextBox2.WholeInteger = false;
            // 
            // PaymentsTotalTextBox
            // 
            this.PaymentsTotalTextBox.AllowClick = true;
            this.PaymentsTotalTextBox.AllowNegativeSign = false;
            this.PaymentsTotalTextBox.ApplyCFGFormat = false;
            this.PaymentsTotalTextBox.ApplyCurrencyFormat = false;
            this.PaymentsTotalTextBox.ApplyFocusColor = true;
            this.PaymentsTotalTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.PaymentsTotalTextBox.ApplyNegativeStandard = true;
            this.PaymentsTotalTextBox.ApplyParentFocusColor = true;
            this.PaymentsTotalTextBox.ApplyTimeFormat = false;
            this.PaymentsTotalTextBox.BackColor = System.Drawing.Color.LightSteelBlue;
            this.PaymentsTotalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PaymentsTotalTextBox.CFromatWihoutSymbol = false;
            this.PaymentsTotalTextBox.CheckForEmpty = false;
            this.PaymentsTotalTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PaymentsTotalTextBox.Digits = -1;
            this.PaymentsTotalTextBox.EmptyDecimalValue = false;
            this.PaymentsTotalTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold);
            this.PaymentsTotalTextBox.IsEditable = false;
            this.PaymentsTotalTextBox.IsQueryableFileld = false;
            this.PaymentsTotalTextBox.Location = new System.Drawing.Point(0, 0);
            this.PaymentsTotalTextBox.LockKeyPress = true;
            this.PaymentsTotalTextBox.Name = "PaymentsTotalTextBox";
            this.PaymentsTotalTextBox.PersistDefaultColor = false;
            this.PaymentsTotalTextBox.Precision = 2;
            this.PaymentsTotalTextBox.QueryingFileldName = "";
            this.PaymentsTotalTextBox.ReadOnly = true;
            this.PaymentsTotalTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.PaymentsTotalTextBox.Size = new System.Drawing.Size(101, 20);
            this.PaymentsTotalTextBox.SpecialCharacter = "%";
            this.PaymentsTotalTextBox.TabIndex = 119;
            this.PaymentsTotalTextBox.TabStop = false;
            this.PaymentsTotalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PaymentsTotalTextBox.TextCustomFormat = "$ #,##0.00";
            this.PaymentsTotalTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.PaymentsTotalTextBox.WholeInteger = false;
            // 
            // BalanceAmountTextBox
            // 
            this.BalanceAmountTextBox.AllowClick = true;
            this.BalanceAmountTextBox.AllowNegativeSign = false;
            this.BalanceAmountTextBox.ApplyCFGFormat = false;
            this.BalanceAmountTextBox.ApplyCurrencyFormat = false;
            this.BalanceAmountTextBox.ApplyFocusColor = true;
            this.BalanceAmountTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.BalanceAmountTextBox.ApplyNegativeStandard = true;
            this.BalanceAmountTextBox.ApplyParentFocusColor = true;
            this.BalanceAmountTextBox.ApplyTimeFormat = false;
            this.BalanceAmountTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(189)))), ((int)(((byte)(140)))));
            this.BalanceAmountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BalanceAmountTextBox.CFromatWihoutSymbol = false;
            this.BalanceAmountTextBox.CheckForEmpty = false;
            this.BalanceAmountTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.BalanceAmountTextBox.Digits = -1;
            this.BalanceAmountTextBox.EmptyDecimalValue = false;
            this.BalanceAmountTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold);
            this.BalanceAmountTextBox.IsEditable = false;
            this.BalanceAmountTextBox.IsQueryableFileld = false;
            this.BalanceAmountTextBox.Location = new System.Drawing.Point(0, 0);
            this.BalanceAmountTextBox.LockKeyPress = true;
            this.BalanceAmountTextBox.Multiline = true;
            this.BalanceAmountTextBox.Name = "BalanceAmountTextBox";
            this.BalanceAmountTextBox.PersistDefaultColor = false;
            this.BalanceAmountTextBox.Precision = 2;
            this.BalanceAmountTextBox.QueryingFileldName = "";
            this.BalanceAmountTextBox.ReadOnly = true;
            this.BalanceAmountTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.BalanceAmountTextBox.Size = new System.Drawing.Size(101, 23);
            this.BalanceAmountTextBox.SpecialCharacter = "%";
            this.BalanceAmountTextBox.TabIndex = 118;
            this.BalanceAmountTextBox.TabStop = false;
            this.BalanceAmountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.BalanceAmountTextBox.TextCustomFormat = "$ #,##0.00";
            this.BalanceAmountTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.BalanceAmountTextBox.WholeInteger = false;
            // 
            // ValidEntriesTotalTextBox
            // 
            this.ValidEntriesTotalTextBox.AllowClick = true;
            this.ValidEntriesTotalTextBox.AllowNegativeSign = false;
            this.ValidEntriesTotalTextBox.ApplyCFGFormat = false;
            this.ValidEntriesTotalTextBox.ApplyCurrencyFormat = false;
            this.ValidEntriesTotalTextBox.ApplyFocusColor = true;
            this.ValidEntriesTotalTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.ValidEntriesTotalTextBox.ApplyNegativeStandard = true;
            this.ValidEntriesTotalTextBox.ApplyParentFocusColor = true;
            this.ValidEntriesTotalTextBox.ApplyTimeFormat = false;
            this.ValidEntriesTotalTextBox.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ValidEntriesTotalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ValidEntriesTotalTextBox.CFromatWihoutSymbol = false;
            this.ValidEntriesTotalTextBox.CheckForEmpty = false;
            this.ValidEntriesTotalTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ValidEntriesTotalTextBox.Digits = -1;
            this.ValidEntriesTotalTextBox.EmptyDecimalValue = false;
            this.ValidEntriesTotalTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold);
            this.ValidEntriesTotalTextBox.IsEditable = false;
            this.ValidEntriesTotalTextBox.IsQueryableFileld = false;
            this.ValidEntriesTotalTextBox.Location = new System.Drawing.Point(0, 0);
            this.ValidEntriesTotalTextBox.LockKeyPress = true;
            this.ValidEntriesTotalTextBox.Name = "ValidEntriesTotalTextBox";
            this.ValidEntriesTotalTextBox.PersistDefaultColor = false;
            this.ValidEntriesTotalTextBox.Precision = 2;
            this.ValidEntriesTotalTextBox.QueryingFileldName = "";
            this.ValidEntriesTotalTextBox.ReadOnly = true;
            this.ValidEntriesTotalTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ValidEntriesTotalTextBox.Size = new System.Drawing.Size(101, 20);
            this.ValidEntriesTotalTextBox.SpecialCharacter = "%";
            this.ValidEntriesTotalTextBox.TabIndex = 117;
            this.ValidEntriesTotalTextBox.TabStop = false;
            this.ValidEntriesTotalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ValidEntriesTotalTextBox.TextCustomFormat = "$ #,##0.00";
            this.ValidEntriesTotalTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.ValidEntriesTotalTextBox.WholeInteger = false;
            // 
            // BalanceLabel
            // 
            this.BalanceLabel.AutoSize = true;
            this.BalanceLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BalanceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(142)))));
            this.BalanceLabel.Location = new System.Drawing.Point(622, 130);
            this.BalanceLabel.Name = "BalanceLabel";
            this.BalanceLabel.Size = new System.Drawing.Size(53, 15);
            this.BalanceLabel.TabIndex = 122;
            this.BalanceLabel.Text = "Balance";
            // 
            // PaymentTotalLabel
            // 
            this.PaymentTotalLabel.AutoSize = true;
            this.PaymentTotalLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.PaymentTotalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(142)))));
            this.PaymentTotalLabel.Location = new System.Drawing.Point(423, 149);
            this.PaymentTotalLabel.Name = "PaymentTotalLabel";
            this.PaymentTotalLabel.Size = new System.Drawing.Size(88, 14);
            this.PaymentTotalLabel.TabIndex = 121;
            this.PaymentTotalLabel.Text = "Payment Total:";
            // 
            // TotalDueLabel
            // 
            this.TotalDueLabel.AutoSize = true;
            this.TotalDueLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.TotalDueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(142)))));
            this.TotalDueLabel.Location = new System.Drawing.Point(451, 111);
            this.TotalDueLabel.Name = "TotalDueLabel";
            this.TotalDueLabel.Size = new System.Drawing.Size(61, 14);
            this.TotalDueLabel.TabIndex = 120;
            this.TotalDueLabel.Text = "Total Due:";
            // 
            // CancelReceiptButton
            // 
            this.CancelReceiptButton.ActualPermission = false;
            this.CancelReceiptButton.ApplyDisableBehaviour = false;
            this.CancelReceiptButton.AutoEllipsis = true;
            this.CancelReceiptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelReceiptButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelReceiptButton.CommentPriority = false;
            this.CancelReceiptButton.EnableAutoPrint = false;
            this.CancelReceiptButton.FilterStatus = false;
            this.CancelReceiptButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelReceiptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelReceiptButton.FocusRectangleEnabled = true;
            this.CancelReceiptButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelReceiptButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelReceiptButton.ImageSelected = false;
            this.CancelReceiptButton.Location = new System.Drawing.Point(218, 123);
            this.CancelReceiptButton.Name = "CancelReceiptButton";
            this.CancelReceiptButton.NewPadding = 5;
            this.CancelReceiptButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.CancelReceiptButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelReceiptButton.Size = new System.Drawing.Size(93, 29);
            this.CancelReceiptButton.StatusIndicator = false;
            this.CancelReceiptButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelReceiptButton.StatusOffText = null;
            this.CancelReceiptButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelReceiptButton.StatusOnText = null;
            this.CancelReceiptButton.TabIndex = 128;
            this.CancelReceiptButton.Text = "Cancel";
            this.CancelReceiptButton.UseVisualStyleBackColor = false;
            // 
            // SaveButton
            // 
            this.SaveButton.ActualPermission = false;
            this.SaveButton.ApplyDisableBehaviour = false;
            this.SaveButton.AutoEllipsis = true;
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SaveButton.BorderColor = System.Drawing.Color.Wheat;
            this.SaveButton.CommentPriority = false;
            this.SaveButton.EnableAutoPrint = false;
            this.SaveButton.FilterStatus = false;
            this.SaveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.FocusRectangleEnabled = true;
            this.SaveButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SaveButton.ImageSelected = false;
            this.SaveButton.Location = new System.Drawing.Point(119, 123);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.NewPadding = 5;
            this.SaveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Save;
            this.SaveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SaveButton.Size = new System.Drawing.Size(93, 29);
            this.SaveButton.StatusIndicator = false;
            this.SaveButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SaveButton.StatusOffText = null;
            this.SaveButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SaveButton.StatusOnText = null;
            this.SaveButton.TabIndex = 127;
            this.SaveButton.Text = "Save / Batch";
            this.SaveButton.UseVisualStyleBackColor = false;
            // 
            // NewButton
            // 
            this.NewButton.ActualPermission = false;
            this.NewButton.ApplyDisableBehaviour = false;
            this.NewButton.AutoEllipsis = true;
            this.NewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.NewButton.BorderColor = System.Drawing.Color.Wheat;
            this.NewButton.CommentPriority = false;
            this.NewButton.EnableAutoPrint = false;
            this.NewButton.FilterStatus = false;
            this.NewButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.NewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewButton.FocusRectangleEnabled = true;
            this.NewButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.NewButton.ImageSelected = false;
            this.NewButton.Location = new System.Drawing.Point(20, 123);
            this.NewButton.Name = "NewButton";
            this.NewButton.NewPadding = 5;
            this.NewButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.NewButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.NewButton.Size = new System.Drawing.Size(93, 29);
            this.NewButton.StatusIndicator = false;
            this.NewButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NewButton.StatusOffText = null;
            this.NewButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.NewButton.StatusOnText = null;
            this.NewButton.TabIndex = 126;
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = false;
            // 
            // AuditLinkLabel
            // 
            this.AuditLinkLabel.AutoSize = true;
            this.AuditLinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuditLinkLabel.Location = new System.Drawing.Point(646, 259);
            this.AuditLinkLabel.Name = "AuditLinkLabel";
            this.AuditLinkLabel.Size = new System.Drawing.Size(127, 15);
            this.AuditLinkLabel.TabIndex = 129;
            this.AuditLinkLabel.TabStop = true;
            this.AuditLinkLabel.Text = "tTR_Rcpt [ReceiptID] ";
            // 
            // PaymentPictureBox
            // 
            this.PaymentPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("PaymentPictureBox.Image")));
            this.PaymentPictureBox.Location = new System.Drawing.Point(773, 167);
            this.PaymentPictureBox.Name = "PaymentPictureBox";
            this.PaymentPictureBox.Size = new System.Drawing.Size(24, 89);
            this.PaymentPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PaymentPictureBox.TabIndex = 131;
            this.PaymentPictureBox.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.paymentEngineUserControl1);
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Location = new System.Drawing.Point(-1, 167);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(777, 89);
            this.panel3.TabIndex = 130;
            this.panel3.TabStop = true;
            // 
            // paymentEngineUserControl1
            // 
            this.paymentEngineUserControl1.AllowOverUnder = true;
            this.paymentEngineUserControl1.AmountTotal = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.paymentEngineUserControl1.ApplyAmountFiled = false;
            this.paymentEngineUserControl1.ApplyAmountResolution = false;
            this.paymentEngineUserControl1.ApplyInstrumentBalanceAmount = null;
            this.paymentEngineUserControl1.ApplyInstrumentPayment = false;
            this.paymentEngineUserControl1.ApplyReadonlyColumn = false;
            this.paymentEngineUserControl1.ApplyToolTip = false;
            this.paymentEngineUserControl1.AutoResizeWithOutppId = false;
            this.paymentEngineUserControl1.AutoResizeWithppId = false;
            this.paymentEngineUserControl1.AvailableTenderType = null;
            this.paymentEngineUserControl1.BackColor = System.Drawing.Color.White;
            this.paymentEngineUserControl1.BalanceAmount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.paymentEngineUserControl1.DataGridViewColumnWidth.AmountWidth = 100;
            this.paymentEngineUserControl1.DataGridViewColumnWidth.CheckNumberWidth = 67;
            this.paymentEngineUserControl1.DataGridViewColumnWidth.PaidByWidth = 324;
            this.paymentEngineUserControl1.DataGridViewColumnWidth.PIDWidth = 75;
            this.paymentEngineUserControl1.DataGridViewColumnWidth.PPIDWidth = 65;
            this.paymentEngineUserControl1.DataGridViewColumnWidth.TenderTypeWidth = 105;
            this.paymentEngineUserControl1.IsAutoPayment = false;
            this.paymentEngineUserControl1.Location = new System.Drawing.Point(-2, -2);
            this.paymentEngineUserControl1.Name = "paymentEngineUserControl1";
            this.paymentEngineUserControl1.OwnerName = null;
            this.paymentEngineUserControl1.ParentFormId = 0;
            this.paymentEngineUserControl1.ParentWorkItem = null;
            this.paymentEngineUserControl1.PIDVisible = true;
            this.paymentEngineUserControl1.PPaymentId = 0;
            this.paymentEngineUserControl1.PPIDVisible = true;
            this.paymentEngineUserControl1.RefundNow = false;
            this.paymentEngineUserControl1.RestrictedTenderType = false;
            this.paymentEngineUserControl1.RowLock = false;
            this.paymentEngineUserControl1.RowsVisibleNo = 4;
            this.paymentEngineUserControl1.Size = new System.Drawing.Size(772, 89);
            this.paymentEngineUserControl1.SourceImage = null;
            this.paymentEngineUserControl1.TabIndex = 0;
            this.paymentEngineUserControl1.TotalDue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.paymentEngineUserControl1.TotalReceiptAmount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.paymentEngineUserControl1.WillShowLabel = true;
            // 
            // CommentButton
            // 
            this.CommentButton.ActualPermission = false;
            this.CommentButton.ApplyDisableBehaviour = false;
            this.CommentButton.AutoEllipsis = true;
            this.CommentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CommentButton.BorderColor = System.Drawing.Color.Wheat;
            this.CommentButton.CommentPriority = false;
            this.CommentButton.EnableAutoPrint = false;
            this.CommentButton.FilterStatus = false;
            this.CommentButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CommentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CommentButton.FocusRectangleEnabled = true;
            this.CommentButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommentButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CommentButton.ImageSelected = false;
            this.CommentButton.Location = new System.Drawing.Point(640, 77);
            this.CommentButton.Name = "CommentButton";
            this.CommentButton.NewPadding = 5;
            this.CommentButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Save;
            this.CommentButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CommentButton.Size = new System.Drawing.Size(93, 29);
            this.CommentButton.StatusIndicator = false;
            this.CommentButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CommentButton.StatusOffText = null;
            this.CommentButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CommentButton.StatusOnText = null;
            this.CommentButton.TabIndex = 133;
            this.CommentButton.Text = "Comment";
            this.CommentButton.UseVisualStyleBackColor = false;
            // 
            // AttachmentButton
            // 
            this.AttachmentButton.ActualPermission = false;
            this.AttachmentButton.ApplyDisableBehaviour = false;
            this.AttachmentButton.AutoEllipsis = true;
            this.AttachmentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AttachmentButton.BorderColor = System.Drawing.Color.Wheat;
            this.AttachmentButton.CommentPriority = false;
            this.AttachmentButton.EnableAutoPrint = false;
            this.AttachmentButton.FilterStatus = false;
            this.AttachmentButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AttachmentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AttachmentButton.FocusRectangleEnabled = true;
            this.AttachmentButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttachmentButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AttachmentButton.ImageSelected = false;
            this.AttachmentButton.Location = new System.Drawing.Point(640, 35);
            this.AttachmentButton.Name = "AttachmentButton";
            this.AttachmentButton.NewPadding = 5;
            this.AttachmentButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.AttachmentButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AttachmentButton.Size = new System.Drawing.Size(93, 29);
            this.AttachmentButton.StatusIndicator = false;
            this.AttachmentButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AttachmentButton.StatusOffText = null;
            this.AttachmentButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AttachmentButton.StatusOnText = null;
            this.AttachmentButton.TabIndex = 132;
            this.AttachmentButton.Text = "Attachment";
            this.AttachmentButton.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.ValidEntriesTotalTextBox);
            this.panel2.Location = new System.Drawing.Point(514, 107);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(101, 20);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.BalanceAmountTextBox);
            this.panel4.Location = new System.Drawing.Point(514, 126);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(101, 23);
            this.panel4.TabIndex = 121;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.PaymentsTotalTextBox);
            this.panel5.Location = new System.Drawing.Point(514, 148);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(101, 20);
            this.panel5.TabIndex = 121;
            // 
            // ExciseTaxReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.CommentButton);
            this.Controls.Add(this.AttachmentButton);
            this.Controls.Add(this.PaymentPictureBox);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.AuditLinkLabel);
            this.Controls.Add(this.CancelReceiptButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.BalanceLabel);
            this.Controls.Add(this.PaymentTotalLabel);
            this.Controls.Add(this.TotalDueLabel);
            this.Controls.Add(this.panel1);
            this.Name = "ExciseTaxReceipt";
            this.Size = new System.Drawing.Size(808, 279);
            this.panel1.ResumeLayout(false);
            this.ErrorGridPanel.ResumeLayout(false);
            this.ErrorGridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentPictureBox)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel ErrorGridPanel;
        private TerraScan.UI.Controls.TerraScanTextBox terraScanTextBox1;
        private System.Windows.Forms.Label label1;
        private TerraScan.UI.Controls.TerraScanDataGridView ErrorGridView;
        private TerraScan.UI.Controls.TerraScanTextBox ErrorAmountTotalTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox PaymentsTotalTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox BalanceAmountTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox ValidEntriesTotalTextBox;
        private System.Windows.Forms.Label BalanceLabel;
        private System.Windows.Forms.Label PaymentTotalLabel;
        private System.Windows.Forms.Label TotalDueLabel;
        private TerraScan.UI.Controls.TerraScanButton CancelReceiptButton;
        private TerraScan.UI.Controls.TerraScanButton SaveButton;
        private TerraScan.UI.Controls.TerraScanButton NewButton;
        private System.Windows.Forms.LinkLabel AuditLinkLabel;
        private System.Windows.Forms.PictureBox PaymentPictureBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private TerraScan.UI.Controls.TerraScanTextBox terraScanTextBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeeAmount;
        private TerraScan.UI.Controls.TerraScanButton CommentButton;
        private TerraScan.UI.Controls.TerraScanButton AttachmentButton;
        private TerraScan.PaymentEngine.PaymentEngineUserControl paymentEngineUserControl1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
    }
}
