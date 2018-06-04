//--------------------------------------------------------------------------------------------
// <copyright file="D9100ModuleInit.cs" company="Congruent">
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
// 19 July 06		KARTHIKEYAN V	    Created
//*********************************************************************************/
[assembly: System.CLSCompliant(false)]

namespace D9100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Services;

    /// <summary>
    /// D9100ModuleInit class file
    /// </summary>
    public class D9100ModuleInit : ModuleInit
    {
        /* /// <summary>
        /// rootWorkItem
        /// </summary>
        private WorkItem rootWorkItem;

        /// <summary>
        /// Sets the root work item.
        /// </summary>
        /// <value>The root work item.</value>
        [ServiceDependency]
        public WorkItem RootWorkItem
        {
            set { this.rootWorkItem = value; }
        } */

        /// <summary>
        /// See <see cref="IModule.Load"/> for more information.
        /// </summary>
        public override void Load()
        {
            base.Load();
        }
    }
}
