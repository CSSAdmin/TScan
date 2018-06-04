//--------------------------------------------------------------------------------------------
// <copyright file="F2000Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F35101Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// May 14 2007      Sam K     Created                
//*********************************************************************************/

namespace D2000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class for Controller
    /// </summary>
    public class F2000Controller : Controller
    {
        /// <summary>
        ///  Gets the current work item where the controller lives.
        /// </summary>     
        public new F2000WorkItem WorkItem
        {
            get { return base.WorkItem as F2000WorkItem; }
        }
    }
}