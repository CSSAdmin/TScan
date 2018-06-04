// -------------------------------------------------------------------------------------------------
// <copyright file="F1530WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Nov 06        Ranjani            Created// 
//*********************************************************************************/
namespace D1500
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
    /// F1530 WorkItem
    /// </summary>
    public class F1530WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the institution list and Detail
        /// </summary>
        /// <param name="institutionId">The institution id.</param>
        /// <returns>F1530CashAccountManagementData with institution Detail</returns>
        public F1530CashAccountManagementData F1530_GetInstitutionDetail(int institutionId)
        {
            return WSHelper.F1530_GetInstitutionDetail(institutionId);
        }

        /// <summary>
        /// saves the institution
        /// </summary>
        /// <param name="institutionId">The institution id.</param>
        /// <param name="institutionElements">The institution elements.</param>
        /// <returns>saved institution id</returns>
        public int F1530_SaveInstitution(int institutionId, string institutionElements,int userId)
        {
            return WSHelper.F1530_SaveInstitution(institutionId, institutionElements, userId);
        }

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

        /// <summary>
        /// Gets the CashAccount Detail
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <returns>
        /// F1530CashAccountManagementData with CashAccount Detail
        /// </returns>
        public F1530CashAccountManagementData F1531_GetCashAccountDetail(int registerId)
        {
            return WSHelper.F1531_GetCashAccountDetail(registerId);
        }

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
    }
}
