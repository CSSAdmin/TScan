// -------------------------------------------------------------------------------------------
// <copyright file="TerraScanTextBox.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access TerraScanTextBox related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------
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
    using System.Resources;
    using System.Configuration;
    using System.Globalization;
    using System.Threading;

    /// <summary>
    /// Custom TextBox.
    /// </summary>
    public class TerraScanTextBox : System.Windows.Forms.TextBox
    {
        #region Variables

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = "M/d/yyyy";

        /// <summary>
        /// toolTipDecimalValue
        /// </summary>
        private string toolTipDecimalValue = String.Empty;

        /// <summary>
        /// emptyDecimalValue
        /// </summary>
        private bool emptyDecimalValue = false;

        /// <summary>
        /// DatePicker object
        /// </summary>
        private System.Windows.Forms.DateTimePicker validDate = new System.Windows.Forms.DateTimePicker();

        private System.Windows.Forms.DateTimePicker special = new System.Windows.Forms.DateTimePicker();     
        /////// <summary>
        /////// for performing range Validation 
        /////// </summary>
        ////private string validatingRegex = @"^-?\$?\d*?(\.\d{2})?$";

        /// <summary>
        /// Check Wether the textBox Is Of Type Decimal,Integer
        /// </summary>
        private int textBoxValidType;

        /// <summary>
        /// check the numberofdigits
        /// </summary>
        private int numberDigits = -1;

        /// <summary>
        /// The Number of Characters after Decimal Points
        /// </summary>
        private int decimalPrecision = 2;

        /// <summary>
        /// boolean for checking focuscolor
        /// </summary>
        private bool focusColor;

        /// <summary>
        /// boolean for checking Parent focuscolor
        /// </summary>
        private bool applyParentFocusColor = true;

        /// <summary>
        /// boolean to check the Lock type
        /// </summary>
        private bool lockKey;

        /// <summary>
        /// boolean to check currency format
        /// </summary>
        private bool applyCurrencyFormat;

        /// <summary>
        /// boolean to check currency format
        /// </summary>
        private bool applyCfgFormat;

        /// <summary>
        /// boolean to check whether the field takes empty value or not
        /// </summary>
        private bool checkForEmpty;

        /// <summary>
        /// boolean to check Querying fileld
        /// </summary>
        private bool isqueryableFileld;

        /// <summary>
        /// boolean to check editable
        /// </summary>
        private bool iseditable;

        /// <summary>
        /// string to contain queryingFileldName
        /// </summary>
        private string queryingFileldName = string.Empty;

        /// <summary>
        /// srting to contain text custom format
        /// </summary>
        private string textCustomFormat = "$ #,##0.00";

        /// <summary>
        /// srting to contain text custom format
        /// </summary>
        private string specialCharacter = "%";

        /// <summary>
        /// set the textbox Back Color
        /// </summary>
        private System.Drawing.Color textBoxBackColor = new System.Drawing.Color();

        /// <summary>
        /// set the Parent Back Color
        /// </summary>
        private System.Drawing.Color panelBackColor = new System.Drawing.Color();

        /// <summary>
        /// Sets the textBoxForeColor
        /// </summary>
        private System.Drawing.Color textBoxForeColor = new System.Drawing.Color();

        /// <summary>
        /// set textboxcolor
        /// </summary>
        private System.Drawing.Color textBoxColor = new System.Drawing.Color();

        /// <summary>
        /// string value
        /// </summary>
        private string textBoxValue;

        /// <summary>
        /// for storing Decimal value 
        /// </summary>
        private decimal decimalTextBoxValue;

        /// <summary>
        /// bool to allow Negative Sign
        /// </summary>
        private bool allowNegativeSign;

        /// <summary>
        /// for storing date value 
        /// </summary>
        private DateTime dateTextBoxValue;

        /// <summary>
        /// for storing numeric value - applied for small int
        /// </summary>
        private int numericTextBoxValue;

        /// <summary>
        /// boolean to allow Click
        /// </summary>
        private bool allowClick = true;

        /// <summary>
        /// boolean to check for text changed
        /// </summary>
        private bool textChanged;

        /// <summary>
        /// boolean to Apply Negative Standard or not
        /// </summary>
        private bool applyNegativeStandard = true;

        /// <summary>
        /// set default ForeColor
        /// </summary>
        private System.Drawing.Color defaultForeColor = Color.Empty;

        /// <summary>
        /// set default BackColor
        /// </summary>
        private System.Drawing.Color defaultBackColor = Color.Empty;

        /// <summary>
        /// boolean to allow Click
        /// </summary>
        private bool cformatWithoutSymbol;

        /// <summary>
        /// boolean to persistDefaultColor for enabled control
        /// </summary>
        private bool persistDefaultColor;

        /// <summary>
        /// boolean to disallow UserInput
        /// </summary>
        private bool enabledPersistDefaultColor;

        /// <summary>
        /// boolean to disallow UserInput
        /// </summary>
        private bool applyTimeFormat;

        /// <summary>
        /// for storing default MaxLength 
        /// </summary>
        private int defaultMaxLength = Int16.MinValue;

        /// <summary>
        /// for storing default ValidationType 
        /// </summary>
        private ControlvalidationType defaultValidationType = ControlvalidationType.Text;

        /// <summary>
        /// create a tooltip
        /// </summary>
        private ToolTip textToolTip = new ToolTip();

        /// <summary>
        /// Used to Store time
        /// </summary>
        private DateTime tempTime;

        /// <summary>
        /// boolean - if true returns whole value without removing any braces if any
        /// </summary>
        private bool getWholeTextOnPaint;

        /// <summary>
        /// boolean - if true returns whole value with comma separtor
        /// </summary>
        private bool wholeInteger;

        /// <summary>
        /// set default Forecolor for text box on negative
        /// </summary>
        private System.Drawing.Color applyNegativeForeColor = Color.Empty;

        /// <summary>
        /// Set Date Format
        /// </summary>
        private bool appliedFocusColor = false;

        /// <summary>
        /// setColorFlag
        /// </summary>
        private bool setColorFlag;

        /// <summary>
        /// isValidData - will be true when the validation is correct
        /// added by Biju on 24-Sep-2009 to fix CTRL+S validation issue
        /// </summary>
        private bool isValidData = true;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerraScanTextBox"/> class.
        /// </summary>
        public TerraScanTextBox()
        {
            this.Leave += new System.EventHandler(this.OnLeave);
            this.Enter += new System.EventHandler(this.OnEnter);
            //this.Validated += new System.EventHandler(this.OnValidated);
            //// this.Click += new System.EventHandler(this.OnClick);
            //// this.MouseDown += new MouseEventHandler(this.OnMouseDown);
            //// this.MouseUp += new MouseEventHandler(this.OnMouseUp);
            this.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
            this.KeyDown += new KeyEventHandler(this.OnKeyDown);
            this.validDate.CustomFormat = this.dateFormat; //// "m/d/yyyy";
            this.validDate.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            this.validDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            ///used for File and Instrument Date
            this.special.CustomFormat = this.dateFormat; //// "m/d/yyyy";
            this.special.MaxDate = new System.DateTime(9998,12, 31, 0, 0, 0, 0);
            this.special.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CultureInfo.CurrentCulture.Name, false);

        }

        #endregion

        #region enumeratorControlValidationType

        /// <summary>
        /// TextBox ControlvalidationType.
        /// </summary>
        public enum ControlvalidationType
        {
            /// <summary>
            /// Text = 0.
            /// </summary>
            Text = 0,

            /// <summary>
            /// Numeric = 1.
            /// </summary>
            Numeric = 1,

            /// <summary>
            /// Date = 2.
            /// </summary>
            Date = 2,

            /// <summary>
            /// Decimal = 3.
            /// </summary>
            Decimal = 3,

            /// <summary>
            ///  Time = 4.
            /// </summary>
            Time = 4,

            /// <summary>
            ///  Year = 5.
            /// </summary>
            Year = 5,

            /// <summary>
            ///  Integer = 6.
            /// </summary>
            WholeInteger = 6,

            /// <summary>
            ///  Integer = 6.
            /// </summary>
            Tinyint = 7,

            /// <summary>
            ///  Integer = 8.
            /// </summary>
            Smallint = 8,

            /// <summary>
            ///  Integer =9 .
            /// </summary>
            Money = 9,

            /// <summary>
            ///  Integer = 10.
            /// </summary>
            SmallMoney = 10,

            /// <summary>
            ///  Integer = 11.
            /// </summary>
            Bigint = 11,

            /// <summary>
            ///  Integer = 12.
            /// </summary>
            Integer = 12,

            /// For File Date and Instrument Date
            
            ///<summary>
            /// SpecialDate
            ///<sumary>
            SpecialDate = 13
        }

        #endregion

        #region Properties

        #region  General

        /// <summary>
        /// Gets or sets the type of the validate.
        /// </summary>
        /// <value>The type of the validate.</value>
        public ControlvalidationType ValidateType
        {
            set
            {
                this.textBoxValidType = (int)value;
                if (!this.textBoxValidType.Equals((int)ControlvalidationType.Text))
                {
                    this.defaultValidationType = value;
                }
            }

            get
            {
                return (ControlvalidationType)this.textBoxValidType;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [check for empty].
        /// </summary>
        /// <value><c>true</c> if [check for empty]; otherwise, <c>false</c>.</value>
        public bool CheckForEmpty
        {
            get
            {
                return this.checkForEmpty;
            }

            set
            {
                this.checkForEmpty = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the data in the control is valid after validation.
        /// added by Biju on 24-Sep-2009 to fix CTRL+S validation issue
        /// </summary>
        /// <value><c>true</c> if data is valid; otherwise, <c>false</c>.</value>
        public bool IsValidData
        {
            get
            {
                return this.isValidData;
            }

        }
        #endregion

        #region  LockKey

        /// <summary>
        /// Gets or sets a value indicating whether [lock key press].
        /// </summary>
        /// <value><c>true</c> if [lock key press]; otherwise, <c>false</c>.</value>
        public bool LockKeyPress
        {
            set
            {
                this.lockKey = value;
                if (value == true)
                {
                    this.ReadOnly = true;
                    this.TabStop = false;
                }
                else if (value == false)
                {
                    this.ReadOnly = false;
                    this.TabStop = true;
                }
            }

            get
            {
                return this.lockKey;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is editable.
        /// </summary>
        /// <value>
        ///       <c>true</c> if this instance is editable; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditable
        {
            get
            {
                return this.iseditable;
            }

            set
            {
                this.iseditable = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow click].
        /// </summary>
        /// <value><c>true</c> if [allow click]; otherwise, <c>false</c>.</value>
        public bool AllowClick
        {
            get
            {
                return this.allowClick;
            }

            set
            {
                this.allowClick = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [disallow user input].
        /// </summary>
        /// <value><c>true</c> if [disallow user input]; otherwise, <c>false</c>.</value>
        public bool PersistDefaultColor
        {
            get
            {
                return this.persistDefaultColor;
            }

            set
            {
                if (value)
                {
                    this.enabledPersistDefaultColor = true;
                }

                if (this.enabledPersistDefaultColor)
                {
                    this.SetStyle(ControlStyles.UserPaint, true);
                }

                this.persistDefaultColor = value;
            }
        }

        #endregion

        #region ForeColor

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the control's foreground color.</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                base.ForeColor = value;
                if (this.defaultForeColor == Color.Empty)
                {
                    this.defaultForeColor = this.ForeColor;
                }
            }
        }

        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the background of the control.</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                base.BackColor = value;
                if (this.defaultBackColor == Color.Empty)
                {
                    this.defaultBackColor = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [apply focus color].
        /// </summary>
        /// <value><c>true</c> if [apply focus color]; otherwise, <c>false</c>.</value>
        public bool ApplyFocusColor
        {
            set
            {
                this.focusColor = value;
            }

            get
            {
                return this.focusColor;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [apply focus color].
        /// </summary>
        /// <value><c>true</c> if [apply focus color]; otherwise, <c>false</c>.</value>
        public bool ApplyParentFocusColor
        {
            set
            {
                this.applyParentFocusColor = value;
            }

            get
            {
                return this.applyParentFocusColor;
            }
        }

        /// <summary>
        /// Gets or sets the color of the set focus.
        /// </summary>
        /// <value>The color of the set focus.</value>
        public System.Drawing.Color SetFocusColor
        {
            get
            {
                return this.textBoxColor;
            }

            set
            {
                this.textBoxColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [apply TimeFormat].
        /// </summary>
        /// <value><c>true</c> if [apply TimeFormat]; otherwise, <c>false</c>.</value>
        public bool ApplyTimeFormat
        {
            set
            {
                this.applyTimeFormat = value;
            }

            get
            {
                return this.applyTimeFormat;
            }
        }

        /// <summary>
        /// Gets or sets the Forecolor of the TextBox when negative .
        /// </summary>
        /// <value>The color of the apply negative fore.</value>
        public System.Drawing.Color ApplyNegativeForeColor
        {
            get { return this.applyNegativeForeColor; }
            set { this.applyNegativeForeColor = value; }
        }


        #endregion

        #region Query

        /// <summary>
        /// Gets or sets a value indicating whether this instance is queryable fileld.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is queryable fileld; otherwise, <c>false</c>.
        /// </value>
        public bool IsQueryableFileld
        {
            get
            {
                return this.isqueryableFileld;
            }

            set
            {
                this.isqueryableFileld = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the querying fileld.
        /// </summary>
        /// <value>The name of the querying fileld.</value>
        public string QueryingFileldName
        {
            get
            {
                return this.queryingFileldName;
            }

            set
            {
                this.queryingFileldName = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of characters the user can type or paste into the text box control.
        /// </summary>
        /// <value></value>
        /// <returns>The number of characters that can be entered into the control. The default is 32767.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned to the property is less than 0. </exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public override int MaxLength
        {
            get
            {
                return base.MaxLength;
            }

            set
            {
                base.MaxLength = value;
                if (this.defaultMaxLength == Int16.MinValue)
                {
                    this.defaultMaxLength = value;
                }
            }
        }

        #endregion

        #region Decimals

        /// <summary>
        /// The number of digits to display (to the left of the decimal). -1 for unlimited
        /// </summary>
        /// <value>The digits.</value>
        [Description("The number of digits to display (to the left of the decimal). -1 for unlimited")]
        public int Digits
        {
            get
            {
                return this.numberDigits;
            }

            set
            {
                this.numberDigits = value;
                this.BuildRegStrings();
            }
        }

        /// <summary>
        /// Apply currency format
        /// </summary>
        [Description("true apply currency format to decimal value")]
        public bool ApplyCurrencyFormat
        {
            get
            {
                return this.applyCurrencyFormat;
            }

            set
            {
                this.applyCurrencyFormat = value;
            }
        }

        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        /// <value>The precision.</value>
        [Description("The number of decimal digits to display (to the right of the decimal)")]
        public int Precision
        {
            get
            {
                return this.decimalPrecision;
            }

            set
            {
                this.decimalPrecision = value;
                this.BuildRegStrings();
            }
        }

        /// <summary>
        /// Gets or sets the special character.
        /// </summary>
        /// <value>The special character.</value>
        public string SpecialCharacter
        {
            get
            {
                return this.specialCharacter;
            }

            set
            {
                this.specialCharacter = value;
            }
        }

        /// <summary>
        /// Gets or sets the decimal text box value.
        /// </summary>
        /// <value>The decimal value.</value>
        [Description("The decimal value of the textbox")]
        public decimal DecimalTextBoxValue
        {
            get
            {
                this.SetTextBoxValue();
                return this.decimalTextBoxValue;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the text custom format.
        /// </summary>
        /// <value>The text custom format.</value>
        public string TextCustomFormat
        {
            get
            {
                return this.textCustomFormat;
            }

            set
            {
                this.textCustomFormat = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [apply CFG format].
        /// </summary>
        /// <value><c>true</c> if [apply CFG format]; otherwise, <c>false</c>.</value>
        public bool ApplyCFGFormat
        {
            get
            {
                return this.applyCfgFormat;
            }

            set
            {
                this.applyCfgFormat = value;
            }
        }

        #endregion //// Proerties For Decimal

        #region Without $Sysmbol

        /// <summary>
        /// Gets or sets a value indicating whether this instance is queryable fileld.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is queryable fileld; otherwise, <c>false</c>.
        /// </value>
        public bool CFromatWihoutSymbol
        {
            get
            {
                return this.cformatWithoutSymbol;
            }

            set
            {
                this.cformatWithoutSymbol = value;
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the numeric text box value - for small int.
        /// </summary>
        /// <value>The numeric text box value.</value>
        public int NumericTextBoxValue
        {
            get
            {
                this.SetTextBoxValue();
                return this.numericTextBoxValue;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow negative sign or not (not for string,date).
        /// </summary>
        /// <value><c>true</c> if [allow negative sign]; otherwise, <c>false</c>.</value>
        public bool AllowNegativeSign
        {
            get { return this.allowNegativeSign; }
            set { this.allowNegativeSign = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether apply negative standard(apply brackets and change color to green) or not .
        /// </summary>
        /// <value>
        /// <c>true</c> if [apply negative standard]; otherwise, <c>false</c>.
        /// </value>
        public bool ApplyNegativeStandard
        {
            get { return this.applyNegativeStandard; }
            set { this.applyNegativeStandard = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [set color flag].
        /// </summary>
        /// <value><c>true</c> if [set color flag]; otherwise, <c>false</c>.</value>
        public bool SetColorFlag
        {
            get { return this.setColorFlag; }
            set { this.setColorFlag = value; }
        }

        /// <summary>
        /// Gets or sets the date text box value.
        /// </summary>
        /// <value>The date text box value.</value>
        public DateTime DateTextBoxValue
        {
            get
            {
                this.SetTextBoxValue();
                return this.dateTextBoxValue;
            }
        }

        /// <summary>
        /// Gets or sets the WholeInteger textbox.
        /// </summary>
        /// <value>The WholeInteger textbox box value.</value>
        public bool WholeInteger
        {
            get { return this.wholeInteger; }
            set { this.wholeInteger = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [empty decimal value].
        /// </summary>
        /// <value><c>true</c> if [empty decimal value]; otherwise, <c>false</c>.</value>
        public bool EmptyDecimalValue
        {
            get { return this.emptyDecimalValue; }
            set { this.emptyDecimalValue = value; }
        }

        #endregion //// Properties

        #region Text Property

        /// <summary>
        /// Gets or sets the current text in the <see cref="T:System.Windows.Forms.TextBox"></see>.
        /// </summary>
        /// <value></value>
        public override string Text
        {
            get
            {
                this.textBoxValue = base.Text;
                if (this.applyCurrencyFormat && this.ValidateType == ControlvalidationType.Decimal && !this.getWholeTextOnPaint && this.textBoxValue.Contains("("))
                {
                    decimal tempDecimal = 0;
                    this.textBoxValue = this.textBoxValue.Replace("(", "");
                    this.textBoxValue = this.textBoxValue.Replace(")", "");
                    Decimal.TryParse(this.textBoxValue, System.Globalization.NumberStyles.Currency, null, out tempDecimal);
                    this.textBoxValue = Decimal.Negate(tempDecimal).ToString();
                }

                return this.textBoxValue;
            }

            set
            {
                ////Modified on Jan 5th 07 to retrieve carriage return - Jayanthi Sri
                this.textBoxValue = value.Replace("\n", "\r\n");
                switch (this.ValidateType)
                {
                    case ControlvalidationType.Decimal:
                        if (this.applyCurrencyFormat)
                        {
                            this.toolTipDecimalValue = string.Empty;
                            if (this.textBoxValue.Contains(this.specialCharacter))
                            {
                                this.textBoxValue = this.textBoxValue.Replace(this.specialCharacter, "");
                            }

                            if (Decimal.TryParse(this.textBoxValue, System.Globalization.NumberStyles.Currency, null, out this.decimalTextBoxValue))
                            {
                                if (this.textCustomFormat.EndsWith("%"))
                                {
                                    this.decimalTextBoxValue = this.decimalTextBoxValue / 100;
                                }

                                if (this.applyNegativeStandard && this.decimalTextBoxValue.ToString().Contains("-"))
                                {
                                    this.textBoxValue = string.Concat("(", decimal.Negate(this.decimalTextBoxValue).ToString(this.textCustomFormat), ")");
                                    this.toolTipDecimalValue = this.textBoxValue;

                                    ////when the text box is negative and apply negative fore color is given then color is set
                                    ////else the default color is set
                                    if (this.applyNegativeForeColor.Equals(Color.Empty))
                                    {
                                        this.ForeColor = Color.FromArgb(0, 128, 0);
                                    }
                                    else
                                    {
                                        this.ForeColor = this.applyNegativeForeColor;
                                    }

                                    break;
                                }
                                else
                                {
                                    this.ForeColor = this.defaultForeColor;
                                }

                                this.textBoxValue = this.decimalTextBoxValue.ToString(this.textCustomFormat);
                                //// Set Empty instead of Zero
                                ////if (this.emptyDecimalValue)
                                ////{
                                ////    this.textBoxValue = string.Empty;
                                ////}                              
                            }
                            else
                            {
                                this.textBoxValue = this.decimalTextBoxValue.ToString(this.textCustomFormat);
                                this.ForeColor = this.defaultForeColor;
                                //// Set Empty instead of Zero
                                if (this.emptyDecimalValue)
                                {
                                    this.textBoxValue = string.Empty;
                                }
                            }
                        }

                        break;

                    ////Added By Ramya

                    ////case ControlvalidationType.Money:
                    ////    {
                    ////        if (this.applyNegativeStandard && this.textBoxValue.ToString().Contains("-"))
                    ////        {
                    ////            this.textBoxValue = string.Concat("(", decimal.Negate(this.decimalTextBoxValue).ToString(this.textCustomFormat), ")");
                    ////            ////this.toolTipDecimalValue = this.textBoxValue;
                    ////            ///this.ForeColor = Color.FromArgb(1280, 128, 0);
                    ////            break;
                    ////        }
                    ////        else
                    ////        {
                    ////            this.ForeColor = this.defaultForeColor;
                    ////        }
                    ////        break;
                    ////        this.textBoxValue = this.textBoxValue.ToString(this.textCustomFormat);
                    ////    }

                    case ControlvalidationType.Date:
                        {
                            DateTime tempValue = new DateTime();
                            if (DateTime.TryParse(this.textBoxValue.Trim(), out tempValue))
                            {
                                try
                                {
                                    this.validDate.Value = DateTime.Parse(this.textBoxValue);
                                    this.textBoxValue = this.validDate.Value.ToString(this.dateFormat);
                                }
                                catch
                                {
                                    /////MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), SharedFunctions.GetResourceString("DateValidation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    MessageBox.Show(Strings.ValidDate + "\n" + Strings.Allowed + "\n" + Strings.MinDate + this.validDate.MinDate.ToShortDateString() + "\n" + Strings.MaxDate + this.validDate.MaxDate.ToShortDateString(), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    this.textBoxValue = DateTime.Now.ToString(this.dateFormat);
                                    this.Focus();
                                    //// TODO
                                }
                            }

                            break;
                        }
                        ////used for File and Instrument Date
                    case ControlvalidationType.SpecialDate:
                        {
                            DateTime tempValue = new DateTime();
                            if (DateTime.TryParse(this.textBoxValue.Trim(), out tempValue))
                            {
                                try
                                {
                                    this.special.Value = DateTime.Parse(this.textBoxValue);
                                    this.textBoxValue = this.special.Value.ToString(this.dateFormat);
                                }
                                catch
                                {
                                    /////MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), SharedFunctions.GetResourceString("DateValidation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    MessageBox.Show(Strings.ValidDate + "\n" + Strings.Allowed + "\n" + Strings.MinDate + this.special.MinDate.ToShortDateString() + "\n" + Strings.MaxDate + this.special.MaxDate.ToShortDateString(), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    this.textBoxValue = DateTime.Now.ToString(this.dateFormat);
                                    this.Focus();
                                    //// TODO
                                }
                            }

                            break;
                        }
                    case ControlvalidationType.Money:
                        if (!string.IsNullOrEmpty(this.textBoxValue))
                        {
                            decimal outValue;
                            string moneyValue = this.textBoxValue;

                            if (moneyValue.Contains("$"))
                            {
                                moneyValue = moneyValue.Replace("$", "").Trim();
                            }

                            if (moneyValue.Contains(","))
                            {
                                moneyValue = moneyValue.Replace(",", "").Trim();
                            }

                            if (moneyValue.Contains("-") || moneyValue.Contains("("))
                            {
                                if (moneyValue.Contains("("))
                                {
                                    moneyValue = moneyValue.Replace("(", "").Trim();
                                }

                                if (moneyValue.Contains(")"))
                                {
                                    moneyValue = moneyValue.Replace(")", "").Trim();
                                }
                                ////Commented by Biju on 08/Feb/2010 to fix #5877
                                ////this.Text = this.Text.Replace("-", "").Trim();
                                ////Modified by Biju on 08/Feb/2010 to fix #5877
                                decimal.TryParse(moneyValue.Replace("-", "").Trim(), out outValue);
                                ////Commented by Biju on 08/Feb/2010 to fix #5877
                                ////outValue = decimal.Negate(outValue);
                                if (this.applyNegativeStandard)
                                {
                                    this.textBoxValue = string.Concat("(", outValue.ToString(this.textCustomFormat), ")");
                                    this.ForeColor = Color.FromArgb(128, 0, 0);
                                }
                                else
                                {
                                    ////this.ForeColor=this.Text = 
                                    this.textBoxValue = outValue.ToString(this.textCustomFormat);
                                }
                            }
                            else
                            {

                                decimal.TryParse(moneyValue, out outValue);
                                ////// Added to fix the Issue in Parcel Sale tracking form's textboxes validation type 'Money'
                                if (outValue > 0 && this.textBoxValidType == (int)ControlvalidationType.Money && SetColorFlag)
                                {
                                    this.ForeColor = Color.FromArgb(0, 128, 0);
                                }
                                else
                                {
                                    this.ForeColor = this.defaultForeColor;
                                }

                                this.textBoxValue = outValue.ToString(this.textCustomFormat).Trim();
                            }
                        }
                        else
                        {
                            this.textBoxValue = "0";
                        }

                        break;

                    case ControlvalidationType.SmallMoney:
                        if (!string.IsNullOrEmpty(this.textBoxValue.Trim()))
                        {
                            decimal outValue;
                            string moneyValue = this.textBoxValue.Trim();

                            if (moneyValue.Contains("$"))
                            {
                                moneyValue = moneyValue.Replace("$", "").Trim();
                            }

                            if (moneyValue.Contains(","))
                            {
                                moneyValue = moneyValue.Replace(",", "").Trim();
                            }

                            if (moneyValue.Contains("-") || moneyValue.Contains("("))
                            {
                                if (moneyValue.Contains("("))
                                {
                                    moneyValue = moneyValue.Replace("(", "").Trim();
                                }

                                if (moneyValue.Contains(")"))
                                {
                                    moneyValue = moneyValue.Replace(")", "").Trim();
                                }
                                ////Commented by Biju on 08/Feb/2010 to fix #5877
                                ////this.Text = this.Text.Replace("-", "").Trim();
                                ////Modified by Biju on 08/Feb/2010 to fix #5877
                                decimal.TryParse(moneyValue.Replace("-", "").Trim(), out outValue);
                                ////Commented by Biju on 08/Feb/2010 to fix #5877
                                ////outValue = decimal.Negate(outValue);
                                if (this.applyNegativeStandard)
                                {
                                    this.textBoxValue = string.Concat("(", outValue.ToString(this.textCustomFormat), ")");
                                    this.ForeColor = Color.FromArgb(128, 0, 0);
                                }
                                else
                                {
                                    ////this.ForeColor=this.Text = 
                                    this.textBoxValue = outValue.ToString(this.textCustomFormat);
                                }
                            }
                            else
                            {

                                decimal.TryParse(moneyValue, out outValue);
                                ////// Added to fix the Issue in Parcel Sale tracking form's textboxes validation type 'Money'
                                if (outValue > 0 && this.textBoxValidType == (int)ControlvalidationType.Money && SetColorFlag)
                                {
                                    this.ForeColor = Color.FromArgb(0, 128, 0);
                                }
                                else
                                {
                                    this.ForeColor = this.defaultForeColor;
                                }

                                this.textBoxValue = outValue.ToString(this.textCustomFormat).Trim();
                            }
                        }
                        else
                        {
                            this.textBoxValue = "0";
                        }
                        break;
                }

                base.Text = this.textBoxValue;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the defult max length.
        /// </summary>
        public void SetMaxLength()
        {
            if (this.defaultMaxLength > -1)
            {
                this.MaxLength = this.defaultMaxLength;
            }
            else
            {
                this.MaxLength = Int16.MaxValue;
            }
        }

        /// <summary>
        /// Sets the defult Back color
        /// </summary>
        public void SetDefaultBackColor()
        {
            this.BackColor = this.defaultBackColor;
        }

        /// <summary>
        /// Sets the default validtype.
        /// </summary>
        public void SetDefaultValidType()
        {
            this.ValidateType = this.defaultValidationType;
        }


        /// <summary>
        /// Validate the textBox Control value on leave and validated event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void validDateText(object sender, EventArgs e)
        {

            ////Added by Biju on 24-Sep-2009 to set IsValidData flag when the validation fails
            this.isValidData = true;

            ////To retain the correct text from left after removing the selection
            this.Select(0, 0);
            TerraScanTextBox terraScanTextBox = (TerraScanTextBox)sender;
            if (this.ApplyFocusColor)
            {
                terraScanTextBox.BackColor = this.textBoxBackColor;
                terraScanTextBox.ForeColor = this.textBoxForeColor;
                if (this.ApplyParentFocusColor == true)
                {
                    if (this.Parent.GetType() == typeof(System.Windows.Forms.Panel))
                    {
                        Panel parentPanel = (Panel)this.Parent;
                        parentPanel.BackColor = this.panelBackColor;
                    }
                }
            }

            ////Added By Ramya.D
            if (this.textBoxValidType == (int)ControlvalidationType.Money)
            {
                ////Added by Biju on 08/Feb/2010 to fix #5877
                //this.Text = this.DecimalTextBoxValue.ToString();
                ////till here
                if (this.CheckRange(922337203685477.5807M, -922337203685477.5808M))
                {
                    if (!this.LockKeyPress && this.textChanged)
                    {
                        terraScanTextBox.Text = this.Text.Trim();
                    }
                }
                else
                {
                    terraScanTextBox.Text = "0".Trim();
                    terraScanTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                }

                this.textChanged = false;
            }
            else if (this.textBoxValidType == (int)ControlvalidationType.SmallMoney)
            {
                if (this.CheckRange(214748.3647M, -214748.3648M))
                {
                    if (!this.LockKeyPress && this.textChanged)
                    {
                        terraScanTextBox.Text = this.Text;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.Text))
                    {
                        terraScanTextBox.Text = "0";
                        terraScanTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                    }
                }

                this.textChanged = false;
            }
            ////else if (this.textBoxValidType == (int)ControlvalidationType.Bigint)
            ////{
            ////    if (this.CheckRange(9223372036854775807, -9223372036854775808))
            ////    {
            ////        terraScanTextBox.Text = this.Text;
            ////    }
            ////    else
            ////    {
            ////        terraScanTextBox.Text = "0";
            ////    }
            ////}           

            ////else if (this.textBoxValidType == (int)ControlvalidationType.smallmoney)
            ////{
            ////    if (this.CheckRange(214748.3647M, -214748.3648M))
            ////    {
            ////        terraScanTextBox.Text = this.Text;
            ////    }
            ////    else
            ////    {
            ////        terraScanTextBox.Text = "0";
            ////    }
            ////}            
            else if (this.textBoxValidType == (int)ControlvalidationType.Date)
            {
                try
                {
                    if (!this.checkForEmpty && string.IsNullOrEmpty(this.Text.Trim()))
                    {


                        if (this.Text.Length > 0)
                        {
                            MessageBox.Show(Strings.ValidDate + "\n" + Strings.Allowed + "\n" + Strings.MinDate + this.validDate.MinDate.ToShortDateString() + "\n" + Strings.MaxDate + this.validDate.MaxDate.ToShortDateString(), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Text = DateTime.Now.ToString(this.dateFormat);
                            ////Added by Biju on 24-Sep-2009 to set IsValidData flag when the validation fails
                            this.isValidData = false;
                            terraScanTextBox.Focus();
                        }
                        else
                        {
                            this.Text = this.Text.Trim();
                        }

                        return;
                    }

                    this.validDate.Value = DateTime.Parse(terraScanTextBox.Text.Trim());
                    terraScanTextBox.Text = this.validDate.Value.ToString(this.dateFormat);
                }
                catch
                {
                    ////MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), SharedFunctions.GetResourceString("DateValidation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MessageBox.Show(Strings.ValidDate + "\n" + Strings.Allowed + "\n" + Strings.MinDate + this.validDate.MinDate.ToShortDateString() + "\n" + Strings.MaxDate + this.validDate.MaxDate.ToShortDateString(), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    terraScanTextBox.Text = DateTime.Now.ToString(this.dateFormat);
                    terraScanTextBox.Focus();
                    ////Added by Biju on 24-Sep-2009 to set IsValidData flag when the validation fails
                    this.isValidData = false;
                }
            }
                ///used for File and Instrument Date
            else if (this.textBoxValidType == (int)ControlvalidationType.SpecialDate)
            {
                try
                {
                    if (!this.checkForEmpty && string.IsNullOrEmpty(this.Text.Trim()))
                    {


                        if (this.Text.Length > 0)
                        {
                            MessageBox.Show(Strings.ValidDate + "\n" + Strings.Allowed + "\n" + Strings.MinDate + this.special.MinDate.ToShortDateString() + "\n" + Strings.MaxDate + this.special.MaxDate.ToShortDateString(), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Text = DateTime.Now.ToString(this.dateFormat);
                            ////Added by Biju on 24-Sep-2009 to set IsValidData flag when the validation fails
                            this.isValidData = false;
                            terraScanTextBox.Focus();
                        }
                        else
                        {
                            this.Text = this.Text.Trim();
                        }

                        return;
                    }

                    this.special.Value = DateTime.Parse(terraScanTextBox.Text.Trim());
                    terraScanTextBox.Text = this.special.Value.ToString(this.dateFormat);
                }
                catch
                {
                    ////MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), SharedFunctions.GetResourceString("DateValidation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MessageBox.Show(Strings.ValidDate + "\n" + Strings.Allowed + "\n" + Strings.MinDate + this.special.MinDate.ToShortDateString() + "\n" + Strings.MaxDate + this.special.MaxDate.ToShortDateString(), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    terraScanTextBox.Text = DateTime.Now.ToString(this.dateFormat);
                    terraScanTextBox.Focus();
                    ////Added by Biju on 24-Sep-2009 to set IsValidData flag when the validation fails
                    this.isValidData = false;
                }
            }
            else if (this.textBoxValidType == (int)ControlvalidationType.Time)
            {
                if (!this.checkForEmpty && string.IsNullOrEmpty(this.Text.Trim()))
                {
                    return;
                }

                if (DateTime.TryParse(DateTime.Now.ToShortDateString() + " " + terraScanTextBox.Text, out this.tempTime) && terraScanTextBox.Text != "am" && terraScanTextBox.Text != "pm" && terraScanTextBox.Text != "AM" && terraScanTextBox.Text != "PM" && terraScanTextBox.Text != "Am" && terraScanTextBox.Text != "Pm" && terraScanTextBox.Text != "aM" && terraScanTextBox.Text != "pM" && terraScanTextBox.Text != "." && terraScanTextBox.Text != ",")
                {
                    //// Used For Formating  the Date/Time
                    DateTime tempDateTime;
                    /* Modified for Bug #2359: Unhandle exception occurs if series of dot entered in Time fields. */
                    if (terraScanTextBox.Text.Contains("."))
                    {
                        terraScanTextBox.Text = string.Empty;
                        terraScanTextBox.Focus();
                    }
                    /*Till here - Modified for Bug #2359*/
                    else
                    {
                        DateTime.TryParse(terraScanTextBox.Text, out tempDateTime);
                        ////tempDateTime = Convert.ToDateTime(terraScanTextBox.Text);
                        terraScanTextBox.Text = tempDateTime.ToString("hh:mm tt");
                    }
                    ////if (!(terraScanTextBox.Text.ToUpper().LastIndexOf("AM") > 0) && !(terraScanTextBox.Text.ToUpper().LastIndexOf("PM") > 0))
                    ////{
                    ////    terraScanTextBox.Text = DateTime.ParseExact(terraScanTextBox.Text, "t", null);
                    ////    terraScanTextBox.Text = terraScanTextBox.Text + " AM";
                    ////} 


                }
                else
                {
                    ////MessageBox.Show("Enter valid time format (HH:MM)", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    terraScanTextBox.Text = string.Empty;
                    terraScanTextBox.Focus();
                }
            }
            else
            {
                ////terraScanTextBox.Text="0";
            }
        }

        #region Decimals

        /// <summary>
        /// Builds the reg strings.
        /// </summary>
        public void BuildRegStrings()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"^-?\$?\d");
            //// keypressregex, digits
            if (this.numberDigits < 0)
            {
                //// no limit on digits
                sb.Append(@"*");
            }
            else
            {
                sb.Append(@"{,");
                sb.Append(this.numberDigits.ToString());
                sb.Append("}");
            }
            //// keypressregex, precision
            sb.Append(@"?(\.)?(\d{0,");
            sb.Append(this.decimalPrecision.ToString());
            sb.Append(@"})?$");
            //// onvalidatingregex, digits
            sb = new StringBuilder();
            sb.Append(@"^-?\$?\d|,");
            if (this.numberDigits < 0)
            {   // no limit on digits
                sb.Append(@"*");
            }
            else
            {
                sb.Append(@"{0,");
                sb.Append(this.numberDigits.ToString());
                sb.Append("}");
            }

            //// onvalidatingregex, precision
            sb.Append(@"?(\.\d{0,");
            sb.Append(this.decimalPrecision.ToString());
            sb.Append(@"})?$");
            ////this.validatingRegex = sb.ToString();
        }

        #endregion

        #region Events

        #region protectedEvents

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Enter"></see> event
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnEnter(EventArgs e)
        {
            ////if (this.LockKeyPress && !this.allowClick)
            ////{
            ////    return;
            ////}
            base.OnEnter(e);
        }

        /// <summary>
        /// Raises the text changed event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            ////DateTime tempValue = new DateTime();
            if (!this.LockKeyPress && this.Focused)
            {
                this.textChanged = true;
            }

            base.OnTextChanged(e);
        }

        /// <summary>
        /// make sure the text is in the correct format
        /// </summary>
        /// <param name="e">CancelEvent</param>
        protected override void OnValidating(CancelEventArgs e)
        {
            try
            {
                string tempStr = this.Text.Trim();
                switch (this.ValidateType)
                {
                    case ControlvalidationType.Decimal:
                        tempStr = tempStr.Replace(this.specialCharacter, "");
                        if (!this.checkForEmpty && string.IsNullOrEmpty(this.Text.Trim()))
                        {
                            return;
                        }

                        if (Decimal.TryParse(tempStr, System.Globalization.NumberStyles.Currency, null, out this.decimalTextBoxValue))
                        {
                            if (!this.LockKeyPress && this.textChanged)
                            {
                                if (this.applyCfgFormat)
                                {
                                    if (tempStr.EndsWith("."))
                                    {
                                        tempStr = string.Concat(tempStr, "0");
                                    }
                                    else if (!tempStr.Contains("."))
                                    {
                                        this.decimalTextBoxValue = this.decimalTextBoxValue / Convert.ToDecimal((Math.Pow(10, this.decimalPrecision)));
                                        tempStr = this.decimalTextBoxValue.ToString();
                                    }
                                }

                                this.Text = tempStr;
                            }
                        }
                        else
                        
                        {
                            this.Text = this.decimalTextBoxValue.ToString();
                        }

                        break;
                    case ControlvalidationType.Numeric:
                        ////Modified by Jayanthi
                        int tempValue;
                        if (!this.checkForEmpty && string.IsNullOrEmpty(this.Text.Trim()))
                        {
                            return;
                        }

                        if (!(int.TryParse(this.Text, out tempValue)))
                        {
                            this.Text = "0";
                            this.Focus();
                        }

                        ////Till here
                        break;
                    case ControlvalidationType.Year:
                        if (!this.checkForEmpty && string.IsNullOrEmpty(this.Text.Trim()))
                        {
                            return;
                        }

                        if (int.TryParse(tempStr, out this.numericTextBoxValue))
                        {
                            if (this.numericTextBoxValue != 0 && (this.numericTextBoxValue < this.validDate.MinDate.Year || this.numericTextBoxValue > this.validDate.MaxDate.Year))
                            {
                                MessageBox.Show(Strings.YearRangeValidate, ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ////////default value
                                this.Text = "0";
                                this.Focus();
                            }
                            else
                            {
                                this.Text = this.numericTextBoxValue.ToString();
                            }
                        }
                        else
                        {
                            ////MessageBox.Show(Strings.InvalidFieldYear, ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Text = this.numericTextBoxValue.ToString();
                            this.Focus();
                        }

                        break;
                    case ControlvalidationType.WholeInteger:
                        Int64 tempValue1;
                        Int64.TryParse(this.Text.Replace(",", ""), out tempValue1);
                        this.Text = tempValue1.ToString("#,##0");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.textChanged = false;
                base.OnValidating(e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Click"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            ////if (this.LockKeyPress && !this.allowClick)
            ////{
            ////    return;
            ////}
            base.OnClick(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.Focused && this.LockKeyPress)
            {
                ////if (this.allowClick)
                {
                    this.SelectAll();
                }

                return;
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            if (!this.Multiline)
            {
                string tempValue = string.Empty;
                if (!string.IsNullOrEmpty(this.toolTipDecimalValue))
                {
                    tempValue = this.toolTipDecimalValue;
                }
                else
                {
                    tempValue = this.Text;
                }

                Graphics graphics = this.CreateGraphics();
                SizeF sizeF = graphics.MeasureString(tempValue, this.Font);
                float preferredwidth = sizeF.Width;
                if (this.ValidateType.Equals(ControlvalidationType.Decimal) && tempValue.Contains("-"))
                {
                    ////for balancing "(",")"
                    preferredwidth = sizeF.Width + 7;
                }

                if (preferredwidth > this.Width)
                {
                    this.textToolTip.RemoveAll();
                    this.textToolTip.SetToolTip(this, tempValue);
                }
                else
                {
                    this.textToolTip.RemoveAll();
                }

                graphics.Dispose();
            }

            base.OnMouseEnter(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            ////if (this.LockKeyPress && !this.allowClick)
            ////{
            ////    return;
            ////}
            this.SelectAllText();
            base.OnMouseDown(e);
        }

        /////// <summary>
        /////// Called when [mouse up].
        /////// </summary>
        /////// <param name="sender">The sender.</param>
        /////// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        ////private void OnMouseUp(object sender, MouseEventArgs e)
        ////{
        ////    ////this.SelectAllText();
        ////}
        /////// <summary>
        /////// Called when [mouse down].
        /////// </summary>
        /////// <param name="sender">The sender.</param>
        /////// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        ////private void OnMouseDown(object sender, MouseEventArgs e)
        ////{
        ////    this.SelectAllText();
        ////}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.enabledPersistDefaultColor)
            {
                SolidBrush textBrush;
                SolidBrush backBrush;
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                switch (this.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case HorizontalAlignment.Left:
                        sf.Alignment = StringAlignment.Near;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        break;
                }

                RectangleF rdraw = new RectangleF(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - 2, this.ClientRectangle.Height - 2);
                rdraw.Inflate(0, 0);
                if (this.BorderStyle.Equals(BorderStyle.FixedSingle))
                {
                    backBrush = new SolidBrush(Color.Black);
                    e.Graphics.FillRectangle(backBrush, 0.0F, 0.0F, this.Width, this.Height);
                }

                if (this.persistDefaultColor)
                {
                    textBrush = new SolidBrush(this.ForeColor);
                    backBrush = new SolidBrush(this.BackColor);
                }
                else
                {
                    textBrush = new SolidBrush(Color.Gray);
                    backBrush = new SolidBrush(this.BackColor);
                }
                ////textBrush = new SolidBrush(this.ForeColor);
                ////SolidBrush backBrush = new SolidBrush(Color.Black);
                ////e.Graphics.FillRectangle(backBrush, 0.0F, 0.0F, this.Width, this.Height);
                ////backBrush = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(backBrush, 1.0F, 1.0F, this.Width - 2, this.Height - 2);
                ////if (this.Text.Contains("-"))
                ////{
                ////    this.Invalidate();
                ////}
                this.getWholeTextOnPaint = true;
                e.Graphics.DrawString(this.Text, this.Font, textBrush, rdraw, sf);
                this.getWholeTextOnPaint = false;
                if (this.Focused && this.LockKeyPress)
                {
                    this.SelectAll();
                }
            }
            else
            {
                base.OnPaint(e);
            }
        }

        /// <summary>
        /// Gets a value indicating the state of the <see cref="P:System.Windows.Forms.TextBoxBase.ShortcutsEnabled"/> property.
        /// </summary>
        /// <param name="msg">A <see cref="T:System.Windows.Forms.Message"/>, passed by reference that represents the window message to process.</param>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the shortcut key to process.</param>
        /// <returns>
        /// true if the shortcut key was processed by the control; otherwise, false.
        /// </returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Code forBug #5940: preview(Ctrl+R) and Email(Ctrl+E) is not working 
            // while the TextBox state is read only.
            if (this.ReadOnly
                && (keyData.Equals(Keys.Control | Keys.E)
                || keyData.Equals(Keys.Control | Keys.R)))
            {
                this.ReadOnly = false;
                base.ProcessCmdKey(ref msg, keyData);
                this.ReadOnly = true;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region PrivateEvents


        private void OnValidated(object sender, EventArgs e)
        {
            this.validDateText(sender, e);
        }

        /// <summary>
        /// BackGround Color Change
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">click</param>
        private void OnLeave(object sender, EventArgs e)
        {
            if (this.appliedFocusColor)
            {
                this.ApplyParentFocusColor = true;
                this.ApplyFocusColor = true;
                this.appliedFocusColor = false;
            }
            this.validDateText(sender, e);

        }

        /// <summary>
        /// Key Press Event
        /// </summary>
        /// <param name="sender">Control</param>
        /// <param name="e">KeyPress</param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            int keyCode = (int)e.KeyChar;
            if (keyCode == 3 || keyCode == 22)
            {
                return;
            }

            if (!this.LockKeyPress)
            {
                try
                {
                    switch (this.textBoxValidType)
                    {
                        case (int)ControlvalidationType.Year:
                        ////numeric validation applied
                        case (int)ControlvalidationType.Numeric:
                            {
                                ////keycode 45 which allows negative sign
                                if (keyCode == 8 || e.KeyChar == 24 || e.KeyChar == 26 || e.KeyChar == 25 || (this.allowNegativeSign && keyCode == 45))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else if (char.IsDigit(e.KeyChar))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else
                                {
                                    e.Handled = true;
                                    break;
                                }
                            }

                        case (int)ControlvalidationType.WholeInteger:
                            {
                                ////keycode 45 which allows negative sign
                                if (keyCode == 8 || e.KeyChar == 24 || e.KeyChar == 26 || e.KeyChar == 25 || (this.allowNegativeSign && keyCode == 45))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else if (char.IsDigit(e.KeyChar))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else
                                {
                                    e.Handled = true;
                                    break;
                                }
                            }

                        case (int)ControlvalidationType.Text:
                            {
                                e.Handled = false;
                                break;
                                /* if (char.IsDigit(e.KeyChar) || (keyCode <= 47 || keyCode > 57) && (keyCode < 64 || keyCode > 90) && (keyCode < 97 || keyCode > 122) && (keyCode != 8) && (keyCode != 32))
                                 {
                                     e.Handled = true;
                                     return;
                                     break;
                                 }
                                 else
                                 {
                                     e.Handled = false;
                                     return;
                                     break;
                                 }*/
                            }

                        case (int)ControlvalidationType.Date:
                            {
                                if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == Convert.ToChar("-")) && !(e.KeyChar == Convert.ToChar(".")) && !(e.KeyChar == Convert.ToChar("\\")) && !(e.KeyChar == Convert.ToChar("/")) && !char.IsControl(e.KeyChar))
                                {
                                    e.Handled = true;
                                }

                                return;
                            }
                            ///used for File and Instrument Date
                        case (int)ControlvalidationType.SpecialDate:
                            {
                                if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == Convert.ToChar("-")) && !(e.KeyChar == Convert.ToChar(".")) && !(e.KeyChar == Convert.ToChar("\\")) && !(e.KeyChar == Convert.ToChar("/")) && !char.IsControl(e.KeyChar))
                                {
                                    e.Handled = true;
                                }

                                return;
                            }

                        case (int)ControlvalidationType.Decimal:
                            {
                                int keyCodeDecimal = (int)e.KeyChar;
                                ////this coded added by Biju to check the number of character entered in the textbox
                                if (!this.numberDigits.Equals(-1))
                                {
                                    if (this.Text.Length.Equals(this.SelectionLength) & char.IsDigit(e.KeyChar))
                                    {
                                        e.Handled = false;
                                        break;
                                    }

                                    string textBoxValue = string.IsNullOrEmpty(this.specialCharacter) ? this.Text : this.Text.Trim().Replace(this.specialCharacter, "");
                                    textBoxValue = this.Text.Trim().Replace(",", "");
                                    ////if number of integer parts are defined, then after entering that 
                                    ////no digits should be allowed other than the decimal point.
                                    if (textBoxValue.Length >= this.numberDigits & char.IsDigit(e.KeyChar) & !textBoxValue.Contains("."))
                                    {
                                        e.Handled = true;
                                        break;
                                    }
                                    else if (textBoxValue.Contains(".") & char.IsDigit(e.KeyChar))
                                    {
                                        int dotPosition = this.textBoxValue.IndexOf('.');
                                        if ((textBoxValue.Split(new Char[] { '.' })[1]).Length >= this.Precision & this.SelectionStart > dotPosition)
                                        {
                                            e.Handled = true;
                                            break;
                                        }
                                        else if ((textBoxValue.Split(new Char[] { '.' })[0]).Length >= this.numberDigits & this.SelectionStart <= dotPosition)
                                        {
                                            e.Handled = true;
                                            break;
                                        }
                                    }
                                }

                                ////till here added by Biju
                                ////keycode 45 which allows negative sign
                                if (char.IsDigit(e.KeyChar) || keyCodeDecimal == 8 || e.KeyChar == 24 || e.KeyChar == 26 || e.KeyChar == 25 || (this.allowNegativeSign && keyCodeDecimal == 45))
                                {
                                    e.Handled = false;
                                }
                                else if (this.allowNegativeSign && keyCodeDecimal == 45)
                                {
                                    e.Handled = false;
                                }
                                else if (keyCodeDecimal == 46)
                                {
                                    if (this.Text.IndexOf(".") < 0)
                                    {
                                        e.Handled = false;
                                    }
                                    else
                                    {
                                        e.Handled = true;
                                    }
                                }
                                else
                                {
                                    e.Handled = true;
                                }

                                break;
                            }
                        //// Added By Ramya.D
                        case (int)ControlvalidationType.Money:
                            {
                                int keyCodeDecimal = (int)e.KeyChar;
                                if (char.IsDigit(e.KeyChar) || keyCodeDecimal == 8 || e.KeyChar == 24 || e.KeyChar == 26 || e.KeyChar == 25 || (this.allowNegativeSign && keyCodeDecimal == 45))
                                {
                                    e.Handled = false;
                                }
                                else if (this.allowNegativeSign && keyCodeDecimal == 45)
                                {
                                    e.Handled = false;
                                }
                                else if (keyCodeDecimal == 46)
                                {
                                    if (this.Text.IndexOf(".") < 0)
                                    {
                                        e.Handled = false;
                                    }
                                    else
                                    {
                                        e.Handled = true;
                                    }
                                }
                                else
                                {
                                    e.Handled = true;
                                }

                                break;
                            }

                        case (int)ControlvalidationType.SmallMoney:
                            {
                                int keyCodeDecimal = (int)e.KeyChar;
                                if (char.IsDigit(e.KeyChar) || keyCodeDecimal == 8 || keyCodeDecimal == 46 || e.KeyChar == 24 || e.KeyChar == 26 || e.KeyChar == 25 || (this.allowNegativeSign && keyCodeDecimal == 45))
                                {
                                    e.Handled = false;
                                }
                                else if (this.allowNegativeSign && keyCodeDecimal == 45)
                                {
                                    e.Handled = false;
                                }
                                else if (keyCodeDecimal == 46)
                                {
                                    if (this.Text.IndexOf(".") < 0)
                                    {
                                        e.Handled = false;
                                    }
                                    else
                                    {
                                        e.Handled = true;
                                    }
                                }
                                else
                                {
                                    e.Handled = true;
                                }

                                break;
                            }

                        case (int)ControlvalidationType.Bigint:
                            {
                                ////keycode 45 which allows negative sign
                                if (keyCode == 8 || e.KeyChar == 24 || e.KeyChar == 26 || e.KeyChar == 25 || (this.allowNegativeSign && keyCode == 45))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else if (char.IsDigit(e.KeyChar))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else
                                {
                                    e.Handled = true;
                                    break;
                                }
                            }

                        case (int)ControlvalidationType.Smallint:
                            {
                                ////keycode 45 which allows negative sign
                                if (keyCode == 8 || e.KeyChar == 24 || e.KeyChar == 26 || e.KeyChar == 25 || (this.allowNegativeSign && keyCode == 45))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else if (char.IsDigit(e.KeyChar))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else
                                {
                                    e.Handled = true;
                                    break;
                                }
                            }

                        case (int)ControlvalidationType.Tinyint:
                            {
                                ////keycode 45 which allows negative sign
                                if (keyCode == 8 || e.KeyChar == 24 || e.KeyChar == 26 || e.KeyChar == 25 || (!this.allowNegativeSign && keyCode != 45))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else if (char.IsDigit(e.KeyChar))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else
                                {
                                    e.Handled = true;
                                    break;
                                }
                            }

                        case (int)ControlvalidationType.Integer:
                            {
                                ////keycode 45 which allows negative sign
                                if (keyCode == 8 || e.KeyChar == 24 || e.KeyChar == 26 || e.KeyChar == 25 || (this.allowNegativeSign && keyCode == 45))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else if (char.IsDigit(e.KeyChar))
                                {
                                    e.Handled = false;
                                    break;
                                }
                                else
                                {
                                    e.Handled = true;
                                    break;
                                }
                            }
                    }
                }
                catch (Exception)
                {
                    //// MessageBox.Show(ex.Message, SharedFunctions.GetResourceString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ////MessageBox.Show(ex.Message, ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// back Ground Color Change
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">click</param>
        private void OnEnter(object sender, EventArgs e)
        {
            TerraScanTextBox terraScanTextBox = (TerraScanTextBox)sender;
            if (this.ApplyFocusColor)
            {
                this.appliedFocusColor = true;
                this.textBoxBackColor = terraScanTextBox.BackColor;
                this.textBoxForeColor = terraScanTextBox.ForeColor;
                terraScanTextBox.BackColor = this.SetFocusColor;
                terraScanTextBox.ForeColor = System.Drawing.Color.Black;
                if (this.ApplyParentFocusColor == true)
                {
                    if (this.Parent.GetType() == typeof(System.Windows.Forms.Panel))
                    {
                        Panel parentPanel = (Panel)this.Parent;
                        this.panelBackColor = parentPanel.BackColor;
                        parentPanel.BackColor = this.SetFocusColor;
                    }
                }
                this.ApplyFocusColor = false;
                this.ApplyParentFocusColor = false;
            }

            this.SelectAll();
        }

        /////// <summary>
        /////// Called when [click].
        /////// </summary>
        /////// <param name="sender">The sender.</param>
        /////// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        ////private void OnClick(object sender, EventArgs e)
        ////{
        ////   this.SelectAllText();
        ////}

        /// <summary>
        /// Selects all text.
        /// </summary>
        private void SelectAllText()
        {
            if (this.LockKeyPress == true)
            {
                this.SelectAll();
            }
        }

        /// <summary>
        /// Called when [key down].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            ////ALT key value 18 and F4 key value 115 are allowed in this event for form closing using short cut keys (ALT + F4)           
            if (this.LockKeyPress == true && e.KeyValue != 18 && e.KeyValue != 115)
            {
                e.Handled = true;
            }
            else
            {
                if (e.KeyCode.Equals(Keys.Delete))
                {
                    if (!this.numberDigits.Equals(-1))
                    {
                        string textBoxValue = string.IsNullOrEmpty(this.specialCharacter) ? this.Text : this.Text.Trim().Replace(this.specialCharacter, "");
                        textBoxValue = this.Text.Trim().Replace(",", "");
                        string[] textBoxNumbersArray;
                        textBoxNumbersArray = this.textBoxValue.Split(new char[] { '.' });
                        if (this.Text.Contains("."))
                        {
                            if (this.SelectionStart.Equals(this.Text.Trim().IndexOf('.')) & (textBoxNumbersArray[0].ToString().Length + textBoxNumbersArray[1].ToString().Length) > this.numberDigits)
                            {
                                e.Handled = true;
                            }
                        }
                    }
                }
                else if (e.KeyCode.Equals(Keys.Back))
                {
                    if (!this.numberDigits.Equals(-1))
                    {
                        string textBoxValue = string.IsNullOrEmpty(this.specialCharacter) ? this.Text : this.Text.Trim().Replace(this.specialCharacter, "");
                        textBoxValue = this.Text.Trim().Replace(",", "");
                        string[] textBoxNumbersArray;
                        textBoxNumbersArray = this.textBoxValue.Split(new char[] { '.' });
                        if (this.Text.Contains("."))
                        {
                            if (this.SelectionStart.Equals(this.Text.Trim().IndexOf('.') + 1) & (textBoxNumbersArray[0].ToString().Length + textBoxNumbersArray[1].ToString().Length) > this.numberDigits & !this.numberDigits.Equals(-1))
                            {
                                e.Handled = true;
                                e.SuppressKeyPress = true;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// sets the default datatype value.
        /// </summary>
        private void SetTextBoxValue()
        {
            switch (this.ValidateType)
            {
                case ControlvalidationType.Date:
                    DateTime.TryParse(this.Text.Trim(), out this.dateTextBoxValue);
                    break;
                    ///used for File and Instrument Date
                case ControlvalidationType.SpecialDate:
                    DateTime.TryParse(this.Text.Trim(), out this.dateTextBoxValue);
                    break;
                case ControlvalidationType.Year:
                ////year valid type use numeric validation
                case ControlvalidationType.Numeric:
                    int.TryParse(this.Text.Trim(), out this.numericTextBoxValue);
                    break;
                case ControlvalidationType.Decimal:
                    string tempTextString = this.Text.Trim().Replace(this.specialCharacter, "");
                    Decimal.TryParse(tempTextString, System.Globalization.NumberStyles.Currency, null, out this.decimalTextBoxValue);
                    break;
                ////Added by Biju on 08/Feb/2010 to fix #5877
                case ControlvalidationType.Money:
                    string tempString = this.Text.Trim().Replace(this.specialCharacter, "");
                    Decimal.TryParse(tempString, System.Globalization.NumberStyles.Currency, null, out this.decimalTextBoxValue);
                    break;
                ////till here
            }
        }

        /// <summary>
        /// Checks the range.
        /// </summary>
        /// <param name="maxValue">The max value.</param>
        /// <param name="minValue">The min value.</param>
        /// <returns>boolean value</returns>
        private bool CheckRange(decimal maxValue, decimal minValue)
        {
            decimal outValue;
            int maximumValue;
            int minimumValue;
            if (((int)ControlvalidationType.Money == this.textBoxValidType || (int)ControlvalidationType.SmallMoney == this.textBoxValidType)
                && !string.IsNullOrEmpty(this.Text))
            {
                string moneyValue = this.Text;

                if (moneyValue.Contains("$"))
                {
                    moneyValue = moneyValue.Replace("$", "").Trim();
                }

                if (moneyValue.Contains(","))
                {
                    moneyValue = moneyValue.Replace(",", "").Trim();
                }

                if (moneyValue.Contains("-") || moneyValue.Contains("("))
                {
                    if (moneyValue.Contains("("))
                    {
                        moneyValue = moneyValue.Replace("(", "").Trim();
                    }

                    if (moneyValue.Contains(")"))
                    {
                        moneyValue = moneyValue.Replace(")", "").Trim();
                    }
                    ////Commented by Biju on 08/Feb/2010 to fix #5877
                    ////this.Text = this.Text.Replace("-", "").Trim();
                    ////Modified by Biju on 08/Feb/2010 to fix #5877
                    decimal.TryParse(moneyValue.Replace("-", "").Trim(), out outValue);
                    ////Commented by Biju on 08/Feb/2010 to fix #5877
                    ////outValue = decimal.Negate(outValue);
                   if (this.applyNegativeStandard)
                    {
                        this.Text = string.Concat("(", outValue.ToString(this.textCustomFormat), ")");
                        this.ForeColor = Color.FromArgb(128, 0, 0);
                    }
                    else
                    {
                        ////this.ForeColor=this.Text = 
                        this.Text = outValue.ToString(this.textCustomFormat);
                    }
                    ////maximumValue = decimal.Compare(outValue, maxValue);
                    minimumValue = decimal.Compare(outValue, minValue);
                    ////minimumValue = decimal.Compare(-10, -9);

                    this.Text = this.Text.Trim();
                    if (minimumValue < 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {

                    decimal.TryParse(moneyValue, out outValue);
                    ////// Added to fix the Issue in Parcel Sale tracking form's textboxes validation type 'Money'
                    if (outValue > 0 && this.textBoxValidType == (int)ControlvalidationType.Money && SetColorFlag)
                    {
                        this.ForeColor = Color.FromArgb(0, 128, 0);
                    }
                    else
                    {
                        this.ForeColor = this.defaultForeColor;
                    }

                    this.Text = outValue.ToString(this.textCustomFormat).Trim();
                    decimal.TryParse(this.Text, out outValue);
                    maximumValue = decimal.Compare(outValue, maxValue);
                    //// minimumValue = decimal.Compare(outValue, minValue);
                    if (maximumValue > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                this.Text = "0";
                return false;
            }
        }
        ////int.TryParse(this.Text,out textValue);            
    }

        #endregion

        #endregion
}

