// -------------------------------------------------------------------------------------------
// <copyright file="F29351AssociationLinkComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36041CropComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 24/4/08          Malliga             Created
// -------------------------------------------------------------------------------------------


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
    /// F29531AssociationLink Class File.
    /// </summary>
    public class F29531AssociationLinkComp
    {
        /// <summary>
        /// F29531s the type of the association link.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public static F29531AssciationLinkData F29531AssociationLinkType(int userid)
        {
            F29531AssciationLinkData associationlinktype = new F29531AssciationLinkData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userid);
            Utility.LoadDataSet(associationlinktype.AssociationLinkTypeDataTable, "f29531_pclst_AssociationType", ht);
            return associationlinktype;
        }
        
        /// <summary>
        /// F29531_s the fill association link grid.
        /// </summary>
        /// <param name="keyid">The keyid.</param>
        /// <param name="formId">The form id.</param>
        /// <returns></returns>
        public static F29531AssciationLinkData F29531_FillAssociationLinkGrid(int keyid,int formId)
        {
            F29531AssciationLinkData associationLink = new F29531AssciationLinkData();
            Hashtable ht = new Hashtable();
            ht.Add("@KeyID", keyid);
            ht.Add("@Form", formId);
            Utility.LoadDataSet(associationLink.AssociationDataTable, "f29531_pclst_Association", ht);
            return associationLink;
        }

        /// <summary>
        /// F29531_s the get link text.
        /// </summary>
        /// <param name="cfgid">The cfgid.</param>
        /// <param name="keyid">The keyid.</param>
        /// <returns></returns>
        public static string F29531_GetLinkText(int cfgid,int keyid)
        {
            //DataSet associationLink = new DataSet();
            //F29531AssciationLinkData associationLink = new F29531AssciationLinkData();
            Hashtable ht = new Hashtable();
            ht.Add("@CfgID", cfgid);
            ht.Add("@KeyID", keyid);
            return Utility.FetchSPExecuteKeyString("f29531_pcget_LinkText", ht);
        }


        /// <summary>
        /// F29531_s the save association link.
        /// </summary>
        /// <param name="associationid">The associationid.</param>
        /// <param name="associationLinkItems">The association link items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F29531_SaveAssociationLink(int associationid, string associationLinkItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AssociationID", associationid);
            ht.Add("@AssociationLinkItems", associationLinkItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f29531_pcins_AssociationLink", ht);
        }
        
        /// <summary>
        /// F29531_s the delete association link.
        /// </summary>
        /// <param name="associationId">The association id.</param>
        /// <param name="userId">The user id.</param>
        public static void F29531_DeleteAssociationLink(int associationId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AssociationID", associationId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f29531_pcdel_AssociationLink", ht);
        }

        /// <summary>
        /// Updates the association link details.
        /// </summary>
        /// <param name="associationDetails">The association details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static void UpdateAssociationLinkDetails( string associationDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AssociationXML", associationDetails);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f29531_pcupd_AssociationLink", ht);
        }
   }
}
