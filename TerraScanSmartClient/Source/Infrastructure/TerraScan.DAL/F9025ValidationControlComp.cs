// -------------------------------------------------------------------------------------------
// <copyright file="F9025ValidationControlComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F9025ValidationControlComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		            Description
// ----------		---------		        --------------------------------------------------
// 06/01/09         A.Shanmuga Sundaram     Create
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
    /// 
    /// </summary>
    public static class F9025ValidationControlComp
    {
        #region F9025ValidationControl Selection

        #region F9025 FormValidationDetails Selection

        /// <summary>
        /// F9076s the save new comment template.
        /// </summary>
        /// <param name="formid">The formid.</param>
        /// <param name="userid">The userid.</param>
        /// <returns>int</returns>
        public static int F9025FormValidationDetails(int formid, int userid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formid);
            ht.Add("@USerID", userid);
            //return Utility.FetchSPOutput("f9025_pcget_ValidationCfg", ht);
            return Utility.FetchSPExecuteKeyId("f9025_pcget_ValidationCfg", ht);
        }        
        #endregion F9025 FormValidationDetails Selection

        #region F9025 SaveValidationDetails Selection


        /// <summary>
        /// F9025s the save validation details.
        /// </summary>
        /// <param name="formid">The formid.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="keyid">The keyid.</param>
        /// <returns>int</returns>
        public static int F9025SaveValidationDetails(int formid, int userid, int keyid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formid);
            ht.Add("@USerID", userid);
            ht.Add("@KeyID", keyid);
            return Utility.FetchSPExecuteKeyId("f9025_pcins_Validation", ht);
        }
        #endregion F9025 SaveValidationDetails Selection

        #endregion F9025ValidationControl Selection
    }
}
