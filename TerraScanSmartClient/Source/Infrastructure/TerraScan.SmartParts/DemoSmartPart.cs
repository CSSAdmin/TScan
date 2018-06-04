//--------------------------------------------------------------------------------------------
// <copyright file="DemoSmartPart.cs" company="Congruent">
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

namespace TerraScan.SmartParts
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
    using TerraScan.Common;

    /// <summary>
    /// DemoSmartPart class file
    /// </summary>
    public partial class DemoSmartPart : PrimaryBaseSmartPart
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DemoSmartPart"/> class.
        /// </summary>
        public DemoSmartPart()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Event Publication for SampleButtonClick
        /// </summary>
        [EventPublication("topic://TerraScan.SmartParts/DemoSmartPart/SampleButtonClick", PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> SampleButtonClick;

        /// <summary>
        /// Handles the Click event of the SampleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SampleButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SampleButtonClick(this, new DataEventArgs<string>(this.Tag.ToString().Trim()));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }        
    }
}
