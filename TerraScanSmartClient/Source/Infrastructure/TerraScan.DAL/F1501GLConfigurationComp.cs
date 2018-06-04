// -------------------------------------------------------------------------------------------
// <copyright file="F1501GLConfigurationComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access General Ledger Configuration related information</summary>
// Release history
// // Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 Nov 06		Krishna	            Created
// -------------------------------------------------------------------------------------------
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// GL Configuration Configuration class file
    /// </summary>
    public static class F1501GLConfigurationComp
    {
        #region List RollYear

        /// <summary>
        /// F1501_s the list roll year.
        /// </summary>
        /// <returns>roll year list</returns>
        public static F1501GLConfigurationData F1501_ListRollYear()
        {
            F1501GLConfigurationData glconfigDetails = new F1501GLConfigurationData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(glconfigDetails.ListGLConfigRollYear, "f1501_pclst_GLConfigRollYear", ht);
            return glconfigDetails;
        }

        #endregion

        #region List GL config Details

        /// <summary>
        /// F1501_s the list GL config details.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>glConfigDetails</returns>
        public static F1501GLConfigurationData F1501_ListGLConfigDetails(int rollYear)
        {
            F1501GLConfigurationData glconfigDetails = new F1501GLConfigurationData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            Utility.LoadDataSet(glconfigDetails.ListGLConfigDetail, "f1501_pclst_GLConfigDetail", ht);
            return glconfigDetails;
        }

        #endregion

        #region Get GL config Details

        /// <summary>
        /// F1501_s the get GL config details.
        /// </summary>
        /// <param name="glconfigId">The glconfig ID.</param>
        /// <returns>glconfigDetails</returns>
        public static F1501GLConfigurationData F1501_GetGLConfigDetails(int glconfigId)
        {
            F1501GLConfigurationData glconfigDetails = new F1501GLConfigurationData();
            Hashtable ht = new Hashtable();
            ht.Add("@GLConfigID", glconfigId);
            Utility.LoadDataSet(glconfigDetails.GetGLConfigDetail, "f1501_pcget_GLConfigDetail", ht);
            return glconfigDetails;
        }

        #endregion

        #region Save and Edit the GL config Details

        /// <summary>
        /// F1501_s the create or edit GL config details.
        /// </summary>
        /// <param name="glconfigId">The glconfig id.</param>
        /// <param name="glconfigElements">The glconfig elements.</param>
        /// <param name="userId">userId</param>
        /// <returns>errorId</returns>
        public static int F1501_CreateOrEditGLConfigDetails(int glconfigId, string glconfigElements, int userId)
        {
            Hashtable ht = new Hashtable();
            if (glconfigId != -999)
            {
                ht.Add("@GLConfigID", glconfigId);
            }
            else
            {
                ht.Add("@GLConfigID", DBNull.Value);
            }

            ht.Add("@GLConfigElements ", glconfigElements);
            ht.Add("@UserID", userId);

            int errorId;

            errorId = Utility.FetchSPExecuteKeyId("f1501_pcupd_GLConfigDetail", ht);
            return errorId;
        }

        #endregion
    }
}
