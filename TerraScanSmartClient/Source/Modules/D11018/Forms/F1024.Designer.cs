namespace D11018
{
    partial class F1024
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1024));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DistrictControlsPanel = new System.Windows.Forms.Panel();
            this.LeviesPanel = new System.Windows.Forms.Panel();
            this.LevisComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.LeviesLabel = new System.Windows.Forms.Label();
            this.DistrictPanel = new System.Windows.Forms.Panel();
            this.Districbutton = new System.Windows.Forms.Button();
            this.DistrictTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DistrictLabel = new System.Windows.Forms.Label();
            this.AmountPanel = new System.Windows.Forms.Panel();
            this.AmountTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.AmountLabel = new System.Windows.Forms.Label();
            this.ReplaceDistrictButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CancelMiscTemplateButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DistrictLinePanel = new System.Windows.Forms.Panel();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.SaveTemplateMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SaveMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SubFundPanel = new System.Windows.Forms.Panel();
            this.MiscTemplateVscrollBar = new System.Windows.Forms.VScrollBar();
            this.DistrictListDataGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.AppendDistrictButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SubFundID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Checked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SubFund = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsVoterApproved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DistrictControlsPanel.SuspendLayout();
            this.LeviesPanel.SuspendLayout();
            this.DistrictPanel.SuspendLayout();
            this.AmountPanel.SuspendLayout();
            this.SaveTemplateMenuStrip.SuspendLayout();
            this.SubFundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DistrictListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DistrictControlsPanel
            // 
            this.DistrictControlsPanel.Controls.Add(this.LeviesPanel);
            this.DistrictControlsPanel.Controls.Add(this.DistrictPanel);
            this.DistrictControlsPanel.Controls.Add(this.AmountPanel);
            this.DistrictControlsPanel.Location = new System.Drawing.Point(10, 10);
            this.DistrictControlsPanel.Name = "DistrictControlsPanel";
            this.DistrictControlsPanel.Size = new System.Drawing.Size(457, 81);
            this.DistrictControlsPanel.TabIndex = 1;
            this.DistrictControlsPanel.TabStop = true;
            // 
            // LeviesPanel
            // 
            this.LeviesPanel.BackColor = System.Drawing.Color.Transparent;
            this.LeviesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LeviesPanel.Controls.Add(this.LevisComboBox);
            this.LeviesPanel.Controls.Add(this.LeviesLabel);
            this.LeviesPanel.Location = new System.Drawing.Point(219, 0);
            this.LeviesPanel.Name = "LeviesPanel";
            this.LeviesPanel.Size = new System.Drawing.Size(238, 42);
            this.LeviesPanel.TabIndex = 1;
            // 
            // LevisComboBox
            // 
            this.LevisComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LevisComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LevisComboBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.LevisComboBox.FormattingEnabled = true;
            this.LevisComboBox.Location = new System.Drawing.Point(16, 13);
            this.LevisComboBox.Name = "LevisComboBox";
            this.LevisComboBox.Size = new System.Drawing.Size(208, 24);
            this.LevisComboBox.TabIndex = 6;
            this.LevisComboBox.SelectedIndexChanged += new System.EventHandler(this.LevisComboBox_SelectedIndexChanged);
            // 
            // LeviesLabel
            // 
            this.LeviesLabel.AutoSize = true;
            this.LeviesLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.LeviesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.LeviesLabel.Location = new System.Drawing.Point(-1, -1);
            this.LeviesLabel.Name = "LeviesLabel";
            this.LeviesLabel.Size = new System.Drawing.Size(85, 14);
            this.LeviesLabel.TabIndex = 0;
            this.LeviesLabel.Text = "Levies to Use:";
            // 
            // DistrictPanel
            // 
            this.DistrictPanel.BackColor = System.Drawing.Color.Transparent;
            this.DistrictPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DistrictPanel.Controls.Add(this.Districbutton);
            this.DistrictPanel.Controls.Add(this.DistrictTextBox);
            this.DistrictPanel.Controls.Add(this.DistrictLabel);
            this.DistrictPanel.Location = new System.Drawing.Point(0, 41);
            this.DistrictPanel.Name = "DistrictPanel";
            this.DistrictPanel.Size = new System.Drawing.Size(457, 40);
            this.DistrictPanel.TabIndex = 2;
            // 
            // Districbutton
            // 
            this.Districbutton.BackColor = System.Drawing.Color.White;
            this.Districbutton.FlatAppearance.BorderSize = 0;
            this.Districbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Districbutton.Image = ((System.Drawing.Image)(resources.GetObject("Districbutton.Image")));
            this.Districbutton.Location = new System.Drawing.Point(430, 10);
            this.Districbutton.Name = "Districbutton";
            this.Districbutton.Size = new System.Drawing.Size(22, 23);
            this.Districbutton.TabIndex = 3;
            this.Districbutton.UseVisualStyleBackColor = false;
            this.Districbutton.Click += new System.EventHandler(this.Districbutton_Click);
            // 
            // DistrictTextBox
            // 
            this.DistrictTextBox.AllowClick = true;
            this.DistrictTextBox.AllowNegativeSign = false;
            this.DistrictTextBox.ApplyCFGFormat = false;
            this.DistrictTextBox.ApplyCurrencyFormat = false;
            this.DistrictTextBox.ApplyFocusColor = true;
            this.DistrictTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.DistrictTextBox.ApplyNegativeStandard = true;
            this.DistrictTextBox.ApplyParentFocusColor = true;
            this.DistrictTextBox.ApplyTimeFormat = false;
            this.DistrictTextBox.BackColor = System.Drawing.Color.White;
            this.DistrictTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DistrictTextBox.CFromatWihoutSymbol = false;
            this.DistrictTextBox.CheckForEmpty = false;
            this.DistrictTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DistrictTextBox.Digits = -1;
            this.DistrictTextBox.EmptyDecimalValue = false;
            this.DistrictTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.DistrictTextBox.ForeColor = System.Drawing.Color.Black;
            this.DistrictTextBox.IsEditable = false;
            this.DistrictTextBox.IsQueryableFileld = true;
            this.DistrictTextBox.Location = new System.Drawing.Point(8, 16);
            this.DistrictTextBox.LockKeyPress = false;
            this.DistrictTextBox.MaxLength = 250;
            this.DistrictTextBox.Name = "DistrictTextBox";
            this.DistrictTextBox.PersistDefaultColor = false;
            this.DistrictTextBox.Precision = 2;
            this.DistrictTextBox.QueryingFileldName = "";
            this.DistrictTextBox.SetColorFlag = false;
            this.DistrictTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DistrictTextBox.Size = new System.Drawing.Size(416, 16);
            this.DistrictTextBox.SpecialCharacter = "%";
            this.DistrictTextBox.TabIndex = 0;
            this.DistrictTextBox.Tag = "";
            this.DistrictTextBox.TextCustomFormat = "$#,##0.00";
            this.DistrictTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DistrictTextBox.WholeInteger = false;
            // 
            // DistrictLabel
            // 
            this.DistrictLabel.AutoSize = true;
            this.DistrictLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.DistrictLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DistrictLabel.Location = new System.Drawing.Point(-1, -1);
            this.DistrictLabel.Name = "DistrictLabel";
            this.DistrictLabel.Size = new System.Drawing.Size(49, 14);
            this.DistrictLabel.TabIndex = 0;
            this.DistrictLabel.Text = "District:";
            // 
            // AmountPanel
            // 
            this.AmountPanel.BackColor = System.Drawing.Color.Transparent;
            this.AmountPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AmountPanel.Controls.Add(this.AmountTextBox);
            this.AmountPanel.Controls.Add(this.AmountLabel);
            this.AmountPanel.Location = new System.Drawing.Point(0, 0);
            this.AmountPanel.Name = "AmountPanel";
            this.AmountPanel.Size = new System.Drawing.Size(222, 42);
            this.AmountPanel.TabIndex = 0;
            // 
            // AmountTextBox
            // 
            this.AmountTextBox.AllowClick = true;
            this.AmountTextBox.AllowNegativeSign = true;
            this.AmountTextBox.ApplyCFGFormat = true;
            this.AmountTextBox.ApplyCurrencyFormat = true;
            this.AmountTextBox.ApplyFocusColor = true;
            this.AmountTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.AmountTextBox.ApplyNegativeStandard = true;
            this.AmountTextBox.ApplyParentFocusColor = true;
            this.AmountTextBox.ApplyTimeFormat = false;
            this.AmountTextBox.BackColor = System.Drawing.Color.White;
            this.AmountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AmountTextBox.CFromatWihoutSymbol = true;
            this.AmountTextBox.CheckForEmpty = false;
            this.AmountTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AmountTextBox.Digits = -1;
            this.AmountTextBox.EmptyDecimalValue = true;
            this.AmountTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.AmountTextBox.ForeColor = System.Drawing.Color.Black;
            this.AmountTextBox.IsEditable = true;
            this.AmountTextBox.IsQueryableFileld = true;
            this.AmountTextBox.Location = new System.Drawing.Point(8, 18);
            this.AmountTextBox.LockKeyPress = false;
            this.AmountTextBox.MaxLength = 15;
            this.AmountTextBox.Name = "AmountTextBox";
            this.AmountTextBox.PersistDefaultColor = false;
            this.AmountTextBox.Precision = 2;
            this.AmountTextBox.QueryingFileldName = "";
            this.AmountTextBox.SetColorFlag = false;
            this.AmountTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.AmountTextBox.Size = new System.Drawing.Size(197, 16);
            this.AmountTextBox.SpecialCharacter = "%";
            this.AmountTextBox.TabIndex = 0;
            this.AmountTextBox.Tag = "";
            this.AmountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AmountTextBox.TextCustomFormat = "$#,##0.00";
            this.AmountTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.AmountTextBox.WholeInteger = false;
            // 
            // AmountLabel
            // 
            this.AmountLabel.AutoSize = true;
            this.AmountLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.AmountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.AmountLabel.Location = new System.Drawing.Point(-1, -1);
            this.AmountLabel.Name = "AmountLabel";
            this.AmountLabel.Size = new System.Drawing.Size(54, 14);
            this.AmountLabel.TabIndex = 0;
            this.AmountLabel.Text = "Amount:";
            // 
            // ReplaceDistrictButton
            // 
            this.ReplaceDistrictButton.ActualPermission = false;
            this.ReplaceDistrictButton.ApplyDisableBehaviour = false;
            this.ReplaceDistrictButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ReplaceDistrictButton.BorderColor = System.Drawing.Color.Wheat;
            this.ReplaceDistrictButton.CommentPriority = false;
            this.ReplaceDistrictButton.EnableAutoPrint = false;
            this.ReplaceDistrictButton.FilterStatus = false;
            this.ReplaceDistrictButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ReplaceDistrictButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReplaceDistrictButton.FocusRectangleEnabled = true;
            this.ReplaceDistrictButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReplaceDistrictButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ReplaceDistrictButton.ImageSelected = false;
            this.ReplaceDistrictButton.Location = new System.Drawing.Point(10, 277);
            this.ReplaceDistrictButton.Name = "ReplaceDistrictButton";
            this.ReplaceDistrictButton.NewPadding = 5;
            this.ReplaceDistrictButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.ReplaceDistrictButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ReplaceDistrictButton.Size = new System.Drawing.Size(98, 28);
            this.ReplaceDistrictButton.StatusIndicator = false;
            this.ReplaceDistrictButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ReplaceDistrictButton.StatusOffText = null;
            this.ReplaceDistrictButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ReplaceDistrictButton.StatusOnText = null;
            this.ReplaceDistrictButton.TabIndex = 174;
            this.ReplaceDistrictButton.TabStop = false;
            this.ReplaceDistrictButton.Text = "Replace";
            this.ReplaceDistrictButton.UseVisualStyleBackColor = false;
            this.ReplaceDistrictButton.Click += new System.EventHandler(this.SaveMiscTemplateButton_Click);
            // 
            // CancelMiscTemplateButton
            // 
            this.CancelMiscTemplateButton.ActualPermission = false;
            this.CancelMiscTemplateButton.ApplyDisableBehaviour = false;
            this.CancelMiscTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelMiscTemplateButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelMiscTemplateButton.CommentPriority = false;
            this.CancelMiscTemplateButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelMiscTemplateButton.EnableAutoPrint = false;
            this.CancelMiscTemplateButton.FilterStatus = false;
            this.CancelMiscTemplateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelMiscTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelMiscTemplateButton.FocusRectangleEnabled = true;
            this.CancelMiscTemplateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelMiscTemplateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelMiscTemplateButton.ImageSelected = false;
            this.CancelMiscTemplateButton.Location = new System.Drawing.Point(368, 276);
            this.CancelMiscTemplateButton.Name = "CancelMiscTemplateButton";
            this.CancelMiscTemplateButton.NewPadding = 5;
            this.CancelMiscTemplateButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CancelMiscTemplateButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelMiscTemplateButton.Size = new System.Drawing.Size(98, 28);
            this.CancelMiscTemplateButton.StatusIndicator = false;
            this.CancelMiscTemplateButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelMiscTemplateButton.StatusOffText = null;
            this.CancelMiscTemplateButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelMiscTemplateButton.StatusOnText = null;
            this.CancelMiscTemplateButton.TabIndex = 176;
            this.CancelMiscTemplateButton.TabStop = false;
            this.CancelMiscTemplateButton.Text = "Cancel";
            this.CancelMiscTemplateButton.UseVisualStyleBackColor = false;
            this.CancelMiscTemplateButton.Click += new System.EventHandler(this.CancelMiscTemplateButton_Click);
            // 
            // DistrictLinePanel
            // 
            this.DistrictLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.DistrictLinePanel.Location = new System.Drawing.Point(10, 311);
            this.DistrictLinePanel.Name = "DistrictLinePanel";
            this.DistrictLinePanel.Size = new System.Drawing.Size(457, 2);
            this.DistrictLinePanel.TabIndex = 176;
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(11, 316);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(35, 15);
            this.FormIDLabel.TabIndex = 177;
            this.FormIDLabel.Text = "1024";
            // 
            // SaveTemplateMenuStrip
            // 
            this.SaveTemplateMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveMenuToolStripMenuItem});
            this.SaveTemplateMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.SaveTemplateMenuStrip.Name = "SaveTemplateMenuStrip";
            this.SaveTemplateMenuStrip.Size = new System.Drawing.Size(381, 24);
            this.SaveTemplateMenuStrip.TabIndex = 179;
            this.SaveTemplateMenuStrip.Text = "menuStrip";
            this.SaveTemplateMenuStrip.Visible = false;
            // 
            // SaveMenuToolStripMenuItem
            // 
            this.SaveMenuToolStripMenuItem.Name = "SaveMenuToolStripMenuItem";
            this.SaveMenuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveMenuToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.SaveMenuToolStripMenuItem.Text = "SaveMenu";
            this.SaveMenuToolStripMenuItem.Visible = false;
            // 
            // SubFundPanel
            // 
            this.SubFundPanel.BackColor = System.Drawing.Color.Transparent;
            this.SubFundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SubFundPanel.Controls.Add(this.MiscTemplateVscrollBar);
            this.SubFundPanel.Controls.Add(this.DistrictListDataGridView);
            this.SubFundPanel.Location = new System.Drawing.Point(10, 90);
            this.SubFundPanel.Name = "SubFundPanel";
            this.SubFundPanel.Size = new System.Drawing.Size(457, 174);
            this.SubFundPanel.TabIndex = 181;
            // 
            // MiscTemplateVscrollBar
            // 
            this.MiscTemplateVscrollBar.Enabled = false;
            this.MiscTemplateVscrollBar.Location = new System.Drawing.Point(440, 0);
            this.MiscTemplateVscrollBar.Name = "MiscTemplateVscrollBar";
            this.MiscTemplateVscrollBar.Size = new System.Drawing.Size(16, 173);
            this.MiscTemplateVscrollBar.TabIndex = 182;
            // 
            // DistrictListDataGridView
            // 
            this.DistrictListDataGridView.AllowCellClick = true;
            this.DistrictListDataGridView.AllowDoubleClick = true;
            this.DistrictListDataGridView.AllowEmptyRows = true;
            this.DistrictListDataGridView.AllowEnterKey = false;
            this.DistrictListDataGridView.AllowSorting = true;
            this.DistrictListDataGridView.AllowUserToAddRows = false;
            this.DistrictListDataGridView.AllowUserToDeleteRows = false;
            this.DistrictListDataGridView.AllowUserToResizeColumns = false;
            this.DistrictListDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.DistrictListDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DistrictListDataGridView.ApplyStandardBehaviour = false;
            this.DistrictListDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.DistrictListDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DistrictListDataGridView.ClearCurrentCellOnLeave = false;
            this.DistrictListDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DistrictListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DistrictListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DistrictListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SubFundID,
            this.Checked,
            this.SubFund,
            this.Description,
            this.Rate,
            this.IsVoterApproved});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DistrictListDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.DistrictListDataGridView.DefaultRowIndex = -1;
            this.DistrictListDataGridView.DeselectCurrentCell = false;
            this.DistrictListDataGridView.DeselectSpecifiedRow = -1;
            this.DistrictListDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DistrictListDataGridView.EnableBinding = false;
            this.DistrictListDataGridView.EnableHeadersVisualStyles = false;
            this.DistrictListDataGridView.GridColor = System.Drawing.Color.Black;
            this.DistrictListDataGridView.GridContentSelected = false;
            this.DistrictListDataGridView.IsEditableGrid = false;
            this.DistrictListDataGridView.IsMultiSelect = false;
            this.DistrictListDataGridView.IsSorted = false;
            this.DistrictListDataGridView.Location = new System.Drawing.Point(-1, 0);
            this.DistrictListDataGridView.MultiSelect = false;
            this.DistrictListDataGridView.Name = "DistrictListDataGridView";
            this.DistrictListDataGridView.NumRowsVisible = 7;
            this.DistrictListDataGridView.PrimaryKeyColumnName = "";
            this.DistrictListDataGridView.RemainSortFields = false;
            this.DistrictListDataGridView.RemoveDefaultSelection = false;
            this.DistrictListDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DistrictListDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.DistrictListDataGridView.RowHeadersWidth = 20;
            this.DistrictListDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DistrictListDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DistrictListDataGridView.Size = new System.Drawing.Size(441, 173);
            this.DistrictListDataGridView.StandardTab = true;
            this.DistrictListDataGridView.TabIndex = 181;
            // 
            // AppendDistrictButton
            // 
            this.AppendDistrictButton.ActualPermission = false;
            this.AppendDistrictButton.ApplyDisableBehaviour = false;
            this.AppendDistrictButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AppendDistrictButton.BorderColor = System.Drawing.Color.Wheat;
            this.AppendDistrictButton.CommentPriority = false;
            this.AppendDistrictButton.EnableAutoPrint = false;
            this.AppendDistrictButton.FilterStatus = false;
            this.AppendDistrictButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AppendDistrictButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AppendDistrictButton.FocusRectangleEnabled = true;
            this.AppendDistrictButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppendDistrictButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AppendDistrictButton.ImageSelected = false;
            this.AppendDistrictButton.Location = new System.Drawing.Point(189, 277);
            this.AppendDistrictButton.Name = "AppendDistrictButton";
            this.AppendDistrictButton.NewPadding = 5;
            this.AppendDistrictButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.AppendDistrictButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AppendDistrictButton.Size = new System.Drawing.Size(98, 28);
            this.AppendDistrictButton.StatusIndicator = false;
            this.AppendDistrictButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AppendDistrictButton.StatusOffText = null;
            this.AppendDistrictButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AppendDistrictButton.StatusOnText = null;
            this.AppendDistrictButton.TabIndex = 175;
            this.AppendDistrictButton.TabStop = false;
            this.AppendDistrictButton.Text = "Append";
            this.AppendDistrictButton.UseVisualStyleBackColor = false;
            this.AppendDistrictButton.Click += new System.EventHandler(this.AppendDistrictButton_Click);
            // 
            // SubFundID
            // 
            this.SubFundID.HeaderText = "SubFundID";
            this.SubFundID.Name = "SubFundID";
            this.SubFundID.ReadOnly = true;
            this.SubFundID.Visible = false;
            // 
            // Checked
            // 
            this.Checked.DataPropertyName = "Checked";
            this.Checked.HeaderText = "";
            this.Checked.Name = "Checked";
            this.Checked.Width = 30;
            // 
            // SubFund
            // 
            this.SubFund.HeaderText = "SubFund";
            this.SubFund.Name = "SubFund";
            this.SubFund.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 160;
            // 
            // Rate
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle3;
            this.Rate.HeaderText = "Levy Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 150;
            // 
            // IsVoterApproved
            // 
            this.IsVoterApproved.HeaderText = "IsVoterApproved";
            this.IsVoterApproved.Name = "IsVoterApproved";
            this.IsVoterApproved.ReadOnly = true;
            this.IsVoterApproved.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsVoterApproved.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsVoterApproved.Visible = false;
            this.IsVoterApproved.Width = 20;
            // 
            // F1024
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(472, 335);
            this.Controls.Add(this.AppendDistrictButton);
            this.Controls.Add(this.SubFundPanel);
            this.Controls.Add(this.SaveTemplateMenuStrip);
            this.Controls.Add(this.ReplaceDistrictButton);
            this.Controls.Add(this.CancelMiscTemplateButton);
            this.Controls.Add(this.DistrictLinePanel);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.DistrictControlsPanel);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 360);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(380, 360);
            this.Name = "F1024";
            this.ParentFormId = 1024;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - Distribution by District";
            this.Load += new System.EventHandler(this.F1024_Load);
            this.DistrictControlsPanel.ResumeLayout(false);
            this.LeviesPanel.ResumeLayout(false);
            this.LeviesPanel.PerformLayout();
            this.DistrictPanel.ResumeLayout(false);
            this.DistrictPanel.PerformLayout();
            this.AmountPanel.ResumeLayout(false);
            this.AmountPanel.PerformLayout();
            this.SaveTemplateMenuStrip.ResumeLayout(false);
            this.SaveTemplateMenuStrip.PerformLayout();
            this.SubFundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DistrictListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DistrictControlsPanel;
        private System.Windows.Forms.Panel DistrictPanel;
        private System.Windows.Forms.Label DistrictLabel;
        private System.Windows.Forms.Panel AmountPanel;
        private TerraScan.UI.Controls.TerraScanTextBox AmountTextBox;
        private System.Windows.Forms.Label AmountLabel;
        private TerraScan.UI.Controls.TerraScanButton ReplaceDistrictButton;
        private TerraScan.UI.Controls.TerraScanButton CancelMiscTemplateButton;
        private System.Windows.Forms.Panel DistrictLinePanel;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.MenuStrip SaveTemplateMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SaveMenuToolStripMenuItem;
        private System.Windows.Forms.Panel LeviesPanel;
        private System.Windows.Forms.Label LeviesLabel;
        private TerraScan.UI.Controls.TerraScanComboBox LevisComboBox;
        private TerraScan.UI.Controls.TerraScanTextBox DistrictTextBox;
        private System.Windows.Forms.Button Districbutton;
        private System.Windows.Forms.Panel SubFundPanel;
        private TerraScan.UI.Controls.TerraScanDataGridView DistrictListDataGridView;
        private TerraScan.UI.Controls.TerraScanButton AppendDistrictButton;
        private System.Windows.Forms.VScrollBar MiscTemplateVscrollBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubFundID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Checked;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubFund;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsVoterApproved;
    }
}