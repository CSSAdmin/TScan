//--------------------------------------------------------------------------------------------
// <copyright file="F1202WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1202 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20-Sept-2006       Krishna Abburi    Created
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
    /// The F1202WorkItem
    /// </summary>
    public class F1202WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the post id details.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <returns>PostIdDetails</returns>
        public PostIdDetailsData GetPostIdDetails(int postId)
        {
            return WSHelper.GetPostIdDetails(postId);
        }

        /// <summary>
        /// Inserts the reverse GL post.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="userId">The user id.</param>
        public void InsertReverseGLPost(int postId, int userId)
        {
            WSHelper.InsertReverseGLPost(postId, userId);
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
