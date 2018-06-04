// -------------------------------------------------------------------------------------------
// <copyright file="F9510WebFormXMLComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F9510WebFormXML related information</summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 16/12/08          LathaMaheswari.D    Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    #endregion Namespace

    /// <summary>
    /// Main class for F9510WebFormXML Component
    /// </summary>
    public static class F9510WebFormXMLComp
    {
        /// <summary>
        /// Gets the webformXML.
        /// </summary>        
        /// <param name="form">form</param>
        /// <param name="userId">UserID</param>
        /// <returns>Typed dataset</returns>
        public static F95010GetWebFormXMLData GetWebFormXML(int form, int userId)
        {
            F95010GetWebFormXMLData form9510getwebFormXMLData = new F95010GetWebFormXMLData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", form);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(form9510getwebFormXMLData, "f9510_pcget_WebFormURL", ht, new string[] { form9510getwebFormXMLData.F95010GetWebFormXML.TableName });
            return form9510getwebFormXMLData;
        }
    }
}
