// -------------------------------------------------------------------------------------------------
// <copyright file="TerraScanToolStripMenuItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// Custom Control 
// </summary>
// -------------------------------------------------------------------------------------------------
namespace TerraScan.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Custom ToolStripMenuItem Control
    /// </summary>
    public partial class TerraScanToolStripMenuItem : ToolStripMenuItem
    {
        #region Private Variables

        /// <summary>
        /// variable to hold permission value
        /// </summary>
        private int permissionOpen;

        /// <summary>
        /// used to store formId
        /// </summary>
        private int formId;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public TerraScanToolStripMenuItem()
        {
            this.InitializeComponent();
        }
  
        #endregion

        #region Property

        /// <summary>
        /// Property To Set Open Permission
        /// </summary>
        public int PermissionOpen
        {
            get { return this.permissionOpen; }
            set { this.permissionOpen = value; }
        }

        /// <summary>
        /// Gets or sets the form id.
        /// </summary>
        /// <value>The form id.</value>
        public int FormId
        {
            set
            {
                this.formId = value;
            }

            get
            {
                return this.formId;
            }
        }

        #endregion
    }
}
