// -------------------------------------------------------------------------------------------
// <copyright file="F49920WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F49920</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 2/11/2007        Malliga           Created
// -------------------------------------------------------------------------------------------

namespace D49910
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
    /// F49920WorkItem
    /// </summary>
   public class F49920WorkItem : WorkItem
    {
        /// <summary>
        /// F49920_s the list instrument load.
        /// </summary>
        /// <returns>DataSet</returns>
        public F49920InstrumentSearchEngineData F49920_ListInstrumentLoad()
        {
            return WSHelper.F49920_ListInstrumentLoad();
        }

        /// <summary>
        /// F49920_s the list instrument search.
        /// </summary>
        /// <param name="instrumentcondition">The instrumentcondition.</param>
        /// <returns>DataSet</returns>
        public F49920InstrumentSearchEngineData F49920_ListInstrumentSearch(string instrumentcondition)
        {
            return WSHelper.F49920_ListInstrumentSearch(instrumentcondition);
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
