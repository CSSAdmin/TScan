namespace TerraScan.Common
{
    partial class HelpEngine
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpEngine));
            this.HelperHeader = new System.Windows.Forms.Label();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HelpEngineDataGridView = new System.Windows.Forms.DataGridView();
            this.HelpID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewLinkColumn();
            this.IsFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HelpFormLabel = new System.Windows.Forms.Label();
            this.ReportCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.HelpPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.HelpEngineDataGridView)).BeginInit();
            this.HelpPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // HelperHeader
            // 
            this.HelperHeader.AutoSize = true;
            this.HelperHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelperHeader.Location = new System.Drawing.Point(12, 0);
            this.HelperHeader.Name = "HelperHeader";
            this.HelperHeader.Size = new System.Drawing.Size(215, 16);
            this.HelperHeader.TabIndex = 3;
            this.HelperHeader.Text = "Select a Help Document to view:";
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(3, 212);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(192, 15);
            this.FormIDLabel.TabIndex = 117;
            this.FormIDLabel.Text = "9018 - Help Documents for Form ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(80)))), ((int)(((byte)(121)))));
            this.panel1.Location = new System.Drawing.Point(6, 208);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 2);
            this.panel1.TabIndex = 116;
            // 
            // HelpEngineDataGridView
            // 
            this.HelpEngineDataGridView.AllowUserToAddRows = false;
            this.HelpEngineDataGridView.AllowUserToResizeColumns = false;
            this.HelpEngineDataGridView.AllowUserToResizeRows = false;
            this.HelpEngineDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.HelpEngineDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HelpEngineDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.HelpEngineDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HelpEngineDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.HelpEngineDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HelpEngineDataGridView.ColumnHeadersVisible = false;
            this.HelpEngineDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HelpID,
            this.FormName,
            this.FileName,
            this.Description,
            this.IsFile});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.HelpEngineDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.HelpEngineDataGridView.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.HelpEngineDataGridView.Location = new System.Drawing.Point(37, 25);
            this.HelpEngineDataGridView.MultiSelect = false;
            this.HelpEngineDataGridView.Name = "HelpEngineDataGridView";
            this.HelpEngineDataGridView.ReadOnly = true;
            this.HelpEngineDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HelpEngineDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.HelpEngineDataGridView.RowHeadersVisible = false;
            this.HelpEngineDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.HelpEngineDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.HelpEngineDataGridView.Size = new System.Drawing.Size(359, 155);
            this.HelpEngineDataGridView.TabIndex = 119;
            this.HelpEngineDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HelpEngineDataGridView_CellClick);
            this.HelpEngineDataGridView.Leave += new System.EventHandler(this.HelpEngineDataGridView_Leave);
            // 
            // HelpID
            // 
            this.HelpID.HeaderText = "HelpID";
            this.HelpID.Name = "HelpID";
            this.HelpID.ReadOnly = true;
            this.HelpID.Visible = false;
            // 
            // FormName
            // 
            this.FormName.HeaderText = "FormName";
            this.FormName.Name = "FormName";
            this.FormName.ReadOnly = true;
            this.FormName.Visible = false;
            // 
            // FileName
            // 
            this.FileName.HeaderText = "FileName";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Visible = false;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 300;
            // 
            // IsFile
            // 
            this.IsFile.HeaderText = "IsFile";
            this.IsFile.Name = "IsFile";
            this.IsFile.ReadOnly = true;
            this.IsFile.Visible = false;
            // 
            // HelpFormLabel
            // 
            this.HelpFormLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpFormLabel.AutoSize = true;
            this.HelpFormLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpFormLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(80)))), ((int)(((byte)(129)))));
            this.HelpFormLabel.Location = new System.Drawing.Point(382, 0);
            this.HelpFormLabel.Name = "HelpFormLabel";
            this.HelpFormLabel.Size = new System.Drawing.Size(51, 22);
            this.HelpFormLabel.TabIndex = 120;
            this.HelpFormLabel.Text = "Help";
            // 
            // ReportCancelButton
            // 
            this.ReportCancelButton.ActualPermission = false;
            this.ReportCancelButton.ApplyDisableBehaviour = false;
            this.ReportCancelButton.AutoSize = true;
            this.ReportCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ReportCancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.ReportCancelButton.CommentPriority = false;
            this.ReportCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ReportCancelButton.EnableAutoPrint = false;
            this.ReportCancelButton.FilterStatus = false;
            this.ReportCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ReportCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReportCancelButton.FocusRectangleEnabled = true;
            this.ReportCancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportCancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ReportCancelButton.ImageSelected = false;
            this.ReportCancelButton.Location = new System.Drawing.Point(363, 180);
            this.ReportCancelButton.Name = "ReportCancelButton";
            this.ReportCancelButton.NewPadding = 5;
            this.ReportCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.ReportCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ReportCancelButton.Size = new System.Drawing.Size(75, 27);
            this.ReportCancelButton.StatusIndicator = false;
            this.ReportCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ReportCancelButton.StatusOffText = null;
            this.ReportCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ReportCancelButton.StatusOnText = null;
            this.ReportCancelButton.TabIndex = 118;
            this.ReportCancelButton.Text = "Cancel";
            this.ReportCancelButton.UseVisualStyleBackColor = false;
            // 
            // HelpPanel
            // 
            this.HelpPanel.Controls.Add(this.ReportCancelButton);
            this.HelpPanel.Location = new System.Drawing.Point(-2, 0);
            this.HelpPanel.Name = "HelpPanel";
            this.HelpPanel.Size = new System.Drawing.Size(444, 228);
            this.HelpPanel.TabIndex = 121;
            // 
            // HelpEngine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(444, 228);
            this.Controls.Add(this.HelpFormLabel);
            this.Controls.Add(this.HelpEngineDataGridView);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.HelperHeader);
            this.Controls.Add(this.HelpPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 260);
            this.Name = "HelpEngine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan - Help Documents";
            ((System.ComponentModel.ISupportInitialize)(this.HelpEngineDataGridView)).EndInit();
            this.HelpPanel.ResumeLayout(false);
            this.HelpPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label HelperHeader;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.Panel panel1;
        private TerraScan.UI.Controls.TerraScanButton ReportCancelButton;
        private System.Windows.Forms.DataGridView HelpEngineDataGridView;
        private System.Windows.Forms.Label HelpFormLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn HelpID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewLinkColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsFile;
        private System.Windows.Forms.Panel HelpPanel;
    }
}