// -------------------------------------------------------------------------------------------
// <copyright file="F25011WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F25011 Funcationality</summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 04/05/2007       M.Vijayakumar      Created
// 
// -------------------------------------------------------------------------------------------

namespace D20003
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
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F25011WorkItem Class file
    /// </summary>
    public class F25011WorkItem : WorkItem
    {
        #region F25011 Street List Management

        #region Get the Master Street Data

        /// <summary>
        /// To Get Master Street Data.
        /// </summary>
        /// <param name="streetID">Street ID</param>
        /// <returns>Typed DataSet Containing the Master Street data.</returns>
        public F25011StreetListManagementData F25011_GetMasterStreetList(int streetID)
        {
            return WSHelper.F25011_GetMasterStreetList(streetID);
        }

        #endregion Get the Master Street Data


        #region List Master Street List

        /// <summary>
        /// To List Master Street List.
        /// </summary>
        /// <param name="streetName">Name of the street.</param>
        /// <param name="city">The city.</param>
        /// <returns>Typed DataSet Containing the Master Street List details.</returns>
        public F25011StreetListManagementData F25011_ListMasterStreetList(int streetID, string streetName, string city)
        {
            return WSHelper.F25011_ListMasterStreetList(streetID,streetName, city);
        }

        #endregion List Master Street List

        #region List Street City Directional Suffix

        /// <summary>
        /// To List Street City Directional Suffix Details.
        /// </summary>
        /// <returns>Typed Dataset conitaining the Street's City, Directional and Suffixs details</returns>
        public F25011StreetListManagementData F25011_ListStreetCityDirectionalSuffixDetails()
        {
            return WSHelper.F25011_ListStreetCityDirectionalSuffixDetails();
        }

        #endregion List Street City Directional Suffix

        #region Save Street List Management

        /// <summary>
        /// To Save Street List Management Details.
        /// </summary>
        /// <param name="streetId">The street id.</param>
        /// <param name="streetListMgmt">The street list MGMT.</param>
        /// <returns>The current Saved streetId</returns>
        public int F25011_SaveStreetListManagement(int streetId, string streetListMgmt, int userId)
        {
            return WSHelper.F25011_SaveStreetListManagement(streetId, streetListMgmt, userId);
        }

        #endregion Save Street List Management

        #region Delete Street List

        /// <summary>
        /// F25011_s the delete street list.
        /// </summary>
        /// <param name="streetId">The street id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Deleted Flag</returns>
        public int F25011_DeleteStreetList(int streetId, int userId)
        {
            return WSHelper.F25011_DeleteStreetList(streetId, userId);
        }

        #endregion Delete Street List

        #endregion F25011 Street List Management

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

        #region WorkItem Common Methods

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

        #endregion WorkItem Common Methods
    }
}
