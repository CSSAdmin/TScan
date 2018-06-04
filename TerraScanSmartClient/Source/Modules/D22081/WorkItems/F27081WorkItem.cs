// -------------------------------------------------------------------------------------------------
// <copyright file="F27080WorkItem.cs" company="Congruent">
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



namespace D22081
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities; 

    /// <summary>
    /// workItem for F27081
    /// </summary>
    public class F27081WorkItem : WorkItem
    {

        #region F27081_GetTIFDetails

        /// <summary>
        /// F27081_GetTIFDistrictDetails
        /// </summary>
        /// <param name="TIFId">TIFId</param>
        /// <returns>DataSet</returns>
        public F27081TIFDistrictData F27081_GetTIFDistrictDetails(int TIFIdDistId)
        {
            return WSHelper.F27081_GetTIFDistrictDetails(TIFIdDistId);
        }

        #endregion
        
        #region F27081_SaveTIFDetails

        /// <summary>
        /// F27081_SaveTIFDistrict
        /// </summary>
        /// <param name="TIFID">TIFID</param>
        /// <param name="TIFDetails">TIFDetails</param>
        /// <returns>int</returns>
        public int F27081_SaveDistrictDetails(int? TIFIdDistId, string TIFDetails, int userId)
        {
            return WSHelper.F27081_saveDistrictDetails(TIFIdDistId, TIFDetails, userId);
        }
        
        #endregion

        /// <summary>
        /// F27081_s the delete TIF district details.
        /// </summary>
        /// <param name="TIFIdDistId">The TIF id dist id.</param>
        /// <param name="userId">The user id.</param>
        public string F27081_DeleteTIFDistrictDetails(int TIFIdDistId, int userId, bool IsReadyToDelete)
        {
            return WSHelper.F27081_DeleteTIFDistrictDetails(TIFIdDistId, userId, IsReadyToDelete);
        }
        #region F1515_GetSubFundSelection

        /// <summary>
        /// To Get the Sub Fund Selection Details
        /// </summary>
        /// <param name="subFund">The Sub fund</param>
        /// <param name="description">The Description</param>
        /// <param name="rollYear">The Roll year</param>
        /// <returns>Typed Dataset containing the Sub Fund Selection Details</returns>
        public F1515SubFundSelectionData F1515_GetSubFundSelection(string subFund, string description, int rollYear, int iscash)
        {
            return WSHelper.F1515_GetSubFundSelection(subFund, description, rollYear, iscash);
        }

        /// <summary>
        /// F27081_s the get TIF combo box details.
        /// </summary>
        /// <returns></returns>
        public F27081TIFDistrictData F27081_GetTIFComboBoxDetails(int RollYear)
        {
            return WSHelper.F27081_GetTIFComboBoxDetails(RollYear);
        }
        #endregion F1515_GetSubFundSelection

        #region ConfigDetails

        /// <summary>
        /// Gets the config Roll Year.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
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
