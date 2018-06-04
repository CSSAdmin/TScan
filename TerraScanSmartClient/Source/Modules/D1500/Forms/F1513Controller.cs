//--------------------------------------------------------------------------------------------
// <copyright file="F1513Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1513Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/11/2001   	M.Vijayakumar      Created
//                  
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class file for F1513Controller
    /// </summary>
    public class F1513Controller : Controller
    {
        /// <summary>
        /// From the form F1513 workitem
        /// </summary>
        public new F1513WorkItem WorkItem
        {
            get { return base.WorkItem as F1513WorkItem; }
        }
    }
}
