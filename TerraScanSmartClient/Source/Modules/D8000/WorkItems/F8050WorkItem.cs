// -------------------------------------------------------------------------------------------
// <copyright file="F8050WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update GDoc Comments</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
// -------------------------------------------------------------------------------------------

namespace D8000
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

    /// <summary>
    /// Class for F8050WorkItem
    /// </summary>
    public class F8050WorkItem : WorkItem
    {
        #region GDoc Comment

        #region Get GDoc Comment

        /// <summary>
        /// Gets the GDoc Comment.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Typed Dataset containing the GDoc comment</returns>
        public GDocCommentData GetGDocComment(int eventId)
        {
            return WSHelper.GetGDocComment(eventId);            
        }

        #endregion Get GDoc Comment

        #region Save GDoc Comment

        /// <summary>
        /// Saves the GDoc comment.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="comment">The comment.</param>
        public void SaveGDocComment(int eventId, string comment,int userId)
        {
            WSHelper.SaveGDocComment(eventId, comment, userId);
        }

        #endregion Save GDoc Comment      

        #endregion GDoc Comment        

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
