namespace D1203
{
    partial class F1203
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DueDateGridPanel = new System.Windows.Forms.Panel();
            this.DueDateGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.PostTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RollYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsFirstDueEditable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstDue = new TerraScan.UI.Controls.TerraScanTextAndImageColumn();
            this.IsSecondDueEditable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SecondDue = new TerraScan.UI.Controls.TerraScanTextAndImageColumn();
            this.DefaultGracePeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SaveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.OwnerDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.FormHeaderDeckWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.DueDatePictureBox = new System.Windows.Forms.PictureBox();
            this.HelpLinkLabel = new TerraScan.SmartParts.HelpSmartPart();
            this.panel5 = new System.Windows.Forms.Panel();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.DueDateGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DueDateGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DueDatePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DueDateGridPanel
            // 
            this.DueDateGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DueDateGridPanel.Controls.Add(this.DueDateGridView);
            this.DueDateGridPanel.Location = new System.Drawing.Point(30, 60);
            this.DueDateGridPanel.Name = "DueDateGridPanel";
            this.DueDateGridPanel.Size = new System.Drawing.Size(754, 573);
            this.DueDateGridPanel.TabIndex = 4;
            this.DueDateGridPanel.TabStop = true;
            // 
            // DueDateGridView
            // 
            this.DueDateGridView.AllowCellClick = true;
            this.DueDateGridView.AllowDoubleClick = false;
            this.DueDateGridView.AllowEmptyRows = true;
            this.DueDateGridView.AllowEnterKey = true;
            this.DueDateGridView.AllowSorting = true;
            this.DueDateGridView.AllowUserToAddRows = false;
            this.DueDateGridView.AllowUserToDeleteRows = false;
            this.DueDateGridView.AllowUserToResizeColumns = false;
            this.DueDateGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.DueDateGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DueDateGridView.ApplyStandardBehaviour = false;
            this.DueDateGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.DueDateGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DueDateGridView.ClearCurrentCellOnLeave = true;
            this.DueDateGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DueDateGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DueDateGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DueDateGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PostTypeID,
            this.RollYear,
            this.PostType,
            this.IsFirstDueEditable,
            this.FirstDue,
            this.IsSecondDueEditable,
            this.SecondDue,
            this.DefaultGracePeriod});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DueDateGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.DueDateGridView.DefaultRowIndex = -1;
            this.DueDateGridView.DeselectCurrentCell = false;
            this.DueDateGridView.DeselectSpecifiedRow = -1;
            this.DueDateGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DueDateGridView.EnableBinding = true;
            this.DueDateGridView.EnableHeadersVisualStyles = false;
            this.DueDateGridView.GridColor = System.Drawing.Color.Black;
            this.DueDateGridView.GridContentSelected = false;
            this.DueDateGridView.IsEditableGrid = false;
            this.DueDateGridView.IsMultiSelect = false;
            this.DueDateGridView.IsSorted = true;
            this.DueDateGridView.Location = new System.Drawing.Point(-1, -1);
            this.DueDateGridView.MultiSelect = false;
            this.DueDateGridView.Name = "DueDateGridView";
            this.DueDateGridView.NumRowsVisible = 24;
            this.DueDateGridView.PrimaryKeyColumnName = "";
            this.DueDateGridView.RemainSortFields = false;
            this.DueDateGridView.RemoveDefaultSelection = true;
            this.DueDateGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DueDateGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DueDateGridView.RowHeadersWidth = 20;
            this.DueDateGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DueDateGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DueDateGridView.Size = new System.Drawing.Size(754, 574);
            this.DueDateGridView.TabIndex = 5;
            this.DueDateGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DueDateGridView_CellValueChanged);
            this.DueDateGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DueDateGridView_CellMouseClick);
            this.DueDateGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DueDateGridView_MouseClick);
            this.DueDateGridView.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DueDateGridView_CellValidated);
            this.DueDateGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DueDateGridView_RowPrePaint);
            this.DueDateGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.DueDateGridView_CellParsing);
            this.DueDateGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DueDateGridView_CellEndEdit);
            this.DueDateGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DueDateGridView_CellClick);
            this.DueDateGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DueDateGridView_EditingControlShowing);
            this.DueDateGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DueDateGridView_DataError);
            this.DueDateGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DueDateGridView_CellEnter);
            this.DueDateGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DueDateGridView_DataBindingComplete);
            this.DueDateGridView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DueDateGridView_KeyPress);
            // 
            // PostTypeID
            // 
            this.PostTypeID.HeaderText = "PostTypeID";
            this.PostTypeID.Name = "PostTypeID";
            this.PostTypeID.Visible = false;
            // 
            // RollYear
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gray;
            this.RollYear.DefaultCellStyle = dataGridViewCellStyle3;
            this.RollYear.HeaderText = "Roll Year";
            this.RollYear.Name = "RollYear";
            this.RollYear.ReadOnly = true;
            this.RollYear.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // PostType
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Gray;
            this.PostType.DefaultCellStyle = dataGridViewCellStyle4;
            this.PostType.HeaderText = "Post Type";
            this.PostType.Name = "PostType";
            this.PostType.ReadOnly = true;
            this.PostType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.PostType.Width = 235;
            // 
            // IsFirstDueEditable
            // 
            this.IsFirstDueEditable.HeaderText = "IsFirstDueEditable";
            this.IsFirstDueEditable.Name = "IsFirstDueEditable";
            this.IsFirstDueEditable.Visible = false;
            // 
            // FirstDue
            // 
            this.FirstDue.HeaderText = "First Due";
            this.FirstDue.Image = null;
            this.FirstDue.Name = "FirstDue";
            this.FirstDue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FirstDue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.FirstDue.Width = 105;
            // 
            // IsSecondDueEditable
            // 
            this.IsSecondDueEditable.HeaderText = "IsSecondDueEditable";
            this.IsSecondDueEditable.Name = "IsSecondDueEditable";
            this.IsSecondDueEditable.Visible = false;
            // 
            // SecondDue
            // 
            this.SecondDue.HeaderText = "Second Due";
            this.SecondDue.Image = null;
            this.SecondDue.Name = "SecondDue";
            this.SecondDue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SecondDue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.SecondDue.Width = 105;
            // 
            // DefaultGracePeriod
            // 
            this.DefaultGracePeriod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DefaultGracePeriod.HeaderText = "Default Grace Period";
            this.DefaultGracePeriod.MaxInputLength = 5;
            this.DefaultGracePeriod.Name = "DefaultGracePeriod";
            this.DefaultGracePeriod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // CancelButton
            // 
            this.CancelButton.ActualPermission = false;
            this.CancelButton.ApplyDisableBehaviour = false;
            this.CancelButton.AutoSize = true;
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.CancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.CancelButton.CommentPriority = false;
            this.CancelButton.EnableAutoPrint = false;
            this.CancelButton.FilterStatus = false;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.FocusRectangleEnabled = true;
            this.CancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelButton.ImageSelected = false;
            this.CancelButton.Location = new System.Drawing.Point(155, 14);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NewPadding = 5;
            this.CancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.CancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelButton.Size = new System.Drawing.Size(104, 28);
            this.CancelButton.StatusIndicator = false;
            this.CancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CancelButton.StatusOffText = null;
            this.CancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.CancelButton.StatusOnText = null;
            this.CancelButton.TabIndex = 3;
            this.CancelButton.TabStop = false;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.ActualPermission = false;
            this.SaveButton.ApplyDisableBehaviour = false;
            this.SaveButton.AutoSize = true;
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.SaveButton.BorderColor = System.Drawing.Color.Wheat;
            this.SaveButton.CommentPriority = false;
            this.SaveButton.EnableAutoPrint = false;
            this.SaveButton.FilterStatus = false;
            this.SaveButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.FocusRectangleEnabled = true;
            this.SaveButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SaveButton.ImageSelected = false;
            this.SaveButton.Location = new System.Drawing.Point(30, 14);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.NewPadding = 5;
            this.SaveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.SaveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SaveButton.Size = new System.Drawing.Size(104, 28);
            this.SaveButton.StatusIndicator = false;
            this.SaveButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SaveButton.StatusOffText = null;
            this.SaveButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.SaveButton.StatusOnText = null;
            this.SaveButton.TabIndex = 2;
            this.SaveButton.TabStop = false;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // OwnerDateTimePicker
            // 
            this.OwnerDateTimePicker.CustomFormat = "MM/dd/yyyy";
            this.OwnerDateTimePicker.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OwnerDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.OwnerDateTimePicker.Location = new System.Drawing.Point(466, 34);
            this.OwnerDateTimePicker.Margin = new System.Windows.Forms.Padding(0);
            this.OwnerDateTimePicker.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            this.OwnerDateTimePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.OwnerDateTimePicker.Name = "OwnerDateTimePicker";
            this.OwnerDateTimePicker.Size = new System.Drawing.Size(0, 20);
            this.OwnerDateTimePicker.TabIndex = 0;
            this.OwnerDateTimePicker.TabStop = false;
            this.OwnerDateTimePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OwnerDateTimePicker_KeyPress);
            this.OwnerDateTimePicker.CloseUp += new System.EventHandler(this.OwnerDateTimePicker_CloseUp);
            // 
            // FormHeaderDeckWorkspace
            // 
            this.FormHeaderDeckWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FormHeaderDeckWorkspace.BackColor = System.Drawing.Color.White;
            this.FormHeaderDeckWorkspace.Location = new System.Drawing.Point(720, 0);
            this.FormHeaderDeckWorkspace.Name = "FormHeaderDeckWorkspace";
            this.FormHeaderDeckWorkspace.Size = new System.Drawing.Size(231, 62);
            this.FormHeaderDeckWorkspace.TabIndex = 60;
            this.FormHeaderDeckWorkspace.TabStop = false;
            this.FormHeaderDeckWorkspace.Text = "FormHeaderSmartPart";
            // 
            // DueDatePictureBox
            // 
            this.DueDatePictureBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DueDatePictureBox.Location = new System.Drawing.Point(778, 60);
            this.DueDatePictureBox.Name = "DueDatePictureBox";
            this.DueDatePictureBox.Size = new System.Drawing.Size(42, 573);
            this.DueDatePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DueDatePictureBox.TabIndex = 208;
            this.DueDatePictureBox.TabStop = false;
            // 
            // HelpLinkLabel
            // 
            this.HelpLinkLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.HelpLinkLabel.BackColor = System.Drawing.Color.White;
            this.HelpLinkLabel.FormId = "1203";
            this.HelpLinkLabel.Location = new System.Drawing.Point(346, 691);
            this.HelpLinkLabel.Name = "HelpLinkLabel";
            this.HelpLinkLabel.Size = new System.Drawing.Size(43, 27);
            this.HelpLinkLabel.TabIndex = 1016;
            this.HelpLinkLabel.Tag = "1203";
            this.HelpLinkLabel.VisibleHelpButton = false;
            this.HelpLinkLabel.VisibleHelpLinkButton = true;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.panel5.Location = new System.Drawing.Point(30, 686);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(800, 2);
            this.panel5.TabIndex = 1015;
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AccessibleDescription = "0";
            this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(25, 691);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(35, 15);
            this.FormIDLabel.TabIndex = 1014;
            this.FormIDLabel.Text = "1203";
            // 
            // F1203
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.HelpLinkLabel);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.FormIDLabel);
            this.Controls.Add(this.OwnerDateTimePicker);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DueDateGridPanel);
            this.Controls.Add(this.FormHeaderDeckWorkspace);
            this.Controls.Add(this.DueDatePictureBox);
            this.Name = "F1203";
            this.Size = new System.Drawing.Size(950, 750);
            this.Tag = "1203";
            this.Load += new System.EventHandler(this.F1203_Load);
            this.DueDateGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DueDateGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DueDatePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DueDateGridPanel;
        private TerraScan.UI.Controls.TerraScanDataGridView DueDateGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostTypeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RollYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostType;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsFirstDueEditable;
        private TerraScan.UI.Controls.TerraScanTextAndImageColumn FirstDue;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsSecondDueEditable;
        private TerraScan.UI.Controls.TerraScanTextAndImageColumn SecondDue;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultGracePeriod;
        private TerraScan.UI.Controls.TerraScanButton CancelButton;
        private TerraScan.UI.Controls.TerraScanButton SaveButton;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FormHeaderDeckWorkspace;
        private System.Windows.Forms.PictureBox DueDatePictureBox;
        private System.Windows.Forms.DateTimePicker OwnerDateTimePicker;
        private TerraScan.SmartParts.HelpSmartPart HelpLinkLabel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label FormIDLabel;

    }
}

