//--------------------------------------------------------------------------------------------
// <copyright file="F95005Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F95005Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07/06/2007       M.Vijayakumar       Created
//                  
//*********************************************************************************/

namespace D90005
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F95005Controller class file 
    /// </summary>
    public class F95005Controller : Controller
    {
        /// <summary>
        /// From the form F95005 workitem
        /// </summary>
        public new F95005WorkItem WorkItem
        {
            get { return base.WorkItem as F95005WorkItem; }
        }
    }
}
