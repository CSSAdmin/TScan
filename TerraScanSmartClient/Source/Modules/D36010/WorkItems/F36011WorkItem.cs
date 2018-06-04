// -------------------------------------------------------------------------------------------
// <copyright file="F36011WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F36011</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28/6/2007        M.Vijayakumar       ///Created
// 26/3/2009        M.Sadha Shivudu     Added methods for TSCO# 5176
// 20/6/2010        P. Manoj Kumar      Change Order TSCO# 11442 
// -------------------------------------------------------------------------------------------

namespace D36010
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
    /// F36011WorkItem Class file
    /// </summary>
    public class F36011WorkItem : WorkItem
    {
        #region F36011 Misc Improvement Overview

        #region List Depr Table

        /// <summary>
        /// To List the Depr Table details
        /// </summary>
        /// <param name="valueSliceId">ValuSliceID</param>
        /// <returns>Typed dataset containing the Depr Table details</returns>
        public F36011MiscImprovementOverviewData F36011_ListDeprTable(int valueSliceId)
        {
            return WSHelper.F36011_ListDeprTable(valueSliceId);
        }

        #endregion List Depr Table

        #region List Misc Code

        /// <summary>
        ///To List Misc Code Details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed dataset containing the Misc Code Details</returns>
        public F36011MiscImprovementOverviewData F36011_ListMiscCode(int valueSliceId)
        {
            return WSHelper.F36011_ListMiscCode(valueSliceId);
        }

        #endregion List Misc Code

        #region List Misc Improvements

        /// <summary>
        /// To List Misc Improvements details.
        /// </summary>
        /// <param name="miscId">The misc id.</param>
        /// <returns>Typed dataset containing the Misc Improvements details</returns>
        public F36011MiscImprovementOverviewData F36011_ListMiscImprovements(int miscId)
        {
            return WSHelper.F36011_ListMiscImprovements(miscId);
        }

        #endregion List Misc Improvements

        #region List MICatalog Code

        /// <summary>
        /// To List Catalog Code Details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed Dataset containg the MICatalog Code Details</returns>
        public F36011MiscImprovementOverviewData F36011_ListCatalogCode(int valueSliceId)
        {
            return WSHelper.F36011_ListCatalogCode(valueSliceId);
        }

        #endregion List MICatalog Code

        #region Delete MICode

        /// <summary>
        /// F36011_DeleteMICode
        /// </summary>
        /// <param name="miscId">miscId</param>
        /// <param name="userId">userId</param>
        public void F36011_DeleteMICode(int miscId, int userId)
        {
            WSHelper.F36011_DeleteMICode(miscId, userId);
        }

        #endregion Delete MICode

        #region Delete Value Slice

        /// <summary>
        /// F35001_DeleteValueSlice
        /// </summary>
        /// <param name="valueSliceId">valueSliceId</param>
        /// <param name="userId">userId</param>
        public void F35001_DeleteValueSlice(int valueSliceId, int userId)
        {
            WSHelper.F35001_DeleteValueSlice(valueSliceId, userId);
        }

        #endregion Delete Value Slice

        #region Save Misc Improvements

        /// <summary>
        /// To Save the Misc Improvements Overview
        /// </summary>
        /// <param name="miscmId">mid</param>
        /// <param name="miscItems">xml string containing the Misc Improvents Overview Details</param>
        /// <param name="userId">User id</param>
        /// <returns>Integer value containing the key id</returns>
        public int F36011_SaveMiscImprovement(int miscmId, string miscItems, int userId)
        {
            return WSHelper.F36011_SaveMiscImprovement(miscmId, miscItems, userId);
        }

        #endregion Save Misc Improvements

        #region List Qualit Comm

        /// <summary>
        /// F36011_s the list quality comm.
        /// </summary>
        /// <returns>Typed dataset containing the Quality Comm list table</returns>
        public F36011MiscImprovementOverviewData F36011_ListQualityComm()
        {
            return WSHelper.F36011_ListQualityComm();
        }

        #endregion List Qualit Comm

        #region List Qualit Res

        /// <summary>
        /// F36011_s the list quality res.
        /// </summary>
        /// <returns>Typed dataset containing the Quality Res table</returns>
        public F36011MiscImprovementOverviewData F36011_ListQualityRes()
        {
            return WSHelper.F36011_ListQualityRes();
        }

        #endregion List Qualit Comm

        #region List Condition

        /// <summary>
        /// F36011_s the list Condition
        /// </summary>
        /// <returns>Typed dataset containing the Condition table</returns>
        public F36011MiscImprovementOverviewData F36011_ListCondition()
        {
            return WSHelper.F36011_ListCondition();
        }

        #endregion List Condition

        #region List DeprFuncCategory

        /// <summary>
        /// F36011_s the list Depr FuncCategory
        /// </summary>
        /// <returns>Typed dataset containing the Depr FuncCategory table</returns>
        public F36011MiscImprovementOverviewData F36011_ListDeprFuncCategory()
        {
            return WSHelper.F36011_ListDeprFuncCategory();
        }

        #endregion List DeprFuncCategory

        #region List MiscCatalogChoice

        /// <summary>
        /// F36012_s the list misc catalog choice.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        /// <returns>Typed dataset containing the MiscCatalogChoice table</returns>
        public F36011MiscImprovementOverviewData F36012_ListMiscCatalogChoice(int miscCodeId, int fieldNum)
        {
            return WSHelper.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
        }

        #endregion List MiscCatalogChoice

        #endregion F36011 Misc Improvement Overview

        #region F36010 Misc Improvement Catalog

        #region Get Misc Improvement Catalog

        /// <summary>
        /// Get Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeId</param>
        /// <returns>To get the Misc Improvement CataLog details</returns>
        public F36010MiscImprovementCatalog F36010_GetMiscImprovementCatalog(int miscCodeId)
        {
            return WSHelper.F36010_GetMiscImprovementCatalog(miscCodeId);
        }

        #endregion Get Misc Improvement Catalog

        #endregion F36010 Misc Improvement Catalog

        #region Attachment

        /// <summary>
        /// Gets the Attachments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
        }

        #endregion Attachment

        #region Comments

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        #endregion Comments

        #region Depreciation Percentage

        /// <summary>
        /// F36000_GetDeprPercentage
        /// </summary>
        /// <param name="age">The age.</param>
        /// <param name="objectCondition">The object condition.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <returns>string</returns>
        public string F36000_GetDeprPercentage(int age, decimal objectCondition, int deprTableId)
        {
            return WSHelper.F36000_GetDeprPercentage(age, objectCondition, deprTableId);
        }

        #endregion Depreciation Percentage

        #region To Get Configuration Roll Year

        /// <summary>
        /// Gets the config Roll Year.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #endregion To Get Configuration Roll Year


        #region Recalculate Misc Improvement

        /// <summary>
        /// F36011_s the Recalculae Misc Improvement
        /// </summary>
        /// <returns> recalculate physical and functional reciation values </returns>
        public F36011MiscImprovementOverviewData F36011_RecalcMiscImprovement(  bool withprimary, int? yearIn, string condition, int? economicLife, int? effectiveAge, decimal? physDeprPerc, decimal? funcDeprPerc, decimal? BaseCost, decimal? physDepr, decimal? funcDepr,int valueSliceId,int miscCodeId)
        {
            return WSHelper.F36011_RecalcMiscImprovement(withprimary, yearIn, condition, economicLife, effectiveAge, physDeprPerc, funcDeprPerc, BaseCost, physDepr,funcDepr,valueSliceId, miscCodeId);
        }

        #endregion


    }
}
