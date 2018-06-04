namespace D11018
{
    partial class F1021
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1021));
            this.MiscReceiptTemplatePanel = new System.Windows.Forms.Panel();
            this.HighPriorityPanel = new System.Windows.Forms.Panel();
            this.HighPriorityLabel = new System.Windows.Forms.Label();
            this.DefaultCommentPanel = new System.Windows.Forms.Panel();
            this.DefaultCommentTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DefaultCommentLabel = new System.Windows.Forms.Label();
            this.TemplateNamePanel = new System.Windows.Forms.Panel();
            this.TemplateNameTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TemplateNameLabel = new System.Windows.Forms.Label();
            this.SaveMiscTemplateButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CancelMiscTemplateButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DistrictLinePanel = new System.Windows.Forms.Panel();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.SaveTemplateMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SaveMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HighPriorityCheckBox = new TerraScan.UI.Controls.TerraScanCheckBox();
            this.MiscReceiptTemplatePanel.SuspendLayout();
            this.HighPriorityPanel.SuspendLayout();
            this.DefaultCommentPanel.SuspendLayout();
            this.TemplateNamePanel.SuspendLayout();
            this.SaveTemplateMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MiscReceiptTemplatePanel
            // 
            this.MiscReceiptTemplatePanel.Controls.Add(this.HighPriorityPanel);
            this.MiscReceiptTemplatePanel.Controls.Add(this.DefaultCommentPanel);
            this.MiscReceiptTemplatePanel.Controls.Add(this.TemplateNamePanel);
            this.MiscReceiptTemplatePanel.Location = new System.Drawing.Point(10, 10);
            this.MiscReceiptTemplatePanel.Name = "MiscReceiptTemplatePanel";
            this.MiscReceiptTemplatePanel.Size = new System.Drawing.Size(470, 73);
            this.MiscReceiptTemplatePanel.TabIndex = 1;
            this.MiscReceiptTemplatePanel.TabStop = true;
            // 
            // HighPriorityPanel
            // 
            this.HighPriorityPanel.BackColor = System.Drawing.Color.Transparent;
            this.HighPriorityPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HighPriorityPanel.Controls.Add(this.HighPriorityCheckBox);
            this.HighPriorityPanel.Controls.Add(this.HighPriorityLabel);
            this.HighPriorityPanel.Location = new System.Drawing.Point(391, 36);
            this.HighPriorityPanel.Name = "HighPriorityPanel";
            this.HighPriorityPanel.Size = new System.Drawing.Size(79, 37);
            this.HighPriorityPanel.TabIndex = 2;
            // 
            // HighPriorityLabel
            // 
            this.HighPriorityLabel.AutoSize = true;
            this.HighPriorityLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.HighPriorityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.HighPriorityLabel.Location = new System.Drawing.Point(-1, -1);
            this.HighPriorityLabel.Name = "HighPriorityLabel";
            this.HighPriorityLabel.Size = new System.Drawing.Size(77, 14);
            this.HighPriorityLabel.TabIndex = 20;
            this.HighPriorityLabel.Text = "High Priority:";
            // 
            // DefaultCommentPanel
            // 
            this.DefaultCommentPanel.BackColor = System.Drawing.Color.Transparent;
            this.DefaultCommentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DefaultCommentPanel.Controls.Add(this.DefaultCommentTextBox);
            this.DefaultCommentPanel.Controls.Add(this.DefaultCommentLabel);
            this.DefaultCommentPanel.Location = new System.Drawing.Point(0, 36);
            this.DefaultCommentPanel.Name = "DefaultCommentPanel";
            this.DefaultCommentPanel.Size = new System.Drawing.Size(392, 37);
            this.DefaultCommentPanel.TabIndex = 1;
            // 
            // DefaultCommentTextBox
            // 
            this.DefaultCommentTextBox.AllowClick = true;
            this.DefaultCommentTextBox.AllowNegativeSign = false;
            this.DefaultCommentTextBox.ApplyCFGFormat = false;
            this.DefaultCommentTextBox.ApplyCurrencyFormat = false;
            this.DefaultCommentTextBox.ApplyFocusColor = true;
            this.DefaultCommentTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.DefaultCommentTextBox.ApplyNegativeStandard = true;
            this.DefaultCommentTextBox.ApplyParentFocusColor = true;
            this.DefaultCommentTextBox.ApplyTimeFormat = false;
            this.DefaultCommentTextBox.BackColor = System.Drawing.Color.White;
            this.DefaultCommentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DefaultCommentTextBox.CFromatWihoutSymbol = false;
            this.DefaultCommentTextBox.CheckForEmpty = false;
            this.DefaultCommentTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DefaultCommentTextBox.Digits = -1;
            this.DefaultCommentTextBox.EmptyDecimalValue = false;
            this.DefaultCommentTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.DefaultCommentTextBox.ForeColor = System.Drawing.Color.Black;
            this.DefaultCommentTextBox.IsEditable = false;
            this.DefaultCommentTextBox.IsQueryableFileld = true;
            this.DefaultCommentTextBox.Location = new System.Drawing.Point(8, 16);
            this.DefaultCommentTextBox.LockKeyPress = false;
            this.DefaultCommentTextBox.MaxLength = 250;
            this.DefaultCommentTextBox.Name = "DefaultCommentTextBox";
            this.DefaultCommentTextBox.PersistDefaultColor = false;
            this.DefaultCommentTextBox.Precision = 2;
            this.DefaultCommentTextBox.QueryingFileldName = "";
            this.DefaultCommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.DefaultCommentTextBox.Size = new System.Drawing.Size(379, 16);
            this.DefaultCommentTextBox.SpecialCharacter = "%";
            this.DefaultCommentTextBox.TabIndex = 0;
            this.DefaultCommentTextBox.Tag = "";
            this.DefaultCommentTextBox.TextCustomFormat = "$#,##0.00";
            this.DefaultCommentTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.DefaultCommentTextBox.WholeInteger = false;
            // 
            // DefaultCommentLabel
            // 
            this.DefaultCommentLabel.AutoSize = true;
            this.DefaultCommentLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.DefaultCommentLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DefaultCommentLabel.Location = new System.Drawing.Point(-1, -1);
            this.DefaultCommentLabel.Name = "DefaultCommentLabel";
            this.DefaultCommentLabel.Size = new System.Drawing.Size(106, 14);
            this.DefaultCommentLabel.TabIndex = 20;
            this.DefaultCommentLabel.Text = "Default Comment:";
            // 
            // TemplateNamePanel
            // 
            this.TemplateNamePanel.BackColor = System.Drawing.Color.Transparent;
            this.TemplateNamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TemplateNamePanel.Controls.Add(this.TemplateNameTextBox);
            this.TemplateNamePanel.Controls.Add(this.TemplateNameLabel);
            this.TemplateNamePanel.Location = new System.Drawing.Point(0, 0);
            this.TemplateNamePanel.Name = "TemplateNamePanel";
            this.TemplateNamePanel.Size = new System.Drawing.Size(470, 37);
            this.TemplateNamePanel.TabIndex = 0;
            // 
            // TemplateNameTextBox
            // 
            this.TemplateNameTextBox.AllowClick = true;
            this.TemplateNameTextBox.AllowNegativeSign = false;
            this.TemplateNameTextBox.ApplyCFGFormat = false;
            this.TemplateNameTextBox.ApplyCurrencyFormat = false;
            this.TemplateNameTextBox.ApplyFocusColor = true;
            this.TemplateNameTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.TemplateNameTextBox.ApplyNegativeStandard = true;
            this.TemplateNameTextBox.ApplyParentFocusColor = true;
            this.TemplateNameTextBox.ApplyTimeFormat = false;
            this.TemplateNameTextBox.BackColor = System.Drawing.Color.White;
            this.TemplateNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TemplateNameTextBox.CFromatWihoutSymbol = false;
            this.TemplateNameTextBox.CheckForEmpty = false;
            this.TemplateNameTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TemplateNameTextBox.Digits = -1;
            this.TemplateNameTextBox.EmptyDecimalValue = false;
            this.TemplateNameTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.TemplateNameTextBox.ForeColor = System.Drawing.Color.Black;
            this.TemplateNameTextBox.IsEditable = false;
            this.TemplateNameTextBox.IsQueryableFileld = true;
            this.TemplateNameTextBox.Location = new System.Drawing.Point(8, 16);
            this.TemplateNameTextBox.LockKeyPress = false;
            this.TemplateNameTextBox.MaxLength = 100;
            this.TemplateNameTextBox.Name = "TemplateNameTextBox";
            this.TemplateNameTextBox.PersistDefaultColor = false;
            this.TemplateNameTextBox.Precision = 2;
            this.TemplateNameTextBox.QueryingFileldName = "";
            this.TemplateNameTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.TemplateNameTextBox.Size = new System.Drawing.Size(457, 16);
            this.TemplateNameTextBox.SpecialCharacter = "%";
            this.TemplateNameTextBox.TabIndex = 0;
            this.TemplateNameTextBox.Tag = "";
            this.TemplateNameTextBox.TextCustomFormat = "$#,##0.00";
            this.TemplateNameTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.TemplateNameTextBox.WholeInteger = false;
            // 
            // TemplateNameLabel
            // 
            this.TemplateNameLabel.AutoSize = true;
            this.TemplateNameLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.TemplateNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TemplateNameLabel.Location = new System.Drawing.Point(-1, -1);
            this.TemplateNameLabel.Name = "TemplateNameLabel";
            this.TemplateNameLabel.Size = new System.Drawing.Size(96, 14);
            this.TemplateNameLabel.TabIndex = 20;
            this.TemplateNameLabel.Text = "Template Name:";
            // 
            // SaveMiscTemplateButton
            // 
            this.SaveMiscTemplateButton.ActualPermission = false;
            this.SaveMiscTemplateButton.ApplyDisableBehaviour = false;
            this.SaveMiscTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SaveMiscTemplateButton.BorderColor = System.Drawing.Color.Wheat;
            this.SaveMiscTemplateButton.CommentPriority = false;
            this.SaveMiscTemplateButton.EnableAutoPrint = false;
            this.SaveMiscTemplateButton.FilterStatus = false;
            this.SaveMiscTemplateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveMiscTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveMiscTemplateButton.FocusRectangleEnabled = true;
            this.SaveMiscTemplateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveMiscTemplateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SaveMiscTemplateButton.ImageSelected = false;
            this.SaveMiscTemplateButton.Location = new System.Drawing.Point(10, 92);
            this.SaveMiscTemplateButton.Name = "SaveMiscTemplateButton";
            this.SaveMiscTemplateButton.NewPadding = 5;
            this.SaveMiscTemplateButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.SaveMiscTemplateButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SaveMiscTemplateButton.Size = new System.Drawing.Size(98, 28);
            this.SaveMiscTemplateButton.StatusIndicator = false;
            this.SaveMiscTemplateButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SaveMiscTemplateButton.StatusOffText = null;
            this.SaveMiscTemplateButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SaveMiscTemplateButton.StatusOnText = null;
            this.SaveMiscTemplateButton.TabIndex = 174;
            this.SaveMiscTemplateButton.TabStop = false;
            this.SaveMiscTemplateButton.Text = "Save";
            this.SaveMiscTemplateButton.UseVisualStyleBackColor = false;
            this.SaveMiscTemplateButton.Click += new System.EventHandler(this.SaveMiscTemplateButton_Click);
            // 
            // CancelMiscTemplateButton
            // 
            this.CancelMiscTemplateButton.ActualPermission = false;
            this.CancelMiscTemplateButton.ApplyDisableBehaviour = false;
            this.CancelMiscTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelMiscTemplateButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelMiscTemplateButton.CommentPriority = false;
            this.CancelMiscTemplateButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelMiscTemplateButton.EnableAutoPrint = false;
            this.CancelMiscTemplateButton.FilterStatus = false;
            this.CancelMiscTemplateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelMiscTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelMiscTemplateButton.FocusRectangleEnabled = true;
            this.CancelMiscTemplateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelMiscTemplateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelMiscTemplateButton.ImageSelected = false;
            this.CancelMiscTemplateButton.Location = new System.Drawing.Point(382, 92);
            this.CancelMiscTemplateButton.Name = "CancelMiscTemplateButton";
            this.CancelMiscTemplateButton.NewPadding = 5;
            this.CancelMiscTemplateButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CancelMiscTemplateButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelMiscTemplateButton.Size = new System.Drawing.Size(98, 28);
            this.CancelMiscTemplateButton.StatusIndicator = false;
            this.CancelMiscTemplateButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelMiscTemplateButton.StatusOffText = null;
            this.CancelMiscTemplateButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelMiscTemplateButton.StatusOnText = null;
            this.CancelMiscTemplateButton.TabIndex = 175;
            this.CancelMiscTemplateButton.TabStop = false;
            this.CancelMiscTemplateButton.Text = "Cancel";
            this.CancelMiscTemplateButton.UseVisualStyleBackColor = false;
            // 
            // DistrictLinePanel
            // 
            this.DistrictLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.DistrictLinePanel.Location = new System.Drawing.Point(10, 126);
            this.DistrictLinePanel.Name = "DistrictLinePanel";
            this.DistrictLinePanel.Size = new System.Drawing.Size(470, 2);
            this.DistrictLinePanel.TabIndex = 176;
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(11, 131);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(35, 15);
            this.FormIDLabel.TabIndex = 177;
            this.FormIDLabel.Text = "1021";
            // 
            // SaveTemplateMenuStrip
            // 
            this.SaveTemplateMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveMenuToolStripMenuItem});
            this.SaveTemplateMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.SaveTemplateMenuStrip.Name = "SaveTemplateMenuStrip";
            this.SaveTemplateMenuStrip.Size = new System.Drawing.Size(490, 24);
            this.SaveTemplateMenuStrip.TabIndex = 179;
            this.SaveTemplateMenuStrip.Text = "menuStrip";
            this.SaveTemplateMenuStrip.Visible = false;
            // 
            // SaveMenuToolStripMenuItem
            // 
            this.SaveMenuToolStripMenuItem.Name = "SaveMenuToolStripMenuItem";
            this.SaveMenuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveMenuToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.SaveMenuToolStripMenuItem.Text = "SaveMenu";
            this.SaveMenuToolStripMenuItem.Visible = false;
            // 
            // HighPriorityCheckBox
            // 
            this.HighPriorityCheckBox.Font = new System.Drawing.Font("Arial", 10F);
            this.HighPriorityCheckBox.Location = new System.Drawing.Point(33, 18);
            this.HighPriorityCheckBox.Name = "HighPriorityCheckBox";
            this.HighPriorityCheckBox.Size = new System.Drawing.Size(20, 14);
            this.HighPriorityCheckBox.TabIndex = 0;
            this.HighPriorityCheckBox.UseCompatibleTextRendering = true;
            this.HighPriorityCheckBox.UseVisualStyleBackColor = true;
            // 
            // F1021
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(490, 148);
            this.Controls.Add(this.SaveTemplateMenuStrip);
            this.Controls.Add(this.SaveMiscTemplateButton);
            this.Controls.Add(this.CancelMiscTemplateButton);
            this.Controls.Add(this.DistrictLinePanel);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.MiscReceiptTemplatePanel);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(496, 180);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(496, 180);
            this.Name = "F1021";
            this.ParentFormId = 1021;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2";
            this.Load += new System.EventHandler(this.F1021_Load);
            this.MiscReceiptTemplatePanel.ResumeLayout(false);
            this.HighPriorityPanel.ResumeLayout(false);
            this.HighPriorityPanel.PerformLayout();
            this.DefaultCommentPanel.ResumeLayout(false);
            this.DefaultCommentPanel.PerformLayout();
            this.TemplateNamePanel.ResumeLayout(false);
            this.TemplateNamePanel.PerformLayout();
            this.SaveTemplateMenuStrip.ResumeLayout(false);
            this.SaveTemplateMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MiscReceiptTemplatePanel;
        private System.Windows.Forms.Panel DefaultCommentPanel;
        private TerraScan.UI.Controls.TerraScanTextBox DefaultCommentTextBox;
        private System.Windows.Forms.Label DefaultCommentLabel;
        private System.Windows.Forms.Panel TemplateNamePanel;
        private TerraScan.UI.Controls.TerraScanTextBox TemplateNameTextBox;
        private System.Windows.Forms.Label TemplateNameLabel;
        private System.Windows.Forms.Panel HighPriorityPanel;
        private System.Windows.Forms.Label HighPriorityLabel;
        private TerraScan.UI.Controls.TerraScanButton SaveMiscTemplateButton;
        private TerraScan.UI.Controls.TerraScanButton CancelMiscTemplateButton;
        private System.Windows.Forms.Panel DistrictLinePanel;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.MenuStrip SaveTemplateMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SaveMenuToolStripMenuItem;
        private TerraScan.UI.Controls.TerraScanCheckBox HighPriorityCheckBox;
    }
}