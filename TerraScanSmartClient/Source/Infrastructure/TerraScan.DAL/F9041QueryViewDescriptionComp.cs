// -------------------------------------------------------------------------------------------
// <copyright file="F9041QueryViewDescriptionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F9041QueryViewDescriptionComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 25/11/2008           D.LathaMaheswari    Created
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
    /// F9041QueryViewDescriptionComp Class File.
    /// </summary>
    public static class F9041QueryViewDescriptionComp
    {
        /// <summary>
        /// F9041s the get query description.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        public static F9041QueryViewDescriptionData F9041GetQueryDescription(int queryViewId)
        {
            F9041QueryViewDescriptionData queryViewItems = new F9041QueryViewDescriptionData();
            Hashtable ht = new Hashtable();
            ht.Add("@QueryViewID", queryViewId);
            Utility.LoadDataSet(queryViewItems.GetQueryViewDescription, "f9041_pcget_QueryViewDescription", ht);
            return queryViewItems;
        }
    }
}
