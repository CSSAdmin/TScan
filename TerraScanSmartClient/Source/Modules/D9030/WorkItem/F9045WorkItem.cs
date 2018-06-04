//----------------------------------------------------------------------------------
// <copyright file="F9045WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the State Code.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			  Author		         Description
// ----------	  ----------		     -------------------------------------------
// 13/10/2011     P.Manoj Kumar    Created
//*********************************************************************************/


namespace D9030
{
    #region NameSpaces

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

    #endregion NameSpaces

    /// <summary>
    /// F9045WorkItem
    /// </summary>
    public class F9045WorkItem : WorkItem 
    {
        /////<summary>
        /////Generic Search Form Label Information 
        ///// </summary>
        public F9045GenericSearchData F9045GetConfiguration(int GenericSearchID)
        {
            return WSHelper.F9045GetConfiguration(GenericSearchID);
        }

        ///<summary>
        /// generic Search List for DataGrid
        /// </summary>
        public F9045GenericSearchData F9045GetSearchResults(int GenericSearchID, string SearchData, int UserId)
        {
            return WSHelper.F9045GetSearchResults(GenericSearchID,SearchData,UserId);
        }

     

        #region Base Methods
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

        #endregion Base Methods.

    }
}
