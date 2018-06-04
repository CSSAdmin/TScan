//--------------------------------------------------------------------------------------------
// <copyright file="TerraScanLinkLabel.cs" company="Congruent">
//	Copyright (c) Congruent Info-Tech.  All rights reserved.
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
    /// custom control for link label
    /// </summary>
    public class TerraScanLinkLabel : LinkLabel
    {
        #region Private Variables

        /// <summary>
        ///  used to store menu name
        /// </summary>
        private string menuName;

        /// <summary>
        /// used to store formId
        /// </summary>
        private int formId;

        /// <summary>
        /// used to store formId
        /// </summary>
        private string formDllName;

        /// <summary>
        /// Variable Holds the OpenPermission for the User
        /// </summary>
        private int permissionOpen;
      
        /// <summary>
        /// LinkLabelValidType
        /// </summary>
        private ControlValidationType linkLabelValidType;

        /// <summary>
        /// for storing Decimal value 
        /// </summary>
        private decimal decimalLinkLabelValue;

        /// <summary>
        /// srting to contain text custom format
        /// </summary>
        private string textCustomFormat = "#,##0.00";

        /// <summary>
        /// Tooltip displays text for the Link Label wherever needed
        /// </summary>
        private ToolTip linkLabelToolTip = new ToolTip();

        #endregion

        #region Constructor

        /// <summary>
        /// constructor for terrascan link label
        /// </summary>
        public TerraScanLinkLabel()
        {
            this.MouseEnter += new EventHandler(this.OnMouseEnter);             
        }

        #endregion

        #region Enum

        /// <summary>
        /// TextBox ControlvalidationType.
        /// </summary>
        public enum ControlValidationType
        {
            /// <summary>
            /// Text = 0.
            /// </summary>
            Text = 0,

            /// <summary>
            /// Decimal = 1.
            /// </summary>
            Decimal = 1
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the type of the validate.
        /// </summary>
        /// <value>The type of the validate.</value>
        public ControlValidationType ValidateType
        {
            set
            {
                this.linkLabelValidType = value;                
            }

            get
            {
                return this.linkLabelValidType;
            }
        }

        /// <summary>
        /// Gets or sets the text custom format.
        /// </summary>
        /// <value>The text custom format.</value>
        public string TextCustomFormat
        {
            get { return this.textCustomFormat; }
            set { this.textCustomFormat = value; }
        }

        /// <summary>
        /// Gets or sets the decimal text box value.
        /// </summary>
        /// <value>The decimal value.</value>        
        public decimal DecimalLinkLabelValue
        {
            get { return this.decimalLinkLabelValue; }            
        }

        /// <summary>
        /// Gets or sets the name of the menu.
        /// </summary>
        /// <value>The name of the menu.</value>
        public string MenuName
        {
            set
            {
                this.Text = this.menuName;
                this.menuName = value;
            }

            get
            {
                return this.menuName;
            }
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

        /// <summary>
        /// Gets or sets the name of the form DLL.
        /// </summary>
        /// <value>The name of the form DLL.</value>
        public string FormDllName
        {
            set
            {
                this.formDllName = value;
            }

            get
            {
                return this.formDllName;
            }
        }

        /// <summary>
        /// Property to Get/Set for PermissionOpen
        /// </summary>
        public int PermissionOpen
        {
            get { return this.permissionOpen; }
            set { this.permissionOpen = value; }
        }

        #endregion

        #region Text Property

        /// <summary>
        /// Gets or sets the current text in the <see cref="T:System.Windows.Forms.TextBox"></see>.
        /// </summary>
        /// <value></value>
        public override string Text
        {            
            get
            {
                return base.Text;
            }

            set
            {
                string tempValue;
                tempValue = value;
                if (this.ValidateType.Equals(ControlValidationType.Decimal))
                {
                    Decimal.TryParse(tempValue, System.Globalization.NumberStyles.Currency, null, out this.decimalLinkLabelValue);
                    tempValue = this.decimalLinkLabelValue.ToString(this.textCustomFormat);                   
                }

                base.Text = tempValue;
            }
        }

        #endregion  

        #region Events
        /// <summary>
        /// Text of a LinkLabel is displayed when the text width exceeds controls width
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OnMouseEnter(object sender, EventArgs e)
        {
            Graphics graphics = this.CreateGraphics();
            SizeF widthHeight = graphics.MeasureString(this.Text.Trim(), this.Font);
            if (this.AutoSize)
            {
                if (this.Parent.GetType() == typeof(System.Windows.Forms.Panel))
                {
                    Panel parentPanel = (Panel)this.Parent;
                    int parentx = parentPanel.Location.X;
                    int childx = this.Location.X;
                    int total = childx - parentx;
                    int width = this.Size.Width;
                    if (widthHeight.Width > parentPanel.Width - total)
                    {
                        this.linkLabelToolTip.RemoveAll();
                        string linktext = string.Empty;
                        linktext = this.Text.Trim();
                        if (linktext.Contains("&&"))
                        {
                            linktext = linktext.Replace("&&", "&");
                            //this.Text = linktext;   
                        }
                        //this.linkLabelToolTip.SetToolTip(this, this.Text.Trim());
                        this.linkLabelToolTip.SetToolTip(this, linktext.Trim());
                    }
                    else
                    {
                        this.linkLabelToolTip.RemoveAll();
                    }
                }
            }
            else
            {
                if (widthHeight.Width > this.Width)
                {
                    this.linkLabelToolTip.RemoveAll();
                    string linktext = string.Empty;
                    linktext = this.Text.Trim();
                    if (linktext.Contains("&&"))
                    {
                        linktext = linktext.Replace("&&", "&");
                        //this.Text = linktext;   
                    }
                   // this.linkLabelToolTip.SetToolTip(this, this.Text.Trim());
                    this.linkLabelToolTip.SetToolTip(this, linktext.Trim());
                }
                else
                {
                    this.linkLabelToolTip.RemoveAll();
                }
            }

            graphics.Dispose();
        }
        #endregion
    }
}
