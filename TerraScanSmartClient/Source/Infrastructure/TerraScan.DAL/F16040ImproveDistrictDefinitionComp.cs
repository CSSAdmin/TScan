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
// 12/06/2017       Dhineshkumar        Created.
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
    public static class F16040ImproveDistrictDefinitionComp
    {
        #region F16040 list Primary Land Type Selection
        /// <summary>
        /// F16040 list Interest Method.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F16040ImprovementDistrictDefinition ListInterestMethod()
        {
            F16040ImprovementDistrictDefinition getInterestMethod = new F16040ImprovementDistrictDefinition();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(getInterestMethod.InterestMethodTable, "f10040_pclst_InterestMethod", ht);
            return getInterestMethod;
        }

        /// <summary>
        /// F16040 list Interest Method.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F16040ImprovementDistrictDefinition ListInterestDelqDetails()
        {
            F16040ImprovementDistrictDefinition getDelqInterestCalcMethod = new F16040ImprovementDistrictDefinition();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(getDelqInterestCalcMethod.DelqInterestCalcTable, "f10040_pclst_DelqInterestCalc", ht);
            return getDelqInterestCalcMethod;
        }

        /// <summary>
        /// F16040 list Interest Method.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F16040ImprovementDistrictDefinition GetDistrictDetails(int districtIdType)
        {
            F16040ImprovementDistrictDefinition getDistrictDetailsdata = new F16040ImprovementDistrictDefinition();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(getDistrictDetailsdata.DistrictTypeTable, "f1044_pclst_DistrictType", ht);
            return getDistrictDetailsdata;
        }

        /// <summary>
        /// F1044 list District Type.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F16040ImprovementDistrictDefinition ImprovementDistrictTypelist(string districtType)
        {
            F16040ImprovementDistrictDefinition getImprovementDistrictTypelist = new F16040ImprovementDistrictDefinition();
            Hashtable ht = new Hashtable();
            ht.Add("@IDType", districtType);
            Utility.LoadDataSet(getImprovementDistrictTypelist.DistrictTypeTable, "f1044_pclst_DistrictType", ht);
            return getImprovementDistrictTypelist;
        }

        /// <summary>
        /// F1044 list District Type.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F16040ImprovementDistrictDefinition GetDistributionDetails()
        {
            F16040ImprovementDistrictDefinition getDistributionDetails = new F16040ImprovementDistrictDefinition();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(getDistributionDetails.GetDistributionDetails, "f10040_pclst_DistributionType", ht);
            return getDistributionDetails;
        }

        /// <summary>
        /// F1044 list District Type.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F16040ImprovementDistrictDefinition GetDistrictDefinitionDetails(int districtId)
        {
            F16040ImprovementDistrictDefinition getDistrictDefinitionDetails = new F16040ImprovementDistrictDefinition();
            Hashtable ht = new Hashtable();
            string[] tableName = new string[] { getDistrictDefinitionDetails.GetDistrictDetails.TableName, getDistrictDefinitionDetails.GetSummaryDetails.TableName, getDistrictDefinitionDetails.GetDistributionDetails.TableName };
            ht.Add("@DistrictID", districtId);
            Utility.LoadDataSet(getDistrictDefinitionDetails, "f10040_pclst_DistrictDefinitionDetails", ht, tableName);
            return getDistrictDefinitionDetails;
        }

        /// <summary>
        /// Execute Rollover ImprovementDistrict.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F16040ImprovementDistrictDefinition RollOver_ImprovementDistrict(int districtId,int userId)
        {
            F16040ImprovementDistrictDefinition getDistributionDetails = new F16040ImprovementDistrictDefinition();
            Hashtable ht = new Hashtable();
            ht.Add("@SADistrictID", districtId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(getDistributionDetails, "f9080_pcexe_RollOver_TR_ImprovementDistrict", ht);
            return getDistributionDetails;
        }

        /// <summary>
        /// Save Improvement District Definition.
        /// </summary>
        /// <param name="districtItem"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string F16040_SaveImproveDistrictDefinition(string districtItem,string distributionItem, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictItem", districtItem);
            ht.Add("@DistributionItem", distributionItem);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyString("f10040_pcins_ImprovementDistrict", ht);
        }

        /// <summary>
        /// Update Improvement District Definition.
        /// </summary>
        /// <param name="districtNo"></param>
        /// <param name="districtItem"></param>
        /// <param name="distributionItem"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string F16040_UpdateImproveDistrictDefinition(int districtNo, string districtItem, string distributionItem, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictNo", districtNo);
            ht.Add("@DistrictItem", districtItem);
            ht.Add("@DistributionItem", distributionItem);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyString("f10040_pcupd_DistrictDefinition", ht);
        }

        #endregion 
    }
}
