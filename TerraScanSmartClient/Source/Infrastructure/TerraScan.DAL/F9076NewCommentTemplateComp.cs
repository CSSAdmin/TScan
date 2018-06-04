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
    /// New comment template comp class.
    /// </summary>
    public static class F9076NewCommentTemplateComp
    {
        #region F9076New Comment Template

        #region F9076 list Template Selection

        /// <summary>
        /// F9076_gets the template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>The comment template dataset.</returns>
        public static F9076NewCommentTemplateData F9076_getTemplate(int templateId)
        {
            F9076NewCommentTemplateData gettemplateData = new F9076NewCommentTemplateData();
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            Utility.LoadDataSet(gettemplateData.GetCommentTemplate, "f9076_pcget_CommentTemplate", ht);
            return gettemplateData;
        }

        #endregion F9076 list Template Selection

        #region F9076 SaveNewCommentTemplate Selection

        /// <summary>
        /// F9076s the save new comment template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="commentItemsXml">The comment items XML.</param>
        /// <param name="isOverwrite">The is overwrite.</param>
        /// <returns></returns>
        public static int F9076SaveNewCommentTemplate(int? templateId, string commentItemsXml, int isOverwrite)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            ht.Add("@CommentItems", commentItemsXml);
            ht.Add("@IsOverwrite", isOverwrite);
            return Utility.FetchSPExecuteKeyId("f9076_pcins_CommentTemplate", ht);
        }

        #endregion F9076 SaveNewCommentTemplate Selection

        #region F9076 DeleteNewCommentTemplate Selection

        /// <summary>
        /// F9076_s the delete new comment template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        public static void F9076_DeleteNewCommentTemplate(int templateId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            Utility.ImplementProcedure("f9076_pcdel_CommentTemplate", ht);
        }

        #endregion F9076 DeleteNewCommentTemplate Selection

        #endregion New Comment Template
    }
}
