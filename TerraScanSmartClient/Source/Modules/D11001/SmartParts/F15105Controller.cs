//--------------------------------------------------------------------------------------------
// <copyright file="F15100Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15100.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D11001
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpaces

    /// <summary>
    /// Class F15100Controller
    /// </summary>
    public class F15105Controller:Controller
    {
        public new F15105WorkItem WorkItem
        {
            get { return base.WorkItem as F15105WorkItem; }
        }
    }
}
