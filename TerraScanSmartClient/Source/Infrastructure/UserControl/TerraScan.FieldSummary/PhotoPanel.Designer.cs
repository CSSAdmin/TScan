namespace TerraScan.FieldSummary
{
    partial class PhotoPanel
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
            this.Imagepanel = new System.Windows.Forms.Panel();
            this.EventDatelabel = new System.Windows.Forms.Label();
            this.Functionlabel = new System.Windows.Forms.Label();
            this.Descriptionlabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Imagepanel
            // 
            this.Imagepanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Imagepanel.Location = new System.Drawing.Point(0, 0);
            this.Imagepanel.Name = "Imagepanel";
            this.Imagepanel.Size = new System.Drawing.Size(255, 161);
            this.Imagepanel.TabIndex = 0;
            // 
            // EventDatelabel
            // 
            this.EventDatelabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EventDatelabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EventDatelabel.ForeColor = System.Drawing.Color.Black;
            this.EventDatelabel.Location = new System.Drawing.Point(0, 163);
            this.EventDatelabel.Name = "EventDatelabel";
            this.EventDatelabel.Size = new System.Drawing.Size(255, 14);
            this.EventDatelabel.TabIndex = 1;
            this.EventDatelabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Functionlabel
            // 
            this.Functionlabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Functionlabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Functionlabel.ForeColor = System.Drawing.Color.Black;
            this.Functionlabel.Location = new System.Drawing.Point(3, 177);
            this.Functionlabel.Name = "Functionlabel";
            this.Functionlabel.Size = new System.Drawing.Size(255, 16);
            this.Functionlabel.TabIndex = 2;
            this.Functionlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Functionlabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // Descriptionlabel
            // 
            this.Descriptionlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Descriptionlabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descriptionlabel.ForeColor = System.Drawing.Color.Black;
            this.Descriptionlabel.Location = new System.Drawing.Point(3, 193);
            this.Descriptionlabel.Name = "Descriptionlabel";
            this.Descriptionlabel.Size = new System.Drawing.Size(255, 14);
            this.Descriptionlabel.TabIndex = 3;
            this.Descriptionlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PhotoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.Descriptionlabel);
            this.Controls.Add(this.Functionlabel);
            this.Controls.Add(this.EventDatelabel);
            this.Controls.Add(this.Imagepanel);
            this.Name = "PhotoPanel";
            this.Size = new System.Drawing.Size(255, 215);
            this.Load += new System.EventHandler(this.PhotoPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Imagepanel;
        private System.Windows.Forms.Label EventDatelabel;
        private System.Windows.Forms.Label Functionlabel;
        private System.Windows.Forms.Label Descriptionlabel;
    }
}
