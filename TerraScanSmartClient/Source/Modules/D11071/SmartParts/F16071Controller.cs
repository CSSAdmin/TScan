//--------------------------------------------------------------------------------------------
// <copyright file="11071Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------

// 9-Dec-2014       Purushotham A       Created
//*********************************************************************************/

namespace D11071
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F16071Controller : Controller
    {

        public new F16071WorkItem WorkItem
        {
            get { return base.WorkItem as F16071WorkItem; }
        }
    }
}
