
namespace D23510
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

    public class F28510WorkItem : WorkItem
    {


        /// <summary>
        /// Gets the Snapshot template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>DataSet</returns>
        public static F23500SnapshotTemplate GetSnapshotImportTemplate(int templateId)
        {
            return WSHelper.GetSnapshotImportTemplate(templateId);
        }
        /// <summary>
        /// F28510_s the Snapshot import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public F23510SnapshotImport F28510_SnapshotImportDetails(int importId)
        {
            return WSHelper.F28510_SnapshotImportDetails(importId);
        }

        /// <summary>
        /// F28510_s the deletetemplate details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F28510_DeleteSnapshotImportDetails(int importId, int userId)
        {
            WSHelper.F28510_DeleteSnapshotImportDetails(importId, userId);
        }
        /// <summary>
        /// F35085_s the insert central template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F28510_InsertImportSnapshotDetails(int? importId, string importXML, int userId)
        {
            return WSHelper.F28510_InsertImportSnapshotDetails(importId, importXML, userId);
        }

        /// <summary>
        /// F35085_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28510_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            return WSHelper.F28510_ExecuteImport(importId, importXML, userId, isProcess);
        }
        /// <summary>
        /// F35085_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28510_ExecuteCheckForErrors(int importId, int userId)
        {
            WSHelper.F28510_ExecuteCheckForErrors(importId, userId);
        }
        /// <summary>
        /// F35085_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28510_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return WSHelper.F28510_CreateImportRecords(importId, userId, isProcess);
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
