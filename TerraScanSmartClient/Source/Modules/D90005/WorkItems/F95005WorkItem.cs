// -------------------------------------------------------------------------------------------
// <copyright file="F95005WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F95005 Reference Data</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07/06/2007       M.Vijayakumar       Created
// 
// -------------------------------------------------------------------------------------------

namespace D90005
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
    /// F95005WorkItem class file
    /// </summary>
    public class F95005WorkItem : WorkItem
    {
        #region F95005 Reference Data

        #region List Refereence Data

        /// <summary>
        /// To List the Reference Data Details
        /// </summary>
        /// <param name="masterFormNo">masterFormNo</param>
        /// <returns>Typed DataSet containg the Reference Data Details</returns>
        public DataSet F95005_ListReferenceData(int masterFormNo)
        {
            return WSHelper.F95005_ListReferenceData(masterFormNo);
        }

        #endregion List Refereence Data

        #region Save Reference Data

        /// <summary>
        /// To Save the Reference Data Details
        /// </summary>
        /// <param name="referenceData">Xml String containing the Reference Data Details</param>
        /// <param name="deletedData">The deleted data.</param>
        /// <param name="tableName">Tabel Name of the Reference Data</param>
        /// <param name="keyColumn">Key Column Name of the Reference Data Table</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Integer Value containing Whther save is performed or Not
        /// if Saved return = 0
        /// else Unsaved return = -1
        /// </returns>
        public int F95005_SaveReferenceData(string referenceData, string deletedData, string tableName, string keyColumn,int userId)
        {
            return WSHelper.F95005_SaveReferenceData(referenceData, deletedData, tableName, keyColumn,userId);
        }

        #endregion Save Reference Data

        #endregion F95005 Reference Data

        #region WorkItem Methods

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

        #endregion WorkItem Methods
    }
}
