//--------------------------------------------------------------------------------------------
// <copyright file="F1011Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the 1016NextNumberForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
//*********************************************************************************/

namespace D23200
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// form F28200 Controller
    /// </summary>
    public class F28200Controller : Controller
    {
        /// <summary>
        /// From the form F28200 workitem
        /// </summary>
        public new F28200WorkItem WorkItem
        {
            get { return base.WorkItem as F28200WorkItem; }
        }
    }
}
