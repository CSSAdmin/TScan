//--------------------------------------------------------------------------------------------
// <copyright file="F1206WorkItem.cs" company="Congruent">
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
// 18-09-2006       Krishna Abburi        Created
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
    /// The F1206WorkItem
    /// </summary>
    public class F1206WorkItem : WorkItem
    {
        /// <summary>
        /// Listpostings the errors.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>ListPostingErrors</returns>
        public PostingErrorsData ListpostingErrors(int userId)
        {
            return WSHelper.ListPostingErrors(userId);
        }

        /// <summary>
        /// Inserts the account.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="errorTypeId">The error type id.</param>
        public void InsertAccount(int userId, int errorTypeId)
        {
            WSHelper.InsertAccount(userId, errorTypeId);
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
