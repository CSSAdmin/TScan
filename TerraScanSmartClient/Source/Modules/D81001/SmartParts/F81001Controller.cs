//--------------------------------------------------------------------------------------------
// <copyright file="F81001Controller.cs" company="Congruent">
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
// 07/11/2007       D.Latha Maheswari    Created
//*********************************************************************************/
namespace D81001
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpace

    /// <summary>
    /// F81001Controller
    /// </summary>
    public class F81001Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F81001WorkItem WorkItem
        {
            get { return base.WorkItem as F81001WorkItem; }
        }
    }
}
