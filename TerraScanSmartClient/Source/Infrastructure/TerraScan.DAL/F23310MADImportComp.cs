// -------------------------------------------------------------------------------------------
// <copyright file="F23310MADImportComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F23310MADImportComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 20160711         priyadharshini             Created
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
    public class F23310MADImportComp
    {
        /// <summary>
        /// F28310_s the MADImport details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public static F28310MADImport F28310_MADImportDetails(int importId)
        {
            F28310MADImport ObjMAD = new F28310MADImport();
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            string[] tableNames = new string[] { ObjMAD.GetMADImportHeaderDetails.TableName, ObjMAD.GetMADImportDetails.TableName };
            Utility.LoadDataSet(ObjMAD, "f23310_pcget_MADImport", ht, tableNames);
            return ObjMAD;
        }

        #region MAD Import Template Selection Details
        /// <summary>
        /// Gets the MAD Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of MAD Import Template Details.</returns>
        public static ListMADimportTemplateData GetMADImportTemplateDetails(string TemplateName, string Description, string FileType)
        {
            ListMADimportTemplateData MADImportTemplateSelectData = new ListMADimportTemplateData();
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateName", TemplateName);
            ht.Add("@Description", Description);
            ht.Add("@FileType", FileType);
            Utility.LoadDataSet(MADImportTemplateSelectData.ListMADImportData, "f2331_pclst_MADImportTemplateSelection", ht);
            return MADImportTemplateSelectData;
        }
        #endregion

        /// <summary>
        /// F28310_s the insert MAD Import details.
        /// </summary>
        /// <param name="importId">The Import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F28310_InsertImportMADDetails(int? importId, string importXML, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@MADImportItems", importXML);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f23310_pcins_MADImport", ht);
        }

        /// <summary>
        /// F28310_s the delete MAD Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28310_DeleteMADImportDetails(int importId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f23310_pcdel_MADImport", ht);
        }

        /// <summary>
        /// F28310_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28310_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@ImportXML", importXML);
            ht.Add("@UserID", userId);
            ht.Add("@IsProcess", isProcess);
            return Utility.CustomFetchSPKeyString("f23310_pcexe_MADImport", ht);
        }

        /// <summary>
        /// F28310_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28310_ExecuteCheckForErrors(int importId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f23310_pcexe_MADImportErrorCheck", ht);
        }

        /// <summary>
        /// F28310_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28310_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            ht.Add("@IsProcess", isProcess);
            return Utility.CustomFetchSPKeyString("f23310_pcexe_MADImportCreateRecords", ht);
        }


        #region List DistrictFileType

        /// <summary>
        /// Lists the type of the District file type.
        /// </summary>
        /// <returns>The dataset containing the District File Type</returns>
        public static F28310MADImport ListDistrictType()
        {
            F28310MADImport DistrictImportData = new F28310MADImport();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(DistrictImportData.ListDistrictType, "f23310_pclst_MADistrictType", ht);
            return DistrictImportData;
        }
        #endregion

    }
}
