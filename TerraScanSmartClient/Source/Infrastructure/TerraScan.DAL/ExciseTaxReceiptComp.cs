// -------------------------------------------------------------------------------------------
// <copyright file="ExciseTaxReceiptComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update NextNumber Configuration</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 July 06		JYOTHI P	            Created
// 21 July 06       JYOTHI P                Added GetExciseTaxReceipt method
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// Main class for Excise Tax Receipt Component
    /// </summary>
    public static class ExciseTaxReceiptComp
    {
        #region Get

        /// <summary>
        /// Gets the Excise Tax Receipt
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>The typed dataset containing the ExciseTaxReceipt information based on statementId</returns>
        public static ExciseTaxReceiptData GetExciseTaxReceipt(int statementId)
        {
            ExciseTaxReceiptData exciseTaxReceiptData = new ExciseTaxReceiptData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            Utility.LoadDataSet(exciseTaxReceiptData, "f1100_pcget_ExciseTaxReceipt", ht);
            return exciseTaxReceiptData;
        }
        #endregion
    }
}
