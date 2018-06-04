// -------------------------------------------------------------------------------------------
// <copyright file="F9075TemplateNameComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F9075TemplateNameComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		            Description
// ----------		---------		        --------------------------------------------------
// 16/12/08         A.Shanmuga Sundaram     Create
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
    /// The comment comp class.
    /// </summary>
    public static class F9075CommentComp
    {
        #region F9075 list Template Selection

        /// <summary>
        /// F9075_s the list template.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userid">The userid.</param>
        /// <returns>listtemplateData</returns>
        public static F9075CommentTemplate F9075_ListTemplate(int form, int userid)
        {
            F9075CommentTemplate listtemplateData = new F9075CommentTemplate();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", form);
            ht.Add("@UserID", userid);
            string[] tableName = new string[] { listtemplateData.TemplateList.TableName, listtemplateData.CommentPriorities.TableName };
            //Utility.FillDataSet(listtemplateData, "f9075_pcget_TemplateList", ht);
            Utility.LoadDataSet(listtemplateData, "f9075_pcget_CommentUtilities", ht, tableName);
            return listtemplateData;
        }

        #endregion F9075 list Template Selection

        #region F9075_DeleteCommentIds

        /// <summary>
        /// F9075_DeleteCommentIds the delete comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="userId">The user id.</param>
        public static void F9075_DeleteCommentIds(string commentIds, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CommentIDs", commentIds);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f9075_pcdel_MassCommentDelete", ht);
        }

        #endregion F36041_DeleteCrop
    }
}