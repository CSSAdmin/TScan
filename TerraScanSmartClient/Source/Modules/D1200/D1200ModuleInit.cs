//--------------------------------------------------------------------------------------------
// <copyright file="D1200ModuleInit.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the ModuleLoad.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 05-09-2006       Krishna Abburi        Created
//*********************************************************************************/

[assembly: System.CLSCompliant(false)]
namespace D1200
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// D1200ModuleInit class file
    /// </summary>
    public class D1200ModuleInit : ModuleInit
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
