namespace D84100
{
    partial class F84124
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F84124));
            this.RegisterPanel = new System.Windows.Forms.Panel();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.GridComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.GridTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.GridLabel = new System.Windows.Forms.Label();
            this.LocationNotesPanel = new System.Windows.Forms.Panel();
            this.LocationNotesTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.LocationNotesLabel = new System.Windows.Forms.Label();
            this.DistrictProjectPanel = new System.Windows.Forms.Panel();
            this.DistrictProjectTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DistrictProjectLabel = new System.Windows.Forms.Label();
            this.OperationalAreaPanel = new System.Windows.Forms.Panel();
            this.OperationalAreaComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.OperationalAreaTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.OperationalAreaLabel = new System.Windows.Forms.Label();
            this.AdministrativeAreaPanel = new System.Windows.Forms.Panel();
            this.AdministrativeAreaComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.AdministrativeAreaLable = new System.Windows.Forms.Label();
            this.LocationPictureBox = new System.Windows.Forms.PictureBox();
            this.SanitaryPipeLocationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.RegisterPanel.SuspendLayout();
            this.GridPanel.SuspendLayout();
            this.LocationNotesPanel.SuspendLayout();
            this.DistrictProjectPanel.SuspendLayout();
            this.OperationalAreaPanel.SuspendLayout();
            this.AdministrativeAreaPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocationPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // RegisterPanel
            // 
            this.RegisterPanel.BackColor = System.Drawing.Color.White;
            this.RegisterPanel.Controls.Add(this.GridPanel);
            this.RegisterPanel.Controls.Add(this.LocationNotesPanel);
            this.RegisterPanel.Controls.Add(this.DistrictProjectPanel);
            this.RegisterPanel.Controls.Add(this.OperationalAreaPanel);
            this.RegisterPanel.Controls.Add(this.AdministrativeAreaPanel);
            this.RegisterPanel.Controls.Add(this.LocationPictureBox);
            this.RegisterPanel.Location = new System.Drawing.Point(0, 0);
            this.RegisterPanel.Name = "RegisterPanel";
            this.RegisterPanel.Size = new System.Drawing.Size(805, 132);
            this.RegisterPanel.TabIndex = 3;
            this.RegisterPanel.TabStop = true;
            // 
            // GridPanel
            // 
            this.GridPanel.BackColor = System.Drawing.Color.Transparent;
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.GridComboBox);
            this.GridPanel.Controls.Add(this.GridTextBox);
            this.GridPanel.Controls.Add(this.GridLabel);
            this.GridPanel.Location = new System.Drawing.Point(0, 39);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(164, 40);
            this.GridPanel.TabIndex = 2;
            this.GridPanel.TabStop = true;
            // 
            // GridComboBox
            // 
            this.GridComboBox.BackColor = System.Drawing.Color.White;
            this.GridComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GridComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GridComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.GridComboBox.FormattingEnabled = true;
            this.GridComboBox.Location = new System.Drawing.Point(12, 12);
            this.GridComboBox.Name = "GridComboBox";
            this.GridComboBox.Size = new System.Drawing.Size(140, 24);
            this.GridComboBox.TabIndex = 1;
            this.GridComboBox.SelectionChangeCommitted += new System.EventHandler(this.EnableSaveCancelButton);
            this.GridComboBox.SelectedIndexChanged += new System.EventHandler(this.GridComboBox_SelectedIndexChanged);
            // 
            // GridTextBox
            // 
            this.GridTextBox.AllowClick = true;
            this.GridTextBox.AllowNegativeSign = false;
            this.GridTextBox.ApplyCFGFormat = false;
            this.GridTextBox.ApplyCurrencyFormat = false;
            this.GridTextBox.ApplyFocusColor = true;
            this.GridTextBox.ApplyNegativeStandard = true;
            this.GridTextBox.ApplyParentFocusColor = true;
            this.GridTextBox.ApplyTimeFormat = false;
            this.GridTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridTextBox.CFromatWihoutSymbol = false;
            this.GridTextBox.CheckForEmpty = false;
            this.GridTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GridTextBox.Digits = -1;
            this.GridTextBox.EmptyDecimalValue = false;
            this.GridTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.GridTextBox.IsEditable = false;
            this.GridTextBox.IsQueryableFileld = true;
            this.GridTextBox.Location = new System.Drawing.Point(17, 14);
            this.GridTextBox.LockKeyPress = false;
            this.GridTextBox.MaxLength = 10;
            this.GridTextBox.Name = "GridTextBox";
            this.GridTextBox.PersistDefaultColor = false;
            this.GridTextBox.Precision = 2;
            this.GridTextBox.QueryingFileldName = "";
            this.GridTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.GridTextBox.Size = new System.Drawing.Size(116, 16);
            this.GridTextBox.SpecialCharacter = "%";
            this.GridTextBox.TabIndex = 4;
            this.GridTextBox.TabStop = false;
            this.GridTextBox.TextCustomFormat = "$#,##0.00";
            this.GridTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.GridTextBox.Visible = false;
            this.GridTextBox.WholeInteger = false;
            // 
            // GridLabel
            // 
            this.GridLabel.AutoSize = true;
            this.GridLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.GridLabel.Location = new System.Drawing.Point(1, -1);
            this.GridLabel.Name = "GridLabel";
            this.GridLabel.Size = new System.Drawing.Size(36, 14);
            this.GridLabel.TabIndex = 0;
            this.GridLabel.Text = "Grid: ";
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
            this.LocationNotesPanel.TabIndex = 4;
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
            this.LocationNotesTextBox.Location = new System.Drawing.Point(27, 16);
            this.LocationNotesTextBox.LockKeyPress = false;
            this.LocationNotesTextBox.MaxLength = 200;
            this.LocationNotesTextBox.Multiline = true;
            this.LocationNotesTextBox.Name = "LocationNotesTextBox";
            this.LocationNotesTextBox.PersistDefaultColor = false;
            this.LocationNotesTextBox.Precision = 2;
            this.LocationNotesTextBox.QueryingFileldName = "";
            this.LocationNotesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LocationNotesTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.LocationNotesTextBox.Size = new System.Drawing.Size(570, 56);
            this.LocationNotesTextBox.SpecialCharacter = "%";
            this.LocationNotesTextBox.TabIndex = 1;
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
            // DistrictProjectPanel
            // 
            this.DistrictProjectPanel.BackColor = System.Drawing.Color.Transparent;
            this.DistrictProjectPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DistrictProjectPanel.Controls.Add(this.DistrictProjectTextBox);
            this.DistrictProjectPanel.Controls.Add(this.DistrictProjectLabel);
            this.DistrictProjectPanel.Location = new System.Drawing.Point(0, 78);
            this.DistrictProjectPanel.Name = "DistrictProjectPanel";
            this.DistrictProjectPanel.Size = new System.Drawing.Size(164, 40);
            this.DistrictProjectPanel.TabIndex = 3;
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
            this.DistrictProjectTextBox.Location = new System.Drawing.Point(9, 15);
            this.DistrictProjectTextBox.LockKeyPress = true;
            this.DistrictProjectTextBox.MaxLength = 4;
            this.DistrictProjectTextBox.Name = "DistrictProjectTextBox";
            this.DistrictProjectTextBox.PersistDefaultColor = false;
            this.DistrictProjectTextBox.Precision = 2;
            this.DistrictProjectTextBox.QueryingFileldName = "";
            this.DistrictProjectTextBox.ReadOnly = true;
            this.DistrictProjectTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DistrictProjectTextBox.Size = new System.Drawing.Size(149, 16);
            this.DistrictProjectTextBox.SpecialCharacter = "%";
            this.DistrictProjectTextBox.TabIndex = 1;
            this.DistrictProjectTextBox.TabStop = false;
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
            // OperationalAreaPanel
            // 
            this.OperationalAreaPanel.BackColor = System.Drawing.Color.Transparent;
            this.OperationalAreaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OperationalAreaPanel.Controls.Add(this.OperationalAreaComboBox);
            this.OperationalAreaPanel.Controls.Add(this.OperationalAreaTextBox);
            this.OperationalAreaPanel.Controls.Add(this.OperationalAreaLabel);
            this.OperationalAreaPanel.Location = new System.Drawing.Point(274, 0);
            this.OperationalAreaPanel.Name = "OperationalAreaPanel";
            this.OperationalAreaPanel.Size = new System.Drawing.Size(494, 40);
            this.OperationalAreaPanel.TabIndex = 1;
            this.OperationalAreaPanel.TabStop = true;
            // 
            // OperationalAreaComboBox
            // 
            this.OperationalAreaComboBox.BackColor = System.Drawing.Color.White;
            this.OperationalAreaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OperationalAreaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OperationalAreaComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.OperationalAreaComboBox.FormattingEnabled = true;
            this.OperationalAreaComboBox.Location = new System.Drawing.Point(12, 13);
            this.OperationalAreaComboBox.Name = "OperationalAreaComboBox";
            this.OperationalAreaComboBox.Size = new System.Drawing.Size(469, 24);
            this.OperationalAreaComboBox.TabIndex = 1;
            this.OperationalAreaComboBox.SelectionChangeCommitted += new System.EventHandler(this.EnableSaveCancelButton);
            this.OperationalAreaComboBox.SelectedIndexChanged += new System.EventHandler(this.OperationalAreaComboBox_SelectedIndexChanged);
            // 
            // OperationalAreaTextBox
            // 
            this.OperationalAreaTextBox.AllowClick = true;
            this.OperationalAreaTextBox.AllowNegativeSign = false;
            this.OperationalAreaTextBox.ApplyCFGFormat = false;
            this.OperationalAreaTextBox.ApplyCurrencyFormat = false;
            this.OperationalAreaTextBox.ApplyFocusColor = true;
            this.OperationalAreaTextBox.ApplyNegativeStandard = true;
            this.OperationalAreaTextBox.ApplyParentFocusColor = true;
            this.OperationalAreaTextBox.ApplyTimeFormat = false;
            this.OperationalAreaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OperationalAreaTextBox.CFromatWihoutSymbol = false;
            this.OperationalAreaTextBox.CheckForEmpty = false;
            this.OperationalAreaTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.OperationalAreaTextBox.Digits = -1;
            this.OperationalAreaTextBox.EmptyDecimalValue = false;
            this.OperationalAreaTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.OperationalAreaTextBox.IsEditable = false;
            this.OperationalAreaTextBox.IsQueryableFileld = true;
            this.OperationalAreaTextBox.Location = new System.Drawing.Point(17, 14);
            this.OperationalAreaTextBox.LockKeyPress = false;
            this.OperationalAreaTextBox.MaxLength = 10;
            this.OperationalAreaTextBox.Name = "OperationalAreaTextBox";
            this.OperationalAreaTextBox.PersistDefaultColor = false;
            this.OperationalAreaTextBox.Precision = 2;
            this.OperationalAreaTextBox.QueryingFileldName = "";
            this.OperationalAreaTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.OperationalAreaTextBox.Size = new System.Drawing.Size(278, 16);
            this.OperationalAreaTextBox.SpecialCharacter = "%";
            this.OperationalAreaTextBox.TabIndex = 4;
            this.OperationalAreaTextBox.TabStop = false;
            this.OperationalAreaTextBox.TextCustomFormat = "$#,##0.00";
            this.OperationalAreaTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.OperationalAreaTextBox.Visible = false;
            this.OperationalAreaTextBox.WholeInteger = false;
            // 
            // OperationalAreaLabel
            // 
            this.OperationalAreaLabel.AutoSize = true;
            this.OperationalAreaLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OperationalAreaLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.OperationalAreaLabel.Location = new System.Drawing.Point(1, -1);
            this.OperationalAreaLabel.Name = "OperationalAreaLabel";
            this.OperationalAreaLabel.Size = new System.Drawing.Size(105, 14);
            this.OperationalAreaLabel.TabIndex = 0;
            this.OperationalAreaLabel.Text = "Operational Area: ";
            // 
            // AdministrativeAreaPanel
            // 
            this.AdministrativeAreaPanel.BackColor = System.Drawing.Color.Transparent;
            this.AdministrativeAreaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdministrativeAreaPanel.Controls.Add(this.AdministrativeAreaComboBox);
            this.AdministrativeAreaPanel.Controls.Add(this.AdministrativeAreaLable);
            this.AdministrativeAreaPanel.Location = new System.Drawing.Point(0, 0);
            this.AdministrativeAreaPanel.Name = "AdministrativeAreaPanel";
            this.AdministrativeAreaPanel.Size = new System.Drawing.Size(275, 40);
            this.AdministrativeAreaPanel.TabIndex = 0;
            this.AdministrativeAreaPanel.TabStop = true;
            // 
            // AdministrativeAreaComboBox
            // 
            this.AdministrativeAreaComboBox.BackColor = System.Drawing.Color.White;
            this.AdministrativeAreaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AdministrativeAreaComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AdministrativeAreaComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.AdministrativeAreaComboBox.FormattingEnabled = true;
            this.AdministrativeAreaComboBox.Location = new System.Drawing.Point(18, 12);
            this.AdministrativeAreaComboBox.Name = "AdministrativeAreaComboBox";
            this.AdministrativeAreaComboBox.Size = new System.Drawing.Size(249, 24);
            this.AdministrativeAreaComboBox.TabIndex = 1;
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
            // LocationPictureBox
            // 
            this.LocationPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("LocationPictureBox.Image")));
            this.LocationPictureBox.Location = new System.Drawing.Point(761, 0);
            this.LocationPictureBox.Name = "LocationPictureBox";
            this.LocationPictureBox.Size = new System.Drawing.Size(42, 118);
            this.LocationPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LocationPictureBox.TabIndex = 51;
            this.LocationPictureBox.TabStop = false;
            this.LocationPictureBox.Click += new System.EventHandler(this.LocationPictureBox_Click);
            this.LocationPictureBox.MouseEnter += new System.EventHandler(this.LocationPictureBox_MouseEnter);
            // 
            // F84124
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RegisterPanel);
            this.Name = "F84124";
            this.Size = new System.Drawing.Size(804, 118);
            this.Tag = "84124";
            this.Load += new System.EventHandler(this.F84124_Load);
            this.RegisterPanel.ResumeLayout(false);
            this.GridPanel.ResumeLayout(false);
            this.GridPanel.PerformLayout();
            this.LocationNotesPanel.ResumeLayout(false);
            this.LocationNotesPanel.PerformLayout();
            this.DistrictProjectPanel.ResumeLayout(false);
            this.DistrictProjectPanel.PerformLayout();
            this.OperationalAreaPanel.ResumeLayout(false);
            this.OperationalAreaPanel.PerformLayout();
            this.AdministrativeAreaPanel.ResumeLayout(false);
            this.AdministrativeAreaPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocationPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

      
        private System.Windows.Forms.Panel RegisterPanel;
        private System.Windows.Forms.Panel GridPanel;
        private TerraScan.UI.Controls.TerraScanComboBox GridComboBox;
        private TerraScan.UI.Controls.TerraScanTextBox GridTextBox;
        private System.Windows.Forms.Label GridLabel;
        private System.Windows.Forms.Panel LocationNotesPanel;
        private TerraScan.UI.Controls.TerraScanTextBox LocationNotesTextBox;
        private System.Windows.Forms.Label LocationNotesLabel;
        private System.Windows.Forms.Panel DistrictProjectPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DistrictProjectTextBox;
        private System.Windows.Forms.Label DistrictProjectLabel;
        private System.Windows.Forms.Panel OperationalAreaPanel;
        private TerraScan.UI.Controls.TerraScanComboBox OperationalAreaComboBox;
        private TerraScan.UI.Controls.TerraScanTextBox OperationalAreaTextBox;
        private System.Windows.Forms.Label OperationalAreaLabel;
        private System.Windows.Forms.Panel AdministrativeAreaPanel;
        private System.Windows.Forms.Label AdministrativeAreaLable;
        private System.Windows.Forms.PictureBox LocationPictureBox;
        private TerraScan.UI.Controls.TerraScanComboBox AdministrativeAreaComboBox;
        private System.Windows.Forms.ToolTip SanitaryPipeLocationToolTip;

    }
}
