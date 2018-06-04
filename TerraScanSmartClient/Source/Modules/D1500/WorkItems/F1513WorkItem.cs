//--------------------------------------------------------------------------------------------
// <copyright file="F1513WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Fund Selection. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 9/11/2006   	M.Vijayakumar      Created
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
    /// Class file for F1513WorkItem
    /// </summary>
    public class F1513WorkItem : WorkItem
    {
        #region F1513 Fund Selection

        #region F1513_GetFundSelection

        /// <summary>
        /// To Get the Fund Selection details
        /// </summary>
        /// <param name="fund">The Fund</param>
        /// <param name="description">The Description</param>
        /// <returns>Typed Dataset Containing the Fund Selection details</returns>
        public F1513FundSelectionData F1513_GetFundSelection(string fund, string description)
        {
            return WSHelper.F1513_GetFundSelection(fund, description);
        }

        /// <summary>
        /// F1513_CentralFundItemValidation
        /// </summary>
        /// <param name="fundId"></param>
        /// <param name="rollYear"></param>
        /// <returns></returns>
        public int F1513_CentralFundItemValidation(int fundId, int rollYear)
        {
            return WSHelper.F1513_CentralFundItemValidation(fundId, rollYear);
        }

        #endregion F1513_GetFundSelection

        #endregion F1513 Fund Selection

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
