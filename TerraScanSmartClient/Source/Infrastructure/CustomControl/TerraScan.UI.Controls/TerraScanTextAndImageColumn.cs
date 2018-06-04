// -------------------------------------------------------------------------------------------------
// <copyright file="TerraScanTextAndImageColumn.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// User Control 
// </summary>
// -------------------------------------------------------------------------------------------------

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
    /// TerraScanTextAndImageColumn
    /// </summary>
    public class TerraScanTextAndImageColumn : DataGridViewTextBoxColumn
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
        /// TerraScanTextAndImageColumn
        /// </summary>
        public TerraScanTextAndImageColumn()
        {
            this.CellTemplate = new TerraScanTextAndImageCell();
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
        /// TerraScanTextAndImageCellTemplate
        /// </summary>
        private TerraScanTextAndImageCell TerraScanTextAndImageCellTemplate
        {
            get 
            { 
                return this.CellTemplate as TerraScanTextAndImageCell; 
            }
        } 

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns>Object</returns>
        public override object Clone()
        {
            TerraScanTextAndImageColumn c = base.Clone() as TerraScanTextAndImageColumn;

            c.imageValue = this.imageValue;

            c.imageSize = this.imageSize;

            return c;
        }
    }
}
