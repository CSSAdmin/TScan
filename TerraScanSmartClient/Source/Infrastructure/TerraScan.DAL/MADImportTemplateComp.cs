// -------------------------------------------------------------------------------------------
// <copyright file="MADImportTemplateComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access MADImportTemplateComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//  01072016      R.Priyadharshini       Created
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

    public static class MADImportTemplateComp
    {
        #region Get

        /// <summary>
        /// Gets the MAD import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>The dataset containing the MAD Import information based on templateId</returns>
        public static F23300MADImportTemplate GetMADImportTemplate(int templateId)
        {
            F23300MADImportTemplate MADImpotTemplateData = new F23300MADImportTemplate();
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            Utility.LoadDataSet(MADImpotTemplateData.GetMADImportTemplate, "f23300_pcget_MADImportTemplate", ht);
            return MADImpotTemplateData;
        }

        #endregion

        #region List MADImportFileType

        /// <summary>
        /// Lists the type of the MAD import file.
        /// </summary>
        /// <returns>The dataset containing the MAD Import FileType</returns>
        public static F23300MADImportTemplate ListMADImportFileType()
        {
            F23300MADImportTemplate MADImportData = new F23300MADImportTemplate();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(MADImportData.ListMADImportFileType, "f23300_pclst_MADImportFileType", ht);
            return MADImportData;
        }
        #endregion

        #region Save MAD Import Template

        /// <summary>
        /// Saves the MAD import template.
        /// </summary>
        public static int SaveMADImportTemplate(int? templateId, string MADImportXML, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            ht.Add("@MADImportTemplateItems", MADImportXML);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f23300_pcins_MADImportTemplate", ht);
        }
        #endregion

        #region Delete MAD Import Template

        /// <summary>
        /// Deletes the MAD Import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">userId</param>
        /// <param name="Message">output param Message</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static string DeleteMADTemplate(int templateId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            ht.Add("@UserID", userId);
            return Utility.FetchSingleOuputParameter("f23300_pcdel_MADImportTemplate", ht, "@Message");
        }

        #endregion
    }
}

