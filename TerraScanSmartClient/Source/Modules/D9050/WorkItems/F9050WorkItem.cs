// -------------------------------------------------------------------------------------------
// <copyright file="F9050WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Statement</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
// -------------------------------------------------------------------------------------------
namespace D9050
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// form F9050 WorkItem
    /// </summary>
    public class F9050WorkItem : WorkItem
    {      
        /// <summary>
        /// insert query 
        /// </summary>
        /// <param name="queryId">queryId</param>
        /// <param name="queryName">queryName</param>
        /// <param name="formId">formId</param>
        /// <param name="description">description</param>
        /// <param name="userId">userId</param>
        /// <param name="whereCondition">whereCondition</param>
        /// <param name="userWhereCondition">userWhereCondition</param>
        /// <param name="overrideValue">overrideValue</param>
        /// <returns>return integer</returns>
        public int InsertQueryUtility(int queryId, string queryName, int formId, string description, int userId, string whereCondition, string userWhereCondition, int overrideValue)
        {
            return WSHelper.InsertQueryUtility(queryId, queryName, formId, description, userId, whereCondition, userWhereCondition, overrideValue);
        }

        /// <summary>
        /// get query
        /// </summary>
        /// <param name="formId">the foem id</param>
        /// <returns>dataset</returns>
        public QueryUtilityData GetQueryUtilityList(int formId)
        {
            return WSHelper.GetQueryUtilityList(formId);
        }

        /// <summary>
        /// Delete the query
        /// </summary>
        /// <param name="queryId">The Query</param>
        public void DeleteQueryUtility(int queryId,int userID)
        {
           WSHelper.DeleteQueryUtility(queryId,userID);
        }

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }  
    }
}
