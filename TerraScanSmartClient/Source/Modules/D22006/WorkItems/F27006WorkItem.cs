// -------------------------------------------------------------------------------------------
// <copyright file="F27006WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access and Update F27006 Parcel Ownership
// </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28/03/07         M.Vijayakumar       Created
// -------------------------------------------------------------------------------------------

namespace D22006
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
    /// F27006WorkItem Class file
    /// </summary>
    public class F27006WorkItem : WorkItem
    {
        #region F27006 Parcel Ownership

        #region List Parcel Ownership

        /// <summary>
        /// To List the Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Typed Dataset Containing the Parcel Ownership Details</returns>
        public F27006ParcelOwnershipData F27006_ListParcelOwnership(int parcelId)
        {
            return WSHelper.F27006_ListParcelOwnership(parcelId);
        }

        #endregion List Parcel Ownership

        #region List All Owner Details

        /// <summary>
        /// To List All Owners Details
        /// </summary>
        /// <param name="firstName">The First Name.</param>
        /// <param name="lastName">The Last Name.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="city">The city.</param>
        /// <returns>Typed Dataset Containg the All Owners Details</returns>
        public F27006ParcelOwnershipData F27006_ListALLOwnerDetails(string firstName, string lastName, string address1, string address2, string city)
        {
            return WSHelper.F27006_ListALLOwnerDetails(firstName, lastName, address1, address2, city);
        }

        #endregion List All Owner Details

        #region Save Parcel Ownership

        /// <summary>
        /// To Save Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelOwnership">The parcel ownership.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">UserID</param>
        public void F27006_SaveParcelOwnership(string parcelOwnership, int parcelId, int userId, bool isfuturePush)
        {
            WSHelper.F27006_SaveParcelOwnership(parcelOwnership, parcelId, userId, isfuturePush);
        }

        #endregion Save Parcel Ownership

        #region Check Ownership Details

        /// <summary>
        /// To Check Given Ownership Details is valid.
        /// </summary>
        /// <param name="ownershipDetails">The ownership details.</param>
        /// <returns>returns an integer Value whather given details are correct or not</returns>
        public int F27006_CheckOwnershipDetails(string ownershipDetails)
        {
            return WSHelper.F27006_CheckOwnershipDetails(ownershipDetails);
        }

        #endregion Check Ownership Details

        #region List MOwnerType

        /// <summary>
        /// Lists the type of the M owner.
        /// </summary>
        /// <returns>string</returns>
        public F27006ParcelOwnershipData ListMOwnerType()
        {
            return WSHelper.ListMOwnerType();
        }

        #endregion

#endregion F27006 Parcel Ownership

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
