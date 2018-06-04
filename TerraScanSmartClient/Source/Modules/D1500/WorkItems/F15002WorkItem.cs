//--------------------------------------------------------------------------------------------
// <copyright file="F15002WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F15002 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22-12-2006       Shiva              Created
// 16-12-2010       Manoj              modified due to co:To implement #7325
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
    /// F15002 WorkItem
    /// </summary>
    public class F15002WorkItem : WorkItem
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

        #region Get Distict Fund Details

        /// <summary>
        /// F15002_s the get distirct fund details.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>DataSet Contains the District Fund Deatails</returns>
        public F15002DistMgmtData F15002_GetDistirctFundDetails(int? districtId)
        {
            return WSHelper.F15002_GetDistirctFundDetails(districtId);
        }

        /// <summary>
        /// F15002_s the list all funds.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <param name="fund">The fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet Contains the All Funds Deatails</returns>
        public F15002DistMgmtData F15002_ListAllFunds(int? fundId, string fund, int? rollYear)
        {
            return WSHelper.F15002_ListAllFunds(fundId, fund, rollYear);
        }

        #endregion

        #region Get District Selection
        /// <summary>
        /// Gets the district selection.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>Returns GetDistrictSelection Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetDistrictSelection(int exciseRateId)
        {
            return WSHelper.F15010_GetDistrictSelection(exciseRateId);
        }
        #endregion
        
        #region Get District Type
        /// <summary>
        /// F15002_s the type of the get district.
        /// </summary>
        /// <returns></returns>
        public F15002DistMgmtData F15002_GetDistrictType(int UserId)
        {
            return WSHelper.F15002_GetDistrictType(UserId);
        }
        #endregion

        #region Save and Edit District and Fund

        /// <summary>
        /// F15002_s the check district.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="district">The district.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>the error id or primaryKeyId </returns>
        public int F15002_CheckDistrict(int? districtId, string district, int rollYear)
        {
            return WSHelper.F15002_CheckDistrict(districtId, district, rollYear);
        }

        /// <summary>
        /// F15002_s the create or edit district MGMT.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="district">The district.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="activeStatus">The is active.</param>
        /// <param name="districtFundItems">The district fund items.</param>
        /// <returns>Error Statement or PrimaryKey Id</returns>
        public int F15002_CreateOrEditDistrictMgmt(int? districtId, string districtDetails, string districtFundItems, int userId)
        {
            return WSHelper.F15002_CreateOrEditDistrictMgmt(districtId, districtDetails, districtFundItems, userId);
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
