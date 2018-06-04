//--------------------------------------------------------------------------------------------
// <copyright file="F1212WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1212 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 05-09-2006       Shiva              Created
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
    /// F1212 WorkItem Class
    /// </summary>
    public class F1212WorkItem : WorkItem
    {
        #region Public Methods

        /// <summary>
        /// Gets the payment items details.
        /// </summary>
        /// <returns>TypedDataSet Which Contains the PaymentItemsDetails</returns>
        public MakeDepositsData GetPaymentItemsDetails()
        {
            return WSHelper.GetPaymentItemsDetails();
        }

        /// <summary>
        /// Saves the payment items details.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="paymentItemsDetails">The payment items details.</param>
        public void SavePaymentItemsDetails(int registerId, decimal amount, int userId, string paymentItemsDetails)
        {
            WSHelper.SavePaymentItemsDetails(registerId, amount, userId, paymentItemsDetails);
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
