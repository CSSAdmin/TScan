//--------------------------------------------------------------------------------------------
// <copyright file="F1504Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1504Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 1/9/2009     	R.Malliga           Created
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
    public class F1504Controller : Controller
    {
        /// <summary>
        /// From the form F1503 workitem
        /// </summary>
        public new F1504WorkItem WorkItem
        {
            get { return base.WorkItem as F1504WorkItem; }
        }
    }
}
