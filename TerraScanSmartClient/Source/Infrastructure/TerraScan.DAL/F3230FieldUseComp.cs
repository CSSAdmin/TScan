using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TerraScan.DataLayer;
using TerraScan.BusinessEntities;

namespace TerraScan.Dal
{
    /// <summary>
    /// F3230FieldUseComp
    /// </summary>
    public static class F3230FieldUseComp
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KeyID"></param>
        /// <param name="KeyField"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static int InsertFieldUseDetails(int KeyID, string KeyField, int UserID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@KeyID", KeyID);
            ht.Add("@KeyField", KeyField);
            ht.Add("@UserID", UserID);

            return Utility.FetchSPExecuteKeyId("f3230_pcins_UpdatedParcels", ht);
        }

        #region F3230_GetSnapshotDetail

        /// <summary>
        /// F3230_s the get snapshot detail.
        /// </summary>
        /// <returns>F9065FieldUseData</returns>
        public static F3230FieldUseData F3230_GetSnapshotDetail()
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(fieldUseData.ListSnapshotTable, "f3230_pcget_Snapshot", ht);
            return fieldUseData;
        }
        #endregion  F3230_GetSnapshotDetail

        #region F3230_GetApexFilePathDetail

        /// <summary>
        ///F3230_Get Apex File Path Detail.
        /// </summary>
        /// <returns>F9065FieldUseData</returns>
        public static F3230FieldUseData F3230GetApexFilePath(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            Utility.LoadDataSet(fieldUseData.ApexFilePath, "f3230_pcget_SketchFieldPath", ht);
            return fieldUseData;
        }
        #endregion  F3230_GetApexFilePathDetail


        /// <summary>
        /// F3230_AddValues
        /// </summary>
        /// <param name="KeyID"></param>
        /// <param name="KeyField"></param>
        /// <param name="Form"></param>
        /// <param name="ModuleID"></param>
        /// <param name="InsertedBy"></param>
        /// <returns></returns>
        public static int F3230_AddValues(int KeyID, string KeyField, int Form, int? ModuleID, int InsertedBy)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@KeyID", KeyID);
            ht.Add("@KeyField", KeyField);
            ht.Add("@Form", Form);
            ht.Add("@ModuleID", ModuleID);
            ht.Add("@InsertedBy", InsertedBy);
            return Utility.FetchSPExecuteKeyId("f3230_pcins_AddValues", ht);
        }

        #region F3230_UpdateApplicationStatus
        /// <summary>
        /// F9065_s the update application status.
        /// </summary>
        /// <param name="ischeckedout">if set to <c>true</c> [ischeckedout].</param>
        /// <param name="isonline">if set to <c>true</c> [isonline].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value</returns>
        public static int F3230_UpdateApplicationStatus(bool ischeckedout, bool isonline, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@IsCheckedOut", ischeckedout);
            ht.Add("@IsOnline", isonline);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f3230_pcupd_ChkOutStatus", ht);
        }
        #endregion F3230_UpdateApplicationStatus

        #region F3230_GetcfgConfiguration
        /// <summary>
        /// To get Configruation Value
        /// </summary>
        /// <param name="cfgname">The cfgname.</param>
        /// <returns>
        /// Typed dataset containing the Configruation Value.
        /// </returns>
        public static F3230FieldUseData F3230_GetcfgConfiguration(string cfgname)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            Hashtable ht = new Hashtable();
            ht.Add("@CfgName", cfgname);
            Utility.LoadDataSet(fieldUseData.ListCfgConfigTable, "f9020_pcget_Configuration", ht);
            return fieldUseData;
        }
        #endregion F3230_GetcfgConfiguration

        #region F3230_InsertApplicationConfiguration
        /// <summary>
        /// F3230_InsertApplicationConfiguration
        /// </summary>
        /// <param name="configXml">The config.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public static int F3230_InsertApplicationConfiguration(string configXml, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AppConfigItems", configXml);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f3230_pcins_ApplicationConfiguration", ht);
        }
        #endregion   F3230_InsertApplicationConfiguration

        #region F3230_InsertFieldElement
        /// <summary>
        /// F9065_s the insert field element.
        /// </summary>
        /// <param name="fieldElement">The field element.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public static int F3230_InsertFieldElement(string fieldElement, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FieldElements", fieldElement);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f3230_pcupd_ChkOutParcels", ht);
        }
        #endregion F3230_InsertFieldElement

        #region GetAuditCount
        ///// <summary>
        ///// Get Audit Count
        ///// </summary>
        ///// <returns>Integer</returns>
        //public static int F3230_GetAuditCount()
        //{
        //    Hashtable ht = new Hashtable();
        //    return Utility.FetchSPExecuteKeyId("f3230_pcget_AuditCount", ht);
        //}
        #endregion GetAuditCount

        #region F3230_GetPreviewDetail
        /// <summary>
        /// F9065_s the preview detail.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetail">The snap shot detail.</param>
        /// <returns>F9065FieldUseData</returns>
        public static F3230FieldUseData F3230_GetPreviewDetail(int snapShotId, string snapShotDetail)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotDetail);
            Utility.LoadDataSet(fieldUseData.ListPreviewDetailTable, "f3230_pcget_PreviewCount", ht);
            return fieldUseData;
        }
        #endregion F3230_GetPreviewDetail

        #region Delete the values
        /// <summary>
        /// Delete the values
        /// </summary>
        /// <returns>Integer value</returns>
        public static int F3230_DeleteCheckOutTable()
        {
            Hashtable ht = new Hashtable();
            return Utility.FetchSPExecuteKeyId("f3230_pcdel_ChkOutTables", ht);
        }
        #endregion Delete the values

        #region F3230 CheckOut

        #region ChkoutDeprMisc
        /// <summary>
        /// F3230 the get ChkoutDeprMisc XML.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>Typed Dataset</returns>
        public static F3230FieldUseData F3230ChkOutDeprMiscXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();

            string[] tableNames = {   fieldUseData.tAA_CropValues.TableName
                                    , fieldUseData.tAA_LandInfluence.TableName
                                    , fieldUseData.tTS_FileType.TableName
                                    , fieldUseData.tAA_LandInfluenceItem.TableName
                                    , fieldUseData.tTS_QueryUpdate.TableName
                                    , fieldUseData.tAA_LandProgram.TableName
                                    , fieldUseData.Remarks.TableName
                                    , fieldUseData.tAA_DeprFunctional_Category.TableName
                                    , fieldUseData.tAA_Misc_CatalogChoice.TableName
                                    , fieldUseData.Estimate.TableName
                                    , fieldUseData.tTS_TableXML.TableName
                                   };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutDeprMiscXML", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// F3230ChkOutDeprXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230ChkOutDeprXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();

            string[] tableNames = {     fieldUseData.tAA_DeprItem.TableName   
                                       ,fieldUseData.tAA_LandType1.TableName 
                                       ,fieldUseData.tAA_LandType2.TableName 
                                       ,fieldUseData.tAA_LandType3.TableName 
                                       ,fieldUseData.tAA_VSTG_QItem.TableName       
                                       ,fieldUseData.tTS_TableXML.TableName
                                   };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutDeprXML", ht, tableNames);
            return fieldUseData;
        }

        #endregion ChkoutDeprMisc

        #region ChkOutEstimateComp

        /// <summary>
        /// F3230_ChkOutEstimateCompXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutEstimateCompXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = {     fieldUseData.EstimateComponentGroup.TableName
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutEstimateCompXML", ht, tableNames);
            return fieldUseData;
        }

        #endregion ChkOutEstimateComp

        #region ChkOutVSTGCitem

        /// <summary>
        /// F3230_CChkOutVSTGCitemXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutVSTGCitemXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            Hashtable ht = new Hashtable();
            string[] tableNames = { fieldUseData.tAA_VSTG_Citem.TableName
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutVSTGCitemXML", ht, tableNames);
            return fieldUseData;
        }

        #endregion ChkOutVSTGCitem

        #region ChkOutMSCEstimate

        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutMSCEstimateXML(int snapShotId, string snapShotValue)
        {

            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = {  fieldUseData.tAA_MSC_Estimate.TableName
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutMSCEstimateXML", ht, tableNames);
            return fieldUseData;
        }

        #endregion ChkOutMSCEstimate

        #region ChkOutEstimateResult

        /// <summary>
        /// F3230_ChkOutEstimateResultXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutEstimateResultXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = {   fieldUseData.EstimateResult.TableName
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutEstimateResultXML", ht, tableNames);
            return fieldUseData;
        }

        #endregion ChkOutEstimateResult

        #region ChkOutMSCEstimateOccupancy

        /// <summary>
        /// F3230_ChkOutMSCEstimateOccupancyXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutMSCEstimateOccupancyXML(int snapShotId, string snapShotValue)
        {

            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = {     fieldUseData.tAA_MSC_EstimateOccupancy.TableName
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutMSCEstimateOccupancyXML", ht, tableNames);
            return fieldUseData;
        }

        #endregion ChkOutMSCEstimateOccupancy

        #region ChkOutEstimateBuilding

        /// <summary>
        /// F3230_ChkOutMSCEstimateOccupancyXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutEstimateBuildingXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = {  fieldUseData.tAA_MSC_EstimateBuilding.TableName 
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutEstimateBuildingXML", ht, tableNames);
            return fieldUseData;
        }

        #endregion ChkOutEstimateBuilding

        #region ChkOutLandValues

        /// <summary>
        /// F3230_ChkOutLandValuesXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutLandValuesXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {

            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = { fieldUseData.tAA_LandValues.TableName
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            RowendValue = 0;
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutLandValuesXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }

        #endregion ChkOutLandValues

        #region ChkOutVSTerraGon

        /// <summary>
        /// F3230_ChkOutVSTerraGonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutTerraGonXML(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = {   fieldUseData.tAA_VSTerraGon.TableName
                                      ,fieldUseData.tAA_VSTG_Component.TableName
                                      ,fieldUseData.   tAA_VSTG_GonBldg.TableName
                                      ,fieldUseData.tTS_TableXML.TableName
                                      };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutTerraGonXML", ht, tableNames);
            return fieldUseData;
        }

        #endregion ChkOutVSTerraGon

        #region ChkOutEstimateComponent

        /// <summary>
        /// F3230_ChkOutEstimateComponentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutEstimateComponentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = {fieldUseData.EstimateComponent.TableName
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            RowendValue = 0;
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutEstimateComponentXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }

        #endregion ChkOutEstimateComponent

        #region ChkOutComment

        /// <summary>
        /// f3230_pcget_ChkOutCommentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutCommentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = {fieldUseData.tTS_Comment.TableName
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            RowendValue = 0;
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutCommentXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }

        #endregion ChkOutComment

        #region ChkOutVSTGComponent

        /// <summary>
        /// f3230_pcget_ChkOutVSTGComponentXM
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutVSTGComponentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = { fieldUseData.tAA_VSTG_Component.TableName
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            RowendValue = 0;
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutVSTGComponentXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }

        #endregion ChkOutVSTGComponent

        #region ChkOutFile

        /// <summary>
        /// F3230_ChkOutFileXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutFileXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = {fieldUseData.tTS_File.TableName
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            RowendValue = 0;
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutFileXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }

        #endregion ChkOutFile

        #region ChkOutVSTGGonBldg

        /// <summary>
        /// F3230_ChkOutVSTGGonBldgXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutVSTGGonBldgXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = {fieldUseData.tAA_VSTG_GonBldg.TableName
                                      , fieldUseData.tTS_TableXML.TableName
                                      };
            RowendValue = 0;
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutVSTGGonBldgXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }

        #endregion ChkOutVSTGGonBldg

        #region ChkOutUserXML

        /// <summary>
        /// F3230_ChkOutUserXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutUserXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            string[] tableNames = {   
                                      fieldUseData.tTS_UserGroup.TableName
                                    , fieldUseData.tTS_User.TableName
                                    , fieldUseData.tTS_User2Group.TableName
                                    , fieldUseData.tTS_UserPermission.TableName
                                    , fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutUserXML", ht, tableNames);
            return fieldUseData;
        }
        #endregion ChkOutUserXML

        #region ChkOutMiscXML
        /// <summary>
        /// F3230_ChkOutMiscXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutMiscXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();

            string[] tableNames = {   fieldUseData.tAA_ParcelType.TableName
                                    , fieldUseData.tTS_SubDivision.TableName
                                    , fieldUseData.tAA_ObjectGroup.TableName
                                    , fieldUseData.tTS_Unit.TableName
                                    , fieldUseData.tTS_StreetList.TableName
                                    , fieldUseData.tGD_FeatureClass.TableName
                                    , fieldUseData.tAA_LandCode.TableName
                                    , fieldUseData.tGD_EventType.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutMiscXML", ht, tableNames);
            return fieldUseData;
        }

        #endregion ChkOutMiscXML

        #region ChkOutFormXML
        /// <summary>
        /// F3230_ChkOutFormXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="rowStart"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutFormXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseDataSet = new F3230FieldUseData();
            string[] tableNames = {   fieldUseDataSet.tAA_Object2Slice.TableName
                                    , fieldUseDataSet.tGD_System.TableName
                                    , fieldUseDataSet.tAA_DeprControl.TableName
                                    , fieldUseDataSet.tAA_SliceType.TableName
                                    , fieldUseDataSet.tGD_EG_Status.TableName
                                    , fieldUseDataSet.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseDataSet, "f3230_pcget_ChkOutFormXML", ht, tableNames);
            return fieldUseDataSet;
        }
        #endregion ChkOutFormXML

        #region ChkOutConfigXML
        /// <summary>
        /// F39065_s the get CHK out XML.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>Typed Dataset</returns>
        public static F3230FieldUseData F3230_ChkOutConfigXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();

            string[] tableNames = {   fieldUseData.tTS_QueryViewLayout.TableName
                                    , fieldUseData.tAA_ApexPolygon.TableName
                                    , fieldUseData.EstimateComponentGroup.TableName                                      
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutConfigXML", ht, tableNames);
            return fieldUseData;
        }
        #endregion ChkOutConfigXML

        /// <summary>
        /// F3230_ChkOutCommonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutCommonXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();

            string[] tableNames = {   fieldUseData.tAA_Condition.TableName
                                    , fieldUseData.tAA_NBHDCfg.TableName
                                    , fieldUseData.tAA_QualityRes.TableName
                                    , fieldUseData.tAA_MSC_EstimateComponent.TableName
                                    , fieldUseData.tAA_Depr.TableName
                                    , fieldUseData.tTS_CommentPriority.TableName
                                    , fieldUseData.tTS_CommentTemplate.TableName
                                    , fieldUseData.tAA_Class.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutCommonXML", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// f3230_ChkOutCorrectionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public static F3230FieldUseData f3230_ChkOutCorrectionXML(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();

            string[] tableNames = {   fieldUseData.tAA_ParcelHistory.TableName
                                    , fieldUseData.tAA_Permit.TableName
                                    , fieldUseData.tAA_Combine.TableName
                                    , fieldUseData.tAA_Split.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutCorrectionXML", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// f3230_ChkOutSaleXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public static F3230FieldUseData f3230_ChkOutSaleXML(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();

            string[] tableNames = { fieldUseData.tTS_OwnerStatus.TableName
                                    ,fieldUseData.tAA_Sale.TableName
                                    ,fieldUseData.tAA_SaleParcels.TableName
                                    ,fieldUseData.tAA_SaleOwners.TableName
                                    ,fieldUseData.tTR_Statement_SupCorr.TableName
                                    ,fieldUseData.tTR_StatementCorr.TableName
                                    ,fieldUseData.tAA_VSCOnditionCL_Items.TableName
                                    ,fieldUseData.tAA_VSQualityCL_Items.TableName
                                    ,fieldUseData.tTS_Correction.TableName
                                    ,fieldUseData.tAA_MakeSub.TableName
                                    ,fieldUseData.tAA_VSBuildingDataItem.TableName
                                    ,fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutSaleXML", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// f3230_ChkOutReceiptXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public static F3230FieldUseData f3230_ChkOutReceiptXML(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();

            string[] tableNames = {   fieldUseData.tTR_Receipt.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutReceiptXML", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// f3230_ChkOutStatementXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public static F3230FieldUseData f3230_ChkOutStatementXML(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();

            string[] tableNames = {   fieldUseData.  tTR_Statement.TableName
                                    , fieldUseData.  tTR_Statement_Sup.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutStatementXML", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// F3230_LockParcelID
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="LockAdminBy"></param>
        /// <param name="UserID"></param>
        /// <param name="UnlockParcelXML"></param>
        /// <returns></returns>
        public static int F3230_LockParcelID(int? SnapShotID, int? LockAdminBy, int? UserID, string UnlockParcelXML)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", SnapShotID);
            ht.Add("@LockAdminBy", LockAdminBy);
            ht.Add("@UserID", UserID);
            ht.Add("@UnlockParcelXML", UnlockParcelXML);

            return Utility.FetchSPExecuteKeyId("f3230_pcupd_ParcelLock", ht);
        }

       /// <summary>
        /// F3230_ListLockedParcelID
       /// </summary>
       /// <param name="SnapShotID"></param>
       /// <param name="RowendValue"></param>
       /// <returns></returns>
        public static F3230FieldUseData F3230_ListLockedParcelID(int? SnapShotID, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseDataSet = new F3230FieldUseData();
            string[] tableNames = { fieldUseDataSet.LockedParcel.TableName
                                    , fieldUseDataSet.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", SnapShotID);
            IList returnList = Utility.LoadDataSet(fieldUseDataSet, "f3230_pcget_ParcelLock", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[1].Value.ToString());
            return fieldUseDataSet;
        }
        
        #region ChkOutEventXML

        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutEventXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tGD_Event.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutEventXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkOutEventXML

        /// <summary>
        /// F3230_ChkOutParcelXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutParcelXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tAA_Parcel.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutParcelXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }

        #region ChkOutOwnerXML
        /// <summary>
        /// F3230_ChkOutOwnerXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutOwnerXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            RowendValue = 0;
            string[] tableNames = { fieldUseData.tTS_Owner.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutOwnerXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }

        #endregion ChkOutOwnerXML

        #region ChkOutDistrictXML
        /// <summary>
        /// F3230_ChkOutDistrictXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutDistrictXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            RowendValue = 0;
            string[] tableNames = {  fieldUseData.tTS_District.TableName
                                    ,fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutDistrictXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkOutDistrictXML

        #region ChkOutNBHDXML
        /// <summary>
        /// F3230_ChkOutNBHDXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutNBHDXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            RowendValue = 0;
            string[] tableNames = {  fieldUseData.tAA_NBHD.TableName
                                    ,fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutNBHDXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkOutNBHDXML

        #region ChkOutLegalXML

        /// <summary>
        /// F3230_ChkOutLegalXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutLegalXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            RowendValue = 0;
            string[] tableNames = {  fieldUseData.tTS_Legal.TableName
                                    ,fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutLegalXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkOutLegalXML

        #region ChkOutMisc_CatalogXML

        /// <summary>
        /// F3230_ChkOutMisc_CatalogXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutMisc_CatalogXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            RowendValue = 0;
            string[] tableNames = {  fieldUseData.tAA_Misc_Catalog.TableName
                                    ,fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutMisc_CatalogXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkOutMisc_CatalogXML

        #region ChkOutMiscTableXML
        /// <summary>
        /// F3230_ChkOutMiscTableXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutMiscTableXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            RowendValue = 0;
            string[] tableNames = {  fieldUseData.tAA_Misc.TableName
                                    ,fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutMiscTableXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }

        #endregion ChkOutMiscTableXML

        #region ChkOutMOwnerXML
        /// <summary>
        /// F3230_ChkOutMOwnerXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutMOwnerXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            RowendValue = 0;
            string[] tableNames = {  fieldUseData.tAA_MOwner.TableName
                                    ,fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutMOwnerXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkOutMOwnerXML

        #region ChkOutObjectXML
        /// <summary>
        /// F3230_ChkOutObjectXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutObjectXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            RowendValue = 0;
            string[] tableNames = {  fieldUseData.tAA_Object.TableName
                                    ,fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutObjectXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkOutObjectXML

        #region ChkOutValueSliceXML
        /// <summary>
        /// F3230_ChkOutValueSliceXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutValueSliceXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            RowendValue = 0;
            string[] tableNames = {  fieldUseData.tAA_ValueSlice.TableName
                                    ,fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutValueSliceXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkOutValueSliceXML

        #region ChkOutSitusXML
        /// <summary>
        /// F3230_ChkOutSitusXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutSitusXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseDataSet = new F3230FieldUseData();
            string[] tableNames = { fieldUseDataSet.tTS_Situs.TableName
                                    , fieldUseDataSet.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseDataSet, "f3230_pcget_ChkOutSitusXML", ht, tableNames);
            return fieldUseDataSet;
        }
        #endregion ChkOutSitusXML

        #region InsertChkOutXML

       /// <summary>
        /// F3230_InsertChkOutXML
       /// </summary>
       /// <param name="xmlInsContent"></param>
       /// <param name="tableXml"></param>
       /// <param name="userId"></param>
       /// <param name="IsDelete"></param>
       /// <returns></returns>
        public static int F3230_InsertChkOutXML(string xmlInsContent, string tableXml, int userId, bool IsDelete)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@xmlInsContent", xmlInsContent);
            ht.Add("@UserID", userId);
            ht.Add("@IsDelete", IsDelete);
            return Utility.FetchSPExecuteKeyId("f3230_pcins_ChkOutXML", ht);
        }
        #endregion InsertChkOutXML

        #endregion F3230 CheckOut

        #region F3230 CheckIn
        
        /// <summary>
        /// F3230_ChkInDeprXML
        /// </summary>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInDeprXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            string[] tableNames = {   fieldUseData.tAA_DeprItem.TableName
                                    , fieldUseData.tAA_Misc_Catalog.TableName
                                    , fieldUseData.tAA_Depr.TableName
                                    , fieldUseData.tAA_ApexPolygon.TableName
                                    , fieldUseData.tAA_LandType1.TableName
                                    , fieldUseData.tTS_QueryViewLayout.TableName
                                    , fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInDeprXML", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// F3230_ChkInInsertedFileXML
        /// </summary>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInInsertedFileXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            string[] tableNames = {   fieldUseData.tTS_File.TableName
                                  };
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInInsertedFileXML", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// F3230_InsertFile
        /// </summary>
        /// <param name="insertxmlContent"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_InsertFile(string insertxmlContent)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            string[] tableNames = {   fieldUseData.tTS_File.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@insertxmlContent", insertxmlContent);
            Utility.LoadDataSet(fieldUseData, "f3230_pcins_ChkInInsertedFileXML", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// F3230_UpdateFile
        /// </summary>
        /// <param name="updatexmlContent"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_UpdateFile(string updatexmlContent)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            Hashtable ht = new Hashtable();
            ht.Add("@insertxmlContent", updatexmlContent);
            Utility.LoadDataSet(fieldUseData, "f3230_pcins_ChkInInsertedFileXML", ht);
            return fieldUseData;
        }
        

        #region ChkInTypesXML
        /// <summary>
        /// F3230_ChkInTypesXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInTypesXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            string[] tableNames = {   fieldUseData.tTS_Exemption_Type1.TableName
                                    , fieldUseData.tAA_LandInfluenceItem.TableName
                                    , fieldUseData.tAA_MA_Type9.TableName
                                    , fieldUseData.tAA_MAD_Type9.TableName
                                    , fieldUseData.tAA_MAD_Type2.TableName
                                    , fieldUseData.tAA_MA_Type6.TableName
                                    , fieldUseData.tAA_MA_Type1.TableName
                                    , fieldUseData.tAA_MAD_Type10.TableName
                                    , fieldUseData.tAA_MA_Type10.TableName
                                    , fieldUseData.tAA_LandUnit.TableName
                                    , fieldUseData.tTS_CommentTemplate.TableName
                                    , fieldUseData.tAA_MA_Type11.TableName
                                    , fieldUseData.tAA_MA_Type12.TableName
                                    , fieldUseData.tAA_MA_Type3.TableName
                                    , fieldUseData.tAA_MA_Type4.TableName
                                    , fieldUseData.tAA_MA_Type7.TableName
                                    , fieldUseData.tAA_MA_Type8.TableName
                                    , fieldUseData.tAA_MAD_Type11.TableName
                                    , fieldUseData.tAA_MAD_Type12.TableName
                                    , fieldUseData.tAA_MAD_Type3.TableName
                                    , fieldUseData.tAA_MAD_Type4.TableName
                                    , fieldUseData.tAA_MAD_Type7.TableName
                                    , fieldUseData.tAA_MAD_Type8.TableName
                                    , fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInTypesXML", ht, tableNames);
            return fieldUseData;
        }
        #endregion ChkInTypesXML

        /// <summary>
        /// F3230_ChkInLandCodeXML
        /// </summary>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInLandCodeXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            string[] tableNames = {   fieldUseData.EstimateComponent.TableName
                                    , fieldUseData.tAA_LandCode.TableName
                                    , fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInLandCodeXML", ht, tableNames);
            return fieldUseData;
        }

        #region ChkInEstimateComponentGroupXML
        /// <summary>
        /// F3230_ChkInEstimateComponentGroupXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInEstimateComponentGroupXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            string[] tableNames = {   
                                      fieldUseData.EstimateComponentGroup.TableName
                                    , fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInEstimateComponentGroupXML", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// F3230_ParcelID
        /// </summary>
        /// <returns></returns>
        public static F3230CheckInData F3230_ParcelID()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            string[] tableNames = {   fieldUseData.ParcelIDs.TableName
                                  };
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ParcelID", ht, tableNames);
            return fieldUseData;
        }

        #endregion ChkInEstimateComponentGroupXML
        
        #region ChkInNBHDXML
        /// <summary>
        /// F3230_ChkInNBHDXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInNBHDXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            string[] tableNames = {   
                                      fieldUseData.tAA_NBHD.TableName
                                    , fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInNBHDXML", ht, tableNames);
            return fieldUseData;
        }
        #endregion ChkInNBHDXML

        #region ChkInValueSliceXML
        public static F3230CheckInData F3230_ChkInValueSliceXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tTS_Comment.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInValueSliceXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInValueSliceXML

        /// <summary>
        /// F3230_ChkInInsertXML
        /// </summary>
        /// <returns></returns>
        public static string F3230_ChkInInsertXML(out string  ChkInInsertXML )
        {
            ChkInInsertXML = string.Empty;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            Hashtable ht = new Hashtable();
            ht.Add("@ChkInInsertXML", ChkInInsertXML);
            string[] tableNames = { fieldUseData.InsertXml.TableName };
            IList returnList = Utility.LoadDataSet(fieldUseData.InsertXml, "f3230_pcget_ChkInInsertXML", ht);
            if (fieldUseData.InsertXml.Rows.Count > 0)
                 ChkInInsertXML = fieldUseData.InsertXml.Rows[0][0].ToString();
            else
                ChkInInsertXML = string.Empty;
            return ChkInInsertXML;
        }

        /// <summary>
        /// F3230_ChkInTerraGonInsertXML
        /// </summary>
        /// <returns></returns>
        public static string F3230_ChkInTerraGonInsertXML(out string ChkInInsertXML)
        {
            ChkInInsertXML = string.Empty;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            Hashtable ht = new Hashtable();
            ht.Add("@ChkInInsertXML", ChkInInsertXML);            
            string[] tableNames = { fieldUseData.InsertXml.TableName };
            IList returnList = Utility.LoadDataSet(fieldUseData.InsertXml, "f3230_pcget_ChkInTerraGonInsertXML", ht);
            if (fieldUseData.InsertXml.Rows.Count > 0)
                ChkInInsertXML = fieldUseData.InsertXml.Rows[0][0].ToString();
            else
                ChkInInsertXML = string.Empty;
            return ChkInInsertXML;
        }

        /// <summary>
        /// F3230_GetChkOutParcelIDs
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_GetChkOutParcelIDs(int SnapShotID)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            string[] tableNames = {   fieldUseData.ParcelIDs.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", SnapShotID);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutParcelIDs", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// F3230_GetCheckOutDetails
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_GetCheckOutDetails(int SnapShotID, int UserID)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            string[] tableNames = { fieldUseData.ChecoutDetailsXML.TableName };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", SnapShotID);
            ht.Add("@UserID", UserID);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_CheckOutDetails", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// F3230_SaveChkOutParcelIDs
        /// </summary>
        /// <param name="ParcelXML"></param>
        /// <returns></returns>
        public static int F3230_SaveChkOutParcelIDs(string ParcelXML)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelXML", ParcelXML);
            return Utility.FetchSPExecuteKeyId("f3230_pcins_ChkOutParcelIDs", ht);
        }

        /// <summary>
        /// F3230_SaveCheckOutDetails
        /// </summary>
        /// <param name="CheckOutXML"></param>
        /// <returns></returns>
        public static int F3230_SaveCheckOutDetails(string CheckOutXML)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CheckOutXML", CheckOutXML);
            return Utility.FetchSPExecuteKeyId("f3230_pcins_CheckOutDetails", ht);
        }

        #region ChkInCommentXML
        /// <summary>
        /// F3230_ChkInCommentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInCommentXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tTS_Comment.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInCommentXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInCommentXML

        #region ChkInEstimateXML
        /// <summary>
        /// F3230_ChkInEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInEstimateXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.Estimate.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInEstimateXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInEstimateXML

        #region ChkInFileXML
        /// <summary>
        /// F3230_ChkInFileXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInFileXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tTS_File.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInFileXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInFileXML


        #region ChkInLandValuesXML
        /// <summary>
        /// F3230_ChkInLandValuesXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInLandValuesXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tAA_LandValues.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInLandValuesXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInLandValuesXML

        #region ChkInLandXML
        /// <summary>
        /// F3230_ChkInLandXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInLandXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tAA_Land.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInLandXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInLandXML

        #region ChkInMiscXML
        /// <summary>
        /// F3230_ChkInMiscXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInMiscXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tAA_Misc.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInMiscXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInMiscXML

        #region ChkInMSC_EstimateXML
        /// <summary>
        /// F3230_ChkInMSC_EstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInMSC_EstimateXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tAA_MSC_Estimate.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInMSC_EstimateXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInMSC_EstimateXML

        #region ChkInObjectXML
        /// <summary>
        /// F3230_ChkInObjectXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInObjectXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tAA_Object.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInObjectXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInObjectXML

        #region ChkInParcelValueXML
        /// <summary>
        /// F3230_ChkInParcelValueXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInParcelValueXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tAA_ParcelValue.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInParcelValueXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInParcelValueXML

        #region ChkInParcelXML
        /// <summary>
        /// F3230_ChkInParcelXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInParcelXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tAA_Parcel.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInParcelXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInParcelXML

        #region ChkInTerraGonXML
        /// <summary>
        /// F3230_ChkInTerraGonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInTerraGonXML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tAA_VSTerraGon.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInTerraGonXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInTerraGonXML

        #region ChkInType2XML
        /// <summary>
        /// F3230_ChkInType2XML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInType2XML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tAA_MA_Type2.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInType2XML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInType2XML

        #region ChkInType6XML
        /// <summary>
        /// F3230_ChkInType6XML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInType6XML(string TableName, int StartRow, out int RowendValue)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            RowendValue = 0;
            string[] tableNames = {   fieldUseData.tAA_MAD_Type6.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkInType6XML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[2].Value.ToString());
            return fieldUseData;
        }
        #endregion ChkInType6XML

        #region InsertChkInXML
        /// <summary>
        /// F3230_InsertChkInXML
        /// </summary>
        /// <param name="xmlInsContent"></param>
        /// <param name="tableXml"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int F3230_InsertChkInXML(string xmlInsContent, string tableXml, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@xmlInsContent", xmlInsContent);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f3230_pcupd_ChkInXML", ht);
        }
        #endregion InsertChkInXML

        /// <summary>
        /// F3230_InsertAddedRecordXML
        /// </summary>
        /// <param name="xmlInsContent"></param>
        /// <param name="tableXml"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int F3230_InsertAddedRecordXML(string xmlInsContent, string tableXml, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@xmlInsContent", xmlInsContent);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f3230_pcins_ChkInXML", ht);
        }

        #endregion CheckIn


    }
}
