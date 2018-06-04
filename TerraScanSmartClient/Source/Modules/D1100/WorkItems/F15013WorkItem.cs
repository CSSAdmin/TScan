//--------------------------------------------------------------------------------------------
// <copyright file="F15013WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15013WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Jan 24        JYOTHI              Created
//*********************************************************************************/
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
    /// F15013WorkItem
    /// </summary>
    public class F15013WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>returns dataset contains Excise Tax Rate Details</returns>
        public F15013ExciseTaxRateData F15013_GetExciseTaxRate(int exciseRateId)
        {
            return WSHelper.F15013_GetExciseTaxRate(exciseRateId);
        }

        /// <summary>
        /// Lists the excise tax rate.
        /// </summary>
        /// <returns>returns dataset contains Excise Tax Rate Details</returns>
        public F15013ExciseTaxRateData F15013_ListExciseTaxRate()
        {
            return WSHelper.F15013_ListExciseTaxRate();
        }


        /// <summary>
        /// F15013_s the save excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="exciseTaxDetails">The excise tax details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>errorId/primary Key Id</returns>
        public int F15013_SaveExciseTaxRate(int exciseRateId, string exciseTaxDetails, int userId)
        {
            return WSHelper.F15013_SaveExciseTaxRate(exciseRateId, exciseTaxDetails, userId);
        }

        /// <summary>
        /// Gets the Excise Tax Statement
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>returns dataset contains District Name</returns>
        public F15013ExciseTaxRateData F15013_GetDistrictName(int districtId)
        {
            return WSHelper.F15013_GetDistrictName(districtId);
        }

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public F15013ExciseTaxRateData F15013_GetAccountName(int accountId)
        {
            return WSHelper.F15013_GetAccountName(accountId);
        }

        /// <summary>
        /// Gets the Year
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Year.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        /// <summary>
        /// Deletes the Excise Tax Rate
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="userID">The user ID.</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public int F15013_DeleteExciseTaxRate(int exciseRateId, int userID)
        {
            return WSHelper.F15013_DeleteExciseTaxRate(exciseRateId, userID);
        }

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        /// <summary>
        /// Gets the Attachments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
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
    }
}
