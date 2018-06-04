//--------------------------------------------------------------------------------------------
// <copyright file="F1214WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1214 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10-10-2006       Krishna Abburi       Created
//*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using TerraScan.BusinessEntities;
using TerraScan.Helper;

namespace D1210
{
    /// <summary>
    /// F1214 WorkItem Class
    /// </summary>
    public class F1214WorkItem : WorkItem
    {
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

        #region Public Methods

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public PartiesOwnerDetailsData GetOwnerDetails(int ownerId)
        {
            return WSHelper.GetOwnerDetails(ownerId);
        }

        //public RefundManagementData.ListAccountNamesDataTable RefundAccountNames()
        //{
        //    return WSHelper.RefundAccountNames();
        //}

        /// <summary>
        /// Lists the account names.
        /// </summary>
        /// <returns>The dataTable containing the AccountNames.</returns>
        #region List AccontNames

        /// <summary>
        /// F1214 the account names.
        /// </summary>
        /// <returns>the datatable contains the Account Names</returns>
        public RefundManagementData.ListAccountNamesDataTable F1214_AccountNames()
        {
            return WSHelper.F1214_AccountNames();
        }

        #endregion

        public RefundManagementData.ListRefundPaymentsDataTable ListRefundPayments(int form, string whereCondnSql)
        {
            return WSHelper.ListRefundPayments(form, whereCondnSql);
        }

        /// <summary>
        /// F1214_s the prepare checks.
        /// </summary>
        /// <param name="registerID">The register ID.</param>
        /// <param name="ownerID">The owner ID.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="userID">The user ID.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <returns>ErrorID</returns>
        public int F1214_PrepareChecks(int registerId, int ownerId, DateTime interestDate, int userId, string paymentItems)
        {
            return WSHelper.F1214_PrepareChecks(registerId, ownerId, interestDate, userId, paymentItems);
        }

        #endregion
    }
}
