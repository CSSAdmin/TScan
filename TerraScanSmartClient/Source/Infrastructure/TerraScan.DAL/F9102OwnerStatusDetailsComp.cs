// ------------------------------------------------------------------------------------------------------------
// <copyright file="F9102OwnerStatusDetailsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F9102OwnerStatusDetailsComp.cs methods</summary>
// Release history
//*************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ------------------------------------------------------------------------
// 
// 
// ------------------------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F9102OwnerStatusDetailsComp Class file
    /// </summary>
    public static class F9102OwnerStatusDetailsComp
    {
        /// <summary>
        /// F9102_GetOwnerStatusDetails
        /// </summary>
        /// <param name="typeId">typeID</param>
        /// <param name="keyId">keyID</param>
        /// <returns>Typed dataset</returns>
        public static F9102OwnerStatusData F9102_GetOwnerStatusDetails(int typeId, int keyId)
        {
            F9102OwnerStatusData form9102ownerStatusData = new F9102OwnerStatusData();
            Hashtable ht = new Hashtable();
            string[] optionalParameter = new string[] 
            { 
                form9102ownerStatusData.TitleTable.TableName,
                form9102ownerStatusData.OwnerStatusDetailsTable.TableName
            };

            ht.Add("@TypeID", typeId);
            ht.Add("@KeyID", keyId);
            Utility.LoadDataSet(form9102ownerStatusData, "f9102_pcget_OwnerStatus", ht, optionalParameter);
            return form9102ownerStatusData;
        }
    }
}
