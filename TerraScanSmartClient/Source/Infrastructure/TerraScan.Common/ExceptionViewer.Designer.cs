namespace TerraScan.Common
{
    partial class ExceptionViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionViewer));
            this.ErrorMessageLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.MoreCommentButton = new System.Windows.Forms.Button();
            this.CommentTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.SuspendLayout();
            // 
            // ErrorMessageLabel
            // 
            this.ErrorMessageLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorMessageLabel.Location = new System.Drawing.Point(12, 18);
            this.ErrorMessageLabel.Name = "ErrorMessageLabel";
            this.ErrorMessageLabel.Size = new System.Drawing.Size(507, 56);
            this.ErrorMessageLabel.TabIndex = 0;
            this.ErrorMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OKButton
            // 
            this.OKButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.ForeColor = System.Drawing.Color.White;
            this.OKButton.Location = new System.Drawing.Point(215, 83);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "&OK";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // MoreCommentButton
            // 
            this.MoreCommentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.MoreCommentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoreCommentButton.ForeColor = System.Drawing.Color.White;
            this.MoreCommentButton.Location = new System.Drawing.Point(395, 83);
            this.MoreCommentButton.Name = "MoreCommentButton";
            this.MoreCommentButton.Size = new System.Drawing.Size(124, 23);
            this.MoreCommentButton.TabIndex = 2;
            this.MoreCommentButton.Text = "&More Comments >>";
            this.MoreCommentButton.UseVisualStyleBackColor = false;
            this.MoreCommentButton.Visible = false;
            this.MoreCommentButton.Click += new System.EventHandler(this.MoreCommentButton_Click);
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.ApplyFocusColor = false;
            this.CommentTextBox.ApplyParentFocusColor = true;
            this.CommentTextBox.Digits = -1;
            this.CommentTextBox.Location = new System.Drawing.Point(12, 120);
            this.CommentTextBox.LockKeyPress = false;
            this.CommentTextBox.Multiline = true;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.Precision = 2;
            this.CommentTextBox.SetFocusColor = System.Drawing.Color.Empty;
            this.CommentTextBox.Size = new System.Drawing.Size(504, 123);
            this.CommentTextBox.TabIndex = 3;
            this.CommentTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            // 
            // ExceptionViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(531, 112);
            this.Controls.Add(this.CommentTextBox);
            this.Controls.Add(this.MoreCommentButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.ErrorMessageLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(539, 146);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(539, 146);
            this.Name = "ExceptionViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ErrorMessageLabel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button MoreCommentButton;
        private TerraScan.UI.Controls.TerraScanTextBox CommentTextBox;
    }
}