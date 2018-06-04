// -------------------------------------------------------------------------------------------------
// <copyright file="F1531WorkItem.cs" company="Congruent">
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
    /// F1531 WorkItem
    /// </summary>
    public class F1531WorkItem : WorkItem
    {
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
        /// saves cash account.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="registerItems">The register items.</param>
        /// <returns>subfund validated result,-1- validation failed else registerId</returns>
        public int F1531_SaveCashAccount(int registerId, string registerItems, int userId)
        {
            return WSHelper.F1531_SaveCashAccount(registerId, registerItems, userId);
        }

        /// <summary>
        /// List the register types.
        /// </summary>
        /// <returns>AccountManagementData with register type</returns>
        public AccountManagementData F1500_ListRegisterType()
        {
            return WSHelper.F1500_ListRegisterType();            
        }

        /// <summary>
        /// F9503_s the get sub fund items.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public F9503SubFundManagementData F9503_GetSubFundItems(string subFund, short rollYear)
        {
            return WSHelper.F9503_GetSubFundItems(subFund, rollYear);
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
