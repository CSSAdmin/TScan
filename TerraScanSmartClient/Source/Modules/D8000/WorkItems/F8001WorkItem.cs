//--------------------------------------------------------------------------------------------
// <copyright file="F8001WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 06/09/2001   	GUHAN S	           Created
//*********************************************************************************/

namespace D8000
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using System.Data;
    using TerraScan.BusinessEntities;

    #endregion

    /// <summary>
    /// F8001WorkItem
    /// </summary> 
  public class F8001WorkItem : WorkItem
  {
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

      #region GetEventEngineDataHeader

      /// <summary>
      /// List The TypeStatus
      /// </summary>
      /// <param name="featureClassId">The feature class id.</param>
      /// <param name="featureId">The feature id.</param>
      /// <returns>
      /// The dataset containing Details Evetn Type and status
      /// </returns>
      public GDocEventEngineData GetEventEngineDataHeader(int featureClassId, int featureId)
      {
          return WSHelper.GetEventEngineDataHeader(featureClassId, featureId);
      }

      #endregion
        /// <summary>
        /// Fires the <see cref="E:Microsoft.Practices.CompositeUI.WorkItem.RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="M:Microsoft.Practices.CompositeUI.WorkItem.Run"/>
        /// method is called on the <see cref="T:Microsoft.Practices.CompositeUI.WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="E:Microsoft.Practices.CompositeUI.WorkItem.Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
