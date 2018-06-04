//--------------------------------------------------------------------------------------------
// <copyright file="F8044WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8044WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Oct 06        JYOTHI              Created
//*********************************************************************************/
namespace D8000
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
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F8044WorkItem
    /// </summary>
    public class F8044WorkItem : WorkItem
    {
        /// <summary>
        /// Lists the Inspection Details
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Inspection Details</returns>
        public F8044MaterialsData F8044_ListMaterialDetails(int formId, int keyId)
        {
            return WSHelper.F8044_ListMaterialDetails(formId, keyId);
        }

        /// <summary>
        /// Lists the Inspection Details
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Inspection Details</returns>
        public F8044MaterialsData F8044_ListMaterialsResourceType(int flagActiveAndAll, int evenId)
        {
            return WSHelper.F8044_ListMaterialsResourceType(flagActiveAndAll, evenId);
        }

        /// <summary>
        /// F8040_s the list time information.
        /// </summary>
        /// <param name="isactive">The active value</param>
        /// <returns>ListTimeDataTable</returns>
        public F8040TimeData F8040_ListTimeResourceInformation(int isactive)
        {
            return WSHelper.F8040_ListTimeResourceInformation(isactive);
        }

        /// <summary>
        /// Save the Material Details
        /// </summary>
        /// <param name="materialItems">The material items.</param>
        public void F8044_SaveMaterialDetails(string materialItems,int userID)
        {
            WSHelper.F8044_SaveMaterialDetails(materialItems, userID);
        }

        /// <summary>
        /// Updates the Material Details
        /// </summary>
        /// <param name="materialItems">The material items.</param>
        public void F8044_UpdateMaterialDetails(string materialItems,int userID)
        {
            WSHelper.F8044_UpdateMaterialDetails(materialItems, userID);
        }

        /// <summary>
        /// Deletes the Material Item
        /// </summary>
        /// <param name="materialId">The material id.</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public int F8044_DeleteMaterialItem(int materialId,int userID)
        {
            return WSHelper.F8044_DeleteMaterialItem(materialId, userID);
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
        /// F8040_s the check event id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns></returns>
        public int F8044_CheckEventId(int formId, int keyId)
        {
            return WSHelper.F8040_CheckEventId(formId, keyId);
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
