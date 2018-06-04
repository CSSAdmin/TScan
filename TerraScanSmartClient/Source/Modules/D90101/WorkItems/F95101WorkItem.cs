// -------------------------------------------------------------------------------------------
// <copyright file="F95101WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F95101</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                  H.Vinayagamurthy       Created// 
// 
// -------------------------------------------------------------------------------------------

namespace D90101
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
    /// F95101WorkItem Class file
    /// </summary>
    public class F95101WorkItem : WorkItem
    {
        #region F95101 Audit Trail

        /// <summary>
        /// To List Audit Trail records
        /// </summary>
        /// <param name="form">Form No</param>
        /// <param name="keyId">Key ID</param>
        /// <returns>Typed DataSet Containing the Audit Trail Details</returns>
        public DataSet F95101_ListAuditTrail(int form, int keyId)
        {
            return WSHelper.F95101_ListAuditTrail(form, keyId);
        }

        #endregion F95101 Audit Trail
    }
}
