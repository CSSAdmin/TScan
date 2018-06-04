//--------------------------------------------------------------------------------------------
// <copyright file="F29630WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F29620 Agland Application.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 01/10/2008        D.LathaMaheswari  Created
//***********************************************************************************************/
namespace D24630
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
    /// F29630WorkItem
    /// </summary>
    public class F29630WorkItem : WorkItem
    {
        /// <summary>
        /// F29630s the get board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id value.</param>
        /// <returns>Int</returns>
        public F29630BoardOfEqualizationData F29630GetBoardOfEqualizationDetails(int boeId)
        {
            return WSHelper.F29630GetBoardOfEqualizationDetails(boeId);
        }

        /// <summary>
        /// F29630s the save board of equalization details.
        /// </summary>
        /// <param name="boeElements">The boe elements.</param>
        /// <param name="boeValues">The boe values.</param>
        /// <param name="userId">The user id.</param>
        public void F29630SaveBoardOfEqualizationDetails(string boeElements, string boeValues, int userId)
        {
            WSHelper.F29630SaveBoardOfEqualizationDetails(boeElements, boeValues, userId);
        }

        /// <summary>
        /// F29630s the delete board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public void F29630DeleteBoardOfEqualizationDetails(int boeId, int userId)
        {
            WSHelper.F29630DeleteBoardOfEqualizationDetails(boeId, userId);
        }

        /// <summary>
        /// F29630s the push board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public void F29630PushBoardOfEqualizationDetails(int boeId, int userId)
        {
            WSHelper.F29630PushBoardOfEqualizationDetails(boeId, userId);
        }

        /// <summary>
        /// F9002_s the get user details.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns>DataSet</returns>
        public UserManagementData F9002_GetUserDetails(int applicationId)
        {
            return WSHelper.F9002_GetUserDetails(applicationId);
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
