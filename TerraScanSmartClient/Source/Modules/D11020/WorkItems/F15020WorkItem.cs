// -------------------------------------------------------------------------------------------------
// <copyright file="F15020WorkItem.cs" company="Congruent">
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
// 19 Dec 06        Ranjani            Created// 
//*********************************************************************************/
namespace D11020
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
    /// F15020 WorkItem
    /// </summary>
    public class F15020WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">The user id.</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        /// <summary>
        /// list history grid.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>F15020ReceiptEngineData with receipt history and Detail</returns>
        public F15020ReceiptEngineData F15020_ListHistoryGrid(int statementId)
        {           
            return WSHelper.F15020_ListHistoryGrid(statementId);
        }

        /// <summary>
        /// get receipt details and payment items.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>F15020ReceiptEngineData with receipt detail</returns>
        public F15020ReceiptEngineData F15020_GetReceiptDetails(int receiptId)
        {            
            return WSHelper.F15020_GetReceiptDetails(receiptId);
        }

        /// <summary>
        /// Gets the minimum tax due amount
        /// </summary>
        /// <param name="statmentId">The statment id.</param>
        /// <param name="interestDate">The interest date of the reciept.</param>
        /// <returns>
        /// The decimal containing minimum tax amount due.
        /// </returns>
        public decimal F1003_GetMinTaxDue(int statmentId, string interestDate)
        {
            return WSHelper.F1003_GetMinTaxDue(statmentId, interestDate);
        }

        /// <summary>
        /// Get the interest amoount.
        /// </summary>
        /// <param name="statmentId">The statment id.</param>
        /// <param name="interestDate">The interest date of the reciept.</param>
        /// <param name="taxDueAmount">The tax due amount.</param>
        /// <returns>
        /// The decimal containing the interest information.
        /// </returns>
        public decimal F1004_GetInterestAmount(int statmentId, string interestDate, decimal taxDueAmount)
        {
            return WSHelper.F1004_GetInterestAmount(statmentId, interestDate, taxDueAmount);
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
        /// <param name="sharedPaymentId">Shared Payment ID for Reverce Receipt</param>
        /// <returns>the integer - receipt id</returns>
        public int F1405_SaveMasterReceipt(int statementId, int receiptSourceId, string otherParameterInfo, int? sharedPaymentId)
        {
            return WSHelper.F1405_SaveMasterReceipt(statementId, receiptSourceId, otherParameterInfo, sharedPaymentId);
        }

        /// <summary>
        /// F9025s the save validation details.
        /// </summary>
        /// <param name="formid">The formid.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="keyid">The keyid.</param>
        /// <returns>the integer - validated id</returns>
        public int F9025SaveValidationDetails(int formid, int userid, int keyid)
        {
            return WSHelper.F9025SaveValidationDetails(formid, userid, keyid);
        }
        
        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <returns>Tender type configuartion information</returns>
        public PaymentEngineData GetTenderTypeConfiguration()
        {          
            return WSHelper.GetTenderTypeConfiguration();
        }

        /// <summary>
        /// Lists the tender type.
        /// </summary>
        /// <param name="allowOverUnder">if set to <c>true</c> [allow over under].</param>
        /// <returns>
        /// The typed dataset containing the types of tender.
        /// </returns>
        public ReceiptEngineData.ListTenderTypeDataTable F1018_ListTenderType(bool allowOverUnder)
        {
            return WSHelper.ListTenderType(allowOverUnder).ListTenderType;
        }

        /// <summary>
        /// Saves the payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="ownerId">The ownerId id.</param>
        /// <returns>Return PPayment ID</returns>
        public int F1018_SavePayment(int ppaymentId, string paymentItems, int userId, int ownerId)
        {
            return WSHelper.SavePayment(ppaymentId, paymentItems, userId, ownerId);
        }

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        /// <summary>
        /// Gets the Attachments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
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
        /// Gets the config AutoPrint
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #region F15020 Receipt Type

        /// <summary>
        /// F15020_s the get receipt types.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>Receipt Types</returns>
        public F1070ReceiptTypeData F15020_GetReceiptTypes(int userId, short formId, int keyId)
        {
            return WSHelper.F15020_GetReceiptTypes(userId, formId, keyId);
        }

        #endregion F15020 Receipt Type

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public PaymentEngineData F1019_GetPayeeDetails(int ownerId)
        {
            return WSHelper.F1019_GetPayeeDetails(ownerId);
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
