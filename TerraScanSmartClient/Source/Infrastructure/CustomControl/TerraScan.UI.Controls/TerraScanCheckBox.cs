// -------------------------------------------------------------------------------------------
// <copyright file="TerraScanCheckBox.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access TerraScanCheckBox related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------
namespace TerraScan.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Configuration;       

    /// <summary>
    /// TerraScanCheckBox class file
    /// </summary>
    public partial class TerraScanCheckBox : System.Windows.Forms.CheckBox
    {
        #region Variable

        /// <summary>
        /// set the Parent Back Color
        /// </summary>
        private System.Drawing.Color panelBackColor = new System.Drawing.Color();

        /// <summary>
        /// set the Parent Back Color
        /// </summary>
        private System.Drawing.Color checkBoxBackColor = new System.Drawing.Color();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerraScanCheckBox"/> class.
        /// </summary>
        public TerraScanCheckBox()
        {
            // InitializeComponent();
            this.Enter += new System.EventHandler(this.OnEnter);
            this.Leave += new System.EventHandler(this.OnLeave);
        }

        #endregion

        /// <summary>
        /// Raises the paint event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(e);
        }

        #region Private Events
        /// <summary>
        /// Called when [leave].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OnLeave(object sender, EventArgs e)
        {
            this.BackColor = this.checkBoxBackColor;
            if (this.Parent.GetType() == typeof(System.Windows.Forms.Panel))
            {
                Panel parentPanel = (Panel)this.Parent;
                parentPanel.BackColor = this.panelBackColor;
            }
        }

        /// <summary>
        /// Called when [enter].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OnEnter(object sender, EventArgs e)
        {
            Color color = Color.FromArgb(Convert.ToInt32(Strings.ActiveControlBackRedColor), Convert.ToInt32(Strings.ActiveControlBackGreenColor), Convert.ToInt32(Strings.ActiveControlBackBlueColor));

           this.checkBoxBackColor = this.BackColor;
            this.BackColor = color;

            if (this.Parent.GetType() == typeof(System.Windows.Forms.Panel))
            {
                Panel parentPanel = (Panel)this.Parent;
                this.panelBackColor = parentPanel.BackColor;
                parentPanel.BackColor = color;
            }
        }

        #endregion
    }
}
