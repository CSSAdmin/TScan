//--------------------------------------------------------------------------------------------
// <copyright file="ExciseTaxReceipt.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.SmartParts;
    using TerraScan.Common;

    /// <summary>
    /// ExciseTaxReceipt class file
    /// </summary>
    [SmartPart]
    public partial class ExciseTaxReceipt : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExciseTaxReceipt"/> class.
        /// </summary>
        public ExciseTaxReceipt()
        {
            this.InitializeComponent();
            this.PaymentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PaymentPictureBox.Height, this.PaymentPictureBox.Width, "Payment", 174, 150, 94);
        }
    }
}
