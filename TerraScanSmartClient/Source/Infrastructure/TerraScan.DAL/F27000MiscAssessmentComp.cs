// -------------------------------------------------------------------------------------------
// <copyright file="F27000MiscAssessmentComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F27000MiscAssessmentComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 05/04/07         J.G. Ranjani         Created
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
    /// F27000MiscAssessmentComp Class file
    /// </summary>
    public static class F27000MiscAssessmentComp
    {
        #region F27000 Misc Assessment

        #region Get Misc Assessment Details

        /// <summary>
        /// Gets the Misc Assessment details based on the Misc Assessment DistrictId
        /// </summary>
        /// <param name="madistrictId">The Misc Assessment District Id.</param>
        /// <returns>
        /// The typed dataset containing the Misc Assessment information of the madistrictId.
        /// </returns>
        public static F22000MiscAssessmentData F27000_GetMiscAssessment(int madistrictId)
        {
            F22000MiscAssessmentData miscAssessmentData = new F22000MiscAssessmentData();
            //string[] tableNames = {  miscAssessmentData.GetMADetails.TableName,
            //                          miscAssessmentData.ListMADistributionItem.TableName
            //                          ,miscAssessmentData.OutParameterListTable.TableName
            //                         };
            //isAccountRequired = true;
            Hashtable ht = new Hashtable();
            ht.Add("@MADistrictID", madistrictId);
            //IList returnList = Utility.LoadDataSet(miscAssessmentData, "f27000_pcget_MADetails", ht,tableNames);
            Utility.LoadDataSet(miscAssessmentData, "f27000_pcget_MADetails", ht, new string[] { miscAssessmentData.GetMADetails.TableName, miscAssessmentData.ListMADistributionItem.TableName,miscAssessmentData.AccountRequiredTable.TableName });
            
            //isAccountRequired = Convert.ToBoolean(((System.Data.SqlClient.SqlParameter[])(returnList))[1].Value.ToString());
            
            return miscAssessmentData;
        }

        #endregion Get Misc Assessment Details

        #region List Misc Assessment District Type

        /// <summary>
        /// To List all the Misc Assessment District Types.
        /// </summary>
        /// <returns>Typed Dataset Containing the Misc Assessment District Types</returns>
        public static CommonData F27000_ListMADistrictType()
        {
            CommonData commonData = new CommonData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(commonData.ComboBoxDataTable, "f27000_pclst_MADistrictType", ht);
            return commonData;
        }

        #endregion List Misc Assessment District Type

        #region List Misc Assessment District Item Type

        /// <summary>
        /// To List All Misc Assessment District Item Type
        /// </summary>
        /// <param name="madistrictTypeId">The Misc Assessment District type Id.</param>
        /// <returns>Typed Dataset Containg the All Misc Assessment Misc Assessment Item Types</returns>
        public static CommonData F27000_ListMADistrictItemType(int madistrictTypeId)
        {
            CommonData commonData = new CommonData();
            Hashtable ht = new Hashtable();
            ht.Add("@MADTypeID", madistrictTypeId);
            Utility.LoadDataSet(commonData.ComboKeyStringDataTable, "f27000_pclst_MADistrictItemType", ht);
            return commonData;
        }

        #endregion List Misc Assessment District Item Type

        #region Save Misc Assessment Details

        /// <summary>
        /// To Save Misc Assessment Details
        /// </summary>
        /// <param name="distributionItems">distributionItems</param>
        /// <param name="subHeaderItems">subHeaderItems</param>
        /// <param name="userId">userId</param>
        /// <returns>integer</returns>
        public static int F27000_SaveMADetails(string distributionItems, string subHeaderItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DistributionItems", distributionItems);
            ht.Add("@SubHeaderItems", subHeaderItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f27000_pcins_MADetails", ht);
        }

        #endregion Save Misc Assessment Details

        #endregion F27000 Misc Assessment
    }
}
