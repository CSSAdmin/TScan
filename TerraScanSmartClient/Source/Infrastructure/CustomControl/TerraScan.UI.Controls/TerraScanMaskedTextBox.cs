// -------------------------------------------------------------------------------------------------
// <copyright file="TerraScanMaskedTextBox.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// User Control 
// </summary>
// Created by thilak raj on 17-Aug-2006

namespace TerraScan.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;

    /// <summary>
    /// TerraScanMaskedTextBox
    /// </summary>
    public class TerraScanMaskedTextBox : System.Windows.Forms.MaskedTextBox
    {
        /// <summary>
        /// boolean for checking focuscolor
        /// </summary>
        private bool focusColor;

        /// <summary>
        /// set the textbox Back Color
        /// </summary>
        private System.Drawing.Color textBoxBackColor = new System.Drawing.Color();

        /// <summary>
        /// set the Parent Back Color
        /// </summary>
        private System.Drawing.Color panelBackColor = new System.Drawing.Color();

        /// <summary>
        /// Sets the textBoxForeColor
        /// </summary>
        private System.Drawing.Color textBoxForeColor = new System.Drawing.Color();

        /// <summary>
        /// set textboxcolor
        /// </summary>
        private System.Drawing.Color textBoxColor = new System.Drawing.Color();

        /// <summary>
        /// boolean for checking Parent focuscolor
        /// </summary>
        private bool applyParentFocusColor = true;

        /// <summary>
        /// create a tooltip
        /// </summary>
        private ToolTip textToolTip = new ToolTip();

        /// <summary>
        /// TerraScanMaskedTextBox default constructor
        /// </summary>
        public TerraScanMaskedTextBox()
        {
            this.Enter += new System.EventHandler(this.OnEnter);
            this.Leave += new System.EventHandler(this.OnLeave);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [apply focus color].
        /// </summary>
        /// <value><c>true</c> if [apply focus color]; otherwise, <c>false</c>.</value>
        public bool ApplyFocusColor
        {
            set
            {
                this.focusColor = value;
            }

            get
            {
                return this.focusColor;
            }
        }

        /// <summary>
        /// Gets or sets the color of the set focus.
        /// </summary>
        /// <value>The color of the set focus.</value>
        public System.Drawing.Color SetFocusColor
        {
            get
            {
                return this.textBoxColor;
            }

            set
            {
                this.textBoxColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [apply focus color].
        /// </summary>
        /// <value><c>true</c> if [apply focus color]; otherwise, <c>false</c>.</value>
        public bool ApplyParentFocusColor
        {
            set
            {
                this.applyParentFocusColor = value;
            }

            get
            {
                return this.applyParentFocusColor;
            }
        }

        /// <summary>
        /// Method used to set focus color for control on enter event
        /// </summary>
        /// <param name="sender">Masktextbox</param>
        /// <param name="e">enter</param>
        private void OnEnter(object sender, EventArgs e)
        {
            TerraScanMaskedTextBox terraScanMaskedTextBox = (TerraScanMaskedTextBox)sender;

            if (this.ApplyFocusColor)
            {
                this.textBoxBackColor = terraScanMaskedTextBox.BackColor;
                this.textBoxForeColor = terraScanMaskedTextBox.ForeColor;
                terraScanMaskedTextBox.BackColor = this.SetFocusColor;
                terraScanMaskedTextBox.ForeColor = System.Drawing.Color.Black;
                if (this.ApplyParentFocusColor == true)
                {
                    if (this.Parent.GetType() == typeof(System.Windows.Forms.Panel))
                    {
                        Panel parentPanel = (Panel)this.Parent;
                        this.panelBackColor = parentPanel.BackColor;
                        parentPanel.BackColor = this.SetFocusColor;
                    }
                }
            }

            this.SelectAll();
        }

        /// <summary>
        /// BackGround Color Change
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">click</param>
        private void OnLeave(object sender, EventArgs e)
        {
            TerraScanMaskedTextBox terraScanMaskedTextBox = (TerraScanMaskedTextBox)sender;
            if (this.ApplyFocusColor)
            {
                terraScanMaskedTextBox.BackColor = this.textBoxBackColor;
                terraScanMaskedTextBox.ForeColor = this.textBoxForeColor;
                if (this.ApplyParentFocusColor == true)
                {
                    if (this.Parent.GetType() == typeof(System.Windows.Forms.Panel))
                    {
                        Panel parentPanel = (Panel)this.Parent;
                        parentPanel.BackColor = this.panelBackColor;
                    }
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            if (!this.Multiline)
            {
                string tempValue = string.Empty;
                ////this.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                tempValue = this.Text;
                

                Graphics graphics = this.CreateGraphics();
                SizeF sizeF = graphics.MeasureString(tempValue, this.Font);
                float preferredwidth = sizeF.Width;

                if (preferredwidth > this.Width)
                {
                    this.textToolTip.RemoveAll();
                    this.textToolTip.SetToolTip(this, tempValue);
                }
                else
                {
                    this.textToolTip.RemoveAll();
                }

                graphics.Dispose();
            }

            base.OnMouseEnter(e);
        }
    }
}
