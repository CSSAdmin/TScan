// -------------------------------------------------------------------------------------------------
// <copyright file="F29550WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D24550
{
    #region NameSpace

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

    #endregion NameSpace

    public class F29550WorkItem : WorkItem
    {
        #region ParcelSaleTracking
        /// <summary>
        /// F29550_GetParcelSaleTrackingDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>DataSet</returns>
        public F29550ParcelSaleTracking F29550_GetParcelSaleTrackingDetails(int eventId)
        {
            return WSHelper.F29550_GetParcelSaleTrackingDetails(eventId);
        }

        /// <summary>
        /// F29550_GetParcelSaleTrackingDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>DataSet</returns>
        public F29550ParcelSaleTracking F29550_GetPushOwner(int saleId)
        {
            return WSHelper.F29550_GetPushOwner(saleId);
        }


        /// <summary>
        /// F29550_GetParcelSaleTrackingComboDetails
        /// </summary>
        /// <returns>DataSet</returns>
        public F29550ParcelSaleTracking F29550_GetParcelSaleTrackingComboDetails()
        {
            return WSHelper.F29550_GetParcelSaleTrackingComboDetails();
        }

        public int F29550_saveParcelSaleDetails(int eventId, string saleItems, string parcelItems, string ownerItems, int userId)
        {
            return WSHelper.F29550_saveParcelSaleDetails(eventId, saleItems,parcelItems,ownerItems, userId);
        }
        /// <summary>
        /// F29550_GetParcelSaleTrackingComboDetails
        /// </summary>
        /// <returns>DataSet</returns>
        public F29550ParcelSaleTracking F29550_GetParcelDetails(string parcelIdDetails,int newParcelId,int saleId)
        {
            return WSHelper.F29550_GetParcelDetails(parcelIdDetails, newParcelId, saleId);
        }


        /// <summary>
        /// F29550_GetParcelsOwnerDetails
        /// </summary>
        /// <param name="parcelDetails">parcelDetails</param>
        /// <returns>DataSet</returns>
        public F29550ParcelSaleTracking F29550_GetParcelsOwnerDetails(string parcelDetails)
        {
            return WSHelper.F29550_GetParcelsOwnerDetails(parcelDetails);
        }

        #region OwnerSelection

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetOwnerDetails(int ownerId)
        {
            return WSHelper.F15010_GetOwnerDetails(ownerId);
        }
        #endregion OwnerSelection

        #endregion ParcelSaleTracking

        #region InsertSnapShotItems

        /// <summary>
        /// F9033_s the insert snap shot items.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="systemSnapShotXML">The system snap shot XML.</param>
        /// <returns></returns>
        public int F9033_InsertSnapShotItems(int? userId, string systemSnapShotXml)
        {
            return WSHelper.F9033_InsertSnapShotItems(userId, systemSnapShotXml);
        }

        #endregion InsertSnapShotItems

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
    }
}
