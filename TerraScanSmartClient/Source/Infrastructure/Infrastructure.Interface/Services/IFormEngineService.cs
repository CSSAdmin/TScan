//--------------------------------------------------------------------------------------------
// <copyright file="IFormEngineService.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the IFormEngineService.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// May-20-2009      Sadha Shivudu      	Created
//*********************************************************************************/

namespace TerraScan.Infrastructure.Interface.Services
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI;

    #endregion Namespace

    /// <summary>
    /// IFormEngineService class
    /// </summary>
    public interface IFormEngineService
    {
        /// <summary>
        /// Gets the smart part.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="optionalParameter">The optional parameter.</param>
        /// <param name="parentWorkItem">The parent work item.</param>
        /// <returns>The generated smartpart.</returns>
        UserControl GetSmartPart(int formId, object[] optionalParameter, WorkItem parentWorkItem);
        
        /// <summary>
        /// Gets the form.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="optionalParameter">The optional parameter.</param>
        /// <param name="parentWorkItem">The parent work item.</param>
        /// <returns>The generated form.</returns>
        Form GetForm(int formId, object[] optionalParameter, WorkItem parentWorkItem);
    }
}
