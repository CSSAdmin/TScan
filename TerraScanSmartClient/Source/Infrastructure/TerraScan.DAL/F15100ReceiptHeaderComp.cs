// -------------------------------------------------------------------------------------------
// <copyright file="F15100ReceiptHeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access receipt header related information</summary>
// Release history
// VERSION	DESCRIPTION
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
    /// Main class for F15100ReceiptHeader Component
    /// </summary>
    public static class F15100ReceiptHeaderComp
    {
        #region Receipt Header

        #region GetReceiptHeader

        ///<summary>
        ///Get the Receipt Header
        ///</summary>
        ///<param name="receiptId">The Receipt ID.</param>
        /// <returns>
        /// The Typed dataset containing the details of the Receipt header.
        /// </returns>
        public static F15100ReceiptHeaderData GetReceiptHeaderDetails(int receiptId)
        {
            F15100ReceiptHeaderData form15100ReceiptHeaderData = new F15100ReceiptHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            Utility.LoadDataSet(form15100ReceiptHeaderData, "f15100_pcget_FSReceiptHeader", ht, new string[] { form15100ReceiptHeaderData.GetReceiptHeader.TableName });
            return form15100ReceiptHeaderData;
        }

        ///<summary>
        ///Get the Receipt Header
        ///</summary>
        ///<param name="receiptId">The Receipt ID.</param>
        /// <returns>
        /// The Typed dataset containing the details of the Receipt header.
        /// </returns>
        public static F15100ReceiptHeaderData GetReceiptListDetails(int receiptId)
        {
            F15100ReceiptHeaderData form15100AssociatedReceipts = new F15100ReceiptHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            Utility.LoadDataSet(form15100AssociatedReceipts, "f15105_pcget_FSRelatedReceipts", ht, new string[] { form15100AssociatedReceipts.GetAssociatedReceiptDetails.TableName });
            return form15100AssociatedReceipts;
        }
        
        #endregion GetReceiptHeader

        #endregion Receipt Header

        #region Save Receipt Header

        /// <summary>
        /// To Save the Receipt Header Receipt Number.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <param name="receiptNumber">The receipt number.</param>
        /// <param name="userId">userId</param>
        public static void F15100_SaveReceiptHeaderreceiptNumber(int receiptId, string receiptNumber, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            ht.Add("@ReceiptDetails", receiptNumber);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f15100_pcupd_FSReceiptHeader", ht);
        }

        #endregion Save Receipt Header
    }
}
