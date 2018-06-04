// -------------------------------------------------------------------------------------------
// <copyright file="F8030WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update GDOC Events Header</summary>
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
    /// Class for F8030WorkItem
    /// </summary>
    public class F8030WorkItem : WorkItem
    {
        #region GDoc Event Header

        #region GetGDocEventHeader

        /// <summary>
        /// Gets the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Typed dataset containing the Event,Event date,Work Order and Is complete. </returns>
        public GDocEventHeaderData GetGDocEventHeader(int eventId)
        {
            return WSHelper.GetGDocEventHeader(eventId);
        }

        #endregion GetGDocEventHeader

        #region ListGDocEventHeaderStatus

        /// <summary>
        /// Lists the GDoc event header status.
        /// </summary>
        /// <param name="eventId">The event Id.</param>
        /// <returns>Typed status containing Event Engine status.</returns>
        public GDocEventHeaderData ListGDocEventHeaderStatus(int eventId)
        {
            return WSHelper.ListGDocEventHeaderStatus(eventId);            
        }

        #endregion  ListGDocEventHeaderStatus

        #region DeleteGDocEventHeader

        /// <summary>
        /// Deletes the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="childFlag">The child flag.</param>
        public void DeleteGDocEventHeader(int eventId, int childFlag,int userId)
        {
            WSHelper.DeleteGDocEventHeader(eventId, childFlag, userId);
        }

        #endregion DeleteGDocEventHeader

        #region SaveGDocEventHeader

        /// <summary>
        /// Saves the GDoc event header.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <returns>Typed dataset</returns>
        public GDocEventHeaderData SaveGDocEventHeader(string eventItems,int userId)
        {
            return WSHelper.SaveGDocEventHeader(eventItems, userId);
        }       

        #endregion SaveGDocEventHeader       

        #endregion GDoc Event Header

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
