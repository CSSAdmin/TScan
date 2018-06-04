// -------------------------------------------------------------------------------------------
// <copyright file="F9038LayoutManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Rates</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 29 Dec 06		Guhan             Created
// -------------------------------------------------------------------------------------------


namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using TerraScan.BusinessEntities;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;

    /// <summary>
    /// BUSINESS COMPONENT FOR LAYOUT MANAGEMENT
    /// </summary>
    public static class F9038LayoutManagementComp
    {
        #region  LoadLayoutManagement  Grid

        /// <summary>
        /// F9038_s the load layout information.
        /// </summary>
        /// <param name="queryViewId">The query view ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>THE RETURNED LAYOUT DETAILS FOR THE VIEWID</returns>
        public static F9038LayoutManagementData F9038_LoadLayoutInformation(int queryViewId, int userId)
        {
            F9038LayoutManagementData layouManagementData = new F9038LayoutManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@QueryViewID", queryViewId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(layouManagementData.AvailableLayoutTable, "f9038_pclst_LayoutManagement", ht);
            return layouManagementData;
        }

        #region Save LoadLayoutManagement

        /// <summary>
        /// F9038_s the save layout information.
        /// </summary>
        /// <param name="queryLayoutId">The query layout ID.</param>
        /// <param name="layoutManagement">The layout management.</param>
        /// <param name="layoutXMLValue">The layout XML.</param>
        /// <param name="userId">userId</param>
        /// <returns>THE GENERATED LAYOUTID</returns>
        public static int F9038_SaveLayoutInformation(int queryLayoutId, string layoutManagement, string layoutXMLValue, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@QueryLayoutID", queryLayoutId);
            ht.Add("@LayoutManagement", layoutManagement);
            ht.Add("@LayoutXML", layoutXMLValue);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f9038_pcins_LayoutManagement", ht);
        }

        #endregion Save LoadLayoutManagement

        #region Delete LoadLayoutManagement

        /// <summary>
        /// F9038_s the delete water pipe properties.
        /// </summary>
        /// <param name="queryLayoutId">The pipe id.</param>
        /// <param name="userId">userId</param>
        public static void F9038_DeleteLayoutInformation(int queryLayoutId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@QueryLayoutID", queryLayoutId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f9038_pcdel_LayoutManagement", ht);
        }

        #endregion Delete LoadLayoutManagement

        #endregion
    }
}
