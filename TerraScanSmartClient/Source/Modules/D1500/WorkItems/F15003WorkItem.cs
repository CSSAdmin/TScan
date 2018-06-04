//--------------------------------------------------------------------------------------------
// <copyright file="F15003WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F15003 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22-12-2006       Shiva              Created
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F15003 WorkItem
    /// </summary>
    public class F15003WorkItem : WorkItem
    {
        #region GetDefault Year

        /// <summary>
        /// Gets the Year
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Year.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #endregion

        #region Get Fund and SubFund Details

        /// <summary>
        /// F15003_s the get fund sub fund details.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <returns>dataset which contains Fund Details</returns>
        public F15003FundMgmtData F15003_GetFundSubFundDetails(int? fundId)
        {
            return WSHelper.F15003_GetFundSubFundDetails(fundId);
        }

        /// <summary>
        /// F15003_s the list available sub funds.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="fundId">The fund id.</param>
        /// <returns>DataSet Contains the Available Funds Details</returns>
        public F15003FundMgmtData F15003_ListAvailableSubFunds(string subFund, string description, int? rollYear, int? fundId)
        {
            return WSHelper.F15003_ListAvailableSubFunds(subFund, description, rollYear, fundId);
        }

        /// <summary>
        /// F15003_s the type of the list fund.
        /// </summary>
        /// <returns>dataTable Contains the FundTypes</returns>
        public F15003FundMgmtData.ListFundTypeDataTable F15003_ListFundType()
        {
            return WSHelper.F15003_ListFundType();
        }

        #endregion

        #region Save and Edit Fund Details

        /// <summary>
        /// F15003_s the check fund.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <param name="fund">The fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>returns the fund valid status</returns>
        public int F15003_CheckFund(int? fundId, string fund, int rollYear)
        {
            return WSHelper.F15003_CheckFund(fundId, fund, rollYear);
        }

        /// <summary>
        /// F15003_s the create or edit fund MGMT.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <param name="fund">The fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="description">The description.</param>
        /// <param name="fundGroupId">The fund group id.</param>
        /// <param name="fundItems">The fund items.</param>
        /// <returns>returns the save process status</returns>
        public int F15003_CreateOrEditFundMgmt(int? fundId, string fund, int rollYear, string description, int? fundGroupId, string fundItems, int userId)
        {
            return WSHelper.F15003_CreateOrEditFundMgmt(fundId, fund, rollYear, description, fundGroupId, fundItems, userId);
        }

        #endregion

        #region GetForm Detials

        /// <summary>
        /// Gets the form details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>FormDetails DataSet</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        #endregion

        #region Protected Methods

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

        #endregion
    }
}
