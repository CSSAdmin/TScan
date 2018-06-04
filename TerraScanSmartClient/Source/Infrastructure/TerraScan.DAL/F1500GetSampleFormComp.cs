using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TerraScan.DataLayer;
using TerraScan.BusinessEntities;

namespace TerraScan.Dal
{
    /// <summary>
    /// Data Access Layer helps to communicate with business entity
    /// </summary>
   public static class F1500GetSampleFormComp
    {
        /// <summary>
        /// F1500_s the get sample form details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <returns> Returns the values from form and form config tables</returns>
        #region F1500 Getting Form Details
        public static F1500SampleForm F1500_GetSampleFormDetails(int FormID)
        {
            F1500SampleForm objSampleData = new F1500SampleForm();
            Hashtable ht = new Hashtable();
            ht.Add("@FormID", FormID);
            string[] tempTable =new string[] { objSampleData.FormSliceDetails.TableName, objSampleData.SampleFormApplicationIdTable.TableName, objSampleData.SampleFormMenuGroupTable.TableName };
            Utility.LoadDataSet(objSampleData, "aGET_SampleForm", ht, tempTable);
            return objSampleData;
        } 
        #endregion



        /// <summary>
        /// Inserts the sample form details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <param name="SampleFormDetails">The sample form details.</param>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        public static int InsertSampleFormDetails(int FormID, string SampleFormDetails, int UserID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FormID", FormID);
            ht.Add("@SampleFormDetails", SampleFormDetails);
            ht.Add("@UserID", UserID);

            return Utility.FetchSPExecuteKeyId("pcinc_SampleForm", ht);  

            }


        /// <summary>
        /// Gets the application id.
        /// </summary>
        /// <returns></returns>
        #region getting Application Id
        public static F1500SampleForm GetApplicationId()
        {
            F1500SampleForm objFormApplicationId = new F1500SampleForm();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(objFormApplicationId.SampleFormApplicationIdTable, "pclstSampleFormApplicationId", ht);
            return objFormApplicationId;
        }
        
        #endregion


        /// <summary>
        /// Gets the menu id details.
        /// </summary>
        /// <returns></returns>
        #region MenugroupIdDetails
        public static F1500SampleForm GetMenuIdDetails()
        {
            F1500SampleForm objFormMenuId = new F1500SampleForm();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(objFormMenuId.SampleFormMenuGroupTable, "pclstSampleFormMenuGroupId", ht);
            return objFormMenuId;
        } 
        #endregion


        #region Delete Sample Form Details
        /// <summary>
        /// F1500_s the delete fom ID details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <param name="GroupID">The group ID.</param>
        public static void F1500_DeleteFomIDDetails(int FormID, int GroupID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FormID", FormID);
            ht.Add("GroupID", GroupID);
            Utility.ImplementProcedure("pcdel_F1500SampleForm", ht);
        } 
        #endregion
 
    }
}
