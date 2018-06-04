//--------------------------------------------------------------------------------------------
// <copyright file="F35101WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F35101WorkItem.cs. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// May 16 2007   	B.KARTHIKEYAN      Created
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
    /// F35101WorkItem Class file 
    /// </summary>
    public class F35101WorkItem : WorkItem
    {
        #region Common Methods

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">userId</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

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

        #endregion

        #region F35101 Neighborhood Group Header

        #region Get Neighborhood Group Header

        /// <summary>
        ///  To Load F35101 Neighborhood Group Header.
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group ID.</param>
        /// <returns>Typed DataSet Containing All the Neighborhood Group Header Details</returns>
        public F35101NeighborhoodGroupHeaderData F35101_GetNeighborhoodGroupHeader(int nbhdGroupId)
        {
            return WSHelper.F35101_GetNeighborhoodGroupHeader(nbhdGroupId);
        }

        #endregion

        #region Save Neighborhood Group Header

        /// <summary>
        /// To Save F35101 Neighborhood Group Header.
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group Header id.</param>
        /// <param name="neighborhoodGroupHeader">The Neighborhood Group Header Details.</param>
        /// <returns>The integer value containing Neighborhood Group Header id</returns>
        public int F35101_SaveNeighborhoodGroupHeader(int nbhdGroupId, string neighborhoodGroupHeader,int userID)
        {
            return WSHelper.F35101_SaveNeighborhoodGroupHeader(nbhdGroupId, neighborhoodGroupHeader, userID);
        }

        #endregion

        #region Delete Neighborhood Group Header

        /// <summary>
        /// To Delete F35101 Neighborhood Group Header
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group Header Id</param>
        public void F35101_DeleteNeighborhoodGroupHeader(int nbhdGroupId,int userID)
        {
            WSHelper.F35101_DeleteNeighborhoodGroupHeader(nbhdGroupId, userID);
        }

        #endregion

        #endregion

        #region F35100 Neighborhood Header

        /// <summary>
        /// To load Neighborhood Header User Details
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>Typed DataSet Containing All the Neighborhood Header User Details</returns>
        public F35100NeighborhoodHeaderData F35100_GetNeighborhoodHeaderUserDetails(int applicationId)
        {
            return WSHelper.F35100_GetNeighborhoodHeaderUserDetails(applicationId);
        }

        #endregion

    }
}
