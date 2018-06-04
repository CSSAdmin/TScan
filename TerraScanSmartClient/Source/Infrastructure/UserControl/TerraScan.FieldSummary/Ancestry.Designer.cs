namespace TerraScan.FieldSummary
{
    partial class Ancestry
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Headerpanel = new System.Windows.Forms.Panel();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.GridViewPanel = new System.Windows.Forms.Panel();
            this.verticalScrollBar = new System.Windows.Forms.VScrollBar();
            this.AncestryGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.EventDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Relation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParcelNumber = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParcelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Headerpanel.SuspendLayout();
            this.GridViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AncestryGridView)).BeginInit();
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
            this.HeaderLabel.Size = new System.Drawing.Size(128, 19);
            this.HeaderLabel.TabIndex = 26;
            this.HeaderLabel.Text = "Parcel Ancestry";
            this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GridViewPanel
            // 
            this.GridViewPanel.Controls.Add(this.verticalScrollBar);
            this.GridViewPanel.Controls.Add(this.AncestryGridView);
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
            this.verticalScrollBar.Size = new System.Drawing.Size(16, 200);
            this.verticalScrollBar.TabIndex = 7;
            // 
            // AncestryGridView
            // 
            this.AncestryGridView.AllowCellClick = false;
            this.AncestryGridView.AllowDoubleClick = false;
            this.AncestryGridView.AllowEmptyRows = true;
            this.AncestryGridView.AllowEnterKey = false;
            this.AncestryGridView.AllowSorting = false;
            this.AncestryGridView.AllowUserToAddRows = false;
            this.AncestryGridView.AllowUserToDeleteRows = false;
            this.AncestryGridView.AllowUserToResizeColumns = false;
            this.AncestryGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.AncestryGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.AncestryGridView.ApplyStandardBehaviour = false;
            this.AncestryGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.AncestryGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AncestryGridView.ClearCurrentCellOnLeave = true;
            this.AncestryGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.AncestryGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.AncestryGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.AncestryGridView.ColumnHeadersHeight = 24;
            this.AncestryGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.AncestryGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EventDate,
            this.EventType,
            this.Relation,
            this.ParcelNumber,
            this.Status,
            this.ProcessedBy,
            this.EventTypeID,
            this.EventID,
            this.ParcelID});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AncestryGridView.DefaultCellStyle = dataGridViewCellStyle9;
            this.AncestryGridView.DefaultRowIndex = 0;
            this.AncestryGridView.DeselectCurrentCell = false;
            this.AncestryGridView.DeselectSpecifiedRow = -1;
            this.AncestryGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.AncestryGridView.EnableBinding = false;
            this.AncestryGridView.EnableHeadersVisualStyles = false;
            this.AncestryGridView.GridColor = System.Drawing.Color.Black;
            this.AncestryGridView.GridContentSelected = false;
            this.AncestryGridView.IsEditableGrid = false;
            this.AncestryGridView.IsMultiSelect = false;
            this.AncestryGridView.IsSorted = false;
            this.AncestryGridView.Location = new System.Drawing.Point(-1, 0);
            this.AncestryGridView.MultiSelect = false;
            this.AncestryGridView.Name = "AncestryGridView";
            this.AncestryGridView.NumRowsVisible = 8;
            this.AncestryGridView.PrimaryKeyColumnName = "";
            this.AncestryGridView.RemainSortFields = false;
            this.AncestryGridView.RemoveDefaultSelection = true;
            this.AncestryGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AncestryGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.AncestryGridView.RowHeadersVisible = false;
            this.AncestryGridView.RowHeadersWidth = 20;
            this.AncestryGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.AncestryGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AncestryGridView.Size = new System.Drawing.Size(761, 200);
            this.AncestryGridView.StandardTab = true;
            this.AncestryGridView.TabIndex = 4;
            this.AncestryGridView.TabStop = false;
            // 
            // EventDate
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.EventDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.EventDate.HeaderText = "Event Date";
            this.EventDate.Name = "EventDate";
            this.EventDate.Width = 92;
            // 
            // EventType
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.EventType.DefaultCellStyle = dataGridViewCellStyle4;
            this.EventType.HeaderText = "Event Type";
            this.EventType.Name = "EventType";
            this.EventType.Width = 150;
            // 
            // Relation
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Relation.DefaultCellStyle = dataGridViewCellStyle5;
            this.Relation.HeaderText = "Relation";
            this.Relation.Name = "Relation";
            this.Relation.Width = 80;
            // 
            // ParcelNumber
            // 
            this.ParcelNumber.ActiveLinkColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ParcelNumber.DefaultCellStyle = dataGridViewCellStyle6;
            this.ParcelNumber.HeaderText = "Parcel / Roll Year";
            this.ParcelNumber.LinkColor = System.Drawing.Color.Blue;
            this.ParcelNumber.Name = "ParcelNumber";
            this.ParcelNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ParcelNumber.VisitedLinkColor = System.Drawing.Color.Blue;
            this.ParcelNumber.Width = 195;
            // 
            // Status
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Status.DefaultCellStyle = dataGridViewCellStyle7;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 75;
            // 
            // ProcessedBy
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProcessedBy.DefaultCellStyle = dataGridViewCellStyle8;
            this.ProcessedBy.HeaderText = "Processed By";
            this.ProcessedBy.Name = "ProcessedBy";
            this.ProcessedBy.Width = 150;
            // 
            // EventTypeID
            // 
            this.EventTypeID.HeaderText = "EventTypeId";
            this.EventTypeID.Name = "EventTypeID";
            this.EventTypeID.Visible = false;
            // 
            // EventID
            // 
            this.EventID.HeaderText = "EventId";
            this.EventID.Name = "EventID";
            this.EventID.Visible = false;
            // 
            // ParcelID
            // 
            this.ParcelID.HeaderText = "ParcelId";
            this.ParcelID.Name = "ParcelID";
            this.ParcelID.Visible = false;
            // 
            // Ancestry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Headerpanel);
            this.Controls.Add(this.GridViewPanel);
            this.Name = "Ancestry";
            this.Size = new System.Drawing.Size(761, 235);
            this.Load += new System.EventHandler(this.Ancestry_Load);
            this.Headerpanel.ResumeLayout(false);
            this.Headerpanel.PerformLayout();
            this.GridViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AncestryGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Headerpanel;
        private TerraScan.UI.Controls.TerraScanDataGridView AncestryGridView;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.VScrollBar verticalScrollBar;
        private System.Windows.Forms.Panel GridViewPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Relation;
        private System.Windows.Forms.DataGridViewLinkColumn ParcelNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventTypeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParcelID;


    }
}
