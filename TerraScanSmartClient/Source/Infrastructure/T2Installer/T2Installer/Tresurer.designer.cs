namespace T2Installer
{
    partial class Tresurer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tresurer));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtMSWCF = new System.Windows.Forms.TextBox();
            this.lblMSWCF = new System.Windows.Forms.Label();
            this.txtInstalURL = new System.Windows.Forms.TextBox();
            this.lblInstal = new System.Windows.Forms.Label();
            this.txtWCFService = new System.Windows.Forms.TextBox();
            this.lblWCF = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.Location = new System.Drawing.Point(0, -12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(500, 84);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please Enter the WCF Service URL :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(412, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(89, 65);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtMSWCF
            // 
            this.txtMSWCF.Location = new System.Drawing.Point(34, 260);
            this.txtMSWCF.Name = "txtMSWCF";
            this.txtMSWCF.Size = new System.Drawing.Size(441, 20);
            this.txtMSWCF.TabIndex = 3;
            this.txtMSWCF.TextChanged += new System.EventHandler(this.txtMSWCF_TextChanged);
            // 
            // lblMSWCF
            // 
            this.lblMSWCF.AutoSize = true;
            this.lblMSWCF.Location = new System.Drawing.Point(32, 242);
            this.lblMSWCF.Name = "lblMSWCF";
            this.lblMSWCF.Size = new System.Drawing.Size(117, 13);
            this.lblMSWCF.TabIndex = 62;
            this.lblMSWCF.Text = "MSWCF Service URL :";
            // 
            // txtInstalURL
            // 
            this.txtInstalURL.Location = new System.Drawing.Point(34, 209);
            this.txtInstalURL.Name = "txtInstalURL";
            this.txtInstalURL.Size = new System.Drawing.Size(441, 20);
            this.txtInstalURL.TabIndex = 2;
            this.txtInstalURL.TextChanged += new System.EventHandler(this.txtInstalURL_TextChanged);
            // 
            // lblInstal
            // 
            this.lblInstal.AutoSize = true;
            this.lblInstal.Location = new System.Drawing.Point(32, 192);
            this.lblInstal.Name = "lblInstal";
            this.lblInstal.Size = new System.Drawing.Size(65, 13);
            this.lblInstal.TabIndex = 60;
            this.lblInstal.Text = "Install URL :";
            // 
            // txtWCFService
            // 
            this.txtWCFService.BackColor = System.Drawing.SystemColors.Window;
            this.txtWCFService.Location = new System.Drawing.Point(34, 164);
            this.txtWCFService.Name = "txtWCFService";
            this.txtWCFService.Size = new System.Drawing.Size(441, 20);
            this.txtWCFService.TabIndex = 1;
            this.txtWCFService.Text = " ";
            this.txtWCFService.TextChanged += new System.EventHandler(this.txtWCFService_TextChanged);
            // 
            // lblWCF
            // 
            this.lblWCF.AutoSize = true;
            this.lblWCF.Location = new System.Drawing.Point(30, 146);
            this.lblWCF.Name = "lblWCF";
            this.lblWCF.Size = new System.Drawing.Size(101, 13);
            this.lblWCF.TabIndex = 58;
            this.lblWCF.Text = "WCF Service URL :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "Input Details :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBack);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnNext);
            this.groupBox3.Location = new System.Drawing.Point(-9, 327);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(510, 58);
            this.groupBox3.TabIndex = 65;
            this.groupBox3.TabStop = false;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(308, 19);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(89, 27);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "< &Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(213, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 27);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(405, 19);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(89, 27);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "&Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Treasurer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 380);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMSWCF);
            this.Controls.Add(this.lblMSWCF);
            this.Controls.Add(this.txtInstalURL);
            this.Controls.Add(this.lblInstal);
            this.Controls.Add(this.txtWCFService);
            this.Controls.Add(this.lblWCF);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Tresurer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerranScan T2 Treasurer";
            this.Load += new System.EventHandler(this.Tresurer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtMSWCF;
        private System.Windows.Forms.Label lblMSWCF;
        private System.Windows.Forms.TextBox txtInstalURL;
        private System.Windows.Forms.Label lblInstal;
        private System.Windows.Forms.TextBox txtWCFService;
        private System.Windows.Forms.Label lblWCF;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}