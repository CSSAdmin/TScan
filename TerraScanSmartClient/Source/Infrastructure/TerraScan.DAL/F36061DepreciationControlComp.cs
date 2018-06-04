// -------------------------------------------------------------------------------------------
// <copyright file="F36061DepreciationControlComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36061DepreciationControlComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 11/02/07         M.Vijayakumar      Created
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
    /// F36061DepreciationControlComp class file
    /// </summary>
    public static class F36061DepreciationControlComp
    {
        #region F36061 Depreciation Control      

        #region F36061_ListDeprControlItems

        /// <summary>
        /// Used to Get the Depreciation Control Items Details.
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Depreciation Control Items Details
        /// </returns>
        public static F36061DepreciationControlData F36061_ListDeprControlItems(int nbhdId)
        {
            F36061DepreciationControlData depreciationControlData = new F36061DepreciationControlData();
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhdId);
            string[] tableName = new string[] {depreciationControlData.GetDeprDescriptionTitle.TableName, depreciationControlData.ListDeprControlItems.TableName};
            Utility.LoadDataSet(depreciationControlData, "f36061_pclst_DeprControlItems", ht, tableName);
            return depreciationControlData;
        }

        #endregion F36061_ListDeprControlItems

        #region F36061_ListDepr

        /// <summary>
        /// Used to List the Depr Details
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Depr Details
        /// </returns>
        public static F36061DepreciationControlData F36061_ListDepr(int nbhdId)
        {
            F36061DepreciationControlData depreciationControlData = new F36061DepreciationControlData();
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhdId);
            Utility.LoadDataSet(depreciationControlData.ListDepr, "f36061_pclst_Depr", ht);
            return depreciationControlData;
        }

        #endregion F36061_ListDepr

        #region F36061_SaveDeprControlItems

        /// <summary>
        /// Used to save the Depreciation Control Items Details .
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="deprControlItems">The depr control items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>saved key id</returns>
        public static int F36061_SaveDeprControlItems(int? nbhdId, string deprControlItems, int userId)
        { 
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhdId);
            ht.Add("@DeprControlItems", deprControlItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36061_pcins_DeprControl", ht);
        }

        #endregion F36061_SaveDeprControlItems

        #endregion F36061 Depreciation Control
    }
}
