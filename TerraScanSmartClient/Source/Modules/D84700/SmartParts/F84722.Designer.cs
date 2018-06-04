namespace D84700
{
    partial class F84722
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F84722));
            this.GPSByPanel = new System.Windows.Forms.Panel();
            this.GPSByComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.GPSByLabel = new System.Windows.Forms.Label();
            this.XCoordinatePanel = new System.Windows.Forms.Panel();
            this.XCoordinateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.XCoordinateLabel = new System.Windows.Forms.Label();
            this.YCoordinatePanel = new System.Windows.Forms.Panel();
            this.YCoordinateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.YCoordinateLabel = new System.Windows.Forms.Label();
            this.WaterValveLocationPictureBox = new System.Windows.Forms.PictureBox();
            this.AdministrativeAreaPanel = new System.Windows.Forms.Panel();
            this.AdministrativeAreaComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.AdministrativeAreaLabel = new System.Windows.Forms.Label();
            this.NorthSouthStreetPanel = new System.Windows.Forms.Panel();
            this.NorthSouthStreetComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.NorthSouthStreetLabel = new System.Windows.Forms.Label();
            this.EastWestStreetPanel = new System.Windows.Forms.Panel();
            this.EastWestStreetComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.EastWestStreetLabel = new System.Windows.Forms.Label();
            this.OperationalAreaPanel = new System.Windows.Forms.Panel();
            this.OperationalAreaComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.OperationalAreaLabel = new System.Windows.Forms.Label();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.GridComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.GridLabel = new System.Windows.Forms.Label();
            this.DistrictProjectPanel = new System.Windows.Forms.Panel();
            this.DistrictProjectTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DistrictProjectLabel = new System.Windows.Forms.Label();
            this.DepthPanel = new System.Windows.Forms.Panel();
            this.DepthTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DepthLable = new System.Windows.Forms.Label();
            this.ElevationPanel = new System.Windows.Forms.Panel();
            this.ElevationTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.ElevationLabel = new System.Windows.Forms.Label();
            this.LocationNotesPanel = new System.Windows.Forms.Panel();
            this.LocationNotesTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.LocationNotesLabel = new System.Windows.Forms.Label();
            this.GPSDatePanel = new System.Windows.Forms.Panel();
            this.GPSDateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.GPSDateLabel = new System.Windows.Forms.Label();
            this.GPSDatePict = new System.Windows.Forms.Button();
            this.GPSTimePicker = new System.Windows.Forms.DateTimePicker();
            this.GDocWaterValveLocationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.GPSByPanel.SuspendLayout();
            this.XCoordinatePanel.SuspendLayout();
            this.YCoordinatePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaterValveLocationPictureBox)).BeginInit();
            this.AdministrativeAreaPanel.SuspendLayout();
            this.NorthSouthStreetPanel.SuspendLayout();
            this.EastWestStreetPanel.SuspendLayout();
            this.OperationalAreaPanel.SuspendLayout();
            this.GridPanel.SuspendLayout();
            this.DistrictProjectPanel.SuspendLayout();
            this.DepthPanel.SuspendLayout();
            this.ElevationPanel.SuspendLayout();
            this.LocationNotesPanel.SuspendLayout();
            this.GPSDatePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // GPSByPanel
            // 
            this.GPSByPanel.BackColor = System.Drawing.Color.Transparent;
            this.GPSByPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GPSByPanel.Controls.Add(this.GPSByComboBox);
            this.GPSByPanel.Controls.Add(this.GPSByLabel);
            this.GPSByPanel.Location = new System.Drawing.Point(153, 0);
            this.GPSByPanel.Name = "GPSByPanel";
            this.GPSByPanel.Size = new System.Drawing.Size(267, 40);
            this.GPSByPanel.TabIndex = 4;
            this.GPSByPanel.TabStop = true;
            // 
            // GPSByComboBox
            // 
            this.GPSByComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.GPSByComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GPSByComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GPSByComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.GPSByComboBox.FormattingEnabled = true;
            this.GPSByComboBox.Location = new System.Drawing.Point(12, 12);
            this.GPSByComboBox.Name = "GPSByComboBox";
            this.GPSByComboBox.Size = new System.Drawing.Size(242, 24);
            this.GPSByComboBox.TabIndex = 5;
            this.GPSByComboBox.SelectionChangeCommitted += new System.EventHandler(this.EnableSaveCancelButton);
            this.GPSByComboBox.SelectedIndexChanged += new System.EventHandler(this.GPSByComboBox_SelectedIndexChanged);
            // 
            // GPSByLabel
            // 
            this.GPSByLabel.AutoSize = true;
            this.GPSByLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GPSByLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.GPSByLabel.Location = new System.Drawing.Point(1, -1);
            this.GPSByLabel.Name = "GPSByLabel";
            this.GPSByLabel.Size = new System.Drawing.Size(51, 14);
            this.GPSByLabel.TabIndex = 1;
            this.GPSByLabel.Text = "GPS By: ";
            // 
            // XCoordinatePanel
            // 
            this.XCoordinatePanel.BackColor = System.Drawing.Color.Transparent;
            this.XCoordinatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.XCoordinatePanel.Controls.Add(this.XCoordinateTextBox);
            this.XCoordinatePanel.Controls.Add(this.XCoordinateLabel);
            this.XCoordinatePanel.Location = new System.Drawing.Point(419, 0);
            this.XCoordinatePanel.Name = "XCoordinatePanel";
            this.XCoordinatePanel.Size = new System.Drawing.Size(162, 40);
            this.XCoordinatePanel.TabIndex = 6;
            this.XCoordinatePanel.TabStop = true;
            // 
            // XCoordinateTextBox
            // 
            this.XCoordinateTextBox.AllowClick = true;
            this.XCoordinateTextBox.AllowNegativeSign = true;
            this.XCoordinateTextBox.ApplyCFGFormat = true;
            this.XCoordinateTextBox.ApplyCurrencyFormat = true;
            this.XCoordinateTextBox.ApplyFocusColor = true;
            this.XCoordinateTextBox.ApplyNegativeStandard = false;
            this.XCoordinateTextBox.ApplyParentFocusColor = true;
            this.XCoordinateTextBox.ApplyTimeFormat = false;
            this.XCoordinateTextBox.BackColor = System.Drawing.Color.White;
            this.XCoordinateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.XCoordinateTextBox.CFromatWihoutSymbol = false;
            this.XCoordinateTextBox.CheckForEmpty = false;
            this.XCoordinateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.XCoordinateTextBox.Digits = 14;
            this.XCoordinateTextBox.EmptyDecimalValue = false;
            this.XCoordinateTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.XCoordinateTextBox.ForeColor = System.Drawing.Color.Black;
            this.XCoordinateTextBox.IsEditable = false;
            this.XCoordinateTextBox.IsQueryableFileld = true;
            this.XCoordinateTextBox.Location = new System.Drawing.Point(11, 18);
            this.XCoordinateTextBox.LockKeyPress = false;
            this.XCoordinateTextBox.MaxLength = 0;
            this.XCoordinateTextBox.Name = "XCoordinateTextBox";
            this.XCoordinateTextBox.PersistDefaultColor = false;
            this.XCoordinateTextBox.Precision = 3;
            this.XCoordinateTextBox.QueryingFileldName = "";
            this.XCoordinateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.XCoordinateTextBox.Size = new System.Drawing.Size(145, 16);
            this.XCoordinateTextBox.SpecialCharacter = "%";
            this.XCoordinateTextBox.TabIndex = 7;
            this.XCoordinateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.XCoordinateTextBox.TextCustomFormat = "#,##0.000";
            this.XCoordinateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.XCoordinateTextBox.WholeInteger = false;
            this.XCoordinateTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // XCoordinateLabel
            // 
            this.XCoordinateLabel.AutoSize = true;
            this.XCoordinateLabel.BackColor = System.Drawing.Color.Transparent;
            this.XCoordinateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XCoordinateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.XCoordinateLabel.Location = new System.Drawing.Point(1, -1);
            this.XCoordinateLabel.Name = "XCoordinateLabel";
            this.XCoordinateLabel.Size = new System.Drawing.Size(81, 14);
            this.XCoordinateLabel.TabIndex = 0;
            this.XCoordinateLabel.Text = "X Coordinate:";
            // 
            // YCoordinatePanel
            // 
            this.YCoordinatePanel.BackColor = System.Drawing.Color.Transparent;
            this.YCoordinatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.YCoordinatePanel.Controls.Add(this.YCoordinateTextBox);
            this.YCoordinatePanel.Controls.Add(this.YCoordinateLabel);
            this.YCoordinatePanel.Location = new System.Drawing.Point(580, 0);
            this.YCoordinatePanel.Name = "YCoordinatePanel";
            this.YCoordinatePanel.Size = new System.Drawing.Size(188, 40);
            this.YCoordinatePanel.TabIndex = 8;
            this.YCoordinatePanel.TabStop = true;
            // 
            // YCoordinateTextBox
            // 
            this.YCoordinateTextBox.AllowClick = true;
            this.YCoordinateTextBox.AllowNegativeSign = true;
            this.YCoordinateTextBox.ApplyCFGFormat = true;
            this.YCoordinateTextBox.ApplyCurrencyFormat = true;
            this.YCoordinateTextBox.ApplyFocusColor = true;
            this.YCoordinateTextBox.ApplyNegativeStandard = false;
            this.YCoordinateTextBox.ApplyParentFocusColor = true;
            this.YCoordinateTextBox.ApplyTimeFormat = false;
            this.YCoordinateTextBox.BackColor = System.Drawing.Color.White;
            this.YCoordinateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.YCoordinateTextBox.CFromatWihoutSymbol = false;
            this.YCoordinateTextBox.CheckForEmpty = false;
            this.YCoordinateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.YCoordinateTextBox.Digits = 14;
            this.YCoordinateTextBox.EmptyDecimalValue = false;
            this.YCoordinateTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.YCoordinateTextBox.ForeColor = System.Drawing.Color.Black;
            this.YCoordinateTextBox.IsEditable = false;
            this.YCoordinateTextBox.IsQueryableFileld = true;
            this.YCoordinateTextBox.Location = new System.Drawing.Point(11, 18);
            this.YCoordinateTextBox.LockKeyPress = false;
            this.YCoordinateTextBox.MaxLength = 0;
            this.YCoordinateTextBox.Name = "YCoordinateTextBox";
            this.YCoordinateTextBox.PersistDefaultColor = false;
            this.YCoordinateTextBox.Precision = 3;
            this.YCoordinateTextBox.QueryingFileldName = "";
            this.YCoordinateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.YCoordinateTextBox.Size = new System.Drawing.Size(169, 16);
            this.YCoordinateTextBox.SpecialCharacter = "%";
            this.YCoordinateTextBox.TabIndex = 9;
            this.YCoordinateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.YCoordinateTextBox.TextCustomFormat = "#,##0.000";
            this.YCoordinateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.YCoordinateTextBox.WholeInteger = false;
            this.YCoordinateTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // YCoordinateLabel
            // 
            this.YCoordinateLabel.AutoSize = true;
            this.YCoordinateLabel.BackColor = System.Drawing.Color.Transparent;
            this.YCoordinateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YCoordinateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.YCoordinateLabel.Location = new System.Drawing.Point(1, -1);
            this.YCoordinateLabel.Name = "YCoordinateLabel";
            this.YCoordinateLabel.Size = new System.Drawing.Size(81, 14);
            this.YCoordinateLabel.TabIndex = 0;
            this.YCoordinateLabel.Text = "Y Coordinate:";
            // 
            // WaterValveLocationPictureBox
            // 
            this.WaterValveLocationPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("WaterValveLocationPictureBox.Image")));
            this.WaterValveLocationPictureBox.Location = new System.Drawing.Point(761, 0);
            this.WaterValveLocationPictureBox.Name = "WaterValveLocationPictureBox";
            this.WaterValveLocationPictureBox.Size = new System.Drawing.Size(42, 157);
            this.WaterValveLocationPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WaterValveLocationPictureBox.TabIndex = 12;
            this.WaterValveLocationPictureBox.TabStop = false;
            this.WaterValveLocationPictureBox.Click += new System.EventHandler(this.WaterValveLocationPictureBox_Click);
            this.WaterValveLocationPictureBox.MouseEnter += new System.EventHandler(this.WaterValveLocationPictureBox_MouseEnter);
            // 
            // AdministrativeAreaPanel
            // 
            this.AdministrativeAreaPanel.BackColor = System.Drawing.Color.Transparent;
            this.AdministrativeAreaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdministrativeAreaPanel.Controls.Add(this.AdministrativeAreaComboBox);
            this.AdministrativeAreaPanel.Controls.Add(this.AdministrativeAreaLabel);
            this.AdministrativeAreaPanel.Location = new System.Drawing.Point(0, 39);
            this.AdministrativeAreaPanel.Name = "AdministrativeAreaPanel";
            this.AdministrativeAreaPanel.Size = new System.Drawing.Size(174, 40);
            this.AdministrativeAreaPanel.TabIndex = 10;
            this.AdministrativeAreaPanel.TabStop = true;
            // 
            // AdministrativeAreaComboBox
            // 
            this.AdministrativeAreaComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.AdministrativeAreaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AdministrativeAreaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AdministrativeAreaComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.AdministrativeAreaComboBox.FormattingEnabled = true;
            this.AdministrativeAreaComboBox.Location = new System.Drawing.Point(12, 12);
            this.AdministrativeAreaComboBox.Name = "AdministrativeAreaComboBox";
            this.AdministrativeAreaComboBox.Size = new System.Drawing.Size(149, 24);
            this.AdministrativeAreaComboBox.TabIndex = 11;
            this.AdministrativeAreaComboBox.SelectionChangeCommitted += new System.EventHandler(this.EnableSaveCancelButton);
            this.AdministrativeAreaComboBox.SelectedIndexChanged += new System.EventHandler(this.AdministrativeAreaComboBox_SelectedIndexChanged);
            // 
            // AdministrativeAreaLabel
            // 
            this.AdministrativeAreaLabel.AutoSize = true;
            this.AdministrativeAreaLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdministrativeAreaLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.AdministrativeAreaLabel.Location = new System.Drawing.Point(1, -1);
            this.AdministrativeAreaLabel.Name = "AdministrativeAreaLabel";
            this.AdministrativeAreaLabel.Size = new System.Drawing.Size(123, 14);
            this.AdministrativeAreaLabel.TabIndex = 1;
            this.AdministrativeAreaLabel.Text = "Administrative Area: ";
            // 
            // NorthSouthStreetPanel
            // 
            this.NorthSouthStreetPanel.BackColor = System.Drawing.Color.Transparent;
            this.NorthSouthStreetPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NorthSouthStreetPanel.Controls.Add(this.NorthSouthStreetComboBox);
            this.NorthSouthStreetPanel.Controls.Add(this.NorthSouthStreetLabel);
            this.NorthSouthStreetPanel.Location = new System.Drawing.Point(0, 78);
            this.NorthSouthStreetPanel.Name = "NorthSouthStreetPanel";
            this.NorthSouthStreetPanel.Size = new System.Drawing.Size(174, 40);
            this.NorthSouthStreetPanel.TabIndex = 18;
            this.NorthSouthStreetPanel.TabStop = true;
            // 
            // NorthSouthStreetComboBox
            // 
            this.NorthSouthStreetComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.NorthSouthStreetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NorthSouthStreetComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NorthSouthStreetComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.NorthSouthStreetComboBox.FormattingEnabled = true;
            this.NorthSouthStreetComboBox.Location = new System.Drawing.Point(12, 12);
            this.NorthSouthStreetComboBox.Name = "NorthSouthStreetComboBox";
            this.NorthSouthStreetComboBox.Size = new System.Drawing.Size(149, 24);
            this.NorthSouthStreetComboBox.TabIndex = 19;
            this.NorthSouthStreetComboBox.SelectionChangeCommitted += new System.EventHandler(this.EnableSaveCancelButton);
            this.NorthSouthStreetComboBox.SelectedIndexChanged += new System.EventHandler(this.NorthSouthStreetComboBox_SelectedIndexChanged);
            // 
            // NorthSouthStreetLabel
            // 
            this.NorthSouthStreetLabel.AutoSize = true;
            this.NorthSouthStreetLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NorthSouthStreetLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.NorthSouthStreetLabel.Location = new System.Drawing.Point(1, -1);
            this.NorthSouthStreetLabel.Name = "NorthSouthStreetLabel";
            this.NorthSouthStreetLabel.Size = new System.Drawing.Size(115, 14);
            this.NorthSouthStreetLabel.TabIndex = 1;
            this.NorthSouthStreetLabel.Text = "North/South Street: ";
            // 
            // EastWestStreetPanel
            // 
            this.EastWestStreetPanel.BackColor = System.Drawing.Color.Transparent;
            this.EastWestStreetPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EastWestStreetPanel.Controls.Add(this.EastWestStreetComboBox);
            this.EastWestStreetPanel.Controls.Add(this.EastWestStreetLabel);
            this.EastWestStreetPanel.Location = new System.Drawing.Point(0, 117);
            this.EastWestStreetPanel.Name = "EastWestStreetPanel";
            this.EastWestStreetPanel.Size = new System.Drawing.Size(174, 40);
            this.EastWestStreetPanel.TabIndex = 22;
            this.EastWestStreetPanel.TabStop = true;
            // 
            // EastWestStreetComboBox
            // 
            this.EastWestStreetComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.EastWestStreetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EastWestStreetComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EastWestStreetComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.EastWestStreetComboBox.FormattingEnabled = true;
            this.EastWestStreetComboBox.Location = new System.Drawing.Point(12, 12);
            this.EastWestStreetComboBox.Name = "EastWestStreetComboBox";
            this.EastWestStreetComboBox.Size = new System.Drawing.Size(149, 24);
            this.EastWestStreetComboBox.TabIndex = 23;
            this.EastWestStreetComboBox.SelectionChangeCommitted += new System.EventHandler(this.EnableSaveCancelButton);
            this.EastWestStreetComboBox.SelectedIndexChanged += new System.EventHandler(this.EastWestStreetComboBox_SelectedIndexChanged);
            // 
            // EastWestStreetLabel
            // 
            this.EastWestStreetLabel.AutoSize = true;
            this.EastWestStreetLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EastWestStreetLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.EastWestStreetLabel.Location = new System.Drawing.Point(1, -1);
            this.EastWestStreetLabel.Name = "EastWestStreetLabel";
            this.EastWestStreetLabel.Size = new System.Drawing.Size(104, 14);
            this.EastWestStreetLabel.TabIndex = 1;
            this.EastWestStreetLabel.Text = "East/West Street: ";
            // 
            // OperationalAreaPanel
            // 
            this.OperationalAreaPanel.BackColor = System.Drawing.Color.Transparent;
            this.OperationalAreaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OperationalAreaPanel.Controls.Add(this.OperationalAreaComboBox);
            this.OperationalAreaPanel.Controls.Add(this.OperationalAreaLabel);
            this.OperationalAreaPanel.Location = new System.Drawing.Point(173, 39);
            this.OperationalAreaPanel.Name = "OperationalAreaPanel";
            this.OperationalAreaPanel.Size = new System.Drawing.Size(256, 40);
            this.OperationalAreaPanel.TabIndex = 12;
            this.OperationalAreaPanel.TabStop = true;
            // 
            // OperationalAreaComboBox
            // 
            this.OperationalAreaComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.OperationalAreaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OperationalAreaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OperationalAreaComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.OperationalAreaComboBox.FormattingEnabled = true;
            this.OperationalAreaComboBox.Location = new System.Drawing.Point(12, 12);
            this.OperationalAreaComboBox.Name = "OperationalAreaComboBox";
            this.OperationalAreaComboBox.Size = new System.Drawing.Size(231, 24);
            this.OperationalAreaComboBox.TabIndex = 13;
            this.OperationalAreaComboBox.SelectionChangeCommitted += new System.EventHandler(this.EnableSaveCancelButton);
            this.OperationalAreaComboBox.SelectedIndexChanged += new System.EventHandler(this.OperationalAreaComboBox_SelectedIndexChanged);
            // 
            // OperationalAreaLabel
            // 
            this.OperationalAreaLabel.AutoSize = true;
            this.OperationalAreaLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OperationalAreaLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.OperationalAreaLabel.Location = new System.Drawing.Point(1, -1);
            this.OperationalAreaLabel.Name = "OperationalAreaLabel";
            this.OperationalAreaLabel.Size = new System.Drawing.Size(105, 14);
            this.OperationalAreaLabel.TabIndex = 1;
            this.OperationalAreaLabel.Text = "Operational Area: ";
            // 
            // GridPanel
            // 
            this.GridPanel.BackColor = System.Drawing.Color.Transparent;
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.GridComboBox);
            this.GridPanel.Controls.Add(this.GridLabel);
            this.GridPanel.Location = new System.Drawing.Point(428, 39);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(142, 40);
            this.GridPanel.TabIndex = 14;
            this.GridPanel.TabStop = true;
            // 
            // GridComboBox
            // 
            this.GridComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.GridComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GridComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GridComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.GridComboBox.FormattingEnabled = true;
            this.GridComboBox.Location = new System.Drawing.Point(12, 12);
            this.GridComboBox.Name = "GridComboBox";
            this.GridComboBox.Size = new System.Drawing.Size(118, 24);
            this.GridComboBox.TabIndex = 15;
            this.GridComboBox.SelectionChangeCommitted += new System.EventHandler(this.EnableSaveCancelButton);
            this.GridComboBox.SelectedIndexChanged += new System.EventHandler(this.GridComboBox_SelectedIndexChanged);
            // 
            // GridLabel
            // 
            this.GridLabel.AutoSize = true;
            this.GridLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.GridLabel.Location = new System.Drawing.Point(1, -1);
            this.GridLabel.Name = "GridLabel";
            this.GridLabel.Size = new System.Drawing.Size(36, 14);
            this.GridLabel.TabIndex = 1;
            this.GridLabel.Text = "Grid: ";
            // 
            // DistrictProjectPanel
            // 
            this.DistrictProjectPanel.BackColor = System.Drawing.Color.Transparent;
            this.DistrictProjectPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DistrictProjectPanel.Controls.Add(this.DistrictProjectTextBox);
            this.DistrictProjectPanel.Controls.Add(this.DistrictProjectLabel);
            this.DistrictProjectPanel.Location = new System.Drawing.Point(569, 39);
            this.DistrictProjectPanel.Name = "DistrictProjectPanel";
            this.DistrictProjectPanel.Size = new System.Drawing.Size(199, 40);
            this.DistrictProjectPanel.TabIndex = 16;
            this.DistrictProjectPanel.TabStop = true;
            // 
            // DistrictProjectTextBox
            // 
            this.DistrictProjectTextBox.AllowClick = true;
            this.DistrictProjectTextBox.AllowNegativeSign = false;
            this.DistrictProjectTextBox.ApplyCFGFormat = false;
            this.DistrictProjectTextBox.ApplyCurrencyFormat = false;
            this.DistrictProjectTextBox.ApplyFocusColor = true;
            this.DistrictProjectTextBox.ApplyNegativeStandard = true;
            this.DistrictProjectTextBox.ApplyParentFocusColor = true;
            this.DistrictProjectTextBox.ApplyTimeFormat = false;
            this.DistrictProjectTextBox.BackColor = System.Drawing.Color.White;
            this.DistrictProjectTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DistrictProjectTextBox.CFromatWihoutSymbol = false;
            this.DistrictProjectTextBox.CheckForEmpty = false;
            this.DistrictProjectTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DistrictProjectTextBox.Digits = -1;
            this.DistrictProjectTextBox.EmptyDecimalValue = false;
            this.DistrictProjectTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.DistrictProjectTextBox.ForeColor = System.Drawing.Color.Black;
            this.DistrictProjectTextBox.IsEditable = false;
            this.DistrictProjectTextBox.IsQueryableFileld = true;
            this.DistrictProjectTextBox.Location = new System.Drawing.Point(9, 18);
            this.DistrictProjectTextBox.LockKeyPress = false;
            this.DistrictProjectTextBox.MaxLength = 4;
            this.DistrictProjectTextBox.Name = "DistrictProjectTextBox";
            this.DistrictProjectTextBox.PersistDefaultColor = false;
            this.DistrictProjectTextBox.Precision = 2;
            this.DistrictProjectTextBox.QueryingFileldName = "";
            this.DistrictProjectTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DistrictProjectTextBox.Size = new System.Drawing.Size(178, 16);
            this.DistrictProjectTextBox.SpecialCharacter = "%";
            this.DistrictProjectTextBox.TabIndex = 17;
            this.DistrictProjectTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DistrictProjectTextBox.TextCustomFormat = "$#,##0.00";
            this.DistrictProjectTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DistrictProjectTextBox.WholeInteger = false;
            this.DistrictProjectTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // DistrictProjectLabel
            // 
            this.DistrictProjectLabel.AutoSize = true;
            this.DistrictProjectLabel.BackColor = System.Drawing.Color.Transparent;
            this.DistrictProjectLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistrictProjectLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DistrictProjectLabel.Location = new System.Drawing.Point(1, -1);
            this.DistrictProjectLabel.Name = "DistrictProjectLabel";
            this.DistrictProjectLabel.Size = new System.Drawing.Size(97, 14);
            this.DistrictProjectLabel.TabIndex = 0;
            this.DistrictProjectLabel.Text = "District / Project:";
            // 
            // DepthPanel
            // 
            this.DepthPanel.BackColor = System.Drawing.Color.Transparent;
            this.DepthPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DepthPanel.Controls.Add(this.DepthTextBox);
            this.DepthPanel.Controls.Add(this.DepthLable);
            this.DepthPanel.Location = new System.Drawing.Point(173, 117);
            this.DepthPanel.Name = "DepthPanel";
            this.DepthPanel.Size = new System.Drawing.Size(126, 40);
            this.DepthPanel.TabIndex = 24;
            this.DepthPanel.TabStop = true;
            // 
            // DepthTextBox
            // 
            this.DepthTextBox.AllowClick = true;
            this.DepthTextBox.AllowNegativeSign = true;
            this.DepthTextBox.ApplyCFGFormat = true;
            this.DepthTextBox.ApplyCurrencyFormat = true;
            this.DepthTextBox.ApplyFocusColor = true;
            this.DepthTextBox.ApplyNegativeStandard = false;
            this.DepthTextBox.ApplyParentFocusColor = true;
            this.DepthTextBox.ApplyTimeFormat = false;
            this.DepthTextBox.BackColor = System.Drawing.Color.White;
            this.DepthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DepthTextBox.CFromatWihoutSymbol = false;
            this.DepthTextBox.CheckForEmpty = false;
            this.DepthTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DepthTextBox.Digits = 14;
            this.DepthTextBox.EmptyDecimalValue = false;
            this.DepthTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.DepthTextBox.ForeColor = System.Drawing.Color.Black;
            this.DepthTextBox.IsEditable = false;
            this.DepthTextBox.IsQueryableFileld = true;
            this.DepthTextBox.Location = new System.Drawing.Point(9, 18);
            this.DepthTextBox.LockKeyPress = false;
            this.DepthTextBox.MaxLength = 0;
            this.DepthTextBox.Name = "DepthTextBox";
            this.DepthTextBox.PersistDefaultColor = false;
            this.DepthTextBox.Precision = 3;
            this.DepthTextBox.QueryingFileldName = "";
            this.DepthTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DepthTextBox.Size = new System.Drawing.Size(111, 16);
            this.DepthTextBox.SpecialCharacter = "%";
            this.DepthTextBox.TabIndex = 25;
            this.DepthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DepthTextBox.TextCustomFormat = "#,##0.000";
            this.DepthTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.DepthTextBox.WholeInteger = false;
            this.DepthTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // DepthLable
            // 
            this.DepthLable.AutoSize = true;
            this.DepthLable.BackColor = System.Drawing.Color.Transparent;
            this.DepthLable.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DepthLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DepthLable.Location = new System.Drawing.Point(1, -1);
            this.DepthLable.Name = "DepthLable";
            this.DepthLable.Size = new System.Drawing.Size(42, 14);
            this.DepthLable.TabIndex = 0;
            this.DepthLable.Text = "Depth:";
            // 
            // ElevationPanel
            // 
            this.ElevationPanel.BackColor = System.Drawing.Color.Transparent;
            this.ElevationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ElevationPanel.Controls.Add(this.ElevationTextBox);
            this.ElevationPanel.Controls.Add(this.ElevationLabel);
            this.ElevationPanel.Location = new System.Drawing.Point(173, 78);
            this.ElevationPanel.Name = "ElevationPanel";
            this.ElevationPanel.Size = new System.Drawing.Size(126, 40);
            this.ElevationPanel.TabIndex = 20;
            this.ElevationPanel.TabStop = true;
            // 
            // ElevationTextBox
            // 
            this.ElevationTextBox.AllowClick = true;
            this.ElevationTextBox.AllowNegativeSign = false;
            this.ElevationTextBox.ApplyCFGFormat = true;
            this.ElevationTextBox.ApplyCurrencyFormat = true;
            this.ElevationTextBox.ApplyFocusColor = true;
            this.ElevationTextBox.ApplyNegativeStandard = false;
            this.ElevationTextBox.ApplyParentFocusColor = true;
            this.ElevationTextBox.ApplyTimeFormat = false;
            this.ElevationTextBox.BackColor = System.Drawing.Color.White;
            this.ElevationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ElevationTextBox.CFromatWihoutSymbol = false;
            this.ElevationTextBox.CheckForEmpty = false;
            this.ElevationTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ElevationTextBox.Digits = 14;
            this.ElevationTextBox.EmptyDecimalValue = false;
            this.ElevationTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.ElevationTextBox.ForeColor = System.Drawing.Color.Black;
            this.ElevationTextBox.IsEditable = false;
            this.ElevationTextBox.IsQueryableFileld = true;
            this.ElevationTextBox.Location = new System.Drawing.Point(9, 18);
            this.ElevationTextBox.LockKeyPress = false;
            this.ElevationTextBox.MaxLength = 0;
            this.ElevationTextBox.Name = "ElevationTextBox";
            this.ElevationTextBox.PersistDefaultColor = false;
            this.ElevationTextBox.Precision = 3;
            this.ElevationTextBox.QueryingFileldName = "";
            this.ElevationTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ElevationTextBox.Size = new System.Drawing.Size(111, 16);
            this.ElevationTextBox.SpecialCharacter = "%";
            this.ElevationTextBox.TabIndex = 21;
            this.ElevationTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ElevationTextBox.TextCustomFormat = "#,##0.000";
            this.ElevationTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.ElevationTextBox.WholeInteger = false;
            this.ElevationTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // ElevationLabel
            // 
            this.ElevationLabel.AutoSize = true;
            this.ElevationLabel.BackColor = System.Drawing.Color.Transparent;
            this.ElevationLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElevationLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ElevationLabel.Location = new System.Drawing.Point(1, -1);
            this.ElevationLabel.Name = "ElevationLabel";
            this.ElevationLabel.Size = new System.Drawing.Size(59, 14);
            this.ElevationLabel.TabIndex = 0;
            this.ElevationLabel.Text = "Elevation:";
            // 
            // LocationNotesPanel
            // 
            this.LocationNotesPanel.BackColor = System.Drawing.Color.Transparent;
            this.LocationNotesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LocationNotesPanel.Controls.Add(this.LocationNotesTextBox);
            this.LocationNotesPanel.Controls.Add(this.LocationNotesLabel);
            this.LocationNotesPanel.Location = new System.Drawing.Point(298, 78);
            this.LocationNotesPanel.Name = "LocationNotesPanel";
            this.LocationNotesPanel.Size = new System.Drawing.Size(470, 79);
            this.LocationNotesPanel.TabIndex = 26;
            // 
            // LocationNotesTextBox
            // 
            this.LocationNotesTextBox.AllowClick = true;
            this.LocationNotesTextBox.AllowNegativeSign = false;
            this.LocationNotesTextBox.ApplyCFGFormat = false;
            this.LocationNotesTextBox.ApplyCurrencyFormat = false;
            this.LocationNotesTextBox.ApplyFocusColor = true;
            this.LocationNotesTextBox.ApplyNegativeStandard = true;
            this.LocationNotesTextBox.ApplyParentFocusColor = true;
            this.LocationNotesTextBox.ApplyTimeFormat = false;
            this.LocationNotesTextBox.BackColor = System.Drawing.Color.White;
            this.LocationNotesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LocationNotesTextBox.CFromatWihoutSymbol = false;
            this.LocationNotesTextBox.CheckForEmpty = false;
            this.LocationNotesTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.LocationNotesTextBox.Digits = -1;
            this.LocationNotesTextBox.EmptyDecimalValue = false;
            this.LocationNotesTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.LocationNotesTextBox.ForeColor = System.Drawing.Color.Black;
            this.LocationNotesTextBox.IsEditable = false;
            this.LocationNotesTextBox.IsQueryableFileld = false;
            this.LocationNotesTextBox.Location = new System.Drawing.Point(14, 14);
            this.LocationNotesTextBox.LockKeyPress = false;
            this.LocationNotesTextBox.MaxLength = 200;
            this.LocationNotesTextBox.Multiline = true;
            this.LocationNotesTextBox.Name = "LocationNotesTextBox";
            this.LocationNotesTextBox.PersistDefaultColor = false;
            this.LocationNotesTextBox.Precision = 2;
            this.LocationNotesTextBox.QueryingFileldName = "";
            this.LocationNotesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LocationNotesTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.LocationNotesTextBox.Size = new System.Drawing.Size(452, 59);
            this.LocationNotesTextBox.SpecialCharacter = "%";
            this.LocationNotesTextBox.TabIndex = 27;
            this.LocationNotesTextBox.Tag = "";
            this.LocationNotesTextBox.TextCustomFormat = "$#,##0.00";
            this.LocationNotesTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.LocationNotesTextBox.WholeInteger = false;
            this.LocationNotesTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LocationNotesTextBox_KeyPress);
            this.LocationNotesTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // LocationNotesLabel
            // 
            this.LocationNotesLabel.AutoSize = true;
            this.LocationNotesLabel.BackColor = System.Drawing.Color.Transparent;
            this.LocationNotesLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationNotesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.LocationNotesLabel.Location = new System.Drawing.Point(1, -1);
            this.LocationNotesLabel.Name = "LocationNotesLabel";
            this.LocationNotesLabel.Size = new System.Drawing.Size(92, 14);
            this.LocationNotesLabel.TabIndex = 0;
            this.LocationNotesLabel.Text = "Location Notes:";
            // 
            // GPSDatePanel
            // 
            this.GPSDatePanel.BackColor = System.Drawing.Color.Transparent;
            this.GPSDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GPSDatePanel.Controls.Add(this.GPSDateTextBox);
            this.GPSDatePanel.Controls.Add(this.GPSDateLabel);
            this.GPSDatePanel.Controls.Add(this.GPSDatePict);
            this.GPSDatePanel.Controls.Add(this.GPSTimePicker);
            this.GPSDatePanel.Location = new System.Drawing.Point(0, 0);
            this.GPSDatePanel.Name = "GPSDatePanel";
            this.GPSDatePanel.Size = new System.Drawing.Size(154, 40);
            this.GPSDatePanel.TabIndex = 1;
            this.GPSDatePanel.TabStop = true;
            // 
            // GPSDateTextBox
            // 
            this.GPSDateTextBox.AllowClick = true;
            this.GPSDateTextBox.AllowNegativeSign = false;
            this.GPSDateTextBox.ApplyCFGFormat = false;
            this.GPSDateTextBox.ApplyCurrencyFormat = false;
            this.GPSDateTextBox.ApplyFocusColor = true;
            this.GPSDateTextBox.ApplyNegativeStandard = true;
            this.GPSDateTextBox.ApplyParentFocusColor = true;
            this.GPSDateTextBox.ApplyTimeFormat = false;
            this.GPSDateTextBox.BackColor = System.Drawing.Color.White;
            this.GPSDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GPSDateTextBox.CFromatWihoutSymbol = false;
            this.GPSDateTextBox.CheckForEmpty = false;
            this.GPSDateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GPSDateTextBox.Digits = -1;
            this.GPSDateTextBox.EmptyDecimalValue = false;
            this.GPSDateTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.GPSDateTextBox.IsEditable = true;
            this.GPSDateTextBox.IsQueryableFileld = true;
            this.GPSDateTextBox.Location = new System.Drawing.Point(6, 18);
            this.GPSDateTextBox.LockKeyPress = false;
            this.GPSDateTextBox.MaxLength = 10;
            this.GPSDateTextBox.Name = "GPSDateTextBox";
            this.GPSDateTextBox.PersistDefaultColor = false;
            this.GPSDateTextBox.Precision = 2;
            this.GPSDateTextBox.QueryingFileldName = "";
            this.GPSDateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.GPSDateTextBox.Size = new System.Drawing.Size(116, 16);
            this.GPSDateTextBox.SpecialCharacter = "%";
            this.GPSDateTextBox.TabIndex = 2;
            this.GPSDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GPSDateTextBox.TextCustomFormat = "";
            this.GPSDateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.GPSDateTextBox.WholeInteger = false;
            this.GPSDateTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // GPSDateLabel
            // 
            this.GPSDateLabel.AutoSize = true;
            this.GPSDateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GPSDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.GPSDateLabel.Location = new System.Drawing.Point(1, -1);
            this.GPSDateLabel.Name = "GPSDateLabel";
            this.GPSDateLabel.Size = new System.Drawing.Size(59, 14);
            this.GPSDateLabel.TabIndex = 1;
            this.GPSDateLabel.Tag = "1105";
            this.GPSDateLabel.Text = "GPS Date:";
            // 
            // GPSDatePict
            // 
            this.GPSDatePict.FlatAppearance.BorderSize = 0;
            this.GPSDatePict.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GPSDatePict.Image = ((System.Drawing.Image)(resources.GetObject("GPSDatePict.Image")));
            this.GPSDatePict.Location = new System.Drawing.Point(127, 8);
            this.GPSDatePict.Name = "GPSDatePict";
            this.GPSDatePict.Size = new System.Drawing.Size(20, 24);
            this.GPSDatePict.TabIndex = 3;
            this.GPSDatePict.Tag = "ReceiptDateTextBox";
            this.GPSDatePict.UseVisualStyleBackColor = true;
            this.GPSDatePict.Click += new System.EventHandler(this.GPSDatePict_Click);
            // 
            // GPSTimePicker
            // 
            this.GPSTimePicker.CustomFormat = "M/d/yyyy";
            this.GPSTimePicker.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GPSTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.GPSTimePicker.Location = new System.Drawing.Point(133, 9);
            this.GPSTimePicker.Margin = new System.Windows.Forms.Padding(0);
            this.GPSTimePicker.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            this.GPSTimePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.GPSTimePicker.Name = "GPSTimePicker";
            this.GPSTimePicker.Size = new System.Drawing.Size(10, 20);
            this.GPSTimePicker.TabIndex = 0;
            this.GPSTimePicker.TabStop = false;
            this.GPSTimePicker.CloseUp += new System.EventHandler(this.GPSTimePicker_CloseUp);
            this.GPSTimePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GPSTimePicker_KeyPress);
            // 
            // F84722
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.GPSDatePanel);
            this.Controls.Add(this.LocationNotesPanel);
            this.Controls.Add(this.ElevationPanel);
            this.Controls.Add(this.DepthPanel);
            this.Controls.Add(this.DistrictProjectPanel);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.OperationalAreaPanel);
            this.Controls.Add(this.EastWestStreetPanel);
            this.Controls.Add(this.NorthSouthStreetPanel);
            this.Controls.Add(this.AdministrativeAreaPanel);
            this.Controls.Add(this.YCoordinatePanel);
            this.Controls.Add(this.XCoordinatePanel);
            this.Controls.Add(this.GPSByPanel);
            this.Controls.Add(this.WaterValveLocationPictureBox);
            this.Name = "F84722";
            this.Size = new System.Drawing.Size(804, 157);
            this.Tag = "84722";
            this.Load += new System.EventHandler(this.F84722_Load);
            this.GPSByPanel.ResumeLayout(false);
            this.GPSByPanel.PerformLayout();
            this.XCoordinatePanel.ResumeLayout(false);
            this.XCoordinatePanel.PerformLayout();
            this.YCoordinatePanel.ResumeLayout(false);
            this.YCoordinatePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaterValveLocationPictureBox)).EndInit();
            this.AdministrativeAreaPanel.ResumeLayout(false);
            this.AdministrativeAreaPanel.PerformLayout();
            this.NorthSouthStreetPanel.ResumeLayout(false);
            this.NorthSouthStreetPanel.PerformLayout();
            this.EastWestStreetPanel.ResumeLayout(false);
            this.EastWestStreetPanel.PerformLayout();
            this.OperationalAreaPanel.ResumeLayout(false);
            this.OperationalAreaPanel.PerformLayout();
            this.GridPanel.ResumeLayout(false);
            this.GridPanel.PerformLayout();
            this.DistrictProjectPanel.ResumeLayout(false);
            this.DistrictProjectPanel.PerformLayout();
            this.DepthPanel.ResumeLayout(false);
            this.DepthPanel.PerformLayout();
            this.ElevationPanel.ResumeLayout(false);
            this.ElevationPanel.PerformLayout();
            this.LocationNotesPanel.ResumeLayout(false);
            this.LocationNotesPanel.PerformLayout();
            this.GPSDatePanel.ResumeLayout(false);
            this.GPSDatePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel GPSByPanel;
        private TerraScan.UI.Controls.TerraScanComboBox GPSByComboBox;
        private System.Windows.Forms.Label GPSByLabel;
        private System.Windows.Forms.Panel XCoordinatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox XCoordinateTextBox;
        private System.Windows.Forms.Label XCoordinateLabel;
        private System.Windows.Forms.Panel YCoordinatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox YCoordinateTextBox;
        private System.Windows.Forms.Label YCoordinateLabel;
        private System.Windows.Forms.PictureBox WaterValveLocationPictureBox;
        private System.Windows.Forms.Panel AdministrativeAreaPanel;
        private TerraScan.UI.Controls.TerraScanComboBox AdministrativeAreaComboBox;
        private System.Windows.Forms.Label AdministrativeAreaLabel;
        private System.Windows.Forms.Panel NorthSouthStreetPanel;
        private TerraScan.UI.Controls.TerraScanComboBox NorthSouthStreetComboBox;
        private System.Windows.Forms.Label NorthSouthStreetLabel;
        private System.Windows.Forms.Panel EastWestStreetPanel;
        private TerraScan.UI.Controls.TerraScanComboBox EastWestStreetComboBox;
        private System.Windows.Forms.Label EastWestStreetLabel;
        private System.Windows.Forms.Panel OperationalAreaPanel;
        private TerraScan.UI.Controls.TerraScanComboBox OperationalAreaComboBox;
        private System.Windows.Forms.Label OperationalAreaLabel;
        private System.Windows.Forms.Panel GridPanel;
        private TerraScan.UI.Controls.TerraScanComboBox GridComboBox;
        private System.Windows.Forms.Label GridLabel;
        private System.Windows.Forms.Panel DistrictProjectPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DistrictProjectTextBox;
        private System.Windows.Forms.Label DistrictProjectLabel;
        private System.Windows.Forms.Panel DepthPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DepthTextBox;
        private System.Windows.Forms.Label DepthLable;
        private System.Windows.Forms.Panel ElevationPanel;
        private TerraScan.UI.Controls.TerraScanTextBox ElevationTextBox;
        private System.Windows.Forms.Label ElevationLabel;
        private System.Windows.Forms.Panel LocationNotesPanel;
        private TerraScan.UI.Controls.TerraScanTextBox LocationNotesTextBox;
        private System.Windows.Forms.Label LocationNotesLabel;
        private System.Windows.Forms.Panel GPSDatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox GPSDateTextBox;
        private System.Windows.Forms.Label GPSDateLabel;
        private System.Windows.Forms.Button GPSDatePict;
        private System.Windows.Forms.DateTimePicker GPSTimePicker;
        private System.Windows.Forms.ToolTip GDocWaterValveLocationToolTip;
    }
}
