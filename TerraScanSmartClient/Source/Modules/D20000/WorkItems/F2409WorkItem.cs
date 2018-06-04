//--------------------------------------------------------------------------------------------
// <copyright file="F2010WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2010 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15/07/2009       R.Malliga         Created
//*********************************************************************************/

namespace D20000
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
    /// F2409WorkItem
    /// </summary>
    public class F2409WorkItem : WorkItem 
    {
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

        /// <summary>
        /// F2409_s the type of the reviewstatus inspection.
        /// </summary>
        /// <returns></returns>
        public F2409ReviewStatusData F2409_ReviewstatusInspectionType()
        {
            return WSHelper.F2409_ReviewstatusInspectionType();
        }

        /// <summary>
        /// F2409_s the reviewstatus inspection by user.
        /// </summary>
        /// <returns></returns>
        public F2409ReviewStatusData F2409_ReviewstatusInspectionByUser(int applicationId)
        {
            return WSHelper.F2409_ReviewstatusInspectionByUser(applicationId);
        }

        /// <summary>
        /// F2409s the update parcel review details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public F2409ReviewStatusData F2409_ListReviewstatus(int parcelId)
        {
            return WSHelper.F2409_ListReviewstatus(parcelId);
        }

        /// <summary>
        ///  F2409_s the reviewstatus 
        /// </summary>
        /// <param name="parcelId">The applicationId id.</param>
        /// <returns></returns>
        public F2409ReviewStatusData F2409_ReviewStatus()
        {
            return WSHelper.F2409_ReviewStatus();
        }

        /// <summary>
        /// F2409s the update parcel review details.
        /// </summary>
        /// <param name="reviewXML">The review XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public void  F2409UpdateParcelReviewDetails(string reviewXML, int userId)
        {
            WSHelper.F2409UpdateParcelReviewDetails(reviewXML, userId);
        }


    }
}
