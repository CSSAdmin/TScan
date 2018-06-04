//--------------------------------------------------------------------------------------------
// <copyright file="F1043Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1043Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20170608         Dhineshkumar        Created
//*********************************************************************************/

namespace D10040
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F1044Controller:Controller
    {
        /// <summary>
        /// From the form F1044 workitem
        /// </summary>
        public new F1044WorkItem WorkItem
        {
            get { return base.WorkItem as F1044WorkItem; }
        }
    }
}
