namespace D1100
{
    partial class F1111
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1111));
            this.AffadavitValidationPanel = new System.Windows.Forms.Panel();
            this.AffadavitCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.UnverifiedButton = new TerraScan.UI.Controls.TerraScanButton();
            this.RejectedButton = new TerraScan.UI.Controls.TerraScanButton();
            this.ApprovedButton = new TerraScan.UI.Controls.TerraScanButton();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.formIDLabel = new System.Windows.Forms.Label();
            this.LinePanel = new System.Windows.Forms.Panel();
            this.UserDetailsLabel = new System.Windows.Forms.Label();
            this.AffadavitValidationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // AffadavitValidationPanel
            // 
            this.AffadavitValidationPanel.Controls.Add(this.UserDetailsLabel);
            this.AffadavitValidationPanel.Controls.Add(this.AffadavitCancelButton);
            this.AffadavitValidationPanel.Controls.Add(this.UnverifiedButton);
            this.AffadavitValidationPanel.Controls.Add(this.RejectedButton);
            this.AffadavitValidationPanel.Controls.Add(this.ApprovedButton);
            this.AffadavitValidationPanel.Controls.Add(this.StatusLabel);
            this.AffadavitValidationPanel.Controls.Add(this.formIDLabel);
            this.AffadavitValidationPanel.Controls.Add(this.LinePanel);
            this.AffadavitValidationPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AffadavitValidationPanel.Location = new System.Drawing.Point(0, 2);
            this.AffadavitValidationPanel.Name = "AffadavitValidationPanel";
            this.AffadavitValidationPanel.Size = new System.Drawing.Size(408, 124);
            this.AffadavitValidationPanel.TabIndex = 0;
            // 
            // AffadavitCancelButton
            // 
            this.AffadavitCancelButton.ActualPermission = false;
            this.AffadavitCancelButton.ApplyDisableBehaviour = false;
            this.AffadavitCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.AffadavitCancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.AffadavitCancelButton.CommentPriority = false;
            this.AffadavitCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AffadavitCancelButton.EnableAutoPrint = false;
            this.AffadavitCancelButton.FilterStatus = false;
            this.AffadavitCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AffadavitCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AffadavitCancelButton.FocusRectangleEnabled = true;
            this.AffadavitCancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AffadavitCancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AffadavitCancelButton.ImageSelected = false;
            this.AffadavitCancelButton.Location = new System.Drawing.Point(302, 68);
            this.AffadavitCancelButton.Name = "AffadavitCancelButton";
            this.AffadavitCancelButton.NewPadding = 5;
            this.AffadavitCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.AffadavitCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.AffadavitCancelButton.Size = new System.Drawing.Size(92, 30);
            this.AffadavitCancelButton.StatusIndicator = false;
            this.AffadavitCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AffadavitCancelButton.StatusOffText = null;
            this.AffadavitCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AffadavitCancelButton.StatusOnText = null;
            this.AffadavitCancelButton.TabIndex = 4;
            this.AffadavitCancelButton.Text = "Cancel";
            this.AffadavitCancelButton.UseVisualStyleBackColor = false;
            // 
            // UnverifiedButton
            // 
            this.UnverifiedButton.ActualPermission = false;
            this.UnverifiedButton.ApplyDisableBehaviour = false;
            this.UnverifiedButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.UnverifiedButton.BorderColor = System.Drawing.Color.Wheat;
            this.UnverifiedButton.CommentPriority = false;
            this.UnverifiedButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UnverifiedButton.EnableAutoPrint = false;
            this.UnverifiedButton.FilterStatus = false;
            this.UnverifiedButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.UnverifiedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnverifiedButton.FocusRectangleEnabled = true;
            this.UnverifiedButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnverifiedButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.UnverifiedButton.ImageSelected = false;
            this.UnverifiedButton.Location = new System.Drawing.Point(205, 68);
            this.UnverifiedButton.Name = "UnverifiedButton";
            this.UnverifiedButton.NewPadding = 5;
            this.UnverifiedButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.UnverifiedButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.UnverifiedButton.Size = new System.Drawing.Size(92, 30);
            this.UnverifiedButton.StatusIndicator = false;
            this.UnverifiedButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.UnverifiedButton.StatusOffText = null;
            this.UnverifiedButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.UnverifiedButton.StatusOnText = null;
            this.UnverifiedButton.TabIndex = 3;
            this.UnverifiedButton.Text = "Unverified";
            this.UnverifiedButton.UseVisualStyleBackColor = false;
            this.UnverifiedButton.Click += new System.EventHandler(this.UnverifiedButton_Click);
            // 
            // RejectedButton
            // 
            this.RejectedButton.ActualPermission = false;
            this.RejectedButton.ApplyDisableBehaviour = false;
            this.RejectedButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.RejectedButton.BorderColor = System.Drawing.Color.Wheat;
            this.RejectedButton.CommentPriority = false;
            this.RejectedButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.RejectedButton.EnableAutoPrint = false;
            this.RejectedButton.FilterStatus = false;
            this.RejectedButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.RejectedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RejectedButton.FocusRectangleEnabled = true;
            this.RejectedButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RejectedButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.RejectedButton.ImageSelected = false;
            this.RejectedButton.Location = new System.Drawing.Point(108, 68);
            this.RejectedButton.Name = "RejectedButton";
            this.RejectedButton.NewPadding = 5;
            this.RejectedButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.RejectedButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.RejectedButton.Size = new System.Drawing.Size(92, 30);
            this.RejectedButton.StatusIndicator = false;
            this.RejectedButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RejectedButton.StatusOffText = null;
            this.RejectedButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.RejectedButton.StatusOnText = null;
            this.RejectedButton.TabIndex = 2;
            this.RejectedButton.Text = "Rejected";
            this.RejectedButton.UseVisualStyleBackColor = false;
            this.RejectedButton.Click += new System.EventHandler(this.RejectedButton_Click);
            // 
            // ApprovedButton
            // 
            this.ApprovedButton.ActualPermission = false;
            this.ApprovedButton.ApplyDisableBehaviour = false;
            this.ApprovedButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ApprovedButton.BorderColor = System.Drawing.Color.Wheat;
            this.ApprovedButton.CommentPriority = false;
            this.ApprovedButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ApprovedButton.EnableAutoPrint = false;
            this.ApprovedButton.FilterStatus = false;
            this.ApprovedButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ApprovedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApprovedButton.FocusRectangleEnabled = true;
            this.ApprovedButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApprovedButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ApprovedButton.ImageSelected = false;
            this.ApprovedButton.Location = new System.Drawing.Point(11, 68);
            this.ApprovedButton.Name = "ApprovedButton";
            this.ApprovedButton.NewPadding = 5;
            this.ApprovedButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.ApprovedButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ApprovedButton.Size = new System.Drawing.Size(92, 30);
            this.ApprovedButton.StatusIndicator = false;
            this.ApprovedButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ApprovedButton.StatusOffText = null;
            this.ApprovedButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ApprovedButton.StatusOnText = null;
            this.ApprovedButton.TabIndex = 1;
            this.ApprovedButton.Text = "Approved";
            this.ApprovedButton.UseVisualStyleBackColor = false;
            this.ApprovedButton.Click += new System.EventHandler(this.ApprovedButton_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.Color.DarkGray;
            this.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StatusLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Location = new System.Drawing.Point(11, 7);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(383, 34);
            this.StatusLabel.TabIndex = 103;
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // formIDLabel
            // 
            this.formIDLabel.AutoSize = true;
            this.formIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formIDLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.formIDLabel.Location = new System.Drawing.Point(8, 104);
            this.formIDLabel.Name = "formIDLabel";
            this.formIDLabel.Size = new System.Drawing.Size(35, 15);
            this.formIDLabel.TabIndex = 102;
            this.formIDLabel.Text = "1111";
            // 
            // LinePanel
            // 
            this.LinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.LinePanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LinePanel.Location = new System.Drawing.Point(11, 102);
            this.LinePanel.Name = "LinePanel";
            this.LinePanel.Size = new System.Drawing.Size(383, 2);
            this.LinePanel.TabIndex = 97;
            // 
            // UserDetailsLabel
            // 
            this.UserDetailsLabel.BackColor = System.Drawing.Color.DarkGray;
            this.UserDetailsLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserDetailsLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UserDetailsLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserDetailsLabel.Location = new System.Drawing.Point(11, 45);
            this.UserDetailsLabel.Name = "UserDetailsLabel";
            this.UserDetailsLabel.Size = new System.Drawing.Size(383, 19);
            this.UserDetailsLabel.TabIndex = 105;
            this.UserDetailsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // F1111
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.AffadavitCancelButton;
            this.ClientSize = new System.Drawing.Size(406, 127);
            this.Controls.Add(this.AffadavitValidationPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F1111";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan – Affidavit Validation";
            this.Load += new System.EventHandler(this.F1111_Load);
            this.AffadavitValidationPanel.ResumeLayout(false);
            this.AffadavitValidationPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel AffadavitValidationPanel;
        private System.Windows.Forms.Panel LinePanel;
        private System.Windows.Forms.Label formIDLabel;
        private System.Windows.Forms.Label StatusLabel;
        private TerraScan.UI.Controls.TerraScanButton AffadavitCancelButton;
        private TerraScan.UI.Controls.TerraScanButton UnverifiedButton;
        private TerraScan.UI.Controls.TerraScanButton RejectedButton;
        private TerraScan.UI.Controls.TerraScanButton ApprovedButton;
        private System.Windows.Forms.Label UserDetailsLabel;
    }
}