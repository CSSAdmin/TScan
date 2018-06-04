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
// 14/09/2007       Kuppusamy.B              Created
//                  
//**************************************************************************
namespace D36030
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F36099Controller Class file
    /// </summary>
    public class F36099Controller : Controller
    {
        /// <summary>
        /// From the form F36033 workitem
        /// </summary>
        public new F36099WorkItem WorkItem
        {
            get { return base.WorkItem as F36099WorkItem; }
        }
    }
}
