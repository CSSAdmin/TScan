// -------------------------------------------------------------------------------------------
// <copyright file="F16040ImproveDistrictDefinitionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F16040ImproveDistrictDefinitionComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 25/07/2017       Dhineshkumar        Created.
// -------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using TerraScan.BusinessEntities;
using TerraScan.DataLayer;

namespace TerraScan.Dal
{
    public static class F16041ImprovementDistrictParcelsComp
    {
        /// <summary>
        /// Get District Parcels.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F16041ImprovementDistrictParcels GetDistrictParcels(int districtId)
        {
            F16041ImprovementDistrictParcels getDistrictParcels = new F16041ImprovementDistrictParcels();
            Hashtable ht = new Hashtable();
            string[] tableName = new string[] { getDistrictParcels.GetDistrictParcels.TableName, getDistrictParcels.GetParcelgridDetails.TableName, getDistrictParcels.GetSummaryDetails.TableName };
            ht.Add("@SADistrictID", districtId);
            Utility.LoadDataSet(getDistrictParcels, "f10041_pcget_DistrictParcels", ht, tableName);
            return getDistrictParcels;
        }

        /// <summary>
        /// List selected parcel details.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F16041ImprovementDistrictParcels ListDistrictParcelsDetails(string parcelval, int? parcelId, int? rollYear)
        {
            F16041ImprovementDistrictParcels getDistrictParcels = new F16041ImprovementDistrictParcels();
            Hashtable ht = new Hashtable();
            string[] tableName = new string[] { getDistrictParcels.ListParcelDetails.TableName };
            ht.Add("@ParcelNumber", parcelval);
            ht.Add("@ParcelID", parcelId);
            ht.Add("@RollYear", rollYear);
            Utility.LoadDataSet(getDistrictParcels, "f10041_pclst_ImprovementDistrictParcel", ht, tableName);
            return getDistrictParcels;
        }

        /// <summary>
        /// Save Improvement District Parcels.
        /// </summary>
        /// <param name="districtItem"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string F16041_SaveDistrictParcels(string districtProperty, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictProperty", districtProperty);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyString("f10041_pcins_ImprovementParcel", ht);
        }

        /// <summary>
        /// Check Parcel Details.
        /// </summary>
        /// <param name="districtItem"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string CheckParcelDetails(string districtProperty)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictProperty", districtProperty);
            return Utility.FetchSPExecuteKeyString("f10041_pcchk_ImprovementParcel", ht);
        }

        /// <summary>
        /// Delete District Parcels.
        /// </summary>
        /// <param name="workingFileItemId">workingFileItemId.</param>
        /// <param name="userId"></param>
        public static string F16041_DeleteDistrictParcels(int workingFileItemId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@WorkingFileID", workingFileItemId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyString("f10041_pcdel_ImprovementParcel", ht);
        }
    }
}
