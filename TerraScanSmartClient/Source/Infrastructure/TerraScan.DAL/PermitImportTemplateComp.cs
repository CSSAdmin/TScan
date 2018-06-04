// -------------------------------------------------------------------------------------------
// <copyright file="PermitImportTemplateComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access PermitImportTemplateComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//  19-05-2016      R.Priyadharshini       Created
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

    public static class PermitImportTemplateComp
    {
        #region Get

        /// <summary>
        /// Gets the permit import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>The dataset containing the Permit Import information based on templateId</returns>
        public static F23200PermitImportTemplate GetPermitImportTemplate(int templateId)
        {
            F23200PermitImportTemplate permitImpotTemplateData = new F23200PermitImportTemplate();
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            Utility.LoadDataSet(permitImpotTemplateData.GetPermitImportTemplate, "f23200_pcget_PermitImportTemplate", ht);
            return permitImpotTemplateData;
        }

        #endregion

        #region List Mortgage Import Template

        /// <summary>
        /// Lists the mortgage template.
        /// </summary>
        /// <returns>Mortgage template list</returns>
        //public static F23200PermitImportTemplate ListMortgageTemplate()
        //{
        //    F23200PermitImportTemplate mortgageImpotTemplateData = new F23200PermitImportTemplate();
        //    Hashtable ht = new Hashtable();
        //    Utility.LoadDataSet(mortgageImpotTemplateData.ListMortgageImportTemplate, "f1011_pclst_MortgageImportTemplate", ht);
        //    return mortgageImpotTemplateData;
        //}
        #endregion

        #region List PermitImportFileType

        /// <summary>
        /// Lists the type of the permit import file.
        /// </summary>
        /// <returns>The dataset containing the permit Import FileType</returns>
        public static F23200PermitImportTemplate ListPermitImportFileType()
        {
            F23200PermitImportTemplate permitImportData = new F23200PermitImportTemplate();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(permitImportData.ListPermitImportFileType, "f23200_pclst_PermitImportFileType", ht);
            return permitImportData;
        }
        #endregion

        #region Save Permit Import Template



        /// <summary>
        /// Saves the permit import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="description">The description.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="parcelNumberPos">The parcel number pos.</param>
        /// <param name="parcelNumberWid">The parcel number width .</param>
        /// <param name="rollYearPos">The roll year pos.</param>
        /// <param name="rollYearWid">The roll year wid.</param>
        /// <param name="permitNumberPos">The permit num pos.</param>
        /// <param name="permitNumberWid">The permit num wid.</param>
        /// <param name="dateOpenedPos">The date opened pos.</param>
        /// <param name="dateOpenedWid">The date opened wid.</param>
        /// <param name="dateLastVisitPos">The date visit pos.</param>
        /// <param name="dateLastVisitWid">The date visit wid.</param>
        /// <param name="dateClosedPos">The date closed pos.</param>
        /// <param name="dateClosedWid">The date closed wid.</param>
        /// <param name="estValuePos">The est value pos.</param>
        /// <param name="estValueWid">The est value wid.</param>
        /// <param name="assignedAppraiserUserNamePos">The ass approver pos.</param>
        /// <param name="assignedAppraiserUserNameWid">The ass approver wid.</param>
        /// <param name="permitTypePos">The permit type pos.</param>
        /// <param name="permitTypeWid">The permit wid.</param>
        /// <param name="percentCompletePos">The percent complete pos.</param>
        /// <param name="percentCompleteWid">The percent complete wid.</param>
        ///  <param name="permitDescriptionPos">The permit description pos.</param>
        /// <param name="permitDescriptionWid">The permit description wid.</param>
        /// <param name="insertedBy">inserted by user.</param>
        /// <param name="updatedBy">updated by user.</param>
        /// 
        public static int SavePermitImportTemplate(int? templateId, string permitImportXML, int userId)
        {
           
                Hashtable ht = new Hashtable();
                ht.Add("@TemplateID", templateId);
                ht.Add("@PermitImportTemplateItems", permitImportXML);
                ht.Add("@UserID", userId);
                return Utility.FetchSPExecuteKeyId("f23200_pcins_PermitImportTemplate", ht);
        }
        #endregion

        #region Delete Permit Import Template

        /// <summary>
        /// Deletes the Permit Import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">userId</param>
        /// <param name="Message">output param Message</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static string DeletePermitTemplate(int templateId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            ht.Add("@UserID", userId);
            return Utility.FetchSingleOuputParameter("f23200_pcdel_PermitImportTemplate", ht, "@Message");
        }

        #endregion
    }
}
