//--------------------------------------------------------------------------------------------
// <copyright file="F2551WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2551WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 23 Sep 2011        Manoj Kumar.P              Created
//*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.EventBroker;
using System.Data;
using Microsoft.Practices.CompositeUI.Utility;
using TerraScan.SmartParts;
using TerraScan.BusinessEntities;
using TerraScan.Helper;

namespace D2550
{
    /// <summary>
    /// F2551WorkItem
    /// </summary>
    public class F2551WorkItem : WorkItem 
    {

        
        /// <summary>
        /// F2551_s the list Edit Statement details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">The Type id.</param>
        /// <param name="itemXML">The item XML.</param>
        /// <param name="headerXML">The headerXML.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        public F2551EditStmtData F2551_ListEditStatementDetails(int parcelId, short typeId, int statementId, int ownerId, int userId)
        {
            return WSHelper.F2551_ListEditStatementDetails(parcelId, typeId, statementId, ownerId, userId);
        }



        /// <summary>
        /// F2551_s the list ExecuteLoadGrid details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">The Type id.</param>
        /// <param name="itemXML">The item XML.</param>
        /// <param name="headerXML">The headerXML.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        public F2551EditStmtData F2551_LoadStatementGridDetails(int parcelId, short typeId, int statementId, int ownerId, string itemXML, string changeXML, int userId)
        {
            return WSHelper.F2551_LoadStatementGridDetails(parcelId, typeId, statementId, ownerId, itemXML, changeXML, userId);
        }
        



        /// <summary>
        /// Save Edit Statement details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">The Type id.</param>
        /// <param name="itemXML">The item XML.</param>
        /// <param name="headerXML">The headerXML.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        public int SaveEditStatementtDetails(int parcelId, short typeId, int statementId, int ownerId, string itemXML, string headerXML, int userId)
        {
           return WSHelper.SaveEditStatementtDetails(parcelId, typeId, statementId, ownerId, itemXML, headerXML, userId);
        }

        #region Get FilePath

        /// <summary>
        /// Gets the files path
        /// </summary>
        /// <param name="source"> The source path of the file.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="extension"> The file extension.</param>
        /// <returns> The typed dataset containing the path of the file.</returns>
        public AttachmentsData.GetFilePathDataTable GetFilePath(string source, int formId, int keyId, string extension)
        {

            return WSHelper.GetFilePath(source, formId, keyId, extension, TerraScan.Common.TerraScanCommon.UserId).GetFilePath;
        }

        #endregion Get FilePath

        /// <summary>
        /// Gets the attachment items.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The typed dataset containing the attachment items.</returns>
        public AttachmentsData.GetAttachmentItemsDataTable GetAttachmentItems(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentItems(formId, keyId, userId).GetAttachmentItems;
        }

        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="userId"> The user id of the user logged in.</param>
        /// <returns> The typed dataset containing the comments.</returns>
        public CommentsData GetComments(int keyId, int formId, int userId)
        {
            return WSHelper.GetComments(keyId, formId, userId);
        }


        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
