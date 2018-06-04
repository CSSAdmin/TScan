

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    public class SnapshotImportComp
    {
        /// <summary>
        /// F28510_s the SnapshotImport details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public static F23510SnapshotImport F28510_SnapshotImportDetails(int importId)
        {
            F23510SnapshotImport ObjSnapshot = new F23510SnapshotImport();
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            string[] tableNames = new string[] { ObjSnapshot.GetSnapshotImportHeaderDetails.TableName, ObjSnapshot.GetSnapshotImportDetails.TableName };
            Utility.LoadDataSet(ObjSnapshot, "f23510_pcget_SnapshotImport", ht, tableNames);
            return ObjSnapshot;
        }

        #region Snapshot Import Template Selection Details
        /// <summary>
        /// Gets the Snapshot Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of Snapshot Import Template Details.</returns>
        public static ListSnapshotImportTemplateData GetSnapshotImportTemplateDetails(string TemplateName, string Description, string FileType)
        {
            ListSnapshotImportTemplateData snapshotImportTemplateSelectData = new ListSnapshotImportTemplateData();
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateName", TemplateName);
            ht.Add("@Description", Description);
            ht.Add("@FileType", FileType);
            Utility.LoadDataSet(snapshotImportTemplateSelectData.ListSnapshotImportData, "f2351_pclst_SnapshotImportTemplateSelection", ht);
            return snapshotImportTemplateSelectData;
        }
        #endregion

        /// <summary>
        /// F28510_s the insert Snapshot Import details.
        /// </summary>
        /// <param name="importId">The Import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F28510_InsertImportSnapshotDetails(int? importId, string importXML, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@SnapshotImportItems", importXML);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f23510_pcins_SnapshotImport", ht);
        }

        /// <summary>
        /// F28210_s the delete Snapshot Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28510_DeleteSnapshotImportDetails(int importId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f23510_pcdel_SnapshotImport", ht);
        }

        /// <summary>
        /// F28510_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28510_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@ImportXML", importXML);
            ht.Add("@UserID", userId);
            ht.Add("@IsProcess", isProcess);
            return Utility.CustomFetchSPKeyString("f23510_pcexe_SnapshotImport", ht);
        }

        /// <summary>
        /// F28210_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28510_ExecuteCheckForErrors(int importId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f23510_pcexe_SnapshotImportErrorCheck", ht);
        }

        /// <summary>
        /// F28510_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28510_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            ht.Add("@IsProcess", isProcess);
            return Utility.CustomFetchSPKeyString("f23510_pcexe_SnapshotImportCreateRecords", ht);
        }

        
    }
}
