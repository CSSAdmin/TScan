// -------------------------------------------------------------------------------------------
// <copyright file="F95010GetWebFormXMLComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F95010GetWebFormXML related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    #endregion Namespace

    /// <summary>
    /// Main class for F95010GetWebFormXML Component
    /// </summary>
    public static class F95010GetWebFormXMLComp
    {
        /// <summary>
        /// Gets the webformXML.
        /// </summary>        
        /// <param name="keyId">keyid</param>
        /// <param name="form">form</param>
        /// <param name="userId">UserID</param>
        /// <returns>Typed dataset</returns>
        public static F95010GetWebFormXMLData GetWebFormXML(int? keyId, int form, int userId)
        {
            F95010GetWebFormXMLData form95010getwebFormXMLData = new F95010GetWebFormXMLData();
            Hashtable ht = new Hashtable();
            ht.Add("@KeyID", keyId);
            ht.Add("@Form", form);
            ht.Add("@UserID", userId);
            string[] tableName = new string[] { form95010getwebFormXMLData.F95010GetWebFormXML.TableName, form95010getwebFormXMLData.WebFormHeight.TableName };
            Utility.LoadDataSet(form95010getwebFormXMLData, "f95010_pcget_WebFormXML", ht, tableName);
            return form95010getwebFormXMLData;
        }
    }
}
