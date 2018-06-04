// -------------------------------------------------------------------------------------------------
// <copyright file="F34100WorkItem.cs" company="Congruent">
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

namespace D34100
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;


    /// <summary>
    /// workItem for F34100
    /// </summary>
    public class F39100WorkItem : WorkItem
    {
        #region F39100_GetTIFDetails

        /// <summary>
        /// F27081_GetTIFDistrictDetails
        /// </summary>
        /// <param name="TIFId">TIFId</param>
        /// <returns>DataSet</returns>
        public F34100AglandUseData F34100_GetAglandDetails(int AglandID)
        {
            return WSHelper.F34100_GetAglandDetails(AglandID);
        }

        #endregion

        #region F34100_SaveAglandDetails

        /// <summary>
        /// F27081_SaveTIFDistrict
        /// </summary>
        /// <param name="TIFID">TIFID</param>
        /// <param name="TIFDetails">TIFDetails</param>
        /// <returns>int</returns>
        public int F34100_SaveAglandDetails(int? AglandID, string AglandIDDetails, int userId)
        {
            return WSHelper.F34100_saveAglandDetails(AglandID, AglandIDDetails, userId);
        }

        #endregion

        #region F34100_DeleteAgland Detaqils

        ///<summary>
        /// F34100_Delete Agland Details
        /// </summary>
        /// <Param name="AglandID">AglandID</Param>
        /// <Param name="AglandID">UserID</Param>
        public void F34100_DeleteAglandDetails(int AglandID, int UserId)
        {
            WSHelper.F34100_DeleteAglandDetails(AglandID, UserId);    
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
