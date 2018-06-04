//--------------------------------------------------------------------------------------------
// <copyright file="11024WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------

// 9-Dec-2014       Purushotham A       Created
//*********************************************************************************/
namespace D11024
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

    public class F11024WorkItem : WorkItem
    {
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


        /// <summary>
        /// get next working day - depends on clode time.
        /// </summary>
        /// <returns>return today or next working day</returns>
        public DateTime F9001_GetNextWorkingDay()
        {
            return WSHelper.F9001_GetNextWorkingDay();
        }

        /// <summary>
        /// F11024_s the get multiple journal template details.
        /// </summary>
        /// <param name="jetTemplateID">The jet template ID.</param>
        /// <returns></returns>
        public  F11024MultiplejournalEntryData F11024_GetMultipleJournalTemplateDetails(int jetTemplateID)
        {
            return WSHelper.F11024_GetMultipleJournalTemplateDetails(jetTemplateID);
        }


        /// <summary>
        /// F11024_s the save multiple journal template.
        /// </summary>
        /// <param name="transferDate">The transfer date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="description">The description.</param>
        /// <param name="journalTemplateDetails">The journal template details.</param>
        public void F11024_SaveMultipleJournalTemplate(string transferDate, int userId, string description, string journalTemplateDetails)
        {
           WSHelper.F11024_SaveMultipleJournalTemplate(transferDate, userId, description, journalTemplateDetails);
        }

        /// <summary>
        /// F11024_s the search template details.
        /// </summary>
        /// <returns></returns>
        public F11024MultiplejournalEntryData F11024_SearchTemplateDetails()
        {
            return WSHelper.F11024_SearchTemplateDetails();
        }


        /// <summary>
        /// List account details
        /// </summary>
        /// <param name="filterValue">Filter Value</param>
        /// <returns>Account details</returns>
        public F11018MiscReceiptData F15018_ListAccountDetails(string filterDetails, int? rollYear, int? formNo)
        {
            return WSHelper.F15018_ListAccountDetails(filterDetails, rollYear, formNo);
        }

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public F15013ExciseTaxRateData F15013_GetAccountName(int accountId)
        {
            return WSHelper.F15013_GetAccountName(accountId);
        }

    }
}
