

namespace TerraScan.Dal

{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    public static class F1406CentralAssessedSearchComp
    {

        /// <summary>
        /// F1406_s the central assessed grid details.
        /// </summary>
        /// <param name="centralSearchXML">The central search XML.</param>
        /// <returns></returns>
        public static F1406CentralAssessedSearchData F1406_CentralAssessedGridDetails(string  centralSearchXML)
        {
            F1406CentralAssessedSearchData CentralOwner = new F1406CentralAssessedSearchData();
            Hashtable ht = new Hashtable();
            ht.Add("@CentralConditionXML", centralSearchXML);
            Utility.LoadDataSet(CentralOwner.f1406_pclst_CentrallyAssessedGridTable, "f1406_pclst_CentrallyAssessed", ht);
            return CentralOwner;
        }

        /// <summary>
        /// F1406_s the load propert class combo.
        /// </summary>
        /// <returns></returns>
        public static F1406CentralAssessedSearchData F1406_LoadPropertClassCombo()
        {
            F1406CentralAssessedSearchData CentralOwner = new F1406CentralAssessedSearchData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(CentralOwner.f1406_pcget_PropertyClass, "f1406_pcget_PropertyClass", ht);
            return CentralOwner;
        }
    }
}
