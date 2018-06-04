//--------------------------------------------------------------------------------------------
// <copyright file="F1505Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1504Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20131118      	Manoj P           Created
//*********************************************************************************/
namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1503Controller class file
    /// </summary>
    public class F1505Controller :Controller
    {

        /// <summary>
        /// From the form F1503 workitem
        /// </summary>
        public new F1505WorkItem WorkItem
        {
            get { return base.WorkItem as F1505WorkItem; }
        }
    }
}
