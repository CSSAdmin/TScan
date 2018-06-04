namespace D20000
{
    partial class F2010
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F2010));
            this.label1 = new System.Windows.Forms.Label();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.StateCodePanel = new System.Windows.Forms.Panel();
            this.StateCodeComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.StateCodeLabel = new System.Windows.Forms.Label();
            this.StateCodeManagementLinkLabel = new System.Windows.Forms.LinkLabel();
            this.StateCodeAcceptButton = new TerraScan.UI.Controls.TerraScanButton();
            this.StateCodeCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.StateCodePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(10, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 120;
            this.label1.Text = "2010";
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(11, 103);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(527, 2);
            this.FormLinePanel.TabIndex = 153;
            // 
            // StateCodePanel
            // 
            this.StateCodePanel.BackColor = System.Drawing.Color.White;
            this.StateCodePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StateCodePanel.Controls.Add(this.StateCodeComboBox);
            this.StateCodePanel.Controls.Add(this.StateCodeLabel);
            this.StateCodePanel.Location = new System.Drawing.Point(11, 12);
            this.StateCodePanel.Name = "StateCodePanel";
            this.StateCodePanel.Size = new System.Drawing.Size(527, 41);
            this.StateCodePanel.TabIndex = 0;
            this.StateCodePanel.TabStop = true;
            // 
            // StateCodeComboBox
            // 
            this.StateCodeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.StateCodeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.StateCodeComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.StateCodeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StateCodeComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateCodeComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.StateCodeComboBox.FormattingEnabled = true;
            this.StateCodeComboBox.Location = new System.Drawing.Point(4, 14);
            this.StateCodeComboBox.Name = "StateCodeComboBox";
            this.StateCodeComboBox.Size = new System.Drawing.Size(518, 24);
            this.StateCodeComboBox.TabIndex = 1;
            this.StateCodeComboBox.SelectionChangeCommitted += new System.EventHandler(this.StateCodeComboBox_SelectionChangeCommitted);
            this.StateCodeComboBox.TextChanged += new System.EventHandler(this.StateCodeComboBox_TextChanged);
            // 
            // StateCodeLabel
            // 
            this.StateCodeLabel.AutoSize = true;
            this.StateCodeLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.StateCodeLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateCodeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.StateCodeLabel.Location = new System.Drawing.Point(0, 0);
            this.StateCodeLabel.Name = "StateCodeLabel";
            this.StateCodeLabel.Size = new System.Drawing.Size(70, 14);
            this.StateCodeLabel.TabIndex = 62;
            this.StateCodeLabel.Text = "State Code:";
            // 
            // StateCodeManagementLinkLabel
            // 
            this.StateCodeManagementLinkLabel.AutoSize = true;
            this.StateCodeManagementLinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateCodeManagementLinkLabel.Location = new System.Drawing.Point(394, 109);
            this.StateCodeManagementLinkLabel.MinimumSize = new System.Drawing.Size(144, 15);
            this.StateCodeManagementLinkLabel.Name = "StateCodeManagementLinkLabel";
            this.StateCodeManagementLinkLabel.Size = new System.Drawing.Size(146, 15);
            this.StateCodeManagementLinkLabel.TabIndex = 2;
            this.StateCodeManagementLinkLabel.TabStop = true;
            this.StateCodeManagementLinkLabel.Text = "State Code Management";
            this.StateCodeManagementLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.StateCodeManagementLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.StateCodeManagementLinkLabel_LinkClicked);
            // 
            // StateCodeAcceptButton
            // 
            this.StateCodeAcceptButton.ActualPermission = false;
            this.StateCodeAcceptButton.ApplyDisableBehaviour = false;
            this.StateCodeAcceptButton.AutoSize = true;
            this.StateCodeAcceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.StateCodeAcceptButton.BorderColor = System.Drawing.Color.Wheat;
            this.StateCodeAcceptButton.CommentPriority = false;
            this.StateCodeAcceptButton.EnableAutoPrint = false;
            this.StateCodeAcceptButton.FilterStatus = false;
            this.StateCodeAcceptButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.StateCodeAcceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StateCodeAcceptButton.FocusRectangleEnabled = true;
            this.StateCodeAcceptButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateCodeAcceptButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.StateCodeAcceptButton.ImageSelected = false;
            this.StateCodeAcceptButton.Location = new System.Drawing.Point(173, 64);
            this.StateCodeAcceptButton.Name = "StateCodeAcceptButton";
            this.StateCodeAcceptButton.NewPadding = 5;
            this.StateCodeAcceptButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.StateCodeAcceptButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.StateCodeAcceptButton.Size = new System.Drawing.Size(98, 28);
            this.StateCodeAcceptButton.StatusIndicator = false;
            this.StateCodeAcceptButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.StateCodeAcceptButton.StatusOffText = null;
            this.StateCodeAcceptButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.StateCodeAcceptButton.StatusOnText = null;
            this.StateCodeAcceptButton.TabIndex = 154;
            this.StateCodeAcceptButton.TabStop = false;
            this.StateCodeAcceptButton.Text = "Accept";
            this.StateCodeAcceptButton.UseVisualStyleBackColor = false;
            this.StateCodeAcceptButton.Click += new System.EventHandler(this.StateCodeAcceptButton_Click);
            // 
            // StateCodeCancelButton
            // 
            this.StateCodeCancelButton.ActualPermission = false;
            this.StateCodeCancelButton.ApplyDisableBehaviour = false;
            this.StateCodeCancelButton.AutoSize = true;
            this.StateCodeCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.StateCodeCancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.StateCodeCancelButton.CommentPriority = false;
            this.StateCodeCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.StateCodeCancelButton.EnableAutoPrint = false;
            this.StateCodeCancelButton.FilterStatus = false;
            this.StateCodeCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.StateCodeCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StateCodeCancelButton.FocusRectangleEnabled = true;
            this.StateCodeCancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateCodeCancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.StateCodeCancelButton.ImageSelected = false;
            this.StateCodeCancelButton.Location = new System.Drawing.Point(278, 64);
            this.StateCodeCancelButton.Name = "StateCodeCancelButton";
            this.StateCodeCancelButton.NewPadding = 5;
            this.StateCodeCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.StateCodeCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.StateCodeCancelButton.Size = new System.Drawing.Size(98, 28);
            this.StateCodeCancelButton.StatusIndicator = false;
            this.StateCodeCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.StateCodeCancelButton.StatusOffText = null;
            this.StateCodeCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.StateCodeCancelButton.StatusOnText = null;
            this.StateCodeCancelButton.TabIndex = 155;
            this.StateCodeCancelButton.TabStop = false;
            this.StateCodeCancelButton.Text = "Cancel";
            this.StateCodeCancelButton.UseVisualStyleBackColor = false;
            // 
            // F2010
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(550, 127);
            this.Controls.Add(this.StateCodeCancelButton);
            this.Controls.Add(this.StateCodeAcceptButton);
            this.Controls.Add(this.StateCodeManagementLinkLabel);
            this.Controls.Add(this.StateCodePanel);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F2010";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - State Code Selection";
            this.Load += new System.EventHandler(this.F2010_Load);
            this.StateCodePanel.ResumeLayout(false);
            this.StateCodePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Panel StateCodePanel;
        private System.Windows.Forms.Label StateCodeLabel;
        private System.Windows.Forms.LinkLabel StateCodeManagementLinkLabel;
        private TerraScan.UI.Controls.TerraScanComboBox StateCodeComboBox;
        private TerraScan.UI.Controls.TerraScanButton StateCodeAcceptButton;
        private TerraScan.UI.Controls.TerraScanButton StateCodeCancelButton;
    }
}