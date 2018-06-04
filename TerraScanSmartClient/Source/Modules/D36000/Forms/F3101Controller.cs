//--------------------------------------------------------------------------------------------
// <copyright file="F3101Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F3101Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/

namespace D36000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F3101Controller class file
    /// </summary>
    public class F3101Controller : Controller
    {
        /// <summary>
        /// From the form F27006 workitem
        /// </summary>
        public new F3101WorkItem WorkItem
        {
            get { return base.WorkItem as F3101WorkItem; }
        }
    }
}
