//--------------------------------------------------------------------------------------------
// <copyright file="F9510WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2550WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 Aug 06        D.LathaMaheswari   Created
//*********************************************************************************/
namespace D90010
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
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F9510WorkItem
    /// </summary>
    public class F9510WorkItem : WorkItem
    {
        #region F9510GetWebFormXML

        /// <summary>
        /// Gets the web form XML.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>DataSet</returns>
        public F95010GetWebFormXMLData GetWebFormXML(int form, int userId)
        {
            return WSHelper.F9510GetWebFormXML(form, userId);
        }

        #endregion F9510GetWebFormXML

        /// <summary>
        /// Called when [run started].
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
