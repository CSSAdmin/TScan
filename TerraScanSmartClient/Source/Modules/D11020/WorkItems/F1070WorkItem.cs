//--------------------------------------------------------------------------------------------
// <copyright file="F1207WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1201 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 Aug 2009      LathaMaheswari      Created
//*********************************************************************************/

namespace D11020
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// The F1070WorkItem
    /// </summary>
    public class F1070WorkItem : WorkItem
    {
        #region F95010GetWebFormXML

        /// <summary>
        /// Gets the web form XML.
        /// </summary>
        /// <param name="keyId">The key ID.</param>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>DataSet contains URL</returns>
        public F95010GetWebFormXMLData GetWebFormDetails(int? keyId, int form, int userId)
        {
            return WSHelper.GetWebFormXML(keyId, form, userId);
        }

        #endregion F95010GetWebFormXML

        #region Form Methods
        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion Form Methods
    }
}
