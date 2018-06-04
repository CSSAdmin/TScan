namespace TerraScan.SmartParts
{
    partial class StatusBarSmartPart
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
            this.DelinquentButton = new TerraScan.UI.Controls.TerraScanButton();
            this.AutoPrintOnButton = new TerraScan.UI.Controls.TerraScanButton();
            this.FilteredButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SuspendLayout();
            // 
            // DelinquentButton
            // 
            this.DelinquentButton.ActualPermission = false;
            this.DelinquentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DelinquentButton.AutoEllipsis = true;
            this.DelinquentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DelinquentButton.BorderColor = System.Drawing.Color.Wheat;
            this.DelinquentButton.CommentPriority = false;
            this.DelinquentButton.EnableAutoPrint = false;
            this.DelinquentButton.Enabled = false;
            this.DelinquentButton.FilterStatus = false;
            this.DelinquentButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.DelinquentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DelinquentButton.FocusRectangleEnabled = true;
            this.DelinquentButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DelinquentButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DelinquentButton.ImageSelected = false;
            this.DelinquentButton.Location = new System.Drawing.Point(120, 11);
            this.DelinquentButton.Name = "DelinquentButton";
            this.DelinquentButton.NewPadding = 5;
            this.DelinquentButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.DelinquentButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.Status;
            this.DelinquentButton.Size = new System.Drawing.Size(98, 28);
            this.DelinquentButton.StatusIndicator = false;
            this.DelinquentButton.StatusOffText = "Delinquent";
            this.DelinquentButton.StatusOnText = "Not Delinquent";
            this.DelinquentButton.TabIndex = 1;
            this.DelinquentButton.TabStop = false;
            this.DelinquentButton.Text = "Delinquent";
            this.DelinquentButton.UseVisualStyleBackColor = false;
            this.DelinquentButton.Click += new System.EventHandler(this.DelinquentButton_Click);
            // 
            // AutoPrintOnButton
            // 
            this.AutoPrintOnButton.ActualPermission = false;
            this.AutoPrintOnButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoPrintOnButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AutoPrintOnButton.BorderColor = System.Drawing.Color.Wheat;
            this.AutoPrintOnButton.CommentPriority = false;
            this.AutoPrintOnButton.EnableAutoPrint = false;
            this.AutoPrintOnButton.FilterStatus = false;
            this.AutoPrintOnButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AutoPrintOnButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AutoPrintOnButton.FocusRectangleEnabled = true;
            this.AutoPrintOnButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutoPrintOnButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AutoPrintOnButton.ImageSelected = false;
            this.AutoPrintOnButton.Location = new System.Drawing.Point(605, 11);
            this.AutoPrintOnButton.Name = "AutoPrintOnButton";
            this.AutoPrintOnButton.NewPadding = 5;
            this.AutoPrintOnButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.AutoPrintOnButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.AutoPrint;
            this.AutoPrintOnButton.Size = new System.Drawing.Size(104, 28);
            this.AutoPrintOnButton.StatusIndicator = false;
            this.AutoPrintOnButton.StatusOffText = null;
            this.AutoPrintOnButton.StatusOnText = null;
            this.AutoPrintOnButton.TabIndex = 2;
            this.AutoPrintOnButton.TabStop = false;
            this.AutoPrintOnButton.Text = "AutoPrint Off";
            this.AutoPrintOnButton.UseVisualStyleBackColor = false;
            this.AutoPrintOnButton.Click += new System.EventHandler(this.AutoPrintOnButton_Click);
            // 
            // FilteredButton
            // 
            this.FilteredButton.ActualPermission = false;
            this.FilteredButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FilteredButton.AutoEllipsis = true;
            this.FilteredButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.FilteredButton.BorderColor = System.Drawing.Color.Black;
            this.FilteredButton.CommentPriority = false;
            this.FilteredButton.EnableAutoPrint = false;
            this.FilteredButton.FilterStatus = false;
            this.FilteredButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.FilteredButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FilteredButton.FocusRectangleEnabled = true;
            this.FilteredButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilteredButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FilteredButton.ImageSelected = false;
            this.FilteredButton.Location = new System.Drawing.Point(14, 11);
            this.FilteredButton.Name = "FilteredButton";
            this.FilteredButton.NewPadding = 5;
            this.FilteredButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Other;
            this.FilteredButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.Filter;
            this.FilteredButton.Size = new System.Drawing.Size(98, 28);
            this.FilteredButton.StatusIndicator = false;
            this.FilteredButton.StatusOffText = null;
            this.FilteredButton.StatusOnText = null;
            this.FilteredButton.TabIndex = 0;
            this.FilteredButton.TabStop = false;
            this.FilteredButton.Text = "Not Filtered";
            this.FilteredButton.UseVisualStyleBackColor = false;
            this.FilteredButton.Click += new System.EventHandler(this.FilteredButton_Click);
            // 
            // StatusBarSmartPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.DelinquentButton);
            this.Controls.Add(this.AutoPrintOnButton);
            this.Controls.Add(this.FilteredButton);
            this.Name = "StatusBarSmartPart";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(714, 46);
            this.ResumeLayout(false);

        }

        #endregion

        protected internal TerraScan.UI.Controls.TerraScanButton FilteredButton;
        protected internal TerraScan.UI.Controls.TerraScanButton DelinquentButton;
        protected internal TerraScan.UI.Controls.TerraScanButton AutoPrintOnButton;


    }
}
