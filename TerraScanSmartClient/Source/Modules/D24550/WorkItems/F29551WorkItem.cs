namespace D24550
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

    public class F29551WorkItem : WorkItem
    {
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
        
        /// <summary>
        /// F2550_s the list parcel details.
        /// </summary>
        /// <param name="parcelID">The parcel ID.</param>
        /// <returns>Typed DataSet</returns>
        public F2550TaxRollCorrectionData F2550_ListParcelDetails(string parcelID, string scheduleID, string stateID, string centralXmlIds)
        {
            return WSHelper.F2550_ListParcelDetails(parcelID, scheduleID, stateID,centralXmlIds);
        }

        /// <summary>
        /// DataSet to populate combo values
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <returns>DataSet to populate combos</returns>
        public F29551ParcelSaleTrackingData F29551_GetParcelSaleComboDetails(int userId)
        {
            return WSHelper.F29551_GetParcelSaleComboDetails(userId);
        }
        
        /// <summary>
        /// DataSet to Populate Grid and other controls
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User ID</param>
        /// <returns>DataSet to populate Controls</returns>
        public F29551ParcelSaleTrackingData F29551_GetParcelSaleTrackingDetails(int eventId, int userId)
        {
            return WSHelper.F29551_GetParcelSaleTrackingDetails(eventId, userId);
        }

        /// <summary>
        /// Data to populate Owner Grid
        /// </summary>
        /// <param name="saleId">The Sale Id</param>
        /// <param name="ownerId">The Owner Id</param>
        /// <param name="parcelId">The Parcel Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Owner Details DataSet</returns>
        public F29551ParcelSaleTrackingData F29551_GetOwnerDetails(int? saleId, int? ownerId, int? parcelId, int userId)
        {
            return WSHelper.F29551_GetOwnerDetails(saleId, ownerId, parcelId, userId);
        }

        /// <summary>
        /// Save ParcelSale Details
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="saleItems">saleItems</param>
        /// <param name="parcelItems">parcelItems</param>
        /// <param name="ownerItems">ownerItems</param>
        /// <param name="userId">userId</param>
        /// <returns>integer value</returns>
        public int F29551_SaveParcelSaleDetails(int eventId, string saleItems, string parcelItems, string ownerItems, int userId)
        {
            return WSHelper.F29551_SaveParcelSaleDetails(eventId, saleItems, parcelItems, ownerItems, userId);
        }

        /// <summary>
        /// Parcel and Owner details
        /// </summary>
        /// <param name="parcelId">The Parcel Id</param>
        /// <param name="parcelCollection">Parcel Collections</param>
        /// <param name="saleId">The Sale Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Parcel and Owner details</returns>
        public F29551ParcelSaleTrackingData F29551_GetParcelOwnerDetails(int? parcelId, string parcelCollection, int? saleId, int userId)
        {
            return WSHelper.F29551_GetParcelOwnerDetails(parcelId, parcelCollection, saleId, userId);
        }

        /// <summary>
        /// Create Sale Versions
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User Id</param>
        /// <param name="checkedParcels">Checked Parcels List</param>
        /// <returns>Message returned from SP</returns>
        public string F29551_CreateSaleVersions(int eventId, int userId, string checkedParcels)
        {
            return WSHelper.F29551_CreateSaleVersions(eventId, userId, checkedParcels);
        }

        /// <summary>
        /// Transfer Ownership Details
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Message returned from SP</returns>
        public string F29551_TransferOwnership(int eventId, int userId)
        {
            return WSHelper.F29551_TransferOwnership(eventId, userId);
        }

        /// <summary>
        /// F29551_s the update sale parcel.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Message returned from SP</returns>
        public string F29551_UpdateSaleParcel(int eventId, int userId)
        {
            return WSHelper.F29551_UpdateSaleParcel(eventId, userId);
        }

        /// <summary>
        /// F1403_s the get sale tracking roll year.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns></returns>
        public  F1403ParcelSearch F1403_GetSaleTrackingRollYear(int eventID)
        {
            return WSHelper.F1403_GetSaleTrackingRollYear(eventID);
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
    }
}
