// -------------------------------------------------------------------------------------------
// <copyright file="ReverseGLpostComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access Reverse GL post related information</summary>
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

    /// <summary>
    /// ReverseGLpostComp class file
    /// </summary>
    public static class ReverseGLPostComp
    {
        /// <summary>
        /// Gets the post id data.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <returns>postIdDetails</returns>
        public static PostIdDetailsData GetPostIdDetails(int postId)
        {
            PostIdDetailsData postIdDetailsData = new PostIdDetailsData();
            Hashtable ht = new Hashtable();
            ht.Add("@PostID", postId);
            Utility.LoadDataSet(postIdDetailsData.PostingIdDetails, "f1201_pcget_PostingHistory", ht);
            return postIdDetailsData;
        }

        /// <summary>
        /// Reverses the GL post.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="userId">The user id.</param>
        public static void InsertReverseGLPost(int postId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@PostID", postId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f1202_pcins_ReverseGLPost", ht);
        }
    }
}
