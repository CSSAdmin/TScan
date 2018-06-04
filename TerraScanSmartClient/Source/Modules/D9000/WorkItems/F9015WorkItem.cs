//--------------------------------------------------------------------------------------------
// <copyright file="F9015WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the GetSqlQueryResult.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07 Sep 06        VINOTHBABU         Created
//*********************************************************************************/

namespace D9000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.Helper;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F9015WorkItem class
    /// </summary>
    public class F9015WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the SqlCatagory
        /// </summary>
        /// <returns>DataTable</returns>
        public SQLSupportData.ListSqlCategoryDataTable GetSqlCatagory
        {
            get
            {
                return WSHelper.GetSQLCategory().ListSqlCategory;
            }
        }

        /// <summary>
        /// Gets SqlDescription DataTable
        /// </summary>
        /// <param name="categoryId">categoryId</param>
        /// <returns>DataTable</returns>
        public SQLSupportData.ListSqlDescriptionDataTable GetSqlDescription(int categoryId)
        {
            return WSHelper.GetSQLDescription(categoryId).ListSqlDescription;
        }

        /// <summary>
        /// Get SqlString
        /// </summary>
        /// <param name="categoryId">categoryId</param>
        /// <param name="sqlId">sqlId</param>
        /// <returns>DataTable</returns>
        public SQLSupportData.GetSqlStringDataTable GetSqlString(int categoryId, int sqlId)
        {
            return WSHelper.GetSQLString(categoryId, sqlId).GetSqlString;
        }

        /// <summary>
        /// Gets the SQL query result.
        /// </summary>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <returns>DataTable</returns>
        public DataSet GetSqlQueryResult(string sqlQuery)
        {
            return WSHelper.GetSQLQueryResult(sqlQuery);
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
