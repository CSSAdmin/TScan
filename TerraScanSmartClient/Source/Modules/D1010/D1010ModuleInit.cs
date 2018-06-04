//--------------------------------------------------------------------------------------------
// <copyright file="D1010ModuleInit.cs" company="Congruent">
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
// 31 Jul 06        Suganth Mani       Created
//*********************************************************************************/

[assembly: System.CLSCompliant(false)]

namespace D1010
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Services;

    #endregion Namespaces

    /// <summary>
    /// Mortgage module
    /// </summary>
    public class D1010ModuleInit : ModuleInit 
    {
        /// <summary>
        /// See <see cref="IModule.Load"/> for more information.
        /// </summary>
        public override void Load()
        {
            base.Load();
        }
    }
}
