// -------------------------------------------------------------------------------------------------
// <copyright file="F1021WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Feb 07        Ranjani            Created// 
//*********************************************************************************/
namespace D11018
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F1021 WorkItem
    /// </summary>
    public class F1021WorkItem : WorkItem
    {
        /// <summary>
        /// saves the Misc Receipt template
        /// </summary>
        /// <param name="miscTemplateDetails">The misc template details.</param>
        /// <param name="templateItems">The template items.</param>
        /// <returns>
        /// new created templated id - return templatedid if succeed else return negative value
        /// </returns>
        public int F1021_SaveMiscReceiptTemplate(string miscTemplateDetails, string templateItems, int userId)
        {
            return WSHelper.F1021_SaveMiscReceiptTemplate(miscTemplateDetails, templateItems, userId);
        }   

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        } 
    }
}