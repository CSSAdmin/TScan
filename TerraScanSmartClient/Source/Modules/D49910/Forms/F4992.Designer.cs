namespace D49910
{
    partial class F4992
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F4992));
            this.CommentsOkButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CommentsCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.LinePanel = new System.Windows.Forms.Panel();
            this.formIDLabel = new System.Windows.Forms.Label();
            this.LegalCommentsTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.SuspendLayout();
            // 
            // CommentsOkButton
            // 
            this.CommentsOkButton.ActualPermission = false;
            this.CommentsOkButton.ApplyDisableBehaviour = false;
            this.CommentsOkButton.AutoSize = true;
            this.CommentsOkButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CommentsOkButton.BorderColor = System.Drawing.Color.Wheat;
            this.CommentsOkButton.CommentPriority = false;
            this.CommentsOkButton.EnableAutoPrint = false;
            this.CommentsOkButton.FilterStatus = false;
            this.CommentsOkButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CommentsOkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CommentsOkButton.FocusRectangleEnabled = true;
            this.CommentsOkButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommentsOkButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CommentsOkButton.ImageSelected = false;
            this.CommentsOkButton.Location = new System.Drawing.Point(212, 152);
            this.CommentsOkButton.Name = "CommentsOkButton";
            this.CommentsOkButton.NewPadding = 5;
            this.CommentsOkButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CommentsOkButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CommentsOkButton.Size = new System.Drawing.Size(98, 28);
            this.CommentsOkButton.StatusIndicator = false;
            this.CommentsOkButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CommentsOkButton.StatusOffText = null;
            this.CommentsOkButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CommentsOkButton.StatusOnText = null;
            this.CommentsOkButton.TabIndex = 1;
            this.CommentsOkButton.TabStop = false;
            this.CommentsOkButton.Text = "OK";
            this.CommentsOkButton.UseVisualStyleBackColor = false;
            this.CommentsOkButton.Click += new System.EventHandler(this.CommentsOkButton_Click);
            // 
            // CommentsCancelButton
            // 
            this.CommentsCancelButton.ActualPermission = false;
            this.CommentsCancelButton.ApplyDisableBehaviour = false;
            this.CommentsCancelButton.AutoSize = true;
            this.CommentsCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CommentsCancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.CommentsCancelButton.CommentPriority = false;
            this.CommentsCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CommentsCancelButton.EnableAutoPrint = false;
            this.CommentsCancelButton.FilterStatus = false;
            this.CommentsCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CommentsCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CommentsCancelButton.FocusRectangleEnabled = true;
            this.CommentsCancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommentsCancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CommentsCancelButton.ImageSelected = false;
            this.CommentsCancelButton.Location = new System.Drawing.Point(316, 152);
            this.CommentsCancelButton.Name = "CommentsCancelButton";
            this.CommentsCancelButton.NewPadding = 5;
            this.CommentsCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CommentsCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CommentsCancelButton.Size = new System.Drawing.Size(98, 28);
            this.CommentsCancelButton.StatusIndicator = false;
            this.CommentsCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CommentsCancelButton.StatusOffText = null;
            this.CommentsCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CommentsCancelButton.StatusOnText = null;
            this.CommentsCancelButton.TabIndex = 2;
            this.CommentsCancelButton.TabStop = false;
            this.CommentsCancelButton.Text = "Cancel";
            this.CommentsCancelButton.UseVisualStyleBackColor = false;
            this.CommentsCancelButton.Click += new System.EventHandler(this.CommentsCancelButton_Click);
            // 
            // LinePanel
            // 
            this.LinePanel.BackColor = System.Drawing.Color.Black;
            this.LinePanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LinePanel.Location = new System.Drawing.Point(9, 189);
            this.LinePanel.Name = "LinePanel";
            this.LinePanel.Size = new System.Drawing.Size(407, 2);
            this.LinePanel.TabIndex = 99;
            // 
            // formIDLabel
            // 
            this.formIDLabel.AutoSize = true;
            this.formIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formIDLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.formIDLabel.Location = new System.Drawing.Point(6, 192);
            this.formIDLabel.Name = "formIDLabel";
            this.formIDLabel.Size = new System.Drawing.Size(35, 15);
            this.formIDLabel.TabIndex = 104;
            this.formIDLabel.Text = "4992";
            // 
            // LegalCommentsTextBox
            // 
            this.LegalCommentsTextBox.AllowClick = true;
            this.LegalCommentsTextBox.AllowNegativeSign = false;
            this.LegalCommentsTextBox.ApplyCFGFormat = false;
            this.LegalCommentsTextBox.ApplyCurrencyFormat = false;
            this.LegalCommentsTextBox.ApplyFocusColor = false;
            this.LegalCommentsTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.LegalCommentsTextBox.ApplyNegativeStandard = true;
            this.LegalCommentsTextBox.ApplyParentFocusColor = true;
            this.LegalCommentsTextBox.ApplyTimeFormat = false;
            this.LegalCommentsTextBox.BackColor = System.Drawing.Color.White;
            this.LegalCommentsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LegalCommentsTextBox.CFromatWihoutSymbol = false;
            this.LegalCommentsTextBox.CheckForEmpty = false;
            this.LegalCommentsTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.LegalCommentsTextBox.Digits = -1;
            this.LegalCommentsTextBox.EmptyDecimalValue = false;
            this.LegalCommentsTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.LegalCommentsTextBox.ForeColor = System.Drawing.Color.Black;
            this.LegalCommentsTextBox.IsEditable = false;
            this.LegalCommentsTextBox.IsQueryableFileld = true;
            this.LegalCommentsTextBox.Location = new System.Drawing.Point(-1, 0);
            this.LegalCommentsTextBox.LockKeyPress = false;
            this.LegalCommentsTextBox.MaxLength = 255;
            this.LegalCommentsTextBox.Multiline = true;
            this.LegalCommentsTextBox.Name = "LegalCommentsTextBox";
            this.LegalCommentsTextBox.PersistDefaultColor = false;
            this.LegalCommentsTextBox.Precision = 2;
            this.LegalCommentsTextBox.QueryingFileldName = "";
            this.LegalCommentsTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.LegalCommentsTextBox.Size = new System.Drawing.Size(431, 142);
            this.LegalCommentsTextBox.SpecialCharacter = "";
            this.LegalCommentsTextBox.TabIndex = 1;
            this.LegalCommentsTextBox.TextCustomFormat = "";
            this.LegalCommentsTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.LegalCommentsTextBox.WholeInteger = false;
            this.LegalCommentsTextBox.TextChanged += new System.EventHandler(this.EquipmentNameTextBox_TextChanged);
            // 
            // F4992
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(429, 211);
            this.Controls.Add(this.LegalCommentsTextBox);
            this.Controls.Add(this.formIDLabel);
            this.Controls.Add(this.LinePanel);
            this.Controls.Add(this.CommentsCancelButton);
            this.Controls.Add(this.CommentsOkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(435, 243);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(435, 243);
            this.Name = "F4992";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "4992";
            this.Text = "TerraScan T2 - Legal Comments";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.F4992_FormClosed);
            this.Load += new System.EventHandler(this.F4992_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanButton CommentsOkButton;
        private TerraScan.UI.Controls.TerraScanButton CommentsCancelButton;
        private System.Windows.Forms.Panel LinePanel;
        private System.Windows.Forms.Label formIDLabel;
        private TerraScan.UI.Controls.TerraScanTextBox LegalCommentsTextBox;
    }
}