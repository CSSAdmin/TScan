// -------------------------------------------------------------------------------------------
// <copyright file="F23210PermitImportComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F23210PermitImportComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 26/05/2016         priyadharshini             Created
// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using TerraScan.DataLayer;
using System.Collections;
using TerraScan.BusinessEntities;

namespace TerraScan.Dal
{
    public static class F23210PermitImportComp
    {

        /// <summary>
        /// F28210_s the PermitImport details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public static F28210PermitImport F28210_PermitImportDetails(int importId)
        {
            F28210PermitImport ObjPermit = new F28210PermitImport();
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            string[] tableNames = new string[] { ObjPermit.GetPermitImportHeaderDetails.TableName, ObjPermit.GetPermitImportDetails.TableName };
            Utility.LoadDataSet(ObjPermit, "f23210_pcget_PermitImport", ht, tableNames);
            return ObjPermit;
        }

        #region Permit Import Template Selection Details
        /// <summary>
        /// Gets the Permit Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of Permit Import Template Details.</returns>
        public static ListPermitImportTemplateData GetPermitImportTemplateDetails(string TemplateName, string Description, string FileType)
        {
            ListPermitImportTemplateData permitImportTemplateSelectData = new ListPermitImportTemplateData();
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateName", TemplateName);
            ht.Add("@Description", Description);
            ht.Add("@FileType", FileType);
            Utility.LoadDataSet(permitImportTemplateSelectData.ListPermitImportData, "f2321_pclst_PermitImportTemplateSelection", ht);
            return permitImportTemplateSelectData;
        }
        #endregion

        /// <summary>
        /// F28210_s the insert Permit Import details.
        /// </summary>
        /// <param name="importId">The Import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F28210_InsertImportPermitDetails(int? importId, string importXML, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@PermitImportItems", importXML);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f23210_pcins_PermitImport", ht);
        }

        /// <summary>
        /// F28210_s the delete Permit Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28210_DeletePermitImportDetails(int importId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f23210_pcdel_PermitImport", ht);
        }

        /// <summary>
        /// F28210_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28210_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@ImportXML", importXML);
            ht.Add("@UserID", userId);
            ht.Add("@IsProcess", isProcess);
            return Utility.CustomFetchSPKeyString("f23210_pcexe_PermitImport", ht);
        }

        /// <summary>
        /// F28210_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28210_ExecuteCheckForErrors(int importId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f23210_pcexe_PermitImportErrorCheck", ht);
        }

        /// <summary>
        /// F28210_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28210_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            ht.Add("@IsProcess", isProcess);
            return Utility.CustomFetchSPKeyString("f23210_pcexe_PermitImportCreateRecords", ht);
        }

    }
}
