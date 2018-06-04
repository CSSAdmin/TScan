//--------------------------------------------------------------------------------------------
// <copyright file="F9040WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 May 2007      Suganth Mani       Created
//*********************************************************************************/
namespace D9030
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
    /// F9040WorkItem
    /// </summary>
    public class F9040WorkItem : WorkItem 
    {
        #region SnapShotUtility

        #region F9040_ListBatchButtonSnapShots

        /// <summary>
        /// To List the F1440 Batch Button SnapShots for Current form slice.
        /// </summary>
        /// <param name="formsSliceNo">The forms slice no.</param>
        /// <returns>Typed DataSet containg the list of F1440 Batch Button SnapShots for Current form slice</returns>
        public F9040SnapShotUtilityData F9040_ListBatchButtonSnapShots(int formsSliceNo)
        {
            return WSHelper.F9040_ListBatchButtonSnapShots(formsSliceNo);
        }

        #endregion F9040_ListBatchButtonSnapShots

        #region F9040_SaveBatchButtonSnapShots

        /// <summary>
        /// To save the F1440 Batch Button SnapShots for Current form slice.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetails">The snap shot details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns the snapshot id</returns>
        public int F9040_SaveBatchButtonSnapShots(int snapShotId, string snapShotDetails, int userId)
        {
            return WSHelper.F9040_SaveBatchButtonSnapShots(snapShotId, snapShotDetails, userId);
        }

        #endregion F9040_SaveBatchButtonSnapShots


        #region ListSnapShots

        /// <summary>
        /// Lists the SnapShots for the form.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>F9040SnapShotUtilityData Dataset</returns>
        public F9040SnapShotUtilityData F9040_ListSnapShots(int formId)
        {
            return WSHelper.F9040_ListSnapShots(formId);
        }

        #endregion ListSnapShot       

        #region SaveSnapShot

        /// <summary>
        /// F9040_s the save snap shot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotXml">The snap shot XML.</param>
        /// <param name="snapshotItemsXml">The snapshot items XML.</param>
        /// <param name="filterXML">The Filter XML</param>
        /// <param name="pinType">Pinning Type</param>
        /// <param name="userId">The user id.</param>
        /// <param name="parentSnapShotID">Parent SnapShotID</param>
        /// <returns>the saved snapshotid</returns>
        public int F9040_SaveSnapShot(int snapShotId, string snapShotXml, string snapshotItemsXml, string filterXML, string pinType, int userId, int parentSnapShotID)
        {
            return WSHelper.F9040_SaveSnapShot(snapShotId, snapShotXml, snapshotItemsXml, filterXML, pinType, userId, parentSnapShotID);
        }

        #endregion SaveSnapShot

        #region DeleteSnapShot

        /// <summary>
        /// To Delete F040 Snapshot
        /// </summary>
        /// <param name="snapshotId">The snapshotId</param>
        /// <param name="userId">The user id.</param>
        public void F9040_DeleteSnapShot(int snapshotId, int userId)
        {
            WSHelper.F9040_DeleteSnapShot(snapshotId, userId);
        }

        #endregion DeleteSnapShot

        #endregion SnapShotUtility
    }
}
