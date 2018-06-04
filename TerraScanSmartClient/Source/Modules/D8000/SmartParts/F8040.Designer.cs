namespace D8000
{
    partial class F8040
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8040));
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.HourPanel = new System.Windows.Forms.Panel();
            this.HoursTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.EndDatePanel = new System.Windows.Forms.Panel();
            this.EndDateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.EndTimePanel = new System.Windows.Forms.Panel();
            this.EndTimeTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.StartDatePanel = new System.Windows.Forms.Panel();
            this.StartDateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.StartTimePanel = new System.Windows.Forms.Panel();
            this.StartTimeTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.ResourcePanel = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ResourceComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.CommentPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.CommentTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.AddButton = new TerraScan.UI.Controls.TerraScanButton();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.TimeGridVscrollBar = new System.Windows.Forms.VScrollBar();
            this.TimeGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.Resource = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TimePictureBox = new System.Windows.Forms.PictureBox();
            this.TimeDetailsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.StartdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.StartDatepickerbutton = new System.Windows.Forms.Button();
            this.EnddateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.EndDatepickerbutton = new System.Windows.Forms.Button();
            this.HeaderPanel.SuspendLayout();
            this.HourPanel.SuspendLayout();
            this.EndDatePanel.SuspendLayout();
            this.EndTimePanel.SuspendLayout();
            this.StartDatePanel.SuspendLayout();
            this.StartTimePanel.SuspendLayout();
            this.ResourcePanel.SuspendLayout();
            this.CommentPanel.SuspendLayout();
            this.GridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeGridView)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Controls.Add(this.HourPanel);
            this.HeaderPanel.Controls.Add(this.EndDatePanel);
            this.HeaderPanel.Controls.Add(this.EndTimePanel);
            this.HeaderPanel.Controls.Add(this.StartDatePanel);
            this.HeaderPanel.Controls.Add(this.StartTimePanel);
            this.HeaderPanel.Controls.Add(this.ResourcePanel);
            this.HeaderPanel.Controls.Add(this.CommentPanel);
            this.HeaderPanel.Location = new System.Drawing.Point(-1, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(769, 25);
            this.HeaderPanel.TabIndex = 1;
            // 
            // HourPanel
            // 
            this.HourPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HourPanel.Controls.Add(this.HoursTextBox);
            this.HourPanel.Location = new System.Drawing.Point(510, 0);
            this.HourPanel.Name = "HourPanel";
            this.HourPanel.Size = new System.Drawing.Size(61, 25);
            this.HourPanel.TabIndex = 6;
            this.HourPanel.TabStop = true;
            // 
            // HoursTextBox
            // 
            this.HoursTextBox.AllowClick = true;
            this.HoursTextBox.AllowNegativeSign = false;
            this.HoursTextBox.ApplyCFGFormat = false;
            this.HoursTextBox.ApplyCurrencyFormat = false;
            this.HoursTextBox.ApplyFocusColor = true;
            this.HoursTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.HoursTextBox.ApplyNegativeStandard = true;
            this.HoursTextBox.ApplyParentFocusColor = true;
            this.HoursTextBox.ApplyTimeFormat = false;
            this.HoursTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.HoursTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HoursTextBox.CFromatWihoutSymbol = false;
            this.HoursTextBox.CheckForEmpty = false;
            this.HoursTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.HoursTextBox.Digits = -1;
            this.HoursTextBox.EmptyDecimalValue = false;
            this.HoursTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.HoursTextBox.ForeColor = System.Drawing.Color.Black;
            this.HoursTextBox.IsEditable = true;
            this.HoursTextBox.IsQueryableFileld = true;
            this.HoursTextBox.Location = new System.Drawing.Point(4, 4);
            this.HoursTextBox.LockKeyPress = false;
            this.HoursTextBox.MaxLength = 9;
            this.HoursTextBox.Name = "HoursTextBox";
            this.HoursTextBox.PersistDefaultColor = false;
            this.HoursTextBox.Precision = 2;
            this.HoursTextBox.QueryingFileldName = "";
            this.HoursTextBox.SetColorFlag = false;
            this.HoursTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.HoursTextBox.Size = new System.Drawing.Size(51, 16);
            this.HoursTextBox.SpecialCharacter = "%";
            this.HoursTextBox.TabIndex = 0;
            this.HoursTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HoursTextBox.TextCustomFormat = "$#,##0.00";
            this.HoursTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.HoursTextBox.WholeInteger = false;
            this.HoursTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // EndDatePanel
            // 
            this.EndDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EndDatePanel.Controls.Add(this.EnddateTimePicker);
            this.EndDatePanel.Controls.Add(this.EndDatepickerbutton);
            this.EndDatePanel.Controls.Add(this.EndDateTextBox);
            this.EndDatePanel.Location = new System.Drawing.Point(330, 0);
            this.EndDatePanel.Name = "EndDatePanel";
            this.EndDatePanel.Size = new System.Drawing.Size(101, 25);
            this.EndDatePanel.TabIndex = 4;
            this.EndDatePanel.TabStop = true;
            // 
            // EndDateTextBox
            // 
            this.EndDateTextBox.AllowClick = true;
            this.EndDateTextBox.AllowNegativeSign = false;
            this.EndDateTextBox.ApplyCFGFormat = false;
            this.EndDateTextBox.ApplyCurrencyFormat = false;
            this.EndDateTextBox.ApplyFocusColor = true;
            this.EndDateTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.EndDateTextBox.ApplyNegativeStandard = true;
            this.EndDateTextBox.ApplyParentFocusColor = true;
            this.EndDateTextBox.ApplyTimeFormat = false;
            this.EndDateTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.EndDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EndDateTextBox.CFromatWihoutSymbol = false;
            this.EndDateTextBox.CheckForEmpty = false;
            this.EndDateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EndDateTextBox.Digits = -1;
            this.EndDateTextBox.EmptyDecimalValue = false;
            this.EndDateTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.EndDateTextBox.ForeColor = System.Drawing.Color.Black;
            this.EndDateTextBox.IsEditable = true;
            this.EndDateTextBox.IsQueryableFileld = true;
            this.EndDateTextBox.Location = new System.Drawing.Point(4, 4);
            this.EndDateTextBox.LockKeyPress = false;
            this.EndDateTextBox.MaxLength = 10;
            this.EndDateTextBox.Name = "EndDateTextBox";
            this.EndDateTextBox.PersistDefaultColor = false;
            this.EndDateTextBox.Precision = 2;
            this.EndDateTextBox.QueryingFileldName = "";
            this.EndDateTextBox.SetColorFlag = false;
            this.EndDateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.EndDateTextBox.Size = new System.Drawing.Size(71, 16);
            this.EndDateTextBox.SpecialCharacter = "%";
            this.EndDateTextBox.TabIndex = 0;
            this.EndDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EndDateTextBox.TextCustomFormat = "$#,##0.00";
            this.EndDateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.EndDateTextBox.WholeInteger = false;
            this.EndDateTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            this.EndDateTextBox.Leave += new System.EventHandler(this.EndDateTextBox_Leave);
            this.EndDateTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.EndDateTextBox_Validating);
            // 
            // EndTimePanel
            // 
            this.EndTimePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EndTimePanel.Controls.Add(this.EndTimeTextBox);
            this.EndTimePanel.Location = new System.Drawing.Point(430, 0);
            this.EndTimePanel.Name = "EndTimePanel";
            this.EndTimePanel.Size = new System.Drawing.Size(82, 25);
            this.EndTimePanel.TabIndex = 5;
            this.EndTimePanel.TabStop = true;
            // 
            // EndTimeTextBox
            // 
            this.EndTimeTextBox.AllowClick = true;
            this.EndTimeTextBox.AllowNegativeSign = false;
            this.EndTimeTextBox.ApplyCFGFormat = false;
            this.EndTimeTextBox.ApplyCurrencyFormat = false;
            this.EndTimeTextBox.ApplyFocusColor = true;
            this.EndTimeTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.EndTimeTextBox.ApplyNegativeStandard = true;
            this.EndTimeTextBox.ApplyParentFocusColor = true;
            this.EndTimeTextBox.ApplyTimeFormat = false;
            this.EndTimeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.EndTimeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EndTimeTextBox.CFromatWihoutSymbol = false;
            this.EndTimeTextBox.CheckForEmpty = false;
            this.EndTimeTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EndTimeTextBox.Digits = -1;
            this.EndTimeTextBox.EmptyDecimalValue = false;
            this.EndTimeTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.EndTimeTextBox.ForeColor = System.Drawing.Color.Black;
            this.EndTimeTextBox.IsEditable = true;
            this.EndTimeTextBox.IsQueryableFileld = true;
            this.EndTimeTextBox.Location = new System.Drawing.Point(4, 4);
            this.EndTimeTextBox.LockKeyPress = false;
            this.EndTimeTextBox.MaxLength = 8;
            this.EndTimeTextBox.Name = "EndTimeTextBox";
            this.EndTimeTextBox.PersistDefaultColor = false;
            this.EndTimeTextBox.Precision = 2;
            this.EndTimeTextBox.QueryingFileldName = "";
            this.EndTimeTextBox.SetColorFlag = false;
            this.EndTimeTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.EndTimeTextBox.Size = new System.Drawing.Size(72, 16);
            this.EndTimeTextBox.SpecialCharacter = "%";
            this.EndTimeTextBox.TabIndex = 0;
            this.EndTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EndTimeTextBox.TextCustomFormat = "$#,##0.00";
            this.EndTimeTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Time;
            this.EndTimeTextBox.WholeInteger = false;
            this.EndTimeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            this.EndTimeTextBox.Leave += new System.EventHandler(this.EndTimeTextBox_Leave);
            // 
            // StartDatePanel
            // 
            this.StartDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartDatePanel.Controls.Add(this.StartdateTimePicker);
            this.StartDatePanel.Controls.Add(this.StartDatepickerbutton);
            this.StartDatePanel.Controls.Add(this.StartDateTextBox);
            this.StartDatePanel.Location = new System.Drawing.Point(150, 0);
            this.StartDatePanel.Name = "StartDatePanel";
            this.StartDatePanel.Size = new System.Drawing.Size(101, 25);
            this.StartDatePanel.TabIndex = 2;
            this.StartDatePanel.TabStop = true;
            // 
            // StartDateTextBox
            // 
            this.StartDateTextBox.AllowClick = true;
            this.StartDateTextBox.AllowNegativeSign = false;
            this.StartDateTextBox.ApplyCFGFormat = false;
            this.StartDateTextBox.ApplyCurrencyFormat = false;
            this.StartDateTextBox.ApplyFocusColor = true;
            this.StartDateTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.StartDateTextBox.ApplyNegativeStandard = true;
            this.StartDateTextBox.ApplyParentFocusColor = true;
            this.StartDateTextBox.ApplyTimeFormat = false;
            this.StartDateTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.StartDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StartDateTextBox.CFromatWihoutSymbol = false;
            this.StartDateTextBox.CheckForEmpty = false;
            this.StartDateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.StartDateTextBox.Digits = -1;
            this.StartDateTextBox.EmptyDecimalValue = false;
            this.StartDateTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.StartDateTextBox.ForeColor = System.Drawing.Color.Black;
            this.StartDateTextBox.IsEditable = true;
            this.StartDateTextBox.IsQueryableFileld = true;
            this.StartDateTextBox.Location = new System.Drawing.Point(4, 4);
            this.StartDateTextBox.LockKeyPress = false;
            this.StartDateTextBox.MaxLength = 10;
            this.StartDateTextBox.Name = "StartDateTextBox";
            this.StartDateTextBox.PersistDefaultColor = false;
            this.StartDateTextBox.Precision = 2;
            this.StartDateTextBox.QueryingFileldName = "";
            this.StartDateTextBox.SetColorFlag = false;
            this.StartDateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.StartDateTextBox.Size = new System.Drawing.Size(71, 16);
            this.StartDateTextBox.SpecialCharacter = "%";
            this.StartDateTextBox.TabIndex = 0;
            this.StartDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.StartDateTextBox.TextCustomFormat = "$#,##0.00";
            this.StartDateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.StartDateTextBox.WholeInteger = false;
            this.StartDateTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            this.StartDateTextBox.Leave += new System.EventHandler(this.StartDateTextBox_Leave);
            // 
            // StartTimePanel
            // 
            this.StartTimePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartTimePanel.Controls.Add(this.StartTimeTextBox);
            this.StartTimePanel.Location = new System.Drawing.Point(250, 0);
            this.StartTimePanel.Name = "StartTimePanel";
            this.StartTimePanel.Size = new System.Drawing.Size(81, 25);
            this.StartTimePanel.TabIndex = 3;
            this.StartTimePanel.TabStop = true;
            // 
            // StartTimeTextBox
            // 
            this.StartTimeTextBox.AllowClick = true;
            this.StartTimeTextBox.AllowNegativeSign = false;
            this.StartTimeTextBox.ApplyCFGFormat = false;
            this.StartTimeTextBox.ApplyCurrencyFormat = false;
            this.StartTimeTextBox.ApplyFocusColor = true;
            this.StartTimeTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.StartTimeTextBox.ApplyNegativeStandard = true;
            this.StartTimeTextBox.ApplyParentFocusColor = true;
            this.StartTimeTextBox.ApplyTimeFormat = false;
            this.StartTimeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.StartTimeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StartTimeTextBox.CFromatWihoutSymbol = false;
            this.StartTimeTextBox.CheckForEmpty = false;
            this.StartTimeTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.StartTimeTextBox.Digits = -1;
            this.StartTimeTextBox.EmptyDecimalValue = false;
            this.StartTimeTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.StartTimeTextBox.ForeColor = System.Drawing.Color.Black;
            this.StartTimeTextBox.IsEditable = true;
            this.StartTimeTextBox.IsQueryableFileld = true;
            this.StartTimeTextBox.Location = new System.Drawing.Point(4, 4);
            this.StartTimeTextBox.LockKeyPress = false;
            this.StartTimeTextBox.MaxLength = 8;
            this.StartTimeTextBox.Name = "StartTimeTextBox";
            this.StartTimeTextBox.PersistDefaultColor = false;
            this.StartTimeTextBox.Precision = 2;
            this.StartTimeTextBox.QueryingFileldName = "";
            this.StartTimeTextBox.SetColorFlag = false;
            this.StartTimeTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.StartTimeTextBox.Size = new System.Drawing.Size(72, 16);
            this.StartTimeTextBox.SpecialCharacter = "%";
            this.StartTimeTextBox.TabIndex = 0;
            this.StartTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.StartTimeTextBox.TextCustomFormat = "$#,##0.00";
            this.StartTimeTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Time;
            this.StartTimeTextBox.WholeInteger = false;
            this.StartTimeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            this.StartTimeTextBox.Leave += new System.EventHandler(this.StartTimeTextBox_Leave);
            // 
            // ResourcePanel
            // 
            this.ResourcePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResourcePanel.Controls.Add(this.panel6);
            this.ResourcePanel.Controls.Add(this.ResourceComboBox);
            this.ResourcePanel.Location = new System.Drawing.Point(1, 0);
            this.ResourcePanel.Name = "ResourcePanel";
            this.ResourcePanel.Size = new System.Drawing.Size(150, 25);
            this.ResourcePanel.TabIndex = 1;
            this.ResourcePanel.TabStop = true;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Silver;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(-1, -1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(20, 25);
            this.panel6.TabIndex = 112;
            // 
            // ResourceComboBox
            // 
            this.ResourceComboBox.BackColor = System.Drawing.Color.White;
            this.ResourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResourceComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResourceComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResourceComboBox.FormattingEnabled = true;
            this.ResourceComboBox.Location = new System.Drawing.Point(23, 1);
            this.ResourceComboBox.Name = "ResourceComboBox";
            this.ResourceComboBox.Size = new System.Drawing.Size(126, 24);
            this.ResourceComboBox.TabIndex = 1;
            this.ResourceComboBox.Leave += new System.EventHandler(this.ResourceComboBox_Leave);
            this.ResourceComboBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ResourceComboBox_MouseMove);
            this.ResourceComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // CommentPanel
            // 
            this.CommentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CommentPanel.Controls.Add(this.panel3);
            this.CommentPanel.Controls.Add(this.CommentTextBox);
            this.CommentPanel.Controls.Add(this.AddButton);
            this.CommentPanel.Location = new System.Drawing.Point(570, 0);
            this.CommentPanel.Name = "CommentPanel";
            this.CommentPanel.Size = new System.Drawing.Size(199, 25);
            this.CommentPanel.TabIndex = 7;
            this.CommentPanel.TabStop = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(178, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(20, 25);
            this.panel3.TabIndex = 118;
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.AllowClick = true;
            this.CommentTextBox.AllowNegativeSign = false;
            this.CommentTextBox.ApplyCFGFormat = false;
            this.CommentTextBox.ApplyCurrencyFormat = false;
            this.CommentTextBox.ApplyFocusColor = true;
            this.CommentTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.CommentTextBox.ApplyNegativeStandard = true;
            this.CommentTextBox.ApplyParentFocusColor = true;
            this.CommentTextBox.ApplyTimeFormat = false;
            this.CommentTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.CommentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommentTextBox.CFromatWihoutSymbol = false;
            this.CommentTextBox.CheckForEmpty = false;
            this.CommentTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CommentTextBox.Digits = -1;
            this.CommentTextBox.EmptyDecimalValue = false;
            this.CommentTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.CommentTextBox.ForeColor = System.Drawing.Color.Black;
            this.CommentTextBox.IsEditable = true;
            this.CommentTextBox.IsQueryableFileld = true;
            this.CommentTextBox.Location = new System.Drawing.Point(5, 3);
            this.CommentTextBox.LockKeyPress = false;
            this.CommentTextBox.MaxLength = 100;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.PersistDefaultColor = false;
            this.CommentTextBox.Precision = 2;
            this.CommentTextBox.QueryingFileldName = "";
            this.CommentTextBox.SetColorFlag = false;
            this.CommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CommentTextBox.Size = new System.Drawing.Size(108, 16);
            this.CommentTextBox.SpecialCharacter = "%";
            this.CommentTextBox.TabIndex = 0;
            this.CommentTextBox.TextCustomFormat = "$#,##0.00";
            this.CommentTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CommentTextBox.WholeInteger = false;
            this.CommentTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderControlsKeyDown);
            // 
            // AddButton
            // 
            this.AddButton.ActualPermission = false;
            this.AddButton.ApplyDisableBehaviour = true;
            this.AddButton.AutoEllipsis = true;
            this.AddButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(102)))));
            this.AddButton.BorderColor = System.Drawing.Color.Wheat;
            this.AddButton.CommentPriority = false;
            this.AddButton.EnableAutoPrint = false;
            this.AddButton.FilterStatus = false;
            this.AddButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddButton.FocusRectangleEnabled = true;
            this.AddButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AddButton.ImageSelected = false;
            this.AddButton.Location = new System.Drawing.Point(120, -1);
            this.AddButton.Name = "AddButton";
            this.AddButton.NewPadding = 5;
            this.AddButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.AddButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.None;
            this.AddButton.Size = new System.Drawing.Size(60, 25);
            this.AddButton.StatusIndicator = false;
            this.AddButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AddButton.StatusOffText = null;
            this.AddButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AddButton.StatusOnText = null;
            this.AddButton.TabIndex = 8;
            this.AddButton.TabStop = false;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // GridPanel
            // 
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.TimeGridVscrollBar);
            this.GridPanel.Controls.Add(this.TimeGridView);
            this.GridPanel.Location = new System.Drawing.Point(0, 24);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(768, 109);
            this.GridPanel.TabIndex = 100;
            this.GridPanel.TabStop = true;
            // 
            // TimeGridVscrollBar
            // 
            this.TimeGridVscrollBar.Enabled = false;
            this.TimeGridVscrollBar.Location = new System.Drawing.Point(749, 0);
            this.TimeGridVscrollBar.Name = "TimeGridVscrollBar";
            this.TimeGridVscrollBar.Size = new System.Drawing.Size(16, 110);
            this.TimeGridVscrollBar.TabIndex = 1004;
            // 
            // TimeGridView
            // 
            this.TimeGridView.AllowCellClick = true;
            this.TimeGridView.AllowDoubleClick = true;
            this.TimeGridView.AllowEmptyRows = true;
            this.TimeGridView.AllowEnterKey = false;
            this.TimeGridView.AllowSorting = true;
            this.TimeGridView.AllowUserToAddRows = false;
            this.TimeGridView.AllowUserToDeleteRows = false;
            this.TimeGridView.AllowUserToResizeColumns = false;
            this.TimeGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.TimeGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle23;
            this.TimeGridView.ApplyStandardBehaviour = false;
            this.TimeGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TimeGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TimeGridView.ClearCurrentCellOnLeave = false;
            this.TimeGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TimeGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.TimeGridView.ColumnHeadersHeight = 21;
            this.TimeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TimeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Resource,
            this.StartDate,
            this.StartTime,
            this.EndDate,
            this.EndTime,
            this.Hours,
            this.Comment,
            this.TRID,
            this.IsUser});
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TimeGridView.DefaultCellStyle = dataGridViewCellStyle31;
            this.TimeGridView.DefaultRowIndex = -1;
            this.TimeGridView.DeselectCurrentCell = false;
            this.TimeGridView.DeselectSpecifiedRow = -1;
            this.TimeGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.TimeGridView.EnableBinding = true;
            this.TimeGridView.EnableHeadersVisualStyles = false;
            this.TimeGridView.GridColor = System.Drawing.Color.Black;
            this.TimeGridView.GridContentSelected = false;
            this.TimeGridView.IsEditableGrid = true;
            this.TimeGridView.IsMultiSelect = false;
            this.TimeGridView.IsSorted = false;
            this.TimeGridView.Location = new System.Drawing.Point(-1, -1);
            this.TimeGridView.MultiSelect = false;
            this.TimeGridView.Name = "TimeGridView";
            this.TimeGridView.NumRowsVisible = 4;
            this.TimeGridView.PrimaryKeyColumnName = "";
            this.TimeGridView.RemainSortFields = true;
            this.TimeGridView.RemoveDefaultSelection = false;
            this.TimeGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle32.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TimeGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle32;
            this.TimeGridView.RowHeadersWidth = 20;
            this.TimeGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle33.SelectionBackColor = System.Drawing.Color.Navy;
            this.TimeGridView.RowsDefaultCellStyle = dataGridViewCellStyle33;
            this.TimeGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TimeGridView.Size = new System.Drawing.Size(766, 109);
            this.TimeGridView.StandardTab = true;
            this.TimeGridView.TabIndex = 9;
            this.TimeGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.TimeGridView_CellValueChanged);
            this.TimeGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.TimeGridView_RowEnter);
            this.TimeGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.TimeGridView_CellParsing);
            this.TimeGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TimeGridView_CellFormatting);
            this.TimeGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TimeGridView_CellClick);
            this.TimeGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.TimeGridView_DataError);
            this.TimeGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TimeGridView_KeyDown);
            this.TimeGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.TimeGridView_DataBindingComplete);
            this.TimeGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TimeGridView_RowHeaderMouseClick);
            // 
            // Resource
            // 
            this.Resource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Resource.HeaderText = "Resource";
            this.Resource.Name = "Resource";
            this.Resource.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Resource.Width = 130;
            // 
            // StartDate
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StartDate.DefaultCellStyle = dataGridViewCellStyle25;
            this.StartDate.HeaderText = "Start Date";
            this.StartDate.MaxInputLength = 10;
            this.StartDate.Name = "StartDate";
            this.StartDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.StartDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StartTime
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StartTime.DefaultCellStyle = dataGridViewCellStyle26;
            this.StartTime.HeaderText = "Start Time";
            this.StartTime.MaxInputLength = 8;
            this.StartTime.Name = "StartTime";
            this.StartTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.StartTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StartTime.Width = 80;
            // 
            // EndDate
            // 
            this.EndDate.DataPropertyName = "Comment";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EndDate.DefaultCellStyle = dataGridViewCellStyle27;
            this.EndDate.HeaderText = "End Date";
            this.EndDate.MaxInputLength = 10;
            this.EndDate.Name = "EndDate";
            this.EndDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EndDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EndTime
            // 
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EndTime.DefaultCellStyle = dataGridViewCellStyle28;
            this.EndTime.HeaderText = "End Time";
            this.EndTime.MaxInputLength = 8;
            this.EndTime.Name = "EndTime";
            this.EndTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EndTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EndTime.Width = 80;
            // 
            // Hours
            // 
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Hours.DefaultCellStyle = dataGridViewCellStyle29;
            this.Hours.HeaderText = "Hours";
            this.Hours.MaxInputLength = 9;
            this.Hours.Name = "Hours";
            this.Hours.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Hours.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Hours.Width = 60;
            // 
            // Comment
            // 
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Comment.DefaultCellStyle = dataGridViewCellStyle30;
            this.Comment.HeaderText = "Comment";
            this.Comment.MaxInputLength = 500;
            this.Comment.Name = "Comment";
            this.Comment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Comment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Comment.Width = 179;
            // 
            // TRID
            // 
            this.TRID.HeaderText = "TRID";
            this.TRID.Name = "TRID";
            this.TRID.ReadOnly = true;
            this.TRID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TRID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TRID.Visible = false;
            // 
            // IsUser
            // 
            this.IsUser.HeaderText = "IsUser";
            this.IsUser.Name = "IsUser";
            this.IsUser.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IsUser.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(0, 132);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(20, 21);
            this.panel2.TabIndex = 116;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Location = new System.Drawing.Point(19, 132);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(749, 21);
            this.panel4.TabIndex = 115;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(728, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 21);
            this.panel1.TabIndex = 117;
            // 
            // TimePictureBox
            // 
            this.TimePictureBox.Location = new System.Drawing.Point(761, 0);
            this.TimePictureBox.Name = "TimePictureBox";
            this.TimePictureBox.Size = new System.Drawing.Size(42, 153);
            this.TimePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TimePictureBox.TabIndex = 117;
            this.TimePictureBox.TabStop = false;
            this.TimePictureBox.Click += new System.EventHandler(this.TimePictureBox_Click);
            this.TimePictureBox.MouseEnter += new System.EventHandler(this.TimePictureBox_MouseEnter);
            // 
            // StartdateTimePicker
            // 
            this.StartdateTimePicker.Location = new System.Drawing.Point(91, 4);
            this.StartdateTimePicker.Name = "StartdateTimePicker";
            this.StartdateTimePicker.Size = new System.Drawing.Size(0, 20);
            this.StartdateTimePicker.TabIndex = 8;
            this.StartdateTimePicker.TabStop = false;
            this.StartdateTimePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TimePicker_KeyPress);
            this.StartdateTimePicker.CloseUp += new System.EventHandler(this.StartdateTimePicker_CloseUp);
            // 
            // StartDatepickerbutton
            // 
            this.StartDatepickerbutton.FlatAppearance.BorderSize = 0;
            this.StartDatepickerbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartDatepickerbutton.Image = ((System.Drawing.Image)(resources.GetObject("StartDatepickerbutton.Image")));
            this.StartDatepickerbutton.Location = new System.Drawing.Point(79, 1);
            this.StartDatepickerbutton.Name = "StartDatepickerbutton";
            this.StartDatepickerbutton.Size = new System.Drawing.Size(19, 20);
            this.StartDatepickerbutton.TabIndex = 7;
            this.StartDatepickerbutton.UseVisualStyleBackColor = true;
            this.StartDatepickerbutton.Click += new System.EventHandler(this.StartDatepickerbutton_Click);
            // 
            // EnddateTimePicker
            // 
            this.EnddateTimePicker.Location = new System.Drawing.Point(90, 4);
            this.EnddateTimePicker.Name = "EnddateTimePicker";
            this.EnddateTimePicker.Size = new System.Drawing.Size(0, 20);
            this.EnddateTimePicker.TabIndex = 8;
            this.EnddateTimePicker.TabStop = false;
            this.EnddateTimePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TimePicker_KeyPress);
            this.EnddateTimePicker.CloseUp += new System.EventHandler(this.EnddateTimePicker_CloseUp);
            // 
            // EndDatepickerbutton
            // 
            this.EndDatepickerbutton.FlatAppearance.BorderSize = 0;
            this.EndDatepickerbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EndDatepickerbutton.Image = ((System.Drawing.Image)(resources.GetObject("EndDatepickerbutton.Image")));
            this.EndDatepickerbutton.Location = new System.Drawing.Point(78, 1);
            this.EndDatepickerbutton.Name = "EndDatepickerbutton";
            this.EndDatepickerbutton.Size = new System.Drawing.Size(19, 20);
            this.EndDatepickerbutton.TabIndex = 7;
            this.EndDatepickerbutton.UseVisualStyleBackColor = true;
            this.EndDatepickerbutton.Click += new System.EventHandler(this.EndDatepickerbutton_Click);
            // 
            // F8040
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.HeaderPanel);
            this.Controls.Add(this.TimePictureBox);
            this.Name = "F8040";
            this.ParentFormId = 8040;
            this.Size = new System.Drawing.Size(804, 153);
            this.Tag = "";
            this.Load += new System.EventHandler(this.F8040_Load);
            this.HeaderPanel.ResumeLayout(false);
            this.HourPanel.ResumeLayout(false);
            this.HourPanel.PerformLayout();
            this.EndDatePanel.ResumeLayout(false);
            this.EndDatePanel.PerformLayout();
            this.EndTimePanel.ResumeLayout(false);
            this.EndTimePanel.PerformLayout();
            this.StartDatePanel.ResumeLayout(false);
            this.StartDatePanel.PerformLayout();
            this.StartTimePanel.ResumeLayout(false);
            this.StartTimePanel.PerformLayout();
            this.ResourcePanel.ResumeLayout(false);
            this.CommentPanel.ResumeLayout(false);
            this.CommentPanel.PerformLayout();
            this.GridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TimeGridView)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TimePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Panel StartTimePanel;
        private System.Windows.Forms.Panel ResourcePanel;
        private System.Windows.Forms.Panel panel6;
        private TerraScan.UI.Controls.TerraScanComboBox ResourceComboBox;
        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.VScrollBar TimeGridVscrollBar;
        private TerraScan.UI.Controls.TerraScanDataGridView TimeGridView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox TimePictureBox;
        private TerraScan.UI.Controls.TerraScanTextBox StartTimeTextBox;
        private System.Windows.Forms.Panel StartDatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox StartDateTextBox;
        private System.Windows.Forms.Panel EndDatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox EndDateTextBox;
        private System.Windows.Forms.Panel EndTimePanel;
        private TerraScan.UI.Controls.TerraScanTextBox EndTimeTextBox;
        private System.Windows.Forms.Panel CommentPanel;
        private TerraScan.UI.Controls.TerraScanButton AddButton;
        private System.Windows.Forms.Panel HourPanel;
        private TerraScan.UI.Controls.TerraScanTextBox HoursTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox CommentTextBox;
        private System.Windows.Forms.ToolTip TimeDetailsToolTip;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Resource;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hours;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsUser;
        private System.Windows.Forms.DateTimePicker EnddateTimePicker;
        private System.Windows.Forms.Button EndDatepickerbutton;
        private System.Windows.Forms.DateTimePicker StartdateTimePicker;
        private System.Windows.Forms.Button StartDatepickerbutton;
    }
}
