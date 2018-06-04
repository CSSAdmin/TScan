namespace D3230
{
    partial class Progressform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Progressform));
            this.ProgressPictureBox = new System.Windows.Forms.PictureBox();
            this.ProcessLabel = new System.Windows.Forms.Label();
            this.DetailListBox = new System.Windows.Forms.ListBox();
            this.OkButton = new TerraScan.UI.Controls.TerraScanButton();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ProgressPictureBox
            // 
            this.ProgressPictureBox.Image = global::D3230.Properties.Resources.ProgressPictureBox_Image;
            this.ProgressPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("ProgressPictureBox.InitialImage")));
            this.ProgressPictureBox.Location = new System.Drawing.Point(46, 45);
            this.ProgressPictureBox.Name = "ProgressPictureBox";
            this.ProgressPictureBox.Size = new System.Drawing.Size(201, 13);
            this.ProgressPictureBox.TabIndex = 0;
            this.ProgressPictureBox.TabStop = false;
            // 
            // ProcessLabel
            // 
            this.ProcessLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessLabel.Location = new System.Drawing.Point(1, 9);
            this.ProcessLabel.Name = "ProcessLabel";
            this.ProcessLabel.Size = new System.Drawing.Size(290, 23);
            this.ProcessLabel.TabIndex = 1;
            this.ProcessLabel.Text = "CheckOut Processing ....";
            this.ProcessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DetailListBox
            // 
            this.DetailListBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetailListBox.FormattingEnabled = true;
            this.DetailListBox.HorizontalScrollbar = true;
            this.DetailListBox.ItemHeight = 14;
            this.DetailListBox.Location = new System.Drawing.Point(12, 75);
            this.DetailListBox.Name = "DetailListBox";
            this.DetailListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.DetailListBox.Size = new System.Drawing.Size(267, 116);
            this.DetailListBox.TabIndex = 2;
            // 
            // OkButton
            // 
            this.OkButton.ActualPermission = false;
            this.OkButton.ApplyDisableBehaviour = false;
            this.OkButton.AutoEllipsis = true;
            this.OkButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.OkButton.BorderColor = System.Drawing.Color.Wheat;
            this.OkButton.CommentPriority = false;
            this.OkButton.EnableAutoPrint = false;
            this.OkButton.Enabled = false;
            this.OkButton.FilterStatus = false;
            this.OkButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton.FocusRectangleEnabled = true;
            this.OkButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.OkButton.ImageSelected = false;
            this.OkButton.Location = new System.Drawing.Point(88, 197);
            this.OkButton.Name = "OkButton";
            this.OkButton.NewPadding = 5;
            this.OkButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Edit;
            this.OkButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.OkButton.Size = new System.Drawing.Size(98, 28);
            this.OkButton.StatusIndicator = false;
            this.OkButton.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.OkButton.StatusOffText = null;
            this.OkButton.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
            this.OkButton.StatusOnText = null;
            this.OkButton.TabIndex = 225;
            this.OkButton.TabStop = false;
            this.OkButton.Tag = "NEW";
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // Progressform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(291, 233);
            this.ControlBox = false;
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.DetailListBox);
            this.Controls.Add(this.ProcessLabel);
            this.Controls.Add(this.ProgressPictureBox);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Progressform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.ProgressPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.PictureBox ProgressPictureBox;
        private System.Windows.Forms.Label ProcessLabel;
        private System.Windows.Forms.ListBox DetailListBox;
        private TerraScan.UI.Controls.TerraScanButton OkButton;
    }
}