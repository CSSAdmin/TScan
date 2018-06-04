namespace D11020
{
    partial class F1070
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1070));
            this.WebSlicePanel = new System.Windows.Forms.Panel();
            this.WebSliceWebBrowser = new System.Windows.Forms.WebBrowser();
            this.WebSlicePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WebSlicePanel
            // 
            this.WebSlicePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WebSlicePanel.Controls.Add(this.WebSliceWebBrowser);
            this.WebSlicePanel.Location = new System.Drawing.Point(-2, -2);
            this.WebSlicePanel.Margin = new System.Windows.Forms.Padding(0);
            this.WebSlicePanel.Name = "WebSlicePanel";
            this.WebSlicePanel.Size = new System.Drawing.Size(365, 135);
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
            this.WebSliceWebBrowser.ScrollBarsEnabled = false;
            this.WebSliceWebBrowser.Size = new System.Drawing.Size(365, 135);
            this.WebSliceWebBrowser.TabIndex = 1;
            this.WebSliceWebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebSliceWebBrowser_DocumentCompleted);
            // 
            // F1070
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(358, 134);
            this.Controls.Add(this.WebSlicePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(366, 166);
            this.Name = "F1070";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "1070";
            this.Text = "TerraScan T2";
            this.Load += new System.EventHandler(this.F1070_Load);
            this.Resize += new System.EventHandler(F1070_Resize);
            this.WebSlicePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

      
        #endregion

        private System.Windows.Forms.Panel WebSlicePanel;
        public System.Windows.Forms.WebBrowser WebSliceWebBrowser;

    }
}