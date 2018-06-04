//--------------------------------------------------------------------------------------------
// <copyright file="F9080WorkItem.cs" company="Congruent">
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
// 22 Nov 2011      Manoj P             Created
//*********************************************************************************/


namespace D9080
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
    /// F9080 WorkItem class file
    /// </summary>
    public class F9080WorkItem : WorkItem
    {
        /// <summary>
        /// F9080_s the list Roll Year Management.
        /// </summary>
        /// <param name="userID">The User ID.</param>
        /// <returns>Typed DataSet</returns>
        public F9080RollYearManagementData F9080_ListRollYearManagement(int userId)
        {
            return WSHelper.F9080_ListRollYearManagement(userId);
        }

        /// <summary>
        /// F9080_s the Get Roll Year Management.
        /// </summary>
        /// <param name="RollYearID">The Roll Year ID.</param>
        /// <param name="userID">The User ID.</param>
        /// <returns>Typed DataSet</returns>
        public F9080RollYearManagementData F9080_GetRollYearManagement(short rollYear,int userId)
        {
            return WSHelper.F9080_GetRollYearManagement(rollYear,userId);
        }

        /// <summary>
        /// F9080_s the Exec Roll Year Management.
        /// </summary>
        /// <param name="RollOverID">The Roll Over ID.</param>
        /// <param name="userID">The User ID.</param>
        /// <returns>Typed DataSet</returns>
        public  string F9080_ExecRollYearStep(short rollOverId, int userId)
        {
            return WSHelper.F9080_ExecRollYearStep(rollOverId, userId);
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
