namespace D9050
{
    partial class F9050
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9050));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.headerLabel = new System.Windows.Forms.Label();
            this.formIDLabel = new System.Windows.Forms.Label();
            this.namePanel = new System.Windows.Forms.Panel();
            this.nameTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.createdByPanel = new System.Windows.Forms.Panel();
            this.createdByTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.createdByLabel = new System.Windows.Forms.Label();
            this.createdOnPanel = new System.Windows.Forms.Panel();
            this.createdOnTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.createdOnLabel = new System.Windows.Forms.Label();
            this.descriptionPanel = new System.Windows.Forms.Panel();
            this.descriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.whereConditionPanel = new System.Windows.Forms.Panel();
            this.whereConditionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.whereConditionLabel = new System.Windows.Forms.Label();
            this.newEditPictureBox = new System.Windows.Forms.PictureBox();
            this.existingPictureBox = new System.Windows.Forms.PictureBox();
            this.closeQueryUtilityButton = new TerraScan.UI.Controls.TerraScanButton();
            this.deleteQueryUtilityButton = new TerraScan.UI.Controls.TerraScanButton();
            this.loadQueryUtilityButton = new TerraScan.UI.Controls.TerraScanButton();
            this.cancelQueryUtilityButton = new TerraScan.UI.Controls.TerraScanButton();
            this.saveQueryUtilityButton = new TerraScan.UI.Controls.TerraScanButton();
            this.newQueryUtilityButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CommentLinePanel = new System.Windows.Forms.Panel();
            this.queryVScrollBar = new System.Windows.Forms.VScrollBar();
            this.queryUtilityGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.QueryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SavedQueryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhereCondnSql = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserWhereCondn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CommentMenuStrip = new System.Windows.Forms.MenuStrip();
            this.NewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.namePanel.SuspendLayout();
            this.createdByPanel.SuspendLayout();
            this.createdOnPanel.SuspendLayout();
            this.descriptionPanel.SuspendLayout();
            this.whereConditionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newEditPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.existingPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.queryUtilityGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.CommentMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(80)))), ((int)(((byte)(129)))));
            this.headerLabel.Location = new System.Drawing.Point(591, 14);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(123, 22);
            this.headerLabel.TabIndex = 23;
            this.headerLabel.Text = "Query Utility";
            // 
            // formIDLabel
            // 
            this.formIDLabel.AutoSize = true;
            this.formIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.formIDLabel.Location = new System.Drawing.Point(11, 380);
            this.formIDLabel.Name = "formIDLabel";
            this.formIDLabel.Size = new System.Drawing.Size(0, 15);
            this.formIDLabel.TabIndex = 96;
            // 
            // namePanel
            // 
            this.namePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.namePanel.Controls.Add(this.nameTextBox);
            this.namePanel.Controls.Add(this.nameLabel);
            this.namePanel.Location = new System.Drawing.Point(12, 57);
            this.namePanel.Name = "namePanel";
            this.namePanel.Size = new System.Drawing.Size(363, 37);
            this.namePanel.TabIndex = 2;
            this.namePanel.TabStop = true;
            // 
            // nameTextBox
            // 
            this.nameTextBox.AllowClick = true;
            this.nameTextBox.ApplyCFGFormat = false;
            this.nameTextBox.ApplyCurrencyFormat = false;
            this.nameTextBox.ApplyFocusColor = true;
            this.nameTextBox.ApplyParentFocusColor = true;
            this.nameTextBox.ApplyTimeFormat = false;
            this.nameTextBox.BackColor = System.Drawing.Color.White;
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameTextBox.CFromatWihoutSymbol = false;
            this.nameTextBox.CheckForEmpty = false;
            this.nameTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nameTextBox.Digits = -1;
            this.nameTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.nameTextBox.ForeColor = System.Drawing.Color.Black;
            this.nameTextBox.IsEditable = false;
            this.nameTextBox.IsQueryableFileld = false;
            this.nameTextBox.Location = new System.Drawing.Point(14, 16);
            this.nameTextBox.LockKeyPress = false;
            this.nameTextBox.MaxLength = 50;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.PersistDefaultColor = false;
            this.nameTextBox.Precision = 2;
            this.nameTextBox.QueryingFileldName = "";
            this.nameTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.nameTextBox.Size = new System.Drawing.Size(342, 16);
            this.nameTextBox.SpecialCharacter = "%";
            this.nameTextBox.TabIndex = 3;
            this.nameTextBox.TextCustomFormat = "$#,##0.00";
            this.nameTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.nameTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NameTextBox_KeyUp);
            this.nameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameTextBox_KeyPress);
            this.nameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NameTextBox_KeyDown);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nameLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.nameLabel.Location = new System.Drawing.Point(0, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(41, 14);
            this.nameLabel.TabIndex = 62;
            this.nameLabel.Text = "Name:";
            // 
            // createdByPanel
            // 
            this.createdByPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.createdByPanel.Controls.Add(this.createdByTextBox);
            this.createdByPanel.Controls.Add(this.createdByLabel);
            this.createdByPanel.Location = new System.Drawing.Point(374, 57);
            this.createdByPanel.Name = "createdByPanel";
            this.createdByPanel.Size = new System.Drawing.Size(143, 37);
            this.createdByPanel.TabIndex = 98;
            this.createdByPanel.TabStop = true;
            // 
            // createdByTextBox
            // 
            this.createdByTextBox.AllowClick = true;
            this.createdByTextBox.ApplyCFGFormat = false;
            this.createdByTextBox.ApplyCurrencyFormat = false;
            this.createdByTextBox.ApplyFocusColor = true;
            this.createdByTextBox.ApplyParentFocusColor = true;
            this.createdByTextBox.ApplyTimeFormat = false;
            this.createdByTextBox.BackColor = System.Drawing.Color.White;
            this.createdByTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.createdByTextBox.CFromatWihoutSymbol = false;
            this.createdByTextBox.CheckForEmpty = false;
            this.createdByTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.createdByTextBox.Digits = -1;
            this.createdByTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.createdByTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.createdByTextBox.IsEditable = false;
            this.createdByTextBox.IsQueryableFileld = false;
            this.createdByTextBox.Location = new System.Drawing.Point(14, 16);
            this.createdByTextBox.LockKeyPress = true;
            this.createdByTextBox.Name = "createdByTextBox";
            this.createdByTextBox.PersistDefaultColor = false;
            this.createdByTextBox.Precision = 2;
            this.createdByTextBox.QueryingFileldName = "";
            this.createdByTextBox.ReadOnly = true;
            this.createdByTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.createdByTextBox.Size = new System.Drawing.Size(121, 16);
            this.createdByTextBox.SpecialCharacter = "%";
            this.createdByTextBox.TabIndex = 2;
            this.createdByTextBox.TabStop = false;
            this.createdByTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.createdByTextBox.TextCustomFormat = "$#,##0.00";
            this.createdByTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            // 
            // createdByLabel
            // 
            this.createdByLabel.AutoSize = true;
            this.createdByLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.createdByLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createdByLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.createdByLabel.Location = new System.Drawing.Point(0, 0);
            this.createdByLabel.Name = "createdByLabel";
            this.createdByLabel.Size = new System.Drawing.Size(70, 14);
            this.createdByLabel.TabIndex = 62;
            this.createdByLabel.Text = "Created By:";
            // 
            // createdOnPanel
            // 
            this.createdOnPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.createdOnPanel.Controls.Add(this.createdOnTextBox);
            this.createdOnPanel.Controls.Add(this.createdOnLabel);
            this.createdOnPanel.Location = new System.Drawing.Point(516, 57);
            this.createdOnPanel.Name = "createdOnPanel";
            this.createdOnPanel.Size = new System.Drawing.Size(147, 37);
            this.createdOnPanel.TabIndex = 99;
            this.createdOnPanel.TabStop = true;
            // 
            // createdOnTextBox
            // 
            this.createdOnTextBox.AllowClick = true;
            this.createdOnTextBox.ApplyCFGFormat = false;
            this.createdOnTextBox.ApplyCurrencyFormat = false;
            this.createdOnTextBox.ApplyFocusColor = true;
            this.createdOnTextBox.ApplyParentFocusColor = true;
            this.createdOnTextBox.ApplyTimeFormat = false;
            this.createdOnTextBox.BackColor = System.Drawing.Color.White;
            this.createdOnTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.createdOnTextBox.CFromatWihoutSymbol = false;
            this.createdOnTextBox.CheckForEmpty = false;
            this.createdOnTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.createdOnTextBox.Digits = -1;
            this.createdOnTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.createdOnTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.createdOnTextBox.IsEditable = false;
            this.createdOnTextBox.IsQueryableFileld = false;
            this.createdOnTextBox.Location = new System.Drawing.Point(14, 16);
            this.createdOnTextBox.LockKeyPress = true;
            this.createdOnTextBox.Name = "createdOnTextBox";
            this.createdOnTextBox.PersistDefaultColor = false;
            this.createdOnTextBox.Precision = 2;
            this.createdOnTextBox.QueryingFileldName = "";
            this.createdOnTextBox.ReadOnly = true;
            this.createdOnTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.createdOnTextBox.Size = new System.Drawing.Size(121, 16);
            this.createdOnTextBox.SpecialCharacter = "%";
            this.createdOnTextBox.TabIndex = 2;
            this.createdOnTextBox.TabStop = false;
            this.createdOnTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.createdOnTextBox.TextCustomFormat = "$#,##0.00";
            this.createdOnTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            // 
            // createdOnLabel
            // 
            this.createdOnLabel.AutoSize = true;
            this.createdOnLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.createdOnLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createdOnLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.createdOnLabel.Location = new System.Drawing.Point(0, 0);
            this.createdOnLabel.Name = "createdOnLabel";
            this.createdOnLabel.Size = new System.Drawing.Size(72, 14);
            this.createdOnLabel.TabIndex = 62;
            this.createdOnLabel.Text = "Created On:";
            // 
            // descriptionPanel
            // 
            this.descriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionPanel.Controls.Add(this.descriptionTextBox);
            this.descriptionPanel.Controls.Add(this.descriptionLabel);
            this.descriptionPanel.Location = new System.Drawing.Point(12, 93);
            this.descriptionPanel.Name = "descriptionPanel";
            this.descriptionPanel.Size = new System.Drawing.Size(651, 37);
            this.descriptionPanel.TabIndex = 4;
            this.descriptionPanel.TabStop = true;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.AllowClick = true;
            this.descriptionTextBox.ApplyCFGFormat = false;
            this.descriptionTextBox.ApplyCurrencyFormat = false;
            this.descriptionTextBox.ApplyFocusColor = true;
            this.descriptionTextBox.ApplyParentFocusColor = true;
            this.descriptionTextBox.ApplyTimeFormat = false;
            this.descriptionTextBox.BackColor = System.Drawing.Color.White;
            this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionTextBox.CFromatWihoutSymbol = false;
            this.descriptionTextBox.CheckForEmpty = false;
            this.descriptionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.descriptionTextBox.Digits = -1;
            this.descriptionTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.descriptionTextBox.ForeColor = System.Drawing.Color.Black;
            this.descriptionTextBox.IsEditable = false;
            this.descriptionTextBox.IsQueryableFileld = false;
            this.descriptionTextBox.Location = new System.Drawing.Point(35, 16);
            this.descriptionTextBox.LockKeyPress = false;
            this.descriptionTextBox.MaxLength = 250;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.PersistDefaultColor = false;
            this.descriptionTextBox.Precision = 2;
            this.descriptionTextBox.QueryingFileldName = "";
            this.descriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.descriptionTextBox.Size = new System.Drawing.Size(564, 16);
            this.descriptionTextBox.SpecialCharacter = "%";
            this.descriptionTextBox.TabIndex = 5;
            this.descriptionTextBox.TextCustomFormat = "$#,##0.00";
            this.descriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.descriptionTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DescriptionTextBox_KeyUp);
            this.descriptionTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DescriptionTextBox_KeyPress);
            this.descriptionTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DescriptionTextBox_KeyDown);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.descriptionLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.descriptionLabel.Location = new System.Drawing.Point(0, 0);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(73, 14);
            this.descriptionLabel.TabIndex = 62;
            this.descriptionLabel.Text = "Description:";
            // 
            // whereConditionPanel
            // 
            this.whereConditionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.whereConditionPanel.Controls.Add(this.whereConditionTextBox);
            this.whereConditionPanel.Controls.Add(this.whereConditionLabel);
            this.whereConditionPanel.Location = new System.Drawing.Point(12, 129);
            this.whereConditionPanel.Name = "whereConditionPanel";
            this.whereConditionPanel.Size = new System.Drawing.Size(651, 37);
            this.whereConditionPanel.TabIndex = 101;
            this.whereConditionPanel.TabStop = true;
            // 
            // whereConditionTextBox
            // 
            this.whereConditionTextBox.AllowClick = true;
            this.whereConditionTextBox.ApplyCFGFormat = false;
            this.whereConditionTextBox.ApplyCurrencyFormat = false;
            this.whereConditionTextBox.ApplyFocusColor = true;
            this.whereConditionTextBox.ApplyParentFocusColor = true;
            this.whereConditionTextBox.ApplyTimeFormat = false;
            this.whereConditionTextBox.BackColor = System.Drawing.Color.White;
            this.whereConditionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.whereConditionTextBox.CFromatWihoutSymbol = false;
            this.whereConditionTextBox.CheckForEmpty = false;
            this.whereConditionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.whereConditionTextBox.Digits = -1;
            this.whereConditionTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.whereConditionTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.whereConditionTextBox.IsEditable = false;
            this.whereConditionTextBox.IsQueryableFileld = false;
            this.whereConditionTextBox.Location = new System.Drawing.Point(35, 16);
            this.whereConditionTextBox.LockKeyPress = true;
            this.whereConditionTextBox.Multiline = true;
            this.whereConditionTextBox.Name = "whereConditionTextBox";
            this.whereConditionTextBox.PersistDefaultColor = false;
            this.whereConditionTextBox.Precision = 2;
            this.whereConditionTextBox.QueryingFileldName = "";
            this.whereConditionTextBox.ReadOnly = true;
            this.whereConditionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.whereConditionTextBox.Size = new System.Drawing.Size(572, 16);
            this.whereConditionTextBox.SpecialCharacter = "%";
            this.whereConditionTextBox.TabIndex = 2;
            this.whereConditionTextBox.TabStop = false;
            this.whereConditionTextBox.TextCustomFormat = "$#,##0.00";
            this.whereConditionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            // 
            // whereConditionLabel
            // 
            this.whereConditionLabel.AutoSize = true;
            this.whereConditionLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.whereConditionLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whereConditionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.whereConditionLabel.Location = new System.Drawing.Point(0, 0);
            this.whereConditionLabel.Name = "whereConditionLabel";
            this.whereConditionLabel.Size = new System.Drawing.Size(46, 14);
            this.whereConditionLabel.TabIndex = 62;
            this.whereConditionLabel.Text = "Where:";
            // 
            // newEditPictureBox
            // 
            this.newEditPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("newEditPictureBox.Image")));
            this.newEditPictureBox.Location = new System.Drawing.Point(652, 57);
            this.newEditPictureBox.Name = "newEditPictureBox";
            this.newEditPictureBox.Size = new System.Drawing.Size(42, 109);
            this.newEditPictureBox.TabIndex = 102;
            this.newEditPictureBox.TabStop = false;
            // 
            // existingPictureBox
            // 
            this.existingPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("existingPictureBox.Image")));
            this.existingPictureBox.Location = new System.Drawing.Point(652, 196);
            this.existingPictureBox.Name = "existingPictureBox";
            this.existingPictureBox.Size = new System.Drawing.Size(42, 129);
            this.existingPictureBox.TabIndex = 103;
            this.existingPictureBox.TabStop = false;
            // 
            // closeQueryUtilityButton
            // 
            this.closeQueryUtilityButton.ActualPermission = false;
            this.closeQueryUtilityButton.ApplyDisableBehaviour = false;
            this.closeQueryUtilityButton.AutoSize = true;
            this.closeQueryUtilityButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.closeQueryUtilityButton.BorderColor = System.Drawing.Color.Wheat;
            this.closeQueryUtilityButton.CommentPriority = false;
            this.closeQueryUtilityButton.EnableAutoPrint = false;
            this.closeQueryUtilityButton.FilterStatus = false;
            this.closeQueryUtilityButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.closeQueryUtilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeQueryUtilityButton.FocusRectangleEnabled = true;
            this.closeQueryUtilityButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeQueryUtilityButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.closeQueryUtilityButton.ImageSelected = false;
            this.closeQueryUtilityButton.Location = new System.Drawing.Point(584, 340);
            this.closeQueryUtilityButton.Name = "closeQueryUtilityButton";
            this.closeQueryUtilityButton.NewPadding = 5;
            this.closeQueryUtilityButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.closeQueryUtilityButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.closeQueryUtilityButton.Size = new System.Drawing.Size(110, 30);
            this.closeQueryUtilityButton.StatusIndicator = false;
            this.closeQueryUtilityButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.closeQueryUtilityButton.StatusOffText = null;
            this.closeQueryUtilityButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.closeQueryUtilityButton.StatusOnText = null;
            this.closeQueryUtilityButton.TabIndex = 10;
            this.closeQueryUtilityButton.TabStop = false;
            this.closeQueryUtilityButton.Text = "Close";
            this.closeQueryUtilityButton.UseVisualStyleBackColor = false;
            this.closeQueryUtilityButton.Click += new System.EventHandler(this.CloseQueryUtilityButton_Click);
            // 
            // deleteQueryUtilityButton
            // 
            this.deleteQueryUtilityButton.ActualPermission = false;
            this.deleteQueryUtilityButton.ApplyDisableBehaviour = false;
            this.deleteQueryUtilityButton.AutoSize = true;
            this.deleteQueryUtilityButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.deleteQueryUtilityButton.BorderColor = System.Drawing.Color.Wheat;
            this.deleteQueryUtilityButton.CommentPriority = false;
            this.deleteQueryUtilityButton.EnableAutoPrint = false;
            this.deleteQueryUtilityButton.FilterStatus = false;
            this.deleteQueryUtilityButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.deleteQueryUtilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteQueryUtilityButton.FocusRectangleEnabled = true;
            this.deleteQueryUtilityButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteQueryUtilityButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.deleteQueryUtilityButton.ImageSelected = false;
            this.deleteQueryUtilityButton.Location = new System.Drawing.Point(476, 12);
            this.deleteQueryUtilityButton.Name = "deleteQueryUtilityButton";
            this.deleteQueryUtilityButton.NewPadding = 5;
            this.deleteQueryUtilityButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Delete;
            this.deleteQueryUtilityButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.deleteQueryUtilityButton.Size = new System.Drawing.Size(110, 30);
            this.deleteQueryUtilityButton.StatusIndicator = false;
            this.deleteQueryUtilityButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.deleteQueryUtilityButton.StatusOffText = null;
            this.deleteQueryUtilityButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.deleteQueryUtilityButton.StatusOnText = null;
            this.deleteQueryUtilityButton.TabIndex = 9;
            this.deleteQueryUtilityButton.TabStop = false;
            this.deleteQueryUtilityButton.Text = "Delete";
            this.deleteQueryUtilityButton.UseVisualStyleBackColor = false;
            this.deleteQueryUtilityButton.Click += new System.EventHandler(this.DeleteQueryUtilityButton_Click);
            // 
            // loadQueryUtilityButton
            // 
            this.loadQueryUtilityButton.ActualPermission = false;
            this.loadQueryUtilityButton.ApplyDisableBehaviour = false;
            this.loadQueryUtilityButton.AutoSize = true;
            this.loadQueryUtilityButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.loadQueryUtilityButton.BorderColor = System.Drawing.Color.Wheat;
            this.loadQueryUtilityButton.CommentPriority = false;
            this.loadQueryUtilityButton.EnableAutoPrint = false;
            this.loadQueryUtilityButton.FilterStatus = false;
            this.loadQueryUtilityButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.loadQueryUtilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadQueryUtilityButton.FocusRectangleEnabled = true;
            this.loadQueryUtilityButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadQueryUtilityButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.loadQueryUtilityButton.ImageSelected = false;
            this.loadQueryUtilityButton.Location = new System.Drawing.Point(360, 12);
            this.loadQueryUtilityButton.Name = "loadQueryUtilityButton";
            this.loadQueryUtilityButton.NewPadding = 5;
            this.loadQueryUtilityButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.loadQueryUtilityButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.loadQueryUtilityButton.Size = new System.Drawing.Size(110, 30);
            this.loadQueryUtilityButton.StatusIndicator = false;
            this.loadQueryUtilityButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.loadQueryUtilityButton.StatusOffText = null;
            this.loadQueryUtilityButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.loadQueryUtilityButton.StatusOnText = null;
            this.loadQueryUtilityButton.TabIndex = 8;
            this.loadQueryUtilityButton.TabStop = false;
            this.loadQueryUtilityButton.Text = "Load";
            this.loadQueryUtilityButton.UseVisualStyleBackColor = false;
            this.loadQueryUtilityButton.Click += new System.EventHandler(this.LoadQueryUtilityButton_Click);
            // 
            // cancelQueryUtilityButton
            // 
            this.cancelQueryUtilityButton.ActualPermission = false;
            this.cancelQueryUtilityButton.ApplyDisableBehaviour = false;
            this.cancelQueryUtilityButton.AutoSize = true;
            this.cancelQueryUtilityButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.cancelQueryUtilityButton.BorderColor = System.Drawing.Color.Wheat;
            this.cancelQueryUtilityButton.CommentPriority = false;
            this.cancelQueryUtilityButton.EnableAutoPrint = false;
            this.cancelQueryUtilityButton.FilterStatus = false;
            this.cancelQueryUtilityButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.cancelQueryUtilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelQueryUtilityButton.FocusRectangleEnabled = true;
            this.cancelQueryUtilityButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelQueryUtilityButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cancelQueryUtilityButton.ImageSelected = false;
            this.cancelQueryUtilityButton.Location = new System.Drawing.Point(244, 12);
            this.cancelQueryUtilityButton.Name = "cancelQueryUtilityButton";
            this.cancelQueryUtilityButton.NewPadding = 5;
            this.cancelQueryUtilityButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.cancelQueryUtilityButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.cancelQueryUtilityButton.Size = new System.Drawing.Size(110, 30);
            this.cancelQueryUtilityButton.StatusIndicator = false;
            this.cancelQueryUtilityButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cancelQueryUtilityButton.StatusOffText = null;
            this.cancelQueryUtilityButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.cancelQueryUtilityButton.StatusOnText = null;
            this.cancelQueryUtilityButton.TabIndex = 7;
            this.cancelQueryUtilityButton.TabStop = false;
            this.cancelQueryUtilityButton.Text = "Cancel";
            this.cancelQueryUtilityButton.UseVisualStyleBackColor = false;
            this.cancelQueryUtilityButton.Click += new System.EventHandler(this.CancelQueryUtilityButton_Click);
            // 
            // saveQueryUtilityButton
            // 
            this.saveQueryUtilityButton.ActualPermission = false;
            this.saveQueryUtilityButton.ApplyDisableBehaviour = false;
            this.saveQueryUtilityButton.AutoSize = true;
            this.saveQueryUtilityButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.saveQueryUtilityButton.BorderColor = System.Drawing.Color.Wheat;
            this.saveQueryUtilityButton.CommentPriority = false;
            this.saveQueryUtilityButton.EnableAutoPrint = false;
            this.saveQueryUtilityButton.FilterStatus = false;
            this.saveQueryUtilityButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.saveQueryUtilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveQueryUtilityButton.FocusRectangleEnabled = true;
            this.saveQueryUtilityButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveQueryUtilityButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.saveQueryUtilityButton.ImageSelected = false;
            this.saveQueryUtilityButton.Location = new System.Drawing.Point(128, 12);
            this.saveQueryUtilityButton.Name = "saveQueryUtilityButton";
            this.saveQueryUtilityButton.NewPadding = 5;
            this.saveQueryUtilityButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Save;
            this.saveQueryUtilityButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.saveQueryUtilityButton.Size = new System.Drawing.Size(110, 30);
            this.saveQueryUtilityButton.StatusIndicator = false;
            this.saveQueryUtilityButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.saveQueryUtilityButton.StatusOffText = null;
            this.saveQueryUtilityButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.saveQueryUtilityButton.StatusOnText = null;
            this.saveQueryUtilityButton.TabIndex = 6;
            this.saveQueryUtilityButton.TabStop = false;
            this.saveQueryUtilityButton.Text = "Save";
            this.saveQueryUtilityButton.UseVisualStyleBackColor = false;
            this.saveQueryUtilityButton.Click += new System.EventHandler(this.SaveQueryUtilityButton_Click);
            // 
            // newQueryUtilityButton
            // 
            this.newQueryUtilityButton.ActualPermission = false;
            this.newQueryUtilityButton.ApplyDisableBehaviour = false;
            this.newQueryUtilityButton.AutoSize = true;
            this.newQueryUtilityButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.newQueryUtilityButton.BorderColor = System.Drawing.Color.Wheat;
            this.newQueryUtilityButton.CommentPriority = false;
            this.newQueryUtilityButton.EnableAutoPrint = false;
            this.newQueryUtilityButton.FilterStatus = false;
            this.newQueryUtilityButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.newQueryUtilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newQueryUtilityButton.FocusRectangleEnabled = true;
            this.newQueryUtilityButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newQueryUtilityButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.newQueryUtilityButton.ImageSelected = false;
            this.newQueryUtilityButton.Location = new System.Drawing.Point(12, 12);
            this.newQueryUtilityButton.Name = "newQueryUtilityButton";
            this.newQueryUtilityButton.NewPadding = 5;
            this.newQueryUtilityButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.newQueryUtilityButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.newQueryUtilityButton.Size = new System.Drawing.Size(110, 30);
            this.newQueryUtilityButton.StatusIndicator = false;
            this.newQueryUtilityButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.newQueryUtilityButton.StatusOffText = null;
            this.newQueryUtilityButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.newQueryUtilityButton.StatusOnText = null;
            this.newQueryUtilityButton.TabIndex = 1;
            this.newQueryUtilityButton.TabStop = false;
            this.newQueryUtilityButton.Text = "New";
            this.newQueryUtilityButton.UseVisualStyleBackColor = false;
            this.newQueryUtilityButton.Click += new System.EventHandler(this.NewQueryUtilityButton_Click);
            // 
            // CommentLinePanel
            // 
            this.CommentLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.CommentLinePanel.Location = new System.Drawing.Point(11, 374);
            this.CommentLinePanel.Name = "CommentLinePanel";
            this.CommentLinePanel.Size = new System.Drawing.Size(688, 2);
            this.CommentLinePanel.TabIndex = 152;
            // 
            // queryVScrollBar
            // 
            this.queryVScrollBar.Enabled = false;
            this.queryVScrollBar.Location = new System.Drawing.Point(628, 0);
            this.queryVScrollBar.Name = "queryVScrollBar";
            this.queryVScrollBar.Size = new System.Drawing.Size(17, 127);
            this.queryVScrollBar.TabIndex = 154;
            // 
            // queryUtilityGridView
            // 
            this.queryUtilityGridView.AllowCellClick = true;
            this.queryUtilityGridView.AllowDoubleClick = false;
            this.queryUtilityGridView.AllowEmptyRows = true;
            this.queryUtilityGridView.AllowEnterKey = false;
            this.queryUtilityGridView.AllowSorting = false;
            this.queryUtilityGridView.AllowUserToAddRows = false;
            this.queryUtilityGridView.AllowUserToDeleteRows = false;
            this.queryUtilityGridView.AllowUserToResizeColumns = false;
            this.queryUtilityGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.queryUtilityGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.queryUtilityGridView.ApplyStandardBehaviour = false;
            this.queryUtilityGridView.BackgroundColor = System.Drawing.Color.White;
            this.queryUtilityGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.queryUtilityGridView.ClearCurrentCellOnLeave = false;
            this.queryUtilityGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.queryUtilityGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.queryUtilityGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.queryUtilityGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QueryName,
            this.Description,
            this.CreatedBy,
            this.CreatedOn,
            this.SavedQueryId,
            this.WhereCondnSql,
            this.UserWhereCondn});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.queryUtilityGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.queryUtilityGridView.DefaultRowIndex = -1;
            this.queryUtilityGridView.DeselectCurrentCell = false;
            this.queryUtilityGridView.DeselectSpecifiedRow = -1;
            this.queryUtilityGridView.EnableBinding = true;
            this.queryUtilityGridView.EnableHeadersVisualStyles = false;
            this.queryUtilityGridView.GridColor = System.Drawing.Color.Black;
            this.queryUtilityGridView.GridContentSelected = false;
            this.queryUtilityGridView.IsEditableGrid = false;
            this.queryUtilityGridView.Location = new System.Drawing.Point(-1, -1);
            this.queryUtilityGridView.MultiSelect = false;
            this.queryUtilityGridView.Name = "queryUtilityGridView";
            this.queryUtilityGridView.NumRowsVisible = 5;
            this.queryUtilityGridView.PrimaryKeyColumnName = "";
            this.queryUtilityGridView.RemainSortFields = false;
            this.queryUtilityGridView.RemoveDefaultSelection = false;
            this.queryUtilityGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.queryUtilityGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.queryUtilityGridView.RowHeadersWidth = 20;
            this.queryUtilityGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.queryUtilityGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.queryUtilityGridView.Size = new System.Drawing.Size(647, 129);
            this.queryUtilityGridView.TabIndex = 1;
            this.queryUtilityGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QueryUtilityGridView_KeyDown);
            this.queryUtilityGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryUtilityGridView_CellClick);
            this.queryUtilityGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryUtilityGridView_RowEnter);
            this.queryUtilityGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryUtilityGridView_CellClick);
            // 
            // QueryName
            // 
            this.QueryName.HeaderText = "Name";
            this.QueryName.Name = "QueryName";
            this.QueryName.ReadOnly = true;
            this.QueryName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.QueryName.Width = 150;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Description.Width = 260;
            // 
            // CreatedBy
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CreatedBy.DefaultCellStyle = dataGridViewCellStyle3;
            this.CreatedBy.HeaderText = "Created By";
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.ReadOnly = true;
            this.CreatedBy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CreatedOn
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CreatedOn.DefaultCellStyle = dataGridViewCellStyle4;
            this.CreatedOn.HeaderText = "Created On";
            this.CreatedOn.Name = "CreatedOn";
            this.CreatedOn.ReadOnly = true;
            this.CreatedOn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SavedQueryId
            // 
            this.SavedQueryId.HeaderText = "QueryID";
            this.SavedQueryId.Name = "SavedQueryId";
            this.SavedQueryId.ReadOnly = true;
            this.SavedQueryId.Visible = false;
            // 
            // WhereCondnSql
            // 
            this.WhereCondnSql.HeaderText = "WhereCondnSql";
            this.WhereCondnSql.Name = "WhereCondnSql";
            this.WhereCondnSql.ReadOnly = true;
            this.WhereCondnSql.Visible = false;
            // 
            // UserWhereCondn
            // 
            this.UserWhereCondn.HeaderText = "WhereCondition";
            this.UserWhereCondn.Name = "UserWhereCondn";
            this.UserWhereCondn.ReadOnly = true;
            this.UserWhereCondn.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.queryVScrollBar);
            this.panel1.Controls.Add(this.queryUtilityGridView);
            this.panel1.Location = new System.Drawing.Point(11, 196);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 129);
            this.panel1.TabIndex = 0;
            // 
            // CommentMenuStrip
            // 
            this.CommentMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMenu,
            this.SaveMenu});
            this.CommentMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.CommentMenuStrip.Name = "CommentMenuStrip";
            this.CommentMenuStrip.Size = new System.Drawing.Size(711, 24);
            this.CommentMenuStrip.TabIndex = 153;
            this.CommentMenuStrip.Text = "menuStrip1";
            this.CommentMenuStrip.Visible = false;
            // 
            // NewMenu
            // 
            this.NewMenu.Name = "NewMenu";
            this.NewMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewMenu.Size = new System.Drawing.Size(66, 20);
            this.NewMenu.Text = "NewMenu";
            // 
            // SaveMenu
            // 
            this.SaveMenu.Name = "SaveMenu";
            this.SaveMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveMenu.Size = new System.Drawing.Size(69, 20);
            this.SaveMenu.Text = "SaveMenu";
            // 
            // F9050
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(711, 398);
            this.Controls.Add(this.CommentMenuStrip);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CommentLinePanel);
            this.Controls.Add(this.whereConditionPanel);
            this.Controls.Add(this.descriptionPanel);
            this.Controls.Add(this.createdOnPanel);
            this.Controls.Add(this.createdByPanel);
            this.Controls.Add(this.namePanel);
            this.Controls.Add(this.formIDLabel);
            this.Controls.Add(this.headerLabel);
            this.Controls.Add(this.closeQueryUtilityButton);
            this.Controls.Add(this.deleteQueryUtilityButton);
            this.Controls.Add(this.loadQueryUtilityButton);
            this.Controls.Add(this.cancelQueryUtilityButton);
            this.Controls.Add(this.saveQueryUtilityButton);
            this.Controls.Add(this.newQueryUtilityButton);
            this.Controls.Add(this.newEditPictureBox);
            this.Controls.Add(this.existingPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F9050";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - Query Utility";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QueryUtilityForm_FormClosing);
            this.Load += new System.EventHandler(this.QueryUtility_Load);
            this.namePanel.ResumeLayout(false);
            this.namePanel.PerformLayout();
            this.createdByPanel.ResumeLayout(false);
            this.createdByPanel.PerformLayout();
            this.createdOnPanel.ResumeLayout(false);
            this.createdOnPanel.PerformLayout();
            this.descriptionPanel.ResumeLayout(false);
            this.descriptionPanel.PerformLayout();
            this.whereConditionPanel.ResumeLayout(false);
            this.whereConditionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newEditPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.existingPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.queryUtilityGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.CommentMenuStrip.ResumeLayout(false);
            this.CommentMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanButton newQueryUtilityButton;
        private TerraScan.UI.Controls.TerraScanButton saveQueryUtilityButton;
        private TerraScan.UI.Controls.TerraScanButton cancelQueryUtilityButton;
        private TerraScan.UI.Controls.TerraScanButton loadQueryUtilityButton;
        private TerraScan.UI.Controls.TerraScanButton deleteQueryUtilityButton;
        private TerraScan.UI.Controls.TerraScanButton closeQueryUtilityButton;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label formIDLabel;
        private System.Windows.Forms.Panel namePanel;
        private TerraScan.UI.Controls.TerraScanTextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Panel createdByPanel;
        private TerraScan.UI.Controls.TerraScanTextBox createdByTextBox;
        private System.Windows.Forms.Label createdByLabel;
        private System.Windows.Forms.Panel createdOnPanel;
        private TerraScan.UI.Controls.TerraScanTextBox createdOnTextBox;
        private System.Windows.Forms.Label createdOnLabel;
        private System.Windows.Forms.Panel descriptionPanel;
        private TerraScan.UI.Controls.TerraScanTextBox descriptionTextBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Panel whereConditionPanel;
        private TerraScan.UI.Controls.TerraScanTextBox whereConditionTextBox;
        private System.Windows.Forms.Label whereConditionLabel;
        private System.Windows.Forms.PictureBox newEditPictureBox;
        private System.Windows.Forms.PictureBox existingPictureBox;
        private System.Windows.Forms.Panel CommentLinePanel;
        private System.Windows.Forms.VScrollBar queryVScrollBar;
        private TerraScan.UI.Controls.TerraScanDataGridView queryUtilityGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn QueryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedOn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SavedQueryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn WhereCondnSql;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserWhereCondn;
        private System.Windows.Forms.MenuStrip CommentMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem NewMenu;
        private System.Windows.Forms.ToolStripMenuItem SaveMenu;
    }
}
