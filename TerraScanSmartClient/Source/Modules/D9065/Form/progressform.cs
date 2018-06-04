// -------------------------------------------------------------------------------------------------
// <copyright file="progressform.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// progressform.cs
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D9065
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// class for progressform
    /// </summary>
    public partial class Progressform : Form
    {
        #region Variable

        /// <summary>
        /// detailValue
        /// </summary>
        private string detailValue = string.Empty;

        /// <summary>
        /// configValue
        /// </summary>
        private bool configValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Progressform
        /// </summary>
        public Progressform(bool value)
        {
            this.InitializeComponent();
            this.configValue = value;
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the progress status.
        /// </summary>
        /// <value>The progress status.</value>
        public string ProgressStatus
        {
            set
            {
                this.ProcessLabel.Text = value;
            }

            get
            {
                return this.ProcessLabel.Text;
            }
        }

        /// <summary>
        /// Gets or sets the process duration.
        /// </summary>
        /// <value>The process duration.</value>
        public string ProcessDuration
        {
            set
            {
                this.detailValue = value;
                this.DetailListBox.Items.Add(value);
            }

            get
            {
                return this.detailValue;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable button].
        /// </summary>
        /// <value><c>true</c> if [enable button]; otherwise, <c>false</c>.</value>
        public bool EnableButton
        {
            set
            {
                this.OkButton.Enabled = value;
                this.OkButton.Focus();
            }

            get
            {
                return this.OkButton.Enabled;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [form close].
        /// </summary>
        /// <value><c>true</c> if [form close]; otherwise, <c>false</c>.</value>
        public bool FormClose
        {
            set
            {
                this.Close();
            }

            get
            {
                return this.OkButton.Enabled;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable button].
        /// </summary>
        /// <value><c>true</c> if [enable button]; otherwise, <c>false</c>.</value>
        public bool DisableProgressBar
        {
            set
            {
                this.ProgressPictureBox.Visible = value;
            }

            get
            {
                return this.ProgressPictureBox.Visible;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the OkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        /// <summary>
        /// Progressform_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Progressform_Load(object sender, EventArgs e)
        {
            if (!configValue)
            {
                this.DetailListBox.Visible = false;
                this.Size = new Size(this.Size.Width, 107);
                this.OkButton.Location = new Point(this.OkButton.Location.X, 70);
                this.Show();
            }
        }

        #endregion
    }
}