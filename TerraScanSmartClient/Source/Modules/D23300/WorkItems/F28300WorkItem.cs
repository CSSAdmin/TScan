//--------------------------------------------------------------------------------------------
// <copyright file="F28300WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F28300WorkItem WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07082016       priyadharshini       
//*********************************************************************************/
namespace D23300
{
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    public class F28300WorkItem : WorkItem
    {
        #region Get
        /// <summary>
        /// Gets the MAD import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>DataSet With MAD Import Template Details</returns>
        public F23300MADImportTemplate GetMADImportTemplate(int templateId)
        {
            return WSHelper.GetMADImportTemplate(templateId);
        }
        #endregion

        #region Save MAD Import Template

        /// <summary>
        /// Saves the MAD import template.
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
        /// <param name="districtNumberPos">The district num pos.</param>
        /// <param name="districtNumberWid">The district num wid.</param>
        /// <param name="OverrideamountPos">The Override amount pos.</param>
        /// <param name="OverrideamountWid">The Override amount wid.</param>
        /// <param name="insertedBy">inserted by user.</param>
        /// <param name="updatedBy">updated by user.</param>
        /// 

        public int SaveMADImportTemplate(int? templateId, string madImportXML, int userId)
        {
            return WSHelper.SaveMADImportTemplate(templateId, madImportXML, userId);
        }
        #endregion

        #region List MADImportFileType
        /// <summary>
        /// Lists the type of the MAD import file.
        /// </summary>
        /// <returns>The dataset containing the MAD Import FileType</returns>
        public F23300MADImportTemplate ListMADImportFileType()
        {
            return WSHelper.ListMADImportFileType();
        }
        #endregion

        #region Delete MAD Import Template

        /// <summary>
        /// Deletes the MAD import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">The user Id.</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public string DeleteMADTemplate(int templateId, int userId)
        {
            return WSHelper.DeleteMADTemplate(templateId, userId);
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
