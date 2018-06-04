//--------------------------------------------------------------------------------------------
// <copyright file="F9020WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the GetCountyConfiguration,UpdateCountyConfigDetails.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 Jul 06        VINOTHBABU         Created
//*********************************************************************************/

namespace D9020
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using System.Data;
    
    /// <summary>
    /// F9020WorkItem class
    /// </summary>
    public class F9020WorkItem : WorkItem
    {
        /// <summary>
        /// GetCountyConfiguration
        /// </summary>
        /// <param name="applicationId">int applicationId</param>
        /// <param name="userId">int userId</param>
        /// <returns>DataSet</returns>
        public static DataSet GetCountyConfiguration(int applicationId, int userId)
        {
            return WSHelper.GetCountyConfiguration(applicationId, userId);
        }

        /// <summary>
        /// UpdateCountyConfigDetails
        /// </summary>
        /// <param name="configType">int configType</param>
        /// <param name="configValue">string configValue</param>
        public static void UpdateCountyConfigDetails(int configType, string configValue,int userID)
        {
            WSHelper.UpdateCountyConfigDetails(configType, configValue,userID);
        }

        /// <summary>
        /// Override Method for OnRunStarted
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Override Method for OnActivated
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
