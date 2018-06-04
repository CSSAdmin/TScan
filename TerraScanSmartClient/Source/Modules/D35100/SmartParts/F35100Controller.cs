//--------------------------------------------------------------------------------------------
// <copyright file="F35100Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F35100Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 may 07        Ramya.D       Created                
//*********************************************************************************/


namespace D35100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F35100Controller Class file
    /// </summary>
    public class F35100Controller : Controller
    {
        /// <summary>
        /// From the form F35100 workitem
        /// </summary>
        public new F35100WorkItem WorkItem
        {
            get { return base.WorkItem as F35100WorkItem; }
        }
    }
}
