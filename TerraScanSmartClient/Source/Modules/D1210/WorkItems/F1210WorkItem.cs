//--------------------------------------------------------------------------------------------
// <copyright file="F1210WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1213 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-10-2006       Shiva              Created
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F1210 WorkItem
    /// </summary>
    public class F1210WorkItem : WorkItem
    {
        #region Public Methods

        /// <summary>
        /// F1210_s the get disbursement details.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <returns>Disbursement DataSet</returns>
        public DisbursementData F1210_GetDisbursementDetails(DateTime postDate)
        {
            return WSHelper.F1210_GetDisbursementDetails(postDate);
        }

        /// <summary>
        /// Lists the account names.
        /// </summary>
        /// <returns>The dataTable containing the AccountNames.</returns>
        public DisbursementData.ListAccountNameDataTable F1210_DisbursementAccountNames()
        {
            return WSHelper.F1210_DisbursementAccountNames();
        }

        /// <summary>
        /// F1210s the save disbursement.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="postDate">The post date.</param>
        /// <param name="agencies">The agencies.</param>
        /// <param name="overrideStatus">The override status.</param>
        /// <returns>bit Value to Override the Checks</returns>
        public int F1210_SaveDisbursement(int registerId, int userId, DateTime postDate, string agencies, int overrideStatus)
        {
            return WSHelper.F1210_SaveDisbursement(registerId, userId, postDate, agencies, overrideStatus);
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
