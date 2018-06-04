// -------------------------------------------------------------------------------------------
// <copyright file="F27008WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access and Update F27008 Parcel Ownership
// </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// ------------------------------------------------------------------------------

namespace D22008
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
    public class F27008WorkItem : WorkItem
    {
        #region List Parcel Ownership

        /// <summary>
        /// To List the Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Typed Dataset Containing the Parcel Ownership Details</returns>
        public F27008TRParcelOwnershipData F27008_ListParcelOwnership(int parcelId)
        {
            return WSHelper.F27008_ListParcelOwnership(parcelId);
        }

        #endregion List Parcel Ownership

        #region Save Parcel Ownership

        /// <summary>
        /// To Save Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelOwnership">The parcel ownership.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">UserID</param>
        public void F27008_SaveParcelOwnership(string parcelOwnership, int parcelId, int userId)
        {
            WSHelper.F27008_SaveParcelOwnership(parcelOwnership, parcelId, userId);
        }
        #endregion Save Parcel Ownership

        #region GET OwnerDetails
       
        /// <summary>
        /// To Get TRParcel Ownership Details.
        /// </summary>
        /// <param name="parcelId">Owner id.</param>
        /// <param name="userId">UserID</param>
        public F27008TRParcelOwnershipData F27008_GetOwnerDetails(int extraownerId, int userId)
        {
            return WSHelper.F27008_GetOwnerDetails(extraownerId, userId);  
        }

        #endregion GET OwnerDetails



        #region OwnerDetails

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetOwnerDetails(int ownerId)
        {
            return WSHelper.F15010_GetOwnerDetails(ownerId);
        }

        #endregion OwnerDetails

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
        #endregion WorkItems Methods
    }
       
}