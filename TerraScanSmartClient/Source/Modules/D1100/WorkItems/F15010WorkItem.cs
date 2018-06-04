// -------------------------------------------------------------------------------------------
// <copyright file="F15010WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F15010</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                  M.Vijayakumar      Created// 
// 
// -------------------------------------------------------------------------------------------

namespace D1100
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
    /// F15010WorkItem Class file
    /// </summary>
    public class F15010WorkItem : WorkItem
    {
        #region F15010 Excise Affidavit

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetExciseIndividualType()
        {
            return WSHelper.F15010_GetExciseIndividualType();
        }

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetExciseTaxAffidavitDetails(int statementId)
        {
            return WSHelper.F15010_GetExciseTaxAffidavitDetails(statementId);
        }

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="paymentDate">The payment date.</param>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="taxableSaleAmount">The taxable sale amount.</param>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetExciseTaxAffidavitCalulateAmountDue(DateTime saleDate, DateTime paymentDate, int exciseRateId, int taxCode, double taxableSaleAmount)
        {
            return WSHelper.F15010_GetExciseTaxAffidavitCalulateAmountDue(saleDate, paymentDate, exciseRateId, taxCode, taxableSaleAmount);
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="partiesAddress">The parties address.</param>
        /// <param name="parcelDetails">The parcel details.</param>
        /// <param name="exciseAffidavitDetails">The excise affidavit details.</param>
        /// <param name="mobileHomeDetails">The mobile home details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// returns dataset containing AffiDavit Details
        /// </returns>
        public int F15010_SaveAffiDavitDetails(int statementId, string partiesAddress, string parcelDetails, string exciseAffidavitDetails, string mobileHomeDetails, int userId)
        {
            return WSHelper.F15010_SaveAffiDavitDetails(statementId, partiesAddress, parcelDetails, exciseAffidavitDetails, mobileHomeDetails, userId);
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="orderField">The order field.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>Returns dataset for list of statementID</returns>
        public F15010ExciseAffidavitData F15010_GetAffidavitStatementId(int formId, string orderField, string orderBy)
        {
            return WSHelper.F15010_GetAffidavitStatementId(formId, orderField, orderBy);
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetOwnerDetails(int ownerId)
        {
            return WSHelper.F15010_GetOwnerDetails(ownerId);
        }

        /// <summary>
        /// Gets the owner status.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetOwnerStatus(int ownerId)
        {
            return WSHelper.F15010_GetOwnerStatus(ownerId);
        }

        /// <summary>
        /// Gets the district selection.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>Returns Dataset foe District Selection</returns>
        public F15010ExciseAffidavitData F15010_GetDistrictSelection(int exciseRateId)
        {
            return WSHelper.F15010_GetDistrictSelection(exciseRateId);
        }

        /// <summary>
        /// Delete The PArticular StatmentID Detials
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">The user id.</param>
        public void F15010_DeleteAffidavitDetails(int statementId, int userId)
        {
            WSHelper.F15010_DeleteAffidavitDetails(statementId, userId);
        }

        /// <summary>
        /// Loads the excise tax affidavit1.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Details Of All Six Header</returns>
        public F15010ExciseAffidavitData F15010_LoadExciseTaxAffidavit(int statementId)
        {
            return WSHelper.F15010_GetExciseTaxAffidavitDetails(statementId);
        }

        /// <summary>
        /// Gets the parcel detail.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <returns>F15010ExciseAffidavitData</returns>
        public F15010ExciseAffidavitData GetParcelDetail(int? parcelId, string parcelNumber)
        {
            //return WSHelper.F15010_GetParcelDetail(parcelId, parcelNumber).ListParcelDetailTable;
            return WSHelper.F15010_GetParcelDetail(parcelId, parcelNumber);
        }

        /// <summary>
        /// F15010_s the list excise WAC.
        /// </summary>
        /// <returns></returns>
        public F15010ExciseAffidavitData F15010_ListExciseWAC()
        {
            return WSHelper.F15010_ListExciseWAC();
        }

        /// <summary>
        /// F15010_s the list excise individual.
        /// </summary>
        /// <param name="ExciseIndividualElements">The excise individual elements.</param>
        /// <returns></returns>
        public F15010ExciseAffidavitData F15010_ListExciseIndividual(string ExciseIndividualElements)
        {
            return WSHelper.F15010_ListExciseIndividual(ExciseIndividualElements);
        }

        public F15010ExciseAffidavitData F15010_ListOpenSpaceField(string parcelIds)
        {
            return WSHelper.F15010_ListOpenSpaceField(parcelIds);
        }
        #endregion F15010 Excise Affidavit 

        #region F9001_GetNextWorkingDay

        /// <summary>
        /// get next working day - depends on clode time.
        /// </summary>
        /// <returns>return today or next working day</returns>
        public DateTime F9001_GetNextWorkingDay()
        {
            return WSHelper.F9001_GetNextWorkingDay();
        }

        #endregion F9001_GetNextWorkingDay

        #region To Get Configuration Roll Year

        /// <summary>
        /// Gets the config Roll Year.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #endregion To Get Configuration Roll Year

        #region GetWorkQueue Results
        /// <summary>
        /// Gets the work queue search result.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="name">The name.</param>
        /// <param name="receiptDate">The Receipt date.</param>
        /// <param name="address">The address.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="receiptNumber">The receipt number.</param>
        /// <returns>return AffidavitWorkQueueData Search</returns>
        public SubmittalQueueData GetWorkQueueSearchResult(string parcelNumber, string name, string receiptDate, string address, string taxCode, string receiptNumber, string statementNumber)
        {
            return WSHelper.F1108_ListManagementQueue(parcelNumber, name, receiptDate, address, taxCode, receiptNumber, statementNumber);
        }
         #endregion

        #region submitAffidavit
        /// <summary>
        /// F1108_s the get submit affidavit.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Returns SubmitAffidavit DatatSet</returns>
        public REETA GetSubmitAffidavit(string statementId)
        {
            return WSHelper.F1108_GetSubmitAffidavit(statementId);
        }
        #endregion

        #region List Configuration detail
        /// <summary>
        /// Gets the list configuration detail.
        /// </summary>
        /// <value>The list configuration detail.</value>
        public SubmittalQueueData.ListConfigurationDetailDataTable ListConfigurationDetail
        {
            get
            {
                return WSHelper.F1108_ListConfigurationDetail().ListConfigurationDetail;
            }
        }
        #endregion

        /// <summary>
        /// F1108_s the get submit affidavit reply.
        /// </summary>
        /// <param name="reetXml">The reet XML.</param>
        /// <param name="reetReplyXml">The reet reply XML.</param>
        /// <param name="userID">The user ID.</param>
        /// <returns>Returns SubmitAffidavit reply datatSet</returns>
        public int F1108_SaveReplyReetXml(string reetXml, string reetReplyXml, int userID)
        {
            return WSHelper.F1108_SaveReplyReetXml(reetXml, reetReplyXml, userID);
        }

        /// <summary>
        /// F1108_s the get submit affidavit reply.
        /// </summary>
        /// <param name="reetReplyXml">The reet reply XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns SubmitAffidavit reply datatSet</returns>
        public REETA F1108_GetSubmitAffidavitReply(string reetReplyXml, int userId)
        {
            return WSHelper.F1108_GetSubmitAffidavitReply(reetReplyXml, userId);
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
