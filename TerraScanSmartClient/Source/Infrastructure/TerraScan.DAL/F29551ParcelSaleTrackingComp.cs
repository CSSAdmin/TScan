// -------------------------------------------------------------------------------------------
// <copyright file="F29551ParcelSaleTrackingComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F29551ParcelSaleTrackingComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 21 June 2011     LathaMaheswari D    Created to implement Data Access For F29551
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;
    using System.Data;

    /// <summary>
    /// Data Access for F29551 Parcel Sale Tracking
    /// </summary>
    public static class F29551ParcelSaleTrackingComp
    {
        /// <summary>
        /// DataSet to populate combo values
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <returns>DataSet to populate combos</returns>
        public static F29551ParcelSaleTrackingData F29551_GetParcelSaleComboDetails(int userId)
        {
            F29551ParcelSaleTrackingData parcelSaleTracking = new F29551ParcelSaleTrackingData();
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet();
            ht.Add("@UserID", userId);
            string[] tableNames = new string[] 
            { 
                parcelSaleTracking.DeedType.TableName, 
                parcelSaleTracking.StateList.TableName, 
                parcelSaleTracking.Advisory.TableName, 
                parcelSaleTracking.Assignment.TableName,
                parcelSaleTracking.Status.TableName,
                parcelSaleTracking.Apprasiser.TableName,
                parcelSaleTracking.LocalQualification.TableName
            };

            Utility.LoadDataSet(parcelSaleTracking, "f29551_pclst_ParcelSaleTracking", ht, tableNames);
            return parcelSaleTracking;
        }

        /// <summary>
        /// DataSet to Populate Grid and other controls
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User ID</param>
        /// <returns>DataSet to populate Controls</returns>
        public static F29551ParcelSaleTrackingData F29551_GetParcelSaleTrackingDetails(int eventId, int userId)
        {
            F29551ParcelSaleTrackingData parcelSaleTracking = new F29551ParcelSaleTrackingData();
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet();
            ht.Add("@EventID", eventId);
            ht.Add("@UserID", userId);
            string[] tableNames = new string[] 
            { 
                parcelSaleTracking.SaleDetails.TableName, 
                parcelSaleTracking.ParcelDetails.TableName, 
                parcelSaleTracking.OwnerDetails.TableName, 
                parcelSaleTracking.ReturnMessage.TableName,
                parcelSaleTracking.ValidRecord.TableName
            };

            Utility.LoadDataSet(parcelSaleTracking, "f29551_pcget_ParcelSaleTracking", ht, tableNames);
            return parcelSaleTracking;
        }

        /// <summary>
        /// Data to populate Owner Grid
        /// </summary>
        /// <param name="saleId">The Sale Id</param>
        /// <param name="ownerId">The Owner Id</param>
        /// <param name="parcelId">The Parcel Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Owner Details DataSet</returns>
        public static F29551ParcelSaleTrackingData F29551_GetOwnerDetails(int? saleId, int? ownerId, int? parcelId, int userId)
        {
            F29551ParcelSaleTrackingData parcelSaleTracking = new F29551ParcelSaleTrackingData();
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet();
            ht.Add("@SaleID", saleId);
            ht.Add("@OwnerID", ownerId);
            ht.Add("@ParcelID", parcelId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(parcelSaleTracking.OwnerDetails, "f29551_pcget_SaleOwners", ht);
            return parcelSaleTracking;
        }

        /// <summary>
        /// Parcel and Owner details
        /// </summary>
        /// <param name="parcelId">The Parcel Id</param>
        /// <param name="parcelCollection">Parcel Collections</param>
        /// <param name="saleId">The Sale Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Parcel and Owner details</returns>
        /// 
        public static F29551ParcelSaleTrackingData F29551_GetParcelOwnerDetails(int? parcelId, string parcelCollection, int? saleId, int userId)
        {
            F29551ParcelSaleTrackingData parcelSaleTracking = new F29551ParcelSaleTrackingData();
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@ParcelIDs", parcelCollection);
            ht.Add("@SaleID", saleId);
            ht.Add("@UserID", userId);
            string[] tableNames = new string[] 
            { 
                parcelSaleTracking.ParcelDetails.TableName, 
                parcelSaleTracking.OwnerDetails.TableName, 
                parcelSaleTracking.ReturnMessage.TableName
            };
            Utility.LoadDataSet(parcelSaleTracking, "f29551_pcget_SaleParcelsAndOwners", ht, tableNames);
            return parcelSaleTracking;
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
        public static int F29551_SaveParcelSaleDetails(int eventId, string saleItems, string parcelItems, string ownerItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@SaleItems", saleItems);
            ht.Add("@ParcelItems", parcelItems);
            ht.Add("@OwnerItems", ownerItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f29551_pcins_ParcelSaleTracking", ht);
        }

        /// <summary>
        /// Create Sale Versions
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User Id</param>
        /// <param name="checkedParcels">Checked Parcels List</param>
        /// <returns>Message returned from SP</returns>
        public static string F29551_CreateSaleVersions(int eventId, int userId, string checkedParcels)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@UserID", userId);
            ht.Add("@CheckedParcels", checkedParcels);
            return Utility.FetchSingleOuputParameter("f29551_pcexe_CreateSaleVersions", ht, "@Result");
        }

        /// <summary>
        /// Transfer Ownership Details
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Message returned from SP</returns>
        public static string F29551_TransferOwnership(int eventId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@UserID", userId);
            return Utility.FetchSingleOuputParameter("f29551_pcexe_TransferOwnership", ht, "@Result");
        }

        /// <summary>
        /// F29551_s the update sale parcel.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Dialog Text</returns>
        public static string F29551_UpdateSaleParcel(int eventId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@UserID", userId);
            return Utility.FetchSingleOuputParameter("f29551_pcupd_SaleParcelValues", ht, "@DialogText");
        }
    }
}
