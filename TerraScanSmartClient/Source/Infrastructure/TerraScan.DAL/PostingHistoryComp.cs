// -------------------------------------------------------------------------------------------
// <copyright file="PostingHistoryComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to PostingHistory  related information</summary>
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
    /// PostingHistoryComp class file
    /// </summary>
    public static class PostingHistoryComp
    {
        /// <summary>
        /// Gets the type of the A post.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>retuns postingHistoryData.ListPostType</returns>
        public static PostingHistoryData.ListPostTypeDataTable ListPostTypesData(int form)
        {
            PostingHistoryData postingHistoryData = new PostingHistoryData();
            Hashtable ht = new Hashtable();
            if (form != 0)
            {
                ht.Add("@Form", form);
            }
            else
            {
                ht.Add("@Form", DBNull.Value);
            }

            Utility.LoadDataSet(postingHistoryData.ListPostType, "f1000_pclst_PostType", ht);
            return postingHistoryData.ListPostType;
        }

        /// <summary>
        /// Lists the postinghistory data.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="postTypeId">The post type ID.</param>
        /// <returns>postingHistoryData.ListPostingHistory data table</returns>
        public static PostingHistoryData.ListPostingHistoryDataTable ListPostinghistoryData(int count, int postTypeId)
        {
            PostingHistoryData postingHistoryData = new PostingHistoryData();
            Hashtable ht = new Hashtable();
            if (count != 0)
            {
                ht.Add("@Count", count);
            }
            else
            {
                ht.Add("@Count", DBNull.Value);
            }
            
            if (postTypeId != 0)
            {
                ht.Add("@PostTypeID", postTypeId);
            }
            else
            {
                ht.Add("@PostTypeID", DBNull.Value);
            }
            
           // ht.Add("@PostTypeID", postTypeID);
            Utility.LoadDataSet(postingHistoryData.ListPostingHistory, "f1201_pclst_PostingHistory", ht);
            return postingHistoryData.ListPostingHistory;
        }
    }
}
