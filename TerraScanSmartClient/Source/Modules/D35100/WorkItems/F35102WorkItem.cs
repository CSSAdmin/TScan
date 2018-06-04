//--------------------------------------------------------------------------------------------
// <copyright file="F35102WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F35102WorkItem.cs. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22 June 2007 	Ramya.D           Created
//*********************************************************************************/

namespace D35100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    public class F35102WorkItem : WorkItem
    {
         #region WorkItems Methods

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #region F35102 Neighborhood Configuration

        #region Get Neighborhood Cfg Details

        /// <summary>
        ///  To Load F35101 Neighborhood Group Header.
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group ID.</param>
        /// <returns>Typed DataSet Containing All the Neighborhood Group Header Details</returns>
        public F35102NeighborhoodCfgData GetNeighborhoodCfgDetails(int nbhdId)
        {
            return WSHelper.GetNeighborhoodCfgDetails(nbhdId);
        }

        #endregion Get Neighborhood Cfg Details

        #region Save Neighborhood Cfg Details

        /// <summary>
        /// To Save the Receipt Header Receipt Number.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <param name="receiptNumber">The receipt number.</param>
        public void F35102_SaveNeighborhoodCfgDetails(int neighborhoodConfigId, string neighborhoodConfigDetails,int userId)
        {
            WSHelper.F35102_SaveNeighborhoodCfgDetails(neighborhoodConfigId, neighborhoodConfigDetails, userId);
        }

        #endregion Save Neighborhood Cfg Details

        #region Get Neighborhood Cfg Choice

        /// <summary>
        /// Gets the neighborhood CFG Choice.
        /// </summary>
        /// <param name="nbhdID">nbhdID</param>
        /// <param name="nbhdCfgID">nbhdCfgID</param>
        /// <returns>Typed Dataset containing the  neighborhood CFG Choice details</returns>
        public F35102NeighborhoodCfgData GetNeighborhoodCfgChoice(int nbhdId, int nbhdCfgId)
        {
            return WSHelper.GetNeighborhoodCfgChoice(nbhdId, nbhdCfgId);
        }

        #endregion Get Neighborhood Cfg Choice

        #endregion F35102 Neighborhood Configuration


         #endregion WorkItems Methods
    }

    
}
