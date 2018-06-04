//--------------------------------------------------------------------------------------------
// <copyright file="F9104WorkItem.cs" company="Congruent">
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

namespace D9500
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
    /// Class file for F9104WorkItem
    /// </summary>
    public class F9104WorkItem : WorkItem
    {
        #region F9104 Fund Selection

        #region F9104_GetFundSelection

        /// <summary>
        /// To Get the Fund Selection details
        /// </summary>
        /// <param name="fund">The Fund</param>
        /// <param name="description">The Description</param>
        /// <returns>Typed Dataset Containing the Fund Selection details</returns>
        ////public F9104FundSelectionData F9104_GetFundSelection(int fund, string description)
        ////{
        ////    ////return WSHelper.F9104_GetFundSelection(fund, description);
        ////}

        #endregion F9104_GetFundSelection

        #endregion F9104 Fund Selection

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
