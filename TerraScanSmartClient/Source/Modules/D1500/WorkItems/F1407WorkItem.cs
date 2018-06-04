//--------------------------------------------------------------------------------------------
// <copyright file="F1407WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1407WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 2014-12-15      	Purushotham A          Created
//*********************************************************************************/

namespace D1500
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
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    public class F1407WorkItem :WorkItem
    {
        #region basic

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

        #endregion basic

        #region Configdetails
        /// <summary>
        /// Gets the Year
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Year.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }
        #endregion Configdetails

        /// <summary>
        /// F1407_s the get pull list status.
        /// </summary>
        /// <returns></returns>
        public F14062StatementPullListData F1407_GetPullListStatus()
        {
            return WSHelper.F1407_GetPullListStatus();
        }

        /// <summary>
        /// F1407_s the type of the get pull list.
        /// </summary>
        /// <returns></returns>
        public F14062StatementPullListData F1407_GetPullListType()
        {
            return WSHelper.F1407_GetPullListType();
        }

        /// <summary>
        /// F14062_s the save grid details.
        /// </summary>
        /// <param name="pullListItems">The pull list items.</param>
        /// <param name="userId">The user id.</param>
        public void F14062_SaveGridDetails(string pullListItems, int userId)
        {
            WSHelper.F14062_SaveGridDetails(pullListItems, userId);
        }

    }
}
