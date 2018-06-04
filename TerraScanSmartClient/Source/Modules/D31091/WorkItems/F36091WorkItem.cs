
namespace D31091
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

    public  class F36091WorkItem:WorkItem
    {

        #region To Get Income Source Details
        /// <summary>
        /// Gets the F36091_ListSourceDetails        
        /// </summary>
        /// <param name="valueSliceID">valueSliceID</param>
        /// <returns>Type Dataset Returns Income Sources Details</returns>
        public F36091IncomeApproachData F36091_ListSourceDetails(int valueSliceId)
        {
            return WSHelper.F36091_ListSourceDetails(valueSliceId);
        }
        #endregion

        #region Save Income Approach Details

        /// <summary>
        /// Saves the Income Approach Details.
        /// </summary>
       
        /// 

        public void F36091_SaveIncomeSourceDetails(int valueSliceId, string SourceGridDetails, string IncomeApproachDetails, int userId)
        {
            WSHelper.F36091_SaveIncomeSourceDetails(valueSliceId, SourceGridDetails,IncomeApproachDetails, userId);
        }
        #endregion

        #region To Get Source Details
        /// <summary>
        /// Gets the F36041_CropDetails        
        /// </summary>
        /// <param name="valueSliceID">valueSliceID</param>
        /// <returns>Type Dataset Returns CropDetails</returns>
        public F36091IncomeApproachData F36091_GetIncomeSources(int valueSliceId)
        {
            return WSHelper.F36091_GetIncomeSources(valueSliceId);
        }

        #endregion 

        #region To Get Approach Details
        /// <summary>
        /// Gets the F36091_ListApproachValues        
        /// </summary>
        /// <param name="incomeSourceID">incomeSourceID</param>
        /// <param name="Units">Units</param>
        /// <param name="ContractPerUnit">ContractPerUnit</param>
        /// <returns>Type Dataset Returns ApproachDetails</returns>
        public F36091IncomeApproachData F36091_ListApproachValues(int incomeSourceID, decimal Units, decimal ContractPerUnit,out decimal contract, out decimal marketperunit, out decimal market)
        {
            return WSHelper.F36091_ListApproachValues(incomeSourceID, Units, ContractPerUnit, out contract, out marketperunit, out market);
        }

        #endregion 

        #region To Get Approach Item Details
        /// <summary>
        /// Gets the F36091_GetIncomeApproachItemDetails        
        /// </summary>
        /// <param name="IncomeApproachDetails">IncomeApproachDetails</param>
        /// <returns>Type Dataset Returns Approach Details</returns>
        public F36091IncomeApproachData F36091_GetIncomeApproachItemDetails(string IncomeApproachDetails)
        {
            return WSHelper.F36091_GetIncomeApproachItemDetails(IncomeApproachDetails);
        }

        #endregion 

        #region To Delete Income Source Item Details
        /// <summary>
        /// Gets the F36091_DeleteIncomeSource        
        /// </summary>
        /// <param name="incomesourceIds">incomesourceIds</param>
        ///  <param name="userId">userId</param>
        public void F36091_DeleteIncomeSource(string incomesourceIds, int userId)
        {
             WSHelper.F36091_DeleteIncomeSource(incomesourceIds,userId);
        }

        #endregion 


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
