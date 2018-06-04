// -------------------------------------------------------------------------------------------
// <copyright file="F1504CopyAccountComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F1504CopyAccountComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 2/9/2009         Malliga             Created
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
    /// F1504CopyAccount Class File.
    /// </summary>
    public static class F1504CopyAccountComp
    {
        /// <summary>
        /// F1504_s the get copy account sub fund.
        /// </summary>
        /// <returns></returns>
        public static F1504CopyAccountData F1504_GetCopyAccountSubFund()
        {
            F1504CopyAccountData copyAccountSubFund = new F1504CopyAccountData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(copyAccountSubFund.F1504_ListSubFund, "f1504_pclst_SubFund", ht);
            return copyAccountSubFund;
        }

        /// <summary>
        /// F1504_s the get account detail.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns></returns>
        public static F1504CopyAccountData F1504_GetAccountDetail(int accountId)
        {
            F1504CopyAccountData getAccountDetail = new F1504CopyAccountData();
            Hashtable ht = new Hashtable();
            ht.Add("@AccountID", accountId);
            Utility.LoadDataSet(getAccountDetail.F1504_GetAccountDetail, "f1504_pcget_AccountDetail", ht);
            return getAccountDetail;
        }

        /// <summary>
        /// F1504_s the save copy account details.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="description">The description.</param>
        /// <param name="function">The function.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="accObject">The acc object.</param>
        /// <param name="line">The line.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static F1504CopyAccountData F1504_SaveCopyAccountDetails(int rollYear, string subFund, string description, string function, string bars, string accObject, string line, string userId)
        {
            F1504CopyAccountData copyAccountDataSet= new F1504CopyAccountData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            ht.Add("@SubFund", subFund);
            ht.Add("@Description", description);
            ht.Add("@Function", function);
            ht.Add("@Bars", bars);
            ht.Add("@Object", accObject);
            ht.Add("@Line", line);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(copyAccountDataSet.F1504_SaveCopyAccount, "f1504_pcins_Account", ht);
            return copyAccountDataSet;
        }
    }
}
