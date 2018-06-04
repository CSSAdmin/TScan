// --------------------------------------------------------------------------------------------
// <copyright file="F1345WorkItem.cs" company="Congruent">
//      Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//  This file contains methods for the Account Selection.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date               Author            Description
// ----------        ---------          ---------------------------------------------------------
// 26 July 06      KRISHNA ABBURI       Created
// 31 Aug  09      Sadha Shivudu M      Implemented TSCO # 3372 Add new field function. 
// *********************************************************************************/

namespace D1500
{
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F1345WorkItem class file
    /// </summary> 
    public class F1345WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the account selection data.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="bars">The bars value.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="objectname">The objectname.</param>
        /// <param name="line">The line number.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="desciption">The desciption.</param>
        /// <param name="iscash">The is cash value.</param>
        /// <returns>The account selection dataset.</returns>
        public static AccountSelectionData GetAccountSelectionData(string subFund, string bars, string functionName, string objectname, string line, int rollYear, string desciption, int iscash)
        {
            return WSHelper.GetAccountSelectionData(subFund, bars, functionName, objectname, line, rollYear, desciption, iscash);
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
