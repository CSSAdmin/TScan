// -------------------------------------------------------------------------------------------
// <copyright file="F25000ParcelHeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F25000ParcelHeaderComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 28/10/2013       Purushotham.A       Created
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
    /// Data Access Layer which talks to the DB directly for F26000
    /// </summary>
    public static class F26000ParcelHeaderFormComp
    {
        /// <summary>
        /// F26000_GetParcelDetails
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <returns>Typed dataset</returns>
        public static F26000ParcelHeaderFormData F26000_GetParcelFormDetails(int parcelId)
        {
            F26000ParcelHeaderFormData parcelHeaderFormData = new F26000ParcelHeaderFormData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            string[] tableName = new string[] { parcelHeaderFormData.F26000ParcelHeader.TableName, parcelHeaderFormData.GetConfigState.TableName };
            Utility.LoadDataSet(parcelHeaderFormData, "f26000_pcget_ParcelHeader", ht,tableName);
            return parcelHeaderFormData;
        }


        /// <summary>
        /// F26000_s the exemption details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <param name="exemptionFromAmount">The exemption from amount.</param>
        /// <returns></returns>
        public static F26000ParcelHeaderFormData F26000_ExemptionDetails(int parcelId, string exemptionCode, decimal? exemptionFromAmount)
        {
            F26000ParcelHeaderFormData parcelHeaderObj = new F26000ParcelHeaderFormData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@ExemptionCode", exemptionCode);
            ht.Add("@ExemptFromAmount", exemptionFromAmount);
            Utility.LoadDataSet(parcelHeaderObj.f26000ParcelHeaderExemptionDetails, "f26000_pcget_ParcelHeaderExemptionDetails", ht);
            return parcelHeaderObj;
        }



        public static F26000ParcelHeaderFormData F26000_ExemptFieldDetails(int parcelId, int exemptionId, string exemptionCode)
        {
            F26000ParcelHeaderFormData parcelHeaderObj = new F26000ParcelHeaderFormData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@ExemptionID", exemptionId);
            ht.Add("@ExemptionCode", exemptionCode);
            Utility.LoadDataSet(parcelHeaderObj.f26000_pcget_ExemptFieldDetails, "f26000_pcget_ExemptFieldDetails", ht);
            return parcelHeaderObj;
        }
        /// <summary>
        /// F26000_s the class code details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public static F26000ParcelHeaderFormData F26000_ClassCodeDetails(string filterValue)
        {
            F26000ParcelHeaderFormData parcelheaderObject = new F26000ParcelHeaderFormData();
            Hashtable ht = new Hashtable();
            ht.Add("@Filter", filterValue);
            Utility.LoadDataSet(parcelheaderObject.f26000ClassCode, "f26000_pcget_ClassCode", ht);
            return parcelheaderObject;
        }
        /// <summary>
        /// UpdateParcelHeaderDetails
        /// </summary>
        /// <param name="parcelId">parcelID</param>
        /// <param name="parcelDetails">parcelDetails</param>
        /// <param name="userId">userId</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Integer value</returns>
        public static int UpdateParcelHeaderFormDetails(int parcelId, string parcelDetails, int userId, int rollYear)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@ParcelItems", parcelDetails);
            ht.Add("@UserID", userId);
            ht.Add("@RollYear", rollYear);
            return Utility.FetchSPExecuteKeyId("f26000_pcupd_ParcelDetails", ht);
        }

        #region F26000 list Primary Improvement Selection



        /// <summary>
        /// Primaries the improvement list.
        /// </summary>
        /// <returns></returns>
        public static F26000ParcelHeaderFormData PrimaryImprovementList()
        {
            F26000ParcelHeaderFormData getPrimaryImprovementData = new F26000ParcelHeaderFormData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(getPrimaryImprovementData.f26000ListParcelImprovement, "f25000_pclst_ParcelImprovement", ht);
            return getPrimaryImprovementData;
        }

        #endregion

        #region F26000 list Primary Land Type Selection



        /// <summary>
        /// Primaries the land type list.
        /// </summary>
        /// <returns></returns>
        public static F26000ParcelHeaderFormData PrimaryLandTypeList()
        {
            F26000ParcelHeaderFormData getPrimaryLandTypeData = new F26000ParcelHeaderFormData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(getPrimaryLandTypeData.f26000ListParcelLandTypes, "f25000_pclst_ParcelLandTypes", ht);
            return getPrimaryLandTypeData;
        }

        #endregion

        public static DataSet ClassCode_RGB(string storedProcedureName)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = DataProxy.FetchDataTable(storedProcedureName);
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// F26000_GetApprisalType
        /// </summary>
        /// <returns></returns>
        public static F26000ParcelHeaderFormData F26000_GetApprisalType()
        {
            F26000ParcelHeaderFormData GetApprisalType = new F26000ParcelHeaderFormData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(GetApprisalType.f26000_pclst_AppraisalType, "f26000_pclst_AppraisalType", ht);
            return GetApprisalType;
        }
    }
}
