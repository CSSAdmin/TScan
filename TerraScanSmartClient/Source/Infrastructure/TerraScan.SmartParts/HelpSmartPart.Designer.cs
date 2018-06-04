namespace TerraScan.SmartParts
{
    partial class HelpSmartPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpSmartPart));
            this.HelpButton = new System.Windows.Forms.Button();
            this.HelpLinkLabel = new TerraScan.UI.Controls.TerraScanLinkLabel();
            this.HelpLinkMenuStrip = new System.Windows.Forms.MenuStrip();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpLinkMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // HelpButton
            // 
            this.HelpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpButton.AutoEllipsis = true;
            this.HelpButton.BackColor = System.Drawing.Color.White;
            this.HelpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.HelpButton.FlatAppearance.BorderSize = 0;
            this.HelpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HelpButton.Font = new System.Drawing.Font("Verdana", 5F, System.Drawing.FontStyle.Bold);
            this.HelpButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.HelpButton.Image = ((System.Drawing.Image)(resources.GetObject("HelpButton.Image")));
            this.HelpButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.HelpButton.Location = new System.Drawing.Point(1, 1);
            this.HelpButton.Margin = new System.Windows.Forms.Padding(0);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(38, 38);
            this.HelpButton.TabIndex = 0;
            this.HelpButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.HelpButton.UseVisualStyleBackColor = false;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // HelpLinkLabel
            // 
            this.HelpLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpLinkLabel.AutoSize = true;
            this.HelpLinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLinkLabel.FormDllName = null;
            this.HelpLinkLabel.FormId = 0;
            this.HelpLinkLabel.Location = new System.Drawing.Point(4, 13);
            this.HelpLinkLabel.MenuName = null;
            this.HelpLinkLabel.Name = "HelpLinkLabel";
            this.HelpLinkLabel.PermissionOpen = 0;
            this.HelpLinkLabel.Size = new System.Drawing.Size(32, 15);
            this.HelpLinkLabel.TabIndex = 125;
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
            this.HelpLinkMenuStrip.Size = new System.Drawing.Size(40, 24);
            this.HelpLinkMenuStrip.TabIndex = 214;
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
            // HelpSmartPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.HelpLinkMenuStrip);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.HelpLinkLabel);
            this.Name = "HelpSmartPart";
            this.Size = new System.Drawing.Size(40, 40);
            this.HelpLinkMenuStrip.ResumeLayout(false);
            this.HelpLinkMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button HelpButton;
        private TerraScan.UI.Controls.TerraScanLinkLabel HelpLinkLabel;
        private System.Windows.Forms.MenuStrip HelpLinkMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
    }
}
