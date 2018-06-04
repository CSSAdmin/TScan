//--------------------------------------------------------------------------------------------
// <copyright file="F1013WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1013 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-05-2007       Shiva              Created
//*********************************************************************************/

namespace D1013
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F1013 WorkItem.
    /// </summary>
    public class F1013WorkItem : WorkItem
    { 
        /// <summary>
        /// F1013_s the list unpaid receipts.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>returns BatchPaymentMgmtDataSet.</returns>
        public F1013BatchPaymentMgmtData F1013_ListUnpaidReceipts(int? userId)
        {
            return WSHelper.F1013_ListUnpaidReceipts(userId);
        }

        /// <summary>
        /// F1013_s the save batch payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="paymentItemsXml">The payment items XML.</param>
        /// <param name="receiptItemsXml">The receipt items XML.</param>
        /// <returns>returns the error id.</returns>
        public int F1013_SaveBatchPayment(int ppaymentId, int userId, string paymentItemsXml, string receiptItemsXml, string receiptDate)
        {
            return WSHelper.F1013_SaveBatchPayment(ppaymentId, userId, paymentItemsXml, receiptItemsXml, receiptDate);
        }
        
        #region F1013_ListSnapShotItems

        /// <summary>
        /// To list snap shot items collection.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <returns>Typed dataset containing the snap shot items collection</returns>
        public F1013BatchPaymentMgmtData F1013_ListSnapShotItems(int snapShotId)
        {
            return WSHelper.F1013_ListSnapShotItems(snapShotId);
        }

        #endregion F1013_ListSnapShotItems

        #region F1013_DeleteReceiptItems

        /// <summary>
        /// F1013_s the delete receipt items.
        /// </summary>
        /// <param name="paymentId">The payment id.</param>
        /// <param name="receiptItems">The receipt items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        public int F1013_DeleteReceiptItems(int paymentId, string receiptItems, int userId)
        {
            return WSHelper.F1013_DeleteReceiptItems(paymentId, receiptItems, userId);
        }

        #endregion F1013_DeleteReceiptItems

        #region AutoPrint Methods

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

        #endregion AutoPrint Methods

        #region GetConfigValue

        /// <summary>
        /// Gets the Image Patha
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Config value.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #endregion GetConfigValue

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
    }
}
