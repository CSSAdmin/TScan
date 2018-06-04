// -------------------------------------------------------------------------------------------
// <copyright file="F81004SelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F81004 Selection</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26/12/2008       Sadha Shivudu M       Added
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
    /// F81004SelectionComp class file
    /// </summary>
    public static class F81004SelectionComp
    {
        /// <summary>
        /// F81004_s the get selection details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="form">The form.</param>
        /// <returns>selection dataset</returns>
        public static F81004SelectionData F81004_GetSelectionDetails(int eventId, int form)
        {
            F81004SelectionData selectionData = new F81004SelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@Form", form);
            string[] tableName = new string[] { selectionData.ListCategoryHeaderDetails.TableName, selectionData.GetSelectionDetails.TableName };
            Utility.LoadDataSet(selectionData, "f81004_pcget_Selection", ht, tableName);
            return selectionData;
        }

        /// <summary>
        /// F81004_s the get selection catalog details.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>selection catalog data table</returns>
        public static F81004SelectionData.GetSelectionCatalogDetailsDataTable F81004_GetSelectionCatalogDetails(int categoryId)
        {
            F81004SelectionData selectionData = new F81004SelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@CategoryID", categoryId);
            Utility.LoadDataSet(selectionData.GetSelectionCatalogDetails, "f81004_pcget_SelectionCatalog", ht);
            return selectionData.GetSelectionCatalogDetails;
        }

        /// <summary>
        /// F81004_s the save selection items.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="selectionItemsXml">The selection items XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>eventId</returns>
        public static int F81004_SaveSelectionItems(int eventId, string selectionItemsXml, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@SelectionItems", selectionItemsXml);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f81004_pcins_Selection", ht);
        }
    }
}
