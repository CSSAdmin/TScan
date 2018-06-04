// -------------------------------------------------------------------------------------------
// <copyright file="F16030SpecialDistrictDefinitionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update NextNumber Configuration</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Jun 07		suganth Mani       Created
// -------------------------------------------------------------------------------------------


namespace TerraScan.Dal
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    #endregion NameSpace

    /// <summary>
    /// Main class for Special District Definition Component
    /// </summary>
    public static class F16030SpecialDistrictDefinitionComp
    {
        #region ListMethods

        /// <summary>
        /// F1030_s the type of the list district definition.
        /// </summary>
        /// <returns>F1030SpecialDistrictDefinitionData</returns>
        public static F1030SpecialDistrictDefinitionData F16030_ListDistrictDefinitionType()
        {
            F1030SpecialDistrictDefinitionData listDistrictDefinitioData = new F1030SpecialDistrictDefinitionData();
            Hashtable ht = new Hashtable();
            string[] tableName = new string[] { listDistrictDefinitioData.DistrictDetails.TableName, listDistrictDefinitioData.DistrictPostingList.TableName, listDistrictDefinitioData.DistrictRateList.TableName, listDistrictDefinitioData.DistrictDistributionTypeList.TableName, listDistrictDefinitioData.ListDistrictDefinitionID.TableName };
            Utility.LoadDataSet(listDistrictDefinitioData, "f1030_pclst_DistrictDefinitionType", ht, tableName);
            return listDistrictDefinitioData;
        }

        #endregion ListMethods

        #region GetMethods

        /// <summary>
        /// F1030_s the get district definition details.
        /// </summary>
        /// <param name="districtNo">The district no.</param>
        /// <returns>F1030SpecialDistrictDefinitionData</returns>
        public static F1030SpecialDistrictDefinitionData F16030_GetDistrictDefinitionDetails(int districtNo)
        {
            F1030SpecialDistrictDefinitionData listDistrictDefinitioData = new F1030SpecialDistrictDefinitionData();
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictNo", districtNo);
            string[] tableName = new string[] { listDistrictDefinitioData.GetDistrictDefinitionDetails.TableName, listDistrictDefinitioData.GetDistrictRateDetails.TableName, listDistrictDefinitioData.GetDistrictDistributionDetails.TableName };
            Utility.LoadDataSet(listDistrictDefinitioData, "f1030_pclst_DistrictDefinitionDetails", ht, tableName);
            return listDistrictDefinitioData;
        }

        #endregion GetMethods

        #region DeleteMethods

        /// <summary>
        /// F1030 Delete DistrictDefinition
        /// </summary>
        /// <param name="specialDistrictId">The special district ID.</param>
        /// <param name="userId">userId</param>
        /// <returns>the integer value</returns>
        public static int F16030_DeleteDistrictDefinition(int specialDistrictId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SADistrictID", specialDistrictId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f1030_pcdel_DistrictDefinition", ht);
        }

        /// <summary>
        /// F1030 Delete DistrictDefinitionRate
        /// </summary>
        /// <param name="specialDistrictRateItemId">The special district rate item ID.</param>
        /// <param name="userId">userId</param>
        public static void F16030_DeleteDistrictDefinitionRate(int specialDistrictRateItemId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SARateItemID", specialDistrictRateItemId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1030_pcdel_DistrictDefinitionRate", ht);
        }

        #endregion DeleteMethods

        #region SaveMethods

        /// <summary>
        /// F16030_s the save district definition.
        /// </summary>
        /// <param name="districtNo">The district no.</param>
        /// <param name="districtItem">The district item.</param>
        /// <param name="rateItem">The rate item.</param>
        /// <param name="distributionItem">The distribution item.</param>
        /// <param name="flagOverride">if set to <c>true</c> [flag override].</param>
        /// <param name="flagValidation">if set to <c>true</c> [flag validation].</param>
        /// <param name="userId">userId</param>
        /// <returns>KeyID</returns>
        public static string F16030_SaveDistrictDefinition(int districtNo, string districtItem, string rateItem, string distributionItem, bool flagOverride, bool flagValidation, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictNo", districtNo);
            ht.Add("@DistrictItem", districtItem);
            ht.Add("@RateItem", rateItem);
            ht.Add("@DistributionItem", distributionItem);
            ht.Add("@IsOverride", flagOverride);
            ht.Add("@IsValidation", flagValidation);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyString("f1030_pcins_DistrictDefinition", ht);
        }

        #endregion SaveMethods
    }
}
