//--------------------------------------------------------------------------------------------
// <copyright file="F28310Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F28310Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20160708       priyadharshini              Created
//*********************************************************************************/
namespace D23310
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F28310Controller : Controller
    {

        /// <summary>
        /// From the form F28310 workitem
        /// </summary>
        public new F28310WorkItem WorkItem
        {
            get { return base.WorkItem as F28310WorkItem; }
        }
    }
}
