//--------------------------------------------------------------------------------------------
// <copyright file="F1405Controller.cs" company="Congruent">
// Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1405Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//*******************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//  02/11/10        P.Manoj kumar           Created
//**************************************************************************************/


namespace D2000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
 
    public class F1405Controller : Controller
    {
        /// <summary>
        /// From the form F1405 workitem
        /// </summary>
        public new F1405WorkItem WorkItem 
        {
            get { return base.WorkItem as F1405WorkItem;}
        }
    }
}
