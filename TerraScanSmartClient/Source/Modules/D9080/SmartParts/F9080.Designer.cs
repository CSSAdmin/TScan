namespace D9080
{
    partial class F9080
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
            this.formHeaderSmartPartdeckWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.formPanel = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.StepPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.DatePanel = new System.Windows.Forms.Panel();
            this.DateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DateLabel = new System.Windows.Forms.Label();
            this.RollYearPanel = new System.Windows.Forms.Panel();
            this.RollYearLabel = new System.Windows.Forms.Label();
            this.RollYearCombobox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.CompletePanel = new System.Windows.Forms.Panel();
            this.CompleteTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CompleteLabel = new System.Windows.Forms.Label();
            this.UserControlPanel = new System.Windows.Forms.Panel();
            this.UserControlPanel1 = new System.Windows.Forms.Panel();
            this.PictureBoxPanel = new System.Windows.Forms.Panel();
            this.ParcelPictureBox = new System.Windows.Forms.PictureBox();
            this.FooterWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.StepTimer = new System.Windows.Forms.Timer(this.components);
            this.formPanel.SuspendLayout();
            this.StepPanel.SuspendLayout();
            this.DatePanel.SuspendLayout();
            this.RollYearPanel.SuspendLayout();
            this.CompletePanel.SuspendLayout();
            this.PictureBoxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParcelPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // formHeaderSmartPartdeckWorkspace
            // 
            this.formHeaderSmartPartdeckWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.formHeaderSmartPartdeckWorkspace.Location = new System.Drawing.Point(472, 5);
            this.formHeaderSmartPartdeckWorkspace.Name = "formHeaderSmartPartdeckWorkspace";
            this.formHeaderSmartPartdeckWorkspace.Size = new System.Drawing.Size(388, 31);
            this.formHeaderSmartPartdeckWorkspace.TabIndex = 116;
            this.formHeaderSmartPartdeckWorkspace.Text = "FormHeaderSmartPart";
            // 
            // formPanel
            // 
            this.formPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.formPanel.Controls.Add(this.vScrollBar1);
            this.formPanel.Controls.Add(this.StepPanel);
            this.formPanel.Controls.Add(this.DatePanel);
            this.formPanel.Controls.Add(this.RollYearPanel);
            this.formPanel.Controls.Add(this.CompletePanel);
            this.formPanel.Controls.Add(this.UserControlPanel);
            this.formPanel.Controls.Add(this.UserControlPanel1);
            this.formPanel.Controls.Add(this.PictureBoxPanel);
            this.formPanel.Location = new System.Drawing.Point(12, 41);
            this.formPanel.Name = "formPanel";
            this.formPanel.Size = new System.Drawing.Size(840, 568);
            this.formPanel.TabIndex = 10;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.vScrollBar1.Enabled = false;
            this.vScrollBar1.Location = new System.Drawing.Point(750, 83);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 479);
            this.vScrollBar1.TabIndex = 20;
            // 
            // StepPanel
            // 
            this.StepPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(37)))), ((int)(((byte)(65)))));
            this.StepPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StepPanel.Controls.Add(this.label1);
            this.StepPanel.Location = new System.Drawing.Point(0, 40);
            this.StepPanel.Name = "StepPanel";
            this.StepPanel.Size = new System.Drawing.Size(768, 43);
            this.StepPanel.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 22);
            this.label1.TabIndex = 71;
            this.label1.Text = "Steps";
            // 
            // DatePanel
            // 
            this.DatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DatePanel.Controls.Add(this.DateTextBox);
            this.DatePanel.Controls.Add(this.DateLabel);
            this.DatePanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DatePanel.Location = new System.Drawing.Point(554, 0);
            this.DatePanel.Name = "DatePanel";
            this.DatePanel.Size = new System.Drawing.Size(214, 41);
            this.DatePanel.TabIndex = 13;
            // 
            // DateTextBox
            // 
            this.DateTextBox.AllowClick = true;
            this.DateTextBox.AllowNegativeSign = false;
            this.DateTextBox.ApplyCFGFormat = false;
            this.DateTextBox.ApplyCurrencyFormat = false;
            this.DateTextBox.ApplyFocusColor = true;
            this.DateTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.DateTextBox.ApplyNegativeStandard = true;
            this.DateTextBox.ApplyParentFocusColor = true;
            this.DateTextBox.ApplyTimeFormat = false;
            this.DateTextBox.BackColor = System.Drawing.Color.White;
            this.DateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DateTextBox.CFromatWihoutSymbol = false;
            this.DateTextBox.CheckForEmpty = false;
            this.DateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DateTextBox.Digits = -1;
            this.DateTextBox.EmptyDecimalValue = false;
            this.DateTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTextBox.ForeColor = System.Drawing.Color.Black;
            this.DateTextBox.IsEditable = false;
            this.DateTextBox.IsQueryableFileld = false;
            this.DateTextBox.Location = new System.Drawing.Point(19, 21);
            this.DateTextBox.LockKeyPress = true;
            this.DateTextBox.MaxLength = 103;
            this.DateTextBox.Name = "DateTextBox";
            this.DateTextBox.PersistDefaultColor = false;
            this.DateTextBox.Precision = 2;
            this.DateTextBox.QueryingFileldName = "";
            this.DateTextBox.ReadOnly = true;
            this.DateTextBox.SetColorFlag = false;
            this.DateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DateTextBox.Size = new System.Drawing.Size(165, 15);
            this.DateTextBox.SpecialCharacter = "%";
            this.DateTextBox.TabIndex = 18;
            this.DateTextBox.TabStop = false;
            this.DateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DateTextBox.TextCustomFormat = "MM/DD/YYYY";
            this.DateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.DateTextBox.WholeInteger = false;
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.DateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DateLabel.Location = new System.Drawing.Point(1, 0);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(113, 14);
            this.DateLabel.TabIndex = 70;
            this.DateLabel.Text = "Last Step Run Date:";
            // 
            // RollYearPanel
            // 
            this.RollYearPanel.AutoScroll = true;
            this.RollYearPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RollYearPanel.Controls.Add(this.RollYearLabel);
            this.RollYearPanel.Controls.Add(this.RollYearCombobox);
            this.RollYearPanel.Location = new System.Drawing.Point(0, 0);
            this.RollYearPanel.Name = "RollYearPanel";
            this.RollYearPanel.Size = new System.Drawing.Size(112, 41);
            this.RollYearPanel.TabIndex = 11;
            this.RollYearPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.RollYearPanel_Paint);
            // 
            // RollYearLabel
            // 
            this.RollYearLabel.AutoSize = true;
            this.RollYearLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.RollYearLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.RollYearLabel.Location = new System.Drawing.Point(1, 0);
            this.RollYearLabel.Name = "RollYearLabel";
            this.RollYearLabel.Size = new System.Drawing.Size(57, 14);
            this.RollYearLabel.TabIndex = 70;
            this.RollYearLabel.Text = "Roll Year:";
            // 
            // RollYearCombobox
            // 
            this.RollYearCombobox.BackColor = System.Drawing.Color.White;
            this.RollYearCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RollYearCombobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RollYearCombobox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.RollYearCombobox.FormattingEnabled = true;
            this.RollYearCombobox.Location = new System.Drawing.Point(16, 14);
            this.RollYearCombobox.Name = "RollYearCombobox";
            this.RollYearCombobox.Size = new System.Drawing.Size(87, 24);
            this.RollYearCombobox.TabIndex = 2;
            this.RollYearCombobox.SelectionChangeCommitted += new System.EventHandler(this.RollYearCombobox_SelectionChangeCommitted);
            this.RollYearCombobox.SelectedValueChanged += new System.EventHandler(this.RollYearCombobox_SelectedValueChanged);
            // 
            // CompletePanel
            // 
            this.CompletePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CompletePanel.Controls.Add(this.CompleteTextBox);
            this.CompletePanel.Controls.Add(this.CompleteLabel);
            this.CompletePanel.Location = new System.Drawing.Point(111, 0);
            this.CompletePanel.Name = "CompletePanel";
            this.CompletePanel.Size = new System.Drawing.Size(444, 41);
            this.CompletePanel.TabIndex = 12;
            this.CompletePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.CompletePanel_Paint);
            // 
            // CompleteTextBox
            // 
            this.CompleteTextBox.AllowClick = true;
            this.CompleteTextBox.AllowNegativeSign = false;
            this.CompleteTextBox.ApplyCFGFormat = false;
            this.CompleteTextBox.ApplyCurrencyFormat = false;
            this.CompleteTextBox.ApplyFocusColor = true;
            this.CompleteTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.CompleteTextBox.ApplyNegativeStandard = true;
            this.CompleteTextBox.ApplyParentFocusColor = true;
            this.CompleteTextBox.ApplyTimeFormat = false;
            this.CompleteTextBox.BackColor = System.Drawing.Color.White;
            this.CompleteTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CompleteTextBox.CFromatWihoutSymbol = false;
            this.CompleteTextBox.CheckForEmpty = false;
            this.CompleteTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CompleteTextBox.Digits = -1;
            this.CompleteTextBox.EmptyDecimalValue = false;
            this.CompleteTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompleteTextBox.ForeColor = System.Drawing.Color.Black;
            this.CompleteTextBox.IsEditable = false;
            this.CompleteTextBox.IsQueryableFileld = false;
            this.CompleteTextBox.Location = new System.Drawing.Point(19, 21);
            this.CompleteTextBox.LockKeyPress = true;
            this.CompleteTextBox.MaxLength = 250;
            this.CompleteTextBox.Name = "CompleteTextBox";
            this.CompleteTextBox.PersistDefaultColor = false;
            this.CompleteTextBox.Precision = 2;
            this.CompleteTextBox.QueryingFileldName = "";
            this.CompleteTextBox.ReadOnly = true;
            this.CompleteTextBox.SetColorFlag = false;
            this.CompleteTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CompleteTextBox.Size = new System.Drawing.Size(400, 15);
            this.CompleteTextBox.SpecialCharacter = "%";
            this.CompleteTextBox.TabIndex = 18;
            this.CompleteTextBox.TabStop = false;
            this.CompleteTextBox.TextCustomFormat = "$#,##0.00";
            this.CompleteTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CompleteTextBox.WholeInteger = false;
            // 
            // CompleteLabel
            // 
            this.CompleteLabel.AutoSize = true;
            this.CompleteLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.CompleteLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CompleteLabel.Location = new System.Drawing.Point(1, 0);
            this.CompleteLabel.Name = "CompleteLabel";
            this.CompleteLabel.Size = new System.Drawing.Size(126, 14);
            this.CompleteLabel.TabIndex = 70;
            this.CompleteLabel.Text = "Last Step Completed:";
            // 
            // UserControlPanel
            // 
            this.UserControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.UserControlPanel.AutoScroll = true;
            this.UserControlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.UserControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserControlPanel.Location = new System.Drawing.Point(0, 81);
            this.UserControlPanel.Name = "UserControlPanel";
            this.UserControlPanel.Size = new System.Drawing.Size(768, 482);
            this.UserControlPanel.TabIndex = 19;
            // 
            // UserControlPanel1
            // 
            this.UserControlPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.UserControlPanel1.AutoScroll = true;
            this.UserControlPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.UserControlPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserControlPanel1.Location = new System.Drawing.Point(0, 81);
            this.UserControlPanel1.Name = "UserControlPanel1";
            this.UserControlPanel1.Size = new System.Drawing.Size(768, 482);
            this.UserControlPanel1.TabIndex = 19;
            // 
            // PictureBoxPanel
            // 
            this.PictureBoxPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBoxPanel.Controls.Add(this.ParcelPictureBox);
            this.PictureBoxPanel.Location = new System.Drawing.Point(759, -1);
            this.PictureBoxPanel.Name = "PictureBoxPanel";
            this.PictureBoxPanel.Size = new System.Drawing.Size(44, 564);
            this.PictureBoxPanel.TabIndex = 101;
            // 
            // ParcelPictureBox
            // 
            this.ParcelPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ParcelPictureBox.Location = new System.Drawing.Point(0, -1);
            this.ParcelPictureBox.Name = "ParcelPictureBox";
            this.ParcelPictureBox.Size = new System.Drawing.Size(44, 564);
            this.ParcelPictureBox.TabIndex = 14;
            this.ParcelPictureBox.TabStop = false;
            // 
            // FooterWorkspace
            // 
            this.FooterWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FooterWorkspace.Location = new System.Drawing.Point(-10, 632);
            this.FooterWorkspace.Name = "FooterWorkspace";
            this.FooterWorkspace.Size = new System.Drawing.Size(861, 33);
            this.FooterWorkspace.TabIndex = 117;
            this.FooterWorkspace.Tag = "";
            // 
            // StepTimer
            // 
            this.StepTimer.Interval = 1;
            // 
            // F9080
            // 
            this.AccessibleName = "RollYearManagement";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.FooterWorkspace);
            this.Controls.Add(this.formHeaderSmartPartdeckWorkspace);
            this.Controls.Add(this.formPanel);
            this.MinimumSize = new System.Drawing.Size(864, 669);
            this.Name = "F9080";
            this.Size = new System.Drawing.Size(864, 669);
            this.Tag = "9080";
            this.Load += new System.EventHandler(this.F9080_Load);
            this.Resize += new System.EventHandler(this.F9080_Resize);
            this.formPanel.ResumeLayout(false);
            this.StepPanel.ResumeLayout(false);
            this.StepPanel.PerformLayout();
            this.DatePanel.ResumeLayout(false);
            this.DatePanel.PerformLayout();
            this.RollYearPanel.ResumeLayout(false);
            this.RollYearPanel.PerformLayout();
            this.CompletePanel.ResumeLayout(false);
            this.CompletePanel.PerformLayout();
            this.PictureBoxPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ParcelPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel formPanel;
        private System.Windows.Forms.Label CompleteLabel;
        private System.Windows.Forms.Panel CompletePanel;
        private TerraScan.UI.Controls.TerraScanTextBox CompleteTextBox;     
        private System.Windows.Forms.Panel DatePanel;
        private System.Windows.Forms.Label DateLabel;
        private TerraScan.UI.Controls.TerraScanTextBox DateTextBox;
       // private TerraScan.RollYearStep.RollYearStepUserControl RollYearStepUserControl;
        private System.Windows.Forms.Panel UserControlPanel;
        private System.Windows.Forms.Panel UserControlPanel1;  
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace formHeaderSmartPartdeckWorkspace;
        private System.Windows.Forms.Panel RollYearPanel;
        private System.Windows.Forms.Label RollYearLabel;
        private TerraScan.UI.Controls.TerraScanComboBox RollYearCombobox;
        private System.Windows.Forms.PictureBox ParcelPictureBox;
        private System.Windows.Forms.Panel StepPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FooterWorkspace;
        private System.Windows.Forms.Timer StepTimer;
        private System.Windows.Forms.Panel PictureBoxPanel;  
    }
}
