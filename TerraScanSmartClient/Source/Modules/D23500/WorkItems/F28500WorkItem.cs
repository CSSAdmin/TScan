//--------------------------------------------------------------------------------------------
// <copyright file="F23500WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F23500WorkItem WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21122016       priyadharshini       
//*********************************************************************************/
namespace D23500
{
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    public class F28500WorkItem : WorkItem
    {
        #region Get
        /// <summary>
        /// Gets the Snapshot import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>DataSet With Snapshot Import Template Details</returns>
        public F23500SnapshotTemplate GetSnapshotImportTemplate(int templateId)
        {
            return WSHelper.GetSnapshotImportTemplate(templateId);
        }
        #endregion

        #region Save Snapshot Import Template

        /// <summary>
        /// Saves the Snapshot import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="description">The description.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="parcelNumberPos">The parcel number pos.</param>
        /// <param name="parcelNumberWid">The parcel number width .</param>
        /// <param name="insertedBy">inserted by user.</param>
        /// <param name="updatedBy">updated by user.</param>
        /// 

        public int SaveSnapshotImportTemplate(int? templateId, string snapshotImportXML, int userId)
        {
            return WSHelper.SaveSnapshotImportTemplate(templateId, snapshotImportXML, userId);
        }
        #endregion

        #region List SnapshotImportFileType
        /// <summary>
        /// Lists the type of the Snapshot import file.
        /// </summary>
        /// <returns>The dataset containing the Snapshot Import FileType</returns>
        public F23500SnapshotTemplate ListSnapshotImportFileType()
        {
            return WSHelper.ListSnapshotImportFileType();
        }
        #endregion

        #region Delete Snapshot Import Template

        /// <summary>
        /// Deletes the Snapshot import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">The user Id.</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public string DeleteSnapshotTemplate(int templateId, int userId)
        {
            return WSHelper.DeleteSnapshotTemplate(templateId, userId);
        }

        #endregion

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
