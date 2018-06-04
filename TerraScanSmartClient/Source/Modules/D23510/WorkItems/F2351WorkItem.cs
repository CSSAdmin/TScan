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

namespace D23510
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
    /// F2351WorkItem Class
    /// </summary>
    public class F2351WorkItem : WorkItem
    {
        /// <summary>
        /// Gets GetsnapshotImportTemplateDetails
        /// </summary>
        /// <returns>DataSet</returns>

        public ListSnapshotImportTemplateData GetSnapshotImportTemplateDetails(string TemplateName, string Description, string FileType)
        {
            return WSHelper.GetSnapshotImportTemplateDetails(TemplateName,Description,FileType);
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
