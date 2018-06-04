
namespace D23310
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

    public class F28310WorkItem : WorkItem
    {


        /// <summary>
        /// Gets the MAD template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>DataSet</returns>
        public static F23300MADImportTemplate GetMADImportTemplate(int templateId)
        {
            return WSHelper.GetMADImportTemplate(templateId);
        }

        /// <summary>
        /// F28310_s the MAD import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public F28310MADImport F28310_MADImportDetails(int importId)
        {
            return WSHelper.F28310_MADImportDetails(importId);
        }

        /// <summary>
        /// F28310_s the deleteMADtemplate details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F28310_DeleteMADImportDetails(int importId, int userId)
        {
            WSHelper.F28310_DeleteMADImportDetails(importId, userId);
        }
        /// <summary>
        /// F28310_s the insert MAD template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F28310_InsertImportMADDetails(int? importId, string importXML, int userId)
        {
            return WSHelper.F28310_InsertImportMADDetails(importId, importXML, userId);
        }

        /// <summary>
        /// F28310_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28310_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            return WSHelper.F28310_ExecuteImport(importId, importXML, userId, isProcess);
        }

        /// <summary>
        /// F28310_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28310_ExecuteCheckForErrors(int importId, int userId)
        {
            WSHelper.F28310_ExecuteCheckForErrors(importId, userId);
        }

        /// <summary>
        /// F28310_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28310_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return WSHelper.F28310_CreateImportRecords(importId, userId, isProcess);
        }

        #region List ListDistrictType
        /// <summary>
        /// Lists the type of the MAD import district file.
        /// </summary>
        /// <returns>The dataset containing the District File Type</returns>
        public F28310MADImport ListDistrictType()
        {
            return WSHelper.ListDistrictType();
        }
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
