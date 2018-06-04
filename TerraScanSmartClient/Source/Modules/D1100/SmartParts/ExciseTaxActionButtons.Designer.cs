namespace D1100
{
    partial class ExciseTaxActionButtons
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
            this.AffidavitFormButton = new System.Windows.Forms.Button();
            this.ExciseRatesButton = new System.Windows.Forms.Button();
            this.WorkQueueButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AffidavitFormButton
            // 
            this.AffidavitFormButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.AffidavitFormButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AffidavitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AffidavitFormButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.AffidavitFormButton.ForeColor = System.Drawing.Color.White;
            this.AffidavitFormButton.Location = new System.Drawing.Point(0, 3);
            this.AffidavitFormButton.Name = "AffidavitFormButton";
            this.AffidavitFormButton.Size = new System.Drawing.Size(98, 28);
            this.AffidavitFormButton.TabIndex = 10;
            this.AffidavitFormButton.Text = "Affidavit Form";
            this.AffidavitFormButton.UseVisualStyleBackColor = false;
            this.AffidavitFormButton.Click += new System.EventHandler(this.AffidavitFormButton_Click);
            // 
            // ExciseRatesButton
            // 
            this.ExciseRatesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.ExciseRatesButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ExciseRatesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExciseRatesButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.ExciseRatesButton.ForeColor = System.Drawing.Color.White;
            this.ExciseRatesButton.Location = new System.Drawing.Point(104, 3);
            this.ExciseRatesButton.Name = "ExciseRatesButton";
            this.ExciseRatesButton.Size = new System.Drawing.Size(98, 28);
            this.ExciseRatesButton.TabIndex = 11;
            this.ExciseRatesButton.Text = "Excise Rates";
            this.ExciseRatesButton.UseVisualStyleBackColor = false;
            this.ExciseRatesButton.Click += new System.EventHandler(this.ExciseRatesButton_Click);
            // 
            // WorkQueueButton
            // 
            this.WorkQueueButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.WorkQueueButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.WorkQueueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WorkQueueButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.WorkQueueButton.ForeColor = System.Drawing.Color.White;
            this.WorkQueueButton.Location = new System.Drawing.Point(208, 3);
            this.WorkQueueButton.Name = "WorkQueueButton";
            this.WorkQueueButton.Size = new System.Drawing.Size(98, 28);
            this.WorkQueueButton.TabIndex = 12;
            this.WorkQueueButton.Text = "Work Queue";
            this.WorkQueueButton.UseVisualStyleBackColor = false;
            this.WorkQueueButton.Click += new System.EventHandler(this.WorkQueueButton_Click);
            // 
            // ExciseTaxActionButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.WorkQueueButton);
            this.Controls.Add(this.ExciseRatesButton);
            this.Controls.Add(this.AffidavitFormButton);
            this.Name = "ExciseTaxActionButtons";
            this.Size = new System.Drawing.Size(311, 34);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AffidavitFormButton;
        private System.Windows.Forms.Button ExciseRatesButton;
        private System.Windows.Forms.Button WorkQueueButton;
    }
}
