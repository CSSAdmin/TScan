//--------------------------------------------------------------------------
// <copyright file="F36033Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36033 Land Code Value.
// </summary>
//--------------------------------------------------------------------------
// Change History
//**************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------
// 14/09/2007       M.Vijayakumar       Created
//                  
//**************************************************************************

namespace D24530
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

   public  class F29530Controller: Controller
   {
       /// <summary>
       /// From the form F36033 workitem
       /// </summary>
       public new F29530WorkItem WorkItem
       {
           get { return base.WorkItem as F29530WorkItem; }
       }
   }
}
