//--------------------------------------------------------------------------------------------
// <copyright file="F28100WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F28100 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07/05/2011        D.LathaMaheswari  Created
//***********************************************************************************************/
namespace D23100
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
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F28100WorkItem
    /// </summary>
    public class F28100WorkItem : WorkItem
    {
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

        #region Get BOE Details
        /// <summary>
        /// Get BOE Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>BOE Details</returns>
        public F28100BOEData F28100_GetBOEDetails(int eventId)
        {
            return WSHelper.F28100_GetBOEDetails(eventId);
        }

        #endregion Get BOE Details

        #region Get Total Amount

        /// <summary>
        /// Get Total amounts
        /// </summary>
        /// <param name="boeId">boe ID</param>
        /// <param name="eventId">Event ID</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <returns>Total values</returns>
        public F28100BOEData F28100_GetTotalAmount(int boeId, int eventId, string assessedValues)
        {
            return WSHelper.F28100_GetTotalAmount(boeId, eventId, assessedValues);
        }

        #endregion Get Total Amount

        #region Save BOE Details

        /// <summary>
        /// Save BOE Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="boeItems">BOE Items</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <param name="userId">User ID</param>
        /// <returns>Primary Key</returns>
        public int F28100_SaveBOEDetails(int eventId, string boeItems, string assessedValues, int userId)
        {
            return WSHelper.F28100_SaveBOEDetails(eventId, boeItems, assessedValues, userId);
        }

        #endregion Save BOE Details

        #region Delete BOE Details

        /// <summary>
        /// Delete BOE
        /// </summary>
        /// <param name="boeId">BOE ID</param>
        /// <param name="userId">The User ID</param>
        public void F28100_DeleteBOEDetails(int? boeId, int userId)
        {
            WSHelper.F28100_DeleteBOEDetails(boeId, userId);
        }

        #endregion Delete BOE Details

        /// <summary>
        /// F9002_s the get user details.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns>DataSet</returns>
        public UserManagementData F9002_GetUserDetails(int applicationId)
        {
            return WSHelper.F9002_GetUserDetails(applicationId);
        }

        #region Push Value
        /// <summary>
        /// F28100 the push board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public void F28100_PushBOEDetails(int boeId, int userId)
        {
            WSHelper.F28100_PushBOEDetails(boeId, userId);
        }
        #endregion Push Value

        #region Local Values

        /// <summary>
        /// Get Local Values
        /// </summary>
        /// <param name="assessedValues">Assessed Value</param>
        /// <returns>Assesed Value</returns>
        public F28100BOEData F28100_GetLocalValues(string assessedValues)
        {
            return WSHelper.F28100_GetLocalValues(assessedValues);
        }

        #endregion Local Values

        #region County Values

        /// <summary>
        /// Get County Values
        /// </summary>
        /// <param name="isLocal">Is Local</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <returns>Assessed Value</returns>
        public F28100BOEData F28100_GetCountyValues(bool isLocal, string assessedValues)
        {
            return WSHelper.F28100_GetCountyValues(isLocal, assessedValues);
        }

        #endregion County Values

        #region State Values

        /// <summary>
        /// Get State Values
        /// </summary>
        /// <param name="isLocal">Is Local</param>
        /// <param name="isCounty">Is Couny</param>
        /// <param name="assessedValues">Assessed Value</param>
        /// <returns>Assessed Value</returns>
        public F28100BOEData F28100_GetStateValues(bool isLocal, bool isCounty, string assessedValues)
        {
            return WSHelper.F28100_GetStateValues(isLocal, isCounty, assessedValues);
        }

        #endregion State Values
    }
}
