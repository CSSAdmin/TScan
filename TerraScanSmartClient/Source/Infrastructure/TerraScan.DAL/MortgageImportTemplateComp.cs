// -------------------------------------------------------------------------------------------
// <copyright file="MortgageImportTemplateComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access MortgageImportTemplate related information</summary>
// Release history
// VERSION	DESCRIPTION
// 
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

    /// <summary>
    /// Main class for Mortgage Import Template Component
    /// </summary>
    public static class MortgageImportTemplateComp
    {
        #region Get

        /// <summary>
        /// Gets the mortgage template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>The dataset containing the MortgageTemplate information based on templateId</returns>
        public static MortgageImpotTemplateData GetMortgageTemplate(int templateId)
        {
            MortgageImpotTemplateData mortgageImpotTemplateData = new MortgageImpotTemplateData();
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            Utility.LoadDataSet(mortgageImpotTemplateData.GetMortgageImportTemplate, "f1011_pcget_MortgageImportTemplate", ht);
            return mortgageImpotTemplateData;
        }

        #endregion

        #region List Mortgage Import Template

        /// <summary>
        /// Lists the mortgage template.
        /// </summary>
        /// <returns>Mortgage template list</returns>
        public static MortgageImpotTemplateData ListMortgageTemplate()
        {
            MortgageImpotTemplateData mortgageImpotTemplateData = new MortgageImpotTemplateData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(mortgageImpotTemplateData.ListMortgageImportTemplate, "f1011_pclst_MortgageImportTemplate", ht);
            return mortgageImpotTemplateData;
        }
        #endregion

        #region List MortgageImportFileType

        /// <summary>
        /// Lists the type of the mortgage import file.
        /// </summary>
        /// <returns>The dataset containing the Mortgage Import FileType</returns>
        public static MortageImportData ListMortgageImportFileType()
        {
            MortageImportData mortageImportData = new MortageImportData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(mortageImportData.ListMortgageImportFileType, "f1010_pclst_MortgageImportFileType", ht);
            return mortageImportData;
        }
        #endregion

        #region Save Mortgage Import Template



        /// <summary>
        /// Saves the mortgage import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="description">The description.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="statementIdPos">The statement id pos.</param>
        /// <param name="statementIdWid">The statement id wid.</param>
        /// <param name="statementNumPos">The statement num pos.</param>
        /// <param name="statementNumWid">The statement num wid.</param>
        /// <param name="amountPos">The amount pos.</param>
        /// <param name="amountWid">The amount wid.</param>
        /// <param name="commentPos">The comment pos.</param>
        /// <param name="commentWid">The comment wid.</param>
        /// <param name="bankCodePos">The bank code pos.</param>
        /// <param name="bankCodeWid">The bank code wid.</param>
        /// <param name="loanNumPos">The loan num pos.</param>
        /// <param name="loanNumWid">The loan num wid.</param>
        /// <param name="taxPayNamePos">The tax pay name pos.</param>
        /// <param name="taxPayNameWid">The tax pay name wid.</param>
        /// <param name="ParcelNumPos">The parcel num pos.</param>
        /// <param name="ParcelNumWid">The parcel num wid.</param>
        /// <param name="PostTypePos">The post type pos.</param>
        /// <param name="PostTypeWid">The post type wid.</param>
        /// <param name="OwnerIDPos">The owner Id pos.</param>
        /// <param name="OwnerIDWid">The owner Id wid.</param>
        public static void SaveMortgageImportTemplate(int templateId, string templateName, int typeId, int userId, string description, string filePath, int statementIdPos, int statementIdWid, int statementNumPos, int statementNumWid, int amountPos, int amountWid, int commentPos, int commentWid, int bankCodePos, int bankCodeWid, int loanNumPos, int loanNumWid, int taxPayNamePos, int taxPayNameWid, int ParcelNumPos, int ParcelNumWid, int PostTypePos, int PostTypeWid, int OwnerIDPos, int OwnerIDWid,int CartIdPos,int CartidWid)
        {
            Hashtable ht = new Hashtable();
            if (templateId != 0)
            {
                ht.Add("@TemplateID", templateId);
            }

            ht.Add("@TemplateName", templateName);
            ht.Add("@TypeID", typeId);
            ht.Add("@UserID", userId);
            ht.Add("@Description", description);
            ht.Add("@FilePath", filePath);
            if (statementIdPos != 0)
            {
                ht.Add("@StatementID_Pos", statementIdPos);
            }

            if (statementIdWid != 0)
            {
                ht.Add("@StatementID_Wid", statementIdWid);
            }

            if (statementNumPos != 0)
            {
                ht.Add("@StatementNum_Pos", statementNumPos);
            }

            if (statementNumWid != 0)
            {
                ht.Add("@StatementNum_Wid", statementNumWid);
            }

            if (amountPos != 0)
            {
                ht.Add("@Amount_Pos", amountPos);
            }

            if (amountWid != 0)
            {
                ht.Add("@Amount_Wid", amountWid);
            }

            if (commentPos != 0)
            {
                ht.Add("@Comment_Pos", commentPos);
            }

            if (commentWid != 0)
            {
                ht.Add("@Comment_Wid", commentWid);
            }

            if (bankCodePos != 0)
            {
                ht.Add("@BankCode_Pos", bankCodePos);
            }

            if (bankCodeWid != 0)
            {
                ht.Add("@BankCode_Wid", bankCodeWid);
            }

            if (loanNumPos != 0)
            {
                ht.Add("@LoanNum_Pos", loanNumPos);
            }

            if (loanNumWid != 0)
            {
                ht.Add("@LoanNum_Wid", loanNumWid);
            }

            if (taxPayNamePos != 0)
            {
                ht.Add("@TaxPayName_Pos", taxPayNamePos);
            }

            if (taxPayNameWid != 0)
            {
                ht.Add("@TaxPayName_Wid", taxPayNameWid);
            }

            if (ParcelNumPos != 0)
            {
                ht.Add("@ParcelNum_Pos", ParcelNumPos);
            }

            if (ParcelNumWid != 0)
            {
                ht.Add("@ParcelNum_Wid", ParcelNumWid);
            }

            if (PostTypePos != 0)
            {
                ht.Add("@PostType_Pos", PostTypePos);
            }

            if (PostTypeWid != 0)
            {
                ht.Add("@PostType_Wid", PostTypeWid);
            }

            if (OwnerIDPos != 0)
            {
                ht.Add("@OwnerID_Pos", OwnerIDPos);
            }

            if (OwnerIDWid != 0)
            {
                ht.Add("@OwnerID_Wid", OwnerIDWid);
            }
            if (CartIdPos != 0)
            {
                ht.Add("@CartID_Pos", CartIdPos);
            }
            if (CartidWid != 0)
            {
                ht.Add("@CartID_Wid", CartidWid);
            }
            Utility.ImplementProcedure("f1011_pcins_MortgageImportTemplate", ht);
            }
        #endregion

        #region Delete Mortgage Template

        /// <summary>
        /// Deletes the mortgage template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="overrideStatus">if set to <c>true</c> [override status].</param>
        /// <param name="userId">userId</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int DeleteMortgageTemplate(int templateId, bool overrideStatus, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            ht.Add("@IsOverRide", overrideStatus);
            ht.Add("@UserID", userId);
           ////return DataProxy.FetchSPOutput("f1011_pcdel_MortgageImportTemplate", ht);
            return Utility.FetchSPOutput("f1011_pcdel_MortgageImportTemplate", ht);
        }

        #endregion
    }
}
