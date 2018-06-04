// -------------------------------------------------------------------------------------------
// <copyright file="F15110WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F15110
// </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07/05/07         S. Pradeep         Created
// -------------------------------------------------------------------------------------------
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
    /// F15110WorkItem class file 
    /// </summary>
    public class F15110WorkItem : WorkItem
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

        #region Get Receipt actions Details

        /// <summary>
        /// F15110_s the get receipt actions.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>typed dataset for receipt actions</returns>
        public F15110ReceiptActionsData F15110_GetReceiptActions(int receiptId)
        {
            return WSHelper.F15110_GetReceiptActions(receiptId);
        }


        /// <summary>
        /// F1557_s the insert refund interest.
        /// </summary>
        /// <param name="receiptID">The receipt ID.</param>
        /// <param name="userID">The user ID.</param>
        public void F1557_InsertRefundInterest(int receiptID, int userID)
        {
            WSHelper.F1557_InsertRefundInterest(receiptID, userID);
        }
        #endregion Get Form Slice Permission Details

        #region WorkItems Methods

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
        #endregion WorkItems Methods
    }
}
