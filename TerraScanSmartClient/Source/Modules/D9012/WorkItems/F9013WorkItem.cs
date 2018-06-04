// -------------------------------------------------------------------------------------------
// <copyright file="F9013WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F9013</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                  M.Vijayakumar       Created// 
// 
// -------------------------------------------------------------------------------------------

namespace D9012
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
    /// F9013WorkItem Class file
    /// </summary>
    public class F9013WorkItem : WorkItem
    {
        #region F9013 Next Number Configuration

        #region List NextNumber Configuration

        /// <summary>
        /// List the NextNumber Configuration details
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The dataset containing the list of NextNumber Configuration.
        /// </returns>
        public F9013NextNumberData F9013_ListNextNumberConfiguration(int rollYear, int userId)
        {
            return WSHelper.F9013_ListNextNumberConfiguration(rollYear, userId);
        }

        #endregion List NextNumber Configuration

        #region Check Next Number

        /// <summary>
        /// Check for valid Next Number
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <returns>The dataset containing the valid Next Number details.</returns>
        public DataSet F9013_CheckNextNumber(int rollYear, int nextNum, string formula)
        {
            return WSHelper.F9013_CheckNextNumber(rollYear, nextNum, formula);
        }

        #endregion Check Next Number

        #region Update NextNumber ConfigDetails

        /// <summary>
        /// Saves Next Number configuration details
        /// </summary>
        /// <param name="nextNumId">The next num id.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        public void F9013_UpdateNextNumberConfigDetails(int nextNumId, int nextNum, string formula,int userID)
        {
            WSHelper.F9013_UpdateNextNumberConfigDetails(nextNumId, nextNum, formula,userID);
        }

        #endregion Update NextNumber ConfigDetails

        #region List Roll Year

        /// <summary>
        /// To List next number roll year.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The dataset containing the list of Next Number RollYear.
        /// </returns>
        public F9013NextNumberData F9013_ListNextNumberRollYear(int userId)
        {
            return WSHelper.F9013_ListNextNumberRollYear(userId);
        }

        #endregion List Roll Year

        #endregion F9013 Next Number Configuration

        #region WorkItems Methods

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

        #endregion WorkItems Methods
    }
}