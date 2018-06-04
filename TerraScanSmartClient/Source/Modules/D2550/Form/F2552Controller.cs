//--------------------------------------------------------------------------------------------
// <copyright file="F2552Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2552Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22/09/2011         Manoj Kumar.P   Created
//*********************************************************************************/


namespace D2550
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class file for F2552 Controller
    /// </summary>
    public class F2552Controller : Controller 
    {
        /// <summary>
        /// From the form F2205 WorkItem
        /// </summary>
        public new F2552WorkItem WorkItem
        {
            get { return base.WorkItem as F2552WorkItem;}
        }
    }
}
