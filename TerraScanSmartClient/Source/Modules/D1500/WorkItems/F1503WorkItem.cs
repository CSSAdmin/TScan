//--------------------------------------------------------------------------------------------
// <copyright file="F1503WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Generic Element Management. 
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
    /// F1503WorkItem Class file
    /// </summary>
    public class F1503WorkItem : WorkItem
    {
        #region F1503 Generic Management Comp

        #region GetGenericElementMgmt

        /// <summary>
        /// To Get the Generic Element Management details
        /// </summary>
        /// <param name="keyValue">The key value(Element ID)</param>
        /// <param name="description">The Description</param>
        /// <param name="formName">The Form Name</param>
        /// <returns>Typed Dataset containing the Element ID and Description Value</returns>
        public F1503GenericManagementData F1503_GetGenericElementMgmt(string keyValue, string description, string formName)
        {
            return WSHelper.F1503_GetGenericElementMgmt(keyValue, description, formName);
        }

        #endregion GetGenericElementMgmt

        #region SaveGenericElementMgmt

        /// <summary>
        /// To Save the Generic Element Management details
        /// </summary>
        /// <param name="functionElemnts">The Xml string containing Element ID and Description Value</param>
        /// <param name="formName">The Form name</param>
        /// <returns>Integer value containing whether save is compleded or not</returns>
        public int F1503_SaveGenericElementMgmt(string functionElemnts, string formName, int userId)
        {
            return WSHelper.F1503_SaveGenericElementMgmt(functionElemnts, formName, userId);
        }

        #endregion SaveGenericElementMgmt

        #region DeleteGenericElementMgmt

        /// <summary>
        /// To Delete the Generic Element Management details
        /// </summary>
        /// <param name="elementId">The Particular Element ID</param>
        /// <param name="formName">The Form name</param>
        public void F1503_DeleteGenericElementMgmt(string elementId, string formName, int userId)
        {
            WSHelper.F1503_DeleteGenericElementMgmt(elementId, formName, userId);
        }

        #endregion DeleteGenericElementMgmt

        #endregion F1503 Generic Management Comp

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
