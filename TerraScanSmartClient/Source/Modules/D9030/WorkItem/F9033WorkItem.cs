//--------------------------------------------------------------------------------------------
// <copyright file="F9033WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 Dec 06        Guhan               Created
// 21 Dec 06        Vinoth              Modified
//*********************************************************************************/

#region NameSpaces

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using TerraScan.Helper;
using System.Data;
using TerraScan.BusinessEntities;

#endregion NameSpaces
namespace D9030
{
    /// <summary>
    /// F9033 WorkItem Class
    /// </summary>
    public class F9033WorkItem : WorkItem
    {
        #region ListQueryEngine

        /// <summary>
        /// F9033_s the list query engine.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        public DataSet F9033_ListQueryEngine(int queryViewId)
        {
            return WSHelper.F9033_ListQueryEngine(queryViewId);
        }

        #endregion

        #region GetSnapShotRecordSet

        /// <summary>
        /// F9033_s the get snap shot record set.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns></returns>
        public DataSet F9033_GetSnapShotRecordSet(int snapShotId, int queryViewId)
        {
            return WSHelper.F9033_GetSnapShotRecordSet(snapShotId, queryViewId);
        }

        #endregion GetSnapShotRecordSet

        #region ListQueryView

        /// <summary>
        /// F9033_s the list query view.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>DataTable</returns>
        public F9033QueryEngineData.ListQueryViewDataTable F9033_ListQueryView(int formId)
        {
            return WSHelper.F9033_ListQueryView(formId).ListQueryView;
        }

        #endregion

        #region LoadDefaultLayout

        /// <summary>
        /// F9033_s the get default layout.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>XMLDataTable</returns>
        public F9033QueryEngineData.GetDefaultLayoutXMLDataTable F9033_GetDefaultLayout(int queryViewId)
        {
            return WSHelper.F9033_GetDefaultLayout(queryViewId).GetDefaultLayoutXML;
        }

        #endregion

        #region ListQuerySnapShot

        /// <summary>
        /// F9033_s the list query snap shot.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns></returns>
        public F9033QueryEngineData.ListQuerySnapShotDataTable F9033_ListQuerySnapShot(int queryViewId)
        {
            return WSHelper.F9033_ListQuerySnapShot(queryViewId).ListQuerySnapShot;
        }

        #endregion ListQuerySnapShot

        #region ListQueryLayout

        /// <summary>
        /// F9033_s the list query layout.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public F9033QueryEngineData.ListQueryLayoutDataTable F9033_ListQueryLayout(int queryViewId, int userId)
        {
            return WSHelper.F9033_ListQueryLayout(queryViewId, userId).ListQueryLayout;
        }

        #endregion ListQueryLayout

        #region InsertSnapShotItems

        /// <summary>
        /// F9033_s the insert snap shot items.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="systemSnapShotXML">The system snap shot XML.</param>
        /// <returns></returns>
        public int F9033_InsertSnapShotItems(int? userId, string systemSnapShotXML)
        {
            return WSHelper.F9033_InsertSnapShotItems(userId, systemSnapShotXML);
        }

        #endregion InsertSnapShotItems

        #region GetSystemSnapShotRecordSet

        /// <summary>
        /// F9033_s the get system snap shot record set.
        /// </summary>
        /// <param name="systemSnapShotId">The system snap shot id.</param>
        /// <param name="masterFormNO">The master form NO.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">The key id collection.</param>
        /// <param name="isFilter">The is filter.</param>
        /// <returns>DataSet</returns>
        public DataSet F9033_GetSystemSnapShotRecordSet(int systemSnapShotId, int masterFormNO, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, int isFilter)
        {
            return WSHelper.F9033_GetSystemSnapShotRecordSet(systemSnapShotId, masterFormNO, filterValue, sortOrder, summaryValue, columnValue, keyIdCollection, isFilter);
        }

        #endregion GetSystemSnapShotRecordSet

        ////Added by Latha

        #region CustomGridFunctionality

        /// <summary>
        /// Lists the query engine grid function.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">Newly added KeyIds</param>
        /// <param name="isFilter">Flag for load all records</param>
        /// <
        /// <returns>DataSet</returns>
        public DataSet ListQueryEngineGridFunction(int queryViewId, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, string isFilter, string maxRecord)
        {
            return WSHelper.ListQueryEngineGridFunction(queryViewId, filterValue, sortOrder, summaryValue, columnValue, keyIdCollection, isFilter, maxRecord);
        }


        /// <summary>
        /// Lists the query engine grid snapshot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">Newly added KeyIds</param>
        /// <param name="isFilter">Flag for load all records</param>
        /// <param name="maxRecord">Max Record Count</param>
        /// <returns>DataSet</returns>
        public DataSet ListQueryEngineGridSnapshot(int snapShotId, int queryViewId, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, int isFilter, string maxRecord)
        {
            return WSHelper.ListQueryEngineGridSnapshot(snapShotId, queryViewId, filterValue, sortOrder, summaryValue, columnValue, keyIdCollection, isFilter, maxRecord);
        }

        /// <summary>
        /// Lists the columns.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        public DataSet ListColumns(int queryViewId)
        {
            return WSHelper.ListColumns(queryViewId);
        }

        #endregion CustomGridFunctionality
    }
}
