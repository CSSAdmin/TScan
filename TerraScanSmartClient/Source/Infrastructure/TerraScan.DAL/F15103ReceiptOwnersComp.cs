// -------------------------------------------------------------------------------------------
// <copyright file="F15103ReceiptOwnersComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access receipt header related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;
    #endregion Namespace

    /// <summary>
    /// Class for ReceiptOwners Component
    /// </summary>    
    public static class F15103ReceiptOwnersComp
    {
        ///<summary>
        ///List the ReceiptItems
        ///</summary>
        ///<param name="receiptId">The Receipt ID.</param>
        /// <returns>
        /// The Typed dataset containing the details of the ReceiptItems.
        /// </returns>
        public static F15103ReceiptOwnersData ListReceiptOwners(int receiptId)
        {
            F15103ReceiptOwnersData form15103ReceiptOwnersData = new F15103ReceiptOwnersData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            Utility.LoadDataSet(form15103ReceiptOwnersData, "f15103_pclst_FSReceiptOwner", ht, new string[] { form15103ReceiptOwnersData.ListReceiptOwners.TableName, form15103ReceiptOwnersData.ValidRecord.TableName });
            return form15103ReceiptOwnersData;
        }
    }
}
