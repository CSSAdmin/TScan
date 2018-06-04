//--------------------------------------------------------------------------------------------
// <copyright file="F3040Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F3040Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15/05/2007       M.Vijayakumar      Created
//                  
//*********************************************************************************/

namespace D35100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F3040Controller class file
    /// </summary>
    public class F3040Controller : Controller
    {
        /// <summary>
        /// From the form F3040 workitem
        /// </summary>
        public new F3040WorkItem WorkItem
        {
            get { return base.WorkItem as F3040WorkItem; }
        }
    }
}
