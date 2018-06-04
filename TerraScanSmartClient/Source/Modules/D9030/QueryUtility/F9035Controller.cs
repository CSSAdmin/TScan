//--------------------------------------------------------------------------------------------
// <copyright file="F9035Controller.cs" company="Congruent">
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
// 20 Dec 06       Guhan                Created
//*********************************************************************************/


#region NameSpaces

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

#endregion NameSpaces

namespace D9030
{
    /// <summary>
    /// Class F9030Controller
    /// </summary>
    public class F9035Controller : Controller
    {
        /// <summary>
        /// Created Property for F90320WorkItem
        /// </summary>
        public new F9035WorkItem WorkItem
        {
            get { return base.WorkItem as F9035WorkItem; }
        }
    }
}
