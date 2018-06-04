namespace D1000
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;
    using TerraScan.SmartParts;
    

    /// <summary>
    /// 
    /// </summary>
     public class F1500WorkItem :WorkItem
    {


        /// <summary>
        /// F1500_s the get sample form details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <returns>Returns the values based on FormId</returns>
         public F1500SampleForm F1500_GetSampleFormDetails(int FormID)
         {
             return WSHelper.F1500_GetSampleFormDetails(FormID);
         }


         /// <summary>
         /// Inserts the sample form details.
         /// </summary>
         /// <param name="FormID">The form ID.</param>
         /// <param name="SampleFormDetails">The sample form details.</param>
         /// <param name="UserID">The user ID.</param>
         /// <returns></returns>
         public int InsertSampleFormDetails(int FormID, string SampleFormDetails, int UserID)
         {
             return WSHelper.InsertSampleFormDetails(FormID, SampleFormDetails, UserID);
         }

         /// <summary>
         /// Gets the application id.
         /// </summary>
         /// <returns></returns>
         public F1500SampleForm GetApplicationId()
         {
             return WSHelper.GetApplicationId();
         }

         public F1500SampleForm GetMenuIdDetails()
         {
             return WSHelper.GetMenuIdDetails();
            
         }
    }
}
