namespace D8000
{
    partial class F8046
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8046));
            this.EmptyPanel1 = new System.Windows.Forms.Panel();
            this.CountPanel = new System.Windows.Forms.Panel();
            this.CountTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CountLable = new System.Windows.Forms.Label();
            this.TotalCostPanel = new System.Windows.Forms.Panel();
            this.TotalCostLinkLabel = new TerraScan.UI.Controls.TerraScanLinkLabel();
            this.TotalCostLabel = new System.Windows.Forms.Label();
            this.MaterialsFooterPictureBox = new System.Windows.Forms.PictureBox();
            this.EmptyPanel2 = new System.Windows.Forms.Panel();
            this.TotalPartsPanel = new System.Windows.Forms.Panel();
            this.TotalPartsTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TotalPartsLabel = new System.Windows.Forms.Label();
            this.EmptyPanel3 = new System.Windows.Forms.Panel();
            this.MaterialsFooterToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.CountPanel.SuspendLayout();
            this.TotalCostPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialsFooterPictureBox)).BeginInit();
            this.TotalPartsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // EmptyPanel1
            // 
            this.EmptyPanel1.BackColor = System.Drawing.Color.Silver;
            this.EmptyPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmptyPanel1.Location = new System.Drawing.Point(0, 0);
            this.EmptyPanel1.Name = "EmptyPanel1";
            this.EmptyPanel1.Size = new System.Drawing.Size(20, 37);
            this.EmptyPanel1.TabIndex = 16;
            // 
            // CountPanel
            // 
            this.CountPanel.BackColor = System.Drawing.Color.Transparent;
            this.CountPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CountPanel.Controls.Add(this.CountTextBox);
            this.CountPanel.Controls.Add(this.CountLable);
            this.CountPanel.Location = new System.Drawing.Point(19, 0);
            this.CountPanel.Name = "CountPanel";
            this.CountPanel.Size = new System.Drawing.Size(128, 37);
            this.CountPanel.TabIndex = 17;
            // 
            // CountTextBox
            // 
            this.CountTextBox.AllowClick = true;
            this.CountTextBox.AllowNegativeSign = false;
            this.CountTextBox.ApplyCFGFormat = false;
            this.CountTextBox.ApplyCurrencyFormat = false;
            this.CountTextBox.ApplyFocusColor = true;
            this.CountTextBox.ApplyNegativeStandard = true;
            this.CountTextBox.ApplyParentFocusColor = true;
            this.CountTextBox.ApplyTimeFormat = false;
            this.CountTextBox.BackColor = System.Drawing.Color.White;
            this.CountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CountTextBox.CFromatWihoutSymbol = false;
            this.CountTextBox.CheckForEmpty = false;
            this.CountTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CountTextBox.Digits = -1;
            this.CountTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.CountTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CountTextBox.IsEditable = false;
            this.CountTextBox.IsQueryableFileld = true;
            this.CountTextBox.Location = new System.Drawing.Point(33, 17);
            this.CountTextBox.LockKeyPress = true;
            this.CountTextBox.MaxLength = 4;
            this.CountTextBox.Name = "CountTextBox";
            this.CountTextBox.PersistDefaultColor = false;
            this.CountTextBox.Precision = 2;
            this.CountTextBox.QueryingFileldName = "";
            this.CountTextBox.ReadOnly = true;
            this.CountTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CountTextBox.Size = new System.Drawing.Size(87, 16);
            this.CountTextBox.SpecialCharacter = "%";
            this.CountTextBox.TabIndex = 2;
            this.CountTextBox.TabStop = false;
            this.CountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CountTextBox.TextCustomFormat = "$#,##0.00";
            this.CountTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            // 
            // CountLable
            // 
            this.CountLable.AutoSize = true;
            this.CountLable.BackColor = System.Drawing.Color.Transparent;
            this.CountLable.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CountLable.Location = new System.Drawing.Point(1, -1);
            this.CountLable.Name = "CountLable";
            this.CountLable.Size = new System.Drawing.Size(43, 14);
            this.CountLable.TabIndex = 0;
            this.CountLable.Text = "Count:";
            // 
            // TotalCostPanel
            // 
            this.TotalCostPanel.BackColor = System.Drawing.Color.Transparent;
            this.TotalCostPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalCostPanel.Controls.Add(this.TotalCostLinkLabel);
            this.TotalCostPanel.Controls.Add(this.TotalCostLabel);
            this.TotalCostPanel.Location = new System.Drawing.Point(449, 0);
            this.TotalCostPanel.Name = "TotalCostPanel";
            this.TotalCostPanel.Size = new System.Drawing.Size(300, 37);
            this.TotalCostPanel.TabIndex = 10;
            // 
            // TotalCostLinkLabel
            // 
            this.TotalCostLinkLabel.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.TotalCostLinkLabel.FormDllName = null;
            this.TotalCostLinkLabel.FormId = 0;
            this.TotalCostLinkLabel.Location = new System.Drawing.Point(84, 15);
            this.TotalCostLinkLabel.MenuName = null;
            this.TotalCostLinkLabel.Name = "TotalCostLinkLabel";
            this.TotalCostLinkLabel.PermissionOpen = 0;
            this.TotalCostLinkLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TotalCostLinkLabel.Size = new System.Drawing.Size(211, 17);
            this.TotalCostLinkLabel.TabIndex = 10;
            this.TotalCostLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.TotalCostLinkLabel.TextCustomFormat = "$ #,##0.00";
            this.TotalCostLinkLabel.ValidateType = TerraScan.UI.Controls.TerraScanLinkLabel.ControlValidationType.Decimal;
            this.TotalCostLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TotalCostLinkLabel_LinkClicked);
            // 
            // TotalCostLabel
            // 
            this.TotalCostLabel.AutoSize = true;
            this.TotalCostLabel.BackColor = System.Drawing.Color.Transparent;
            this.TotalCostLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalCostLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TotalCostLabel.Location = new System.Drawing.Point(1, -1);
            this.TotalCostLabel.Name = "TotalCostLabel";
            this.TotalCostLabel.Size = new System.Drawing.Size(66, 14);
            this.TotalCostLabel.TabIndex = 0;
            this.TotalCostLabel.Text = "Total Cost:";
            // 
            // MaterialsFooterPictureBox
            // 
            this.MaterialsFooterPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("MaterialsFooterPictureBox.Image")));
            this.MaterialsFooterPictureBox.Location = new System.Drawing.Point(761, 0);
            this.MaterialsFooterPictureBox.Name = "MaterialsFooterPictureBox";
            this.MaterialsFooterPictureBox.Size = new System.Drawing.Size(42, 37);
            this.MaterialsFooterPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MaterialsFooterPictureBox.TabIndex = 18;
            this.MaterialsFooterPictureBox.TabStop = false;
            this.MaterialsFooterPictureBox.MouseEnter += new System.EventHandler(this.MaterialsFooterPictureBox_MouseEnter);
            // 
            // EmptyPanel2
            // 
            this.EmptyPanel2.BackColor = System.Drawing.Color.Silver;
            this.EmptyPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmptyPanel2.Location = new System.Drawing.Point(748, 0);
            this.EmptyPanel2.Name = "EmptyPanel2";
            this.EmptyPanel2.Size = new System.Drawing.Size(20, 37);
            this.EmptyPanel2.TabIndex = 19;
            // 
            // TotalPartsPanel
            // 
            this.TotalPartsPanel.BackColor = System.Drawing.Color.Transparent;
            this.TotalPartsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalPartsPanel.Controls.Add(this.TotalPartsTextBox);
            this.TotalPartsPanel.Controls.Add(this.TotalPartsLabel);
            this.TotalPartsPanel.Location = new System.Drawing.Point(273, 0);
            this.TotalPartsPanel.Name = "TotalPartsPanel";
            this.TotalPartsPanel.Size = new System.Drawing.Size(177, 37);
            this.TotalPartsPanel.TabIndex = 20;
            // 
            // TotalPartsTextBox
            // 
            this.TotalPartsTextBox.AllowClick = true;
            this.TotalPartsTextBox.AllowNegativeSign = false;
            this.TotalPartsTextBox.ApplyCFGFormat = false;
            this.TotalPartsTextBox.ApplyCurrencyFormat = false;
            this.TotalPartsTextBox.ApplyFocusColor = true;
            this.TotalPartsTextBox.ApplyNegativeStandard = true;
            this.TotalPartsTextBox.ApplyParentFocusColor = true;
            this.TotalPartsTextBox.ApplyTimeFormat = false;
            this.TotalPartsTextBox.BackColor = System.Drawing.Color.White;
            this.TotalPartsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TotalPartsTextBox.CFromatWihoutSymbol = false;
            this.TotalPartsTextBox.CheckForEmpty = false;
            this.TotalPartsTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TotalPartsTextBox.Digits = -1;
            this.TotalPartsTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.TotalPartsTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TotalPartsTextBox.IsEditable = false;
            this.TotalPartsTextBox.IsQueryableFileld = true;
            this.TotalPartsTextBox.Location = new System.Drawing.Point(60, 17);
            this.TotalPartsTextBox.LockKeyPress = true;
            this.TotalPartsTextBox.MaxLength = 4;
            this.TotalPartsTextBox.Name = "TotalPartsTextBox";
            this.TotalPartsTextBox.PersistDefaultColor = false;
            this.TotalPartsTextBox.Precision = 2;
            this.TotalPartsTextBox.QueryingFileldName = "";
            this.TotalPartsTextBox.ReadOnly = true;
            this.TotalPartsTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.TotalPartsTextBox.Size = new System.Drawing.Size(106, 16);
            this.TotalPartsTextBox.SpecialCharacter = "%";
            this.TotalPartsTextBox.TabIndex = 8;
            this.TotalPartsTextBox.TabStop = false;
            this.TotalPartsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TotalPartsTextBox.TextCustomFormat = "$#,##0.00";
            this.TotalPartsTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            // 
            // TotalPartsLabel
            // 
            this.TotalPartsLabel.AutoSize = true;
            this.TotalPartsLabel.BackColor = System.Drawing.Color.Transparent;
            this.TotalPartsLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalPartsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TotalPartsLabel.Location = new System.Drawing.Point(1, -1);
            this.TotalPartsLabel.Name = "TotalPartsLabel";
            this.TotalPartsLabel.Size = new System.Drawing.Size(69, 14);
            this.TotalPartsLabel.TabIndex = 0;
            this.TotalPartsLabel.Text = "Total Parts:";
            // 
            // EmptyPanel3
            // 
            this.EmptyPanel3.BackColor = System.Drawing.Color.Silver;
            this.EmptyPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmptyPanel3.Location = new System.Drawing.Point(145, 0);
            this.EmptyPanel3.Name = "EmptyPanel3";
            this.EmptyPanel3.Size = new System.Drawing.Size(131, 37);
            this.EmptyPanel3.TabIndex = 21;
            // 
            // F8046
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.EmptyPanel3);
            this.Controls.Add(this.TotalPartsPanel);
            this.Controls.Add(this.TotalCostPanel);
            this.Controls.Add(this.EmptyPanel2);
            this.Controls.Add(this.CountPanel);
            this.Controls.Add(this.EmptyPanel1);
            this.Controls.Add(this.MaterialsFooterPictureBox);
            this.Name = "F8046";
            this.Size = new System.Drawing.Size(804, 37);
            this.Tag = "8046";
            this.Load += new System.EventHandler(this.F8046_Load);
            this.CountPanel.ResumeLayout(false);
            this.CountPanel.PerformLayout();
            this.TotalCostPanel.ResumeLayout(false);
            this.TotalCostPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialsFooterPictureBox)).EndInit();
            this.TotalPartsPanel.ResumeLayout(false);
            this.TotalPartsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel EmptyPanel1;
        private System.Windows.Forms.Panel CountPanel;
        private TerraScan.UI.Controls.TerraScanTextBox CountTextBox;
        private System.Windows.Forms.Label CountLable;
        private System.Windows.Forms.Panel TotalCostPanel;
        private TerraScan.UI.Controls.TerraScanLinkLabel TotalCostLinkLabel;
        private System.Windows.Forms.Label TotalCostLabel;
        private System.Windows.Forms.PictureBox MaterialsFooterPictureBox;
        private System.Windows.Forms.Panel EmptyPanel2;
        private System.Windows.Forms.Panel TotalPartsPanel;
        private TerraScan.UI.Controls.TerraScanTextBox TotalPartsTextBox;
        private System.Windows.Forms.Label TotalPartsLabel;
        private System.Windows.Forms.Panel EmptyPanel3;
        private System.Windows.Forms.ToolTip MaterialsFooterToolTip;
    }
}
