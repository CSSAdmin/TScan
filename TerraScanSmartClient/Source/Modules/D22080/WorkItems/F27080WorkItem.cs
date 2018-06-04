// -------------------------------------------------------------------------------------------------
// <copyright file="F27080WorkItem.cs" company="Congruent">
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
// 14 Sep 07        Sriparameswari           Created// 
//*********************************************************************************/


namespace D22080
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
    /// 
    /// </summary>
   public class F27080WorkItem : WorkItem
    {
        #region Work Item Methods

       /// <summary>
        /// F27080_ListExemptionTypeCombo
       /// </summary>
        /// <param name="applicationId">applicationId</param>
       /// <returns>Dataset</returns>
       public F27080ExemptionDefinitionData F27080_ListExemptionTypeCombo(int applicationId)
       {
           return WSHelper.F27080_ListExemptionTypeCombo(applicationId);
       }

       /// <summary>
       /// F27080_FillExemptionTypeGrid
       /// </summary>
       /// <param name="exemptionId">exemptionId</param>
       /// <returns>F27080ExemptionDefinitionData</returns>
       public F27080ExemptionDefinitionData F27080_FillExemptionTypeGrid(int exemptionId)
       {
           return WSHelper.F27080_FillExemptionTypeGrid(exemptionId);
       }

       /// <summary>
       /// F27080_GetExemptionError
       /// </summary>
       /// <param name="exemptionId">exemptionId</param>
       /// <param name="exemptionCode">exemptionCode</param>
       /// <returns>NULL</returns>
       public void F27080_DeleteExemption(int userId, int exemptionId, string exemptionCode)
       {
           WSHelper.F27080_DeleteExemption(userId, exemptionId, exemptionCode);
       }

       /// <summary>
       /// F27080_GetExemptionError
       /// </summary>
       /// <param name="exemptionId">exemptionId</param>
       /// <param name="exemptionCode">exemptionCode</param>
       /// <returns>NULL</returns>
       public string F27080_GetExemptionError(int exemptionId, string exemptionCode)
       {
           return WSHelper.F27080_GetExemptionError(exemptionId, exemptionCode);
       }

       /// <summary>
       /// F27080_SaveExemptionType
       /// </summary>
       /// <param name="exemptionID">exemptionID</param>
       /// <param name="seniorExemption">seniorExemption</param>
       /// <returns>int</returns>
       public int F27080_SaveExemptionType(int exemptionID, string seniorExemption, string exemptionType, int checkError, int userId)
       {
           return WSHelper.F27080_SaveExemptionType(exemptionID, seniorExemption, exemptionType, checkError, userId);
       }

       /// <summary>
       /// Gets the config Roll Year.
       /// </summary>
       /// <param name="configName">Name of the config.</param>
       /// <returns>GetConfigDetails</returns>
       public CommentsData GetConfigDetails(string configName)
       {
           return WSHelper.GetConfigDetails(configName);
       }


       public F2550TaxRollCorrectionData F2550_GetConfiguredState()
       {
           return WSHelper.F2550_GetConfiguredState();
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

        #endregion Work Item Methods
    }
}
