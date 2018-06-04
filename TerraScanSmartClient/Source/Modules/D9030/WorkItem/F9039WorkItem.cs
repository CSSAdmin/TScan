//--------------------------------------------------------------------------------------------
// <copyright file="F9039WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 Aug 07        VINOTH             Created
//*********************************************************************************/

namespace D9030
{
    #region NameSpace

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

    #endregion NameSpace

    /// <summary>
    /// F9039WorkItem
    /// </summary>
    public class F9039WorkItem : WorkItem
    {
        #region ListQueryViewColumn

        /// <summary>
        /// F9039s the list query view column.
        /// </summary>
        /// <param name="queryViewId">The query view ID.</param>
        /// <returns></returns>
        public F9039QueryUpdate F9039_ListQueryViewColumn(int queryViewId)
        {
            return WSHelper.F9039ListQueryViewColumn(queryViewId);
        }

        #endregion ListQueryViewColumn

        #region GetCommandResult

        /// <summary>
        /// F9039s the get command result.
        /// </summary>
        /// <param name="replaceId">The replace Id.</param>
        /// <param name="commandResult">The command result.</param>
        /// <returns></returns>
        public DataSet F9039_GetCommandResult(int replaceId, string commandResult)
        {
            return WSHelper.F9039GetCommandResult(replaceId, commandResult);
        }

        #endregion GetCommandResult

        /// <summary>
        /// F9039s the get TaxRollCollection.
        /// </summary>
        /// <param name="parcelId">The Parcel Id.</param>
        /// <returns></returns>
        public F2550TaxRollCorrectionData F9039_ListTaxRollCorrection(string parcelId, string scheduleId, string stateId, string centralXmlIds)
        {
            return WSHelper.F2550_ListParcelDetails(parcelId, scheduleId,stateId,centralXmlIds);
        }

        #region UpdateQueryData

        /// <summary>
        /// F9039s the update query data.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="keyField">The key field.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="updateField">The update field.</param>
        ///   <param name="doProcessValue">doProcessValue.</param>
        ///  <param name="userId">userId.</param>
        /// <returns></returns>
        public string F9039_UpdateQueryData(int queryViewId, string keyField, string keyId, string updateField,int doProcessValue,int userId)
        {
            return WSHelper.F9039UpdateQueryData(queryViewId, keyField, keyId, updateField, doProcessValue,userId);
        }

        #endregion UpdateQueryData

        #region WorkItem Events

        /// <summary>
        /// Override Method for OnRunStarted
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Override Method for OnActivated
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion WorkItem Events
    }
}
