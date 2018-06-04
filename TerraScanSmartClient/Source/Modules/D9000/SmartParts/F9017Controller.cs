//--------------------------------------------------------------------------------------------
// <copyright file="F9017Controller.cs" company="Congruent">
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
// 13 Sep 06        VINOTH              Created
//*********************************************************************************/

namespace D9000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using System.Windows.Forms;

    /// <summary>
    /// F9016Controller Class
    /// </summary>
    public class F9017Controller : Controller
    {
        /// <summary>
        /// F9016WorkItem Property
        /// </summary>
        public new F9017WorkItem WorkItem
        {
            get { return base.WorkItem as F9017WorkItem; }
        }
    }
}
