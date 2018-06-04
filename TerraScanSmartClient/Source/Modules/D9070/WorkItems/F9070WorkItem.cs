//--------------------------------------------------------------------------------------------
// <copyright file="F9070WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9070WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//
//*********************************************************************************/


namespace D9070
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using System.Data;
    using TerraScan.BusinessEntities;

    #endregion NameSpaces

    /// <summary>
    /// F9070WorkItem class
    /// </summary>
    public class F9070WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the ReportListingDetails.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>F9070ReportListingDetails</returns>
        public F9070ReportListingData F9070GetReportListingDetails(int userId)
        {
            return WSHelper.F9070GetReportListingDetails(userId);
        }
    }
}
