//--------------------------------------------------------------------------------------------
// <copyright file="F27008Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F27008Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------               
//*********************************************************************************/

namespace D22008
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F27008Controller Class file
    /// </summary>
    public class F27008Controller : Controller 
    {
        /// <summary>
        /// From the form F27008 workitem
        /// </summary>
        public new F27008WorkItem WorkItem
        {
            get { return base.WorkItem as F27008WorkItem; }
        }
    }
}
