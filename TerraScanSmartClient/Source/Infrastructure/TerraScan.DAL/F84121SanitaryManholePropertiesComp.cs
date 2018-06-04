// -------------------------------------------------------------------------------------------
// <copyright file="F84121SanitaryManholePropertiesComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access GDoc methods to Load Common ComboBoxs </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26/12/2006       Karthikeyan.B       Added
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
    /// F84121SanitaryManholePropertiesComp Class File
    /// </summary>
    public static class F84121SanitaryManholePropertiesComp
    {
        #region F84121 Sanitary Manhole Properties

        #region Get Sanitary Manhole Properties

        /// <summary>
        ///  To Load F84121 Sanitary Manhole properties.
        /// </summary>
        /// <param name="manholeId">The Manhole ID.</param>
        /// <returns>Typed DataSet Containing All the Sanitary Manhole properties Details</returns>
        public static F84121SanitaryManholePropertiesData F84121_GetSanitaryManholeProperties(int manholeId)
        {
            F84121SanitaryManholePropertiesData sanitaryManholePropertiesData = new F84121SanitaryManholePropertiesData();
            Hashtable ht = new Hashtable();
            ht.Add("@ManholeID", manholeId);
            Utility.LoadDataSet(sanitaryManholePropertiesData.GetSanitaryManholeProperties, "f84121_pcget_FS_SanitaryManholeProperty", ht);
            return sanitaryManholePropertiesData;
        }

        #endregion

        #region Save Sanitary Manhole Properties

        /// <summary>
        /// To Save F84121 Sanitary Manhole properties.
        /// </summary>
        /// <param name="manholeId">The Manhole ID.</param>
        /// <param name="sanitaryManholeProperties">The XML string Containing All values in Sanitary Manhole properties.</param>
        /// <param name="userId">userId</param>
        /// <returns>The integer value containing manhole id</returns>
        public static int F84121_SaveSanitaryManholeProperties(int manholeId, string sanitaryManholeProperties, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ManholeID", manholeId);
            ht.Add("@SanitaryProperty", sanitaryManholeProperties);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f84121_pcins_FS_SanitaryManholeProperty", ht);
        }

        #endregion

        #region Delete Sanitary Manhole Properties

        /// <summary>
        /// To Delete F84121 Sanitary Manhole properties
        /// </summary>
        /// <param name="manholeId">The Manhole Id</param>
        /// <param name="userId">userId</param>
        public static void F84121_DeleteSanitaryManholeProperties(int manholeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ManholeID", manholeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f84121_pcdel_FS_SanitaryManholeProperty", ht);
        }

        #endregion

        #endregion
    }
}
