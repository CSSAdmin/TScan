namespace TerraScan.UI
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.OkButton1 = new TerraScan.UI.Controls.TerraScanButton();
            this.DescriptionHeaderlabel = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.ClickOnceDescription = new System.Windows.Forms.Panel();
            this.descriptionTexbox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel.SuspendLayout();
            this.ClickOnceDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.12389F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.87611F));
            this.tableLayoutPanel.Controls.Add(this.OkButton1, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.DescriptionHeaderlabel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.ClickOnceDescription, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.83488F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.20492F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.65369F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.48737F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.44043F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(565, 273);
            this.tableLayoutPanel.TabIndex = 0;
            this.tableLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel_Paint);
            // 
            // OkButton1
            // 
            this.OkButton1.ActualPermission = false;
            this.OkButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton1.ApplyDisableBehaviour = false;
            this.OkButton1.AutoSize = true;
            this.OkButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.OkButton1.BorderColor = System.Drawing.Color.Wheat;
            this.OkButton1.CommentPriority = false;
            this.OkButton1.EnableAutoPrint = false;
            this.OkButton1.FilterStatus = false;
            this.OkButton1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.OkButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton1.FocusRectangleEnabled = true;
            this.OkButton1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.OkButton1.ImageSelected = false;
            this.OkButton1.Location = new System.Drawing.Point(477, 243);
            this.OkButton1.Name = "OkButton1";
            this.OkButton1.NewPadding = 5;
            this.OkButton1.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.OkButton1.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.OkButton1.Size = new System.Drawing.Size(85, 27);
            this.OkButton1.StatusIndicator = false;
            this.OkButton1.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.OkButton1.StatusOffText = null;
            this.OkButton1.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.OkButton1.StatusOnText = null;
            this.OkButton1.TabIndex = 29;
            this.OkButton1.Text = "OK";
            this.OkButton1.UseVisualStyleBackColor = false;
            this.OkButton1.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // DescriptionHeaderlabel
            // 
            this.tableLayoutPanel.SetColumnSpan(this.DescriptionHeaderlabel, 2);
            this.DescriptionHeaderlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DescriptionHeaderlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionHeaderlabel.Location = new System.Drawing.Point(6, 90);
            this.DescriptionHeaderlabel.Margin = new System.Windows.Forms.Padding(6, 10, 6, 0);
            this.DescriptionHeaderlabel.MaximumSize = new System.Drawing.Size(0, 17);
            this.DescriptionHeaderlabel.Name = "DescriptionHeaderlabel";
            this.DescriptionHeaderlabel.Size = new System.Drawing.Size(553, 17);
            this.DescriptionHeaderlabel.TabIndex = 31;
            this.DescriptionHeaderlabel.Text = "Deployment Information";
            this.DescriptionHeaderlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelProductName
            // 
            this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductName.Location = new System.Drawing.Point(352, 10);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(2, 10, 3, 0);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(210, 17);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "TerraScan T2";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.Location = new System.Drawing.Point(352, 42);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(2, 0, 3, 0);
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(210, 17);
            this.labelCopyright.TabIndex = 21;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ClickOnceDescription
            // 
            this.ClickOnceDescription.AutoScroll = true;
            this.ClickOnceDescription.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClickOnceDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel.SetColumnSpan(this.ClickOnceDescription, 2);
            this.ClickOnceDescription.Controls.Add(this.descriptionTexbox);
            this.ClickOnceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClickOnceDescription.Location = new System.Drawing.Point(3, 111);
            this.ClickOnceDescription.Name = "ClickOnceDescription";
            this.ClickOnceDescription.Size = new System.Drawing.Size(556, 117);
            this.ClickOnceDescription.TabIndex = 28;
            // 
            // descriptionTexbox
            // 
            this.descriptionTexbox.AllowClick = true;
            this.descriptionTexbox.AllowNegativeSign = false;
            this.descriptionTexbox.ApplyCFGFormat = false;
            this.descriptionTexbox.ApplyCurrencyFormat = false;
            this.descriptionTexbox.ApplyFocusColor = true;
            this.descriptionTexbox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.descriptionTexbox.ApplyNegativeStandard = true;
            this.descriptionTexbox.ApplyParentFocusColor = true;
            this.descriptionTexbox.ApplyTimeFormat = false;
            this.descriptionTexbox.BackColor = System.Drawing.Color.White;
            this.descriptionTexbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionTexbox.CFromatWihoutSymbol = false;
            this.descriptionTexbox.CheckForEmpty = false;
            this.descriptionTexbox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.descriptionTexbox.Digits = -1;
            this.descriptionTexbox.EmptyDecimalValue = false;
            this.descriptionTexbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.descriptionTexbox.ForeColor = System.Drawing.Color.Black;
            this.descriptionTexbox.IsEditable = false;
            this.descriptionTexbox.IsQueryableFileld = false;
            this.descriptionTexbox.Location = new System.Drawing.Point(3, 0);
            this.descriptionTexbox.LockKeyPress = false;
            this.descriptionTexbox.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.descriptionTexbox.MaxLength = 500;
            this.descriptionTexbox.Multiline = true;
            this.descriptionTexbox.Name = "descriptionTexbox";
            this.descriptionTexbox.PersistDefaultColor = false;
            this.descriptionTexbox.Precision = 2;
            this.descriptionTexbox.QueryingFileldName = "";
            this.descriptionTexbox.ReadOnly = true;
            this.descriptionTexbox.SetColorFlag = false;
            this.descriptionTexbox.SetFocusColor = System.Drawing.Color.White;
            this.descriptionTexbox.Size = new System.Drawing.Size(670, 118);
            this.descriptionTexbox.SpecialCharacter = "%";
            this.descriptionTexbox.TabIndex = 43;
            this.descriptionTexbox.TabStop = false;
            this.descriptionTexbox.TextCustomFormat = "$#,##0.00";
            this.descriptionTexbox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.descriptionTexbox.WholeInteger = false;
            this.descriptionTexbox.WordWrap = false;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Image = global::TerraScan.UI.Properties.Resources.ThomsonReutersLogo;
            this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 2);
            this.logoPictureBox.Size = new System.Drawing.Size(344, 74);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.logoPictureBox.TabIndex = 25;
            this.logoPictureBox.TabStop = false;
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(583, 291);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About TerraScan T2";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ClickOnceDescription.ResumeLayout(false);
            this.ClickOnceDescription.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Panel ClickOnceDescription;
        private System.Windows.Forms.Label DescriptionHeaderlabel;
        private TerraScan.UI.Controls.TerraScanButton OkButton1;
        private TerraScan.UI.Controls.TerraScanTextBox descriptionTexbox;
    }
}
