//--------------------------------------------------------------------------------------------
// <copyright file="F9060Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9060Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//                  
//*********************************************************************************/

namespace D9060
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F9060Controller Class file
    /// </summary>
    public class F9060Controller : Controller
    {
        /// <summary>
        ///  From the form F9060 workitem
        /// </summary>
        public new F9060WorkItem WorkItem
        {
            get { return base.WorkItem as F9060WorkItem; }
        }
    }
}
