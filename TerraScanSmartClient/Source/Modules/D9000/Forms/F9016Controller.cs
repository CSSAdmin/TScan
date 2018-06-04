//--------------------------------------------------------------------------------------------
// <copyright file="F9016Controller.cs" company="Congruent">
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
// 12 Sep 06        VINOTHBABU         Created
//*********************************************************************************/
namespace D9000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// form f9050 Controller
    /// </summary>
    public class F9016Controller : Controller
    {
        /// <summary>
        /// From the form QuerySave workitem
        /// </summary>
        public new F9016WorkItem WorkItem
        {
            get { return base.WorkItem as F9016WorkItem; }
        }
    }
}
