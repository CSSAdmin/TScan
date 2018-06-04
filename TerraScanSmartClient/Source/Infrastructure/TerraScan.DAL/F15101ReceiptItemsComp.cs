// -------------------------------------------------------------------------------------------
// <copyright file="F15101ReceiptItemsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F15101ReceiptItemsComp.cs methods
// </summary>
// Release history
// ********************************************************************************************
// Date               Author               Description
// ----------       ---------         -------------------------------------------------------
// 14/08/09         Sadha Shivudu     Added methods to implement the TSCO 2810 
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    #region Namespace

    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    #endregion Namespace

    /// <summary>
    /// Main class for ReciptItems Component
    /// </summary>
    public static class F15101ReceiptItemsComp
    {
        #region ListReceiptItems

        /// <summary>
        /// Lists the receipt items.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>the reciept items dataset.</returns>
        public static F15101ReceiptItemsData ListReceiptItems(int receiptId)
        {
            F15101ReceiptItemsData form15101ReceiptItemsData = new F15101ReceiptItemsData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            string[] tableName = new string[] { form15101ReceiptItemsData.ListReceiptItems.TableName, form15101ReceiptItemsData.GetReceiptTotal.TableName, form15101ReceiptItemsData.ValidRecord.TableName };
            Utility.LoadDataSet(form15101ReceiptItemsData, "f15101_pclst_FSReceiptTransactionItems", ht, tableName);
            return form15101ReceiptItemsData;
        }

        #endregion ListReceiptItems

        #region Update Transaction Items

        /// <summary>
        /// F15101_s the update transaction items.
        /// </summary>
        /// <param name="transactionItems">The transaction items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>the status.</returns>
        public static int F15101_UpdateTransactionItems(string transactionItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TransactionItems", transactionItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f15101_pcupd_TransactionItem", ht);
        }

        #endregion Update Transaction Items
    }
}
