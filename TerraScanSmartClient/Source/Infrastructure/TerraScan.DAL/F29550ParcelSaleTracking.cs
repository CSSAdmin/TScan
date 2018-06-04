// -------------------------------------------------------------------------------------------
// <copyright file="F29550ParcelSaleTracking.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F29550ParcelSaleTrackingComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 
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
    ///  Data Access Layer which talks to the DB directly for F29600
    /// </summary>
    public static class F29550ParcelSaleTrackingComp
    {
        /// <summary>
        /// F29550_GetParcelSaleTrackingDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>parcelSaleTracking</returns>
        public static F29550ParcelSaleTracking F29550_GetParcelSaleTrackingDetails(int eventId)
        {
            F29550ParcelSaleTracking parcelSaleTracking = new F29550ParcelSaleTracking();
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet();
            ht.Add("@EventID", eventId);
            string[] tableNames = new string[] 
            { 
                parcelSaleTracking.f29550ListParcelSaleTracking.TableName, 
                parcelSaleTracking.f29550ListParcelDetails.TableName, 
                parcelSaleTracking.f29550ListOwnerDetails.TableName, 
                parcelSaleTracking.f29550_ModeTable.TableName
            };

            Utility.LoadDataSet(parcelSaleTracking, "f29550_pcget_ParcelSaleTracking", ht, tableNames);

            Utility.LoadDataSet(parcelSaleTracking, "f29550_pcget_ParcelSaleTracking", ht);
           //// Utility.FillDataSet(parcelSaleTracking.f29550ListParcelSaleTracking, "f29550_pcget_ParcelSaleTracking", ht);
            Utility.LoadDataSet(ds, "f29550_pcget_ParcelSaleTracking", ht); 
            return parcelSaleTracking;
        }

        /// <summary>
        /// F29550_GetParcelSaleTrackingComboDetails
        /// </summary>
        /// <returns>parcelSaleTracking</returns>
        public static F29550ParcelSaleTracking F29550_GetParcelSaleTrackingComboDetails()
        {
            F29550ParcelSaleTracking parcelSaleTracking = new F29550ParcelSaleTracking();
            ////DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
           //// string[] tableNames = new string[] { parcelSaleTracking.f29550ListSaleAdvisory.TableName, parcelSaleTracking.f29550ListSaleConfidence.TableName, parcelSaleTracking.f29550ListSaleStateCategory.TableName, parcelSaleTracking.f29550ListSaleStatus.TableName };
            ////Utility.FillDataSet(parcelSaleTracking, "f29550_pclst_ParcelSaleTracking", ht, tableNames);
            Utility.LoadDataSet(parcelSaleTracking, "f29550_pclst_ParcelSaleTracking", ht);
            return parcelSaleTracking;
        }

        /// <summary>
        /// F29550_GetParcelsOwnerDetails
        /// </summary>
        /// <param name="parcelIdDetails">parcelIdDetails</param>
        /// <returns>parcelSaleTracking</returns>
        public static F29550ParcelSaleTracking F29550_GetParcelsOwnerDetails(string parcelIdDetails)
        {
            F29550ParcelSaleTracking parcelSaleTracking = new F29550ParcelSaleTracking();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelIDlist", parcelIdDetails);
            Utility.LoadDataSet(parcelSaleTracking.f29550ListOwnerDetails, "f29550_pclst_SaleOwners", ht);
            return parcelSaleTracking;
        }

        /// <summary>
        /// F29550_GetParcelsOwnerDetails
        /// </summary>
        /// <param name="saleId">saleId</param>
        /// <returns>parcelSaleTracking</returns>
        public static F29550ParcelSaleTracking F29550_GetPushOwner(int saleId)
        {
            F29550ParcelSaleTracking parcelSaleTracking = new F29550ParcelSaleTracking();
            Hashtable ht = new Hashtable();
            ht.Add("@SaleID", saleId);
            Utility.LoadDataSet(parcelSaleTracking.f29550_PushSaleOwner, "f29550_pcexe_PushSaleOwners", ht);
            return parcelSaleTracking;
        }

        #region Save ParcelSale Tracking            

        /// <summary>
        /// F29550_saveParcelSaleDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="saleItems">saleItems</param>
        /// <param name="parcelItems">parcelItems</param>
        /// <param name="ownerItems">ownerItems</param>
        /// <param name="userId">userId</param>
        /// <returns>integer value</returns>
        public static int F29550_saveParcelSaleDetails(int eventId, string saleItems, string parcelItems, string ownerItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@SaleItems", saleItems);
            ht.Add("@ParcelItems", parcelItems);
            ht.Add("@OwnerItems", ownerItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f29550_pcins_ParcelSaleTrackingValues", ht);
        }

        #endregion Save ParcelSale Tracking

        #region List Parcel Details based on parcelId

        /// <summary>
        /// F29550_GetParcelsOwnerDetails
        /// </summary>
        /// <param name="parcelIdDetails">parcelIdDetails</param>
        /// <param name="newParcelId">newParcelId</param>
        /// <param name="saleId">saleId</param>
        /// <returns>parcelSaleTracking</returns>
        public static F29550ParcelSaleTracking F29550_GetParcelDetails(string parcelIdDetails, int newParcelId, int saleId)
        {
            F29550ParcelSaleTracking parcelSaleTracking = new F29550ParcelSaleTracking();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelIDlist", parcelIdDetails);
            if (newParcelId > 0)
            {
                ht.Add("@NewParcelID", newParcelId);
            }

            ht.Add("@SaleID", saleId);
            Utility.LoadDataSet(parcelSaleTracking, "f29550_pcget_IncludedParcelsGridValues", ht);
            return parcelSaleTracking;
        }

        #endregion List Parcel Details based on parcelId
    }
}
