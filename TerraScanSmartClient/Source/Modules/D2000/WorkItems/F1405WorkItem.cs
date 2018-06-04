//--------------------------------------------------------------------------------------------
// <copyright file="F1405WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1405WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 Nov 2010        P.Manoj            Created
//*********************************************************************************/



namespace D2000
{
    using Microsoft.Practices.CompositeUI;  
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;
    
    /// <summary>
    /// F1405WorkItem
    /// </summary>
    public class F1405WorkItem : WorkItem 
    {
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

        /// <summary>
        /// F1405_s the list state search.
        /// </summary>
        /// <param name="ScheduleConditionXML">The state condition XML.</param>
        /// <returns></returns>
        public F1405StateSelectionData F1405_ListStateSearch(string stateConditionXML)
        {
            return WSHelper.F1405_ListStateSearch(stateConditionXML);
        }


        /// <summary>
        /// F1401_s the type of the get parcel.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F1401ParcelSearch</returns>
        public F1403ParcelSearch F1403_GetParcelType(int? parcelId)
        {
            return WSHelper.F1403_GetParcelType(parcelId);
        }
        /// <summary>
        /// F1404_s the type of the get state.
        /// </summary>
        /// <param name="scheduleId">The state id.</param>
        /// <returns></returns>
        //public F1405StateSelectionData F1405_GetStateType(int? stateId)
        //{
        //    return WSHelper.F1405_GetStateType(stateId);
        //}


    }
}
