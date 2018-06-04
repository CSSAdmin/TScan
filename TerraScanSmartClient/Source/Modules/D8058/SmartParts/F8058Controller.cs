//--------------------------------------------------------------------------------------------
// <copyright file="F8058Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8058Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/

namespace D8058
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class F8058Controller
    /// </summary>
    public class F8058Controller : Controller
    {
        public new F8058WorkItem WorkItem
        {
            get { return base.WorkItem as F8058WorkItem; }
        }
    }
}
