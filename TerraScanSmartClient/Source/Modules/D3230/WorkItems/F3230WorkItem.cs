namespace D3230
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;
    using TerraScan.SmartParts;



    public class F3230WorkItem : WorkItem
    {

        public int DeleteCheckOutTable
        {
            get
            {
                return WSHelper.F3230_DeleteCheckOutTable();
            }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="KeyID"></param>
        ///// <param name="KeyField"></param>
        ///// <param name="UserID"></param>
        ///// <returns></returns>
        //public int InsertFieldUseDetails(int KeyID, int KeyField, int UserID)
        //{
        //    return WSHelper.InsertFieldUseDetails(KeyID, KeyField, UserID);
        //}


        ///<summary>
        ///Check Out Apex File Path
        /// </summary>
        public F3230FieldUseData F3230GetApexFilePath(int snapShotId)
        {
            return WSHelper.F3230_GetApexFilePathDetail(snapShotId);
        }

        /// <summary>
        /// Gets the preview detail.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetail">The snap shot detail.</param>
        /// <returns>F9065FieldUseData</returns>
        public F3230FieldUseData.ListPreviewDetailTableDataTable GetPreviewDetail(int snapShotId, string snapShotDetail)
        {
            return WSHelper.F3230_GetPreviewDetail(snapShotId, snapShotDetail).ListPreviewDetailTable;
        }
        
        /// <summary>
        /// F9065_s the get snapshot detail.
        /// </summary>
        /// <returns>F9065FieldUseData</returns>
        public F3230FieldUseData GetSnapshotDetail()
        {
            return WSHelper.F3230_GetSnapshotDetail();
        }

        /// <summary>
        /// Inserts the check out XML.
        /// </summary>
        /// <param name="xmlInsterContent">Content of the XML inster.</param>
        /// <param name="tableXml">The table XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int InsertCheckOutXml(string xmlInsterContent, string tableXml, int userId, bool IsDelete)
        {
            return WSHelper.F3230_InsertChkOutXML(xmlInsterContent, tableXml, userId, IsDelete);
        }

        /// <summary>
        /// F3230_ListLockedParcelID
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="ParcelXML"></param>
        /// <returns></returns>
        public F3230FieldUseData ListLockedParcelID(int? SnapShotID, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ListLockedParcelID(SnapShotID, out RowendValue);
        }

        /// <summary>
        /// F3230_LockParcelID
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="LockAdminBy"></param>
        /// <param name="UserID"></param>
        /// <param name="UnlockParcelXML"></param>
        /// <returns></returns>
        public int LockParcelID(int? SnapShotID, int? LockAdminBy, int? UserID, string UnlockParcelXML)
        {
            return WSHelper.F3230_LockParcelID(SnapShotID, LockAdminBy, UserID, UnlockParcelXML);
        }
       
        /// <summary>
        /// Inserts the field element.
        /// </summary>
        /// <param name="fieldElement">The field element.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int InsertFieldElement(string fieldElement, int userId)
        {
            return WSHelper.F3230_InsertFieldElement(fieldElement, userId);
        }
        
        /// <summary>
        /// Inserts the application configuration.
        /// </summary>
        /// <param name="configXml">The config XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int InsertApplicationConfiguration(string configXml, int userId)
        {
            return WSHelper.F3230_InsertApplicationConfiguration(configXml, userId);
        }

        /// To get Configruation Value
        /// </summary>
        /// <param name="cfgname">The cfgname.</param>
        /// <returns>
        /// Typed dataset containing the Configruation Value.
        /// </returns>
        public F3230FieldUseData GetcfgConfiguration(string cfgname)
        {
            return WSHelper.F3230_GetcfgConfiguration(cfgname);
        }

        #region checkOut

        /// <summary>
        /// Gets the check out XML.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">The snap shot value.</param>
        /// <returns>F9065FieldUseData</returns>
        public F3230FieldUseData ChkOutConfigXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230_ChkOutConfigXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ParcelHeaderChkOutXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F25000FieldUseData ParcelHeaderChkOutXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F25000_ParcelHeaderChkOutXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutFormXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutFormXML(int snapShotId, string snapShotValue, int rowStart)
        {
            return WSHelper.F3230_ChkOutFormXML(snapShotId, snapShotValue, rowStart);
        }

        /// <summary>
        /// ChkOutMiscXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutMiscXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230_ChkOutMiscXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutUserXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutUserXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230_ChkOutUserXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// F3230_GetChkOutParcelIDs
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <returns></returns>
        public F3230CheckInData GetChkOutParcelIDs(int SnapShotID)
        {
            return WSHelper.F3230_GetChkOutParcelIDs(SnapShotID);
        }

        /// <summary>
        /// F3230_GetCheckOutDetails
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public F3230CheckInData GetCheckOutDetails(int SnapShotID, int UserID)
        {
            return WSHelper.F3230_GetCheckOutDetails( SnapShotID,  UserID);
        }

        /// <summary>
        /// F3230_SaveChkOutParcelIDs
        /// </summary>
        /// <param name="ParcelXML"></param>
        /// <returns></returns>
        public int SaveChkOutParcelIDs(string ParcelXML)
        {
            return WSHelper.F3230_SaveChkOutParcelIDs(ParcelXML);
        }

        /// <summary>
        /// F3230_SaveCheckOutDetails
        /// </summary>
        /// <param name="CheckOutXML"></param>
        /// <returns></returns>
        public int SaveCheckOutDetails(string CheckOutXML)
        {
            return WSHelper.F3230_SaveCheckOutDetails(CheckOutXML);
        }



        /// <summary>
        /// ChkOutOwnerXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutOwnerXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            return WSHelper.F3230_ChkOutOwnerXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutEventXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            return WSHelper.F3230_ChkOutEventXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutParcelXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutParcelXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            return WSHelper.F3230_ChkOutParcelXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutDistrictXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutDistrictXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutDistrictXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutNBHDXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutNBHDXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutNBHDXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutLegalXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutLegalXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutLegalXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutMisc_CatalogXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutMisc_CatalogXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutMisc_CatalogXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutMiscTableXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutMiscTableXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutMiscTableXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutMOwnerXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutMOwnerXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutMOwnerXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutObjectXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutObjectXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutObjectXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutValueSliceXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutValueSliceXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutValueSliceXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutLandXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F25000FieldUseData ChkOutLandXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F25000_ChkOutLandXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutVersionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F25000FieldUseData ChkOutVersionXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F25000_ChkOutVersionXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutSitusXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutSitusXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230_ChkOutSitusXML(snapShotId, snapShotValue);
        }
        
        /// <summary>
        /// ChkOutDeprMiscXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutDeprMiscXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230ChkOutDeprMiscXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutDeprXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutDeprXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230ChkOutDeprXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutEstimateCompXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutEstimateCompXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230_ChkOutEstimateCompXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutVSTGCitemXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutVSTGCitemXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230_ChkOutVSTGCitemXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutMSCEstimateXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230_ChkOutMSCEstimateXML(snapShotId, snapShotValue);
        }
        
        /// <summary>
        /// ChkOutEstimateResultXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutEstimateResultXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230_ChkOutEstimateResultXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutMSCEstimateOccupancyXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutMSCEstimateOccupancyXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230_ChkOutMSCEstimateOccupancyXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutEstimateBuildingXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutEstimateBuildingXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230_ChkOutEstimateBuildingXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutCommonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutCommonXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F3230_ChkOutCommonXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutCorrectionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutCorrectionXML(int snapShotId)
        {
            return WSHelper.f3230_ChkOutCorrectionXML(snapShotId);
        }

        /// <summary>
        /// ChkOutSaleXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutSaleXML(int snapShotId)
        {
            return WSHelper.f3230_ChkOutSaleXML(snapShotId);
        }

        /// <summary>
        /// ChkOutReceiptXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutReceiptXML(int snapShotId)
        {
            return WSHelper.f3230_ChkOutReceiptXML(snapShotId);
        }

        /// <summary>
        /// ChkOutStatementXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutStatementXML(int snapShotId)
        {
            return WSHelper.f3230_ChkOutStatementXML(snapShotId);
        }

        /// <summary>
        /// ChkOutLandValuesXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutLandValuesXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutLandValuesXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutVSTerraGonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutTerraGonXML(int snapShotId)
        {
            return WSHelper.F3230_ChkOutTerraGonXML(snapShotId);
        }

        /// <summary>
        /// ChkOutEstimateComponentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutEstimateComponentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutEstimateComponentXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutCommentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutCommentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutCommentXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutVSTGComponentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutVSTGComponentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutVSTGComponentXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }


        /// <summary>
        /// ChkOutFileXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutFileXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutFileXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkOutVSTGGonBldgXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData ChkOutVSTGGonBldgXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkOutVSTGGonBldgXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }
        
        /// <summary>
        /// ChkOutSeniorExemptionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F25000FieldUseData ChkOutSeniorExemptionXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F25000_ChkOutSeniorExemptionXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutAssessmentTypeXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F25000FieldUseData ChkOutAssessmentTypeXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F25000_ChkOutAssessmentTypeXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutParcelValueXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F25000FieldUseData ChkOutParcelValueXML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F25000_ChkOutParcelValueXML(snapShotId, snapShotValue);
        }

        /// <summary>
        /// ChkOutType2XML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F25000FieldUseData ChkOutType2XML(int snapShotId, string snapShotValue)
        {
            return WSHelper.F25000_ChkOutType2XML(snapShotId, snapShotValue);
        }


        #endregion CheckOut

        #region F3230 Check in

        /// <summary>
        /// ChkInTypesXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInTypesXML()
        {
            return WSHelper.F3230_ChkInTypesXML();
        }

        /// <summary>
        /// ChkInDeprXML
        /// </summary>
        /// <returns></returns>
        public F3230CheckInData ChkInDeprXML()
        {
            return WSHelper.F3230_ChkInDeprXML();
        }

        /// <summary>
        /// ChkInEstimateComponentGroupXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInEstimateComponentGroupXML()
        {
            return WSHelper.F3230_ChkInEstimateComponentGroupXML();
        }

        /// <summary>
        /// ChkInNBHDXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInNBHDXML()
        {
            return WSHelper.F3230_ChkInNBHDXML();
        }

        /// <summary>
        /// ChkInCommentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInCommentXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInCommentXML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkInEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInEstimateXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInEstimateXML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkInFileXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInFileXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInFileXML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkInLandValuesXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInLandValuesXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInLandValuesXML(TableName, StartRow, out  RowendValue);
        }



        /// <summary>
        /// ChkInLandXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInLandXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInLandXML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkInInsertXML
        /// </summary>
        /// <returns></returns>
        public string ChkInInsertXML(out string ChkInInsertXML)
        {
            return WSHelper.F3230_ChkInInsertXML(out  ChkInInsertXML);
        }

        /// <summary>
        /// ChkInTerraGonInsertXML
        /// </summary>
        /// <returns></returns>
        public string ChkInTerraGonInsertXML(out string ChkInInsertXML)
        {
            return WSHelper.F3230_ChkInTerraGonInsertXML(out ChkInInsertXML);
        }

        /// <summary>
        /// ChkInLandCodeXML
        /// </summary>
        /// <returns></returns>
        public F3230CheckInData ChkInLandCodeXML()
        {
            return WSHelper.F3230_ChkInLandCodeXML();
        }

        /// <summary>
        /// F3230_ParcelID
        /// </summary>
        /// <returns></returns>
        public F3230CheckInData ParcelIDs()
        {
            return WSHelper.F3230_ParcelID();
        }


        /// <summary>
        /// ChkInInsertedFileXML
        /// </summary>
        /// <returns></returns>
        public  F3230CheckInData ChkInInsertedFileXML()
        {
            return WSHelper.F3230_ChkInInsertedFileXML();
        }

        /// <summary>
        /// InsertFile
        /// </summary>
        /// <param name="insertxmlContent"></param>
        /// <returns></returns>
        public F3230CheckInData InsertFile(string insertxmlContent)
        {
            return WSHelper.F3230_InsertFile(insertxmlContent);
        }

        /// <summary>
        /// UpdateFileXML
        /// </summary>
        /// <param name="updatexmlContent"></param>
        /// <returns></returns>
        public F3230CheckInData UpdateFileXML(string updatexmlContent)
        {
            return WSHelper.F3230_UpdateFile(updatexmlContent);
        }

        /// <summary>
        /// ChkInMiscXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInMiscXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInMiscXML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkInMSC_EstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInMSC_EstimateXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInMSC_EstimateXML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkInObjectXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInObjectXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInObjectXML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkInParcelValueXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInParcelValueXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInParcelValueXML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkInParcelXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInParcelXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInParcelXML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkInTerraGonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInTerraGonXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInTerraGonXML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkInType2XML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInType2XML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInType2XML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// ChkInType6XML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData ChkInType6XML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInType6XML(TableName, StartRow, out  RowendValue);
        }

        public F3230CheckInData ChkInValueSliceXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInValueSliceXML(TableName, StartRow, out  RowendValue);
        }

        /// <summary>
        /// InsertCheckInXml
        /// </summary>
        /// <param name="xmlInsterContent"></param>
        /// <param name="tableXml"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int InsertCheckInXml(string xmlInsterContent, string tableXml, int userId)
        {
            return WSHelper.F3230_InsertChkInXML(xmlInsterContent, tableXml, userId);
        }

        /// <summary>
        /// InsertAddedRecordXML
        /// </summary>
        /// <param name="xmlInsterContent"></param>
        /// <param name="tableXml"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int InsertAddedRecordXML(string xmlInsterContent, string tableXml, int userId)
        {
            if (string.IsNullOrEmpty(xmlInsterContent))
                xmlInsterContent = null;
            return WSHelper.F3230_InsertAddedRecordXML(xmlInsterContent, tableXml, userId);
        }

        #endregion

        #region WorkItemEvents

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion WorkItemEvents
    }
}