namespace D8900
{
    partial class F8910
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8910));
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.DescriptionTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.LocationNotesPanel = new System.Windows.Forms.Panel();
            this.LocationNotesTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.LocationNotesLabel = new System.Windows.Forms.Label();
            this.CorrectionsPanel = new System.Windows.Forms.Panel();
            this.CorrectionsTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CorrectionsLabel = new System.Windows.Forms.Label();
            this.GeneralPictureBox = new System.Windows.Forms.PictureBox();
            this.GDocWorkOrderGeneralToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.DescriptionPanel.SuspendLayout();
            this.LocationNotesPanel.SuspendLayout();
            this.CorrectionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GeneralPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.BackColor = System.Drawing.Color.Transparent;
            this.DescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionPanel.Controls.Add(this.DescriptionTextBox);
            this.DescriptionPanel.Controls.Add(this.DescriptionLabel);
            this.DescriptionPanel.Location = new System.Drawing.Point(0, 0);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(768, 60);
            this.DescriptionPanel.TabIndex = 1;
            this.DescriptionPanel.TabStop = true;
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.AllowClick = true;
            this.DescriptionTextBox.ApplyCFGFormat = false;
            this.DescriptionTextBox.ApplyCurrencyFormat = false;
            this.DescriptionTextBox.ApplyFocusColor = true;
            this.DescriptionTextBox.ApplyParentFocusColor = true;
            this.DescriptionTextBox.ApplyTimeFormat = false;
            this.DescriptionTextBox.BackColor = System.Drawing.Color.White;
            this.DescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DescriptionTextBox.CFromatWihoutSymbol = false;
            this.DescriptionTextBox.CheckForEmpty = false;
            this.DescriptionTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DescriptionTextBox.Digits = -1;
            this.DescriptionTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionTextBox.IsEditable = false;
            this.DescriptionTextBox.IsQueryableFileld = false;
            this.DescriptionTextBox.Location = new System.Drawing.Point(17, 18);
            this.DescriptionTextBox.LockKeyPress = false;
            this.DescriptionTextBox.MaxLength = 500;
            this.DescriptionTextBox.Multiline = true;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.PersistDefaultColor = false;
            this.DescriptionTextBox.Precision = 2;
            this.DescriptionTextBox.QueryingFileldName = "";
            this.DescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DescriptionTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DescriptionTextBox.Size = new System.Drawing.Size(748, 36);
            this.DescriptionTextBox.SpecialCharacter = "%";
            this.DescriptionTextBox.TabIndex = 2;
            this.DescriptionTextBox.TextCustomFormat = "$#,##0.00";
            this.DescriptionTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DescriptionTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DescriptionTextBox_KeyPress);
            this.DescriptionTextBox.TextChanged += new System.EventHandler(this.DescriptionTextBox_TextChanged);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.DescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DescriptionLabel.Location = new System.Drawing.Point(1, 1);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(73, 14);
            this.DescriptionLabel.TabIndex = 0;
            this.DescriptionLabel.Text = "Description:";
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LocationNotesPanel
            // 
            this.LocationNotesPanel.BackColor = System.Drawing.Color.Transparent;
            this.LocationNotesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LocationNotesPanel.Controls.Add(this.LocationNotesTextBox);
            this.LocationNotesPanel.Controls.Add(this.LocationNotesLabel);
            this.LocationNotesPanel.Location = new System.Drawing.Point(0, 59);
            this.LocationNotesPanel.Name = "LocationNotesPanel";
            this.LocationNotesPanel.Size = new System.Drawing.Size(768, 60);
            this.LocationNotesPanel.TabIndex = 3;
            this.LocationNotesPanel.TabStop = true;
            // 
            // LocationNotesTextBox
            // 
            this.LocationNotesTextBox.AllowClick = true;
            this.LocationNotesTextBox.ApplyCFGFormat = false;
            this.LocationNotesTextBox.ApplyCurrencyFormat = false;
            this.LocationNotesTextBox.ApplyFocusColor = true;
            this.LocationNotesTextBox.ApplyParentFocusColor = true;
            this.LocationNotesTextBox.ApplyTimeFormat = false;
            this.LocationNotesTextBox.BackColor = System.Drawing.Color.White;
            this.LocationNotesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LocationNotesTextBox.CFromatWihoutSymbol = false;
            this.LocationNotesTextBox.CheckForEmpty = false;
            this.LocationNotesTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.LocationNotesTextBox.Digits = -1;
            this.LocationNotesTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationNotesTextBox.IsEditable = false;
            this.LocationNotesTextBox.IsQueryableFileld = false;
            this.LocationNotesTextBox.Location = new System.Drawing.Point(17, 18);
            this.LocationNotesTextBox.LockKeyPress = false;
            this.LocationNotesTextBox.MaxLength = 500;
            this.LocationNotesTextBox.Multiline = true;
            this.LocationNotesTextBox.Name = "LocationNotesTextBox";
            this.LocationNotesTextBox.PersistDefaultColor = false;
            this.LocationNotesTextBox.Precision = 2;
            this.LocationNotesTextBox.QueryingFileldName = "";
            this.LocationNotesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LocationNotesTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.LocationNotesTextBox.Size = new System.Drawing.Size(748, 36);
            this.LocationNotesTextBox.SpecialCharacter = "%";
            this.LocationNotesTextBox.TabIndex = 4;
            this.LocationNotesTextBox.TextCustomFormat = "$#,##0.00";
            this.LocationNotesTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.LocationNotesTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LocationNotesTextBox_KeyPress);
            this.LocationNotesTextBox.TextChanged += new System.EventHandler(this.LocationNotesTextBox_TextChanged);
            // 
            // LocationNotesLabel
            // 
            this.LocationNotesLabel.AutoSize = true;
            this.LocationNotesLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.LocationNotesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.LocationNotesLabel.Location = new System.Drawing.Point(1, 1);
            this.LocationNotesLabel.Name = "LocationNotesLabel";
            this.LocationNotesLabel.Size = new System.Drawing.Size(92, 14);
            this.LocationNotesLabel.TabIndex = 0;
            this.LocationNotesLabel.Text = "Location Notes:";
            this.LocationNotesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CorrectionsPanel
            // 
            this.CorrectionsPanel.BackColor = System.Drawing.Color.Transparent;
            this.CorrectionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CorrectionsPanel.Controls.Add(this.CorrectionsTextBox);
            this.CorrectionsPanel.Controls.Add(this.CorrectionsLabel);
            this.CorrectionsPanel.Location = new System.Drawing.Point(0, 118);
            this.CorrectionsPanel.Name = "CorrectionsPanel";
            this.CorrectionsPanel.Size = new System.Drawing.Size(768, 60);
            this.CorrectionsPanel.TabIndex = 5;
            this.CorrectionsPanel.TabStop = true;
            // 
            // CorrectionsTextBox
            // 
            this.CorrectionsTextBox.AllowClick = true;
            this.CorrectionsTextBox.ApplyCFGFormat = false;
            this.CorrectionsTextBox.ApplyCurrencyFormat = false;
            this.CorrectionsTextBox.ApplyFocusColor = true;
            this.CorrectionsTextBox.ApplyParentFocusColor = true;
            this.CorrectionsTextBox.ApplyTimeFormat = false;
            this.CorrectionsTextBox.BackColor = System.Drawing.Color.White;
            this.CorrectionsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CorrectionsTextBox.CFromatWihoutSymbol = false;
            this.CorrectionsTextBox.CheckForEmpty = false;
            this.CorrectionsTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CorrectionsTextBox.Digits = -1;
            this.CorrectionsTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CorrectionsTextBox.IsEditable = false;
            this.CorrectionsTextBox.IsQueryableFileld = false;
            this.CorrectionsTextBox.Location = new System.Drawing.Point(17, 18);
            this.CorrectionsTextBox.LockKeyPress = false;
            this.CorrectionsTextBox.MaxLength = 500;
            this.CorrectionsTextBox.Multiline = true;
            this.CorrectionsTextBox.Name = "CorrectionsTextBox";
            this.CorrectionsTextBox.PersistDefaultColor = false;
            this.CorrectionsTextBox.Precision = 2;
            this.CorrectionsTextBox.QueryingFileldName = "";
            this.CorrectionsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CorrectionsTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CorrectionsTextBox.Size = new System.Drawing.Size(748, 36);
            this.CorrectionsTextBox.SpecialCharacter = "%";
            this.CorrectionsTextBox.TabIndex = 6;
            this.CorrectionsTextBox.TextCustomFormat = "$#,##0.00";
            this.CorrectionsTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CorrectionsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CorrectionsTextBox_KeyPress);
            this.CorrectionsTextBox.TextChanged += new System.EventHandler(this.CorrectionsTextBox_TextChanged);
            // 
            // CorrectionsLabel
            // 
            this.CorrectionsLabel.AutoSize = true;
            this.CorrectionsLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.CorrectionsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CorrectionsLabel.Location = new System.Drawing.Point(1, 1);
            this.CorrectionsLabel.Name = "CorrectionsLabel";
            this.CorrectionsLabel.Size = new System.Drawing.Size(127, 14);
            this.CorrectionsLabel.TabIndex = 0;
            this.CorrectionsLabel.Text = "Corrections / Actions:";
            this.CorrectionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GeneralPictureBox
            // 
            this.GeneralPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("GeneralPictureBox.Image")));
            this.GeneralPictureBox.Location = new System.Drawing.Point(761, 0);
            this.GeneralPictureBox.Name = "GeneralPictureBox";
            this.GeneralPictureBox.Size = new System.Drawing.Size(42, 178);
            this.GeneralPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GeneralPictureBox.TabIndex = 28;
            this.GeneralPictureBox.TabStop = false;
            this.GeneralPictureBox.Click += new System.EventHandler(this.GeneralPictureBox_Click);
            this.GeneralPictureBox.MouseEnter += new System.EventHandler(this.GeneralPictureBox_MouseEnter);
            // 
            // F8910
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.CorrectionsPanel);
            this.Controls.Add(this.LocationNotesPanel);
            this.Controls.Add(this.DescriptionPanel);
            this.Controls.Add(this.GeneralPictureBox);
            this.Name = "F8910";
            this.Size = new System.Drawing.Size(804, 178);
            this.Tag = "8910";
            this.Load += new System.EventHandler(this.F8910_Load);
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.LocationNotesPanel.ResumeLayout(false);
            this.LocationNotesPanel.PerformLayout();
            this.CorrectionsPanel.ResumeLayout(false);
            this.CorrectionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GeneralPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel DescriptionPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DescriptionTextBox;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Panel LocationNotesPanel;
        private TerraScan.UI.Controls.TerraScanTextBox LocationNotesTextBox;
        private System.Windows.Forms.Label LocationNotesLabel;
        private System.Windows.Forms.Panel CorrectionsPanel;
        private TerraScan.UI.Controls.TerraScanTextBox CorrectionsTextBox;
        private System.Windows.Forms.Label CorrectionsLabel;
        private System.Windows.Forms.PictureBox GeneralPictureBox;
        private System.Windows.Forms.ToolTip GDocWorkOrderGeneralToolTip;
    }
}
