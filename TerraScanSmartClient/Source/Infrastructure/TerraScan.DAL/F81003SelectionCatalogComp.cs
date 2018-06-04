// -------------------------------------------------------------------------------------------
// <copyright file="F81003SelectionCatalogComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F81003 Selection Catalog</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12/12/2008       Sadha Shivudu M       Added
// 
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
    /// F81003SelectionCatalogComp class file.
    /// </summary>
    public static class F81003SelectionCatalogComp
    {
        #region F81003 Selection Catalog

        #region Get Selection Catalog Details

        /// <summary>
        /// F81003_s the get selection catalog details.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        /// <returns>selection catalog dataset</returns>
        public static F81003SelectionCatalogData F81003_GetSelectionCatalogDetails(int catalogId)
        {
            F81003SelectionCatalogData selectionCatalogData = new F81003SelectionCatalogData();
            Hashtable ht = new Hashtable();
            ht.Add("@CatalogID", catalogId);
            Utility.LoadDataSet(selectionCatalogData.GetSelectionCatalog, "f81003_pcget_SelectionCatalog", ht);
            return selectionCatalogData;
        }

        #endregion Get Selection Catalog Details

        #region List Selection Category

        /// <summary>
        /// F81003_s the list selection category.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>selection catalog dataset</returns>
        public static F81003SelectionCatalogData F81003_ListSelectionCategory(int userId)
        {
            F81003SelectionCatalogData selectionCatalogData = new F81003SelectionCatalogData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(selectionCatalogData.ListSelectionCategory, "f81003_pclst_SelectionCategory", ht);
            return selectionCatalogData;
        }

        #endregion List Selection Category

        #region Save Selection Catalog

        /// <summary>
        /// F81003_s the save selection catalog.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        /// <param name="selectionItemsXml">The selection items XML.</param>
        /// <returns>key id.</returns>
        public static int F81003_SaveSelectionCatalog(int? catalogId, string selectionItemsXml)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CatalogID", catalogId);
            ht.Add("@SelectionItems", selectionItemsXml);
            return Utility.FetchSPExecuteKeyId("f81003_pcins_SelectionCatalog", ht);
        }

        #endregion Selection Catalog

        #region Delete Selection Catalog

        /// <summary>
        /// F81003_s the delete selection catalog.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        public static void F81003_DeleteSelectionCatalog(int catalogId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CatalogID", catalogId);
            Utility.ImplementProcedure("f81003_pcdel_SelectionCatalog", ht);
        }

        #endregion Delete Selection Catalog

        #endregion F81003 Selection Catalog
    }
}
