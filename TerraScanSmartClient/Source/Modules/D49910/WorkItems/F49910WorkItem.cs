//--------------------------------------------------------------------------------------------
// <copyright file="F49910WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F49910WorkItem.cs. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 31 Jan  2008 	     Ramya.D           Created
//*********************************************************************************/
namespace D49910
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
    /// F49910WorkItem
   /// </summary>
    public class F49910WorkItem : WorkItem
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

        #region InstrumentHeader

        #region GetInstrumentHeaderDetails

        /// <summary>
        /// F49910_GetInstrumentHeaderDetails
        /// </summary>
        /// <param name="instId">instId</param>
        /// <returns>DataSet</returns>
        public F49910InstrumentHeaderDataSet F49910_GetInstrumentHeaderDetails(int instId)
        {
            return WSHelper.F49910_GetInstrumentHeaderDetails(instId);
        }

        #endregion GetInstrumentHeaderDetails

        #region GetInstrumentTypeDetails

       /// <summary>
        /// F49910_GetInstrumentTypeDetails
       /// </summary>
       /// <returns>dataset</returns>
        public F49910InstrumentHeaderDataSet F49910_GetInstrumentTypeDetails()
        {
            return WSHelper.F49910_GetInstrumentTypeDetails();
        }

        #endregion GetInstrumentTypeDetails

        #region SaveInstrumentHeaderDetails

        /// <summary>
        /// F49910_SaveInstrumentHeaderDetails
        /// </summary>
        /// <param name="instId">instId</param>
        /// <param name="instrumentItems">instrumentItems</param>
        /// <param name="paymentItems">paymentItems</param>
        /// <param name="userId">userId</param>
        /// <returns>Int</returns>
        public int F49910_SaveInstrumentHeaderDetails(int instId, string instrumentItems, string paymentItems, int userId)
        {
            return WSHelper.F49910_SaveInstrumentHeaderDetails(instId, instrumentItems, paymentItems, userId);
        }

        #endregion SaveInstrumentHeaderDetails

        #region F49910CheckInstrumentHeader Deatils

        /// <summary>
        /// F49910CheckInstrumentHeaderDetails
        /// Used to validate whether the records can be saved
        /// </summary>        
        /// <param name="instId"></param>
        /// <param name="instrumentItems"></param>
        /// <param name="paymentItems"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// 0 - When the records can be saved
        /// -1 - when Instrument Number already exists in the Database
        /// </returns>
        public int F49910CheckInstrumentHeaderDetails(int instId, string instrumentItems, string paymentItems, int userId)
        {
            return WSHelper.F49910CheckInstrumentHeaderDetails(instId, instrumentItems, paymentItems, userId);
        }

        #endregion F49910CheckInstrumentHeader Deatils

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

        #region CopyInstrumentHeaderDetails

        /// <summary>
        /// F49910_CopyInstrumentHeaderDetails
        /// </summary>
        /// <param name="instrumentId">instrumentId</param>
        /// <param name="instrumentValue">instrumentValue</param>
        /// <param name="grantorValue">grantorValue</param>
        /// <param name="granteeValue">granteeValue</param>
        /// <param name="legalValue">legalValue</param>
        /// <returns>DataSet</returns>
        public F49910InstrumentHeaderDataSet F49910_CopyInstrumentHeaderDetails(int instrumentId, int instrumentValue, int grantorValue, int granteeValue, int legalValue)
        {
            return WSHelper.F49910_CopyInstrumentHeaderDetails(instrumentId, instrumentValue, grantorValue, granteeValue, legalValue);
        }

        #endregion CopyInstrumentHeaderDetails

        #region GetInstrumentHeaderDetails

        /// <summary>
        /// F49910_GetGranterAddressDetails
        /// </summary>
        /// <param name="grantId">grantId</param>
        /// <returns>DatSet</returns>
        public F49910InstrumentHeaderDataSet F49910_GetGranterAddressDetails(int grantId)
        {
            return WSHelper.F49910_GetGranterAddressDetails(grantId);
        }

        #endregion GetInstrumentHeaderDetails

        #region GetFeeDetails

        /// <summary>
        /// F49910_GetFeeDetails
        /// </summary>
        /// <param name="insTypeId">insTypeId</param>
        /// <returns>dataSet</returns>
        public F49910InstrumentHeaderDataSet F49910_GetFeeDetails(int insTypeId)
        {
            return WSHelper.F49910_GetFeeDetails(insTypeId);
        }

        #endregion GetFeeDetails

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
        #endregion InstrumentHeader
    }
}
