//--------------------------------------------------------------------------------------------
// <copyright file="F8106.Designer.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8106.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Oct 06        Automatic Creation              Created
//*********************************************************************************/
namespace D8106
{
    /// <summary>
    /// Designer Class
    /// </summary>
    public partial class F8106
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8106));
            this.RiskManagerDatePanel = new System.Windows.Forms.Panel();
            this.RiskManagerDateTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.RiskManagerDateLabel = new System.Windows.Forms.Label();
            this.RiskManagerDatePict = new System.Windows.Forms.Button();
            this.RiskManagerDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.DamagePanel = new System.Windows.Forms.Panel();
            this.DamageCheckBox = new TerraScan.UI.Controls.TerraScanCheckBox();
            this.DamageLable = new System.Windows.Forms.Label();
            this.PicturesPanel = new System.Windows.Forms.Panel();
            this.PicturesCheckBox = new TerraScan.UI.Controls.TerraScanCheckBox();
            this.PicturesLabel = new System.Windows.Forms.Label();
            this.EmptyPanel = new System.Windows.Forms.Panel();
            this.StoppageEventPictureBox = new System.Windows.Forms.PictureBox();
            this.stoppageEventTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.RiskManagerDatePanel.SuspendLayout();
            this.DamagePanel.SuspendLayout();
            this.PicturesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StoppageEventPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // RiskManagerDatePanel
            // 
            this.RiskManagerDatePanel.BackColor = System.Drawing.Color.Transparent;
            this.RiskManagerDatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RiskManagerDatePanel.Controls.Add(this.RiskManagerDateTextBox);
            this.RiskManagerDatePanel.Controls.Add(this.RiskManagerDateLabel);
            this.RiskManagerDatePanel.Controls.Add(this.RiskManagerDatePict);
            this.RiskManagerDatePanel.Controls.Add(this.RiskManagerDateTimePicker);
            this.RiskManagerDatePanel.Location = new System.Drawing.Point(0, 0);
            this.RiskManagerDatePanel.Name = "RiskManagerDatePanel";
            this.RiskManagerDatePanel.Size = new System.Drawing.Size(274, 37);
            this.RiskManagerDatePanel.TabIndex = 1;
            this.RiskManagerDatePanel.TabStop = true;
            // 
            // RiskManagerDateTextBox
            // 
            this.RiskManagerDateTextBox.AllowClick = true;
            this.RiskManagerDateTextBox.AllowNegativeSign = false;
            this.RiskManagerDateTextBox.ApplyCFGFormat = false;
            this.RiskManagerDateTextBox.ApplyCurrencyFormat = false;
            this.RiskManagerDateTextBox.ApplyFocusColor = true;
            this.RiskManagerDateTextBox.ApplyParentFocusColor = true;
            this.RiskManagerDateTextBox.ApplyTimeFormat = false;
            this.RiskManagerDateTextBox.BackColor = System.Drawing.Color.White;
            this.RiskManagerDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RiskManagerDateTextBox.CFromatWihoutSymbol = false;
            this.RiskManagerDateTextBox.CheckForEmpty = false;
            this.RiskManagerDateTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RiskManagerDateTextBox.Digits = -1;
            this.RiskManagerDateTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.RiskManagerDateTextBox.IsEditable = true;
            this.RiskManagerDateTextBox.IsQueryableFileld = true;
            this.RiskManagerDateTextBox.Location = new System.Drawing.Point(9, 15);
            this.RiskManagerDateTextBox.LockKeyPress = false;
            this.RiskManagerDateTextBox.MaxLength = 10;
            this.RiskManagerDateTextBox.Name = "RiskManagerDateTextBox";
            this.RiskManagerDateTextBox.PersistDefaultColor = false;
            this.RiskManagerDateTextBox.Precision = 2;
            this.RiskManagerDateTextBox.QueryingFileldName = "";
            this.RiskManagerDateTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.RiskManagerDateTextBox.Size = new System.Drawing.Size(143, 16);
            this.RiskManagerDateTextBox.SpecialCharacter = "%";
            this.RiskManagerDateTextBox.TabIndex = 2;
            this.RiskManagerDateTextBox.TextCustomFormat = "";
            this.RiskManagerDateTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Date;
            this.RiskManagerDateTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RiskManagerDateTextBox_KeyPress);
            this.RiskManagerDateTextBox.TextChanged += new System.EventHandler(this.RiskManagerDateTextBox_TextChanged);
            // 
            // RiskManagerDateLabel
            // 
            this.RiskManagerDateLabel.AutoSize = true;
            this.RiskManagerDateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RiskManagerDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.RiskManagerDateLabel.Location = new System.Drawing.Point(1, -1);
            this.RiskManagerDateLabel.Name = "RiskManagerDateLabel";
            this.RiskManagerDateLabel.Size = new System.Drawing.Size(168, 14);
            this.RiskManagerDateLabel.TabIndex = 0;
            this.RiskManagerDateLabel.Tag = "";
            this.RiskManagerDateLabel.Text = "Date Copied to Risk Manager:";
            // 
            // RiskManagerDatePict
            // 
            this.RiskManagerDatePict.FlatAppearance.BorderSize = 0;
            this.RiskManagerDatePict.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RiskManagerDatePict.Image = ((System.Drawing.Image)(resources.GetObject("RiskManagerDatePict.Image")));
            this.RiskManagerDatePict.Location = new System.Drawing.Point(243, 9);
            this.RiskManagerDatePict.Name = "RiskManagerDatePict";
            this.RiskManagerDatePict.Size = new System.Drawing.Size(20, 24);
            this.RiskManagerDatePict.TabIndex = 3;
            this.RiskManagerDatePict.Tag = "ReceiptDateTextBox";
            this.RiskManagerDatePict.UseVisualStyleBackColor = true;
            this.RiskManagerDatePict.Click += new System.EventHandler(this.RiskManagerDatePict_Click);
            // 
            // RiskManagerDateTimePicker
            // 
            this.RiskManagerDateTimePicker.CustomFormat = "MM/dd/yyyy";
            this.RiskManagerDateTimePicker.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RiskManagerDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.RiskManagerDateTimePicker.Location = new System.Drawing.Point(245, 11);
            this.RiskManagerDateTimePicker.Margin = new System.Windows.Forms.Padding(0);
            this.RiskManagerDateTimePicker.MaxDate = new System.DateTime(2075, 12, 31, 0, 0, 0, 0);
            this.RiskManagerDateTimePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.RiskManagerDateTimePicker.Name = "RiskManagerDateTimePicker";
            this.RiskManagerDateTimePicker.Size = new System.Drawing.Size(10, 20);
            this.RiskManagerDateTimePicker.TabIndex = 8;
            this.RiskManagerDateTimePicker.TabStop = false;
            this.RiskManagerDateTimePicker.CloseUp += new System.EventHandler(this.RiskManagerDateTimePicker_CloseUp);
            this.RiskManagerDateTimePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RiskManagerDateTimePicker_KeyPress);
            // 
            // DamagePanel
            // 
            this.DamagePanel.BackColor = System.Drawing.Color.Transparent;
            this.DamagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DamagePanel.Controls.Add(this.DamageCheckBox);
            this.DamagePanel.Controls.Add(this.DamageLable);
            this.DamagePanel.Location = new System.Drawing.Point(273, 0);
            this.DamagePanel.Name = "DamagePanel";
            this.DamagePanel.Size = new System.Drawing.Size(112, 37);
            this.DamagePanel.TabIndex = 4;
            // 
            // DamageCheckBox
            // 
            this.DamageCheckBox.AutoSize = true;
            this.DamageCheckBox.Location = new System.Drawing.Point(87, 17);
            this.DamageCheckBox.Name = "DamageCheckBox";
            this.DamageCheckBox.Size = new System.Drawing.Size(15, 14);
            this.DamageCheckBox.TabIndex = 5;
            this.DamageCheckBox.UseVisualStyleBackColor = true;
            this.DamageCheckBox.CheckedChanged += new System.EventHandler(this.DamageCheckBox_CheckedChanged);
            // 
            // DamageLable
            // 
            this.DamageLable.AutoSize = true;
            this.DamageLable.BackColor = System.Drawing.Color.Transparent;
            this.DamageLable.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DamageLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.DamageLable.Location = new System.Drawing.Point(1, -1);
            this.DamageLable.Name = "DamageLable";
            this.DamageLable.Size = new System.Drawing.Size(54, 14);
            this.DamageLable.TabIndex = 0;
            this.DamageLable.Text = "Damage:";
            // 
            // PicturesPanel
            // 
            this.PicturesPanel.BackColor = System.Drawing.Color.Transparent;
            this.PicturesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicturesPanel.Controls.Add(this.PicturesCheckBox);
            this.PicturesPanel.Controls.Add(this.PicturesLabel);
            this.PicturesPanel.Location = new System.Drawing.Point(384, 0);
            this.PicturesPanel.Name = "PicturesPanel";
            this.PicturesPanel.Size = new System.Drawing.Size(112, 37);
            this.PicturesPanel.TabIndex = 6;
            // 
            // PicturesCheckBox
            // 
            this.PicturesCheckBox.AutoSize = true;
            this.PicturesCheckBox.Location = new System.Drawing.Point(87, 17);
            this.PicturesCheckBox.Name = "PicturesCheckBox";
            this.PicturesCheckBox.Size = new System.Drawing.Size(15, 14);
            this.PicturesCheckBox.TabIndex = 7;
            this.PicturesCheckBox.UseVisualStyleBackColor = true;
            this.PicturesCheckBox.CheckedChanged += new System.EventHandler(this.PicturesCheckBox_CheckedChanged);
            // 
            // PicturesLabel
            // 
            this.PicturesLabel.AutoSize = true;
            this.PicturesLabel.BackColor = System.Drawing.Color.Transparent;
            this.PicturesLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PicturesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.PicturesLabel.Location = new System.Drawing.Point(1, -1);
            this.PicturesLabel.Name = "PicturesLabel";
            this.PicturesLabel.Size = new System.Drawing.Size(56, 14);
            this.PicturesLabel.TabIndex = 0;
            this.PicturesLabel.Text = "Pictures:";
            // 
            // EmptyPanel
            // 
            this.EmptyPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.EmptyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmptyPanel.Location = new System.Drawing.Point(494, 0);
            this.EmptyPanel.Name = "EmptyPanel";
            this.EmptyPanel.Size = new System.Drawing.Size(274, 37);
            this.EmptyPanel.TabIndex = 11;
            // 
            // StoppageEventPictureBox
            // 
            this.StoppageEventPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("StoppageEventPictureBox.Image")));
            this.StoppageEventPictureBox.Location = new System.Drawing.Point(761, 0);
            this.StoppageEventPictureBox.Name = "StoppageEventPictureBox";
            this.StoppageEventPictureBox.Size = new System.Drawing.Size(42, 37);
            this.StoppageEventPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StoppageEventPictureBox.TabIndex = 10;
            this.StoppageEventPictureBox.TabStop = false;
            this.StoppageEventPictureBox.MouseEnter += new System.EventHandler(this.StoppageEventPictureBox_MouseEnter);
            // 
            // F8106
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.EmptyPanel);
            this.Controls.Add(this.PicturesPanel);
            this.Controls.Add(this.DamagePanel);
            this.Controls.Add(this.RiskManagerDatePanel);
            this.Controls.Add(this.StoppageEventPictureBox);
            this.Name = "F8106";
            this.Size = new System.Drawing.Size(804, 37);
            this.Tag = "8106";
            this.Load += new System.EventHandler(this.F8106_Load);
            this.RiskManagerDatePanel.ResumeLayout(false);
            this.RiskManagerDatePanel.PerformLayout();
            this.DamagePanel.ResumeLayout(false);
            this.DamagePanel.PerformLayout();
            this.PicturesPanel.ResumeLayout(false);
            this.PicturesPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StoppageEventPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region Fields
        /// <summary>
        /// Panel to put Date TextBox
        /// </summary>
        private System.Windows.Forms.Panel RiskManagerDatePanel;

        /// <summary>
        /// Text Box
        /// </summary>
        private TerraScan.UI.Controls.TerraScanTextBox RiskManagerDateTextBox;

        /// <summary>
        /// Label
        /// </summary>
        private System.Windows.Forms.Label RiskManagerDateLabel;

        /// <summary>
        /// PictureBox to have an image
        /// </summary>
        private System.Windows.Forms.Button RiskManagerDatePict;

        /// <summary>
        /// Datetime picker (Calendar control)
        /// </summary>
        private System.Windows.Forms.DateTimePicker RiskManagerDateTimePicker;

        /// <summary>
        /// Panel
        /// </summary>
        private System.Windows.Forms.Panel DamagePanel;

        /// <summary>
        /// Checkbox
        /// </summary>
        private TerraScan.UI.Controls.TerraScanCheckBox DamageCheckBox;

        /// <summary>
        /// Label
        /// </summary>
        private System.Windows.Forms.Label DamageLable;

        /// Panel
        /// </summary>
        private System.Windows.Forms.Panel PicturesPanel;

        /// <summary>
        /// Check Box
        /// </summary>
        private TerraScan.UI.Controls.TerraScanCheckBox PicturesCheckBox;

        /// <summary>
        /// Label
        /// </summary>
        private System.Windows.Forms.Label PicturesLabel;

        /// <summary>
        /// Empty Panel (Just for Look and feel)
        /// </summary>
        private System.Windows.Forms.Panel EmptyPanel;

        /// <summary>
        /// Picturebox for an image
        /// </summary>
        private System.Windows.Forms.PictureBox StoppageEventPictureBox;

        /// <summary>
        /// Tool tip for display
        /// </summary>
        private System.Windows.Forms.ToolTip stoppageEventTooltip;
        #endregion Fields

    }
}
