// -------------------------------------------------------------------------------------------------
// <copyright file="TerraScanButton.cs" company="Congruent">
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
    using System.Text.RegularExpressions;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Diagnostics;

    /// <summary>
    /// Custom Button Control.
    /// </summary>
    public class TerraScanButton : System.Windows.Forms.Button
    {
        #region  variables

        /// <summary>
        /// boolean Value To set the Foucs When the Rectangle is Enabled
        /// </summary>        
        private bool boolFocusRectangleEnabled = true;

        /// <summary>
        /// Check whether the Button is Valid Type
        /// </summary>
        private int buttonValidType;

        /// <summary>
        /// Check the button Action Type
        /// </summary>
        private int buttonActionType;

        /// <summary>
        /// check whether Permission there or not
        /// </summary>
        private bool actualPermission;

        /// <summary>
        /// To Filter
        /// </summary>
        private bool boolFilter;

        /// <summary>
        /// Checking the Status
        /// </summary>
        private bool boolStatus;

        /// <summary>
        /// containing the Status text
        /// </summary>
        private string statusOnText;

        /// <summary>
        /// containing the Status text
        /// </summary>
        private string statusOffText;

        /// <summary>
        /// containing the Status On Color
        /// </summary>
        private Color statusOnColor = Color.FromArgb(71, 133, 85);        

        /// <summary>
        /// containing the Status Off Color
        /// </summary>
        private Color statusOffColor = Color.FromArgb(128, 0, 0);        
        
        /// <summary>
        /// containing the Status text
        /// </summary>
        private bool applyDisableBehaviour; 

        /// <summary>
        /// Checking the autoPrint Status - true auto print on else off
        /// </summary>
        private bool autoPrintStatus;

        /// <summary>
        /// Button Spacing
        /// </summary>
        private int buttonPadding = 5;

        /// <summary>
        /// Attachment Image Selected
        /// </summary>
        private bool imageSelected;

        /// <summary>
        /// Set that Comment has Priority
        /// </summary>
        private bool commentPriority;

        /// <summary>
        /// default Text
        /// </summary>
        private string defaultText;

        /// <summary>
        /// set Border color
        /// </summary>
        private System.Drawing.Color borderColor = System.Drawing.Color.Wheat;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerraScanButton"/> class.
        /// </summary>
        public TerraScanButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.defaultText = this.Text;
            this.TextChanged += new System.EventHandler(this.OnTextChanged);
        }

        #region Enum
        /// <summary>
        /// Different Button type.
        /// </summary>
        public enum ButtonType
        {
            /// <summary>
            /// Print = 0.
            /// </summary>
            Print = 0,

            /// <summary>
            /// CommandButton = 1.(defult blue button)
            /// </summary>            
            CommandButton = 1,

            /// <summary>
            /// Filter = 2.
            /// </summary>
            Filter = 2,

            /// <summary>
            /// Status = 3.
            /// </summary>
            Status = 3,

            /// <summary>
            /// Attachment = 3.
            /// </summary>
            Attachment = 4,

            /// <summary>
            /// None = 5
            /// </summary>
            None = 5,

            /// <summary>
            /// Auto Print = 6.
            /// </summary>
            AutoPrint = 6,

            /// <summary>
            /// CommandButton = 7.(purple button)
            /// </summary>            
            PurpleCommandButton = 7,

            /// <summary>
            /// For font size 14  bold arial
            /// </summary>
            CustomButton=8,
        }

        /// <summary>
        /// Different Button Action.
        /// </summary>
        public enum ActionType
        {
            /// <summary>
            /// Other = 0.
            /// </summary>
            Other = 0,

            /// <summary>
            /// New = 1.
            /// </summary>
            New = 1,

            /// <summary>
            /// Save = 2.
            /// </summary>            
            Save = 2,

            /// <summary>
            /// Edit = 3.
            /// </summary>
            Edit = 3,

            /// <summary>
            /// Delete = 4.
            /// </summary>
            Delete = 4,

            /// <summary>
            /// Cancel = 5.
            /// </summary>
            Cancel = 5,

            /// <summary>
            /// Open = 6.
            /// </summary>
            Open = 6
        }
        #endregion

        #region ButtonType Property

        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        /// <value>The status text.</value>
        public string StatusOnText
        {
            get { return this.statusOnText; }
            set { this.statusOnText = value; }
        }

        /// <summary>
        /// Gets or sets the status off text.
        /// </summary>
        /// <value>The status off text.</value>
        public string StatusOffText
        {
            get { return this.statusOffText; }
            set { this.statusOffText = value; }
        }

        /// <summary>
        /// Gets or sets the color of the status on.
        /// </summary>
        /// <value>The color of the status on.</value>
        public Color StatusOnColor
        {
            get { return this.statusOnColor; }
            set { this.statusOnColor = value; }
        }

        /// <summary>
        /// Gets or sets the color of the status off.
        /// </summary>
        /// <value>The color of the status off.</value>
        public Color StatusOffColor
        {
            get 
            {
                return this.statusOffColor; 
            }

            set 
            {
                this.statusOffColor = value; 
            }
        }

        /// <summary>
        /// Gets or sets the ApplyDisableBehaviour(for button type none).
        /// </summary>
        /// <value>The status text.</value>
        public bool ApplyDisableBehaviour
        {
            get { return this.applyDisableBehaviour; }
            set { this.applyDisableBehaviour = value; }
        }

        /// <summary>
        /// Gets or sets the type of the set button.
        /// </summary>
        /// <value>The type of the set button.</value>
        public ButtonType SetButtonType
        {
            set
            {
                this.buttonValidType = (int)value;
                switch (this.buttonValidType)
                {
                    case (int)ButtonType.Print:
                        {
                            // print
                            this.BackColor = System.Drawing.Color.FromArgb(174, 150, 94);
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            break;
                        }

                    case (int)ButtonType.CommandButton:
                        {
                            // Command Button

                            this.BackColor = System.Drawing.Color.FromArgb(28, 81, 128);
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;

                            ////Command Button

                            break;
                        }

                    case (int)ButtonType.PurpleCommandButton:
                        {
                            ////Purple Command Button

                            this.BackColor = System.Drawing.Color.FromArgb(96, 51, 102);
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;

                            ////Purple Command Button

                            break;
                        }

                    case (int)ButtonType.Filter:
                        {
                            //// Not Filter 150, 150, 150
                            this.BackColor = System.Drawing.Color.FromArgb(150, 150, 150);
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            this.Text = "Not Filtered";
                            break;
                        }

                    case (int)ButtonType.Status:
                        {
                            ////filter: 71, 133, 85 
                            
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            if (this.boolStatus)
                            {
                                this.BackColor = this.statusOnColor;
                                this.Text = this.statusOnText;
                            }
                            else
                            {
                                this.BackColor = this.statusOffColor;
                                this.Text = this.statusOffText;
                            }

                            break;
                        }

                    case (int)ButtonType.AutoPrint:
                        {
                            // AutoPrint OFF 128, 0, 0
                            this.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            this.Text = Strings.AutoPrintOff;
                            break;
                        }

                    case (int)ButtonType.Attachment:
                        {
                            // For Attachment Browse and Scan

                            this.BackColor = System.Drawing.Color.FromArgb(28, 81, 128);
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            break;
                        }

                    case (int)ButtonType.None:
                        {
                            // For Attachment Browse and Scan

                            this.BackColor = this.BackColor;
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = this.ForeColor;
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            break;
                        }

                    case (int)ButtonType.CustomButton:
                        {
                            // For 9040 button alone

                            this.BackColor = this.BackColor;
                            this.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = this.ForeColor;
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            break;
                        }
                }
            }

            get
            {
                return (ButtonType)this.buttonValidType;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [filter status].
        /// </summary>
        /// <value><c>true</c> if [filter status]; otherwise, <c>false</c>.</value>
        public bool FilterStatus
        {
            set
            {
                this.boolFilter = value;
                if (this.SetButtonType.Equals(ButtonType.Filter) && this.boolFilter == true)
                {
                    // Filter
                    this.BackColor = System.Drawing.Color.FromArgb(199, 125, 44);
                    this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                    this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                    this.FlatAppearance.BorderSize = 1;
                    this.Text = "Filtered";
                }
                else if (this.SetButtonType.Equals(ButtonType.Filter) && this.boolFilter == false)
                {
                    // Not  Filter
                    this.BackColor = System.Drawing.Color.FromArgb(150, 150, 150);
                    this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                    this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                    this.FlatAppearance.BorderSize = 1;
                    this.Text = "Not Filtered";
                }
            }

            get
            {
                return this.boolFilter;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [status indicator].
        /// </summary>
        /// <value><c>true</c> if [status indicator]; otherwise, <c>false</c>.</value>
        public bool StatusIndicator
        {
            set
            {
                this.boolStatus = value;
                if (this.buttonValidType == 3 && this.boolStatus == true)
                {
                    // status on
                    this.BackColor = this.statusOnColor;
                    this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                    this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                    this.FlatAppearance.BorderSize = 1;                    
                    this.Text = this.statusOnText;                    
                }
                else if (this.buttonValidType == 3 && this.boolStatus == false)
                {
                    // status off
                    this.BackColor = this.statusOffColor;
                    this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                    this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                    this.FlatAppearance.BorderSize = 1;
                    this.Text = this.statusOffText;
                }
            }

            get
            {
                return this.boolStatus;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable auto print].
        /// </summary>
        /// <value><c>true</c> if [enable auto print]; otherwise, <c>false</c>.</value>
        public bool EnableAutoPrint
        {
            set
            {
                this.autoPrintStatus = value;
                if (this.SetButtonType.Equals(ButtonType.AutoPrint))
                {
                    if (this.autoPrintStatus)
                    {
                        // autoPrint ON
                        this.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                        this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                        this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                        this.FlatAppearance.BorderSize = 1;
                        this.Text = Strings.AutoPrintOn;
                    }
                    else
                    {
                        // autoPrint OFF
                        this.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
                        this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                        this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                        this.FlatAppearance.BorderSize = 1;
                        this.Text = Strings.AutoPrintOff;
                    }
                }
            }

            get
            {
                return this.autoPrintStatus;
            }
        }

        /// <summary>
        /// Gets or sets padding within the control.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Windows.Forms.Padding"></see> representing the control's internal spacing characteristics.</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public int NewPadding
        {
            get { return this.buttonPadding; }
            set { this.buttonPadding = value; }
        }

        /// <summary>
        /// Enable Rectangle
        /// </summary>
        public bool FocusRectangleEnabled
        {
            get { return this.boolFocusRectangleEnabled; }
            set { this.boolFocusRectangleEnabled = value; }
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public System.Drawing.Color BorderColor
        {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }

        /// <summary>
        /// Set that CommentPriority
        /// </summary>
        /// <value><c>true</c> if [comment priority]; otherwise, <c>false</c>.</value>
        public bool CommentPriority
        {
            get 
            { 
                return this.commentPriority; 
            }

            set 
            { 
                this.commentPriority = value; 
            }
        }

        #endregion

        #region ActionType
        /// <summary>
        /// Gets or sets the Action of the set button.
        /// </summary>
        /// <value>The Action of the set button.</value>
        public ActionType SetActionType
        {
            set
            {
                this.buttonActionType = (int)value;
            }

            get
            {
                return (ActionType)this.buttonActionType;
            }
        }

        #endregion

        #region ActualPermission
        /// <summary>
        /// Gets or sets the Actual Permission of the button.
        /// </summary>
        /// <value>The Permission of the set button.</value>
        public bool ActualPermission
        {
            set
            {
                this.actualPermission = (bool)value;
            }

            get
            {
                return (bool)this.actualPermission;
            }
        }
        #endregion      

        /// <summary>
        /// Gets or sets a value indicating whether [image selected].
        /// </summary>
        /// <value><c>true</c> if [image selected]; otherwise, <c>false</c>.</value>
        public bool ImageSelected
        {
            set
            {
                this.imageSelected = value;
                if (this.buttonValidType.Equals((int)ButtonType.Attachment))
                {
                    if (this.imageSelected)
                    {
                        this.BackColor = System.Drawing.Color.Green;
                        this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                        this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                        this.FlatAppearance.BorderSize = 1;
                    }
                    else
                    {
                        this.BackColor = System.Drawing.Color.FromArgb(32, 94, 149);
                        this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                        this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                        this.FlatAppearance.BorderSize = 1;
                    }
                }
            }

            get
            {
                return this.imageSelected;
            }
        }      

        /// <summary>
        /// Returns a Color Based on calculation
        /// </summary>
        /// <param name="scolor">scolor</param>
        /// <param name="dcolor">dcolor</param>
        /// <param name="percentage">percentage</param>
        /// <returns>color</returns>
        protected static Color Blend(Color scolor, Color dcolor, int percentage)
        {
            int r = scolor.R + ((dcolor.R - scolor.R) * percentage) / 100;
            int g = scolor.G + ((dcolor.G - scolor.G) * percentage) / 100;
            int b = scolor.B + ((dcolor.B - scolor.B) * percentage) / 100;
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Override OnPaint Method
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush textBrush;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            System.Drawing.Rectangle rect = this.GetTextDestinationRect();
            System.Drawing.SizeF size = this.Txt_Size(e.Graphics, this.Text, this.Font);
            System.Drawing.Point pt = this.Calculate_LeftEdgeTopEdge(this.TextAlign, rect, (int)size.Width, (int)size.Height);
            if (string.IsNullOrEmpty(this.defaultText))
            {
                this.defaultText = this.Text;
            }

            if (!this.Enabled)
            {
                switch (this.buttonValidType)
                {
                    case (int)ButtonType.Print:
                        {
                            // print

                            if (this.Text.IndexOf("&") >= 0)
                            {
                                this.Text = this.Text.Remove(this.Text.IndexOf("&"), 1);
                            }

                            textBrush = new SolidBrush(Color.FromArgb(128, 128, 128));
                            SolidBrush backBrush = new SolidBrush(Color.FromArgb(174, 150, 94));
                            e.Graphics.FillRectangle(backBrush, 0.0F, 0.0F, this.Width, this.Height);
                            e.Graphics.DrawString(this.Text, this.Font, textBrush, pt.X + 1, pt.Y - 1);

                            break;
                        }

                    case (int)ButtonType.CommandButton:
                        {
                            if (this.Text.IndexOf("&") >= 0)
                            {
                                this.Text = this.Text.Remove(this.Text.IndexOf("&"), 1);
                            }

                            textBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
                            SolidBrush backBrush = new SolidBrush(Color.FromArgb(28, 81, 128));
                            e.Graphics.FillRectangle(backBrush, 0.0F, 0.0F, this.Width, this.Height);
                            e.Graphics.DrawString(this.Text, this.Font, textBrush, pt.X + 1, pt.Y - 1);
                            break;
                        }

                    case (int)ButtonType.PurpleCommandButton:
                        {
                            if (this.Text.IndexOf("&") >= 0)
                            {
                                this.Text = this.Text.Remove(this.Text.IndexOf("&"), 1);
                            }

                            textBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
                            SolidBrush backBrush = new SolidBrush(Color.FromArgb(96, 51, 102));
                            e.Graphics.FillRectangle(backBrush, 0.0F, 0.0F, this.Width, this.Height);
                            e.Graphics.DrawString(this.Text, this.Font, textBrush, pt.X + 1, pt.Y - 1);
                            break;
                        }

                    case (int)ButtonType.Attachment:
                        {
                            if (this.Text.IndexOf("&") >= 0)
                            {
                                this.Text = this.Text.Remove(this.Text.IndexOf("&"), 1);
                            }

                            textBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
                            SolidBrush backBrush = new SolidBrush(Color.FromArgb(28, 81, 128));
                            e.Graphics.FillRectangle(backBrush, 0.0F, 0.0F, this.Width, this.Height);
                            e.Graphics.DrawString(this.Text, this.Font, textBrush, pt.X + 1, pt.Y - 1);
                            break;
                        }

                    case (int)ButtonType.AutoPrint:
                        {
                            if (this.Text.IndexOf("&") >= 0)
                            {
                                this.Text = this.Text.Remove(this.Text.IndexOf("&"), 1);
                            }

                            textBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
                            SolidBrush backBrush = new SolidBrush(this.BackColor);
                            e.Graphics.FillRectangle(backBrush, 0.0F, 0.0F, this.Width, this.Height);
                            e.Graphics.DrawString(this.Text, this.Font, textBrush, pt.X + 1, pt.Y - 1);
                            break;
                        }

                    case (int)ButtonType.None:
                        {
                            if (this.Text.IndexOf("&") >= 0)
                            {
                                this.Text = this.Text.Remove(this.Text.IndexOf("&"), 1);
                            }

                            SolidBrush backBrush;
                            if (this.applyDisableBehaviour)
                            {
                                textBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
                                backBrush = new SolidBrush(this.BackColor);
                                e.Graphics.FillRectangle(backBrush, 0.0F, 0.0F, this.Width, this.Height);
                            }
                            else
                            {
                                textBrush = new SolidBrush(this.ForeColor);
                                backBrush = new SolidBrush(Color.Black);
                                e.Graphics.FillRectangle(backBrush, 0.0F, 0.0F, this.Width, this.Height);
                                backBrush = new SolidBrush(this.BackColor);
                                e.Graphics.FillRectangle(backBrush, 1.0F, 1.0F, this.Width - 2, this.Height - 2);
                            }
                            
                            e.Graphics.DrawString(this.Text, this.Font, textBrush, pt.X + 1, pt.Y - 1);
                            break;
                        }

                    default:
                        {
                            if (this.Text.IndexOf("&") >= 0)
                            {
                                this.Text = this.Text.Remove(this.Text.IndexOf("&"), 1);
                            }

                            textBrush = new SolidBrush(Color.White);
                            SolidBrush backBrush = new SolidBrush(Color.Black);
                            e.Graphics.FillRectangle(backBrush, 0.0F, 0.0F, this.Width, this.Height);
                            backBrush = new SolidBrush(this.BackColor);
                            e.Graphics.FillRectangle(backBrush, 1.0F, 1.0F, this.Width - 2, this.Height - 2);
                            e.Graphics.DrawString(this.Text, this.Font, textBrush, pt.X + 1, pt.Y - 1);
                            break;
                        }

                    case (int)ButtonType.CustomButton:
                        {
                            if (this.Text.IndexOf("&") >= 0)
                            {
                                this.Text = this.Text.Remove(this.Text.IndexOf("&"), 1);
                            }

                            textBrush = new SolidBrush(Color.FromArgb(150, 150, 150));
                            SolidBrush backBrush = new SolidBrush(Color.FromArgb(28, 81, 128));
                            e.Graphics.FillRectangle(backBrush, 0.0F, 0.0F, this.Width, this.Height);
                            e.Graphics.DrawString(this.Text, this.Font, textBrush, pt.X + 1, pt.Y - 1);
                            break;
                        }
                }
            }
            else
            {
                this.Text = this.defaultText;
                base.OnPaint(e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            //// Checks if Comment Priority not set then Do
            if (!this.commentPriority)
            {
                switch (this.buttonValidType)
                {
                    case (int)ButtonType.Print:
                        {
                            // Print 
                            this.BackColor = System.Drawing.Color.FromArgb(200, 183, 145);
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            base.OnMouseDown(e);
                            break;
                        }

                    case (int)ButtonType.CommandButton:
                        {
                            //// Command

                            this.BackColor = System.Drawing.Color.FromArgb(41, 118, 188);

                            //// this.BackColor = System.Drawing.Color.Gold;

                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            base.OnMouseDown(e);
                            break;
                        }

                    case (int)ButtonType.CustomButton:
                        {
                            //// Command

                            this.BackColor = System.Drawing.Color.FromArgb(41, 118, 188);

                            //// this.BackColor = System.Drawing.Color.Gold;

                            this.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            base.OnMouseDown(e);
                            break;
                        }

                    default:
                        {
                            base.OnMouseDown(e);
                            break;
                        }
                }
            }
            else
            {
                base.OnMouseDown(e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseHover"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnMouseHover(EventArgs e)
        {
            //// Checks if Comment Priority not set then Do
            if (!this.commentPriority)
            {
                switch (this.buttonValidType)
                {
                    case (int)ButtonType.Print:
                        {
                            // print
                            this.BackColor = System.Drawing.Color.FromArgb(183, 162, 113);
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            base.OnMouseHover(e);
                            break;
                        }

                    case (int)ButtonType.CommandButton:
                        {
                            this.BackColor = System.Drawing.Color.FromArgb(32, 94, 149);
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            base.OnMouseHover(e);
                            break;
                        }

                    case (int)ButtonType.CustomButton:
                        {
                            this.BackColor = System.Drawing.Color.FromArgb(32, 94, 149);
                            this.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            base.OnMouseHover(e);
                            break;
                        }
                    default:
                        {
                            base.OnMouseHover(e);
                            break;
                        }
                }
            }
            else
            {
                base.OnMouseHover(e);
            }
        }

        /// <summary>
        /// Raises the mouse leave event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            //// Checks if Comment Priority not set then Do
            if (!this.commentPriority)
            {
                switch (this.buttonValidType)
                {
                    case (int)ButtonType.Print:
                        {
                            // print
                            this.BackColor = System.Drawing.Color.FromArgb(174, 150, 94);
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            base.OnMouseLeave(e);
                            break;
                        }

                    case (int)ButtonType.CommandButton:
                        {
                            // Command
                            this.BackColor = System.Drawing.Color.FromArgb(28, 81, 128);
                            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            base.OnMouseLeave(e);
                            break;
                        }
                    case (int)ButtonType.CustomButton:
                        {
                            // Command
                            this.BackColor = System.Drawing.Color.FromArgb(28, 81, 128);
                            this.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                            this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                            this.FlatAppearance.BorderSize = 1;
                            base.OnMouseLeave(e);
                            break;
                        }

                    case (int)ButtonType.Status:
                        {
                            if (this.StatusIndicator)
                            {
                                this.BackColor = this.StatusOnColor;
                                this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                                this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                                this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                                this.FlatAppearance.BorderSize = 1;
                                base.OnMouseLeave(e);
                            }
                            else
                            {
                                this.BackColor = this.StatusOffColor;
                                this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                                this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                                this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                                this.FlatAppearance.BorderSize = 1;
                                base.OnMouseLeave(e);
                            }

                            break;
                        }

                    case (int)ButtonType.Filter:
                        {
                            if (this.boolFilter)
                            {
                                this.BackColor = System.Drawing.Color.FromArgb(199, 125, 44);
                                this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                                this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                                this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                                this.FlatAppearance.BorderSize = 1;
                                base.OnMouseLeave(e);
                            }
                            else
                            {
                                this.BackColor = System.Drawing.Color.FromArgb(150, 150, 150);
                                this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
                                this.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                                this.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                                this.FlatAppearance.BorderSize = 1;
                                base.OnMouseLeave(e);
                            }

                            break;
                        }

                    default:
                        {
                            base.OnMouseLeave(e);
                            break;
                        }
                }
            }
            else
            {
                base.OnMouseLeave(e);
            }
        }

        /// <summary>
        /// Called when [text changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OnTextChanged(object sender, EventArgs e)
        {
            this.defaultText = this.Text;
        }

        /// <summary>
        /// Gets the image destination rect.
        /// </summary>
        /// <returns>Rectangle</returns>
        private System.Drawing.Rectangle GetImageDestinationRect()
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 0, 0);
            return rect;
        }

        /// <summary>
        /// TXT_s the size.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="strText">The STR text.</param>
        /// <param name="font">The font.</param>
        /// <returns>Size</returns>
        private System.Drawing.SizeF Txt_Size(Graphics g, string strText, Font font)
        {
            System.Drawing.SizeF size = g.MeasureString(strText, font);
            return size;
        }

        /// <summary>
        /// Gets the text destination rect.
        /// </summary>
        /// <returns>Rectangle</returns>
        private System.Drawing.Rectangle GetTextDestinationRect()
        {
            System.Drawing.Rectangle imageRect = this.GetImageDestinationRect();
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 0, 0);
            switch (this.ImageAlign)
            {
                case System.Drawing.ContentAlignment.BottomCenter:
                    rect = new System.Drawing.Rectangle(0, 0, this.Width, imageRect.Top);
                    break;
                case System.Drawing.ContentAlignment.BottomLeft:
                    rect = new System.Drawing.Rectangle(0, 0, this.Width, imageRect.Top);
                    break;
                case System.Drawing.ContentAlignment.BottomRight:
                    rect = new System.Drawing.Rectangle(0, 0, this.Width, imageRect.Top);
                    break;
                case System.Drawing.ContentAlignment.MiddleCenter:
                    rect = new System.Drawing.Rectangle(0, imageRect.Bottom, this.Width, this.Height - imageRect.Bottom);
                    break;
                case System.Drawing.ContentAlignment.MiddleLeft:
                    rect = new System.Drawing.Rectangle(imageRect.Right, 0, this.Width - imageRect.Right, this.Height);
                    break;
                case System.Drawing.ContentAlignment.MiddleRight:
                    rect = new System.Drawing.Rectangle(0, 0, imageRect.Left, this.Height);
                    break;
                case System.Drawing.ContentAlignment.TopCenter:
                    rect = new System.Drawing.Rectangle(0, imageRect.Bottom, this.Width, this.Height - imageRect.Bottom);
                    break;
                case System.Drawing.ContentAlignment.TopLeft:
                    rect = new System.Drawing.Rectangle(0, imageRect.Bottom, this.Width, this.Height - imageRect.Bottom);
                    break;
                case System.Drawing.ContentAlignment.TopRight:
                    rect = new System.Drawing.Rectangle(0, imageRect.Bottom, this.Width, this.Height - imageRect.Bottom);
                    break;
            }

            rect.Inflate(-this.NewPadding, -this.NewPadding);
            return rect;
        }

        /// <summary>
        /// Calculate_s the left edge top edge.
        /// </summary>
        /// <param name="alignment">The alignment.</param>
        /// <param name="rect">rect</param>
        /// <param name="width">width</param>
        /// <param name="height">Height</param>
        /// <returns>point</returns>
        private System.Drawing.Point Calculate_LeftEdgeTopEdge(System.Drawing.ContentAlignment alignment, System.Drawing.Rectangle rect, int width, int height)
        {
            System.Drawing.Point pt = new System.Drawing.Point(0, 0);
            switch (alignment)
            {
                case System.Drawing.ContentAlignment.BottomCenter:
                    pt.X = (rect.Width - width) / 2;
                    pt.Y = rect.Height - height;
                    break;
                case System.Drawing.ContentAlignment.BottomLeft:
                    pt.X = 0;
                    pt.Y = rect.Height - height;
                    break;
                case System.Drawing.ContentAlignment.BottomRight:
                    pt.X = rect.Width - width;
                    pt.Y = rect.Height - height;
                    break;
                case System.Drawing.ContentAlignment.MiddleCenter:
                    pt.X = (rect.Width - width) / 2;
                    pt.Y = (rect.Height - height) / 2;
                    break;
                case System.Drawing.ContentAlignment.MiddleLeft:
                    pt.X = 0;
                    pt.Y = (rect.Height - height) / 2;
                    break;
                case System.Drawing.ContentAlignment.MiddleRight:
                    pt.X = rect.Width - width;
                    pt.Y = (rect.Height - height) / 2;
                    break;
                case System.Drawing.ContentAlignment.TopCenter:
                    pt.X = (rect.Width - width) / 2;
                    pt.Y = 0;
                    break;

                case System.Drawing.ContentAlignment.TopLeft:
                    pt.X = 0;
                    pt.Y = 0;
                    break;

                case System.Drawing.ContentAlignment.TopRight:
                    pt.X = rect.Width - width;
                    pt.Y = 0;
                    break;
            }

            pt.X += rect.Left;
            pt.Y += rect.Top;
            return pt;
        }
    }
}
