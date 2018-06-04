namespace D9030
{
	partial class F9042
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9042));
            this.NamePanel = new System.Windows.Forms.Panel();
            this.TemplateNameCombo = new TerraScan.UI.Controls.TerraScanComboBox();
            this.QueryViewNameLabel = new System.Windows.Forms.Label();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.FormNumberLabel = new System.Windows.Forms.Label();
            this.DefinationPanel = new System.Windows.Forms.Panel();
            this.DefinitionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.PurposePanel = new System.Windows.Forms.Panel();
            this.PurposeTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Linepanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.DescriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.TitleTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.NewDatasetButton = new TerraScan.UI.Controls.TerraScanButton();
            this.HelpLink = new System.Windows.Forms.LinkLabel();
            this.NamePanel.SuspendLayout();
            this.DefinationPanel.SuspendLayout();
            this.PurposePanel.SuspendLayout();
            this.DescriptionPanel.SuspendLayout();
            this.TitlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // NamePanel
            // 
            this.NamePanel.BackColor = System.Drawing.Color.White;
            this.NamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NamePanel.Controls.Add(this.TemplateNameCombo);
            this.NamePanel.Controls.Add(this.QueryViewNameLabel);
            this.NamePanel.Location = new System.Drawing.Point(4, 4);
            this.NamePanel.Name = "NamePanel";
            this.NamePanel.Size = new System.Drawing.Size(621, 54);
            this.NamePanel.TabIndex = 0;
            this.NamePanel.TabStop = true;
            // 
            // TemplateNameCombo
            // 
            this.TemplateNameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TemplateNameCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TemplateNameCombo.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.TemplateNameCombo.FormattingEnabled = true;
            this.TemplateNameCombo.Location = new System.Drawing.Point(12, 17);
            this.TemplateNameCombo.Name = "TemplateNameCombo";
            this.TemplateNameCombo.Size = new System.Drawing.Size(595, 24);
            this.TemplateNameCombo.TabIndex = 6;
            this.TemplateNameCombo.SelectedIndexChanged += new System.EventHandler(this.TemplateNameCombo_SelectedIndexChanged);
            // 
            // QueryViewNameLabel
            // 
            this.QueryViewNameLabel.AutoSize = true;
            this.QueryViewNameLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.QueryViewNameLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QueryViewNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.QueryViewNameLabel.Location = new System.Drawing.Point(1, 0);
            this.QueryViewNameLabel.Name = "QueryViewNameLabel";
            this.QueryViewNameLabel.Size = new System.Drawing.Size(96, 14);
            this.QueryViewNameLabel.TabIndex = 0;
            this.QueryViewNameLabel.Text = "Template Name:";
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(5, 485);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(621, 2);
            this.FormLinePanel.TabIndex = 115;
            // 
            // FormNumberLabel
            // 
            this.FormNumberLabel.AutoSize = true;
            this.FormNumberLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormNumberLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.FormNumberLabel.Location = new System.Drawing.Point(4, 491);
            this.FormNumberLabel.Name = "FormNumberLabel";
            this.FormNumberLabel.Size = new System.Drawing.Size(35, 15);
            this.FormNumberLabel.TabIndex = 116;
            this.FormNumberLabel.Text = "9042";
            // 
            // DefinationPanel
            // 
            this.DefinationPanel.BackColor = System.Drawing.Color.White;
            this.DefinationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DefinationPanel.Controls.Add(this.DefinitionTextBox);
            this.DefinationPanel.Controls.Add(this.DescriptionLabel);
            this.DefinationPanel.Location = new System.Drawing.Point(4, 51);
            this.DefinationPanel.Name = "DefinationPanel";
            this.DefinationPanel.Size = new System.Drawing.Size(621, 95);
            this.DefinationPanel.TabIndex = 2;
            this.DefinationPanel.TabStop = true;
            // 
            // DefinitionTextBox
            // 
            this.DefinitionTextBox.AllowClick = true;
            this.DefinitionTextBox.AllowNegativeSign = false;
            this.DefinitionTextBox.ApplyCFGFormat = false;
            this.DefinitionTextBox.ApplyCurrencyFormat = false;
            this.DefinitionTextBox.ApplyFocusColor = true;
            this.DefinitionTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.DefinitionTextBox.ApplyNegativeStandard = true;
            this.DefinitionTextBox.ApplyParentFocusColor = true;
            this.DefinitionTextBox.ApplyTimeFormat = false;
            this.DefinitionTextBox.BackColor = System.Drawing.Color.White;
            this.DefinitionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DefinitionTextBox.CFromatWihoutSymbol = false;
            this.DefinitionTextBox.CheckForEmpty = false;
            this.DefinitionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DefinitionTextBox.Digits = -1;
            this.DefinitionTextBox.EmptyDecimalValue = false;
            this.DefinitionTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.DefinitionTextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.DefinitionTextBox.IsEditable = false;
            this.DefinitionTextBox.IsQueryableFileld = false;
            this.DefinitionTextBox.Location = new System.Drawing.Point(12, 18);
            this.DefinitionTextBox.LockKeyPress = false;
            this.DefinitionTextBox.MaxLength = 0;
            this.DefinitionTextBox.Multiline = true;
            this.DefinitionTextBox.Name = "DefinitionTextBox";
            this.DefinitionTextBox.PersistDefaultColor = false;
            this.DefinitionTextBox.Precision = 2;
            this.DefinitionTextBox.QueryingFileldName = "";
            this.DefinitionTextBox.ReadOnly = true;
            this.DefinitionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DefinitionTextBox.SetFocusColor = System.Drawing.Color.White;
            this.DefinitionTextBox.Size = new System.Drawing.Size(594, 68);
            this.DefinitionTextBox.SpecialCharacter = "%";
            this.DefinitionTextBox.TabIndex = 3;
            this.DefinitionTextBox.TextCustomFormat = "$#,##0.00";
            this.DefinitionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DefinitionTextBox.WholeInteger = false;
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DescriptionLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DescriptionLabel.Location = new System.Drawing.Point(1, 0);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(117, 14);
            this.DescriptionLabel.TabIndex = 0;
            this.DescriptionLabel.Text = "Template Definition:";
            // 
            // PurposePanel
            // 
            this.PurposePanel.BackColor = System.Drawing.Color.White;
            this.PurposePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PurposePanel.Controls.Add(this.PurposeTextBox);
            this.PurposePanel.Controls.Add(this.label2);
            this.PurposePanel.Location = new System.Drawing.Point(4, 145);
            this.PurposePanel.Name = "PurposePanel";
            this.PurposePanel.Size = new System.Drawing.Size(621, 82);
            this.PurposePanel.TabIndex = 4;
            this.PurposePanel.TabStop = true;
            // 
            // PurposeTextBox
            // 
            this.PurposeTextBox.AllowClick = true;
            this.PurposeTextBox.AllowNegativeSign = false;
            this.PurposeTextBox.ApplyCFGFormat = false;
            this.PurposeTextBox.ApplyCurrencyFormat = false;
            this.PurposeTextBox.ApplyFocusColor = true;
            this.PurposeTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.PurposeTextBox.ApplyNegativeStandard = true;
            this.PurposeTextBox.ApplyParentFocusColor = true;
            this.PurposeTextBox.ApplyTimeFormat = false;
            this.PurposeTextBox.BackColor = System.Drawing.Color.White;
            this.PurposeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PurposeTextBox.CFromatWihoutSymbol = false;
            this.PurposeTextBox.CheckForEmpty = false;
            this.PurposeTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PurposeTextBox.Digits = -1;
            this.PurposeTextBox.EmptyDecimalValue = false;
            this.PurposeTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.PurposeTextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.PurposeTextBox.IsEditable = false;
            this.PurposeTextBox.IsQueryableFileld = false;
            this.PurposeTextBox.Location = new System.Drawing.Point(12, 18);
            this.PurposeTextBox.LockKeyPress = false;
            this.PurposeTextBox.MaxLength = 0;
            this.PurposeTextBox.Multiline = true;
            this.PurposeTextBox.Name = "PurposeTextBox";
            this.PurposeTextBox.PersistDefaultColor = false;
            this.PurposeTextBox.Precision = 2;
            this.PurposeTextBox.QueryingFileldName = "";
            this.PurposeTextBox.ReadOnly = true;
            this.PurposeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PurposeTextBox.SetFocusColor = System.Drawing.Color.White;
            this.PurposeTextBox.Size = new System.Drawing.Size(595, 56);
            this.PurposeTextBox.SpecialCharacter = "%";
            this.PurposeTextBox.TabIndex = 5;
            this.PurposeTextBox.TextCustomFormat = "$#,##0.00";
            this.PurposeTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.PurposeTextBox.WholeInteger = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label2.Location = new System.Drawing.Point(1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Template Purpose:";
            // 
            // Linepanel
            // 
            this.Linepanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.Linepanel.Location = new System.Drawing.Point(4, 227);
            this.Linepanel.Name = "Linepanel";
            this.Linepanel.Size = new System.Drawing.Size(622, 14);
            this.Linepanel.TabIndex = 121;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkGray;
            this.label4.Location = new System.Drawing.Point(4, 466);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 15);
            this.label4.TabIndex = 122;
            this.label4.Text = "Query View:  Current Appraisal Year";
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.BackColor = System.Drawing.Color.White;
            this.DescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionPanel.Controls.Add(this.DescriptionTextBox);
            this.DescriptionPanel.Controls.Add(this.label5);
            this.DescriptionPanel.Location = new System.Drawing.Point(4, 286);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(622, 141);
            this.DescriptionPanel.TabIndex = 8;
            this.DescriptionPanel.TabStop = true;
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.AllowClick = true;
            this.DescriptionTextBox.AllowNegativeSign = false;
            this.DescriptionTextBox.ApplyCFGFormat = false;
            this.DescriptionTextBox.ApplyCurrencyFormat = false;
            this.DescriptionTextBox.ApplyFocusColor = true;
            this.DescriptionTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.DescriptionTextBox.ApplyNegativeStandard = true;
            this.DescriptionTextBox.ApplyParentFocusColor = true;
            this.DescriptionTextBox.ApplyTimeFormat = false;
            this.DescriptionTextBox.BackColor = System.Drawing.Color.White;
            this.DescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DescriptionTextBox.CFromatWihoutSymbol = false;
            this.DescriptionTextBox.CheckForEmpty = false;
            this.DescriptionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DescriptionTextBox.Digits = -1;
            this.DescriptionTextBox.EmptyDecimalValue = false;
            this.DescriptionTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.DescriptionTextBox.ForeColor = System.Drawing.Color.Black;
            this.DescriptionTextBox.IsEditable = true;
            this.DescriptionTextBox.IsQueryableFileld = false;
            this.DescriptionTextBox.Location = new System.Drawing.Point(12, 18);
            this.DescriptionTextBox.LockKeyPress = false;
            this.DescriptionTextBox.MaxLength = 0;
            this.DescriptionTextBox.Multiline = true;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.PersistDefaultColor = false;
            this.DescriptionTextBox.Precision = 2;
            this.DescriptionTextBox.QueryingFileldName = "";
            this.DescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DescriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescriptionTextBox.Size = new System.Drawing.Size(595, 112);
            this.DescriptionTextBox.SpecialCharacter = "%";
            this.DescriptionTextBox.TabIndex = 9;
            this.DescriptionTextBox.TextCustomFormat = "$#,##0.00";
            this.DescriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescriptionTextBox.WholeInteger = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label5.Location = new System.Drawing.Point(1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "Description:";
            // 
            // TitlePanel
            // 
            this.TitlePanel.BackColor = System.Drawing.Color.White;
            this.TitlePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TitlePanel.Controls.Add(this.TitleTextBox);
            this.TitlePanel.Controls.Add(this.label6);
            this.TitlePanel.Location = new System.Drawing.Point(4, 241);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(622, 46);
            this.TitlePanel.TabIndex = 6;
            this.TitlePanel.TabStop = true;
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.AllowClick = true;
            this.TitleTextBox.AllowNegativeSign = false;
            this.TitleTextBox.ApplyCFGFormat = false;
            this.TitleTextBox.ApplyCurrencyFormat = false;
            this.TitleTextBox.ApplyFocusColor = true;
            this.TitleTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.TitleTextBox.ApplyNegativeStandard = true;
            this.TitleTextBox.ApplyParentFocusColor = true;
            this.TitleTextBox.ApplyTimeFormat = false;
            this.TitleTextBox.BackColor = System.Drawing.Color.White;
            this.TitleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TitleTextBox.CFromatWihoutSymbol = false;
            this.TitleTextBox.CheckForEmpty = false;
            this.TitleTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TitleTextBox.Digits = -1;
            this.TitleTextBox.EmptyDecimalValue = false;
            this.TitleTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.TitleTextBox.ForeColor = System.Drawing.Color.Black;
            this.TitleTextBox.IsEditable = true;
            this.TitleTextBox.IsQueryableFileld = false;
            this.TitleTextBox.Location = new System.Drawing.Point(17, 22);
            this.TitleTextBox.LockKeyPress = false;
            this.TitleTextBox.MaxLength = 50;
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.PersistDefaultColor = false;
            this.TitleTextBox.Precision = 2;
            this.TitleTextBox.QueryingFileldName = "";
            this.TitleTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.TitleTextBox.Size = new System.Drawing.Size(584, 16);
            this.TitleTextBox.SpecialCharacter = "%";
            this.TitleTextBox.TabIndex = 7;
            this.TitleTextBox.TextCustomFormat = "$#,##0.00";
            this.TitleTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.TitleTextBox.WholeInteger = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label6.Location = new System.Drawing.Point(1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "Title:";
            // 
            // NewDatasetButton
            // 
            this.NewDatasetButton.ActualPermission = false;
            this.NewDatasetButton.ApplyDisableBehaviour = false;
            this.NewDatasetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.NewDatasetButton.BorderColor = System.Drawing.Color.Wheat;
            this.NewDatasetButton.CommentPriority = false;
            this.NewDatasetButton.EnableAutoPrint = false;
            this.NewDatasetButton.FilterStatus = false;
            this.NewDatasetButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.NewDatasetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewDatasetButton.FocusRectangleEnabled = true;
            this.NewDatasetButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewDatasetButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.NewDatasetButton.ImageSelected = false;
            this.NewDatasetButton.Location = new System.Drawing.Point(4, 433);
            this.NewDatasetButton.Name = "NewDatasetButton";
            this.NewDatasetButton.NewPadding = 5;
            this.NewDatasetButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Open;
            this.NewDatasetButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.None;
            this.NewDatasetButton.Size = new System.Drawing.Size(621, 30);
            this.NewDatasetButton.StatusIndicator = false;
            this.NewDatasetButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NewDatasetButton.StatusOffText = null;
            this.NewDatasetButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.NewDatasetButton.StatusOnText = null;
            this.NewDatasetButton.TabIndex = 10;
            this.NewDatasetButton.Text = "Create new dataset";
            this.NewDatasetButton.UseVisualStyleBackColor = false;
            this.NewDatasetButton.Click += new System.EventHandler(this.NewDatasetButton_Click);
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpLink.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLink.Location = new System.Drawing.Point(566, 490);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(56, 15);
            this.HelpLink.TabIndex = 11;
            this.HelpLink.TabStop = true;
            this.HelpLink.Text = "Help";
            this.HelpLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // F9042
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(631, 510);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.NewDatasetButton);
            this.Controls.Add(this.DescriptionPanel);
            this.Controls.Add(this.TitlePanel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Linepanel);
            this.Controls.Add(this.PurposePanel);
            this.Controls.Add(this.DefinationPanel);
            this.Controls.Add(this.FormNumberLabel);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.NamePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F9042";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "9042";
            this.Text = "Analytics Template Selection";
            this.Load += new System.EventHandler(this.F9042_Load);
            this.NamePanel.ResumeLayout(false);
            this.NamePanel.PerformLayout();
            this.DefinationPanel.ResumeLayout(false);
            this.DefinationPanel.PerformLayout();
            this.PurposePanel.ResumeLayout(false);
            this.PurposePanel.PerformLayout();
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Panel NamePanel;
        private System.Windows.Forms.Label QueryViewNameLabel;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label FormNumberLabel;
        private System.Windows.Forms.Panel DefinationPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DefinitionTextBox;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Panel PurposePanel;
        private TerraScan.UI.Controls.TerraScanTextBox PurposeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel Linepanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel DescriptionPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DescriptionTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel TitlePanel;
        private TerraScan.UI.Controls.TerraScanTextBox TitleTextBox;
        private System.Windows.Forms.Label label6;
        private TerraScan.UI.Controls.TerraScanButton NewDatasetButton;
        private System.Windows.Forms.LinkLabel HelpLink;
        private TerraScan.UI.Controls.TerraScanComboBox TemplateNameCombo;
	}
}