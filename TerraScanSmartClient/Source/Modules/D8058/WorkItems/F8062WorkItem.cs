//--------------------------------------------------------------------------------------------
// <copyright file="F8062WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8062WorkItem.
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
    /// F8062WorkItem
    /// </summary>
    public class F8062WorkItem : WorkItem
    {
        /// <summary>
        /// F8062_s the list components configuration.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns>typed dataset containing the components configuration</returns>>
        public F8062ComponentsConfigData F8062_ListComponentsConfiguration(int applicationId)
        {
            return WSHelper.F8062_ListComponentsConfiguration(applicationId);
        }

        /// <summary>
        /// F8062_s the list feature class.
        /// </summary>
        /// <returns>typed dataset containing the Feature class</returns>
        public F8062ComponentsConfigData F8062_ListFeatureClass(int applicationId)
        {
            return WSHelper.F8062_ListFeatureClass(applicationId);
        }

        /// <summary>
        /// F8062_s the save components configuration.
        /// </summary>
        /// <param name="componentsConfig">The components config.</param>
        public void F8062_SaveComponentsConfiguration(string componentsConfig, int userId)
        {
            WSHelper.F8062_SaveComponentsConfiguration(componentsConfig, userId);
        }

        /// <summary>
        /// Deletes the Components Configuration.
        /// </summary>
        /// <param name="componentId">The component id.</param>
        public int F8062_DeleteComponentsConfiguration(int componentId, int userId)
        {
            return WSHelper.F8062_DeleteComponentsConfiguration(componentId, userId);
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
