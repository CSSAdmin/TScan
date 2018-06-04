namespace D24530
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;
    using System.Data; 

    public class F29531WorkItem : WorkItem
    {
        #region F29531 AssociationLink-LinkType

        /// <summary>
        /// F29531s the type of the association link.
        /// </summary>
        /// <returns></returns>
        public F29531AssciationLinkData F29531AssociationLinkType(int userid)
        {
            return WSHelper.F29531AssociationLinkType(userid);
        }

        /// <summary>
        /// F29531_s the fill association link grid.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns></returns>
        public F29531AssciationLinkData F29531_FillAssociationLinkGrid(int keyid,int formId)
        {
            return WSHelper.F29531_FillAssociationLinkGrid(keyid,formId);
        }

        /// <summary>
        /// F29531_s the save association link.
        /// </summary>
        /// <param name="associationid">The associationid.</param>
        /// <param name="associationLinkItems">The association link items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F29531_SaveAssociationLink(int associationid, string associationLinkItems, int userId)
        {
            return WSHelper.F29531_SaveAssociationLink(associationid, associationLinkItems, userId);
        }

        /// <summary>
        /// Updates the association link details.
        /// </summary>
        /// <param name="associationDetails">The association details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public void UpdateAssociationLinkDetails(string associationDetails, int userId)
        {
             WSHelper.UpdateAssociationLinkDetails(associationDetails, userId);
        }
        /// <summary>
        /// F29531_s the get link text.
        /// </summary>
        /// <param name="cfgid">The cfgid.</param>
        /// <param name="keyid">The keyid.</param>
        /// <returns></returns>
        public static string F29531_GetLinkText(int cfgid, int keyid)
        {
            return WSHelper.F29531_GetLinkText(cfgid, keyid);
        }

        /// <summary>
        /// F29531_s the delete association link.
        /// </summary>
        /// <param name="associationid">The associationid.</param>
        /// <param name="userId">The user id.</param>
        public void F29531_DeleteAssociationLink(int associationid, int userId)
        {
            WSHelper.F29531_DeleteAssociationLink(associationid, userId);
        }

        #endregion

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
