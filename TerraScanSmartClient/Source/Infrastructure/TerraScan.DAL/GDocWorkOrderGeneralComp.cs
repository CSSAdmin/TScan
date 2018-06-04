// -------------------------------------------------------------------------------------------
// <copyright file="GDocWorkOrderGeneralComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and GDoc Work order General Comp methods</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
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
    /// GDocWorkOrderGeneralComp Class file
    /// </summary>
    public static class GDocWorkOrderGeneralComp
    {
        #region GDoc Work order General

        #region Get GDoc Work order General

        /// <summary>
        /// Get work order general values for F8910.
        /// </summary>
        /// <param name="workorderId">The workorder id.</param>
        /// <returns>Typed DataSet containing the GDoc Work order General Values</returns>
        public static GDocWorkOrderGeneralData F8910_GetWorkOrderGeneral(int workorderId)
        {
            GDocWorkOrderGeneralData gdocWorkOrderGeneralData = new GDocWorkOrderGeneralData();
            Hashtable ht = new Hashtable();
            ht.Add("@WOID", workorderId);
            Utility.LoadDataSet(gdocWorkOrderGeneralData.F8910_GetWorkOrderGeneral, "f8910_pcget_WorkOrderGeneral", ht);
            return gdocWorkOrderGeneralData;
        }

        #endregion Get GDoc Work order General

        #region Save GDoc Work order General

        /// <summary>
        /// Save work order general values for F8910.
        /// </summary>
        /// <param name="workOrderGeneral">The work order general.</param>
        /// <param name="userId">userId</param>
        /// <returns>Typed DataSet containing the GDoc Work order General Values</returns>
        public static GDocWorkOrderGeneralData F8910_SaveWorkOrderGeneral(string workOrderGeneral, int userId)
        {
            GDocWorkOrderGeneralData gdocWorkOrderGeneralData = new GDocWorkOrderGeneralData();
            Hashtable ht = new Hashtable();
            ht.Add("@WorkOrderGeneral", workOrderGeneral);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(gdocWorkOrderGeneralData.F8910_SaveWorkOrderGeneral, "f8910_pcupd_WorkOrderGeneral", ht);
            return gdocWorkOrderGeneralData;
        }

        #endregion Save GDoc Work order General

        #endregion GDoc Work order General
    }
}
