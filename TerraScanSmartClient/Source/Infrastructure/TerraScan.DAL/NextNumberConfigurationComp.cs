// -------------------------------------------------------------------------------------------
// <copyright file="NextNumberConfigurationComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update NextNumber Configuration</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 June 06		JYOTHI P	            Created
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
    /// Main class for Next Number Configuration Component
    /// </summary>
    public static class NextNumberConfigurationComp
    {
        #region List NextNumber Configuration

        /// <summary>
        /// Lists the NextNumber Configuration.
        /// </summary>
        /// <returns>NextNumber Configuration list</returns>
        public static NextNumberData ListNextNumberConfiguration()
        {
            NextNumberData nextNumberData = new NextNumberData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(nextNumberData.ListNextNumber, "f1016_pclst_NextNumber", ht);
            return nextNumberData;
        }
        #endregion

        #region Check Next Number

        /// <summary>
        /// Check For Valid Next Number
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <returns>The DataSet containg valid Next Number details</returns>
        public static DataSet CheckNextNumber(int rollYear, int nextNum, string formula)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            ht.Add("@NextNum", nextNum);
            ht.Add("@Formula", formula);
            return DataProxy.FetchDataSet("f1016_pcchk_NextNumber", ht);
        }
        #endregion

        #region Update NextNumberConfig Details

        /// <summary>
        /// Saves the next number config details.
        /// </summary>
        /// <param name="nextNumId">The next num id.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <param name="userId">userId</param>
        public static void UpdateNextNumberConfigDetails(int nextNumId, int nextNum, string formula, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NextNumID", nextNumId);
            ht.Add("@NextNum", nextNum);
            ht.Add("@Formula", formula);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f1016_pcupd_NextNumber", ht);
        }
        #endregion

    }
}
