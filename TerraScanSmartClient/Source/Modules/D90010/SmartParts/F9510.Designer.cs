namespace D90010
{
    partial class F9510
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
            this.WebSlicePanel = new System.Windows.Forms.Panel();
            this.WebSliceWebBrowser = new System.Windows.Forms.WebBrowser();
            this.WebSlicePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WebSlicePanel
            // 
            this.WebSlicePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.WebSlicePanel.Controls.Add(this.WebSliceWebBrowser);
            this.WebSlicePanel.Location = new System.Drawing.Point(-2, -1);
            this.WebSlicePanel.Margin = new System.Windows.Forms.Padding(0);
            this.WebSlicePanel.Name = "WebSlicePanel";
            this.WebSlicePanel.Size = new System.Drawing.Size(1613, 1366);
            this.WebSlicePanel.TabIndex = 70;
            // 
            // WebSliceWebBrowser
            // 
            this.WebSliceWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.WebSliceWebBrowser.Location = new System.Drawing.Point(-1, -1);
            this.WebSliceWebBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.WebSliceWebBrowser.Name = "WebSliceWebBrowser";
            this.WebSliceWebBrowser.Size = new System.Drawing.Size(1613, 1366);
            this.WebSliceWebBrowser.TabIndex = 67;
            this.WebSliceWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebSliceWebBrowser_Navigating);
            this.WebSliceWebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebSliceWebBrowser_DocumentCompleted);
            // 
            // F9510
            // 
            this.AccessibleName = "WebForm";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.WebSlicePanel);
            this.MinimumSize = new System.Drawing.Size(858, 682);
            this.Name = "F9510";
            this.Size = new System.Drawing.Size(1618, 1394);
            this.Tag = "9510";
            this.Load += new System.EventHandler(this.F9510_Load);
            this.WebSlicePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel WebSlicePanel;
        public System.Windows.Forms.WebBrowser WebSliceWebBrowser;




    }
}
