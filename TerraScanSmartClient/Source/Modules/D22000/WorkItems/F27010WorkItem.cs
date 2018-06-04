// -------------------------------------------------------------------------------------------------
// <copyright file="F27010WorkItem.cs" company="Congruent">
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
// 26 Mar 08        D.LathaMaheswari     Created 
//*********************************************************************************/
namespace D22000
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
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F27000 WorkItem
    /// </summary>
    public class F27010WorkItem : WorkItem
    {
        #region Form Details

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">The user id.</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        #endregion Form Details

        #region GetConfigValue

        /// <summary>
        /// Gets the Image Patha
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Config value.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #endregion GetConfigValue

        #region GetRollYear
        /// <summary>
        /// F27010s the get roll year.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Integer</returns>
        public int F27010GetRollYear(int parcelId)
        {
            return WSHelper.F27010GetRollYear(parcelId);
        }
        #endregion GetRollYear

        #region Get Assessment Type
        /// <summary>
        /// F27010s the type of the get assessment.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet</returns>
        public F27010MiscAssessmentData F27010GetAssessmentType(int rollYear)
        {
            return WSHelper.F27010GetAssessmentType(rollYear);
        }

        #endregion Get Assessment Type

        #region GetDistrict
        /// <summary>
        /// F27010s the get district.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet</returns>
        public F27010MiscAssessmentData F27010GetDistrict(int parcelId, int madTypeId, int rollYear)
        {
            return WSHelper.F27010GetDistrict(parcelId, madTypeId, rollYear);
        }
        #endregion GetDistrict

        #region Check Default District
        /// <summary>
        /// F27010s the check default district.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Integer</returns>
        public int F27010CheckDefaultDistrict(int parcelId, int madTypeId, int rollYear)
        {
            return WSHelper.F27010CheckDefaultDistrict(parcelId, madTypeId, rollYear);
        }
        #endregion Check Default District

        #region Get ToolTip Message
        /// <summary>
        /// F27010s the get message.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <returns>DataSet</returns>
        public F27010MiscAssessmentData F27010GetMessage(int parcelId, int madTypeId, int madDistrictId)
        {
            return WSHelper.F27010GetMessage(parcelId, madTypeId, madDistrictId);
        }
        #endregion Get ToolTip Message

        #region GetMiscAssessment (MADType1)
        /// <summary>
        /// F27010s the get misc data.
        /// </summary>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>DataSet</returns>
        public F27010MiscAssessmentData F27010GetMiscData(int madDistrictId, int parcelId)
        {
            return WSHelper.F27010GetMiscData(madDistrictId, parcelId);
        }
        #endregion GetMiscAssessment (MADType1)

        #region GetMiscAssessment (Other MADType)
        /// <summary>
        /// F27010s the get others misc data.
        /// </summary>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns>DataSet</returns>
        public F27010MiscAssessmentData F27010GetOthersMiscData(int madDistrictId, int parcelId, string procedureName)
        {
            return WSHelper.F27010GetOthersMiscData(madDistrictId, parcelId, procedureName);
        }
        #endregion GetMiscAssessment (Other MADType)

        #region GetDefaultMiscData

        /// <summary>
        /// F27010s the get default misc data.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <returns>DataSet</returns>
        public F27010MiscAssessmentData F27010GetDefaultMiscData(int parcelId, int madTypeId)
        {
            return WSHelper.F27010GetDefaultMiscData(parcelId, madTypeId);
        }
        #endregion GetDefaultMiscData

        #region SaveMiscAssessment
        /// <summary>
        /// F27010_s the save misc assessment.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="miscType">Type of the misc.</param>
        /// <param name="madItem">The mad item.</param>
        /// <param name="miscItems">The misc items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        public int F27010_SaveMiscAssessment(int parcelId, string miscType, string madItem, string miscItems, int userId)
        {
            return WSHelper.F27010_SaveMiscAssessment(parcelId, miscType, madItem, miscItems, userId);
        }
        #endregion SaveMiscAssessment

        #region WorkItems
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
        #endregion WorkItems
    }
}
