//--------------------------------------------------------------------------------------------
// <copyright file="F1557WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1555WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28/09/2016   Priyadharshini.R       Created
//*********************************************************************************/

namespace D11001
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
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;    
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// class for workitem F1557
    /// </summary>
    public class F1557WorkItem : WorkItem
    {
        #region Public Methods

        /// <summary>
        /// Gets the receipt payment.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>ManagePayamentDataSet</returns>
        public F1557PayamentManagementData GetManagePayment(int receiptId)
        {
            return WSHelper.GetPaymentManagement(receiptId);
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public PaymentEngineData F1019_GetPayeeDetails(int ownerId)
        {
            return WSHelper.F1019_GetPayeeDetails(ownerId);
        }

        /// <summary>
        /// Lists the tender type.
        /// </summary>
        /// <param name="allowOverUnder">if set to <c>true</c> [allow over under].</param>
        /// <returns>
        /// The typed dataset containing the types of tender.
        /// </returns>
        public ReceiptEngineData.ListTenderTypeDataTable F1018_ListTenderType(bool allowOverUnder)
        {
            return WSHelper.ListTenderType(allowOverUnder).ListTenderType;
        }
      

        /// <summary>
        /// Insert the payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">The user ID.</param>
        public void InsertPayment(string receiptPayment, int userId)
        {
            WSHelper.F1557_InsertPayment(receiptPayment, userId);
        }

        /// <summary>
        /// Update the payment.
        /// </summary>
        /// <param name="UpdatePayment">The receipt payment.</param>
        /// <param name="userId">The user ID.</param>
        public void UpdatePayment(string receiptPayment, int userId)
        {
            WSHelper.F1557_UpdatePayment(receiptPayment, userId);
        }

        /// <summary>
        /// F1557 the delete Payment.
        /// </summary>
        /// <param name="PaymentIDs">Payment Management.</param>
        /// <param name="userId">The user id.</param>
        public void F1557_DeletePayment(string PaymentIDs, int userId)
        {
            WSHelper.F1557_DeletePayment(PaymentIDs, userId);
        }
     

        #endregion Public methods

        #region Protected methods
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

        #endregion Protected methods
    }
}
