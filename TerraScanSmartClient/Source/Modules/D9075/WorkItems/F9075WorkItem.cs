// -------------------------------------------------------------------------------------------
// <copyright file="F9075WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Statement</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
// -------------------------------------------------------------------------------------------
namespace D9075
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
    /// form F9075 WorkItem
    /// </summary>
    public class F9075WorkItem : WorkItem
    {
        /// <summary>
        /// Saves the comments.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="commentDate">The comment date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="printOnReceipt">The print on receipt.</param>
        /// <param name="publicComment">The public comment.</param>
        /// <param name="ispriority">The is priority.</param>
        /// <param name="commentPriorityId">The commentPriorityId.</param>
        public void SaveComments(int commentId, int formId, int keyId, DateTime commentDate, int userId, string comments, int printOnReceipt, int publicComment, int ispriority, int isRoll, int commentPriorityId)
        {
            WSHelper.SaveComments(commentId, formId, keyId, commentDate, userId, comments, printOnReceipt, publicComment, ispriority, isRoll, commentPriorityId);
        }

        /// <summary>
        /// Delete the comment based on the commentid, formid and keyid.
        /// </summary>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="commentId"> The commentid of the comment to be deletd.</param>
        public void DeleteComments(int keyId, int formId, int commentId, int userId)
        {
            WSHelper.DeleteComments(keyId, formId, commentId, userId);
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
        /// Gets the config details.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The typed dataset containing config details</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        /// <summary>
        /// F9075_s the list template.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userid">The userid.</param>
        /// <returns>The typed dataset containing List Template details</returns>
        public F9075CommentTemplate F9075_ListTemplate(int form, int userid)
        {
            return WSHelper.F9075_ListTemplate(form, userid);
        }

        /// <summary>
        /// F9076_gets the template.
        /// </summary>
        /// <param name="templateid">The templateid.</param>
        /// <returns>The typed dataset containing List Template details</returns>
        public F9076NewCommentTemplateData F9076_getTemplate(int templateid)
        {
            return WSHelper.F9076_getTemplate(templateid);
        }

        /// <summary>
        /// For Testing Purpose added this method. later stage it should be removed.
        /// </summary>
        /// <param name="MSVersionID">The MS version ID.</param>
        /// <returns>Connection string.</returns>
        public CommonData GetConnectionString(int MSVersionID)
        {
            return WSHelper.GetConnectionString(MSVersionID);
        }

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


        #region F9075_DeleteCommentIds

        /// <summary>
        /// F9075_s the delete comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="userId">The user id.</param>
        public void F9075_DeleteCommentIds(string commentIds, int userId)
        {
            WSHelper.F9075_DeleteCommentIds(commentIds, userId);
        }

        #endregion F36041_DeleteCrop

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
