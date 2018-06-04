namespace D8058
{
    partial class F8060
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8060));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.PartsDetailsGridVscrollBar = new System.Windows.Forms.VScrollBar();
            this.PartsGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.PartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComponentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.ComponentPanel = new System.Windows.Forms.Panel();
            this.ComponentLabel = new System.Windows.Forms.Label();
            this.ComponentComboBox = new TerraScan.UI.Controls.TerraScanComboBox();
            this.formName = new System.Windows.Forms.Label();
            this.SaveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DeleteButton = new TerraScan.UI.Controls.TerraScanButton();
            this.PartsCfgSecIndicatorPictureBox = new System.Windows.Forms.PictureBox();
            this.PartsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel4.SuspendLayout();
            this.GridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PartsGridView)).BeginInit();
            this.HeaderPanel.SuspendLayout();
            this.ComponentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PartsCfgSecIndicatorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(0, 226);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(20, 21);
            this.panel2.TabIndex = 124;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Location = new System.Drawing.Point(19, 226);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(749, 21);
            this.panel4.TabIndex = 123;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(728, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 21);
            this.panel1.TabIndex = 115;
            // 
            // GridPanel
            // 
            this.GridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridPanel.Controls.Add(this.PartsDetailsGridVscrollBar);
            this.GridPanel.Controls.Add(this.PartsGridView);
            this.GridPanel.Location = new System.Drawing.Point(0, 52);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(768, 195);
            this.GridPanel.TabIndex = 2;
            this.GridPanel.TabStop = true;
            // 
            // PartsDetailsGridVscrollBar
            // 
            this.PartsDetailsGridVscrollBar.Enabled = false;
            this.PartsDetailsGridVscrollBar.Location = new System.Drawing.Point(749, 0);
            this.PartsDetailsGridVscrollBar.Name = "PartsDetailsGridVscrollBar";
            this.PartsDetailsGridVscrollBar.Size = new System.Drawing.Size(16, 174);
            this.PartsDetailsGridVscrollBar.TabIndex = 1004;
            // 
            // PartsGridView
            // 
            this.PartsGridView.AllowCellClick = true;
            this.PartsGridView.AllowDoubleClick = true;
            this.PartsGridView.AllowEmptyRows = true;
            this.PartsGridView.AllowEnterKey = false;
            this.PartsGridView.AllowSorting = true;
            this.PartsGridView.AllowUserToAddRows = false;
            this.PartsGridView.AllowUserToDeleteRows = false;
            this.PartsGridView.AllowUserToResizeColumns = false;
            this.PartsGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.PartsGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.PartsGridView.ApplyStandardBehaviour = false;
            this.PartsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.PartsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PartsGridView.ClearCurrentCellOnLeave = false;
            this.PartsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PartsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.PartsGridView.ColumnHeadersHeight = 21;
            this.PartsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.PartsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartName,
            this.PartNumber,
            this.Active,
            this.Cost,
            this.PartID,
            this.ComponentID});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PartsGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.PartsGridView.DefaultRowIndex = 0;
            this.PartsGridView.DeselectCurrentCell = false;
            this.PartsGridView.DeselectSpecifiedRow = -1;
            this.PartsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.PartsGridView.EnableBinding = true;
            this.PartsGridView.EnableHeadersVisualStyles = false;
            this.PartsGridView.GridColor = System.Drawing.Color.Black;
            this.PartsGridView.GridContentSelected = false;
            this.PartsGridView.IsEditableGrid = true;
            this.PartsGridView.IsSorted = false;
            this.PartsGridView.Location = new System.Drawing.Point(-1, -1);
            this.PartsGridView.MultiSelect = false;
            this.PartsGridView.Name = "PartsGridView";
            this.PartsGridView.NumRowsVisible = 7;
            this.PartsGridView.PrimaryKeyColumnName = "";
            this.PartsGridView.RemainSortFields = true;
            this.PartsGridView.RemoveDefaultSelection = true;
            this.PartsGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PartsGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.PartsGridView.RowHeadersWidth = 20;
            this.PartsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PartsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PartsGridView.Size = new System.Drawing.Size(766, 175);
            this.PartsGridView.TabIndex = 3;
            this.PartsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PartsGridView_CellClick);
            this.PartsGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.PartsGridView_RowEnter);
            this.PartsGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.PartsGridView_RowHeaderMouseClick);
            this.PartsGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.PartsGridView_DataBindingComplete);
            this.PartsGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.PartsGridView_CellParsing);
            this.PartsGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.PartsGridView_CellEndEdit);
            this.PartsGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.PartsGridView_DataError);
            this.PartsGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.PartsGridView_EditingControlShowing);
            this.PartsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PartsGridView_CellContentClick);
            // 
            // PartName
            // 
            this.PartName.DataPropertyName = "PartName";
            this.PartName.HeaderText = "Part Name";
            this.PartName.MaxInputLength = 50;
            this.PartName.Name = "PartName";
            this.PartName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PartName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PartName.Width = 299;
            // 
            // PartNumber
            // 
            this.PartNumber.DataPropertyName = "PartNumber";
            this.PartNumber.HeaderText = "Part Number";
            this.PartNumber.MaxInputLength = 50;
            this.PartNumber.Name = "PartNumber";
            this.PartNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PartNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PartNumber.Width = 180;
            // 
            // Active
            // 
            this.Active.HeaderText = "Active";
            this.Active.Name = "Active";
            // 
            // Cost
            // 
            this.Cost.DataPropertyName = "Rate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Cost.DefaultCellStyle = dataGridViewCellStyle3;
            this.Cost.HeaderText = "Cost";
            this.Cost.MaxInputLength = 15;
            this.Cost.Name = "Cost";
            this.Cost.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Cost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Cost.Width = 150;
            // 
            // PartID
            // 
            this.PartID.DataPropertyName = "PartID";
            this.PartID.HeaderText = "PartID";
            this.PartID.Name = "PartID";
            this.PartID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PartID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PartID.Visible = false;
            // 
            // ComponentID
            // 
            this.ComponentID.DataPropertyName = "ComponentID";
            this.ComponentID.HeaderText = "ComponentID";
            this.ComponentID.Name = "ComponentID";
            this.ComponentID.ReadOnly = true;
            this.ComponentID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ComponentID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ComponentID.Visible = false;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.HeaderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HeaderPanel.Controls.Add(this.ComponentPanel);
            this.HeaderPanel.Controls.Add(this.formName);
            this.HeaderPanel.Controls.Add(this.SaveButton);
            this.HeaderPanel.Controls.Add(this.CancelButton);
            this.HeaderPanel.Controls.Add(this.DeleteButton);
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(768, 53);
            this.HeaderPanel.TabIndex = 0;
            this.HeaderPanel.TabStop = true;
            // 
            // ComponentPanel
            // 
            this.ComponentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ComponentPanel.Controls.Add(this.ComponentLabel);
            this.ComponentPanel.Controls.Add(this.ComponentComboBox);
            this.ComponentPanel.Location = new System.Drawing.Point(91, 6);
            this.ComponentPanel.Name = "ComponentPanel";
            this.ComponentPanel.Size = new System.Drawing.Size(272, 42);
            this.ComponentPanel.TabIndex = 0;
            this.ComponentPanel.TabStop = true;
            // 
            // ComponentLabel
            // 
            this.ComponentLabel.AutoSize = true;
            this.ComponentLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.ComponentLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ComponentLabel.Location = new System.Drawing.Point(0, -1);
            this.ComponentLabel.Name = "ComponentLabel";
            this.ComponentLabel.Size = new System.Drawing.Size(75, 14);
            this.ComponentLabel.TabIndex = 2;
            this.ComponentLabel.Text = "Component:";
            this.ComponentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ComponentComboBox
            // 
            this.ComponentComboBox.BackColor = System.Drawing.Color.White;
            this.ComponentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComponentComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComponentComboBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComponentComboBox.ForeColor = System.Drawing.Color.Black;
            this.ComponentComboBox.FormattingEnabled = true;
            this.ComponentComboBox.Location = new System.Drawing.Point(5, 14);
            this.ComponentComboBox.Name = "ComponentComboBox";
            this.ComponentComboBox.Size = new System.Drawing.Size(261, 24);
            this.ComponentComboBox.TabIndex = 1;
            this.ComponentComboBox.SelectionChangeCommitted += new System.EventHandler(this.ComponentComboBox_SelectionChangeCommitted);
            this.ComponentComboBox.SelectedIndexChanged += new System.EventHandler(this.ComponentComboBox_SelectedIndexChanged);
            // 
            // formName
            // 
            this.formName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.formName.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(80)))), ((int)(((byte)(129)))));
            this.formName.Location = new System.Drawing.Point(12, 19);
            this.formName.Name = "formName";
            this.formName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.formName.Size = new System.Drawing.Size(59, 22);
            this.formName.TabIndex = 16;
            this.formName.Text = "Parts";
            this.formName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SaveButton
            // 
            this.SaveButton.ActualPermission = false;
            this.SaveButton.ApplyDisableBehaviour = false;
            this.SaveButton.AutoEllipsis = true;
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SaveButton.BorderColor = System.Drawing.Color.Wheat;
            this.SaveButton.CommentPriority = false;
            this.SaveButton.EnableAutoPrint = false;
            this.SaveButton.Enabled = false;
            this.SaveButton.FilterStatus = false;
            this.SaveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.FocusRectangleEnabled = true;
            this.SaveButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SaveButton.ImageSelected = false;
            this.SaveButton.Location = new System.Drawing.Point(550, 16);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.NewPadding = 5;
            this.SaveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Edit;
            this.SaveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SaveButton.Size = new System.Drawing.Size(98, 28);
            this.SaveButton.StatusIndicator = false;
            this.SaveButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SaveButton.StatusOffText = null;
            this.SaveButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SaveButton.StatusOnText = null;
            this.SaveButton.TabIndex = 13;
            this.SaveButton.TabStop = false;
            this.SaveButton.Tag = "SAVE";
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.ActualPermission = false;
            this.CancelButton.ApplyDisableBehaviour = false;
            this.CancelButton.AutoEllipsis = true;
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelButton.CausesValidation = false;
            this.CancelButton.CommentPriority = false;
            this.CancelButton.EnableAutoPrint = false;
            this.CancelButton.Enabled = false;
            this.CancelButton.FilterStatus = false;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.FocusRectangleEnabled = true;
            this.CancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelButton.ImageSelected = false;
            this.CancelButton.Location = new System.Drawing.Point(654, 16);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NewPadding = 5;
            this.CancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.CancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelButton.Size = new System.Drawing.Size(98, 28);
            this.CancelButton.StatusIndicator = false;
            this.CancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelButton.StatusOffText = null;
            this.CancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelButton.StatusOnText = null;
            this.CancelButton.TabIndex = 14;
            this.CancelButton.TabStop = false;
            this.CancelButton.Tag = "CANCEL";
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.ActualPermission = false;
            this.DeleteButton.ApplyDisableBehaviour = false;
            this.DeleteButton.AutoEllipsis = true;
            this.DeleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.DeleteButton.BorderColor = System.Drawing.Color.Wheat;
            this.DeleteButton.CommentPriority = false;
            this.DeleteButton.EnableAutoPrint = false;
            this.DeleteButton.FilterStatus = false;
            this.DeleteButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteButton.FocusRectangleEnabled = true;
            this.DeleteButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DeleteButton.ImageSelected = false;
            this.DeleteButton.Location = new System.Drawing.Point(446, 16);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.NewPadding = 5;
            this.DeleteButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Delete;
            this.DeleteButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.DeleteButton.Size = new System.Drawing.Size(98, 28);
            this.DeleteButton.StatusIndicator = false;
            this.DeleteButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DeleteButton.StatusOffText = null;
            this.DeleteButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.DeleteButton.StatusOnText = null;
            this.DeleteButton.TabIndex = 15;
            this.DeleteButton.TabStop = false;
            this.DeleteButton.Tag = "DELETE";
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // PartsCfgSecIndicatorPictureBox
            // 
            this.PartsCfgSecIndicatorPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("PartsCfgSecIndicatorPictureBox.Image")));
            this.PartsCfgSecIndicatorPictureBox.Location = new System.Drawing.Point(761, 0);
            this.PartsCfgSecIndicatorPictureBox.Name = "PartsCfgSecIndicatorPictureBox";
            this.PartsCfgSecIndicatorPictureBox.Size = new System.Drawing.Size(42, 247);
            this.PartsCfgSecIndicatorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PartsCfgSecIndicatorPictureBox.TabIndex = 125;
            this.PartsCfgSecIndicatorPictureBox.TabStop = false;
            this.PartsCfgSecIndicatorPictureBox.Click += new System.EventHandler(this.PartsCfgSecIndicatorPictureBox_Click);
            this.PartsCfgSecIndicatorPictureBox.MouseEnter += new System.EventHandler(this.PartsCfgSecIndicatorPictureBox_MouseEnter);
            // 
            // F8060
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.HeaderPanel);
            this.Controls.Add(this.PartsCfgSecIndicatorPictureBox);
            this.Name = "F8060";
            this.ParentFormId = 8060;
            this.Size = new System.Drawing.Size(804, 249);
            this.Load += new System.EventHandler(this.F8060_Load);
            this.panel4.ResumeLayout(false);
            this.GridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PartsGridView)).EndInit();
            this.HeaderPanel.ResumeLayout(false);
            this.ComponentPanel.ResumeLayout(false);
            this.ComponentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PartsCfgSecIndicatorPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.VScrollBar PartsDetailsGridVscrollBar;
        private TerraScan.UI.Controls.TerraScanDataGridView PartsGridView;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Label formName;
        private TerraScan.UI.Controls.TerraScanButton SaveButton;
        private TerraScan.UI.Controls.TerraScanButton CancelButton;
        private TerraScan.UI.Controls.TerraScanButton DeleteButton;
        private System.Windows.Forms.PictureBox PartsCfgSecIndicatorPictureBox;
        private System.Windows.Forms.Panel ComponentPanel;
        private TerraScan.UI.Controls.TerraScanComboBox ComponentComboBox;
        private System.Windows.Forms.Label ComponentLabel;
        private System.Windows.Forms.ToolTip PartsToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComponentID;
    }
}