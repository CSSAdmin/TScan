// -------------------------------------------------------------------------------------------
// <copyright file="F95101AuditTrailComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F95101AuditTrailComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//                  H.Vinayagamurthy       Created
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
    /// F95101AuditTrailComp Class File
    /// </summary>
    public static class F95101AuditTrailComp
    {
        #region F95101 Audit Configuration

        /// <summary>
        /// To List Audit Trail records
        /// </summary>
        /// <param name="form">Form No</param>
        /// <param name="keyId">Key id</param>
        /// <returns>Typed DataSet Containing the Audit Details</returns>
        public static F95101AuditTrailData F95101_ListAuditTrail(int form, int keyId)
        {
            F95101AuditTrailData auditTrailData = new F95101AuditTrailData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", form);
            ht.Add("@KeyID", keyId);
            Utility.LoadDataSet(auditTrailData.F95101ListAuditTrail, "f95101_pclst_AuditTrail", ht);
            return auditTrailData;
        }

        #endregion F95101 Audit Configuration
    }
}
