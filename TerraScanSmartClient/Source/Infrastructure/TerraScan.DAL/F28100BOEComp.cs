// -------------------------------------------------------------------------------------------
// <copyright file="F28100BOEComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access attachment related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    public static class F28100BOEComp
    {
        #region Get BOE

        /// <summary>
        /// Get BOE Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>BOE Details</returns>
        public static F28100BOEData F28100_GetBOEDetails(int eventId)
        {
            F28100BOEData boeData = new F28100BOEData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            string[] tableName = new string[] { boeData.BOEValues.TableName, boeData.AssessedGridValues.TableName, boeData.TotalsValues.TableName, boeData.ClassTypes.TableName, boeData.ValidRecord.TableName };
            Utility.LoadDataSet(boeData, "f23100_pcget_BOEValues", ht, tableName);
            return boeData;
        }

        #endregion Get BOE

        #region Get Class

        /// <summary>
        /// Class Details
        /// </summary>
        /// <param name="stateId">State ID</param>
        /// <param name="eventId">Event ID</param>
        /// <returns>Class Details</returns>
        public static F28100BOEData F23100_GetClass()
        {
            F28100BOEData boeData = new F28100BOEData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(boeData.ClassTypes, "f20050_pclst_ParcelClassTypes", ht);
            return boeData;
        }

        #endregion Get Class

        #region Get Total Amount

        /// <summary>
        /// Get Total amounts
        /// </summary>
        /// <param name="boeId">boe ID</param>
        /// <param name="eventId">Event ID</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <returns>Total values</returns>
        public static F28100BOEData F28100_GetTotalAmount(int boeId, int eventId, string assessedValues)
        {
            F28100BOEData boeData = new F28100BOEData();
            Hashtable ht = new Hashtable();
            ht.Add("@BOEID", boeId);
            ht.Add("@EventID", eventId);
            ht.Add("@AssessedValues", assessedValues);
            Utility.LoadDataSet(boeData.TotalsValues, "f23100_pcget_Totals", ht);
            return boeData;
        }

        #endregion Get Total Amount

        #region Save BOE Details

        /// <summary>
        /// Save BOE Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="boeItems">BOE Items</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <param name="userId">User ID</param>
        /// <returns>Primary Key</returns>
        public static int F28100_SaveBOEDetails(int eventId, string boeItems, string assessedValues, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@BOEElements", boeItems);
            ht.Add("@AssessedValues", assessedValues);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f23100_pcins_BoE", ht);
        }

        #endregion Save BOE Details

        #region Delete Discretionary Details

        /// <summary>
        /// Delete BOE
        /// </summary>
        /// <param name="boeId">BOE ID</param>
        /// <param name="userId">The User ID</param>
        public static void F28100_DeleteBOEDetails(int? boeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@BOEID", boeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f29630_pcdel_BoE", ht);
        }

        #endregion Delete Discretionary Details

        #region Push Value
        /// <summary>
        /// F28100 the push board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28100_PushBOEDetails(int boeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@BOEID", boeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f23100_BoEPushValue", ht);
        }
        #endregion Push Value

        #region Local Values

        /// <summary>
        /// Get Local Values
        /// </summary>
        /// <param name="assessedValues">Assessed Value</param>
        /// <returns>Assesed Value</returns>
        public static F28100BOEData F28100_GetLocalValues(string assessedValues)
        {
            F28100BOEData boeData = new F28100BOEData();
            Hashtable ht = new Hashtable();
            ht.Add("@BOEValues", assessedValues);
            Utility.LoadDataSet(boeData.AssessedValues, "f23100_pcget_LocalGridValues", ht);
            return boeData;
        }

        #endregion Local Values

        #region County Values

        /// <summary>
        /// Get County Values
        /// </summary>
        /// <param name="isLocal">Is Local</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <returns>Assessed Value</returns>
        public static F28100BOEData F28100_GetCountyValues(bool isLocal, string assessedValues)
        {
            F28100BOEData boeData = new F28100BOEData();
            Hashtable ht = new Hashtable();
            ht.Add("@IsLocalChecked", isLocal); 
            ht.Add("@BOEValues", assessedValues);
            Utility.LoadDataSet(boeData.AssessedValues, "f23100_pcget_CountyGridValues", ht);
            return boeData;
        }


        #endregion County Values

        #region State Values

        /// <summary>
        /// Get State Values
        /// </summary>
        /// <param name="isLocal">Is Local</param>
        /// <param name="isCounty">Is Couny</param>
        /// <param name="assessedValues">Assessed Value</param>
        /// <returns>Assessed Value</returns>
        public static F28100BOEData F28100_GetStateValues(bool isLocal, bool isCounty, string assessedValues)
        {
            F28100BOEData boeData = new F28100BOEData();
            Hashtable ht = new Hashtable();
            ht.Add("@IsLocalChecked", isLocal);
            ht.Add("@IsCountyChecked", isCounty);
            ht.Add("@BOEValues", assessedValues);
            Utility.LoadDataSet(boeData.AssessedValues, "f23100_pcget_StateGridValues", ht);
            return boeData;
        }

        #endregion State Values
    }
}
