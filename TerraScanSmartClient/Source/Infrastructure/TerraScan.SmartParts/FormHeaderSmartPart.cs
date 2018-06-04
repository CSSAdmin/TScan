//--------------------------------------------------------------------------------------------
// <copyright file="FormHeaderSmartPart.cs" company="Congruent">
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
    /// FormHeaderSmartPart class file
    /// </summary>
    public partial class FormHeaderSmartPart : PrimaryBaseSmartPart
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FormHeaderSmartPart"/> class.
        /// </summary>
        public FormHeaderSmartPart()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Sets the form header.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, Thread = ThreadOption.UserInterface)]
        public void SetFormHeader(object sender, DataEventArgs<string[]> e)
        {
            this.formName.Text = e.Data[0];
            if (!string.IsNullOrEmpty(e.Data[0]))
            {
                this.formName.Text = e.Data[0];
            }

            if (!string.IsNullOrEmpty(e.Data[1]))
            {
                this.operationName.Text  = e.Data[1];
            }
        }
    }
}
