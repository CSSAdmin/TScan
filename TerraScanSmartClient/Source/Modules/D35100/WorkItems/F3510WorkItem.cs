//--------------------------------------------------------------------------------------------
// <copyright file="F3510WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Fund Selection. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10/12/2007   	R.Malliga      Created
//*********************************************************************************/

namespace D35100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// Class file for F3510WorkItem
    /// </summary>
    public class F3510WorkItem : WorkItem
    {
        #region F3510_ListNeighborhoodSelection

        /// <summary>
        /// To Get the NeighborhoodSelection details
        /// </summary>
        /// <param name="neighborhood">Neighborhood</param>
        /// <param name="childOf">ChildOf</param>
        /// <param name="rollYear">RollYear</param>
        /// <param name="type">Type</param>
        /// <param name="description">Description</param>
        /// <returns>Typed Dataset Containing the Neighborhood Selection details</returns>
        public F3510NeighborhoodSelectionData F3510_ListNeighborhoodSelectionDetails(string neighborhood, string childOf, string rollYear, string type, string description)
        {
            return WSHelper.F3510_ListNeighborhoodSelectionDetails(neighborhood, childOf, rollYear, type, description);
        }

        #endregion F3510_NeighborhoodSelection

        #region F3510_ListNeighborhoodType
        /// <summary>
        /// F3510_s the type of the list neighborhood.
        /// </summary>
        /// <returns>neighborhoodTypeDataSet</returns>
        public F3510NeighborhoodSelectionData F3510_ListNeighborhoodType()
        {
            return WSHelper.F3510_ListNeighborhoodType();
        }
        #endregion

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
    }
}
