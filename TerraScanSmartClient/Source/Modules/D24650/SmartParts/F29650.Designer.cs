namespace D24650
{
    partial class F29650
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
            this.ExemptionPanel = new System.Windows.Forms.Panel();
            this.MaximumPanel = new System.Windows.Forms.Panel();
            this.MaximumTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.MaximumLabel = new System.Windows.Forms.Label();
            this.ReductionValuepanel = new System.Windows.Forms.Panel();
            this.ReductionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.ReductionValueLabel = new System.Windows.Forms.Label();
            this.LossPanel = new System.Windows.Forms.Panel();
            this.LossTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.LossLabel = new System.Windows.Forms.Label();
            this.ExemptionTypePanel = new System.Windows.Forms.Panel();
            this.ExemptionTypeComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.ExemptionTypeLabel = new System.Windows.Forms.Label();
            this.ExemptionPictureBox = new System.Windows.Forms.PictureBox();
            this.ExemptionToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ExemptionPanel.SuspendLayout();
            this.MaximumPanel.SuspendLayout();
            this.ReductionValuepanel.SuspendLayout();
            this.LossPanel.SuspendLayout();
            this.ExemptionTypePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExemptionPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ExemptionPanel
            // 
            this.ExemptionPanel.Controls.Add(this.MaximumPanel);
            this.ExemptionPanel.Controls.Add(this.ReductionValuepanel);
            this.ExemptionPanel.Controls.Add(this.LossPanel);
            this.ExemptionPanel.Controls.Add(this.ExemptionTypePanel);
            this.ExemptionPanel.Controls.Add(this.ExemptionPictureBox);
            this.ExemptionPanel.Location = new System.Drawing.Point(0, 0);
            this.ExemptionPanel.Name = "ExemptionPanel";
            this.ExemptionPanel.Size = new System.Drawing.Size(804, 42);
            this.ExemptionPanel.TabIndex = 0;
            // 
            // MaximumPanel
            // 
            this.MaximumPanel.BackColor = System.Drawing.Color.Transparent;
            this.MaximumPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaximumPanel.Controls.Add(this.MaximumTextBox);
            this.MaximumPanel.Controls.Add(this.MaximumLabel);
            this.MaximumPanel.Location = new System.Drawing.Point(140, 0);
            this.MaximumPanel.Name = "MaximumPanel";
            this.MaximumPanel.Size = new System.Drawing.Size(210, 40);
            this.MaximumPanel.TabIndex = 1;
            // 
            // MaximumTextBox
            // 
            this.MaximumTextBox.AllowClick = true;
            this.MaximumTextBox.AllowNegativeSign = false;
            this.MaximumTextBox.ApplyCFGFormat = false;
            this.MaximumTextBox.ApplyCurrencyFormat = true;
            this.MaximumTextBox.ApplyFocusColor = true;
            this.MaximumTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.MaximumTextBox.ApplyNegativeStandard = false;
            this.MaximumTextBox.ApplyParentFocusColor = true;
            this.MaximumTextBox.ApplyTimeFormat = false;
            this.MaximumTextBox.BackColor = System.Drawing.Color.White;
            this.MaximumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MaximumTextBox.CFromatWihoutSymbol = false;
            this.MaximumTextBox.CheckForEmpty = false;
            this.MaximumTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.MaximumTextBox.Digits = -1;
            this.MaximumTextBox.EmptyDecimalValue = false;
            this.MaximumTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.MaximumTextBox.ForeColor = System.Drawing.Color.Gray;
            this.MaximumTextBox.IsEditable = true;
            this.MaximumTextBox.IsQueryableFileld = true;
            this.MaximumTextBox.Location = new System.Drawing.Point(8, 16);
            this.MaximumTextBox.LockKeyPress = true;
            this.MaximumTextBox.MaxLength = 20;
            this.MaximumTextBox.Name = "MaximumTextBox";
            this.MaximumTextBox.PersistDefaultColor = false;
            this.MaximumTextBox.Precision = 2;
            this.MaximumTextBox.QueryingFileldName = "";
            this.MaximumTextBox.ReadOnly = true;
            this.MaximumTextBox.SetColorFlag = false;
            this.MaximumTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.MaximumTextBox.Size = new System.Drawing.Size(190, 16);
            this.MaximumTextBox.SpecialCharacter = "%";
            this.MaximumTextBox.TabIndex = 1;
            this.MaximumTextBox.TabStop = false;
            this.MaximumTextBox.Tag = "";
            this.MaximumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MaximumTextBox.TextCustomFormat = "$ #,##0";
            this.MaximumTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.MaximumTextBox.WholeInteger = true;
            // 
            // MaximumLabel
            // 
            this.MaximumLabel.AutoSize = true;
            this.MaximumLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.MaximumLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.MaximumLabel.Location = new System.Drawing.Point(0, 0);
            this.MaximumLabel.Name = "MaximumLabel";
            this.MaximumLabel.Size = new System.Drawing.Size(64, 14);
            this.MaximumLabel.TabIndex = 0;
            this.MaximumLabel.Text = "Maximum:";
            // 
            // ReductionValuepanel
            // 
            this.ReductionValuepanel.BackColor = System.Drawing.Color.Transparent;
            this.ReductionValuepanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReductionValuepanel.Controls.Add(this.ReductionTextBox);
            this.ReductionValuepanel.Controls.Add(this.ReductionValueLabel);
            this.ReductionValuepanel.Location = new System.Drawing.Point(558, 0);
            this.ReductionValuepanel.Name = "ReductionValuepanel";
            this.ReductionValuepanel.Size = new System.Drawing.Size(211, 40);
            this.ReductionValuepanel.TabIndex = 3;
            // 
            // ReductionTextBox
            // 
            this.ReductionTextBox.AllowClick = true;
            this.ReductionTextBox.AllowNegativeSign = false;
            this.ReductionTextBox.ApplyCFGFormat = false;
            this.ReductionTextBox.ApplyCurrencyFormat = true;
            this.ReductionTextBox.ApplyFocusColor = true;
            this.ReductionTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.ReductionTextBox.ApplyNegativeStandard = false;
            this.ReductionTextBox.ApplyParentFocusColor = true;
            this.ReductionTextBox.ApplyTimeFormat = false;
            this.ReductionTextBox.BackColor = System.Drawing.Color.White;
            this.ReductionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReductionTextBox.CFromatWihoutSymbol = false;
            this.ReductionTextBox.CheckForEmpty = false;
            this.ReductionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ReductionTextBox.Digits = -1;
            this.ReductionTextBox.EmptyDecimalValue = false;
            this.ReductionTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.ReductionTextBox.ForeColor = System.Drawing.Color.Gray;
            this.ReductionTextBox.IsEditable = true;
            this.ReductionTextBox.IsQueryableFileld = true;
            this.ReductionTextBox.Location = new System.Drawing.Point(8, 16);
            this.ReductionTextBox.LockKeyPress = true;
            this.ReductionTextBox.MaxLength = 15;
            this.ReductionTextBox.Name = "ReductionTextBox";
            this.ReductionTextBox.PersistDefaultColor = false;
            this.ReductionTextBox.Precision = 2;
            this.ReductionTextBox.QueryingFileldName = "";
            this.ReductionTextBox.ReadOnly = true;
            this.ReductionTextBox.SetColorFlag = false;
            this.ReductionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ReductionTextBox.Size = new System.Drawing.Size(190, 16);
            this.ReductionTextBox.SpecialCharacter = "%";
            this.ReductionTextBox.TabIndex = 1;
            this.ReductionTextBox.TabStop = false;
            this.ReductionTextBox.Tag = "";
            this.ReductionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReductionTextBox.TextCustomFormat = "$ #,##0";
            this.ReductionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.ReductionTextBox.WholeInteger = true;
            // 
            // ReductionValueLabel
            // 
            this.ReductionValueLabel.AutoSize = true;
            this.ReductionValueLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.ReductionValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ReductionValueLabel.Location = new System.Drawing.Point(0, 0);
            this.ReductionValueLabel.Name = "ReductionValueLabel";
            this.ReductionValueLabel.Size = new System.Drawing.Size(117, 14);
            this.ReductionValueLabel.TabIndex = 0;
            this.ReductionValueLabel.Text = "Reduction of Value:";
            // 
            // LossPanel
            // 
            this.LossPanel.BackColor = System.Drawing.Color.White;
            this.LossPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LossPanel.Controls.Add(this.LossTextBox);
            this.LossPanel.Controls.Add(this.LossLabel);
            this.LossPanel.Location = new System.Drawing.Point(349, 0);
            this.LossPanel.Name = "LossPanel";
            this.LossPanel.Size = new System.Drawing.Size(210, 40);
            this.LossPanel.TabIndex = 2;
            this.LossPanel.TabStop = true;
            // 
            // LossTextBox
            // 
            this.LossTextBox.AllowClick = true;
            this.LossTextBox.AllowNegativeSign = false;
            this.LossTextBox.ApplyCFGFormat = false;
            this.LossTextBox.ApplyCurrencyFormat = true;
            this.LossTextBox.ApplyFocusColor = true;
            this.LossTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.LossTextBox.ApplyNegativeStandard = false;
            this.LossTextBox.ApplyParentFocusColor = true;
            this.LossTextBox.ApplyTimeFormat = false;
            this.LossTextBox.BackColor = System.Drawing.Color.White;
            this.LossTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LossTextBox.CFromatWihoutSymbol = false;
            this.LossTextBox.CheckForEmpty = false;
            this.LossTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.LossTextBox.Digits = -1;
            this.LossTextBox.EmptyDecimalValue = false;
            this.LossTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.LossTextBox.ForeColor = System.Drawing.Color.Black;
            this.LossTextBox.IsEditable = true;
            this.LossTextBox.IsQueryableFileld = true;
            this.LossTextBox.Location = new System.Drawing.Point(9, 16);
            this.LossTextBox.LockKeyPress = false;
            this.LossTextBox.MaxLength = 15;
            this.LossTextBox.Name = "LossTextBox";
            this.LossTextBox.PersistDefaultColor = false;
            this.LossTextBox.Precision = 2;
            this.LossTextBox.QueryingFileldName = "";
            this.LossTextBox.SetColorFlag = false;
            this.LossTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.LossTextBox.Size = new System.Drawing.Size(190, 16);
            this.LossTextBox.SpecialCharacter = "%";
            this.LossTextBox.TabIndex = 1;
            this.LossTextBox.Tag = "";
            this.LossTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.LossTextBox.TextCustomFormat = "$ #,##0";
            this.LossTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.LossTextBox.WholeInteger = true;
            this.LossTextBox.TextChanged += new System.EventHandler(this.EnableEditButtonInMasterForm);
            this.LossTextBox.Leave += new System.EventHandler(this.LossTextBox_Leave);
            // 
            // LossLabel
            // 
            this.LossLabel.AutoSize = true;
            this.LossLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LossLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LossLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.LossLabel.Location = new System.Drawing.Point(0, 0);
            this.LossLabel.Name = "LossLabel";
            this.LossLabel.Size = new System.Drawing.Size(38, 14);
            this.LossLabel.TabIndex = 0;
            this.LossLabel.Text = "Loss:";
            // 
            // ExemptionTypePanel
            // 
            this.ExemptionTypePanel.BackColor = System.Drawing.Color.Transparent;
            this.ExemptionTypePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExemptionTypePanel.Controls.Add(this.ExemptionTypeComboBox);
            this.ExemptionTypePanel.Controls.Add(this.ExemptionTypeLabel);
            this.ExemptionTypePanel.Location = new System.Drawing.Point(0, 0);
            this.ExemptionTypePanel.Name = "ExemptionTypePanel";
            this.ExemptionTypePanel.Size = new System.Drawing.Size(141, 40);
            this.ExemptionTypePanel.TabIndex = 0;
            // 
            // ExemptionTypeComboBox
            // 
            this.ExemptionTypeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ExemptionTypeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ExemptionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExemptionTypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExemptionTypeComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExemptionTypeComboBox.ForeColor = System.Drawing.Color.Black;
            this.ExemptionTypeComboBox.FormattingEnabled = true;
            this.ExemptionTypeComboBox.Location = new System.Drawing.Point(8, 12);
            this.ExemptionTypeComboBox.Name = "ExemptionTypeComboBox";
            this.ExemptionTypeComboBox.Size = new System.Drawing.Size(128, 24);
            this.ExemptionTypeComboBox.TabIndex = 1;
            this.ExemptionTypeComboBox.Tag = "";
            this.ExemptionTypeComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.ExemptionTypeComboBox_Validating);
            this.ExemptionTypeComboBox.SelectionChangeCommitted += new System.EventHandler(this.ExemptionTypeComboBox_SelectionChangeCommitted);
            this.ExemptionTypeComboBox.TextChanged += new System.EventHandler(this.ExemptionTypeComboBox_TextChanged);
            // 
            // ExemptionTypeLabel
            // 
            this.ExemptionTypeLabel.AutoSize = true;
            this.ExemptionTypeLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.ExemptionTypeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ExemptionTypeLabel.Location = new System.Drawing.Point(0, -1);
            this.ExemptionTypeLabel.Name = "ExemptionTypeLabel";
            this.ExemptionTypeLabel.Size = new System.Drawing.Size(98, 14);
            this.ExemptionTypeLabel.TabIndex = 0;
            this.ExemptionTypeLabel.Text = "Exemption Type:";
            // 
            // ExemptionPictureBox
            // 
            this.ExemptionPictureBox.Location = new System.Drawing.Point(762, 0);
            this.ExemptionPictureBox.Name = "ExemptionPictureBox";
            this.ExemptionPictureBox.Size = new System.Drawing.Size(42, 40);
            this.ExemptionPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ExemptionPictureBox.TabIndex = 120;
            this.ExemptionPictureBox.TabStop = false;
            this.ExemptionPictureBox.Click += new System.EventHandler(this.ExemptionPictureBox_Click);
            this.ExemptionPictureBox.MouseEnter += new System.EventHandler(this.ExemptionPictureBox_MouseEnter);
            // 
            // F29650
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ExemptionPanel);
            this.Name = "F29650";
            this.Size = new System.Drawing.Size(804, 41);
            this.Tag = "29650";
            this.Load += new System.EventHandler(this.F29650_Load);
            this.ExemptionPanel.ResumeLayout(false);
            this.MaximumPanel.ResumeLayout(false);
            this.MaximumPanel.PerformLayout();
            this.ReductionValuepanel.ResumeLayout(false);
            this.ReductionValuepanel.PerformLayout();
            this.LossPanel.ResumeLayout(false);
            this.LossPanel.PerformLayout();
            this.ExemptionTypePanel.ResumeLayout(false);
            this.ExemptionTypePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExemptionPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ExemptionPanel;
        private System.Windows.Forms.Panel ExemptionTypePanel;
        private System.Windows.Forms.Label ExemptionTypeLabel;
        private System.Windows.Forms.Panel LossPanel;
        private System.Windows.Forms.Label LossLabel;
        private System.Windows.Forms.PictureBox ExemptionPictureBox;
        private System.Windows.Forms.Panel ReductionValuepanel;
        private TerraScan.UI.Controls.TerraScanTextBox ReductionTextBox;
        private System.Windows.Forms.Label ReductionValueLabel;
        private System.Windows.Forms.Panel MaximumPanel;
        private TerraScan.UI.Controls.TerraScanTextBox MaximumTextBox;
        private System.Windows.Forms.Label MaximumLabel;
        private TerraScan.UI.Controls.TerraScanTextBox LossTextBox;
        private TerraScan.UI.Controls.TerraScanComboBox ExemptionTypeComboBox;
        private System.Windows.Forms.ToolTip ExemptionToolTip;

    }
}
