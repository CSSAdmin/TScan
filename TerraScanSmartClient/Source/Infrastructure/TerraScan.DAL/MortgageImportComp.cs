// -------------------------------------------------------------------------------------------
// <copyright file="MortgageImportComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access MortgageImport related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using System.Data;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// Main Class For MortgageImport Comp
    /// </summary>
    public static class MortgageImportComp
    {
        #region Mortgage Import Statement Ids

        /// <summary>
        /// Gets the Mortgage Import statement Id's
        /// </summary>
        /// <returns> The dataset containing the list of Mortgage Import statementids.</returns>
        public static DataSet GetMortgageImportStatementIds()
        {
              return DataProxy.FetchDataSet("pclst_MortgageImportID");
        }
        
        #endregion

        #region MortgageImport Statement

        /// <summary>
        /// Gets the Mortgage Import statement based on the import id
        /// </summary>
        /// <param name="importId"> The importId of the statement to be fetched.</param>
        /// <param name="nextAvailableRecord">true fetch next available record if current record deleted,false previoud record</param>
        /// <returns> The dataset containing the statement information of the importId.</returns>
        public static MortageImportData GetMortgageImportStatement(int importId, bool nextAvailableRecord)
        {
            MortageImportData mortageImportData = new MortageImportData();
            Hashtable ht = new Hashtable();
            if (importId > 0)
            {
                ht.Add("@ImportID", importId);
            }

            ht.Add("@MoveNext", nextAvailableRecord);
            string[] optionalParameter = new string[] { mortageImportData.GetMortgageImportIds.TableName, mortageImportData.GetMortgageImportDetails.TableName, mortageImportData.GetMortgageImportError.TableName ,mortageImportData.PayListTable.TableName};
            Utility.LoadDataSet(mortageImportData, "f1010_pcget_MortgageImport", ht, optionalParameter);
            return mortageImportData;
        }

        #endregion

        #region Mortgage Import Template Selection

        /// <summary>
        /// Gets the Mortgage Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of Mortgage Import Template Details.</returns>
        public static MortgageImportTemplateSelectData GetMortgageImportTemplateDetails()
        {
            MortgageImportTemplateSelectData mortgageImportTemplateSelectData = new MortgageImportTemplateSelectData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(mortgageImportTemplateSelectData.ListMortgageImportTemplate, "f1011_pclst_MortgageImportTemplate", ht);
            return mortgageImportTemplateSelectData;
        }

        #endregion

        #region MortgageImport Error Check

        /// <summary>
        /// Method Will Check the Error Records for given parameters
        /// </summary>
        /// <param name="importId">importId</param>
        /// <param name="templateId">templateId</param>
        /// <param name="templateName">templateName</param>
        /// <param name="typeId">typeId</param>
        /// <param name="filePath">filePath</param>
        /// <param name="recieptDate">recieptDate</param>
        /// <param name="interestDate">interestDate</param>
        /// <param name="payCode">payCode</param>
        /// <param name="userId">userId</param>
        /// <param name="rollYear">rollYear</param>
        /// <param name="ppaymentId">The ppaymentId</param>
        /// <returns>
        /// the DataSet Containing the Error Records Information
        /// </returns>
        public static MortageImportData MortgageImportCheckErrors(int importId, int templateId, string templateName, int typeId, string filePath, DateTime recieptDate, DateTime interestDate, bool allowPartialPmts, int userId, int rollYear,int firstHalfpaycode, int ppaymentId)
        {
            MortageImportData mortageImportData = new MortageImportData();
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@TemplateID", templateId);
            ht.Add("@TemplateName", templateName);
            ht.Add("@TypeID", typeId);
            ht.Add("@FilePath", filePath);
            ht.Add("@ReceiptDate", recieptDate);
            ht.Add("@InterestDate", interestDate);
            ht.Add("@AllowPartialPmts", allowPartialPmts);
            ht.Add("@UserID", userId);
            ht.Add("@PayCode", firstHalfpaycode);

            if (ppaymentId > 0)
            {
                ht.Add("@PPaymentID", ppaymentId);
            }

            ht.Add("@RollYear", rollYear);
            string[] optionalParameter = new string[] { mortageImportData.CheckMortgageImportCheckErrors.TableName, mortageImportData.GetMortgageImportError.TableName, mortageImportData.CheckMortgageImportErrorDetails.TableName };
            Utility.LoadDataSet(mortageImportData, "f1010_pcchk_MortgageImportCheckErrors", ht, optionalParameter);
            return mortageImportData;
        }

        #endregion        

        #region Mortgage Import Import File

        /// <summary>
        /// Saves Mortgage Import Entries
        /// </summary>
        /// <param name="importId">the Import id</param>
        /// <param name="templateId">The template id</param>
        /// <param name="templateName">The template name</param>
        /// <param name="typeId">The type id</param>
        /// <param name="filePath">The file path</param>
        /// <param name="receiptDate">The receipt date</param>
        /// <param name="interestDate">The interest date</param>
        /// <param name="payCode">The pay code</param>       
        /// <param name="userId">the userId</param>
        /// <param name="rollYear">The rollyear</param>
        /// <param name="ppaymentId">The ppaymentId</param>
        /// <param name="mortgageImportEntries">The Mortgage Import Entries</param> 
        /// <returns>The DataSet containg inserted entries details</returns>  
        public static MortageImportData SaveMortgageImportEntries(int importId, int templateId, string templateName, int typeId, string filePath, DateTime receiptDate, DateTime interestDate, bool payCode, int userId, int rollYear, int ppaymentId,int firstHalfpaycode, string mortgageImportEntries)
        {
            MortageImportData mortageImportData = new MortageImportData();
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@TemplateID", templateId);
            ht.Add("@TemplateName", templateName);
            ht.Add("@TypeID", typeId);
            ht.Add("@FilePath", filePath);
            ht.Add("@ReceiptDate", receiptDate);
            ht.Add("@InterestDate", interestDate);
            ht.Add("@AllowPartialPmts", payCode);            
            ht.Add("@RollYear", rollYear); 
            ht.Add("@UserID", userId);
            ht.Add("@PayCode", firstHalfpaycode);

            if (ppaymentId > 0)
            {
                ht.Add("@PPaymentID", ppaymentId);
            }

            ht.Add("@MortgageImportEntries", mortgageImportEntries);
            string[] optionalParameter = new string[] { mortageImportData.SaveMortgageImportEntryError.TableName, mortageImportData.SaveMortgageImportEntry.TableName };
            Utility.LoadDataSet(mortageImportData, "f1010_pcins_MortgageImportEntry", ht, optionalParameter);
            return mortageImportData;
        }

        #endregion

        #region Mortgage Import Check Valid Receipt

        /// <summary>
        /// Check For Valid Receipt 
        /// </summary>
        /// <param name="importId">The import id</param>       
        /// <param name="receiptDate">The receipt date</param>       
        /// <returns>The DataSet containg valid receipt details</returns>
        public static MortageImportData CheckMortgageImportValidReceipt(int importId, DateTime receiptDate)
        {
            MortageImportData mortageImportData = new MortageImportData();
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId); 
            ht.Add("@ReceiptDate", receiptDate);
            Utility.LoadDataSet(mortageImportData.CheckMortgageImportValidReceiptTest, "f1010_pcchk_MortgageImportValidReceiptTest", ht);
            return mortageImportData;
        }

        #endregion

        #region Save Mortgage Import

        /// <summary>
        /// Saves Mortgage Import 
        /// </summary>
        /// <param name="importId">The import id</param>
        /// <param name="templateId">The template id</param>
        /// <param name="templateName">The template name</param>
        /// <param name="typeId">The type id</param>
        /// <param name="filePath">The file path</param>
        /// <param name="receiptDate">The receipt date</param>
        /// <param name="interestDate">The interest date</param>
        /// <param name="payCode">The pay code</param>
        /// <param name="userId">The user id</param>
        /// <param name="rollYear">The roll year</param>
        /// <param name="ppaymentId">The ppaymentId</param>
        /// <param name="resetErrorCheck">resetErrorCheck</param>
        /// <returns>The DataSet containg inserted entries import id</returns>
        public static MortageImportData SaveMortgageImport(int importId, int templateId, string templateName, int typeId, string filePath, DateTime receiptDate, DateTime interestDate, bool payCode, int userId, int rollYear, int ppaymentId,int firstHalfPayCode, bool resetErrorCheck)
        {
            MortageImportData mortageImportData = new MortageImportData();
            Hashtable ht = new Hashtable();
            if (importId > 0)
            {
                ht.Add("@ImportID", importId);
            }

            ht.Add("@TemplateID", templateId);
            ht.Add("@TemplateName", templateName);
            ht.Add("@TypeID", typeId);
            ht.Add("@FilePath", filePath);
            ht.Add("@ReceiptDate", receiptDate);
            ht.Add("@InterestDate", interestDate);
            ht.Add("@AllowPartialPmts", payCode);
            ht.Add("@UserID", userId);
            ht.Add("@RollYear", rollYear);
            ht.Add("@PayCode", firstHalfPayCode);

            if (ppaymentId > 0)
            {
                ht.Add("@PPaymentID", ppaymentId);
            }

            ht.Add("@ResetErrorCheck", resetErrorCheck);
            Utility.LoadDataSet(mortageImportData.SaveMortgageImport, "f1010_pcins_MortgageImport", ht);
            return mortageImportData;
        }

        #endregion

        #region Delete Mortgage Import

        /// <summary>
        /// Delete Mortgage import record
        /// </summary>
        /// <param name="importId">The import id</param>
        /// <param name="userId">userId</param>
        /// <returns>The DataSet</returns>
        public static MortageImportData DeleteMortgageImport(int importId, int userId)
        {
            MortageImportData mortageImportData = new MortageImportData();
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(mortageImportData.DeleteMortgageImport, "f1010_pcdel_MortgageImport", ht);
            return mortageImportData;
        }

        /// <summary>
        /// Delete Mortgage import file entries
        /// </summary>
        /// <param name="importId">The import id</param>
        /// <param name="userId">userId</param>
        /// <returns>The DataSet</returns>
        public static MortageImportData DeleteMortgageImportFileEntries(int importId, int userId)
        {
            MortageImportData mortageImportData = new MortageImportData();
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(mortageImportData.DeleteMortgageImportEntry, "f1010_pcdel_MortgageImportEntry", ht);
            return mortageImportData;
        }
        
        #endregion

        #region Create Receipt
        /// <summary>
        /// Saves the payment.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="templateId">The template ID.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="payCode">The pay code.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="ppaymentId">The ppayment ID.</param>
        /// <param name="resetErrorCheck">if set to <c>true</c> [reset error check].</param>
        /// <returns>The dataset</returns>
        public static MortageImportData CreateReceipt(int importId, int templateId, string templateName, string filePath, int typeId, DateTime receiptDate, DateTime interestDate, bool payCode,int firstHalfPaycode, int userId, int rollYear, int? ppaymentId, bool resetErrorCheck)
        {
            MortageImportData mortageImportData = new MortageImportData();
            Hashtable ht = new Hashtable();

            if (importId > 0)
            {
                ht.Add("@ImportID", importId);
            }

            ht.Add("@TemplateID", templateId);
            ht.Add("@TemplateName", templateName);
            ht.Add("@FilePath", filePath);
            ht.Add("@TypeID", typeId);
            ht.Add("@ReceiptDate", receiptDate);
            ht.Add("@InterestDate", interestDate);   
            ht.Add("@AllowPartialPmts", payCode);
            ht.Add("@PayCode", firstHalfPaycode);
            ht.Add("@UserID", userId);
            ht.Add("@RollYear", rollYear);
            if (ppaymentId > 0)
            {
                ht.Add("@PPaymentID", ppaymentId);
            }
            else
                ht.Add("@PPaymentID", 0);

            ht.Add("@ResetErrorCheck", resetErrorCheck);
            string[] optionalParameter = new string[] { mortageImportData.CreateRecieptError.TableName, mortageImportData.CreateRecieptDetails.TableName };
            Utility.LoadDataSet(mortageImportData, "f1010_pcins_MortgageImportReceipt", ht, optionalParameter);
            return mortageImportData;
        }

        #endregion  Create Receipt
    }
}
