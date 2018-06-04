
// -------------------------------------------------------------------------------------------
// <copyright file="CommentsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access comment related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------
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
    /// Main Class for the Comments Component
    /// </summary>
    public static class CommentsComp
    {
        #region Comments

        #region SaveComments

        /// <summary>
        /// Save the comment entered by the userid.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="commentDate">The comment date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="comments">The comment to be saved.</param>
        /// <param name="printOnReceipt">The print on receipt.</param>
        /// <param name="publicComment">The public comment.</param>
        /// <param name="ispriority">The ispriority.</param>
        /// <param name="isroll">The isroll.</param>
        /// <param name="CommentPriorityId">The Comment PriorityId</param>
        public static void SaveComments(int commentId, int formId, int keyId, DateTime commentDate, int userId, string comments, int printOnReceipt, int publicComment, int ispriority, int isroll, int commentPriorityId)
        {
            Hashtable ht = new Hashtable();
            if (commentId == 0)
            {
                ht.Add("@CommentID", DBNull.Value);
            }
            else
            {
                ht.Add("@CommentID", commentId);
            }

            ht.Add("@Form", formId);
            ht.Add("@KeyID", keyId);
            ht.Add("@CommentDate", commentDate);
            ht.Add("@UserID", userId);
            ht.Add("@Comment", comments);
            ht.Add("@IsPublic", publicComment);
            ht.Add("@IsPrint", printOnReceipt);
            ht.Add("@IsHighPriority", ispriority);
            ht.Add("@IsRoll", isroll);
            ht.Add("@CommentPriorityID", commentPriorityId);
            DataProxy.ExecuteSP("f9075_pcins_Comment", ht);
        }

        #endregion SaveComments

        #region DeleteComments

        /// <summary>
        /// Delete the comment based on the commentid, formid and keyid.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="commentId">The comment id.</param>
        /// <param name="userId">The userId.</param>
        public static void DeleteComments(int keyId, int formId, int commentId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@KeyID", keyId);
            ht.Add("@Form", formId);
            ht.Add("@CommentID", commentId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f9075_pcdel_Comment", ht);
        }

        #endregion DeleteComments

        #region GetComments

        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The dataset containing the comments.</returns>
        public static CommentsData GetComments(int keyId, int formId, int userId)
        {
            CommentsData commentsData = new CommentsData();
            Hashtable ht = new Hashtable();
            ht.Add("@KeyID", keyId);
            ht.Add("@Form", formId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(commentsData.GetComments, "f9075_pclst_Comment", ht);
            return commentsData;
            ////return DataProxy.FetchDataSet("f9075_pcget_Comment", ht);
        }

        /// <summary>
        /// Gets the comments based on the keyid, formid.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>The dataset containing the comment and priority.</returns>
        public static CommentsData F9075_GetComment(int keyId, int formId)
        {
            CommentsData commentsData = new CommentsData();
            Hashtable ht = new Hashtable();
            ht.Add("@KeyID", keyId);
            ht.Add("@Form", formId);
            Utility.LoadDataSet(commentsData.GetComments, "f9075_pcget_Comment", ht);
            return commentsData;           
        }

        #endregion GetComments

        #region GetCommentsCount

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The typed dataset having count of comments.</returns>
        public static CommentsData GetCommentsCount(int formId, int keyId, int userId)
        {
            CommentsData commentsData = new CommentsData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            ht.Add("@KeyID", keyId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(commentsData.GetCommentsCount, "f9075_pcget_CommentsCount", ht);
            return commentsData;
            ////return DataProxy.FetchSPOutput("f9075_pcget_CommentsCount", ht);
        }

        #endregion GetCommentsCount

        #region GetCommentsConfigDetails

        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the comments.</returns>
        public static CommentsData GetConfigDetails(string configName)
        {
            CommentsData commentsData = new CommentsData();
            Hashtable ht = new Hashtable();
            ht.Add("@CfgName", configName);
            Utility.LoadDataSet(commentsData.GetCommentsConfigDetails, "f9020_pcget_Configuration", ht);
            return commentsData;

            ////return DataProxy.FetchDataSet("f9020_pcget_Configuration", ht);
        }

        #endregion

        /// <summary>
        /// For Testing Purpose added this method. later stage it should be removed.
        /// </summary>
        /// <param name="msversionId">The msversionId.</param>
        /// <returns>Connection string.</returns>
        public static CommonData GetConnectionString(int msversionId)
        {
            CommonData connectionStringData = new CommonData();
            Hashtable ht = new Hashtable();
            ht.Add("@MSVersionID", msversionId);
            Utility.LoadDataSet(connectionStringData.ConnectionStringDataTable, "f36000_pcget_M&S_ConnectionString", ht);
            return connectionStringData;         
        }

        #endregion Comments
    }
}
