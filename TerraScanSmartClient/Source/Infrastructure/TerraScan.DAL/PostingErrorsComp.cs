// -------------------------------------------------------------------------------------------
// <copyright file="PostingErrorsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access Posting Errors related information</summary>
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
    /// PostingErrorsComp class file
    /// </summary>
    public static class PostingErrorsComp
    {
        /// <summary>
        /// Lists the posting errors.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>ListPostingErrors data table</returns>
        public static PostingErrorsData ListPostingErrors(int userId)
        {
            PostingErrorsData postingErrorsData = new PostingErrorsData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(postingErrorsData, "f1206_pclst_PostingError", ht, new string[] { postingErrorsData.ListPostingErrors.TableName, postingErrorsData.ListPostingErrorFlag.TableName });
            return postingErrorsData;
        }

        /// <summary>
        /// Inserts the account.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="errorTypeId">The error type id.</param>
        public static void InsertAccount(int userId, int errorTypeId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@ErrorTypeID", errorTypeId);
            DataProxy.ExecuteSP("f1206_pcins_PostingErrorAccount", ht);
        }
    }
}
