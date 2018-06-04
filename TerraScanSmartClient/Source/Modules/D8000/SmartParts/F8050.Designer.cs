namespace D8000
{
    partial class F8050
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8050));
            this.CommentPanel = new System.Windows.Forms.Panel();
            this.CommentTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CommentLabel = new System.Windows.Forms.Label();
            this.CommentPictureBox = new System.Windows.Forms.PictureBox();
            this.GDocCommentsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.CommentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CommentPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CommentPanel
            // 
            this.CommentPanel.BackColor = System.Drawing.Color.Transparent;
            this.CommentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CommentPanel.Controls.Add(this.CommentTextBox);
            this.CommentPanel.Controls.Add(this.CommentLabel);
            this.CommentPanel.Location = new System.Drawing.Point(0, 0);
            this.CommentPanel.Name = "CommentPanel";
            this.CommentPanel.Size = new System.Drawing.Size(768, 151);
            this.CommentPanel.TabIndex = 1;
            this.CommentPanel.TabStop = true;
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.AllowClick = true;
            this.CommentTextBox.AllowNegativeSign = false;
            this.CommentTextBox.ApplyCFGFormat = false;
            this.CommentTextBox.ApplyCurrencyFormat = false;
            this.CommentTextBox.ApplyFocusColor = true;
            this.CommentTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.CommentTextBox.ApplyNegativeStandard = true;
            this.CommentTextBox.ApplyParentFocusColor = true;
            this.CommentTextBox.ApplyTimeFormat = false;
            this.CommentTextBox.BackColor = System.Drawing.Color.White;
            this.CommentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommentTextBox.CFromatWihoutSymbol = false;
            this.CommentTextBox.CheckForEmpty = false;
            this.CommentTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CommentTextBox.Digits = -1;
            this.CommentTextBox.EmptyDecimalValue = false;
            this.CommentTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.CommentTextBox.ForeColor = System.Drawing.Color.Black;
            this.CommentTextBox.IsEditable = false;
            this.CommentTextBox.IsQueryableFileld = false;
            this.CommentTextBox.Location = new System.Drawing.Point(14, 17);
            this.CommentTextBox.LockKeyPress = false;
            this.CommentTextBox.MaxLength = 1000;
            this.CommentTextBox.Multiline = true;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.PersistDefaultColor = false;
            this.CommentTextBox.Precision = 2;
            this.CommentTextBox.QueryingFileldName = "";
            this.CommentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CommentTextBox.Size = new System.Drawing.Size(751, 131);
            this.CommentTextBox.SpecialCharacter = "%";
            this.CommentTextBox.TabIndex = 2;
            this.CommentTextBox.Tag = "";
            this.CommentTextBox.TextCustomFormat = "$#,##0.00";
            this.CommentTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CommentTextBox.WholeInteger = false;
            this.CommentTextBox.TextChanged += new System.EventHandler(this.CommentTextBox_TextChanged);
            this.CommentTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CommentTextBox_KeyPress);
            // 
            // CommentLabel
            // 
            this.CommentLabel.AutoSize = true;
            this.CommentLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.CommentLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CommentLabel.Location = new System.Drawing.Point(1, -1);
            this.CommentLabel.Name = "CommentLabel";
            this.CommentLabel.Size = new System.Drawing.Size(65, 14);
            this.CommentLabel.TabIndex = 20;
            this.CommentLabel.Text = "Comment:";
            // 
            // CommentPictureBox
            // 
            this.CommentPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("CommentPictureBox.Image")));
            this.CommentPictureBox.Location = new System.Drawing.Point(761, 0);
            this.CommentPictureBox.Name = "CommentPictureBox";
            this.CommentPictureBox.Size = new System.Drawing.Size(42, 151);
            this.CommentPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CommentPictureBox.TabIndex = 6;
            this.CommentPictureBox.TabStop = false;
            this.CommentPictureBox.Click += new System.EventHandler(this.CommentPictureBox_Click);
            this.CommentPictureBox.MouseEnter += new System.EventHandler(this.CommentPictureBox_MouseEnter);
            // 
            // F8050
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.CommentPanel);
            this.Controls.Add(this.CommentPictureBox);
            this.Name = "F8050";
            this.Size = new System.Drawing.Size(804, 151);
            this.Tag = "8050";
            this.Load += new System.EventHandler(this.F8050_Load);
            this.CommentPanel.ResumeLayout(false);
            this.CommentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CommentPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel CommentPanel;
        private TerraScan.UI.Controls.TerraScanTextBox CommentTextBox;
        private System.Windows.Forms.Label CommentLabel;
        private System.Windows.Forms.PictureBox CommentPictureBox;
        private System.Windows.Forms.ToolTip GDocCommentsToolTip;
    }
}
