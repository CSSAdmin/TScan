//--------------------------------------------------------------------------------------------
// <copyright file="F16030Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Nov 06		Suganth Mani       Created
//*********************************************************************************/
namespace D10030
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpace

    /// <summary>
    /// F1030Controller class file
    /// </summary>
    public class F16030Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F16030WorkItem WorkItem
        {
            get { return base.WorkItem as F16030WorkItem; }
        }
    }
}
