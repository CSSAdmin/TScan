//--------------------------------------------------------------------------------------------
// <copyright file="F15050 Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15050Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/05/07        A.Sriparameswari       Created                
//*********************************************************************************/

namespace D11050
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F15050WorkItem
    /// </summary>
    public class F15050WorkItem : WorkItem
    {
        /// <summary>
        /// F15050_s the combo data.
        /// </summary>
        /// <returns>Fee manamgement dataset.</returns>
        public F15050FeeManagementData F15050_ComboData()
        {
            return WSHelper.F15050_ComboData();
        }

        /// <summary>
        /// F15050_gets the datas.
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <returns>Fee manamgement dataset.</returns>
        public F15050FeeManagementData F15050_getDatas(int feeId)
        {
            return WSHelper.F15050_getDatas(feeId);
        }

        /// <summary>
        /// Gets the config details.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>config dataset</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        /// <summary>
        /// F15050_s the save fee management.
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <param name="description">The description.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="accountId">The account id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="feeTypeId">The fee type id.</param>
        /// <returns>return value</returns>
        public  int F15050_SaveFeeManagement(int feeId, string description, decimal amount, int accountId, int userId, byte feeTypeId)
        {
            return WSHelper.F15050_SaveFeeManagement(feeId, description, amount, accountId, userId, feeTypeId);
        }

        #region GetSystemSnapShotCount

        /// <summary>
        /// F9033_GetSystemSnapShotCount
        /// </summary>
        /// <param name="snapShotId">snapShotId</param>
        /// <returns>Dataset</returns>
        public F9033QueryEngineData.GetSystemSnapshotCountDataTable F9033_GetSystemSnapShotCount(int snapShotId)
        {
            return WSHelper.F9033_GetSystemSnapShotCount(snapShotId).GetSystemSnapshotCount;
        }

        #endregion GetSystemSnapShotCount

        #region ApplyFees

        /// <summary>
        /// F9033_ApplyFees
        /// </summary>
        /// <param name="sysSnapshotID">sysSnapshotID</param>
        /// <param name="amount">amount</param>
        /// <param name="description">description</param>
        /// <param name="accountId">accountId</param>
        /// <returns>Integer</returns>
        public int F9033_ApplyFees(string feeXML, decimal amount, string description, int accountId, int userId)
        {
            return WSHelper.F15050_AppyFees(feeXML, amount, description, accountId, userId);
        }

        #endregion ApplyFees

        /// <summary>
        /// F15050_s the list fee types.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The fee mgmt dataset</returns>
        public F15050FeeManagementData F15050_ListFeeTypes(int userId)
        {
            return WSHelper.F15050_ListFeeTypes(userId);
        }

        /// <summary>
        /// F15050_s the remove template.
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <param name="userId">The user id.</param>
        public void F15050_RemoveTemplate(int feeId, int userId)
        {
            WSHelper.F15050_RemoveTemplate(feeId, userId);
        }

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        } 
    }
}
