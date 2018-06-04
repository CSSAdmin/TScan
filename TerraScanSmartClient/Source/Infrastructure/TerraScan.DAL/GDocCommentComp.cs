// -------------------------------------------------------------------------------------------
// <copyright file="GDocCommentComp.cs" company="Congruent">
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
    /// Main class for GDocCommentComp
    /// </summary>
    public static class GDocCommentComp
    {
        #region GDoc Comment

        #region Get GDoc Comment

        /// <summary>
        /// Gets the GDoc Comment.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Typed Dataset containing the GDoc comment</returns>
        public static GDocCommentData GetGDocComment(int eventId)
        {
            GDocCommentData gdocCommentData = new GDocCommentData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(gdocCommentData.GetGDocComment, "f8050_pcget_Comment", ht);
            return gdocCommentData;            
        }

        #endregion Get GDoc Comment

        #region Save GDoc Comment

        /// <summary>
        /// Saves the GDoc Comment.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="comment">The GDoc comment.</param>
        /// <param name="userId">userId</param>
        public static void SaveGDocComment(int eventId, string comment, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@Comment", comment);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8050_pcupd_Comment", ht);
        }

        #endregion Save GDoc Comment

        #endregion GDoc Comment
    }
}
