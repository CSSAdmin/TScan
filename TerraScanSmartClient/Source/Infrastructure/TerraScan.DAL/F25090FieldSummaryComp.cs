// -------------------------------------------------------------------------------------------
// <copyright file="F25090FieldSummaryComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to Field Summary Data</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20120705 		Manoj P	            Created
// -------------------------------------------------------------------------------------------


namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// Main class for F25090 Field Summary Comp
    /// </summary>
    public static class F25090FieldSummaryComp
    {
        #region Get Field Summary

        /// <summary>
        /// Gets the Get Field Summary
        /// </summary>
        /// <param name="KeyId">The KeyId.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public static F25090FieldSummaryData F25090_FieldSummary(int keyId)
        {
            F25090FieldSummaryData summaryDetailData = new F25090FieldSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@KeyID", keyId);
            Utility.LoadDataSet(summaryDetailData, "f25099_pcget_QuickValueSummary", ht, new string[] { summaryDetailData.QuickValueSummaryData.TableName });
            return summaryDetailData;
        }

        #endregion

        #region Get Sale Detail

        /// <summary>
        /// Gets the Get Sale Detail
        /// </summary>
        /// <param name="KeyId">The KeyId.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public static F25090FieldSummaryData F25090_ParcelSale(int keyId)
        {
            F25090FieldSummaryData SaleDetailData = new F25090FieldSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", keyId);
            Utility.LoadDataSet(SaleDetailData, "f25030_pcget_ParcelSaleHistory", ht, new string[] { SaleDetailData.ParcelSaleHistoryData.TableName });
            return SaleDetailData;
        }
        #endregion

        #region Get Building Permits

        /// <summary>
        /// Get Building Permits
        /// </summary>
        /// <param name="KeyId">The KeyId.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public static F25090FieldSummaryData F25090_BuildingPermits(int keyId)
        {
            F25090FieldSummaryData BuildingPermitsData = new F25090FieldSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@parcelID", keyId);
            Utility.LoadDataSet(BuildingPermitsData, "f24555_pcget_BuildingPermits", ht, new string[] { BuildingPermitsData.BuildingPermitsData.TableName });
            return BuildingPermitsData;
        }
        #endregion

        #region Get Photos Datas

        /// <summary>
        /// Get Photos Data
        /// </summary>
        /// <param name="KeyId">The KeyId.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public static F25090FieldSummaryData F25090_GetPhotos(int keyId, int form)
        {
            F25090FieldSummaryData GetPhotosData = new F25090FieldSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@KeyID", keyId);
            ht.Add("@Form", form);
            Utility.LoadDataSet(GetPhotosData, "f9005_pcget_GetPhotos", ht, new string[] { GetPhotosData.PhotosData.TableName });
            return GetPhotosData;
        }
        #endregion

        #region Get Correction Data

        /// <summary>
        /// Get Correction Data
        /// </summary>
        /// <param name="KeyId">The KeyId.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public static F25090FieldSummaryData F25090_GetCorrection(int keyId)
        {
            F25090FieldSummaryData GetCorrectionData = new F25090FieldSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@keyId", keyId);
            Utility.LoadDataSet(GetCorrectionData, "f19099_pcget_TxRollCorrectAA", ht, new string[] { GetCorrectionData.CorrectionData.TableName });
            return GetCorrectionData;
        }
        #endregion

        #region Get History Data

        /// <summary>
        /// Get History Data
        /// </summary>
        /// <param name="KeyId">The KeyId.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public static F25090FieldSummaryData F25090_GetHistoryData(int keyId)
        {
            F25090FieldSummaryData GetHistoryData = new F25090FieldSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@Keyid", keyId);
            Utility.LoadDataSet(GetHistoryData, "f19097_pcget_relStatAA", ht, new string[] { GetHistoryData.HistoryData.TableName });
            return GetHistoryData;
        }
        #endregion

        #region Get Ancestry Data

        /// <summary>
        ///  Get Ancestry Data
        /// </summary>
        /// <param name="KeyId">The KeyId.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public static F25090FieldSummaryData F25090_GetAncestryData(int keyId)
        {
            F25090FieldSummaryData GetAncestryData = new F25090FieldSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@KeyID", keyId);
            Utility.LoadDataSet(GetAncestryData, "f25040_pcget_ParcelAncestry", ht, new string[] { GetAncestryData.ParcelAncestryData.TableName });
            return GetAncestryData;
        }
        #endregion

        #region Get ParcelOwnerShip Data

        /// <summary>
        ///  Get ParcelOwnerShip Data
        /// </summary>
        /// <param name="KeyId">The KeyId.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public static F25090FieldSummaryData F25090_GetParcelOwnerShip(int keyId)
        {
            F25090FieldSummaryData GetParcelOwnerShip = new F25090FieldSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@KeyID", keyId);
            Utility.LoadDataSet(GetParcelOwnerShip, "f25098_pcget_ParcelOwnership", ht, new string[] { GetParcelOwnerShip.ParcelOwnershipData.TableName });
            return GetParcelOwnerShip;
        }
        #endregion
    }
}
