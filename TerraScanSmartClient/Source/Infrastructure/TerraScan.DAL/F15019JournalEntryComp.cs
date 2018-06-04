// -------------------------------------------------------------------------------------------
// <copyright file="F15019JournalEntryComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access Journal Entry related information</summary>
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
    /// Main class for F15019JournalEntry Component
    /// </summary>
    public static class F15019JournalEntryComp
    {
        #region Get JournalEntry
        /// <summary>
        /// F15019s the get journal entry details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>        
        /// <returns>Typed dataset</returns>
        public static F15019JournalEntryData F15019GetJournalEntryDetails(int receiptId)
        {
            F15019JournalEntryData form15019journalEntryData = new F15019JournalEntryData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            Utility.LoadDataSet(form15019journalEntryData, "f15019_pcget_JournalEntryDetail", ht, new string[] { form15019journalEntryData.F15019GetJournalEntryDetails.TableName });
            return form15019journalEntryData;
        }

        #endregion Get JournalEntry

        #region Save JournalEntry

        /// <summary>
        /// F15019s the insert journal entry details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="journalItems">The journal items.</param>
        /// <returns>Integer value</returns>
        public static int F15019InsertJournalEntryDetails(int statementId, int receiptSourceId, string journalItems)
        {            
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", 0);
            ht.Add("@ReceiptSourceID", 1);
            ht.Add("@OtherParameterInfo", journalItems);
            
            int journalItemsId;
            journalItemsId = Utility.FetchSPExecuteKeyId("f1405_pcins_MasterReceipt", ht);
            return journalItemsId;
        }

        /// <summary>
        /// F15019_s the check roll year.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="journalItems">The journal items.</param>
        /// <returns>Integer Value</returns>
        public static int F15019_CheckRollYear(int statementId, int receiptSourceId, string journalItems)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", 0);
            ht.Add("@ReceiptSourceID", 1);
            ht.Add("@OtherParameterInfo", journalItems);

            int journalItemsId;
            journalItemsId = Utility.FetchSPExecuteKeyId("f15019_pcchk_JournalEntry", ht);
            return journalItemsId;
        }
        #endregion Save JournalEntry
    }
}
