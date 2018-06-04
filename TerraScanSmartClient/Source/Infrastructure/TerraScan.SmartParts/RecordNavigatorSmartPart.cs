//--------------------------------------------------------------------------------------------
// <copyright file="RecordNavigatorSmartPart.cs" company="Congruent">
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

namespace TerraScan.SmartParts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Common;
    using TerraScan.Utilities;

    /// <summary>
    /// RecordNavigatorSmartPart class file
    /// </summary>
    [SmartPart]
    public partial class RecordNavigatorSmartPart : PrimaryBaseSmartPart
    {
        /// <summary>
        /// sets focus to the prev or next button
        /// </summary>
        private bool setFocus;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RecordNavigatorSmartPart"/> class.
        /// </summary>
        public RecordNavigatorSmartPart()
        {
            this.InitializeComponent();
            this.LeftMenuItem.Click += new EventHandler(this.NavigationButton_Click);
            this.RightMenuItem.Click += new EventHandler(this.NavigationButton_Click);
            this.LastMenuItem.Click += new EventHandler(this.NavigationButton_Click);
            this.FirstMenuItem.Click += new EventHandler(this.NavigationButton_Click);
        }

        #endregion Constructor               

        #region Event Publication

        /// <summary>
        /// Event Publication for CheckPageStatus
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> CheckPageStatus;

        /// <summary>
        /// Event Publication for DisplayNavigatedRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<RecordNavigationEntity>> DisplayNavigatedRecord;

        #endregion Event Publication

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [set focus].
        /// </summary>
        /// <value><c>true</c> if [set focus]; otherwise, <c>false</c>.</value>
        public bool SetFocus
        {
            get
            {
                return this.setFocus;
            }

            set
            {
                if (value)
                {
                    this.SetFocusToControl();
                }

                this.setFocus = value;
            }
        }

        #endregion Properties       

        #region EventSubscription

        /// <summary>
        /// Sets the record count.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, Thread = ThreadOption.UserInterface)]
        public void SetRecordCount(object sender, DataEventArgs<int> e)
        {
            ////this.RecordCountLabel.Text = string.Concat(" of ", e.Data);
            this.RecordCountLabel.Text = e.Data.ToString();
        }

        /// <summary>
        /// Sets the active record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, Thread = ThreadOption.UserInterface)]
        public void SetActiveRecord(object sender, DataEventArgs<int> e)
        {
            this.RecordIDLabel.Text = e.Data.ToString();
        }

        /// <summary>
        /// Sets the active record buttons.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, Thread = ThreadOption.UserInterface)]
        public void SetActiveRecordButtons(object sender, DataEventArgs<int[]> e)
        {
            if (e.Data[0] > 0 && e.Data[0] <= e.Data[1])
            {
                ////check for the first record
                if (e.Data[0] == 1)
                {
                    this.PreviousButton.Enabled = false;
                    this.FirstButton.Enabled = false;
                }
                else
                {
                    this.PreviousButton.Enabled = true;
                    this.FirstButton.Enabled = true;
                }

                ////check for the first record
                if (e.Data[0] == e.Data[1])
                {
                    this.NextButton.Enabled = false;
                    this.LastButton.Enabled = false;
                }
                else
                {
                    this.NextButton.Enabled = true;
                    this.LastButton.Enabled = true;
                }
            }
            else
            {
                this.NextButton.Enabled = false;
                this.LastButton.Enabled = false;
                this.PreviousButton.Enabled = false;
                this.FirstButton.Enabled = false;
            }
        }

        /// <summary>
        /// Pages the status activated.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, Thread = ThreadOption.UserInterface)]
        public void PageStatusActivated(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                Control sourceControl = e.Data;
                string sourceTagName = Convert.ToString(sourceControl.Tag).ToUpper();
                bool flag = false;
                ////true - fetch next available record if the current record is not available,false - fetch previous record
                bool fetchNextRecord = false;
                int recordIndex = 0;

                switch (sourceTagName)
                {
                    case "FIRST":
                        if (Convert.ToInt32(this.RecordCountLabel.Text) > 0)
                        {
                            recordIndex = 1;
                            flag = true;
                            fetchNextRecord = true;
                        }

                        break;
                    case "LAST":
                        if (Convert.ToInt64(this.RecordCountLabel.Text) > 0)
                        {
                            recordIndex = Convert.ToInt32(this.RecordCountLabel.Text);
                            flag = true;
                            fetchNextRecord = false;
                        }

                        break;
                    case "PREVIOUS":
                        int.TryParse(this.RecordIDLabel.Text, out recordIndex);
                        if (recordIndex > 1)
                        {
                            recordIndex -= 1;
                            flag = true;
                            fetchNextRecord = false;
                        }

                        break;
                    case "NEXT":
                        int.TryParse(this.RecordIDLabel.Text, out recordIndex);
                        if (recordIndex < Convert.ToInt32(this.RecordCountLabel.Text))
                        {
                            recordIndex += 1;
                            flag = true;
                            fetchNextRecord = true;
                        }

                        break;
                }

                this.DisplayNavigatedRecord(this, new DataEventArgs<RecordNavigationEntity>(new RecordNavigationEntity(fetchNextRecord, recordIndex)));
                if (flag)
                {
                    if (sourceControl.Enabled)
                    {
                        sourceControl.Focus();
                    }
                    else
                    {
                        if (sourceControl.Equals(this.LastButton) || sourceControl.Equals(this.NextButton))
                        {
                            if (this.PreviousButton.Enabled)
                            {
                                this.PreviousButton.Focus();
                            }
                        }
                        else
                        {
                            if (this.NextButton.Enabled)
                            {
                                this.NextButton.Focus();
                            }
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion EventSubscription     

        /// <summary>
        /// Handles the Click event of the NavigationButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button)
                {
                    this.CheckPageStatus(this, new DataEventArgs<Button>(sender as Button));
                }
                else
                {
                    if (this.Visible)
                    {
                        if ((((ToolStripDropDownItem)sender).Tag.ToString() == "PREVIOUS") && this.PreviousButton.Enabled)
                        {
                            this.CheckPageStatus(this, new DataEventArgs<Button>(this.PreviousButton));
                        }
                        else if ((((ToolStripDropDownItem)sender).Tag.ToString() == "NEXT") && this.NextButton.Enabled)
                        {
                            this.CheckPageStatus(this, new DataEventArgs<Button>(this.NextButton));
                        }
                        else if ((((ToolStripDropDownItem)sender).Tag.ToString() == "FIRST") && this.FirstButton.Enabled)
                        {
                            this.CheckPageStatus(this, new DataEventArgs<Button>(this.FirstButton));
                        }
                        else if ((((ToolStripDropDownItem)sender).Tag.ToString() == "LAST") && this.LastButton.Enabled)
                        {
                            this.CheckPageStatus(this, new DataEventArgs<Button>(this.LastButton));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the focus to control.
        /// </summary>
        private void SetFocusToControl()
        {
            if (this.PreviousButton.Enabled)
            {
                this.ActiveControl = this.PreviousButton;
            }
            else if (this.NextButton.Enabled)
            {
                this.ActiveControl = this.NextButton;
            }
        }

        /// <summary>
        /// Handles the Load event of the RecordNavigatorSmartPart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RecordNavigatorSmartPart_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Parent != null && this.Parent.Parent != null && this.Parent.Parent.GetType().FullName == SharedFunctions.GetResourceString("RecordNavigatorValidation"))
                {
                    this.Parent.Parent.MouseWheel += new MouseEventHandler(Parent_MouseWheel);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseWheel event of the Parent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void Parent_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.Visible)
                {
                    if ((e.Delta < 0) && this.PreviousButton.Enabled)
                    {
                        this.CheckPageStatus(this, new DataEventArgs<Button>(this.PreviousButton));
                    }
                    else if ((e.Delta > 0) && this.NextButton.Enabled)
                    {
                        this.CheckPageStatus(this, new DataEventArgs<Button>(this.NextButton));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
