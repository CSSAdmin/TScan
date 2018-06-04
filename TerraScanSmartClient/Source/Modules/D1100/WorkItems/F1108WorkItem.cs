//--------------------------------------------------------------------------------------------
// <copyright file="F1108WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1108.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 Nov 06		KARTHIKEYAN V	    Created
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
    /// F1108WorkItem
    /// </summary>
    public class F1108WorkItem : WorkItem
    {
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

        /// <summary>
        /// F1108_s the get submit affidavit.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Returns SubmitAffidavit DatatSet</returns>
        public REETA GetSubmitAffidavit(string statementId)
        {
            return WSHelper.F1108_GetSubmitAffidavit(statementId);
        }

        /// <summary>
        /// Gets the reet web service reply.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="password">The password.</param>
        /// <param name="xml">The XML.</param>
        /// <param name="amend">if set to <c>true</c> [amend].</param>
        /// <returns>Returns SubmitAffidavit DatatSet</returns>
        public string GetReetWebServiceReply(string url, string methodName, string userId, string password, string xml, Boolean amend)
        {
            return WSHelper.F1108_GetReetWebServiceReply(url, methodName, userId, password, xml, amend);
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
    }
}
