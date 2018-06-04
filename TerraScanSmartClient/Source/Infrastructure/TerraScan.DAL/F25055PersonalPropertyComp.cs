// -------------------------------------------------------------------------------------------
// <copyright file="F25055PersonalPropertyComp.cs" company="Congruent">
//      Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F25055PersonalPropertyComp.cs methods
// </summary>
// Release history
// ********************************************************************************************
// Date              Author              Description
// ----------       ---------          -------------------------------------------------------
// 17/07/2009        Malliga             Created
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// F25055 PersonalPropertyComp
    /// </summary>
    public static class F25055PersonalPropertyComp
    {
        #region Get Property Header Details

        /// <summary>
        /// Gets the property header details.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns>Personal property header details</returns>
        public static F25055PropertyHeaderData GetPropertyHeaderDetails(int scheduleId)
        {
            F25055PropertyHeaderData personalPropertyTable = new F25055PropertyHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@SheduleID", scheduleId);
            Utility.LoadDataSet(personalPropertyTable.GetPersonalPropertyDetail, "f25055_pcget_Schedule", ht);
            return personalPropertyTable;
        }

        #endregion Get Property Header Details

    }
}
