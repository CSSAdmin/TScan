//--------------------------------------------------------------------------------------------
// <copyright file="PaymentEngineColumnWidth.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the PaymentEngineColumnWidth.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------                            	   
// 05 Feb 07        ranjani            created
//*********************************************************************************/
namespace TerraScan.PaymentEngine
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;

    #region Delegetes

    /// <summary>
    /// Deelegate delc
    /// </summary>
    /// <param name="propertyValue">propertyValue</param>
    public delegate void PropertyChangedEventHandler(int propertyValue);

    #endregion

    /// <summary>
    /// PaymentEngineColumnWidth.
    /// </summary>   
    [TypeConverter(typeof(ColumnWidthConverter))]
    public class PaymentEngineColumnWidth
    {
        #region Member Variables

        /// <summary>
        /// used to set amount width
        /// </summary>
        private int amountWidth;

        /// <summary>
        /// used to set amount width
        /// </summary>
        private int tenderTypeWidth;

        /// <summary>
        /// used to set amount width
        /// </summary>
        private int paidByWidth;

        /// <summary>
        /// used to set amount width
        /// </summary>
        private int checkNumberWidth;

        /// <summary>
        /// used to set amount width
        /// </summary>
        private int pidWidth;

        /// <summary>
        /// used to set amount width
        /// </summary>
        private int ppidWidth;

        /// <summary>
        /// used to set amount width
        /// </summary>
        private int paidByImageWidth;

        #endregion     

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PaymentEngineColumnWidth"/> class.
        /// </summary>
        public PaymentEngineColumnWidth()
        {
            this.tenderTypeWidth = 105;
            this.paidByWidth = 299;
            this.amountWidth = 100;
            this.checkNumberWidth = 67;
            this.ppidWidth = 65;
            this.pidWidth = 75;
            this.paidByImageWidth = 25;

        }

        #endregion Constructors   
   
        #region Events

        /// <summary>
        /// PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Property

        /// <summary>
        /// This variable is used to maintain the name value.
        /// </summary>
        public int AmountWidth
        {
            get
            {
                return this.amountWidth;
            }

            set
            {
                if (this.PropertyChanged != null)
                {
                    this.amountWidth = value;
                    this.PropertyChanged(this.amountWidth);
                }
            }
        }

        /// <summary>
        /// This variable is used to maintain the countrycode value.
        /// </summary>
        public int TenderTypeWidth
        {
            get
            {
                return this.tenderTypeWidth;
            }

            set
            {
                if (this.PropertyChanged != null)
                {
                    this.tenderTypeWidth = value;
                    this.PropertyChanged(this.tenderTypeWidth);
                }
            }
        }

        /// <summary>
        /// This variable is used to maintain the areacode value.
        /// </summary>
        public int PaidByWidth
        {
            get
            {
                return this.paidByWidth;
            }

            set
            {
                if (this.PropertyChanged != null)
                {
                    this.paidByWidth = value;
                    this.PropertyChanged(this.paidByWidth);
                }
            }
        }

        public int PaidByImageWidth
        {
            get
            {
                return this.paidByImageWidth;
            }

            set
            {
                if (this.PropertyChanged != null)
                {
                    this.paidByImageWidth = value;
                    this.PropertyChanged(this.paidByImageWidth);
                }
            }
        }
        /// <summary>
        /// This variable is used to maintain the phonenumber value. 
        /// </summary>
        public int CheckNumberWidth
        {
            get
            {
                return this.checkNumberWidth;
            }

            set
            {
                if (this.PropertyChanged != null)
                {
                    this.checkNumberWidth = value;
                    this.PropertyChanged(this.checkNumberWidth);
                }
            }
        }

        /// <summary>
        /// This variable is used to maintain the phonenumber value.     
        /// </summary>
        public int PIDWidth
        {
            get
            {
                return this.pidWidth;
            }

            set
            {
                if (this.PropertyChanged != null)
                {
                    this.pidWidth = value;
                    this.PropertyChanged(this.pidWidth);
                }
            }
        }

        /// <summary>
        /// This variable is used to maintain the phonenumber value.      
        /// </summary>
        public int PPIDWidth
        {
            get
            {
                return this.ppidWidth;
            }

            set
            {
                if (this.PropertyChanged != null)
                {
                    this.ppidWidth = value;
                    this.PropertyChanged(this.ppidWidth);
                }
            }
        }

        #endregion Property        

        #region Overrides

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return string.Empty;
        }

        #endregion Constructors
    }   

    /// <summary>
    /// ColumnWidthConverter.
    /// </summary>
    public class ColumnWidthConverter : TypeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ColumnWidthConverter"/> class.
        /// </summary>
        public ColumnWidthConverter()
        {
        }

        /// <summary>
        /// allows us to display the + symbol near the property name
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context.</param>
        /// <returns>the boolean</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context.</param>
        /// <param name="value">An <see cref="T:System.Object"></see> that specifies the type of array for which to get properties.</param>
        /// <param name="attributes">An array of type <see cref="T:System.Attribute"></see> that is used as a filter.</param>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"></see> with the properties that are exposed for this data type, or null if there are no properties.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(PaymentEngineColumnWidth));
        }
    }
}
