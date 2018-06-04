// ---------------------------------------------------------------------------------------------------------------
// <copyright file="F49912WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F49912WorkItem</summary>
// Release history
//*****************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ----------------------------------------------------------------------------
// 05/02/2008       KUPPUSAMY.B             Created
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

    public class F49912WorkItem : WorkItem
    {
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
        /// <returns></returns>
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

        #region Get LegalDetails
        /// <summary>
        /// F49912_s the list legal field.
        /// </summary>
        /// <returns></returns>
        public F49912LegalData F49912_ListLegalField(int instId)
        {
            return WSHelper.F49912_ListLegalField(instId);
        }
        #endregion Get LegalDetails

        #endregion Get

        #region Insert

        /// <summary>
        /// F49912_s the insert legal field details.
        /// </summary>
        /// <param name="instid">The instid.</param>
        /// <param name="legalItems">The legal items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F49912_InsertLegalFieldDetails(int instid, string legalItems, int userId, int isCopy)
        {
            return WSHelper.F49912_InsertLegalFieldDetails(instid, legalItems, userId, isCopy);
        }        

        #endregion Insert

        #region GetSubDivisionCombo

        /// <summary>
        /// F49912_s the list sub division combo.
        /// </summary>
        /// <returns></returns>
        public F49912LegalData F49912_ListSubDivisionCombo()
        {
            return WSHelper.F49912_ListSubDivisionCombo();
        }

        #endregion GetSubDivisionCombo

        #region DeleteInstrumentHeaderDetails

        /// <summary>
        /// F49910_DeleteInstrumentHeader
        /// </summary>
        /// <param name="instId">instId</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public int F49910_DeleteInstrumentHeader(int instId, int userId)
        {
            return WSHelper.F49910_DeleteInstrumentHeader(instId, userId);
        }

        #endregion DeleteInstrumentHeaderDetails
    }
}
