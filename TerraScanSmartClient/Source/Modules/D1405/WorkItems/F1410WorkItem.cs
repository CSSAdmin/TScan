//--------------------------------------------------------------------------------------------
// <copyright file="F1410WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19 July 06		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D1405
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
    /// F1410WorkItem
    /// </summary>
    public class F1410WorkItem : WorkItem
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
        /// Gets the owner receipting.
        /// </summary>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns Owner Reeipting DataSet</returns>
        public F1410OwnerReceiptingData GetOwnerReceipting(string interestDate, string ownerId, string parcelIDs)
        {
            return WSHelper.F1410_GetOwnerReceipting(interestDate, ownerId, parcelIDs );
        }

        /// <summary>
        /// Lists the owner receipting.
        /// </summary>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="statementXml">The statement XML.</param>
        /// <param name="formBackColor">form Backcolor</param>
        /// <returns>Returns Datatable</returns>
        public F1410OwnerReceiptingData ListOwnerReceipting(string interestDate, string statementXml, string formBackColor)
        {
            return WSHelper.F1410_ListOwnerReceipting(interestDate, statementXml, formBackColor);//.ListOwnerStatementTable;
        }

        /// <summary>
        /// F1410_s the delete owner receipting.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="ownerXml">The owner XML.</param>
        /// <param name="statementXml">The statement XML.</param>
        /// <param name="formBackColor">form BackColor</param>
        /// <returns>Returns Owner Receipting DataSet</returns>
        public F1410OwnerReceiptingData DeleteOwnerReceipting(int ownerId, string ownerXml, string statementXml,int userId, string formBackColor)
        {
            return WSHelper.F1410_DeleteOwnerReceipting(ownerId, ownerXml, statementXml, userId, formBackColor);
        }

        /// <summary>
        /// F1410_s the save owner receipting.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="paymentOption">The payment option.</param>
        /// <param name="statementXml">The statement XML.</param>
        /// <returns>Returns Owner Receipting DataSet</returns>
        public string F1410_SaveOwnerReceipting(int userId, string receiptDate, string interestDate, int ppaymentId, int paymentOption, string statementXml)
        {
            return WSHelper.F1410_SaveOwnerReceipting(userId, receiptDate, interestDate, ppaymentId, paymentOption, statementXml);
        }

        #region List Attachmet Details

        /// <summary>
        /// List attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>Typed DataSet</returns>
        public F1410OwnerReceiptingData F1410_ListAttachmentDetails(int formId, string keyIds, int userId, int moduleId)
        {
            return WSHelper.F1410_ListAttachmentDetails(formId, keyIds, userId, moduleId);
        }

        #endregion List Attachmet Details

        #region Delete Attachment Details

        /// <summary>
        /// Delete attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        public void F2550_DeleteAttachmentDetails(int formId)
        {
            WSHelper.F2550_DeleteAttachmentDetails(formId);
        }

        #endregion

        #region Get FilePath

        /// <summary>
        /// Gets the files path
        /// </summary>
        /// <param name="source"> The source path of the file.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="extension"> The file extension.</param>
        /// <returns> The typed dataset containing the path of the file.</returns>
        public AttachmentsData.GetFilePathDataTable GetFilePath(string source, int formId, int keyId, string extension)
        {

            return WSHelper.GetFilePath(source, formId, keyId, extension, TerraScan.Common.TerraScanCommon.UserId).GetFilePath;
        }

        #endregion Get FilePath


        /// <summary>
        /// F1410_s the save owner receipt preview.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="statementDetails">The statement details.</param>
        /// <returns>int</returns>
        public int F1410_SaveOwnerReceiptPreview(int userId, string statementDetails)
        {
            return WSHelper.F1410_SaveOwnerReceiptPreview(userId, statementDetails);
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
