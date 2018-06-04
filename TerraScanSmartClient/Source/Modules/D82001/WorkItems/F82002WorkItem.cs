// -------------------------------------------------------------------------------------------
// <copyright file="F82002WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access and Update F82002 Statement Ownership
// </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/04/07        A.Sriparameswari       Created
// -------------------------------------------------------------------------------------------

namespace D82001
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
    /// F82002WorkItem
    /// </summary>
    public class F82002WorkItem : WorkItem
    {
        /// <summary>
        /// F82002_ListContractorManagementData
        /// </summary>
        /// <param name="iContractorId">iContractorId</param>
        /// <param name="contractorXml">contractorXml</param>
        /// <returns>data</returns>
         public F82002ContractorManagementData F82002_ListContractorManagementData(int? iContractorId, string contractorXml)
         {
             return WSHelper.F82002_ListContractorManagementData(iContractorId, contractorXml);
         }

        /// <summary>
         /// F82002_InsertBuildingPermitDetails
        /// </summary>
         /// <param name="iContractorId">iContractorId</param>
         /// <param name="contractorItems">contractorItems</param>
         /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public int F82002_InsertBuildingPermitDetails(int? iContractorId, string contractorItems, int userId)
        {
            return WSHelper.F82002_InsertBuildingPermitDetails(iContractorId, contractorItems, userId);
        }

        /// <summary>
        /// F82002_DeleteContractorManagement
        /// </summary>
        /// <param name="contractorId">contractorId</param>
        /// <param name="userId">userId</param>
        public void F82002_DeleteContractorManagement(int contractorId, int userId)
        {
            WSHelper.F82002_DeleteContractorManagement(contractorId, userId);
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
