//--------------------------------------------------------------------------------------------
// <copyright file="F1430Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1430Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 13/12/2007       Jaya Prakash.k     ///Created
//*********************************************************************************/

namespace D11020
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
    /// class inheriting the controller
    /// </summary>
    public class F1430Controller : Controller
    {
        /// <summary>
        /// From the form F1430 workitem
        /// </summary>
        public new F1430WorkItem WorkItem
        {
            get { return base.WorkItem as F1430WorkItem; }
        }
    }
}
