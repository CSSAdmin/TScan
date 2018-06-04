namespace D49910
{
    partial class F49912
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F49912));
            this.LegalGridUserControl = new TerraScan.FSLegalGrid.FSLegalGridUserControl();
            //this.UserControlGridViewpanel = new System.Windows.Forms.Panel();
            this.UserControlGridViewpanel = new TerraScan.UI.Controls.TerraScanPanel(this.components);
            this.LegalPictureBox = new System.Windows.Forms.PictureBox();
            this.LegalToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.UserControlGridViewpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LegalPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LegalGridUserControl
            // 
            this.LegalGridUserControl.BackColor = System.Drawing.Color.White;
            this.LegalGridUserControl.CommentsHide = true;
            this.LegalGridUserControl.Location = new System.Drawing.Point(-1, -1);
            this.LegalGridUserControl.MultirowSetting = true;
            this.LegalGridUserControl.Name = "LegalGridUserControl";
            this.LegalGridUserControl.ParentFormId = 0;
            this.LegalGridUserControl.SecGridBoolean = false;
            this.LegalGridUserControl.Size = new System.Drawing.Size(769, 110);
            this.LegalGridUserControl.TabIndex = 0;
            this.LegalGridUserControl.CommandImageCellClick += new TerraScan.FSLegalGrid.FSLegalGridUserControl.EventHandler(this.LegalGridUserControl_CommandImageCellClick);
            this.LegalGridUserControl.SectionTextChanged += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridTextChanged(this.LegalGridUserControl_SectionTextChanged);
            this.LegalGridUserControl.SectionKeyPressEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridKeyPressEventHandler(this.LegalGridUserControl_SectionKeyPressEvent);
            this.LegalGridUserControl.SectionGridSelectionChangeEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridSelectionChangeEventHandler(this.LegalGridUserControl_SectionGridSelectionChangeEvent);
            // 
            // UserControlGridViewpanel
            // 
            this.UserControlGridViewpanel.BackColor = System.Drawing.Color.White;
            this.UserControlGridViewpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserControlGridViewpanel.Controls.Add(this.LegalGridUserControl);
            this.UserControlGridViewpanel.Location = new System.Drawing.Point(0, 0);
            this.UserControlGridViewpanel.Name = "UserControlGridViewpanel";
            this.UserControlGridViewpanel.Size = new System.Drawing.Size(767, 108);
            this.UserControlGridViewpanel.TabIndex = 1;
            // 
            // LegalPictureBox
            // 
            this.LegalPictureBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LegalPictureBox.Location = new System.Drawing.Point(759, 0);
            this.LegalPictureBox.Name = "LegalPictureBox";
            this.LegalPictureBox.Size = new System.Drawing.Size(42, 109);
            this.LegalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LegalPictureBox.TabIndex = 15;
            this.LegalPictureBox.TabStop = false;
            this.LegalPictureBox.Click += new System.EventHandler(this.LegalPictureBox_Click);
            this.LegalPictureBox.MouseEnter += new System.EventHandler(this.LegalPictureBox_MouseEnter);
            // 
            // F49912
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.UserControlGridViewpanel);
            this.Controls.Add(this.LegalPictureBox);
            this.Name = "F49912";
            this.Size = new System.Drawing.Size(802, 110);
            this.Tag = "49912";
            this.Load += new System.EventHandler(this.F49912_Load);
            this.UserControlGridViewpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LegalPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TerraScan.FSLegalGrid.FSLegalGridUserControl LegalGridUserControl;
        //private System.Windows.Forms.Panel UserControlGridViewpanel;
        private TerraScan.UI.Controls.TerraScanPanel UserControlGridViewpanel;
        private System.Windows.Forms.PictureBox LegalPictureBox;
        private System.Windows.Forms.ToolTip LegalToolTip;
    }
}
