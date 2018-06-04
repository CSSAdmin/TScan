// -------------------------------------------------------------------------------------------------
// <copyright file="F36010WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D24600
{
    #region NameSpace

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

    #endregion NameSpace

    public class F29600WorkItem : WorkItem
    {
        #region SeniorExempt

        /// <summary>
        /// F29600_GetSeniorExemptionDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>DataSet</returns>
        public F29600SeniorExemptData F29600_GetSeniorExemptionDetails(int eventId,int userId)
        {
            return WSHelper.F29600_GetSeniorExemptionDetails(eventId, userId);
        }

        /// <summary>
        /// F29600_GetSeniorExemptionCode
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>DataSet</returns>
        public F29600SeniorExemptData F29600_GetSeniorExemptionCode(string effectiveDate)
        {
            return WSHelper.F29600_GetSeniorExemptionCode(effectiveDate);
        }

        public int F29600_saveSeniorExemptionDetails(int eventId, string seniorExemptDetails, int userId)
        {
            return WSHelper.F29600_saveSeniorExemptionDetails(eventId, seniorExemptDetails, userId);
        }

        #endregion SeniorExempt

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetOwnerDetails(int ownerId)
        {
            return WSHelper.F15010_GetOwnerDetails(ownerId);
        }

        #region WorkItemEvents

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

        #endregion WorkItemEvents
    }
}
