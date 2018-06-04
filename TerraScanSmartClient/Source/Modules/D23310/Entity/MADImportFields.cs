//--------------------------------------------------------------------------------------------
// <copyright file="MADImportFields.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Static Variables.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02160708        priyadharshini                    	    Created// 
//*********************************************************************************/

namespace D23310
{
    using System;
    using TerraScan.Common;

    /// <summary>
    /// static class containing available MAD import fields
    /// </summary>
    public class MADImportFields
    {
        /// <summary>
        /// Import Id
        /// </summary>
        private int importId;

        /// <summary>
        /// Template Id
        /// </summary>
        private int templateId;

        /// <summary>
        /// Template Name
        /// </summary>
        private string templateName;

        /// <summary>
        /// Interest Date
        /// </summary>
        private DateTime interestDate;

        /// <summary>
        /// Receipt Date
        /// </summary>
        private DateTime receiptDate;

        /// <summary>
        /// Pay Code
        /// </summary>
        private bool payCode;

        /// <summary>
        /// Source Type Id
        /// </summary>
        private int sourceTypeId;

        /// <summary>
        /// File Path
        /// </summary>
        private string filePath;

        /// <summary>
        /// Roll Year
        /// </summary>
        private int rollYear;

        /// <summary>
        /// ppaymentid
        /// </summary>
        private int ppaymentId;

        /// <summary>
        /// error entries count
        /// </summary>
        private string errorEntries;

        /// <summary>
        /// Import File entries count
        /// </summary>
        private string importedEntries;

        /// <summary>
        /// Import File Process Status
        /// </summary>
        private TerraScanCommon.StatusAction importFileStatus;

        /// <summary>
        /// Check For Errors Process Status
        /// </summary>
        private TerraScanCommon.StatusAction checkErrorStatus;

        /// <summary>
        /// Create Receipts Process Status
        /// </summary>
        private TerraScanCommon.StatusAction createReceiptStatus;

        /// <summary>
        /// Print Receipts Process Status
        /// </summary>
        private TerraScanCommon.StatusAction printReceiptStatus;

        /// <summary>
        /// Gets or sets the import id.
        /// </summary>
        /// <value>The import id.</value>
        public int ImportId
        {
            get { return this.importId; }
            set { this.importId = value; }
        }       

        /// <summary>
        /// Gets or sets the template id.
        /// </summary>
        /// <value>The template id.</value>
        public int TemplateId
        {
            get { return this.templateId; }
            set { this.templateId = value; }
        }        

        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        /// <value>The name of the template.</value>
        public string TemplateName
        {
            get { return this.templateName; }
            set { this.templateName = value; }
        }       

        /// <summary>
        /// Gets or sets the receipt date.
        /// </summary>
        /// <value>The receipt date.</value>
        public DateTime ReceiptDate
        {
            get { return this.receiptDate; }
            set { this.receiptDate = value; }
        }        

        /// <summary>
        /// Gets or sets the interest date.
        /// </summary>
        /// <value>The interest date.</value>
        public DateTime InterestDate
        {
            get { return this.interestDate; }
            set { this.interestDate = value; }
        }        

        /// <summary>
        /// Gets or sets the pay code.
        /// </summary>
        /// <value>The pay code.</value>
        public Boolean PayCode
        {
            get { return this.payCode; }
            set { this.payCode = value; }
        }       

        /// <summary>
        /// Gets or sets the source type id.
        /// </summary>
        /// <value>The source type id.</value>
        public int SourceTypeId
        {
            get { return this.sourceTypeId; }
            set { this.sourceTypeId = value; }
        }       

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }       

        /// <summary>
        /// Gets or sets the roll year.
        /// </summary>
        /// <value>The roll year.</value>
        public int RollYear
        {
            get { return this.rollYear; }
            set { this.rollYear = value; }
        }        

        /// <summary>
        /// Gets or sets the ppayment id.
        /// </summary>
        /// <value>The ppayment id.</value>
        public int PpaymentId
        {
            get { return this.ppaymentId; }
            set { this.ppaymentId = value; }
        }       

        /// <summary>
        /// Gets or sets the imported entries.
        /// </summary>
        /// <value>The imported entries.</value>
        public string ImportedEntries
        {
            get { return this.importedEntries; }
            set { this.importedEntries = value; }
        }        

        /// <summary>
        /// Gets or sets the error entries.
        /// </summary>
        /// <value>The error entries.</value>
        public string ErrorEntries
        {
            get { return this.errorEntries; }
            set { this.errorEntries = value; }
        }        

        /// <summary>
        /// Gets or sets the import file status.
        /// </summary>
        /// <value>The import file status.</value>
        public TerraScanCommon.StatusAction ImportFileStatus
        {
            get { return this.importFileStatus; }
            set { this.importFileStatus = value; }
        }        

        /// <summary>
        /// Gets or sets the check error status.
        /// </summary>
        /// <value>The check error status.</value>
        public TerraScanCommon.StatusAction CheckErrorStatus
        {
            get { return this.checkErrorStatus; }
            set { this.checkErrorStatus = value; }
        }
       
        /// <summary>
        /// Gets or sets the create receipt status.
        /// </summary>
        /// <value>The create receipt status.</value>
        public TerraScanCommon.StatusAction CreateReceiptStatus
        {
            get { return this.createReceiptStatus; }
            set { this.createReceiptStatus = value; }
        }       

        /// <summary>
        /// Gets or sets the print receipt status.
        /// </summary>
        /// <value>The print receipt status.</value>
        public TerraScanCommon.StatusAction PrintReceiptStatus
        {
            get { return this.printReceiptStatus; }
            set { this.printReceiptStatus = value; }
        }
    }
}
