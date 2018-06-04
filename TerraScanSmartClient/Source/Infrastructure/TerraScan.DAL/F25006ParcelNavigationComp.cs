namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.DataLayer;
    using System.Data;
    using System.Collections;
    using TerraScan.BusinessEntities;


    public static class F25006ParcelNavigationComp
    {
        public static F25006ParcelNavigation GetParcelDetails(int keyID,bool IsNext)
        {
            F25006ParcelNavigation parcelDetails = new F25006ParcelNavigation();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", keyID);
            ht.Add("@IsNext", IsNext);
            Utility.LoadDataSet(parcelDetails.f25006_ParcelNavigation, "f25006_pcget_ParcelNavigation", ht);
            return parcelDetails;
        }
    }
}
