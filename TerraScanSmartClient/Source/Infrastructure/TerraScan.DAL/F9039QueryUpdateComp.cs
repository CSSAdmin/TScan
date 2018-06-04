// -------------------------------------------------------------------------------------------
// <copyright file="F9039QueryUpdateComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update QueryView</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 17 Aug 07		VINOTH             Created
// 17 Aug 07        VINOTH             Bussiness Component
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using TerraScan.BusinessEntities;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;

    #endregion NameSpace

    /// <summary>
    /// 9039QueryUpdateComp Class
    /// </summary>
    public static class F9039QueryUpdateComp
    {
        #region ListQueryViewColumn

        /// <summary>
        /// Lists the query view column.
        /// </summary>
        /// <param name="queryViewId">The query view ID.</param>
        /// <returns>Typed dataset</returns>
        public static F9039QueryUpdate F9039ListQueryViewColumn(int queryViewId)
        {
            F9039QueryUpdate queryUpdateData = new F9039QueryUpdate();
            Hashtable ht = new Hashtable();
            ht.Add("@QueryViewID", queryViewId);
            Utility.LoadDataSet(queryUpdateData.ListColumnDetails, "f9039_pclst_DataUpdateFields", ht);
            return queryUpdateData;
        }

        #endregion ListQueryViewColumn

        #region GetCommandResult

        /// <summary>
        /// F9039s the get command result.
        /// </summary>
        /// <param name="replaceId">The replace ID.</param>
        /// <param name="commandResult">The command result.</param>
        /// <returns>queryUpdateData</returns>
        public static DataSet F9039GetCommandResult(int replaceId, string commandResult)
        {
            DataSet queryUpdateData = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@ReplaceID", replaceId);
            ht.Add("@CommandResult", commandResult);
            Utility.LoadDataSet(queryUpdateData, "f9039_pcget_CommandResult", ht);
            return queryUpdateData;
        }

        #endregion GetCommandResult

        #region UpdateQueryData

        /// <summary>
        /// F9039s the update query data.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="keyField">The key field.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="updateField">The update field.</param>
        /// <param name="doprocessValue">The do process value.</param>
        /// <param name="userId">userId</param>
        /// <returns>String value</returns>
        public static string F9039UpdateQueryData(int queryViewId, string keyField, string keyId, string updateField, int doprocessValue, int userId)
        {
            string errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@QueryViewID", queryViewId);
            ht.Add("@KeyField", keyField);
            ht.Add("@KeyIDs", keyId);
            ht.Add("@UpdateFields", updateField);
            ht.Add("@DoProcess", doprocessValue);
            ht.Add("@UserID", userId);
            errorId = Utility.FetchSPExecuteKeyString("f9039_pcupd_DataUpdate", ht);
            return errorId;
        }

        #endregion UpdateQueryData
    }
}
