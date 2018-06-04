//--------------------------------------------------------------------------------------------
// <copyright file="F8060WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8060WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14 May 06        JYOTHI              Created
//*********************************************************************************/
namespace D8058
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
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F8060WorkItem
    /// </summary>
    public class F8060WorkItem : WorkItem
    {
        /// <summary>
        /// Lists the Parts Configuration details
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <returns>returns dataset contains Parts Configuration details</returns>
        public F8060PartsConfigData F8060_ListPartsConfig(int componentId)
        {
            return WSHelper.F8060_ListPartsConfig(componentId);
        }

        /// <summary>
        /// Lists the Components detail
        /// </summary>
        /// <returns>returns dataset contains Components details</returns>
        public F8060PartsConfigData F8060_ListComponents()
        {
            return WSHelper.F8060_ListComponents();
        }

        /// <summary>
        /// F8062_s the save Parts configuration.
        /// </summary>
        /// <param name="partId">The part id.</param>
        /// <param name="partsConfig">The parts config.</param>
        public void F8060_SavePartsConfiguration(int partId, string partsConfig,int userId)
        {
            WSHelper.F8060_SavePartsConfiguration(partId, partsConfig, userId);
        }

        /// <summary>
        /// Deletes the Parts Configuration.
        /// </summary>
        /// <param name="partId">The part id.</param>
        public int F8060_DeletePartsConfiguration(int partId, int userId)
        {
            return WSHelper.F8060_DeletePartsConfiguration(partId, userId);
        }

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">The user id.</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
