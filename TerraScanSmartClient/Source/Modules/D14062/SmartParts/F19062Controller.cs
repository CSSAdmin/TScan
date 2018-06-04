//--------------------------------------------------------------------------------------------
// <copyright file="11024Controller.cs" company="Congruent">
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

namespace D14062
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F19062Controller : Controller
    {

        public new F19062WorkItem WorkItem
        {
            get { return base.WorkItem as F19062WorkItem; }
        }
    }
}
