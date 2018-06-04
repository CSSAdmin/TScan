// -------------------------------------------------------------------------------------------
// <copyright file="F27080ExemptionDefinitionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F27080ExemptionDefinitionComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 15/09/07        A.Sriparameswari      Created
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
    /// F27080ExemptionDefinitionComp Class file
    /// </summary>
    public static class F27080ExemptionDefinitionComp
    {
        #region FillExemptionTypeCombo
        /// <summary>
        /// F27080_ListExemptionTypeCombo
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>F27080ExemptionDefinitionData</returns>
        public static F27080ExemptionDefinitionData F27080_ListExemptionTypeCombo(int applicationId)
        {
            F27080ExemptionDefinitionData exemptionTypeData = new F27080ExemptionDefinitionData();
            Hashtable ht = new Hashtable();
            ht.Add("@ApplicationID", applicationId);
            Utility.LoadDataSet(exemptionTypeData.ListExemptionTypeTable, "f27080_pclst_ExemptionType", ht);
            return exemptionTypeData;
        }

        #endregion

        #region FillDataGridExemptionType

        /// <summary>
        /// F27080_FillExemptionTypeGrid
        /// </summary>
        /// <param name="exemptionId">exemptionId</param>
        /// <returns>F27080ExemptionDefinitionData</returns>
        public static F27080ExemptionDefinitionData F27080_FillExemptionTypeGrid(int exemptionId)
        {
            F27080ExemptionDefinitionData exemptionTyprGrid = new F27080ExemptionDefinitionData();
            Hashtable ht = new Hashtable();
            ht.Add("@ExemptionID", exemptionId);
            ////DataSet ds = new DataSet();
            string[] tableName = new string[] { exemptionTyprGrid.GridLoadExemptionTypeTable.TableName, exemptionTyprGrid.GetSeniorExemptionTypeTable.TableName };
            Utility.LoadDataSet(exemptionTyprGrid, "f27080_pclst_SeniorExemption", ht, tableName);
            return exemptionTyprGrid;
        }

        #endregion

        #region GetExemptionError

        /// <summary>
        /// F27080_GetErrorExemption
        /// </summary>
        /// <param name="exemptionId">exemptionId</param>
        ///   /// <param name="exemptionCode">exemptionCode</param>
        /// <returns>Message</returns>
        public static string F27080_GetExemptionError(int exemptionId, string exemptionCode)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ExemptionID", exemptionId);
            ht.Add("@ExemptionCode", exemptionCode);
            return Utility.FetchSingleOuputParameter("f27080_pget_SeniorExemptionError", ht, "@Message");
        }

        #endregion

        #region DeleteExemption

        /// <summary>
        /// F27080_DeleteExemption
        /// </summary>
        /// 
        /// <param name="exemptionId">userId</param>
        /// <param name="exemptionId">exemptionId</param>
        ///   /// <param name="exemptionCode">exemptionCode</param>
        /// <returns>NULL</returns>
        public static void F27080_DeleteExemption(int userId, int exemptionId, string exemptionCode)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@ExemptionID", exemptionId);
            ht.Add("@ExemptionCode", exemptionCode);
            Utility.ImplementProcedure("f27080_pcel_SeniorExemption", ht);
        }

        #endregion

        #region SaveExemptionType

        /// <summary>
        /// F27080_SaveExemptionType
        /// </summary>
        /// <param name="exemptionId">exemptionID</param>
        /// <param name="seniorExemption">seniorExemption</param>
        /// <param name="exemptionType">exemptionType</param>
        /// <param name="checkError">checkError</param>        
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F27080_SaveExemptionType(int exemptionId, string seniorExemption, string exemptionType, int checkError, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ExemptionID", exemptionId);
            ht.Add("@SeniorExemption", seniorExemption);
            ht.Add("@ExemptionType", exemptionType);
            ht.Add("@CheckError", checkError);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f27080_pcins_SeniorExemption", ht);
        }

        #endregion
    }
}
