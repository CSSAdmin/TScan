//--------------------------------------------------------------------------------------------
// <copyright file="ExciseTaxActionButtons.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.SmartParts;

    /// <summary>
    /// ExciseTaxActionButtons class file
    /// </summary>
    [SmartPart]
    public partial class ExciseTaxActionButtons : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExciseTaxActionButtons"/> class.
        /// </summary>
        public ExciseTaxActionButtons()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the WorkQueueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkQueueButton_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the AffidavitFormButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AffidavitFormButton_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the ExciseRatesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExciseRatesButton_Click(object sender, EventArgs e)
        {
        }
    }
}
