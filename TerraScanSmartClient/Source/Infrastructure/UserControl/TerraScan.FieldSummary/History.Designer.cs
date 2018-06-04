namespace TerraScan.FieldSummary
{
    partial class History
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridViewPanel = new System.Windows.Forms.Panel();
            this.verticalScrollBar = new System.Windows.Forms.VScrollBar();
            this.HistoryGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.parNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rollyear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.posttype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.origval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.origtax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statementid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balanceD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Headerpanel = new System.Windows.Forms.Panel();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.Historypanel = new System.Windows.Forms.Panel();
            this.GridViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryGridView)).BeginInit();
            this.Headerpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridViewPanel
            // 
            this.GridViewPanel.Controls.Add(this.verticalScrollBar);
            this.GridViewPanel.Controls.Add(this.HistoryGridView);
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
            // HistoryGridView
            // 
            this.HistoryGridView.AllowCellClick = false;
            this.HistoryGridView.AllowDoubleClick = false;
            this.HistoryGridView.AllowEmptyRows = false;
            this.HistoryGridView.AllowEnterKey = false;
            this.HistoryGridView.AllowSorting = false;
            this.HistoryGridView.AllowUserToAddRows = false;
            this.HistoryGridView.AllowUserToDeleteRows = false;
            this.HistoryGridView.AllowUserToResizeColumns = false;
            this.HistoryGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.HistoryGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.HistoryGridView.ApplyStandardBehaviour = false;
            this.HistoryGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.HistoryGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HistoryGridView.ClearCurrentCellOnLeave = true;
            this.HistoryGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.HistoryGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HistoryGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.HistoryGridView.ColumnHeadersHeight = 24;
            this.HistoryGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.HistoryGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.parNum,
            this.rollyear,
            this.posttype,
            this.origval,
            this.origtax,
            this.statementid,
            this.balanceD});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.HistoryGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.HistoryGridView.DefaultRowIndex = 0;
            this.HistoryGridView.DeselectCurrentCell = false;
            this.HistoryGridView.DeselectSpecifiedRow = -1;
            this.HistoryGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.HistoryGridView.EnableBinding = false;
            this.HistoryGridView.EnableHeadersVisualStyles = false;
            this.HistoryGridView.GridColor = System.Drawing.Color.Black;
            this.HistoryGridView.GridContentSelected = false;
            this.HistoryGridView.IsEditableGrid = false;
            this.HistoryGridView.IsMultiSelect = false;
            this.HistoryGridView.IsSorted = false;
            this.HistoryGridView.Location = new System.Drawing.Point(-1, 0);
            this.HistoryGridView.MultiSelect = false;
            this.HistoryGridView.Name = "HistoryGridView";
            this.HistoryGridView.NumRowsVisible = 8;
            this.HistoryGridView.PrimaryKeyColumnName = "";
            this.HistoryGridView.RemainSortFields = false;
            this.HistoryGridView.RemoveDefaultSelection = true;
            this.HistoryGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HistoryGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.HistoryGridView.RowHeadersVisible = false;
            this.HistoryGridView.RowHeadersWidth = 20;
            this.HistoryGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.HistoryGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HistoryGridView.Size = new System.Drawing.Size(761, 200);
            this.HistoryGridView.StandardTab = true;
            this.HistoryGridView.TabIndex = 3;
            this.HistoryGridView.TabStop = false;
            this.HistoryGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.HistoryGridView_CellFormatting);
            // 
            // parNum
            // 
            this.parNum.HeaderText = "Related Statements";
            this.parNum.Name = "parNum";
            this.parNum.Width = 182;
            // 
            // rollyear
            // 
            this.rollyear.HeaderText = "rollyear";
            this.rollyear.Name = "rollyear";
            this.rollyear.Visible = false;
            this.rollyear.Width = 140;
            // 
            // posttype
            // 
            this.posttype.HeaderText = "Post Type";
            this.posttype.Name = "posttype";
            this.posttype.Width = 140;
            // 
            // origval
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "##,##0";
            dataGridViewCellStyle3.NullValue = null;
            this.origval.DefaultCellStyle = dataGridViewCellStyle3;
            this.origval.HeaderText = "Assessed Value";
            this.origval.Name = "origval";
            this.origval.Width = 140;
            // 
            // origtax
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "##,##0.00";
            dataGridViewCellStyle4.NullValue = null;
            this.origtax.DefaultCellStyle = dataGridViewCellStyle4;
            this.origtax.HeaderText = "Tax Amount";
            this.origtax.Name = "origtax";
            this.origtax.Width = 140;
            // 
            // statementid
            // 
            this.statementid.HeaderText = "statementid";
            this.statementid.Name = "statementid";
            this.statementid.Visible = false;
            this.statementid.Width = 140;
            // 
            // balanceD
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "##,##0.00";
            dataGridViewCellStyle5.NullValue = null;
            this.balanceD.DefaultCellStyle = dataGridViewCellStyle5;
            this.balanceD.HeaderText = "Balance Due";
            this.balanceD.Name = "balanceD";
            this.balanceD.Width = 140;
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
            this.HeaderLabel.Size = new System.Drawing.Size(110, 19);
            this.HeaderLabel.TabIndex = 25;
            this.HeaderLabel.Text = "Value History";
            this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Historypanel
            // 
            this.Historypanel.Location = new System.Drawing.Point(0, 0);
            this.Historypanel.Name = "Historypanel";
            this.Historypanel.Size = new System.Drawing.Size(200, 100);
            this.Historypanel.TabIndex = 0;
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.Headerpanel);
            this.Controls.Add(this.GridViewPanel);
            this.Name = "History";
            this.Size = new System.Drawing.Size(761, 235);
            this.Load += new System.EventHandler(this.History_Load);
            this.GridViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HistoryGridView)).EndInit();
            this.Headerpanel.ResumeLayout(false);
            this.Headerpanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Headerpanel;
        private TerraScan.UI.Controls.TerraScanDataGridView HistoryGridView;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.Panel Historypanel;
        private System.Windows.Forms.VScrollBar verticalScrollBar;
        private System.Windows.Forms.Panel GridViewPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn parNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn rollyear;
        private System.Windows.Forms.DataGridViewTextBoxColumn posttype;
        private System.Windows.Forms.DataGridViewTextBoxColumn origval;
        private System.Windows.Forms.DataGridViewTextBoxColumn origtax;
        private System.Windows.Forms.DataGridViewTextBoxColumn statementid;
        private System.Windows.Forms.DataGridViewTextBoxColumn balanceD;
        


    }
}
