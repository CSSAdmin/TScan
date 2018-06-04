// -------------------------------------------------------------------------------------------
// <copyright file="AttachmentsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access attachment related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    ///<summary>
    ///Attachments Components.
    ///</summary>
    public static class AttachmentsComp
    {
        #region Attachments

        #region GetAttachmentCount

        /// <summary>
        /// Gets the attachment count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="receiptId">The receipt id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of attachments.</returns>
        public static int GetAttachmentCount(int formId, int receiptId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            ht.Add("@ReceiptID", receiptId);
            ht.Add("@UserID", userId);
            ////return DataProxy.FetchSPOutput("f9005_pcget_AttachmentCount", ht);
            return Utility.FetchSPOutput("f9005_pcget_AttachmentCount", ht);
        }

        #endregion GetAttachmentCount

        #region GetAttachmentItems

        /// <summary>
        /// Gets the attachment items.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The typed dataset containing the attachment items.</returns>
        public static AttachmentsData GetAttachmentItems(int formId, int keyId, int userId)
        {
            AttachmentsData attachmentsData = new AttachmentsData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            ht.Add("@KeyID", keyId);
            //// ht.Add("@UserID", userId);            
            Utility.LoadDataSet(attachmentsData.GetAttachmentItems, "f9005_pclst_Attachment", ht);
            return attachmentsData;

            ////return DataProxy.FetchDataSet("f9005_pcget_Attachment", ht);
        }

        #endregion GetAttachmentItems

        #region GetAttachementFunctionName

        /// <summary>
        /// Get the attachment function name.
        /// </summary>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <returns> The typed dataset containing the attachment function name.</returns>
        public static AttachmentsData GetAttachementFunctionName(int formId)
        {
            AttachmentsData attachmentsData = new AttachmentsData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            Utility.LoadDataSet(attachmentsData.GetAttachementFunctionName, "f9005_pcget_AttachmentFunctionName", ht);
            return attachmentsData;

            ////return DataProxy.FetchDataSet("f9005_pcget_AttachmentFunctionName", ht);
        }

        #endregion GetAttachementFunctionName

        #region SaveAttachments


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
        /// <param name="isroll">The isroll.</param>
        /// <param name="linktype">The linktype.</param>
        /// <param name="aurl">The aurl.</param>
        /// <param name="pfileid">The pfileid.</param>
        /// <param name="sourceConfig">Source Config from tts_cfg</param>
        /// <returns>File Path data</returns>
        public static AttachmentsData SaveAttachments(int? fileId, string extension, int formId, int keyId, int fileTypeId, string source, int primary, string description, string eventDate, int userId, int publicValue, int isroll, int linktype, string aurl, int pfileid, string sourceConfig)
        {
            AttachmentsData attachmentsData = new AttachmentsData();
            Hashtable ht = new Hashtable();
            if (fileId != null)
            {
                ht.Add("@FileID", fileId);
            }
            else
            {
                ht.Add("@FileID", DBNull.Value);
            }
            ht.Add("@Extension", extension);
            ht.Add("@Form", formId);
            ht.Add("@KeyID", keyId);
            ht.Add("@FileTypeID", fileTypeId);
            ht.Add("@Source", source);
            ht.Add("@Primary", primary);
            ht.Add("@Description", description);
            ht.Add("@EventDate", eventDate);
            ht.Add("@UserID", userId);
            ht.Add("@IsPublic", publicValue);
            ht.Add("@IsRoll", isroll);
            ht.Add("@LinkType", linktype);
            ht.Add("@AURL", aurl);
            ht.Add("@PFileID", pfileid);
            ht.Add("@SourceConfig",sourceConfig);
            Utility.LoadDataSet(attachmentsData.SaveFilePath, "f9005_pcins_Attachment", ht);
            return attachmentsData;
        }
         
        #endregion SaveAttachments

        #region DeleteAttachments

        /// <summary>
        /// Deletes the attachments.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">The userId.</param>
        public static void DeleteAttachments(int fileId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FileID", fileId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f9005_pcdel_Attachment", ht);
        }

        #endregion DeleteAttachments

        #region GetProgramId

        /// <summary>
        /// GetProgramId
        /// </summary>
        /// <param name="fileTypeId">The integer name of the file type.</param>
        /// <returns>
        /// The typed dataset containing the attachment file type.
        /// </returns>
        public static AttachmentsData GetProgramId(int fileTypeId)
        {
            AttachmentsData attachmentsData = new AttachmentsData();
            Hashtable ht = new Hashtable();
            ht.Add("@FileTypeID", fileTypeId);
            Utility.LoadDataSet(attachmentsData.GetProgramId, "f9005_pcget_AttachmentFileType", ht);
            return attachmentsData;

            ////return DataProxy.FetchDataSet("f9005_pcget_AttachmentFileType", ht);
        }

        #endregion GetProgramID

        #region GetFilePath

        /// <summary>
        /// Gets the files path
        /// </summary>
        /// <param name="source"> The source path of the file.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="extension"> The file extension.</param>
        /// <param name="userId">userId</param>
        /// <returns> The typed dataset containing the path of the file.</returns>
        public static AttachmentsData GetFilePath(string source, int formId, int keyId, string extension,int userId)
        {
            AttachmentsData attachmentsData = new AttachmentsData();
            Hashtable ht = new Hashtable();
            ht.Add("@Source", source);
            ht.Add("@Form", formId);
            ht.Add("@KeyID", keyId);
            ht.Add("@Extension", extension);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(attachmentsData.GetFilePath, "f9005_pcget_AttachmentFilePath", ht);
            return attachmentsData;
        }
        
        /// <summary>
        /// F9005_s the get original file path.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The original file path</returns>
        public static string F9005_GetOriginalFilePath(int fileId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FileID", fileId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyString("f9005_pcget_AttachmentFile", ht);
        }

        #endregion GetFilePath

        #region Create Thumbnails

        /// <summary>
        /// Create Thumbnails
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">The userId.</param>
        public static void GenerateThumbnail(int? fileId, int userId, string fileIdXml)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FileID", fileId);
            ht.Add("@UserID", userId);
            ht.Add("@FileIDXML", fileIdXml);
            DataProxy.ExecuteSP("f9005_pcexe_GenerateThumbnails", ht);
        }

        #endregion Create Thumbnails

        #endregion
    }
}