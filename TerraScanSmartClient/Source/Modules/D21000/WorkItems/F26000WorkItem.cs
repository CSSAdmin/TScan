//----------------------------------------------------------------------------------
// <copyright file="F26000WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm. 
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		            Description
// ----------		---------		        ----------------------------------------
// 24 OCT 2013		Purushotham.A            Created
//-----------------------------------------------------------------------------------
namespace D21000
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
    /// F26000WorkItem
    /// </summary>
    public class F26000WorkItem : WorkItem
    {
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
        /// Gets the details of F25000 Parceldetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F26000ParcelHeaderFormData F26000_GetParcelFormDetails(int parcelId)
        {
            return WSHelper.F26000_GetParcelFormDetails(parcelId);
        }

        /// <summary>
        /// F26000_s the exemption details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <param name="exemptionFromAmount">The exemption from amount.</param>
        /// <returns></returns>
        public F26000ParcelHeaderFormData F26000_ExemptionDetails(int parcelId, string exemptionCode, decimal? exemptionFromAmount)
        {
            return WSHelper.F26000_ExemptionDetails(parcelId, exemptionCode, exemptionFromAmount);
        }


        /// <summary>
        /// F26000_s the exempt field details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="exemptionId">The exemption id.</param>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <returns></returns>
        public F26000ParcelHeaderFormData F26000_ExemptFieldDetails(int parcelId, int exemptionId,string exemptionCode)
        {
            return WSHelper.F26000_ExemptFieldDetails(parcelId, exemptionId, exemptionCode);
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

        #region List Primary Implementation Type

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns>F25000ParcelHeaderData</returns>
        public F26000ParcelHeaderFormData PrimaryImprovementList()
        {
            return WSHelper.PrimaryImprovementList();
        }

        #endregion



        /// <summary>
        /// Updates the parcel header form details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelDetails">The parcel details.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public int UpdateParcelHeaderFormDetails(int parcelId, string parcelDetails, int userId, int rollYear)
        {
            return WSHelper.UpdateParcelHeaderFormDetails(parcelId, parcelDetails, userId,rollYear);
        }

        #region List Primary Land Type

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns>F25000ParcelHeaderData</returns>
        public F26000ParcelHeaderFormData PrimaryLandTypeList()
        {
            return WSHelper.PrimaryLandTypeList();
        }

        #endregion


        /// <summary>
        /// F26000_s the type of the get apprisal.
        /// </summary>
        /// <returns></returns>
        public F26000ParcelHeaderFormData F26000_GetApprisalType()
        {
            return WSHelper.F26000_GetApprisalType();
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
