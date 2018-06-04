namespace TerraScan.Common
{
    partial class ErrorEngine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorEngine));
            this.errorStringLabel = new System.Windows.Forms.Label();
            this.commentsPanel = new System.Windows.Forms.Panel();
            this.commentTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.commentLabel = new System.Windows.Forms.Label();
            this.CommentLinePanel = new System.Windows.Forms.Panel();
            this.causeListLinkLabel = new System.Windows.Forms.LinkLabel();
            this.formIDLabel = new System.Windows.Forms.Label();
            this.dateTimeLabel = new System.Windows.Forms.Label();
            this.okErrorEngineButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ErrorCloseButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CloseButtonPanel = new System.Windows.Forms.Panel();
            this.commentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorStringLabel
            // 
            this.errorStringLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorStringLabel.Location = new System.Drawing.Point(12, 9);
            this.errorStringLabel.Name = "errorStringLabel";
            this.errorStringLabel.Size = new System.Drawing.Size(493, 47);
            this.errorStringLabel.TabIndex = 0;
            // 
            // commentsPanel
            // 
            this.commentsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.commentsPanel.Controls.Add(this.commentTextBox);
            this.commentsPanel.Controls.Add(this.commentLabel);
            this.commentsPanel.Location = new System.Drawing.Point(12, 65);
            this.commentsPanel.Name = "commentsPanel";
            this.commentsPanel.Size = new System.Drawing.Size(493, 75);
            this.commentsPanel.TabIndex = 1;
            // 
            // commentTextBox
            // 
            this.commentTextBox.AllowClick = true;
            this.commentTextBox.AllowNegativeSign = false;
            this.commentTextBox.ApplyCFGFormat = false;
            this.commentTextBox.ApplyCurrencyFormat = false;
            this.commentTextBox.ApplyFocusColor = true;
            this.commentTextBox.ApplyNegativeStandard = true;
            this.commentTextBox.ApplyParentFocusColor = true;
            this.commentTextBox.ApplyTimeFormat = false;
            this.commentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commentTextBox.CFromatWihoutSymbol = false;
            this.commentTextBox.CheckForEmpty = false;
            this.commentTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.commentTextBox.Digits = -1;
            this.commentTextBox.EmptyDecimalValue = false;
            this.commentTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentTextBox.ForeColor = System.Drawing.Color.Black;
            this.commentTextBox.IsEditable = false;
            this.commentTextBox.IsQueryableFileld = false;
            this.commentTextBox.Location = new System.Drawing.Point(36, 18);
            this.commentTextBox.LockKeyPress = false;
            this.commentTextBox.MaxLength = 100;
            this.commentTextBox.Multiline = true;
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.PersistDefaultColor = false;
            this.commentTextBox.Precision = 2;
            this.commentTextBox.QueryingFileldName = "";
            this.commentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.commentTextBox.Size = new System.Drawing.Size(436, 45);
            this.commentTextBox.SpecialCharacter = "%";
            this.commentTextBox.TabIndex = 2;
            this.commentTextBox.TextCustomFormat = "$ #,##0.00";
            this.commentTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.commentTextBox.WholeInteger = false;
            // 
            // commentLabel
            // 
            this.commentLabel.AutoSize = true;
            this.commentLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.commentLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.commentLabel.Location = new System.Drawing.Point(-1, -1);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(65, 14);
            this.commentLabel.TabIndex = 63;
            this.commentLabel.Text = "Comment:";
            // 
            // CommentLinePanel
            // 
            this.CommentLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.CommentLinePanel.Location = new System.Drawing.Point(4, 182);
            this.CommentLinePanel.Name = "CommentLinePanel";
            this.CommentLinePanel.Size = new System.Drawing.Size(505, 2);
            this.CommentLinePanel.TabIndex = 153;
            // 
            // causeListLinkLabel
            // 
            this.causeListLinkLabel.AutoSize = true;
            this.causeListLinkLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.causeListLinkLabel.Location = new System.Drawing.Point(439, 162);
            this.causeListLinkLabel.Name = "causeListLinkLabel";
            this.causeListLinkLabel.Size = new System.Drawing.Size(66, 14);
            this.causeListLinkLabel.TabIndex = 4;
            this.causeListLinkLabel.TabStop = true;
            this.causeListLinkLabel.Text = "Cause List";
            this.causeListLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CauseListLinkLabel_LinkClicked);
            // 
            // formIDLabel
            // 
            this.formIDLabel.AutoSize = true;
            this.formIDLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formIDLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.formIDLabel.Location = new System.Drawing.Point(1, 184);
            this.formIDLabel.Name = "formIDLabel";
            this.formIDLabel.Size = new System.Drawing.Size(31, 14);
            this.formIDLabel.TabIndex = 155;
            this.formIDLabel.Text = "9010";
            // 
            // dateTimeLabel
            // 
            this.dateTimeLabel.AutoSize = true;
            this.dateTimeLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.dateTimeLabel.Location = new System.Drawing.Point(208, 184);
            this.dateTimeLabel.Name = "dateTimeLabel";
            this.dateTimeLabel.Size = new System.Drawing.Size(0, 14);
            this.dateTimeLabel.TabIndex = 156;
            // 
            // okErrorEngineButton
            // 
            this.okErrorEngineButton.ActualPermission = false;
            this.okErrorEngineButton.ApplyDisableBehaviour = false;
            this.okErrorEngineButton.AutoSize = true;
            this.okErrorEngineButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.okErrorEngineButton.BorderColor = System.Drawing.Color.Wheat;
            this.okErrorEngineButton.CommentPriority = false;
            this.okErrorEngineButton.EnableAutoPrint = false;
            this.okErrorEngineButton.FilterStatus = false;
            this.okErrorEngineButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.okErrorEngineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okErrorEngineButton.FocusRectangleEnabled = true;
            this.okErrorEngineButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okErrorEngineButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.okErrorEngineButton.ImageSelected = false;
            this.okErrorEngineButton.Location = new System.Drawing.Point(12, 146);
            this.okErrorEngineButton.Name = "okErrorEngineButton";
            this.okErrorEngineButton.NewPadding = 5;
            this.okErrorEngineButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.okErrorEngineButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.okErrorEngineButton.Size = new System.Drawing.Size(110, 30);
            this.okErrorEngineButton.StatusIndicator = false;
            this.okErrorEngineButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.okErrorEngineButton.StatusOffText = null;
            this.okErrorEngineButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.okErrorEngineButton.StatusOnText = null;
            this.okErrorEngineButton.TabIndex = 3;
            this.okErrorEngineButton.TabStop = false;
            this.okErrorEngineButton.Text = "OK";
            this.okErrorEngineButton.UseVisualStyleBackColor = false;
            this.okErrorEngineButton.Click += new System.EventHandler(this.OKErrorEngineButton_Click);
            // 
            // ErrorCloseButton
            // 
            this.ErrorCloseButton.ActualPermission = false;
            this.ErrorCloseButton.ApplyDisableBehaviour = false;
            this.ErrorCloseButton.AutoSize = true;
            this.ErrorCloseButton.BackColor = System.Drawing.Color.White;
            this.ErrorCloseButton.BorderColor = System.Drawing.Color.Wheat;
            this.ErrorCloseButton.CommentPriority = false;
            this.ErrorCloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ErrorCloseButton.EnableAutoPrint = false;
            this.ErrorCloseButton.FilterStatus = false;
            this.ErrorCloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ErrorCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ErrorCloseButton.FocusRectangleEnabled = true;
            this.ErrorCloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorCloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ErrorCloseButton.ImageSelected = false;
            this.ErrorCloseButton.Location = new System.Drawing.Point(378, 159);
            this.ErrorCloseButton.Name = "ErrorCloseButton";
            this.ErrorCloseButton.NewPadding = 5;
            this.ErrorCloseButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.ErrorCloseButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ErrorCloseButton.Size = new System.Drawing.Size(12, 10);
            this.ErrorCloseButton.StatusIndicator = false;
            this.ErrorCloseButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ErrorCloseButton.StatusOffText = null;
            this.ErrorCloseButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ErrorCloseButton.StatusOnText = null;
            this.ErrorCloseButton.TabIndex = 157;
            this.ErrorCloseButton.TabStop = false;
            this.ErrorCloseButton.UseVisualStyleBackColor = false;
            this.ErrorCloseButton.Click += new System.EventHandler(this.ErrorCloseButton_Click);
            // 
            // CloseButtonPanel
            // 
            this.CloseButtonPanel.BackColor = System.Drawing.Color.White;
            this.CloseButtonPanel.Location = new System.Drawing.Point(358, 154);
            this.CloseButtonPanel.Name = "CloseButtonPanel";
            this.CloseButtonPanel.Size = new System.Drawing.Size(44, 20);
            this.CloseButtonPanel.TabIndex = 158;
            this.CloseButtonPanel.TabStop = true;
            // 
            // ErrorEngine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.ErrorCloseButton;
            this.ClientSize = new System.Drawing.Size(517, 203);
            this.Controls.Add(this.CloseButtonPanel);
            this.Controls.Add(this.ErrorCloseButton);
            this.Controls.Add(this.dateTimeLabel);
            this.Controls.Add(this.formIDLabel);
            this.Controls.Add(this.causeListLinkLabel);
            this.Controls.Add(this.CommentLinePanel);
            this.Controls.Add(this.okErrorEngineButton);
            this.Controls.Add(this.commentsPanel);
            this.Controls.Add(this.errorStringLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorEngine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "9010";
            this.Text = "TerraScan -  ";
            this.Load += new System.EventHandler(this.ErrorEngineForm_Load);
            this.commentsPanel.ResumeLayout(false);
            this.commentsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label errorStringLabel;
        private System.Windows.Forms.Panel commentsPanel;
        private TerraScan.UI.Controls.TerraScanButton okErrorEngineButton;
        private System.Windows.Forms.Panel CommentLinePanel;
        private System.Windows.Forms.LinkLabel causeListLinkLabel;
        private System.Windows.Forms.Label commentLabel;
        private System.Windows.Forms.Label formIDLabel;
        private System.Windows.Forms.Label dateTimeLabel;
        private TerraScan.UI.Controls.TerraScanTextBox commentTextBox;
        private TerraScan.UI.Controls.TerraScanButton ErrorCloseButton;
        private System.Windows.Forms.Panel CloseButtonPanel;
    }
}