namespace TerraScan.FieldSummary
{
    partial class OwnerShipUserControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Headerpanel = new System.Windows.Forms.Panel();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.GridViewPanel = new System.Windows.Forms.Panel();
            this.verticalScrollBar = new System.Windows.Forms.VScrollBar();
            this.OwnershipGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.LastFirst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OwnerPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OwnerType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OwnerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParcelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsPrimary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsTaxPayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BackgroundColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Headerpanel.SuspendLayout();
            this.GridViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OwnershipGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Headerpanel
            // 
            this.Headerpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(36)))), ((int)(((byte)(60)))));
            this.Headerpanel.Controls.Add(this.HeaderLabel);
            this.Headerpanel.Location = new System.Drawing.Point(0, 0);
            this.Headerpanel.Name = "Headerpanel";
            this.Headerpanel.Size = new System.Drawing.Size(768, 35);
            this.Headerpanel.TabIndex = 0;
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.HeaderLabel.ForeColor = System.Drawing.Color.White;
            this.HeaderLabel.Location = new System.Drawing.Point(0, 4);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.HeaderLabel.Size = new System.Drawing.Size(144, 19);
            this.HeaderLabel.TabIndex = 27;
            this.HeaderLabel.Text = "Parcel Ownership";
            this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GridViewPanel
            // 
            this.GridViewPanel.Controls.Add(this.verticalScrollBar);
            this.GridViewPanel.Controls.Add(this.OwnershipGridView);
            this.GridViewPanel.Location = new System.Drawing.Point(0, 34);
            this.GridViewPanel.Name = "GridViewPanel";
            this.GridViewPanel.Size = new System.Drawing.Size(761, 200);
            this.GridViewPanel.TabIndex = 0;
            // 
            // verticalScrollBar
            // 
            this.verticalScrollBar.Enabled = false;
            this.verticalScrollBar.Location = new System.Drawing.Point(743, 0);
            this.verticalScrollBar.Name = "verticalScrollBar";
            this.verticalScrollBar.Size = new System.Drawing.Size(16, 201);
            this.verticalScrollBar.TabIndex = 6;
            // 
            // OwnershipGridView
            // 
            this.OwnershipGridView.AllowCellClick = false;
            this.OwnershipGridView.AllowDoubleClick = false;
            this.OwnershipGridView.AllowEmptyRows = true;
            this.OwnershipGridView.AllowEnterKey = false;
            this.OwnershipGridView.AllowSorting = false;
            this.OwnershipGridView.AllowUserToAddRows = false;
            this.OwnershipGridView.AllowUserToDeleteRows = false;
            this.OwnershipGridView.AllowUserToResizeColumns = false;
            this.OwnershipGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.OwnershipGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.OwnershipGridView.ApplyStandardBehaviour = false;
            this.OwnershipGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.OwnershipGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OwnershipGridView.ClearCurrentCellOnLeave = true;
            this.OwnershipGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.OwnershipGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OwnershipGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.OwnershipGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.OwnershipGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LastFirst,
            this.OwnerPercent,
            this.OwnerType,
            this.OwnerID,
            this.ParcelID,
            this.IsPrimary,
            this.IsTaxPayer,
            this.BackgroundColor});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.OwnershipGridView.DefaultCellStyle = dataGridViewCellStyle10;
            this.OwnershipGridView.DefaultRowIndex = 0;
            this.OwnershipGridView.DeselectCurrentCell = false;
            this.OwnershipGridView.DeselectSpecifiedRow = -1;
            this.OwnershipGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.OwnershipGridView.EnableBinding = false;
            this.OwnershipGridView.EnableHeadersVisualStyles = false;
            this.OwnershipGridView.GridColor = System.Drawing.Color.Black;
            this.OwnershipGridView.GridContentSelected = false;
            this.OwnershipGridView.IsEditableGrid = false;
            this.OwnershipGridView.IsMultiSelect = false;
            this.OwnershipGridView.IsSorted = false;
            this.OwnershipGridView.Location = new System.Drawing.Point(-1, 0);
            this.OwnershipGridView.MultiSelect = false;
            this.OwnershipGridView.Name = "OwnershipGridView";
            this.OwnershipGridView.NumRowsVisible = 8;
            this.OwnershipGridView.PrimaryKeyColumnName = "";
            this.OwnershipGridView.RemainSortFields = false;
            this.OwnershipGridView.RemoveDefaultSelection = true;
            this.OwnershipGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OwnershipGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.OwnershipGridView.RowHeadersVisible = false;
            this.OwnershipGridView.RowHeadersWidth = 20;
            this.OwnershipGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.OwnershipGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OwnershipGridView.Size = new System.Drawing.Size(761, 200);
            this.OwnershipGridView.StandardTab = true;
            this.OwnershipGridView.TabIndex = 5;
            this.OwnershipGridView.TabStop = false;
            this.OwnershipGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.OwnershipGridView_CellFormatting);
            // 
            // LastFirst
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.LastFirst.DefaultCellStyle = dataGridViewCellStyle3;
            this.LastFirst.HeaderText = "Owner";
            this.LastFirst.Name = "LastFirst";
            this.LastFirst.Width = 268;
            // 
            // OwnerPercent
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "0.00 %";
            dataGridViewCellStyle4.NullValue = null;
            this.OwnerPercent.DefaultCellStyle = dataGridViewCellStyle4;
            this.OwnerPercent.HeaderText = "%";
            this.OwnerPercent.Name = "OwnerPercent";
            // 
            // OwnerType
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.OwnerType.DefaultCellStyle = dataGridViewCellStyle5;
            this.OwnerType.HeaderText = "Owner Type";
            this.OwnerType.Name = "OwnerType";
            this.OwnerType.Width = 173;
            // 
            // OwnerID
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.OwnerID.DefaultCellStyle = dataGridViewCellStyle6;
            this.OwnerID.HeaderText = "OwnerID";
            this.OwnerID.Name = "OwnerID";
            this.OwnerID.Visible = false;
            // 
            // ParcelID
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ParcelID.DefaultCellStyle = dataGridViewCellStyle7;
            this.ParcelID.HeaderText = "ParcelID";
            this.ParcelID.Name = "ParcelID";
            this.ParcelID.Visible = false;
            // 
            // IsPrimary
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IsPrimary.DefaultCellStyle = dataGridViewCellStyle8;
            this.IsPrimary.HeaderText = "Primary";
            this.IsPrimary.Name = "IsPrimary";
            // 
            // IsTaxPayer
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IsTaxPayer.DefaultCellStyle = dataGridViewCellStyle9;
            this.IsTaxPayer.HeaderText = "Tax Payer";
            this.IsTaxPayer.Name = "IsTaxPayer";
            // 
            // BackgroundColor
            // 
            this.BackgroundColor.HeaderText = "BackGroundColor";
            this.BackgroundColor.Name = "BackgroundColor";
            this.BackgroundColor.Visible = false;
            // 
            // OwnerShipUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridViewPanel);
            this.Controls.Add(this.Headerpanel);
            this.Name = "OwnerShipUserControl";
            this.Size = new System.Drawing.Size(761, 235);
            this.Load += new System.EventHandler(this.OwnerShipUserControl_Load);
            this.Headerpanel.ResumeLayout(false);
            this.Headerpanel.PerformLayout();
            this.GridViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OwnershipGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Headerpanel;
        private TerraScan.UI.Controls.TerraScanDataGridView OwnershipGridView;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.VScrollBar verticalScrollBar;
        private System.Windows.Forms.Panel GridViewPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastFirst;
        private System.Windows.Forms.DataGridViewTextBoxColumn OwnerPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn OwnerType;
        private System.Windows.Forms.DataGridViewTextBoxColumn OwnerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParcelID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsPrimary;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsTaxPayer;
        private System.Windows.Forms.DataGridViewTextBoxColumn BackgroundColor;


    }
}
