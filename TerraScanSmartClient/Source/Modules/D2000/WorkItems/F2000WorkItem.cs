//--------------------------------------------------------------------------------------------
// <copyright file="F2000WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1031WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 08 May 07        Sam K              Created
//*********************************************************************************/
namespace D2000
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
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    public class F2000WorkItem :WorkItem 
    {
        #region WorkItemEvents

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

        #endregion WorkItemEvents

        #region List Parcel Status
        /// <summary>
        /// To get the Parcel status Data Table
        /// </summary>
        /// <param name="parcelID"></param>
        /// <returns>Returns the Parcel status Data Table</returns>
        public F2000ParcelStatusData.ListParcelStatusDataTableDataTable F2000_ListParcelStatus(int parcelID)
        {
            return WSHelper.F2000_ListParcelStatus(parcelID);  
        }
		 
	    #endregion

        #region F2000 Delete Parcel

        /// <summary>
        /// To Dealte ParcelId
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        public void F2000_DeleteParcelStatus(int parcelId, int userId)
        {
            WSHelper.F2000_DeleteParcelStatus(parcelId, userId);
        }

        #endregion

        #region F2000 Update parcel

        /// <summary>
        /// Update ParcelDetails For a ParcelID
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <param name="description">description</param>
        /// <param name="isActive">isActive</param>
        /// <param name="isExempt">isExempt</param>
        /// <param name="isOwnerPrimary">isOwnerPrimary</param>
        /// <returns>It Returns a Updated ParcelID</returns>
        public int F2000_UpdateParcelStatus(int parcelId, string description, string parcelType, int isExempt, int isOwnerPrimary, int userId)
        {
            return WSHelper.F2000_UpdateParcelStatus(parcelId, description, parcelType, isExempt, isOwnerPrimary, userId);            
        }

        #endregion 




        #region Get Form Permission Details

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

        #endregion Get Form Permission Details

        #region ListParcelType

        /// <summary>
        /// Gets the details of F2000 ParcelType
        /// </summary>
        /// <returns>Typed Dataset</returns>
        public F2000ParcelStatusData.f2000ListParcelTypeDataTable GetParcelType()
        {
            return WSHelper.GetParcelType();
        }

        public string ListRecordLockStatus(int formNo, int keyId)
        {
            return WSHelper.ListRecordLockStatus(formNo, keyId);
        }
        #endregion
    }
}
