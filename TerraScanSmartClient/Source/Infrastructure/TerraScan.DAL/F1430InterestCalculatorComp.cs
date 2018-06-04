// -------------------------------------------------------------------------------------------
// <copyright file="F1430InterestCalculatorComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F1430InterestCalculatorComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 12/12/07        Jaya Prakash .k     Created
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
    /// F1430InterestCalculatorComp class file 
    /// </summary>    
    public static class F1430InterestCalculatorComp
    {
        /// <summary>
        /// F1430_GetCalculatorDetails gets the calculator details on load.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>TypedDataSet</returns>
        public static F1430InterestCalculatorData F1430_GetCalculatorDetails(int statementId)
        {
            F1430InterestCalculatorData getCalculatorDetails = new F1430InterestCalculatorData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            string[] tableNames = new string[] {getCalculatorDetails.GetPayDetails.TableName, getCalculatorDetails.CalculatorFields.TableName };
            Utility.LoadDataSet(getCalculatorDetails, "f1430_pcget_InterestCalc", ht, tableNames);
            return getCalculatorDetails;
        }
         
        /// <summary>
        /// F1430_GetInterestDetails get the interest and deliquency details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="taxAmount">The tax amount.</param>
        /// <returns>TypedDataSet</returns>
        public static F1430InterestCalculatorData F1430_GetInterestDetails(int statementId,DateTime interestDate, decimal taxAmount)
        {
            F1430InterestCalculatorData getInterestDetails = new F1430InterestCalculatorData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatmentID", statementId);
            ht.Add("@InterestDate", interestDate);
            ht.Add("@TaxAmount", taxAmount);
            Utility.LoadDataSet(getInterestDetails.InterestDescription, "f1430_pcget_InterestDescription", ht);
            return getInterestDetails;
        }

    }
}
