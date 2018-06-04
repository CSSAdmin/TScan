//--------------------------------------------------------------------------------------------
// <copyright file="F36010Controller.cs" company="Congruent">
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
// 28 Nov 06		Suganth Mani       Created
//*********************************************************************************/
namespace D36010
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpace

    /// <summary>
    /// F36010Controller
    /// </summary>
    public class F36010Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F36010WorkItem WorkItem
        {
            get { return base.WorkItem as F36010WorkItem; }
        }
    }
}
