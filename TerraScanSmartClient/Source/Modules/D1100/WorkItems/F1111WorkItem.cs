//--------------------------------------------------------------------------------------------
// <copyright file="F1111WorkItem.cs" company="Congruent">
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
// 
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F1111 WorkItem class file
    /// </summary>
    public class F1111WorkItem : WorkItem
    {
        /// <summary>
        /// Updates the excise affidavit status.
        /// </summary>
        /// <param name="statementID">The statement ID.</param>
        /// <param name="treasurerStatus">The treasurer status.</param>
        /// <param name="statusID">The status ID.</param>
        /// <param name="userID">The user ID.</param>
        public void UpdateExciseAffidavitStatus(int statementID, int treasurerStatus, int statusID, int userID)
        {
            WSHelper.UpdateExciseAffidavitStatus(statementID, treasurerStatus, statusID, userID);
        }

        /// <summary>
        /// Gets the excise tax affidavit status.
        /// </summary>
        /// <param name="statementID">The statement ID.</param>
        /// <param name="treasurerStatus">The treasurer status.</param>
        /// <returns>
        /// returns dataset with GetExciseTaxAffidavitStatus datatable
        /// </returns>
        public ExciseAffidavitValidationData GetExciseTaxAffidavitStatus(int statementID, int treasurerStatus)
        {
            return WSHelper.GetExciseTaxAffidavitStatus(statementID, treasurerStatus);
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
