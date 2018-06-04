//--------------------------------------------------------------------------------------------
// <copyright file="F1503Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1503Controller.
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
    /// F1503Controller class file
    /// </summary>
    public class F1503Controller : Controller
    {
        /// <summary>
        /// From the form F1503 workitem
        /// </summary>
        public new F1503WorkItem WorkItem
        {
            get { return base.WorkItem as F1503WorkItem; }
        }
    }
}
