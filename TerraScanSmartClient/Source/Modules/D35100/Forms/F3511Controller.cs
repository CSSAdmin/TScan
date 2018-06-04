
//--------------------------------------------------------------------------------------------
// <copyright file="F3511Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F3040Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20121022         Palanaiappan        Created
//                  
//*********************************************************************************/
namespace D35100
{
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F3511Controller Class file
    /// </summary>
    public class F3511Controller : Controller
    {        
        /// <summary>
        /// From the form F3511 workitem
        /// </summary>
        public new F3511WorkItem WorkItem
        {
            get { return base.WorkItem as F3511WorkItem; }
        }
    }
}
