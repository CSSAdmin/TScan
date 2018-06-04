//--------------------------------------------------------------------------------------------
// <copyright file="F15011WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15011.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 24 JAN 07        Ranjani             Created// 
//*********************************************************************************/
namespace D1100
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
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F15011 WorkItem
    /// </summary>
    public class F15011WorkItem : WorkItem
    {
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
        /// Gets the Excise Receipt details 
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Excise Tax Receipt Details</returns>
        public F11011ExciseStatementData F15011_GetExciseStatement(int statementId)
        {
            return WSHelper.F15011_GetExciseStatement(statementId);
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
        /// saves the master receipt.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="otherParameterInfo">The other parameter info.</param>
        /// <returns>the integer - receipt id</returns>
        public int F1405_SaveMasterReceipt(int statementId, int receiptSourceId, string otherParameterInfo, int? sharedPaymentId)
        {
            return WSHelper.F1405_SaveMasterReceipt(statementId, receiptSourceId, otherParameterInfo, sharedPaymentId);
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
