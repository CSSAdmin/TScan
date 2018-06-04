//--------------------------------------------------------------------------------------------
// <copyright file="F8002WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8002WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/
namespace D8000
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
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;    
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F8002WorkItem
    /// </summary>
    public class F8002WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the work order details.
        /// </summary>
        /// <param name="featureClassId">The featureClass id.</param>
        /// <returns>typed dataset containing the WOID,Date,Type and Comments</returns>
        public GDocWorkOrderData GetWorkOrderDetails(int featureClassId)
        {
            return WSHelper.GetWorkOrderDetails(featureClassId);
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
