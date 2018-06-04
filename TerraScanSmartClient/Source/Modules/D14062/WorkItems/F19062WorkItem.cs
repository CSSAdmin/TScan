//--------------------------------------------------------------------------------------------
// <copyright file="14062WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------

// 9-Dec-2014       Purushotham A       Created
//*********************************************************************************/
namespace D14062
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

    public class F19062WorkItem : WorkItem
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



        /// <summary>
        /// F14062_s the grid result details.
        /// </summary>
        /// <param name="ownerIds">The owner ids.</param>
        /// <param name="statementIds">The statement ids.</param>
        /// <param name="parcelIds">The parcel ids.</param>
        /// <param name="scheduleIds">The schedule ids.</param>
        /// <param name="stateIds">The state ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public F14062StatementPullListData F14062_GridResultDetails(string ownerIds, string statementIds, string parcelIds, string scheduleIds, string stateIds, int userId)
        {
            return WSHelper.F14062_GridResultDetails(ownerIds, statementIds, parcelIds, scheduleIds, stateIds, userId);
        }

        /// <summary>
        /// F14062_s the get statement pull list details.
        /// </summary>
        /// <returns></returns>
        public F14062StatementPullListData F14062_GetStatementPullListDetails()
        {
           return WSHelper.F14062_GetStatementPullListDetails();            
        }

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

       /// <summary>
       /// F14062_s the delete statement pull list.
       /// </summary>
       /// <param name="pullListItems">The pull list items.</param>
       /// <param name="userId">The user id.</param>
       /// <param name="isProcess">if set to <c>true</c> [is process].</param>
       /// <returns></returns>
       public string F14062_DeleteStatementPullList(string pullListItems, int userId, bool isProcess)
       {
           return WSHelper.F14062_DeleteStatementPullList(pullListItems, userId, isProcess);
       }
    }
}