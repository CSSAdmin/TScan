namespace D35000
{
    partial class F35001
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
            this.ValueSliceHeaderPanel = new System.Windows.Forms.Panel();
            this.NewConstpanel = new System.Windows.Forms.Panel();
            this.NewConstValueTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.NewConstlabel = new System.Windows.Forms.Label();
            this.WillRollPanel = new System.Windows.Forms.Panel();
            this.WillRollCombo = new TerraScan.UI.Controls.TerraScanComboBox();
            this.WillRollLabel = new System.Windows.Forms.Label();
            this.WillValuePanel = new System.Windows.Forms.Panel();
            this.WillValueCombo = new TerraScan.UI.Controls.TerraScanComboBox();
            this.WillValueLabel = new System.Windows.Forms.Label();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.DescriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.SliceTypePanel = new System.Windows.Forms.Panel();
            this.SliceTypeTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.SliceTypeLabel = new System.Windows.Forms.Label();
            this.ValueSliceHeaderPictureBox = new System.Windows.Forms.PictureBox();
            this.ValueSliceHeadeToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ValueSliceHeaderPanel.SuspendLayout();
            this.NewConstpanel.SuspendLayout();
            this.WillRollPanel.SuspendLayout();
            this.WillValuePanel.SuspendLayout();
            this.DescriptionPanel.SuspendLayout();
            this.SliceTypePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ValueSliceHeaderPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ValueSliceHeaderPanel
            // 
            this.ValueSliceHeaderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ValueSliceHeaderPanel.Controls.Add(this.NewConstpanel);
            this.ValueSliceHeaderPanel.Controls.Add(this.WillRollPanel);
            this.ValueSliceHeaderPanel.Controls.Add(this.WillValuePanel);
            this.ValueSliceHeaderPanel.Controls.Add(this.DescriptionPanel);
            this.ValueSliceHeaderPanel.Controls.Add(this.SliceTypePanel);
            this.ValueSliceHeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.ValueSliceHeaderPanel.Name = "ValueSliceHeaderPanel";
            this.ValueSliceHeaderPanel.Size = new System.Drawing.Size(767, 40);
            this.ValueSliceHeaderPanel.TabIndex = 6;
            // 
            // NewConstpanel
            // 
            this.NewConstpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewConstpanel.Controls.Add(this.NewConstValueTextBox);
            this.NewConstpanel.Controls.Add(this.NewConstlabel);
            this.NewConstpanel.Location = new System.Drawing.Point(490, -1);
            this.NewConstpanel.Name = "NewConstpanel";
            this.NewConstpanel.Size = new System.Drawing.Size(108, 40);
            this.NewConstpanel.TabIndex = 4;
            this.NewConstpanel.TabStop = true;
            // 
            // NewConstValueTextBox
            // 
            this.NewConstValueTextBox.AllowClick = true;
            this.NewConstValueTextBox.AllowNegativeSign = true;
            this.NewConstValueTextBox.ApplyCFGFormat =false  ;
            this.NewConstValueTextBox.ApplyCurrencyFormat = false;
            this.NewConstValueTextBox.ApplyFocusColor = true;
            this.NewConstValueTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty ;
            this.NewConstValueTextBox.ApplyNegativeStandard = true;
            this.NewConstValueTextBox.ApplyParentFocusColor = true;
            this.NewConstValueTextBox.ApplyTimeFormat = false;
            this.NewConstValueTextBox.BackColor = System.Drawing.Color.White;
            this.NewConstValueTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NewConstValueTextBox.CFromatWihoutSymbol = false;
            this.NewConstValueTextBox.CheckForEmpty = false;
            this.NewConstValueTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NewConstValueTextBox.Digits = -1;
            this.NewConstValueTextBox.EmptyDecimalValue = true;
            this.NewConstValueTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.NewConstValueTextBox.ForeColor = System.Drawing.Color.Black;
            this.NewConstValueTextBox.IsEditable = false;
            this.NewConstValueTextBox.IsQueryableFileld = true;
            this.NewConstValueTextBox.Location = new System.Drawing.Point(9, 18);
            this.NewConstValueTextBox.LockKeyPress = false;
            this.NewConstValueTextBox.MaxLength = 20;
            this.NewConstValueTextBox.Name = "NewConstValueTextBox";
            this.NewConstValueTextBox.PersistDefaultColor = false;
            this.NewConstValueTextBox.Precision = 2;
            this.NewConstValueTextBox.QueryingFileldName = "";
            this.NewConstValueTextBox.SetColorFlag = false;
            this.NewConstValueTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.NewConstValueTextBox.Size = new System.Drawing.Size(92, 16);
            this.NewConstValueTextBox.SpecialCharacter = "%";
            this.NewConstValueTextBox.TabIndex = 5;
            this.NewConstValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NewConstValueTextBox.TextCustomFormat = "#,##0.00";
            this.NewConstValueTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Money  ;
            this.NewConstValueTextBox.WholeInteger = false;
            //this.NewConstValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(NewConstValueTextBox_KeyPress);
            this.NewConstValueTextBox.TextChanged +=new System.EventHandler(this.TextBox_TextChanged);
            this.NewConstValueTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NewConstValueTextBox_KeyDown);
            // 
            // NewConstlabel
            // 
            this.NewConstlabel.AutoSize = true;
            this.NewConstlabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.NewConstlabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewConstlabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.NewConstlabel.Location = new System.Drawing.Point(0, 0);
            this.NewConstlabel.Name = "NewConstlabel";
            this.NewConstlabel.Size = new System.Drawing.Size(104, 14);
            this.NewConstlabel.TabIndex = 63;
            this.NewConstlabel.Text = "New Const Value:";
            // 
            // WillRollPanel
            // 
            this.WillRollPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WillRollPanel.Controls.Add(this.WillRollCombo);
            this.WillRollPanel.Controls.Add(this.WillRollLabel);
            this.WillRollPanel.Location = new System.Drawing.Point(681, -1);
            this.WillRollPanel.Name = "WillRollPanel";
            this.WillRollPanel.Size = new System.Drawing.Size(85, 40);
            this.WillRollPanel.TabIndex = 8;
            this.WillRollPanel.TabStop = true;
            // 
            // WillRollCombo
            // 
            this.WillRollCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WillRollCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WillRollCombo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WillRollCombo.ForeColor = System.Drawing.Color.Black;
            this.WillRollCombo.FormattingEnabled = true;
            this.WillRollCombo.Location = new System.Drawing.Point(11, 13);
            this.WillRollCombo.Name = "WillRollCombo";
            this.WillRollCombo.Size = new System.Drawing.Size(70, 24);
            this.WillRollCombo.TabIndex = 9;
            this.WillRollCombo.Tag = "";
            this.WillRollCombo.SelectionChangeCommitted += new System.EventHandler(this.WillRollCombo_SelectionChangeCommitted);
            this.WillRollCombo.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // WillRollLabel
            // 
            this.WillRollLabel.AutoSize = true;
            this.WillRollLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.WillRollLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WillRollLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.WillRollLabel.Location = new System.Drawing.Point(1, 0);
            this.WillRollLabel.Name = "WillRollLabel";
            this.WillRollLabel.Size = new System.Drawing.Size(52, 14);
            this.WillRollLabel.TabIndex = 63;
            this.WillRollLabel.Text = "Will Roll:";
            // 
            // WillValuePanel
            // 
            this.WillValuePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WillValuePanel.Controls.Add(this.WillValueCombo);
            this.WillValuePanel.Controls.Add(this.WillValueLabel);
            this.WillValuePanel.Location = new System.Drawing.Point(597, -1);
            this.WillValuePanel.Name = "WillValuePanel";
            this.WillValuePanel.Size = new System.Drawing.Size(85, 40);
            this.WillValuePanel.TabIndex = 6;
            this.WillValuePanel.TabStop = true;
            // 
            // WillValueCombo
            // 
            this.WillValueCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WillValueCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WillValueCombo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WillValueCombo.ForeColor = System.Drawing.Color.Black;
            this.WillValueCombo.FormattingEnabled = true;
            this.WillValueCombo.Location = new System.Drawing.Point(11, 13);
            this.WillValueCombo.Name = "WillValueCombo";
            this.WillValueCombo.Size = new System.Drawing.Size(70, 24);
            this.WillValueCombo.TabIndex = 7;
            this.WillValueCombo.Tag = "";
            this.WillValueCombo.SelectedIndexChanged += new System.EventHandler(this.WillValueCombo_SelectedIndexChanged);
            // 
            // WillValueLabel
            // 
            this.WillValueLabel.AutoSize = true;
            this.WillValueLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.WillValueLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WillValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.WillValueLabel.Location = new System.Drawing.Point(0, 0);
            this.WillValueLabel.Name = "WillValueLabel";
            this.WillValueLabel.Size = new System.Drawing.Size(63, 14);
            this.WillValueLabel.TabIndex = 63;
            this.WillValueLabel.Text = "Will Value:";
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionPanel.Controls.Add(this.DescriptionTextBox);
            this.DescriptionPanel.Controls.Add(this.DescriptionLabel);
            this.DescriptionPanel.Location = new System.Drawing.Point(269, -1);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(222, 40);
            this.DescriptionPanel.TabIndex = 2;
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
            this.DescriptionTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.DescriptionTextBox.ForeColor = System.Drawing.Color.Black;
            this.DescriptionTextBox.IsEditable = true;
            this.DescriptionTextBox.IsQueryableFileld = false;
            this.DescriptionTextBox.Location = new System.Drawing.Point(14, 19);
            this.DescriptionTextBox.LockKeyPress = false;
            this.DescriptionTextBox.MaxLength = 50;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.PersistDefaultColor = false;
            this.DescriptionTextBox.Precision = 2;
            this.DescriptionTextBox.QueryingFileldName = "";
            this.DescriptionTextBox.SetColorFlag = false;
            this.DescriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescriptionTextBox.Size = new System.Drawing.Size(202, 16);
            this.DescriptionTextBox.SpecialCharacter = "%";
            this.DescriptionTextBox.TabIndex = 3;
            this.DescriptionTextBox.Tag = "";
            this.DescriptionTextBox.TextCustomFormat = "$#,##0.00";
            this.DescriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescriptionTextBox.WholeInteger = false;
            this.DescriptionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.DescriptionTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NewConstValueTextBox_KeyDown);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DescriptionLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DescriptionLabel.Location = new System.Drawing.Point(0, 0);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(73, 14);
            this.DescriptionLabel.TabIndex = 63;
            this.DescriptionLabel.Text = "Description:";
            // 
            // SliceTypePanel
            // 
            this.SliceTypePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SliceTypePanel.Controls.Add(this.SliceTypeTextBox);
            this.SliceTypePanel.Controls.Add(this.SliceTypeLabel);
            this.SliceTypePanel.Location = new System.Drawing.Point(-1, -1);
            this.SliceTypePanel.Name = "SliceTypePanel";
            this.SliceTypePanel.Size = new System.Drawing.Size(271, 40);
            this.SliceTypePanel.TabIndex = 0;
            // 
            // SliceTypeTextBox
            // 
            this.SliceTypeTextBox.AllowClick = true;
            this.SliceTypeTextBox.AllowNegativeSign = false;
            this.SliceTypeTextBox.ApplyCFGFormat = false;
            this.SliceTypeTextBox.ApplyCurrencyFormat = false;
            this.SliceTypeTextBox.ApplyFocusColor = true;
            this.SliceTypeTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.SliceTypeTextBox.ApplyNegativeStandard = true;
            this.SliceTypeTextBox.ApplyParentFocusColor = true;
            this.SliceTypeTextBox.ApplyTimeFormat = false;
            this.SliceTypeTextBox.BackColor = System.Drawing.Color.White;
            this.SliceTypeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SliceTypeTextBox.CFromatWihoutSymbol = false;
            this.SliceTypeTextBox.CheckForEmpty = false;
            this.SliceTypeTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SliceTypeTextBox.Digits = -1;
            this.SliceTypeTextBox.EmptyDecimalValue = false;
            this.SliceTypeTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.SliceTypeTextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.SliceTypeTextBox.IsEditable = true;
            this.SliceTypeTextBox.IsQueryableFileld = false;
            this.SliceTypeTextBox.Location = new System.Drawing.Point(12, 19);
            this.SliceTypeTextBox.LockKeyPress = true;
            this.SliceTypeTextBox.MaxLength = 50;
            this.SliceTypeTextBox.Name = "SliceTypeTextBox";
            this.SliceTypeTextBox.PersistDefaultColor = false;
            this.SliceTypeTextBox.Precision = 2;
            this.SliceTypeTextBox.QueryingFileldName = "";
            this.SliceTypeTextBox.ReadOnly = true;
            this.SliceTypeTextBox.SetColorFlag = false;
            this.SliceTypeTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.SliceTypeTextBox.Size = new System.Drawing.Size(252, 16);
            this.SliceTypeTextBox.SpecialCharacter = "%";
            this.SliceTypeTextBox.TabIndex = 1;
            this.SliceTypeTextBox.TabStop = false;
            this.SliceTypeTextBox.Tag = "";
            this.SliceTypeTextBox.TextCustomFormat = "$#,##0.00";
            this.SliceTypeTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.SliceTypeTextBox.WholeInteger = false;
            // 
            // SliceTypeLabel
            // 
            this.SliceTypeLabel.AutoSize = true;
            this.SliceTypeLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SliceTypeLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SliceTypeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.SliceTypeLabel.Location = new System.Drawing.Point(0, 0);
            this.SliceTypeLabel.Name = "SliceTypeLabel";
            this.SliceTypeLabel.Size = new System.Drawing.Size(66, 14);
            this.SliceTypeLabel.TabIndex = 63;
            this.SliceTypeLabel.Text = "Slice Type:";
            // 
            // ValueSliceHeaderPictureBox
            // 
            this.ValueSliceHeaderPictureBox.Location = new System.Drawing.Point(760, 0);
            this.ValueSliceHeaderPictureBox.Name = "ValueSliceHeaderPictureBox";
            this.ValueSliceHeaderPictureBox.Size = new System.Drawing.Size(42, 40);
            this.ValueSliceHeaderPictureBox.TabIndex = 197;
            this.ValueSliceHeaderPictureBox.TabStop = false;
            this.ValueSliceHeaderPictureBox.MouseHover += new System.EventHandler(this.ValueSliceHeaderPictureBox_MouseHover);
            // 
            // F35001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.ValueSliceHeaderPanel);
            this.Controls.Add(this.ValueSliceHeaderPictureBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "F35001";
            this.Size = new System.Drawing.Size(804, 41);
            this.Tag = "35001";
            this.Load += new System.EventHandler(this.F35001_Load);
            this.ValueSliceHeaderPanel.ResumeLayout(false);
            this.NewConstpanel.ResumeLayout(false);
            this.NewConstpanel.PerformLayout();
            this.WillRollPanel.ResumeLayout(false);
            this.WillRollPanel.PerformLayout();
            this.WillValuePanel.ResumeLayout(false);
            this.WillValuePanel.PerformLayout();
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.SliceTypePanel.ResumeLayout(false);
            this.SliceTypePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ValueSliceHeaderPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

      

        #endregion

        private System.Windows.Forms.Panel ValueSliceHeaderPanel;
        private System.Windows.Forms.PictureBox ValueSliceHeaderPictureBox;
        private System.Windows.Forms.Panel SliceTypePanel;
        private TerraScan.UI.Controls.TerraScanTextBox SliceTypeTextBox;
        private System.Windows.Forms.Label SliceTypeLabel;
        private System.Windows.Forms.Panel DescriptionPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DescriptionTextBox;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Panel WillRollPanel;
        private TerraScan.UI.Controls.TerraScanComboBox WillRollCombo;
        private System.Windows.Forms.Label WillRollLabel;
        private System.Windows.Forms.Panel WillValuePanel;
        private TerraScan.UI.Controls.TerraScanComboBox WillValueCombo;
        private System.Windows.Forms.Label WillValueLabel;
        private System.Windows.Forms.ToolTip ValueSliceHeadeToolTip;
        private System.Windows.Forms.Panel NewConstpanel;
        private System.Windows.Forms.Label NewConstlabel;
        private TerraScan.UI.Controls.TerraScanTextBox NewConstValueTextBox;
    }
}
