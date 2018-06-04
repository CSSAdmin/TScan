//--------------------------------------------------------------------------------------------
// <copyright file="F1109WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1108.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Nov 06		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F1109WorkItem
    /// </summary>
    public class F1109WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the list roll year.
        /// </summary>
        /// <value>The list roll year.</value>
        public AffidavitManagementQueue.ListRollYearDataTable ListRollYear
        {
            get
            {
                return WSHelper.F1109_ListRollYear().ListRollYear;
            }
        }

        /// <summary>
        /// Lists the management queue.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="name">The name.</param>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="address">The address.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="treasurer">The treasurer.</param>
        /// <param name="assessor">The assessor.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Returns ManagementWorkQueue DataSet</returns>
        public AffidavitManagementQueue ListManagementQueue(string parcelNumber, string name, string receiptDate, string address, string taxCode, string treasurer, string assessor, int rollYear, string statementNumber)
        {
            return WSHelper.F1109_ListManagementQueue(parcelNumber, name, receiptDate, address, taxCode, treasurer, assessor, rollYear, statementNumber);
        }

        /// <summary>
        /// Managements the queue filter result.
        /// </summary>
        /// <param name="statusFilterId">The status filter id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="filterFromDate">The filter from date.</param>
        /// <param name="filterToDate">The filter to date.</param>
        /// <returns>
        /// Returns ManagementWorkQueue Filter Result
        /// </returns>
        public AffidavitManagementQueue ManagementQueueFilterResult(int statusFilterId, int rollYear, string filterFromDate, string filterToDate)
        {
            return WSHelper.F1109_ManagementQueueFilterResult(statusFilterId, rollYear, filterFromDate, filterToDate);
        }

        /// <summary>
        /// Filters the search affidavit.
        /// </summary>
        /// <param name="filterXml">The filter XML.</param>
        /// <returns>Datset of filter records</returns>
        public AffidavitManagementQueue FilterSearchAffidavit(string filterXml)
        {
            return WSHelper.F1109_FilterSearchAffidavit(filterXml);
        }

        /// <summary>
        /// Fires the <see cref="E:Microsoft.Practices.CompositeUI.WorkItem.RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="M:Microsoft.Practices.CompositeUI.WorkItem.Run"/>
        /// method is called on the <see cref="T:Microsoft.Practices.CompositeUI.WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="E:Microsoft.Practices.CompositeUI.WorkItem.Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
