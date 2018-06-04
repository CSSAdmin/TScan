//--------------------------------------------------------------------------------------------
// <copyright file="F15035WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F15035 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-05-2007       Shiva              Created
//*********************************************************************************/
namespace D11035
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F15035 WorkItem
    /// </summary>
    public class F15035WorkItem : WorkItem
    {
        #region Protected Methods

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

        #endregion

        #region GetForm Detials

        /// <summary>
        /// Gets the form details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>FormDetails DataSet</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        #endregion

        /// <summary>
        /// Saves the auto print.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="autoPrint">if set to <c>true</c> [auto print].</param>
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
        /// F15035_s the get suspended payment detials.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns></returns>
        public F15035SuspendedPaymentsData F15035_GetSuspendedPaymentDetials(int receiptId)
        {
            return WSHelper.F15035SuspendedPayments(receiptId);
        }

        /// <summary>
        /// F15035_s the create suspended payment receipt.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="otherParameterInfo">The other parameter info.</param>
        /// <param name="sharedPaymentId">Shared Payment ID for Reverse Receipt</param>
        /// <returns>returns the Primary Id value.</returns>
        public int F15035_CreateSuspendedPaymentReceipt(int statementId, int receiptSourceId, string otherParameterInfo, int? sharedPaymentId)
        {
            return WSHelper.F1405_SaveMasterReceipt(statementId, receiptSourceId, otherParameterInfo, sharedPaymentId);
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
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public PaymentEngineData F1019_GetPayeeDetails(int ownerId)
        {
            return WSHelper.F1019_GetPayeeDetails(ownerId);
        }

        /// <summary>
        /// Test for reciept validity
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptDate">The transaction date of the reciept.</param>
        /// <returns>The string containing the recipiet's validity information.</returns>
        public string F11035_GetValidReceiptTest(int statementId, DateTime receiptDate)
        {
            return WSHelper.F1009_GetValidReceiptTest(statementId, receiptDate);
        }

        /// <summary>
        /// F15035_s the delete suspended payment.
        /// </summary>
        /// <param name="receiptID">The receipt ID.</param>
        public void F15035_DeleteSuspendedPayment(int receiptID, int userId)
        {
            WSHelper.F15035_DeleteSuspendedPayment(receiptID, userId);
        }

        /// <summary>
        /// F15035_s the check suspended accounts.
        /// </summary>
        /// <returns>status id</returns>
        public int F15035_CheckSuspendedAccounts()
        {
            return WSHelper.F15035_CheckSuspendedAccounts();
        }
    }
}
