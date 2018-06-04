//--------------------------------------------------------------------------------------------
// <copyright file="1124Controller.cs" company="Congruent">
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

// 07-Jan-2015       Purushotham A       Created
//*********************************************************************************/

namespace D11024
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F1124Controller : Controller
    {

        public new F1124WorkItem WorkItem
        {
            get { return base.WorkItem as F1124WorkItem; }
        }
    }
}
