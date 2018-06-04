namespace D9005
{
	partial class Scanning
	{
        /// <summary>
        /// Required designer variable.
        /// </summary>
        ////private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scanning));
            this.ContinueButton = new System.Windows.Forms.Button();
            this.DoneButton = new System.Windows.Forms.Button();
            this.ScanCancelButton = new System.Windows.Forms.Button();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.ContinueLabel = new System.Windows.Forms.Label();
            this.DoneLabel = new System.Windows.Forms.Label();
            this.CancelLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AxViewer = new AxMODI.AxMiDocView();
            ((System.ComponentModel.ISupportInitialize)(this.AxViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // ContinueButton
            // 
            this.ContinueButton.Location = new System.Drawing.Point(19, 114);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(75, 23);
            this.ContinueButton.TabIndex = 0;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // DoneButton
            // 
            this.DoneButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.DoneButton.Location = new System.Drawing.Point(20, 154);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(75, 23);
            this.DoneButton.TabIndex = 1;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // ScanCancelButton
            // 
            this.ScanCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ScanCancelButton.Location = new System.Drawing.Point(19, 193);
            this.ScanCancelButton.Name = "ScanCancelButton";
            this.ScanCancelButton.Size = new System.Drawing.Size(75, 23);
            this.ScanCancelButton.TabIndex = 2;
            this.ScanCancelButton.Text = "Cancel";
            this.ScanCancelButton.UseVisualStyleBackColor = true;
            this.ScanCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderLabel.Location = new System.Drawing.Point(12, 9);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(294, 47);
            this.HeaderLabel.TabIndex = 3;
            // 
            // ContinueLabel
            // 
            this.ContinueLabel.Location = new System.Drawing.Point(100, 114);
            this.ContinueLabel.Name = "ContinueLabel";
            this.ContinueLabel.Size = new System.Drawing.Size(175, 31);
            this.ContinueLabel.TabIndex = 4;
            this.ContinueLabel.Text = "Insert the next page and continue scanning.";
            // 
            // DoneLabel
            // 
            this.DoneLabel.AutoSize = true;
            this.DoneLabel.Location = new System.Drawing.Point(101, 161);
            this.DoneLabel.Name = "DoneLabel";
            this.DoneLabel.Size = new System.Drawing.Size(141, 13);
            this.DoneLabel.TabIndex = 5;
            this.DoneLabel.Text = "Stop scanning and save file.";
            // 
            // CancelLabel
            // 
            this.CancelLabel.AutoSize = true;
            this.CancelLabel.Location = new System.Drawing.Point(101, 198);
            this.CancelLabel.Name = "CancelLabel";
            this.CancelLabel.Size = new System.Drawing.Size(174, 13);
            this.CancelLabel.TabIndex = 6;
            this.CancelLabel.Text = "Stop scanning and do not save file.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Scanner has finished";
            // 
            // AxViewer
            // 
            this.AxViewer.Enabled = true;
            this.AxViewer.Location = new System.Drawing.Point(100, 98);
            this.AxViewer.Name = "AxViewer";
            this.AxViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("AxViewer.OcxState")));
            this.AxViewer.Size = new System.Drawing.Size(192, 133);
            this.AxViewer.TabIndex = 8;
            this.AxViewer.Visible = false;
            // 
            // Scanning
            // 
            this.AcceptButton = this.ContinueButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 240);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelLabel);
            this.Controls.Add(this.DoneLabel);
            this.Controls.Add(this.ContinueLabel);
            this.Controls.Add(this.HeaderLabel);
            this.Controls.Add(this.ScanCancelButton);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.AxViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Scanning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerraScan Document Scanning";
            this.Load += new System.EventHandler(this.Scanning_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AxViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.Button ScanCancelButton;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.Label ContinueLabel;
        private System.Windows.Forms.Label DoneLabel;
        private System.Windows.Forms.Label CancelLabel;
        private System.Windows.Forms.Label label1;
        private AxMODI.AxMiDocView AxViewer;
	}
}