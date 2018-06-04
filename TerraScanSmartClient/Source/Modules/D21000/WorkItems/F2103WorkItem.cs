//----------------------------------------------------------------------------------
// <copyright file="F2103WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm. 
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		            Description
// ----------		---------		        ----------------------------------------
// 25 OCT 2013		PurusHotham A           Created
//-----------------------------------------------------------------------------------
namespace D21000
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

    public class F2103WorkItem :WorkItem
    {
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

        /// <summary>
        /// F2103_s the get exemption selection.
        /// </summary>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <param name="description">The description.</param>
        /// <param name="percent">The percent.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public F2103ExemptionSelectionData f2103_GetExemptionSelection(string exemptionCode, string description, decimal? percent, decimal? maximum, int? rollYear)
        {
            return WSHelper.f2103_GetExemptionSelection(exemptionCode, description, percent, maximum, rollYear);
        }
    }
}
