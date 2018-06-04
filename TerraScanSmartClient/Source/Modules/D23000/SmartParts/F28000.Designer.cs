//--------------------------------------------------------------------------------------------
// <copyright file="F23000Designer.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F28000 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26/04/2011       D.LathaMaheswari   Created
//***********************************************************************************************/
namespace D23000
{
    partial class F28000
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DiscretionaryPanel = new System.Windows.Forms.Panel();
            this.FooterPanel4 = new System.Windows.Forms.Panel();
            this.FooterPanel3 = new System.Windows.Forms.Panel();
            this.ExemptionSumLabel = new System.Windows.Forms.Label();
            this.FooterPanel2 = new System.Windows.Forms.Panel();
            this.FooterPanel1 = new System.Windows.Forms.Panel();
            this.ExemptionPanel = new System.Windows.Forms.Panel();
            this.ExemptionVscrollBar = new System.Windows.Forms.VScrollBar();
            this.ExemptionGridView = new TerraScan.UI.Controls.TerraScanDataGridView();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.NonAgriculturePanel = new System.Windows.Forms.Panel();
            this.NonAgricultureTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.NonAgricultureLabel = new System.Windows.Forms.Label();
            this.AgriculturePanel = new System.Windows.Forms.Panel();
            this.AgricultureTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.AgricultureLabel = new System.Windows.Forms.Label();
            this.RollYearPanel = new System.Windows.Forms.Panel();
            this.RollYearTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.RollYearLabel = new System.Windows.Forms.Label();
            this.ExemptionPictureBox = new System.Windows.Forms.PictureBox();
            this.ExemptionTotalTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.DiscretionaryToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.DetailID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExemptionYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Class = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubjectAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExemptionAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscretionaryPanel.SuspendLayout();
            this.FooterPanel3.SuspendLayout();
            this.ExemptionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExemptionGridView)).BeginInit();
            this.HeaderPanel.SuspendLayout();
            this.NonAgriculturePanel.SuspendLayout();
            this.AgriculturePanel.SuspendLayout();
            this.RollYearPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExemptionPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DiscretionaryPanel
            // 
            this.DiscretionaryPanel.BackColor = System.Drawing.Color.White;
            this.DiscretionaryPanel.Controls.Add(this.FooterPanel4);
            this.DiscretionaryPanel.Controls.Add(this.FooterPanel3);
            this.DiscretionaryPanel.Controls.Add(this.FooterPanel2);
            this.DiscretionaryPanel.Controls.Add(this.FooterPanel1);
            this.DiscretionaryPanel.Controls.Add(this.ExemptionPanel);
            this.DiscretionaryPanel.Controls.Add(this.HeaderPanel);
            this.DiscretionaryPanel.Location = new System.Drawing.Point(0, 0);
            this.DiscretionaryPanel.Name = "DiscretionaryPanel";
            this.DiscretionaryPanel.Size = new System.Drawing.Size(771, 373);
            this.DiscretionaryPanel.TabIndex = 121;
            this.DiscretionaryPanel.TabStop = true;
            // 
            // FooterPanel4
            // 
            this.FooterPanel4.BackColor = System.Drawing.Color.Gray;
            this.FooterPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FooterPanel4.Location = new System.Drawing.Point(753, 349);
            this.FooterPanel4.Name = "FooterPanel4";
            this.FooterPanel4.Size = new System.Drawing.Size(19, 24);
            this.FooterPanel4.TabIndex = 10;
            this.FooterPanel4.TabStop = true;
            // 
            // FooterPanel3
            // 
            this.FooterPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(203)))), ((int)(((byte)(133)))));
            this.FooterPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FooterPanel3.Controls.Add(this.ExemptionSumLabel);
            this.FooterPanel3.Location = new System.Drawing.Point(612, 349);
            this.FooterPanel3.Name = "FooterPanel3";
            this.FooterPanel3.Size = new System.Drawing.Size(144, 24);
            this.FooterPanel3.TabIndex = 9;
            this.FooterPanel3.TabStop = true;
            // 
            // ExemptionSumLabel
            // 
            this.ExemptionSumLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.ExemptionSumLabel.ForeColor = System.Drawing.Color.Black;
            this.ExemptionSumLabel.Location = new System.Drawing.Point(7, 3);
            this.ExemptionSumLabel.Name = "ExemptionSumLabel";
            this.ExemptionSumLabel.Size = new System.Drawing.Size(130, 16);
            this.ExemptionSumLabel.TabIndex = 21;
            this.ExemptionSumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FooterPanel2
            // 
            this.FooterPanel2.BackColor = System.Drawing.Color.Gray;
            this.FooterPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FooterPanel2.Location = new System.Drawing.Point(19, 349);
            this.FooterPanel2.Name = "FooterPanel2";
            this.FooterPanel2.Size = new System.Drawing.Size(594, 24);
            this.FooterPanel2.TabIndex = 9;
            this.FooterPanel2.TabStop = true;
            // 
            // FooterPanel1
            // 
            this.FooterPanel1.BackColor = System.Drawing.Color.Gray;
            this.FooterPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FooterPanel1.Location = new System.Drawing.Point(0, 349);
            this.FooterPanel1.Name = "FooterPanel1";
            this.FooterPanel1.Size = new System.Drawing.Size(20, 24);
            this.FooterPanel1.TabIndex = 8;
            this.FooterPanel1.TabStop = true;
            // 
            // ExemptionPanel
            // 
            this.ExemptionPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ExemptionPanel.BackColor = System.Drawing.Color.Silver;
            this.ExemptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExemptionPanel.Controls.Add(this.ExemptionVscrollBar);
            this.ExemptionPanel.Controls.Add(this.ExemptionGridView);
            this.ExemptionPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ExemptionPanel.Location = new System.Drawing.Point(0, 40);
            this.ExemptionPanel.Name = "ExemptionPanel";
            this.ExemptionPanel.Size = new System.Drawing.Size(771, 310);
            this.ExemptionPanel.TabIndex = 7;
            // 
            // ExemptionVscrollBar
            // 
            this.ExemptionVscrollBar.Enabled = false;
            this.ExemptionVscrollBar.Location = new System.Drawing.Point(753, 0);
            this.ExemptionVscrollBar.Name = "ExemptionVscrollBar";
            this.ExemptionVscrollBar.Size = new System.Drawing.Size(16, 308);
            this.ExemptionVscrollBar.TabIndex = 1042;
            // 
            // ExemptionGridView
            // 
            this.ExemptionGridView.AllowCellClick = true;
            this.ExemptionGridView.AllowDoubleClick = false;
            this.ExemptionGridView.AllowEmptyRows = true;
            this.ExemptionGridView.AllowEnterKey = false;
            this.ExemptionGridView.AllowSorting = false;
            this.ExemptionGridView.AllowUserToAddRows = false;
            this.ExemptionGridView.AllowUserToDeleteRows = false;
            this.ExemptionGridView.AllowUserToResizeColumns = false;
            this.ExemptionGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ExemptionGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ExemptionGridView.ApplyStandardBehaviour = false;
            this.ExemptionGridView.BackgroundColor = System.Drawing.Color.White;
            this.ExemptionGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ExemptionGridView.ClearCurrentCellOnLeave = true;
            this.ExemptionGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.ExemptionGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ExemptionGridView.ColumnHeadersHeight = 24;
            this.ExemptionGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ExemptionGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DetailID,
            this.ExemptionYear,
            this.State,
            this.Class,
            this.SubjectAmount,
            this.ExemptionAmount});
            this.ExemptionGridView.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ExemptionGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.ExemptionGridView.DefaultRowIndex = 0;
            this.ExemptionGridView.DeselectCurrentCell = false;
            this.ExemptionGridView.DeselectSpecifiedRow = -1;
            this.ExemptionGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ExemptionGridView.EnableBinding = true;
            this.ExemptionGridView.EnableHeadersVisualStyles = false;
            this.ExemptionGridView.GridColor = System.Drawing.Color.Black;
            this.ExemptionGridView.GridContentSelected = false;
            this.ExemptionGridView.IsEditableGrid = true;
            this.ExemptionGridView.IsMultiSelect = true;
            this.ExemptionGridView.IsSorted = false;
            this.ExemptionGridView.Location = new System.Drawing.Point(-1, -1);
            this.ExemptionGridView.Name = "ExemptionGridView";
            this.ExemptionGridView.NumRowsVisible = 13;
            this.ExemptionGridView.PrimaryKeyColumnName = "";
            this.ExemptionGridView.RemainSortFields = false;
            this.ExemptionGridView.RemoveDefaultSelection = true;
            this.ExemptionGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ExemptionGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.ExemptionGridView.RowHeadersWidth = 20;
            this.ExemptionGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.ExemptionGridView.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.ExemptionGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ExemptionGridView.Size = new System.Drawing.Size(771, 310);
            this.ExemptionGridView.TabIndex = 0;
            this.ExemptionGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExemptionGridView_RowEnter);
            this.ExemptionGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.ExemptionGridView_CellParsing);
            this.ExemptionGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.ExemptionGridView_CellFormatting);
            this.ExemptionGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ExemptionGridView_MouseUp);
            this.ExemptionGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExemptionGridView_CellEndEdit);
            this.ExemptionGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.ExemptionGridView_EditingControlShowing);
            this.ExemptionGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExemptionGridView_KeyDown);
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.Color.Transparent;
            this.HeaderPanel.Controls.Add(this.NonAgriculturePanel);
            this.HeaderPanel.Controls.Add(this.AgriculturePanel);
            this.HeaderPanel.Controls.Add(this.RollYearPanel);
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(771, 41);
            this.HeaderPanel.TabIndex = 1;
            // 
            // NonAgriculturePanel
            // 
            this.NonAgriculturePanel.BackColor = System.Drawing.Color.Transparent;
            this.NonAgriculturePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NonAgriculturePanel.Controls.Add(this.NonAgricultureTextBox);
            this.NonAgriculturePanel.Controls.Add(this.NonAgricultureLabel);
            this.NonAgriculturePanel.Location = new System.Drawing.Point(455, 0);
            this.NonAgriculturePanel.Name = "NonAgriculturePanel";
            this.NonAgriculturePanel.Size = new System.Drawing.Size(316, 41);
            this.NonAgriculturePanel.TabIndex = 4;
            this.NonAgriculturePanel.TabStop = true;
            // 
            // NonAgricultureTextBox
            // 
            this.NonAgricultureTextBox.AllowClick = true;
            this.NonAgricultureTextBox.AllowNegativeSign = false;
            this.NonAgricultureTextBox.ApplyCFGFormat = true;
            this.NonAgricultureTextBox.ApplyCurrencyFormat = true;
            this.NonAgricultureTextBox.ApplyFocusColor = true;
            this.NonAgricultureTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.NonAgricultureTextBox.ApplyNegativeStandard = true;
            this.NonAgricultureTextBox.ApplyParentFocusColor = true;
            this.NonAgricultureTextBox.ApplyTimeFormat = false;
            this.NonAgricultureTextBox.BackColor = System.Drawing.Color.White;
            this.NonAgricultureTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NonAgricultureTextBox.CFromatWihoutSymbol = false;
            this.NonAgricultureTextBox.CheckForEmpty = false;
            this.NonAgricultureTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NonAgricultureTextBox.Digits = -1;
            this.NonAgricultureTextBox.EmptyDecimalValue = false;
            this.NonAgricultureTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.NonAgricultureTextBox.ForeColor = System.Drawing.Color.Gray;
            this.NonAgricultureTextBox.IsEditable = false;
            this.NonAgricultureTextBox.IsQueryableFileld = true;
            this.NonAgricultureTextBox.Location = new System.Drawing.Point(4, 16);
            this.NonAgricultureTextBox.LockKeyPress = true;
            this.NonAgricultureTextBox.MaxLength = 4;
            this.NonAgricultureTextBox.Name = "NonAgricultureTextBox";
            this.NonAgricultureTextBox.PersistDefaultColor = false;
            this.NonAgricultureTextBox.Precision = 2;
            this.NonAgricultureTextBox.QueryingFileldName = "";
            this.NonAgricultureTextBox.ReadOnly = true;
            this.NonAgricultureTextBox.SetColorFlag = false;
            this.NonAgricultureTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.NonAgricultureTextBox.Size = new System.Drawing.Size(295, 16);
            this.NonAgricultureTextBox.SpecialCharacter = "%";
            this.NonAgricultureTextBox.TabIndex = 0;
            this.NonAgricultureTextBox.TabStop = false;
            this.NonAgricultureTextBox.Tag = "";
            this.NonAgricultureTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NonAgricultureTextBox.TextCustomFormat = "#,##0";
            this.NonAgricultureTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.NonAgricultureTextBox.WholeInteger = false;
            // 
            // NonAgricultureLabel
            // 
            this.NonAgricultureLabel.AutoSize = true;
            this.NonAgricultureLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.NonAgricultureLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.NonAgricultureLabel.Location = new System.Drawing.Point(0, 0);
            this.NonAgricultureLabel.Name = "NonAgricultureLabel";
            this.NonAgricultureLabel.Size = new System.Drawing.Size(237, 14);
            this.NonAgricultureLabel.TabIndex = 20;
            this.NonAgricultureLabel.Text = "Prior Non-Agricultural Exemption Amount:";
            // 
            // AgriculturePanel
            // 
            this.AgriculturePanel.BackColor = System.Drawing.Color.Transparent;
            this.AgriculturePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AgriculturePanel.Controls.Add(this.AgricultureTextBox);
            this.AgriculturePanel.Controls.Add(this.AgricultureLabel);
            this.AgriculturePanel.Location = new System.Drawing.Point(141, 0);
            this.AgriculturePanel.Name = "AgriculturePanel";
            this.AgriculturePanel.Size = new System.Drawing.Size(315, 41);
            this.AgriculturePanel.TabIndex = 3;
            this.AgriculturePanel.TabStop = true;
            // 
            // AgricultureTextBox
            // 
            this.AgricultureTextBox.AllowClick = true;
            this.AgricultureTextBox.AllowNegativeSign = false;
            this.AgricultureTextBox.ApplyCFGFormat = true;
            this.AgricultureTextBox.ApplyCurrencyFormat = true;
            this.AgricultureTextBox.ApplyFocusColor = true;
            this.AgricultureTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.AgricultureTextBox.ApplyNegativeStandard = true;
            this.AgricultureTextBox.ApplyParentFocusColor = true;
            this.AgricultureTextBox.ApplyTimeFormat = false;
            this.AgricultureTextBox.BackColor = System.Drawing.Color.White;
            this.AgricultureTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AgricultureTextBox.CFromatWihoutSymbol = false;
            this.AgricultureTextBox.CheckForEmpty = false;
            this.AgricultureTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AgricultureTextBox.Digits = -1;
            this.AgricultureTextBox.EmptyDecimalValue = false;
            this.AgricultureTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.AgricultureTextBox.ForeColor = System.Drawing.Color.Gray;
            this.AgricultureTextBox.IsEditable = false;
            this.AgricultureTextBox.IsQueryableFileld = true;
            this.AgricultureTextBox.Location = new System.Drawing.Point(4, 16);
            this.AgricultureTextBox.LockKeyPress = true;
            this.AgricultureTextBox.MaxLength = 4;
            this.AgricultureTextBox.Name = "AgricultureTextBox";
            this.AgricultureTextBox.PersistDefaultColor = false;
            this.AgricultureTextBox.Precision = 2;
            this.AgricultureTextBox.QueryingFileldName = "";
            this.AgricultureTextBox.ReadOnly = true;
            this.AgricultureTextBox.SetColorFlag = false;
            this.AgricultureTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.AgricultureTextBox.Size = new System.Drawing.Size(295, 16);
            this.AgricultureTextBox.SpecialCharacter = "%";
            this.AgricultureTextBox.TabIndex = 0;
            this.AgricultureTextBox.TabStop = false;
            this.AgricultureTextBox.Tag = "";
            this.AgricultureTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AgricultureTextBox.TextCustomFormat = "#,##0";
            this.AgricultureTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.AgricultureTextBox.WholeInteger = false;
            // 
            // AgricultureLabel
            // 
            this.AgricultureLabel.AutoSize = true;
            this.AgricultureLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.AgricultureLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.AgricultureLabel.Location = new System.Drawing.Point(0, 0);
            this.AgricultureLabel.Name = "AgricultureLabel";
            this.AgricultureLabel.Size = new System.Drawing.Size(212, 14);
            this.AgricultureLabel.TabIndex = 20;
            this.AgricultureLabel.Text = "Prior Agricultural Exemption Amount:";
            // 
            // RollYearPanel
            // 
            this.RollYearPanel.BackColor = System.Drawing.Color.Transparent;
            this.RollYearPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RollYearPanel.Controls.Add(this.RollYearTextBox);
            this.RollYearPanel.Controls.Add(this.RollYearLabel);
            this.RollYearPanel.Location = new System.Drawing.Point(0, 0);
            this.RollYearPanel.Name = "RollYearPanel";
            this.RollYearPanel.Size = new System.Drawing.Size(142, 41);
            this.RollYearPanel.TabIndex = 2;
            this.RollYearPanel.TabStop = true;
            // 
            // RollYearTextBox
            // 
            this.RollYearTextBox.AllowClick = true;
            this.RollYearTextBox.AllowNegativeSign = false;
            this.RollYearTextBox.ApplyCFGFormat = false;
            this.RollYearTextBox.ApplyCurrencyFormat = false;
            this.RollYearTextBox.ApplyFocusColor = true;
            this.RollYearTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.RollYearTextBox.ApplyNegativeStandard = true;
            this.RollYearTextBox.ApplyParentFocusColor = true;
            this.RollYearTextBox.ApplyTimeFormat = false;
            this.RollYearTextBox.BackColor = System.Drawing.Color.White;
            this.RollYearTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RollYearTextBox.CFromatWihoutSymbol = false;
            this.RollYearTextBox.CheckForEmpty = false;
            this.RollYearTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RollYearTextBox.Digits = -1;
            this.RollYearTextBox.EmptyDecimalValue = false;
            this.RollYearTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.RollYearTextBox.ForeColor = System.Drawing.Color.Gray;
            this.RollYearTextBox.IsEditable = false;
            this.RollYearTextBox.IsQueryableFileld = true;
            this.RollYearTextBox.Location = new System.Drawing.Point(10, 16);
            this.RollYearTextBox.LockKeyPress = true;
            this.RollYearTextBox.MaxLength = 4;
            this.RollYearTextBox.Name = "RollYearTextBox";
            this.RollYearTextBox.PersistDefaultColor = false;
            this.RollYearTextBox.Precision = 2;
            this.RollYearTextBox.QueryingFileldName = "";
            this.RollYearTextBox.ReadOnly = true;
            this.RollYearTextBox.SetColorFlag = false;
            this.RollYearTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.RollYearTextBox.Size = new System.Drawing.Size(120, 16);
            this.RollYearTextBox.SpecialCharacter = "%";
            this.RollYearTextBox.TabIndex = 0;
            this.RollYearTextBox.TabStop = false;
            this.RollYearTextBox.Tag = "";
            this.RollYearTextBox.TextCustomFormat = "$#,##0.00";
            this.RollYearTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Year;
            this.RollYearTextBox.WholeInteger = false;
            // 
            // RollYearLabel
            // 
            this.RollYearLabel.AutoSize = true;
            this.RollYearLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.RollYearLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.RollYearLabel.Location = new System.Drawing.Point(0, 0);
            this.RollYearLabel.Name = "RollYearLabel";
            this.RollYearLabel.Size = new System.Drawing.Size(57, 14);
            this.RollYearLabel.TabIndex = 20;
            this.RollYearLabel.Text = "Roll Year:";
            // 
            // ExemptionPictureBox
            // 
            this.ExemptionPictureBox.Location = new System.Drawing.Point(762, 0);
            this.ExemptionPictureBox.Name = "ExemptionPictureBox";
            this.ExemptionPictureBox.Size = new System.Drawing.Size(42, 373);
            this.ExemptionPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ExemptionPictureBox.TabIndex = 122;
            this.ExemptionPictureBox.TabStop = false;
            this.ExemptionPictureBox.MouseEnter += new System.EventHandler(this.ExemptionPictureBox_MouseEnter);
            // 
            // ExemptionTotalTextBox
            // 
            this.ExemptionTotalTextBox.AllowClick = true;
            this.ExemptionTotalTextBox.AllowNegativeSign = false;
            this.ExemptionTotalTextBox.ApplyCFGFormat = true;
            this.ExemptionTotalTextBox.ApplyCurrencyFormat = true;
            this.ExemptionTotalTextBox.ApplyFocusColor = true;
            this.ExemptionTotalTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.ExemptionTotalTextBox.ApplyNegativeStandard = true;
            this.ExemptionTotalTextBox.ApplyParentFocusColor = true;
            this.ExemptionTotalTextBox.ApplyTimeFormat = false;
            this.ExemptionTotalTextBox.BackColor = System.Drawing.Color.White;
            this.ExemptionTotalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ExemptionTotalTextBox.CFromatWihoutSymbol = false;
            this.ExemptionTotalTextBox.CheckForEmpty = false;
            this.ExemptionTotalTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ExemptionTotalTextBox.Digits = -1;
            this.ExemptionTotalTextBox.EmptyDecimalValue = false;
            this.ExemptionTotalTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.ExemptionTotalTextBox.IsEditable = false;
            this.ExemptionTotalTextBox.IsQueryableFileld = true;
            this.ExemptionTotalTextBox.Location = new System.Drawing.Point(529, 353);
            this.ExemptionTotalTextBox.LockKeyPress = true;
            this.ExemptionTotalTextBox.MaxLength = 11;
            this.ExemptionTotalTextBox.Name = "ExemptionTotalTextBox";
            this.ExemptionTotalTextBox.PersistDefaultColor = false;
            this.ExemptionTotalTextBox.Precision = 2;
            this.ExemptionTotalTextBox.QueryingFileldName = "";
            this.ExemptionTotalTextBox.ReadOnly = true;
            this.ExemptionTotalTextBox.SetColorFlag = false;
            this.ExemptionTotalTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.ExemptionTotalTextBox.Size = new System.Drawing.Size(67, 16);
            this.ExemptionTotalTextBox.SpecialCharacter = "%";
            this.ExemptionTotalTextBox.TabIndex = 183;
            this.ExemptionTotalTextBox.TabStop = false;
            this.ExemptionTotalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ExemptionTotalTextBox.TextCustomFormat = "#,##0";
            this.ExemptionTotalTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Integer;
            this.ExemptionTotalTextBox.Visible = false;
            this.ExemptionTotalTextBox.WholeInteger = false;
            // 
            // DetailID
            // 
            this.DetailID.HeaderText = "DetailID";
            this.DetailID.Name = "DetailID";
            this.DetailID.Visible = false;
            // 
            // ExemptionYear
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.ExemptionYear.DefaultCellStyle = dataGridViewCellStyle3;
            this.ExemptionYear.HeaderText = "Exemption Year";
            this.ExemptionYear.MaxInputLength = 4;
            this.ExemptionYear.Name = "ExemptionYear";
            this.ExemptionYear.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ExemptionYear.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // State
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.State.DefaultCellStyle = dataGridViewCellStyle4;
            this.State.DisplayStyleForCurrentCellOnly = true;
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.State.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.State.Width = 200;
            // 
            // Class
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.Class.DefaultCellStyle = dataGridViewCellStyle5;
            this.Class.HeaderText = "Class";
            this.Class.Name = "Class";
            this.Class.ReadOnly = true;
            this.Class.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Class.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Class.Width = 150;
            // 
            // SubjectAmount
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.SubjectAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.SubjectAmount.HeaderText = "Subject Amount";
            this.SubjectAmount.MaxInputLength = 16;
            this.SubjectAmount.Name = "SubjectAmount";
            this.SubjectAmount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SubjectAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.SubjectAmount.Width = 142;
            // 
            // ExemptionAmount
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            this.ExemptionAmount.DefaultCellStyle = dataGridViewCellStyle7;
            this.ExemptionAmount.HeaderText = "Exemption Amount";
            this.ExemptionAmount.Name = "ExemptionAmount";
            this.ExemptionAmount.ReadOnly = true;
            this.ExemptionAmount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ExemptionAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ExemptionAmount.Width = 142;
            // 
            // F28000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DiscretionaryPanel);
            this.Controls.Add(this.ExemptionPictureBox);
            this.Controls.Add(this.ExemptionTotalTextBox);
            this.Name = "F28000";
            this.Size = new System.Drawing.Size(804, 373);
            this.Tag = "28000";
            this.Load += new System.EventHandler(this.F28000_Load);
            this.DiscretionaryPanel.ResumeLayout(false);
            this.FooterPanel3.ResumeLayout(false);
            this.ExemptionPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ExemptionGridView)).EndInit();
            this.HeaderPanel.ResumeLayout(false);
            this.NonAgriculturePanel.ResumeLayout(false);
            this.NonAgriculturePanel.PerformLayout();
            this.AgriculturePanel.ResumeLayout(false);
            this.AgriculturePanel.PerformLayout();
            this.RollYearPanel.ResumeLayout(false);
            this.RollYearPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExemptionPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DiscretionaryPanel;
        private System.Windows.Forms.Panel ExemptionPanel;
        private System.Windows.Forms.VScrollBar ExemptionVscrollBar;
        private TerraScan.UI.Controls.TerraScanDataGridView ExemptionGridView;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.PictureBox ExemptionPictureBox;
        private System.Windows.Forms.Panel NonAgriculturePanel;
        private TerraScan.UI.Controls.TerraScanTextBox NonAgricultureTextBox;
        private System.Windows.Forms.Label NonAgricultureLabel;
        private System.Windows.Forms.Panel AgriculturePanel;
        private TerraScan.UI.Controls.TerraScanTextBox AgricultureTextBox;
        private System.Windows.Forms.Label AgricultureLabel;
        private System.Windows.Forms.Panel RollYearPanel;
        private TerraScan.UI.Controls.TerraScanTextBox RollYearTextBox;
        private System.Windows.Forms.Label RollYearLabel;
        private System.Windows.Forms.Panel FooterPanel1;
        private System.Windows.Forms.Panel FooterPanel2;
        private System.Windows.Forms.Panel FooterPanel3;
        private System.Windows.Forms.Panel FooterPanel4;
        private System.Windows.Forms.Label ExemptionSumLabel;
        private TerraScan.UI.Controls.TerraScanTextBox ExemptionTotalTextBox;
        private System.Windows.Forms.ToolTip DiscretionaryToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExemptionYear;
        private System.Windows.Forms.DataGridViewComboBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Class;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubjectAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExemptionAmount;
    }
}
