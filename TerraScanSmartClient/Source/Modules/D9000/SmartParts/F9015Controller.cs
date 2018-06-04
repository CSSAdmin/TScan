//--------------------------------------------------------------------------------------------
// <copyright file="F9015Controller.cs" company="Congruent">
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
// 07 Sep 06        VINOTHBABU         Created
//*********************************************************************************/

namespace D9000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using System.Windows.Forms;

    /// <summary>
    /// F9015Controller Class
    /// </summary>
    public class F9015Controller : Controller
    {
        /// <summary>
        /// F9015WorkItem Property
        /// </summary>
        public new F9015WorkItem WorkItem
        {
            get { return base.WorkItem as F9015WorkItem; }
        }
    }
}
