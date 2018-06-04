
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
    /// F82002ContractorManagementComp
    /// </summary>
    public static class F82002ContractorManagementComp
    {
        /// <summary>
        /// F82002_s the list contractor management data.
        /// </summary>
        /// <param name="iContractorID">The i contractor ID.</param>
        /// <param name="ContractorXML">The contractor XML.</param>
        /// <returns>The contractor management dataset.</returns>
        public static F82002ContractorManagementData F82002_ListContractorManagementData(int? iContractorID, string ContractorXML)
        {
            F82002ContractorManagementData ContractorManagementData = new F82002ContractorManagementData();           
            Hashtable ht = new Hashtable();
            if (iContractorID != null || iContractorID > 0)
            {
                ht.Add("@iContractorID", iContractorID);
            }
            
            ht.Add("@ContractorXML", ContractorXML);
            Utility.LoadDataSet(ContractorManagementData.ListContractorManagement, "f82002_pclst_Contractor", ht);
            return ContractorManagementData;
        }

        #region Delete Contractor Management
        
        /// <summary>
        /// F82002_s the delete contractor management.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="userId">The user id.</param>
        public static void F82002_DeleteContractorManagement(int contractorId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ContractorID", contractorId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f82002_pcdel_Contractor", ht);
        }

        #endregion

        #region Insert
        
        /// <summary>
        /// F82002_s the insert building permit details.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="contractorItems">The contractor items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The saved record status.</returns>
        public static int F82002_InsertBuildingPermitDetails(int? contractorId, string contractorItems, int userId)
        {
            Hashtable ht = new Hashtable();
            if (contractorId != null || contractorId > 0)
            {
                ht.Add("@ContractorID", contractorId);
            }
            ht.Add("@ContractorItems", contractorItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f82002_pcins_Contractor", ht);
        }

        #endregion
    }
}
