//--------------------------------------------------------------------------------------------
// <copyright file="F1505WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Copy Account. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20131119         Manoj               Created
//*********************************************************************************/


namespace D1500
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
    /// F1505WorkItem Class file
    /// </summary>
    public class F1505WorkItem : WorkItem
    {
        #region ExeCopyoperation
        /// <summary>
        /// F1505_s the create copy district MGMT.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="district">The district.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="activeStatus">The is active.</param>
        /// <param name="districtFundItems">The district fund items.</param>
        /// <returns>Error Statement or PrimaryKey Id</returns>
        /// F1502ExecuteCopyDistrict(this.districtID,this.DistrictTextBox.Text,this.RollYearTextBox.Text,this.DescriptionTextBox.Text,this.ActiveComboBox.SelectedIndex,this.DistrictTypeCOmbo.SelectedIndex, this.exciseRateId, TerraScanCommon.UserId); 
        public string F1505ExecuteCopyDistrict(int districtId, string districtText,int rollyear,string description,bool isactive,int districtTypeId, int ExciseId, int  userId)
        {
            return WSHelper.F1505ExecuteCopyDistrict(districtId, districtText, rollyear, description, isactive, districtTypeId, ExciseId, userId);
        }

        #endregion ExeCopyoperation

        #region Get District Selection
        /// <summary>
        /// Gets the district selection.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>Returns GetDistrictSelection Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetDistrictSelection(int exciseRateId)
        {
            return WSHelper.F15010_GetDistrictSelection(exciseRateId);
        }
        #endregion

        #region Configdetails
        /// <summary>
        /// Gets the Year
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Year.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }
        #endregion Configdetails

        #region Get District Type
        /// <summary>
        /// F15002_s the type of the get district.
        /// </summary>
        /// <returns></returns>
        public F15002DistMgmtData F15002_GetDistrictType(int UserId)
        {
            return WSHelper.F15002_GetDistrictType(UserId);
        }
        #endregion

        #region basic

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

        #endregion basic
    }
}
