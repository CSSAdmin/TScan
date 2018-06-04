//--------------------------------------------------------------------------------------------
// <copyright file="F2321WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the GetPermitImportTemplateDetails.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 27 may 2016        priyadharshini              Created
//*********************************************************************************/

namespace D23210
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using System.Data;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    #endregion Namespaces

    /// <summary>
    /// F2321WorkItem Class
    /// </summary>
    public class F2321WorkItem : WorkItem
    {
        /// <summary>
        /// Gets GetPermitImportTemplateDetails
        /// </summary>
        /// <returns>DataSet</returns>

        public ListPermitImportTemplateData GetPermitImportTemplateDetails(string TemplateName, string Description, string FileType)
        {
            return WSHelper.GetPermitImportTemplateDetails(TemplateName,Description,FileType);
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }
    }
}
