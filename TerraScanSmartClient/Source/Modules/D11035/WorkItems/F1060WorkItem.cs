//--------------------------------------------------------------------------------------------
// <copyright file="F15035WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F15035 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-05-2007       Shiva              Created
//*********************************************************************************/
namespace D11035
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
    /// F1060 WorkItem.
    /// </summary>
    public class F1060WorkItem : WorkItem
    {
        #region F1060 Suspended Payment Selection

        #region F1060 List Suspended Payment

        /// <summary>
        /// F1060_s the list suspended payment.
        /// </summary>
        /// <param name="lastName">Name of the last.</param>
        /// <param name="firstName">Name of the first.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <returns>Typed DataSet containing the Suspended Payment Details.</returns>
        //public F1060SudpendedPaymentSelectionData F1060_ListSuspendedPayment(string lastName, string firstName, string receiptDate)
        //{
        //    return WSHelper.F1060_ListSuspendedPayment(lastName, firstName, receiptDate);
        //}
        public F1060SudpendedPaymentSelectionData F1060_ListSuspendedPayment(string SearchDetail)
        {
            return WSHelper.F1060_ListSuspendedPayment(SearchDetail);
        }

        #endregion F1060 List Suspended Payment

        #endregion F1060 Suspended Payment Selection

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
