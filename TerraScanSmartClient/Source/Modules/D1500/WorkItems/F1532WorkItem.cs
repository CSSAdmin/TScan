// -------------------------------------------------------------------------------------------------
// <copyright file="F1532WorkItem.cs" company="Congruent">
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
// 11 Nov 06       Ranjani            Created// 
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
    /// F1532 WorkItem
    /// </summary>
    public class F1532WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the institution contact detail
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <returns>
        /// F1530CashAccountManagementData with institution contact Detail
        /// </returns>
        public F1530CashAccountManagementData F1532_GetInstitutionContactDetail(int contactId)
        {           
            return WSHelper.F1532_GetInstitutionContactDetail(contactId);            
        }

        /// <summary>
        /// saves the Institution Contact.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <returns>saved contact id</returns>
        public int F1532_SaveInstitutionContact(int contactId, string acctEmelemts, int userId)
        {
            return WSHelper.F1532_SaveInstitutionContact(contactId, acctEmelemts, userId);
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
