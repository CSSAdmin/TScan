//--------------------------------------------------------------------------------------------
// <copyright file="F1011Controller.cs" company="Congruent">
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

namespace D1010
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// form F1011 Controller
    /// </summary>
    public class F1011Controller : Controller
    {
        /// <summary>
        /// From the form F1011 workitem
        /// </summary>
        public new F1011WorkItem WorkItem
        {
            get { return base.WorkItem as F1011WorkItem; }
        }
    }
}
