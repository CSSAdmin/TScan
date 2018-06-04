// -------------------------------------------------------------------------------------------
// <copyright file="F15007AccountMgmtComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access Posting Errors related information</summary>
// Release history
// // Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 Nov 06		Krishna	            Created
// -------------------------------------------------------------------------------------------
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// AccountSelectionComp class file
    /// </summary>
    public static class F15007AccountMgmtComp
    {
        #region Check For Duplicate Account

        /// <summary>
        /// F15007_s the check duplicate account.
        /// </summary>
        /// <param name="accountId">The account ID.</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <returns>errorId</returns>
        public static int F15007_CheckDuplicateAccount(int accountId, string acctEmelemts)
        {
            int errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@AccountID", accountId);
            ht.Add("@AcctEmelemts", acctEmelemts);
            errorId = Utility.FetchSPExecuteKeyId("f1500_pcchk_Account", ht);
            return errorId;
        }

        #endregion
    }
}
