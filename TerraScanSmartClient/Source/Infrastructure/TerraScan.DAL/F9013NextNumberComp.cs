// -------------------------------------------------------------------------------------------
// <copyright file="F9013NextNumberComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F9013NextNumberComp.cs </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//  27 Feb 07       VijayaKumar.M       Added
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
    /// F9013NextNumberComp Class file
    /// </summary>
    public static class F9013NextNumberComp
    {
        #region List NextNumber Configuration

        /// <summary>
        /// Lists the NextNumber Configuration.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>NextNumber Configuration list</returns>
        public static F9013NextNumberData F9013_ListNextNumberConfiguration(int rollYear, int userId)
        {
            F9013NextNumberData nextNumberData = new F9013NextNumberData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(nextNumberData.LisNextNumberConfiguration, "f9013_pclst_NextNumberConfiguration", ht);
            return nextNumberData;
        }

        #endregion List NextNumber Configuration

        #region Check Next Number

        /// <summary>
        /// Check For Valid Next Number
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <returns>The DataSet containg valid Next Number details</returns>
        public static DataSet F9013_CheckNextNumber(int rollYear, int nextNum, string formula)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            ht.Add("@NextNumber", nextNum);
            ht.Add("@Formula", formula);
            return DataProxy.FetchDataSet("f9013_pcchk_NextNumberConfiguration", ht);
        }

        #endregion Check Next Number

        #region Update NextNumberConfig Details

        /// <summary>
        /// Saves the next number config details.
        /// </summary>
        /// <param name="nextNumId">The next num id.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <param name="userId">userId</param>
        public static void F9013_UpdateNextNumberConfigDetails(int nextNumId, int nextNum, string formula, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NextNumberID", nextNumId);
            ht.Add("@NextNumber", nextNum);
            ht.Add("@Formula", formula);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f9013_pcupd_NextNumberConfiguration", ht);
        }

        #endregion Update NextNumberConfig Details

        #region List Roll Year

        /// <summary>
        /// Lists the NextNumber RollYear.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>NextNumber RollYear list</returns>
        public static F9013NextNumberData F9013_ListNextNumberRollYear(int userId)
        {
            F9013NextNumberData nextNumberData = new F9013NextNumberData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(nextNumberData.ListNextNumberRollYear, "f9013_pclst_NextNumberRollYear", ht);
            return nextNumberData;
        }

        #endregion List Roll Year

    }
}
