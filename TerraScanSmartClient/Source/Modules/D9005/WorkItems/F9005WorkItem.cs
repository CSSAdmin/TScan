//--------------------------------------------------------------------------------------------
// <copyright file="F9005WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the GetAttachementFunctionName, GetAttachmentItems,
//  SaveAttachments, GetAttachmentCount, GetFilePath, DeleteAttachments, GetProgramId.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 03 Aug 06        VINOTHBABU         Created
//*********************************************************************************/

namespace D9005
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.CompositeUI;
    using System.Text;
    using TerraScan.Helper;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F9005WorkItem Class
    /// </summary>
    public class F9005WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the attachment count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="receiptId">The receipt id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The count of attachments.</returns>
        public static int GetAttachmentCount(int formId, int receiptId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, receiptId, userId);
        }

        /// <summary>
        /// Gets the Image Patha
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Config value.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        /// <summary>
        /// Deletes the attachments.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">The user id.</param>
        public void DeleteAttachments(int fileId, int userId)
        {
            WSHelper.DeleteAttachments(fileId, userId);
        }

        /// <summary>
        /// Gets the program id.
        /// </summary>
        /// <param name="fileTypeId">The file type id.</param>
        /// <returns>Returns fileTypeId</returns>
        public AttachmentsData.GetProgramIdDataTable GetProgramId(int fileTypeId)
        {
            return WSHelper.GetProgramId(fileTypeId).GetProgramId;
        }

        /// <summary>
        /// Get the attachment function name.
        /// </summary>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <returns> The typed dataset containing the attachment function name.</returns>
        public AttachmentsData GetAttachementFunctionName(int formId)
        {
            return WSHelper.GetAttachementFunctionName(formId);
        }

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
        /// Saves the attachments.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="fileTypeId">The file type id.</param>
        /// <param name="source">The source.</param>
        /// <param name="primary">The primary.</param>
        /// <param name="description">The description.</param>
        /// <param name="eventDate">The event date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="publicValue">The public value.</param>
        /// <param name="isroll">The is roll.</param>
        /// <param name="linktype">The linktype.</param>
        /// <param name="aurl">The aurl.</param>
        /// <param name="pfileid">The pfileid.</param>
        /// <param name="sourceConfig">The source config.</param>
        /// <returns>file ID.</returns>
        public AttachmentsData.SaveFilePathDataTable SaveAttachments(int? fileId, string extension, int formId, int keyId, int fileTypeId, string source, int primary, string description, string eventDate, int userId, int publicValue, int isroll, int linktype, string aurl, int pfileid, string sourceConfig)
        {
            return WSHelper.SaveAttachments(fileId, extension, formId, keyId, fileTypeId, source, primary, description, eventDate, userId, publicValue, isroll, linktype, aurl, pfileid, sourceConfig).SaveFilePath;
        }

        /// <summary>
        /// Gets the files path
        /// </summary>
        /// <param name="source"> The source path of the file.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="extension"> The file extension.</param>
        /// <param name="userId">userId</param>
        /// <returns> The typed dataset containing the path of the file.</returns>
        public AttachmentsData.GetFilePathDataTable GetFilePath(string source, int formId, int keyId, string extension, int userId)
        {
            return WSHelper.GetFilePath(source, formId, keyId, extension, userId).GetFilePath;
        }

        /// <summary>
        /// Gets the original file path.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>FilePath</returns>
        public string GetOriginalFilePath(int fileId, int userId)
        {
            return WSHelper.F9005_GetOriginalFilePath(fileId, userId);
        }

        /// <summary>
        /// Create Thumbnails
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">The userId.</param>
        public void GenerateThumbnail(int? fileId, int userId,string fileIdXml)
        {
            WSHelper.GenerateThumbnail(fileId, userId, fileIdXml);
        }

        /// <summary>
        /// Override Method for OnRunStarted
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Override Method for OnActivated
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
