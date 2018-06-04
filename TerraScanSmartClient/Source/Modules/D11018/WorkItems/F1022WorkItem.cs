// -------------------------------------------------------------------------------------------------
// <copyright file="F1022WorkItem.cs" company="Congruent">
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
    /// F1022 WorkItem
    /// </summary>
    public class F1022WorkItem : WorkItem
    {
        /// <summary>
        /// List the Misc Receipt template
        /// </summary>
        /// <returns>
        /// The typed dataset containing the Misc Receipt Template
        /// </returns>
        public F11018MiscReceiptData F1022_ListMiscReceiptTemplate()
        {
            return WSHelper.F1022_ListMiscReceiptTemplate();
        }

        /// <summary>
        /// Deletes the Misc Receipt Template based on the miscTemplateId
        /// </summary>
        /// <param name="miscTemplateId">The misc template id.</param>
        //public void F1022_DeleteMiscReceiptTemplate(int miscTemplateId, int userId)
        //{
        //    WSHelper.F1022_DeleteMiscReceiptTemplate(miscTemplateId, userId);
        //}

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