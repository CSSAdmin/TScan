//--------------------------------------------------------------------------------------------
// <copyright file="F81003WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F81003WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Dec 08        Sadha Shivudu M    Created
//*********************************************************************************/
namespace D81003
{
    #region namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    #endregion namespace

    /// <summary>
    /// F81003WorkItem
    /// </summary>
    public class F81003WorkItem : WorkItem
    {
        #region CRUD Methods

        #region Get Selection Catalog Details

        /// <summary>
        /// F81003_s the get selection catalog details.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        /// <returns>selection catalog dataset</returns>
        public F81003SelectionCatalogData F81003_GetSelectionCatalogDetails(int catalogId)
        {
            return WSHelper.F81003_GetSelectionCatalogDetails(catalogId);
        }

        #endregion Get Selection Catalog Details

        #region List Selection Category

        /// <summary>
        /// F81003_s the list selection category.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>selection catalog dataset</returns>
        public F81003SelectionCatalogData F81003_ListSelectionCategory(int userId)
        {
            return WSHelper.F81003_ListSelectionCategory(userId);
        }

        #endregion List Selection Category

        #region Save Selection Catalog

        /// <summary>
        /// F81003_s the save selection catalog.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        /// <param name="selectionItemsXml">The selection items XML.</param>
        /// <returns>key id.</returns>
        public int F81003_SaveSelectionCatalog(int? catalogId, string selectionItemsXml)
        {
            return WSHelper.F81003_SaveSelectionCatalog(catalogId, selectionItemsXml);
        }

        #endregion Selection Catalog

        #region Delete Selection Catalog

        /// <summary>
        /// F81003_s the delete selection catalog.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        public void F81003_DeleteSelectionCatalog(int catalogId)
        {
            WSHelper.F81003_DeleteSelectionCatalog(catalogId);
        }

        #endregion Delete Selection Catalog

        #endregion CRUD Methods

        #region Base Methods

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion Base Methods.
    }
}
