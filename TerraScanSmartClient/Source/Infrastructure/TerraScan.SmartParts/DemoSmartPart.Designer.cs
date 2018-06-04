namespace TerraScan.SmartParts
{
    partial class DemoSmartPart
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
            this.SampleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SampleButton
            // 
            this.SampleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.SampleButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SampleButton.Location = new System.Drawing.Point(3, 3);
            this.SampleButton.Name = "SampleButton";
            this.SampleButton.Size = new System.Drawing.Size(99, 26);
            this.SampleButton.TabIndex = 0;
            this.SampleButton.Text = "&Click Here";
            this.SampleButton.UseVisualStyleBackColor = false;
            this.SampleButton.Click += new System.EventHandler(this.SampleButton_Click);
            // 
            // DemoSmartPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.SampleButton);
            this.Name = "DemoSmartPart";
            this.Size = new System.Drawing.Size(105, 33);
            this.Tag = "Demo SmartPart";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SampleButton;


    }
}
