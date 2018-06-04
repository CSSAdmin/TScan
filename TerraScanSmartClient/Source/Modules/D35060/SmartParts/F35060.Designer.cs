namespace D35060
{
    partial class F35060
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ItemCodeGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.ItemCodeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Abstract = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCodePanel = new System.Windows.Forms.Panel();
            this.ItemCodeScroll = new System.Windows.Forms.VScrollBar();
            this.FooterPanel = new System.Windows.Forms.Panel();
            this.ItemCodePictureBox = new System.Windows.Forms.PictureBox();
            this.ItemCodeToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ItemCodeGridView)).BeginInit();
            this.ItemCodePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemCodePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ItemCodeGridView
            // 
            this.ItemCodeGridView.AllowCellClick = true;
            this.ItemCodeGridView.AllowDoubleClick = false;
            this.ItemCodeGridView.AllowEmptyRows = true;
            this.ItemCodeGridView.AllowEnterKey = false;
            this.ItemCodeGridView.AllowSorting = true;
            this.ItemCodeGridView.AllowUserToAddRows = false;
            this.ItemCodeGridView.AllowUserToDeleteRows = false;
            this.ItemCodeGridView.AllowUserToResizeColumns = false;
            this.ItemCodeGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ItemCodeGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ItemCodeGridView.ApplyStandardBehaviour = false;
            this.ItemCodeGridView.BackgroundColor = System.Drawing.Color.White;
            this.ItemCodeGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ItemCodeGridView.ClearCurrentCellOnLeave = true;
            this.ItemCodeGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.ItemCodeGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ItemCodeGridView.ColumnHeadersHeight = 24;
            this.ItemCodeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ItemCodeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemCodeID,
            this.ItemCode,
            this.Abstract,
            this.Description});
            this.ItemCodeGridView.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemCodeGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.ItemCodeGridView.DefaultRowIndex = 0;
            this.ItemCodeGridView.DeselectCurrentCell = false;
            this.ItemCodeGridView.DeselectSpecifiedRow = -1;
            this.ItemCodeGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ItemCodeGridView.EnableBinding = true;
            this.ItemCodeGridView.EnableHeadersVisualStyles = false;
            this.ItemCodeGridView.GridColor = System.Drawing.Color.Black;
            this.ItemCodeGridView.GridContentSelected = false;
            this.ItemCodeGridView.IsEditableGrid = true;
            this.ItemCodeGridView.IsMultiSelect = true;
            this.ItemCodeGridView.IsSorted = false;
            this.ItemCodeGridView.Location = new System.Drawing.Point(-2, -2);
            this.ItemCodeGridView.Name = "ItemCodeGridView";
            this.ItemCodeGridView.NumRowsVisible = 7;
            this.ItemCodeGridView.PrimaryKeyColumnName = "";
            this.ItemCodeGridView.RemainSortFields = false;
            this.ItemCodeGridView.RemoveDefaultSelection = true;
            this.ItemCodeGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemCodeGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.ItemCodeGridView.RowHeadersWidth = 20;
            this.ItemCodeGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.ItemCodeGridView.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.ItemCodeGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ItemCodeGridView.Size = new System.Drawing.Size(770, 178);
            this.ItemCodeGridView.TabIndex = 4;
            this.ItemCodeGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemCodeGridView_RowEnter);
            this.ItemCodeGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ItemCodeGridView_MouseUp);
            this.ItemCodeGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemCodeGridView_CellEndEdit);
            this.ItemCodeGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.ItemCodeGridView_EditingControlShowing);
            this.ItemCodeGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemCodeGridView_KeyDown);
            this.ItemCodeGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ItemCodeGridView_RowHeaderMouseClick);
            // 
            // ItemCodeID
            // 
            this.ItemCodeID.HeaderText = "ItemCodeID";
            this.ItemCodeID.Name = "ItemCodeID";
            this.ItemCodeID.Visible = false;
            this.ItemCodeID.Width = 40;
            // 
            // ItemCode
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.ItemCode.DefaultCellStyle = dataGridViewCellStyle3;
            this.ItemCode.HeaderText = "ItemCode";
            this.ItemCode.MaxInputLength = 50;
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ItemCode.Width = 180;
            // 
            // Abstract
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.Abstract.DefaultCellStyle = dataGridViewCellStyle4;
            this.Abstract.HeaderText = "Abstract";
            this.Abstract.MaxInputLength = 15;
            this.Abstract.Name = "Abstract";
            this.Abstract.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Abstract.Width = 160;
            // 
            // Description
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.NullValue = null;
            this.Description.DefaultCellStyle = dataGridViewCellStyle5;
            this.Description.HeaderText = "Description";
            this.Description.MaxInputLength = 50;
            this.Description.Name = "Description";
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Description.Width = 393;
            // 
            // ItemCodePanel
            // 
            this.ItemCodePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ItemCodePanel.Controls.Add(this.ItemCodeScroll);
            this.ItemCodePanel.Controls.Add(this.FooterPanel);
            this.ItemCodePanel.Controls.Add(this.ItemCodeGridView);
            this.ItemCodePanel.Location = new System.Drawing.Point(0, 0);
            this.ItemCodePanel.Name = "ItemCodePanel";
            this.ItemCodePanel.Size = new System.Drawing.Size(770, 198);
            this.ItemCodePanel.TabIndex = 5;
            // 
            // ItemCodeScroll
            // 
            this.ItemCodeScroll.Enabled = false;
            this.ItemCodeScroll.Location = new System.Drawing.Point(752, 0);
            this.ItemCodeScroll.Name = "ItemCodeScroll";
            this.ItemCodeScroll.Size = new System.Drawing.Size(16, 175);
            this.ItemCodeScroll.TabIndex = 204;
            // 
            // FooterPanel
            // 
            this.FooterPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FooterPanel.BackColor = System.Drawing.Color.Gray;
            this.FooterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FooterPanel.Location = new System.Drawing.Point(-1, 175);
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(771, 24);
            this.FooterPanel.TabIndex = 122;
            // 
            // ItemCodePictureBox
            // 
            this.ItemCodePictureBox.Location = new System.Drawing.Point(762, -1);
            this.ItemCodePictureBox.Name = "ItemCodePictureBox";
            this.ItemCodePictureBox.Size = new System.Drawing.Size(42, 199);
            this.ItemCodePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ItemCodePictureBox.TabIndex = 121;
            this.ItemCodePictureBox.TabStop = false;
            this.ItemCodePictureBox.Click += new System.EventHandler(this.ItemCodePictureBox_Click);
            this.ItemCodePictureBox.MouseEnter += new System.EventHandler(this.ItemCodePictureBox_MouseEnter);
            // 
            // F35060
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ItemCodePanel);
            this.Controls.Add(this.ItemCodePictureBox);
            this.Name = "F35060";
            this.Size = new System.Drawing.Size(804, 220);
            this.Tag = "35060";
            this.Load += new System.EventHandler(this.F35060_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ItemCodeGridView)).EndInit();
            this.ItemCodePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ItemCodePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanDataGridView ItemCodeGridView;
        private System.Windows.Forms.Panel ItemCodePanel;
        private System.Windows.Forms.PictureBox ItemCodePictureBox;
        private System.Windows.Forms.Panel FooterPanel;
        private System.Windows.Forms.ToolTip ItemCodeToolTip;
        private System.Windows.Forms.VScrollBar ItemCodeScroll;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCodeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Abstract;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}
