//--------------------------------------------------------------------------------------------
// <copyright file="F8040WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8040WorkItem.
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
    /// F8040WorkItem
    /// </summary>
    public class F8040WorkItem : WorkItem
    {
        #region List Time and Resource

        /// <summary>
        /// List The TypeStatus
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <param name="featureId">The feature id.</param>
        /// <returns>
        /// The dataset containing Details Evetn Type and status
        /// </returns>
        public F8040TimeData F8040_ListTimeInformation(int formId, int keyId)
        {
            return WSHelper.F8040_ListTimeInformation(formId, keyId);
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
        #endregion

        #region Save
        /// <summary>
        /// F8040_s the save time.
        /// </summary>
        /// <param name="timeDetails">The time details.</param>
        public void F8040_SaveTime(string timeDetails,int userID)
        {
            WSHelper.F8040_SaveTime(timeDetails, userID);
        }
        #endregion Save


        #region Update
        /// <summary>
        /// F8040_s the update time.
        /// </summary>
        /// <param name="timeDetails">The time details.</param>
        public void F8040_UpdateTime(string timeDetails,int userID)
        {
            WSHelper.F8040_UpdateTime(timeDetails,userID);
        }
        #endregion Update

        #region Delete
        /// <summary>
        /// F8040_s the delete time.
        /// </summary>
        /// <param name="timeId">The time id.</param>
        public void F8040_DeleteTime(int timeId,int userID)
        {
            WSHelper.F8040_DeleteTime(timeId,userID);
        }
        #endregion Delete
        
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
        public int F8040_CheckEventId(int formId, int keyId)
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
