//--------------------------------------------------------------------------------------------
// <copyright file="F82002Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F82002Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12/04/07         A.Sriparameswari       Created                
//*********************************************************************************/

namespace D82001
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F82002Controler class file
    /// </summary>
    public class F82002Controler : Controller
    {
        /// <summary>
        /// From the form F27006 workitem
        /// </summary>
        public new F82002WorkItem WorkItem
        {
            get { return base.WorkItem as F82002WorkItem; }
        }
    }
}
