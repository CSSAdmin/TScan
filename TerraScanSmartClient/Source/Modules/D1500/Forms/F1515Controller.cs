//--------------------------------------------------------------------------------------------
// <copyright file="F1515Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1515Controller.
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
    /// Class file for F1515Controller
    /// </summary>
    public class F1515Controller : Controller
    {
        /// <summary>
        /// From the form F1515 workitem
        /// </summary>
        public new F1515WorkItem WorkItem
        {
            get { return base.WorkItem as F1515WorkItem; }
        }
    }
}
