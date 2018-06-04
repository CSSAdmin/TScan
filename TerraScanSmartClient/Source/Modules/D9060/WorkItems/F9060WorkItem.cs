//--------------------------------------------------------------------------------------------
// <copyright file="F9060WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9060WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 April 06      JYOTHI              Created
//*********************************************************************************/
namespace D9060
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
    /// F9060WorkItem
    /// </summary>
    public class F9060WorkItem : WorkItem
    {
        #region F9060 Auditing Configuration

        #region List Auditing Tables

        /// <summary>
        /// To List Audit Table Details
        /// </summary>
        /// <param name="tableType">Table Type</param>
        /// <returns>Typed Dataset Containing the Audit Table Details</returns>
        public F9060AuditingConfigurationData F9060_ListAuditingTables(string tableType)
        {
            return WSHelper.F9060_ListAuditingTables(tableType);
        }

        #endregion List Auditing Tables

        #region List Auditing Columns

        /// <summary>
        /// To List Audit Column Details
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <returns>Typed Dataset Containing the Audit Column Details</returns>
        public F9060AuditingConfigurationData F9060_ListAuditingColumns(string tableName)
        {
            return WSHelper.F9060_ListAuditingColumns(tableName);
        }

        #endregion List Auditing Columns

        #region Save Audit Column Configuration

        /// <summary>
        /// To Save the Audit Configuration
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="auditColumns">Audit Columns</param>
        public void F9060_SaveAuditConfiguration(string tableName, string auditColumns,int userID)
        {
            WSHelper.F9060_SaveAuditConfiguration(tableName, auditColumns,userID);
        }

        #endregion Save Audit Column Configuration

        #region Delete Audit Column Configuration

        /// <summary>
        /// To Save the Audit Configuration
        /// </summary>
        /// <param name="auditTableID">Audit TableID</param>
        public void F9060_DeleteAuditConfiguration(int auditTableID,int userID)
        {
            WSHelper.F9060_DeleteAuditConfiguration(auditTableID,userID);
        }

        #endregion Delete Audit Column Configuration

        #endregion F9060 Auditing Configuration

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
