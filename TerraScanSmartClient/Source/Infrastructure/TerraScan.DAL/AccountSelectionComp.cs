// -------------------------------------------------------------------------------------------
// <copyright file="AccountSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access AccountSelection related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// AccountSelectionComp class file
    /// </summary>
   public static class AccountSelectionComp
    {
        /// <summary>
        /// Gets the account selection data.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="objectname">The objectname.</param>
        /// <param name="line">The line.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="desciption">The desciption.</param>
        /// <param name="iscash">The iscash.</param>
        /// <returns>The account selection dataset.</returns>
       public static AccountSelectionData GetAccountSelectionData(string subFund, string bars, string functionName, string objectname, string line, int rollYear, string desciption, int iscash)
       {
           AccountSelectionData accountSelectionData = new AccountSelectionData();
           Hashtable ht = new Hashtable();
           ht.Add("@SubFund", subFund);
           ht.Add("@Bars", bars);
           ht.Add("@Function", functionName);
           ht.Add("@Object", objectname);
           ht.Add("@Line", line);

           if (rollYear != -999)
           {
               ht.Add("@RollYear", rollYear);
           }

           ht.Add("@Description", desciption);

           if (iscash == 1 || iscash == 2)
           {
               ht.Add("@IsCash", iscash);
           }
           else
           {
               ht.Add("@IsCash", 3);
           }

           Utility.LoadDataSet(accountSelectionData.ListAccountSelection, "f1345_pclst_Account", ht);
           return accountSelectionData;
       }
    }
}
