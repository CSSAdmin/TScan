namespace D11001
{
    partial class F15104
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PaymentPictureBox = new System.Windows.Forms.PictureBox();
            this.ReceitPaymentPanel = new System.Windows.Forms.Panel();
            this.PaymentEngineVscrollBar = new System.Windows.Forms.VScrollBar();
            this.ReceiptPaymentGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.TenderType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PaidBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaidByImage = new TerraScan.UI.Controls.TerraScanTextAndImageColumn();
            this.CheckNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PPaymentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEditable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Zip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptHeaderToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PaymentPictureBox)).BeginInit();
            this.ReceitPaymentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReceiptPaymentGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // PaymentPictureBox
            // 
            this.PaymentPictureBox.Location = new System.Drawing.Point(761, 0);
            this.PaymentPictureBox.Name = "PaymentPictureBox";
            this.PaymentPictureBox.Size = new System.Drawing.Size(42, 110);
            this.PaymentPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PaymentPictureBox.TabIndex = 118;
            this.PaymentPictureBox.TabStop = false;
            this.PaymentPictureBox.Click += new System.EventHandler(this.PaymentPictureBox_Click);
            this.PaymentPictureBox.MouseEnter += new System.EventHandler(this.PaymentPictureBox_MouseEnter);
            // 
            // ReceitPaymentPanel
            // 
            this.ReceitPaymentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReceitPaymentPanel.Controls.Add(this.PaymentEngineVscrollBar);
            this.ReceitPaymentPanel.Controls.Add(this.ReceiptPaymentGridView);
            this.ReceitPaymentPanel.Location = new System.Drawing.Point(0, 0);
            this.ReceitPaymentPanel.Name = "ReceitPaymentPanel";
            this.ReceitPaymentPanel.Size = new System.Drawing.Size(769, 110);
            this.ReceitPaymentPanel.TabIndex = 120;
            // 
            // PaymentEngineVscrollBar
            // 
            this.PaymentEngineVscrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.PaymentEngineVscrollBar.Enabled = false;
            this.PaymentEngineVscrollBar.Location = new System.Drawing.Point(751, 0);
            this.PaymentEngineVscrollBar.Name = "PaymentEngineVscrollBar";
            this.PaymentEngineVscrollBar.Size = new System.Drawing.Size(17, 109);
            this.PaymentEngineVscrollBar.TabIndex = 124;
            // 
            // ReceiptPaymentGridView
            // 
            this.ReceiptPaymentGridView.AllowCellClick = true;
            this.ReceiptPaymentGridView.AllowDoubleClick = false;
            this.ReceiptPaymentGridView.AllowEmptyRows = true;
            this.ReceiptPaymentGridView.AllowEnterKey = false;
            this.ReceiptPaymentGridView.AllowSorting = true;
            this.ReceiptPaymentGridView.AllowUserToAddRows = false;
            this.ReceiptPaymentGridView.AllowUserToDeleteRows = false;
            this.ReceiptPaymentGridView.AllowUserToResizeColumns = false;
            this.ReceiptPaymentGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.ReceiptPaymentGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.ReceiptPaymentGridView.ApplyStandardBehaviour = false;
            this.ReceiptPaymentGridView.BackgroundColor = System.Drawing.Color.White;
            this.ReceiptPaymentGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReceiptPaymentGridView.ClearCurrentCellOnLeave = true;
            this.ReceiptPaymentGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ReceiptPaymentGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.ReceiptPaymentGridView.ColumnHeadersHeight = 21;
            this.ReceiptPaymentGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ReceiptPaymentGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TenderType,
            this.PaidBy,
            this.PaidByImage,
            this.CheckNumber,
            this.Amount,
            this.PaymentID,
            this.PPaymentID,
            this.IsEditable,
            this.Address1,
            this.Address2,
            this.City,
            this.State,
            this.Zip,
            this.Comment});
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ReceiptPaymentGridView.DefaultCellStyle = dataGridViewCellStyle26;
            this.ReceiptPaymentGridView.DefaultRowIndex = 0;
            this.ReceiptPaymentGridView.DeselectCurrentCell = false;
            this.ReceiptPaymentGridView.DeselectSpecifiedRow = -1;
            this.ReceiptPaymentGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ReceiptPaymentGridView.EnableBinding = true;
            this.ReceiptPaymentGridView.EnableHeadersVisualStyles = false;
            this.ReceiptPaymentGridView.GridColor = System.Drawing.Color.Black;
            this.ReceiptPaymentGridView.GridContentSelected = false;
            this.ReceiptPaymentGridView.IsEditableGrid = false;
            this.ReceiptPaymentGridView.IsMultiSelect = false;
            this.ReceiptPaymentGridView.IsSorted = false;
            this.ReceiptPaymentGridView.Location = new System.Drawing.Point(-1, -1);
            this.ReceiptPaymentGridView.MultiSelect = false;
            this.ReceiptPaymentGridView.Name = "ReceiptPaymentGridView";
            this.ReceiptPaymentGridView.NumRowsVisible = 4;
            this.ReceiptPaymentGridView.PrimaryKeyColumnName = "";
            this.ReceiptPaymentGridView.RemainSortFields = false;
            this.ReceiptPaymentGridView.RemoveDefaultSelection = false;
            this.ReceiptPaymentGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ReceiptPaymentGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.ReceiptPaymentGridView.RowHeadersWidth = 20;
            this.ReceiptPaymentGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ReceiptPaymentGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ReceiptPaymentGridView.Size = new System.Drawing.Size(768, 112);
            this.ReceiptPaymentGridView.TabIndex = 1;
            this.ReceiptPaymentGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ReceiptPaymentGridView_CellValueChanged);
            this.ReceiptPaymentGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ReceiptPaymentGridView_CellMouseClick);
            this.ReceiptPaymentGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.ReceiptPaymentGridView_CellBeginEdit);
            this.ReceiptPaymentGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ReceiptPaymentGridView_CellFormatting);
            this.ReceiptPaymentGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.ReceiptPaymentGridView_EditingControlShowing);
            this.ReceiptPaymentGridView.Paint += new System.Windows.Forms.PaintEventHandler(this.ReceiptPaymentGridView_Paint);
            this.ReceiptPaymentGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.ReceiptPaymentGridView_DataBindingComplete);
            // 
            // TenderType
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TenderType.DefaultCellStyle = dataGridViewCellStyle21;
            this.TenderType.HeaderText = "Type";
            this.TenderType.Name = "TenderType";
            this.TenderType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TenderType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.TenderType.Width = 105;
            // 
            // PaidBy
            // 
            this.PaidBy.HeaderText = "Paid By";
            this.PaidBy.MaxInputLength = 150;
            this.PaidBy.Name = "PaidBy";
            this.PaidBy.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PaidBy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PaidBy.Width = 371;
            // 
            // PaidByImage
            // 
            this.PaidByImage.HeaderText = "";
            this.PaidByImage.Image = global::D11001.Properties.Resources.PeopleButton;
            this.PaidByImage.Name = "PaidByImage";
            this.PaidByImage.ReadOnly = true;
            this.PaidByImage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PaidByImage.Width = 25;
            // 
            // CheckNumber
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CheckNumber.DefaultCellStyle = dataGridViewCellStyle22;
            this.CheckNumber.HeaderText = "#";
            this.CheckNumber.MaxInputLength = 50;
            this.CheckNumber.MinimumWidth = 10;
            this.CheckNumber.Name = "CheckNumber";
            this.CheckNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CheckNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.CheckNumber.Width = 67;
            // 
            // Amount
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Bold);
            this.Amount.DefaultCellStyle = dataGridViewCellStyle23;
            this.Amount.HeaderText = "Amount";
            this.Amount.MinimumWidth = 10;
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // PaymentID
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.PaymentID.DefaultCellStyle = dataGridViewCellStyle24;
            this.PaymentID.HeaderText = "PID";
            this.PaymentID.MinimumWidth = 10;
            this.PaymentID.Name = "PaymentID";
            this.PaymentID.ReadOnly = true;
            this.PaymentID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PaymentID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PaymentID.Width = 64;
            // 
            // PPaymentID
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.PPaymentID.DefaultCellStyle = dataGridViewCellStyle25;
            this.PPaymentID.HeaderText = "PPID";
            this.PPaymentID.MinimumWidth = 10;
            this.PPaymentID.Name = "PPaymentID";
            this.PPaymentID.ReadOnly = true;
            this.PPaymentID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PPaymentID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PPaymentID.Visible = false;
            this.PPaymentID.Width = 75;
            // 
            // IsEditable
            // 
            this.IsEditable.HeaderText = "IsEditable";
            this.IsEditable.Name = "IsEditable";
            this.IsEditable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsEditable.Visible = false;
            // 
            // Address1
            // 
            this.Address1.HeaderText = "Address1";
            this.Address1.Name = "Address1";
            this.Address1.Visible = false;
            // 
            // Address2
            // 
            this.Address2.HeaderText = "Address2";
            this.Address2.Name = "Address2";
            this.Address2.Visible = false;
            // 
            // City
            // 
            this.City.HeaderText = "City";
            this.City.Name = "City";
            this.City.Visible = false;
            // 
            // State
            // 
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.Visible = false;
            // 
            // Zip
            // 
            this.Zip.HeaderText = "Zip";
            this.Zip.Name = "Zip";
            this.Zip.Visible = false;
            // 
            // Comment
            // 
            this.Comment.HeaderText = "Comment";
            this.Comment.Name = "Comment";
            this.Comment.Visible = false;
            // 
            // F15104
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ReceitPaymentPanel);
            this.Controls.Add(this.PaymentPictureBox);
            this.Name = "F15104";
            this.Size = new System.Drawing.Size(804, 112);
            this.Tag = "15104";
            this.Load += new System.EventHandler(this.F15104_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PaymentPictureBox)).EndInit();
            this.ReceitPaymentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReceiptPaymentGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PaymentPictureBox;
        private System.Windows.Forms.Panel ReceitPaymentPanel;
        private TerraScan.UI.Controls.TerraScanDataGridView ReceiptPaymentGridView;
        private System.Windows.Forms.VScrollBar PaymentEngineVscrollBar;
        private System.Windows.Forms.ToolTip ReceiptHeaderToolTip;
        private System.Windows.Forms.DataGridViewComboBoxColumn TenderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaidBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PPaymentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsEditable;
        private TerraScan.UI.Controls.TerraScanTextAndImageColumn PaidByImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address2;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
    }
}

