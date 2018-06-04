// ---------------------------------------------------------------------------------------------------------------
// <copyright file="F36060WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F36060</summary>
// Release history
//*****************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ----------------------------------------------------------------------------
// 14/12/2007       M.Vijayakumar      Created
// 
// ----------------------------------------------------------------------------------------------------------------

namespace D36060
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F36060WorkItem class file
    /// </summary>
    public class F36060WorkItem : WorkItem
    {
        #region F36060DepreciationComp

        #region F36060_GetDepreciationTables

        /// <summary>
        /// To get the Depreciation  tables
        /// </summary>
        /// <param name="deprTableId">Deprtable id</param>
        /// <returns>Typed dataset containing the Deprecition and Deprecition items datatable</returns>
        public F36060DepreciationData F36060_GetDepreciationTables(int deprTableId)
        {
            return WSHelper.F36060_GetDepreciationTables(deprTableId);
        }

        #endregion F36060_GetDepreciationTables

        #region F36060_SaveDepreciationTables

        /// <summary>
        /// To save depreciation tables.
        /// </summary>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="deprecationItem">The deprecation item.</param>
        /// <param name="otherDeprItem">The other depr item.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Inserted or update key id is returned</returns>
        public int F36060_SaveDepreciationTables(int deprTableId, string deprecationItem, string otherDeprItem, int userId)
        {
            return WSHelper.F36060_SaveDepreciationTables(deprTableId, deprecationItem, otherDeprItem, userId);
        }

        #endregion F36060_SaveDepreciationTables

        #region F36060_DeleteDepreciationTables

        /// <summary>
        /// To delete Depreciation Tables.
        /// </summary>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="userId">The user id.</param>
        public void F36060_DeleteDepreciationTables(int deprTableId, int userId)
        {
            WSHelper.F36060_DeleteDepreciationTables(deprTableId, userId);
        }

        #endregion F36060_DeleteDepreciationTables

        #endregion F36060DepreciationComp

        #region To Get Configuration Roll Year

        /// <summary>
        /// Gets the config Roll Year.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #endregion To Get Configuration Roll Year

        #region WorkItem Protected Methods.

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

        #endregion WorkItem Protected Methods.
    }
}
