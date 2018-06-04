// -------------------------------------------------------------------------------------------
// <copyright file="ExciseDistrictCopyComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Statement</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 July 06		VIJAYAKUMAR M	   Created
// 
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
    /// Main class for Excise District Copy Component
    /// </summary>
    public static class ExciseDistrictCopyComp
    {
        #region Get Excise District Copy 

        /// <summary>
        /// Get the district and roll year details
        /// </summary>
        /// <param name="exciserateId">exciserateId</param>
        /// <returns>typed dataset with district and roll year</returns>
        public static ExciseDistrictCopyData GetExciseDistrictCopy(int exciserateId)
        {
            ExciseDistrictCopyData exciseDistrictCopyData = new ExciseDistrictCopyData();
            Hashtable ht = new Hashtable();
            ht.Add("@ExciseRateID", exciserateId);
            Utility.LoadDataSet(exciseDistrictCopyData.GetExciseRateDistrict, "f1104_pcget_ExciseRateDistrict", ht);
            return exciseDistrictCopyData;
        }

        #endregion Get Excise District Copy 

        #region Save Excise District Copy
        
        /// <summary>
        /// Save the district,base year(roll year) and new district year
        /// The returns values from Database 
        /// when 0 = The record is successfully saved.
        /// when 1 = Invalid Source Record
        /// when 2 = Invalid Destination Record
        /// </summary>
        /// <param name="district">The District</param>
        /// <param name="basedOnYear">The roll year</param>
        /// <param name="newDistrictYear">the new district year</param>
        /// <param name="userId">The userId.</param>
        /// <returns>
        /// The returns values from Database 
        /// when 0 = The record is successfully saved.
        /// when 1 = Invalid Source Record
        /// when 2 = Invalid Destination Record
        /// </returns>
        public static int SaveExciseDistrictCopy(int district, int basedOnYear, int newDistrictYear, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@District", district);
            ht.Add("@BasedOnYear", basedOnYear);
            ht.Add("@NewDistrictYear", newDistrictYear);
            ht.Add("@UserID", userId);
            ////return DataProxy.FetchSPOutput("f1104_pcins_ExciseRateDistrict ", ht);
            return Utility.FetchSPOutput("f1104_pcins_ExciseRateDistrict ", ht);
        } 
        
        #endregion Save Excise District Copy
    }
}
