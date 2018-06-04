namespace D3200
{
    partial class F3299
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F3299));
            this.WebSlicePanel = new System.Windows.Forms.Panel();
            this.WebSliceWebBrowser = new System.Windows.Forms.WebBrowser();
            this.FooterPanel = new System.Windows.Forms.Panel();
            this.formIDLabel = new System.Windows.Forms.Label();
            this.OKButton = new TerraScan.UI.Controls.TerraScanButton();
            this.WebSlicePanel.SuspendLayout();
            this.FooterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WebSlicePanel
            // 
            this.WebSlicePanel.Controls.Add(this.WebSliceWebBrowser);
            this.WebSlicePanel.Location = new System.Drawing.Point(-2, -2);
            this.WebSlicePanel.Margin = new System.Windows.Forms.Padding(0);
            this.WebSlicePanel.Name = "WebSlicePanel";
            this.WebSlicePanel.Size = new System.Drawing.Size(419, 241);
            this.WebSlicePanel.TabIndex = 68;
            // 
            // WebSliceWebBrowser
            // 
            this.WebSliceWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.WebSliceWebBrowser.Location = new System.Drawing.Point(-1, -1);
            this.WebSliceWebBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.WebSliceWebBrowser.Name = "WebSliceWebBrowser";
            this.WebSliceWebBrowser.Size = new System.Drawing.Size(421, 240);
            this.WebSliceWebBrowser.TabIndex = 1;
            this.WebSliceWebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebSliceWebBrowser_DocumentCompleted);
            // 
            // FooterPanel
            // 
            this.FooterPanel.Controls.Add(this.formIDLabel);
            this.FooterPanel.Controls.Add(this.OKButton);
            this.FooterPanel.Location = new System.Drawing.Point(0, 238);
            this.FooterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(419, 38);
            this.FooterPanel.TabIndex = 69;
            // 
            // formIDLabel
            // 
            this.formIDLabel.AutoSize = true;
            this.formIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.formIDLabel.Location = new System.Drawing.Point(6, 16);
            this.formIDLabel.Name = "formIDLabel";
            this.formIDLabel.Size = new System.Drawing.Size(35, 15);
            this.formIDLabel.TabIndex = 30;
            this.formIDLabel.Text = "3299";
            // 
            // OKButton
            // 
            this.OKButton.ActualPermission = false;
            this.OKButton.ApplyDisableBehaviour = false;
            this.OKButton.AutoSize = true;
            this.OKButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.OKButton.BorderColor = System.Drawing.Color.Wheat;
            this.OKButton.CommentPriority = false;
            this.OKButton.EnableAutoPrint = false;
            this.OKButton.FilterStatus = false;
            this.OKButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OKButton.FocusRectangleEnabled = true;
            this.OKButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.OKButton.ImageSelected = false;
            this.OKButton.Location = new System.Drawing.Point(308, 6);
            this.OKButton.Name = "OKButton";
            this.OKButton.NewPadding = 5;
            this.OKButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.OKButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.OKButton.Size = new System.Drawing.Size(97, 27);
            this.OKButton.StatusIndicator = false;
            this.OKButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.OKButton.StatusOffText = null;
            this.OKButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.OKButton.StatusOnText = null;
            this.OKButton.TabIndex = 6;
            this.OKButton.TabStop = false;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // F3299
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(416, 275);
            this.Controls.Add(this.FooterPanel);
            this.Controls.Add(this.WebSlicePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(366, 166);
            this.Name = "F3299";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "3299";
            this.Text = "Characteristics Check";
            this.Load += new System.EventHandler(this.F3299_Load);
            this.Resize += new System.EventHandler(this.F3299_Resize);
            this.WebSlicePanel.ResumeLayout(false);
            this.FooterPanel.ResumeLayout(false);
            this.FooterPanel.PerformLayout();
            this.ResumeLayout(false);

        }

      
        #endregion

        private System.Windows.Forms.Panel WebSlicePanel;
        public System.Windows.Forms.WebBrowser WebSliceWebBrowser;
        private System.Windows.Forms.Panel FooterPanel;
        private TerraScan.UI.Controls.TerraScanButton OKButton;
        private System.Windows.Forms.Label formIDLabel;

    }
}