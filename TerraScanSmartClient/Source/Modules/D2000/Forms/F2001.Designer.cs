namespace D2000
{
    partial class F2001
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F2001));
            this.ParcelnumberLabel1 = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.LockButton = new TerraScan.UI.Controls.TerraScanButton();
            this.UnlockButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CloseButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.FormNoLabel = new System.Windows.Forms.Label();
            this.VisualStatusLabel = new System.Windows.Forms.Label();
            this.ParcelnumberLabel2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ParcelnumberLabel1
            // 
            this.ParcelnumberLabel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ParcelnumberLabel1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ParcelnumberLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ParcelnumberLabel1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ParcelnumberLabel1.Location = new System.Drawing.Point(109, 18);
            this.ParcelnumberLabel1.Name = "ParcelnumberLabel1";
            this.ParcelnumberLabel1.Size = new System.Drawing.Size(143, 23);
            this.ParcelnumberLabel1.TabIndex = 63;
            this.ParcelnumberLabel1.Text = " P111 - 2007 - GS";
            this.ParcelnumberLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DescriptionLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionLabel.ForeColor = System.Drawing.Color.Black;
            this.DescriptionLabel.Location = new System.Drawing.Point(14, 41);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(421, 28);
            this.DescriptionLabel.TabIndex = 64;
            this.DescriptionLabel.Text = "locking Appraisal Values will prevent any user or process from changing the appra" +
                "ised value of the current parcel record";
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LockButton
            // 
            this.LockButton.ActualPermission = false;
            this.LockButton.ApplyDisableBehaviour = false;
            this.LockButton.AutoSize = true;
            this.LockButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.LockButton.BorderColor = System.Drawing.Color.Wheat;
            this.LockButton.CommentPriority = false;
            this.LockButton.EnableAutoPrint = false;
            this.LockButton.FilterStatus = false;
            this.LockButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.LockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LockButton.FocusRectangleEnabled = true;
            this.LockButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LockButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LockButton.ImageSelected = false;
            this.LockButton.Location = new System.Drawing.Point(17, 131);
            this.LockButton.Name = "LockButton";
            this.LockButton.NewPadding = 5;
            this.LockButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.LockButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.LockButton.Size = new System.Drawing.Size(109, 30);
            this.LockButton.StatusIndicator = false;
            this.LockButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LockButton.StatusOffText = null;
            this.LockButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.LockButton.StatusOnText = null;
            this.LockButton.TabIndex = 66;
            this.LockButton.TabStop = false;
            this.LockButton.Text = "Lock";
            this.LockButton.UseVisualStyleBackColor = false;
            this.LockButton.Click += new System.EventHandler(this.LockButton_Click);
            // 
            // UnlockButton
            // 
            this.UnlockButton.ActualPermission = false;
            this.UnlockButton.ApplyDisableBehaviour = false;
            this.UnlockButton.AutoSize = true;
            this.UnlockButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.UnlockButton.BorderColor = System.Drawing.Color.Wheat;
            this.UnlockButton.CommentPriority = false;
            this.UnlockButton.EnableAutoPrint = false;
            this.UnlockButton.FilterStatus = false;
            this.UnlockButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.UnlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnlockButton.FocusRectangleEnabled = true;
            this.UnlockButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnlockButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.UnlockButton.ImageSelected = false;
            this.UnlockButton.Location = new System.Drawing.Point(172, 131);
            this.UnlockButton.Name = "UnlockButton";
            this.UnlockButton.NewPadding = 5;
            this.UnlockButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.UnlockButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.UnlockButton.Size = new System.Drawing.Size(110, 30);
            this.UnlockButton.StatusIndicator = false;
            this.UnlockButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.UnlockButton.StatusOffText = null;
            this.UnlockButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.UnlockButton.StatusOnText = null;
            this.UnlockButton.TabIndex = 67;
            this.UnlockButton.TabStop = false;
            this.UnlockButton.Text = "Unlock";
            this.UnlockButton.UseVisualStyleBackColor = false;
            this.UnlockButton.Click += new System.EventHandler(this.UnlockButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.ActualPermission = false;
            this.CloseButton.ApplyDisableBehaviour = false;
            this.CloseButton.AutoSize = true;
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CloseButton.BorderColor = System.Drawing.Color.Wheat;
            this.CloseButton.CommentPriority = false;
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.EnableAutoPrint = false;
            this.CloseButton.FilterStatus = false;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.FocusRectangleEnabled = true;
            this.CloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseButton.ImageSelected = false;
            this.CloseButton.Location = new System.Drawing.Point(326, 131);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.NewPadding = 5;
            this.CloseButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.CloseButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CloseButton.Size = new System.Drawing.Size(109, 30);
            this.CloseButton.StatusIndicator = false;
            this.CloseButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CloseButton.StatusOffText = null;
            this.CloseButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CloseButton.StatusOnText = null;
            this.CloseButton.TabIndex = 68;
            this.CloseButton.TabStop = false;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(17, 167);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(418, 2);
            this.FormLinePanel.TabIndex = 166;
            // 
            // FormNoLabel
            // 
            this.FormNoLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FormNoLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.FormNoLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.FormNoLabel.Location = new System.Drawing.Point(0, 171);
            this.FormNoLabel.Name = "FormNoLabel";
            this.FormNoLabel.Size = new System.Drawing.Size(66, 18);
            this.FormNoLabel.TabIndex = 167;
            this.FormNoLabel.Text = "2001";
            this.FormNoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VisualStatusLabel
            // 
            this.VisualStatusLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.VisualStatusLabel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisualStatusLabel.ForeColor = System.Drawing.Color.White;
            this.VisualStatusLabel.Location = new System.Drawing.Point(19, 80);
            this.VisualStatusLabel.Name = "VisualStatusLabel";
            this.VisualStatusLabel.Size = new System.Drawing.Size(416, 37);
            this.VisualStatusLabel.TabIndex = 168;
            this.VisualStatusLabel.Text = "Appraisal values - Unlocked";
            this.VisualStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ParcelnumberLabel2
            // 
            this.ParcelnumberLabel2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ParcelnumberLabel2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ParcelnumberLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(104)))), ((int)(((byte)(13)))));
            this.ParcelnumberLabel2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ParcelnumberLabel2.Location = new System.Drawing.Point(264, 18);
            this.ParcelnumberLabel2.Name = "ParcelnumberLabel2";
            this.ParcelnumberLabel2.Size = new System.Drawing.Size(45, 23);
            this.ParcelnumberLabel2.TabIndex = 169;
            this.ParcelnumberLabel2.Text = "2005";
            // 
            // label1
            // 
            this.label1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label1.Location = new System.Drawing.Point(251, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 23);
            this.label1.TabIndex = 170;
            this.label1.Text = "/";
            // 
            // F2001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(454, 193);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ParcelnumberLabel2);
            this.Controls.Add(this.VisualStatusLabel);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.UnlockButton);
            this.Controls.Add(this.LockButton);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.ParcelnumberLabel1);
            this.Controls.Add(this.FormNoLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(460, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(460, 225);
            this.Name = "F2001";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.F2001_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ParcelnumberLabel1;
        private System.Windows.Forms.Label DescriptionLabel;
        private TerraScan.UI.Controls.TerraScanButton LockButton;
        private TerraScan.UI.Controls.TerraScanButton UnlockButton;
        private TerraScan.UI.Controls.TerraScanButton CloseButton;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label FormNoLabel;
        private System.Windows.Forms.Label VisualStatusLabel;
        private System.Windows.Forms.Label ParcelnumberLabel2;
        private System.Windows.Forms.Label label1;
    }
}