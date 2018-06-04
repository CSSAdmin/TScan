// -------------------------------------------------------------------------------------------
// <copyright file="F15015WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access and Update F15015 Statement Ownership
// </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/04/07         M.Vijayakumar       Created
// -------------------------------------------------------------------------------------------

namespace D11015
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
    /// F15015WorkItem Class file
    /// </summary>
    public class F15015WorkItem : WorkItem
    {
        #region F15015 Statement Ownership

        #region List Statement Ownership

        /// <summary>
        /// To List Statement Ownership Details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Typed Dataset Containing the Statement Ownership Details</returns>
        public F15015StatementOwnershipData F15015_ListStatementOwnership(int statementId)
        {
            return WSHelper.F15015_ListStatementOwnership(statementId);
        }

        #endregion List Statement Ownership

        #region Save Statement Ownership

        /// <summary>
        /// To Save Statement Ownership Details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="statementOwner">The statement owner.</param>
        /// <param name="userID">The user ID.</param>
        public void F15015_SaveStatementOwnership(int statementId, string statementOwner, int userID)
        {
            WSHelper.F15015_SaveStatementOwnership(statementId, statementOwner, userID);
        }

        #endregion Save Statement Ownership

        #endregion F15015 Statement Ownership

        #region Check Ownership Details

        /// <summary>
        /// To Check Given Ownership Details is valid.
        /// </summary>
        /// <param name="ownershipDetails">The ownership details.</param>
        /// <returns>returns an integer Value whather given details are correct or not</returns>
        public int F27006_CheckOwnershipDetails(string ownershipDetails)
        {
            return WSHelper.F27006_CheckOwnershipDetails(ownershipDetails);
        }

        #endregion Check Ownership Details

        #region List All Owner Details

        /// <summary>
        /// To List All Owners Details
        /// </summary>
        /// <param name="firstName">The First Name.</param>
        /// <param name="lastName">The Last Name.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="city">The city.</param>
        /// <returns>Typed Dataset Containg the All Owners Details</returns>
        public F15015StatementOwnershipData F15015_ListALLOwnerDetails(string firstName, string lastName, string address1, string address2, string city)
        {
            return WSHelper.F15015_ListALLOwnerDetails(firstName, lastName, address1, address2, city);
        }

        #endregion List All Owner Details       

        #region List MOwnerType

        /// <summary>
        /// Lists the type of the M owner.
        /// </summary>
        /// <returns>MOwner Type List.</returns>
        public F15015StatementOwnershipData F15015_ListMOwnerType()
        {
            return WSHelper.F15015_ListMOwnerType();
        }

        #endregion

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
