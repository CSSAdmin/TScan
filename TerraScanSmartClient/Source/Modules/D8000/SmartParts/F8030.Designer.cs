namespace D8000
{
    partial class F8030
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8030));
            this.EventPictureBox = new System.Windows.Forms.PictureBox();
            this.EventPanel = new System.Windows.Forms.Panel();
            this.EventTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.EventLable = new System.Windows.Forms.Label();
            this.WorkOrderLinkLablePanel = new System.Windows.Forms.Panel();
            this.WorkOrdertButton = new System.Windows.Forms.Button();
            this.WorkOrderLinkLabel = new System.Windows.Forms.LinkLabel();
            this.WorkOrderLinkTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.WorkOrderLabel = new System.Windows.Forms.Label();
            this.EventDatePanel = new System.Windows.Forms.Panel();
            this.GeneralEventDate = new TerraScan.UI.Controls.TerraScanTextBox();
            this.EventDateLabel = new System.Windows.Forms.Label();
            this.EventHeaderDatePict = new System.Windows.Forms.Button();
            this.EventDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.StatusColorLabel = new System.Windows.Forms.Label();
            this.EventStatus = new TerraScan.UI.Controls.TerraScanComboBox();
            this.EventStatusTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.CompletePanel = new System.Windows.Forms.Panel();
            this.CompleteCheckBox = new TerraScan.UI.Controls.TerraScanCheckBox();
            this.CompleteLabel = new System.Windows.Forms.Label();
            this.GDocEventHeaderToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.EnddatePanel = new System.Windows.Forms.Panel();
            this.EnddateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.EnddateLabel = new System.Windows.Forms.Label();
            this.EnddatePicturebox = new System.Windows.Forms.Button();
            this.EnddatedateTimePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.EventPictureBox)).BeginInit();
            this.EventPanel.SuspendLayout();
            this.WorkOrderLinkLablePanel.SuspendLayout();
            this.EventDatePanel.SuspendLayout();
            this.StatusPanel.SuspendLayout();
            this.CompletePanel.SuspendLayout();
            this.EnddatePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // EventPictureBox
            // 
            this.EventPictureBox.BackColor = System.Drawing.Color.White;
            this.EventPictureBox.Location = new System.Drawing.Point(757, 2);
            this.EventPictureBox.Name = "EventPictureBox";
            this.EventPictureBox.Size = new System.Drawing.Size(42, 79);
            this.EventPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.EventPictureBox.TabIndex = 9;
            this.EventPictureBox.TabStop = false;
            this.EventPictureBox.Click += new System.EventHandler(this.EventPictureBox_Click);
            this.EventPictureBox.MouseEnter += new System.EventHandler(this.EventPictureBox_MouseEnter);
            // 
            // EventPanel
            // 
            this.EventPanel.BackColor = System.Drawing.Color.White;
            this.EventPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EventPanel.Controls.Add(this.EventTextBox);
            this.EventPanel.Controls.Add(this.EventLable);
            this.EventPanel.Location = new System.Drawing.Point(1, 2);
            this.EventPanel.Name = "EventPanel";
            this.EventPanel.Size = new System.Drawing.Size(763, 40);
            this.EventPanel.TabIndex = 0;
            this.EventPanel.TabStop = true;
            // 
            // EventTextBox
            // 
            this.EventTextBox.AllowClick = true;
            this.EventTextBox.AllowNegativeSign = false;
            this.EventTextBox.ApplyCFGFormat = false;
            this.EventTextBox.ApplyCurrencyFormat = false;
            this.EventTextBox.ApplyFocusColor = true;
            this.EventTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.EventTextBox.ApplyNegativeStandard = true;
            this.EventTextBox.ApplyParentFocusColor = true;
            this.EventTextBox.ApplyTimeFormat = false;
            this.EventTextBox.BackColor = System.Drawing.Color.White;
            this.EventTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EventTextBox.CFromatWihoutSymbol = false;
            this.EventTextBox.CheckForEmpty = false;
            this.EventTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EventTextBox.Digits = -1;
            this.EventTextBox.EmptyDecimalValue = false;
            this.EventTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.EventTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.EventTextBox.IsEditable = true;
            this.EventTextBox.IsQueryableFileld = true;
            this.EventTextBox.Location = new System.Drawing.Point(9, 17);
            this.EventTextBox.LockKeyPress = false;
            this.EventTextBox.MaxLength = 100;
            this.EventTextBox.Name = "EventTextBox";
            this.EventTextBox.PersistDefaultColor = false;
            this.EventTextBox.Precision = 2;
            this.EventTextBox.QueryingFileldName = "";
            this.EventTextBox.SetColorFlag = false;
            this.EventTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.EventTextBox.Size = new System.Drawing.Size(745, 16);
            this.EventTextBox.SpecialCharacter = "%";
            this.EventTextBox.TabIndex = 2;
            this.EventTextBox.TextCustomFormat = "$#,##0.00";
            this.EventTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.EventTextBox.WholeInteger = false;
            this.EventTextBox.TextChanged += new System.EventHandler(this.EventTextBox_TextChanged);
            // 
            // EventLable
            // 
            this.EventLable.AutoSize = true;
            this.EventLable.BackColor = System.Drawing.Color.Transparent;
            this.EventLable.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EventLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.EventLable.Location = new System.Drawing.Point(1, -1);
            this.EventLable.Name = "EventLable";
            this.EventLable.Size = new System.Drawing.Size(40, 14);
            this.EventLable.TabIndex = 0;
            this.EventLable.Text = "Event:";
            // 
            // WorkOrderLinkLablePanel
            // 
            this.WorkOrderLinkLablePanel.BackColor = System.Drawing.Color.White;
            this.WorkOrderLinkLablePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WorkOrderLinkLablePanel.Controls.Add(this.WorkOrdertButton);
            this.WorkOrderLinkLablePanel.Controls.Add(this.WorkOrderLinkLabel);
            this.WorkOrderLinkLablePanel.Controls.Add(this.WorkOrderLinkTextBox);
            this.WorkOrderLinkLablePanel.Controls.Add(this.WorkOrderLabel);
            this.WorkOrderLinkLablePanel.Location = new System.Drawing.Point(596, 41);
            this.WorkOrderLinkLablePanel.Name = "WorkOrderLinkLablePanel";
            this.WorkOrderLinkLablePanel.Size = new System.Drawing.Size(100, 40);
            this.WorkOrderLinkLablePanel.TabIndex = 4;
            this.WorkOrderLinkLablePanel.TabStop = true;
            // 
            // WorkOrdertButton
            // 
            this.WorkOrdertButton.FlatAppearance.BorderSize = 0;
            this.WorkOrdertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WorkOrdertButton.Image = ((System.Drawing.Image)(resources.GetObject("WorkOrdertButton.Image")));
            this.WorkOrdertButton.Location = new System.Drawing.Point(78, 4);
            this.WorkOrdertButton.Name = "WorkOrdertButton";
            this.WorkOrdertButton.Size = new System.Drawing.Size(21, 25);
            this.WorkOrdertButton.TabIndex = 11;
            this.WorkOrdertButton.Tag = "";
            this.WorkOrdertButton.UseVisualStyleBackColor = true;
            this.WorkOrdertButton.Click += new System.EventHandler(this.WorkOrdertButton_Click);
            // 
            // WorkOrderLinkLabel
            // 
            this.WorkOrderLinkLabel.AutoSize = true;
            this.WorkOrderLinkLabel.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.WorkOrderLinkLabel.Location = new System.Drawing.Point(16, 17);
            this.WorkOrderLinkLabel.Name = "WorkOrderLinkLabel";
            this.WorkOrderLinkLabel.Size = new System.Drawing.Size(0, 16);
            this.WorkOrderLinkLabel.TabIndex = 8;
            this.WorkOrderLinkLabel.TextChanged += new System.EventHandler(this.WorkOrderLinkLabel_TextChanged);
            this.WorkOrderLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WorkOrderLinkLabel_LinkClicked);
            // 
            // WorkOrderLinkTextBox
            // 
            this.WorkOrderLinkTextBox.AllowClick = true;
            this.WorkOrderLinkTextBox.AllowNegativeSign = false;
            this.WorkOrderLinkTextBox.ApplyCFGFormat = false;
            this.WorkOrderLinkTextBox.ApplyCurrencyFormat = false;
            this.WorkOrderLinkTextBox.ApplyFocusColor = true;
            this.WorkOrderLinkTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.WorkOrderLinkTextBox.ApplyNegativeStandard = true;
            this.WorkOrderLinkTextBox.ApplyParentFocusColor = true;
            this.WorkOrderLinkTextBox.ApplyTimeFormat = false;
            this.WorkOrderLinkTextBox.BackColor = System.Drawing.Color.White;
            this.WorkOrderLinkTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.WorkOrderLinkTextBox.CFromatWihoutSymbol = false;
            this.WorkOrderLinkTextBox.CheckForEmpty = false;
            this.WorkOrderLinkTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WorkOrderLinkTextBox.Digits = -1;
            this.WorkOrderLinkTextBox.EmptyDecimalValue = false;
            this.WorkOrderLinkTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.WorkOrderLinkTextBox.IsEditable = false;
            this.WorkOrderLinkTextBox.IsQueryableFileld = true;
            this.WorkOrderLinkTextBox.Location = new System.Drawing.Point(3, 17);
            this.WorkOrderLinkTextBox.LockKeyPress = false;
            this.WorkOrderLinkTextBox.MaxLength = 10;
            this.WorkOrderLinkTextBox.Name = "WorkOrderLinkTextBox";
            this.WorkOrderLinkTextBox.PersistDefaultColor = false;
            this.WorkOrderLinkTextBox.Precision = 2;
            this.WorkOrderLinkTextBox.QueryingFileldName = "";
            this.WorkOrderLinkTextBox.SetColorFlag = false;
            this.WorkOrderLinkTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.WorkOrderLinkTextBox.Size = new System.Drawing.Size(71, 16);
            this.WorkOrderLinkTextBox.SpecialCharacter = "%";
            this.WorkOrderLinkTextBox.TabIndex = 0;
            this.WorkOrderLinkTextBox.TabStop = false;
            this.WorkOrderLinkTextBox.TextCustomFormat = "$#,##0.00";
            this.WorkOrderLinkTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.WorkOrderLinkTextBox.Visible = false;
            this.WorkOrderLinkTextBox.WholeInteger = false;
            // 
            // WorkOrderLabel
            // 
            this.WorkOrderLabel.AutoSize = true;
            this.WorkOrderLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkOrderLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.WorkOrderLabel.Location = new System.Drawing.Point(1, -1);
            this.WorkOrderLabel.Name = "WorkOrderLabel";
            this.WorkOrderLabel.Size = new System.Drawing.Size(74, 14);
            this.WorkOrderLabel.TabIndex = 0;
            this.WorkOrderLabel.Text = "Work Order:";
            // 
            // EventDatePanel
            // 
            this.EventDatePanel.BackColor = System.Drawing.Color.White;
            this.EventDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EventDatePanel.Controls.Add(this.GeneralEventDate);
            this.EventDatePanel.Controls.Add(this.EventDateLabel);
            this.EventDatePanel.Controls.Add(this.EventHeaderDatePict);
            this.EventDatePanel.Controls.Add(this.EventDateTimePicker);
            this.EventDatePanel.Location = new System.Drawing.Point(398, 41);
            this.EventDatePanel.Name = "EventDatePanel";
            this.EventDatePanel.Size = new System.Drawing.Size(100, 40);
            this.EventDatePanel.TabIndex = 2;
            this.EventDatePanel.TabStop = true;
            // 
            // GeneralEventDate
            // 
            this.GeneralEventDate.AllowClick = true;
            this.GeneralEventDate.AllowNegativeSign = false;
            this.GeneralEventDate.ApplyCFGFormat = false;
            this.GeneralEventDate.ApplyCurrencyFormat = false;
            this.GeneralEventDate.ApplyFocusColor = true;
            this.GeneralEventDate.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.GeneralEventDate.ApplyNegativeStandard = true;
            this.GeneralEventDate.ApplyParentFocusColor = true;
            this.GeneralEventDate.ApplyTimeFormat = false;
            this.GeneralEventDate.BackColor = System.Drawing.Color.White;
            this.GeneralEventDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GeneralEventDate.CFromatWihoutSymbol = false;
            this.GeneralEventDate.CheckForEmpty = false;
            this.GeneralEventDate.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GeneralEventDate.Digits = -1;
            this.GeneralEventDate.EmptyDecimalValue = false;
            this.GeneralEventDate.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.GeneralEventDate.IsEditable = true;
            this.GeneralEventDate.IsQueryableFileld = true;
            this.GeneralEventDate.Location = new System.Drawing.Point(6, 17);
            this.GeneralEventDate.LockKeyPress = false;
            this.GeneralEventDate.MaxLength = 10;
            this.GeneralEventDate.Name = "GeneralEventDate";
            this.GeneralEventDate.PersistDefaultColor = false;
            this.GeneralEventDate.Precision = 2;
            this.GeneralEventDate.QueryingFileldName = "";
            this.GeneralEventDate.SetColorFlag = false;
            this.GeneralEventDate.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.GeneralEventDate.Size = new System.Drawing.Size(65, 16);
            this.GeneralEventDate.SpecialCharacter = "%";
            this.GeneralEventDate.TabIndex = 7;
            this.GeneralEventDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GeneralEventDate.TextCustomFormat = "";
            this.GeneralEventDate.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.GeneralEventDate.WholeInteger = false;
            this.GeneralEventDate.TextChanged += new System.EventHandler(this.GeneralEventDate_TextChanged);
            this.GeneralEventDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GeneralEventDate_KeyPress);
            // 
            // EventDateLabel
            // 
            this.EventDateLabel.AutoSize = true;
            this.EventDateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EventDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.EventDateLabel.Location = new System.Drawing.Point(1, -1);
            this.EventDateLabel.Name = "EventDateLabel";
            this.EventDateLabel.Size = new System.Drawing.Size(67, 14);
            this.EventDateLabel.TabIndex = 1;
            this.EventDateLabel.Tag = "1105";
            this.EventDateLabel.Text = "Event Date:";
            // 
            // EventHeaderDatePict
            // 
            this.EventHeaderDatePict.FlatAppearance.BorderSize = 0;
            this.EventHeaderDatePict.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EventHeaderDatePict.Image = ((System.Drawing.Image)(resources.GetObject("EventHeaderDatePict.Image")));
            this.EventHeaderDatePict.Location = new System.Drawing.Point(76, 8);
            this.EventHeaderDatePict.Name = "EventHeaderDatePict";
            this.EventHeaderDatePict.Size = new System.Drawing.Size(20, 24);
            this.EventHeaderDatePict.TabIndex = 8;
            this.EventHeaderDatePict.Tag = "ReceiptDateTextBox";
            this.EventHeaderDatePict.UseVisualStyleBackColor = true;
            this.EventHeaderDatePict.Click += new System.EventHandler(this.EventHeaderDatePict_Click);
            // 
            // EventDateTimePicker
            // 
            this.EventDateTimePicker.CustomFormat = "MM/dd/yyyy";
            this.EventDateTimePicker.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EventDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EventDateTimePicker.Location = new System.Drawing.Point(80, 12);
            this.EventDateTimePicker.Margin = new System.Windows.Forms.Padding(0);
            this.EventDateTimePicker.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            this.EventDateTimePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.EventDateTimePicker.Name = "EventDateTimePicker";
            this.EventDateTimePicker.Size = new System.Drawing.Size(10, 20);
            this.EventDateTimePicker.TabIndex = 9;
            this.EventDateTimePicker.TabStop = false;
            this.EventDateTimePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventDateTimePicker_KeyPress);
            this.EventDateTimePicker.CloseUp += new System.EventHandler(this.EventDateTimePicker_CloseUp);
            this.EventDateTimePicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EventDateTimePicker_KeyDown);
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.White;
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Controls.Add(this.StatusColorLabel);
            this.StatusPanel.Controls.Add(this.EventStatus);
            this.StatusPanel.Controls.Add(this.EventStatusTextBox);
            this.StatusPanel.Controls.Add(this.StatusLabel);
            this.StatusPanel.Location = new System.Drawing.Point(1, 41);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(398, 40);
            this.StatusPanel.TabIndex = 1;
            this.StatusPanel.TabStop = true;
            // 
            // StatusColorLabel
            // 
            this.StatusColorLabel.BackColor = System.Drawing.Color.White;
            this.StatusColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusColorLabel.Font = new System.Drawing.Font("Arial", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusColorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.StatusColorLabel.Location = new System.Drawing.Point(361, 6);
            this.StatusColorLabel.Name = "StatusColorLabel";
            this.StatusColorLabel.Size = new System.Drawing.Size(29, 29);
            this.StatusColorLabel.TabIndex = 5;
            // 
            // EventStatus
            // 
            this.EventStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.EventStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.EventStatus.BackColor = System.Drawing.Color.White;
            this.EventStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EventStatus.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.EventStatus.FormattingEnabled = true;
            this.EventStatus.Location = new System.Drawing.Point(12, 12);
            this.EventStatus.Name = "EventStatus";
            this.EventStatus.Size = new System.Drawing.Size(343, 24);
            this.EventStatus.TabIndex = 4;
            this.EventStatus.Validating += new System.ComponentModel.CancelEventHandler(this.EventStatus_Validating);
            this.EventStatus.SelectionChangeCommitted += new System.EventHandler(this.EventStatus_SelectionChangeCommitted);
            this.EventStatus.SelectedIndexChanged += new System.EventHandler(this.EventStatus_SelectedIndexChanged);
            this.EventStatus.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EventStatus_MouseMove);
            this.EventStatus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventStatus_KeyPress);
            this.EventStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EventStatus_KeyDown);
            this.EventStatus.TextChanged += new System.EventHandler(this.EventStatus_TextChanged);
            // 
            // EventStatusTextBox
            // 
            this.EventStatusTextBox.AllowClick = true;
            this.EventStatusTextBox.AllowNegativeSign = false;
            this.EventStatusTextBox.ApplyCFGFormat = false;
            this.EventStatusTextBox.ApplyCurrencyFormat = false;
            this.EventStatusTextBox.ApplyFocusColor = true;
            this.EventStatusTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.EventStatusTextBox.ApplyNegativeStandard = true;
            this.EventStatusTextBox.ApplyParentFocusColor = true;
            this.EventStatusTextBox.ApplyTimeFormat = false;
            this.EventStatusTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EventStatusTextBox.CFromatWihoutSymbol = false;
            this.EventStatusTextBox.CheckForEmpty = false;
            this.EventStatusTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EventStatusTextBox.Digits = -1;
            this.EventStatusTextBox.EmptyDecimalValue = false;
            this.EventStatusTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.EventStatusTextBox.IsEditable = false;
            this.EventStatusTextBox.IsQueryableFileld = true;
            this.EventStatusTextBox.Location = new System.Drawing.Point(17, 14);
            this.EventStatusTextBox.LockKeyPress = false;
            this.EventStatusTextBox.MaxLength = 10;
            this.EventStatusTextBox.Name = "EventStatusTextBox";
            this.EventStatusTextBox.PersistDefaultColor = false;
            this.EventStatusTextBox.Precision = 2;
            this.EventStatusTextBox.QueryingFileldName = "";
            this.EventStatusTextBox.SetColorFlag = false;
            this.EventStatusTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.EventStatusTextBox.Size = new System.Drawing.Size(121, 16);
            this.EventStatusTextBox.SpecialCharacter = "%";
            this.EventStatusTextBox.TabIndex = 4;
            this.EventStatusTextBox.TabStop = false;
            this.EventStatusTextBox.TextCustomFormat = "$#,##0.00";
            this.EventStatusTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.EventStatusTextBox.Visible = false;
            this.EventStatusTextBox.WholeInteger = false;
            this.EventStatusTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventStatusTextBox_KeyPress);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.StatusLabel.Location = new System.Drawing.Point(1, -1);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(48, 14);
            this.StatusLabel.TabIndex = 1;
            this.StatusLabel.Text = "Status: ";
            // 
            // CompletePanel
            // 
            this.CompletePanel.BackColor = System.Drawing.Color.White;
            this.CompletePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CompletePanel.Controls.Add(this.CompleteCheckBox);
            this.CompletePanel.Controls.Add(this.CompleteLabel);
            this.CompletePanel.Location = new System.Drawing.Point(695, 41);
            this.CompletePanel.Name = "CompletePanel";
            this.CompletePanel.Size = new System.Drawing.Size(69, 40);
            this.CompletePanel.TabIndex = 5;
            this.CompletePanel.TabStop = true;
            // 
            // CompleteCheckBox
            // 
            this.CompleteCheckBox.AutoSize = true;
            this.CompleteCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CompleteCheckBox.Location = new System.Drawing.Point(25, 18);
            this.CompleteCheckBox.Name = "CompleteCheckBox";
            this.CompleteCheckBox.Size = new System.Drawing.Size(12, 11);
            this.CompleteCheckBox.TabIndex = 13;
            this.CompleteCheckBox.UseVisualStyleBackColor = true;
            this.CompleteCheckBox.CheckedChanged += new System.EventHandler(this.CompleteCheckBox_CheckedChanged);
            // 
            // CompleteLabel
            // 
            this.CompleteLabel.AutoSize = true;
            this.CompleteLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompleteLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CompleteLabel.Location = new System.Drawing.Point(1, -1);
            this.CompleteLabel.Name = "CompleteLabel";
            this.CompleteLabel.Size = new System.Drawing.Size(64, 14);
            this.CompleteLabel.TabIndex = 0;
            this.CompleteLabel.Text = "Complete:";
            // 
            // EnddatePanel
            // 
            this.EnddatePanel.BackColor = System.Drawing.Color.White;
            this.EnddatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EnddatePanel.Controls.Add(this.EnddateTextBox);
            this.EnddatePanel.Controls.Add(this.EnddateLabel);
            this.EnddatePanel.Controls.Add(this.EnddatePicturebox);
            this.EnddatePanel.Controls.Add(this.EnddatedateTimePicker);
            this.EnddatePanel.Location = new System.Drawing.Point(497, 41);
            this.EnddatePanel.Name = "EnddatePanel";
            this.EnddatePanel.Size = new System.Drawing.Size(100, 40);
            this.EnddatePanel.TabIndex = 3;
            this.EnddatePanel.TabStop = true;
            // 
            // EnddateTextBox
            // 
            this.EnddateTextBox.AllowClick = true;
            this.EnddateTextBox.AllowNegativeSign = false;
            this.EnddateTextBox.ApplyCFGFormat = false;
            this.EnddateTextBox.ApplyCurrencyFormat = false;
            this.EnddateTextBox.ApplyFocusColor = true;
            this.EnddateTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.EnddateTextBox.ApplyNegativeStandard = true;
            this.EnddateTextBox.ApplyParentFocusColor = true;
            this.EnddateTextBox.ApplyTimeFormat = false;
            this.EnddateTextBox.BackColor = System.Drawing.Color.White;
            this.EnddateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EnddateTextBox.CFromatWihoutSymbol = false;
            this.EnddateTextBox.CheckForEmpty = false;
            this.EnddateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EnddateTextBox.Digits = -1;
            this.EnddateTextBox.EmptyDecimalValue = false;
            this.EnddateTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.EnddateTextBox.IsEditable = true;
            this.EnddateTextBox.IsQueryableFileld = true;
            this.EnddateTextBox.Location = new System.Drawing.Point(6, 17);
            this.EnddateTextBox.LockKeyPress = false;
            this.EnddateTextBox.MaxLength = 10;
            this.EnddateTextBox.Name = "EnddateTextBox";
            this.EnddateTextBox.PersistDefaultColor = false;
            this.EnddateTextBox.Precision = 2;
            this.EnddateTextBox.QueryingFileldName = "";
            this.EnddateTextBox.SetColorFlag = false;
            this.EnddateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.EnddateTextBox.Size = new System.Drawing.Size(65, 16);
            this.EnddateTextBox.SpecialCharacter = "%";
            this.EnddateTextBox.TabIndex = 7;
            this.EnddateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.EnddateTextBox.TextCustomFormat = "";
            this.EnddateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.EnddateTextBox.WholeInteger = false;
            this.EnddateTextBox.TextChanged += new System.EventHandler(this.EnddateTextBox_TextChanged);
            this.EnddateTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EnddateTextBox_KeyPress);
            // 
            // EnddateLabel
            // 
            this.EnddateLabel.AutoSize = true;
            this.EnddateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnddateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.EnddateLabel.Location = new System.Drawing.Point(1, -1);
            this.EnddateLabel.Name = "EnddateLabel";
            this.EnddateLabel.Size = new System.Drawing.Size(57, 14);
            this.EnddateLabel.TabIndex = 1;
            this.EnddateLabel.Tag = "1105";
            this.EnddateLabel.Text = "End Date:";
            // 
            // EnddatePicturebox
            // 
            this.EnddatePicturebox.FlatAppearance.BorderSize = 0;
            this.EnddatePicturebox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnddatePicturebox.Image = ((System.Drawing.Image)(resources.GetObject("EnddatePicturebox.Image")));
            this.EnddatePicturebox.Location = new System.Drawing.Point(76, 8);
            this.EnddatePicturebox.Name = "EnddatePicturebox";
            this.EnddatePicturebox.Size = new System.Drawing.Size(20, 24);
            this.EnddatePicturebox.TabIndex = 8;
            this.EnddatePicturebox.Tag = "ReceiptDateTextBox";
            this.EnddatePicturebox.UseVisualStyleBackColor = true;
            this.EnddatePicturebox.Click += new System.EventHandler(this.EnddatePicturebox_Click);
            // 
            // EnddatedateTimePicker
            // 
            this.EnddatedateTimePicker.CustomFormat = "MM/dd/yyyy";
            this.EnddatedateTimePicker.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnddatedateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EnddatedateTimePicker.Location = new System.Drawing.Point(80, 12);
            this.EnddatedateTimePicker.Margin = new System.Windows.Forms.Padding(0);
            this.EnddatedateTimePicker.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            this.EnddatedateTimePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.EnddatedateTimePicker.Name = "EnddatedateTimePicker";
            this.EnddatedateTimePicker.Size = new System.Drawing.Size(10, 20);
            this.EnddatedateTimePicker.TabIndex = 9;
            this.EnddatedateTimePicker.TabStop = false;
            this.EnddatedateTimePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EnddatedateTimePicker_KeyPress);
            this.EnddatedateTimePicker.CloseUp += new System.EventHandler(this.EnddatedateTimePicker_CloseUp);
            this.EnddatedateTimePicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EnddatedateTimePicker_KeyDown);
            // 
            // F8030
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.EnddatePanel);
            this.Controls.Add(this.EventDatePanel);
            this.Controls.Add(this.CompletePanel);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.WorkOrderLinkLablePanel);
            this.Controls.Add(this.EventPanel);
            this.Controls.Add(this.EventPictureBox);
            this.Name = "F8030";
            this.Size = new System.Drawing.Size(804, 82);
            this.Tag = "8030";
            this.Load += new System.EventHandler(this.F8030_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EventPictureBox)).EndInit();
            this.EventPanel.ResumeLayout(false);
            this.EventPanel.PerformLayout();
            this.WorkOrderLinkLablePanel.ResumeLayout(false);
            this.WorkOrderLinkLablePanel.PerformLayout();
            this.EventDatePanel.ResumeLayout(false);
            this.EventDatePanel.PerformLayout();
            this.StatusPanel.ResumeLayout(false);
            this.StatusPanel.PerformLayout();
            this.CompletePanel.ResumeLayout(false);
            this.CompletePanel.PerformLayout();
            this.EnddatePanel.ResumeLayout(false);
            this.EnddatePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox EventPictureBox;
        private System.Windows.Forms.Panel EventPanel;
        private TerraScan.UI.Controls.TerraScanTextBox EventTextBox;
        private System.Windows.Forms.Label EventLable;
        private System.Windows.Forms.Panel WorkOrderLinkLablePanel;
        private System.Windows.Forms.LinkLabel WorkOrderLinkLabel;
        private TerraScan.UI.Controls.TerraScanTextBox WorkOrderLinkTextBox;
        private System.Windows.Forms.Label WorkOrderLabel;
        private System.Windows.Forms.Panel EventDatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox GeneralEventDate;
        private System.Windows.Forms.Label EventDateLabel;
        private System.Windows.Forms.Panel StatusPanel;
        private TerraScan.UI.Controls.TerraScanComboBox EventStatus;
        private TerraScan.UI.Controls.TerraScanTextBox EventStatusTextBox;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Panel CompletePanel;
        private TerraScan.UI.Controls.TerraScanCheckBox CompleteCheckBox;
        private System.Windows.Forms.Label CompleteLabel;
        private System.Windows.Forms.Button WorkOrdertButton;
        private System.Windows.Forms.Label StatusColorLabel;
        private System.Windows.Forms.Button EventHeaderDatePict;
        private System.Windows.Forms.ToolTip GDocEventHeaderToolTip;
        private System.Windows.Forms.DateTimePicker EventDateTimePicker;
        private System.Windows.Forms.Panel EnddatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox EnddateTextBox;
        private System.Windows.Forms.Label EnddateLabel;
        private System.Windows.Forms.Button EnddatePicturebox;
        private System.Windows.Forms.DateTimePicker EnddatedateTimePicker;

    }
}