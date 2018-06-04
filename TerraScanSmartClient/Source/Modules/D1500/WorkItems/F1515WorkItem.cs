//--------------------------------------------------------------------------------------------
// <copyright file="F1515WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the SubFund Selection. 
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
    /// Class file for F1515WorkItem
    /// </summary>
    public class F1515WorkItem : WorkItem
    {

        #region F1515 Sub Fund Selection

        #region F1515_GetSubFundSelection

        /// <summary>
        /// To Get the Sub Fund Selection Details
        /// </summary>
        /// <param name="subFund">The Sub fund</param>
        /// <param name="description">The Description</param>
        /// <param name="rollYear">The Roll year</param>
        /// <returns>Typed Dataset containing the Sub Fund Selection Details</returns>
        public F1515SubFundSelectionData F1515_GetSubFundSelection(string subFund, string description, int rollYear, int iscash)
        {
            return WSHelper.F1515_GetSubFundSelection(subFund, description, rollYear, iscash);
        }

        #endregion F1515_GetSubFundSelection

        #endregion F1515 Sub Fund Selection


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
