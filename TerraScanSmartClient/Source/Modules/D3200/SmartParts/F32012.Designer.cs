namespace D3200
{
    partial class F32012
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
            this.mainPanel = new TerraScan.UI.Controls.TerraScanPanel(this.components);
            this.CatalogPictureBox = new System.Windows.Forms.PictureBox();
            this.OverallPanel = new TerraScan.UI.Controls.TerraScanPanel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.CatalogPictureBox)).BeginInit();
            this.OverallPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(767, 574);
            this.mainPanel.TabIndex = 1;
            // 
            // CatalogPictureBox
            // 
            this.CatalogPictureBox.Location = new System.Drawing.Point(760, 0);
            this.CatalogPictureBox.Name = "CatalogPictureBox";
            this.CatalogPictureBox.Size = new System.Drawing.Size(42, 574);
            this.CatalogPictureBox.TabIndex = 198;
            this.CatalogPictureBox.TabStop = false;
            // 
            // OverallPanel
            // 
            this.OverallPanel.AutoScroll = true;
            this.OverallPanel.Controls.Add(this.mainPanel);
            this.OverallPanel.Controls.Add(this.CatalogPictureBox);
            this.OverallPanel.Location = new System.Drawing.Point(0, 0);
            this.OverallPanel.Name = "OverallPanel";
            this.OverallPanel.Size = new System.Drawing.Size(804, 574);
            this.OverallPanel.TabIndex = 0;
            // 
            // F32012
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OverallPanel);
            this.MinimumSize = new System.Drawing.Size(804, 596);
            this.Name = "F32012";
            this.Size = new System.Drawing.Size(804, 596);
            ((System.ComponentModel.ISupportInitialize)(this.CatalogPictureBox)).EndInit();
            this.OverallPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanPanel mainPanel;
        private System.Windows.Forms.PictureBox CatalogPictureBox;
        private TerraScan.UI.Controls.TerraScanPanel OverallPanel;

    }
}
