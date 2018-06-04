namespace D24530
{
    partial class F29530
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CIDPanel = new System.Windows.Forms.Panel();
            this.AssociationEventGridVscrollBar = new System.Windows.Forms.VScrollBar();
            this.AssociationEventsGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.AssociationEventGridpictureBox = new System.Windows.Forms.PictureBox();
            this.EventAssociationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.LinkText = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Form = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Param1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Param2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Param3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssociationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIDPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AssociationEventsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssociationEventGridpictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CIDPanel
            // 
            this.CIDPanel.BackColor = System.Drawing.Color.Transparent;
            this.CIDPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CIDPanel.Controls.Add(this.AssociationEventGridVscrollBar);
            this.CIDPanel.Controls.Add(this.AssociationEventsGridView);
            this.CIDPanel.Location = new System.Drawing.Point(0, 0);
            this.CIDPanel.Name = "CIDPanel";
            this.CIDPanel.Size = new System.Drawing.Size(769, 68);
            this.CIDPanel.TabIndex = 4;
            this.CIDPanel.TabStop = true;
            // 
            // AssociationEventGridVscrollBar
            // 
            this.AssociationEventGridVscrollBar.Enabled = false;
            this.AssociationEventGridVscrollBar.Location = new System.Drawing.Point(750, -1);
            this.AssociationEventGridVscrollBar.Name = "AssociationEventGridVscrollBar";
            this.AssociationEventGridVscrollBar.Size = new System.Drawing.Size(16, 66);
            this.AssociationEventGridVscrollBar.TabIndex = 1045;
            // 
            // AssociationEventsGridView
            // 
            this.AssociationEventsGridView.AllowCellClick = true;
            this.AssociationEventsGridView.AllowDoubleClick = false;
            this.AssociationEventsGridView.AllowEmptyRows = true;
            this.AssociationEventsGridView.AllowEnterKey = false;
            this.AssociationEventsGridView.AllowSorting = true;
            this.AssociationEventsGridView.AllowUserToAddRows = false;
            this.AssociationEventsGridView.AllowUserToDeleteRows = false;
            this.AssociationEventsGridView.AllowUserToResizeColumns = false;
            this.AssociationEventsGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.AssociationEventsGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.AssociationEventsGridView.ApplyStandardBehaviour = false;
            this.AssociationEventsGridView.BackgroundColor = System.Drawing.Color.White;
            this.AssociationEventsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AssociationEventsGridView.ClearCurrentCellOnLeave = true;
            this.AssociationEventsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.AssociationEventsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.AssociationEventsGridView.ColumnHeadersHeight = 24;
            this.AssociationEventsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.AssociationEventsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LinkText,
            this.Description,
            this.Form,
            this.Param1,
            this.Param2,
            this.Param3,
            this.AssociationID});
            this.AssociationEventsGridView.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AssociationEventsGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.AssociationEventsGridView.DefaultRowIndex = 0;
            this.AssociationEventsGridView.DeselectCurrentCell = false;
            this.AssociationEventsGridView.DeselectSpecifiedRow = -1;
            this.AssociationEventsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.AssociationEventsGridView.EnableBinding = true;
            this.AssociationEventsGridView.EnableHeadersVisualStyles = false;
            this.AssociationEventsGridView.GridColor = System.Drawing.Color.Black;
            this.AssociationEventsGridView.GridContentSelected = false;
            this.AssociationEventsGridView.IsEditableGrid = true;
            this.AssociationEventsGridView.IsSorted = true;
            this.AssociationEventsGridView.Location = new System.Drawing.Point(-2, -1);
            this.AssociationEventsGridView.MultiSelect = false;
            this.AssociationEventsGridView.Name = "AssociationEventsGridView";
            this.AssociationEventsGridView.NumRowsVisible = 2;
            this.AssociationEventsGridView.PrimaryKeyColumnName = "AssociationID";
            this.AssociationEventsGridView.RemainSortFields = false;
            this.AssociationEventsGridView.RemoveDefaultSelection = true;
            this.AssociationEventsGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AssociationEventsGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.AssociationEventsGridView.RowHeadersWidth = 20;
            this.AssociationEventsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.AssociationEventsGridView.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.AssociationEventsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AssociationEventsGridView.Size = new System.Drawing.Size(769, 69);
            this.AssociationEventsGridView.TabIndex = 4;
            this.AssociationEventsGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.AssociationEventsGridView_CellFormatting);
            this.AssociationEventsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AssociationEventsGridView_CellContentClick);
            // 
            // AssociationEventGridpictureBox
            // 
            this.AssociationEventGridpictureBox.BackColor = System.Drawing.Color.White;
            this.AssociationEventGridpictureBox.Location = new System.Drawing.Point(761, 0);
            this.AssociationEventGridpictureBox.Name = "AssociationEventGridpictureBox";
            this.AssociationEventGridpictureBox.Size = new System.Drawing.Size(42, 68);
            this.AssociationEventGridpictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AssociationEventGridpictureBox.TabIndex = 124;
            this.AssociationEventGridpictureBox.TabStop = false;
            this.AssociationEventGridpictureBox.Click += new System.EventHandler(this.AssociationEventGridpictureBox_Click);
            this.AssociationEventGridpictureBox.MouseHover += new System.EventHandler(this.AssociationEventGridpictureBox_MouseHover);
            // 
            // LinkText
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LinkText.DefaultCellStyle = dataGridViewCellStyle3;
            this.LinkText.HeaderText = "Go To";
            this.LinkText.Name = "LinkText";
            this.LinkText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.LinkText.Width = 263;
            // 
            // Description
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Description.DefaultCellStyle = dataGridViewCellStyle4;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Description.Width = 469;
            // 
            // Form
            // 
            this.Form.HeaderText = "Form";
            this.Form.Name = "Form";
            this.Form.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Form.Visible = false;
            this.Form.Width = 5;
            // 
            // Param1
            // 
            this.Param1.HeaderText = "Param1";
            this.Param1.Name = "Param1";
            this.Param1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Param1.Visible = false;
            this.Param1.Width = 5;
            // 
            // Param2
            // 
            this.Param2.HeaderText = "Param2";
            this.Param2.Name = "Param2";
            this.Param2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Param2.Visible = false;
            this.Param2.Width = 5;
            // 
            // Param3
            // 
            this.Param3.HeaderText = "Param3";
            this.Param3.Name = "Param3";
            this.Param3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Param3.Visible = false;
            this.Param3.Width = 5;
            // 
            // AssociationID
            // 
            this.AssociationID.HeaderText = "AssociationID";
            this.AssociationID.Name = "AssociationID";
            this.AssociationID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.AssociationID.Visible = false;
            this.AssociationID.Width = 5;
            // 
            // F29530
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.CIDPanel);
            this.Controls.Add(this.AssociationEventGridpictureBox);
            this.Name = "F29530";
            this.Size = new System.Drawing.Size(804, 69);
            this.Tag = "29530";
            this.Load += new System.EventHandler(this.F29530_Load);
            this.CIDPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AssociationEventsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssociationEventGridpictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel CIDPanel;
        private TerraScan.UI.Controls.TerraScanDataGridView AssociationEventsGridView;
        private System.Windows.Forms.PictureBox AssociationEventGridpictureBox;
        private System.Windows.Forms.ToolTip EventAssociationToolTip;
        private System.Windows.Forms.VScrollBar AssociationEventGridVscrollBar;
        private System.Windows.Forms.DataGridViewLinkColumn LinkText;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Form;
        private System.Windows.Forms.DataGridViewTextBoxColumn Param1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Param2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Param3;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssociationID;

    }
}
