namespace D9050
{
    partial class F9052
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label CurrentSnapshotNameLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9052));
            this.currentSnapshotNamePanel = new System.Windows.Forms.Panel();
            this.SnapshotNameValueLabel = new System.Windows.Forms.Label();
            this.currentSnapshotDescriptionPanel = new System.Windows.Forms.Panel();
            this.SnapshotDescriptionValueLabel = new System.Windows.Forms.Label();
            this.CurrentSnapshotDescriptionLabel = new System.Windows.Forms.Label();
            this.currentQueryPanel = new System.Windows.Forms.Panel();
            this.CurrentQueryTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CurrentQueryLabel = new System.Windows.Forms.Label();
            this.CloseRequeryButton = new TerraScan.UI.Controls.TerraScanButton();
            this.queryUtilityLinePanel = new System.Windows.Forms.Panel();
            this.requeryFormIDLabel = new System.Windows.Forms.Label();
            this.formIdlabel = new System.Windows.Forms.Label();
            this.DescriptionToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.RequeryButton = new TerraScan.UI.Controls.TerraScanButton();
            CurrentSnapshotNameLabel = new System.Windows.Forms.Label();
            this.currentSnapshotNamePanel.SuspendLayout();
            this.currentSnapshotDescriptionPanel.SuspendLayout();
            this.currentQueryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CurrentSnapshotNameLabel
            // 
            CurrentSnapshotNameLabel.AutoSize = true;
            CurrentSnapshotNameLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            CurrentSnapshotNameLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            CurrentSnapshotNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            CurrentSnapshotNameLabel.Location = new System.Drawing.Point(0, 2);
            CurrentSnapshotNameLabel.Name = "CurrentSnapshotNameLabel";
            CurrentSnapshotNameLabel.Size = new System.Drawing.Size(142, 14);
            CurrentSnapshotNameLabel.TabIndex = 0;
            CurrentSnapshotNameLabel.Text = "Current Snapshot Name:";
            // 
            // currentSnapshotNamePanel
            // 
            this.currentSnapshotNamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentSnapshotNamePanel.Controls.Add(this.SnapshotNameValueLabel);
            this.currentSnapshotNamePanel.Controls.Add(CurrentSnapshotNameLabel);
            this.currentSnapshotNamePanel.Location = new System.Drawing.Point(12, 12);
            this.currentSnapshotNamePanel.Name = "currentSnapshotNamePanel";
            this.currentSnapshotNamePanel.Size = new System.Drawing.Size(241, 45);
            this.currentSnapshotNamePanel.TabIndex = 0;
            this.currentSnapshotNamePanel.TabStop = true;
            // 
            // SnapshotNameValueLabel
            // 
            this.SnapshotNameValueLabel.AutoSize = true;
            this.SnapshotNameValueLabel.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SnapshotNameValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.SnapshotNameValueLabel.Location = new System.Drawing.Point(15, 18);
            this.SnapshotNameValueLabel.Name = "SnapshotNameValueLabel";
            this.SnapshotNameValueLabel.Size = new System.Drawing.Size(0, 16);
            this.SnapshotNameValueLabel.TabIndex = 113;
            // 
            // currentSnapshotDescriptionPanel
            // 
            this.currentSnapshotDescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentSnapshotDescriptionPanel.Controls.Add(this.SnapshotDescriptionValueLabel);
            this.currentSnapshotDescriptionPanel.Controls.Add(this.CurrentSnapshotDescriptionLabel);
            this.currentSnapshotDescriptionPanel.Location = new System.Drawing.Point(253, 12);
            this.currentSnapshotDescriptionPanel.Name = "currentSnapshotDescriptionPanel";
            this.currentSnapshotDescriptionPanel.Size = new System.Drawing.Size(322, 45);
            this.currentSnapshotDescriptionPanel.TabIndex = 0;
            this.currentSnapshotDescriptionPanel.TabStop = true;
            // 
            // SnapshotDescriptionValueLabel
            // 
            this.SnapshotDescriptionValueLabel.AutoSize = true;
            this.SnapshotDescriptionValueLabel.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SnapshotDescriptionValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.SnapshotDescriptionValueLabel.Location = new System.Drawing.Point(15, 18);
            this.SnapshotDescriptionValueLabel.Name = "SnapshotDescriptionValueLabel";
            this.SnapshotDescriptionValueLabel.Size = new System.Drawing.Size(0, 16);
            this.SnapshotDescriptionValueLabel.TabIndex = 114;
            this.SnapshotDescriptionValueLabel.MouseEnter += new System.EventHandler(this.SnapshotDescriptionValueLabel_MouseEnter);
            // 
            // CurrentSnapshotDescriptionLabel
            // 
            this.CurrentSnapshotDescriptionLabel.AutoSize = true;
            this.CurrentSnapshotDescriptionLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CurrentSnapshotDescriptionLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentSnapshotDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CurrentSnapshotDescriptionLabel.Location = new System.Drawing.Point(0, 2);
            this.CurrentSnapshotDescriptionLabel.Name = "CurrentSnapshotDescriptionLabel";
            this.CurrentSnapshotDescriptionLabel.Size = new System.Drawing.Size(174, 14);
            this.CurrentSnapshotDescriptionLabel.TabIndex = 62;
            this.CurrentSnapshotDescriptionLabel.Text = "Current Snapshot Description:";
            // 
            // currentQueryPanel
            // 
            this.currentQueryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentQueryPanel.Controls.Add(this.CurrentQueryTextBox);
            this.currentQueryPanel.Controls.Add(this.CurrentQueryLabel);
            this.currentQueryPanel.Location = new System.Drawing.Point(12, 62);
            this.currentQueryPanel.Name = "currentQueryPanel";
            this.currentQueryPanel.Size = new System.Drawing.Size(563, 63);
            this.currentQueryPanel.TabIndex = 1;
            this.currentQueryPanel.TabStop = true;
            // 
            // CurrentQueryTextBox
            // 
            this.CurrentQueryTextBox.AllowClick = true;
            this.CurrentQueryTextBox.ApplyCFGFormat = false;
            this.CurrentQueryTextBox.ApplyCurrencyFormat = false;
            this.CurrentQueryTextBox.ApplyFocusColor = true;
            this.CurrentQueryTextBox.ApplyParentFocusColor = true;
            this.CurrentQueryTextBox.BackColor = System.Drawing.Color.White;
            this.CurrentQueryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CurrentQueryTextBox.CFromatWihoutSymbol = false;
            this.CurrentQueryTextBox.CheckForEmpty = false;
            this.CurrentQueryTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CurrentQueryTextBox.Digits = -1;
            this.CurrentQueryTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentQueryTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.CurrentQueryTextBox.IsEditable = false;
            this.CurrentQueryTextBox.IsQueryableFileld = false;
            this.CurrentQueryTextBox.Location = new System.Drawing.Point(16, 19);
            this.CurrentQueryTextBox.LockKeyPress = false;
            this.CurrentQueryTextBox.MaxLength = 8000;
            this.CurrentQueryTextBox.Multiline = true;
            this.CurrentQueryTextBox.Name = "CurrentQueryTextBox";
            this.CurrentQueryTextBox.PersistDefaultColor = false;
            this.CurrentQueryTextBox.Precision = 2;
            this.CurrentQueryTextBox.QueryingFileldName = "";
            this.CurrentQueryTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CurrentQueryTextBox.Size = new System.Drawing.Size(532, 39);
            this.CurrentQueryTextBox.SpecialCharacter = "%";
            this.CurrentQueryTextBox.TabIndex = 1;
            this.CurrentQueryTextBox.TextCustomFormat = "$#,##0.00";
            this.CurrentQueryTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CurrentQueryTextBox.TextChanged += new System.EventHandler(this.CurrentQueryTextBox_TextChanged);
            // 
            // CurrentQueryLabel
            // 
            this.CurrentQueryLabel.AutoSize = true;
            this.CurrentQueryLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CurrentQueryLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentQueryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CurrentQueryLabel.Location = new System.Drawing.Point(0, 2);
            this.CurrentQueryLabel.Name = "CurrentQueryLabel";
            this.CurrentQueryLabel.Size = new System.Drawing.Size(89, 14);
            this.CurrentQueryLabel.TabIndex = 62;
            this.CurrentQueryLabel.Text = "Current Query:";
            // 
            // CloseRequeryButton
            // 
            this.CloseRequeryButton.ActualPermission = false;
            this.CloseRequeryButton.AutoSize = true;
            this.CloseRequeryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CloseRequeryButton.BorderColor = System.Drawing.Color.Wheat;
            this.CloseRequeryButton.CommentPriority = false;
            this.CloseRequeryButton.EnableAutoPrint = false;
            this.CloseRequeryButton.FilterStatus = false;
            this.CloseRequeryButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CloseRequeryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseRequeryButton.FocusRectangleEnabled = true;
            this.CloseRequeryButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseRequeryButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CloseRequeryButton.ImageSelected = false;
            this.CloseRequeryButton.Location = new System.Drawing.Point(465, 129);
            this.CloseRequeryButton.Name = "CloseRequeryButton";
            this.CloseRequeryButton.NewPadding = 5;
            this.CloseRequeryButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CloseRequeryButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CloseRequeryButton.Size = new System.Drawing.Size(110, 30);
            this.CloseRequeryButton.StatusIndicator = false;
            this.CloseRequeryButton.StatusOffText = null;
            this.CloseRequeryButton.StatusOnText = null;
            this.CloseRequeryButton.TabIndex = 3;
            this.CloseRequeryButton.Text = "Close";
            this.CloseRequeryButton.UseVisualStyleBackColor = false;
            this.CloseRequeryButton.Click += new System.EventHandler(this.CloseRequeryButton_Click);
            // 
            // queryUtilityLinePanel
            // 
            this.queryUtilityLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.queryUtilityLinePanel.Location = new System.Drawing.Point(12, 166);
            this.queryUtilityLinePanel.Name = "queryUtilityLinePanel";
            this.queryUtilityLinePanel.Size = new System.Drawing.Size(563, 2);
            this.queryUtilityLinePanel.TabIndex = 110;
            // 
            // requeryFormIDLabel
            // 
            this.requeryFormIDLabel.AutoSize = true;
            this.requeryFormIDLabel.Location = new System.Drawing.Point(13, 171);
            this.requeryFormIDLabel.Name = "requeryFormIDLabel";
            this.requeryFormIDLabel.Size = new System.Drawing.Size(0, 13);
            this.requeryFormIDLabel.TabIndex = 111;
            // 
            // formIdlabel
            // 
            this.formIdlabel.AutoSize = true;
            this.formIdlabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formIdlabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.formIdlabel.Location = new System.Drawing.Point(9, 171);
            this.formIdlabel.Name = "formIdlabel";
            this.formIdlabel.Size = new System.Drawing.Size(35, 15);
            this.formIdlabel.TabIndex = 112;
            this.formIdlabel.Text = "9052";
            // 
            // RequeryButton
            // 
            this.RequeryButton.ActualPermission = false;
            this.RequeryButton.AutoSize = true;
            this.RequeryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.RequeryButton.BorderColor = System.Drawing.Color.Wheat;
            this.RequeryButton.CommentPriority = false;
            this.RequeryButton.EnableAutoPrint = false;
            this.RequeryButton.FilterStatus = false;
            this.RequeryButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.RequeryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RequeryButton.FocusRectangleEnabled = true;
            this.RequeryButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RequeryButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.RequeryButton.ImageSelected = false;
            this.RequeryButton.Location = new System.Drawing.Point(349, 129);
            this.RequeryButton.Name = "RequeryButton";
            this.RequeryButton.NewPadding = 5;
            this.RequeryButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.RequeryButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.RequeryButton.Size = new System.Drawing.Size(110, 30);
            this.RequeryButton.StatusIndicator = false;
            this.RequeryButton.StatusOffText = null;
            this.RequeryButton.StatusOnText = null;
            this.RequeryButton.TabIndex = 115;
            this.RequeryButton.Text = "Requery";
            this.RequeryButton.UseVisualStyleBackColor = false;
            this.RequeryButton.Click += new System.EventHandler(this.RequeryButton_Click);
            // 
            // F9052
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(585, 195);
            this.Controls.Add(this.RequeryButton);
            this.Controls.Add(this.formIdlabel);
            this.Controls.Add(this.requeryFormIDLabel);
            this.Controls.Add(this.queryUtilityLinePanel);
            this.Controls.Add(this.CloseRequeryButton);
            this.Controls.Add(this.currentQueryPanel);
            this.Controls.Add(this.currentSnapshotDescriptionPanel);
            this.Controls.Add(this.currentSnapshotNamePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F9052";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan T2 - Query String";
            this.Shown += new System.EventHandler(this.QueryString_Shown);
            this.Load += new System.EventHandler(this.QueryString_Load);
            this.currentSnapshotNamePanel.ResumeLayout(false);
            this.currentSnapshotNamePanel.PerformLayout();
            this.currentSnapshotDescriptionPanel.ResumeLayout(false);
            this.currentSnapshotDescriptionPanel.PerformLayout();
            this.currentQueryPanel.ResumeLayout(false);
            this.currentQueryPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel currentSnapshotNamePanel;
        private System.Windows.Forms.Panel currentSnapshotDescriptionPanel;
        private System.Windows.Forms.Label CurrentSnapshotDescriptionLabel;
        private System.Windows.Forms.Panel currentQueryPanel;
        private TerraScan.UI.Controls.TerraScanTextBox CurrentQueryTextBox;
        private System.Windows.Forms.Label CurrentQueryLabel;       
        private TerraScan.UI.Controls.TerraScanButton CloseRequeryButton;
        private System.Windows.Forms.Panel queryUtilityLinePanel;
        private System.Windows.Forms.Label requeryFormIDLabel;
        private System.Windows.Forms.Label formIdlabel;
        private System.Windows.Forms.Label SnapshotDescriptionValueLabel;
        private System.Windows.Forms.Label SnapshotNameValueLabel;
        private System.Windows.Forms.ToolTip DescriptionToolTip;
        private TerraScan.UI.Controls.TerraScanButton RequeryButton;
    }
}