//--------------------------------------------------------------------------------------------
// <copyright file="F29500WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 13 Sep 07		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D24500
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
    /// F29500WorkItem
    /// </summary>
    public class F29500WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the base parcel value.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F29500ParcelSplitData</returns>
        public F29500ParcelSplitData GetBaseParcelValue(int parcelId)
        {
            return WSHelper.F29500_GetBaseParcelValue(parcelId);
        }

        /// <summary>
        /// Parcels the split load.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>F29500ParcelSplitData</returns>
        public F29500ParcelSplitData ParcelSplitLoad(int eventId)
        {
            return WSHelper.F29500_ParcelSplitLoad(eventId);
        }

        /// <summary>
        /// Saves the parcel split.
        /// </summary>
        /// <param name="splitDefinitionXml">The split definition XML.</param>
        /// <param name="splitHeaderXml">The split header XML.</param>
        /// <param name="parcelSplitXml">The parcel split XML.</param>
        /// <param name="parcelObjectXml">The parcel object XML.</param>
        /// <param name="cropXml">Crop XML</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public int SaveParcelSplit(string splitDefinitionXml, string splitHeaderXml, string parcelSplitXml, string parcelObjectXml, string cropXml, int userId)
        {
            return WSHelper.F29500_SaveParcelSplit(splitDefinitionXml, splitHeaderXml, parcelSplitXml, parcelObjectXml, cropXml, userId);
        }

        /// <summary>
        /// F29500_s the create parcel.
        /// </summary>
        /// <param name="splitId">The split id.</param>
        /// <param name="userId">User ID</param>
        /// <returns>Return message</returns>
        public string F29500_CreateParcel(int splitId, int userId)
        {
            return WSHelper.F29500_CreateParcel(splitId, userId);
        }

        /// <summary>
        /// ListRecordLockStatus
        /// </summary>
        /// <param name="formNo">formNo</param>
        /// <param name="keyId">keyId</param>
        /// <returns>String</returns>
        public string ListRecordLockStatus(int formNo, int keyId)
        {
            return WSHelper.ListRecordLockStatus(formNo, keyId);
        }

        public F2550TaxRollCorrectionData F2550_GetConfiguredState()
        {
            return WSHelper.F2550_GetConfiguredState();
        }

        public F26000ParcelHeaderFormData F26000_ClassCodeDetails(string filterValue)
        {
            return WSHelper.F26000_ClassCodeDetails(filterValue);
        }

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
    }
}
