
namespace D23210
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

    public class F28210WorkItem : WorkItem
    {


        /// <summary>
        /// Gets the mortgage template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>DataSet</returns>
        public static F23200PermitImportTemplate GetPermitImportTemplate(int templateId)
        {
            return WSHelper.GetPermitImportTemplate(templateId);
        }
        /// <summary>
        /// F28210_s the Permit import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public  F28210PermitImport F28210_PermitImportDetails(int importId)
        {
            return WSHelper.F28210_PermitImportDetails(importId);
        }

        /// <summary>
        /// F35085_s the deletetemplate details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F28210_DeleteimportDetails(int importId, int userId)
        {
            WSHelper.F28210_DeleteimportDetails(importId, userId);
        }
        /// <summary>
        /// F35085_s the insert central template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F28210_InsertPermitImportDetails(int? importId, string importXML, int userId)
        {
            return WSHelper.F28210_InsertImportPermitDetails(importId, importXML, userId);
        }

        /// <summary>
        /// F35085_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28210_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            return WSHelper.F28210_ExecuteImport(importId, importXML, userId, isProcess);
        }
        /// <summary>
        /// F35085_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28210_ExecuteCheckForErrors(int importId, int userId)
        {
            WSHelper.F28210_ExecuteCheckForErrors(importId, userId);
        }
        /// <summary>
        /// F35085_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28210_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return WSHelper.F28210_CreateImportRecords(importId, userId, isProcess);
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
