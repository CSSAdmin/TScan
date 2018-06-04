// -------------------------------------------------------------------------------------------
// <copyright file="F15102ReceiptStatementHeaderComp.cs" company="Congruent">
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
    /// Main class for ReceiptStatementHeader Component
    /// </summary>
   public static class F15102ReceiptStatementHeaderComp
    {
        ///<summary>
        ///Get the Receipt Header
        ///</summary>
        ///<param name="receiptId">The Receipt ID.</param>
        /// <returns>
        /// The Typed dataset containing the details of the Receipt header.
        /// </returns>
       public static F15102ReceiptStatementHeaderData GetReceiptStatementHeaderDetails(int receiptId)
       {
           F15102ReceiptStatementHeaderData form15102ReceiptStatementHeaderData = new F15102ReceiptStatementHeaderData();
           Hashtable ht = new Hashtable();
           ht.Add("@ReceiptID", receiptId);
           Utility.LoadDataSet(form15102ReceiptStatementHeaderData, "f15102_pcget_FSReceiptStatementHeader", ht, new string[] { form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.TableName, form15102ReceiptStatementHeaderData.ValidRecord.TableName });
           return form15102ReceiptStatementHeaderData;
       }
    }
}
