// -------------------------------------------------------------------------------------------
// <copyright file="F29505WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F29505</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 29/12/2008        Malliga           Created
// -------------------------------------------------------------------------------------------

namespace D29505
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
    /// F24505WorkItem
    /// </summary>
    public class F29505WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public PartiesOwnerDetailsData GetOwnerDetails(int ownerId)
        {
            return WSHelper.GetOwnerDetails(ownerId);
        }
         
        /// <summary>
        /// F429505_s the list all comoboxes.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Returns AllCombox value</returns>
        public F29505CreateSubdivisionData F429505_ListAllComoboxes(int eventId)
        {
            return WSHelper.F429505_ListAllComoboxes(eventId);
        }

        /// <summary>
        /// F429505_s the list all LandCodes.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Returns AllCombox value</returns>
        public F29505CreateSubdivisionData LandCodes(int nbhdid,int rollyear)
        {
            return WSHelper.ListLandCodes(nbhdid,rollyear);
        }


        /// <summary>
        /// F29505_s the get base parcel value.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Returns baseparcel value</returns>
        public F29505CreateSubdivisionData F29505_GetBaseParcelValue(int eventId)
        {
            return WSHelper.F29505_GetBaseParcelValue(eventId);
        }
        
        /// <summary>
        /// F29505_s the create parcel.
        /// </summary>
        /// <param name="makeSubId">The make sub id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Return message</returns>
        public string F29505_CreateParcel(int makeSubId, int userId)
        {
            return WSHelper.F29505_CreateParcel(makeSubId, userId);
        }
        
        /// <summary>
        /// F29505_s the save division parcels.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="makeSubItemsXml">The make sub items XML.</param>
        /// <param name="makeSubParcelsXml">The make sub parcels XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns makesubid</returns>
        public int F29505_SaveDivisionParcels(int eventId, string makeSubItemsXml, string makeSubParcelsXml, int userId)
        {
            return WSHelper.F29505_SaveDivisionParcels(eventId, makeSubItemsXml, makeSubParcelsXml, userId);
        }
        
        /// <summary>
        /// F29505_s the save sub division.
        /// </summary>
        /// <param name="makeSubID">The make sub ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns processed value</returns>
        public int F29505_SaveSubDivision(int makeSubID, int userId)
        {
            return WSHelper.F29505_SaveSubDivision(makeSubID, userId);
        }
        
        /// <summary>
        /// F29505_s the get land code.
        /// </summary>
        /// <param name="landType1">The land type1.</param>
        /// <param name="landType2">The land type2.</param>
        /// <param name="landType3">The land type3.</param>
        /// <param name="nbhdid">The nbhdid.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public F29505CreateSubdivisionData F29505_GetLandCode(int landType1, int landType2, int landType3, int nbhdid, int rollYear)
        {
            return WSHelper.F29505_GetLandCode(landType1, landType2, landType3, nbhdid, rollYear);
        }
        
        /// <summary>
        /// F25003_s the list street.
        /// </summary>
        /// <returns>returns streetdataset</returns>
        public F25003SitusManagementData F25003_ListStreet()
        {
            return WSHelper.F25003_ListStreet();
        }

        /// <summary>
        /// F2550_s the state of the get configured.
        /// </summary>
        /// <returns></returns>
        public F2550TaxRollCorrectionData F2550_GetConfiguredState()
        {
            return WSHelper.F2550_GetConfiguredState();
        }

        /// <summary>
        /// F26000_s the class code details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public F26000ParcelHeaderFormData F26000_ClassCodeDetails(string filterValue)
        {
            return WSHelper.F26000_ClassCodeDetails(filterValue);
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
    }
}
