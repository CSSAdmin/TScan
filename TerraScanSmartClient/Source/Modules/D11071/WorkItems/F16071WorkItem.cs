
namespace D11071
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
    /// 
    /// </summary>
    public class F16071WorkItem : WorkItem
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
        /// F16071_s the get journal teplate details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns></returns>
        public F16071JournalEntryTemplateData F16071_GetJournalTeplateDetails(int templateId)
        {
            return WSHelper.F16071_GetJournalTeplateDetails(templateId);
        }

        public int F16071_SaveHeaderTemplateDetails(int? templateId, int rollYear, string description, int userId)
        {
            return WSHelper.F16071_SaveHeaderTemplateDetails(templateId, rollYear, description, userId);
        }

        /// <summary>
        /// F16071_s the save grid template details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="gridDetails">The grid details.</param>
        /// <param name="userId">The user id.</param>
        public void F16071_SaveGridTemplateDetails(int? templateId, string gridDetails, int userId)
        {
            WSHelper.F16071_SaveGridTemplateDetails(templateId, gridDetails, userId);
        }

        /// <summary>
        /// F16071_s the delete journal header details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">The user id.</param>
        public void F16071_DeleteJournalHeaderDetails(int templateId, int userId)
        {
            WSHelper.F16071_DeleteJournalHeaderDetails(templateId, userId);
        }

        /// <summary>
        /// F16071_s the delete journal grid details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="gridDetails">The grid details.</param>
        /// <param name="userId">The user id.</param>
        public void F16071_DeleteJournalGridDetails(int templateId, string gridDetails, int userId)
        {
            WSHelper.F16071_DeleteJournalGridDetails(templateId, gridDetails, userId);
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
