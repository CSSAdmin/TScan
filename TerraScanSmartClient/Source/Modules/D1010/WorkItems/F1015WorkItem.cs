//--------------------------------------------------------------------------------------------
// <copyright file="F1015WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the GetMortgageImportTemplateDetails.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 Aug 06        Vinoth              Created
//*********************************************************************************/

namespace D1010
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
    /// F1015WorkItem Class
    /// </summary>
    public class F1015WorkItem : WorkItem
    {
        /// <summary>
        /// Gets Mortgage Import Template Details
        /// </summary>
        /// <returns>DataSet</returns>
        public MortgageImportTemplateSelectData GetMortgageImportTemplateDetails
        {
            get { return WSHelper.GetMortgageImportTemplateDetails(); }
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
