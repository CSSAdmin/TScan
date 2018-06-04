namespace TerraScan.FieldSummary
{
    partial class SalesUserControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.GridViewPanel = new System.Windows.Forms.Panel();
            this.verticalScrollBar = new System.Windows.Forms.VScrollBar();
            this.SalesGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.SaleDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BookPage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ttlprcls = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grantor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleStudy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grantee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThrowAway = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalePrice1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.GridViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SalesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(36)))), ((int)(((byte)(60)))));
            this.panel1.Controls.Add(this.HeaderLabel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 35);
            this.panel1.TabIndex = 0;
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.HeaderLabel.ForeColor = System.Drawing.Color.White;
            this.HeaderLabel.Location = new System.Drawing.Point(8, 9);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.HeaderLabel.Size = new System.Drawing.Size(176, 19);
            this.HeaderLabel.TabIndex = 24;
            this.HeaderLabel.Text = "Sale \\ Transfer History";
            this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GridViewPanel
            // 
            this.GridViewPanel.BackColor = System.Drawing.Color.White;
            this.GridViewPanel.Controls.Add(this.verticalScrollBar);
            this.GridViewPanel.Controls.Add(this.SalesGridView);
            this.GridViewPanel.Location = new System.Drawing.Point(0, 34);
            this.GridViewPanel.Name = "GridViewPanel";
            this.GridViewPanel.Size = new System.Drawing.Size(761, 200);
            this.GridViewPanel.TabIndex = 0;
            // 
            // verticalScrollBar
            // 
            this.verticalScrollBar.Enabled = false;
            this.verticalScrollBar.Location = new System.Drawing.Point(743, 1);
            this.verticalScrollBar.Name = "verticalScrollBar";
            this.verticalScrollBar.Size = new System.Drawing.Size(16, 202);
            this.verticalScrollBar.TabIndex = 2;
            // 
            // SalesGridView
            // 
            this.SalesGridView.AllowCellClick = false;
            this.SalesGridView.AllowDoubleClick = false;
            this.SalesGridView.AllowEmptyRows = true;
            this.SalesGridView.AllowEnterKey = false;
            this.SalesGridView.AllowSorting = false;
            this.SalesGridView.AllowUserToAddRows = false;
            this.SalesGridView.AllowUserToDeleteRows = false;
            this.SalesGridView.AllowUserToResizeColumns = false;
            this.SalesGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.SalesGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.SalesGridView.ApplyStandardBehaviour = false;
            this.SalesGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SalesGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SalesGridView.ClearCurrentCellOnLeave = false;
            this.SalesGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.SalesGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SalesGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SalesGridView.ColumnHeadersHeight = 24;
            this.SalesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.SalesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SaleDate,
            this.BookPage,
            this.ttlprcls,
            this.Grantor,
            this.SalePrice,
            this.SaleStudy,
            this.Grantee,
            this.EventID,
            this.ThrowAway,
            this.SalePrice1,
            this.SaleID});
            this.SalesGridView.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SalesGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.SalesGridView.DefaultRowIndex = 0;
            this.SalesGridView.DeselectCurrentCell = false;
            this.SalesGridView.DeselectSpecifiedRow = -1;
            this.SalesGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.SalesGridView.EnableBinding = false;
            this.SalesGridView.EnableHeadersVisualStyles = false;
            this.SalesGridView.GridColor = System.Drawing.Color.Black;
            this.SalesGridView.GridContentSelected = false;
            this.SalesGridView.IsEditableGrid = false;
            this.SalesGridView.IsMultiSelect = false;
            this.SalesGridView.IsSorted = false;
            this.SalesGridView.Location = new System.Drawing.Point(-1, 0);
            this.SalesGridView.MultiSelect = false;
            this.SalesGridView.Name = "SalesGridView";
            this.SalesGridView.NumRowsVisible = 8;
            this.SalesGridView.PrimaryKeyColumnName = "";
            this.SalesGridView.RemainSortFields = false;
            this.SalesGridView.RemoveDefaultSelection = true;
            this.SalesGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SalesGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.SalesGridView.RowHeadersVisible = false;
            this.SalesGridView.RowHeadersWidth = 20;
            this.SalesGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.SalesGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SalesGridView.Size = new System.Drawing.Size(761, 200);
            this.SalesGridView.StandardTab = true;
            this.SalesGridView.TabIndex = 1;
            this.SalesGridView.TabStop = false;
            // 
            // SaleDate
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "d";
            this.SaleDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.SaleDate.HeaderText = "Sale Date";
            this.SaleDate.Name = "SaleDate";
            this.SaleDate.Width = 90;
            // 
            // BookPage
            // 
            this.BookPage.HeaderText = "Recording #";
            this.BookPage.Name = "BookPage";
            // 
            // ttlprcls
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ttlprcls.DefaultCellStyle = dataGridViewCellStyle4;
            this.ttlprcls.HeaderText = "Parcels";
            this.ttlprcls.Name = "ttlprcls";
            this.ttlprcls.Width = 80;
            // 
            // Grantor
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Grantor.DefaultCellStyle = dataGridViewCellStyle5;
            this.Grantor.HeaderText = "Grantor \\ Grantee";
            this.Grantor.Name = "Grantor";
            this.Grantor.Width = 272;
            // 
            // SalePrice
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "##,##0";
            this.SalePrice.DefaultCellStyle = dataGridViewCellStyle6;
            this.SalePrice.HeaderText = "Sale Price";
            this.SalePrice.Name = "SalePrice";
            // 
            // SaleStudy
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.SaleStudy.DefaultCellStyle = dataGridViewCellStyle7;
            this.SaleStudy.HeaderText = "Sales Study";
            this.SaleStudy.Name = "SaleStudy";
            // 
            // Grantee
            // 
            this.Grantee.HeaderText = "Grantee";
            this.Grantee.Name = "Grantee";
            this.Grantee.Visible = false;
            // 
            // EventID
            // 
            this.EventID.HeaderText = "EventID";
            this.EventID.Name = "EventID";
            this.EventID.Visible = false;
            // 
            // ThrowAway
            // 
            this.ThrowAway.HeaderText = "ThrowAway";
            this.ThrowAway.Name = "ThrowAway";
            this.ThrowAway.Visible = false;
            // 
            // SalePrice1
            // 
            this.SalePrice1.HeaderText = "SalePrice";
            this.SalePrice1.Name = "SalePrice1";
            this.SalePrice1.Visible = false;
            // 
            // SaleID
            // 
            this.SaleID.HeaderText = "SaleID";
            this.SaleID.Name = "SaleID";
            this.SaleID.Visible = false;
            // 
            // SalesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GridViewPanel);
            this.Name = "SalesUserControl";
            this.Size = new System.Drawing.Size(761, 235);
            this.Load += new System.EventHandler(this.SalesUserControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.GridViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SalesGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private TerraScan.UI.Controls.TerraScanDataGridView SalesGridView;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.VScrollBar verticalScrollBar;
        private System.Windows.Forms.Panel GridViewPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BookPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ttlprcls;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grantor;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleStudy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grantee;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThrowAway;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalePrice1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleID;   
    }
}
