//--------------------------------------------------------------------------------------------
// <copyright file="F15004WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F15004 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28-12-2006       Krishna              Created
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F15004 FormSlice - Agency fund Mgmt WorkItem
    /// </summary>
    public class F15004WorkItem : WorkItem
    {
        #region Public Methods

        /// <summary>
        /// Gets the form details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>GetFormDetails</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        /// <summary>
        /// F15004_s the get agency details.
        /// </summary>
        /// <param name="agencyID">The agency ID.</param>
        /// <returns>F15004AgencyManagementData</returns>
        public F15004AgencyManagementData F15004_GetAgencyDetails(int agencyID)
        {
            return WSHelper.F15004_GetAgencyDetails(agencyID);
        }

        /// <summary>
        /// F15004_s the check duplicate agency.
        /// </summary>
        /// <param name="agencyID">The agency ID.</param>
        /// <param name="agencyName">Name of the agency.</param>
        /// <returns>errorId</returns>
        public int F15004_CheckDuplicateAgency(int agencyID, string agencyName)
        {
            return WSHelper.F15004_CheckDuplicateAgency(agencyID, agencyName);
        }

        /// <summary>
        /// F15004_s the create or edit agency details.
        /// </summary>
        /// <param name="agencyID">The agency ID.</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <returns>primarykeyId</returns>
        public int F15004_CreateOrEditAgencyDetails(int agencyID, string acctEmelemts, int userId)
        {
            return WSHelper.F15004_CreateOrEditAgencyDetails(agencyID, acctEmelemts, userId);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion
    }
}
