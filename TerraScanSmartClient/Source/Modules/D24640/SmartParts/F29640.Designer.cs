namespace D24640
{
    partial class F29640
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.NotePanel = new System.Windows.Forms.Panel();
            this.NoteTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.NoteLabel = new System.Windows.Forms.Label();
            this.FrozenValuePanel = new System.Windows.Forms.Panel();
            this.FrozenValueTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.FrozenValueLabel = new System.Windows.Forms.Label();
            this.FrozenPictureBox = new System.Windows.Forms.PictureBox();
            this.FrozenToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.NotePanel.SuspendLayout();
            this.FrozenValuePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrozenPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.NotePanel);
            this.panel1.Controls.Add(this.FrozenValuePanel);
            this.panel1.Controls.Add(this.FrozenPictureBox);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 39);
            this.panel1.TabIndex = 0;
            // 
            // NotePanel
            // 
            this.NotePanel.BackColor = System.Drawing.Color.White;
            this.NotePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NotePanel.Controls.Add(this.NoteTextBox);
            this.NotePanel.Controls.Add(this.NoteLabel);
            this.NotePanel.Location = new System.Drawing.Point(249, 0);
            this.NotePanel.Name = "NotePanel";
            this.NotePanel.Size = new System.Drawing.Size(520, 37);
            this.NotePanel.TabIndex = 1;
            this.NotePanel.TabStop = true;
            // 
            // NoteTextBox
            // 
            this.NoteTextBox.AllowClick = true;
            this.NoteTextBox.AllowNegativeSign = false;
            this.NoteTextBox.ApplyCFGFormat = false;
            this.NoteTextBox.ApplyCurrencyFormat = false;
            this.NoteTextBox.ApplyFocusColor = true;
            this.NoteTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.NoteTextBox.ApplyNegativeStandard = true;
            this.NoteTextBox.ApplyParentFocusColor = true;
            this.NoteTextBox.ApplyTimeFormat = false;
            this.NoteTextBox.BackColor = System.Drawing.Color.White;
            this.NoteTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NoteTextBox.CFromatWihoutSymbol = false;
            this.NoteTextBox.CheckForEmpty = false;
            this.NoteTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NoteTextBox.Digits = -1;
            this.NoteTextBox.EmptyDecimalValue = false;
            this.NoteTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.NoteTextBox.ForeColor = System.Drawing.Color.Black;
            this.NoteTextBox.IsEditable = true;
            this.NoteTextBox.IsQueryableFileld = true;
            this.NoteTextBox.Location = new System.Drawing.Point(10, 15);
            this.NoteTextBox.LockKeyPress = false;
            this.NoteTextBox.MaxLength = 50;
            this.NoteTextBox.Name = "NoteTextBox";
            this.NoteTextBox.PersistDefaultColor = false;
            this.NoteTextBox.Precision = 2;
            this.NoteTextBox.QueryingFileldName = "";
            this.NoteTextBox.SetColorFlag = false;
            this.NoteTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.NoteTextBox.Size = new System.Drawing.Size(496, 16);
            this.NoteTextBox.SpecialCharacter = "%";
            this.NoteTextBox.TabIndex = 1;
            this.NoteTextBox.TextCustomFormat = "$#,##0.00";
            this.NoteTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.NoteTextBox.WholeInteger = false;
            this.NoteTextBox.TextChanged += new System.EventHandler(this.EnableEditButtonInMasterForm);
            // 
            // NoteLabel
            // 
            this.NoteLabel.AutoSize = true;
            this.NoteLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.NoteLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoteLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.NoteLabel.Location = new System.Drawing.Point(0, 0);
            this.NoteLabel.Name = "NoteLabel";
            this.NoteLabel.Size = new System.Drawing.Size(35, 14);
            this.NoteLabel.TabIndex = 0;
            this.NoteLabel.Text = "Note:";
            // 
            // FrozenValuePanel
            // 
            this.FrozenValuePanel.BackColor = System.Drawing.Color.Transparent;
            this.FrozenValuePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FrozenValuePanel.Controls.Add(this.FrozenValueTextBox);
            this.FrozenValuePanel.Controls.Add(this.FrozenValueLabel);
            this.FrozenValuePanel.Location = new System.Drawing.Point(0, 0);
            this.FrozenValuePanel.Name = "FrozenValuePanel";
            this.FrozenValuePanel.Size = new System.Drawing.Size(250, 37);
            this.FrozenValuePanel.TabIndex = 0;
            // 
            // FrozenValueTextBox
            // 
            this.FrozenValueTextBox.AllowClick = true;
            this.FrozenValueTextBox.AllowNegativeSign = false;
            this.FrozenValueTextBox.ApplyCFGFormat = false;
            this.FrozenValueTextBox.ApplyCurrencyFormat = true;
            this.FrozenValueTextBox.ApplyFocusColor = true;
            this.FrozenValueTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.FrozenValueTextBox.ApplyNegativeStandard = false;
            this.FrozenValueTextBox.ApplyParentFocusColor = true;
            this.FrozenValueTextBox.ApplyTimeFormat = false;
            this.FrozenValueTextBox.BackColor = System.Drawing.Color.White;
            this.FrozenValueTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FrozenValueTextBox.CFromatWihoutSymbol = false;
            this.FrozenValueTextBox.CheckForEmpty = false;
            this.FrozenValueTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FrozenValueTextBox.Digits = -1;
            this.FrozenValueTextBox.EmptyDecimalValue = false;
            this.FrozenValueTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.FrozenValueTextBox.ForeColor = System.Drawing.Color.Black;
            this.FrozenValueTextBox.IsEditable = true;
            this.FrozenValueTextBox.IsQueryableFileld = true;
            this.FrozenValueTextBox.Location = new System.Drawing.Point(10, 15);
            this.FrozenValueTextBox.LockKeyPress = false;
            this.FrozenValueTextBox.MaxLength = 15;
            this.FrozenValueTextBox.Name = "FrozenValueTextBox";
            this.FrozenValueTextBox.PersistDefaultColor = false;
            this.FrozenValueTextBox.Precision = 2;
            this.FrozenValueTextBox.QueryingFileldName = "";
            this.FrozenValueTextBox.SetColorFlag = false;
            this.FrozenValueTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FrozenValueTextBox.Size = new System.Drawing.Size(226, 16);
            this.FrozenValueTextBox.SpecialCharacter = "%";
            this.FrozenValueTextBox.TabIndex = 1;
            this.FrozenValueTextBox.Tag = "";
            this.FrozenValueTextBox.TextCustomFormat = "$ #,##0";
            this.FrozenValueTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.FrozenValueTextBox.WholeInteger = true;
            this.FrozenValueTextBox.TextChanged += new System.EventHandler(this.EnableEditButtonInMasterForm);
            // 
            // FrozenValueLabel
            // 
            this.FrozenValueLabel.AutoSize = true;
            this.FrozenValueLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.FrozenValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.FrozenValueLabel.Location = new System.Drawing.Point(0, 0);
            this.FrozenValueLabel.Name = "FrozenValueLabel";
            this.FrozenValueLabel.Size = new System.Drawing.Size(82, 14);
            this.FrozenValueLabel.TabIndex = 0;
            this.FrozenValueLabel.Text = "Frozen Value:";
            // 
            // FrozenPictureBox
            // 
            this.FrozenPictureBox.Location = new System.Drawing.Point(762, 0);
            this.FrozenPictureBox.Name = "FrozenPictureBox";
            this.FrozenPictureBox.Size = new System.Drawing.Size(42, 37);
            this.FrozenPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FrozenPictureBox.TabIndex = 120;
            this.FrozenPictureBox.TabStop = false;
            this.FrozenPictureBox.Click += new System.EventHandler(this.FrozenPictureBox_Click);
            this.FrozenPictureBox.MouseEnter += new System.EventHandler(this.FrozenPictureBox_MouseEnter);
            // 
            // F29640
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "F29640";
            this.Size = new System.Drawing.Size(804, 38);
            this.Tag = "29640";
            this.Load += new System.EventHandler(this.F29640_Load);
            this.panel1.ResumeLayout(false);
            this.NotePanel.ResumeLayout(false);
            this.NotePanel.PerformLayout();
            this.FrozenValuePanel.ResumeLayout(false);
            this.FrozenValuePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrozenPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel FrozenValuePanel;
        private System.Windows.Forms.Label FrozenValueLabel;
        private System.Windows.Forms.Panel NotePanel;
        private TerraScan.UI.Controls.TerraScanTextBox NoteTextBox;
        private System.Windows.Forms.Label NoteLabel;
        private System.Windows.Forms.PictureBox FrozenPictureBox;
        private TerraScan.UI.Controls.TerraScanTextBox FrozenValueTextBox;
        private System.Windows.Forms.ToolTip FrozenToolTip;
    }
}
