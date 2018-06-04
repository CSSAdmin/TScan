// -------------------------------------------------------------------------------------------------
// <copyright file="F27000WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 Apr 07        Ranjani            Created// 
//*********************************************************************************/
namespace D22000
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
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F27000 WorkItem
    /// </summary>
    public class F27000WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">The user id.</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        /// <summary>
        /// Gets the Misc Assessment details based on the Misc Assessment DistrictId
        /// </summary>
        /// <param name="madistrictId">The Misc Assessment District Id.</param>
        /// <returns>
        /// The typed dataset containing the Misc Assessment information of the madistrictId.
        /// </returns>
        public F22000MiscAssessmentData F27000_GetMiscAssessment(int madistrictId)
        {
            return WSHelper.F27000_GetMiscAssessment(madistrictId); 
        }

        /// <summary>
        /// To Save Misc Assessment Details
        /// </summary>
        /// <param name="distributionItems">distributionItems</param>
        /// <param name="subHeaderItems">subHeaderItems</param>
        /// <returns>integer</returns>
        public int F27000_SaveMADetails(string distributionItems, string subHeaderItems, int userId)
        {
            return WSHelper.F27000_SaveMADetails(distributionItems, subHeaderItems, userId);
        }

        /// <summary>
        /// To List all the Misc Assessment District Types.
        /// </summary>
        /// <returns>Typed Dataset Containing the Misc Assessment District Types</returns>
        public CommonData F27000_ListMADistrictType()
        {
            return WSHelper.F27000_ListMADistrictType();
        }

        /// <summary>
        /// To List All Misc Assessment District Item Type
        /// </summary>
        /// <param name="madistrictTypeId">The Misc Assessment District type Id.</param>
        /// <returns>Typed Dataset Containg the All Misc Assessment Misc Assessment Item Types</returns>
        public CommonData F27000_ListMADistrictItemType(int madistrictTypeId)
        {
            return WSHelper.F27000_ListMADistrictItemType(madistrictTypeId);
        }

        /// <summary>
        /// Gets the config Roll Year.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public F15013ExciseTaxRateData F15013_GetAccountName(int accountId)
        {
            return WSHelper.F15013_GetAccountName(accountId);
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
