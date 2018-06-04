//--------------------------------------------------------------------------------------------
// <copyright file="F1016WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the GetCountyConfiguration,UpdateCountyConfigDetails.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 27 Jul 06        VINOTHBABU         Created
//*********************************************************************************/

namespace D1016
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using System.Data;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F1016WorkItem class
    /// </summary>
    public class F1016WorkItem : WorkItem
    {
        /// <summary>
        /// Updates the next number config details.
        /// </summary>
        /// <param name="nextNumId">The next num id.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        public static void UpdateNextNumberConfigDetails(int nextNumId, int nextNum, string formula,int userId)
        {
            WSHelper.UpdateNextNumberConfigDetails(nextNumId, nextNum, formula, userId);
        }

        /// <summary>
        /// Checks the next number.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <returns>Dataset</returns>
        public static DataSet CheckNextNumber(int rollYear, int nextNum, string formula)
        {
            return WSHelper.CheckNextNumber(rollYear, nextNum, formula);
        }

        /// <summary>
        /// Lists the next number configuration.
        /// </summary>
        /// <returns>TypedDataset</returns>
        public static NextNumberData ListNextNumberConfiguration()
        {
            return WSHelper.ListNextNumberConfiguration();
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
