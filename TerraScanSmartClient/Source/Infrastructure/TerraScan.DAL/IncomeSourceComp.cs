// -------------------------------------------------------------------------------------------
// <copyright file="IncomeSourceComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access IncomeSourceComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//  24-08-2016      R.Priyadharshini       Created
// -------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using TerraScan.DataLayer;
using System.Collections;
using TerraScan.BusinessEntities;

namespace TerraScan.Dal
{
    public static class IncomeSourceComp
    {
        #region List UnitTerms

        /// <summary>
        /// Lists the type of the Unit Terms.
        /// </summary>
        /// <returns>The dataset containing the Unit Terms</returns>
        public static F36090IncomeSourceData ListUnitTerms()
        {
            F36090IncomeSourceData unitTermsData = new F36090IncomeSourceData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(unitTermsData.ListUnitTerms, "f31090_pclst_UnitTerm", ht);
            return unitTermsData;
        }
        #endregion

        #region Save IncomeSource Items
        /// <summary>
        /// Saves the income source items.
        /// </summary>
        /// <param name="IncomeSourceItems">IncomeSourceItems.</param>
        /// <param name="IncomeSourceID">IncomeSourceID.</param>
        /// <param name="userId">userId.</param>
        /// 
        public static int SaveIncomeSourceDetails(int? IncomeSourceID, string IncomeSourceDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@IncomeSourceID", IncomeSourceID);
            ht.Add("@IncomeSourceDetails", IncomeSourceDetails);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f31090_pcins_IncomeSource", ht);
        }
        #endregion

        #region Delete Income Source Details

        /// <summary>
        /// Deletes the Income Source Details.
        /// </summary>
        /// <param name="incomeSourceID">The incomeSource id.</param>
        /// <param name="userId">userId</param>
        /// <param name="Message">output param Message</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static string DeleteIncomeSource(int incomeSourceID, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@IncomeSourceID", incomeSourceID);
            ht.Add("@UserID", userId);
            return Utility.FetchSingleOuputParameter("f31090_pcdel_IncomeSource", ht, "@Message");
        }

        #endregion

        #region Get

        /// <summary>
        /// Gets the Income Source Detail.
        /// </summary>
        /// <param name="IncomeSourceID">The IncomeSource id.</param>
        /// <returns>The dataset containing the Income Source information based on IncomeSourceID</returns>
        public static F36090IncomeSourceData GetIncomeSourceData(int IncomeSourceID)
        {
            F36090IncomeSourceData IncomeSourceTemplateData = new F36090IncomeSourceData();
            Hashtable ht = new Hashtable();
            ht.Add("@IncomeSourceID", IncomeSourceID);
            Utility.LoadDataSet(IncomeSourceTemplateData.GetIncomeSource, "f31090_pcget_IncomeSource", ht);
            return IncomeSourceTemplateData;
        }

        #endregion

    }
}
