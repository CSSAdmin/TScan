// -------------------------------------------------------------------------------------------------
// <copyright file="F96000WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// ------------------------------------------------------------------------------------------------

namespace D91000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// Class F96000WorkItem
    /// </summary>
    public class F96000WorkItem : WorkItem
    {
        #region F96000 OwnerManagement

        #region GetOwnerDetails

        /// <summary>
        /// Gets the F96000_GetOwnerDetails
        /// It Returns two table[OwnerDetails,OwnerList]
        /// </summary>
        /// <param name="ownerID">ownerID</param>
        /// <returns>Type Dataset Returns two table[OwnerDetails,OwnerList]</returns>
        public F96000OwnerManagementData F96000_GetOwnerManagementDetails(int ownerID)
        {
            return WSHelper.F96000_GetOwnerManagementDetails(ownerID);
        }
   
        #endregion GetOwnerDetails 


        /// <summary>
        /// F96000_s the country combo details.
        /// </summary>
        /// <returns></returns>
        public F96000OwnerManagementData F96000_CountryComboDetails()
        {
            return WSHelper.F96000_CountryComboDetails();
        }
        #region List OwnerStatusType

        /// <summary>
        /// F96000_s the list OwnerStatusType.
        /// </summary>
        /// <returns></returns>
        public F96000OwnerManagementData.F96000ListOwnerStatusTypeDataTable F96000_ListOwnerStatusType()
        {
            return WSHelper.F96000_ListOwnerStatusType();
        } 
       

        #endregion

        #region Insert OwnerManagementDetails

        /// <summary>
        /// Inserts the F96000_OwnerManagementDetails
        /// </summary>
        /// <param name="ownerID">ownerID</param>
        /// <param name="ownerStatus">ownerStatus</param>
        /// <returns></returns>
        public int F96000_InsertOwnerManagementDetails(int ownerID,string ownerDetails, string ownerStatus,int userId)
        {
            return WSHelper.F96000_InsertOwnerManagementDetails(ownerID, ownerDetails, ownerStatus, userId);
        }       
        #endregion Insert OwnerManagementDetails


        #region DeleteData

        public void F96000_DeleteData(int statusId)
        {
           WSHelper.F96000_DeleteData(statusId);
        }



        #endregion

        #endregion F96000 OwnerManagement


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

        #region SliceEvents
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
        #endregion SliceEvents




     

    }
}
