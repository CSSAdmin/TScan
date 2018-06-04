// -------------------------------------------------------------------------------------------
// <copyright file="TerraScanComboBox.cs" company="Congruent">
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
    /// terrascan combobox custom control
    /// </summary>
    public class TerraScanComboBox : System.Windows.Forms.ComboBox
    {
        /// <summary>
        /// set the Parent Back Color
        /// </summary>
        private System.Drawing.Color panelBackColor = new System.Drawing.Color();

        /// <summary>
        /// set the Parent Back Color
        /// </summary>
        private System.Drawing.Color comboBackColor = new System.Drawing.Color();

        /// <summary>
        /// Tooltip displays text for the combo wherever needed
        /// </summary>
        private ToolTip comboToolTip = new ToolTip();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerraScanComboBox"/> class.
        /// </summary>
        public TerraScanComboBox()
        {
            this.Enter += new System.EventHandler(this.OnEnter);
            this.Leave += new System.EventHandler(this.OnLeave);
            this.MouseEnter += new System.EventHandler(this.OnMouseEnter);
        }

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
            this.BackColor = this.comboBackColor;
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
                this.comboBackColor = this.BackColor;
                this.BackColor = color;
                
            if (this.Parent.GetType() == typeof(System.Windows.Forms.Panel))
            {
                Panel parentPanel = (Panel)this.Parent;
                this.panelBackColor = parentPanel.BackColor;

                parentPanel.BackColor = color;
            }
        }

        /// <summary>
        /// Text of a combo is displayed when the text width exceeds controls width
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OnMouseEnter(object sender, EventArgs e)
        {
            Graphics graphics = this.CreateGraphics();
            SizeF widthHeight = graphics.MeasureString(this.Text.Trim(), this.Font);
            ////Width of an arrow in combo is 16
            if (widthHeight.Width > this.Width - 16)
            {
                this.comboToolTip.RemoveAll();
                this.comboToolTip.SetToolTip(this, this.Text.Trim());
            }
            else
            {
                this.comboToolTip.RemoveAll();
            }

            graphics.Dispose();

            ////Label Label = new Label();
            ////Label.AutoSize = true;
            ////Label.Font = this.Font;
            ////int i = Label.Width;
            ////Label.Text = this.Text;
            ////if (Label.Width > this.Width)
            ////{
            ////    //comboToolTip.RemoveAll();

            ////}
        }
        #endregion
    }
}
