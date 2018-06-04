//--------------------------------------------------------------------------------------------
// <copyright file="F49910Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F49910Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 31 Jan 2008        Ramya.D       Created                
//*********************************************************************************/
namespace D49910
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F35100Controller Class file
    /// </summary>
    public class F49910Controller : Controller
    {
        /// <summary>
        /// From the form F49910WorkItem
        /// </summary>
        public new F49910WorkItem WorkItem
        {
            get { return base.WorkItem as F49910WorkItem; }
        }
    }
}
