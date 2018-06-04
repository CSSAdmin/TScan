//--------------------------------------------------------------------------------------------
// <copyright file="F15104WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28 Dec 06		KARTHIKEYAN V	    Created
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
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F15104WorkItem
    /// </summary>
    public class F15104WorkItem : WorkItem
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
        /// Gets the receipt payment.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>ReceiptPayamentDataSet</returns>
        public F15104ReceiptPayamentData GetReceiptPayment(int receiptId)
        {
            return WSHelper.F15104_GetReceiptPayment(receiptId);
        }

        /// <summary>
        /// Updates the receipt payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">The user ID.</param>
        public void UpdateReceiptPayment(string receiptPayment, int userId)
        {
            WSHelper.F15104_UpdateReceiptPayment(receiptPayment, userId);
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
        /// Fires the <see cref="E:Microsoft.Practices.CompositeUI.WorkItem.RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="M:Microsoft.Practices.CompositeUI.WorkItem.Run"/>
        /// method is called on the <see cref="T:Microsoft.Practices.CompositeUI.WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="E:Microsoft.Practices.CompositeUI.WorkItem.Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
