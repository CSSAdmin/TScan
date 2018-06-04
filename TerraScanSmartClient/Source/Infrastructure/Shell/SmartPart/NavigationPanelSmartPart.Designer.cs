namespace TerraScan.UI
{
    partial class NavigationPanelSmartPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigationPanelSmartPart));
            this.xpPanelGroup2 = new TerraScan.XPPanel.TerraScanXPPanelGroup();
            this.xpPanelGroup3 = new TerraScan.XPPanel.TerraScanXPPanelGroup();
            this.itemLayoutPanel1 = new TerraScan.XPPanel.ItemLayoutPanel();
            this.LogoutPanel = new System.Windows.Forms.Panel();
            this.LogOutLinkLabel = new System.Windows.Forms.LinkLabel();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.purpleGlyphsImageSet = new TerraScan.XPPanel.ImageSet();
            ((System.ComponentModel.ISupportInitialize)(this.xpPanelGroup2)).BeginInit();
            this.xpPanelGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpPanelGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemLayoutPanel1)).BeginInit();
            this.itemLayoutPanel1.SuspendLayout();
            this.LogoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // xpPanelGroup2
            // 
            this.xpPanelGroup2.AutoScroll = true;
            this.xpPanelGroup2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xpPanelGroup2.BackColor = System.Drawing.Color.Transparent;
            this.xpPanelGroup2.BorderMargin = new System.Drawing.Size(0, 0);
            this.xpPanelGroup2.Controls.Add(this.xpPanelGroup3);
            this.xpPanelGroup2.Controls.Add(this.itemLayoutPanel1);
            this.xpPanelGroup2.Location = new System.Drawing.Point(0, 0);
            this.xpPanelGroup2.Margin = new System.Windows.Forms.Padding(0);
            this.xpPanelGroup2.Name = "xpPanelGroup2";
            this.xpPanelGroup2.PanelGradient = ((TerraScan.XPPanel.GradientColor)(resources.GetObject("xpPanelGroup2.PanelGradient")));
            this.xpPanelGroup2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.xpPanelGroup2.Size = new System.Drawing.Size(172, 660);
            this.xpPanelGroup2.TabIndex = 10;
            // 
            // xpPanelGroup3
            // 
            this.xpPanelGroup3.AutoScroll = true;
            this.xpPanelGroup3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xpPanelGroup3.BackColor = System.Drawing.Color.Transparent;
            this.xpPanelGroup3.BorderMargin = new System.Drawing.Size(0, 0);
            this.xpPanelGroup3.Location = new System.Drawing.Point(0, 70);
            this.xpPanelGroup3.Margin = new System.Windows.Forms.Padding(0);
            this.xpPanelGroup3.Name = "xpPanelGroup3";
            this.xpPanelGroup3.PanelGradient = ((TerraScan.XPPanel.GradientColor)(resources.GetObject("xpPanelGroup3.PanelGradient")));
            this.xpPanelGroup3.Size = new System.Drawing.Size(172, 590);
            this.xpPanelGroup3.TabIndex = 19;
            // 
            // LogOutLinkLabel
            // 
            this.LogOutLinkLabel.AutoSize = true;
            this.LogOutLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogOutLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.LogOutLinkLabel.Location = new System.Drawing.Point(115, 43);
            this.LogOutLinkLabel.Name = "LogOutLinkLabel";
            this.LogOutLinkLabel.Size = new System.Drawing.Size(52, 13);
            this.LogOutLinkLabel.TabIndex = 9;
            this.LogOutLinkLabel.TabStop = true;
            this.LogOutLinkLabel.Text = "&Log Out";
            this.LogOutLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LogOutLinkLabel_LinkClicked);
            // 
            // itemLayoutPanel1
            // 
            this.itemLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.itemLayoutPanel1.BackgroundStyle = TerraScan.XPPanel.BackgroundStyle.Transparent;
            this.itemLayoutPanel1.BorderMargin = new System.Drawing.Size(0, 0);
            this.itemLayoutPanel1.Controls.Add(this.LogoutPanel);
            this.itemLayoutPanel1.ItemSpacing = 0;
            this.itemLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.itemLayoutPanel1.Name = "itemLayoutPanel1";
            this.itemLayoutPanel1.PanelGradient = ((TerraScan.XPPanel.GradientColor)(resources.GetObject("itemLayoutPanel1.PanelGradient")));
            this.itemLayoutPanel1.Size = new System.Drawing.Size(172, 62);
            this.itemLayoutPanel1.TabIndex = 18;
            // 
            // LogoutPanel
            // 
            this.LogoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.LogoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(80)))), ((int)(((byte)(129)))));
            this.LogoutPanel.Controls.Add(this.LogOutLinkLabel);
            this.LogoutPanel.Controls.Add(this.UserNameLabel);
            this.LogoutPanel.Controls.Add(this.DateTimeLabel);
            this.LogoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LogoutPanel.Name = "LogoutPanel";
            this.LogoutPanel.Size = new System.Drawing.Size(172, 62);
            this.LogoutPanel.TabIndex = 17;
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameLabel.ForeColor = System.Drawing.Color.White;
            this.UserNameLabel.Location = new System.Drawing.Point(5, 43);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(23, 14);
            this.UserNameLabel.TabIndex = 1;
            this.UserNameLabel.Text = "Bill";
            this.UserNameLabel.MouseEnter += new System.EventHandler(this.UserNameLabel_MouseEnter);
            // 
            // DateTimeLabel
            // 
            this.DateTimeLabel.AutoSize = true;
            this.DateTimeLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimeLabel.ForeColor = System.Drawing.Color.White;
            this.DateTimeLabel.Location = new System.Drawing.Point(7, 5);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(0, 14);
            this.DateTimeLabel.TabIndex = 0;
            // 
            // purpleGlyphsImageSet
            // 
            this.purpleGlyphsImageSet.Images.AddRange(new System.Drawing.Image[] {
            ((System.Drawing.Image)(resources.GetObject("purpleGlyphsImageSet.Images"))),
            ((System.Drawing.Image)(resources.GetObject("purpleGlyphsImageSet.Images1"))),
            ((System.Drawing.Image)(resources.GetObject("purpleGlyphsImageSet.Images2"))),
            ((System.Drawing.Image)(resources.GetObject("purpleGlyphsImageSet.Images3")))});
            this.purpleGlyphsImageSet.Size = new System.Drawing.Size(21, 21);
            this.purpleGlyphsImageSet.TransparentColor = System.Drawing.Color.Empty;
            // 
            // NavigationPanelSmartPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(213)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.xpPanelGroup2);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "NavigationPanelSmartPart";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(172, 667);
            this.Load += new System.EventHandler(this.NavigationPanelSmartPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xpPanelGroup2)).EndInit();
            this.xpPanelGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xpPanelGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemLayoutPanel1)).EndInit();
            this.itemLayoutPanel1.ResumeLayout(false);
            this.LogoutPanel.ResumeLayout(false);
            this.LogoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TerraScan.XPPanel.TerraScanXPPanelGroup xpPanelGroup2;
        private TerraScan.XPPanel.TerraScanXPPanelGroup xpPanelGroup3;
        private TerraScan.XPPanel.ItemLayoutPanel itemLayoutPanel1;
        private TerraScan.XPPanel.ImageSet purpleGlyphsImageSet;
        private System.Windows.Forms.Panel LogoutPanel;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.Label DateTimeLabel;
        private System.Windows.Forms.LinkLabel LogOutLinkLabel;
    }
}
