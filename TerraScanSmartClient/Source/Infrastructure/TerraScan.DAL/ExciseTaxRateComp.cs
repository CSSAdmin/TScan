// -------------------------------------------------------------------------------------------
// <copyright file="ExciseTaxRateComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Rates</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 27 July 06		JYOTHI P	            Created
// 27 July 06       JYOTHI P                Added GetExciseTaxStatement method
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
    public static class ExciseTaxRateComp
    {
        #region Get Excise Tax Rate

        /// <summary>
        /// Gets the Excise Tax Statement
        /// </summary>
        /// <param name="exciseRateId">The exciseRate id.</param>
        /// <returns>returns dataset contains Excise Tax Rate Details</returns>
        public static ExciseTaxRateData GetExciseTaxRate(int exciseRateId)
        {
            ExciseTaxRateData exciseTaxRateData = new ExciseTaxRateData();
            Hashtable ht = new Hashtable();
            ht.Add("@ExciseRateID", exciseRateId);
            Utility.LoadDataSet(exciseTaxRateData, "f1101_pcget_ExciseTaxRate", ht, new string[] { "GetExciseTaxRate", "GetExciseTaxAccountInfo" });
            return exciseTaxRateData;
        }

        #endregion

        #region List Excise Tax Rate

        /// <summary>
        /// Lists the ExciseTaxRateIDs
        /// </summary>
        /// <returns>returns dataset contains ExciseRateID</returns>
        public static ExciseTaxRateData ListExciseTaxRate()
        {
            ExciseTaxRateData exciseTaxRateData = new ExciseTaxRateData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(exciseTaxRateData.ListExciseTaxRate, "f1101_pclst_ExciseTaxRate", ht);
            return exciseTaxRateData;
        }

        #endregion

        #region Save Excise Tax Rate

        /// <summary>
        /// Save the ExciseTaxRate
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="exciseTaxDetails">The excise tax details.</param>
        /// <param name="userId">The userId.</param>
        public static void SaveExciseTaxRate(int exciseRateId, string exciseTaxDetails, int userId)
        {
            ////ExciseTaxRateData exciseTaxRateData = new ExciseTaxRateData();
            Hashtable ht = new Hashtable();
            if (exciseRateId != 0)
            {
                ht.Add("@ExciseRateID", exciseRateId);
            }

            ht.Add("@ExciseTaxDetails", exciseTaxDetails);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1101_pcins_ExciseTaxRate", ht);
        }

        #endregion

        #region Delete Excise Tax Rate

        /// <summary>
        /// Deletes the excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int DeleteExciseTaxRate(int exciseRateId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ExciseRateID", exciseRateId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecutedReturnValue("f1101_pcdel_ExcisetTaxRate", ht);
        }
        #endregion

        #region Get District Name

        /// <summary>
        /// Gets the District Name
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>returns dataset contains District Name</returns>
        public static ExciseTaxRateData GetDistrictName(int districtId)
        {
            ExciseTaxRateData exciseTaxRateData = new ExciseTaxRateData();
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictID", districtId);
            Utility.LoadDataSet(exciseTaxRateData.GetDistrictName, "f1101_pcget_District", ht);
            return exciseTaxRateData;
        }

        #endregion

        #region Get Account Name

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public static ExciseTaxRateData GetAccountName(int accountId)
        {
            ExciseTaxRateData exciseTaxRateData = new ExciseTaxRateData();
            Hashtable ht = new Hashtable();
            ht.Add("@AccountID", accountId);
            Utility.LoadDataSet(exciseTaxRateData.GetAccountName, "f1101_pcget_AccountName", ht);
            return exciseTaxRateData;
        }

        #endregion
    }
}
