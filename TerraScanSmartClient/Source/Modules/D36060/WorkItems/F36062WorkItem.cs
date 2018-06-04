// ---------------------------------------------------------------------------------------------------------------
// <copyright file="F36062WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F36062 FS Depreciation Control Tables</summary>
// Release history
//*****************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ----------------------------------------------------------------------------
// 
// ----------------------------------------------------------------------------------------------------------------

namespace D36060
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F36061WorkItem class file
    /// </summary>
    public class F36062WorkItem : WorkItem
    {
        #region WorkItem Protected Methods.

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

        #endregion WorkItem Protected Methods.

        #region LandInfluenceControl

        #region F36062_LandInfluenceData

        /// <summary>
        /// Used to List theLandInfluence Details
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Land Influence Details
        /// </returns>
        public F36062LandInfluenceData F36062_LandInfluenceItems(int nbhdId)
        {
            return WSHelper.F36062_LandInfluenceItems(nbhdId);
        }
         #endregion F36062_LandInfluenceData

        #region F36062_SaveInfluenceControl

        /// <summary>
        /// Used to save the Depreciation Control Items Details .
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="deprControlItems">The depr control items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>saved key id</returns>
        public int F36062_SaveInfluenceControl(int? nbhdId, string InfluenceItems, int userId)
        {
            return WSHelper.F36062_SaveInfluenceControl(nbhdId, InfluenceItems, userId);
        }

        #endregion F36062_SaveInfluenceControl




        #endregion LandInfluenceControl


    }
}
