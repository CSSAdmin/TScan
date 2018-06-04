// -------------------------------------------------------------------------------------------
// <copyright file="F35101NeighborhoodGroupHeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access Neighborhood Group Header Methods</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// May 16 2007      B.Karthikeyan      Added
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
    /// F35101NeighborhoodGroupHeaderComp Class file
    /// </summary>
    public static class F35101NeighborhoodGroupHeaderComp
    {
        #region F35101 Neighborhood Group Header

        #region Get Neighborhood Group Header

        /// <summary>
        /// To Load F35101 Neighborhood Group Header.
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Neighborhood Group Header Details
        /// </returns>
        public static F35101NeighborhoodGroupHeaderData F35101_GetNeighborhoodGroupHeader(int nbhdGroupId)
        {
            F35101NeighborhoodGroupHeaderData neighborhoodGroupHeaderData = new F35101NeighborhoodGroupHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDGroupID", nbhdGroupId);
            Utility.LoadDataSet(neighborhoodGroupHeaderData.GetNeighborhoodGroupHeader, "f35101_pcget_neighborhoodgroupheader", ht);
            return neighborhoodGroupHeaderData;
        }

        #endregion

        #region Save Neighborhood Group Header

        /// <summary>
        /// To Save F35101 Neighborhood Group Header.
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group id.</param>
        /// <param name="neighborhoodGroupHeader">The Neighborhood Group Header Details.</param>
        /// <returns>The integer value containing the Neighborhood Group Header id</returns>
        /// <param name="userId">userId</param>
        public static int F35101_SaveNeighborhoodGroupHeader(int nbhdGroupId, string neighborhoodGroupHeader, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDGroupID", nbhdGroupId);
            ht.Add("@NeighborhoodGroup", neighborhoodGroupHeader);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35101_pcins_NeighborhoodGroupHeader", ht);
        }

        #endregion

        #region Delete Neighborhood Group Header

        /// <summary>
        /// To Delete F35101 Neighborhood Group Header
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group Header Id</param>
        /// <param name="userId">userId</param>
        public static void F35101_DeleteNeighborhoodGroupHeader(int nbhdGroupId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDGroupID", nbhdGroupId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f35101_pcdel_NeighborhoodGroupHeader", ht);
        }

        #endregion

        #endregion
    }
}
