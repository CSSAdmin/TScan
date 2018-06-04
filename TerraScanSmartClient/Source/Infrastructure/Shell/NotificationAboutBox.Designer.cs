namespace TerraScan.UI
{
    partial class NotificationAboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationAboutBox));
            this.OkButton1 = new TerraScan.UI.Controls.TerraScanButton();
            this.ErrorTypeIconPictureBox = new System.Windows.Forms.PictureBox();
            this.HelperHeader = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NotificationStmtLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorTypeIconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OkButton1
            // 
            this.OkButton1.ActualPermission = false;
            this.OkButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton1.ApplyDisableBehaviour = false;
            this.OkButton1.AutoSize = true;
            this.OkButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.OkButton1.BorderColor = System.Drawing.Color.Wheat;
            this.OkButton1.CommentPriority = false;
            this.OkButton1.EnableAutoPrint = false;
            this.OkButton1.FilterStatus = false;
            this.OkButton1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.OkButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton1.FocusRectangleEnabled = true;
            this.OkButton1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.OkButton1.ImageSelected = false;
            this.OkButton1.Location = new System.Drawing.Point(387, 126);
            this.OkButton1.Name = "OkButton1";
            this.OkButton1.NewPadding = 5;
            this.OkButton1.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.OkButton1.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.OkButton1.Size = new System.Drawing.Size(85, 27);
            this.OkButton1.StatusIndicator = false;
            this.OkButton1.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.OkButton1.StatusOffText = null;
            this.OkButton1.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.OkButton1.StatusOnText = null;
            this.OkButton1.TabIndex = 30;
            this.OkButton1.Text = "OK";
            this.OkButton1.UseVisualStyleBackColor = false;
            this.OkButton1.Click += new System.EventHandler(this.OkButton1_Click);
            // 
            // ErrorTypeIconPictureBox
            // 
            this.ErrorTypeIconPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.ErrorTypeIconPictureBox.Location = new System.Drawing.Point(11, 43);
            this.ErrorTypeIconPictureBox.Name = "ErrorTypeIconPictureBox";
            this.ErrorTypeIconPictureBox.Size = new System.Drawing.Size(473, 58);
            this.ErrorTypeIconPictureBox.TabIndex = 33;
            this.ErrorTypeIconPictureBox.TabStop = false;
            // 
            // HelperHeader
            // 
            this.HelperHeader.AutoSize = true;
            this.HelperHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelperHeader.Location = new System.Drawing.Point(13, 53);
            this.HelperHeader.Name = "HelperHeader";
            this.HelperHeader.Size = new System.Drawing.Size(472, 13);
            this.HelperHeader.TabIndex = 36;
            this.HelperHeader.Text = "We’ve updated our Privacy Statement. Before you continue, please read our new ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(123, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "and familiarize yourself with the terms.";
            // 
            // NotificationStmtLink
            // 
            this.NotificationStmtLink.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.NotificationStmtLink.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotificationStmtLink.Location = new System.Drawing.Point(15, 66);
            this.NotificationStmtLink.Name = "NotificationStmtLink";
            this.NotificationStmtLink.Size = new System.Drawing.Size(111, 14);
            this.NotificationStmtLink.TabIndex = 222;
            this.NotificationStmtLink.TabStop = true;
            this.NotificationStmtLink.Text = "Privacy Statement";
            this.NotificationStmtLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NotificationStmtLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.NotificationStmtLink_LinkClicked);
            // 
            // NotificationAboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(499, 165);
            this.Controls.Add(this.NotificationStmtLink);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HelperHeader);
            this.Controls.Add(this.ErrorTypeIconPictureBox);
            this.Controls.Add(this.OkButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotificationAboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TerraScan T2";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorTypeIconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanButton OkButton1;
        private System.Windows.Forms.PictureBox ErrorTypeIconPictureBox;
        private System.Windows.Forms.Label HelperHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel NotificationStmtLink;


    }
}
