//--------------------------------------------------------------------------------------------
// <copyright file="F84401Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84401Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18/04/2008       D.LathaMaheswari    Created.
//*********************************************************************************/
namespace D84401
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F84401Controller
    /// </summary>
    public class F84401Controller : Controller
    {
        /// <summary>
        /// From the form F84401 workitem
        /// </summary>
        public new F84401WorkItem WorkItem
        {
            get { return base.WorkItem as F84401WorkItem; }
        }
    }
}
