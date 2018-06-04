// -------------------------------------------------------------------------------------------
// <copyright file="F1405MasterReceiptingComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to insert Receipts</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 27 Dec 06		Ranjani	            Created
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
    /// Main class for Master Receipting Component
    /// </summary>
    public static class F1405MasterReceiptingComp
    {
        #region SaveReceipt

        /// <summary>
        /// saves the master receipt.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="otherParameterInfo">The other parameter info.</param>
        /// <param name="sharedPaymentId"> Shared Payment ID.</param>
        /// <returns>the integer - receipt id</returns>
        public static int F1405_SaveMasterReceipt(int statementId, int receiptSourceId, string otherParameterInfo, int? sharedPaymentId)
        {            
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            ht.Add("@ReceiptSourceID", receiptSourceId);
            ht.Add("@OtherParameterInfo", otherParameterInfo);
            ht.Add("@SharedPaymentID", sharedPaymentId);
            return Utility.FetchSPExecuteKeyId("f1405_pcins_MasterReceipt", ht);           
        }

        #endregion SaveReceipt
    }
}
