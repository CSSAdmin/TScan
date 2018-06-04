namespace D81003
{
    partial class F810031
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F810031));
            this.FormulaPanel = new System.Windows.Forms.Panel();
            this.FormulaTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.OkButton = new TerraScan.UI.Controls.TerraScanButton();
            this.HelpLink = new System.Windows.Forms.LinkLabel();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.FormNumberLabel = new System.Windows.Forms.Label();
            this.FormulaPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormulaPanel
            // 
            this.FormulaPanel.BackColor = System.Drawing.Color.White;
            this.FormulaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FormulaPanel.Controls.Add(this.FormulaTextBox);
            this.FormulaPanel.Location = new System.Drawing.Point(1, 1);
            this.FormulaPanel.Name = "FormulaPanel";
            this.FormulaPanel.Size = new System.Drawing.Size(440, 125);
            this.FormulaPanel.TabIndex = 0;
            this.FormulaPanel.TabStop = true;
            // 
            // FormulaTextBox
            // 
            this.FormulaTextBox.AllowClick = true;
            this.FormulaTextBox.AllowNegativeSign = false;
            this.FormulaTextBox.ApplyCFGFormat = false;
            this.FormulaTextBox.ApplyCurrencyFormat = false;
            this.FormulaTextBox.ApplyFocusColor = true;
            this.FormulaTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.FormulaTextBox.ApplyNegativeStandard = true;
            this.FormulaTextBox.ApplyParentFocusColor = true;
            this.FormulaTextBox.ApplyTimeFormat = false;
            this.FormulaTextBox.BackColor = System.Drawing.Color.White;
            this.FormulaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FormulaTextBox.CFromatWihoutSymbol = false;
            this.FormulaTextBox.CheckForEmpty = false;
            this.FormulaTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FormulaTextBox.Digits = -1;
            this.FormulaTextBox.EmptyDecimalValue = false;
            this.FormulaTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.FormulaTextBox.ForeColor = System.Drawing.Color.Black;
            this.FormulaTextBox.IsEditable = true;
            this.FormulaTextBox.IsQueryableFileld = false;
            this.FormulaTextBox.Location = new System.Drawing.Point(0, 2);
            this.FormulaTextBox.LockKeyPress = true;
            this.FormulaTextBox.MaxLength = 0;
            this.FormulaTextBox.Multiline = true;
            this.FormulaTextBox.Name = "FormulaTextBox";
            this.FormulaTextBox.PersistDefaultColor = false;
            this.FormulaTextBox.Precision = 2;
            this.FormulaTextBox.QueryingFileldName = "";
            this.FormulaTextBox.ReadOnly = true;
            this.FormulaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FormulaTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FormulaTextBox.Size = new System.Drawing.Size(436, 120);
            this.FormulaTextBox.SpecialCharacter = "%";
            this.FormulaTextBox.TabIndex = 1;
            this.FormulaTextBox.TabStop = false;
            this.FormulaTextBox.TextCustomFormat = "$#,##0.00";
            this.FormulaTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.FormulaTextBox.WholeInteger = false;
            // 
            // OkButton
            // 
            this.OkButton.ActualPermission = false;
            this.OkButton.ApplyDisableBehaviour = false;
            this.OkButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.OkButton.BorderColor = System.Drawing.Color.Wheat;
            this.OkButton.CommentPriority = false;
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OkButton.EnableAutoPrint = false;
            this.OkButton.FilterStatus = false;
            this.OkButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton.FocusRectangleEnabled = true;
            this.OkButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.OkButton.ImageSelected = false;
            this.OkButton.Location = new System.Drawing.Point(346, 132);
            this.OkButton.Name = "OkButton";
            this.OkButton.NewPadding = 5;
            this.OkButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.OkButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.OkButton.Size = new System.Drawing.Size(90, 30);
            this.OkButton.StatusIndicator = false;
            this.OkButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.OkButton.StatusOffText = null;
            this.OkButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.OkButton.StatusOnText = null;
            this.OkButton.TabIndex = 2;
            this.OkButton.TabStop = false;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpLink.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLink.Location = new System.Drawing.Point(191, 172);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(56, 15);
            this.HelpLink.TabIndex = 3;
            this.HelpLink.TabStop = true;
            this.HelpLink.Text = "Help";
            this.HelpLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(5, 167);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(434, 2);
            this.FormLinePanel.TabIndex = 117;
            // 
            // FormNumberLabel
            // 
            this.FormNumberLabel.AutoSize = true;
            this.FormNumberLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormNumberLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.FormNumberLabel.Location = new System.Drawing.Point(4, 172);
            this.FormNumberLabel.Name = "FormNumberLabel";
            this.FormNumberLabel.Size = new System.Drawing.Size(49, 15);
            this.FormNumberLabel.TabIndex = 118;
            this.FormNumberLabel.Text = "810031";
            // 
            // F810031
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.OkButton;
            this.ClientSize = new System.Drawing.Size(442, 193);
            this.Controls.Add(this.FormNumberLabel);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.FormulaPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F810031";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "810031";
            this.Text = "TerraScan T2 - Formula";
            this.Load += new System.EventHandler(this.F810031_Load);
            this.FormulaPanel.ResumeLayout(false);
            this.FormulaPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel FormulaPanel;
        private TerraScan.UI.Controls.TerraScanTextBox FormulaTextBox;
        private TerraScan.UI.Controls.TerraScanButton OkButton;
        private System.Windows.Forms.LinkLabel HelpLink;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label FormNumberLabel;
    }
}