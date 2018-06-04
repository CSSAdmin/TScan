namespace D84700
{
    partial class F84726
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F84726));
            this.AdministrativeAreaPanel = new System.Windows.Forms.Panel();
            this.AdministrativeAreaComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.AdministrativeAreaLable = new System.Windows.Forms.Label();
            this.OperationalAreaPanel = new System.Windows.Forms.Panel();
            this.OperationalAreaComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.OperationalAreaLabel = new System.Windows.Forms.Label();
            this.ElevationPanel = new System.Windows.Forms.Panel();
            this.ElevationTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.ElevationLabel = new System.Windows.Forms.Label();
            this.DepthPanel = new System.Windows.Forms.Panel();
            this.DepthTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DepthLable = new System.Windows.Forms.Label();
            this.WaterPipeLocationPictureBox = new System.Windows.Forms.PictureBox();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.GridComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.GridLabel = new System.Windows.Forms.Label();
            this.DistrictProjectPanel = new System.Windows.Forms.Panel();
            this.DistrictProjectTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DistrictProjectLabel = new System.Windows.Forms.Label();
            this.LocationNotesPanel = new System.Windows.Forms.Panel();
            this.LocationNotesTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.LocationNotesLabel = new System.Windows.Forms.Label();
            this.GDocWaterPipeLocationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.AdministrativeAreaPanel.SuspendLayout();
            this.OperationalAreaPanel.SuspendLayout();
            this.ElevationPanel.SuspendLayout();
            this.DepthPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaterPipeLocationPictureBox)).BeginInit();
            this.GridPanel.SuspendLayout();
            this.DistrictProjectPanel.SuspendLayout();
            this.LocationNotesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // AdministrativeAreaPanel
            // 
            this.AdministrativeAreaPanel.BackColor = System.Drawing.Color.Transparent;
            this.AdministrativeAreaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdministrativeAreaPanel.Controls.Add(this.AdministrativeAreaComboBox);
            this.AdministrativeAreaPanel.Controls.Add(this.AdministrativeAreaLable);
            this.AdministrativeAreaPanel.Location = new System.Drawing.Point(0, 0);
            this.AdministrativeAreaPanel.Name = "AdministrativeAreaPanel";
            this.AdministrativeAreaPanel.Size = new System.Drawing.Size(164, 40);
            this.AdministrativeAreaPanel.TabIndex = 1;
            this.AdministrativeAreaPanel.TabStop = true;
            // 
            // AdministrativeAreaComboBox
            // 
            this.AdministrativeAreaComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.AdministrativeAreaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AdministrativeAreaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AdministrativeAreaComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.AdministrativeAreaComboBox.FormattingEnabled = true;
            this.AdministrativeAreaComboBox.Location = new System.Drawing.Point(11, 13);
            this.AdministrativeAreaComboBox.Name = "AdministrativeAreaComboBox";
            this.AdministrativeAreaComboBox.Size = new System.Drawing.Size(140, 24);
            this.AdministrativeAreaComboBox.TabIndex = 2;
            this.AdministrativeAreaComboBox.SelectionChangeCommitted += new System.EventHandler(this.EnableSaveCancelButton);
            this.AdministrativeAreaComboBox.SelectedIndexChanged += new System.EventHandler(this.AdministrativeAreaComboBox_SelectedIndexChanged);
            // 
            // AdministrativeAreaLable
            // 
            this.AdministrativeAreaLable.AutoSize = true;
            this.AdministrativeAreaLable.BackColor = System.Drawing.Color.Transparent;
            this.AdministrativeAreaLable.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdministrativeAreaLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.AdministrativeAreaLable.Location = new System.Drawing.Point(1, -1);
            this.AdministrativeAreaLable.Name = "AdministrativeAreaLable";
            this.AdministrativeAreaLable.Size = new System.Drawing.Size(120, 14);
            this.AdministrativeAreaLable.TabIndex = 0;
            this.AdministrativeAreaLable.Text = "Administrative Area:";
            // 
            // OperationalAreaPanel
            // 
            this.OperationalAreaPanel.BackColor = System.Drawing.Color.Transparent;
            this.OperationalAreaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OperationalAreaPanel.Controls.Add(this.OperationalAreaComboBox);
            this.OperationalAreaPanel.Controls.Add(this.OperationalAreaLabel);
            this.OperationalAreaPanel.Location = new System.Drawing.Point(163, 0);
            this.OperationalAreaPanel.Name = "OperationalAreaPanel";
            this.OperationalAreaPanel.Size = new System.Drawing.Size(329, 40);
            this.OperationalAreaPanel.TabIndex = 3;
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
            this.OperationalAreaComboBox.Size = new System.Drawing.Size(304, 24);
            this.OperationalAreaComboBox.TabIndex = 4;
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
            // ElevationPanel
            // 
            this.ElevationPanel.BackColor = System.Drawing.Color.Transparent;
            this.ElevationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ElevationPanel.Controls.Add(this.ElevationTextBox);
            this.ElevationPanel.Controls.Add(this.ElevationLabel);
            this.ElevationPanel.Location = new System.Drawing.Point(491, 0);
            this.ElevationPanel.Name = "ElevationPanel";
            this.ElevationPanel.Size = new System.Drawing.Size(152, 40);
            this.ElevationPanel.TabIndex = 5;
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
            this.ElevationTextBox.Location = new System.Drawing.Point(11, 18);
            this.ElevationTextBox.LockKeyPress = false;
            this.ElevationTextBox.MaxLength = 0;
            this.ElevationTextBox.Name = "ElevationTextBox";
            this.ElevationTextBox.PersistDefaultColor = false;
            this.ElevationTextBox.Precision = 3;
            this.ElevationTextBox.QueryingFileldName = "";
            this.ElevationTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ElevationTextBox.Size = new System.Drawing.Size(134, 16);
            this.ElevationTextBox.SpecialCharacter = "%";
            this.ElevationTextBox.TabIndex = 6;
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
            // DepthPanel
            // 
            this.DepthPanel.BackColor = System.Drawing.Color.Transparent;
            this.DepthPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DepthPanel.Controls.Add(this.DepthTextBox);
            this.DepthPanel.Controls.Add(this.DepthLable);
            this.DepthPanel.Location = new System.Drawing.Point(642, 0);
            this.DepthPanel.Name = "DepthPanel";
            this.DepthPanel.Size = new System.Drawing.Size(126, 40);
            this.DepthPanel.TabIndex = 7;
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
            this.DepthTextBox.TabIndex = 8;
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
            // WaterPipeLocationPictureBox
            // 
            this.WaterPipeLocationPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("WaterPipeLocationPictureBox.Image")));
            this.WaterPipeLocationPictureBox.Location = new System.Drawing.Point(761, 0);
            this.WaterPipeLocationPictureBox.Name = "WaterPipeLocationPictureBox";
            this.WaterPipeLocationPictureBox.Size = new System.Drawing.Size(42, 118);
            this.WaterPipeLocationPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WaterPipeLocationPictureBox.TabIndex = 43;
            this.WaterPipeLocationPictureBox.TabStop = false;
            this.WaterPipeLocationPictureBox.Click += new System.EventHandler(this.WaterPipePictureBox_Click);
            this.WaterPipeLocationPictureBox.MouseEnter += new System.EventHandler(this.WaterPipePictureBox_MouseEnter);
            // 
            // GridPanel
            // 
            this.GridPanel.BackColor = System.Drawing.Color.Transparent;
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.GridComboBox);
            this.GridPanel.Controls.Add(this.GridLabel);
            this.GridPanel.Location = new System.Drawing.Point(0, 39);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(164, 40);
            this.GridPanel.TabIndex = 9;
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
            this.GridComboBox.Size = new System.Drawing.Size(140, 24);
            this.GridComboBox.TabIndex = 10;
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
            this.DistrictProjectPanel.Location = new System.Drawing.Point(0, 78);
            this.DistrictProjectPanel.Name = "DistrictProjectPanel";
            this.DistrictProjectPanel.Size = new System.Drawing.Size(164, 40);
            this.DistrictProjectPanel.TabIndex = 11;
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
            this.DistrictProjectTextBox.Size = new System.Drawing.Size(149, 16);
            this.DistrictProjectTextBox.SpecialCharacter = "%";
            this.DistrictProjectTextBox.TabIndex = 12;
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
            // LocationNotesPanel
            // 
            this.LocationNotesPanel.BackColor = System.Drawing.Color.Transparent;
            this.LocationNotesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LocationNotesPanel.Controls.Add(this.LocationNotesTextBox);
            this.LocationNotesPanel.Controls.Add(this.LocationNotesLabel);
            this.LocationNotesPanel.Location = new System.Drawing.Point(163, 39);
            this.LocationNotesPanel.Name = "LocationNotesPanel";
            this.LocationNotesPanel.Size = new System.Drawing.Size(605, 79);
            this.LocationNotesPanel.TabIndex = 13;
            this.LocationNotesPanel.TabStop = true;
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
            this.LocationNotesTextBox.Location = new System.Drawing.Point(28, 14);
            this.LocationNotesTextBox.LockKeyPress = false;
            this.LocationNotesTextBox.MaxLength = 200;
            this.LocationNotesTextBox.Multiline = true;
            this.LocationNotesTextBox.Name = "LocationNotesTextBox";
            this.LocationNotesTextBox.PersistDefaultColor = false;
            this.LocationNotesTextBox.Precision = 2;
            this.LocationNotesTextBox.QueryingFileldName = "";
            this.LocationNotesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LocationNotesTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.LocationNotesTextBox.Size = new System.Drawing.Size(575, 59);
            this.LocationNotesTextBox.SpecialCharacter = "%";
            this.LocationNotesTextBox.TabIndex = 14;
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
            // F84726
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.LocationNotesPanel);
            this.Controls.Add(this.DistrictProjectPanel);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.DepthPanel);
            this.Controls.Add(this.ElevationPanel);
            this.Controls.Add(this.OperationalAreaPanel);
            this.Controls.Add(this.AdministrativeAreaPanel);
            this.Controls.Add(this.WaterPipeLocationPictureBox);
            this.Name = "F84726";
            this.Size = new System.Drawing.Size(804, 118);
            this.Tag = "84726";
            this.Load += new System.EventHandler(this.F84726_Load);
            this.AdministrativeAreaPanel.ResumeLayout(false);
            this.AdministrativeAreaPanel.PerformLayout();
            this.OperationalAreaPanel.ResumeLayout(false);
            this.OperationalAreaPanel.PerformLayout();
            this.ElevationPanel.ResumeLayout(false);
            this.ElevationPanel.PerformLayout();
            this.DepthPanel.ResumeLayout(false);
            this.DepthPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaterPipeLocationPictureBox)).EndInit();
            this.GridPanel.ResumeLayout(false);
            this.GridPanel.PerformLayout();
            this.DistrictProjectPanel.ResumeLayout(false);
            this.DistrictProjectPanel.PerformLayout();
            this.LocationNotesPanel.ResumeLayout(false);
            this.LocationNotesPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel AdministrativeAreaPanel;
        private System.Windows.Forms.Label AdministrativeAreaLable;
        private System.Windows.Forms.Panel OperationalAreaPanel;
        private TerraScan.UI.Controls.TerraScanComboBox OperationalAreaComboBox;
        private System.Windows.Forms.Label OperationalAreaLabel;
        private System.Windows.Forms.Panel ElevationPanel;
        private TerraScan.UI.Controls.TerraScanTextBox ElevationTextBox;
        private System.Windows.Forms.Label ElevationLabel;
        private System.Windows.Forms.Panel DepthPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DepthTextBox;
        private System.Windows.Forms.Label DepthLable;
        private System.Windows.Forms.PictureBox WaterPipeLocationPictureBox;
        private System.Windows.Forms.Panel GridPanel;
        private TerraScan.UI.Controls.TerraScanComboBox GridComboBox;
        private System.Windows.Forms.Label GridLabel;
        private System.Windows.Forms.Panel DistrictProjectPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DistrictProjectTextBox;
        private System.Windows.Forms.Label DistrictProjectLabel;
        private System.Windows.Forms.Panel LocationNotesPanel;
        private TerraScan.UI.Controls.TerraScanTextBox LocationNotesTextBox;
        private System.Windows.Forms.Label LocationNotesLabel;
        private TerraScan.UI.Controls.TerraScanComboBox AdministrativeAreaComboBox;
        private System.Windows.Forms.ToolTip GDocWaterPipeLocationToolTip;
    }
}
