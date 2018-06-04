//----------------------------------------------------------------------------------
// <copyright file="F9076WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date	          Author		         Description
// ----------	  ---------		         -------------------------------------------
// 25 Nov 2008    A.Shanmuga SUndaram    Created
//*********************************************************************************/

namespace D9075
{
    #region NameSpaces

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

    #endregion NameSpaces

    /// <summary>
    /// F9076WorkItem
    /// </summary>
    public class F9076WorkItem : WorkItem
    {
        #region CRUD Methods.

        #region F9076 Save New Template

        /// <summary>
        /// F9076s the save new comment template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="commentItemsXml">The comment items XML.</param>
        /// <param name="isOverwrite">The is overwrite.</param>
        /// <returns>templateId</returns>
        public int F9076SaveNewCommentTemplate(int? templateId, string commentItemsXml, int isOverwrite)
        {
            return WSHelper.F9076SaveNewCommentTemplate(templateId, commentItemsXml, isOverwrite);
        }

        #endregion F9076 Save New Template

        #region F9076 Delete Template

        /// <summary>
        /// F9076_s the delete new comment template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        public void F9076_DeleteNewCommentTemplate(int templateId)
        {
            WSHelper.F9076_DeleteNewCommentTemplate(templateId);
        }

        #endregion F9076 Delete Template

        #endregion CRUD Methods.

        #region Base Methods
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

        #endregion Base Methods

    }
}
