

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

   public static class F2201CentrallyAssessedSearchComp
    {

        /// <summary>
        /// F2201_s the get personal property description.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
       public static F2201CentrallyAssessedSearchData F2201_GetPersonalPropertyDescription(string code)
       {
           F2201CentrallyAssessedSearchData F2201_PropertyDescription = new F2201CentrallyAssessedSearchData();
           Hashtable ht = new Hashtable();
           ht.Add("@PersonalPropertyCode", code);
           Utility.LoadDataSet(F2201_PropertyDescription.f2200_PersonalPropertyCodeDescription, "f2200_pcget_PersonalPropertyCodeDescription", ht);
           return F2201_PropertyDescription;
       }

       /// <summary>
       /// F2201_s the get personal property search.
       /// </summary>
       /// <param name="code">The code.</param>
       /// <param name="description">The description.</param>
       /// <returns></returns>
       public static F2201CentrallyAssessedSearchData F2201_GetPersonalPropertySearch(string code,string description)
       {
           F2201CentrallyAssessedSearchData F2201_PropertyDescription = new F2201CentrallyAssessedSearchData();
           Hashtable ht = new Hashtable();
           ht.Add("@PersonalPropertyCode", code);
           ht.Add("@Description", description);
           Utility.LoadDataSet(F2201_PropertyDescription.f2201_PersonalPropertyCodeSelection, "f2201_pclst_PersonalPropertyCodeSelection", ht);
           return F2201_PropertyDescription;
       }
    }
}
