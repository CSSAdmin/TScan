//--------------------------------------------------------------------------------------------
// <copyright file="F8058WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8058WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
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
    /// Class F8058WorkItem
    /// </summary>
    public class F8058WorkItem : WorkItem
    {
        #region List ResourceConfig

        /// <summary>
        /// F8058_s the list resources config details.
        /// </summary>
        /// <returns></returns>
        public F8058ResourcesConfigData F8058_ListResourcesConfigDetails()
        {
            return WSHelper.F8058_ListResourcesConfigDetails();
        }

        #endregion List ResourceConfig

        #region Insert ResourceConfig

        /// <summary>
        /// F8058_s the insert reosurces config details.
        /// </summary>
        /// <param name="equipmentId">The equipment id.</param>
        /// <param name="equiptResource">The equipt resource.</param>
        /// <param name="applicationId">The application id.</param>
        /// <returns></returns>
        public int F8058_InsertReosurcesConfigDetails(int equipmentId, string equiptResource, int applicationId, int userId)
        {
            return WSHelper.F8058_InsertResourcesConfigDetails(equipmentId, equiptResource, applicationId, userId);
        }

        #endregion Insert ResourceConfig

        #region Delete ResourceConfig

        /// <summary>
        /// F8058_s the delete resources config details.
        /// </summary>
        /// <param name="equipmentId">The equipment id.</param>
        public int F8058_DeleteResourcesConfigDetails(int equipmentId, int userId)
        {
             return WSHelper.F8058_DeleteResourcesConfigDetails(equipmentId, userId);
        }

        #endregion Delete ResourceConfig

        #region Get Form Slice Permission Details

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">userId</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        #endregion Get Form Slice Permission Details

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
