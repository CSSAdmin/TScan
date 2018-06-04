namespace D9065
{
    partial class F9066
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
            this.HelpLink = new System.Windows.Forms.LinkLabel();
            this.LinePanel = new System.Windows.Forms.Panel();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PreviewButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CheckInButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormHeaderWorkSpace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.PreviewPanel = new System.Windows.Forms.Panel();
            this.PreviewHeaderLabel = new System.Windows.Forms.Label();
            this.ParcelCountLabel = new System.Windows.Forms.Label();
            this.PreviewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpLink.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLink.Location = new System.Drawing.Point(416, 645);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(38, 15);
            this.HelpLink.TabIndex = 210;
            this.HelpLink.TabStop = true;
            this.HelpLink.Text = "Help";
            this.HelpLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.HelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // LinePanel
            // 
            this.LinePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.LinePanel.Location = new System.Drawing.Point(21, 640);
            this.LinePanel.Name = "LinePanel";
            this.LinePanel.Size = new System.Drawing.Size(808, 3);
            this.LinePanel.TabIndex = 212;
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AccessibleDescription = "0";
            this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(24, 644);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(35, 15);
            this.FormIDLabel.TabIndex = 211;
            this.FormIDLabel.Text = "9066";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(20, 426);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 0);
            this.panel1.TabIndex = 221;
            // 
            // PreviewButton
            // 
            this.PreviewButton.ActualPermission = false;
            this.PreviewButton.ApplyDisableBehaviour = false;
            this.PreviewButton.AutoEllipsis = true;
            this.PreviewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.PreviewButton.BorderColor = System.Drawing.Color.Wheat;
            this.PreviewButton.CommentPriority = false;
            this.PreviewButton.EnableAutoPrint = false;
            this.PreviewButton.FilterStatus = false;
            this.PreviewButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.PreviewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreviewButton.FocusRectangleEnabled = true;
            this.PreviewButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviewButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PreviewButton.ImageSelected = false;
            this.PreviewButton.Location = new System.Drawing.Point(20, 20);
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.NewPadding = 5;
            this.PreviewButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Edit;
            this.PreviewButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.PreviewButton.Size = new System.Drawing.Size(98, 28);
            this.PreviewButton.StatusIndicator = false;
            this.PreviewButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PreviewButton.StatusOffText = null;
            this.PreviewButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.PreviewButton.StatusOnText = null;
            this.PreviewButton.TabIndex = 223;
            this.PreviewButton.TabStop = false;
            this.PreviewButton.Tag = "NEW";
            this.PreviewButton.Text = "Preview";
            this.PreviewButton.UseVisualStyleBackColor = false;
            this.PreviewButton.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // CheckInButton
            // 
            this.CheckInButton.ActualPermission = false;
            this.CheckInButton.ApplyDisableBehaviour = false;
            this.CheckInButton.AutoEllipsis = true;
            this.CheckInButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CheckInButton.BorderColor = System.Drawing.Color.Wheat;
            this.CheckInButton.CommentPriority = false;
            this.CheckInButton.EnableAutoPrint = false;
            this.CheckInButton.FilterStatus = false;
            this.CheckInButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CheckInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckInButton.FocusRectangleEnabled = true;
            this.CheckInButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckInButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CheckInButton.ImageSelected = false;
            this.CheckInButton.Location = new System.Drawing.Point(130, 20);
            this.CheckInButton.Name = "CheckInButton";
            this.CheckInButton.NewPadding = 5;
            this.CheckInButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Edit;
            this.CheckInButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CheckInButton.Size = new System.Drawing.Size(98, 28);
            this.CheckInButton.StatusIndicator = false;
            this.CheckInButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CheckInButton.StatusOffText = null;
            this.CheckInButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CheckInButton.StatusOnText = null;
            this.CheckInButton.TabIndex = 224;
            this.CheckInButton.TabStop = false;
            this.CheckInButton.Tag = "NEW";
            this.CheckInButton.Text = "CheckIn";
            this.CheckInButton.UseVisualStyleBackColor = false;
            this.CheckInButton.Click += new System.EventHandler(this.CheckInButton_Click);
            // 
            // FormHeaderWorkSpace
            // 
            this.FormHeaderWorkSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FormHeaderWorkSpace.Location = new System.Drawing.Point(516, 0);
            this.FormHeaderWorkSpace.Name = "FormHeaderWorkSpace";
            this.FormHeaderWorkSpace.Size = new System.Drawing.Size(348, 52);
            this.FormHeaderWorkSpace.TabIndex = 227;
            this.FormHeaderWorkSpace.TabStop = false;
            this.FormHeaderWorkSpace.Text = "deckWorkspace1";
            // 
            // PreviewPanel
            // 
            this.PreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreviewPanel.Controls.Add(this.PreviewHeaderLabel);
            this.PreviewPanel.Controls.Add(this.ParcelCountLabel);
            this.PreviewPanel.Location = new System.Drawing.Point(20, 78);
            this.PreviewPanel.Name = "PreviewPanel";
            this.PreviewPanel.Size = new System.Drawing.Size(774, 535);
            this.PreviewPanel.TabIndex = 228;
            // 
            // PreviewHeaderLabel
            // 
            this.PreviewHeaderLabel.AutoSize = true;
            this.PreviewHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.PreviewHeaderLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.PreviewHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.PreviewHeaderLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PreviewHeaderLabel.Location = new System.Drawing.Point(4, 4);
            this.PreviewHeaderLabel.Name = "PreviewHeaderLabel";
            this.PreviewHeaderLabel.Size = new System.Drawing.Size(95, 14);
            this.PreviewHeaderLabel.TabIndex = 2;
            this.PreviewHeaderLabel.Text = "Preview Report:";
            // 
            // ParcelCountLabel
            // 
            this.ParcelCountLabel.AutoSize = true;
            this.ParcelCountLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.ParcelCountLabel.Location = new System.Drawing.Point(29, 42);
            this.ParcelCountLabel.Name = "ParcelCountLabel";
            this.ParcelCountLabel.Size = new System.Drawing.Size(10, 14);
            this.ParcelCountLabel.TabIndex = 1;
            this.ParcelCountLabel.Text = " ";
            // 
            // F9066
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PreviewPanel);
            this.Controls.Add(this.FormHeaderWorkSpace);
            this.Controls.Add(this.CheckInButton);
            this.Controls.Add(this.PreviewButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.LinePanel);
            this.Controls.Add(this.FormIDLabel);
            this.MinimumSize = new System.Drawing.Size(850, 707);
            this.Name = "F9066";
            this.Size = new System.Drawing.Size(864, 710);
            this.Tag = "9066";
            this.Load += new System.EventHandler(this.F9066_Load);
            this.PreviewPanel.ResumeLayout(false);
            this.PreviewPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel HelpLink;
        private System.Windows.Forms.Panel LinePanel;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.Panel panel1;
        private TerraScan.UI.Controls.TerraScanButton PreviewButton;
        private TerraScan.UI.Controls.TerraScanButton CheckInButton;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FormHeaderWorkSpace;
        private System.Windows.Forms.Panel PreviewPanel;
        private System.Windows.Forms.Label ParcelCountLabel;
        private System.Windows.Forms.Label PreviewHeaderLabel;
    }
}
