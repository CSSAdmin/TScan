namespace D11001
{
    partial class F15101
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F15101));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.EventPictureBox = new System.Windows.Forms.PictureBox();
            this.ReceiptItemsGridViewPanel = new System.Windows.Forms.Panel();
            this.FooterPanel = new System.Windows.Forms.Panel();
            this.CurrentTotalValuePanel = new System.Windows.Forms.Panel();
            this.CurrentTotalValueLabel = new System.Windows.Forms.Label();
            this.CurrentTotalPanel = new System.Windows.Forms.Panel();
            this.CurrentTotalLabel = new System.Windows.Forms.Label();
            this.TotalAmountValuePanel = new System.Windows.Forms.Panel();
            this.TotalAmountValueLabel = new System.Windows.Forms.Label();
            this.TotalAmountLabelPanel = new System.Windows.Forms.Panel();
            this.TotalAmountLabel = new System.Windows.Forms.Label();
            this.ReceiptItemsGridVscrollBar = new System.Windows.Forms.VScrollBar();
            this.ReceiptItemsGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.TransactionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEditable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEdited = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptItemsPictureBox = new System.Windows.Forms.PictureBox();
            this.ReceiptItemsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.TotalsTextBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.EventPictureBox)).BeginInit();
            this.ReceiptItemsGridViewPanel.SuspendLayout();
            this.FooterPanel.SuspendLayout();
            this.CurrentTotalValuePanel.SuspendLayout();
            this.CurrentTotalPanel.SuspendLayout();
            this.TotalAmountValuePanel.SuspendLayout();
            this.TotalAmountLabelPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReceiptItemsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceiptItemsPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // EventPictureBox
            // 
            this.EventPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("EventPictureBox.Image")));
            this.EventPictureBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.EventPictureBox.Location = new System.Drawing.Point(807, 0);
            this.EventPictureBox.Name = "EventPictureBox";
            this.EventPictureBox.Size = new System.Drawing.Size(42, 181);
            this.EventPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.EventPictureBox.TabIndex = 11;
            this.EventPictureBox.TabStop = false;
            // 
            // ReceiptItemsGridViewPanel
            // 
            this.ReceiptItemsGridViewPanel.BackColor = System.Drawing.Color.Transparent;
            this.ReceiptItemsGridViewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReceiptItemsGridViewPanel.Controls.Add(this.FooterPanel);
            this.ReceiptItemsGridViewPanel.Controls.Add(this.ReceiptItemsGridVscrollBar);
            this.ReceiptItemsGridViewPanel.Controls.Add(this.ReceiptItemsGridView);
            this.ReceiptItemsGridViewPanel.Location = new System.Drawing.Point(0, 0);
            this.ReceiptItemsGridViewPanel.Name = "ReceiptItemsGridViewPanel";
            this.ReceiptItemsGridViewPanel.Size = new System.Drawing.Size(769, 114);
            this.ReceiptItemsGridViewPanel.TabIndex = 12;
            // 
            // FooterPanel
            // 
            this.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FooterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FooterPanel.Controls.Add(this.CurrentTotalValuePanel);
            this.FooterPanel.Controls.Add(this.CurrentTotalPanel);
            this.FooterPanel.Controls.Add(this.TotalAmountValuePanel);
            this.FooterPanel.Controls.Add(this.TotalAmountLabelPanel);
            this.FooterPanel.Location = new System.Drawing.Point(-1, 85);
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(769, 28);
            this.FooterPanel.TabIndex = 1006;
            // 
            // CurrentTotalValuePanel
            // 
            this.CurrentTotalValuePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CurrentTotalValuePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentTotalValuePanel.Controls.Add(this.CurrentTotalValueLabel);
            this.CurrentTotalValuePanel.Location = new System.Drawing.Point(366, -1);
            this.CurrentTotalValuePanel.Name = "CurrentTotalValuePanel";
            this.CurrentTotalValuePanel.Size = new System.Drawing.Size(165, 29);
            this.CurrentTotalValuePanel.TabIndex = 23;
            // 
            // CurrentTotalValueLabel
            // 
            this.CurrentTotalValueLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CurrentTotalValueLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentTotalValueLabel.ForeColor = System.Drawing.Color.White;
            this.CurrentTotalValueLabel.Location = new System.Drawing.Point(8, 5);
            this.CurrentTotalValueLabel.Name = "CurrentTotalValueLabel";
            this.CurrentTotalValueLabel.Size = new System.Drawing.Size(150, 16);
            this.CurrentTotalValueLabel.TabIndex = 2;
            this.CurrentTotalValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CurrentTotalValueLabel.MouseHover += new System.EventHandler(this.DisplayLabelToolTip_MouseHover);
            // 
            // CurrentTotalPanel
            // 
            this.CurrentTotalPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CurrentTotalPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentTotalPanel.Controls.Add(this.CurrentTotalLabel);
            this.CurrentTotalPanel.Location = new System.Drawing.Point(285, -2);
            this.CurrentTotalPanel.Name = "CurrentTotalPanel";
            this.CurrentTotalPanel.Size = new System.Drawing.Size(82, 29);
            this.CurrentTotalPanel.TabIndex = 22;
            // 
            // CurrentTotalLabel
            // 
            this.CurrentTotalLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CurrentTotalLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentTotalLabel.ForeColor = System.Drawing.Color.White;
            this.CurrentTotalLabel.Location = new System.Drawing.Point(17, 5);
            this.CurrentTotalLabel.Name = "CurrentTotalLabel";
            this.CurrentTotalLabel.Size = new System.Drawing.Size(60, 16);
            this.CurrentTotalLabel.TabIndex = 2;
            this.CurrentTotalLabel.Text = "Current:";
            this.CurrentTotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TotalAmountValuePanel
            // 
            this.TotalAmountValuePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TotalAmountValuePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalAmountValuePanel.Controls.Add(this.TotalAmountValueLabel);
            this.TotalAmountValuePanel.Location = new System.Drawing.Point(615, -1);
            this.TotalAmountValuePanel.Name = "TotalAmountValuePanel";
            this.TotalAmountValuePanel.Size = new System.Drawing.Size(136, 29);
            this.TotalAmountValuePanel.TabIndex = 21;
            // 
            // TotalAmountValueLabel
            // 
            this.TotalAmountValueLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TotalAmountValueLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalAmountValueLabel.ForeColor = System.Drawing.Color.White;
            this.TotalAmountValueLabel.Location = new System.Drawing.Point(8, 5);
            this.TotalAmountValueLabel.Name = "TotalAmountValueLabel";
            this.TotalAmountValueLabel.Size = new System.Drawing.Size(124, 16);
            this.TotalAmountValueLabel.TabIndex = 2;
            this.TotalAmountValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TotalAmountValueLabel.MouseHover += new System.EventHandler(this.DisplayLabelToolTip_MouseHover);
            // 
            // TotalAmountLabelPanel
            // 
            this.TotalAmountLabelPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TotalAmountLabelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalAmountLabelPanel.Controls.Add(this.TotalAmountLabel);
            this.TotalAmountLabelPanel.Location = new System.Drawing.Point(530, -2);
            this.TotalAmountLabelPanel.Name = "TotalAmountLabelPanel";
            this.TotalAmountLabelPanel.Size = new System.Drawing.Size(86, 29);
            this.TotalAmountLabelPanel.TabIndex = 20;
            // 
            // TotalAmountLabel
            // 
            this.TotalAmountLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TotalAmountLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalAmountLabel.ForeColor = System.Drawing.Color.White;
            this.TotalAmountLabel.Location = new System.Drawing.Point(37, 5);
            this.TotalAmountLabel.Name = "TotalAmountLabel";
            this.TotalAmountLabel.Size = new System.Drawing.Size(45, 16);
            this.TotalAmountLabel.TabIndex = 2;
            this.TotalAmountLabel.Text = "Total:";
            this.TotalAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ReceiptItemsGridVscrollBar
            // 
            this.ReceiptItemsGridVscrollBar.Enabled = false;
            this.ReceiptItemsGridVscrollBar.Location = new System.Drawing.Point(751, 1);
            this.ReceiptItemsGridVscrollBar.Name = "ReceiptItemsGridVscrollBar";
            this.ReceiptItemsGridVscrollBar.Size = new System.Drawing.Size(18, 84);
            this.ReceiptItemsGridVscrollBar.TabIndex = 1005;
            // 
            // ReceiptItemsGridView
            // 
            this.ReceiptItemsGridView.AllowCellClick = true;
            this.ReceiptItemsGridView.AllowDoubleClick = false;
            this.ReceiptItemsGridView.AllowEmptyRows = true;
            this.ReceiptItemsGridView.AllowEnterKey = true;
            this.ReceiptItemsGridView.AllowSorting = true;
            this.ReceiptItemsGridView.AllowUserToAddRows = false;
            this.ReceiptItemsGridView.AllowUserToDeleteRows = false;
            this.ReceiptItemsGridView.AllowUserToResizeColumns = false;
            this.ReceiptItemsGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.ReceiptItemsGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ReceiptItemsGridView.ApplyStandardBehaviour = false;
            this.ReceiptItemsGridView.BackgroundColor = System.Drawing.Color.White;
            this.ReceiptItemsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReceiptItemsGridView.ClearCurrentCellOnLeave = true;
            this.ReceiptItemsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ReceiptItemsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ReceiptItemsGridView.ColumnHeadersHeight = 21;
            this.ReceiptItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ReceiptItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TransactionID,
            this.Description,
            this.AccountName,
            this.ItemType,
            this.Amount,
            this.MaxValue,
            this.IsEditable,
            this.IsEdited,
            this.MinValue});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ReceiptItemsGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.ReceiptItemsGridView.DefaultRowIndex = 0;
            this.ReceiptItemsGridView.DeselectCurrentCell = false;
            this.ReceiptItemsGridView.DeselectSpecifiedRow = -1;
            this.ReceiptItemsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ReceiptItemsGridView.EnableBinding = true;
            this.ReceiptItemsGridView.EnableHeadersVisualStyles = false;
            this.ReceiptItemsGridView.GridColor = System.Drawing.Color.Black;
            this.ReceiptItemsGridView.GridContentSelected = false;
            this.ReceiptItemsGridView.IsEditableGrid = true;
            this.ReceiptItemsGridView.IsMultiSelect = false;
            this.ReceiptItemsGridView.IsSorted = false;
            this.ReceiptItemsGridView.Location = new System.Drawing.Point(-1, -1);
            this.ReceiptItemsGridView.MultiSelect = false;
            this.ReceiptItemsGridView.Name = "ReceiptItemsGridView";
            this.ReceiptItemsGridView.NumRowsVisible = 3;
            this.ReceiptItemsGridView.PrimaryKeyColumnName = "TransactionID";
            this.ReceiptItemsGridView.RemainSortFields = false;
            this.ReceiptItemsGridView.RemoveDefaultSelection = false;
            this.ReceiptItemsGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ReceiptItemsGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.ReceiptItemsGridView.RowHeadersWidth = 20;
            this.ReceiptItemsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ReceiptItemsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ReceiptItemsGridView.Size = new System.Drawing.Size(769, 85);
            this.ReceiptItemsGridView.TabIndex = 11;
            this.ReceiptItemsGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ReceiptItemsGridView_CellValueChanged);
            this.ReceiptItemsGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.ReceiptItemsGridView_CellBeginEdit);
            this.ReceiptItemsGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ReceiptItemsGridView_RowEnter);
            this.ReceiptItemsGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.ReceiptItemsGridView_CellParsing);
            this.ReceiptItemsGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ReceiptItemsGridView_CellFormatting);
            this.ReceiptItemsGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.ReceiptItemsGridView_EditingControlShowing);
            // 
            // TransactionID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.TransactionID.DefaultCellStyle = dataGridViewCellStyle3;
            this.TransactionID.HeaderText = "ID";
            this.TransactionID.Name = "TransactionID";
            this.TransactionID.ReadOnly = true;
            this.TransactionID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TransactionID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.TransactionID.Visible = false;
            this.TransactionID.Width = 75;
            // 
            // Description
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.Description.DefaultCellStyle = dataGridViewCellStyle4;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Description.Width = 267;
            // 
            // AccountName
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.AccountName.DefaultCellStyle = dataGridViewCellStyle5;
            this.AccountName.HeaderText = "Account";
            this.AccountName.Name = "AccountName";
            this.AccountName.ReadOnly = true;
            this.AccountName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AccountName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.AccountName.Width = 245;
            // 
            // ItemType
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.ItemType.DefaultCellStyle = dataGridViewCellStyle6;
            this.ItemType.HeaderText = "Item Type";
            this.ItemType.MaxInputLength = 3;
            this.ItemType.Name = "ItemType";
            this.ItemType.ReadOnly = true;
            this.ItemType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ItemType.Width = 85;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "none";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = null;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle7;
            this.Amount.HeaderText = "Amount";
            this.Amount.MaxInputLength = 26;
            this.Amount.Name = "Amount";
            this.Amount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Amount.Width = 135;
            // 
            // MaxValue
            // 
            this.MaxValue.HeaderText = "MaxValue";
            this.MaxValue.Name = "MaxValue";
            this.MaxValue.ReadOnly = true;
            this.MaxValue.Visible = false;
            // 
            // IsEditable
            // 
            this.IsEditable.HeaderText = "IsEditable";
            this.IsEditable.Name = "IsEditable";
            this.IsEditable.ReadOnly = true;
            this.IsEditable.Visible = false;
            // 
            // IsEdited
            // 
            this.IsEdited.HeaderText = "IsEdited";
            this.IsEdited.Name = "IsEdited";
            this.IsEdited.Visible = false;
            // 
            // MinValue
            // 
            this.MinValue.HeaderText = "MinValue";
            this.MinValue.Name = "MinValue";
            this.MinValue.ReadOnly = true;
            this.MinValue.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 105;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Description";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 400;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn3.HeaderText = "Item Type";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 3;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 110;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "none";
            this.dataGridViewTextBoxColumn4.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 200;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 110;
            // 
            // ReceiptItemsPictureBox
            // 
            this.ReceiptItemsPictureBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ReceiptItemsPictureBox.Location = new System.Drawing.Point(761, 0);
            this.ReceiptItemsPictureBox.Name = "ReceiptItemsPictureBox";
            this.ReceiptItemsPictureBox.Size = new System.Drawing.Size(42, 114);
            this.ReceiptItemsPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ReceiptItemsPictureBox.TabIndex = 13;
            this.ReceiptItemsPictureBox.TabStop = false;
            this.ReceiptItemsPictureBox.Click += new System.EventHandler(this.ReceiptItemsPictureBox_Click);
            this.ReceiptItemsPictureBox.MouseEnter += new System.EventHandler(this.ReceiptItemsPictureBox_MouseEnter);
            // 
            // F15101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ReceiptItemsGridViewPanel);
            this.Controls.Add(this.EventPictureBox);
            this.Controls.Add(this.ReceiptItemsPictureBox);
            this.Name = "F15101";
            this.Size = new System.Drawing.Size(804, 114);
            this.Tag = "15101";
            this.Load += new System.EventHandler(this.F15101_Load);
            this.Resize += new System.EventHandler(this.F15101_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.EventPictureBox)).EndInit();
            this.ReceiptItemsGridViewPanel.ResumeLayout(false);
            this.FooterPanel.ResumeLayout(false);
            this.CurrentTotalValuePanel.ResumeLayout(false);
            this.CurrentTotalPanel.ResumeLayout(false);
            this.TotalAmountValuePanel.ResumeLayout(false);
            this.TotalAmountLabelPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReceiptItemsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceiptItemsPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox EventPictureBox;
        private System.Windows.Forms.Panel ReceiptItemsGridViewPanel;
        private TerraScan.UI.Controls.TerraScanDataGridView ReceiptItemsGridView;
        private System.Windows.Forms.VScrollBar ReceiptItemsGridVscrollBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.PictureBox ReceiptItemsPictureBox;
        private System.Windows.Forms.ToolTip ReceiptItemsToolTip;
        private System.Windows.Forms.ToolTip TotalsTextBoxToolTip;
        private System.Windows.Forms.Panel FooterPanel;
        private System.Windows.Forms.Panel TotalAmountLabelPanel;
        private System.Windows.Forms.Label TotalAmountLabel;
        private System.Windows.Forms.Panel CurrentTotalValuePanel;
        private System.Windows.Forms.Label CurrentTotalValueLabel;
        private System.Windows.Forms.Panel CurrentTotalPanel;
        private System.Windows.Forms.Label CurrentTotalLabel;
        private System.Windows.Forms.Panel TotalAmountValuePanel;
        private System.Windows.Forms.Label TotalAmountValueLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsEditable;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsEdited;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinValue;
    }
}
