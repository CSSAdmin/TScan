//--------------------------------------------------------------------------------------------
// <copyright file="F9066WorkItem.cs" company="Congruent">
//	Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9060WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 23 Nov 07      D.LathaMaheswari      Created
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
    /// F9066WorkItem
    /// </summary>
    public class F9066WorkItem : WorkItem
    {
        #region BusinessProcesses

        #region GetAuditCount
        ///// <summary>
        ///// Get Audit Count
        ///// </summary>
        ///// <returns>Integer</returns>
        //public int F9066_GetAuditCount()
        //{
        //    return WSHelper.F9066_GetAuditCount();
        //}

        #endregion GetAuditCount

        #region GetCheckInXML
        ///// <summary>
        ///// Get Check In Details
        ///// </summary>
        ///// <returns>DataSet</returns>
        //public F9066CheckInData F9066_GetCheckInData()
        //{
        //    return WSHelper.F9066_GetCheckInData();
        //}
        #endregion GetCheckInXML

        #region SaveXML
        /// <summary>
        /// Save the values
        /// </summary>
        /// <param name="insertValue">insertValue</param>
        /// <param name="updateValue">updateValue</param>
        public void F9066_SaveData(string insertValue, string updateValue)
        {
            WSHelper.F9066_SaveData(insertValue, updateValue);
        }
        #endregion SaveXML

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
        public F3230CheckInData ChkInCommentXML( string TableName, int StartRow, out int RowendValue)
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
        public F3230CheckInData ChkInEstimateXML( string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInEstimateXML( TableName, StartRow, out  RowendValue);
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
        public F3230CheckInData ChkInFileXML( string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInFileXML( TableName, StartRow, out  RowendValue);
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
        public F3230CheckInData ChkInLandValuesXML( string TableName, int StartRow, out int RowendValue)
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
        public F3230CheckInData ChkInLandXML( string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInLandXML( TableName, StartRow, out  RowendValue);
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
        public F3230CheckInData ChkInMiscXML( string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInMiscXML( TableName, StartRow, out  RowendValue);
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
        public F3230CheckInData ChkInMSC_EstimateXML( string TableName, int StartRow, out int RowendValue)
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
        public F3230CheckInData ChkInObjectXML( string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInObjectXML( TableName, StartRow, out  RowendValue);
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
        public F3230CheckInData ChkInParcelValueXML( string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInParcelValueXML( TableName, StartRow, out  RowendValue);
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
        public F3230CheckInData ChkInParcelXML( string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInParcelXML( TableName, StartRow, out  RowendValue);
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
        public F3230CheckInData ChkInTerraGonXML( string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInTerraGonXML( TableName, StartRow, out  RowendValue);
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
        public F3230CheckInData ChkInType2XML( string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInType2XML( TableName, StartRow, out  RowendValue);
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
        public F3230CheckInData ChkInType6XML( string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return WSHelper.F3230_ChkInType6XML( TableName, StartRow, out  RowendValue);
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

        #endregion


        #region DeleteData
        ///// <summary>
        ///// Delete the values
        ///// </summary>
        ///// <returns>Integer</returns>
        //public int F9066_DeleteData()
        //{
        //    return WSHelper.F9066_DeleteData();
        //}
        #endregion DeleteData
        #endregion BusinessProcesses

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
