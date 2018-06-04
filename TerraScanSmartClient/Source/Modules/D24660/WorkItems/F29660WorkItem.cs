// -------------------------------------------------------------------------------------------------
// <copyright file="F29660WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//***********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//  
//***********************************************************************************************/


namespace D24660
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper; 

    /// <summary>
    /// workItem for F29660
    /// </summary>
    public class F29660WorkItem : WorkItem
    {
        #region F29660_SaveTIFEvents

        /// <summary>
        /// F29660_SaveTIFEvent
        /// </summary>
        /// <param name="EventID">EventID</param>
        /// <param name="TIFID">TIFID</param>
        /// <param name="BaseValue">BaseValue</param>
        /// <param name="TIFID">UserID</param>
        /// <returns>int</returns>
        public int F29660_SaveEventDetails(int? EventID, int TIFID, decimal basevalue, int userId)
        {
            return WSHelper.F29660_saveEventDetails(EventID, TIFID, basevalue, userId);
        }

        #endregion

        #region F29660_GetTIFEvents

        /// <summary>
        /// F29660_GetTIFEventDetails
        /// </summary>
        /// <param name="EventId">EventId</param>
        /// <returns>DataSet</returns>
        public F29660TIFEventData F29660_GetTIFEventDetails(int EventId, int userId)
        {
            return WSHelper.F29660_GetTIFEventDetails(EventId, userId);      
        }


        #endregion

        #region Work Item Methods

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
        #endregion
 
    }
}
