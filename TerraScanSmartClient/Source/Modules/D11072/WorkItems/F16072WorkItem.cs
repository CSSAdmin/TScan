
namespace D11072
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

    public class F16072WorkItem : WorkItem
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
        /// F16072_s the get miscteplate details.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <returns></returns>
        public F16072MiscReceiptTemplate F16072_GetMiscteplateDetails(int misctemplateId)
        {
            return WSHelper.F16072_GetMiscteplateDetails(misctemplateId);
        }

        /// <summary>
        /// F16072_s the save misc receipt template.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="miscHeaderDetails">The misc header details.</param>
        /// <param name="accountDetails">The account details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F16072_SaveMiscReceiptTemplate(int? misctemplateId, string miscHeaderDetails, string accountDetails, int userId)
        {
            return WSHelper.F16072_SaveMiscReceiptTemplate(misctemplateId, miscHeaderDetails, accountDetails, userId);

        }
        /// <summary>
        /// F16072_s the delete misctemplate details.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="userId">The user id.</param>
        public void F16072_DeleteMisctemplateDetails(int misctemplateId, int userId)
        {
            WSHelper.F16072_DeleteMisctemplateDetails(misctemplateId, userId);
        }

        /// <summary>
        /// F16072_s the delete misc gridtemplate.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="miscIds">The misc ids.</param>
        /// <param name="userId">The user id.</param>
        public void F16072_DeleteMiscGridtemplate(int misctemplateId, string miscIds, int userId)
        {
            WSHelper.F16072_DeleteMiscGridtemplate(misctemplateId, miscIds, userId);
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

        
        /// <summary>
        /// Gets the config Roll Year.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }
    }
    
}
