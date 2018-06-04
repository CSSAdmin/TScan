// -------------------------------------------------------------------------------------------
// <copyright file="ExciseRateDistrictSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update NextNumber Configuration</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 July 06		JYOTHI P	            Created
// 25 July 06       JYOTHI P                Added GetExciseTaxReceipt method
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
    /// Main class for Excise Rate District Selection Component
    /// </summary>
    public static class ExciseRateDistrictSelectionComp
    {
        #region Get

        /// <summary>
        /// Lists the excise rate district.
        /// </summary>
        /// <param name="district">The district.</param>
        /// <param name="year">The year.</param>
        /// <param name="description">The description.</param>
        /// <returns>The typed dataset containing the ExciseTaxReceipt information based on statementId</returns>
        public static ExciseRateDistrictSelectionData ListExciseRateDistrict(string district, int year, string description)
        {
            ExciseRateDistrictSelectionData exciseRateDistrictSelectionData = new ExciseRateDistrictSelectionData();
            Hashtable ht = new Hashtable();
            ////if (district != -999)
            ////{
            ht.Add("@District", district);
            ////}

            if (year != -999)
            {
                ht.Add("@Year", year);
            }

            if (!string.IsNullOrEmpty(description))
            {
                ht.Add("@Description", description);
            }

            Utility.LoadDataSet(exciseRateDistrictSelectionData.ListExciseRateDistrict, "f1102_pclst_ExciseRateDistrict", ht);
            return exciseRateDistrictSelectionData;
        }
        #endregion
    }
}
