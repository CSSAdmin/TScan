namespace TerraScan.SmartParts
{
    partial class RecordNavigatorSmartPart
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.RecordNavigationSmartPartMenuStrip = new System.Windows.Forms.MenuStrip();
            this.LeftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LastMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FirstMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RecordCountLabel = new System.Windows.Forms.Label();
            this.RecordIDLabel = new System.Windows.Forms.Label();
            this.LastButton = new System.Windows.Forms.Button();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.FirstButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.RecordOfLabel = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.RecordNavigationSmartPartMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.RecordNavigationSmartPartMenuStrip);
            this.panel3.Controls.Add(this.RecordCountLabel);
            this.panel3.Controls.Add(this.RecordIDLabel);
            this.panel3.Controls.Add(this.LastButton);
            this.panel3.Controls.Add(this.PreviousButton);
            this.panel3.Controls.Add(this.FirstButton);
            this.panel3.Controls.Add(this.NextButton);
            this.panel3.Controls.Add(this.RecordOfLabel);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel3.Size = new System.Drawing.Size(278, 52);
            this.panel3.TabIndex = 8;
            // 
            // RecordNavigationSmartPartMenuStrip
            // 
            this.RecordNavigationSmartPartMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LeftMenuItem,
            this.RightMenuItem,
            this.LastMenuItem,
            this.FirstMenuItem});
            this.RecordNavigationSmartPartMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.RecordNavigationSmartPartMenuStrip.Name = "RecordNavigationSmartPartMenuStrip";
            this.RecordNavigationSmartPartMenuStrip.Size = new System.Drawing.Size(278, 24);
            this.RecordNavigationSmartPartMenuStrip.TabIndex = 17;
            this.RecordNavigationSmartPartMenuStrip.Text = "menuStrip1";
            this.RecordNavigationSmartPartMenuStrip.Visible = false;
            // 
            // LeftMenuItem
            // 
            this.LeftMenuItem.Name = "LeftMenuItem";
            this.LeftMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.LeftMenuItem.Size = new System.Drawing.Size(69, 20);
            this.LeftMenuItem.Tag = "PREVIOUS";
            this.LeftMenuItem.Text = "PREVIOUS";
            this.LeftMenuItem.Visible = false;
            // 
            // RightMenuItem
            // 
            this.RightMenuItem.Name = "RightMenuItem";
            this.RightMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.RightMenuItem.Size = new System.Drawing.Size(44, 20);
            this.RightMenuItem.Tag = "NEXT";
            this.RightMenuItem.Text = "NEXT";
            this.RightMenuItem.Visible = false;
            // 
            // LastMenuItem
            // 
            this.LastMenuItem.Name = "LastMenuItem";
            this.LastMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)));
            this.LastMenuItem.Size = new System.Drawing.Size(43, 20);
            this.LastMenuItem.Tag = "LAST";
            this.LastMenuItem.Text = "LAST";
            this.LastMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LastMenuItem.Visible = false;
            // 
            // FirstMenuItem
            // 
            this.FirstMenuItem.Name = "FirstMenuItem";
            this.FirstMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)));
            this.FirstMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FirstMenuItem.Tag = "FIRST";
            this.FirstMenuItem.Text = "FIRST";
            this.FirstMenuItem.Visible = false;
            // 
            // RecordCountLabel
            // 
            this.RecordCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RecordCountLabel.BackColor = System.Drawing.Color.Transparent;
            this.RecordCountLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.RecordCountLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.RecordCountLabel.Location = new System.Drawing.Point(149, 22);
            this.RecordCountLabel.Name = "RecordCountLabel";
            this.RecordCountLabel.Size = new System.Drawing.Size(64, 13);
            this.RecordCountLabel.TabIndex = 15;
            this.RecordCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RecordIDLabel
            // 
            this.RecordIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RecordIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.RecordIDLabel.Location = new System.Drawing.Point(68, 22);
            this.RecordIDLabel.Name = "RecordIDLabel";
            this.RecordIDLabel.Size = new System.Drawing.Size(66, 15);
            this.RecordIDLabel.TabIndex = 14;
            this.RecordIDLabel.Text = "0";
            this.RecordIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LastButton
            // 
            this.LastButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LastButton.AutoEllipsis = true;
            this.LastButton.BackColor = System.Drawing.Color.Transparent;
            this.LastButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LastButton.FlatAppearance.BorderSize = 0;
            this.LastButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LastButton.Font = new System.Drawing.Font("Arial", 3.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LastButton.Image = global::TerraScan.SmartParts.Properties.Resources.LastButton;
            this.LastButton.Location = new System.Drawing.Point(242, 12);
            this.LastButton.Margin = new System.Windows.Forms.Padding(0);
            this.LastButton.Name = "LastButton";
            this.LastButton.Size = new System.Drawing.Size(23, 23);
            this.LastButton.TabIndex = 3;
            this.LastButton.TabStop = false;
            this.LastButton.Tag = "Last";
            this.LastButton.UseVisualStyleBackColor = false;
            this.LastButton.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // PreviousButton
            // 
            this.PreviousButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviousButton.AutoEllipsis = true;
            this.PreviousButton.BackColor = System.Drawing.Color.Transparent;
            this.PreviousButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PreviousButton.FlatAppearance.BorderSize = 0;
            this.PreviousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreviousButton.Font = new System.Drawing.Font("Arial", 3.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviousButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PreviousButton.Image = global::TerraScan.SmartParts.Properties.Resources.Previousbutton;
            this.PreviousButton.Location = new System.Drawing.Point(32, 12);
            this.PreviousButton.Margin = new System.Windows.Forms.Padding(0);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(23, 23);
            this.PreviousButton.TabIndex = 1;
            this.PreviousButton.TabStop = false;
            this.PreviousButton.Tag = "Previous";
            this.PreviousButton.UseVisualStyleBackColor = false;
            this.PreviousButton.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // FirstButton
            // 
            this.FirstButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FirstButton.AutoEllipsis = true;
            this.FirstButton.BackColor = System.Drawing.Color.Transparent;
            this.FirstButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.FirstButton.FlatAppearance.BorderSize = 0;
            this.FirstButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FirstButton.Font = new System.Drawing.Font("Arial", 3.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FirstButton.Image = global::TerraScan.SmartParts.Properties.Resources.First_button;
            this.FirstButton.Location = new System.Drawing.Point(2, 12);
            this.FirstButton.Margin = new System.Windows.Forms.Padding(0);
            this.FirstButton.Name = "FirstButton";
            this.FirstButton.Size = new System.Drawing.Size(23, 23);
            this.FirstButton.TabIndex = 0;
            this.FirstButton.TabStop = false;
            this.FirstButton.Tag = "First";
            this.FirstButton.UseVisualStyleBackColor = false;
            this.FirstButton.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextButton.AutoEllipsis = true;
            this.NextButton.BackColor = System.Drawing.Color.Transparent;
            this.NextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NextButton.FlatAppearance.BorderSize = 0;
            this.NextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextButton.Font = new System.Drawing.Font("Arial", 3.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.NextButton.Image = global::TerraScan.SmartParts.Properties.Resources.Nextbutton;
            this.NextButton.Location = new System.Drawing.Point(213, 12);
            this.NextButton.Margin = new System.Windows.Forms.Padding(0);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(23, 23);
            this.NextButton.TabIndex = 2;
            this.NextButton.TabStop = false;
            this.NextButton.Tag = "Next";
            this.NextButton.UseVisualStyleBackColor = false;
            this.NextButton.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // RecordOfLabel
            // 
            this.RecordOfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RecordOfLabel.BackColor = System.Drawing.Color.Transparent;
            this.RecordOfLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.RecordOfLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.RecordOfLabel.Location = new System.Drawing.Point(132, 22);
            this.RecordOfLabel.Name = "RecordOfLabel";
            this.RecordOfLabel.Size = new System.Drawing.Size(20, 13);
            this.RecordOfLabel.TabIndex = 16;
            this.RecordOfLabel.Text = "of";
            this.RecordOfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RecordNavigatorSmartPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel3);
            this.Name = "RecordNavigatorSmartPart";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(278, 52);
            this.Load += new System.EventHandler(this.RecordNavigatorSmartPart_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.RecordNavigationSmartPartMenuStrip.ResumeLayout(false);
            this.RecordNavigationSmartPartMenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Label RecordCountLabel;
        public System.Windows.Forms.Label RecordIDLabel;
        protected internal System.Windows.Forms.Button LastButton;
        protected internal System.Windows.Forms.Button PreviousButton;
        protected internal System.Windows.Forms.Button FirstButton;
        protected internal System.Windows.Forms.Button NextButton;
        public System.Windows.Forms.Label RecordOfLabel;
        private System.Windows.Forms.MenuStrip RecordNavigationSmartPartMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem LeftMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RightMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LastMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FirstMenuItem;

    }
}
