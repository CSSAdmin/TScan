namespace TerraScan.SmartParts
{
    partial class FormHeaderSmartPart
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
            this.formName = new System.Windows.Forms.Label();
            this.operationName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // formName
            // 
            this.formName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.formName.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(80)))), ((int)(((byte)(129)))));
            this.formName.Location = new System.Drawing.Point(5, 9);
            this.formName.Name = "formName";
            this.formName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.formName.Size = new System.Drawing.Size(317, 22);
            this.formName.TabIndex = 9;
            this.formName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // operationName
            // 
            this.operationName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.operationName.AutoSize = true;
            this.operationName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.operationName.ForeColor = System.Drawing.Color.Green;
            this.operationName.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.operationName.Location = new System.Drawing.Point(215, 31);
            this.operationName.Name = "operationName";
            this.operationName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.operationName.Size = new System.Drawing.Size(0, 19);
            this.operationName.TabIndex = 10;
            this.operationName.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // FormHeaderSmartPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.operationName);
            this.Controls.Add(this.formName);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(80)))), ((int)(((byte)(129)))));
            this.Name = "FormHeaderSmartPart";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(340, 62);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label formName;
        private System.Windows.Forms.Label operationName;

    }
}
