//--------------------------------------------------------------------------------------------
// <copyright file="F28100Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for F28100 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07/05/2011        D.LathaMaheswari  Created
//***********************************************************************************************/
namespace D23100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F28100Controller
    /// </summary>
    public class F28100Controller : Controller
    {
        /// <summary>
        /// From the form F28100 workitem
        /// </summary>
        public new F28100WorkItem WorkItem
        {
            get { return base.WorkItem as F28100WorkItem; }
        }
    }
}
