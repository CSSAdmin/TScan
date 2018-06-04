namespace TerraScan.SmartParts
{
    partial class FooterSmartPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FooterSmartPart));
            this.AuditlinkLabel = new System.Windows.Forms.LinkLabel();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.HelpButton = new System.Windows.Forms.Button();
            this.HelpLinkLabel = new TerraScan.UI.Controls.TerraScanLinkLabel();
            this.HelpLinkMenuStrip = new System.Windows.Forms.MenuStrip();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpLinkMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AuditlinkLabel
            // 
            this.AuditlinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AuditlinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuditlinkLabel.Location = new System.Drawing.Point(520, 6);
            this.AuditlinkLabel.Name = "AuditlinkLabel";
            this.AuditlinkLabel.Size = new System.Drawing.Size(325, 15);
            this.AuditlinkLabel.TabIndex = 4;
            this.AuditlinkLabel.TabStop = true;
            this.AuditlinkLabel.Text = "tTR_ExciseRate [ExciseRateID] 175";
            this.AuditlinkLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.AuditlinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AuditlinkLabel_LinkClicked);
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AccessibleDescription = "0";
            this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(14, 6);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(35, 15);
            this.FormIDLabel.TabIndex = 130;
            this.FormIDLabel.Text = "1101";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.panel5.Location = new System.Drawing.Point(18, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(825, 2);
            this.panel5.TabIndex = 0;
            this.panel5.TabStop = true;
            // 
            // HelpButton
            // 
            this.HelpButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.HelpButton.AutoEllipsis = true;
            this.HelpButton.BackColor = System.Drawing.Color.White;
            this.HelpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.HelpButton.FlatAppearance.BorderSize = 0;
            this.HelpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HelpButton.Font = new System.Drawing.Font("Verdana", 5F, System.Drawing.FontStyle.Bold);
            this.HelpButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.HelpButton.Image = ((System.Drawing.Image)(resources.GetObject("HelpButton.Image")));
            this.HelpButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.HelpButton.Location = new System.Drawing.Point(396, -7);
            this.HelpButton.Margin = new System.Windows.Forms.Padding(0);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(38, 38);
            this.HelpButton.TabIndex = 1;
            this.HelpButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.HelpButton.UseVisualStyleBackColor = false;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // HelpLinkLabel
            // 
            this.HelpLinkLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.HelpLinkLabel.AutoSize = true;
            this.HelpLinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLinkLabel.FormDllName = null;
            this.HelpLinkLabel.FormId = 0;
            this.HelpLinkLabel.Location = new System.Drawing.Point(399, 6);
            this.HelpLinkLabel.MenuName = null;
            this.HelpLinkLabel.Name = "HelpLinkLabel";
            this.HelpLinkLabel.PermissionOpen = 0;
            this.HelpLinkLabel.Size = new System.Drawing.Size(32, 15);
            this.HelpLinkLabel.TabIndex = 2;
            this.HelpLinkLabel.TabStop = true;
            this.HelpLinkLabel.Text = "Help";
            this.HelpLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.HelpLinkLabel.TextCustomFormat = "#,##0.00";
            this.HelpLinkLabel.ValidateType = TerraScan.UI.Controls.TerraScanLinkLabel.ControlValidationType.Text;
            this.HelpLinkLabel.Visible = false;
            this.HelpLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLinkLabel_LinkClicked);
            // 
            // HelpLinkMenuStrip
            // 
            this.HelpLinkMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpMenuItem});
            this.HelpLinkMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.HelpLinkMenuStrip.Name = "HelpLinkMenuStrip";
            this.HelpLinkMenuStrip.Size = new System.Drawing.Size(864, 24);
            this.HelpLinkMenuStrip.TabIndex = 3;
            this.HelpLinkMenuStrip.Text = "HelpLinkMenuStrip";
            this.HelpLinkMenuStrip.Visible = false;
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.HelpMenuItem.Size = new System.Drawing.Size(88, 20);
            this.HelpMenuItem.Text = "HelpMenuItem";
            this.HelpMenuItem.Visible = false;
            this.HelpMenuItem.Click += new System.EventHandler(this.HelpMenuItem_Click);
            // 
            // FooterSmartPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.HelpLinkMenuStrip);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.HelpLinkLabel);
            this.Controls.Add(this.AuditlinkLabel);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.panel5);
            this.Name = "FooterSmartPart";
            this.Size = new System.Drawing.Size(864, 33);
            this.HelpLinkMenuStrip.ResumeLayout(false);
            this.HelpLinkMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel AuditlinkLabel;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button HelpButton;
        private TerraScan.UI.Controls.TerraScanLinkLabel HelpLinkLabel;
        private System.Windows.Forms.MenuStrip HelpLinkMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
    }
}
