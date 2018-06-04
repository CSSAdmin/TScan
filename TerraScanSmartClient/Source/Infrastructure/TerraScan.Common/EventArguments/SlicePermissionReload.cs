namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// form event argument for checking valid keyid and
    /// setting permission for slices
    /// </summary>
    public class SlicePermissionReload
    {
        /// <summary>
        /// master form no 
        /// </summary>
        public int MasterFormNo;

        /// <summary>
        /// the userid selected
        /// </summary>
        public int SelectUserId;

        /// <summary>
        /// Used to store the keyId
        /// </summary>
        public int KeyId;

        /// <summary>
        /// flag to identify validity key in slices
        /// </summary>
        public bool FlagInvalidSliceKey;

        /// <summary>
        /// flag to identify validity key in slices
        /// </summary>
        private bool deletePermission;

        /// <summary>
        /// flag to identify validity key in slices
        /// </summary>
        private bool editPermission;

        /// <summary>
        /// flag to identify validity key in slices
        /// </summary>
        private bool newPermission;

        /// <summary>
        /// flag to identify validity key in slices
        /// </summary>
        private bool openPermission;

        #region Property

        /// <summary>
        /// OpenPermission
        /// </summary>
        public bool OpenPermission
        {
            get { return this.openPermission; }
            set { this.openPermission = value; }
        }

        /// <summary>
        /// NewPermission
        /// </summary>
        public bool NewPermission
        {
            get { return this.newPermission; }
            set { this.newPermission = value; }
        }

        /// <summary>
        /// EditPermission
        /// </summary>
        public bool EditPermission
        {
            get { return this.editPermission; }
            set { this.editPermission = value; }
        }

        /// <summary>
        /// DeletePermission
        /// </summary>
        public bool DeletePermission
        {
            get { return this.deletePermission; }
            set { this.deletePermission = value; }
        }

        #endregion Property
    }
}
