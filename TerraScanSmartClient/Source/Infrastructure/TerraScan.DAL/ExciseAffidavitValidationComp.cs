// -------------------------------------------------------------------------------------------
// <copyright file="ExciseAffidavitValidationComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access ExciseAffidavit Validation related information</summary>
// Release history
// VERSION	DESCRIPTION
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
    /// ExciseAffidavitValidationComp
    /// </summary>
    public static class ExciseAffidavitValidationComp
    {
        #region Get ExciseTaxAffidavitStatus

        /// <summary>
        /// Gets the excise tax affidavit status.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="treasurerStatus">The treasurer status.</param>
        /// <returns>The dataset containing statementID status</returns>
        public static ExciseAffidavitValidationData GetExciseTaxAffidavitStatus(int statementId, int treasurerStatus)
        {
            ExciseAffidavitValidationData exciseAffidavitValidationData = new ExciseAffidavitValidationData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            ht.Add("@IsTreasurer", treasurerStatus);
            Utility.LoadDataSet(exciseAffidavitValidationData.ListAffidavitValidation, "f1111_pcget_ExciseTaxAffidavitStatus", ht);
            return exciseAffidavitValidationData;
        }

        #endregion Get ExciseTaxAffidavitStatus

        #region Update ExciseTaxAffidavitStatus
        
        /// <summary>
        /// Updates the excise affidavit status.
        /// </summary>
        /// <param name="statementId">The statement ID.</param>
        /// <param name="treasurerStatus">The treasurer status.</param>
        /// <param name="statusId">The status ID.</param>
        /// <param name="userId">The userId.</param>
        public static void UpdateExciseAffidavitStatus(int statementId, int treasurerStatus, int statusId, int userId)
        {
            Hashtable ht = new Hashtable();

            ht.Add("@StatementID", statementId);
            ht.Add("@IsTreasurer", treasurerStatus);
            ht.Add("@StatusID", statusId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f1111_pcupd_ExciseTaxAffidavitStatus", ht);
        }

        #endregion Update ExciseTaxAffidavitStatus
    }
}
