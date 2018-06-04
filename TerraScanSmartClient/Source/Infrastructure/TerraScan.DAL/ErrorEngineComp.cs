// -------------------------------------------------------------------------------------------
// <copyright file="ErrorEngineComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access attachment related information</summary>
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
    using TerraScan.DataLayer;
    using TerraScan.BusinessEntities; 

    /// <summary>
    /// ErrorEngineComp
    /// </summary>
    public static class ErrorEngineComp
    {
        /// <summary>
        /// Gets the error engine.
        /// </summary>
        /// <param name="errorTypeId">The error type id.</param>
        /// <returns>returns Error engine data</returns>
        public static ErrorEngineData GetErrorEngine(int errorTypeId)
        {
            ////Hashtable ht = new Hashtable();
            ////ht.Add("@ErrorTypeID", errorTypeId);
            ////return DataProxy.FetchDataSet("f9010_pcget_Error", ht);
            ErrorEngineData errorEngineData = new ErrorEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@ErrorTypeID", errorTypeId);
            Utility.LoadDataSet(errorEngineData.ErrorEngineTable, "f9010_pcget_Error", ht);
            return errorEngineData;
        }

        /// <summary> 
        /// Inserts the error engine.
        /// </summary>
        /// <param name="errorDate">The error date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="userIP">The user IP.</param>
        /// <param name="errorTypeId">The error type id.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="comment">The comment.</param>
        public static void InsertErrorEngine(string errorDate, int userId, string userIP, int errorTypeId, string parameter, string comment)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ErrorDate", errorDate);
            ht.Add("UserID", userId);
            ht.Add("UserIP", userIP);
            ht.Add("ErrorTypeID", errorTypeId);
            ht.Add("Parameter", parameter);
            ht.Add("Comment", comment);
            DataProxy.ExecuteSP("f9010_pcins_ErrorAudit", ht);
        }
    }
}
