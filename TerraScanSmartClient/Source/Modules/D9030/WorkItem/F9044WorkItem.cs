//--------------------------------------------------------------------------------------------
// <copyright file="F9040Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 06 March 2013      Purushotham.A        Created
//*********************************************************************************/
namespace D9030
{
    #region NameSpaces

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

    #endregion NameSpaces

    /// <summary>
    /// F9044WorkItem
    /// </summary>
    public class F9044WorkItem : WorkItem
    {
        public F9044SnapshotOperations GetSnapshotDetails(int FormNum, int UserId)
        {
            return WSHelper.GetSnapshotDetails(FormNum, UserId);
        }

        public F9044SnapshotOperations GetSnapshotOperationCount(int OperationId, int LOSnapshotId, int ROSnapshotId, int UserId)
        {
            return WSHelper.GetSnapshotOperationCount(OperationId, LOSnapshotId, ROSnapshotId, UserId);
        }
        public void insertSnapshotDetails(int OperationId, int LOSnapshotId, int ROSnapshotId, int RecordCount, string NewSnapshotName, int UserId)
        {
            WSHelper.insertSnapshotDetails(OperationId, LOSnapshotId, ROSnapshotId, RecordCount, NewSnapshotName, UserId);
        }
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
    }
}
