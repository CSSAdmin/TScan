//--------------------------------------------------------------------------------------------
// <copyright file="F15101WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15101WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//*********************************************************************************/

namespace D11001
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using System.Data;
    using TerraScan.BusinessEntities;

    #endregion NameSpaces

    /// <summary>
    /// F15101WorkItem class
    /// </summary>
    public class F15101WorkItem : WorkItem
    {
        /// <summary>
        /// Lists the receipt items.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>F15101ReceiptItemsData</returns>
        public F15101ReceiptItemsData ListReceiptItems(int receiptId)
        {
            return WSHelper.ListReceiptItems(receiptId);
        }

        /// <summary>
        /// F15100 workitem
        /// </summary>
        /// <param name="receiptId">ReceiptId</param>
        /// <returns>GetReceiptHeaderDetails</returns>
        public F15100ReceiptHeaderData GetReceiptHeaderDetails(int receiptId)
        {
            return WSHelper.GetReceiptHeaderDetails(receiptId);
        }

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

        /// <summary>
        /// F15101_s the update transaction items.
        /// </summary>
        /// <param name="transactionItems">The transaction items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>the status.</returns>
        public int F15101_UpdateTransactionItems(string transactionItems, int userId)
        {
            return WSHelper.F15101_UpdateTransactionItems(transactionItems, userId);
        }

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
    }
}
