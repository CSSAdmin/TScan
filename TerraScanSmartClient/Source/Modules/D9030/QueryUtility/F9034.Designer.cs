namespace D9030
{
    partial class F9034
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9034));
            this.DeleteColumn = new TerraScan.UI.Controls.TerraScanButton();
            this.NewColumnButton = new TerraScan.UI.Controls.TerraScanButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.F9034OKButton = new TerraScan.UI.Controls.TerraScanButton();
            this.F9034CancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.F9034FooterPanel = new System.Windows.Forms.Panel();
            this.HelpLink = new System.Windows.Forms.LinkLabel();
            this.FormIDLabel = new System.Windows.Forms.Label();
            this.FormLinePanel = new System.Windows.Forms.Panel();
            this.ColumnChooserPanel = new System.Windows.Forms.Panel();
            this.EngineFieldDataGrid = new Infragistics.Win.UltraWinGrid.UltraGridColumnChooser();
            this.QueryEngineFieldMgmtMenuStrip = new System.Windows.Forms.MenuStrip();
            this.HelpStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.F9034FooterPanel.SuspendLayout();
            this.ColumnChooserPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EngineFieldDataGrid)).BeginInit();
            this.QueryEngineFieldMgmtMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeleteColumn
            // 
            this.DeleteColumn.ActualPermission = false;
            this.DeleteColumn.ApplyDisableBehaviour = false;
            this.DeleteColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.DeleteColumn.BorderColor = System.Drawing.Color.Wheat;
            this.DeleteColumn.CommentPriority = false;
            this.DeleteColumn.EnableAutoPrint = false;
            this.DeleteColumn.FilterStatus = false;
            this.DeleteColumn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.DeleteColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteColumn.FocusRectangleEnabled = true;
            this.DeleteColumn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteColumn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DeleteColumn.ImageSelected = false;
            this.DeleteColumn.Location = new System.Drawing.Point(141, 271);
            this.DeleteColumn.Name = "DeleteColumn";
            this.DeleteColumn.NewPadding = 5;
            this.DeleteColumn.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.DeleteColumn.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.DeleteColumn.Size = new System.Drawing.Size(110, 30);
            this.DeleteColumn.StatusIndicator = false;
            this.DeleteColumn.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DeleteColumn.StatusOffText = null;
            this.DeleteColumn.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.DeleteColumn.StatusOnText = null;
            this.DeleteColumn.TabIndex = 2;
            this.DeleteColumn.Text = "Delete Field";
            this.DeleteColumn.UseVisualStyleBackColor = false;
            this.DeleteColumn.Click += new System.EventHandler(this.DeleteColumn_Click);
            // 
            // NewColumnButton
            // 
            this.NewColumnButton.ActualPermission = false;
            this.NewColumnButton.ApplyDisableBehaviour = false;
            this.NewColumnButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.NewColumnButton.BorderColor = System.Drawing.Color.Wheat;
            this.NewColumnButton.CommentPriority = false;
            this.NewColumnButton.EnableAutoPrint = false;
            this.NewColumnButton.FilterStatus = false;
            this.NewColumnButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.NewColumnButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewColumnButton.FocusRectangleEnabled = true;
            this.NewColumnButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewColumnButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.NewColumnButton.ImageSelected = false;
            this.NewColumnButton.Location = new System.Drawing.Point(14, 272);
            this.NewColumnButton.Name = "NewColumnButton";
            this.NewColumnButton.NewPadding = 5;
            this.NewColumnButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.NewColumnButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.NewColumnButton.Size = new System.Drawing.Size(110, 30);
            this.NewColumnButton.StatusIndicator = false;
            this.NewColumnButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NewColumnButton.StatusOffText = null;
            this.NewColumnButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.NewColumnButton.StatusOnText = null;
            this.NewColumnButton.TabIndex = 1;
            this.NewColumnButton.Text = "New Field";
            this.NewColumnButton.UseVisualStyleBackColor = false;
            this.NewColumnButton.Click += new System.EventHandler(this.NewColumnButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 23);
            this.label1.TabIndex = 213;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Gray;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(51, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 23);
            this.label2.TabIndex = 214;
            this.label2.Text = "Field";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // F9034OKButton
            // 
            this.F9034OKButton.ActualPermission = false;
            this.F9034OKButton.ApplyDisableBehaviour = false;
            this.F9034OKButton.AutoSize = true;
            this.F9034OKButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.F9034OKButton.BorderColor = System.Drawing.Color.Wheat;
            this.F9034OKButton.CommentPriority = false;
            this.F9034OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.F9034OKButton.EnableAutoPrint = false;
            this.F9034OKButton.FilterStatus = false;
            this.F9034OKButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.F9034OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.F9034OKButton.FocusRectangleEnabled = true;
            this.F9034OKButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.F9034OKButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.F9034OKButton.ImageSelected = false;
            this.F9034OKButton.Location = new System.Drawing.Point(14, 308);
            this.F9034OKButton.Name = "F9034OKButton";
            this.F9034OKButton.NewPadding = 5;
            this.F9034OKButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.F9034OKButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.F9034OKButton.Size = new System.Drawing.Size(110, 30);
            this.F9034OKButton.StatusIndicator = false;
            this.F9034OKButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.F9034OKButton.StatusOffText = null;
            this.F9034OKButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.F9034OKButton.StatusOnText = null;
            this.F9034OKButton.TabIndex = 216;
            this.F9034OKButton.TabStop = false;
            this.F9034OKButton.Text = "OK";
            this.F9034OKButton.UseVisualStyleBackColor = false;
            this.F9034OKButton.Click += new System.EventHandler(this.F9034OKButton_Click);
            // 
            // F9034CancelButton
            // 
            this.F9034CancelButton.ActualPermission = false;
            this.F9034CancelButton.ApplyDisableBehaviour = false;
            this.F9034CancelButton.AutoSize = true;
            this.F9034CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.F9034CancelButton.BorderColor = System.Drawing.Color.Wheat;
            this.F9034CancelButton.CommentPriority = false;
            this.F9034CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.F9034CancelButton.EnableAutoPrint = false;
            this.F9034CancelButton.FilterStatus = false;
            this.F9034CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.F9034CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.F9034CancelButton.FocusRectangleEnabled = true;
            this.F9034CancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.F9034CancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.F9034CancelButton.ImageSelected = false;
            this.F9034CancelButton.Location = new System.Drawing.Point(142, 308);
            this.F9034CancelButton.Name = "F9034CancelButton";
            this.F9034CancelButton.NewPadding = 5;
            this.F9034CancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.F9034CancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.F9034CancelButton.Size = new System.Drawing.Size(110, 30);
            this.F9034CancelButton.StatusIndicator = false;
            this.F9034CancelButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.F9034CancelButton.StatusOffText = null;
            this.F9034CancelButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.F9034CancelButton.StatusOnText = null;
            this.F9034CancelButton.TabIndex = 217;
            this.F9034CancelButton.TabStop = false;
            this.F9034CancelButton.Text = "Cancel";
            this.F9034CancelButton.UseVisualStyleBackColor = false;
            // 
            // F9034FooterPanel
            // 
            this.F9034FooterPanel.BackColor = System.Drawing.Color.Transparent;
            this.F9034FooterPanel.Controls.Add(this.HelpLink);
            this.F9034FooterPanel.Controls.Add(this.FormIDLabel);
            this.F9034FooterPanel.Controls.Add(this.FormLinePanel);
            this.F9034FooterPanel.Location = new System.Drawing.Point(14, 342);
            this.F9034FooterPanel.Name = "F9034FooterPanel";
            this.F9034FooterPanel.Size = new System.Drawing.Size(239, 28);
            this.F9034FooterPanel.TabIndex = 218;
            this.F9034FooterPanel.TabStop = true;
            // 
            // HelpLink
            // 
            this.HelpLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpLink.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLink.Location = new System.Drawing.Point(130, 7);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(106, 15);
            this.HelpLink.TabIndex = 213;
            this.HelpLink.TabStop = true;
            this.HelpLink.Text = "Help";
            this.HelpLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.HelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLink_LinkClicked);
            // 
            // FormIDLabel
            // 
            this.FormIDLabel.AccessibleDescription = "0";
            this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FormIDLabel.AutoSize = true;
            this.FormIDLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.FormIDLabel.Location = new System.Drawing.Point(3, 8);
            this.FormIDLabel.Name = "FormIDLabel";
            this.FormIDLabel.Size = new System.Drawing.Size(35, 15);
            this.FormIDLabel.TabIndex = 212;
            this.FormIDLabel.Text = "9034";
            // 
            // FormLinePanel
            // 
            this.FormLinePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(68)))), ((int)(((byte)(108)))));
            this.FormLinePanel.Location = new System.Drawing.Point(0, 3);
            this.FormLinePanel.Name = "FormLinePanel";
            this.FormLinePanel.Size = new System.Drawing.Size(238, 2);
            this.FormLinePanel.TabIndex = 210;
            // 
            // ColumnChooserPanel
            // 
            this.ColumnChooserPanel.BackColor = System.Drawing.Color.Transparent;
            this.ColumnChooserPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColumnChooserPanel.Controls.Add(this.EngineFieldDataGrid);
            this.ColumnChooserPanel.Location = new System.Drawing.Point(14, 32);
            this.ColumnChooserPanel.Name = "ColumnChooserPanel";
            this.ColumnChooserPanel.Size = new System.Drawing.Size(238, 231);
            this.ColumnChooserPanel.TabIndex = 219;
            this.ColumnChooserPanel.TabStop = true;
            // 
            // EngineFieldDataGrid
            // 
            this.EngineFieldDataGrid.ColumnDisplayOrder = Infragistics.Win.UltraWinGrid.ColumnDisplayOrder.SameAsGrid;
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.EngineFieldDataGrid.DisplayLayout.Appearance = appearance1;
            this.EngineFieldDataGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            this.EngineFieldDataGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.EngineFieldDataGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.EngineFieldDataGrid.DisplayLayout.MaxColScrollRegions = 1;
            this.EngineFieldDataGrid.DisplayLayout.MaxRowScrollRegions = 1;
            this.EngineFieldDataGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.EngineFieldDataGrid.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.None;
            this.EngineFieldDataGrid.DisplayLayout.Override.AllowRowLayoutCellSizing = Infragistics.Win.UltraWinGrid.RowLayoutSizing.None;
            this.EngineFieldDataGrid.DisplayLayout.Override.AllowRowLayoutLabelSizing = Infragistics.Win.UltraWinGrid.RowLayoutSizing.None;
            this.EngineFieldDataGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.EngineFieldDataGrid.DisplayLayout.Override.CellPadding = 2;
            this.EngineFieldDataGrid.DisplayLayout.Override.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            this.EngineFieldDataGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            this.EngineFieldDataGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.EngineFieldDataGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed;
            this.EngineFieldDataGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.EngineFieldDataGrid.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.EngineFieldDataGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.EngineFieldDataGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None;
            this.EngineFieldDataGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;
            this.EngineFieldDataGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.EngineFieldDataGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.EngineFieldDataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EngineFieldDataGrid.Location = new System.Drawing.Point(-1, -1);
            this.EngineFieldDataGrid.Name = "EngineFieldDataGrid";
            this.EngineFieldDataGrid.Size = new System.Drawing.Size(238, 231);
            this.EngineFieldDataGrid.StyleLibraryName = "";
            this.EngineFieldDataGrid.StyleSetName = "";
            this.EngineFieldDataGrid.TabIndex = 213;
            this.EngineFieldDataGrid.Text = "ultraGridColumnChooser1";
            // 
            // QueryEngineFieldMgmtMenuStrip
            // 
            this.QueryEngineFieldMgmtMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpStripMenuItem});
            this.QueryEngineFieldMgmtMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.QueryEngineFieldMgmtMenuStrip.Name = "QueryEngineFieldMgmtMenuStrip";
            this.QueryEngineFieldMgmtMenuStrip.Size = new System.Drawing.Size(265, 24);
            this.QueryEngineFieldMgmtMenuStrip.TabIndex = 220;
            this.QueryEngineFieldMgmtMenuStrip.Text = "QueryEngineFieldMgmtMenuStrip";
            this.QueryEngineFieldMgmtMenuStrip.Visible = false;
            // 
            // HelpStripMenuItem
            // 
            this.HelpStripMenuItem.Name = "HelpStripMenuItem";
            this.HelpStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.HelpStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.HelpStripMenuItem.Text = "HelpStripMenuItem";
            this.HelpStripMenuItem.Click += new System.EventHandler(this.HelpStripMenuItem_Click);
            // 
            // F9034
            // 
            this.AcceptButton = this.F9034OKButton;
            this.AccessibleName = "Query Engine Field Management";
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.F9034CancelButton;
            this.ClientSize = new System.Drawing.Size(265, 371);
            this.Controls.Add(this.QueryEngineFieldMgmtMenuStrip);
            this.Controls.Add(this.ColumnChooserPanel);
            this.Controls.Add(this.F9034FooterPanel);
            this.Controls.Add(this.F9034CancelButton);
            this.Controls.Add(this.F9034OKButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DeleteColumn);
            this.Controls.Add(this.NewColumnButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F9034";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "9034";
            this.Text = "Query Engine Field Management";
            this.Load += new System.EventHandler(this.F9034_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F9034_FormClosing);
            this.F9034FooterPanel.ResumeLayout(false);
            this.F9034FooterPanel.PerformLayout();
            this.ColumnChooserPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EngineFieldDataGrid)).EndInit();
            this.QueryEngineFieldMgmtMenuStrip.ResumeLayout(false);
            this.QueryEngineFieldMgmtMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanButton NewColumnButton;
        private TerraScan.UI.Controls.TerraScanButton DeleteColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private TerraScan.UI.Controls.TerraScanButton F9034OKButton;
        private TerraScan.UI.Controls.TerraScanButton F9034CancelButton;
        private System.Windows.Forms.Panel F9034FooterPanel;
        private System.Windows.Forms.Label FormIDLabel;
        private System.Windows.Forms.Panel FormLinePanel;
        private System.Windows.Forms.LinkLabel HelpLink;
        private System.Windows.Forms.Panel ColumnChooserPanel;
        private Infragistics.Win.UltraWinGrid.UltraGridColumnChooser EngineFieldDataGrid;
        private System.Windows.Forms.MenuStrip QueryEngineFieldMgmtMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem HelpStripMenuItem;
    }
}