// -------------------------------------------------------------------------------------------------
// <copyright file="F1024WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/
namespace D11018
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
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F1024 WorkItem
    /// </summary>
    public class F1024WorkItem : WorkItem
    {

        /// <summary>
        /// Gets the district selection data.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="district">The district.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>typed dataset</returns>
        public F1512DistrictSelectionData F1204_GetDistrictSelectionData(int districtId, string district, string description, int rollYear)
        {
            return WSHelper.F1512_GetDistrictSelectionData(districtId, district, description, rollYear);
        }

        /// <summary>
        /// Save district details
        /// </summary>
        /// <param name="levyOption">Levy OptionID</param>
        /// <param name="districtId">District ID</param>
        /// <param name="amountValue">Amount</param>
        /// <param name="userId">UserID</param>
        /// <returns>F11018MiscReceipt dataset</returns>
        public F11018MiscReceiptData F1024_SaveDistrictDetails(int levyOption, int districtId, decimal amountValue, int userId, bool IsReplace, string SubFundXML)
        {
            return WSHelper.F1024_SaveDistrictDetails(levyOption, districtId, amountValue, userId, IsReplace, SubFundXML);
        }

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

        public DistrictSelectionData f1024_pclst_SubFunds(int districtId)
        {
            return WSHelper.GetDistrictData(districtId);
        }

        ///// <summary>
        ///// Save district details
        ///// </summary>
        ///// <param name="levyOption">Levy OptionID</param>
        ///// <param name="districtId">District ID</param>
        ///// <param name="amountValue">Amount</param>
        ///// <param name="userId">UserID</param>
        ///// <returns>F11018MiscReceipt dataset</returns>
        //public DistrictSelectionData GetDistrictDistributionData(int levyOption, int districtId, decimal amountValue, int userId,bool IsReplace,string SubFundXML)
        //{
        //    return WSHelper.GetDistrictDistributionData(levyOption, districtId, amountValue, userId,IsReplace,SubFundXML);
        //}
    }
}