//--------------------------------------------------------------------------------------------
// <copyright file="F25050Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F81002Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07 Feb 2008       Jaya Prakash.k     ///Created
//                  
//*********************************************************************************/

namespace D20050
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
    /// class F25050
    /// </summary>
    public class F25050Controller : Controller
    {
        /// <summary>
        /// From the form F81002 workitem
        /// </summary>
        public new F25050WorkItem WorkItem
        {
            get { return base.WorkItem as F25050WorkItem; }
        }
    }
}
