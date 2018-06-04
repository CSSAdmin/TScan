//--------------------------------------------------------------------------------------------
// <copyright file="F16041Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F16041Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20170713         Dhineshkumar        Created
//*********************************************************************************/

namespace D10041
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F16041Controller : Controller
    {
        public new F16041WorkItem WorkItem
        {
            get { return base.WorkItem as F16041WorkItem; }
        }
    }
}
