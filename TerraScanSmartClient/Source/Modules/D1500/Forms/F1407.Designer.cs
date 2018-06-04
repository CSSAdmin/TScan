namespace D1500
{
    partial class F1407
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1407));
            this.FormLabel = new System.Windows.Forms.Label();
            this.Typepanel = new System.Windows.Forms.Panel();
            this.TypeComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.Typelabel = new System.Windows.Forms.Label();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.StatusComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.Statuslabel = new System.Windows.Forms.Label();
            this.NotePanel = new System.Windows.Forms.Panel();
            this.NoteTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.Notelabel = new System.Windows.Forms.Label();
            this.SaveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.Typepanel.SuspendLayout();
            this.StatusPanel.SuspendLayout();
            this.NotePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormLabel
            // 
            this.FormLabel.AutoSize = true;
            this.FormLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.FormLabel.Location = new System.Drawing.Point(28, 222);
            this.FormLabel.Name = "FormLabel";
            this.FormLabel.Size = new System.Drawing.Size(35, 15);
            this.FormLabel.TabIndex = 168;
            this.FormLabel.Text = "1407";
            // 
            // Typepanel
            // 
            this.Typepanel.BackColor = System.Drawing.Color.White;
            this.Typepanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Typepanel.Controls.Add(this.TypeComboBox);
            this.Typepanel.Controls.Add(this.Typelabel);
            this.Typepanel.Location = new System.Drawing.Point(30, 71);
            this.Typepanel.Name = "Typepanel";
            this.Typepanel.Size = new System.Drawing.Size(225, 40);
            this.Typepanel.TabIndex = 1;
            this.Typepanel.TabStop = true;
            // 
            // TypeComboBox
            // 
            this.TypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TypeComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeComboBox.ForeColor = System.Drawing.Color.Black;
            this.TypeComboBox.FormattingEnabled = true;
            this.TypeComboBox.Location = new System.Drawing.Point(25, 13);
            this.TypeComboBox.Name = "TypeComboBox";
            this.TypeComboBox.Size = new System.Drawing.Size(195, 24);
            this.TypeComboBox.TabIndex = 15;
            this.TypeComboBox.Tag = "";
            this.TypeComboBox.SelectedIndexChanged += new System.EventHandler(this.TypeComboBox_SelectedIndexChanged);
            // 
            // Typelabel
            // 
            this.Typelabel.AutoSize = true;
            this.Typelabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Typelabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Typelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.Typelabel.Location = new System.Drawing.Point(1, 1);
            this.Typelabel.Name = "Typelabel";
            this.Typelabel.Size = new System.Drawing.Size(36, 14);
            this.Typelabel.TabIndex = 62;
            this.Typelabel.Text = "Type:";
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.White;
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Controls.Add(this.StatusComboBox);
            this.StatusPanel.Controls.Add(this.Statuslabel);
            this.StatusPanel.Location = new System.Drawing.Point(254, 71);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(225, 40);
            this.StatusPanel.TabIndex = 2;
            this.StatusPanel.TabStop = true;
            // 
            // StatusComboBox
            // 
            this.StatusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StatusComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StatusComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusComboBox.ForeColor = System.Drawing.Color.Black;
            this.StatusComboBox.FormattingEnabled = true;
            this.StatusComboBox.Location = new System.Drawing.Point(25, 13);
            this.StatusComboBox.Name = "StatusComboBox";
            this.StatusComboBox.Size = new System.Drawing.Size(195, 24);
            this.StatusComboBox.TabIndex = 15;
            this.StatusComboBox.Tag = "";
            this.StatusComboBox.SelectedIndexChanged += new System.EventHandler(this.StatusComboBox_SelectedIndexChanged);
            // 
            // Statuslabel
            // 
            this.Statuslabel.AutoSize = true;
            this.Statuslabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Statuslabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Statuslabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.Statuslabel.Location = new System.Drawing.Point(1, 1);
            this.Statuslabel.Name = "Statuslabel";
            this.Statuslabel.Size = new System.Drawing.Size(45, 14);
            this.Statuslabel.TabIndex = 62;
            this.Statuslabel.Text = "Status:";
            // 
            // NotePanel
            // 
            this.NotePanel.BackColor = System.Drawing.Color.White;
            this.NotePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NotePanel.Controls.Add(this.NoteTextBox);
            this.NotePanel.Controls.Add(this.Notelabel);
            this.NotePanel.Location = new System.Drawing.Point(30, 110);
            this.NotePanel.Name = "NotePanel";
            this.NotePanel.Size = new System.Drawing.Size(449, 100);
            this.NotePanel.TabIndex = 3;
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
            this.NoteTextBox.IsEditable = false;
            this.NoteTextBox.IsQueryableFileld = true;
            this.NoteTextBox.Location = new System.Drawing.Point(15, 21);
            this.NoteTextBox.LockKeyPress = false;
            this.NoteTextBox.MaxLength = 250;
            this.NoteTextBox.Multiline = true;
            this.NoteTextBox.Name = "NoteTextBox";
            this.NoteTextBox.PersistDefaultColor = false;
            this.NoteTextBox.Precision = 2;
            this.NoteTextBox.QueryingFileldName = "";
            this.NoteTextBox.SetColorFlag = false;
            this.NoteTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.NoteTextBox.Size = new System.Drawing.Size(414, 62);
            this.NoteTextBox.SpecialCharacter = "%";
            this.NoteTextBox.TabIndex = 3;
            this.NoteTextBox.TextCustomFormat = "";
            this.NoteTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.NoteTextBox.WholeInteger = false;
            // 
            // Notelabel
            // 
            this.Notelabel.AutoSize = true;
            this.Notelabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Notelabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Notelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.Notelabel.Location = new System.Drawing.Point(1, 1);
            this.Notelabel.Name = "Notelabel";
            this.Notelabel.Size = new System.Drawing.Size(35, 14);
            this.Notelabel.TabIndex = 0;
            this.Notelabel.Text = "Note:";
            // 
            // SaveButton
            // 
            this.SaveButton.ActualPermission = false;
            this.SaveButton.ApplyDisableBehaviour = false;
            this.SaveButton.AutoSize = true;
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SaveButton.BorderColor = System.Drawing.Color.Wheat;
            this.SaveButton.CommentPriority = false;
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveButton.EnableAutoPrint = false;
            this.SaveButton.Enabled = false;
            this.SaveButton.FilterStatus = false;
            this.SaveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.FocusRectangleEnabled = true;
            this.SaveButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SaveButton.ImageSelected = false;
            this.SaveButton.Location = new System.Drawing.Point(30, 24);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.NewPadding = 5;
            this.SaveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.SaveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SaveButton.Size = new System.Drawing.Size(110, 30);
            this.SaveButton.StatusIndicator = false;
            this.SaveButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SaveButton.StatusOffText = null;
            this.SaveButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SaveButton.StatusOnText = null;
            this.SaveButton.TabIndex = 0;
            this.SaveButton.TabStop = false;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // F1407
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(516, 244);
            this.Controls.Add(this.NotePanel);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.Typepanel);
            this.Controls.Add(this.FormLabel);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F1407";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "1407";
            this.Text = "Terrascan T2 - Add Selected Records to Pull List";
            this.Load += new System.EventHandler(this.F1407_Load);
            this.Typepanel.ResumeLayout(false);
            this.Typepanel.PerformLayout();
            this.StatusPanel.ResumeLayout(false);
            this.StatusPanel.PerformLayout();
            this.NotePanel.ResumeLayout(false);
            this.NotePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanButton SaveButton;
        private System.Windows.Forms.Label FormLabel;
        private System.Windows.Forms.Panel Typepanel;
        private System.Windows.Forms.Label Typelabel;
        private TerraScan.UI.Controls.TerraScanComboBox TypeComboBox;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.Label Statuslabel;
        private TerraScan.UI.Controls.TerraScanComboBox StatusComboBox;
        private System.Windows.Forms.Panel NotePanel;
        private System.Windows.Forms.Label Notelabel;
        private TerraScan.UI.Controls.TerraScanTextBox NoteTextBox;
    }
}