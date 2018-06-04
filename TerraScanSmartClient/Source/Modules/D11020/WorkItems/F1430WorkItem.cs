// -------------------------------------------------------------------------------------------
// <copyright file="F1430WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F1430</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 13/12/2007       Jaya Prakash.k     ///Created
// -------------------------------------------------------------------------------------------

namespace D11020
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
    /// class inheriting the WorkItem
    /// </summary>
    public class F1430WorkItem : WorkItem
    {
        #region F1430 Interest Calculator

        /// <summary>
        /// F1430_GetCalculatorDetails gets the calculator details on load.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>String</returns>
        public F1430InterestCalculatorData F1430_GetCalculatorDetails(int statementId)
        {
            return WSHelper.F1430_GetCalculatorDetails(statementId);
        }

        /// <summary>
        /// F1430_GetInterestDetails get the interest and deliquency details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="taxAmount">The tax amount.</param>
        /// <returns>String</returns>
        public F1430InterestCalculatorData F1430_GetInterestDetails(int statementId, DateTime interestDate, decimal taxAmount)
        {
            return WSHelper.F1430_GetInterestDetails(statementId, interestDate, taxAmount);
        }

        #endregion F1430 Interest Calculator

        #region WorkItemEvents

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

        #endregion WorkItemEvents
    }
}
