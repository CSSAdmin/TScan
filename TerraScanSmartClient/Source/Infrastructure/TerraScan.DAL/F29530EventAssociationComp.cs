// -------------------------------------------------------------------------------------------
// <copyright file="F29530EventAssociationComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F29520EventAssociationComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 23/09/07        A.Sriparameswari      Created
// -------------------------------------------------------------------------------------------


namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F29530EventAssociationComp Class file
    /// </summary>
   public static class F29530EventAssociationComp
   {
       #region FillDatGridEventAssociation

       /// <summary>
       /// F29530_FillAssociationEventGrid
       /// </summary>
       /// <param name="eventId">eventId</param>
       /// <returns>Typed dataset</returns>
       public static F29530EventAssociationData F29530_FillAssociationEventGrid(int eventId)
       {
           F29530EventAssociationData associationEvent = new F29530EventAssociationData();
           Hashtable ht = new Hashtable();
           ht.Add("@EventID", eventId);
           Utility.LoadDataSet(associationEvent.ListEventAssociationTable, "f29530_pclst_AssociationEvents", ht);
           return associationEvent;
       }

       #endregion
   }
}
