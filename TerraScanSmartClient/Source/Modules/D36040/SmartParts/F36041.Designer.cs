namespace D36040
{
    partial class F36041
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CropGridpictureBox = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CropValueSliceToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.CropGridviewPanel = new System.Windows.Forms.Panel();
            this.SelectAllCheckBox = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.RemoveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.terraScanTextBox2 = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CropGridVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.Footerpanel = new System.Windows.Forms.Panel();
            this.TotalAcresLabel = new System.Windows.Forms.Label();
            this.TotalValueLabel = new System.Windows.Forms.Label();
            this.ValueTotalOverrideTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.AcresTotalOverrideTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CropGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.CropID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValidStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CropValueSliceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CropCode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fruit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Planted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Adjust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Acre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Acres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCropConfigured = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalValueToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.AcresTotalToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.CropGridpictureBox)).BeginInit();
            this.CropGridviewPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.Footerpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CropGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CropGridpictureBox
            // 
            this.CropGridpictureBox.BackColor = System.Drawing.Color.White;
            this.CropGridpictureBox.Location = new System.Drawing.Point(761, 0);
            this.CropGridpictureBox.Name = "CropGridpictureBox";
            this.CropGridpictureBox.Size = new System.Drawing.Size(42, 178);
            this.CropGridpictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CropGridpictureBox.TabIndex = 124;
            this.CropGridpictureBox.TabStop = false;
            this.CropGridpictureBox.Click += new System.EventHandler(this.CropGridpictureBox_Click);
            this.CropGridpictureBox.MouseHover += new System.EventHandler(this.CropGridpictureBox_MouseHover);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(-10, 154);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(29, 27);
            this.panel2.TabIndex = 201;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // CropGridviewPanel
            // 
            this.CropGridviewPanel.BackColor = System.Drawing.Color.White;
            this.CropGridviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CropGridviewPanel.Controls.Add(this.SelectAllCheckBox);
            this.CropGridviewPanel.Controls.Add(this.panel3);
            this.CropGridviewPanel.Controls.Add(this.CropGridVerticalScroll);
            this.CropGridviewPanel.Controls.Add(this.Footerpanel);
            this.CropGridviewPanel.Controls.Add(this.panel2);
            this.CropGridviewPanel.Controls.Add(this.CropGridView);
            this.CropGridviewPanel.Location = new System.Drawing.Point(0, 0);
            this.CropGridviewPanel.Name = "CropGridviewPanel";
            this.CropGridviewPanel.Size = new System.Drawing.Size(768, 179);
            this.CropGridviewPanel.TabIndex = 204;
            this.CropGridviewPanel.TabStop = true;
            // 
            // SelectAllCheckBox
            // 
            this.SelectAllCheckBox.BackColor = System.Drawing.Color.Gray;
            this.SelectAllCheckBox.Location = new System.Drawing.Point(27, 46);
            this.SelectAllCheckBox.MaximumSize = new System.Drawing.Size(30, 30);
            this.SelectAllCheckBox.Name = "SelectAllCheckBox";
            this.SelectAllCheckBox.Size = new System.Drawing.Size(17, 15);
            this.SelectAllCheckBox.TabIndex = 205;
            this.SelectAllCheckBox.UseVisualStyleBackColor = false;
            this.SelectAllCheckBox.CheckedChanged += new System.EventHandler(this.SelectAllCheckBox_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.RemoveButton);
            this.panel3.Controls.Add(this.terraScanTextBox2);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(766, 42);
            this.panel3.TabIndex = 205;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gray;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(16, -3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(33, 46);
            this.panel5.TabIndex = 204;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // RemoveButton
            // 
            this.RemoveButton.ActualPermission = false;
            this.RemoveButton.ApplyDisableBehaviour = false;
            this.RemoveButton.AutoSize = true;
            this.RemoveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.RemoveButton.BorderColor = System.Drawing.Color.Wheat;
            this.RemoveButton.CommentPriority = false;
            this.RemoveButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.RemoveButton.EnableAutoPrint = false;
            this.RemoveButton.Enabled = false;
            this.RemoveButton.FilterStatus = false;
            this.RemoveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveButton.FocusRectangleEnabled = true;
            this.RemoveButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.RemoveButton.ImageSelected = false;
            this.RemoveButton.Location = new System.Drawing.Point(56, 5);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.NewPadding = 5;
            this.RemoveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.RemoveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.RemoveButton.Size = new System.Drawing.Size(110, 30);
            this.RemoveButton.StatusIndicator = false;
            this.RemoveButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RemoveButton.StatusOffText = null;
            this.RemoveButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.RemoveButton.StatusOnText = null;
            this.RemoveButton.TabIndex = 206;
            this.RemoveButton.TabStop = false;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = false;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // terraScanTextBox2
            // 
            this.terraScanTextBox2.AllowClick = true;
            this.terraScanTextBox2.AllowNegativeSign = false;
            this.terraScanTextBox2.ApplyCFGFormat = false;
            this.terraScanTextBox2.ApplyCurrencyFormat = true;
            this.terraScanTextBox2.ApplyFocusColor = true;
            this.terraScanTextBox2.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.terraScanTextBox2.ApplyNegativeStandard = false;
            this.terraScanTextBox2.ApplyParentFocusColor = true;
            this.terraScanTextBox2.ApplyTimeFormat = false;
            this.terraScanTextBox2.BackColor = System.Drawing.Color.Gray;
            this.terraScanTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.terraScanTextBox2.CFromatWihoutSymbol = false;
            this.terraScanTextBox2.CheckForEmpty = false;
            this.terraScanTextBox2.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.terraScanTextBox2.Digits = 6;
            this.terraScanTextBox2.EmptyDecimalValue = true;
            this.terraScanTextBox2.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.terraScanTextBox2.ForeColor = System.Drawing.Color.Black;
            this.terraScanTextBox2.IsEditable = false;
            this.terraScanTextBox2.IsQueryableFileld = true;
            this.terraScanTextBox2.Location = new System.Drawing.Point(587, 1);
            this.terraScanTextBox2.LockKeyPress = false;
            this.terraScanTextBox2.MaxLength = 20;
            this.terraScanTextBox2.Name = "terraScanTextBox2";
            this.terraScanTextBox2.PersistDefaultColor = false;
            this.terraScanTextBox2.Precision = 1;
            this.terraScanTextBox2.QueryingFileldName = "";
            this.terraScanTextBox2.SetColorFlag = false;
            this.terraScanTextBox2.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.terraScanTextBox2.Size = new System.Drawing.Size(78, 16);
            this.terraScanTextBox2.SpecialCharacter = "%";
            this.terraScanTextBox2.TabIndex = 182;
            this.terraScanTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.terraScanTextBox2.TextCustomFormat = "#,##0.00";
            this.terraScanTextBox2.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.terraScanTextBox2.Visible = false;
            this.terraScanTextBox2.WholeInteger = false;
            // 
            // CropGridVerticalScroll
            // 
            this.CropGridVerticalScroll.Enabled = false;
            this.CropGridVerticalScroll.Location = new System.Drawing.Point(748, 41);
            this.CropGridVerticalScroll.Name = "CropGridVerticalScroll";
            this.CropGridVerticalScroll.Size = new System.Drawing.Size(16, 116);
            this.CropGridVerticalScroll.TabIndex = 203;
            this.CropGridVerticalScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.CropGridVerticalScroll_Scroll);
            // 
            // Footerpanel
            // 
            this.Footerpanel.BackColor = System.Drawing.Color.Gray;
            this.Footerpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Footerpanel.Controls.Add(this.TotalAcresLabel);
            this.Footerpanel.Controls.Add(this.TotalValueLabel);
            this.Footerpanel.Controls.Add(this.ValueTotalOverrideTextBox);
            this.Footerpanel.Controls.Add(this.AcresTotalOverrideTextBox);
            this.Footerpanel.Location = new System.Drawing.Point(18, 158);
            this.Footerpanel.Name = "Footerpanel";
            this.Footerpanel.Size = new System.Drawing.Size(750, 20);
            this.Footerpanel.TabIndex = 202;
            // 
            // TotalAcresLabel
            // 
            this.TotalAcresLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(203)))), ((int)(((byte)(133)))));
            this.TotalAcresLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalAcresLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TotalAcresLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.TotalAcresLabel.ForeColor = System.Drawing.Color.Black;
            this.TotalAcresLabel.Location = new System.Drawing.Point(577, -2);
            this.TotalAcresLabel.Name = "TotalAcresLabel";
            this.TotalAcresLabel.Size = new System.Drawing.Size(82, 22);
            this.TotalAcresLabel.TabIndex = 180;
            this.TotalAcresLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TotalAcresLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.TotalAcresLabel_Paint);
            this.TotalAcresLabel.MouseHover += new System.EventHandler(this.TotalAcresLabel_MouseHover);
            // 
            // TotalValueLabel
            // 
            this.TotalValueLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(203)))), ((int)(((byte)(133)))));
            this.TotalValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalValueLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TotalValueLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.TotalValueLabel.ForeColor = System.Drawing.Color.Black;
            this.TotalValueLabel.Location = new System.Drawing.Point(650, -3);
            this.TotalValueLabel.Name = "TotalValueLabel";
            this.TotalValueLabel.Size = new System.Drawing.Size(80, 24);
            this.TotalValueLabel.TabIndex = 179;
            this.TotalValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TotalValueLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.TotalValueLabel_Paint);
            this.TotalValueLabel.MouseHover += new System.EventHandler(this.TotalValueLabel_MouseHover);
            // 
            // ValueTotalOverrideTextBox
            // 
            this.ValueTotalOverrideTextBox.AllowClick = true;
            this.ValueTotalOverrideTextBox.AllowNegativeSign = false;
            this.ValueTotalOverrideTextBox.ApplyCFGFormat = false;
            this.ValueTotalOverrideTextBox.ApplyCurrencyFormat = true;
            this.ValueTotalOverrideTextBox.ApplyFocusColor = true;
            this.ValueTotalOverrideTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.ValueTotalOverrideTextBox.ApplyNegativeStandard = false;
            this.ValueTotalOverrideTextBox.ApplyParentFocusColor = true;
            this.ValueTotalOverrideTextBox.ApplyTimeFormat = false;
            this.ValueTotalOverrideTextBox.BackColor = System.Drawing.Color.White;
            this.ValueTotalOverrideTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ValueTotalOverrideTextBox.CFromatWihoutSymbol = false;
            this.ValueTotalOverrideTextBox.CheckForEmpty = false;
            this.ValueTotalOverrideTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ValueTotalOverrideTextBox.Digits = 6;
            this.ValueTotalOverrideTextBox.EmptyDecimalValue = true;
            this.ValueTotalOverrideTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.ValueTotalOverrideTextBox.ForeColor = System.Drawing.Color.Black;
            this.ValueTotalOverrideTextBox.IsEditable = false;
            this.ValueTotalOverrideTextBox.IsQueryableFileld = true;
            this.ValueTotalOverrideTextBox.Location = new System.Drawing.Point(654, 2);
            this.ValueTotalOverrideTextBox.LockKeyPress = false;
            this.ValueTotalOverrideTextBox.MaxLength = 20;
            this.ValueTotalOverrideTextBox.Name = "ValueTotalOverrideTextBox";
            this.ValueTotalOverrideTextBox.PersistDefaultColor = false;
            this.ValueTotalOverrideTextBox.Precision = 2;
            this.ValueTotalOverrideTextBox.QueryingFileldName = "";
            this.ValueTotalOverrideTextBox.ReadOnly = true;
            this.ValueTotalOverrideTextBox.SetColorFlag = false;
            this.ValueTotalOverrideTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ValueTotalOverrideTextBox.Size = new System.Drawing.Size(75, 16);
            this.ValueTotalOverrideTextBox.SpecialCharacter = "%";
            this.ValueTotalOverrideTextBox.TabIndex = 181;
            this.ValueTotalOverrideTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ValueTotalOverrideTextBox.TextCustomFormat = "#,##0.00";
            this.ValueTotalOverrideTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.ValueTotalOverrideTextBox.Visible = false;
            this.ValueTotalOverrideTextBox.WholeInteger = false;
            // 
            // AcresTotalOverrideTextBox
            // 
            this.AcresTotalOverrideTextBox.AllowClick = true;
            this.AcresTotalOverrideTextBox.AllowNegativeSign = false;
            this.AcresTotalOverrideTextBox.ApplyCFGFormat = false;
            this.AcresTotalOverrideTextBox.ApplyCurrencyFormat = true;
            this.AcresTotalOverrideTextBox.ApplyFocusColor = true;
            this.AcresTotalOverrideTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.AcresTotalOverrideTextBox.ApplyNegativeStandard = false;
            this.AcresTotalOverrideTextBox.ApplyParentFocusColor = true;
            this.AcresTotalOverrideTextBox.ApplyTimeFormat = false;
            this.AcresTotalOverrideTextBox.BackColor = System.Drawing.Color.Gray;
            this.AcresTotalOverrideTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AcresTotalOverrideTextBox.CFromatWihoutSymbol = false;
            this.AcresTotalOverrideTextBox.CheckForEmpty = false;
            this.AcresTotalOverrideTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AcresTotalOverrideTextBox.Digits = 6;
            this.AcresTotalOverrideTextBox.EmptyDecimalValue = true;
            this.AcresTotalOverrideTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.AcresTotalOverrideTextBox.ForeColor = System.Drawing.Color.Black;
            this.AcresTotalOverrideTextBox.IsEditable = false;
            this.AcresTotalOverrideTextBox.IsQueryableFileld = true;
            this.AcresTotalOverrideTextBox.Location = new System.Drawing.Point(587, 1);
            this.AcresTotalOverrideTextBox.LockKeyPress = false;
            this.AcresTotalOverrideTextBox.MaxLength = 20;
            this.AcresTotalOverrideTextBox.Name = "AcresTotalOverrideTextBox";
            this.AcresTotalOverrideTextBox.PersistDefaultColor = false;
            this.AcresTotalOverrideTextBox.Precision = 1;
            this.AcresTotalOverrideTextBox.QueryingFileldName = "";
            this.AcresTotalOverrideTextBox.SetColorFlag = false;
            this.AcresTotalOverrideTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.AcresTotalOverrideTextBox.Size = new System.Drawing.Size(78, 16);
            this.AcresTotalOverrideTextBox.SpecialCharacter = "%";
            this.AcresTotalOverrideTextBox.TabIndex = 182;
            this.AcresTotalOverrideTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AcresTotalOverrideTextBox.TextCustomFormat = "#,##0.00";
            this.AcresTotalOverrideTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.AcresTotalOverrideTextBox.Visible = false;
            this.AcresTotalOverrideTextBox.WholeInteger = false;
            // 
            // CropGridView
            // 
            this.CropGridView.AllowCellClick = true;
            this.CropGridView.AllowDoubleClick = true;
            this.CropGridView.AllowEmptyRows = true;
            this.CropGridView.AllowEnterKey = false;
            this.CropGridView.AllowSorting = true;
            this.CropGridView.AllowUserToAddRows = false;
            this.CropGridView.AllowUserToDeleteRows = false;
            this.CropGridView.AllowUserToResizeColumns = false;
            this.CropGridView.AllowUserToResizeRows = false;
            this.CropGridView.ApplyStandardBehaviour = false;
            this.CropGridView.BackgroundColor = System.Drawing.Color.White;
            this.CropGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CropGridView.ClearCurrentCellOnLeave = true;
            this.CropGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CropGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CropGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.CropGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CropID,
            this.ValidStatus,
            this.CropValueSliceID,
            this.CropCode,
            this.Description,
            this.Fruit,
            this.Planted,
            this.Age,
            this.Adjust,
            this.Acre,
            this.Acres,
            this.Value,
            this.IsCropConfigured});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CropGridView.DefaultCellStyle = dataGridViewCellStyle11;
            this.CropGridView.DefaultRowIndex = 0;
            this.CropGridView.DeselectCurrentCell = false;
            this.CropGridView.DeselectSpecifiedRow = -1;
            this.CropGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.CropGridView.EnableBinding = true;
            this.CropGridView.EnableHeadersVisualStyles = false;
            this.CropGridView.GridColor = System.Drawing.Color.Black;
            this.CropGridView.GridContentSelected = false;
            this.CropGridView.IsEditableGrid = true;
            this.CropGridView.IsMultiSelect = false;
            this.CropGridView.IsSorted = false;
            this.CropGridView.Location = new System.Drawing.Point(-1, 42);
            this.CropGridView.MultiSelect = false;
            this.CropGridView.Name = "CropGridView";
            this.CropGridView.NumRowsVisible = 4;
            this.CropGridView.PrimaryKeyColumnName = "";
            this.CropGridView.RemainSortFields = false;
            this.CropGridView.RemoveDefaultSelection = true;
            this.CropGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CropGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.CropGridView.RowHeadersWidth = 20;
            this.CropGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.CropGridView.RowsDefaultCellStyle = dataGridViewCellStyle13;
            this.CropGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CropGridView.Size = new System.Drawing.Size(768, 113);
            this.CropGridView.TabIndex = 204;
            this.CropGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.CropGridView_CellBeginEdit);
            this.CropGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.CropGridView_RowEnter);
            this.CropGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CropGridView_ColumnHeaderMouseClick);
            this.CropGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.CropGridView_RowPrePaint);
            this.CropGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.CropGridView_CellFormatting);
            this.CropGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CropGridView_MouseUp);
            this.CropGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.CropGridView_CellEndEdit);
            this.CropGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CropGridView_CellClick);
            this.CropGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.CropGridView_EditingControlShowing);
            this.CropGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.CropGridView_CellEnter);
            this.CropGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.CropGridView_DataBindingComplete);
            this.CropGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CropGridView_RowHeaderMouseClick);
            this.CropGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CropGridView_CellContentClick);
            // 
            // CropID
            // 
            this.CropID.HeaderText = "CropID";
            this.CropID.Name = "CropID";
            this.CropID.Visible = false;
            // 
            // ValidStatus
            // 
            this.ValidStatus.HeaderText = "";
            this.ValidStatus.Name = "ValidStatus";
            this.ValidStatus.Width = 30;
            // 
            // CropValueSliceID
            // 
            this.CropValueSliceID.HeaderText = "CropValueSliceID";
            this.CropValueSliceID.Name = "CropValueSliceID";
            this.CropValueSliceID.Visible = false;
            // 
            // CropCode
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CropCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.CropCode.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.CropCode.DisplayStyleForCurrentCellOnly = true;
            this.CropCode.HeaderText = "Crop Code ";
            this.CropCode.Name = "CropCode";
            this.CropCode.Width = 89;
            // 
            // Description
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Description.DefaultCellStyle = dataGridViewCellStyle3;
            this.Description.HeaderText = "Description";
            this.Description.MaxInputLength = 50;
            this.Description.Name = "Description";
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Description.Width = 150;
            // 
            // Fruit
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Fruit.DefaultCellStyle = dataGridViewCellStyle4;
            this.Fruit.HeaderText = "Fruit";
            this.Fruit.MaxInputLength = 10;
            this.Fruit.Name = "Fruit";
            this.Fruit.ReadOnly = true;
            this.Fruit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Fruit.Width = 65;
            // 
            // Planted
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Planted.DefaultCellStyle = dataGridViewCellStyle5;
            this.Planted.HeaderText = "Planted";
            this.Planted.MaxInputLength = 4;
            this.Planted.Name = "Planted";
            this.Planted.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Planted.Width = 80;
            // 
            // Age
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Age.DefaultCellStyle = dataGridViewCellStyle6;
            this.Age.HeaderText = "Age";
            this.Age.MaxInputLength = 11;
            this.Age.Name = "Age";
            this.Age.ReadOnly = true;
            this.Age.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Age.Width = 45;
            // 
            // Adjust
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Adjust.DefaultCellStyle = dataGridViewCellStyle7;
            this.Adjust.HeaderText = "Adjust";
            this.Adjust.MaxInputLength = 10;
            this.Adjust.Name = "Adjust";
            this.Adjust.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Adjust.Width = 60;
            // 
            // Acre
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Acre.DefaultCellStyle = dataGridViewCellStyle8;
            this.Acre.HeaderText = "$/Acre";
            this.Acre.Name = "Acre";
            this.Acre.ReadOnly = true;
            this.Acre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Acre.Width = 60;
            // 
            // Acres
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Acres.DefaultCellStyle = dataGridViewCellStyle9;
            this.Acres.HeaderText = "Acres";
            this.Acres.MaxInputLength = 12;
            this.Acres.Name = "Acres";
            this.Acres.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Acres.Width = 80;
            // 
            // Value
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Value.DefaultCellStyle = dataGridViewCellStyle10;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Value.Width = 70;
            // 
            // IsCropConfigured
            // 
            this.IsCropConfigured.HeaderText = "IsCropConfigured";
            this.IsCropConfigured.Name = "IsCropConfigured";
            this.IsCropConfigured.Visible = false;
            // 
            // F36041
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.CropGridviewPanel);
            this.Controls.Add(this.CropGridpictureBox);
            this.Name = "F36041";
            this.Size = new System.Drawing.Size(804, 179);
            this.Tag = "36041";
            this.Load += new System.EventHandler(this.F36041_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CropGridpictureBox)).EndInit();
            this.CropGridviewPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.Footerpanel.ResumeLayout(false);
            this.Footerpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CropGridView)).EndInit();
            this.ResumeLayout(false);

        }

        

        #endregion

        private System.Windows.Forms.PictureBox CropGridpictureBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip CropValueSliceToolTip;
        private System.Windows.Forms.Panel CropGridviewPanel;
        private System.Windows.Forms.VScrollBar CropGridVerticalScroll;
        private TerraScan.UI.Controls.TerraScanDataGridView CropGridView;
        private System.Windows.Forms.ToolTip TotalValueToolTip;
        private System.Windows.Forms.ToolTip AcresTotalToolTip;
        private System.Windows.Forms.Panel panel3;
        private TerraScan.UI.Controls.TerraScanTextBox terraScanTextBox2;
        private System.Windows.Forms.Panel panel5;
        private TerraScan.UI.Controls.TerraScanButton RemoveButton;
        private System.Windows.Forms.CheckBox SelectAllCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn CropID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ValidStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn CropValueSliceID;
        private System.Windows.Forms.DataGridViewComboBoxColumn CropCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fruit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Planted;
        private System.Windows.Forms.DataGridViewTextBoxColumn Age;
        private System.Windows.Forms.DataGridViewTextBoxColumn Adjust;
        private System.Windows.Forms.DataGridViewTextBoxColumn Acre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Acres;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsCropConfigured;
        private System.Windows.Forms.Panel Footerpanel;
        private System.Windows.Forms.Label TotalAcresLabel;
        private System.Windows.Forms.Label TotalValueLabel;
        private TerraScan.UI.Controls.TerraScanTextBox ValueTotalOverrideTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox AcresTotalOverrideTextBox;
    }
}
