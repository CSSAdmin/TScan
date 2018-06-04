// -------------------------------------------------------------------------------------------
// <copyright file="F25000ParcelHeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F25000ParcelHeaderComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 28/10/2013       Purushotham.A       Created
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// 
    /// </summary>
    public static class F2102GroupingSelectionComp
    {

        /// <summary>
        /// F2102s the get grouping selection.
        /// </summary>
        /// <param name="groupCode">The group code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static F2102GroupingSelectionData f2102_GetGroupingSelection(string groupCode, string description)
        {
            F2102GroupingSelectionData groupingSelectionData = new F2102GroupingSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@GroupCode", groupCode);
            ht.Add("@Description", description);
            Utility.LoadDataSet(groupingSelectionData.F2102GroupingSelection, "f2102_pclst_GroupingSelection", ht);
            return groupingSelectionData;
        }
    }
}
