//--------------------------------------------------------------------------------------------
// <copyright file="F2004WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2004WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20/08/07         Ramya.D             Created
//*********************************************************************************/

namespace D2000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F2004WorkItem class file
    /// </summary> 
    public class F2004WorkItem : WorkItem
    {
        #region WorkItems Methods

        /// <summary>
        /// Gets the form details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>GetFormDetails</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }
      
        /// <summary>
        /// Gets the details of F2004 ParcelTypedetails
        /// </summary>
        /// <param name="parcelId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F2004ParcelCopyData GetParcelTypeDetails(int parcelId)
        {
            return WSHelper.GetParcelTypeDetails(parcelId);
        }

       /// <summary>
        /// GetParcelAttachmentDetails
       /// </summary>
        /// <param name="oldParcelID">oldParcelID</param>
        /// <param name="newParcelID">newParcelID</param>
        /// <param name="userID">userID</param>
        /// <param name="moduleID">moduleID</param>
        /// <returns>F2004ParcelCopyData</returns>
        public F2004ParcelCopyData GetParcelAttachmentDetails(int oldParcelId, int newParcelId, int userId, int moduleId)
        {
            return WSHelper.GetParcelAttachmentDetails(oldParcelId, newParcelId, userId, moduleId);
        }

        /// <summary>
        /// Gets the files path
        /// </summary>
        /// <param name="source"> The source path of the file.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="extension"> The file extension.</param>
        /// <returns> The typed dataset containing the path of the file.</returns>
        public AttachmentsData.GetFilePathDataTable GetFilePath(string source, int formId, int keyId, string extension,int userId)
        {
            return WSHelper.GetFilePath(source, formId, keyId, extension, userId).GetFilePath;
        }

        /// <summary>
        /// Saves the attachments.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="fileTypeId">The file type id.</param>
        /// <param name="source">The source.</param>
        /// <param name="primary">The primary.</param>
        /// <param name="description">The description.</param>
        /// <param name="eventDate">The event date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="publicValue">The public value.</param>
        /// <param name="isRoll">Roll</param>
        public void SaveAttachments(int fileId, string extension, int formId, int keyId, int fileTypeId, string source, int primary, string description, string eventDate, int userId, int publicValue, int isRoll, int linktype, string aurl, int pfileid, string sourceConfig)
        {
            WSHelper.SaveAttachments(fileId, extension, formId, keyId, fileTypeId, source, primary, description, eventDate, userId, publicValue, isRoll, linktype, aurl, pfileid, sourceConfig);
        }

        /// <summary>
        /// CreateNewParcelCopy
        /// </summary>
        /// <param name="parcelID">parcelID</param>
        /// <param name="parcelTypeID">parcelTypeID</param>
        /// <param name="copyAllObjects">copyAllObjects</param>
        /// <param name="copyAllSlices">copyAllSlices</param>
        /// <param name="copyAttachments">copyAttachments</param>
        /// <param name="copyComments">copyComments</param>
        /// <param name="parcelElements">parcelElements</param>
        /// <returns>integer</returns>
        public int CreateNewParcelCopy(int parcelId, int parcelTypeId, int copyAllObjects, int copyAllSlices, int copyAttachments, int copyComments, string parcelElements, int userId)
        {
            return WSHelper.CreateNewParcelCopy(parcelId, parcelTypeId, copyAllObjects, copyAllSlices, copyAttachments, copyComments, parcelElements, userId);
        }

        #region List Parcel Status
        /// <summary>
        /// To get the Parcel status Data Table
        /// </summary>
        /// <param name="parcelID">parcelID</param>
        /// <returns>Returns the Parcel status Data Table</returns>
        public F2000ParcelStatusData.ListParcelStatusDataTableDataTable F2000_ListParcelStatus(int parcelId)
        {
            return WSHelper.F2000_ListParcelStatus(parcelId);
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
        #endregion

        /// <summary>
        /// F2200_s the get assessment type details.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <returns></returns>
        public F2200EditScheduleData F2200_GetAssessmentTypeDetails(string assessmentType)
        {
            return WSHelper.F2200_GetAssessmentTypeDetails(assessmentType);
        }


        #endregion WorkItems Methods
    }
}
