// -------------------------------------------------------------------------------------------
// <copyright file="F9051WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Statement</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
// -------------------------------------------------------------------------------------------
namespace D9050
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
    /// form F9051 WorkItem
    /// </summary>
    public class F9051WorkItem : WorkItem
    {
        /// <summary>
        /// Insert Snapshot Utility 
        /// </summary>
        /// <param name="snapshotId">snapshotId</param>
        /// <param name="snapshotName">snapshotName</param>
        /// <param name="snapshotFormId">snapshotFormId</param>
        /// <param name="description">description</param>
        /// <param name="recordCount">recordCount</param>
        /// <param name="userId">userId</param>
        /// <param name="overrideValue">overrideValue</param>
        /// <param name="keyIds">keyIds</param>
        /// <returns>return integer</returns>
        public int InsertSnapshotUtility(int snapshotId, string snapshotName, int snapshotFormId, string description, int recordCount, int userId, int overrideValue, string keyIds)
        {
            return WSHelper.InsertSnapshotUtility(snapshotId, snapshotName, snapshotFormId, description, recordCount, userId, overrideValue, keyIds);
        }

        /// <summary>
        /// Get Snapshot UtilityList
        /// </summary>
        /// <param name="formId">formId</param>
        /// <returns>DataSet</returns>
        public SnapshotUtilityData GetSnapshotUtilityList(int formId)
        {
            return WSHelper.GetSnapshotUtilityList(formId);
        }

        /// <summary>
        /// Delete Snapshot Utility
        /// </summary>
        /// <param name="snapshotId">snapshotID</param>
        public void DeleteSnapshotUtility(int snapshotId,int userID)
        {
            WSHelper.DeleteSnapshotUtility(snapshotId,userID);
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
