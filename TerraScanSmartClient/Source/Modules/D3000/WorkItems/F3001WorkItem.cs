//--------------------------------------------------------------------------------------------
// <copyright file="F1401WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1031WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Aug 07        karthikeyan V            Created
//*********************************************************************************/

namespace D3000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    public class F3001WorkItem : WorkItem
    {
        #region WorkItemEvents

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

        #endregion WorkItemEvents

        /// <summary>
        /// F3001_s the get object detail.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <returns></returns>
        public F3001ObjectManagementData F3001_GetObjectDetail(int objectId)
        {
            return WSHelper.F3001_GetObjectDetail(objectId);
        }

        /// <summary>
        /// F3001_s the save object management.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="objectItems">The objectItems.</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public int F3001_SaveObjectManagement(int objectId, string objectItems,int userId)
        {
            return WSHelper.F3001_SaveObjectManagement(objectId, objectItems, userId);
        }

        /// <summary>
        /// F3001_s the delete object management.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        public void F3001_DeleteObjectManagement(int objectId,int userID)
        {
            WSHelper.F3001_DeleteObjectManagement(objectId, userID);
        }

        /// <summary>
        /// F3001_s the get parcel description.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>string</returns>
        public string F3001_GetParcelDescription(int parcelId)
        {
            return WSHelper.F3001_GetParcelDescription(parcelId);
        }

        /// <summary>
        /// F3001_s the copy object.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="objectXml">The object XML.</param>
        /// <returns>int</returns>
        public int F3001_CopyObject(int objectId, string objectXml,int userID)
        {
            return WSHelper.F3001_CopyObject(objectId, objectXml,TerraScan.Common.TerraScanCommon.UserId);
        }

        public string ListRecordLockStatus(int formNo, int keyId)
        {
            return WSHelper.ListRecordLockStatus(formNo, keyId);
        }

        #region Attachment

        /// <summary>
        /// Gets the Attachments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
        }

        #endregion Attachment

        #region Comments

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        #endregion Comments
    }
}
