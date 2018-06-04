//--------------------------------------------------------------------------------------------
// <copyright file="F36011Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36011Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28/6/07          M.Vijayakumar       ///Created
//                  
//*********************************************************************************/

namespace D36010
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F36011Controller Class file 
    /// </summary>
    public class F36011Controller : Controller
    {
        /// <summary>
        /// From the form F36011 workitem
        /// </summary>
        public new F36011WorkItem WorkItem
        {
            get { return base.WorkItem as F36011WorkItem; }
        }
    }
}
