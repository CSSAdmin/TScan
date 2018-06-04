//--------------------------------------------------------------------------------------------
// <copyright file="F1019Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1019Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 March 11      Manoj            created// 
//*********************************************************************************/



namespace D1018
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;  


    /// <summary>
    /// F1019 Controller class file
    /// </summary>
    public class F1019Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1019WorkItem WorkItem
        {
            get { return base.WorkItem as F1019WorkItem; }
        }
    }

}
