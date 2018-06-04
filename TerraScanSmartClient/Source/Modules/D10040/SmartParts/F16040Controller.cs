//--------------------------------------------------------------------------------------------
// <copyright file="F16040Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F16040Controller.
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

    public class F16040Controller:Controller
    {
        /// <summary>
        /// From the form F16040 workitem
        /// </summary>
        public new F16040WorkItem WorkItem      
        {
            get { return base.WorkItem as F16040WorkItem; }
        }
    }
}
