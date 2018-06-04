namespace D8900
{
	partial class F8902
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8902));
            this.WorkOrderPanel = new System.Windows.Forms.Panel();
            this.WorkIdTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.WorkOrderLabel = new System.Windows.Forms.Label();
            this.WorkOrderIDPanel = new System.Windows.Forms.Panel();
            this.WorkOrderTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.WorkOrderIdLabel = new System.Windows.Forms.Label();
            this.EventDatePanel = new System.Windows.Forms.Panel();
            this.WorkOrderHeaderDatePict = new System.Windows.Forms.Button();
            this.WorkOrderTimePicker = new System.Windows.Forms.DateTimePicker();
            this.WorkOrderDate = new TerraScan.UI.Controls.TerraScanTextBox();
            this.WorkOrderDateLabel = new System.Windows.Forms.Label();
            this.ClosedPanel = new System.Windows.Forms.Panel();
            this.ClosedCheckBox = new TerraScan.UI.Controls.TerraScanCheckBox();
            this.ClosedLabel = new System.Windows.Forms.Label();
            this.WorkOrderPictureBox = new System.Windows.Forms.PictureBox();
            this.WorkOrderHeaderToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.WorkOrderPanel.SuspendLayout();
            this.WorkOrderIDPanel.SuspendLayout();
            this.EventDatePanel.SuspendLayout();
            this.ClosedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorkOrderPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // WorkOrderPanel
            // 
            this.WorkOrderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WorkOrderPanel.Controls.Add(this.WorkIdTextBox);
            this.WorkOrderPanel.Controls.Add(this.WorkOrderLabel);
            this.WorkOrderPanel.Location = new System.Drawing.Point(0, 0);
            this.WorkOrderPanel.Name = "WorkOrderPanel";
            this.WorkOrderPanel.Size = new System.Drawing.Size(427, 39);
            this.WorkOrderPanel.TabIndex = 0;
            // 
            // WorkIdTextBox
            // 
            this.WorkIdTextBox.AcceptsReturn = true;
            this.WorkIdTextBox.AllowClick = true;
            this.WorkIdTextBox.AllowNegativeSign = false;
            this.WorkIdTextBox.ApplyCFGFormat = false;
            this.WorkIdTextBox.ApplyCurrencyFormat = false;
            this.WorkIdTextBox.ApplyFocusColor = true;
            this.WorkIdTextBox.ApplyNegativeStandard = true;
            this.WorkIdTextBox.ApplyParentFocusColor = true;
            this.WorkIdTextBox.ApplyTimeFormat = false;
            this.WorkIdTextBox.BackColor = System.Drawing.Color.White;
            this.WorkIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.WorkIdTextBox.CFromatWihoutSymbol = false;
            this.WorkIdTextBox.CheckForEmpty = false;
            this.WorkIdTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WorkIdTextBox.Digits = -1;
            this.WorkIdTextBox.EmptyDecimalValue = false;
            this.WorkIdTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.WorkIdTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.WorkIdTextBox.IsEditable = false;
            this.WorkIdTextBox.IsQueryableFileld = true;
            this.WorkIdTextBox.Location = new System.Drawing.Point(11, 19);
            this.WorkIdTextBox.LockKeyPress = true;
            this.WorkIdTextBox.MaxLength = 200;
            this.WorkIdTextBox.Name = "WorkIdTextBox";
            this.WorkIdTextBox.PersistDefaultColor = false;
            this.WorkIdTextBox.Precision = 2;
            this.WorkIdTextBox.QueryingFileldName = "";
            this.WorkIdTextBox.ReadOnly = true;
            this.WorkIdTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.WorkIdTextBox.Size = new System.Drawing.Size(408, 16);
            this.WorkIdTextBox.SpecialCharacter = "%";
            this.WorkIdTextBox.TabIndex = 0;
            this.WorkIdTextBox.TabStop = false;
            this.WorkIdTextBox.TextCustomFormat = "";
            this.WorkIdTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.WorkIdTextBox.WholeInteger = false;
            // 
            // WorkOrderLabel
            // 
            this.WorkOrderLabel.AutoSize = true;
            this.WorkOrderLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkOrderLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.WorkOrderLabel.Location = new System.Drawing.Point(1, 0);
            this.WorkOrderLabel.Name = "WorkOrderLabel";
            this.WorkOrderLabel.Size = new System.Drawing.Size(74, 14);
            this.WorkOrderLabel.TabIndex = 3;
            this.WorkOrderLabel.Text = "Work Order:";
            // 
            // WorkOrderIDPanel
            // 
            this.WorkOrderIDPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WorkOrderIDPanel.Controls.Add(this.WorkOrderTextBox);
            this.WorkOrderIDPanel.Controls.Add(this.WorkOrderIdLabel);
            this.WorkOrderIDPanel.Location = new System.Drawing.Point(669, 0);
            this.WorkOrderIDPanel.Name = "WorkOrderIDPanel";
            this.WorkOrderIDPanel.Size = new System.Drawing.Size(99, 39);
            this.WorkOrderIDPanel.TabIndex = 3;
            // 
            // WorkOrderTextBox
            // 
            this.WorkOrderTextBox.AcceptsReturn = true;
            this.WorkOrderTextBox.AllowClick = true;
            this.WorkOrderTextBox.AllowNegativeSign = false;
            this.WorkOrderTextBox.ApplyCFGFormat = false;
            this.WorkOrderTextBox.ApplyCurrencyFormat = false;
            this.WorkOrderTextBox.ApplyFocusColor = true;
            this.WorkOrderTextBox.ApplyNegativeStandard = true;
            this.WorkOrderTextBox.ApplyParentFocusColor = true;
            this.WorkOrderTextBox.ApplyTimeFormat = false;
            this.WorkOrderTextBox.BackColor = System.Drawing.Color.White;
            this.WorkOrderTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.WorkOrderTextBox.CFromatWihoutSymbol = false;
            this.WorkOrderTextBox.CheckForEmpty = false;
            this.WorkOrderTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WorkOrderTextBox.Digits = -1;
            this.WorkOrderTextBox.EmptyDecimalValue = false;
            this.WorkOrderTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.WorkOrderTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.WorkOrderTextBox.IsEditable = false;
            this.WorkOrderTextBox.IsQueryableFileld = true;
            this.WorkOrderTextBox.Location = new System.Drawing.Point(24, 18);
            this.WorkOrderTextBox.LockKeyPress = true;
            this.WorkOrderTextBox.MaxLength = 5;
            this.WorkOrderTextBox.Name = "WorkOrderTextBox";
            this.WorkOrderTextBox.PersistDefaultColor = false;
            this.WorkOrderTextBox.Precision = 2;
            this.WorkOrderTextBox.QueryingFileldName = "";
            this.WorkOrderTextBox.ReadOnly = true;
            this.WorkOrderTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.WorkOrderTextBox.Size = new System.Drawing.Size(58, 16);
            this.WorkOrderTextBox.SpecialCharacter = "%";
            this.WorkOrderTextBox.TabIndex = 0;
            this.WorkOrderTextBox.TabStop = false;
            this.WorkOrderTextBox.TextCustomFormat = "";
            this.WorkOrderTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Numeric;
            this.WorkOrderTextBox.WholeInteger = false;
            // 
            // WorkOrderIdLabel
            // 
            this.WorkOrderIdLabel.AutoSize = true;
            this.WorkOrderIdLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkOrderIdLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.WorkOrderIdLabel.Location = new System.Drawing.Point(1, 0);
            this.WorkOrderIdLabel.Name = "WorkOrderIdLabel";
            this.WorkOrderIdLabel.Size = new System.Drawing.Size(74, 14);
            this.WorkOrderIdLabel.TabIndex = 4;
            this.WorkOrderIdLabel.Text = "Work Order:";
            // 
            // EventDatePanel
            // 
            this.EventDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EventDatePanel.Controls.Add(this.WorkOrderHeaderDatePict);
            this.EventDatePanel.Controls.Add(this.WorkOrderTimePicker);
            this.EventDatePanel.Controls.Add(this.WorkOrderDate);
            this.EventDatePanel.Controls.Add(this.WorkOrderDateLabel);
            this.EventDatePanel.Location = new System.Drawing.Point(426, 0);
            this.EventDatePanel.Name = "EventDatePanel";
            this.EventDatePanel.Size = new System.Drawing.Size(155, 39);
            this.EventDatePanel.TabIndex = 1;
            // 
            // WorkOrderHeaderDatePict
            // 
            this.WorkOrderHeaderDatePict.FlatAppearance.BorderSize = 0;
            this.WorkOrderHeaderDatePict.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WorkOrderHeaderDatePict.Image = ((System.Drawing.Image)(resources.GetObject("WorkOrderHeaderDatePict.Image")));
            this.WorkOrderHeaderDatePict.Location = new System.Drawing.Point(120, 12);
            this.WorkOrderHeaderDatePict.Name = "WorkOrderHeaderDatePict";
            this.WorkOrderHeaderDatePict.Size = new System.Drawing.Size(20, 24);
            this.WorkOrderHeaderDatePict.TabIndex = 2;
            this.WorkOrderHeaderDatePict.Tag = "ReceiptDateTextBox";
            this.WorkOrderHeaderDatePict.UseVisualStyleBackColor = true;
            this.WorkOrderHeaderDatePict.Click += new System.EventHandler(this.WorkOrderHeaderDatePict_Click);
            // 
            // WorkOrderTimePicker
            // 
            this.WorkOrderTimePicker.CustomFormat = "MM/dd/yyyy";
            this.WorkOrderTimePicker.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkOrderTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.WorkOrderTimePicker.Location = new System.Drawing.Point(125, 14);
            this.WorkOrderTimePicker.Margin = new System.Windows.Forms.Padding(0);
            this.WorkOrderTimePicker.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            this.WorkOrderTimePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.WorkOrderTimePicker.Name = "WorkOrderTimePicker";
            this.WorkOrderTimePicker.Size = new System.Drawing.Size(10, 20);
            this.WorkOrderTimePicker.TabIndex = 9;
            this.WorkOrderTimePicker.TabStop = false;
            this.WorkOrderTimePicker.CloseUp += new System.EventHandler(this.WorkOrderTimePicker_CloseUp);
            this.WorkOrderTimePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WorkOrderTimePicker_KeyPress);
            // 
            // WorkOrderDate
            // 
            this.WorkOrderDate.AllowClick = true;
            this.WorkOrderDate.AllowNegativeSign = false;
            this.WorkOrderDate.ApplyCFGFormat = false;
            this.WorkOrderDate.ApplyCurrencyFormat = false;
            this.WorkOrderDate.ApplyFocusColor = true;
            this.WorkOrderDate.ApplyNegativeStandard = true;
            this.WorkOrderDate.ApplyParentFocusColor = true;
            this.WorkOrderDate.ApplyTimeFormat = false;
            this.WorkOrderDate.BackColor = System.Drawing.Color.White;
            this.WorkOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.WorkOrderDate.CFromatWihoutSymbol = false;
            this.WorkOrderDate.CheckForEmpty = false;
            this.WorkOrderDate.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.WorkOrderDate.Digits = -1;
            this.WorkOrderDate.EmptyDecimalValue = false;
            this.WorkOrderDate.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.WorkOrderDate.IsEditable = true;
            this.WorkOrderDate.IsQueryableFileld = true;
            this.WorkOrderDate.Location = new System.Drawing.Point(49, 18);
            this.WorkOrderDate.LockKeyPress = false;
            this.WorkOrderDate.MaxLength = 10;
            this.WorkOrderDate.Name = "WorkOrderDate";
            this.WorkOrderDate.PersistDefaultColor = false;
            this.WorkOrderDate.Precision = 2;
            this.WorkOrderDate.QueryingFileldName = "";
            this.WorkOrderDate.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.WorkOrderDate.Size = new System.Drawing.Size(65, 16);
            this.WorkOrderDate.SpecialCharacter = "%";
            this.WorkOrderDate.TabIndex = 1;
            this.WorkOrderDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.WorkOrderDate.TextCustomFormat = "";
            this.WorkOrderDate.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.WorkOrderDate.WholeInteger = false;
            this.WorkOrderDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WorkOrderDate_KeyPress);
            this.WorkOrderDate.TextChanged += new System.EventHandler(this.WorkOrderDate_TextChanged);
            this.WorkOrderDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WorkOrderTextBox_KeyDown);
            // 
            // WorkOrderDateLabel
            // 
            this.WorkOrderDateLabel.AutoSize = true;
            this.WorkOrderDateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkOrderDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.WorkOrderDateLabel.Location = new System.Drawing.Point(1, 0);
            this.WorkOrderDateLabel.Name = "WorkOrderDateLabel";
            this.WorkOrderDateLabel.Size = new System.Drawing.Size(98, 14);
            this.WorkOrderDateLabel.TabIndex = 2;
            this.WorkOrderDateLabel.Tag = "1105";
            this.WorkOrderDateLabel.Text = "WorkOrder Date:";
            // 
            // ClosedPanel
            // 
            this.ClosedPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ClosedPanel.Controls.Add(this.ClosedCheckBox);
            this.ClosedPanel.Controls.Add(this.ClosedLabel);
            this.ClosedPanel.Location = new System.Drawing.Point(580, 0);
            this.ClosedPanel.Name = "ClosedPanel";
            this.ClosedPanel.Size = new System.Drawing.Size(90, 39);
            this.ClosedPanel.TabIndex = 2;
            // 
            // ClosedCheckBox
            // 
            this.ClosedCheckBox.AutoSize = true;
            this.ClosedCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClosedCheckBox.Location = new System.Drawing.Point(49, 21);
            this.ClosedCheckBox.Name = "ClosedCheckBox";
            this.ClosedCheckBox.Size = new System.Drawing.Size(12, 11);
            this.ClosedCheckBox.TabIndex = 3;
            this.ClosedCheckBox.UseVisualStyleBackColor = true;
            this.ClosedCheckBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClosedCheckBox_MouseClick);
            // 
            // ClosedLabel
            // 
            this.ClosedLabel.AutoSize = true;
            this.ClosedLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClosedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.ClosedLabel.Location = new System.Drawing.Point(1, 0);
            this.ClosedLabel.Name = "ClosedLabel";
            this.ClosedLabel.Size = new System.Drawing.Size(49, 14);
            this.ClosedLabel.TabIndex = 3;
            this.ClosedLabel.Tag = "1105";
            this.ClosedLabel.Text = "Closed:";
            // 
            // WorkOrderPictureBox
            // 
            this.WorkOrderPictureBox.Location = new System.Drawing.Point(761, 0);
            this.WorkOrderPictureBox.Name = "WorkOrderPictureBox";
            this.WorkOrderPictureBox.Size = new System.Drawing.Size(42, 39);
            this.WorkOrderPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WorkOrderPictureBox.TabIndex = 11;
            this.WorkOrderPictureBox.TabStop = false;
            this.WorkOrderPictureBox.MouseEnter += new System.EventHandler(this.WorkOrderPictureBox_MouseEnter);
            // 
            // F8902
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ClosedPanel);
            this.Controls.Add(this.EventDatePanel);
            this.Controls.Add(this.WorkOrderIDPanel);
            this.Controls.Add(this.WorkOrderPanel);
            this.Controls.Add(this.WorkOrderPictureBox);
            this.Name = "F8902";
            this.Size = new System.Drawing.Size(804, 39);
            this.Tag = "8902";
            this.Load += new System.EventHandler(this.F8902_Load);
            this.WorkOrderPanel.ResumeLayout(false);
            this.WorkOrderPanel.PerformLayout();
            this.WorkOrderIDPanel.ResumeLayout(false);
            this.WorkOrderIDPanel.PerformLayout();
            this.EventDatePanel.ResumeLayout(false);
            this.EventDatePanel.PerformLayout();
            this.ClosedPanel.ResumeLayout(false);
            this.ClosedPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorkOrderPictureBox)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel WorkOrderPanel;
        private System.Windows.Forms.Panel WorkOrderIDPanel;
        private System.Windows.Forms.Panel EventDatePanel;
        private System.Windows.Forms.Panel ClosedPanel;
        private System.Windows.Forms.Label WorkOrderLabel;
        private System.Windows.Forms.Label WorkOrderDateLabel;
        private System.Windows.Forms.Label ClosedLabel;
        private System.Windows.Forms.Label WorkOrderIdLabel;
        private TerraScan.UI.Controls.TerraScanTextBox WorkOrderDate;
        private System.Windows.Forms.DateTimePicker WorkOrderTimePicker;
        private System.Windows.Forms.Button WorkOrderHeaderDatePict;
        private TerraScan.UI.Controls.TerraScanCheckBox ClosedCheckBox;
        private System.Windows.Forms.PictureBox WorkOrderPictureBox;
        private TerraScan.UI.Controls.TerraScanTextBox WorkIdTextBox;
        private System.Windows.Forms.ToolTip WorkOrderHeaderToolTip;
        private TerraScan.UI.Controls.TerraScanTextBox WorkOrderTextBox;
	}
}
