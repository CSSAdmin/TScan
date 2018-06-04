//--------------------------------------------------------------------------------------------
// <copyright file="F2331Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the 2331MADImportTemplateSelect Form.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20160712       Priyadharshini              Created
// 
//*********************************************************************************/

namespace D23310
{
    #region Namespaces
    
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion Namespaces

    /// <summary>
    /// controller for F2331 MAD import template select
    /// </summary>
    public class F2331Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F2331WorkItem WorkItem
        {
            get { return base.WorkItem as F2331WorkItem; }
        }
    }
}
