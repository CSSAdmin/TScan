// -------------------------------------------------------------------------------------------
// <copyright file="F25009WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F25009</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
// -------------------------------------------------------------------------------------------

namespace D20009
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

    public class F25009WorkItem : WorkItem
    {
        #region F25009 Legal Management

        #region Get Legal Management

        /// <summary>
        ///  To Load F25009 Legal Management.
        /// </summary>
        /// <param name="parcelId">The Parcel ID.</param>
        /// <returns>Typed DataSet Containing All the Legal Management Details</returns>
        public F25009LegalManagementData F25009_GetLegalManagement(int parcelId, int userId)
        {
            return WSHelper.F25009_GetLegalManagement(parcelId, userId);
        }

        #endregion

        #region Save Legal Management

        /// <summary>
        /// To Save F25009 Legal Management.
        /// </summary>
        /// <param name="legalId">The Legal ID.</param>
        /// <param name="legalItems">The XML string Containing All values in Legal Management.</param>
        /// <returns>The integer value containing Parcel id</returns>
        public int F25009_SaveLegalManagement(int legalId, string legalItems,bool isFuturePush, int userId)
        {
            return WSHelper.F25009_SaveLegalManagement(legalId, legalItems,isFuturePush,userId);
        }

        #endregion

        #region List Subdivision

        /// <summary>
        ///  To Load Subdivision.
        /// </summary>        
        /// <returns>Typed DataSet Containing All the Subdivision Details</returns>
        public F25009LegalManagementData F25009_ListSubdivision()
        {
            return WSHelper.F25009_ListSubdivision();
        }

        #endregion

        #endregion

        #region To Vaildate the Parcel ID is Valid

        /// <summary>
        /// F2001_gets the parcel locking details.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <returns>Typed dataset</returns>
        public F2001ParcelLockingData F2001_getParcelLockingDetails(int keyId)
        {
            return WSHelper.F2001_getParcelLockingDetails(keyId);
        }

        #endregion To Vaildate the Parcel ID is Valid

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
