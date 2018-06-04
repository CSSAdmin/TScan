//--------------------------------------------------------------------------------------------
// <copyright file="F9016WorkItem.cs" company="Congruent">
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
// 13 Sep 06        VINOTHBABU         Created
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
    /// F90151WorkItem Class
    /// </summary>
    public class F9016WorkItem : WorkItem
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
        /// Saves the SQL query.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="description">The description.</param>
        /// <param name="statement">The statement.</param>
        /// <param name="moduleId">The module id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="sqlId">The SQL id.</param>
        /// <returns>Integer</returns>
        public int SaveSqlQuery(int categoryId, string description, string statement, int moduleId, int userId, int sqlId)
        {
            return WSHelper.SaveSQLQuery(categoryId, description, statement, moduleId, userId, sqlId);
        }

        /// <summary>
        /// F9015_s the delete query.
        /// </summary>
        /// <param name="sqlId">The SQL id.</param>
        /// <returns>Integer</returns>
        public int F9015_DeleteQuery(int sqlId,int userID)
        {
            return WSHelper.F9015_DeleteQuery(sqlId,userID);
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

        #region Get Form Slice Permission Details

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">userId</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        #endregion Get Form Slice Permission Details

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
