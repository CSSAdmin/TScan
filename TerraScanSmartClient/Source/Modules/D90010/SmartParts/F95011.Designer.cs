namespace D90010
{
    partial class F95011
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
            this.WebSliceFormPictureBox = new System.Windows.Forms.PictureBox();
            this.WebSlicePanel = new System.Windows.Forms.Panel();
            this.WebSliceWebBrowser = new System.Windows.Forms.WebBrowser();
            this.WebsliceFormToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.WebSliceFormPictureBox)).BeginInit();
            this.WebSlicePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WebSliceFormPictureBox
            // 
            this.WebSliceFormPictureBox.Location = new System.Drawing.Point(759, 0);
            this.WebSliceFormPictureBox.Name = "WebSliceFormPictureBox";
            this.WebSliceFormPictureBox.Size = new System.Drawing.Size(42, 150);
            this.WebSliceFormPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WebSliceFormPictureBox.TabIndex = 68;
            this.WebSliceFormPictureBox.TabStop = false;
            this.WebSliceFormPictureBox.Click += new System.EventHandler(this.AuditTrailPictureBox_Click);
            this.WebSliceFormPictureBox.MouseHover += new System.EventHandler(this.AuditTrailPictureBox_MouseHover);
            // 
            // WebSlicePanel
            // 
            this.WebSlicePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WebSlicePanel.Controls.Add(this.WebSliceWebBrowser);
            this.WebSlicePanel.Location = new System.Drawing.Point(0, 0);
            this.WebSlicePanel.Margin = new System.Windows.Forms.Padding(0);
            this.WebSlicePanel.Name = "WebSlicePanel";
            this.WebSlicePanel.Size = new System.Drawing.Size(768, 150);
            this.WebSlicePanel.TabIndex = 69;
            // 
            // WebSliceWebBrowser
            // 
            this.WebSliceWebBrowser.Location = new System.Drawing.Point(-3, -3);
            this.WebSliceWebBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.WebSliceWebBrowser.Name = "WebSliceWebBrowser";
            this.WebSliceWebBrowser.Size = new System.Drawing.Size(776, 148);
            this.WebSliceWebBrowser.TabIndex = 67;
            this.WebSliceWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebSliceWebBrowser_Navigating);
            this.WebSliceWebBrowser.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.WebSliceWebBrowser_PreviewKeyDown);
            this.WebSliceWebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebSliceWebBrowser_DocumentCompleted);
            // 
            // F95011
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.WebSlicePanel);
            this.Controls.Add(this.WebSliceFormPictureBox);
            this.Name = "F95011";
            this.Size = new System.Drawing.Size(804, 150);
            this.Tag = "95011";
            this.Load += new System.EventHandler(this.F95011_Load);
            this.Resize += new System.EventHandler(this.F95011_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.WebSliceFormPictureBox)).EndInit();
            this.WebSlicePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox WebSliceFormPictureBox;
        private System.Windows.Forms.Panel WebSlicePanel;
        private System.Windows.Forms.WebBrowser WebSliceWebBrowser;
        private System.Windows.Forms.ToolTip WebsliceFormToolTip;
    }
}
