//--------------------------------------------------------------------------------------------
// <copyright file="F15110Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15110Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07/05/07         S. Pradeep         Created                
//*********************************************************************************/

namespace D11001
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F15110Controller Class file
    /// </summary>
    public class F15110Controller : Controller
    {
        /// <summary>
        /// From the form F15110 workitem
        /// </summary>
        public new F15110WorkItem WorkItem
        {
            get { return base.WorkItem as F15110WorkItem; }
        }
    }
}
