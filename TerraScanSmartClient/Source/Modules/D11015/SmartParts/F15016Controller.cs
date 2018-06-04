//--------------------------------------------------------------------------------------------
// <copyright file="F27006Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F27006Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28/03/07         M.Vijayakumar       Created                
//*********************************************************************************/


namespace D11015
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    /// <summary>
    /// F15016Controller Class file
    /// </summary>
    public class F15016Controller: Controller
    {
        /// <summary>
        /// From the form F15016WorkItem
        /// </summary>
        public new F15016WorkItem WorkItem
        {
            get { return base.WorkItem as F15016WorkItem; }
        }
    }
}
