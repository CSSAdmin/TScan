// -------------------------------------------------------------------------------------------
// <copyright file="F1500AccountManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access Posting Errors related information</summary>
// Release history
// // Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 Nov 06		Krishna	            Created
// -------------------------------------------------------------------------------------------
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
   public static class F1500AccountManagementComp
   {
       #region Getdescription

       /// <summary>
       /// F1500_s the get description.
       /// </summary>
       /// <param name="keyId">The key ID.</param>
       /// <param name="elementName">Name of the element.</param>
       /// <returns>Description o of the element </returns>
       public static AccountManagementData F1500_GetDescription(string keyId, string elementName)
       {
           AccountManagementData accountManagement = new AccountManagementData();
           Hashtable ht = new Hashtable();
           ht.Add("@KeyID", keyId);
           ht.Add("@Element", elementName);
           Utility.LoadDataSet(accountManagement.GetDescription, "f1500_pcget_AccountElementDescription", ht);
           return accountManagement;
       }

       #endregion

       #region Get SubFund Items

       /// <summary>
       /// F1500_s the get sub fund items.
       /// </summary>
       /// <param name="subFund">The sub fund.</param>
       /// <param name="rollYear">The roll year.</param>
       /// <returns>accountManagement</returns>
       public static AccountManagementData F1500_GetSubFundItems(string subFund, short rollYear)
       {
           AccountManagementData accountManagement = new AccountManagementData();
           Hashtable ht = new Hashtable();
           ht.Add("@SubFund", subFund);
           if (rollYear != -1)
           {
               ht.Add("@RollYear", rollYear);
           }
           else
           {
               ht.Add("@RollYear", DBNull.Value);
           }

           Utility.LoadDataSet(accountManagement.getSubFundItems, "f9001_pcget_Subfund", ht);
           return accountManagement;
       }

       #endregion

       #region Get Function Items

       /// <summary>
       /// F1500_s the get function items.
       /// </summary>
       /// <param name="function">The function.</param>
       /// <returns>accountManagement</returns>
       public static AccountManagementData F1500_GetFunctionItems(string function)
       {
           AccountManagementData accountManagement = new AccountManagementData();
           Hashtable ht = new Hashtable();
           ht.Add("@FunctionID", function);
           Utility.LoadDataSet(accountManagement.GetFunctionItems, "f9001_pcget_Function", ht);
           return accountManagement;
       }

       #endregion

       #region Get AccountIDs and Details

       /// <summary>
       /// F1500_s the list account details.
       /// </summary>
       /// <param name="accountId">The account ID.</param>
       /// <returns>AccountIDs and Details </returns>
       public static AccountManagementData F1500_ListAccountDetails(int accountId)
       {
           AccountManagementData accountManagement = new AccountManagementData();
           Hashtable ht = new Hashtable();
           if (accountId != 0)
           {
               ht.Add("@AccountID", accountId);
           }
           else
           {
               ht.Add("@AccountID", DBNull.Value);
           }

           Utility.LoadDataSet(accountManagement, "f1500_pcget_AccountDetail", ht, new string[] { accountManagement.ListAccountDetails.TableName });
           return accountManagement;
       }

       #endregion

       #region Save and Edit the Account Details

       /// <summary>
       /// F1500_s the create or edit account.
       /// </summary>
       /// <param name="accountId">The account ID.</param>
       /// <param name="acctEmelemts">The acct emelemts.</param>
       /// <param name="userId">userId</param>
       /// <returns>errorId</returns>
       public static int F1500_CreateOrEditAccount(int accountId, string acctEmelemts, int userId)
       {
           Hashtable ht = new Hashtable();
           if (accountId != 0)
           {
               ht.Add("@AccountID", accountId);
           }
           else
           {
               ht.Add("@AccountID", DBNull.Value);
           }
           
           ht.Add("@AcctEmelemts ", acctEmelemts);
           ht.Add("@UserID", userId);

           int errorId;

           errorId = Utility.FetchSPExecuteKeyId("f1500_pcins_AccountDetail", ht);
           return errorId;
       }

       #endregion

       #region List Register type

       /// <summary>
       /// List the register types.
       /// </summary>
       /// <returns>AccountManagementData with register type</returns>
       public static AccountManagementData F1500_ListRegisterType()
       {
           AccountManagementData accountManagement = new AccountManagementData();
           Hashtable ht = new Hashtable();
           Utility.LoadDataSet(accountManagement.ListRegisterType, "f1500_pclst_CLRegisterType", ht);
           return accountManagement;
       }

       #endregion

       #region Get Configuration Value

       /// <summary>
       /// F1500_s the get configuration value.
       /// </summary>
       /// <param name="cfgName">Name of the CFG.</param>
       /// <returns>accountManagement</returns>
       public static AccountManagementData F1500_GetConfigurationValue(string cfgName)
       {
           AccountManagementData accountManagement = new AccountManagementData();
           Hashtable ht = new Hashtable();
           ht.Add("@CfgName", cfgName);
           Utility.LoadDataSet(accountManagement.GetConfiguration, "f9020_pcget_Configuration", ht);
           return accountManagement;
       }

       #endregion
   }
}
