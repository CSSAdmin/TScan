//--------------------------------------------------------------------------------------------
// <copyright file="F1556WorkItem.cs" company="Congruent">
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
// 01/10/2010   D.LathaMaheswari       Created
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
    /// class for workitem F1556
    /// </summary>
    public class F1556WorkItem : WorkItem
    {
        #region Public Methods

        /// <summary>
        /// Reverse receipt details
        /// </summary>
        /// <param name="receiptId">Receipt ID</param>
        /// <param name="sharedPaymentId">Shared Payment Id</param>
        /// <param name="userId">User ID</param>
        /// <returns>Reverse Payment Details</returns>
        public F1555_ReceiptDetailsData.ReverseSharedPaymentDataTable F1556_ReverseReceiptDetails(int receiptId, int sharedPaymentId, int userId)
        {
            return WSHelper.F1556_ReverseReceiptDetails(receiptId, sharedPaymentId, userId).ReverseSharedPayment;
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
