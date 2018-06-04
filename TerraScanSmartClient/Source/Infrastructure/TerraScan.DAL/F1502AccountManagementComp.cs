// ------------------------------------------------------------------------------------------------------------
// <copyright file="F1502AccountManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F1502AccountManagementComp.cs methods</summary>
// Release history
//*************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ------------------------------------------------------------------------
// 
// 
// ------------------------------------------------------------------------------------------------------------

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
    /// Class file for F1502AccountManagementComp
    /// </summary>
    public static class F1502AccountManagementComp
    {
        #region F1502 Account Element Management

        #region GetAccountElementMgmt

        /// <summary>
        /// To get Account Element Management details
        /// </summary>
        /// <param name="function">The Function Id</param>
        /// <param name="description">The Description</param>
        /// <param name="type">The Type - SemiAnnualCode </param>
        /// <returns>Typed Dataset containing the Account Element Management details</returns>
        public static F1502AccountManagementData F1502_GetAccountElementMgmt(string function, string description, int type)
        {
            F1502AccountManagementData accountElementManagementData = new F1502AccountManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@Function", function);
            ht.Add("@Description", description);
            if (type != -999)
            {
                ht.Add("@Type", type);
            }

            Utility.LoadDataSet(accountElementManagementData.GetAccountElementMgmt, "f1502_pclst_AcctElementMgmt", ht);
            return accountElementManagementData;
        }

        #endregion GetAccountElementMgmt

        #region SaveAccountElementMgmt

        /// <summary>
        /// To Save Account Element Management details
        /// </summary>
        /// <param name="functionElemnts">The xml string which contains the Account elements mgmt Grid values</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value containing whether save is compleded or not</returns>
        public static int F1502_SaveAccountElementMgmt(string functionElemnts, int userId)
        {           
            Hashtable ht = new Hashtable();
            ht.Add("@FunctionElemnts", functionElemnts);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecutedReturnValue("f1502_pcins_AcctElementMgmt", ht);  
        }

        #endregion SaveAccountElementMgmt

        #region DeleteAccountElementMgmt

        /// <summary>
        /// To Delete Account Element Management details
        /// </summary>
        /// <param name="functionId">The Functional Id</param>
        /// <param name="userId">userId</param>
        public static void F1502_DeleteAccountElementMgmt(string functionId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FunctionID", functionId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1502_pcdel_AcctElementMgmt", ht);
        }

        #endregion DeleteAccountElementMgmt

        #endregion F1502 Account Element Management
    }
}
