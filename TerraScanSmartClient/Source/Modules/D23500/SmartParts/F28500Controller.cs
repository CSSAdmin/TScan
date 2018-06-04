//--------------------------------------------------------------------------------------------
// <copyright file="F28300Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F23500Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21122017      priyadharshini       
//*********************************************************************************/

namespace D23500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// form F28500 Controller
    /// </summary>
    public class F28500Controller : Controller
    {
        /// <summary>
        /// From the form F28500 workitem
        /// </summary>
        public new F28500WorkItem WorkItem
        {
            get { return base.WorkItem as F28500WorkItem; }
        }
    }
}
