
namespace TerraScan.Dal
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;
    using System.Data;

    /// <summary>
    /// F9105Copy Name Address Management Comp
    /// </summary>
    public static class F9105CopyNameAddressManagementComp
    {
        /// <summary>
        /// F9105_s the name of the execute copy.
        /// </summary>
        /// <param name="copyData">The copy data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F9105_ExecuteCopyName( string copyData, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CopyData", copyData);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f9105_pcexe_CopyName", ht);
        }
    }
}
