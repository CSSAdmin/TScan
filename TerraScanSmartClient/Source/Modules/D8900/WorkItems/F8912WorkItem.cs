//--------------------------------------------------------------------------------------------
// <copyright file="F8912WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Work Order Call In. 
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
    /// class for F8912WorkItem
    /// </summary>
    public class F8912WorkItem : WorkItem
    {
        #region GDoc Work order CallIn

        #region Get GDoc Work order CallIn

        /// <summary>
        /// Get work order call In values  for F8912.
        /// </summary>
        /// <param name="workorderId">The work order id.</param>
        /// <returns>Typed DataSet Containing the Gdoc Work Order CallIn Values</returns>
        public GDocWorkorderCallInData F8912_GetWorkOrderCallIn(int workorderId)
        {
            return WSHelper.F8912_GetWorkOrderCallIn(workorderId);
        }

        #endregion Get GDoc Work order CallIn

        #region Get GDoc Addresses

        /// <summary>
        /// To Get Addresses for GDOC Form Slices.
        /// </summary>        
        /// <returns>Typed DataSet Containing the Gdoc Addresses</returns>
        public GDocWorkorderCallInData wListAddresses()
        {
            return WSHelper.wListAddresses();
        }

        #endregion Get GDoc Addresses

        #region Save GDoc Work order CallIn

        /// <summary>
        /// Save GDoc work order call In Values.
        /// </summary>
        /// <param name="workOrderCall">The work order call.</param>
        /// <returns>Typed DataSet Containing the Gdoc Work Order CallIn Values</returns>
        public GDocWorkorderCallInData F8912_SaveWorkOrderCallIn(string workOrderCall,int userID)
        {
            return WSHelper.F8912_SaveWorkOrderCallIn(workOrderCall,userID);
        }

        #endregion Save GDoc Work order CallIn  
 
        #endregion GDoc Work order CallIn       

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
