namespace TerraScan.PaymentEngine
{
    partial class PaymentEngineUserControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentEngineUserControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PaymentItemsGridView = new System.Windows.Forms.DataGridView();
            this.TenderType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PaidBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaidByImage = new TerraScan.UI.Controls.TerraScanTextAndImageColumn();
            this.CheckNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Zip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentEngineVscrollBar = new System.Windows.Forms.VScrollBar();
            this.PaymentPictureBox = new System.Windows.Forms.PictureBox();
            this.PaymentEngineTabButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentItemsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PaymentItemsGridView
            // 
            this.PaymentItemsGridView.AllowUserToAddRows = false;
            this.PaymentItemsGridView.AllowUserToDeleteRows = false;
            this.PaymentItemsGridView.AllowUserToResizeColumns = false;
            this.PaymentItemsGridView.AllowUserToResizeRows = false;
            this.PaymentItemsGridView.BackgroundColor = System.Drawing.Color.White;
            this.PaymentItemsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PaymentItemsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PaymentItemsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PaymentItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.PaymentItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TenderType,
            this.PaidBy,
            this.PaidByImage,
            this.CheckNumber,
            this.Amount,
            this.PID,
            this.PPID,
            this.Address1,
            this.Address2,
            this.City,
            this.State,
            this.Zip,
            this.Comment});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PaymentItemsGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.PaymentItemsGridView.Dock = System.Windows.Forms.DockStyle.Left;
            this.PaymentItemsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.PaymentItemsGridView.EnableHeadersVisualStyles = false;
            this.PaymentItemsGridView.GridColor = System.Drawing.Color.Black;
            this.PaymentItemsGridView.Location = new System.Drawing.Point(0, 0);
            this.PaymentItemsGridView.MultiSelect = false;
            this.PaymentItemsGridView.Name = "PaymentItemsGridView";
            this.PaymentItemsGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PaymentItemsGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.PaymentItemsGridView.RowHeadersWidth = 20;
            this.PaymentItemsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.PaymentItemsGridView.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.PaymentItemsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PaymentItemsGridView.Size = new System.Drawing.Size(797, 89);
            this.PaymentItemsGridView.TabIndex = 16;
            this.PaymentItemsGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.PaymentItemsGridView_CellValueChanged);
            this.PaymentItemsGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.PaymentItemsGridView_CellMouseClick);
            this.PaymentItemsGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.PaymentItemsGridView_CellBeginEdit);
            //this.PaymentItemsGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.PaymentItemsGridView_CellMouseLeave);
            this.PaymentItemsGridView.Enter += new System.EventHandler(this.PaymentItemsGridView_Enter);
            this.PaymentItemsGridView.Leave += new System.EventHandler(this.PaymentItemsGridView_Leave);
            this.PaymentItemsGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.PaymentItemsGridView_CellParsing);
            this.PaymentItemsGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.PaymentItemsGridView_CellFormatting);
            this.PaymentItemsGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.PaymentItemsGridView_CellEndEdit);
            this.PaymentItemsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PaymentItemsGridView_CellClick);
            this.PaymentItemsGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.PaymentItemsGridView_RowHeaderMouseDoubleClick);
            this.PaymentItemsGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.PaymentItemsGridView_EditingControlShowing);
            //this.PaymentItemsGridView.CurrentCellChanged += new System.EventHandler(this.PaymentItemsGridView_CurrentCellChanged);
            this.PaymentItemsGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.PaymentItemsGridView_DataError);
            this.PaymentItemsGridView.Paint += new System.Windows.Forms.PaintEventHandler(this.PaymentItemsGridView_Paint);
            this.PaymentItemsGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PaymentItemsGridView_KeyDown);
            this.PaymentItemsGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.PaymentItemsGridView_CellEnter);
            this.PaymentItemsGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.PaymentItemsGridView_DataBindingComplete);
            this.PaymentItemsGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.PaymentItemsGridView_RowHeaderMouseClick);
            // 
            // TenderType
            // 
            this.TenderType.DataPropertyName = "TenderType";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.TenderType.DefaultCellStyle = dataGridViewCellStyle2;
            this.TenderType.DisplayStyleForCurrentCellOnly = true;
            this.TenderType.HeaderText = "Type";
            this.TenderType.Name = "TenderType";
            this.TenderType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TenderType.Width = 105;
            // 
            // PaidBy
            // 
            this.PaidBy.DataPropertyName = "PaidBy";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.PaidBy.DefaultCellStyle = dataGridViewCellStyle3;
            this.PaidBy.HeaderText = "Paid By";
            this.PaidBy.MaxInputLength = 150;
            this.PaidBy.Name = "PaidBy";
            this.PaidBy.ReadOnly = true;
            this.PaidBy.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PaidBy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PaidBy.Width = 299;
            // 
            // PaidByImage
            // 
            this.PaidByImage.DataPropertyName = "PaidByImage";
            this.PaidByImage.HeaderText = "";
            this.PaidByImage.Image = ((System.Drawing.Image)(resources.GetObject("PaidByImage.Image")));
            this.PaidByImage.Name = "PaidByImage";
            this.PaidByImage.ReadOnly = true;
            this.PaidByImage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PaidByImage.Width = 25;
            // 
            // CheckNumber
            // 
            this.CheckNumber.DataPropertyName = "CheckNumber";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.CheckNumber.DefaultCellStyle = dataGridViewCellStyle4;
            this.CheckNumber.HeaderText = "#";
            this.CheckNumber.MaxInputLength = 50;
            this.CheckNumber.Name = "CheckNumber";
            this.CheckNumber.ReadOnly = true;
            this.CheckNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CheckNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CheckNumber.Width = 67;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Bold);
            this.Amount.DefaultCellStyle = dataGridViewCellStyle5;
            this.Amount.HeaderText = "Amount";
            this.Amount.MaxInputLength = 17;
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PID
            // 
            this.PID.DataPropertyName = "PID";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Silver;
            this.PID.DefaultCellStyle = dataGridViewCellStyle6;
            this.PID.HeaderText = "PID";
            this.PID.Name = "PID";
            this.PID.ReadOnly = true;
            this.PID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PID.Width = 65;
            // 
            // PPID
            // 
            this.PPID.DataPropertyName = "PPID";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Silver;
            this.PPID.DefaultCellStyle = dataGridViewCellStyle7;
            this.PPID.HeaderText = "PPID";
            this.PPID.Name = "PPID";
            this.PPID.ReadOnly = true;
            this.PPID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PPID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PPID.Width = 75;
            // 
            // Address1
            // 
            this.Address1.DataPropertyName = "Address1";
            this.Address1.HeaderText = "Address1";
            this.Address1.MaxInputLength = 50;
            this.Address1.Name = "Address1";
            this.Address1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Address1.Visible = false;
            // 
            // Address2
            // 
            this.Address2.DataPropertyName = "Address2";
            this.Address2.HeaderText = "Address2";
            this.Address2.MaxInputLength = 50;
            this.Address2.Name = "Address2";
            this.Address2.Visible = false;
            // 
            // City
            // 
            this.City.DataPropertyName = "City";
            this.City.HeaderText = "City";
            this.City.MaxInputLength = 50;
            this.City.Name = "City";
            this.City.Visible = false;
            // 
            // State
            // 
            this.State.DataPropertyName = "State";
            this.State.HeaderText = "State";
            this.State.MaxInputLength = 50;
            this.State.Name = "State";
            this.State.Visible = false;
            // 
            // Zip
            // 
            this.Zip.DataPropertyName = "Zip";
            this.Zip.HeaderText = "Zip";
            this.Zip.MaxInputLength = 50;
            this.Zip.Name = "Zip";
            this.Zip.Visible = false;
            // 
            // Comment
            // 
            this.Comment.DataPropertyName = "Comment";
            this.Comment.HeaderText = "Comment";
            this.Comment.MaxInputLength = 200;
            this.Comment.Name = "Comment";
            this.Comment.Visible = false;
            // 
            // PaymentEngineVscrollBar
            // 
            this.PaymentEngineVscrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.PaymentEngineVscrollBar.Enabled = false;
            this.PaymentEngineVscrollBar.Location = new System.Drawing.Point(757, 0);
            this.PaymentEngineVscrollBar.Name = "PaymentEngineVscrollBar";
            this.PaymentEngineVscrollBar.Size = new System.Drawing.Size(16, 89);
            this.PaymentEngineVscrollBar.TabIndex = 18;
            // 
            // PaymentPictureBox
            // 
            this.PaymentPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("PaymentPictureBox.Image")));
            this.PaymentPictureBox.Location = new System.Drawing.Point(773, 0);
            this.PaymentPictureBox.Name = "PaymentPictureBox";
            this.PaymentPictureBox.Size = new System.Drawing.Size(24, 90);
            this.PaymentPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PaymentPictureBox.TabIndex = 19;
            this.PaymentPictureBox.TabStop = false;
            // 
            // PaymentEngineTabButton
            // 
            this.PaymentEngineTabButton.Location = new System.Drawing.Point(779, 39);
            this.PaymentEngineTabButton.Name = "PaymentEngineTabButton";
            this.PaymentEngineTabButton.Size = new System.Drawing.Size(10, 10);
            this.PaymentEngineTabButton.TabIndex = 20;
            this.PaymentEngineTabButton.TabStop = false;
            this.PaymentEngineTabButton.Text = "button1";
            this.PaymentEngineTabButton.UseVisualStyleBackColor = true;
            this.PaymentEngineTabButton.Enter += new System.EventHandler(this.PaymentEngineTabButton_Enter);
            // 
            // PaymentEngineUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PaymentPictureBox);
            this.Controls.Add(this.PaymentEngineVscrollBar);
            this.Controls.Add(this.PaymentItemsGridView);
            this.Controls.Add(this.PaymentEngineTabButton);
            this.Name = "PaymentEngineUserControl";
            this.Size = new System.Drawing.Size(797, 89);
            this.Load += new System.EventHandler(this.PaymentEngineUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PaymentItemsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView PaymentItemsGridView;
        private System.Windows.Forms.VScrollBar PaymentEngineVscrollBar;
        private System.Windows.Forms.PictureBox PaymentPictureBox;
        private System.Windows.Forms.Button PaymentEngineTabButton;
        private System.Windows.Forms.DataGridViewComboBoxColumn TenderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaidBy;
        private TerraScan.UI.Controls.TerraScanTextAndImageColumn PaidByImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address2;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
    }
}
