//--------------------------------------------------------------------------------------------
// <copyright file="F8904Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8902Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18 Oct 06        VINOTH              Created
//*********************************************************************************/

namespace D8900
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion

    /// <summary>
    /// F8904Controller Class
    /// </summary>
    public class F8904Controller : Controller
    {
        /// <summary>
        /// From the form F8910 workitem
        /// </summary>
        public new F8904WorkItem WorkItem
        {
            get { return base.WorkItem as F8904WorkItem; }
        }
    }
}
