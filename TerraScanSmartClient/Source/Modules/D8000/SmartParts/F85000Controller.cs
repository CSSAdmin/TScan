//--------------------------------------------------------------------------------------------
// <copyright file="F85000Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F85000 Gdoc Comments Controllers.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 Aug 2007      Ramya.D             Created
//                  
//*********************************************************************************/
namespace D8000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// class for F85000Controller
    /// </summary>
    public class F85000Controller : Controller
    {
        /// <summary>
        /// From the form F85000 WorkItem 
        /// </summary>
        public new F85000WorkItem WorkItem
        {
            get { return base.WorkItem as F85000WorkItem; }
        }
    }
}
