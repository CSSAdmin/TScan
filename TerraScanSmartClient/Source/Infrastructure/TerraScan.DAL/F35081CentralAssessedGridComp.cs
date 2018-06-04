
namespace TerraScan.Dal
{  
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    public static class F35081CentralAssessedGridComp
    {

        /// <summary>
        /// F35081_s the central assessed grid details.
        /// </summary>
        /// <param name="CentralId">The central id.</param>
        /// <returns></returns>
        public static F35081CentralAssessedGridData F35081_CentralAssessedGridDetails(int CentralId)
        {
            F35081CentralAssessedGridData CentralOwner = new F35081CentralAssessedGridData();
            Hashtable ht = new Hashtable();
            ht.Add("@CentralID", CentralId);
            Utility.LoadDataSet(CentralOwner.f35081_GetCentrallyAssessedItem, "f35081_pcget_CentrallyAssessedItem", ht);
            return CentralOwner;
        }

        /// <summary>
        /// F35081_s the central assessed rate details.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="personalProperty">The personal property.</param>
        /// <param name="realProperty">The real property.</param>
        /// <returns></returns>
        public static F35081CentralAssessedGridData F35081_CentralAssessedRateDetails(int subFundId, decimal personalProperty, decimal realProperty, string description, string centralXMLList)
        {
            F35081CentralAssessedGridData CentralOwner = new F35081CentralAssessedGridData();
            Hashtable ht = new Hashtable();
            ht.Add("@FundID", subFundId);
            ht.Add("@PersonalProperty", personalProperty);
            ht.Add("@RealProperty", realProperty);
            ht.Add("@Description", description);
            ht.Add("@CAItemXMLData", centralXMLList);
            Utility.LoadDataSet(CentralOwner.f35081_GetCentrallyAssessedItemRate, "f35081_GetCentrallyAssessedItemRate", ht);
            return CentralOwner;
        }


        /// <summary>
        /// F35081_s the insert owner assessed grid.
        /// </summary>
        /// <param name="centralXMLItems">The central XML items.</param>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static void F35081_InsertOwnerAssessedGrid(string centralXMLItems, int centralId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CentrallyAssessedItems", centralXMLItems);
            ht.Add("@CentralID", centralId);            
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f35081_pcins_CentrallyAssessedItem", ht);
        }


        /// <summary>
        /// F35080_s the delete owner details.
        /// </summary>
        /// <param name="removeXMLItems">The remove XML items.</param>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        public static void F35081_DeleteOwnerGridDetails(string removeXMLItems, int centralId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CentralID", centralId);
            ht.Add("@RemoveCentralItemIDs", removeXMLItems);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f35081_pcdel_CentrallyAssessedItem", ht);
        }
        

    }
}
