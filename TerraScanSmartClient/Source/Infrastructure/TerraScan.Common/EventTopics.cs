//--------------------------------------------------------------------------------------------
// <copyright file="EventTopics.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 May 06		Siva        	    Created
// 24 May 06		Thilak raj        	All Property is changed to static variables
//*********************************************************************************/

namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class Contains All Topic for Event Subcription
    /// </summary>
    public static class EventTopics
    {

        #region D1100 SmartParts Event Topics
        
        /// <summary>
        /// D1100_F1107_AffidavitFormSmartPart_AffidavitFormButtonClick
        /// </summary>
        public const string D1100_F1107_AffidavitFormSmartPart_AffidavitFormButtonClick = "D1100_F1107_AffidavitFormSmartPart_AffidavitFormButtonClick";

        /// <summary>
        /// D1100_F1107_AffidavitFormSmartPart_ReceiptFormButtonClick
        /// </summary>
        public const string D1100_F1107_AffidavitFormSmartPart_ReceiptFormButtonClick = "D1100_F1107_AffidavitFormSmartPart_ReceiptFormButtonClick";

        /// <summary>D1100_F1108_AffidavitFormSmartPart_ReportButtonClick
        /// D1100_F1107_AffidavitFormSmartPart_ViewAfdvtButtonClick
        /// </summary>
        public const string D1100_F1107_AffidavitFormSmartPart_ViewAfdvtButtonClick = "D1100_F1107_AffidavitFormSmartPart_ViewAfdvtButtonClick";

        /// <summary>
        /// D1100_F1107_AffidavitFormSmartPart_ReportButtonClick 
        /// </summary>
        public const string D1100_F1108_AffidavitFormSmartPart_ReportButtonClick = "D1100_F1108_AffidavitFormSmartPart_ReportButtonClick";

        /// <summary>
        /// D1100_F1109_AffidavitFormSmartPart_SubmitQueueButtonClick
        /// </summary>
        public const string D1100_F1109_AffidavitFormSmartPart_SubmitQueueButtonClick = "D1100_F1109_AffidavitFormSmartPart_SubmitQueueButtonClick";
        
        /// <summary>
        /// D1100_F1107_AffidavitFormSmartPart_DisableButtons
        /// </summary>
        public const string D1100_F1107_AffidavitFormSmartPart_DisableButtons = "D1100_F1107_AffidavitFormSmartPart_DisableButtons";

        /// <summary>
        /// D1100_F1107_AffidavitFormSmartPart_EnableButtons
        /// </summary>
        public const string D1100_F1107_AffidavitFormSmartPart_EnableButtons = "D1100_F1107_AffidavitFormSmartPart_EnableButtons";

        /// <summary>
        /// D1100_F1100_F1100SmartPart_SetAutoPrint
        /// </summary>
        public const string D1100_F1100_ExciseTaxSmartPart_SetAutoPrint = "D1100_F1100_F1100SmartPart_SetAutoPrint";

        #endregion

        #region Common SmartPart Event Topics

        #region Status Bar SmartPart Event Topics

        /// <summary>
        /// D9001_TerrascanSmartParts_StatusBarSmartPart_FilteredButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_StatusBarSmartPart_FilteredButtonClick = "D9001_TerrascanSmartParts_StatusBarSmartPart_FilteredButtonClick";
        
        /// <summary>
        /// D9001_TerrascanSmartParts_StatusBarSmartPart_DelinquentButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_StatusBarSmartPart_DelinquentButtonClick = "D9001_TerrascanSmartParts_StatusBarSmartPart_DelinquentButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartParts_StatusBarSmartPart_AutoPrintOnButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_StatusBarSmartPart_AutoPrintOnButtonClick = "D9001_TerrascanSmartParts_StatusBarSmartPart_AutoPrintOnButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartParts_StatusBarSmartPart_HelpRealEstateButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_StatusBarSmartPart_HelpRealEstateButtonClick = "D9001_TerrascanSmartParts_StatusBarSmartPart_HelpRealEstateButtonClick";

        #endregion

        #region Operation SmartPart Event Topics

        /// <summary>
        /// D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick = "D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton
        /// </summary>
        public const string D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton = "D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton";

        /// <summary>
        /// D9001_TerrascanSmartParts_OperationSmartPart_SetButtonMode
        /// </summary>
        public const string D9001_TerrascanSmartParts_OperationSmartPart_SetButtonMode = "D9001_TerrascanSmartParts_OperationSmartPart_SetButtonMode";

        /// <summary>
        /// D9001_TerrascanSmartParts_OperationSmartPart_SetPermissions
        /// </summary>
        public const string D9001_TerrascanSmartParts_OperationSmartPart_SetPermissions = "D9001_TerrascanSmartParts_OperationSmartPart_SetPermissions";

        /// <summary>
        /// D9001_TerrascanSmartParts_OperationSmartPart_MakeButtonEnable
        /// </summary>
        public const string D9001_TerrascanSmartParts_OperationSmartPart_MakeButtonEnable = "D9001_TerrascanSmartParts_OperationSmartPart_MakeButtonEnable";

        /// <summary>
        /// D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton
        /// </summary>
        public const string D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton = "D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton";

        #endregion

        #region FormHeader SmartPart Event Topics


        #endregion

        #region Additional Operation SmartPart Event Topics

        /// <summary>
        /// D9001_TerrascanSmartParts_AdditionalOperationSmartPart_EnableButtons
        /// </summary>
        public const string D9001_TerrascanSmartParts_AdditionalOperationSmartPart_EnableButtons = "D9001_TerrascanSmartParts_AdditionalOperationSmartPart_EnableButtons";

        /// <summary>
        /// D9001_TerrascanSmartParts_AdditionalOperationSmartPart_AttachmentButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_AdditionalOperationSmartPart_AttachmentButtonClick = "D9001_TerrascanSmartParts_AdditionalOperationSmartPart_AttachmentButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartParts_AdditionalOperationSmartPart_CommentButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_AdditionalOperationSmartPart_CommentButtonClick = "D9001_TerrascanSmartParts_AdditionalOperationSmartPart_CommentButtonClick";

        public const string D9001_TerrascanSmartParts_AdditionalOperationSmartPart_SetButtonText = "D9001_TerrascanSmartParts_AdditionalOperationSmartPart_SetButtonText";

        #endregion

        #region ToolBox SmartPart Event Topics


        #endregion

        #region Utility Operation SmartPart Event Topics


        #endregion

        /// <summary>
        /// D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader
        /// </summary>
        public const string D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader = "D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader";

        /// <summary>
        /// D9001_TerrascanSmartParts_SearchGroupSmartPart_SearchButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_SearchGroupSmartPart_SearchButtonClick = "D9001_TerrascanSmartParts_SearchGroupSmartPart_SearchButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartPart_SearchGroupSmartPart_ClearButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_SearchGroupSmartPart_ClearButtonClick = "D9001_TerrascanSmartParts_SearchGroupSmartPart_ClearButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartPart_SearchGroupSmartPart_CancelButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_SearchGroupSmartPart_CancelButtonClick = "D9001_TerrascanSmartParts_SearchGroupSmartPart_CancelButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartPart_SearchGroupSmartPart_CancelButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord = "D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord";

        /// <summary>
        /// D9001_TerrascanSmartPart_SearchGroupSmartPart_CancelButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount = "D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount";

        /// <summary>
        /// D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryByFromButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryByFromButtonClick = "D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryByFromButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartParts_ToolBoxSmartPart_ClearFilterButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_ToolBoxSmartPart_ClearFilterButtonClick = "D9001_TerrascanSmartParts_ToolBoxSmartPart_ClearFilterButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryUtilityButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryUtilityButtonClick = "D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryUtilityButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartParts_ToolBoxSmartPart_SnapshotUtilityButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_ToolBoxSmartPart_SnapshotUtilityButtonClick = "D9001_TerrascanSmartParts_ToolBoxSmartPart_SnapshotUtilityButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartParts_ReportActionSmartPart_PrintButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_ReportActionSmartPart_PrintButtonClick = "D9001_TerrascanSmartParts_ReportActionSmartPart_PrintButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick = "D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartParts_ReportActionSmartPart_EmailButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_ReportActionSmartPart_EmailButtonClick = "D9001_TerrascanSmartParts_ReportActionSmartPart_EmailButtonClick";

        /// <summary>
        /// D9001_TerrascanSmartParts_ReportActionSmartPart_DetailsButtonClick
        /// </summary>
        public const string D9001_TerrascanSmartParts_ReportActionSmartPart_DetailsButtonClick = "D9001_TerrascanSmartParts_ReportActionSmartPart_DetailsButtonClick";

        #endregion

        /// <summary>
        /// D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus
        /// </summary>
        public const string D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus = "D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus";

        /// <summary>   
        /// D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord
        /// </summary>
        public const string D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord = "D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord";

        ///// <summary>
        ///// D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount
        ///// </summary>
        //public const string D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount = "D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount";

        ///// <summary>
        ///// D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord
        ///// </summary>
        //public const string D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord = "D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord";

        /// <summary>
        /// D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons
        /// </summary>
        public const string D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons = "D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons";

        /// <summary>
        /// D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated
        /// </summary>
        public const string D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated = "D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated";

        /// <summary>
        /// D9001_ShellForm_FormActivate
        /// </summary>
        public const string D9001_ShellForm_FormActivate = "D9001_ShellForm_FormActivate";

        /// <summary>
        /// D9001_ShellForm_SetActiveLinkColor
        /// </summary>
        public const string D9001_ShellForm_SetActiveLinkColor = "D9001_ShellForm_SetActiveLinkColor";
        
        /// <summary>
        /// D9001_ShellForm_GetFormStatus
        /// </summary>
        public const string D9001_ShellForm_GetFormStatus = "D9001_ShellForm_GetFormStatus";

        /// <summary>
        /// D9001_ShellForm_SetFormCancelButton
        /// </summary>
        public const string D9001_ShellForm_SetFormCancelButton = "D9001_ShellForm_SetFormCancelButton";

        /// <summary>
        /// D9001_ShellForm_SetFormDefaultButton
        /// </summary>
        public const string D9001_ShellForm_SetFormDefaultButton = "D9001_ShellForm_SetFormDefaultButton";

        /// <summary>
        /// D9001_ShellForm_SendOptionalParameters
        /// </summary>
        public const string D9001_ShellForm_SendOptionalParameters = "D9001_ShellForm_SendOptionalParameters";

        /// <summary>
        /// D9001_ShellForm_NavigationPanelSmartPart_ShowForm
        /// </summary>
        public const string D9001_ShellForm_NavigationPanelSmartPart_ShowForm = "D9001_ShellForm_NavigationPanelSmartPart_ShowForm";
        
        /// <summary>
        /// D9001_ShellForm_SetCancelButton
        /// </summary>
        public const string D9001_ShellForm_SetCancelButton = "D9001_ShellForm_SetCancelButton";

        /// <summary>
        /// D9001_ShellForm_NavigationPanelSmartPart_LogoutEvent
        /// </summary>
        public const string D9001_ShellForm_NavigationPanelSmartPart_LogoutEvent = "D9001_ShellForm_NavigationPanelSmartPart_LogoutEvent";

        /// <summary>
        /// D9001_BaseSmartPart_formClose
        /// </summary>
        public const string D9001_BaseSmartPart_formClose = "D9001_BaseSmartPart_formClose";

        /// <summary>
        /// D9001_ShellForm_NavigationPanelSmartPart_ApplicationStatusLinkLabelClickEvent
        /// </summary>
        public const string D9001_ShellForm_NavigationPanelSmartPart_ApplicationStatusLinkLabelClickEvent = "D9001_ShellForm_NavigationPanelSmartPart_ApplicationStatusLinkLabelClickEvent";

        /// <summary>
        /// D9001_TerrascanSmartPart_FooterSmartpart
        /// </summary>
        public const string D9001_TerrascanSmartParts_FooterSmartPart_GetActiveKeyid = "D9001_TerrascanSmartParts_FooterSmartPart_GetActiveKeyid";

        /// <summary>
        /// D9001_TerrascanSmartParts_SetMasterGnrlPropertis
        /// </summary>
        public const string D9001_TerrascanSmartParts_SetMasterGnrlPropertis = "D9001_TerrascanSmartParts_SetMasterGnrlPropertis";
        
        /// <summary>
        /// D9001_TerrascanSmartParts_EnableReportAdditonalProperties
        /// </summary>
        public const string D9001_TerrascanSmartParts_EnableReportAdditonalProperties = "D9001_TerrascanSmartParts_EnableReportAdditonalProperties";

        /// <summary>
        /// D9001_ShellForm_NavigationPanelSmartPart_WindowsLinkLabelClickEvent
        /// </summary>
        public const string D9001_ShellForm_NavigationPanelSmartPart_WindowsLinkLabelClickEvent = "D9001_ShellForm_NavigationPanelSmartPart_WindowsLinkLabelClickEvent";

        ///<summary>
        /// Quick Find Operation
        /// </Summary>
        public const string F9030_NavigationRecord = "F9030_NavigationRecord";

    }
}


