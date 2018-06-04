namespace TerraScan.SmartParts
{
    partial class TestOperationSmartPart
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
            this.SaveButton = new TerraScan.UI.Controls.TerraScanButton();
            this.CancelButton = new TerraScan.UI.Controls.TerraScanButton();
            this.DeleteButton = new TerraScan.UI.Controls.TerraScanButton();
            this.NewButton = new TerraScan.UI.Controls.TerraScanButton();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.ActualPermission = false;
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
            this.SaveButton.Location = new System.Drawing.Point(104, 9);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.NewPadding = 5;
            this.SaveButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Save;
            this.SaveButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.SaveButton.Size = new System.Drawing.Size(98, 28);
            this.SaveButton.StatusIndicator = false;
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Tag = "SAVE";
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.ActualPermission = false;
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
            this.CancelButton.Location = new System.Drawing.Point(208, 9);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NewPadding = 5;
            this.CancelButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Cancel;
            this.CancelButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.CancelButton.Size = new System.Drawing.Size(98, 28);
            this.CancelButton.StatusIndicator = false;
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Tag = "CANCEL";
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.ActualPermission = false;
            this.DeleteButton.AutoSize = true;
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
            this.DeleteButton.Location = new System.Drawing.Point(312, 9);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.NewPadding = 5;
            this.DeleteButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.Delete;
            this.DeleteButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.DeleteButton.Size = new System.Drawing.Size(98, 28);
            this.DeleteButton.StatusIndicator = false;
            this.DeleteButton.TabIndex = 8;
            this.DeleteButton.Tag = "DELETE";
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // NewButton
            // 
            this.NewButton.ActualPermission = false;
            this.NewButton.AutoSize = true;
            this.NewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.NewButton.BorderColor = System.Drawing.Color.Wheat;
            this.NewButton.CommentPriority = false;
            this.NewButton.EnableAutoPrint = false;
            this.NewButton.FilterStatus = false;
            this.NewButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.NewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewButton.FocusRectangleEnabled = true;
            this.NewButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.NewButton.ImageSelected = false;
            this.NewButton.Location = new System.Drawing.Point(0, 9);
            this.NewButton.Name = "NewButton";
            this.NewButton.NewPadding = 5;
            this.NewButton.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
            this.NewButton.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
            this.NewButton.Size = new System.Drawing.Size(98, 28);
            this.NewButton.StatusIndicator = false;
            this.NewButton.TabIndex = 5;
            this.NewButton.Tag = "NEW";
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = false;
            this.NewButton.Click += new System.EventHandler(this.OperationButton_Click);
            // 
            // TestOperationSmartPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.NewButton);
            this.Name = "TestOperationSmartPart";
            this.Size = new System.Drawing.Size(416, 45);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerraScan.UI.Controls.TerraScanButton SaveButton;
        private TerraScan.UI.Controls.TerraScanButton CancelButton;
        private TerraScan.UI.Controls.TerraScanButton DeleteButton;
        private TerraScan.UI.Controls.TerraScanButton NewButton;
    }
}
