//--------------------------------------------------------------------------------------------
// <copyright file="F9065WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9060WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Nov 06      Karthikeyan v              Created
//*********************************************************************************/

namespace D9065
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
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F9065WorkItem
    /// </summary>
    public class F9065WorkItem : WorkItem
    {
        /// <summary>
        /// Get Audit Count
        /// </summary>
        /// <returns>Integer</returns>
        public int GetAuditCount
        {
            get
            {
                return WSHelper.F9065_GetAuditCount();
            }
        }
        private F3230FieldUseData fieldCheckOutDataSet = new F3230FieldUseData();
        /// <summary>
        /// Get Audit Count
        /// </summary>
        /// <returns>Integer</returns>
        public int DeleteCheckOutTable
        {
            get
            {
                return WSHelper.F9065_DeleteCheckOutTable();
            }
        }

        /// <summary>
        /// F9065_s the get snapshot detail.
        /// </summary>
        /// <returns>F9065FieldUseData</returns>
        public F9065FieldUseData GetSnapshotDetail()
        {
            return WSHelper.F9065_GetSnapshotDetail();
        }

        /// <summary>
        /// Updates the application status.
        /// </summary>
        /// <param name="checkedOutStatus">if set to <c>true</c> [checked out status].</param>
        /// <param name="onlineStatus">if set to <c>true</c> [online status].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int UpdateApplicationStatus(bool checkedOutStatus, bool onlineStatus, int userId)
        {
            return WSHelper.F9065_UpdateApplicationStatus(checkedOutStatus, onlineStatus, userId);
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
            return WSHelper.F3230_InsertChkOutXML(xmlInsterContent, tableXml, userId,  IsDelete);
        }

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
            return WSHelper.F3230_ChkOutFormXML(snapShotId, snapShotValue,  rowStart);
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

        #region Check Out
        
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

        ///// <summary>
        ///// ChkOutVSTerraGonXML
        ///// </summary>
        ///// <param name="snapShotId"></param>
        ///// <param name="snapShotValue"></param>
        ///// <param name="TableName"></param>
        ///// <param name="StartRow"></param>
        ///// <param name="RowendValue"></param>
        ///// <returns></returns>
        //public F3230FieldUseData ChkOutVSTerraGonXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        //{
        //    //RowendValue = 0;
        //    //return WSHelper.F3230_ChkOutTerraGonXML(snapShotId);
        //}

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

        #endregion Check Out

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
        

        /// <summary>
        /// Inserts the field element.
        /// </summary>
        /// <param name="fieldElement">The field element.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int InsertFieldElement(string fieldElement, int userId)
        {
            return WSHelper.F9065_InsertFieldElement(fieldElement, userId);
        }

        /// <summary>
        /// Gets the preview detail.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetail">The snap shot detail.</param>
        /// <returns>F9065FieldUseData</returns>
        public F9065FieldUseData.ListPreviewDetailTableDataTable GetPreviewDetail(int snapShotId, string snapShotDetail)
        {
            return WSHelper.F9065_GetPreviewDetail(snapShotId, snapShotDetail).ListPreviewDetailTable;
        }

        /// <summary>
        /// Inserts the application configuration.
        /// </summary>
        /// <param name="configXml">The config XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int InsertApplicationConfiguration(string configXml, int userId)
        {
            return WSHelper.F9065_InsertApplicationConfiguration(configXml, userId);
        }

        #region F9065_GetcfgConfiguration

        /// <summary>
        /// To get Configruation Value
        /// </summary>
        /// <param name="cfgname">The cfgname.</param>
        /// <returns>
        /// Typed dataset containing the Configruation Value.
        /// </returns>
        public F9065FieldUseData GetcfgConfiguration(string cfgname)
        {
            return WSHelper.F9065_GetcfgConfiguration(cfgname);
        }

        #endregion F9065_GetcfgConfiguration

        #region WorkItems Methods

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion WorkItems Methods
    }
}
