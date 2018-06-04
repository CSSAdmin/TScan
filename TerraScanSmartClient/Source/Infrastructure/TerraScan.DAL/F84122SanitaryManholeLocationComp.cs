// -------------------------------------------------------------------------------------------
// <copyright file="F84122SanitaryManholeLocationComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access GDoc Sanitary Manhole Location Methods</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28/12/2006       Karthikeyan.B       Added
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
    /// F84122SanitaryManholeLocationComp Class file
    /// </summary>
    public static class F84122SanitaryManholeLocationComp
    {
        #region F84122 Sanitary Manhole Location

        #region Get Sanitary Manhole Location

        /// <summary>
        /// To Load F84122 Sanitary Manhole Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Sanitary Manhole Loaction Details
        /// </returns>
        public static F84122SanitaryManholeLocationData F84122_GetSanitaryManholeLocation(int keyId)
        {
            F84122SanitaryManholeLocationData sanitaryManholeLocationData = new F84122SanitaryManholeLocationData();
            Hashtable ht = new Hashtable();
            ht.Add("@ManholeID", keyId);
            Utility.LoadDataSet(sanitaryManholeLocationData.GetSanitaryManholeLocation, "f84122_pcget_FS_SanitaryManholeLocation", ht);
            return sanitaryManholeLocationData;
        }

        #endregion

        #region Save Sanitary Manhole Location

        /// <summary>
        /// To Save F84122 Sanitary Manhole Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="sanitaryManholeLocation">The Sanitary Manhole location.</param>
        /// <param name="userId">userId</param>
        /// <returns>The integer value containing key id</returns>        
        public static int F84122_SaveSanitaryManholeLocation(int keyId, string sanitaryManholeLocation, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ManholeID", keyId);
            ht.Add("@SanitaryLocation", sanitaryManholeLocation);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f84122_pcupd_FS_SanitaryManholeLocation", ht);
        }

        #endregion

        #endregion
    }
}
