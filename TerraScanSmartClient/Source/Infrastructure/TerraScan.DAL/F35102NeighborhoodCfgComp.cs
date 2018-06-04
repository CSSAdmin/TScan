// -------------------------------------------------------------------------------------------
// <copyright file="F35102NeighborhoodCfgComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F35102NeighborhoodCfgComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 
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
    /// Data Access Layer which talks to the DB directly for F35102
    /// </summary>
    public static class F35102NeighborhoodCfgComp
    {
        #region F35102 Neighborhood Configuration

        #region Get Neighborhood Cfg Details

        /// <summary>
        /// Gets the neighborhood CFG details.
        /// </summary>
        /// <param name="nbhdId">The NBHD ID.</param>
        /// <returns>Typed DataSet</returns>
        public static F35102NeighborhoodCfgData GetNeighborhoodCfgDetails(int nbhdId)
        {
            F35102NeighborhoodCfgData neighborhoodCfgData = new F35102NeighborhoodCfgData();
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhdId);
            string[] optionalParameter = new string[] { neighborhoodCfgData.ListNeighborhoodConfigurationTable.TableName };
            Utility.LoadDataSet(neighborhoodCfgData, "f35102_pclst_NeighborhoodConfiguration", ht, optionalParameter);
            return neighborhoodCfgData;
        }

        #endregion Get Neighborhood Cfg Details

        #region Get Neighborhood Cfg Choice

        /// <summary>
        /// Gets the neighborhood CFG Choice.
        /// </summary>
        /// <param name="nbhdId">nbhdID</param>
        /// <param name="nbhdCfgId">nbhdCfgID</param>
        /// <returns>1</returns>
        public static F35102NeighborhoodCfgData GetNeighborhoodCfgChoice(int nbhdId, int nbhdCfgId)
        {
            F35102NeighborhoodCfgData neighborhoodCfgChoice = new F35102NeighborhoodCfgData();
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhdId);
            ht.Add("@NBHDCfgID", nbhdCfgId);
            string[] optionalParameter = new string[] { neighborhoodCfgChoice.ListNeighborhoodChoiceDatatable.TableName, neighborhoodCfgChoice.ListNeighborhoodQueryDatatable.TableName };
            Utility.LoadDataSet(neighborhoodCfgChoice, "f35102_pclst_NeighborhoodChoice", ht, optionalParameter);
            return neighborhoodCfgChoice;
        }

        #endregion Get Neighborhood Cfg Choice

        #region Save Neighborhood Cfg Details

        /// <summary>
        /// To Save Neighborhood CFG details.
        /// </summary>
        /// <param name="neighborhoodConfigId">The neighborhood config id.</param>
        /// <param name="neighborhoodConfigDetails">The neighborhood config details.</param>
        /// <param name="userId">userId</param>
        public static void F35102_SaveNeighborhoodCfgDetails(int neighborhoodConfigId, string neighborhoodConfigDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDCfgID", neighborhoodConfigId);
            ht.Add("@NBHDConf", neighborhoodConfigDetails);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f35102_pcupd_NeighborhoodConfiguration", ht);
        }

        #endregion Save Neighborhood Cfg Details

        #endregion F35102 Neighborhood Configuration
    }
}
