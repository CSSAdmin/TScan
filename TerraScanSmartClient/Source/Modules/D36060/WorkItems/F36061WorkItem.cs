// ---------------------------------------------------------------------------------------------------------------
// <copyright file="F36061WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F36061 FS Depreciation Control Tables</summary>
// Release history
//*****************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ----------------------------------------------------------------------------
// 11/02/2007       M.Vijayakumar      Created
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
    public class F36061WorkItem : WorkItem
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

        #region F36061 Depreciation Control       

        #region F36061_ListDepr

        /// <summary>
        /// Used to List the Depr Details
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Depr Details
        /// </returns>
        public F36061DepreciationControlData F36061_ListDepr(int nbhdId)
        {
            return WSHelper.F36061_ListDepr(nbhdId);
        }

        #endregion F36061_ListDepr

        #region F36061_ListDeprControlItems

        /// <summary>
        /// Used to Get the Depreciation Control Items Details.
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Depreciation Control Items Details
        /// </returns>
        public F36061DepreciationControlData F36061_ListDeprControlItems(int nbhdId)
        {
            return WSHelper.F36061_ListDeprControlItems(nbhdId);
        }

        #endregion F36061_ListDeprControlItems

        #region F36061_SaveDeprControlItems

        /// <summary>
        /// Used to save the Depreciation Control Items Details .
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="deprControlItems">The depr control items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>saved key id</returns>
        public int F36061_SaveDeprControlItems(int? nbhdId, string deprControlItems, int userId)
        {
            return WSHelper.F36061_SaveDeprControlItems(nbhdId, deprControlItems, userId);
        }

        #endregion F36061_SaveDeprControlItems

        #endregion F36061 Depreciation Control
    }
}
