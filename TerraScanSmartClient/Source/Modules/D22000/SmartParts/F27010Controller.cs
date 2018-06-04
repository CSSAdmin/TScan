//--------------------------------------------------------------------------------------------
// <copyright file="F27010Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F27000Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			  Author		       Description
// ----------	  ---------		      ---------------------------------------------------------
// 26 Mar 08      D.LathaMaheswari     Created
//*********************************************************************************/
namespace D22000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F27010 Controller class file
    /// </summary>
    public class F27010Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F27010WorkItem WorkItem
        {
            get { return base.WorkItem as F27010WorkItem; }
        }
    }
}
