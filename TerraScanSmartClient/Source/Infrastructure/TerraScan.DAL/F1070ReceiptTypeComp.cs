// -------------------------------------------------------------------------------------------
// <copyright file="F1070ReceiptTypeComp.cs" company="Congruent">
//    Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36066TrendComp.cs methods
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------         -------------------------------------------------------
// 21/08/2009     D.LathaMahewari       Created
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// F1070 ReceiptTypeComp
    /// </summary>
    public static class F1070ReceiptTypeComp
    {
        #region Get Receipt Type

        /// <summary>
        /// Gets the type of the receipt.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>Receipt Types</returns>
        public static F1070ReceiptTypeData F15020_GetReceiptTypes(int userId, short formId, int keyId)
        {
            F1070ReceiptTypeData receiptTypes = new F1070ReceiptTypeData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@Form", formId);
            ht.Add("@KeyID", keyId);
            Utility.LoadDataSet(receiptTypes.ReceiptTypes, "f1070_pclst_ReceiptTypes", ht);
            return receiptTypes;
        }

        #endregion Get Receipt Type
    }
}
