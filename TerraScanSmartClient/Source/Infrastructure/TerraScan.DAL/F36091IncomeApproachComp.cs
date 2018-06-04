using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerraScan.DataLayer;
using System.Collections;
using TerraScan.BusinessEntities;

namespace TerraScan.Dal
{
    public class F36091IncomeApproachComp
    {
        #region Get Income Sources Details
        /// <summary>
        /// F36091_GetIncomeSources the Income Source Details.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the Income Source Details</returns>
        public static F36091IncomeApproachData F36091_GetIncomeSources(int valueSliceId)
        {
         
            F36091IncomeApproachData incomeSourcesDetails = new F36091IncomeApproachData();
            Hashtable ht = new Hashtable();
            //DataSet ds = new DataSet();
            ht.Add("@ValueSliceID", valueSliceId);
            // ht.Add("@UserID", userId);
            string[] tableNames = new string[] { incomeSourcesDetails.IncomeApproachDetails.TableName, incomeSourcesDetails.IncomeSources.TableName };
            Utility.LoadDataSet(incomeSourcesDetails, "f31091_pcget_IncomeApproach", ht, tableNames);
            return incomeSourcesDetails;
        }

        #endregion Income Sources

        #region F36091_DeleteIncomeApproach

        /// <summary>
        /// F36091_s the delete income approach.
        /// </summary>
        /// <param name="cropId">The income Ids.</param>
        /// <param name="userId">The user id.</param>
        public static void F36091_DeleteIncomeSource(string incomeIds, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@IncomeApproachItemIDs", incomeIds);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f31091_pcdel_IncomeApproachItem", ht);
        }

        #endregion F36091_DeleteIncomeApproach

        #region Get Income Approach Details
        /// <summary>
        /// F36091_GetIncomeApproachDetails the Income Approach Details.
        /// </summary>
        /// <param name="IncomeApproachDetails">IncomeApproachDetails</param>
        /// <returns>the Income Approach Details</returns>
        public static F36091IncomeApproachData F36091_GetIncomeApproachItemDetails(string IncomeApproachDetails)
        {

            F36091IncomeApproachData IncomeApproachItemDetails = new F36091IncomeApproachData();
            Hashtable ht = new Hashtable();
            ht.Add("@IncomeApproachDetails", IncomeApproachDetails);
            string[] tableNames = new string[] { IncomeApproachItemDetails.GetIncomeApproachValues.TableName };
            Utility.LoadDataSet(IncomeApproachItemDetails, "f31091_pcget_IncomeApproachValues", ht, tableNames);
            return IncomeApproachItemDetails;
        }

        #endregion Income Approach Details


        #region Get SourceCode Details
        /// <summary>
        /// F36091_ListSourceDetails _s the source Details.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the source Code Details</returns>
        public static F36091IncomeApproachData F36091_ListSourceDetails(int valueSliceId)
        {

            F36091IncomeApproachData sourceDetails = new F36091IncomeApproachData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            Utility.LoadDataSet(sourceDetails.IncomeSourceTypes, "f31091_pclst_IncomeSourceTypes", ht);
            return sourceDetails;
        }
        #endregion SourceCode Details

        #region Save Income Approach Details

        /// <summary>
        /// To save the Income Approach deatils
        /// </summary>
        /// <param name="valueSliceId">ValueSliceId</param>
        /// <param name="SourceGridDetails">SourceGridDetails</param>
        ///  <param name="IncomeApproachDetails">IncomeApproachDetails</param>
        ///  <param name="userId">UserID</param>
        /// <returns>integer value containing the save income approach Id</returns>
        public static void F36091_SaveIncomeSourceDetails(int valueSliceId, string SourceGridDetails, string IncomeApproachDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            ht.Add("@UserID", userId);
            ht.Add("@SourceGridDetails", SourceGridDetails);
            ht.Add("@IncomeApproachDetails", IncomeApproachDetails);
            DataProxy.ExecuteSP("f31091_pcins_IncomeApproach", ht);
        }
        #endregion F36091_SaveIncomeSource Details 

        #region Get SourceCode Details
        /// <summary>
        /// F36091_ListApproachValues the source Details.
        /// </summary>
        /// <param name="incomeSourceID">incomeSourceID</param>
        /// <param name="Units">Units</param>
        /// <param name="ContractPerUnit">ContractPerUnit</param>
        /// <returns>the Approach Details</returns>
        public static F36091IncomeApproachData F36091_ListApproachValues(int incomeSourceID, decimal Units, decimal ContractPerUnit, out decimal contract, out decimal marketperunit, out decimal market)
        {
            contract = 0;
            marketperunit = 0;
            market = 0;
            F36091IncomeApproachData approachDetails = new F36091IncomeApproachData();
            Hashtable ht = new Hashtable();
            ht.Add("@IncomeSourceID", incomeSourceID);
            ht.Add("@Units", Units);
            ht.Add("@ContractPerUnit", ContractPerUnit);
            ht.Add("@Contract", contract);
            ht.Add("@MarketPerUnit", marketperunit);
            ht.Add("@Market", market);
            IList returnList = Utility.LoadDataSet(approachDetails.IncomeApproachItemValues, "f31091_pcget_IncomeApproachItemValues", ht);
            object var= ((System.Data.SqlClient.SqlParameter[])(returnList))[5].Value.ToString();
            if(((System.Data.SqlClient.SqlParameter[])(returnList))[3].Value.ToString() != "")
            contract = Convert.ToDecimal(((System.Data.SqlClient.SqlParameter[])(returnList))[3].Value.ToString());
            if (((System.Data.SqlClient.SqlParameter[])(returnList))[5].Value.ToString() != "")
            market = Convert.ToDecimal(((System.Data.SqlClient.SqlParameter[])(returnList))[5].Value.ToString());
            if (((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString() != "")
            marketperunit = Convert.ToDecimal(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return approachDetails;
        }
        #endregion approach Details
    }
}
