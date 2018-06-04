//--------------------------------------------------------------------------------------------
// <copyright file="F2331WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2331WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20160708       priyadharshini              Created
//*********************************************************************************/

namespace D23310
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
    /// F2331WorkItem Class
    /// </summary>
    public class F2331WorkItem : WorkItem
    {
        /// <summary>
        /// Gets GetMADImportTemplateDetails
        /// </summary>
        /// <returns>DataSet</returns>

        public ListMADimportTemplateData GetMADImportTemplateDetails(string TemplateName, string Description, string FileType)
        {
            return WSHelper.GetMADImportTemplateDetails(TemplateName, Description, FileType);
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
