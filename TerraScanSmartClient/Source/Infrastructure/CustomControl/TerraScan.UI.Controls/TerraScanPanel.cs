//--------------------------------------------------------------------------------------------
// <copyright file="TerraScanPanel.cs" company="Congruent">
//	Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// User Control for Panel
// Specifically added for override panel scroll
// </summary>
// -------------------------------------------------------------------------------------------------

namespace TerraScan.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Linq;
    using System.ComponentModel;

    /// <summary>
    /// Terrascan custom panel
    /// </summary>
    public partial class TerraScanPanel : Panel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerraScanPanel"/> class.
        /// </summary>
        public TerraScanPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerraScanPanel"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public TerraScanPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// Gets the current scroll.
        /// </summary>
        /// <value>The current scroll.</value>
        public int CurrentScroll
        {
            get
            {
                int scr = AutoScrollPosition.Y;
                if (scr >= 0)
                    scr = -VerticalScroll.Value;
                return scr;
            }
        }

        /// <summary>
        /// Calculates the scroll offset to the specified child control.
        /// </summary>
        /// <param name="activeControl">The child control to scroll into view.</param>
        /// <returns>
        /// The upper-left hand <see cref="T:System.Drawing.Point"/> of the display area relative to the client area required to scroll the control into view.
        /// </returns>
        protected override System.Drawing.Point ScrollToControl(Control activeControl)
        {
            if (activeControl is Infragistics.Win.UltraWinGrid.UltraGrid
                     ||activeControl is System.Windows.Forms.WebBrowser 
                     || activeControl is TerraScanDataGridView 
                     || activeControl is Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace
                     || activeControl is Infragistics.Win.UltraWinTree.UltraTree
                     || activeControl is Infragistics.Win.UltraWinTabControl.UltraTabControl
                     || activeControl is TerraScanLegalGridView
                     || activeControl is DataGridView)
            {
                // Suppress the scroll
                // return new System.Drawing.Point(0, CurrentScroll);
                return DisplayRectangle.Location;
            }
            else
            {
                // Condition added for Catalog form
                if (activeControl.Parent != null)
                {
                    if (activeControl.Parent.Parent != null)
                    {
                        if (activeControl.Parent.Parent is Infragistics.Win.UltraWinTabControl.UltraTabControl
                        || activeControl.Parent.Parent is Infragistics.Win.UltraWinTabControl.UltraTabPageControl)
                        {
                            return DisplayRectangle.Location;
                        }
                    }

                    if (activeControl.Parent is Infragistics.Win.UltraWinTabControl.UltraTabControl
                        || activeControl.Parent is Infragistics.Win.UltraWinTabControl.UltraTabPageControl)
                    {
                        return DisplayRectangle.Location;
                    }
                }

                return base.ScrollToControl(activeControl);
            }
        }
    }
}
