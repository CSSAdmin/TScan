//--------------------------------------------------------------------------
// <copyright file="F36033Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36032 FS Land Codes.
// </summary>
//--------------------------------------------------------------------------
// Change History
//**************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------
// 14/09/2007       Shiva              Created
//                  
//**************************************************************************
namespace D36030
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F36032 Controller Class file.
    /// </summary>
    public class F36032Controller : Controller
    {
        /// <summary>
        /// From the form F36032 workitem
        /// </summary>
        public new F36032WorkItem WorkItem
        {
            get { return base.WorkItem as F36032WorkItem; }
        }
    }
}
