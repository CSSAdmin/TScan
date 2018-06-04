namespace D11001
{
    partial class F1556
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1556));
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.FormLabel = new System.Windows.Forms.Label();
            this.YesButton = new TerraScan.UI.Controls.TerraScanButton();
            this.NoButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FirstLineLabel = new System.Windows.Forms.Label();
            this.SecondLineLabel = new System.Windows.Forms.Label();
            this.ThirdLineLabel = new System.Windows.Forms.Label();
            this.FourthLineLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(9, 213);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(392, 2);
            this.FormLinePanel.TabIndex = 166;
            // 
            // FormLabel
            // 
            this.FormLabel.AutoSize = true;
            this.FormLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.FormLabel.Location = new System.Drawing.Point(7, 217);
            this.FormLabel.Name = "FormLabel";
            this.FormLabel.Size = new System.Drawing.Size(35, 15);
            this.FormLabel.TabIndex = 10;
            this.FormLabel.Text = "1556";
            // 
            // YesButton
            // 
            this.YesButton.ActualPermission = false;
            this.YesButton.ApplyDisableBehaviour = false;
            this.YesButton.AutoSize = true;
            this.YesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.YesButton.BorderColor = System.Drawing.Color.Wheat;
            this.YesButton.CommentPriority = false;
            this.YesButton.EnableAutoPrint = false;
            this.YesButton.FilterStatus = false;
            this.YesButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.YesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.YesButton.FocusRectangleEnabled = true;
            this.YesButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YesButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.YesButton.ImageSelected = false;
            this.YesButton.Location = new System.Drawing.Point(64, 177);
            this.YesButton.Name = "YesButton";
            this.YesButton.NewPadding = 5;
            this.YesButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.YesButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.YesButton.Size = new System.Drawing.Size(110, 30);
            this.YesButton.StatusIndicator = false;
            this.YesButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.YesButton.StatusOffText = null;
            this.YesButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.YesButton.StatusOnText = null;
            this.YesButton.TabIndex = 8;
            this.YesButton.TabStop = false;
            this.YesButton.Text = "Yes";
            this.YesButton.UseVisualStyleBackColor = false;
            this.YesButton.Click += new System.EventHandler(this.YesButton_Click);
            // 
            // NoButton
            // 
            this.NoButton.ActualPermission = false;
            this.NoButton.ApplyDisableBehaviour = false;
            this.NoButton.AutoSize = true;
            this.NoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.NoButton.BorderColor = System.Drawing.Color.Wheat;
            this.NoButton.CommentPriority = false;
            this.NoButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.NoButton.EnableAutoPrint = false;
            this.NoButton.FilterStatus = false;
            this.NoButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.NoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NoButton.FocusRectangleEnabled = true;
            this.NoButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.NoButton.ImageSelected = false;
            this.NoButton.Location = new System.Drawing.Point(232, 177);
            this.NoButton.Name = "NoButton";
            this.NoButton.NewPadding = 5;
            this.NoButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.NoButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.NoButton.Size = new System.Drawing.Size(110, 30);
            this.NoButton.StatusIndicator = false;
            this.NoButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NoButton.StatusOffText = null;
            this.NoButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.NoButton.StatusOnText = null;
            this.NoButton.TabIndex = 9;
            this.NoButton.TabStop = false;
            this.NoButton.Text = "No";
            this.NoButton.UseVisualStyleBackColor = false;
            this.NoButton.Click += new System.EventHandler(NoButton_Click);
            // 
            // FirstLineLabel
            // 
            this.FirstLineLabel.AutoSize = true;
            this.FirstLineLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FirstLineLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstLineLabel.ForeColor = System.Drawing.Color.Black;
            this.FirstLineLabel.Location = new System.Drawing.Point(20, 23);
            this.FirstLineLabel.Name = "FirstLineLabel";
            this.FirstLineLabel.Size = new System.Drawing.Size(231, 15);
            this.FirstLineLabel.TabIndex = 167;
            this.FirstLineLabel.Text = "This payment is shared by 0 Receipts.";
            // 
            // SecondLineLabel
            // 
            this.SecondLineLabel.AutoSize = true;
            this.SecondLineLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SecondLineLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecondLineLabel.ForeColor = System.Drawing.Color.Black;
            this.SecondLineLabel.Location = new System.Drawing.Point(20, 58);
            this.SecondLineLabel.Name = "SecondLineLabel";
            this.SecondLineLabel.Size = new System.Drawing.Size(298, 15);
            this.SecondLineLabel.TabIndex = 168;
            this.SecondLineLabel.Text = "0 of those Recipt will be Reversed by this process.";
            // 
            // ThirdLineLabel
            // 
            this.ThirdLineLabel.AutoSize = true;
            this.ThirdLineLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ThirdLineLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThirdLineLabel.ForeColor = System.Drawing.Color.Black;
            this.ThirdLineLabel.Location = new System.Drawing.Point(20, 93);
            this.ThirdLineLabel.Name = "ThirdLineLabel";
            this.ThirdLineLabel.Size = new System.Drawing.Size(322, 15);
            this.ThirdLineLabel.TabIndex = 169;
            this.ThirdLineLabel.Text = "This will Reverse a total of $0.00 in collected funds.";
            // 
            // FourthLineLabel
            // 
            this.FourthLineLabel.AutoSize = true;
            this.FourthLineLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FourthLineLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FourthLineLabel.ForeColor = System.Drawing.Color.Black;
            this.FourthLineLabel.Location = new System.Drawing.Point(20, 128);
            this.FourthLineLabel.Name = "FourthLineLabel";
            this.FourthLineLabel.Size = new System.Drawing.Size(206, 15);
            this.FourthLineLabel.TabIndex = 170;
            this.FourthLineLabel.Text = "Are you sure you want to continue?";
            // 
            // F1556
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.NoButton;
            this.ClientSize = new System.Drawing.Size(414, 233);
            this.Controls.Add(this.FourthLineLabel);
            this.Controls.Add(this.ThirdLineLabel);
            this.Controls.Add(this.SecondLineLabel);
            this.Controls.Add(this.FirstLineLabel);
            this.Controls.Add(this.YesButton);
            this.Controls.Add(this.FormLabel);
            this.Controls.Add(this.NoButton);
            this.Controls.Add(this.FormLinePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 261);
            this.Name = "F1556";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "1556";
            this.Text = "TerraScan T2 - Reverse Shared Payment";
            this.Load += new System.EventHandler(this.F1556_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Panel FormLinePanel;
        private TerraScan.UI.Controls.TerraScanButton NoButton;
        private System.Windows.Forms.Label FormLabel;
        private TerraScan.UI.Controls.TerraScanButton YesButton;
        private System.Windows.Forms.Label FirstLineLabel;
        private System.Windows.Forms.Label SecondLineLabel;
        private System.Windows.Forms.Label ThirdLineLabel;
        private System.Windows.Forms.Label FourthLineLabel;
    }
}