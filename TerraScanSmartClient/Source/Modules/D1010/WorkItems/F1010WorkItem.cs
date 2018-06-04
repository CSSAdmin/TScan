//--------------------------------------------------------------------------------------------
// <copyright file="F1010WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the GetCountyConfiguration,UpdateCountyConfigDetails.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 01 Aug 06        SuganthMani        Created
//*********************************************************************************/

namespace D1010
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using System.Data;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Utilities;
    using TerraScan.SmartParts;

    #endregion Namespaces

    /// <summary>
    /// Class for F1010 workitem
    /// </summary>
    public class F1010WorkItem : WorkItem 
    {        
        /// <summary>
        /// Gets the mortgage template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>DataSet</returns>
        public static MortgageImpotTemplateData GetMortgageTemplate(int templateId)
        {
            return WSHelper.GetMortgageTemplate(templateId);
        }

        /// <summary>
        /// Deletes the mortgage import.
        /// </summary>
        /// <param name="currentImportId">The current import id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>MortageImportData Dataset</returns>
        public static MortageImportData DeleteMortgageImport(int currentImportId, int userId)
        {
            return WSHelper.DeleteMortgageImport(currentImportId, userId);
        }

        /// <summary>
        /// Deletes the mortgage import file entries.
        /// </summary>
        /// <param name="currentImportId">The current import id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>DataSet</returns>
        public static MortageImportData DeleteMortgageImportFileEntries(int currentImportId, int userId)
        {
            return WSHelper.DeleteMortgageImportFileEntries(currentImportId, userId);
        }

        /// <summary>
        /// Gets the mortgage import statement.
        /// </summary>
        /// <param name="currentImportId">The current import id.</param>
        /// <param name="flag">if set to <c>true</c> [flag].</param>
        /// <returns>DataSet</returns>
        public static MortageImportData GetMortgageImportStatement(int currentImportId, bool flag)
        {
            return WSHelper.GetMortgageImportStatement(currentImportId, flag);
        }

        /// <summary>
        /// Saves the mortgage import entries.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="sourceTypeId">The source type id.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="payCode">The pay code.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="mortgageImportEntry">The mortgage import entry.</param>
        /// <returns>MortageImportData DataSet</returns>
        public static MortageImportData SaveMortgageImportEntries(int importId, int templateId, string templateName, int sourceTypeId, string filePath, DateTime receiptDate, DateTime interestDate, bool payCode, int userId, int rollYear, int ppaymentId,int firstHalfPayCode, TerraScan.BusinessEntities.ImportFile.MortgageImportEntryDataTable mortgageImportEntry)
        {
            return WSHelper.SaveMortgageImportEntries(importId, templateId, templateName, sourceTypeId, filePath, receiptDate, interestDate, payCode, userId, rollYear, ppaymentId, firstHalfPayCode, Utility.GetXmlString(mortgageImportEntry));
        }

        /// <summary>
        /// Checks the mortgage import valid receipt.
        /// </summary>
        /// <param name="currentImportId">The current import id.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <returns>MortageImportData Dataset</returns>
        public static MortageImportData CheckMortgageImportValidReceipt(int currentImportId, DateTime receiptDate)
        {
            return WSHelper.CheckMortgageImportValidReceipt(currentImportId, receiptDate);
        }

        /// <summary>
        /// Mortgages the import check errors.
        /// </summary>
        /// <param name="currentImportId">The current import id.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="sourceTypeId">The source type id.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="payCode">The pay code.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <returns>DataSet</returns>
        public static MortageImportData MortgageImportCheckErrors(int currentImportId, int templateId, string templateName, int sourceTypeId, string filePath, DateTime receiptDate, DateTime interestDate, bool payCode, int userId, int rollYear,int firstHalfpaycode, int ppaymentId)
        {
            return WSHelper.MortgageImportCheckErrors(currentImportId, templateId, templateName, sourceTypeId, filePath, receiptDate, interestDate, payCode, userId, rollYear,firstHalfpaycode, ppaymentId);
        }

        /// <summary>
        /// Saves the mortgage import.
        /// </summary>
        /// <param name="currentImportId">The current import id.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="sourceTypeId">The source type id.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="payCode">The pay code.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="resetErrorCheck">if set to <c>true</c> [reset error check].</param>
        /// <returns>DataSet</returns>
        public static MortageImportData SaveMortgageImport(int currentImportId, int templateId, string templateName, int sourceTypeId, string filePath, DateTime receiptDate, DateTime interestDate, bool payCode, int userId, int rollYear, int ppaymentId,int firstHalfPayCode, bool resetErrorCheck)
        {
            return WSHelper.SaveMortgageImport(currentImportId, templateId, templateName, sourceTypeId, filePath, receiptDate, interestDate, payCode, userId, rollYear, ppaymentId,firstHalfPayCode, resetErrorCheck); 
        }

        /// <summary>
        /// Creates the receipt.
        /// </summary>
        /// <param name="currentImportId">The current import id.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="sourceTypeId">The source type id.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="payCode">The pay code.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="flag">if set to <c>true</c> [flag].</param>
        /// <returns>DataSet</returns>
        public static MortageImportData CreateReceipt(int currentImportId, int templateId, string templateName, string filePath, int sourceTypeId, DateTime receiptDate, DateTime interestDate, bool payCode, int FirsthalfPaycode, int userId, int rollYear, int? ppaymentId, bool flag)
        {
            return WSHelper.CreateReceipt(currentImportId, templateId, templateName, filePath, sourceTypeId, receiptDate, interestDate, payCode,FirsthalfPaycode, userId, rollYear, ppaymentId, flag);
        }

        /// <summary>
        /// Additions the operation text.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>AdditionalOperationCountEntity</returns>
        ////public static AdditionalOperationCountEntity AdditionOperationText(int formId, int keyId, int userId)
        ////{
        ////   //todo: return new AdditionalOperationCountEntity(WSHelper.GetAttachmentCount(formId, keyId, userId), WSHelper.GetCommentsCount(formId, keyId, userId), false);
        ////}

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
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }
    }
}
