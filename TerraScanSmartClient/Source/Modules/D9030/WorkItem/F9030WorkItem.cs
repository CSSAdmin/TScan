//--------------------------------------------------------------------------------------------
// <copyright file="F9030WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 08 Sep 06        Suganth Mani       Created
// 25 Sep 06        Suganth Mani       Modified for style cop 
//*********************************************************************************/

namespace D9030
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using System.Data;
    using TerraScan.BusinessEntities;

    #endregion NameSpaces

    /// <summary>
    /// F9030WorkItem class
    /// </summary>
    public class F9030WorkItem : WorkItem
    {
        #region FormMaster

        #region GetSandwichAndItsSliceInformation

        /// <summary>
        /// Gets the sandwich and its slice information.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>FormMasterData</returns>
        public FormMasterData GetSandwichAndItsSliceInformation(int form, int keyId, int userId)
        {
            return WSHelper.GetSandwichAndItsSliceInformation(form, keyId, userId);
        }

        /// <summary>
        /// Gets the attachment count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>GetAttachmentCount</returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
        }

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>GetCommentsCount</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        #endregion GetSandwichAndItsSliceInformation

        #region GetSandwichSubTitleInformation

        /// <summary>
        /// Gets the sandwich sub title information.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>FormSandwich information</returns>
        public FormMasterData GetSandwichSubTitleInformation(int form, int keyId, int userId)
        {
            return WSHelper.GetSandwichSubTitleInformation(form, keyId, userId);
        }

        #endregion GetSandwichSubTitleInformation

        #endregion FormMaster

        #region WorkItem Events

        /// <summary>
        /// Override Method for OnRunStarted
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Override Method for OnActivated
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion WorkItem Events
    }
}