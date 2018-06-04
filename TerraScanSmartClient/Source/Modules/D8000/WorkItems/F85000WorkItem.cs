// -------------------------------------------------------------------------------------------
// <copyright file="F85000WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update GDoc Comments</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 Aug 2007      Ramya.D             Created
// 
// -------------------------------------------------------------------------------------------



namespace D8000
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
    /// Class for F8050WorkItem
    /// </summary>
    public class F85000WorkItem : WorkItem
    {
        #region Get Form Slice Permission Details

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">userId</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        #endregion Get Form Slice Permission Details

        /// <summary>
        /// Gets the config details.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
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

        #region GetTypeStatus

        /// <summary>
        /// List The TypeStatus
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <returns>
        /// The dataset containing Details Evetn Type and status
        /// </returns>
        public GDocEventEngineTypeStatusData ListEventTypeStatusDetails(int featureClassId)
        {
            return WSHelper.ListEventTypeStatusDetails(featureClassId);
        }

        #endregion

        #region Insert EventEngineData
        /// <summary>
        /// Inserts the G doc event engine data.
        /// </summary>
        /// <param name="eventEngineInsertData">The event engine insert data.</param>
        /// <returns>Inserted EventID</returns>
        public int InsertGDocEventEngineData(string eventEngineInsertData,int userID)
        {
            return WSHelper.InsertGDocEventEngineData(eventEngineInsertData, userID);
        }
        #endregion

        /// <summary>
        /// Inserts the G doc event engine data.
        /// </summary>
        /// <param name="eventEngineInsertData">The event engine insert data.</param>
        /// <returns>Inserted EventID</returns>
        public int GetGDocEventEngineFeatureClassId(int featureId)
        {
            return WSHelper.GetGDocEventEngineFeatureClassId(featureId);
        }

        #region LoadEventEngineDataGrid

        /// <summary>
        /// Load the event engine dataGrid
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <param name="featureId">The feature id.</param>
        /// <returns>
        /// The dataset containing Datas of EventEngine
        /// </returns>
        public GDocEventEngineData LoadEventEngineData(int featureClassId, int featureId)
        {
            return WSHelper.LoadEventEngineData(featureClassId, featureId);
        }

        #endregion
    }
}
