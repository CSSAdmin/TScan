namespace TerraScan.SmartParts
{
    partial class BatchButtonSmartPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchButtonSmartPart));
            this.StopBatchButton = new System.Windows.Forms.PictureBox();
            this.RunBatchButton = new System.Windows.Forms.PictureBox();
            this.BatchButtonTypeImage = new System.Windows.Forms.ImageList(this.components);
            this.BatchButtonStatusToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.StopBatchButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RunBatchButton)).BeginInit();
            this.SuspendLayout();
            // 
            // StopBatchButton
            // 
            this.StopBatchButton.BackColor = System.Drawing.Color.White;
            this.StopBatchButton.Location = new System.Drawing.Point(39, 0);
            this.StopBatchButton.Name = "StopBatchButton";
            this.StopBatchButton.Size = new System.Drawing.Size(40, 40);
            this.StopBatchButton.TabIndex = 16;
            this.StopBatchButton.TabStop = false;
            this.StopBatchButton.Tag = "Stop";
            this.StopBatchButton.Click += new System.EventHandler(this.StopBatchButton_Click);
            this.StopBatchButton.MouseHover += new System.EventHandler(this.StopBatchButton_MouseHover);
            // 
            // RunBatchButton
            // 
            this.RunBatchButton.BackColor = System.Drawing.Color.White;
            this.RunBatchButton.Location = new System.Drawing.Point(0, 0);
            this.RunBatchButton.Name = "RunBatchButton";
            this.RunBatchButton.Size = new System.Drawing.Size(40, 40);
            this.RunBatchButton.TabIndex = 15;
            this.RunBatchButton.TabStop = false;
            this.RunBatchButton.Tag = "RunANDPause";
            this.RunBatchButton.Click += new System.EventHandler(this.RunBatchButton_Click);
            this.RunBatchButton.MouseHover += new System.EventHandler(this.RunBatchButton_MouseHover);
            // 
            // BatchButtonTypeImage
            // 
            this.BatchButtonTypeImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("BatchButtonTypeImage.ImageStream")));
            this.BatchButtonTypeImage.TransparentColor = System.Drawing.Color.White;
            this.BatchButtonTypeImage.Images.SetKeyName(0, "BatchButtonLoad.Image.jpg");
            this.BatchButtonTypeImage.Images.SetKeyName(1, "BatchButtonNoRecordStop.Image.jpg");
            this.BatchButtonTypeImage.Images.SetKeyName(2, "BatchButtonPause.Image.jpg");
            this.BatchButtonTypeImage.Images.SetKeyName(3, "BatchButtonStop.Image.jpg");
            this.BatchButtonTypeImage.Images.SetKeyName(4, "BatchButtonRun.Image.jpg");
            // 
            // BatchButtonSmartPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.StopBatchButton);
            this.Controls.Add(this.RunBatchButton);
            this.Name = "BatchButtonSmartPart";
            this.Size = new System.Drawing.Size(79, 40);
            ((System.ComponentModel.ISupportInitialize)(this.StopBatchButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RunBatchButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox StopBatchButton;
        private System.Windows.Forms.PictureBox RunBatchButton;
        private System.Windows.Forms.ImageList BatchButtonTypeImage;
        private System.Windows.Forms.ToolTip BatchButtonStatusToolTip;
    }
}
