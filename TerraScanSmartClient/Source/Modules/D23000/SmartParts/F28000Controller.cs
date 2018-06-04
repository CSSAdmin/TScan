//--------------------------------------------------------------------------------------------
// <copyright file="F28000Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for F28000 Discretionary Exemption 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26/04/2011        D.LathaMaheswari  Created
//***********************************************************************************************/
namespace D23000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F28000Controller
    /// </summary>
    public class F28000Controller : Controller
    {
        /// <summary>
        /// From the form F28000 workitem
        /// </summary>
        public new F28000WorkItem WorkItem
        {
            get { return base.WorkItem as F28000WorkItem; }
        }
    }
}
