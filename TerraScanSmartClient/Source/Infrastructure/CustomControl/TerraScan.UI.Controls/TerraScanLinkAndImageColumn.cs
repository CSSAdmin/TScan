// -------------------------------------------------------------------------------------------
// <copyright file="TerraScanLinkAndImageColumn.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access TerraScanLinkAndImageColumn related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.UI.Controls
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;

    /// <summary>
    /// TerraScanLinkAndImageColumn
    /// </summary>
    public class TerraScanLinkAndImageColumn : DataGridViewLinkColumn
    {
        /// <summary>
        /// imageValue
        /// </summary>
        private Image imageValue;

        /// <summary>
        /// imageSize
        /// </summary>
        private Size imageSize;

        /// <summary>
        /// TerraScanLinkAndImageColumn
        /// </summary>
        public TerraScanLinkAndImageColumn()
        {
            this.CellTemplate = new TerraScanLinkAndImageCell();
        }

        /// <summary>
        /// Image
        /// </summary>
        public Image Image
        {
            get
            {
                return this.imageValue;
            }

            set
            {
                if (this.Image != value)
                {
                    this.imageValue = value;

                    this.imageSize = value.Size;

                    if (this.InheritedStyle != null)
                    {
                    }
                }
            }
        }        

        /// <summary>
        /// ImageSize
        /// </summary>
        internal Size ImageSize
        {
            get
            {
                return this.imageSize;
            }
        }

        /// <summary>
        /// Gets the terra scan link and image cell template.
        /// </summary>
        /// <value>The terra scan link and image cell template.</value>
        private TerraScanLinkAndImageCell TerraScanLinkAndImageCellTemplate
        {
            get
            {
                return this.CellTemplate as TerraScanLinkAndImageCell;
            }
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns>Object</returns>
        public override object Clone()
        {
            TerraScanLinkAndImageColumn c = base.Clone() as TerraScanLinkAndImageColumn;

            c.imageValue = this.imageValue;

            c.imageSize = this.imageSize;

            return c;
        }
    }
}
