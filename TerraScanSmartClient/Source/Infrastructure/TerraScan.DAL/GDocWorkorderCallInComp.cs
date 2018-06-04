// -------------------------------------------------------------------------------------------
// <copyright file="GDocWorkorderCallInComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and GDoc Work order CallIn Comp methods</summary>
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
    /// GDocWorkorderCallInComp Class file
    /// </summary>
    public static class GDocWorkorderCallInComp
    {
        #region GDoc Work order CallIn

        #region Get GDoc Work order CallIn

        /// <summary>
        /// Get work order call In values  for F8912.
        /// </summary>
        /// <param name="workorderId">The work order id.</param>
        /// <returns>Typed DataSet Containing the Gdoc Work Order CallIn Values</returns>
        public static GDocWorkorderCallInData F8912_GetWorkOrderCallIn(int workorderId)
        {
            GDocWorkorderCallInData gdocWorkorderCallIndata = new GDocWorkorderCallInData();
            Hashtable ht = new Hashtable();
            ht.Add("@WOID", workorderId);
            Utility.LoadDataSet(gdocWorkorderCallIndata.F8912_GetWorkOrderCallIn, "f8912_pcget_WorkOrderCallIn", ht);
            return gdocWorkorderCallIndata;
        }

        #endregion Get GDoc Work order CallIn

        #region Get GDoc Addresses

        /// <summary>
        /// To Get Addresses for GDOC Form Slices.
        /// </summary>        
        /// <returns>Typed DataSet Containing the Gdoc Address</returns>
        public static GDocWorkorderCallInData wListAddresses()
        {
            GDocWorkorderCallInData gdocWorkorderCallIndata = new GDocWorkorderCallInData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(gdocWorkorderCallIndata.ListAddresses, "f8912_pclst_WorkOrderCallIn_Address", ht);
            return gdocWorkorderCallIndata;
        }

        #endregion Get GDoc Addresses

        #region Save GDoc Work order CallIn

        /// <summary>
        /// Save GDoc work order call In Values.
        /// </summary>
        /// <param name="workOrderCall">The work order call.</param>
        /// <param name="userId">userId</param>
        /// <returns>Typed DataSet Containing the Gdoc Work Order CallIn Values</returns>
        public static GDocWorkorderCallInData F8912_SaveWorkOrderCallIn(string workOrderCall, int userId)
        {
            GDocWorkorderCallInData gdocWorkorderCallIndata = new GDocWorkorderCallInData();
            Hashtable ht = new Hashtable();
            ht.Add("@WorkOrderCall", workOrderCall);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(gdocWorkorderCallIndata.F8912_SaveWorkOrderCallIn, "f8912_pcupd_WorkOrderCallIn", ht);
            return gdocWorkorderCallIndata;
        }

        #endregion Save GDoc Work order CallIn

        #endregion GDoc Work order CallIn
    }
}
