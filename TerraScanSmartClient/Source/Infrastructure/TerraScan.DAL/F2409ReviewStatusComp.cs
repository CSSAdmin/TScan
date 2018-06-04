// -------------------------------------------------------------------------------------------
// <copyright file="F2409ReviewStatusComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F2409ReviewStatusComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 15/07/2009         Malliga             Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F2409ReviewStatus Class File.
    /// </summary>
    public static class F2409ReviewStatusComp
    {

        /// <summary>
        /// F2409_s the type of the reviewstatus inspection.
        /// </summary>
        /// <returns></returns>
        public static F2409ReviewStatusData F2409_ReviewstatusInspectionType()
        {
            F2409ReviewStatusData inspectionType = new F2409ReviewStatusData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(inspectionType.F2409_InspectedType, "f2409_pclst_InspectedType", ht);
            return inspectionType;
        }


        /// <summary>
        /// Datat set of Review satatus Data
        /// </summary>
        /// <returns>Datat set of Review satatus Data</returns>
        public static F2409ReviewStatusData F2409_Reviewstatus()
        {
            F2409ReviewStatusData reviewStatus = new F2409ReviewStatusData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(reviewStatus.F2409_ReviewStatus, "f2409_pclst_ReviewStatus", ht);
            return reviewStatus;
        }
        /// <summary>
        /// F2409_s the reviewstatus inspection by user.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns></returns>
        public static F2409ReviewStatusData F2409_ReviewstatusInspectionByUser(int applicationId)
        {
            F2409ReviewStatusData inspectionByUser = new F2409ReviewStatusData();
            Hashtable ht = new Hashtable();
            ht.Add("@ApplicationID", applicationId);
            Utility.LoadDataSet(inspectionByUser.F2409_ListInspectedByUser, "f2409_pclst_InspectedByUser", ht);
            return inspectionByUser;
        }

        /// <summary>
        /// F2409_s the list reviewstatus.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public static F2409ReviewStatusData F2409_ListReviewstatus(int parcelId)
        {
            F2409ReviewStatusData reviewStatus = new F2409ReviewStatusData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            Utility.LoadDataSet(reviewStatus.F2409_ListReviewStatus, "f2409_pcget_ReviewStatus", ht);
            return reviewStatus;
        }

        /// <summary>
        /// Updates the parcel review details.
        /// </summary>
        /// <param name="reviewXML">The review XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static void F2409UpdateParcelReviewDetails(string reviewXML, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ReviewXML", reviewXML);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f2409_pcupd_ReviewStatus", ht);
        }

    }
}
