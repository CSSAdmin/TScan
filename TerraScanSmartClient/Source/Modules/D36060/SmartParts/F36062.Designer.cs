namespace D36060
{
    partial class F36062
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TitleHeaderPanel = new System.Windows.Forms.Panel();
            this.LandInfluencesHeaderLabel = new System.Windows.Forms.Label();
            this.LandInfluenceLabel = new System.Windows.Forms.Label();
            this.InfluenceTablePictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LandInfluenceGridVerticalScroll = new System.Windows.Forms.VScrollBar();
            this.LandInflueneDataGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.InfluenceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Influence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InfluenceTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Isdeleted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsModified = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.F36062InfluenceFormToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Panel2 = new System.Windows.Forms.Panel();
            this.TitleHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfluenceTablePictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LandInflueneDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleHeaderPanel
            // 
            this.TitleHeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(97)))), ((int)(((byte)(137)))));
            this.TitleHeaderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TitleHeaderPanel.Controls.Add(this.LandInfluencesHeaderLabel);
            this.TitleHeaderPanel.Controls.Add(this.LandInfluenceLabel);
            this.TitleHeaderPanel.Location = new System.Drawing.Point(-2, -1);
            this.TitleHeaderPanel.Name = "TitleHeaderPanel";
            this.TitleHeaderPanel.Size = new System.Drawing.Size(770, 30);
            this.TitleHeaderPanel.TabIndex = 0;
            // 
            // LandInfluencesHeaderLabel
            // 
            this.LandInfluencesHeaderLabel.AutoSize = true;
            this.LandInfluencesHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.LandInfluencesHeaderLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LandInfluencesHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.LandInfluencesHeaderLabel.Location = new System.Drawing.Point(4, 4);
            this.LandInfluencesHeaderLabel.Name = "LandInfluencesHeaderLabel";
            this.LandInfluencesHeaderLabel.Size = new System.Drawing.Size(182, 16);
            this.LandInfluencesHeaderLabel.TabIndex = 3;
            this.LandInfluencesHeaderLabel.Text = "Influences – Neighborhood:";
            // 
            // LandInfluenceLabel
            // 
            this.LandInfluenceLabel.AutoSize = true;
            this.LandInfluenceLabel.BackColor = System.Drawing.Color.Transparent;
            this.LandInfluenceLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LandInfluenceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.LandInfluenceLabel.Location = new System.Drawing.Point(185, 5);
            this.LandInfluenceLabel.Name = "LandInfluenceLabel";
            this.LandInfluenceLabel.Size = new System.Drawing.Size(0, 16);
            this.LandInfluenceLabel.TabIndex = 3;
            // 
            // InfluenceTablePictureBox
            // 
            this.InfluenceTablePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.InfluenceTablePictureBox.Location = new System.Drawing.Point(761, 0);
            this.InfluenceTablePictureBox.Name = "InfluenceTablePictureBox";
            this.InfluenceTablePictureBox.Size = new System.Drawing.Size(45, 291);
            this.InfluenceTablePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.InfluenceTablePictureBox.TabIndex = 41;
            this.InfluenceTablePictureBox.TabStop = false;
            this.InfluenceTablePictureBox.Click += new System.EventHandler(this.InfluenceTablePictureBox_Click);
            this.InfluenceTablePictureBox.MouseHover += new System.EventHandler(this.InfluenceTablePictureBox_MouseHover);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.LandInfluenceGridVerticalScroll);
            this.panel1.Controls.Add(this.LandInflueneDataGridView);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 291);
            this.panel1.TabIndex = 42;
            // 
            // LandInfluenceGridVerticalScroll
            // 
            this.LandInfluenceGridVerticalScroll.Enabled = false;
            this.LandInfluenceGridVerticalScroll.Location = new System.Drawing.Point(750, 29);
            this.LandInfluenceGridVerticalScroll.Name = "LandInfluenceGridVerticalScroll";
            this.LandInfluenceGridVerticalScroll.Size = new System.Drawing.Size(19, 262);
            this.LandInfluenceGridVerticalScroll.TabIndex = 166;
            this.LandInfluenceGridVerticalScroll.Visible = false;
            // 
            // LandInflueneDataGridView
            // 
            this.LandInflueneDataGridView.AllowCellClick = true;
            this.LandInflueneDataGridView.AllowDoubleClick = true;
            this.LandInflueneDataGridView.AllowEmptyRows = true;
            this.LandInflueneDataGridView.AllowEnterKey = false;
            this.LandInflueneDataGridView.AllowSorting = false;
            this.LandInflueneDataGridView.AllowUserToAddRows = false;
            this.LandInflueneDataGridView.AllowUserToDeleteRows = false;
            this.LandInflueneDataGridView.AllowUserToResizeColumns = false;
            this.LandInflueneDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.LandInflueneDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.LandInflueneDataGridView.ApplyStandardBehaviour = false;
            this.LandInflueneDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.LandInflueneDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LandInflueneDataGridView.ClearCurrentCellOnLeave = true;
            this.LandInflueneDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LandInflueneDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.LandInflueneDataGridView.ColumnHeadersHeight = 22;
            this.LandInflueneDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.LandInflueneDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InfluenceType,
            this.Type,
            this.Influence,
            this.Description,
            this.InfluenceTypeID,
            this.Isdeleted,
            this.IsModified});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.LandInflueneDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.LandInflueneDataGridView.DefaultRowIndex = 0;
            this.LandInflueneDataGridView.DeselectCurrentCell = false;
            this.LandInflueneDataGridView.DeselectSpecifiedRow = -1;
            this.LandInflueneDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.LandInflueneDataGridView.EnableBinding = true;
            this.LandInflueneDataGridView.EnableHeadersVisualStyles = false;
            this.LandInflueneDataGridView.GridColor = System.Drawing.Color.Black;
            this.LandInflueneDataGridView.GridContentSelected = false;
            this.LandInflueneDataGridView.IsEditableGrid = true;
            this.LandInflueneDataGridView.IsMultiSelect = false;
            this.LandInflueneDataGridView.IsSorted = false;
            this.LandInflueneDataGridView.Location = new System.Drawing.Point(0, 27);
            this.LandInflueneDataGridView.MultiSelect = false;
            this.LandInflueneDataGridView.Name = "LandInflueneDataGridView";
            this.LandInflueneDataGridView.NumRowsVisible = 11;
            this.LandInflueneDataGridView.PrimaryKeyColumnName = "";
            this.LandInflueneDataGridView.RemainSortFields = false;
            this.LandInflueneDataGridView.RemoveDefaultSelection = false;
            this.LandInflueneDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LandInflueneDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.LandInflueneDataGridView.RowHeadersWidth = 20;
            this.LandInflueneDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.LandInflueneDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LandInflueneDataGridView.Size = new System.Drawing.Size(767, 265);
            this.LandInflueneDataGridView.TabIndex = 17;
            this.LandInflueneDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.LandInflueneDataGridView_CellValueChanged);
            this.LandInflueneDataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.LandInflueneDataGridView_CellBeginEdit);
            this.LandInflueneDataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.LandInflueneDataGridView_CellLeave);
            this.LandInflueneDataGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.LandInflueneDataGridView_CellParsing);
            this.LandInflueneDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LandInflueneDataGridView_CellFormatting);
            this.LandInflueneDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.LandInflueneDataGridView_CellEndEdit);
            this.LandInflueneDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.LandInflueneDataGridView_EditingControlShowing);
            this.LandInflueneDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LandInflueneDataGridView_KeyDown);
            this.LandInflueneDataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.LandInflueneDataGridView_CellEnter);
            // 
            // InfluenceType
            // 
            this.InfluenceType.HeaderText = "Influence Type";
            this.InfluenceType.MaxInputLength = 30;
            this.InfluenceType.Name = "InfluenceType";
            this.InfluenceType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InfluenceType.Width = 250;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // Influence
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "#,#00.00";
            dataGridViewCellStyle3.NullValue = null;
            this.Influence.DefaultCellStyle = dataGridViewCellStyle3;
            this.Influence.HeaderText = "Influence";
            this.Influence.MaxInputLength = 10;
            this.Influence.Name = "Influence";
            this.Influence.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.MaxInputLength = 50;
            this.Description.Name = "Description";
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Description.Width = 280;
            // 
            // InfluenceTypeID
            // 
            this.InfluenceTypeID.HeaderText = "InfluenceTypeID";
            this.InfluenceTypeID.Name = "InfluenceTypeID";
            this.InfluenceTypeID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InfluenceTypeID.Visible = false;
            // 
            // Isdeleted
            // 
            this.Isdeleted.HeaderText = "Isdeleted";
            this.Isdeleted.Name = "Isdeleted";
            this.Isdeleted.Visible = false;
            // 
            // IsModified
            // 
            this.IsModified.HeaderText = "IsModified";
            this.IsModified.Name = "IsModified";
            this.IsModified.Visible = false;
            // 
            // Panel2
            // 
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(807, 290);
            this.Panel2.TabIndex = 43;
            // 
            // F36062
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.TitleHeaderPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.InfluenceTablePictureBox);
            this.Controls.Add(this.Panel2);
            this.Location = new System.Drawing.Point(0, 5);
            this.Name = "F36062";
            this.Size = new System.Drawing.Size(810, 300);
            this.Tag = "36062";
            this.Load += new System.EventHandler(this.F36062_Load);
            this.TitleHeaderPanel.ResumeLayout(false);
            this.TitleHeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfluenceTablePictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LandInflueneDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TitleHeaderPanel;
        private System.Windows.Forms.Label LandInfluencesHeaderLabel;
        private System.Windows.Forms.Label LandInfluenceLabel;
        private System.Windows.Forms.PictureBox InfluenceTablePictureBox;
        private System.Windows.Forms.ToolTip F36062DeprToolTip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Panel2;  
        private TerraScan.UI.Controls.TerraScanDataGridView LandInflueneDataGridView;
        private System.Windows.Forms.VScrollBar LandInfluenceGridVerticalScroll;
        private System.Windows.Forms.ToolTip F36062InfluenceFormToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn InfluenceType;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Influence;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn InfluenceTypeID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Isdeleted;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsModified;
    }
}
