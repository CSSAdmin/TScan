// -------------------------------------------------------------------------------------------
// <copyright file="F25008WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F25008</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 April 06      VINAYAGAMURTHY H    Created
// -------------------------------------------------------------------------------------------

namespace D20008
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
    /// F25008WorkItem Class file
    /// </summary>
    public class F25008WorkItem : WorkItem
    {

        #region F25008 Parcel Miscellaneous

        #region Get Parcel Miscellaneous

        /// <summary>
        /// Get Parcel Miscellaneous Data
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <returns>Parcel Miscellaneous</returns>
        public F25008ParcelMiscellaneousData F25008_ParcelMiscellaneousData(int parcelId)
        {
            return WSHelper.F25008_ParcelMiscellaneousData(parcelId);
        }

        #endregion Get Parcel Miscellaneous

        #region Get Parcel Miscellaneous Configuration

        /// <summary>
        /// ParcelMiscellaneous Configuration Data
        /// </summary>
        /// <returns>ParcelMiscellaneous Configuration</returns>
        public F25008ParcelMiscellaneousData F25008_ParcelMiscellaneousConfigData()
        {
            return WSHelper.F25008_ParcelMiscellaneousConfigData();
        }

        #endregion Get Parcel Miscellaneous Configuration

        #region Save Parcel Miscellaneous

        /// <summary>
        /// Save Parcel Miscellaneous
        /// </summary>
        /// <param name="parcelID">parcelID</param>
        /// <param name="miscellaneous">miscellaneous</param>
        public void F25008_SaveParcelMiscellaneous(int parcelID, string miscellaneous, int userId)
        {
            WSHelper.F25008_SaveParcelMiscellaneous(parcelID, miscellaneous, userId);
        }

        #endregion Save Parcel Miscellaneous

        #endregion F25008 Parcel Miscellaneous

        #region WorkItems Methods

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

        #endregion WorkItems Methods
    }
}