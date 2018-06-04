namespace TerraScan.Common
{
    partial class TerraScanMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerraScanMessageBox));
            this.ErrorTypeIconPictureBox = new System.Windows.Forms.PictureBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.ErrorTypeIconList = new System.Windows.Forms.ImageList(this.components);
            this.PanelYesNoCancel = new System.Windows.Forms.Panel();
            this.MessageCancelButton = new System.Windows.Forms.Button();
            this.MessageNoButton = new System.Windows.Forms.Button();
            this.MessageYesButton = new System.Windows.Forms.Button();
            this.PanelYesNo = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.PanelOk = new System.Windows.Forms.Panel();
            this.MessageOKButton = new System.Windows.Forms.Button();
            this.PanelOkCancel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorTypeIconPictureBox)).BeginInit();
            this.PanelYesNoCancel.SuspendLayout();
            this.PanelYesNo.SuspendLayout();
            this.PanelOk.SuspendLayout();
            this.PanelOkCancel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ErrorTypeIconPictureBox
            // 
            this.ErrorTypeIconPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.ErrorTypeIconPictureBox.Location = new System.Drawing.Point(31, 13);
            this.ErrorTypeIconPictureBox.Name = "ErrorTypeIconPictureBox";
            this.ErrorTypeIconPictureBox.Size = new System.Drawing.Size(32, 32);
            this.ErrorTypeIconPictureBox.TabIndex = 17;
            this.ErrorTypeIconPictureBox.TabStop = false;
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.MessageLabel.Location = new System.Drawing.Point(76, 19);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(0, 14);
            this.MessageLabel.TabIndex = 21;
            // 
            // ErrorTypeIconList
            // 
            this.ErrorTypeIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ErrorTypeIconList.ImageStream")));
            this.ErrorTypeIconList.TransparentColor = System.Drawing.Color.Transparent;
            this.ErrorTypeIconList.Images.SetKeyName(0, "Asterisk");
            this.ErrorTypeIconList.Images.SetKeyName(1, "Hand");
            this.ErrorTypeIconList.Images.SetKeyName(2, "Question");
            this.ErrorTypeIconList.Images.SetKeyName(3, "Warning");
            // 
            // PanelYesNoCancel
            // 
            this.PanelYesNoCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelYesNoCancel.BackColor = System.Drawing.Color.Transparent;
            this.PanelYesNoCancel.Controls.Add(this.MessageCancelButton);
            this.PanelYesNoCancel.Controls.Add(this.MessageNoButton);
            this.PanelYesNoCancel.Controls.Add(this.MessageYesButton);
            this.PanelYesNoCancel.Location = new System.Drawing.Point(22, 77);
            this.PanelYesNoCancel.Name = "PanelYesNoCancel";
            this.PanelYesNoCancel.Size = new System.Drawing.Size(267, 41);
            this.PanelYesNoCancel.TabIndex = 26;
            // 
            // MessageCancelButton
            // 
            this.MessageCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.MessageCancelButton.AutoSize = true;
            this.MessageCancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.MessageCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MessageCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MessageCancelButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageCancelButton.ForeColor = System.Drawing.Color.White;
            this.MessageCancelButton.Location = new System.Drawing.Point(179, 7);
            this.MessageCancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.MessageCancelButton.Name = "MessageCancelButton";
            this.MessageCancelButton.Size = new System.Drawing.Size(80, 28);
            this.MessageCancelButton.TabIndex = 27;
            this.MessageCancelButton.Text = "&CANCEL";
            this.MessageCancelButton.UseVisualStyleBackColor = false;
            // 
            // MessageNoButton
            // 
            this.MessageNoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.MessageNoButton.AutoSize = true;
            this.MessageNoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.MessageNoButton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.MessageNoButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MessageNoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MessageNoButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageNoButton.ForeColor = System.Drawing.Color.White;
            this.MessageNoButton.Location = new System.Drawing.Point(93, 6);
            this.MessageNoButton.Margin = new System.Windows.Forms.Padding(4);
            this.MessageNoButton.Name = "MessageNoButton";
            this.MessageNoButton.Size = new System.Drawing.Size(80, 28);
            this.MessageNoButton.TabIndex = 26;
            this.MessageNoButton.Text = "&NO";
            this.MessageNoButton.UseVisualStyleBackColor = false;
            // 
            // MessageYesButton
            // 
            this.MessageYesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.MessageYesButton.AutoSize = true;
            this.MessageYesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.MessageYesButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.MessageYesButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MessageYesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MessageYesButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageYesButton.ForeColor = System.Drawing.Color.White;
            this.MessageYesButton.Location = new System.Drawing.Point(8, 6);
            this.MessageYesButton.Margin = new System.Windows.Forms.Padding(4);
            this.MessageYesButton.Name = "MessageYesButton";
            this.MessageYesButton.Size = new System.Drawing.Size(80, 28);
            this.MessageYesButton.TabIndex = 25;
            this.MessageYesButton.Text = "&YES";
            this.MessageYesButton.UseVisualStyleBackColor = false;
            // 
            // PanelYesNo
            // 
            this.PanelYesNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelYesNo.BackColor = System.Drawing.Color.Transparent;
            this.PanelYesNo.Controls.Add(this.button2);
            this.PanelYesNo.Controls.Add(this.button3);
            this.PanelYesNo.Location = new System.Drawing.Point(66, 71);
            this.PanelYesNo.Name = "PanelYesNo";
            this.PanelYesNo.Size = new System.Drawing.Size(180, 41);
            this.PanelYesNo.TabIndex = 27;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.No;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(93, 6);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 28);
            this.button2.TabIndex = 28;
            this.button2.Text = "&NO";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button3.AutoSize = true;
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(8, 6);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 28);
            this.button3.TabIndex = 27;
            this.button3.Text = "&YES";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // PanelOk
            // 
            this.PanelOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelOk.BackColor = System.Drawing.Color.Transparent;
            this.PanelOk.Controls.Add(this.MessageOKButton);
            this.PanelOk.Location = new System.Drawing.Point(215, 79);
            this.PanelOk.Name = "PanelOk";
            this.PanelOk.Size = new System.Drawing.Size(101, 41);
            this.PanelOk.TabIndex = 29;
            this.PanelOk.Visible = false;
            // 
            // MessageOKButton
            // 
            this.MessageOKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageOKButton.AutoSize = true;
            this.MessageOKButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.MessageOKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.MessageOKButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MessageOKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MessageOKButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageOKButton.ForeColor = System.Drawing.Color.White;
            this.MessageOKButton.Location = new System.Drawing.Point(10, 6);
            this.MessageOKButton.Margin = new System.Windows.Forms.Padding(4);
            this.MessageOKButton.Name = "MessageOKButton";
            this.MessageOKButton.Size = new System.Drawing.Size(80, 28);
            this.MessageOKButton.TabIndex = 27;
            this.MessageOKButton.Text = "&OK";
            this.MessageOKButton.UseVisualStyleBackColor = false;
            // 
            // PanelOkCancel
            // 
            this.PanelOkCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelOkCancel.BackColor = System.Drawing.Color.Transparent;
            this.PanelOkCancel.Controls.Add(this.button1);
            this.PanelOkCancel.Controls.Add(this.button4);
            this.PanelOkCancel.Location = new System.Drawing.Point(59, 76);
            this.PanelOkCancel.Name = "PanelOkCancel";
            this.PanelOkCancel.Size = new System.Drawing.Size(180, 41);
            this.PanelOkCancel.TabIndex = 30;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(93, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 28);
            this.button1.TabIndex = 28;
            this.button1.Text = "&Cancel";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button4.AutoSize = true;
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
            this.button4.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(8, 6);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 28);
            this.button4.TabIndex = 27;
            this.button4.Text = "&OK";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // ShowMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(312, 121);
            this.Controls.Add(this.PanelOk);
            this.Controls.Add(this.PanelOkCancel);
            this.Controls.Add(this.PanelYesNo);
            this.Controls.Add(this.ErrorTypeIconPictureBox);
            this.Controls.Add(this.PanelYesNoCancel);
            this.Controls.Add(this.MessageLabel);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.MessageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorTypeIconPictureBox)).EndInit();
            this.PanelYesNoCancel.ResumeLayout(false);
            this.PanelYesNoCancel.PerformLayout();
            this.PanelYesNo.ResumeLayout(false);
            this.PanelYesNo.PerformLayout();
            this.PanelOk.ResumeLayout(false);
            this.PanelOk.PerformLayout();
            this.PanelOkCancel.ResumeLayout(false);
            this.PanelOkCancel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ErrorTypeIconPictureBox;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.ImageList ErrorTypeIconList;
        private System.Windows.Forms.Panel PanelYesNoCancel;
        private System.Windows.Forms.Button MessageCancelButton;
        private System.Windows.Forms.Button MessageNoButton;
        private System.Windows.Forms.Button MessageYesButton;
        private System.Windows.Forms.Panel PanelYesNo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel PanelOk;
        private System.Windows.Forms.Button MessageOKButton;
        private System.Windows.Forms.Panel PanelOkCancel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
    }
}

