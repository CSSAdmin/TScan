//--------------------------------------------------------------------------------------------
// <copyright file="F1015Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the 1015MortgageImportTemplateSelect Form.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 Aug 06        Vinoth              Created
// 
//*********************************************************************************/

namespace D1010
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion Namespaces

    /// <summary>
    /// controller for F1015 mortgage import template select
    /// </summary>
    public class F1015Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F1015WorkItem WorkItem
        {
            get { return base.WorkItem as F1015WorkItem; }
        }
    }
}
