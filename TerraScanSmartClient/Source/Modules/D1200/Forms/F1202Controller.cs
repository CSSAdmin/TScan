//--------------------------------------------------------------------------------------------
// <copyright file="F1202Controller.cs" company="Congruent">
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
// 08 Sept 06        Krishna Abburi      Created
//*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace D1200
{
    public class F1202Controller : Controller
    {
        public new F1202WorkItem WorkItem
        {
            get { return base.WorkItem as F1202WorkItem; }
        }
    }
}
