// -------------------------------------------------------------------------------------------------
// <copyright file="F1423WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19 Dec 06        Ranjani            Created// 
//*********************************************************************************/
namespace D11020
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

    /// <summary>
    /// F1423 WorkItem
    /// </summary>
    public class F1423WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the real Property statement based on the statement id
        /// </summary>
        /// <param name="statementId">The statement id of the statement to be fetched.</param>
        /// <returns>
        /// The typed dataset containing the statement information of the statementid.
        /// </returns>
        public F11020RealPropertyData F11020_GetRealPropertyStatement(int statementId)
        {
            return WSHelper.F11020_GetRealPropertyStatement(statementId);
        }

        /// <summary>
        /// update the real Property statement based on the statement id
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="statementItems">The statement items.</param>
        public void F1423_UpdateRealPropertyStatement(int statementId, string statementItems, int userId)
        {
            WSHelper.F1423_UpdateRealPropertyStatement(statementId, statementItems, userId);
        }

        /// <summary>
        /// list the mortgage name.
        /// </summary>
        /// <returns>F11020RealPropertyData with morgage name list</returns>
        public F11020RealPropertyData F1423_ListMortgageName()
        {
            return WSHelper.F1423_ListMortgageName();
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
