
namespace D30085
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

    public  class F35085WorkItem:WorkItem
    {

        /// <summary>
        /// F35085_s the central assessed import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public  F35085CentrallyAssessedImportData F35085_CentralAssessedImportDetails(int importId)
        {
            return WSHelper.F35085_CentralAssessedImportDetails(importId);
        }

        /// <summary>
        /// F35085_s the import type combo.
        /// </summary>
        /// <returns></returns>
        public  F35085CentrallyAssessedImportData F35085_ImportTypeCombo()
        {
            return WSHelper.F35085_ImportTypeCombo();
        }
        /// <summary>
        /// F35085_s the deletetemplate details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public  void F35085_DeletetemplateDetails(int importId, int userId)
        {
            WSHelper.F35085_DeletetemplateDetails(importId, userId);
        }
        /// <summary>
        /// F35085_s the insert central template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public  DataSet F35085_InsertCentralTemplateDetails(int? importId, string importXML, int userId)
        {
            return WSHelper.F35085_InsertCentralTemplateDetails(importId, importXML, userId);
        }

        /// <summary>
        /// F35085_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public string F35085_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {

            return WSHelper.F35085_ExecuteImport(importId, importXML, userId, isProcess);
        }
        /// <summary>
        /// F35085_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F35085_ExecuteCheckForErrors(int importId, int userId)
        {
             WSHelper.F35085_ExecuteCheckForErrors(importId,userId);
        }
        /// <summary>
        /// F35085_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public string F35085_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return WSHelper.F35085_CreateImportRecords(importId,userId,isProcess);
        }
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
