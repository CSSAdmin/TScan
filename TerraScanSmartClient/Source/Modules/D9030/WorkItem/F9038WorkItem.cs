//--------------------------------------------------------------------------------------------
// <copyright file="F9038WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    Created
//*********************************************************************************/
namespace D9030
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
    /// F9038WorkItem
    /// </summary>
    public class F9038WorkItem : WorkItem
    {
        #region GetSandwichAndItsSliceInformation

        /// <summary>
        /// Loads the layout information.
        /// </summary>
        /// <param name="queryViewID">The query view ID.</param>
        /// <param name="userID">The user ID.</param>
        /// <returns>the layout details</returns>
        public F9038LayoutManagementData LoadLayoutInformation(int queryViewID, int userID)
        {
           return WSHelper.F9038_LoadLayoutInformation(queryViewID, userID);
        }

        #endregion GetSandwichAndItsSliceInformation

        #region Save LoadLayoutManagement

        /// <summary>
        /// F9038_s the save layout information.
        /// </summary>
        /// <param name="queryLayoutID">The query layout ID.</param>
        /// <param name="layoutManagement">The layout management.</param>
        /// <param name="layoutXML">The layout XML.</param>
        /// <returns>THE LAYOUTID SAVED</returns>
        public int F9038_SaveLayoutInformation(int queryLayoutID, string layoutManagement, string layoutXML,int userID)
        {
            return WSHelper.F9038_SaveLayoutInformation(queryLayoutID, layoutManagement, layoutXML,userID);
        }

        #endregion Save LoadLayoutManagement

        /// <summary>
        /// Gets the form title.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>String with Title</returns>
        public string GetFormTitle(int formId)
        {
            return WSHelper.GetFormTitle(formId);
        }

        #region Delete LoadLayoutManagement

        /// <summary>
        /// F9038_s the delete layout information.
        /// </summary>
        /// <param name="queryLayoutID">The query layout ID.</param>
        public void F9038_DeleteLayoutInformation(int queryLayoutID,int userID)
        {
            WSHelper.F9038_DeleteLayoutInformation(queryLayoutID,userID);
        }

        #endregion Delete LoadLayoutManagement

        #region WorkItem Events

        /// <summary>
        /// Override Method for OnRunStarted
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Override Method for OnActivated
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion WorkItem Events
    }
}
