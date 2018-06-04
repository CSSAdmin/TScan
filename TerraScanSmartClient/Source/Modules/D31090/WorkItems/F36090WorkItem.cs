
namespace D31090
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

    public  class F36090WorkItem:WorkItem
    {
        #region List UnitTermsData
        /// <summary>
        /// Lists the UnitTerms Data.
        /// </summary>
        /// <returns>The dataset containing the unit Terms</returns>
        public F36090IncomeSourceData ListUnitTerms()
        {
            return WSHelper.ListUnitTerms();
        }
        #endregion

        #region Get
        /// <summary>
        /// Gets the Income Source detail .
        /// </summary>
        /// <param name="templateId">The IncomeSource id.</param>
        /// <returns>DataSet With income source Details</returns>
        public F36090IncomeSourceData GetIncomeSourceDetail(int IncomeSourceID)
        {
            return WSHelper.GetIncomeSourceDetail(IncomeSourceID);
        }
        #endregion

        #region Save income source details

        /// <summary>
        /// Saves the income source details.
        /// </summary>
        /// <param name="IncomeSourceID">The IncomeSource id.</param>
        /// <param name="IncomeSourceItems">The IncomeSourceItems.</param>
        /// <param name="userId">The userId.</param>
        /// 

        public int SaveIncomeSourceDetails(int? IncomeSourceID, string IncomeSourceItems, int userId)
        {
            return WSHelper.SaveIncomeSourceDetails(IncomeSourceID, IncomeSourceItems, userId);
        }
        #endregion

        #region Delete Income Source Details

        /// <summary>
        /// Deletes the Income Source Details.
        /// </summary>
        /// <param name="IncomeSourceID">The IncomeSourceID id.</param>
        /// <param name="userId">The user Id.</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public string DeleteIncomeSource(int IncomeSourceID, int userId)
        {
            return WSHelper.DeleteIncomeSource(IncomeSourceID, userId);
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
