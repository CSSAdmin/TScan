//--------------------------------------------------------------------------------------------
// <copyright file="F29550Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Senior Exemption.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 Sep 07	    Ramya.D       Created
//*********************************************************************************/

namespace D24550
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpace

    public class F29550Controller : Controller
    {
        // <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F29550WorkItem WorkItem
        {
            get { return base.WorkItem as F29550WorkItem; }
        }
    }
}
