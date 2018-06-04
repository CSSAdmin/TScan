// ---------------------------------------------------------------------------------------------------------------
// <copyright file="F49911WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F49911</summary>
// Release history
//*****************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ----------------------------------------------------------------------------
// 31/01/2008       KUPPUSAMY.B             Created
// 
// ----------------------------------------------------------------------------------------------------------------

namespace D49910
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F49911WorkItem
    /// </summary>
    public class F49911WorkItem : WorkItem
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

        #region Get

        /// <summary>
        /// F49910_s the get instrument header details.
        /// </summary>
        /// <param name="instId">The inst id.</param>
        /// <returns>InstrumentHeaderDetails</returns>
        public F49910InstrumentHeaderDataSet F49910_GetInstrumentHeaderDetails(int instId)
        {
            return WSHelper.F49910_GetInstrumentHeaderDetails(instId);
        }

        #region GetInstrumentTypeDetails

        /// <summary>
        /// F49910_GetInstrumentTypeDetails
        /// </summary>
        /// <param name="instId">instId</param>
        /// <returns>DataSet</returns>
        public F49910InstrumentHeaderDataSet F49910_GetInstrumentTypeDetails()
        {
            return WSHelper.F49910_GetInstrumentTypeDetails();
        }

        #endregion GetInstrumentTypeDetails

        /// <summary>
        /// F49911_s the list parties field.
        /// </summary>
        /// <returns>PartiesField</returns>
        public F49910InstrumentHeaderDataSet F49911_ListPartiesField()
        {
            return WSHelper.F49911_ListPartiesField();
        }

        /// <summary>
        /// F49911_s the get grid details for new.
        /// </summary>
        /// <returns>InstrumentTypeDetails</returns>
        public F49910InstrumentHeaderDataSet F49911_GetGridDetailsForNew()
        {
            return WSHelper.F49910_GetInstrumentTypeDetails();
        }
        #endregion Get

        #region Insert

        /// <summary>
        /// F49911_s the insert parties field details.
        /// </summary>
        /// <param name="instid">The instid.</param>
        /// <param name="grantorItems">The grantor items.</param>
        /// <param name="granteeItems">The grantee items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>PartiesFieldDetails</returns>
        public int F49911_InsertPartiesFieldDetails(int instid, string grantorItems, string granteeItems, int userId, int isCopy)
        {
            return WSHelper.F49911_InsertPartiesFieldDetails(instid, grantorItems, granteeItems, userId, isCopy);
        }

        #endregion Insert

        #region DeleteInstrumentHeaderDetails

        /// <summary>
        /// F49910_DeleteInstrumentHeader
        /// </summary>
        /// <param name="instId">instId</param>
        /// <param name="userId">userId</param>
        /// <returns>DeleteInstrumentHeader</returns>
        public int F49910_DeleteInstrumentHeader(int instId, int userId)
        {
            return WSHelper.F49910_DeleteInstrumentHeader(instId, userId);
        }

        #endregion DeleteInstrumentHeaderDetails

        #region SliceEvents
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
        #endregion SliceEvents
    }
}
