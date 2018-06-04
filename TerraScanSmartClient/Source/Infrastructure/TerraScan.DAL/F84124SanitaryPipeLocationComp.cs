// -------------------------------------------------------------------------------------------
// <copyright file="F84124SanitaryPipeLocationComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F84124SanitaryPipeLocationComp</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
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
    /// F84124SanitaryPipeLocationComp Class File
    /// </summary>
    public static class F84124SanitaryPipeLocationComp
    {
        #region F84124 Sanitary Pipe Location

        #region Get Sanitary Pipe Location

        /// <summary>
        ///  To Load F84124 Sanitary pipe Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>Typed DataSet Containing All the Sanitary Pipe properties Details</returns>
        public static F84124SanitaryPipeLocationData F84124_GetSanitaryPipeLocation(int keyId, int formId)
        {
            F84124SanitaryPipeLocationData sanitaryPipeLocationData = new F84124SanitaryPipeLocationData();
            Hashtable ht = new Hashtable();
            ht.Add("@PipeID", keyId);
            ht.Add("@FormID", formId);
            Utility.LoadDataSet(sanitaryPipeLocationData.GetSanitaryPipeLocation, "f84124_pcget_FS_SanitaryPipeLocation", ht);
            return sanitaryPipeLocationData;
        }

        #endregion Get Sanitary Pipe Location

        #region Save Sanitary Pipe Location

        /// <summary>
        /// To Save F84123 Sanitary Pipe Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="sanitayPipeLocation">The Sanitary Pipelocation.</param>
        ///<param name="userId">userId</param>
        /// <returns>The integer value containing pipe id</returns>
        public static int F84124_SaveSanitaryPipeLocation(int keyId, string sanitayPipeLocation, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@PipeID", keyId);
            ht.Add("@SanitaryPipeLoc", sanitayPipeLocation);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f84124_pcupd_FS_SanitaryPipeLocation", ht);
        }

        #endregion Save Sanitary pipe Location

        #endregion F84124 Sanitary Pipe Location
    }
}