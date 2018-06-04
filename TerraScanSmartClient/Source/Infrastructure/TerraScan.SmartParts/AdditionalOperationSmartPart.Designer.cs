namespace TerraScan.SmartParts
{
    partial class AdditionalOperationSmartPart
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
            this.components = new System.ComponentModel.Container();
            this.CommentButtonMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AttachmentButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CommentButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AttachmentToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.AdditionalSmartPartMenuStrip = new System.Windows.Forms.MenuStrip();
            this.AttachmentButtonMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdditionalSmartPartMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CommentButtonMenuItem
            // 
            this.CommentButtonMenuItem.Name = "CommentButtonMenuItem";
            this.CommentButtonMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.CommentButtonMenuItem.Size = new System.Drawing.Size(144, 20);
            this.CommentButtonMenuItem.Text = "CommentButtonMenuItem";
            this.CommentButtonMenuItem.Visible = false;
            this.CommentButtonMenuItem.Click += new System.EventHandler(CommentButtonMenuItem_Click);
            // 
            // AttachmentButton
            // 
            this.AttachmentButton.ActualPermission = false;
            this.AttachmentButton.ApplyDisableBehaviour = false;
            this.AttachmentButton.AutoEllipsis = true;
            this.AttachmentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(150)))), ((int)(((byte)(94)))));
            this.AttachmentButton.BorderColor = System.Drawing.Color.Wheat;
            this.AttachmentButton.CommentPriority = false;
            this.AttachmentButton.EnableAutoPrint = false;
            this.AttachmentButton.FilterStatus = false;
            this.AttachmentButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AttachmentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AttachmentButton.FocusRectangleEnabled = true;
            this.AttachmentButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttachmentButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AttachmentButton.ImageSelected = false;
            this.AttachmentButton.Location = new System.Drawing.Point(3, 10);
            this.AttachmentButton.MaximumSize = new System.Drawing.Size(98, 28);
            this.AttachmentButton.MinimumSize = new System.Drawing.Size(98, 28);
            this.AttachmentButton.Name = "AttachmentButton";
            this.AttachmentButton.NewPadding = 5;
            this.AttachmentButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Open;
            this.AttachmentButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.Print;
            this.AttachmentButton.Size = new System.Drawing.Size(98, 28);
            this.AttachmentButton.StatusIndicator = false;
            this.AttachmentButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AttachmentButton.StatusOffText = null;
            this.AttachmentButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.AttachmentButton.StatusOnText = null;
            this.AttachmentButton.TabIndex = 2;
            this.AttachmentButton.TabStop = false;
            this.AttachmentButton.Text = "Attachment";
            this.AttachmentButton.UseVisualStyleBackColor = false;
            this.AttachmentButton.Click += new System.EventHandler(this.AttachmentButton_Click);
            this.AttachmentButton.MouseHover += new System.EventHandler(this.AttachmentButton_MouseHover);
            // 
            // CommentButton
            // 
            this.CommentButton.ActualPermission = false;
            this.CommentButton.ApplyDisableBehaviour = false;
            this.CommentButton.AutoEllipsis = true;
            this.CommentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(150)))), ((int)(((byte)(94)))));
            this.CommentButton.BorderColor = System.Drawing.Color.Wheat;
            this.CommentButton.CommentPriority = false;
            this.CommentButton.EnableAutoPrint = false;
            this.CommentButton.FilterStatus = false;
            this.CommentButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CommentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CommentButton.FocusRectangleEnabled = true;
            this.CommentButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommentButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CommentButton.ImageSelected = false;
            this.CommentButton.Location = new System.Drawing.Point(107, 10);
            this.CommentButton.MaximumSize = new System.Drawing.Size(98, 28);
            this.CommentButton.MinimumSize = new System.Drawing.Size(98, 28);
            this.CommentButton.Name = "CommentButton";
            this.CommentButton.NewPadding = 5;
            this.CommentButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Open;
            this.CommentButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.Print;
            this.CommentButton.Size = new System.Drawing.Size(98, 28);
            this.CommentButton.StatusIndicator = false;
            this.CommentButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CommentButton.StatusOffText = null;
            this.CommentButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CommentButton.StatusOnText = null;
            this.CommentButton.TabIndex = 3;
            this.CommentButton.TabStop = false;
            this.CommentButton.Text = "Comment";
            this.CommentButton.UseVisualStyleBackColor = false;
            this.CommentButton.Click += new System.EventHandler(this.CommentButton_Click);
            // 
            // AdditionalSmartPartMenuStrip
            // 
            this.AdditionalSmartPartMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttachmentButtonMenuItem,
            this.CommentButtonMenuItem});
            this.AdditionalSmartPartMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.AdditionalSmartPartMenuStrip.Name = "AdditionalSmartPartMenuStrip";
            this.AdditionalSmartPartMenuStrip.Size = new System.Drawing.Size(248, 24);
            this.AdditionalSmartPartMenuStrip.TabIndex = 125;
            this.AdditionalSmartPartMenuStrip.Text = "menuStrip1";
            this.AdditionalSmartPartMenuStrip.Visible = false;
            // 
            // AttachmentButtonMenuItem
            // 
            this.AttachmentButtonMenuItem.Name = "AttachmentButtonMenuItem";
            this.AttachmentButtonMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.AttachmentButtonMenuItem.Size = new System.Drawing.Size(155, 20);
            this.AttachmentButtonMenuItem.Text = "AttachmentButtonMenuItem";
            this.AttachmentButtonMenuItem.Visible = false;
            this.AttachmentButtonMenuItem.Click += new System.EventHandler(AttachmentButtonMenuItem_Click);
            // 
            // AdditionalOperationSmartPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.AdditionalSmartPartMenuStrip);
            this.Controls.Add(this.AttachmentButton);
            this.Controls.Add(this.CommentButton);
            this.Name = "AdditionalOperationSmartPart";
            this.Size = new System.Drawing.Size(248, 48);
            this.AdditionalSmartPartMenuStrip.ResumeLayout(false);
            this.AdditionalSmartPartMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        protected internal TerraScan.UI.Controls.TerraScanButton AttachmentButton;
        protected internal TerraScan.UI.Controls.TerraScanButton CommentButton;
        private System.Windows.Forms.ToolTip AttachmentToolTip;
        private System.Windows.Forms.MenuStrip AdditionalSmartPartMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem AttachmentButtonMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CommentButtonMenuItem;

    }
}
