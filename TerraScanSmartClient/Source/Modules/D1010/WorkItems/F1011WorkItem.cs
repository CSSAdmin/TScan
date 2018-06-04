// -------------------------------------------------------------------------------------------
// <copyright file="F1011WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Statement</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
// -------------------------------------------------------------------------------------------
namespace D1010
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

    /// <summary>
    /// form F1011 WorkItem
    /// </summary>
    public class F1011WorkItem : WorkItem
    {
        #region PrivateDataMembers

        /// <summary>
        /// Gets the list mortgage template.
        /// </summary>
        /// <value>The list mortgage template.</value>
        public MortgageImpotTemplateData ListMortgageTemplate
        {
            get { return WSHelper.ListMortgageTemplate(); }           
        }

        #region PrivateDataMembers

        #endregion MortgageImportTemplate

        #region Get
        /// <summary>
        /// Gets the mortgage template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>DataSet With Mortgage Import Template Details</returns>
        public static MortgageImpotTemplateData GetMortgageTemplate(int templateId)
        {
            return WSHelper.GetMortgageTemplate(templateId);
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
        public static void SaveMortgageImportTemplate(int templateId, string templateName, int typeId, int userId, string description, string filePath, int statementIdPos, int statementIdWid, int statementNumPos, int statementNumWid, int amountPos, int amountWid, int commentPos, int commentWid, int bankCodePos, int bankCodeWid, int loanNumPos, int loanNumWid, int taxPayNamePos, int taxPayNameWid, int ParcelNumPos, int ParcelNumWid, int PostTypePos, int PostTypeWid, int OwnerIDPos, int OwnerIDWid, int CartIdPos, int CartidWid)
            {
                WSHelper.SaveMortgageImportTemplate(templateId, templateName, typeId, userId, description, filePath, statementIdPos, statementIdWid, statementNumPos, statementNumWid, amountPos, amountWid, commentPos, commentWid, bankCodePos, bankCodeWid, loanNumPos, loanNumWid, taxPayNamePos, taxPayNameWid, ParcelNumPos, ParcelNumWid, PostTypePos, PostTypeWid, OwnerIDPos, OwnerIDWid,CartIdPos,CartidWid);
        }
        #endregion        

        #region List MortgageImportFileType
        /// <summary>
        /// Lists the type of the mortgage import file.
        /// </summary>
        /// <returns>The dataset containing the Mortgage Import FileType</returns>
        public MortageImportData ListMortgageImportFileType()
        {
            return WSHelper.ListMortgageImportFileType();
        }
        #endregion

        #region Delete Mortgage Template

        /// <summary>
        /// Deletes the mortgage template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="overrideStatus">if set to <c>true</c> [override status].</param>
        /// <param name="userId">The user Id.</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public int DeleteMortgageTemplate(int templateId, bool overrideStatus, int userId)
        {
            return WSHelper.DeleteMortgageTemplate(templateId, overrideStatus, userId);
        }

        #endregion

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
