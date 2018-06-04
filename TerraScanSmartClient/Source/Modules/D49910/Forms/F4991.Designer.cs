namespace D49910
{
    partial class F4991
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F4991));
            this.InstumentCopyPanel = new System.Windows.Forms.Panel();
            this.InstCopyGrid = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.InstCopyOkButton = new TerraScan.UI.Controls.TerraScanButton();
            this.InstCopyCancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Copyid = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Copy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstumentCopyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InstCopyGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // InstumentCopyPanel
            // 
            this.InstumentCopyPanel.BackColor = System.Drawing.Color.White;
            this.InstumentCopyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InstumentCopyPanel.Controls.Add(this.InstCopyGrid);
            this.InstumentCopyPanel.Location = new System.Drawing.Point(18, 16);
            this.InstumentCopyPanel.Name = "InstumentCopyPanel";
            this.InstumentCopyPanel.Size = new System.Drawing.Size(270, 113);
            this.InstumentCopyPanel.TabIndex = 2;
            this.InstumentCopyPanel.TabStop = true;
            // 
            // InstCopyGrid
            // 
            this.InstCopyGrid.AllowCellClick = true;
            this.InstCopyGrid.AllowDoubleClick = true;
            this.InstCopyGrid.AllowEmptyRows = true;
            this.InstCopyGrid.AllowEnterKey = false;
            this.InstCopyGrid.AllowSorting = true;
            this.InstCopyGrid.AllowUserToAddRows = false;
            this.InstCopyGrid.AllowUserToDeleteRows = false;
            this.InstCopyGrid.AllowUserToResizeColumns = false;
            this.InstCopyGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.InstCopyGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.InstCopyGrid.ApplyStandardBehaviour = false;
            this.InstCopyGrid.BackgroundColor = System.Drawing.Color.White;
            this.InstCopyGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InstCopyGrid.ClearCurrentCellOnLeave = true;
            this.InstCopyGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InstCopyGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.InstCopyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.InstCopyGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Copyid,
            this.Copy});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.InstCopyGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.InstCopyGrid.DefaultRowIndex = 0;
            this.InstCopyGrid.DeselectCurrentCell = false;
            this.InstCopyGrid.DeselectSpecifiedRow = -1;
            this.InstCopyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InstCopyGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.InstCopyGrid.EnableBinding = true;
            this.InstCopyGrid.EnableHeadersVisualStyles = false;
            this.InstCopyGrid.GridColor = System.Drawing.Color.Black;
            this.InstCopyGrid.GridContentSelected = false;
            this.InstCopyGrid.IsEditableGrid = false;
            this.InstCopyGrid.IsMultiSelect = false;
            this.InstCopyGrid.IsSorted = false;
            this.InstCopyGrid.Location = new System.Drawing.Point(0, 0);
            this.InstCopyGrid.MultiSelect = false;
            this.InstCopyGrid.Name = "InstCopyGrid";
            this.InstCopyGrid.NumRowsVisible = 4;
            this.InstCopyGrid.PrimaryKeyColumnName = "";
            this.InstCopyGrid.RemainSortFields = false;
            this.InstCopyGrid.RemoveDefaultSelection = true;
            this.InstCopyGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InstCopyGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.InstCopyGrid.RowHeadersVisible = false;
            this.InstCopyGrid.RowHeadersWidth = 20;
            this.InstCopyGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.InstCopyGrid.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.InstCopyGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InstCopyGrid.Size = new System.Drawing.Size(268, 111);
            this.InstCopyGrid.TabIndex = 205;
            this.InstCopyGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InstCopyGrid_CellContentDoubleClick);
            this.InstCopyGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InstCopyGrid_CellContentClick);
            // 
            // InstCopyOkButton
            // 
            this.InstCopyOkButton.ActualPermission = false;
            this.InstCopyOkButton.ApplyDisableBehaviour = false;
            this.InstCopyOkButton.AutoSize = true;
            this.InstCopyOkButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.InstCopyOkButton.BorderColor = System.Drawing.Color.Wheat;
            this.InstCopyOkButton.CommentPriority = false;
            this.InstCopyOkButton.EnableAutoPrint = false;
            this.InstCopyOkButton.FilterStatus = false;
            this.InstCopyOkButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.InstCopyOkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InstCopyOkButton.FocusRectangleEnabled = true;
            this.InstCopyOkButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstCopyOkButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.InstCopyOkButton.ImageSelected = false;
            this.InstCopyOkButton.Location = new System.Drawing.Point(81, 138);
            this.InstCopyOkButton.Name = "InstCopyOkButton";
            this.InstCopyOkButton.NewPadding = 5;
            this.InstCopyOkButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.InstCopyOkButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.InstCopyOkButton.Size = new System.Drawing.Size(100, 30);
            this.InstCopyOkButton.StatusIndicator = false;
            this.InstCopyOkButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.InstCopyOkButton.StatusOffText = null;
            this.InstCopyOkButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.InstCopyOkButton.StatusOnText = null;
            this.InstCopyOkButton.TabIndex = 119;
            this.InstCopyOkButton.TabStop = false;
            this.InstCopyOkButton.Text = "Ok";
            this.InstCopyOkButton.UseVisualStyleBackColor = false;
            this.InstCopyOkButton.Click += new System.EventHandler(this.InstCopyOkButton_Click);
            // 
            // InstCopyCancelButton
            // 
            this.InstCopyCancelButton.ActualPermission = false;
            this.InstCopyCancelButton.ApplyDisableBehaviour = false;
            this.InstCopyCancelButton.AutoSize = true;
            this.InstCopyCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.InstCopyCancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.InstCopyCancelButton.CommentPriority = false;
            this.InstCopyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.InstCopyCancelButton.EnableAutoPrint = false;
            this.InstCopyCancelButton.FilterStatus = false;
            this.InstCopyCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.InstCopyCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InstCopyCancelButton.FocusRectangleEnabled = true;
            this.InstCopyCancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstCopyCancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.InstCopyCancelButton.ImageSelected = false;
            this.InstCopyCancelButton.Location = new System.Drawing.Point(187, 138);
            this.InstCopyCancelButton.Name = "InstCopyCancelButton";
            this.InstCopyCancelButton.NewPadding = 5;
            this.InstCopyCancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.InstCopyCancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.InstCopyCancelButton.Size = new System.Drawing.Size(100, 30);
            this.InstCopyCancelButton.StatusIndicator = false;
            this.InstCopyCancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.InstCopyCancelButton.StatusOffText = null;
            this.InstCopyCancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.InstCopyCancelButton.StatusOnText = null;
            this.InstCopyCancelButton.TabIndex = 120;
            this.InstCopyCancelButton.TabStop = false;
            this.InstCopyCancelButton.Text = "Cancel";
            this.InstCopyCancelButton.UseVisualStyleBackColor = false;
            this.InstCopyCancelButton.Click += new System.EventHandler(this.InstCopyCancelButton_Click);
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(4, 173);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(289, 2);
            this.FormLinePanel.TabIndex = 121;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(7, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 122;
            this.label1.Text = "4991";
            // 
            // Copyid
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle3.NullValue = false;
            this.Copyid.DefaultCellStyle = dataGridViewCellStyle3;
            this.Copyid.HeaderText = "";
            this.Copyid.Name = "Copyid";
            this.Copyid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Copyid.Width = 22;
            // 
            // Copy
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.Copy.DefaultCellStyle = dataGridViewCellStyle4;
            this.Copy.HeaderText = "Copy...";
            this.Copy.Name = "Copy";
            this.Copy.ReadOnly = true;
            this.Copy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Copy.Width = 245;
            // 
            // F4991
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.InstCopyCancelButton;
            this.ClientSize = new System.Drawing.Size(300, 197);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FormLinePanel);
            this.Controls.Add(this.InstCopyCancelButton);
            this.Controls.Add(this.InstCopyOkButton);
            this.Controls.Add(this.InstumentCopyPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F4991";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terrascan T2 - Instrument Copy";
            this.Load += new System.EventHandler(this.F4991_Load);
            this.InstumentCopyPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InstCopyGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel InstumentCopyPanel;
        private TerraScan.UI.Controls.TerraScanDataGridView InstCopyGrid;
        private TerraScan.UI.Controls.TerraScanButton InstCopyOkButton;
        private TerraScan.UI.Controls.TerraScanButton InstCopyCancelButton;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Copyid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Copy;
    }
}