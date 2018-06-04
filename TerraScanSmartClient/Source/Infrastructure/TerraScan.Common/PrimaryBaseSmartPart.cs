using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TerraScan.Common
{
    /// <summary>
    /// wrapper class to solve the tab issue
    /// </summary>
    public partial class PrimaryBaseSmartPart : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:PrimaryBaseSmartPart"/> class.
        /// </summary>
        public PrimaryBaseSmartPart()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.TabStop = false;
            if (this.Parent != null)
            {
                this.Parent.TabStop = false;
            }
        }
    }
}
