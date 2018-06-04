// -------------------------------------------------------------------------------------------------
// <copyright file="F15018WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Feb 07        Ranjani            Created// 
//*********************************************************************************/
namespace D11018
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F15018 WorkItem
    /// </summary>
    public class F15018WorkItem : WorkItem
    {

        /// <summary>
        /// get next working day - depends on clode time.
        /// </summary>
        /// <returns>return today or next working day</returns>
        public DateTime F9001_GetNextWorkingDay()
        {
            return WSHelper.F9001_GetNextWorkingDay();
        }

        /// <summary>
        /// saves the master receipt.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="otherParameterInfo">The other parameter info.</param>
        /// <param name="sharedPaymentId">Shared Payment ID for Reverse Receipt</param>
        /// <returns>the integer - receipt id</returns>
        public int F1405_SaveMasterReceipt(int statementId, int receiptSourceId, string otherParameterInfo, int? sharedPaymentId)
        {           
            return WSHelper.F1405_SaveMasterReceipt(statementId, receiptSourceId, otherParameterInfo, sharedPaymentId);
        }

        /// <summary>
        /// Saves the auto print.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="autoPrint">if set to <c>true</c> [is auto print].</param>
        public void SaveAutoPrint(int formId, int userId, bool autoPrint)
        {
            WSHelper.SaveAutoPrint(formId, userId, autoPrint);
        }

        /// <summary>
        /// Gets the auto print status.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns true/false</returns>
        public int GetAutoPrintStatus(int formId, int userId)
        {
            return WSHelper.GetAutoPrintStatus(formId, userId);
        }

        /// <summary>
        /// Test for reciept validity
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptDate">The transaction date of the reciept.</param>
        /// <returns>The string containing the recipiet's validity information.</returns>
        public string F1009_GetValidReceiptTest(int statementId, DateTime receiptDate)
        {
            return WSHelper.F1009_GetValidReceiptTest(statementId, receiptDate);
        }

        /// <summary>
        /// Gets the Misc Receipt details based on the receiptId
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>
        /// The typed dataset containing the receipt information of the receiptId.
        /// </returns>
        public F11018MiscReceiptData F15018_GetMiscReceipt(int receiptId)
        {
            return WSHelper.F15018_GetMiscReceipt(receiptId);
        }

        /// <summary>
        /// gets the Misc Receipt template
        /// </summary>
        /// <param name="miscTemplateID">The misc template ID.</param>
        /// <returns>The typed dataset containing the receipt Template information of the miscTemplateID.</returns>
        public F11018MiscReceiptData F1021_GetMiscReceiptTemplate(int miscTemplateId)
        {
            return WSHelper.F1021_GetMiscReceiptTemplate(miscTemplateId);
        }

        /// <summary>
        /// Gets the comments based on the keyid, formid.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>The dataset containing the comment and priority.</returns>
        public CommentsData F9075_GetComment(int keyId, int formId)
        {
            return WSHelper.F9075_GetComment(keyId, formId);
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

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public F15013ExciseTaxRateData F15013_GetAccountName(int accountId)
        {
            return WSHelper.F15013_GetAccountName(accountId);
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public PartiesOwnerDetailsData GetOwnerDetails(int ownerId)
        {
            return WSHelper.GetOwnerDetails(ownerId);
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
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        } 
    }
}
