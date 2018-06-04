//--------------------------------------------------------------------------------------------
// <copyright file="F1200WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1201 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 06-09-2006       Ranjani        Created
//*********************************************************************************/
namespace D1200
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// The F1200WorkItem
    /// </summary>
    public class F1200WorkItem : WorkItem
    {       
        /// <summary>
        /// Lists the post types.
        /// </summary>
        /// <returns>PostingHistoryComp.ListPostTypesData</returns>
        public PostingData.ListPostTypesDataTable ListPostTypes()
        {      
            return WSHelper.ListPostTypes().ListPostTypes;            
        }      

        /// <summary>
        /// Lists the preview posting.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <returns>ListPostingPreviewDataTable</returns>
        public PostingData.ListPostingPreviewDataTable ListPreviewPosting(DateTime postDate)
        {            
            return WSHelper.ListPreviewPosting(postDate).ListPostingPreview;    
        }      

        /// <summary>
        /// Clears the temporary records.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public void ClearTemporaryRecords(int userId)
        {
            WSHelper.ClearTemporaryRecords(userId);
        }

        /// <summary>
        /// Compiles the posting record set.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <param name="selectedPostTypes">The selected post types.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>the dataset</returns>
        public PostingData CompilePostingRecordSet(DateTime postDate, string selectedPostTypes, int userId)
        {
            return WSHelper.CompilePostingRecordSet(postDate, selectedPostTypes, userId);
        }

        /// <summary>
        /// Performs the posting.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <param name="selectedPostTypes">The selected post types.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>the dataset</returns>
        public PostingData PerformPosting(DateTime postDate, string selectedPostTypes, int userId)
        {
            return WSHelper.PerformPosting(postDate, selectedPostTypes, userId);
        }

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        } 
    }
}
