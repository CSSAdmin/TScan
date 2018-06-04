// -------------------------------------------------------------------------------------------
// <copyright file="F8904EventGridComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access Event Grid</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 168Oct 06		Vinoth             Created
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
    /// F8904EventGridComp Class
    /// </summary>
    public static class F8904EventGridComp
    {
        #region Get

        /// <summary>
        /// F8904 GetEventGrid
        /// </summary>
        /// <param name="workId">The WorkOrder id.</param>
        /// <returns>List of Grid details of 8904</returns>
        public static F8904EventGridData F8904_GetEventGrid(int workId)
        {
            F8904EventGridData eventGridDataset = new F8904EventGridData();
            Hashtable ht = new Hashtable();
            ht.Add("@WOID", workId);
            Utility.LoadDataSet(eventGridDataset.GetEventGrid, "f8904_pclst_FS_EventGrid", ht);
            return eventGridDataset;
        }

        #endregion
    }
}
