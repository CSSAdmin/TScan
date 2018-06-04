// -------------------------------------------------------------------------------------------------
// <copyright file="F25051WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//***********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//  
//***********************************************************************************************/


namespace D20000
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

    public class F25051WorkItem : WorkItem
    {
        #region FormDetails
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
        #endregion

        #region DistrictLinkLabel

        /// <summary>
        /// Gets the district selection data.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="district">The district.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>typed dataset</returns>
        public F1512DistrictSelectionData F1512_GetDistrictSelectionData(int districtId, string district, string description, int rollYear)
        {
            return WSHelper.F1512_GetDistrictSelectionData(districtId, district, description, rollYear);
        }
        #endregion DistrictLinkLabel

        #region List Primary Implementation Type

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns>F25000ParcelHeaderData</returns>
        public F25000ParcelHeaderData ListPrimaryImprovement()
        {
            return WSHelper.ListPrimaryImprovement();
        }

        #endregion

        #region List Primary Land Type

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns>F25000ParcelHeaderData</returns>
        public F25000ParcelHeaderData ListPrimaryLandType()
        {
            return WSHelper.ListPrimaryLandType();
        }

        #endregion

        #region ParcelDetails
        /// <summary>
        /// Gets the details of F25000 Parceldetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F25051ParcelHeaderData F25051_GetParcelDetails(int parcelId)
        {
            return WSHelper.F25051_GetParcelDetails(parcelId);
        }
        #endregion ParcelDetails

        #region ParcelUpdateDetails

        /// <summary>
        /// Updates the parcel header details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelDetails">The parcel details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F25051ParcelHeaderDetails(int parcelId, string parcelDetails, int userId)
        {
            return WSHelper.F25051ParcelHeaderDetails(parcelId, parcelDetails, userId);
        }
        #endregion ParcelUpdateDetails

        #region ParcelClassType

        /// <summary>
        /// Lists the parcel Class Type.
        /// </summary>
        /// <returns>F25051ParcelHeaderData</returns>
        public F25051ParcelHeaderData F25051ParcelClassTypes()
        {
            return WSHelper.F25051ParcelClassTypes();
        }

        #endregion ParcelClassType

        #region OwnerOccupied

        /// <summary>
        /// Lists the Owner Occupied Type.
        /// </summary>
        /// <returns>F25051ParcelHeaderData</returns>
        public F25051ParcelHeaderData F25051OwnerOccupied()
        {
            return WSHelper.F25051OwnerOccupied();
        }
        #endregion OwnerOccupied

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
