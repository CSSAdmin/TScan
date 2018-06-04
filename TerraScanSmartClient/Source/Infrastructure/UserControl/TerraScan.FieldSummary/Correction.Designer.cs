namespace TerraScan.FieldSummary
{
    partial class Correction
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
            this.GridViewPanel = new System.Windows.Forms.Panel();
            this.verticalScrollBar = new System.Windows.Forms.VScrollBar();
            this.CorrectionGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prvval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.origtax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newtax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.change = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Headerpanel = new System.Windows.Forms.Panel();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.GridViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CorrectionGridView)).BeginInit();
            this.Headerpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridViewPanel
            // 
            this.GridViewPanel.Controls.Add(this.verticalScrollBar);
            this.GridViewPanel.Controls.Add(this.CorrectionGridView);
            this.GridViewPanel.Location = new System.Drawing.Point(0, 34);
            this.GridViewPanel.Name = "GridViewPanel";
            this.GridViewPanel.Size = new System.Drawing.Size(761, 200);
            this.GridViewPanel.TabIndex = 4;
            // 
            // verticalScrollBar
            // 
            this.verticalScrollBar.Enabled = false;
            this.verticalScrollBar.Location = new System.Drawing.Point(743, 0);
            this.verticalScrollBar.Name = "verticalScrollBar";
            this.verticalScrollBar.Size = new System.Drawing.Size(16, 200);
            this.verticalScrollBar.TabIndex = 6;
            // 
            // CorrectionGridView
            // 
            this.CorrectionGridView.AllowCellClick = false;
            this.CorrectionGridView.AllowDoubleClick = false;
            this.CorrectionGridView.AllowEmptyRows = true;
            this.CorrectionGridView.AllowEnterKey = false;
            this.CorrectionGridView.AllowSorting = false;
            this.CorrectionGridView.AllowUserToAddRows = false;
            this.CorrectionGridView.AllowUserToDeleteRows = false;
            this.CorrectionGridView.AllowUserToResizeColumns = false;
            this.CorrectionGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.CorrectionGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.CorrectionGridView.ApplyStandardBehaviour = false;
            this.CorrectionGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.CorrectionGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CorrectionGridView.ClearCurrentCellOnLeave = false;
            this.CorrectionGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.CorrectionGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CorrectionGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CorrectionGridView.ColumnHeadersHeight = 24;
            this.CorrectionGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.CorrectionGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Type,
            this.prvval,
            this.newval,
            this.origtax,
            this.newtax,
            this.change,
            this.Note});
            this.CorrectionGridView.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CorrectionGridView.DefaultCellStyle = dataGridViewCellStyle10;
            this.CorrectionGridView.DefaultRowIndex = 0;
            this.CorrectionGridView.DeselectCurrentCell = false;
            this.CorrectionGridView.DeselectSpecifiedRow = -1;
            this.CorrectionGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.CorrectionGridView.EnableBinding = false;
            this.CorrectionGridView.EnableHeadersVisualStyles = false;
            this.CorrectionGridView.GridColor = System.Drawing.Color.Black;
            this.CorrectionGridView.GridContentSelected = false;
            this.CorrectionGridView.IsEditableGrid = false;
            this.CorrectionGridView.IsMultiSelect = false;
            this.CorrectionGridView.IsSorted = false;
            this.CorrectionGridView.Location = new System.Drawing.Point(-1, 0);
            this.CorrectionGridView.MultiSelect = false;
            this.CorrectionGridView.Name = "CorrectionGridView";
            this.CorrectionGridView.NumRowsVisible = 8;
            this.CorrectionGridView.PrimaryKeyColumnName = "";
            this.CorrectionGridView.RemainSortFields = false;
            this.CorrectionGridView.RemoveDefaultSelection = true;
            this.CorrectionGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CorrectionGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.CorrectionGridView.RowHeadersVisible = false;
            this.CorrectionGridView.RowHeadersWidth = 20;
            this.CorrectionGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.CorrectionGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CorrectionGridView.Size = new System.Drawing.Size(761, 200);
            this.CorrectionGridView.StandardTab = true;
            this.CorrectionGridView.TabIndex = 5;
            this.CorrectionGridView.TabStop = false;
            // 
            // Date
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.Date.DefaultCellStyle = dataGridViewCellStyle3;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // Type
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Type.DefaultCellStyle = dataGridViewCellStyle4;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.Width = 146;
            // 
            // prvval
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "##,##0";
            dataGridViewCellStyle5.NullValue = null;
            this.prvval.DefaultCellStyle = dataGridViewCellStyle5;
            this.prvval.HeaderText = "Prior Value";
            this.prvval.Name = "prvval";
            // 
            // newval
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "##,##0";
            this.newval.DefaultCellStyle = dataGridViewCellStyle6;
            this.newval.HeaderText = "New Value";
            this.newval.Name = "newval";
            this.newval.Width = 99;
            // 
            // origtax
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "##,##0.00";
            this.origtax.DefaultCellStyle = dataGridViewCellStyle7;
            this.origtax.HeaderText = "Prior Tax";
            this.origtax.Name = "origtax";
            this.origtax.Width = 99;
            // 
            // newtax
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "##,##0.00";
            this.newtax.DefaultCellStyle = dataGridViewCellStyle8;
            this.newtax.HeaderText = "New Tax";
            this.newtax.Name = "newtax";
            this.newtax.Width = 99;
            // 
            // change
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            this.change.DefaultCellStyle = dataGridViewCellStyle9;
            this.change.HeaderText = "Change";
            this.change.Name = "change";
            this.change.Width = 99;
            // 
            // Note
            // 
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            this.Note.Visible = false;
            // 
            // Headerpanel
            // 
            this.Headerpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(36)))), ((int)(((byte)(60)))));
            this.Headerpanel.Controls.Add(this.HeaderLabel);
            this.Headerpanel.Location = new System.Drawing.Point(0, 0);
            this.Headerpanel.Name = "Headerpanel";
            this.Headerpanel.Size = new System.Drawing.Size(761, 35);
            this.Headerpanel.TabIndex = 3;
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.HeaderLabel.ForeColor = System.Drawing.Color.White;
            this.HeaderLabel.Location = new System.Drawing.Point(0, 4);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.HeaderLabel.Size = new System.Drawing.Size(149, 19);
            this.HeaderLabel.TabIndex = 24;
            this.HeaderLabel.Text = "Correction History";
            this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Correction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridViewPanel);
            this.Controls.Add(this.Headerpanel);
            this.Name = "Correction";
            this.Size = new System.Drawing.Size(761, 235);
            this.Load += new System.EventHandler(this.Correction_Load);
            this.GridViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CorrectionGridView)).EndInit();
            this.Headerpanel.ResumeLayout(false);
            this.Headerpanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Headerpanel;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.VScrollBar verticalScrollBar;
        private TerraScan.UI.Controls.TerraScanDataGridView CorrectionGridView;
        private System.Windows.Forms.Panel GridViewPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn prvval;
        private System.Windows.Forms.DataGridViewTextBoxColumn newval;
        private System.Windows.Forms.DataGridViewTextBoxColumn origtax;
        private System.Windows.Forms.DataGridViewTextBoxColumn newtax;
        private System.Windows.Forms.DataGridViewTextBoxColumn change;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;


    }
}
