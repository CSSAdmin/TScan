//--------------------------------------------------------------------------------------------
// <copyright file="F9075Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the 1016NextNumberForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
//*********************************************************************************/

namespace D9075
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// form F9075 Controller
    /// </summary>
    public class F9075Controller : Controller
    {
        /// <summary>
        /// From the form F9075 workitem
        /// </summary>
        public new F9075WorkItem WorkItem
        {
            get { return base.WorkItem as F9075WorkItem; }
        }
    }
}
