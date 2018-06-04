// -------------------------------------------------------------------------------------------------
// <copyright file="F25090WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//***********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//  
//***********************************************************************************************/



namespace D20000
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
    
    
    public class F25090WorkItem: WorkItem 
    {
        #region ParcelDetails
        
        /// <summary>
        /// Gets the details of F25000 Parceldetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F25090FieldSummaryData F25090_FieldSummary(int keyId)
        {
            return WSHelper.F25090_FieldSummary(keyId);
        }

        /// <summary>
        /// Gets the details of F25000 Parceldetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F25090FieldSummaryData F25090_GetAncestryData(int keyId)
        {
            return WSHelper.F25090_GetAncestryData(keyId);
        }

        /// <summary>
        /// Gets the details of F25000 Parceldetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F25090FieldSummaryData F25090_GetCorrection(int keyId)
        {
            return WSHelper.F25090_GetCorrection(keyId);
        }

        /// <summary>
        /// Gets the details of F25000 Parceldetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F25090FieldSummaryData F25090_GetHistoryData(int keyId)
        {
            return WSHelper.F25090_GetHistoryData(keyId);
        }

        /// <summary>
        /// Gets the details of F25000 Parceldetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F25090FieldSummaryData F25090_GetParcelOwnerShip(int keyId)
        {
            return WSHelper.F25090_GetParcelOwnerShip(keyId);
        }

        /// <summary>
        /// Gets the details of F25000 Parceldetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F25090FieldSummaryData F25090_ParcelSale(int keyId)
        {
            return WSHelper.F25090_ParcelSale(keyId);
        }

        /// <summary>
        /// Gets the details of F25000 Parceldetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F25090FieldSummaryData F25090_BuildingPermits(int keyId)
        {
            return WSHelper.F25090_BuildingPermits(keyId);
        }

        /// <summary>
        /// Gets the details of F25000 Parceldetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F25090FieldSummaryData F25090_GetPhotos(int keyId,int form)
        {
            return WSHelper.F25090_GetPhotos(keyId,form);
        }

        /// <summary>
        /// Gets the original file path.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>FilePath</returns>
        public string GetOriginalFilePath(int fileId, int userId)
        {
            return WSHelper.F9005_GetOriginalFilePath(fileId, userId);
        }

        #endregion ParcelDetails


        ///<summary>
        ///Get the Original File path
        /// </summary>
        public CommentsData GetudfConfigurationFile()
        {
            return WSHelper.GetConfigDetails("TSFile2");
        }


        #region WorkItems Methods

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

        #endregion WorkItems Methods
    }
}
