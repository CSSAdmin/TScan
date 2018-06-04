// -------------------------------------------------------------------------------------------
// <copyright file="SnapshotImportTemplateComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access SnapshotImportTemplateComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//  01022018      R.Priyadharshini       Created
// -------------------------------------------------------------------------------------------


namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    public class SnapshotImportTemplateComp
    {
        #region Get

        /// <summary>
        /// Gets the Snapshot import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>The dataset containing the Snapshot Import information based on templateId</returns>
        public static F23500SnapshotTemplate GetSnapshotImportTemplate(int templateId)
        {
            F23500SnapshotTemplate SnapshotImpotTemplateData = new F23500SnapshotTemplate();
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            Utility.LoadDataSet(SnapshotImpotTemplateData.GetSnapshotImportTemplate, "f23500_pcget_SnapshotImportTemplate", ht);
            return SnapshotImpotTemplateData;
        }

        #endregion
        
        #region List SnapshotImportFileType

        /// <summary>
        /// Lists the type of the Snapshot import file.
        /// </summary>
        /// <returns>The dataset containing the Snapshot Import FileType</returns>
        public static F23500SnapshotTemplate ListSnapshotImportFileType()
        {
            F23500SnapshotTemplate SnapshotImportData = new F23500SnapshotTemplate();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(SnapshotImportData.ListSnapshotImportFileType, "f23500_pclst_SnapshotImportFileType", ht);
            return SnapshotImportData;
        }
        #endregion

        #region Save Snapshot Import Template

        /// <summary>
        /// Saves the Snapshot import template.
        /// </summary>
        public static int SaveSnapshotImportTemplate(int? templateId, string snapshotImportXML, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            ht.Add("@SnapshotImportTemplateItems", snapshotImportXML);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f23500_pcins_SnapshotImportTemplate", ht);
        }
        #endregion

        #region Delete Snapshot Import Template

        /// <summary>
        /// Deletes the Snapshot Import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">userId</param>
        /// <param name="Message">output param Message</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static string DeleteSnapshotTemplate(int templateId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            ht.Add("@UserID", userId);
            return Utility.FetchSingleOuputParameter("f23500_pcdel_SnapshotImportTemplate", ht, "@Message");
        }

        #endregion
    }
}
