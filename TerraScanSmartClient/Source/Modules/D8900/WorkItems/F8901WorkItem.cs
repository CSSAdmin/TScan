//--------------------------------------------------------------------------------------------
// <copyright file="F8901WorkItem.cs" company="Congruent">
//    Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//  This file contains methods for the Work Order Engine. 
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date               Author             Description
// ----------        ---------        ---------------------------------------------------------
// 10/10/2001      M.Vijayakumar       Created
// *********************************************************************************/

namespace D8900
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;
    using TerraScan.SmartParts;

    /// <summary>
    /// F8901WorkItem Class file
    /// </summary>
    public class F8901WorkItem : WorkItem
    {
        #region GDoc Work Order Engine

        #region GetSystemId

        /// <summary>
        /// Gets the system id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="formNumber">The form number.</param>
        /// <returns>The System Id</returns>
        public int GetSystemId(int userId, int formNumber)
        {
            return WSHelper.GetSystemId(userId, formNumber);
        }

        #endregion GetSystemId

        #region GetWorkOrderEngine

        /// <summary>
        /// Gets the work order engine.
        /// </summary>
        /// <param name="systemId">The system id.</param>
        /// <param name="isOpen">The is open.</param>
        /// <returns>Typed Dataset containing the Work Order Engine Values</returns>
        public GDocWorkOrderEngineData F8901_GetWorkOrderEngine(int systemId, int isOpen)
        {
            return WSHelper.F8901_GetWorkOrderEngine(systemId, isOpen);
        }

        #endregion GetWorkOrderEngine

        #region GetWorkOrderType

        /// <summary>
        /// Gets the type of the work order.
        /// </summary>
        /// <param name="systemId">The system id.</param>        
        /// <returns>Typed Dataset containing the Work Order Type Values</returns>
        public GDocWorkOrderEngineData F8901_GetWorkOrderType(int systemId)
        {
            return WSHelper.F8901_GetWorkOrderType(systemId);
        }

        #endregion GetWorkOrderType

        #region SaveWorkOrderEngine

        /// <summary>
        /// F8901_s the save work order engine.
        /// </summary>
        /// <param name="workOrderItems">The work order items.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>Work order data</returns>
        public GDocWorkOrderEngineData F8901_SaveWorkOrderEngine(string workOrderItems, int userId)
        {
            return WSHelper.F8901_SaveWorkOrderEngine(workOrderItems, userId);
        }

        #endregion SaveWorkOrderEngine

        #endregion GDoc Work Order Engine

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
