// -------------------------------------------------------------------------------------------
// <copyright file="CheckDetailComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Check Detail</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12 Oct 06		Ranjani	            Created
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
    /// Main class for Check Detail Component
    /// </summary>
    public static class CheckDetailComp
    {
        #region Get Cash Ledger

        /// <summary>
        /// Gets the Cash Ledger(check) Detail
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public static CheckDetailData F1226_GetCashLedger(int clid)
        {
            CheckDetailData checkDetailData = new CheckDetailData();
            Hashtable ht = new Hashtable();
            ht.Add("@CLID", clid);
            Utility.LoadDataSet(checkDetailData, "f1226_pcget_CheckDetail", ht, new string[] { checkDetailData.GetCheckDetail.TableName, checkDetailData.ListSubFundItem.TableName });
            return checkDetailData;
        }

        #endregion

        #region List Cash Ledger

        /// <summary>
        /// Gets the Cash Ledger ID
        /// </summary>
        /// <returns>Cash Ledger ID</returns>
        public static CheckDetailData F1226_ListCashLedger()
        {
            CheckDetailData checkDetailData = new CheckDetailData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(checkDetailData.ListCashLedgerID, "f1226_pclst_CashLedgerID", ht);
            return checkDetailData;
        }

        #endregion List Cash Ledger

        #region Save Cash Ledger

        /// <summary>
        /// Updates the Cash Ledger
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="overRide">The over ride.</param>
        /// <param name="checkDetails">The check details.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>1 if checkno already exist else 0</returns>
        public static int F1221_UpdateCashLedger(int clid, int overRide, string checkDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CLID", clid);
            ht.Add("@Override", overRide);
            ht.Add("@CheckDetails", checkDetails);
            ht.Add("@UserID", userId);
            /////Utility.ExecuteSP("f1226_pcupd_CashLedger", ht);
            ////return DataProxy.FetchSPOutput("f1226_pcupd_CashLedger", ht);  
            return Utility.FetchSPOutput("f1226_pcupd_CashLedger", ht);
        }

        /// <summary>
        /// Updates the Cash Ledger Status
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="functionDate">The function date.</param>
        /// <param name="functionId">The function id.</param>
        /// <param name="loginUserId">The loginUserId.</param>
        public static void F1226_UpdateCashLedgerStatus(int clid, int userId, DateTime functionDate, int functionId, int loginUserId)
        {            
            Hashtable ht = new Hashtable();
            ht.Add("@CLID", clid);
            ht.Add("@FunctionID", functionId);
            if (userId != -999)
            {
                ht.Add("@UserID", userId);
                ht.Add("@FunctionDate", functionDate);
            }

            ht.Add("@LoginUserID", loginUserId);
            Utility.ImplementProcedure("f1226_pcupd_CashLedgerStatus", ht);            
        }

        #endregion Save Cash Ledger

        #region Delete Cash Ledger

        /// <summary>
        /// Delete the Cash Ledger
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">The userId.</param>
        public static void F1226_DeleteCashLedger(int clid, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CLID", clid);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1226_pcdel_CashLedger", ht);
        }

        #endregion Delete Cash Ledger
    }
}
