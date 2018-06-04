namespace TerraScan.Common.Reports
{
    partial class MultipleReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultipleReports));
            this.ReportDataGridView = new System.Windows.Forms.DataGridView();
            this.Report = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReportFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewLinkColumn();
            this.OrderBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Printer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReportHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.ReportCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            ((System.ComponentModel.ISupportInitialize)(this.ReportDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportDataGridView
            // 
            this.ReportDataGridView.AllowUserToAddRows = false;
            this.ReportDataGridView.AllowUserToResizeColumns = false;
            this.ReportDataGridView.AllowUserToResizeRows = false;
            this.ReportDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.ReportDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReportDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.ReportDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ReportDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ReportDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReportDataGridView.ColumnHeadersVisible = false;
            this.ReportDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Report,
            this.ReportFile,
            this.Description,
            this.OrderBy,
            this.Printer});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ReportDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.ReportDataGridView.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ReportDataGridView.Location = new System.Drawing.Point(33, 37);
            this.ReportDataGridView.MultiSelect = false;
            this.ReportDataGridView.Name = "ReportDataGridView";
            this.ReportDataGridView.ReadOnly = true;
            this.ReportDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ReportDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ReportDataGridView.RowHeadersVisible = false;
            this.ReportDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ReportDataGridView.Size = new System.Drawing.Size(302, 94);
            this.ReportDataGridView.TabIndex = 0;
            this.ReportDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ReportDataGridView_CellContentClick);
            // 
            // Report
            // 
            this.Report.HeaderText = "Report";
            this.Report.Name = "Report";
            this.Report.ReadOnly = true;
            this.Report.Visible = false;
            // 
            // ReportFile
            // 
            this.ReportFile.HeaderText = "ReportFile";
            this.ReportFile.Name = "ReportFile";
            this.ReportFile.ReadOnly = true;
            this.ReportFile.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ReportFile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ReportFile.Visible = false;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 300;
            // 
            // OrderBy
            // 
            this.OrderBy.HeaderText = "OrderBy";
            this.OrderBy.Name = "OrderBy";
            this.OrderBy.ReadOnly = true;
            this.OrderBy.Visible = false;
            // 
            // Printer
            // 
            this.Printer.HeaderText = "Printer";
            this.Printer.Name = "Printer";
            this.Printer.ReadOnly = true;
            this.Printer.Visible = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.AutoSize = true;
            this.ReportHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportHeader.Location = new System.Drawing.Point(12, 9);
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.Size = new System.Drawing.Size(311, 14);
            this.ReportHeader.TabIndex = 2;
            this.ReportHeader.Text = "There are multiple reports available. Please select one:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Location = new System.Drawing.Point(8, 182);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(459, 1);
            this.panel1.TabIndex = 3;
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(12, 186);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(44, 14);
            this.FormIDLabel.TabIndex = 13;
            this.FormIDLabel.Text = "Report";
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
            this.ReportCancelButton.Location = new System.Drawing.Point(33, 144);
            this.ReportCancelButton.Name = "ReportCancelButton";
            this.ReportCancelButton.NewPadding = 5;
            this.ReportCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.ReportCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ReportCancelButton.Size = new System.Drawing.Size(110, 30);
            this.ReportCancelButton.StatusIndicator = false;
            this.ReportCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ReportCancelButton.StatusOffText = null;
            this.ReportCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ReportCancelButton.StatusOnText = null;
            this.ReportCancelButton.TabIndex = 18;
            this.ReportCancelButton.Text = "Cancel";
            this.ReportCancelButton.UseVisualStyleBackColor = false;
            this.ReportCancelButton.Click += new System.EventHandler(this.ReportCancelButton_Click);
            // 
            // MultipleReports
            // 
            this.AcceptButton = this.ReportCancelButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.ReportCancelButton;
            this.ClientSize = new System.Drawing.Size(474, 198);
            this.Controls.Add(this.ReportCancelButton);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ReportHeader);
            this.Controls.Add(this.ReportDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(480, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 230);
            this.Name = "MultipleReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan: Multiple Reports";
            ((System.ComponentModel.ISupportInitialize)(this.ReportDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ReportDataGridView;
        private System.Windows.Forms.Label ReportHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Report;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportFile;
        private System.Windows.Forms.DataGridViewLinkColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Printer;
        private TerraScan.UI.Controls.TerraScanButton ReportCancelButton;
    }
}