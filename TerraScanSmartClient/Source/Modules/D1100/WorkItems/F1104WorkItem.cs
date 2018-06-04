//--------------------------------------------------------------------------------------------
// <copyright file="F1104WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the 1016NextNumberForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26 July 06       M.VIJAYAKUMAR       Created
// 
//*********************************************************************************/

namespace D1100
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
    /// Form F1104
    /// </summary>
    public class F1104WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the District And Roll Year(Base Year)
        /// </summary>
        /// <param name="exciserateId">ExciseRateID</param>
        /// <returns>The typed dataset containing the information about district and base year</returns>
        public ExciseDistrictCopyData GetExciseDistrictCopy(int exciserateId)
        {
            return WSHelper.GetExciseDistrictCopy(exciserateId);
        }

        /// <summary>
        /// Save the Excise district Copy
        /// </summary>
        /// <param name="district">The district</param>
        /// <param name="basedOnYear">The Based On year</param>
        /// <param name="newDistrictYear">The New District year</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Dataset having district,based on year and new district year datails
        /// </returns>
        public int SaveExciseDistrictCopy(int district, int basedOnYear, int newDistrictYear, int userId)
        {
            return WSHelper.SaveExciseDistrictCopy(district, basedOnYear, newDistrictYear, userId);
        }

        /// <summary>
        /// when run started
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// when activated
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }              
    }
}
