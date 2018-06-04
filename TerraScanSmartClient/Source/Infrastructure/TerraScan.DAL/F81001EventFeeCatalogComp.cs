// -------------------------------------------------------------------------------------------
// <copyright file="F81001EventFeeCatalogComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F81001EventFeeCatalogComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 02/11/07        D.LathaMaheswari     Created
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
    /// F81001EventFeeCatalogComp
    /// </summary>
    public static class F81001EventFeeCatalogComp
    {
        #region Event Fee Catalog

        /// <summary>
        /// Get the Event Fee Catalog data
        /// </summary>
        /// <param name="feeCatId">feeCatID</param>
        /// <returns>DataSet</returns>
        public static F81001FeeCatalogData F81001_GetEventFeeCatalog(int feeCatId)
        {
            F81001FeeCatalogData getEventFeeData = new F81001FeeCatalogData();
            Hashtable ht = new Hashtable();
            ht.Add("@FeeCatID", feeCatId);
            Utility.LoadDataSet(getEventFeeData.GetFeeCatalog, "f81001_pcget_Fee_Catalog", ht);
            return getEventFeeData;
        }

        /// <summary>
        /// Save the Event Fee Catalog data
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="feeCatalogItems">feeCatalogItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer</returns>
        public static int F81001_SaveEventFeeCatalog(int feeCatId, string feeCatalogItems, int userId)
        {
            Hashtable ht = new Hashtable();
            if (feeCatId != -99)
            {
                ht.Add("@FeeCatID", feeCatId);
            }

            ht.Add("@Fee_CatalogItems", feeCatalogItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f81001_pcins_Fee_Catalog", ht);
        }

        /// <summary>
        /// Delete the Event Fee Catalog data
        /// </summary>
        /// <param name="feeCatId">FeeCatID</param>
        /// <param name="userId">UserID</param>
        public static void F81001_DeleteEventFeeCatalog(int feeCatId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FeeCatID", feeCatId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f81001_pcdel_Fee_Catalog", ht);
        }

        /// <summary>
        /// Check the Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="formNumber">formNumber</param>
        /// <param name="effectiveDate">effectiveDate</param>
        /// <returns>integer</returns>
        public static int F81001_CheckEventFeeCatalog(int feeCatId, string formNumber, DateTime effectiveDate)
        {
            Hashtable ht = new Hashtable();
            if (feeCatId != -99)
            {
                ht.Add("@FeeCatID", feeCatId);
            }

            ht.Add("@Form", formNumber);
            ht.Add("@EffectiveDate", effectiveDate);
            return Utility.FetchSPExecuteKeyId("[f81001_pcchk_Fee_Catalog]", ht);
        }

        #endregion Event Fee Catalog
    }
}
