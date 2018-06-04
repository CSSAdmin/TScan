//--------------------------------------------------------------------------------------------
// <copyright file="F1407Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1407Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 2014-12-15      	Purushotham A          Created
//*********************************************************************************/
namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

   public class F1407Controller :Controller
    {

        /// <summary>
        /// From the form F1503 workitem
        /// </summary>
        public new F1407WorkItem WorkItem
        {
            get { return base.WorkItem as F1407WorkItem; }
        }
    }
}
