// -------------------------------------------------------------------------------------------
// <copyright file="PostingComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access real estate related information</summary>
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
    /// PostingComp class file
    /// </summary>
    public static class PostingComp
    {
        /// <summary>
        /// Lists the post types.
        /// </summary>
        /// <returns>the posting data</returns>
        public static PostingData ListPostTypes()
        {
            PostingData postingData = new PostingData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(postingData.ListPostTypes, "f1200_pclst_PostingQueue", ht);
            return postingData;
        }

        /// <summary>
        /// Lists the preview posting.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <returns>the posting data</returns>
        public static PostingData ListPreviewPosting(DateTime postDate)
        {
            PostingData postingData = new PostingData();
            Hashtable ht = new Hashtable();                
            ht.Add("@PostDate", postDate);
            Utility.LoadDataSet(postingData.ListPostingPreview, "f1200_pclst_PostingPreview", ht);
            return postingData;
        }

        /// <summary>
        /// Clears the temporary records.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public static void ClearTemporaryRecords(int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f1200_pcdel_PostingTempRecords", ht);            
        }

        /// <summary>
        /// Compiles the posting record set.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <param name="selectedPostTypes">The selected post types.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>the posting data</returns>
        public static PostingData CompilePostingRecordSet(DateTime postDate, string selectedPostTypes, int userId)
        {
            PostingData postingData = new PostingData();
            Hashtable ht = new Hashtable();
            ht.Add("@PostDate", postDate);
            ht.Add("@PostTypes", selectedPostTypes);
            ht.Add("@UserID", userId);
            ////Utility.FillDataSet(postingData.PostingErrorCount, "f1200_pcins_CompileRecordset_WA", ht);
            Utility.LoadDataSet(postingData.PostingErrorCount, "f1200_pcins_CompileRecordset", ht);
            return postingData;
        }

        /// <summary>
        /// Performs the posting.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <param name="selectedPostTypes">The selected post types.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>the posting data</returns>
        public static PostingData PerformPosting(DateTime postDate, string selectedPostTypes, int userId)
        {
            PostingData postingData = new PostingData();
            Hashtable ht = new Hashtable();
            ht.Add("@PostTypes", selectedPostTypes);
            ht.Add("@PostDate", postDate);     
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(postingData.PostLockCount, "f1200_pcexe_PerformPost", ht);
            return postingData;
        }    
    }
}
