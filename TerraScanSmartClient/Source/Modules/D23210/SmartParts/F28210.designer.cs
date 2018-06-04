namespace D23210
{
    partial class F28210
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F28210));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.FilePathOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.TemplatePictureBox = new System.Windows.Forms.PictureBox();
            this.Errorpanel = new System.Windows.Forms.Panel();
            this.ErrorGridPanel = new System.Windows.Forms.Panel();
            this.ErrorGridVscrollBar = new System.Windows.Forms.VScrollBar();
            this.ErrorGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ErrorListingPictureBox = new System.Windows.Forms.PictureBox();
            this.ImportFileStatusLabel = new System.Windows.Forms.Label();
            this.ImportFileCountStatusLabel = new System.Windows.Forms.Label();
            this.ImportFileButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CheckErrorStatusLabel = new System.Windows.Forms.Label();
            this.CheckErrorCountStatusLabel = new System.Windows.Forms.Label();
            this.CheckErrorButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CreateReceiptStatusLabel = new System.Windows.Forms.Label();
            this.CreateReceiptButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FilePathPanel = new System.Windows.Forms.Panel();
            this.FilePathButton = new System.Windows.Forms.Button();
            this.FilePathTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.FilePathLabel = new System.Windows.Forms.Label();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.SourceTypeTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.SourceTypeLabel = new System.Windows.Forms.Label();
            this.TemplateHeaderPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTemplateNameFile = new TerraScan.UI.Controls.TerraScanLinkLabel();
            this.btnTemplateFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ParcelNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RollYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermitNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EntryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TemplatePictureBox)).BeginInit();
            this.Errorpanel.SuspendLayout();
            this.ErrorGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorListingPictureBox)).BeginInit();
            this.FilePathPanel.SuspendLayout();
            this.DescriptionPanel.SuspendLayout();
            this.TemplateHeaderPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TemplatePictureBox
            // 
            this.TemplatePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("TemplatePictureBox.Image")));
            this.TemplatePictureBox.Location = new System.Drawing.Point(768, 16);
            this.TemplatePictureBox.Name = "TemplatePictureBox";
            this.TemplatePictureBox.Size = new System.Drawing.Size(30, 62);
            this.TemplatePictureBox.TabIndex = 10;
            this.TemplatePictureBox.TabStop = false;
            // 
            // Errorpanel
            // 
            this.Errorpanel.Controls.Add(this.ErrorGridPanel);
            this.Errorpanel.Location = new System.Drawing.Point(0, 174);
            this.Errorpanel.Name = "Errorpanel";
            this.Errorpanel.Size = new System.Drawing.Size(775, 151);
            this.Errorpanel.TabIndex = 14;
            this.Errorpanel.TabStop = true;
            // 
            // ErrorGridPanel
            // 
            this.ErrorGridPanel.BackColor = System.Drawing.Color.Silver;
            this.ErrorGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ErrorGridPanel.Controls.Add(this.ErrorGridVscrollBar);
            this.ErrorGridPanel.Controls.Add(this.ErrorGridView);
            this.ErrorGridPanel.Controls.Add(this.panel2);
            this.ErrorGridPanel.Controls.Add(this.label1);
            this.ErrorGridPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ErrorGridPanel.Location = new System.Drawing.Point(0, 0);
            this.ErrorGridPanel.Name = "ErrorGridPanel";
            this.ErrorGridPanel.Size = new System.Drawing.Size(775, 151);
            this.ErrorGridPanel.TabIndex = 7;
            // 
            // ErrorGridVscrollBar
            // 
            this.ErrorGridVscrollBar.Enabled = false;
            this.ErrorGridVscrollBar.Location = new System.Drawing.Point(755, 0);
            this.ErrorGridVscrollBar.Name = "ErrorGridVscrollBar";
            this.ErrorGridVscrollBar.Size = new System.Drawing.Size(16, 132);
            this.ErrorGridVscrollBar.TabIndex = 118;
            // 
            // ErrorGridView
            // 
            this.ErrorGridView.AllowCellClick = true;
            this.ErrorGridView.AllowDoubleClick = false;
            this.ErrorGridView.AllowEmptyRows = true;
            this.ErrorGridView.AllowEnterKey = false;
            this.ErrorGridView.AllowSorting = true;
            this.ErrorGridView.AllowUserToAddRows = false;
            this.ErrorGridView.AllowUserToDeleteRows = false;
            this.ErrorGridView.AllowUserToResizeColumns = false;
            this.ErrorGridView.AllowUserToResizeRows = false;
            this.ErrorGridView.ApplyStandardBehaviour = false;
            this.ErrorGridView.BackgroundColor = System.Drawing.Color.White;
            this.ErrorGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ErrorGridView.ClearCurrentCellOnLeave = true;
            this.ErrorGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.ErrorGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ErrorGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ErrorGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParcelNumber,
            this.RollYear,
            this.PermitNumber,
            this.ErrorStatus,
            this.Line,
            this.EntryId});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ErrorGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.ErrorGridView.DefaultRowIndex = 0;
            this.ErrorGridView.DeselectCurrentCell = false;
            this.ErrorGridView.DeselectSpecifiedRow = -1;
            this.ErrorGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ErrorGridView.EnableBinding = true;
            this.ErrorGridView.EnableHeadersVisualStyles = false;
            this.ErrorGridView.GridColor = System.Drawing.Color.Black;
            this.ErrorGridView.GridContentSelected = false;
            this.ErrorGridView.IsEditableGrid = false;
            this.ErrorGridView.IsMultiSelect = false;
            this.ErrorGridView.IsSorted = false;
            this.ErrorGridView.Location = new System.Drawing.Point(-1, 0);
            this.ErrorGridView.MultiSelect = false;
            this.ErrorGridView.Name = "ErrorGridView";
            this.ErrorGridView.NumRowsVisible = 5;
            this.ErrorGridView.PrimaryKeyColumnName = "EntryID";
            this.ErrorGridView.RemainSortFields = false;
            this.ErrorGridView.RemoveDefaultSelection = true;
            this.ErrorGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ErrorGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.ErrorGridView.RowHeadersWidth = 20;
            this.ErrorGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.ErrorGridView.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.ErrorGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ErrorGridView.Size = new System.Drawing.Size(775, 133);
            this.ErrorGridView.TabIndex = 119;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(756, 131);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(17, 1);
            this.panel2.TabIndex = 116;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(385, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 15);
            this.label1.TabIndex = 2;
            // 
            // ErrorListingPictureBox
            // 
            this.ErrorListingPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ErrorListingPictureBox.Image")));
            this.ErrorListingPictureBox.Location = new System.Drawing.Point(774, 174);
            this.ErrorListingPictureBox.Name = "ErrorListingPictureBox";
            this.ErrorListingPictureBox.Size = new System.Drawing.Size(26, 151);
            this.ErrorListingPictureBox.TabIndex = 13;
            this.ErrorListingPictureBox.TabStop = false;
            // 
            // ImportFileStatusLabel
            // 
            this.ImportFileStatusLabel.BackColor = System.Drawing.Color.Silver;
            this.ImportFileStatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImportFileStatusLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportFileStatusLabel.ForeColor = System.Drawing.Color.White;
            this.ImportFileStatusLabel.Location = new System.Drawing.Point(173, 104);
            this.ImportFileStatusLabel.Name = "ImportFileStatusLabel";
            this.ImportFileStatusLabel.Size = new System.Drawing.Size(186, 24);
            this.ImportFileStatusLabel.TabIndex = 115;
            this.ImportFileStatusLabel.Text = "Pending";
            this.ImportFileStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ImportFileCountStatusLabel
            // 
            this.ImportFileCountStatusLabel.AutoSize = true;
            this.ImportFileCountStatusLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportFileCountStatusLabel.ForeColor = System.Drawing.Color.Gray;
            this.ImportFileCountStatusLabel.Location = new System.Drawing.Point(372, 110);
            this.ImportFileCountStatusLabel.Name = "ImportFileCountStatusLabel";
            this.ImportFileCountStatusLabel.Size = new System.Drawing.Size(0, 14);
            this.ImportFileCountStatusLabel.TabIndex = 114;
            // 
            // ImportFileButton
            // 
            this.ImportFileButton.ActualPermission = false;
            this.ImportFileButton.ApplyDisableBehaviour = false;
            this.ImportFileButton.AutoEllipsis = true;
            this.ImportFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.ImportFileButton.BorderColor = System.Drawing.Color.Wheat;
            this.ImportFileButton.CommentPriority = false;
            this.ImportFileButton.EnableAutoPrint = false;
            this.ImportFileButton.Enabled = false;
            this.ImportFileButton.FilterStatus = false;
            this.ImportFileButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ImportFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportFileButton.FocusRectangleEnabled = true;
            this.ImportFileButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportFileButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ImportFileButton.ImageSelected = false;
            this.ImportFileButton.Location = new System.Drawing.Point(42, 104);
            this.ImportFileButton.Name = "ImportFileButton";
            this.ImportFileButton.NewPadding = 5;
            this.ImportFileButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Edit;
            this.ImportFileButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.ImportFileButton.Size = new System.Drawing.Size(124, 24);
            this.ImportFileButton.StatusIndicator = false;
            this.ImportFileButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ImportFileButton.StatusOffText = null;
            this.ImportFileButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.ImportFileButton.StatusOnText = null;
            this.ImportFileButton.TabIndex = 3;
            this.ImportFileButton.TabStop = false;
            this.ImportFileButton.Text = "Import File";
            this.ImportFileButton.UseVisualStyleBackColor = false;
            this.ImportFileButton.Click += new System.EventHandler(this.ImportFileButton_Click);
            // 
            // CheckErrorStatusLabel
            // 
            this.CheckErrorStatusLabel.BackColor = System.Drawing.Color.Silver;
            this.CheckErrorStatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CheckErrorStatusLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckErrorStatusLabel.ForeColor = System.Drawing.Color.White;
            this.CheckErrorStatusLabel.Location = new System.Drawing.Point(172, 135);
            this.CheckErrorStatusLabel.Name = "CheckErrorStatusLabel";
            this.CheckErrorStatusLabel.Size = new System.Drawing.Size(186, 24);
            this.CheckErrorStatusLabel.TabIndex = 118;
            this.CheckErrorStatusLabel.Text = "Pending";
            this.CheckErrorStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CheckErrorCountStatusLabel
            // 
            this.CheckErrorCountStatusLabel.AutoSize = true;
            this.CheckErrorCountStatusLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckErrorCountStatusLabel.ForeColor = System.Drawing.Color.Gray;
            this.CheckErrorCountStatusLabel.Location = new System.Drawing.Point(371, 141);
            this.CheckErrorCountStatusLabel.Name = "CheckErrorCountStatusLabel";
            this.CheckErrorCountStatusLabel.Size = new System.Drawing.Size(0, 14);
            this.CheckErrorCountStatusLabel.TabIndex = 117;
            // 
            // CheckErrorButton
            // 
            this.CheckErrorButton.ActualPermission = false;
            this.CheckErrorButton.ApplyDisableBehaviour = false;
            this.CheckErrorButton.AutoEllipsis = true;
            this.CheckErrorButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CheckErrorButton.BorderColor = System.Drawing.Color.Wheat;
            this.CheckErrorButton.CommentPriority = false;
            this.CheckErrorButton.EnableAutoPrint = false;
            this.CheckErrorButton.Enabled = false;
            this.CheckErrorButton.FilterStatus = false;
            this.CheckErrorButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CheckErrorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckErrorButton.FocusRectangleEnabled = true;
            this.CheckErrorButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckErrorButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CheckErrorButton.ImageSelected = false;
            this.CheckErrorButton.Location = new System.Drawing.Point(41, 135);
            this.CheckErrorButton.Name = "CheckErrorButton";
            this.CheckErrorButton.NewPadding = 5;
            this.CheckErrorButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CheckErrorButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CheckErrorButton.Size = new System.Drawing.Size(124, 24);
            this.CheckErrorButton.StatusIndicator = false;
            this.CheckErrorButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CheckErrorButton.StatusOffText = null;
            this.CheckErrorButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CheckErrorButton.StatusOnText = null;
            this.CheckErrorButton.TabIndex = 4;
            this.CheckErrorButton.TabStop = false;
            this.CheckErrorButton.Text = "Check for Errors";
            this.CheckErrorButton.UseVisualStyleBackColor = false;
            this.CheckErrorButton.Click += new System.EventHandler(this.CheckErrorButton_Click);
            // 
            // CreateReceiptStatusLabel
            // 
            this.CreateReceiptStatusLabel.BackColor = System.Drawing.Color.Silver;
            this.CreateReceiptStatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreateReceiptStatusLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
            this.CreateReceiptStatusLabel.Location = new System.Drawing.Point(170, 333);
            this.CreateReceiptStatusLabel.Name = "CreateReceiptStatusLabel";
            this.CreateReceiptStatusLabel.Size = new System.Drawing.Size(186, 24);
            this.CreateReceiptStatusLabel.TabIndex = 120;
            this.CreateReceiptStatusLabel.Text = "Pending";
            this.CreateReceiptStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CreateReceiptButton
            // 
            this.CreateReceiptButton.ActualPermission = false;
            this.CreateReceiptButton.ApplyDisableBehaviour = false;
            this.CreateReceiptButton.AutoEllipsis = true;
            this.CreateReceiptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CreateReceiptButton.BorderColor = System.Drawing.Color.Wheat;
            this.CreateReceiptButton.CommentPriority = false;
            this.CreateReceiptButton.EnableAutoPrint = false;
            this.CreateReceiptButton.Enabled = false;
            this.CreateReceiptButton.FilterStatus = false;
            this.CreateReceiptButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CreateReceiptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateReceiptButton.FocusRectangleEnabled = true;
            this.CreateReceiptButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateReceiptButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CreateReceiptButton.ImageSelected = false;
            this.CreateReceiptButton.Location = new System.Drawing.Point(39, 333);
            this.CreateReceiptButton.Name = "CreateReceiptButton";
            this.CreateReceiptButton.NewPadding = 5;
            this.CreateReceiptButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.CreateReceiptButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CreateReceiptButton.Size = new System.Drawing.Size(124, 24);
            this.CreateReceiptButton.StatusIndicator = false;
            this.CreateReceiptButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CreateReceiptButton.StatusOffText = null;
            this.CreateReceiptButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CreateReceiptButton.StatusOnText = null;
            this.CreateReceiptButton.TabIndex = 119;
            this.CreateReceiptButton.TabStop = false;
            this.CreateReceiptButton.Text = "Create Records";
            this.CreateReceiptButton.UseVisualStyleBackColor = false;
            this.CreateReceiptButton.Click += new System.EventHandler(this.CreateReceiptButton_Click);
            // 
            // FilePathPanel
            // 
            this.FilePathPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilePathPanel.Controls.Add(this.FilePathButton);
            this.FilePathPanel.Controls.Add(this.FilePathTextBox);
            this.FilePathPanel.Controls.Add(this.FilePathLabel);
            this.FilePathPanel.Location = new System.Drawing.Point(-1, 28);
            this.FilePathPanel.Name = "FilePathPanel";
            this.FilePathPanel.Size = new System.Drawing.Size(775, 35);
            this.FilePathPanel.TabIndex = 2;
            this.FilePathPanel.TabStop = true;
            // 
            // FilePathButton
            // 
            this.FilePathButton.FlatAppearance.BorderSize = 0;
            this.FilePathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FilePathButton.Image = ((System.Drawing.Image)(resources.GetObject("FilePathButton.Image")));
            this.FilePathButton.Location = new System.Drawing.Point(734, -1);
            this.FilePathButton.Name = "FilePathButton";
            this.FilePathButton.Size = new System.Drawing.Size(21, 25);
            this.FilePathButton.TabIndex = 2;
            this.FilePathButton.Tag = "FilePathTextBox";
            this.FilePathButton.UseVisualStyleBackColor = true;
            this.FilePathButton.TextChanged += new System.EventHandler(this.EnableEditButtonInMasterForm);
            this.FilePathButton.Click += new System.EventHandler(this.FilePathButton_Click);
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.AllowClick = true;
            this.FilePathTextBox.AllowNegativeSign = false;
            this.FilePathTextBox.ApplyCFGFormat = false;
            this.FilePathTextBox.ApplyCurrencyFormat = false;
            this.FilePathTextBox.ApplyFocusColor = true;
            this.FilePathTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.FilePathTextBox.ApplyNegativeStandard = true;
            this.FilePathTextBox.ApplyParentFocusColor = true;
            this.FilePathTextBox.ApplyTimeFormat = false;
            this.FilePathTextBox.BackColor = System.Drawing.Color.White;
            this.FilePathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FilePathTextBox.CFromatWihoutSymbol = false;
            this.FilePathTextBox.CheckForEmpty = false;
            this.FilePathTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FilePathTextBox.Digits = -1;
            this.FilePathTextBox.EmptyDecimalValue = false;
            this.FilePathTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilePathTextBox.ForeColor = System.Drawing.Color.Black;
            this.FilePathTextBox.IsEditable = false;
            this.FilePathTextBox.IsQueryableFileld = false;
            this.FilePathTextBox.Location = new System.Drawing.Point(63, 8);
            this.FilePathTextBox.LockKeyPress = false;
            this.FilePathTextBox.MaxLength = 250;
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.PersistDefaultColor = false;
            this.FilePathTextBox.Precision = 2;
            this.FilePathTextBox.QueryingFileldName = "";
            this.FilePathTextBox.SetColorFlag = false;
            this.FilePathTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.FilePathTextBox.Size = new System.Drawing.Size(665, 16);
            this.FilePathTextBox.SpecialCharacter = "%";
            this.FilePathTextBox.TabIndex = 0;
            this.FilePathTextBox.TextCustomFormat = "$#,##0.00";
            this.FilePathTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.FilePathTextBox.WholeInteger = false;
            this.FilePathTextBox.TextChanged += new System.EventHandler(this.EnableEditButtonInMasterForm);
            // 
            // FilePathLabel
            // 
            this.FilePathLabel.AutoSize = true;
            this.FilePathLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.FilePathLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.FilePathLabel.Location = new System.Drawing.Point(1, 3);
            this.FilePathLabel.Name = "FilePathLabel";
            this.FilePathLabel.Size = new System.Drawing.Size(56, 14);
            this.FilePathLabel.TabIndex = 63;
            this.FilePathLabel.Text = "File Path:";
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionPanel.Controls.Add(this.SourceTypeTextBox);
            this.DescriptionPanel.Controls.Add(this.SourceTypeLabel);
            this.DescriptionPanel.Location = new System.Drawing.Point(517, -1);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(257, 30);
            this.DescriptionPanel.TabIndex = 3;
            this.DescriptionPanel.TabStop = true;
            // 
            // SourceTypeTextBox
            // 
            this.SourceTypeTextBox.AllowClick = true;
            this.SourceTypeTextBox.AllowNegativeSign = false;
            this.SourceTypeTextBox.ApplyCFGFormat = false;
            this.SourceTypeTextBox.ApplyCurrencyFormat = false;
            this.SourceTypeTextBox.ApplyFocusColor = true;
            this.SourceTypeTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.SourceTypeTextBox.ApplyNegativeStandard = true;
            this.SourceTypeTextBox.ApplyParentFocusColor = true;
            this.SourceTypeTextBox.ApplyTimeFormat = false;
            this.SourceTypeTextBox.BackColor = System.Drawing.Color.White;
            this.SourceTypeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SourceTypeTextBox.CFromatWihoutSymbol = false;
            this.SourceTypeTextBox.CheckForEmpty = false;
            this.SourceTypeTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SourceTypeTextBox.Digits = -1;
            this.SourceTypeTextBox.EmptyDecimalValue = false;
            this.SourceTypeTextBox.Enabled = false;
            this.SourceTypeTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SourceTypeTextBox.ForeColor = System.Drawing.Color.Black;
            this.SourceTypeTextBox.IsEditable = false;
            this.SourceTypeTextBox.IsQueryableFileld = false;
            this.SourceTypeTextBox.Location = new System.Drawing.Point(80, 5);
            this.SourceTypeTextBox.LockKeyPress = true;
            this.SourceTypeTextBox.MaxLength = 50;
            this.SourceTypeTextBox.Name = "SourceTypeTextBox";
            this.SourceTypeTextBox.PersistDefaultColor = false;
            this.SourceTypeTextBox.Precision = 2;
            this.SourceTypeTextBox.QueryingFileldName = "";
            this.SourceTypeTextBox.ReadOnly = true;
            this.SourceTypeTextBox.SetColorFlag = false;
            this.SourceTypeTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.SourceTypeTextBox.Size = new System.Drawing.Size(171, 16);
            this.SourceTypeTextBox.SpecialCharacter = "%";
            this.SourceTypeTextBox.TabIndex = 64;
            this.SourceTypeTextBox.TabStop = false;
            this.SourceTypeTextBox.Tag = "";
            this.SourceTypeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SourceTypeTextBox.TextCustomFormat = "";
            this.SourceTypeTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.SourceTypeTextBox.WholeInteger = false;
            this.SourceTypeTextBox.TextChanged += new System.EventHandler(this.EnableEditButtonInMasterForm);
            // 
            // SourceTypeLabel
            // 
            this.SourceTypeLabel.AutoSize = true;
            this.SourceTypeLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.SourceTypeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.SourceTypeLabel.Location = new System.Drawing.Point(1, 3);
            this.SourceTypeLabel.Name = "SourceTypeLabel";
            this.SourceTypeLabel.Size = new System.Drawing.Size(78, 14);
            this.SourceTypeLabel.TabIndex = 63;
            this.SourceTypeLabel.Text = "Source Type:";
            // 
            // TemplateHeaderPanel
            // 
            this.TemplateHeaderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TemplateHeaderPanel.Controls.Add(this.panel1);
            this.TemplateHeaderPanel.Controls.Add(this.DescriptionPanel);
            this.TemplateHeaderPanel.Controls.Add(this.FilePathPanel);
            this.TemplateHeaderPanel.Location = new System.Drawing.Point(0, 16);
            this.TemplateHeaderPanel.Name = "TemplateHeaderPanel";
            this.TemplateHeaderPanel.Size = new System.Drawing.Size(775, 62);
            this.TemplateHeaderPanel.TabIndex = 11;
            this.TemplateHeaderPanel.TabStop = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtTemplateNameFile);
            this.panel1.Controls.Add(this.btnTemplateFile);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(519, 30);
            this.panel1.TabIndex = 1;
            this.panel1.TabStop = true;
            // 
            // txtTemplateNameFile
            // 
            this.txtTemplateNameFile.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemplateNameFile.FormDllName = null;
            this.txtTemplateNameFile.FormId = 0;
            this.txtTemplateNameFile.LinkArea = new System.Windows.Forms.LinkArea(0, 250);
            this.txtTemplateNameFile.Location = new System.Drawing.Point(100, 5);
            this.txtTemplateNameFile.MenuName = null;
            this.txtTemplateNameFile.Name = "txtTemplateNameFile";
            this.txtTemplateNameFile.PermissionOpen = 0;
            this.txtTemplateNameFile.Size = new System.Drawing.Size(388, 16);
            this.txtTemplateNameFile.TabIndex = 65;
            this.txtTemplateNameFile.TabStop = true;
            this.txtTemplateNameFile.Tag = "0";
            this.txtTemplateNameFile.Text = "Statusvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv";
            this.txtTemplateNameFile.TextCustomFormat = "#,##0.00";
            this.txtTemplateNameFile.UseCompatibleTextRendering = true;
            this.txtTemplateNameFile.ValidateType = TerraScan.UI.Controls.TerraScanLinkLabel.ControlValidationType.Text;
            this.txtTemplateNameFile.TextChanged += new System.EventHandler(this.EnableEditButtonInMasterForm);
            this.txtTemplateNameFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.txtTemplateNameFile_LinkClicked);
            // 
            // btnTemplateFile
            // 
            this.btnTemplateFile.FlatAppearance.BorderSize = 0;
            this.btnTemplateFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTemplateFile.Image = ((System.Drawing.Image)(resources.GetObject("btnTemplateFile.Image")));
            this.btnTemplateFile.Location = new System.Drawing.Point(494, -2);
            this.btnTemplateFile.Name = "btnTemplateFile";
            this.btnTemplateFile.Size = new System.Drawing.Size(21, 25);
            this.btnTemplateFile.TabIndex = 1;
            this.btnTemplateFile.Tag = "txtTmeplateNameFile";
            this.btnTemplateFile.UseVisualStyleBackColor = true;
            this.btnTemplateFile.TextChanged += new System.EventHandler(this.EnableEditButtonInMasterForm);
            this.btnTemplateFile.Click += new System.EventHandler(this.btnTemplateFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label3.Location = new System.Drawing.Point(1, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 14);
            this.label3.TabIndex = 63;
            this.label3.Text = "Template Name:";
            // 
            // ParcelNumber
            // 
            this.ParcelNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.ParcelNumber.DefaultCellStyle = dataGridViewCellStyle2;
            this.ParcelNumber.HeaderText = "Parcel Number";
            this.ParcelNumber.Name = "ParcelNumber";
            this.ParcelNumber.ReadOnly = true;
            this.ParcelNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ParcelNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ParcelNumber.Width = 130;
            // 
            // RollYear
            // 
            this.RollYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.RollYear.DefaultCellStyle = dataGridViewCellStyle3;
            this.RollYear.HeaderText = "RollYear";
            this.RollYear.Name = "RollYear";
            this.RollYear.ReadOnly = true;
            this.RollYear.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RollYear.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.RollYear.Width = 60;
            // 
            // PermitNumber
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.PermitNumber.DefaultCellStyle = dataGridViewCellStyle4;
            this.PermitNumber.HeaderText = "Permit Number";
            this.PermitNumber.Name = "PermitNumber";
            this.PermitNumber.ReadOnly = true;
            this.PermitNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // ErrorStatus
            // 
            this.ErrorStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.ErrorStatus.DefaultCellStyle = dataGridViewCellStyle5;
            this.ErrorStatus.HeaderText = "Error Status";
            this.ErrorStatus.Name = "ErrorStatus";
            this.ErrorStatus.ReadOnly = true;
            this.ErrorStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ErrorStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ErrorStatus.Width = 305;
            // 
            // Line
            // 
            this.Line.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Line.DefaultCellStyle = dataGridViewCellStyle6;
            this.Line.HeaderText = "Line";
            this.Line.Name = "Line";
            this.Line.ReadOnly = true;
            this.Line.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Line.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Line.Width = 65;
            // 
            // EntryId
            // 
            this.EntryId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.EntryId.DefaultCellStyle = dataGridViewCellStyle7;
            this.EntryId.HeaderText = "EntryID";
            this.EntryId.Name = "EntryId";
            this.EntryId.ReadOnly = true;
            this.EntryId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EntryId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.EntryId.Width = 77;
            // 
            // F28210
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.TemplateHeaderPanel);
            this.Controls.Add(this.CreateReceiptStatusLabel);
            this.Controls.Add(this.CreateReceiptButton);
            this.Controls.Add(this.CheckErrorStatusLabel);
            this.Controls.Add(this.CheckErrorCountStatusLabel);
            this.Controls.Add(this.CheckErrorButton);
            this.Controls.Add(this.ImportFileStatusLabel);
            this.Controls.Add(this.ImportFileCountStatusLabel);
            this.Controls.Add(this.ImportFileButton);
            this.Controls.Add(this.Errorpanel);
            this.Controls.Add(this.ErrorListingPictureBox);
            this.Controls.Add(this.TemplatePictureBox);
            this.Name = "F28210";
            this.Size = new System.Drawing.Size(805, 400);
            this.Load += new System.EventHandler(this.F28210_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TemplatePictureBox)).EndInit();
            this.Errorpanel.ResumeLayout(false);
            this.ErrorGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorListingPictureBox)).EndInit();
            this.FilePathPanel.ResumeLayout(false);
            this.FilePathPanel.PerformLayout();
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.TemplateHeaderPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.OpenFileDialog FilePathOpenFileDialog;
        private System.Windows.Forms.PictureBox TemplatePictureBox;
        private System.Windows.Forms.Panel Errorpanel;
        private System.Windows.Forms.Panel ErrorGridPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox ErrorListingPictureBox;
        private System.Windows.Forms.Label ImportFileStatusLabel;
        private System.Windows.Forms.Label ImportFileCountStatusLabel;
        private TerraScan.UI.Controls.TerraScanButton ImportFileButton;
        private System.Windows.Forms.Label CheckErrorStatusLabel;
        private System.Windows.Forms.Label CheckErrorCountStatusLabel;
        private TerraScan.UI.Controls.TerraScanButton CheckErrorButton;
        private System.Windows.Forms.Label CreateReceiptStatusLabel;
        private TerraScan.UI.Controls.TerraScanButton CreateReceiptButton;
        private System.Windows.Forms.Panel FilePathPanel;
        private System.Windows.Forms.Button FilePathButton;
        private TerraScan.UI.Controls.TerraScanTextBox FilePathTextBox;
        private System.Windows.Forms.Label FilePathLabel;
        private System.Windows.Forms.Panel DescriptionPanel;
        private System.Windows.Forms.Label SourceTypeLabel;
        private System.Windows.Forms.Panel TemplateHeaderPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTemplateFile;
        private TerraScan.UI.Controls.TerraScanTextBox SourceTypeTextBox;
        private System.Windows.Forms.VScrollBar ErrorGridVscrollBar;
        private TerraScan.UI.Controls.TerraScanLinkLabel txtTemplateNameFile;
        private System.Windows.Forms.Label label1;
        private TerraScan.UI.Controls.TerraScanDataGridView ErrorGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParcelNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn RollYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermitNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn EntryId;
    }
}
