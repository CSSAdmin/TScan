// ---------------------------------------------------------------------------------------------------------------
// <copyright file="F82001WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F82001 FS Building Permit</summary>
// Release history
//*****************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ----------------------------------------------------------------------------
// 12/12/2007       Kuppusamy.B              Created
// 
// ----------------------------------------------------------------------------------------------------------------

namespace D82001
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F82001WorkItem Class file
    /// </summary>
    public class F82001WorkItem : WorkItem
    {
        #region F82001 BuildingPermit

        #region Get Form Slice Permission Details

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

        #endregion Get Form Slice Permission Details       

        #region Get

        /// <summary>
        /// F82001_s the get building permit details.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns>eventID</returns>
        public F82001BuildingPermitData F82001_GetBuildingPermitDetails(int eventID)
        {
            return WSHelper.F82001_GetBuildingPermitDetails(eventID);
        }

        /// <summary>
        /// F82002_s the list contractor management data.
        /// </summary>
        /// <param name="icontractorID"> icontractorID </param>
        /// <param name="contractorXML"> contractorXML </param>
        /// <returns>ContractorManagementData</returns>
        public F82002ContractorManagementData F82002_ListContractorManagementData(int? icontractorID, string contractorXML)
        {
            return WSHelper.F82002_ListContractorManagementData(icontractorID, contractorXML);
        }

        /// <summary>
        /// Lists the user names.
        /// </summary>
        /// <returns>ListUserNames</returns>
        public SupportFormData ListUserNames()
        {
            return WSHelper.ListUserNames();
        }

        #endregion Get

        #region Insert

        /// <summary>
        /// F82001_s the insert building permit details.
        /// </summary>
        /// <param name="permitId"> permitId </param>
        /// <param name="buildingPermitItems"> buildingPermitItems </param>
        /// <param name="userId"> userId </param>
        /// <returns>BuildingPermitDetails</returns>
        public int F82001_InsertBuildingPermitDetails(int permitId, string buildingPermitItems, int userId)
        {
            return WSHelper.F82001_InsertBuildingPermitDetails(permitId, buildingPermitItems, userId);
        }
        #endregion Insert

        #endregion F82001 BuildingPermit

        #region GDoc EventHeader

        #region GetGDocEventHeader

        /// <summary>
        /// Gets the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Typed dataset containing the Event,Event date,Work Order and Is complete. </returns>
        public GDocEventHeaderData GetGDocEventHeader(int eventId)
        {
            return WSHelper.GetGDocEventHeader(eventId);
        }

        #endregion GetGDocEventHeader

        #region ListGDocEventHeaderStatus

        /// <summary>
        /// Lists the GDoc event header status.
        /// </summary>
        /// <param name="eventId">The event Id.</param>
        /// <returns>Typed status containing Event Engine status.</returns>
        public GDocEventHeaderData ListGDocEventHeaderStatus(int eventId)
        {
            return WSHelper.ListGDocEventHeaderStatus(eventId);
        }

        #endregion  ListGDocEventHeaderStatus

        #endregion GDoc EventHeader

        #region SliceEvents
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
        #endregion SliceEvents        
    }
}

