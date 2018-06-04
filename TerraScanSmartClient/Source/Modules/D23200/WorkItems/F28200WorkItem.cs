
namespace D23200
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

    public class F28200WorkItem : WorkItem
    {


        #region Get
        /// <summary>
        /// Gets the permit import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>DataSet With permit Import Template Details</returns>
        public F23200PermitImportTemplate GetPermitImportTemplate(int templateId)
        {
            return WSHelper.GetPermitImportTemplate(templateId);
        }
        #endregion

        #region Save permit Import Template

        /// <summary>
        /// Saves the permit import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="description">The description.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="parcelNumberPos">The parcel number pos.</param>
        /// <param name="parcelNumberWid">The parcel number width .</param>
        /// <param name="rollYearPos">The roll year pos.</param>
        /// <param name="rollYearWid">The roll year wid.</param>
        /// <param name="permitNumberPos">The permit num pos.</param>
        /// <param name="permitNumberWid">The permit num wid.</param>
        /// <param name="dateOpenedPos">The date opened pos.</param>
        /// <param name="dateOpenedWid">The date opened wid.</param>
        /// <param name="dateLastVisitPos">The date visit pos.</param>
        /// <param name="dateLastVisitWid">The date visit wid.</param>
        /// <param name="dateClosedPos">The date closed pos.</param>
        /// <param name="dateClosedWid">The date closed wid.</param>
        /// <param name="estValuePos">The est value pos.</param>
        /// <param name="estValueWid">The est value wid.</param>
        /// <param name="assignedAppraiserUserNamePos">The ass approver pos.</param>
        /// <param name="assignedAppraiserUserNameWid">The ass approver wid.</param>
        /// <param name="permitTypePos">The permit type pos.</param>
        /// <param name="permitTypeWid">The permit wid.</param>
        /// <param name="percentCompletePos">The percent complete pos.</param>
        /// <param name="percentCompleteWid">The percent complete wid.</param>
        ///  <param name="permitDescriptionPos">The permit description pos.</param>
        /// <param name="permitDescriptionWid">The permit description wid.</param>
        /// <param name="insertedBy">inserted by user.</param>
        /// <param name="updatedBy">updated by user.</param>
        /// 

        public int SavePermitImportTemplate(int? templateId, string permitImportXML, int userId)
        {
           return WSHelper.SavePermitImportTemplate(templateId, permitImportXML,userId);
        }
        #endregion

        #region List PermitImportFileType
        /// <summary>
        /// Lists the type of the permit import file.
        /// </summary>
        /// <returns>The dataset containing the permit Import FileType</returns>
        public F23200PermitImportTemplate ListPermitImportFileType()
        {
            return WSHelper.ListPermitImportFileType();
        }
        #endregion

        #region Delete Permit Import Template

        /// <summary>
        /// Deletes the permit import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">The user Id.</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public string DeletePermiTemplate(int templateId, int userId)
        {
            return WSHelper.DeletePermiTemplate(templateId, userId);
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
