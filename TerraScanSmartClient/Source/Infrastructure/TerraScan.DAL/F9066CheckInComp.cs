// -------------------------------------------------------------------------------------------
// <copyright file="F9066CheckInComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F9065FieldUseComp</summary>
// Release history
//**********************************************************************************
// Date			    Author		        Description
// ----------		---------		    ---------------------------------------------------------
// 11 Dec 11		D.LathaMaheswari    Created
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
    /// F9066CheckInComp
    /// </summary>
    public static class F9066CheckInComp
    {
        #region GetAuditCount
        ///// <summary>
        ///// Get Audit Count
        ///// </summary>
        ///// <returns>Integer</returns>
        //public static int F9066_GetAuditCount()
        //{
        //    Hashtable ht = new Hashtable();
        //    return Utility.FetchSPExecuteKeyId("f9066_pcget_AuditCount", ht);
        //}
        #endregion GetAuditCount

        #region GetCheckInXML
        ///// <summary>
        ///// Get Check In Details
        ///// </summary>
        ///// <returns>DataSet</returns>
        //public static F9066CheckInData F9066_GetCheckInData()
        //{
        //    F9066CheckInData getCheckInData = new F9066CheckInData();
        //    Hashtable ht = new Hashtable();
        //    Utility.FillDataSet(getCheckInData.GetCheckInDetails, "f9066_pcget_Xml", ht);
        //    return getCheckInData;
        //}
        #endregion GetCheckInXML

        #region SaveXML
        /// <summary>
        /// Save the values
        /// </summary>
        /// <param name="insertValue">insertValue</param>
        /// <param name="updateValue">updateValue</param>
        public static void F9066_SaveData(string insertValue, string updateValue)
        {
            Hashtable ht = new Hashtable();
            if (string.IsNullOrEmpty(insertValue))
            {
                ht.Add("@InsertXML", DBNull.Value);
            }
            else
            {
                ht.Add("@InsertXML", insertValue);
            }

            if (string.IsNullOrEmpty(updateValue))
            {
                ht.Add("@UpdateXML", DBNull.Value);
            }
            else
            {
                ht.Add("@UpdateXML", updateValue);
            }

            Utility.ImplementProcedure("f9066_pcins_Xml", ht);
        }
        #endregion SaveXML

        #region DeleteData
        /// <summary>
        /// Delete the values
        /// </summary>
        //public static int F9066_DeleteData()
        //{
        //    Hashtable ht = new Hashtable();
        //    return Utility.FetchSPExecuteKeyId("f9066_pcdel_AuditDetails", ht);
        //}
        #endregion DeleteData
    }
}
