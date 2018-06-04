// -------------------------------------------------------------------------------------------------
// <copyright file="TerraScanLinkAndImageCell.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// User Control 
// </summary>
// -------------------------------------------------------------------------------------------------

namespace TerraScan.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;

    /// <summary>
    /// TerraScanLinkAndImageCell
    /// </summary>
  public class TerraScanLinkAndImageCell : DataGridViewLinkCell 
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
        /// imagexLocation
      /// </summary>
        private int imagexLocation = 295;

      /// <summary>
        /// imageyLocation
      /// </summary>
        private int imageyLocation = 3;

      /// <summary>
        /// ImageyLocation
      /// </summary>
        public int ImageyLocation
        {
            get { return this.imageyLocation; }
            set { this.imageyLocation = value; }
        }

      /// <summary>
        /// ImagexLocation
      /// </summary>
        public int ImagexLocation
        {
            get { return this.imagexLocation; }
            set { this.imagexLocation = value; }
        }      

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public Image Image
        {
            get
            {
                if (this.OwningColumn == null ||
                this.OwningTextAndImageColumn == null)
                {
                    return this.imageValue;
                }
                else if (this.imageValue != null)
                {
                    return this.imageValue;
                }
                else
                {
                    return this.OwningTextAndImageColumn.Image;
                }
            }

            set
            {
                if (this.imageValue != value)
                {
                    this.imageValue = value;

                    this.imageSize = value.Size;

                    Padding inheritedPadding = this.InheritedStyle.Padding;

                    ////this.Style.Padding = new Padding(imageSize.Width,
                    ////inheritedPadding.Left, inheritedPadding.Right,
                    ////inheritedPadding.Bottom);

                    //////this.Style.Padding = new Padding(imageSize.Width,
                    ////inheritedPadding.Left, inheritedPadding.Right,
                    ////inheritedPadding.Bottom);
                }
            }
        }

        /// <summary>
        /// Gets the owning text and image column.
        /// </summary>
        /// <value>The owning text and image column.</value>
        private TerraScanLinkAndImageColumn OwningTextAndImageColumn
        {
            get
            {
                return this.OwningColumn as TerraScanLinkAndImageColumn;
            }
        }

        /// <summary>
        /// Creates an exact copy of this cell.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewLinkCell"></see>.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public override object Clone()
        {
            TerraScanLinkAndImageCell c = base.Clone() as TerraScanLinkAndImageCell;

            c.imageValue = this.imageValue;

            c.imageSize = this.imageSize;

            return c;
        }

        /// <summary>
        /// Paints the specified graphics.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="clipBounds">The clip bounds.</param>
        /// <param name="cellBounds">The cell bounds.</param>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="cellState">State of the cell.</param>
        /// <param name="value">The value.</param>
        /// <param name="formattedValue">The formatted value.</param>
        /// <param name="errorText">The error text.</param>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="advancedBorderStyle">The advanced border style.</param>
        /// <param name="paintParts">The paint parts.</param>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            ////Paint the base content

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            if (this.Image != null)
            {
                // Draw the image clipped to the cell.

                System.Drawing.Drawing2D.GraphicsContainer container =
                graphics.BeginContainer();

                graphics.SetClip(cellBounds);
                Point mypoint = new Point(cellBounds.Location.X + this.imagexLocation, cellBounds.Location.Y + this.imageyLocation);
                ////Point myPoint = new Point(cellBounds.Location.X, cellBounds.Location.Y);
                graphics.DrawImageUnscaled(this.Image, mypoint);
                graphics.EndContainer(container);
            }
        }       
    }
}
