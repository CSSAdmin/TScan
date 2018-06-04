//--------------------------------------------------------------------------------------------
// <copyright file="F1502WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Function Management. 
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
    /// F1502WorkItem class file
    /// </summary>
    public class F1502WorkItem : WorkItem
    {
        #region F1502 Account Element Management

        #region GetAccountElementMgmt

        /// <summary>
        /// To get Account Element Management details
        /// </summary>
        /// <param name="function">The Function Id</param>
        /// <param name="description">The Description</param>
        /// <param name="type">The Type - SemiAnnualCode </param>
        /// <returns>Typed Dataset containing the Account Element Management details</returns>
        public F1502AccountManagementData F1502_GetAccountElementMgmt(string function, string description, int type)
        {
            return WSHelper.F1502_GetAccountElementMgmt(function, description, type);
        }

        #endregion GetAccountElementMgmt

        #region SaveAccountElementMgmt

        /// <summary>
        /// To Save Account Element Management details
        /// </summary>
        /// <param name="functionElemnts">The xml string which contains the Account elements mgmt Grid values</param>
        /// <returns>Integer value containing whether save is compleded or not</returns>
        public int F1502_SaveAccountElementMgmt(string functionElemnts, int userId)
        {
            return WSHelper.F1502_SaveAccountElementMgmt(functionElemnts, userId);
        }

        #endregion SaveAccountElementMgmt

        #region DeleteAccountElementMgmt

        /// <summary>
        /// To Delete Account Element Management details
        /// </summary>
        /// <param name="functionId">The Functional Id</param>
        public void F1502_DeleteAccountElementMgmt(string functionId, int userId)
        {
            WSHelper.F1502_DeleteAccountElementMgmt(functionId, userId);
        }

        #endregion DeleteAccountElementMgmt

        #endregion F1502 Account Element Management

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
