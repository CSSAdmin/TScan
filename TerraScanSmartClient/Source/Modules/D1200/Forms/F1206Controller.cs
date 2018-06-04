//--------------------------------------------------------------------------------------------
// <copyright file="F1206Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property to Load WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18 Sept 06        Krishna Abburi      Created
//*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace D1200
{
    public class F1206Controller : Controller
    {
        public new F1206WorkItem WorkItem
        {
            get { return base.WorkItem as F1206WorkItem; }
        }
    }
}
