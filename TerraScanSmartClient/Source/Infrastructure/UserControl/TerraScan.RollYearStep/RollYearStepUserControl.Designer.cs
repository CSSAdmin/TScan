namespace TerraScan.RollYearStep
{
    partial class RollYearStepUserControl
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
            this.StepNumberpanel = new System.Windows.Forms.Panel();
            this.StepNumberLabel = new System.Windows.Forms.Label();
            this.StepNumberTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.StepDescriptionLabel = new System.Windows.Forms.Label();
            this.DescriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.RunDatePanel = new System.Windows.Forms.Panel();
            this.RunDateLabel = new System.Windows.Forms.Label();
            this.RunDateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.StepRunByPanel = new System.Windows.Forms.Panel();
            this.StepRunByLabel = new System.Windows.Forms.Label();
            this.StepRunByTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.StepButtonpanel = new System.Windows.Forms.Panel();
            this.StepButton = new TerraScan.UI.Controls.TerraScanButton();
            this.Footerpanel = new System.Windows.Forms.Panel();
            this.StepNumberpanel.SuspendLayout();
            this.DescriptionPanel.SuspendLayout();
            this.RunDatePanel.SuspendLayout();
            this.StepRunByPanel.SuspendLayout();
            this.StepButtonpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StepNumberpanel
            // 
            this.StepNumberpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.StepNumberpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StepNumberpanel.Controls.Add(this.StepNumberLabel);
            this.StepNumberpanel.Controls.Add(this.StepNumberTextBox);
            this.StepNumberpanel.Location = new System.Drawing.Point(-3, -1);
            this.StepNumberpanel.Name = "StepNumberpanel";
            this.StepNumberpanel.Size = new System.Drawing.Size(140, 45);
            this.StepNumberpanel.TabIndex = 11;
            // 
            // StepNumberLabel
            // 
            this.StepNumberLabel.AutoSize = true;
            this.StepNumberLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.StepNumberLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.StepNumberLabel.Location = new System.Drawing.Point(0, 0);
            this.StepNumberLabel.Name = "StepNumberLabel";
            this.StepNumberLabel.Size = new System.Drawing.Size(82, 14);
            this.StepNumberLabel.TabIndex = 21;
            this.StepNumberLabel.Text = "Step Number:";
            this.StepNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StepNumberTextBox
            // 
            this.StepNumberTextBox.AllowClick = true;
            this.StepNumberTextBox.AllowNegativeSign = false;
            this.StepNumberTextBox.ApplyCFGFormat = false;
            this.StepNumberTextBox.ApplyCurrencyFormat = false;
            this.StepNumberTextBox.ApplyFocusColor = true;
            this.StepNumberTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.StepNumberTextBox.ApplyNegativeStandard = true;
            this.StepNumberTextBox.ApplyParentFocusColor = true;
            this.StepNumberTextBox.ApplyTimeFormat = false;
            this.StepNumberTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.StepNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StepNumberTextBox.CFromatWihoutSymbol = false;
            this.StepNumberTextBox.CheckForEmpty = false;
            this.StepNumberTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.StepNumberTextBox.Digits = -1;
            this.StepNumberTextBox.EmptyDecimalValue = false;
            this.StepNumberTextBox.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepNumberTextBox.IsEditable = false;
            this.StepNumberTextBox.IsQueryableFileld = false;
            this.StepNumberTextBox.Location = new System.Drawing.Point(27, 14);
            this.StepNumberTextBox.LockKeyPress = true;
            this.StepNumberTextBox.MaxLength = 10;
            this.StepNumberTextBox.Name = "StepNumberTextBox";
            this.StepNumberTextBox.PersistDefaultColor = false;
            this.StepNumberTextBox.Precision = 2;
            this.StepNumberTextBox.QueryingFileldName = "";
            this.StepNumberTextBox.ReadOnly = true;
            this.StepNumberTextBox.SetColorFlag = false;
            this.StepNumberTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.StepNumberTextBox.Size = new System.Drawing.Size(90, 28);
            this.StepNumberTextBox.SpecialCharacter = "%";
            this.StepNumberTextBox.TabIndex = 14;
            this.StepNumberTextBox.TabStop = false;
            this.StepNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.StepNumberTextBox.TextCustomFormat = "$#,##0.00";
            this.StepNumberTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Smallint;
            this.StepNumberTextBox.WholeInteger = false;
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.BackColor = System.Drawing.Color.White;
            this.DescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionPanel.Controls.Add(this.StepDescriptionLabel);
            this.DescriptionPanel.Controls.Add(this.DescriptionTextBox);
            this.DescriptionPanel.Location = new System.Drawing.Point(136, -1);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(615, 45);
            this.DescriptionPanel.TabIndex = 12;
            // 
            // StepDescriptionLabel
            // 
            this.StepDescriptionLabel.AutoSize = true;
            this.StepDescriptionLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.StepDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.StepDescriptionLabel.Location = new System.Drawing.Point(0, 0);
            this.StepDescriptionLabel.Name = "StepDescriptionLabel";
            this.StepDescriptionLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StepDescriptionLabel.Size = new System.Drawing.Size(101, 14);
            this.StepDescriptionLabel.TabIndex = 22;
            this.StepDescriptionLabel.Text = "Step Description:";
            this.StepDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.DescriptionTextBox.IsEditable = false;
            this.DescriptionTextBox.IsQueryableFileld = false;
            this.DescriptionTextBox.Location = new System.Drawing.Point(15, 18);
            this.DescriptionTextBox.LockKeyPress = true;
            this.DescriptionTextBox.MaxLength = 150;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.PersistDefaultColor = false;
            this.DescriptionTextBox.Precision = 2;
            this.DescriptionTextBox.QueryingFileldName = "";
            this.DescriptionTextBox.ReadOnly = true;
            this.DescriptionTextBox.SetColorFlag = false;
            this.DescriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescriptionTextBox.Size = new System.Drawing.Size(450, 16);
            this.DescriptionTextBox.SpecialCharacter = "%";
            this.DescriptionTextBox.TabIndex = 14;
            this.DescriptionTextBox.TabStop = false;
            this.DescriptionTextBox.TextCustomFormat = "$#,##0.00";
            this.DescriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescriptionTextBox.WholeInteger = false;
            // 
            // RunDatePanel
            // 
            this.RunDatePanel.BackColor = System.Drawing.Color.White;
            this.RunDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RunDatePanel.Controls.Add(this.RunDateLabel);
            this.RunDatePanel.Controls.Add(this.RunDateTextBox);
            this.RunDatePanel.Location = new System.Drawing.Point(-3, 43);
            this.RunDatePanel.Name = "RunDatePanel";
            this.RunDatePanel.Size = new System.Drawing.Size(140, 42);
            this.RunDatePanel.TabIndex = 13;
            // 
            // RunDateLabel
            // 
            this.RunDateLabel.AutoSize = true;
            this.RunDateLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.RunDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.RunDateLabel.Location = new System.Drawing.Point(0, 1);
            this.RunDateLabel.Name = "RunDateLabel";
            this.RunDateLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RunDateLabel.Size = new System.Drawing.Size(86, 14);
            this.RunDateLabel.TabIndex = 21;
            this.RunDateLabel.Tag = "";
            this.RunDateLabel.Text = "Step Run Date:";
            this.RunDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RunDateTextBox
            // 
            this.RunDateTextBox.AllowClick = true;
            this.RunDateTextBox.AllowNegativeSign = false;
            this.RunDateTextBox.ApplyCFGFormat = false;
            this.RunDateTextBox.ApplyCurrencyFormat = false;
            this.RunDateTextBox.ApplyFocusColor = true;
            this.RunDateTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.RunDateTextBox.ApplyNegativeStandard = true;
            this.RunDateTextBox.ApplyParentFocusColor = true;
            this.RunDateTextBox.ApplyTimeFormat = false;
            this.RunDateTextBox.BackColor = System.Drawing.Color.White;
            this.RunDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RunDateTextBox.CFromatWihoutSymbol = false;
            this.RunDateTextBox.CheckForEmpty = false;
            this.RunDateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RunDateTextBox.Digits = -1;
            this.RunDateTextBox.EmptyDecimalValue = false;
            this.RunDateTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.RunDateTextBox.ForeColor = System.Drawing.Color.Maroon;
            this.RunDateTextBox.IsEditable = false;
            this.RunDateTextBox.IsQueryableFileld = false;
            this.RunDateTextBox.Location = new System.Drawing.Point(20, 18);
            this.RunDateTextBox.LockKeyPress = true;
            this.RunDateTextBox.MaxLength = 10;
            this.RunDateTextBox.Name = "RunDateTextBox";
            this.RunDateTextBox.PersistDefaultColor = false;
            this.RunDateTextBox.Precision = 2;
            this.RunDateTextBox.QueryingFileldName = "";
            this.RunDateTextBox.ReadOnly = true;
            this.RunDateTextBox.SetColorFlag = false;
            this.RunDateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.RunDateTextBox.Size = new System.Drawing.Size(100, 16);
            this.RunDateTextBox.SpecialCharacter = "%";
            this.RunDateTextBox.TabIndex = 14;
            this.RunDateTextBox.TabStop = false;
            this.RunDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RunDateTextBox.TextCustomFormat = "$#,##0.00";
            this.RunDateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.RunDateTextBox.WholeInteger = false;
            // 
            // StepRunByPanel
            // 
            this.StepRunByPanel.BackColor = System.Drawing.Color.White;
            this.StepRunByPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StepRunByPanel.Controls.Add(this.StepRunByLabel);
            this.StepRunByPanel.Controls.Add(this.StepRunByTextBox);
            this.StepRunByPanel.Location = new System.Drawing.Point(136, 43);
            this.StepRunByPanel.Name = "StepRunByPanel";
            this.StepRunByPanel.Size = new System.Drawing.Size(310, 42);
            this.StepRunByPanel.TabIndex = 14;
            // 
            // StepRunByLabel
            // 
            this.StepRunByLabel.AutoSize = true;
            this.StepRunByLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.StepRunByLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.StepRunByLabel.Location = new System.Drawing.Point(0, 0);
            this.StepRunByLabel.Name = "StepRunByLabel";
            this.StepRunByLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StepRunByLabel.Size = new System.Drawing.Size(75, 14);
            this.StepRunByLabel.TabIndex = 22;
            this.StepRunByLabel.Text = "Step Run By:";
            this.StepRunByLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StepRunByTextBox
            // 
            this.StepRunByTextBox.AllowClick = true;
            this.StepRunByTextBox.AllowNegativeSign = false;
            this.StepRunByTextBox.ApplyCFGFormat = false;
            this.StepRunByTextBox.ApplyCurrencyFormat = false;
            this.StepRunByTextBox.ApplyFocusColor = true;
            this.StepRunByTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.StepRunByTextBox.ApplyNegativeStandard = true;
            this.StepRunByTextBox.ApplyParentFocusColor = true;
            this.StepRunByTextBox.ApplyTimeFormat = false;
            this.StepRunByTextBox.BackColor = System.Drawing.Color.White;
            this.StepRunByTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StepRunByTextBox.CFromatWihoutSymbol = false;
            this.StepRunByTextBox.CheckForEmpty = false;
            this.StepRunByTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.StepRunByTextBox.Digits = -1;
            this.StepRunByTextBox.EmptyDecimalValue = false;
            this.StepRunByTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.StepRunByTextBox.ForeColor = System.Drawing.Color.Maroon;
            this.StepRunByTextBox.IsEditable = false;
            this.StepRunByTextBox.IsQueryableFileld = false;
            this.StepRunByTextBox.Location = new System.Drawing.Point(15, 18);
            this.StepRunByTextBox.LockKeyPress = true;
            this.StepRunByTextBox.MaxLength = 50;
            this.StepRunByTextBox.Name = "StepRunByTextBox";
            this.StepRunByTextBox.PersistDefaultColor = false;
            this.StepRunByTextBox.Precision = 2;
            this.StepRunByTextBox.QueryingFileldName = "";
            this.StepRunByTextBox.ReadOnly = true;
            this.StepRunByTextBox.SetColorFlag = false;
            this.StepRunByTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.StepRunByTextBox.Size = new System.Drawing.Size(200, 16);
            this.StepRunByTextBox.SpecialCharacter = "%";
            this.StepRunByTextBox.TabIndex = 14;
            this.StepRunByTextBox.TabStop = false;
            this.StepRunByTextBox.TextCustomFormat = "$#,##0.00";
            this.StepRunByTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.StepRunByTextBox.WholeInteger = false;
            // 
            // StepButtonpanel
            // 
            this.StepButtonpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.StepButtonpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StepButtonpanel.Controls.Add(this.StepButton);
            this.StepButtonpanel.Location = new System.Drawing.Point(445, 43);
            this.StepButtonpanel.Name = "StepButtonpanel";
            this.StepButtonpanel.Size = new System.Drawing.Size(306, 42);
            this.StepButtonpanel.TabIndex = 4;
            // 
            // StepButton
            // 
            this.StepButton.ActualPermission = false;
            this.StepButton.ApplyDisableBehaviour = false;
            this.StepButton.AutoEllipsis = true;
            this.StepButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.StepButton.BorderColor = System.Drawing.Color.Wheat;
            this.StepButton.CommentPriority = false;
            this.StepButton.EnableAutoPrint = false;
            this.StepButton.FilterStatus = false;
            this.StepButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.StepButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StepButton.FocusRectangleEnabled = true;
            this.StepButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.StepButton.ImageSelected = false;
            this.StepButton.Location = new System.Drawing.Point(37, 6);
            this.StepButton.Name = "StepButton";
            this.StepButton.NewPadding = 5;
            this.StepButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.StepButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.None;
            this.StepButton.Size = new System.Drawing.Size(224, 29);
            this.StepButton.StatusIndicator = false;
            this.StepButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.StepButton.StatusOffText = null;
            this.StepButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.StepButton.StatusOnText = null;
            this.StepButton.TabIndex = 20;
            this.StepButton.TabStop = false;
            this.StepButton.UseVisualStyleBackColor = false;
            this.StepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // Footerpanel
            // 
            this.Footerpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(37)))), ((int)(((byte)(65)))));
            this.Footerpanel.ForeColor = System.Drawing.Color.White;
            this.Footerpanel.Location = new System.Drawing.Point(-1, 85);
            this.Footerpanel.Name = "Footerpanel";
            this.Footerpanel.Size = new System.Drawing.Size(752, 12);
            this.Footerpanel.TabIndex = 5;
            // 
            // RollYearStepUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.StepButtonpanel);
            this.Controls.Add(this.StepRunByPanel);
            this.Controls.Add(this.DescriptionPanel);
            this.Controls.Add(this.StepNumberpanel);
            this.Controls.Add(this.Footerpanel);
            this.Controls.Add(this.RunDatePanel);
            this.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "RollYearStepUserControl";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(750, 97);
            this.Load += new System.EventHandler(this.RollYearStepUserControl_Load);
            this.StepNumberpanel.ResumeLayout(false);
            this.StepNumberpanel.PerformLayout();
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.RunDatePanel.ResumeLayout(false);
            this.RunDatePanel.PerformLayout();
            this.StepRunByPanel.ResumeLayout(false);
            this.StepRunByPanel.PerformLayout();
            this.StepButtonpanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel StepNumberpanel;
        private TerraScan.UI.Controls.TerraScanTextBox StepNumberTextBox;   
        private System.Windows.Forms.Panel DescriptionPanel;
        private System.Windows.Forms.Label StepNumberLabel;
        private TerraScan.UI.Controls.TerraScanTextBox DescriptionTextBox;   
        private System.Windows.Forms.Label StepDescriptionLabel;
        private System.Windows.Forms.Panel RunDatePanel;
        private System.Windows.Forms.Label RunDateLabel;
        private TerraScan.UI.Controls.TerraScanTextBox RunDateTextBox;   
        private System.Windows.Forms.Panel StepRunByPanel;
        private System.Windows.Forms.Label StepRunByLabel;
        private TerraScan.UI.Controls.TerraScanTextBox StepRunByTextBox;
        private System.Windows.Forms.Panel StepButtonpanel;
        private System.Windows.Forms.Panel Footerpanel;
        private TerraScan.UI.Controls.TerraScanButton StepButton;
    }
}
