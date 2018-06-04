//--------------------------------------------------------------------------------------------
// <copyright file="F29630Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F29630 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 01/10/2008        D.LathaMaheswari  Created
//***********************************************************************************************/
namespace D24630
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F29630Controller
    /// </summary>
    public class F29630Controller : Controller
    {
        /// <summary>
        /// From the form F29630 workitem
        /// </summary>
        public new F29630WorkItem WorkItem
        {
            get { return base.WorkItem as F29630WorkItem; }
        }
    }
}
