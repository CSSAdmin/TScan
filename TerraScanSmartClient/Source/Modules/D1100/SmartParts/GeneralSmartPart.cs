//--------------------------------------------------------------------------------------------
// <copyright file="GeneralSmartPart.cs" company="Congruent">
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
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// GeneralSmartPart class file
    /// </summary>
    public partial class GeneralSmartPart : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GeneralSmartPart"/> class.
        /// </summary>
        public GeneralSmartPart()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Event Publication for ShowDistrictSelectorForm
        /// </summary>
        [EventPublication("topic://TerraScan.SmartParts/DemoSmartPart/ShowDistrictSelectorForm", PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> ShowDistrictSelectorForm;

        /// <summary>
        /// Handles the Click event of the DistrictPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictPictureBox_Click(object sender, EventArgs e)
        {
            this.ShowDistrictSelectorForm(this, new DataEventArgs<string>("0"));
        }       
    }
}
