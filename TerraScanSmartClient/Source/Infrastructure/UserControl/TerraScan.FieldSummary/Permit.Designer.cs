namespace TerraScan.FieldSummary
{
    partial class PermitUserControl
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
            this.PermitGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.ParcelId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParcelNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventTypeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermitNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Visited = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Closed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Headerpanel.SuspendLayout();
            this.GridViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PermitGridView)).BeginInit();
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
            this.HeaderLabel.Size = new System.Drawing.Size(135, 19);
            this.HeaderLabel.TabIndex = 24;
            this.HeaderLabel.Text = "Building Permits";
            this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GridViewPanel
            // 
            this.GridViewPanel.Controls.Add(this.verticalScrollBar);
            this.GridViewPanel.Controls.Add(this.PermitGridView);
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
            this.verticalScrollBar.Size = new System.Drawing.Size(16, 201);
            this.verticalScrollBar.TabIndex = 3;
            // 
            // PermitGridView
            // 
            this.PermitGridView.AllowCellClick = false;
            this.PermitGridView.AllowDoubleClick = false;
            this.PermitGridView.AllowEmptyRows = false;
            this.PermitGridView.AllowEnterKey = false;
            this.PermitGridView.AllowSorting = false;
            this.PermitGridView.AllowUserToAddRows = false;
            this.PermitGridView.AllowUserToDeleteRows = false;
            this.PermitGridView.AllowUserToResizeColumns = false;
            this.PermitGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.PermitGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.PermitGridView.ApplyStandardBehaviour = false;
            this.PermitGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.PermitGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PermitGridView.ClearCurrentCellOnLeave = true;
            this.PermitGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.PermitGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PermitGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.PermitGridView.ColumnHeadersHeight = 24;
            this.PermitGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.PermitGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParcelId,
            this.ParcelNumber,
            this.EventTypeId,
            this.EventDate,
            this.Descr,
            this.PermitNumber,
            this.Description,
            this.Visited,
            this.Closed,
            this.EstValue,
            this.EventId});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PermitGridView.DefaultCellStyle = dataGridViewCellStyle9;
            this.PermitGridView.DefaultRowIndex = 0;
            this.PermitGridView.DeselectCurrentCell = false;
            this.PermitGridView.DeselectSpecifiedRow = -1;
            this.PermitGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.PermitGridView.EnableBinding = false;
            this.PermitGridView.EnableHeadersVisualStyles = false;
            this.PermitGridView.GridColor = System.Drawing.Color.Black;
            this.PermitGridView.GridContentSelected = false;
            this.PermitGridView.IsEditableGrid = false;
            this.PermitGridView.IsMultiSelect = false;
            this.PermitGridView.IsSorted = false;
            this.PermitGridView.Location = new System.Drawing.Point(-1, 0);
            this.PermitGridView.MultiSelect = false;
            this.PermitGridView.Name = "PermitGridView";
            this.PermitGridView.NumRowsVisible = 8;
            this.PermitGridView.PrimaryKeyColumnName = "";
            this.PermitGridView.RemainSortFields = false;
            this.PermitGridView.RemoveDefaultSelection = true;
            this.PermitGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PermitGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.PermitGridView.RowHeadersVisible = false;
            this.PermitGridView.RowHeadersWidth = 20;
            this.PermitGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PermitGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PermitGridView.Size = new System.Drawing.Size(761, 200);
            this.PermitGridView.StandardTab = true;
            this.PermitGridView.TabIndex = 2;
            this.PermitGridView.TabStop = false;
            this.PermitGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PermitGridView_CellContentClick);
            // 
            // ParcelId
            // 
            this.ParcelId.HeaderText = "ParcelId";
            this.ParcelId.Name = "ParcelId";
            this.ParcelId.Visible = false;
            // 
            // ParcelNumber
            // 
            this.ParcelNumber.HeaderText = "ParcelNumber";
            this.ParcelNumber.Name = "ParcelNumber";
            this.ParcelNumber.Visible = false;
            // 
            // EventTypeId
            // 
            this.EventTypeId.HeaderText = "EventTypeId";
            this.EventTypeId.Name = "EventTypeId";
            this.EventTypeId.Visible = false;
            // 
            // EventDate
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.EventDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.EventDate.HeaderText = "Opened";
            this.EventDate.Name = "EventDate";
            // 
            // Descr
            // 
            this.Descr.HeaderText = "Descr";
            this.Descr.Name = "Descr";
            this.Descr.Visible = false;
            // 
            // PermitNumber
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PermitNumber.DefaultCellStyle = dataGridViewCellStyle4;
            this.PermitNumber.HeaderText = "Permit #";
            this.PermitNumber.Name = "PermitNumber";
            this.PermitNumber.Width = 145;
            // 
            // Description
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Description.DefaultCellStyle = dataGridViewCellStyle5;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.Width = 197;
            // 
            // Visited
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Visited.DefaultCellStyle = dataGridViewCellStyle6;
            this.Visited.HeaderText = "Visited";
            this.Visited.Name = "Visited";
            // 
            // Closed
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Closed.DefaultCellStyle = dataGridViewCellStyle7;
            this.Closed.HeaderText = "Closed";
            this.Closed.Name = "Closed";
            // 
            // EstValue
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "##,##0";
            dataGridViewCellStyle8.NullValue = null;
            this.EstValue.DefaultCellStyle = dataGridViewCellStyle8;
            this.EstValue.HeaderText = "Amount";
            this.EstValue.Name = "EstValue";
            // 
            // EventId
            // 
            this.EventId.HeaderText = "EventId";
            this.EventId.Name = "EventId";
            this.EventId.Visible = false;
            // 
            // PermitUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Headerpanel);
            this.Controls.Add(this.GridViewPanel);
            this.Name = "PermitUserControl";
            this.Size = new System.Drawing.Size(761, 235);
            this.Load += new System.EventHandler(this.PermitUserControl_Load);
            this.Headerpanel.ResumeLayout(false);
            this.Headerpanel.PerformLayout();
            this.GridViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PermitGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Headerpanel;
        private TerraScan.UI.Controls.TerraScanDataGridView PermitGridView;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.VScrollBar verticalScrollBar;
        private System.Windows.Forms.Panel GridViewPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParcelId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParcelNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventTypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descr;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermitNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Visited;
        private System.Windows.Forms.DataGridViewTextBoxColumn Closed;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventId;
        
        

    }
}
