// -------------------------------------------------------------------------------------------------
// <copyright file="F15019WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D11019
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using System.Data;
    using TerraScan.BusinessEntities;

    #endregion NameSpaces

    /// <summary>
    /// F15019WorkItem class
    /// </summary>
    public class F15019WorkItem : WorkItem
    {
        #region F15019WorkItem
        /// <summary>
        /// F15019WorkItem
        /// </summary>
        /// <param name="receiptId">ReceiptId</param>
        /// <returns>GetJournalEntryDetails</returns>
        public F15019JournalEntryData F15019GetJournalEntryDetails(int receiptId)
        {
            return WSHelper.F15019GetJournalEntryDetails(receiptId);
        }

        /// <summary>
        /// get next working day - depends on close time.
        /// </summary>
        /// <returns>return today or next working day</returns>
        public DateTime F9001_GetNextWorkingDay()
        {
            return WSHelper.F9001_GetNextWorkingDay();
        }

        /// <summary>
        /// F15019s the save journal entry details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <param name="journalItems">The journal items.</param>
        /// <returns></returns>
        public int F15019UpdateJournalEntryDetails(int statementId, int receiptSourceId, string journalItems)
        {
            return WSHelper.F15019UpdateJournalEntryDetails(statementId, receiptSourceId, journalItems);
        }


        /// <summary>
        /// F15019_s the check roll year.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="journalItems">The journal items.</param>
        /// <returns></returns>
        public int F15019_CheckRollYear(int statementId, int receiptSourceId, string journalItems)
        {
            return WSHelper.F15019_CheckRollYear(statementId, receiptSourceId, journalItems);
        }

             
        #endregion F15100WorkItem

        #region Get Form Slice Permission Details

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">userId</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }
        
        /// <summary>
        /// List account details
        /// </summary>
        /// <param name="filterValue">Filter Value</param>
        /// <returns>Account details</returns>
        public F11018MiscReceiptData F15018_ListAccountDetails(string filterDetails,int? rollYear,int? formNo)
        {
            return WSHelper.F15018_ListAccountDetails(filterDetails,rollYear,formNo);
        }

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public ExciseTaxRateData GetAccountName(int accountId)
        {
            return WSHelper.GetAccountName(accountId);
        }

        /// <summary>
        /// Gets the config Roll Year.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #endregion Get Form Slice Permission Details
    }
}
