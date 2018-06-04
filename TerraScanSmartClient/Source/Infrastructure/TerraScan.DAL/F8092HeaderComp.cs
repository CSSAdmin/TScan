// -------------------------------------------------------------------------------------------
// <copyright file="F8092HeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Rates</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 Oct 06		Vinoth             Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.BusinessEntities;
    using System.Collections;
    using TerraScan.DataLayer;

    /// <summary>
    /// F8092HeaderComp Class
    /// </summary>
    public static class F8092HeaderComp
    {
        #region Get

        /// <summary>
        /// F8902 the list Header information.
        /// </summary>
        /// <param name="workId">The WorkOrder id.</param>
        /// <returns> List of Header details of 8040</returns>
        public static F8902HeaderData F8902_GetWorkOrderHeader(int workId)
        {
            F8902HeaderData headerDataset = new F8902HeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@WOID", workId);
            Utility.LoadDataSet(headerDataset.GetWorkOrderHeader, "f8902_pcget_FS_WorkOrderHeader", ht);
            return headerDataset;
        }
        #endregion

        #region Save

        /// <summary>
        /// F8902 Save WorkOrderHeader information
        /// </summary>
        /// <param name="headerDetails">HeaderDetails</param>
        /// <param name="userId">userId</param>
        public static void F8902_SaveWorkOrderHeader(string headerDetails, int userId)
        {
            ////F8902HeaderData headerDataset = new F8902HeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@WOItems", headerDetails);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8902_pcupd_FS_WorkOrderHeader", ht);            
        }

        #endregion

        #region Delete

        /// <summary>
        /// F8902 DeleteWorkOrder Header
        /// </summary>
        /// <param name="workId">workId</param>
        /// <param name="userId">userId</param>
        public static void F8902_DeleteWorkOrderHeader(int workId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@WOID", workId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8902_pcdel_FS_WorkOrderHeader", ht);            
        }
        #endregion
    }
}
