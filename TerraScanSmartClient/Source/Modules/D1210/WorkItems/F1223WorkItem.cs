// -------------------------------------------------------------------------------------------------
// <copyright file="F1223WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10/10/2006       Ranjani            Created// 
//*********************************************************************************/
namespace D1210
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
    /// F1223 WorkItem
    /// </summary>
    public class F1223WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the Cash Ledger(check) Detail
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public CheckDetailData F1226_GetCashLedger(int clid)
        {
            return WSHelper.F1226_GetCashLedger(clid);
        }

        /// <summary>
        /// Updates the Cash Ledger Status
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="functionDate">The function date.</param>
        /// <param name="functionId">The function id.</param>
        public void F1226_UpdateCashLedgerStatus(int clid, int userId, DateTime functionDate, int functionId, int loginUserId)
        {
            WSHelper.F1226_UpdateCashLedgerStatus(clid, userId, functionDate, functionId, loginUserId);
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
