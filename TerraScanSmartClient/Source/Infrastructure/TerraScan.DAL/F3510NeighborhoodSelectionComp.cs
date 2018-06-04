// -------------------------------------------------------------------------------------------
// <copyright file="F3510NeighborhoodSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36041CropComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 10/12/07         Malliga             Created
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
    /// F3510NeighborhoodSelection Class File.
    /// </summary>
    public static class F3510NeighborhoodSelectionComp
    {
        #region Get Neighborhood Selection Details

        /// <summary>
        /// F3510_s the list neighborhood selection details.
        /// </summary>
        /// <param name="neighborhood">The neighborhood.</param>
        /// <param name="childOf">The child of.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="neighborhoodtype">The neighborhoodtype.</param>
        /// <param name="description">The description.</param>
        /// <returns>neighborhoodSelectionDetails</returns>
        public static F3510NeighborhoodSelectionData F3510_ListNeighborhoodSelectionDetails(string neighborhood, string childOf, string rollYear, string neighborhoodtype, string description)
        {
            F3510NeighborhoodSelectionData neighborhoodSelectionDetails = new F3510NeighborhoodSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@Neighborhood", neighborhood);
            ht.Add("@ChildOf", childOf);
            ht.Add("@RollYear", rollYear);
            ht.Add("@Type", neighborhoodtype);
            ht.Add("@Description", description);
            Utility.LoadDataSet(neighborhoodSelectionDetails.GetNeighborhoodSelection, "f3510_pclst_NeighborhoodSelection", ht);
            return neighborhoodSelectionDetails;
        }
        #endregion 

        #region Get Neighborhood Type Details

        /// <summary>
        /// F3510_neighborhoods the type.
        /// </summary>
        /// <returns>neighborhoodtype</returns>
        public static F3510NeighborhoodSelectionData F3510_neighborhoodType()
        {
            F3510NeighborhoodSelectionData neighborhoodtype = new F3510NeighborhoodSelectionData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(neighborhoodtype.GetNeighborhoodType, "f3510_pcget_NeighborhoodSelection", ht);
            return neighborhoodtype;
        }
        #endregion 
     }
}
