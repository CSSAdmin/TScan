// -------------------------------------------------------------------------------------------
// <copyright file="F15013ExciseTaxRateComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Rates</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 Jan 06		JYOTHI P	            Created
// 25 Jan 06        JYOTHI P                Added GetExciseTaxStatement method
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
    /// Main class for Excise Tax Rate Component
    /// </summary>
    public static class F15013ExciseTaxRateComp
    {
        #region Get Excise Tax Rate

        /// <summary>
        /// Gets the Excise Tax Statement
        /// </summary>
        /// <param name="exciseRateId">The exciseRate id.</param>
        /// <returns>returns dataset contains Excise Tax Rate Details</returns>
        public static F15013ExciseTaxRateData F15013_GetExciseTaxRate(int exciseRateId)
        {
            F15013ExciseTaxRateData exciseTaxRateData = new F15013ExciseTaxRateData();
            Hashtable ht = new Hashtable();
            ht.Add("@ExciseRateID", exciseRateId);
            Utility.LoadDataSet(exciseTaxRateData, "f15013_pcget_ExciseTaxRate", ht, new string[] { "GetExciseTaxRate", "GetExciseTaxAccountInfo" });
            return exciseTaxRateData;
        }

        #endregion

        #region List Excise Tax Rate

        /// <summary>
        /// Lists the ExciseTaxRateIDs
        /// </summary>
        /// <returns>returns dataset contains ExciseRateID</returns>
        public static F15013ExciseTaxRateData F15013_ListExciseTaxRate()
        {
            F15013ExciseTaxRateData exciseTaxRateData = new F15013ExciseTaxRateData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(exciseTaxRateData.ListExciseTaxRate, "f15103_pclst_ExciseTaxRate", ht);
            return exciseTaxRateData;
        }

        #endregion

        #region Save Excise Tax Rate

        /// <summary>
        /// F15013_s the save excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="exciseTaxDetails">The excise tax details.</param>
        /// <param name="userId">userId</param>
        /// <returns>the errorId/Primary Key</returns>
        public static int F15013_SaveExciseTaxRate(int exciseRateId, string exciseTaxDetails, int userId)
        {
            int errorId;
            ////F15013ExciseTaxRateData exciseTaxRateData = new F15013ExciseTaxRateData();
            Hashtable ht = new Hashtable();
            if (exciseRateId != 0)
            {
                ht.Add("@ExciseRateID", exciseRateId);
            }

            ht.Add("@ExciseTaxDetails", exciseTaxDetails);
            ht.Add("@UserID", userId);
            ////Utility.ExecuteSP("f15013_pcins_ExciseTaxRate", ht);
            errorId = Utility.FetchSPExecuteKeyId("f15013_pcins_ExciseTaxRate", ht);
            return errorId;
        }

        #endregion

        #region Delete Excise Tax Rate

        /// <summary>
        /// Deletes the excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="userId">userId</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int F15013_DeleteExciseTaxRate(int exciseRateId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ExciseRateID", exciseRateId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecutedReturnValue("f15013_pcdel_ExcisetTaxRate", ht);
        }
        #endregion

        #region Get District Name

        /// <summary>
        /// Gets the District Name
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>returns dataset contains District Name</returns>
        public static F15013ExciseTaxRateData F15013_GetDistrictName(int districtId)
        {
            F15013ExciseTaxRateData exciseTaxRateData = new F15013ExciseTaxRateData();
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictID", districtId);
            Utility.LoadDataSet(exciseTaxRateData.GetDistrictName, "f9108_pcget_District", ht);
            return exciseTaxRateData;
        }

        #endregion

        #region Get Account Name

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public static F15013ExciseTaxRateData F15013_GetAccountName(int accountId)
        {
            F15013ExciseTaxRateData exciseTaxRateData = new F15013ExciseTaxRateData();
            Hashtable ht = new Hashtable();
            ht.Add("@AccountID", accountId);
            Utility.LoadDataSet(exciseTaxRateData.GetAccountName, "f1345_pcget_Account", ht);
            return exciseTaxRateData;
        }

        #endregion
    }
}
