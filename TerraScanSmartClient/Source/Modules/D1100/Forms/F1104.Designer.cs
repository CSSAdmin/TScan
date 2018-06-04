namespace D1100
{
    partial class F1104
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1104));
            this.formIDLabel = new System.Windows.Forms.Label();
            this.LinePanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.NewYearpanel = new System.Windows.Forms.Panel();
            this.NewDistrictLabel = new System.Windows.Forms.Label();
            this.BaseYearPanel = new System.Windows.Forms.Panel();
            this.BaseYearLabel = new System.Windows.Forms.Label();
            this.Districtpanel = new System.Windows.Forms.Panel();
            this.Districtlabel = new System.Windows.Forms.Label();
            this.NewDistrictTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.BasedYearTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DistrictTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CancelExciseDistrictButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CreateExciseDistrictButton = new TerraScan.UI.Controls.TerraScanButton();
            this.panel1.SuspendLayout();
            this.NewYearpanel.SuspendLayout();
            this.BaseYearPanel.SuspendLayout();
            this.Districtpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // formIDLabel
            // 
            this.formIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.formIDLabel.AutoSize = true;
            this.formIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.formIDLabel.Location = new System.Drawing.Point(12, 88);
            this.formIDLabel.Name = "formIDLabel";
            this.formIDLabel.Size = new System.Drawing.Size(35, 15);
            this.formIDLabel.TabIndex = 102;
            this.formIDLabel.Text = "1104";
            // 
            // LinePanel
            // 
            this.LinePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.LinePanel.Location = new System.Drawing.Point(10, 83);
            this.LinePanel.Name = "LinePanel";
            this.LinePanel.Size = new System.Drawing.Size(300, 2);
            this.LinePanel.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.NewYearpanel);
            this.panel1.Controls.Add(this.BaseYearPanel);
            this.panel1.Controls.Add(this.Districtpanel);
            this.panel1.Controls.Add(this.CancelExciseDistrictButton);
            this.panel1.Controls.Add(this.CreateExciseDistrictButton);
            this.panel1.Controls.Add(this.formIDLabel);
            this.panel1.Controls.Add(this.LinePanel);
            this.panel1.Location = new System.Drawing.Point(1, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(321, 108);
            this.panel1.TabIndex = 1;
            // 
            // NewYearpanel
            // 
            this.NewYearpanel.BackColor = System.Drawing.Color.Transparent;
            this.NewYearpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewYearpanel.Controls.Add(this.NewDistrictTextBox);
            this.NewYearpanel.Controls.Add(this.NewDistrictLabel);
            this.NewYearpanel.Location = new System.Drawing.Point(200, 7);
            this.NewYearpanel.Name = "NewYearpanel";
            this.NewYearpanel.Size = new System.Drawing.Size(110, 40);
            this.NewYearpanel.TabIndex = 0;
            // 
            // NewDistrictLabel
            // 
            this.NewDistrictLabel.AutoSize = true;
            this.NewDistrictLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.NewDistrictLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.NewDistrictLabel.Location = new System.Drawing.Point(-4, -1);
            this.NewDistrictLabel.Name = "NewDistrictLabel";
            this.NewDistrictLabel.Size = new System.Drawing.Size(107, 14);
            this.NewDistrictLabel.TabIndex = 3;
            this.NewDistrictLabel.Text = " New District Year:";
            this.NewDistrictLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BaseYearPanel
            // 
            this.BaseYearPanel.BackColor = System.Drawing.Color.Transparent;
            this.BaseYearPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BaseYearPanel.Controls.Add(this.BasedYearTextBox);
            this.BaseYearPanel.Controls.Add(this.BaseYearLabel);
            this.BaseYearPanel.Location = new System.Drawing.Point(89, 7);
            this.BaseYearPanel.Name = "BaseYearPanel";
            this.BaseYearPanel.Size = new System.Drawing.Size(112, 40);
            this.BaseYearPanel.TabIndex = 0;
            // 
            // BaseYearLabel
            // 
            this.BaseYearLabel.AutoSize = true;
            this.BaseYearLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.BaseYearLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.BaseYearLabel.Location = new System.Drawing.Point(-1, -1);
            this.BaseYearLabel.Name = "BaseYearLabel";
            this.BaseYearLabel.Size = new System.Drawing.Size(89, 14);
            this.BaseYearLabel.TabIndex = 2;
            this.BaseYearLabel.Text = "Based on Year:";
            this.BaseYearLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Districtpanel
            // 
            this.Districtpanel.BackColor = System.Drawing.Color.Transparent;
            this.Districtpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Districtpanel.Controls.Add(this.DistrictTextBox);
            this.Districtpanel.Controls.Add(this.Districtlabel);
            this.Districtpanel.Location = new System.Drawing.Point(10, 7);
            this.Districtpanel.Name = "Districtpanel";
            this.Districtpanel.Size = new System.Drawing.Size(80, 40);
            this.Districtpanel.TabIndex = 0;
            // 
            // Districtlabel
            // 
            this.Districtlabel.AutoSize = true;
            this.Districtlabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Districtlabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.Districtlabel.Location = new System.Drawing.Point(-1, 0);
            this.Districtlabel.Name = "Districtlabel";
            this.Districtlabel.Size = new System.Drawing.Size(49, 14);
            this.Districtlabel.TabIndex = 1;
            this.Districtlabel.Text = "District:";
            this.Districtlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NewDistrictTextBox
            // 
            this.NewDistrictTextBox.ApplyCurrencyFormat = false;
            this.NewDistrictTextBox.ApplyFocusColor = true;
            this.NewDistrictTextBox.ApplyParentFocusColor = true;
            this.NewDistrictTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.NewDistrictTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NewDistrictTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NewDistrictTextBox.Digits = -1;
            this.NewDistrictTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.NewDistrictTextBox.ForeColor = System.Drawing.Color.Black;
            this.NewDistrictTextBox.Location = new System.Drawing.Point(3, 18);
            this.NewDistrictTextBox.LockKeyPress = false;
            this.NewDistrictTextBox.MaxLength = 50;
            this.NewDistrictTextBox.Name = "NewDistrictTextBox";
            this.NewDistrictTextBox.Precision = 2;
            this.NewDistrictTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.NewDistrictTextBox.Size = new System.Drawing.Size(102, 16);
            this.NewDistrictTextBox.TabIndex = 3;
            this.NewDistrictTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Numeric;
            // 
            // BasedYearTextBox
            // 
            this.BasedYearTextBox.ApplyCurrencyFormat = false;
            this.BasedYearTextBox.ApplyFocusColor = true;
            this.BasedYearTextBox.ApplyParentFocusColor = true;
            this.BasedYearTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.BasedYearTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BasedYearTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.BasedYearTextBox.Digits = -1;
            this.BasedYearTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.BasedYearTextBox.ForeColor = System.Drawing.Color.Black;
            this.BasedYearTextBox.Location = new System.Drawing.Point(2, 18);
            this.BasedYearTextBox.LockKeyPress = false;
            this.BasedYearTextBox.MaxLength = 50;
            this.BasedYearTextBox.Name = "BasedYearTextBox";
            this.BasedYearTextBox.Precision = 2;
            this.BasedYearTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.BasedYearTextBox.Size = new System.Drawing.Size(106, 16);
            this.BasedYearTextBox.TabIndex = 2;
            this.BasedYearTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Numeric;
            // 
            // DistrictTextBox
            // 
            this.DistrictTextBox.ApplyCurrencyFormat = false;
            this.DistrictTextBox.ApplyFocusColor = true;
            this.DistrictTextBox.ApplyParentFocusColor = true;
            this.DistrictTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.DistrictTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DistrictTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DistrictTextBox.Digits = -1;
            this.DistrictTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.DistrictTextBox.ForeColor = System.Drawing.Color.Black;
            this.DistrictTextBox.Location = new System.Drawing.Point(2, 18);
            this.DistrictTextBox.LockKeyPress = false;
            this.DistrictTextBox.MaxLength = 50;
            this.DistrictTextBox.Name = "DistrictTextBox";
            this.DistrictTextBox.Precision = 2;
            this.DistrictTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DistrictTextBox.Size = new System.Drawing.Size(73, 16);
            this.DistrictTextBox.TabIndex = 1;
            this.DistrictTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Numeric;
            // 
            // CancelExciseDistrictButton
            // 
            this.CancelExciseDistrictButton.ActualPermission = false;
            this.CancelExciseDistrictButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelExciseDistrictButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelExciseDistrictButton.CommentPriority = false;
            this.CancelExciseDistrictButton.FilterStatus = false;
            this.CancelExciseDistrictButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelExciseDistrictButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelExciseDistrictButton.FocusRectangleEnabled = true;
            this.CancelExciseDistrictButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelExciseDistrictButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelExciseDistrictButton.ImageSelected = false;
            this.CancelExciseDistrictButton.Location = new System.Drawing.Point(212, 52);
            this.CancelExciseDistrictButton.Name = "CancelExciseDistrictButton";
            this.CancelExciseDistrictButton.NewPadding = 5;
            this.CancelExciseDistrictButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CancelExciseDistrictButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelExciseDistrictButton.Size = new System.Drawing.Size(98, 28);
            this.CancelExciseDistrictButton.StatusIndicator = false;
            this.CancelExciseDistrictButton.TabIndex = 5;
            this.CancelExciseDistrictButton.Text = "Cancel";
            this.CancelExciseDistrictButton.UseVisualStyleBackColor = false;
            // 
            // CreateExciseDistrictButton
            // 
            this.CreateExciseDistrictButton.ActualPermission = false;
            this.CreateExciseDistrictButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CreateExciseDistrictButton.BorderColor = System.Drawing.Color.Wheat;
            this.CreateExciseDistrictButton.CommentPriority = false;
            this.CreateExciseDistrictButton.FilterStatus = false;
            this.CreateExciseDistrictButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CreateExciseDistrictButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateExciseDistrictButton.FocusRectangleEnabled = true;
            this.CreateExciseDistrictButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateExciseDistrictButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CreateExciseDistrictButton.ImageSelected = false;
            this.CreateExciseDistrictButton.Location = new System.Drawing.Point(10, 52);
            this.CreateExciseDistrictButton.Name = "CreateExciseDistrictButton";
            this.CreateExciseDistrictButton.NewPadding = 5;
            this.CreateExciseDistrictButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CreateExciseDistrictButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CreateExciseDistrictButton.Size = new System.Drawing.Size(98, 28);
            this.CreateExciseDistrictButton.StatusIndicator = false;
            this.CreateExciseDistrictButton.TabIndex = 4;
            this.CreateExciseDistrictButton.Text = "Create";
            this.CreateExciseDistrictButton.UseVisualStyleBackColor = false;
            this.CreateExciseDistrictButton.Click += new System.EventHandler(this.CreateExciseDistrictButton_Click);
            // 
            // F1104
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(323, 109);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(331, 143);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(331, 143);
            this.Name = "F1104";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - Excise District Copy";
            this.Load += new System.EventHandler(this.F1104_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.NewYearpanel.ResumeLayout(false);
            this.NewYearpanel.PerformLayout();
            this.BaseYearPanel.ResumeLayout(false);
            this.BaseYearPanel.PerformLayout();
            this.Districtpanel.ResumeLayout(false);
            this.Districtpanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanButton CreateExciseDistrictButton;
        private System.Windows.Forms.Label formIDLabel;
        private System.Windows.Forms.Panel LinePanel;
        private TerraScan.UI.Controls.TerraScanButton CancelExciseDistrictButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Districtlabel;
        private System.Windows.Forms.Label NewDistrictLabel;
        private System.Windows.Forms.Label BaseYearLabel;
        private System.Windows.Forms.Panel Districtpanel;
        private System.Windows.Forms.Panel BaseYearPanel;
        private System.Windows.Forms.Panel NewYearpanel;
        private TerraScan.UI.Controls.TerraScanTextBox BasedYearTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox DistrictTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox NewDistrictTextBox;
    }
}