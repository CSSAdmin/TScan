
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
    /// F35085CentrallyAssessedImportComp
    /// </summary>
    public static class F35085CentrallyAssessedImportComp
    {
        /// <summary>
        /// F35085_s the central assessed owner details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public static F35085CentrallyAssessedImportData F35085_CentralAssessedImportDetails(int importId)
        {
            F35085CentrallyAssessedImportData ObjCentralOwner = new F35085CentrallyAssessedImportData();
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            string[] tableNames = new string[] { ObjCentralOwner.F35085_GetCAImportDataTable.TableName, ObjCentralOwner.F35085_Get_GridDataTable.TableName };
            Utility.LoadDataSet(ObjCentralOwner, "f35085_pcget_CAImport", ht, tableNames);
            return ObjCentralOwner;
        }



        /// <summary>
        /// F35085_s the import type combo.
        /// </summary>
        /// <returns></returns>
        public static F35085CentrallyAssessedImportData F35085_ImportTypeCombo()
        {
            F35085CentrallyAssessedImportData OwnerObj = new F35085CentrallyAssessedImportData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(OwnerObj.F35085_ListComboImportTypes, "f35085_pclst_CAImportTypes", ht);
            return OwnerObj;
        }


        /// <summary>
        /// F35085_s the deletetemplate details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F35085_DeletetemplateDetails(int importId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f35080_pcdel_CAImport", ht);
        }

        /// <summary>
        /// F35085_s the insert central template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static DataSet F35085_InsertCentralTemplateDetails(int? importId, string importXML, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@ImportXML", importXML);
            ht.Add("@UserID", userId);
            DataSet HeaderDataSet = new DataSet();
            DataTable resultTable = new DataTable();
            HeaderDataSet.Tables.Add(resultTable);
            Utility.FetchSPOuputParameters(HeaderDataSet.Tables[0],"f35085_pcins_CAImport", ht);
            return HeaderDataSet;
        }

        /// <summary>
        /// F35085_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F35085_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@ImportXML", importXML);
            ht.Add("@UserID", userId);
            ht.Add("@IsProcess", isProcess);
            return Utility.CustomFetchSPKeyString("f35085_pcexe_CAImport", ht);
            //return ImportDataSet;
        }

        /// <summary>
        /// F35085_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F35085_ExecuteCheckForErrors(int importId,int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
             DataProxy.ExecuteSP("f35085_pcexe_CAImportErrorCheck", ht);
        }
        /// <summary>
        /// F35085_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F35085_CreateImportRecords(int importId,int userId, bool isProcess)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ImportID", importId);
            ht.Add("@UserID", userId);
            ht.Add("@IsProcess", isProcess);
            return Utility.FetchSPExecuteKeyString("f35085_pcexe_CAImportCreateRecords", ht);
        }


    }
}
