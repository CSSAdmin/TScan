//--------------------------------------------------------------------------------------------
// <copyright file="F9008WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9008WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 23 Jan 07        KUPPUSAMY.B         created
//*********************************************************************************/

namespace D9000
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
    /// F9008WorkItem class
    /// </summary>
    public class F9008WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the report details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>F9008GetReportDetails</returns>
        public F9008ReportDetailsData F9008GetReportDetails(int userId)
        {
            return WSHelper.F9008GetReportDetails(userId);
        }

        #region SaveReportDetails

        /// <summary>
        /// To Save Report Details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="printerConf">The printer conf.</param>
        public void F9008_SaveReportDetails(int userId, string printerConf)
        {
            WSHelper.F9008_SaveReportDetails(userId, printerConf);
        }

        #endregion SaveReportDetails
    }
}
