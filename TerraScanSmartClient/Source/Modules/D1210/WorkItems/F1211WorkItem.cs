//--------------------------------------------------------------------------------------------
// <copyright file="F1107WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 Oct 06		KARTHIKEYAN V	    Created
//*********************************************************************************/
namespace D1210
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
    /// F1211WorkItem 
    /// </summary>
    public class F1211WorkItem :WorkItem
    {
        /// <summary>
        /// Gets the disbursement check list.
        /// </summary>
        /// <returns>DisbursementCheckStagingData Dataset</returns>
        public DisbursementCheckStagingData F1211_GetDisbursementCheckList
        {
            get
            {
                return WSHelper.F1211_GetDisbursementCheckList();
            }
        }

        /// <summary>
        /// Updates the check staging.
        /// </summary>
        /// <param name="tclId">The TCL id.</param>
        /// <param name="disbursementCheck">The disbursement check.</param>
        /// <param name="checkItems">The check items.</param>
        public void F1211_UpdateCheckStaging(int tclId, string disbursementCheck, string checkItems, int userId)
        {
            WSHelper.F1211_UpdateCheckStaging(tclId, disbursementCheck, checkItems, userId);
        }

        /// <summary>
        /// Updates the agency valid status.
        /// </summary>
        /// <param name="tclIds">The TCL ids.</param>
        /// <param name="validStatus">The valid status.</param>
        public void F1211_UpdateAgencyValidStatus(string tclIds, int validStatus, int userId)
        {
            WSHelper.F1211_UpdateAgencyValidStatus(tclIds, validStatus, userId);
        }

        /// <summary>
        /// F1211_s the delete check staging.
        /// </summary>
        /// <param name="tclIdDelete">The TCL id delete.</param>
        public void F1211_DeleteCheckStaging(string tclIdDelete, int userId)
        {
            WSHelper.F1211_DeleteCheckStaging(tclIdDelete, userId);
        }

        /// <summary>
        /// F1211_s the create checks.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="createChecksTclId">The create checks TCL id.</param>
        /// <returns>Returns Count</returns>
        public int F1211_CreateChecks(int userId, string createChecksTclId)
        {
            return WSHelper.F1211_CreateChecks(userId, createChecksTclId);
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
    }
}
