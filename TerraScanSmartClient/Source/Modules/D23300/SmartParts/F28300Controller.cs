//--------------------------------------------------------------------------------------------
// <copyright file="F28300Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F28300Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07082016       priyadharshini       
//*********************************************************************************/

namespace D23300
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// form F28200 Controller
    /// </summary>
    public class F28300Controller : Controller
    {
        /// <summary>
        /// From the form F28300 workitem
        /// </summary>
        public new F28300WorkItem WorkItem
        {
            get { return base.WorkItem as F28300WorkItem; }
        }
    }
}
