namespace D8000
{
    partial class F8042
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8042));
            this.EventTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TimeFooterPictureBox = new System.Windows.Forms.PictureBox();
            this.StartDatePanel = new System.Windows.Forms.Panel();
            this.StartDateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.StartDateLabel = new System.Windows.Forms.Label();
            this.EndDatePanel = new System.Windows.Forms.Panel();
            this.EndDateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.EndDateLabel = new System.Windows.Forms.Label();
            this.CountPanel = new System.Windows.Forms.Panel();
            this.CountTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CountLable = new System.Windows.Forms.Label();
            this.TotalHoursPanel = new System.Windows.Forms.Panel();
            this.TotalHoursTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.TotalHoursLabel = new System.Windows.Forms.Label();
            this.TotalCostPanel = new System.Windows.Forms.Panel();
            this.TotalCostLinkLabel = new TerraScan.UI.Controls.TerraScanLinkLabel();
            this.TotalCostLabel = new System.Windows.Forms.Label();
            this.EmptyPanel1 = new System.Windows.Forms.Panel();
            this.EmptyPanel2 = new System.Windows.Forms.Panel();
            this.TimeFooterToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TimeFooterPictureBox)).BeginInit();
            this.StartDatePanel.SuspendLayout();
            this.EndDatePanel.SuspendLayout();
            this.CountPanel.SuspendLayout();
            this.TotalHoursPanel.SuspendLayout();
            this.TotalCostPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // EventTextBox
            // 
            this.EventTextBox.AllowClick = true;
            this.EventTextBox.AllowNegativeSign = false;
            this.EventTextBox.ApplyCFGFormat = false;
            this.EventTextBox.ApplyCurrencyFormat = false;
            this.EventTextBox.ApplyFocusColor = true;
            this.EventTextBox.ApplyNegativeStandard = true;
            this.EventTextBox.ApplyParentFocusColor = true;
            this.EventTextBox.ApplyTimeFormat = false;
            this.EventTextBox.BackColor = System.Drawing.Color.White;
            this.EventTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EventTextBox.CFromatWihoutSymbol = false;
            this.EventTextBox.CheckForEmpty = false;
            this.EventTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EventTextBox.Digits = -1;
            this.EventTextBox.EmptyDecimalValue = false;
            this.EventTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.EventTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.EventTextBox.IsEditable = false;
            this.EventTextBox.IsQueryableFileld = true;
            this.EventTextBox.Location = new System.Drawing.Point(151, 245);
            this.EventTextBox.LockKeyPress = true;
            this.EventTextBox.MaxLength = 4;
            this.EventTextBox.Name = "EventTextBox";
            this.EventTextBox.PersistDefaultColor = false;
            this.EventTextBox.Precision = 2;
            this.EventTextBox.QueryingFileldName = "";
            this.EventTextBox.ReadOnly = true;
            this.EventTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.EventTextBox.Size = new System.Drawing.Size(297, 16);
            this.EventTextBox.SpecialCharacter = "%";
            this.EventTextBox.TabIndex = 2;
            this.EventTextBox.TabStop = false;
            this.EventTextBox.TextCustomFormat = "$#,##0.00";
            this.EventTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.EventTextBox.WholeInteger = false;
            // 
            // TimeFooterPictureBox
            // 
            this.TimeFooterPictureBox.BackColor = System.Drawing.Color.White;
            this.TimeFooterPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("TimeFooterPictureBox.Image")));
            this.TimeFooterPictureBox.Location = new System.Drawing.Point(762, 0);
            this.TimeFooterPictureBox.Name = "TimeFooterPictureBox";
            this.TimeFooterPictureBox.Size = new System.Drawing.Size(42, 37);
            this.TimeFooterPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TimeFooterPictureBox.TabIndex = 10;
            this.TimeFooterPictureBox.TabStop = false;
            this.TimeFooterPictureBox.MouseEnter += new System.EventHandler(this.TimeFooterPictureBox_MouseEnter);
            // 
            // StartDatePanel
            // 
            this.StartDatePanel.BackColor = System.Drawing.Color.White;
            this.StartDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartDatePanel.Controls.Add(this.StartDateTextBox);
            this.StartDatePanel.Controls.Add(this.StartDateLabel);
            this.StartDatePanel.Location = new System.Drawing.Point(149, 0);
            this.StartDatePanel.Name = "StartDatePanel";
            this.StartDatePanel.Size = new System.Drawing.Size(139, 37);
            this.StartDatePanel.TabIndex = 3;
            this.StartDatePanel.TabStop = true;
            // 
            // StartDateTextBox
            // 
            this.StartDateTextBox.AllowClick = true;
            this.StartDateTextBox.AllowNegativeSign = false;
            this.StartDateTextBox.ApplyCFGFormat = false;
            this.StartDateTextBox.ApplyCurrencyFormat = false;
            this.StartDateTextBox.ApplyFocusColor = true;
            this.StartDateTextBox.ApplyNegativeStandard = true;
            this.StartDateTextBox.ApplyParentFocusColor = true;
            this.StartDateTextBox.ApplyTimeFormat = false;
            this.StartDateTextBox.BackColor = System.Drawing.Color.White;
            this.StartDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StartDateTextBox.CFromatWihoutSymbol = false;
            this.StartDateTextBox.CheckForEmpty = false;
            this.StartDateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.StartDateTextBox.Digits = -1;
            this.StartDateTextBox.EmptyDecimalValue = false;
            this.StartDateTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.StartDateTextBox.IsEditable = true;
            this.StartDateTextBox.IsQueryableFileld = true;
            this.StartDateTextBox.Location = new System.Drawing.Point(23, 17);
            this.StartDateTextBox.LockKeyPress = true;
            this.StartDateTextBox.MaxLength = 10;
            this.StartDateTextBox.Name = "StartDateTextBox";
            this.StartDateTextBox.PersistDefaultColor = false;
            this.StartDateTextBox.Precision = 2;
            this.StartDateTextBox.QueryingFileldName = "";
            this.StartDateTextBox.ReadOnly = true;
            this.StartDateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.StartDateTextBox.Size = new System.Drawing.Size(110, 16);
            this.StartDateTextBox.SpecialCharacter = "%";
            this.StartDateTextBox.TabIndex = 4;
            this.StartDateTextBox.TabStop = false;
            this.StartDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.StartDateTextBox.TextCustomFormat = "";
            this.StartDateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.StartDateTextBox.WholeInteger = false;
            // 
            // StartDateLabel
            // 
            this.StartDateLabel.AutoSize = true;
            this.StartDateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.StartDateLabel.Location = new System.Drawing.Point(1, -1);
            this.StartDateLabel.Name = "StartDateLabel";
            this.StartDateLabel.Size = new System.Drawing.Size(63, 14);
            this.StartDateLabel.TabIndex = 1;
            this.StartDateLabel.Tag = "1105";
            this.StartDateLabel.Text = "Start Date:";
            // 
            // EndDatePanel
            // 
            this.EndDatePanel.BackColor = System.Drawing.Color.White;
            this.EndDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EndDatePanel.Controls.Add(this.EndDateTextBox);
            this.EndDatePanel.Controls.Add(this.EndDateLabel);
            this.EndDatePanel.Location = new System.Drawing.Point(287, 0);
            this.EndDatePanel.Name = "EndDatePanel";
            this.EndDatePanel.Size = new System.Drawing.Size(144, 37);
            this.EndDatePanel.TabIndex = 5;
            this.EndDatePanel.TabStop = true;
            // 
            // EndDateTextBox
            // 
            this.EndDateTextBox.AllowClick = true;
            this.EndDateTextBox.AllowNegativeSign = false;
            this.EndDateTextBox.ApplyCFGFormat = false;
            this.EndDateTextBox.ApplyCurrencyFormat = false;
            this.EndDateTextBox.ApplyFocusColor = true;
            this.EndDateTextBox.ApplyNegativeStandard = true;
            this.EndDateTextBox.ApplyParentFocusColor = true;
            this.EndDateTextBox.ApplyTimeFormat = false;
            this.EndDateTextBox.BackColor = System.Drawing.Color.White;
            this.EndDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EndDateTextBox.CFromatWihoutSymbol = false;
            this.EndDateTextBox.CheckForEmpty = false;
            this.EndDateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EndDateTextBox.Digits = -1;
            this.EndDateTextBox.EmptyDecimalValue = false;
            this.EndDateTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.EndDateTextBox.IsEditable = true;
            this.EndDateTextBox.IsQueryableFileld = true;
            this.EndDateTextBox.Location = new System.Drawing.Point(21, 17);
            this.EndDateTextBox.LockKeyPress = true;
            this.EndDateTextBox.MaxLength = 10;
            this.EndDateTextBox.Name = "EndDateTextBox";
            this.EndDateTextBox.PersistDefaultColor = false;
            this.EndDateTextBox.Precision = 2;
            this.EndDateTextBox.QueryingFileldName = "";
            this.EndDateTextBox.ReadOnly = true;
            this.EndDateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.EndDateTextBox.Size = new System.Drawing.Size(117, 16);
            this.EndDateTextBox.SpecialCharacter = "%";
            this.EndDateTextBox.TabIndex = 6;
            this.EndDateTextBox.TabStop = false;
            this.EndDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EndDateTextBox.TextCustomFormat = "";
            this.EndDateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.EndDateTextBox.WholeInteger = false;
            // 
            // EndDateLabel
            // 
            this.EndDateLabel.AutoSize = true;
            this.EndDateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.EndDateLabel.Location = new System.Drawing.Point(1, -1);
            this.EndDateLabel.Name = "EndDateLabel";
            this.EndDateLabel.Size = new System.Drawing.Size(57, 14);
            this.EndDateLabel.TabIndex = 1;
            this.EndDateLabel.Tag = "1105";
            this.EndDateLabel.Text = "End Date:";
            // 
            // CountPanel
            // 
            this.CountPanel.BackColor = System.Drawing.Color.White;
            this.CountPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CountPanel.Controls.Add(this.CountTextBox);
            this.CountPanel.Controls.Add(this.CountLable);
            this.CountPanel.Location = new System.Drawing.Point(19, 0);
            this.CountPanel.Name = "CountPanel";
            this.CountPanel.Size = new System.Drawing.Size(131, 37);
            this.CountPanel.TabIndex = 1;
            // 
            // CountTextBox
            // 
            this.CountTextBox.AllowClick = true;
            this.CountTextBox.AllowNegativeSign = false;
            this.CountTextBox.ApplyCFGFormat = false;
            this.CountTextBox.ApplyCurrencyFormat = false;
            this.CountTextBox.ApplyFocusColor = true;
            this.CountTextBox.ApplyNegativeStandard = true;
            this.CountTextBox.ApplyParentFocusColor = true;
            this.CountTextBox.ApplyTimeFormat = false;
            this.CountTextBox.BackColor = System.Drawing.Color.White;
            this.CountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CountTextBox.CFromatWihoutSymbol = false;
            this.CountTextBox.CheckForEmpty = false;
            this.CountTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CountTextBox.Digits = -1;
            this.CountTextBox.EmptyDecimalValue = false;
            this.CountTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.CountTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CountTextBox.IsEditable = false;
            this.CountTextBox.IsQueryableFileld = true;
            this.CountTextBox.Location = new System.Drawing.Point(37, 17);
            this.CountTextBox.LockKeyPress = true;
            this.CountTextBox.MaxLength = 4;
            this.CountTextBox.Name = "CountTextBox";
            this.CountTextBox.PersistDefaultColor = false;
            this.CountTextBox.Precision = 2;
            this.CountTextBox.QueryingFileldName = "";
            this.CountTextBox.ReadOnly = true;
            this.CountTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CountTextBox.Size = new System.Drawing.Size(87, 16);
            this.CountTextBox.SpecialCharacter = "%";
            this.CountTextBox.TabIndex = 2;
            this.CountTextBox.TabStop = false;
            this.CountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CountTextBox.TextCustomFormat = "$#,##0.00";
            this.CountTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CountTextBox.WholeInteger = false;
            // 
            // CountLable
            // 
            this.CountLable.AutoSize = true;
            this.CountLable.BackColor = System.Drawing.Color.Transparent;
            this.CountLable.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CountLable.Location = new System.Drawing.Point(1, -1);
            this.CountLable.Name = "CountLable";
            this.CountLable.Size = new System.Drawing.Size(43, 14);
            this.CountLable.TabIndex = 0;
            this.CountLable.Text = "Count:";
            // 
            // TotalHoursPanel
            // 
            this.TotalHoursPanel.BackColor = System.Drawing.Color.White;
            this.TotalHoursPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalHoursPanel.Controls.Add(this.TotalHoursTextBox);
            this.TotalHoursPanel.Controls.Add(this.TotalHoursLabel);
            this.TotalHoursPanel.Location = new System.Drawing.Point(430, 0);
            this.TotalHoursPanel.Name = "TotalHoursPanel";
            this.TotalHoursPanel.Size = new System.Drawing.Size(140, 37);
            this.TotalHoursPanel.TabIndex = 14;
            // 
            // TotalHoursTextBox
            // 
            this.TotalHoursTextBox.AllowClick = true;
            this.TotalHoursTextBox.AllowNegativeSign = false;
            this.TotalHoursTextBox.ApplyCFGFormat = false;
            this.TotalHoursTextBox.ApplyCurrencyFormat = false;
            this.TotalHoursTextBox.ApplyFocusColor = true;
            this.TotalHoursTextBox.ApplyNegativeStandard = true;
            this.TotalHoursTextBox.ApplyParentFocusColor = true;
            this.TotalHoursTextBox.ApplyTimeFormat = false;
            this.TotalHoursTextBox.BackColor = System.Drawing.Color.White;
            this.TotalHoursTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TotalHoursTextBox.CFromatWihoutSymbol = false;
            this.TotalHoursTextBox.CheckForEmpty = false;
            this.TotalHoursTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TotalHoursTextBox.Digits = -1;
            this.TotalHoursTextBox.EmptyDecimalValue = false;
            this.TotalHoursTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.TotalHoursTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TotalHoursTextBox.IsEditable = false;
            this.TotalHoursTextBox.IsQueryableFileld = true;
            this.TotalHoursTextBox.Location = new System.Drawing.Point(29, 17);
            this.TotalHoursTextBox.LockKeyPress = true;
            this.TotalHoursTextBox.MaxLength = 4;
            this.TotalHoursTextBox.Name = "TotalHoursTextBox";
            this.TotalHoursTextBox.PersistDefaultColor = false;
            this.TotalHoursTextBox.Precision = 2;
            this.TotalHoursTextBox.QueryingFileldName = "";
            this.TotalHoursTextBox.ReadOnly = true;
            this.TotalHoursTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.TotalHoursTextBox.Size = new System.Drawing.Size(106, 16);
            this.TotalHoursTextBox.SpecialCharacter = "%";
            this.TotalHoursTextBox.TabIndex = 8;
            this.TotalHoursTextBox.TabStop = false;
            this.TotalHoursTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TotalHoursTextBox.TextCustomFormat = "$#,##0.00";
            this.TotalHoursTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.TotalHoursTextBox.WholeInteger = false;
            // 
            // TotalHoursLabel
            // 
            this.TotalHoursLabel.AutoSize = true;
            this.TotalHoursLabel.BackColor = System.Drawing.Color.Transparent;
            this.TotalHoursLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalHoursLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TotalHoursLabel.Location = new System.Drawing.Point(1, -1);
            this.TotalHoursLabel.Name = "TotalHoursLabel";
            this.TotalHoursLabel.Size = new System.Drawing.Size(73, 14);
            this.TotalHoursLabel.TabIndex = 0;
            this.TotalHoursLabel.Text = "Total Hours:";
            // 
            // TotalCostPanel
            // 
            this.TotalCostPanel.BackColor = System.Drawing.Color.White;
            this.TotalCostPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TotalCostPanel.Controls.Add(this.TotalCostLinkLabel);
            this.TotalCostPanel.Controls.Add(this.TotalCostLabel);
            this.TotalCostPanel.Location = new System.Drawing.Point(569, 0);
            this.TotalCostPanel.Name = "TotalCostPanel";
            this.TotalCostPanel.Size = new System.Drawing.Size(183, 37);
            this.TotalCostPanel.TabIndex = 9;
            // 
            // TotalCostLinkLabel
            // 
            this.TotalCostLinkLabel.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Bold);
            this.TotalCostLinkLabel.FormDllName = null;
            this.TotalCostLinkLabel.FormId = 0;
            this.TotalCostLinkLabel.Location = new System.Drawing.Point(10, 15);
            this.TotalCostLinkLabel.MenuName = null;
            this.TotalCostLinkLabel.Name = "TotalCostLinkLabel";
            this.TotalCostLinkLabel.PermissionOpen = 0;
            this.TotalCostLinkLabel.Size = new System.Drawing.Size(165, 17);
            this.TotalCostLinkLabel.TabIndex = 10;
            this.TotalCostLinkLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.TotalCostLinkLabel.TextCustomFormat = "$ #,##0.00";
            this.TotalCostLinkLabel.ValidateType = TerraScan.UI.Controls.TerraScanLinkLabel.ControlValidationType.Decimal;
            this.TotalCostLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TotalCostLinkLabel_LinkClicked);
            // 
            // TotalCostLabel
            // 
            this.TotalCostLabel.AutoSize = true;
            this.TotalCostLabel.BackColor = System.Drawing.Color.Transparent;
            this.TotalCostLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalCostLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.TotalCostLabel.Location = new System.Drawing.Point(1, -1);
            this.TotalCostLabel.Name = "TotalCostLabel";
            this.TotalCostLabel.Size = new System.Drawing.Size(66, 14);
            this.TotalCostLabel.TabIndex = 0;
            this.TotalCostLabel.Text = "Total Cost:";
            // 
            // EmptyPanel1
            // 
            this.EmptyPanel1.BackColor = System.Drawing.Color.Silver;
            this.EmptyPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmptyPanel1.Location = new System.Drawing.Point(0, 0);
            this.EmptyPanel1.Name = "EmptyPanel1";
            this.EmptyPanel1.Size = new System.Drawing.Size(20, 37);
            this.EmptyPanel1.TabIndex = 15;
            // 
            // EmptyPanel2
            // 
            this.EmptyPanel2.BackColor = System.Drawing.Color.Silver;
            this.EmptyPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmptyPanel2.Location = new System.Drawing.Point(748, 0);
            this.EmptyPanel2.Name = "EmptyPanel2";
            this.EmptyPanel2.Size = new System.Drawing.Size(20, 37);
            this.EmptyPanel2.TabIndex = 16;
            // 
            // F8042
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.EmptyPanel2);
            this.Controls.Add(this.EmptyPanel1);
            this.Controls.Add(this.TotalCostPanel);
            this.Controls.Add(this.TotalHoursPanel);
            this.Controls.Add(this.CountPanel);
            this.Controls.Add(this.EndDatePanel);
            this.Controls.Add(this.StartDatePanel);
            this.Controls.Add(this.EventTextBox);
            this.Controls.Add(this.TimeFooterPictureBox);
            this.Name = "F8042";
            this.Size = new System.Drawing.Size(804, 37);
            this.Tag = "8042";
            this.Load += new System.EventHandler(this.F8042_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TimeFooterPictureBox)).EndInit();
            this.StartDatePanel.ResumeLayout(false);
            this.StartDatePanel.PerformLayout();
            this.EndDatePanel.ResumeLayout(false);
            this.EndDatePanel.PerformLayout();
            this.CountPanel.ResumeLayout(false);
            this.CountPanel.PerformLayout();
            this.TotalHoursPanel.ResumeLayout(false);
            this.TotalHoursPanel.PerformLayout();
            this.TotalCostPanel.ResumeLayout(false);
            this.TotalCostPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanTextBox EventTextBox;
        private System.Windows.Forms.PictureBox TimeFooterPictureBox;
        private System.Windows.Forms.Panel StartDatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox StartDateTextBox;
        private System.Windows.Forms.Label StartDateLabel;
        private System.Windows.Forms.Panel EndDatePanel;
        private TerraScan.UI.Controls.TerraScanTextBox EndDateTextBox;
        private System.Windows.Forms.Label EndDateLabel;
        private System.Windows.Forms.Panel CountPanel;
        private TerraScan.UI.Controls.TerraScanTextBox CountTextBox;
        private System.Windows.Forms.Label CountLable;
        private System.Windows.Forms.Panel TotalHoursPanel;
        private TerraScan.UI.Controls.TerraScanTextBox TotalHoursTextBox;
        private System.Windows.Forms.Label TotalHoursLabel;
        private System.Windows.Forms.Panel TotalCostPanel;
        private System.Windows.Forms.Label TotalCostLabel;
        private TerraScan.UI.Controls.TerraScanLinkLabel TotalCostLinkLabel;
        private System.Windows.Forms.Panel EmptyPanel1;
        private System.Windows.Forms.Panel EmptyPanel2;
        private System.Windows.Forms.ToolTip TimeFooterToolTip;

    }
}
