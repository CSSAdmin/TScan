//--------------------------------------------------------------------------------------------
// <copyright file="F1504WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Copy Account. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 1/9/2009   	   R.Malliga           Created
//*********************************************************************************/

namespace D1500
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
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F1504WorkItem Class file
    /// </summary>
    public class F1504WorkItem : WorkItem
    {
        #region Get Configuration Value

        /// <summary>
        /// F1500_s the get configuration value.
        /// </summary>
        /// <param name="cfgName">Name of the CFG.</param>
        /// <returns>F1500_GetConfigurationValue</returns>
        public AccountManagementData F1500_GetConfigurationValue(string cfgName)
        {
            return WSHelper.F1500_GetConfigurationValue(cfgName);
        }

        #endregion


        /// <summary>
        /// F1504_s the get copy account sub fund.
        /// </summary>
        /// <returns></returns>
        public F1504CopyAccountData F1504_GetCopyAccountSubFund()
        {
            return WSHelper.F1504_GetCopyAccountSubFund();
        }

        /// <summary>
        /// F1504_s the get account detail.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns></returns>
        public F1504CopyAccountData F1504_GetAccountDetail(int accountId)
        {
            return WSHelper.F1504_GetAccountDetail(accountId);
        }

        /// <summary>
        /// F1504_s the save copy account details.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="description">The description.</param>
        /// <param name="function">The function.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="accObject">The acc object.</param>
        /// <param name="line">The line.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public F1504CopyAccountData F1504_SaveCopyAccountDetails(int rollYear, string subFund, string description, string function, string bars, string accObject, string line, string userId)
        {
            return WSHelper.F1504_SaveCopyAccountDetails(rollYear, subFund, description, function, bars, accObject, line, userId);
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
