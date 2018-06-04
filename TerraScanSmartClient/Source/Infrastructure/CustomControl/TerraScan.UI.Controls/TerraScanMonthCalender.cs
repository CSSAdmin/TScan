//--------------------------------------------------------------------------------------------
// <copyright file="TerraScanMonthCalender.cs" company="Congruent">
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
// 10 July 06		Thilak Raj  	    Created
//*********************************************************************************/
namespace TerraScan.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Custom Month Calender
    /// </summary>
    public partial class TerraScanMonthCalender : System.Windows.Forms.MonthCalendar
    {
        #region Variables

        /// <summary>
        /// boolean to check whether the focus removed from the month calender without any action
        /// </summary>
        private bool focusRemovedFrom;

        /// <summary>
        /// boolean to check whether to change the default date
        /// </summary>
        private bool applyDateChange;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerraScanMonthCalender"/> class.
        /// </summary>
        public TerraScanMonthCalender()
        {
            ////small money max date
            base.MaxDate = Convert.ToDateTime("6/6/2079");
            ////small money min date
            base.MinDate = Convert.ToDateTime("1/1/1900");
            this.DateChanged += new DateRangeEventHandler(this.TerraScanMonthCalender_DateChanged);
        }

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [focus removed from].
        /// </summary>
        /// <value><c>true</c> if [focus removed from]; otherwise, <c>false</c>.</value>
        public bool FocusRemovedFrom
        {
            get { return this.focusRemovedFrom; }
            set { this.focusRemovedFrom = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating - used to change the daterange from deault.
        /// </summary>
        /// <value><c>true</c> if [apply date change]; otherwise, <c>false</c>.</value>
        public bool ApplyDateChange
        {
            get { return this.applyDateChange; }
            set { this.applyDateChange = value; }
        }

        /// <summary>
        /// Gets or sets the maximum allowable date - set user selected date if applyDateChange is true else date is db - smallmoney max date.
        /// </summary>
        /// <value>Maximum Datetime</value>
        /// <returns>A DateTime representing the maximum allowable date. The default is 6/6/2079 - for small money.</returns>     
        public new DateTime MaxDate
        {
            get
            {
                return base.MaxDate;
            }

            set
            {
                ////used to change the default max date
                if (this.applyDateChange)
                {
                    base.MaxDate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the minimum allowable date - set user selected date if applyDateChange is true else date is db - smallmoney min date.
        /// </summary>
        /// <value>Maximum Datetime</value>
        /// <returns>A DateTime representing the minimum allowable date. The default is 1/1/1900 - for small money.</returns>     
        public new DateTime MinDate
        {
            get
            {
                return base.MinDate;
            }

            set
            {
                ////used to change the default min date
                if (this.applyDateChange)
                {
                    base.MinDate = value;
                }
            }
        }

        #endregion //// Properties 

        /// <summary>
        /// Sets a date as the currently selected date.
        /// </summary>
        /// <param name="date">The date to be selected.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than the minimum allowable date.-or- The value is greater than the maximum allowable date. </exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public new void SetDate(DateTime date)
        {
            ////check for date validation
            if (date < this.MinDate)
            {
                date = this.MinDate;
            }
            else if (date > this.MaxDate)
            {
                date = this.MaxDate;
            }

            base.SetDate(date);
        }

        /// <summary>
        /// Raises the paint event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Leave"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnLeave(EventArgs e)
        {
            this.Visible = false;
            base.OnLeave(e);
        }

        /// <summary>
        /// Handles the DateChanged event of the TerraScanMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void TerraScanMonthCalender_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.SetDate(Convert.ToDateTime(e.Start.Month.ToString() + "/" + e.Start.Day.ToString() + "/" + e.Start.Year.ToString()));
        }
    }
}
