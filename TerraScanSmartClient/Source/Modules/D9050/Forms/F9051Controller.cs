//--------------------------------------------------------------------------------------------
// <copyright file="F9051Controller.cs" company="Congruent">
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


namespace D9050
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// form f9051 Controller
    /// </summary>
    public class F9051Controller : Controller
    {
        /// <summary>
        /// From the form F9051 workitem
        /// </summary>
        public new F9051WorkItem WorkItem
        {
            get { return base.WorkItem as F9051WorkItem; }
        }
    }
}
