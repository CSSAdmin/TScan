namespace D20000
{
    partial class F25006
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F25006));
            this.ParcelNaviationPanel = new System.Windows.Forms.Panel();
            this.CurrentParcelPanel = new System.Windows.Forms.Panel();
            this.PreviousButtonPanel = new System.Windows.Forms.Panel();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.NextButtonPanel = new System.Windows.Forms.Panel();
            this.NextButton = new System.Windows.Forms.Button();
            this.ParcelNavigationPictureBox = new System.Windows.Forms.PictureBox();
            this.ParcelNavigationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PreviousButtonPanel.SuspendLayout();
            this.NextButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParcelNavigationPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ParcelNaviationPanel
            // 
            this.ParcelNaviationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ParcelNaviationPanel.Location = new System.Drawing.Point(0, -155);
            this.ParcelNaviationPanel.Name = "ParcelNaviationPanel";
            this.ParcelNaviationPanel.Size = new System.Drawing.Size(773, 34);
            this.ParcelNaviationPanel.TabIndex = 1;
            // 
            // CurrentParcelPanel
            // 
            this.CurrentParcelPanel.BackColor = System.Drawing.Color.Gray;
            this.CurrentParcelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentParcelPanel.Location = new System.Drawing.Point(39, 0);
            this.CurrentParcelPanel.Name = "CurrentParcelPanel";
            this.CurrentParcelPanel.Size = new System.Drawing.Size(692, 34);
            this.CurrentParcelPanel.TabIndex = 3;
            // 
            // PreviousButtonPanel
            // 
            this.PreviousButtonPanel.BackColor = System.Drawing.Color.White;
            this.PreviousButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreviousButtonPanel.Controls.Add(this.PreviousButton);
            this.PreviousButtonPanel.Location = new System.Drawing.Point(0, 0);
            this.PreviousButtonPanel.Name = "PreviousButtonPanel";
            this.PreviousButtonPanel.Size = new System.Drawing.Size(40, 34);
            this.PreviousButtonPanel.TabIndex = 4;
            // 
            // PreviousButton
            // 
            this.PreviousButton.AutoSize = true;
            this.PreviousButton.BackColor = System.Drawing.Color.Transparent;
            this.PreviousButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PreviousButton.BackgroundImage")));
            this.PreviousButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PreviousButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.PreviousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreviousButton.Location = new System.Drawing.Point(-1, -1);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(40, 34);
            this.PreviousButton.TabIndex = 12;
            this.PreviousButton.UseVisualStyleBackColor = false;
            this.PreviousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // NextButtonPanel
            // 
            this.NextButtonPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.NextButtonPanel.BackColor = System.Drawing.Color.White;
            this.NextButtonPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NextButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NextButtonPanel.Controls.Add(this.NextButton);
            this.NextButtonPanel.Location = new System.Drawing.Point(729, 0);
            this.NextButtonPanel.Name = "NextButtonPanel";
            this.NextButtonPanel.Size = new System.Drawing.Size(39, 34);
            this.NextButtonPanel.TabIndex = 5;
            // 
            // NextButton
            // 
            this.NextButton.AutoSize = true;
            this.NextButton.BackColor = System.Drawing.Color.Transparent;
            this.NextButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("NextButton.BackgroundImage")));
            this.NextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NextButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.NextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextButton.Location = new System.Drawing.Point(-1, -1);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(39, 34);
            this.NextButton.TabIndex = 13;
            this.NextButton.UseVisualStyleBackColor = false;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // ParcelNavigationPictureBox
            // 
            this.ParcelNavigationPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.ParcelNavigationPictureBox.Location = new System.Drawing.Point(761, 0);
            this.ParcelNavigationPictureBox.Name = "ParcelNavigationPictureBox";
            this.ParcelNavigationPictureBox.Size = new System.Drawing.Size(40, 34);
            this.ParcelNavigationPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ParcelNavigationPictureBox.TabIndex = 5;
            this.ParcelNavigationPictureBox.TabStop = false;
            this.ParcelNavigationPictureBox.Click += new System.EventHandler(this.ParcelNavigationPictureBox_Click);
            this.ParcelNavigationPictureBox.MouseEnter += new System.EventHandler(this.ParcelNavigationPictureBox_MouseEnter);
            // 
            // F25006
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NextButtonPanel);
            this.Controls.Add(this.PreviousButtonPanel);
            this.Controls.Add(this.CurrentParcelPanel);
            this.Controls.Add(this.ParcelNaviationPanel);
            this.Controls.Add(this.ParcelNavigationPictureBox);
            this.Name = "F25006";
            this.Size = new System.Drawing.Size(800, 34);
            this.Tag = "25006";
            this.Load += new System.EventHandler(this.F25006_Load);
            this.PreviousButtonPanel.ResumeLayout(false);
            this.PreviousButtonPanel.PerformLayout();
            this.NextButtonPanel.ResumeLayout(false);
            this.NextButtonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParcelNavigationPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ParcelNaviationPanel;
        private System.Windows.Forms.Panel CurrentParcelPanel;
        private System.Windows.Forms.Panel PreviousButtonPanel;
        private System.Windows.Forms.Panel NextButtonPanel;
        private System.Windows.Forms.PictureBox ParcelNavigationPictureBox;
        private System.Windows.Forms.Button PreviousButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.ToolTip ParcelNavigationToolTip;
    }
}
