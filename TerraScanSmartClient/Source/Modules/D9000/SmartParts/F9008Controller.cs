//--------------------------------------------------------------------------------------------
// <copyright file="F9008Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the workitem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 23 Jan 07        KUPPUSAMY         Created
//*********************************************************************************/

namespace D9000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using System.Windows.Forms;

    /// <summary>
    /// F9008Controller Class
    /// </summary>
    public class F9008Controller : Controller
    {
        /// <summary>
        /// F9008WorkItem Property
        /// </summary>
        public new F9008WorkItem WorkItem
        {
            get { return base.WorkItem as F9008WorkItem; }
        }
    }
}
