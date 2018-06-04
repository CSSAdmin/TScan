// -------------------------------------------------------------------------------------------
// <copyright file="F9065FieldUseComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F9065FieldUseComp</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14 Nov 07		karthikeyan V	            Created
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
    /// F9065FieldUseComp
    /// </summary>
    public static class F9065FieldUseComp
    {

        /// <summary>
        /// F9065_s the get snapshot detail.
        /// </summary>
        /// <returns>F9065FieldUseData</returns>
        public static F9065FieldUseData F9065_GetSnapshotDetail()
        {
            F9065FieldUseData fieldUseData = new F9065FieldUseData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(fieldUseData.ListSnapshotTable, "f9065_pcget_Snapshot", ht);
            return fieldUseData;
        }

        /// <summary>
        /// F9065_s the update application status.
        /// </summary>
        /// <param name="ischeckedout">if set to <c>true</c> [ischeckedout].</param>
        /// <param name="isonline">if set to <c>true</c> [isonline].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value</returns>
        public static int F9065_UpdateApplicationStatus(bool ischeckedout, bool isonline, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@IsCheckedOut", ischeckedout);
            ht.Add("@IsOnline", isonline);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f9065_pcupd_ChkOutStatus", ht);
        }

        /// <summary>
        /// Get Audit Count
        /// </summary>
        /// <returns>Integer</returns>
        public static int F9065_GetAuditCount()
        {
            Hashtable ht = new Hashtable();
            return Utility.FetchSPExecuteKeyId("f9066_pcget_AuditCount", ht);
        }

        /// <summary>
        /// Delete the values
        /// </summary>
        /// <returns>Integer value</returns>
        public static int F9065_DeleteCheckOutTable()
        {
            Hashtable ht = new Hashtable();
            return Utility.FetchSPExecuteKeyId("f9065_pcdel_ChkOutTables", ht);
        }

        /// <summary>
        /// F9065_s the insert field element.
        /// </summary>
        /// <param name="fieldElement">The field element.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public static int F9065_InsertFieldElement(string fieldElement, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FieldElements", fieldElement);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f9065_pcupd_ChkOutParcels", ht);
        }

        /// <summary>
        /// F9065_s the preview detail.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetail">The snap shot detail.</param>
        /// <returns>F9065FieldUseData</returns>
        public static F9065FieldUseData F9065_GetPreviewDetail(int snapShotId, string snapShotDetail)
        {
            F9065FieldUseData fieldUseData = new F9065FieldUseData();
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotDetail);
            Utility.LoadDataSet(fieldUseData.ListPreviewDetailTable, "f9065_pcget_PreviewCount", ht);
            return fieldUseData;
        }

        /// <summary>
        /// F9065_s the insert application configuration.
        /// </summary>
        /// <param name="configXml">The config.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public static int F9065_InsertApplicationConfiguration(string configXml, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AppConfigItems", configXml);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f9065_pcins_ApplicationConfiguration", ht);
        }

        #region F9065_GetcfgConfiguration

        /// <summary>
        /// To get Configruation Value
        /// </summary>
        /// <param name="cfgname">The cfgname.</param>
        /// <returns>
        /// Typed dataset containing the Configruation Value.
        /// </returns>
        public static F9065FieldUseData F9065_GetcfgConfiguration(string cfgname)
        {
            F9065FieldUseData fieldUseData = new F9065FieldUseData();
            Hashtable ht = new Hashtable();
            ht.Add("@CfgName", cfgname);
            Utility.LoadDataSet(fieldUseData.ListCfgConfigTable, "f9020_pcget_Configuration", ht);
            return fieldUseData;
        }

        #endregion F9065_GetcfgConfiguration
    }
}
