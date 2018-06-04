//--------------------------------------------------------------------------------------------
// <copyright file="F8910WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Work Order General. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/10/2001   	M.Vijayakumar      Created
//*********************************************************************************/

namespace D8900
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
    /// Class for F8910WorkItem
    /// </summary>
    public class F8910WorkItem : WorkItem 
    {
        #region GDoc Work order General

        #region Get GDoc Work order General

        /// <summary>
        /// Get work order general values for F8912.
        /// </summary>
        /// <param name="workorderId">The workorder id.</param>
        /// <returns>Typed DataSet containing the GDoc Work order General Values</returns>
        public GDocWorkOrderGeneralData F8910_GetWorkOrderGeneral(int workorderId)
        {
            return WSHelper.F8910_GetWorkOrderGeneral(workorderId);
        }

        #endregion Get GDoc Work order General

        #region Save GDoc Work order General

        /// <summary>
        /// Save work order general values for F8910.
        /// </summary>
        /// <param name="workOrderGeneral">The work order general.</param>
        /// <returns>Typed DataSet containing the GDoc Work order General Values</returns>
        public GDocWorkOrderGeneralData F8910_SaveWorkOrderGeneral(string workOrderGeneral, int userID)
        {
            return WSHelper.F8910_SaveWorkOrderGeneral(workOrderGeneral,userID);
        }

        #endregion Save GDoc Work order General

        #endregion GDoc Work order General       

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
