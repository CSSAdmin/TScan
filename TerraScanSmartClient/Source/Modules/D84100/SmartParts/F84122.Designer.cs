namespace D84100
{
    partial class F84122
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F84122));
            this.RegisterPanel = new System.Windows.Forms.Panel();
            this.GPSDateCalender = new TerraScan.UI.Controls.TerraScanMonthCalender();
            this.YCoordinatePanel = new System.Windows.Forms.Panel();
            this.YCoordinateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.YCoordinateLabel = new System.Windows.Forms.Label();
            this.XCoordinatePanel = new System.Windows.Forms.Panel();
            this.XCoordinateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.XCoordinateLabel = new System.Windows.Forms.Label();
            this.DistrictProjectPanel = new System.Windows.Forms.Panel();
            this.DistrictProjectTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DistrictProjectLabel = new System.Windows.Forms.Label();
            this.LocationNotesPanel = new System.Windows.Forms.Panel();
            this.LocationNotesTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.LocationNotesLabel = new System.Windows.Forms.Label();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.GridComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.GridLabel = new System.Windows.Forms.Label();
            this.OperationalAreaPanel = new System.Windows.Forms.Panel();
            this.OperationalAreaComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.OperationalAreaLabel = new System.Windows.Forms.Label();
            this.GPSByPanel = new System.Windows.Forms.Panel();
            this.GPSByComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.GPSByLabel = new System.Windows.Forms.Label();
            this.EastWestStreetPanel = new System.Windows.Forms.Panel();
            this.EastWestStreetComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.EastWestStreetLabel = new System.Windows.Forms.Label();
            this.NorthSouthStreetPanel = new System.Windows.Forms.Panel();
            this.NorthSouthStreetComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.NorthSouthStreetLabel = new System.Windows.Forms.Label();
            this.AdministrativeAreaPanel = new System.Windows.Forms.Panel();
            this.AdministrativeAreaComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.AdministrativeAreaLabel = new System.Windows.Forms.Label();
            this.GPSDatePanel = new System.Windows.Forms.Panel();
            this.GPSDatePic = new System.Windows.Forms.Button();
            this.GPSDateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.GPSDateLabel = new System.Windows.Forms.Label();
            this.LocationPictureBox = new System.Windows.Forms.PictureBox();
            this.InspectionDetailsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.RegisterPanel.SuspendLayout();
            this.YCoordinatePanel.SuspendLayout();
            this.XCoordinatePanel.SuspendLayout();
            this.DistrictProjectPanel.SuspendLayout();
            this.LocationNotesPanel.SuspendLayout();
            this.GridPanel.SuspendLayout();
            this.OperationalAreaPanel.SuspendLayout();
            this.GPSByPanel.SuspendLayout();
            this.EastWestStreetPanel.SuspendLayout();
            this.NorthSouthStreetPanel.SuspendLayout();
            this.AdministrativeAreaPanel.SuspendLayout();
            this.GPSDatePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocationPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // RegisterPanel
            // 
            this.RegisterPanel.Controls.Add(this.GPSDateCalender);
            this.RegisterPanel.Controls.Add(this.YCoordinatePanel);
            this.RegisterPanel.Controls.Add(this.XCoordinatePanel);
            this.RegisterPanel.Controls.Add(this.DistrictProjectPanel);
            this.RegisterPanel.Controls.Add(this.LocationNotesPanel);
            this.RegisterPanel.Controls.Add(this.GridPanel);
            this.RegisterPanel.Controls.Add(this.OperationalAreaPanel);
            this.RegisterPanel.Controls.Add(this.GPSByPanel);
            this.RegisterPanel.Controls.Add(this.EastWestStreetPanel);
            this.RegisterPanel.Controls.Add(this.NorthSouthStreetPanel);
            this.RegisterPanel.Controls.Add(this.AdministrativeAreaPanel);
            this.RegisterPanel.Controls.Add(this.GPSDatePanel);
            this.RegisterPanel.Controls.Add(this.LocationPictureBox);
            this.RegisterPanel.Location = new System.Drawing.Point(0, 0);
            this.RegisterPanel.Name = "RegisterPanel";
            this.RegisterPanel.Size = new System.Drawing.Size(807, 165);
            this.RegisterPanel.TabIndex = 2;
            this.RegisterPanel.TabStop = true;
            // 
            // GPSDateCalender
            // 
            this.GPSDateCalender.ApplyDateChange = false;
            this.GPSDateCalender.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GPSDateCalender.FocusRemovedFrom = false;
            this.GPSDateCalender.Location = new System.Drawing.Point(781, 10);
            this.GPSDateCalender.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            this.GPSDateCalender.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.GPSDateCalender.Name = "GPSDateCalender";
            this.GPSDateCalender.TabIndex = 3;
            this.GPSDateCalender.Visible = false;
            this.GPSDateCalender.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.GPSDateCalender_DateSelected);
            this.GPSDateCalender.Leave += new System.EventHandler(this.GPSDateCalender_Leave);
            this.GPSDateCalender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GPSDateCalender_KeyDown);
            // 
            // YCoordinatePanel
            // 
            this.YCoordinatePanel.BackColor = System.Drawing.Color.Transparent;
            this.YCoordinatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.YCoordinatePanel.Controls.Add(this.YCoordinateTextBox);
            this.YCoordinatePanel.Controls.Add(this.YCoordinateLabel);
            this.YCoordinatePanel.Location = new System.Drawing.Point(588, 0);
            this.YCoordinatePanel.Name = "YCoordinatePanel";
            this.YCoordinatePanel.Size = new System.Drawing.Size(181, 42);
            this.YCoordinatePanel.TabIndex = 8;
            // 
            // YCoordinateTextBox
            // 
            this.YCoordinateTextBox.AllowClick = true;
            this.YCoordinateTextBox.AllowNegativeSign = true;
            this.YCoordinateTextBox.ApplyCFGFormat = true;
            this.YCoordinateTextBox.ApplyCurrencyFormat = true;
            this.YCoordinateTextBox.ApplyFocusColor = true;
            this.YCoordinateTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
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
            this.YCoordinateTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YCoordinateTextBox.ForeColor = System.Drawing.Color.Black;
            this.YCoordinateTextBox.IsEditable = true;
            this.YCoordinateTextBox.IsQueryableFileld = false;
            this.YCoordinateTextBox.Location = new System.Drawing.Point(3, 17);
            this.YCoordinateTextBox.LockKeyPress = false;
            this.YCoordinateTextBox.MaxLength = 0;
            this.YCoordinateTextBox.Name = "YCoordinateTextBox";
            this.YCoordinateTextBox.PersistDefaultColor = false;
            this.YCoordinateTextBox.Precision = 3;
            this.YCoordinateTextBox.QueryingFileldName = "";
            this.YCoordinateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.YCoordinateTextBox.Size = new System.Drawing.Size(169, 16);
            this.YCoordinateTextBox.SpecialCharacter = "$";
            this.YCoordinateTextBox.TabIndex = 9;
            this.YCoordinateTextBox.Tag = "";
            this.YCoordinateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.YCoordinateTextBox.TextCustomFormat = "#,##0.000";
            this.YCoordinateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.YCoordinateTextBox.WholeInteger = false;
            this.YCoordinateTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // YCoordinateLabel
            // 
            this.YCoordinateLabel.AutoSize = true;
            this.YCoordinateLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.YCoordinateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.YCoordinateLabel.Location = new System.Drawing.Point(0, 0);
            this.YCoordinateLabel.Name = "YCoordinateLabel";
            this.YCoordinateLabel.Size = new System.Drawing.Size(81, 14);
            this.YCoordinateLabel.TabIndex = 0;
            this.YCoordinateLabel.Text = "Y Coordinate:";
            // 
            // XCoordinatePanel
            // 
            this.XCoordinatePanel.BackColor = System.Drawing.Color.Transparent;
            this.XCoordinatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.XCoordinatePanel.Controls.Add(this.XCoordinateTextBox);
            this.XCoordinatePanel.Controls.Add(this.XCoordinateLabel);
            this.XCoordinatePanel.Location = new System.Drawing.Point(408, 0);
            this.XCoordinatePanel.Name = "XCoordinatePanel";
            this.XCoordinatePanel.Size = new System.Drawing.Size(181, 42);
            this.XCoordinatePanel.TabIndex = 6;
            // 
            // XCoordinateTextBox
            // 
            this.XCoordinateTextBox.AllowClick = true;
            this.XCoordinateTextBox.AllowNegativeSign = true;
            this.XCoordinateTextBox.ApplyCFGFormat = true;
            this.XCoordinateTextBox.ApplyCurrencyFormat = true;
            this.XCoordinateTextBox.ApplyFocusColor = true;
            this.XCoordinateTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
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
            this.XCoordinateTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XCoordinateTextBox.ForeColor = System.Drawing.Color.Black;
            this.XCoordinateTextBox.IsEditable = true;
            this.XCoordinateTextBox.IsQueryableFileld = false;
            this.XCoordinateTextBox.Location = new System.Drawing.Point(3, 17);
            this.XCoordinateTextBox.LockKeyPress = false;
            this.XCoordinateTextBox.MaxLength = 0;
            this.XCoordinateTextBox.Name = "XCoordinateTextBox";
            this.XCoordinateTextBox.PersistDefaultColor = false;
            this.XCoordinateTextBox.Precision = 3;
            this.XCoordinateTextBox.QueryingFileldName = "";
            this.XCoordinateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.XCoordinateTextBox.Size = new System.Drawing.Size(170, 16);
            this.XCoordinateTextBox.SpecialCharacter = "%";
            this.XCoordinateTextBox.TabIndex = 7;
            this.XCoordinateTextBox.Tag = "";
            this.XCoordinateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.XCoordinateTextBox.TextCustomFormat = "#,##0.000";
            this.XCoordinateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.XCoordinateTextBox.WholeInteger = false;
            this.XCoordinateTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // XCoordinateLabel
            // 
            this.XCoordinateLabel.AutoSize = true;
            this.XCoordinateLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.XCoordinateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.XCoordinateLabel.Location = new System.Drawing.Point(0, 0);
            this.XCoordinateLabel.Name = "XCoordinateLabel";
            this.XCoordinateLabel.Size = new System.Drawing.Size(81, 14);
            this.XCoordinateLabel.TabIndex = 0;
            this.XCoordinateLabel.Text = "X Coordinate:";
            // 
            // DistrictProjectPanel
            // 
            this.DistrictProjectPanel.BackColor = System.Drawing.Color.Transparent;
            this.DistrictProjectPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DistrictProjectPanel.Controls.Add(this.DistrictProjectTextBox);
            this.DistrictProjectPanel.Controls.Add(this.DistrictProjectLabel);
            this.DistrictProjectPanel.Location = new System.Drawing.Point(563, 41);
            this.DistrictProjectPanel.Name = "DistrictProjectPanel";
            this.DistrictProjectPanel.Size = new System.Drawing.Size(206, 42);
            this.DistrictProjectPanel.TabIndex = 16;
            // 
            // DistrictProjectTextBox
            // 
            this.DistrictProjectTextBox.AllowClick = true;
            this.DistrictProjectTextBox.AllowNegativeSign = false;
            this.DistrictProjectTextBox.ApplyCFGFormat = false;
            this.DistrictProjectTextBox.ApplyCurrencyFormat = false;
            this.DistrictProjectTextBox.ApplyFocusColor = true;
            this.DistrictProjectTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
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
            this.DistrictProjectTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistrictProjectTextBox.ForeColor = System.Drawing.Color.Black;
            this.DistrictProjectTextBox.IsEditable = true;
            this.DistrictProjectTextBox.IsQueryableFileld = false;
            this.DistrictProjectTextBox.Location = new System.Drawing.Point(3, 17);
            this.DistrictProjectTextBox.LockKeyPress = false;
            this.DistrictProjectTextBox.MaxLength = 17;
            this.DistrictProjectTextBox.Name = "DistrictProjectTextBox";
            this.DistrictProjectTextBox.PersistDefaultColor = false;
            this.DistrictProjectTextBox.Precision = 2;
            this.DistrictProjectTextBox.QueryingFileldName = "";
            this.DistrictProjectTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DistrictProjectTextBox.Size = new System.Drawing.Size(194, 16);
            this.DistrictProjectTextBox.SpecialCharacter = "%-";
            this.DistrictProjectTextBox.TabIndex = 17;
            this.DistrictProjectTextBox.Tag = "";
            this.DistrictProjectTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DistrictProjectTextBox.TextCustomFormat = "XX-######-XX-####";
            this.DistrictProjectTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DistrictProjectTextBox.WholeInteger = false;
            this.DistrictProjectTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // DistrictProjectLabel
            // 
            this.DistrictProjectLabel.AutoSize = true;
            this.DistrictProjectLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.DistrictProjectLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DistrictProjectLabel.Location = new System.Drawing.Point(0, 0);
            this.DistrictProjectLabel.Name = "DistrictProjectLabel";
            this.DistrictProjectLabel.Size = new System.Drawing.Size(97, 14);
            this.DistrictProjectLabel.TabIndex = 0;
            this.DistrictProjectLabel.Text = "District / Project:";
            // 
            // LocationNotesPanel
            // 
            this.LocationNotesPanel.BackColor = System.Drawing.Color.Transparent;
            this.LocationNotesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LocationNotesPanel.Controls.Add(this.LocationNotesTextBox);
            this.LocationNotesPanel.Controls.Add(this.LocationNotesLabel);
            this.LocationNotesPanel.Location = new System.Drawing.Point(191, 82);
            this.LocationNotesPanel.Name = "LocationNotesPanel";
            this.LocationNotesPanel.Size = new System.Drawing.Size(578, 83);
            this.LocationNotesPanel.TabIndex = 22;
            this.LocationNotesPanel.TabStop = true;
            // 
            // LocationNotesTextBox
            // 
            this.LocationNotesTextBox.AllowClick = true;
            this.LocationNotesTextBox.AllowNegativeSign = false;
            this.LocationNotesTextBox.ApplyCFGFormat = false;
            this.LocationNotesTextBox.ApplyCurrencyFormat = false;
            this.LocationNotesTextBox.ApplyFocusColor = true;
            this.LocationNotesTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
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
            this.LocationNotesTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationNotesTextBox.IsEditable = false;
            this.LocationNotesTextBox.IsQueryableFileld = false;
            this.LocationNotesTextBox.Location = new System.Drawing.Point(23, 15);
            this.LocationNotesTextBox.LockKeyPress = false;
            this.LocationNotesTextBox.MaxLength = 500;
            this.LocationNotesTextBox.Multiline = true;
            this.LocationNotesTextBox.Name = "LocationNotesTextBox";
            this.LocationNotesTextBox.PersistDefaultColor = false;
            this.LocationNotesTextBox.Precision = 2;
            this.LocationNotesTextBox.QueryingFileldName = "";
            this.LocationNotesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LocationNotesTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.LocationNotesTextBox.Size = new System.Drawing.Size(548, 65);
            this.LocationNotesTextBox.SpecialCharacter = "%";
            this.LocationNotesTextBox.TabIndex = 23;
            this.LocationNotesTextBox.TextCustomFormat = "$#,##0.00";
            this.LocationNotesTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.LocationNotesTextBox.WholeInteger = false;
            this.LocationNotesTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.LocationNotesTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // LocationNotesLabel
            // 
            this.LocationNotesLabel.AutoSize = true;
            this.LocationNotesLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.LocationNotesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.LocationNotesLabel.Location = new System.Drawing.Point(0, 0);
            this.LocationNotesLabel.Name = "LocationNotesLabel";
            this.LocationNotesLabel.Size = new System.Drawing.Size(92, 14);
            this.LocationNotesLabel.TabIndex = 0;
            this.LocationNotesLabel.Text = "Location Notes:";
            // 
            // GridPanel
            // 
            this.GridPanel.BackColor = System.Drawing.Color.Transparent;
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.GridComboBox);
            this.GridPanel.Controls.Add(this.GridLabel);
            this.GridPanel.Location = new System.Drawing.Point(420, 41);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(144, 42);
            this.GridPanel.TabIndex = 14;
            this.GridPanel.TabStop = true;
            // 
            // GridComboBox
            // 
            this.GridComboBox.BackColor = System.Drawing.Color.White;
            this.GridComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GridComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GridComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridComboBox.ForeColor = System.Drawing.Color.Black;
            this.GridComboBox.FormattingEnabled = true;
            this.GridComboBox.Location = new System.Drawing.Point(18, 15);
            this.GridComboBox.Name = "GridComboBox";
            this.GridComboBox.Size = new System.Drawing.Size(118, 24);
            this.GridComboBox.TabIndex = 15;
            this.GridComboBox.Tag = "";
            this.GridComboBox.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_SelectionChangeCommitted);
            this.GridComboBox.SelectedIndexChanged += new System.EventHandler(this.GridComboBox_SelectedIndexChanged);
            // 
            // GridLabel
            // 
            this.GridLabel.AutoSize = true;
            this.GridLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.GridLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.GridLabel.Location = new System.Drawing.Point(0, 0);
            this.GridLabel.Name = "GridLabel";
            this.GridLabel.Size = new System.Drawing.Size(33, 14);
            this.GridLabel.TabIndex = 0;
            this.GridLabel.Text = "Grid:";
            // 
            // OperationalAreaPanel
            // 
            this.OperationalAreaPanel.BackColor = System.Drawing.Color.Transparent;
            this.OperationalAreaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OperationalAreaPanel.Controls.Add(this.OperationalAreaComboBox);
            this.OperationalAreaPanel.Controls.Add(this.OperationalAreaLabel);
            this.OperationalAreaPanel.Location = new System.Drawing.Point(191, 41);
            this.OperationalAreaPanel.Name = "OperationalAreaPanel";
            this.OperationalAreaPanel.Size = new System.Drawing.Size(230, 42);
            this.OperationalAreaPanel.TabIndex = 12;
            this.OperationalAreaPanel.TabStop = true;
            // 
            // OperationalAreaComboBox
            // 
            this.OperationalAreaComboBox.BackColor = System.Drawing.Color.White;
            this.OperationalAreaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OperationalAreaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OperationalAreaComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OperationalAreaComboBox.ForeColor = System.Drawing.Color.Black;
            this.OperationalAreaComboBox.FormattingEnabled = true;
            this.OperationalAreaComboBox.Location = new System.Drawing.Point(18, 15);
            this.OperationalAreaComboBox.Name = "OperationalAreaComboBox";
            this.OperationalAreaComboBox.Size = new System.Drawing.Size(199, 24);
            this.OperationalAreaComboBox.TabIndex = 13;
            this.OperationalAreaComboBox.Tag = "";
            this.OperationalAreaComboBox.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_SelectionChangeCommitted);
            this.OperationalAreaComboBox.SelectedIndexChanged += new System.EventHandler(this.OperationalAreaComboBox_SelectedIndexChanged);
            // 
            // OperationalAreaLabel
            // 
            this.OperationalAreaLabel.AutoSize = true;
            this.OperationalAreaLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.OperationalAreaLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.OperationalAreaLabel.Location = new System.Drawing.Point(0, 0);
            this.OperationalAreaLabel.Name = "OperationalAreaLabel";
            this.OperationalAreaLabel.Size = new System.Drawing.Size(102, 14);
            this.OperationalAreaLabel.TabIndex = 0;
            this.OperationalAreaLabel.Text = "Operational Area:";
            // 
            // GPSByPanel
            // 
            this.GPSByPanel.BackColor = System.Drawing.Color.Transparent;
            this.GPSByPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GPSByPanel.Controls.Add(this.GPSByComboBox);
            this.GPSByPanel.Controls.Add(this.GPSByLabel);
            this.GPSByPanel.Location = new System.Drawing.Point(156, 0);
            this.GPSByPanel.Name = "GPSByPanel";
            this.GPSByPanel.Size = new System.Drawing.Size(253, 42);
            this.GPSByPanel.TabIndex = 4;
            this.GPSByPanel.TabStop = true;
            // 
            // GPSByComboBox
            // 
            this.GPSByComboBox.BackColor = System.Drawing.Color.White;
            this.GPSByComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GPSByComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GPSByComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GPSByComboBox.ForeColor = System.Drawing.Color.Black;
            this.GPSByComboBox.FormattingEnabled = true;
            this.GPSByComboBox.Location = new System.Drawing.Point(18, 15);
            this.GPSByComboBox.Name = "GPSByComboBox";
            this.GPSByComboBox.Size = new System.Drawing.Size(227, 24);
            this.GPSByComboBox.TabIndex = 5;
            this.GPSByComboBox.Tag = "";
            this.GPSByComboBox.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_SelectionChangeCommitted);
            this.GPSByComboBox.SelectedIndexChanged += new System.EventHandler(this.GPSByComboBox_SelectedIndexChanged);
            // 
            // GPSByLabel
            // 
            this.GPSByLabel.AutoSize = true;
            this.GPSByLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.GPSByLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.GPSByLabel.Location = new System.Drawing.Point(0, 0);
            this.GPSByLabel.Name = "GPSByLabel";
            this.GPSByLabel.Size = new System.Drawing.Size(48, 14);
            this.GPSByLabel.TabIndex = 0;
            this.GPSByLabel.Text = "GPS By:";
            // 
            // EastWestStreetPanel
            // 
            this.EastWestStreetPanel.BackColor = System.Drawing.Color.Transparent;
            this.EastWestStreetPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EastWestStreetPanel.Controls.Add(this.EastWestStreetComboBox);
            this.EastWestStreetPanel.Controls.Add(this.EastWestStreetLabel);
            this.EastWestStreetPanel.Location = new System.Drawing.Point(0, 123);
            this.EastWestStreetPanel.Name = "EastWestStreetPanel";
            this.EastWestStreetPanel.Size = new System.Drawing.Size(192, 42);
            this.EastWestStreetPanel.TabIndex = 20;
            this.EastWestStreetPanel.TabStop = true;
            // 
            // EastWestStreetComboBox
            // 
            this.EastWestStreetComboBox.BackColor = System.Drawing.Color.White;
            this.EastWestStreetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EastWestStreetComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EastWestStreetComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EastWestStreetComboBox.ForeColor = System.Drawing.Color.Black;
            this.EastWestStreetComboBox.FormattingEnabled = true;
            this.EastWestStreetComboBox.Location = new System.Drawing.Point(18, 15);
            this.EastWestStreetComboBox.Name = "EastWestStreetComboBox";
            this.EastWestStreetComboBox.Size = new System.Drawing.Size(164, 24);
            this.EastWestStreetComboBox.TabIndex = 21;
            this.EastWestStreetComboBox.Tag = "";
            this.EastWestStreetComboBox.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_SelectionChangeCommitted);
            this.EastWestStreetComboBox.SelectedIndexChanged += new System.EventHandler(this.EastWestStreetComboBox_SelectedIndexChanged);
            // 
            // EastWestStreetLabel
            // 
            this.EastWestStreetLabel.AutoSize = true;
            this.EastWestStreetLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.EastWestStreetLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.EastWestStreetLabel.Location = new System.Drawing.Point(0, 0);
            this.EastWestStreetLabel.Name = "EastWestStreetLabel";
            this.EastWestStreetLabel.Size = new System.Drawing.Size(101, 14);
            this.EastWestStreetLabel.TabIndex = 0;
            this.EastWestStreetLabel.Text = "East/West Street:";
            // 
            // NorthSouthStreetPanel
            // 
            this.NorthSouthStreetPanel.BackColor = System.Drawing.Color.Transparent;
            this.NorthSouthStreetPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NorthSouthStreetPanel.Controls.Add(this.NorthSouthStreetComboBox);
            this.NorthSouthStreetPanel.Controls.Add(this.NorthSouthStreetLabel);
            this.NorthSouthStreetPanel.Location = new System.Drawing.Point(0, 82);
            this.NorthSouthStreetPanel.Name = "NorthSouthStreetPanel";
            this.NorthSouthStreetPanel.Size = new System.Drawing.Size(192, 42);
            this.NorthSouthStreetPanel.TabIndex = 18;
            this.NorthSouthStreetPanel.TabStop = true;
            // 
            // NorthSouthStreetComboBox
            // 
            this.NorthSouthStreetComboBox.BackColor = System.Drawing.Color.White;
            this.NorthSouthStreetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NorthSouthStreetComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NorthSouthStreetComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NorthSouthStreetComboBox.ForeColor = System.Drawing.Color.Black;
            this.NorthSouthStreetComboBox.FormattingEnabled = true;
            this.NorthSouthStreetComboBox.Location = new System.Drawing.Point(18, 15);
            this.NorthSouthStreetComboBox.Name = "NorthSouthStreetComboBox";
            this.NorthSouthStreetComboBox.Size = new System.Drawing.Size(164, 24);
            this.NorthSouthStreetComboBox.TabIndex = 19;
            this.NorthSouthStreetComboBox.Tag = "";
            this.NorthSouthStreetComboBox.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_SelectionChangeCommitted);
            this.NorthSouthStreetComboBox.SelectedIndexChanged += new System.EventHandler(this.NorthSouthStreetComboBox_SelectedIndexChanged);
            // 
            // NorthSouthStreetLabel
            // 
            this.NorthSouthStreetLabel.AutoSize = true;
            this.NorthSouthStreetLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.NorthSouthStreetLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.NorthSouthStreetLabel.Location = new System.Drawing.Point(0, 0);
            this.NorthSouthStreetLabel.Name = "NorthSouthStreetLabel";
            this.NorthSouthStreetLabel.Size = new System.Drawing.Size(112, 14);
            this.NorthSouthStreetLabel.TabIndex = 0;
            this.NorthSouthStreetLabel.Text = "North/South Street:";
            // 
            // AdministrativeAreaPanel
            // 
            this.AdministrativeAreaPanel.BackColor = System.Drawing.Color.Transparent;
            this.AdministrativeAreaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdministrativeAreaPanel.Controls.Add(this.AdministrativeAreaComboBox);
            this.AdministrativeAreaPanel.Controls.Add(this.AdministrativeAreaLabel);
            this.AdministrativeAreaPanel.Location = new System.Drawing.Point(0, 41);
            this.AdministrativeAreaPanel.Name = "AdministrativeAreaPanel";
            this.AdministrativeAreaPanel.Size = new System.Drawing.Size(192, 42);
            this.AdministrativeAreaPanel.TabIndex = 10;
            // 
            // AdministrativeAreaComboBox
            // 
            this.AdministrativeAreaComboBox.BackColor = System.Drawing.Color.White;
            this.AdministrativeAreaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AdministrativeAreaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AdministrativeAreaComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdministrativeAreaComboBox.ForeColor = System.Drawing.Color.Black;
            this.AdministrativeAreaComboBox.FormattingEnabled = true;
            this.AdministrativeAreaComboBox.Location = new System.Drawing.Point(18, 15);
            this.AdministrativeAreaComboBox.Name = "AdministrativeAreaComboBox";
            this.AdministrativeAreaComboBox.Size = new System.Drawing.Size(158, 24);
            this.AdministrativeAreaComboBox.TabIndex = 11;
            this.AdministrativeAreaComboBox.Tag = "";
            this.AdministrativeAreaComboBox.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_SelectionChangeCommitted);
            this.AdministrativeAreaComboBox.SelectedIndexChanged += new System.EventHandler(this.AdministrativeAreaComboBox_SelectedIndexChanged);
            // 
            // AdministrativeAreaLabel
            // 
            this.AdministrativeAreaLabel.AutoSize = true;
            this.AdministrativeAreaLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.AdministrativeAreaLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.AdministrativeAreaLabel.Location = new System.Drawing.Point(0, 0);
            this.AdministrativeAreaLabel.Name = "AdministrativeAreaLabel";
            this.AdministrativeAreaLabel.Size = new System.Drawing.Size(120, 14);
            this.AdministrativeAreaLabel.TabIndex = 0;
            this.AdministrativeAreaLabel.Text = "Administrative Area:";
            this.AdministrativeAreaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GPSDatePanel
            // 
            this.GPSDatePanel.BackColor = System.Drawing.Color.Transparent;
            this.GPSDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GPSDatePanel.Controls.Add(this.GPSDatePic);
            this.GPSDatePanel.Controls.Add(this.GPSDateTextBox);
            this.GPSDatePanel.Controls.Add(this.GPSDateLabel);
            this.GPSDatePanel.Location = new System.Drawing.Point(0, 0);
            this.GPSDatePanel.Name = "GPSDatePanel";
            this.GPSDatePanel.Size = new System.Drawing.Size(157, 42);
            this.GPSDatePanel.TabIndex = 0;
            this.GPSDatePanel.TabStop = true;
            // 
            // GPSDatePic
            // 
            this.GPSDatePic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GPSDatePic.FlatAppearance.BorderSize = 0;
            this.GPSDatePic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GPSDatePic.Image = ((System.Drawing.Image)(resources.GetObject("GPSDatePic.Image")));
            this.GPSDatePic.Location = new System.Drawing.Point(127, 9);
            this.GPSDatePic.Name = "GPSDatePic";
            this.GPSDatePic.Size = new System.Drawing.Size(20, 24);
            this.GPSDatePic.TabIndex = 2;
            this.GPSDatePic.UseVisualStyleBackColor = true;
            this.GPSDatePic.Click += new System.EventHandler(this.GPSDatePic_Click);
            // 
            // GPSDateTextBox
            // 
            this.GPSDateTextBox.AllowClick = true;
            this.GPSDateTextBox.AllowNegativeSign = false;
            this.GPSDateTextBox.ApplyCFGFormat = true;
            this.GPSDateTextBox.ApplyCurrencyFormat = false;
            this.GPSDateTextBox.ApplyFocusColor = true;
            this.GPSDateTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
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
            this.GPSDateTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.GPSDateTextBox.ForeColor = System.Drawing.Color.Black;
            this.GPSDateTextBox.IsEditable = false;
            this.GPSDateTextBox.IsQueryableFileld = true;
            this.GPSDateTextBox.Location = new System.Drawing.Point(18, 21);
            this.GPSDateTextBox.LockKeyPress = true;
            this.GPSDateTextBox.Name = "GPSDateTextBox";
            this.GPSDateTextBox.PersistDefaultColor = false;
            this.GPSDateTextBox.Precision = 2;
            this.GPSDateTextBox.QueryingFileldName = "";
            this.GPSDateTextBox.ReadOnly = true;
            this.GPSDateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.GPSDateTextBox.Size = new System.Drawing.Size(85, 16);
            this.GPSDateTextBox.SpecialCharacter = "%";
            this.GPSDateTextBox.TabIndex = 1;
            this.GPSDateTextBox.TabStop = false;
            this.GPSDateTextBox.Tag = "";
            this.GPSDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GPSDateTextBox.TextCustomFormat = "M/D/YYYY";
            this.GPSDateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.GPSDateTextBox.WholeInteger = false;
            this.GPSDateTextBox.TextChanged += new System.EventHandler(this.EnableSaveCancelButton);
            // 
            // GPSDateLabel
            // 
            this.GPSDateLabel.AutoSize = true;
            this.GPSDateLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.GPSDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.GPSDateLabel.Location = new System.Drawing.Point(0, 0);
            this.GPSDateLabel.Name = "GPSDateLabel";
            this.GPSDateLabel.Size = new System.Drawing.Size(59, 14);
            this.GPSDateLabel.TabIndex = 0;
            this.GPSDateLabel.Text = "GPS Date:";
            // 
            // LocationPictureBox
            // 
            this.LocationPictureBox.Location = new System.Drawing.Point(761, 0);
            this.LocationPictureBox.Name = "LocationPictureBox";
            this.LocationPictureBox.Size = new System.Drawing.Size(42, 165);
            this.LocationPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LocationPictureBox.TabIndex = 30;
            this.LocationPictureBox.TabStop = false;
            this.LocationPictureBox.Click += new System.EventHandler(this.LocationPictureBox_Click);
            this.LocationPictureBox.MouseEnter += new System.EventHandler(this.LocationPictureBox_MouseEnter);
            // 
            // F84122
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.RegisterPanel);
            this.Name = "F84122";
            this.Size = new System.Drawing.Size(804, 165);
            this.Tag = "84122";
            this.Load += new System.EventHandler(this.F84122_Load);
            this.RegisterPanel.ResumeLayout(false);
            this.YCoordinatePanel.ResumeLayout(false);
            this.YCoordinatePanel.PerformLayout();
            this.XCoordinatePanel.ResumeLayout(false);
            this.XCoordinatePanel.PerformLayout();
            this.DistrictProjectPanel.ResumeLayout(false);
            this.DistrictProjectPanel.PerformLayout();
            this.LocationNotesPanel.ResumeLayout(false);
            this.LocationNotesPanel.PerformLayout();
            this.GridPanel.ResumeLayout(false);
            this.GridPanel.PerformLayout();
            this.OperationalAreaPanel.ResumeLayout(false);
            this.OperationalAreaPanel.PerformLayout();
            this.GPSByPanel.ResumeLayout(false);
            this.GPSByPanel.PerformLayout();
            this.EastWestStreetPanel.ResumeLayout(false);
            this.EastWestStreetPanel.PerformLayout();
            this.NorthSouthStreetPanel.ResumeLayout(false);
            this.NorthSouthStreetPanel.PerformLayout();
            this.AdministrativeAreaPanel.ResumeLayout(false);
            this.AdministrativeAreaPanel.PerformLayout();
            this.GPSDatePanel.ResumeLayout(false);
            this.GPSDatePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocationPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel RegisterPanel;
        private System.Windows.Forms.Panel LocationNotesPanel;
        private TerraScan.UI.Controls.TerraScanTextBox LocationNotesTextBox;
        private System.Windows.Forms.Label LocationNotesLabel;
        private System.Windows.Forms.Panel GridPanel;
        private TerraScan.UI.Controls.TerraScanComboBox GridComboBox;
        private System.Windows.Forms.Label GridLabel;
        private System.Windows.Forms.Panel OperationalAreaPanel;
        private TerraScan.UI.Controls.TerraScanComboBox OperationalAreaComboBox;
        private System.Windows.Forms.Label OperationalAreaLabel;
        private System.Windows.Forms.Panel GPSByPanel;
        private TerraScan.UI.Controls.TerraScanComboBox GPSByComboBox;
        private System.Windows.Forms.Label GPSByLabel;
        private System.Windows.Forms.Panel EastWestStreetPanel;
        private TerraScan.UI.Controls.TerraScanComboBox EastWestStreetComboBox;
        private System.Windows.Forms.Label EastWestStreetLabel;
        private System.Windows.Forms.Panel NorthSouthStreetPanel;
        private TerraScan.UI.Controls.TerraScanComboBox NorthSouthStreetComboBox;
        private System.Windows.Forms.Label NorthSouthStreetLabel;
        private System.Windows.Forms.Panel AdministrativeAreaPanel;
        private TerraScan.UI.Controls.TerraScanComboBox AdministrativeAreaComboBox;
        private System.Windows.Forms.Label AdministrativeAreaLabel;
        private System.Windows.Forms.Panel GPSDatePanel;
        private System.Windows.Forms.Label GPSDateLabel;
        private System.Windows.Forms.Panel DistrictProjectPanel;
        private System.Windows.Forms.Label DistrictProjectLabel;
        private TerraScan.UI.Controls.TerraScanTextBox GPSDateTextBox;
        private System.Windows.Forms.Panel YCoordinatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox YCoordinateTextBox;
        private System.Windows.Forms.Label YCoordinateLabel;
        private System.Windows.Forms.Panel XCoordinatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox XCoordinateTextBox;
        private System.Windows.Forms.Label XCoordinateLabel;        
        private System.Windows.Forms.PictureBox LocationPictureBox;
        private System.Windows.Forms.ToolTip InspectionDetailsToolTip;
        private TerraScan.UI.Controls.TerraScanTextBox DistrictProjectTextBox;
        private System.Windows.Forms.Button GPSDatePic;
        private TerraScan.UI.Controls.TerraScanMonthCalender GPSDateCalender;
    }
}
