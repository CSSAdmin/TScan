namespace TerraScan.ApplicationInstaller
{
    partial class Webservicetest
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
            this.webservicelabel = new System.Windows.Forms.Label();
            this.wsurltextbox = new System.Windows.Forms.TextBox();
            this.checkwsurlbutton = new System.Windows.Forms.Button();
            this.statuslabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // webservicelabel
            // 
            this.webservicelabel.AutoSize = true;
            this.webservicelabel.Location = new System.Drawing.Point(21, 45);
            this.webservicelabel.Name = "webservicelabel";
            this.webservicelabel.Size = new System.Drawing.Size(100, 13);
            this.webservicelabel.TabIndex = 0;
            this.webservicelabel.Text = "Web Service URL :";
            // 
            // wsurltextbox
            // 
            this.wsurltextbox.Location = new System.Drawing.Point(127, 41);
            this.wsurltextbox.Name = "wsurltextbox";
            this.wsurltextbox.Size = new System.Drawing.Size(501, 20);
            this.wsurltextbox.TabIndex = 1;
            // 
            // checkwsurlbutton
            // 
            this.checkwsurlbutton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkwsurlbutton.Location = new System.Drawing.Point(251, 77);
            this.checkwsurlbutton.Name = "checkwsurlbutton";
            this.checkwsurlbutton.Size = new System.Drawing.Size(132, 31);
            this.checkwsurlbutton.TabIndex = 2;
            this.checkwsurlbutton.Text = "Test &Web Service";
            this.checkwsurlbutton.UseVisualStyleBackColor = true;
            this.checkwsurlbutton.Click += new System.EventHandler(this.Checkwsurlbutton_Click);
            // 
            // statuslabel
            // 
            this.statuslabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statuslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statuslabel.Location = new System.Drawing.Point(0, 0);
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(636, 28);
            this.statuslabel.TabIndex = 3;
            this.statuslabel.Text = "Status";
            this.statuslabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Webservicetest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 120);
            this.ControlBox = false;
            this.Controls.Add(this.statuslabel);
            this.Controls.Add(this.checkwsurlbutton);
            this.Controls.Add(this.wsurltextbox);
            this.Controls.Add(this.webservicelabel);
            this.Name = "Webservicetest";
            this.Text = "Web Service Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label webservicelabel;
        private System.Windows.Forms.TextBox wsurltextbox;
        private System.Windows.Forms.Button checkwsurlbutton;
        private System.Windows.Forms.Label statuslabel;
    }
}