//--------------------------------------------------------------------------------------------
// <copyright file="F2550WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2550WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 Aug 06        JYOTHI              Created
//*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.EventBroker;
using System.Data;
using Microsoft.Practices.CompositeUI.Utility;
using TerraScan.SmartParts;
using TerraScan.BusinessEntities;
using TerraScan.Helper;

namespace D2550
{
    /// <summary>
    /// F2550WorkItem
    /// </summary>
    public class F2550WorkItem : WorkItem
    {
        /// <summary>
        /// F2550_s the list parcel details.
        /// </summary>
        /// <param name="parcelID">The parcel ID.</param>
        /// <returns>Typed DataSet</returns>
        public F2550TaxRollCorrectionData F2550_ListParcelDetails(string parcelID, string scheduleID, string stateID, string centralXmlIds)
        {
            return WSHelper.F2550_ListParcelDetails(parcelID, scheduleID, stateID,centralXmlIds);
        }

        /// <summary>
        /// F2550_s the exec tax roll corrections.
        /// </summary>
        /// <param name="parcelItems">The parcel items.</param>
        /// <returns></returns>
        public int F2550_ExecTaxRollCorrections(string parcelItems, int userId)
        {
            return WSHelper.F2550_ExecTaxRollCorrections(parcelItems, userId);
        }

        #region List Attachmet Details

        /// <summary>
        /// List attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>Typed DataSet</returns>
        public F2550TaxRollCorrectionData F2550_ListAttachmentDetails(int formId, string keyIds, int userId, int moduleId)
        {
            return WSHelper.F2550_ListAttachmentDetails(formId, keyIds, userId, moduleId);
        }

        #endregion List Attachmet Details

        #region Delete Attachment Details

        /// <summary>
        /// Delete attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        public void F2550_DeleteAttachmentDetails(int formId)
        {
            WSHelper.F2550_DeleteAttachmentDetails(formId);
        }

        #endregion Delete Attachment Details

        #region List Correction Code
        /// <summary>
        /// F2550_s the list correction code.
        /// </summary>
        /// <returns></returns>
        public F2550TaxRollCorrectionData F2550_ListCorrectionCode()
        {
            return WSHelper.F2550_ListCorrectionCode();
        }
        #endregion

        #region Insert Correction Parcels Temp Table
        /// <summary>
        /// F2550_s the save correction parcels temp.
        /// </summary>
        /// <param name="correctionId">The correction id.</param>
        /// <param name="correctionTempItems">The correction temp items.</param>
        /// <param name="corrParcelIds">The corr parcel ids.</param>
        /// <param name="statementsIds">The statements ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F2550_SaveCorrectionParcelsTemp(int? correctionId, string correctionTempItems, string corrParcelIds, string statementsIds, int userId)
        {
            return WSHelper.F2550_SaveCorrectionParcelsTemp(correctionId, correctionTempItems,corrParcelIds,statementsIds,userId);
        }
        #endregion

        #region Get FilePath

        /// <summary>
        /// Gets the files path
        /// </summary>
        /// <param name="source"> The source path of the file.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="extension"> The file extension.</param>
        /// <returns> The typed dataset containing the path of the file.</returns>
        public AttachmentsData.GetFilePathDataTable GetFilePath(string source, int formId, int keyId, string extension)
        {

            return WSHelper.GetFilePath(source, formId, keyId, extension,TerraScan.Common.TerraScanCommon.UserId).GetFilePath;
        }

        #endregion Get FilePath

        /// <summary>
        /// Gets the attachment items.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The typed dataset containing the attachment items.</returns>
        public AttachmentsData.GetAttachmentItemsDataTable GetAttachmentItems(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentItems(formId, keyId, userId).GetAttachmentItems;
        }

        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="userId"> The user id of the user logged in.</param>
        /// <returns> The typed dataset containing the comments.</returns>
        public CommentsData GetComments(int keyId, int formId, int userId)
        {
            return WSHelper.GetComments(keyId, formId, userId);
        }
        
        /// <summary>
        /// F2550_s the state of the get configured.
        /// </summary>
        /// <returns></returns>
        public F2550TaxRollCorrectionData F2550_GetConfiguredState()
        {
            return WSHelper.F2550_GetConfiguredState();
        }
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
    }
}
