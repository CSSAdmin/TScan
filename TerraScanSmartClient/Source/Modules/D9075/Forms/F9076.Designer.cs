namespace D9075
{
    partial class F9076
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9076));
            this.FindPanelPanel = new System.Windows.Forms.Panel();
            this.TemplateNameTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TemplateNameLabel = new System.Windows.Forms.Label();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.FormNumberLabel = new System.Windows.Forms.Label();
            this.DeleteNewCommentTemplatebutton = new TerraScan.UI.Controls.TerraScanButton();
            this.SaveNewCommentTemplatebutton = new TerraScan.UI.Controls.TerraScanButton();
            this.CancelNewCommentTemplateButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FindPanelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FindPanelPanel
            // 
            this.FindPanelPanel.BackColor = System.Drawing.Color.White;
            this.FindPanelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FindPanelPanel.Controls.Add(this.TemplateNameTextBox);
            this.FindPanelPanel.Controls.Add(this.TemplateNameLabel);
            this.FindPanelPanel.Location = new System.Drawing.Point(12, 12);
            this.FindPanelPanel.Name = "FindPanelPanel";
            this.FindPanelPanel.Size = new System.Drawing.Size(350, 46);
            this.FindPanelPanel.TabIndex = 1;
            this.FindPanelPanel.TabStop = true;
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
            this.TemplateNameTextBox.IsQueryableFileld = false;
            this.TemplateNameTextBox.Location = new System.Drawing.Point(14, 20);
            this.TemplateNameTextBox.LockKeyPress = false;
            this.TemplateNameTextBox.MaxLength = 50;
            this.TemplateNameTextBox.Name = "TemplateNameTextBox";
            this.TemplateNameTextBox.PersistDefaultColor = false;
            this.TemplateNameTextBox.Precision = 2;
            this.TemplateNameTextBox.QueryingFileldName = "";
            this.TemplateNameTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.TemplateNameTextBox.Size = new System.Drawing.Size(327, 16);
            this.TemplateNameTextBox.SpecialCharacter = "%";
            this.TemplateNameTextBox.TabIndex = 1;
            this.TemplateNameTextBox.TextCustomFormat = "$#,##0.00";
            this.TemplateNameTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.TemplateNameTextBox.WholeInteger = false;
            this.TemplateNameTextBox.TextChanged += new System.EventHandler(this.TemplateNameTextBox_TextChanged);
            // 
            // TemplateNameLabel
            // 
            this.TemplateNameLabel.AutoSize = true;
            this.TemplateNameLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TemplateNameLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemplateNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TemplateNameLabel.Location = new System.Drawing.Point(1, 0);
            this.TemplateNameLabel.Name = "TemplateNameLabel";
            this.TemplateNameLabel.Size = new System.Drawing.Size(96, 14);
            this.TemplateNameLabel.TabIndex = 0;
            this.TemplateNameLabel.Text = "Template Name:";
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(12, 103);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(349, 2);
            this.FormLinePanel.TabIndex = 115;
            // 
            // FormNumberLabel
            // 
            this.FormNumberLabel.AutoSize = true;
            this.FormNumberLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormNumberLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.FormNumberLabel.Location = new System.Drawing.Point(12, 108);
            this.FormNumberLabel.Name = "FormNumberLabel";
            this.FormNumberLabel.Size = new System.Drawing.Size(35, 15);
            this.FormNumberLabel.TabIndex = 116;
            this.FormNumberLabel.Text = "9076";
            // 
            // DeleteNewCommentTemplatebutton
            // 
            this.DeleteNewCommentTemplatebutton.ActualPermission = false;
            this.DeleteNewCommentTemplatebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteNewCommentTemplatebutton.ApplyDisableBehaviour = false;
            this.DeleteNewCommentTemplatebutton.AutoSize = true;
            this.DeleteNewCommentTemplatebutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.DeleteNewCommentTemplatebutton.BorderColor = System.Drawing.Color.Wheat;
            this.DeleteNewCommentTemplatebutton.CommentPriority = false;
            this.DeleteNewCommentTemplatebutton.EnableAutoPrint = false;
            this.DeleteNewCommentTemplatebutton.FilterStatus = false;
            this.DeleteNewCommentTemplatebutton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.DeleteNewCommentTemplatebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteNewCommentTemplatebutton.FocusRectangleEnabled = true;
            this.DeleteNewCommentTemplatebutton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteNewCommentTemplatebutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DeleteNewCommentTemplatebutton.ImageSelected = false;
            this.DeleteNewCommentTemplatebutton.Location = new System.Drawing.Point(132, 65);
            this.DeleteNewCommentTemplatebutton.Name = "DeleteNewCommentTemplatebutton";
            this.DeleteNewCommentTemplatebutton.NewPadding = 5;
            this.DeleteNewCommentTemplatebutton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.DeleteNewCommentTemplatebutton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.DeleteNewCommentTemplatebutton.Size = new System.Drawing.Size(108, 30);
            this.DeleteNewCommentTemplatebutton.StatusIndicator = false;
            this.DeleteNewCommentTemplatebutton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DeleteNewCommentTemplatebutton.StatusOffText = null;
            this.DeleteNewCommentTemplatebutton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.DeleteNewCommentTemplatebutton.StatusOnText = null;
            this.DeleteNewCommentTemplatebutton.TabIndex = 120;
            this.DeleteNewCommentTemplatebutton.Text = "Delete";
            this.DeleteNewCommentTemplatebutton.UseVisualStyleBackColor = false;
            this.DeleteNewCommentTemplatebutton.Click += new System.EventHandler(this.DeleteNewCommentTemplatebutton_Click);
            // 
            // SaveNewCommentTemplatebutton
            // 
            this.SaveNewCommentTemplatebutton.ActualPermission = false;
            this.SaveNewCommentTemplatebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveNewCommentTemplatebutton.ApplyDisableBehaviour = false;
            this.SaveNewCommentTemplatebutton.AutoSize = true;
            this.SaveNewCommentTemplatebutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SaveNewCommentTemplatebutton.BorderColor = System.Drawing.Color.Wheat;
            this.SaveNewCommentTemplatebutton.CommentPriority = false;
            this.SaveNewCommentTemplatebutton.EnableAutoPrint = false;
            this.SaveNewCommentTemplatebutton.FilterStatus = false;
            this.SaveNewCommentTemplatebutton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveNewCommentTemplatebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveNewCommentTemplatebutton.FocusRectangleEnabled = true;
            this.SaveNewCommentTemplatebutton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveNewCommentTemplatebutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SaveNewCommentTemplatebutton.ImageSelected = false;
            this.SaveNewCommentTemplatebutton.Location = new System.Drawing.Point(12, 65);
            this.SaveNewCommentTemplatebutton.Name = "SaveNewCommentTemplatebutton";
            this.SaveNewCommentTemplatebutton.NewPadding = 5;
            this.SaveNewCommentTemplatebutton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.SaveNewCommentTemplatebutton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SaveNewCommentTemplatebutton.Size = new System.Drawing.Size(108, 30);
            this.SaveNewCommentTemplatebutton.StatusIndicator = false;
            this.SaveNewCommentTemplatebutton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SaveNewCommentTemplatebutton.StatusOffText = null;
            this.SaveNewCommentTemplatebutton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SaveNewCommentTemplatebutton.StatusOnText = null;
            this.SaveNewCommentTemplatebutton.TabIndex = 121;
            this.SaveNewCommentTemplatebutton.Text = "Save";
            this.SaveNewCommentTemplatebutton.UseVisualStyleBackColor = false;
            this.SaveNewCommentTemplatebutton.Click += new System.EventHandler(this.SaveNewCommentTemplatebutton_Click);
            // 
            // CancelNewCommentTemplateButton
            // 
            this.CancelNewCommentTemplateButton.ActualPermission = false;
            this.CancelNewCommentTemplateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelNewCommentTemplateButton.ApplyDisableBehaviour = false;
            this.CancelNewCommentTemplateButton.AutoSize = true;
            this.CancelNewCommentTemplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelNewCommentTemplateButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelNewCommentTemplateButton.CommentPriority = false;
            this.CancelNewCommentTemplateButton.EnableAutoPrint = false;
            this.CancelNewCommentTemplateButton.FilterStatus = false;
            this.CancelNewCommentTemplateButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelNewCommentTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelNewCommentTemplateButton.FocusRectangleEnabled = true;
            this.CancelNewCommentTemplateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelNewCommentTemplateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelNewCommentTemplateButton.ImageSelected = false;
            this.CancelNewCommentTemplateButton.Location = new System.Drawing.Point(252, 65);
            this.CancelNewCommentTemplateButton.Name = "CancelNewCommentTemplateButton";
            this.CancelNewCommentTemplateButton.NewPadding = 5;
            this.CancelNewCommentTemplateButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.CancelNewCommentTemplateButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelNewCommentTemplateButton.Size = new System.Drawing.Size(108, 30);
            this.CancelNewCommentTemplateButton.StatusIndicator = false;
            this.CancelNewCommentTemplateButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelNewCommentTemplateButton.StatusOffText = null;
            this.CancelNewCommentTemplateButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelNewCommentTemplateButton.StatusOnText = null;
            this.CancelNewCommentTemplateButton.TabIndex = 122;
            this.CancelNewCommentTemplateButton.Text = "Cancel";
            this.CancelNewCommentTemplateButton.UseVisualStyleBackColor = false;
            this.CancelNewCommentTemplateButton.Click += new System.EventHandler(this.CancelNewCommentTemplateButton_Click);
            // 
            // F9076
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(372, 124);
            this.Controls.Add(this.CancelNewCommentTemplateButton);
            this.Controls.Add(this.SaveNewCommentTemplatebutton);
            this.Controls.Add(this.DeleteNewCommentTemplatebutton);
            this.Controls.Add(this.FormNumberLabel);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.FindPanelPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F9076";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - New Comment Template";
            this.Load += new System.EventHandler(this.F9076_Load);
            this.FindPanelPanel.ResumeLayout(false);
            this.FindPanelPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel FindPanelPanel;
        private System.Windows.Forms.Label TemplateNameLabel;
        private TerraScan.UI.Controls.TerraScanTextBox TemplateNameTextBox;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label FormNumberLabel;
        private TerraScan.UI.Controls.TerraScanButton DeleteNewCommentTemplatebutton;
        private TerraScan.UI.Controls.TerraScanButton SaveNewCommentTemplatebutton;
        private TerraScan.UI.Controls.TerraScanButton CancelNewCommentTemplateButton;
    }
}