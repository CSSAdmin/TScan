// -------------------------------------------------------------------------------------------
// <copyright file="F1220AccountRegisterComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access Disbursement related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------;

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
    /// F1220AccountRegisterComp Class
    /// </summary>
    public static class F1220AccountRegisterComp
    {
        #region List AccontNames

        /// <summary>
        /// F1220_s the account names.
        /// </summary>
        /// <returns>the datatable contains the Account Names</returns>
        public static F1220AccountRegisterData.ListAccountNamesDataTable F1220_AccountNames()
        {
            F1220AccountRegisterData accountRegisterData = new F1220AccountRegisterData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(accountRegisterData.ListAccountNames, "f1213_pclst_AccountName", ht);
            return accountRegisterData.ListAccountNames;
        }

        #endregion
        
        #region Get ReconciledDetails

        /// <summary>
        /// F1220_s the get reconciled details.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <returns>datatable holds the Reconciled Details</returns>
        public static F1220AccountRegisterData.ReconciledDetailsDataTable F1220_GetReconciledDetails(int registerId)
        {
            F1220AccountRegisterData accountRegisterData = new F1220AccountRegisterData();
            Hashtable ht = new Hashtable();
            ht.Add("@RegisterID", registerId);
            Utility.LoadDataSet(accountRegisterData.ReconciledDetails, "f1220_pcget_ReconciledDetails", ht);
            return accountRegisterData.ReconciledDetails;
        }

        #endregion

        #region List AccountRegister

        /// <summary>
        /// F1220_s the list account register.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="beginningDate">The beginning date.</param>
        /// <returns>datatable contains the Account Register Details</returns>
        public static F1220AccountRegisterData.ListAccountRegisterDataTable F1220_ListAccountRegister(int registerId, DateTime beginningDate)
        {
            F1220AccountRegisterData accountRegisterData = new F1220AccountRegisterData();
            Hashtable ht = new Hashtable();
            ht.Add("@RegisterID", registerId);
            ht.Add("@BeginningDate", beginningDate);
            Utility.LoadDataSet(accountRegisterData.ListAccountRegister, "f1220_pclst_AccountRegister", ht);
            return accountRegisterData.ListAccountRegister;
        }

        #endregion

        #region GetAccountRegisterDetails

        /// <summary>
        /// F1220_s the get account register details.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="beginningDate">The beginning date.</param>
        /// <returns>accountRegister DataSet</returns>
        public static F1220AccountRegisterData F1220_GetAccountRegisterDetails(int registerId, DateTime beginningDate)
        {
            F1220AccountRegisterData accountRegisterData = new F1220AccountRegisterData();
            Hashtable ht = new Hashtable();
            ht.Add("@RegisterID", registerId);
            if (!string.Equals(beginningDate.ToShortDateString(), "1/1/0001"))
            {
                ht.Add("@BeginningDate", beginningDate);
            }

            string[] tableName = new string[] { accountRegisterData.ReconciledDetails.TableName, accountRegisterData.ListAccountRegister.TableName };
            Utility.LoadDataSet(accountRegisterData, "f1220_pcget_ReconciledDetails", ht, tableName);
            return accountRegisterData;
        }

        #endregion
    }
}
