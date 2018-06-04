//--------------------------------------------------------------------------------------------
// <copyright file="F9070Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the workitem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/


namespace D9070
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using System.Windows.Forms;

    /// <summary>
    /// F9070Controller Class
    /// </summary>
    public class F9070Controller : Controller
    {
        public new F9070WorkItem WorkItem
        {
            get { return base.WorkItem as F9070WorkItem; }
        }
    }
}