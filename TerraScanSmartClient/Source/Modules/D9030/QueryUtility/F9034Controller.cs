//--------------------------------------------------------------------------------------------
// <copyright file="F9033Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 Dec 06       Guhan                Created
//*********************************************************************************/


namespace D9030
{

    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpaces
    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
  public class F9034Controller  : Controller
    {

        /// <summary>
        /// Created Property for F90320WorkItem
        /// </summary>
        public new F9034WorkItem WorkItem
        {
            get { return base.WorkItem as F9034WorkItem; }
        }
    }
}
