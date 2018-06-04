//--------------------------------------------------------------------------------------------
// <copyright file="F9080.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Roll Year Management Form.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/



namespace D9080
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using TerraScan.UI.Controls;
    using System.Configuration;
    using Infrastructure.Interface;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.RollYearStep;
    using System.Threading;
 
    /// <summary>
    /// 9080
    /// </summary>
    [SmartPart]
    public partial class F9080 : BaseSmartPart
    {
        #region Member Variables

         /// <summary>
        /// Tax Roll Correction form2550Control Controller
        /// </summary>
        private F9080Controller form9080Control;

        /// <summary>
        /// formLabel Info
        /// </summary>
        private string[] formLabelInfo = new string[2];

        ///<summary>
        /// RollOverID
        /// </summary>
        private short rollOverID;

        /// <summary>
        /// rollYearDataset
        /// </summary>
        private F9080RollYearManagementData rollYearDataset = new F9080RollYearManagementData();

        /// <summary>
        /// stepDataset
        /// </summary>
        private F9080RollYearManagementData stepDataset = new F9080RollYearManagementData();

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;
        public bool Closeform = false;
        private RollYearStepUserControl RollYearInterimControl;
        public delegate void RunStepEventHandler(bool Complete, int Currentstep);
        public event RunStepEventHandler RunStepHandler;

        private bool Buttonclick = false;

        #endregion member variables


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F2550"/> class.
        /// </summary>
        public F9080()
        {
            InitializeComponent();
            this.ParcelPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.formPanel.Height-4, this.ParcelPictureBox.Width, "Roll Year Management", 28, 81, 128);
            //this.FormClose += new EventHandler<DataEventArgs<string>>(F9080_FormClose);
           // this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F1031 control.
        /// </summary>
        /// <value>The F1031 control.</value>
        [CreateNew]
        public F9080Controller Form9080Control
        {
            get { return this.form9080Control as F9080Controller; }
            set { this.form9080Control = value; }
        }

 
        #endregion properties

        #region Event Publication

        /// <summary>
        /// event publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;


        #endregion Event Publication

        private void F9080_FormClose(object sender, DataEventArgs<string> e)
        {
            if (!this.Buttonclick)
            {
                DialogResult result = MessageBox.Show("You Cant Close the Form Before the Operation Completes", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Question);
                {
                    this.Closeform = true;
                    return;
                }
            }
            else
            {
                //this.isClose = true;
                this.Closeform = false;
                //int[] tempArgs = new int[0];
                //this.D2550_F2551_FormClose(this, new DataEventArgs<int[]>(tempArgs));
            }
              
        }
        /// <summary>
        /// Handles the FormClosing event of the ParentForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        public void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Buttonclick)
            {
                //e.Cancel = true;
               // MessageBox.Show("Do you want to Save the changes to Edit Statement?", "TerraScan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                e.Cancel = true;
            }
        }
        #region LoadWorkSpaces

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form9080Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form9080Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form9080Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }
            this.formLabelInfo[0] = "Roll Year Management";
            this.formLabelInfo[1] = string.Empty;
            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
            if (this.form9080Control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
            {
                this.FooterWorkspace.Show(this.form9080Control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
            }
            else
            {
                this.FooterWorkspace.Show(this.form9080Control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
            }

            this.footerSmartPart = (FooterSmartPart)this.form9080Control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
            this.footerSmartPart.ParentWorkItem = this.form9080Control.WorkItem;
            this.footerSmartPart.FormId = "9080";
            this.footerSmartPart.AuditLinkText = "";
            this.footerSmartPart.VisibleHelpButton = false;

            this.footerSmartPart.VisibleHelpLinkButton = true;

            this.footerSmartPart.TabStop = true;
            foreach (UserControl ctrl in this.FooterWorkspace.SmartParts)
            {
                if (ctrl != null)
                {
                    ctrl.TabStop = true;
                }
            }


        }

        #endregion

        private void RollYearPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CompletePanel_Paint(object sender, PaintEventArgs e)
        {


        }

        #region Page Load

        /// <summary>
        /// Handles the Load event of the F2550 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>

        private void F9080_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpaces();
                this.rollYearDataset = this.form9080Control.WorkItem.F9080_ListRollYearManagement(TerraScanCommon.UserId);
                this.RollYearCombobox.DataSource = this.rollYearDataset.ListRollYearManagement;
                this.RollYearCombobox.ValueMember = this.rollYearDataset.ListRollYearManagement.RollYearColumn.ColumnName;
                this.RollYearCombobox.DisplayMember = this.rollYearDataset.ListRollYearManagement.RollYearColumn.ColumnName;
                if (this.rollYearDataset.ListRollYearManagement.Rows.Count > 0)
                {
                    int rollYear;
                    int.TryParse(this.rollYearDataset.ListRollYearManagement.Rows[0][0].ToString(), out rollYear);
                    this.RollYearCombobox.SelectedValue = rollYear;
                    short stepYear;
                    short.TryParse(this.RollYearCombobox.Text.ToString(), out stepYear);
                    this.stepDataset = this.form9080Control.WorkItem.F9080_GetRollYearManagement(stepYear, TerraScanCommon.UserId);
                    this.UserControlPanel1.BringToFront();
                    this.vScrollBar1.BringToFront();
                    this.PopulateStepDataset();
                    this.CompleteTextBox.Text = this.rollYearDataset.ListRollYearManagement.Rows[0]["LastStepCompleted"].ToString();
                    this.DateTextBox.Text = this.rollYearDataset.ListRollYearManagement.Rows[0]["LastStepRunDate"].ToString();
                }
                this.FooterWorkspace.BringToFront();
                this.formPanel.Height = this.Height-168;
                this.FooterWorkspace.Height = 33;
                //this.FooterWorkspace.Width = this.Width+ 350; 
                this.FooterWorkspace.Top = this.Height-75;
                //this.ParcelPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(800, this.ParcelPictureBox.Width, "Roll Year Management", 28, 81, 128); 
                ////this.UserControlPanel.Height = this.formPanel.Height - 100;
                this.ParcelPictureBox.Top = this.Height - (this.formPanel.Height + this.StepPanel.Height + this.CompletePanel.Height + this.StepPanel.Height + this.CompletePanel.Height);
                this.ParcelPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.formPanel.Height - 4, this.ParcelPictureBox.Width, "Roll Year Management", 28, 81, 128); 
                //this.ParcelPictureBox.Height = this.Height-14;
                //this.ParcelPictureBox.SendToBack();  
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.UserControlPanel1.SendToBack();
                this.vScrollBar1.BringToFront();
            }
        }


        #endregion Page Load

        /// <summary>
        /// used to RollYearCombobox
        /// </summary>
        private void RollYearCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.RollYearCombobox.Text))
            {
                //short rollYear;
                //short.TryParse(this.RollYearCombobox.Text.ToString(), out rollYear);
                //this.rollYearDataset = this.form9080Control.WorkItem.F9080_GetRollYearManagement(rollYear, TerraScanCommon.UserId);
            }



        }

        private void RollYearCombobox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.UserControlPanel1.BringToFront();
                short stepYear;
                short.TryParse(this.RollYearCombobox.Text.ToString(), out stepYear);
                this.stepDataset = this.form9080Control.WorkItem.F9080_GetRollYearManagement(stepYear, TerraScanCommon.UserId);
                if (this.stepDataset.GetRollYearManagement.Rows.Count > 0)
                {

                    
                    this.vScrollBar1.BringToFront();
                    this.PopulateStepDataset();
                    this.LastCompleteMethod();

                }
                else
                {
                    if (this.rollYearDataset.ListRollYearManagement.Rows.Count > 0)
                    {
                        DataRow[] rowYear = this.rollYearDataset.ListRollYearManagement.Select("RollYear=" + stepYear);
                        if (rowYear.Length > 0)
                        {
                            this.RollYearCombobox.Text = stepYear.ToString();
                            this.CompleteTextBox.Text = rowYear[0].ItemArray[1].ToString();
                            if (string.IsNullOrEmpty(rowYear[0].ItemArray[2].ToString()))
                            {
                                this.DateTextBox.Text = string.Empty;
                            }
                            else
                            {
                                this.DateTextBox.Text = rowYear[0].ItemArray[2].ToString();
                            }


                        }
                    }
                    else
                    {
                        this.RollYearCombobox.Text = string.Empty;
                        this.CompleteTextBox.Text = string.Empty;
                        this.DateTextBox.Text = string.Empty;
                    }
                    this.UserControlPanel.Controls.Clear();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            //this.UserControlPanel.BringToFront();
            finally
            {
                this.UserControlPanel1.SendToBack();
                this.vScrollBar1.BringToFront();
            }


        }


        /// <summary>
        /// Handles the StepClickEventHandler event of the withPoints control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private  void rollYearControl_StepButtonHandler(object sender, short rollOverId, RollYearStepUserControl tempControl)
        {
            try
            {
                this.RollYearInterimControl = tempControl;
                this.rollOverID = rollOverId;  
                this.StepTimer.Tick +=new EventHandler(StepTimer_Tick);
                this.StepTimer.Start();

                this.Closeform = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// Scrolls the bar visibility.
        /// </summary>
        private void ScrollBarVisibility()
        {
            int count = this.stepDataset.GetRollYearManagement.Rows.Count;
            int userControlValue = count * 97;
            if(userControlValue>this.UserControlPanel.Height ) 
            //if (this.stepDataset.GetRollYearManagement.Rows.Count > 6)
            {
                this.vScrollBar1.Visible = false;  
            }
            else
            {
                this.vScrollBar1.Visible = true; 
            }

        }

        private void PopulateStepDataset()
        {
            if (this.stepDataset.GetRollYearManagement.Rows.Count > 0)
            {
                this.UserControlPanel.Controls.Clear();
                int Yaxis = 0;
                for (int i = 0; i < this.stepDataset.GetRollYearManagement.Rows.Count; i++)
                {
                    F9080RollYearManagementData.GetRollYearManagementRow GetRollYearRow = (F9080RollYearManagementData.GetRollYearManagementRow)this.stepDataset.GetRollYearManagement.Rows[i];
                    RollYearStepUserControl rollYearControl = new RollYearStepUserControl();
                    //rollYearControl = new RollYearStepUserControl();
                    rollYearControl.Name = "RollYearUserControl" + i;  
                    int IsCurrentStep;
                    int.TryParse(this.stepDataset.GetRollYearManagement.Rows[i]["IsCurrentStep"].ToString(), out IsCurrentStep);
                    if (IsCurrentStep > 0)
                    {
                        rollYearControl.CurrentStep = true;
                    }
                    else
                    {
                        rollYearControl.CurrentStep = false;
                    }
                    //rollYearControl.CurrentStep = IsCurrentStep;
                    int IsComplete;
                    int.TryParse(this.stepDataset.GetRollYearManagement.Rows[i]["IsComplete"].ToString(), out IsComplete);
                    if (this.stepDataset.GetRollYearManagement.Rows[i]["IsComplete"].Equals(true))
                    {
                        rollYearControl.Complete = true;
                    }
                    else
                    {
                        rollYearControl.Complete = false;
                    }
                    
                    rollYearControl.StepNumbertextbox = this.stepDataset.GetRollYearManagement.Rows[i]["Step"].ToString();
                    rollYearControl.StepDescriptiontextbox = this.stepDataset.GetRollYearManagement.Rows[i]["Description"].ToString();
                    rollYearControl.StepRunDatetextbox = this.stepDataset.GetRollYearManagement.Rows[i]["RunDate"].ToString();
                    rollYearControl.StepRunBytextbox = this.stepDataset.GetRollYearManagement.Rows[i]["RunBy"].ToString();
                    short rollOver;
                    short.TryParse(this.stepDataset.GetRollYearManagement.Rows[i]["RollOverID"].ToString(), out rollOver);
                    rollYearControl.RollOverID = rollOver;
                    rollYearControl.WarningMsg = this.stepDataset.GetRollYearManagement.Rows[i]["WarningText"].ToString();      
                    rollYearControl.StepButtonHandler += new RollYearStepUserControl.StepClickEventHandler(rollYearControl_StepButtonHandler);
                    this.UserControlPanel.Controls.Add(rollYearControl);
                    rollYearControl.Location = new System.Drawing.Point(-1, Yaxis);
                    Yaxis = Yaxis + rollYearControl.Height - 1;
                }

            }
            this.ScrollBarVisibility(); 
        }

        private void StepTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.Closeform)
                {
                    DataRow[] clickbutton = this.stepDataset.GetRollYearManagement.Select("RollOverId=" + this.rollOverID);
                    if (clickbutton.Length > 0)
                    {
                        //int CONTROL = this.form9080Control.WorkItem.F9080_ExecRollYearStep(this.rollOverID, TerraScanCommon.UserId);
                        string returnMessage = this.form9080Control.WorkItem.F9080_ExecRollYearStep(this.rollOverID, TerraScanCommon.UserId);

                        //if (CONTROL.Equals(1))
                        if (returnMessage != null && !string.IsNullOrEmpty(returnMessage))
                        {
                            this.StepTimer.Stop();
                            MessageBox.Show(returnMessage, "TerraScan – Roll Over Step Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            this.StepTimer.Stop();
                        }
                                DateTime dt = DateTime.Now;
                                String.Format("{0:d}", dt);
                                string date = dt.ToString();
                                string user = TerraScanCommon.UserName;
                                String.Format("{0:M/d/yyyy}", dt);
                                //RollYearInterimControl.StepRunDatetextbox  
                                RollYearInterimControl.StepRunBytextbox = user;
                                RollYearInterimControl.StepRunDatetextbox = dt.ToShortDateString();
                                RollYearInterimControl.StepButtontext = "Step Complete";
                                this.LastCompleteMethod();
                                //this.StepTimer.Stop();
                                short stepYear;
                                int UserControlCount;
                                int.TryParse(RollYearInterimControl.Name.Replace("RollYearUserControl", "").Trim(), out UserControlCount);
                                if (this.stepDataset.GetRollYearManagement.Rows.Count > UserControlCount + 1)
                                {

                                    int Count = UserControlCount + 1;
                                    //RollYearStepUserControl sa = new RollYearStepUserControl();
                                    string sas = "RollYearUserControl" + Count;

                                    //  RollYearInterimControl  = sas;

                                    Control[] ctrl = this.Controls.Find(sas, true);

                                    ((TerraScan.RollYearStep.RollYearStepUserControl)(ctrl[0])).CurrentStep = true;

                                }

                                short.TryParse(this.RollYearCombobox.Text.ToString(), out stepYear);
                                this.stepDataset = this.form9080Control.WorkItem.F9080_GetRollYearManagement(stepYear, TerraScanCommon.UserId);
                                //this.PopulateStepDataset();

                                for (int i = 0; i < this.stepDataset.GetRollYearManagement.Rows.Count; i++)
                                {
                                    //F9080RollYearManagementData.GetRollYearManagementRow GetRollYearRow = (F9080RollYearManagementData.GetRollYearManagementRow)this.stepDataset.GetRollYearManagement.Rows[i];
                                    //RollYearStepUserControl rollYearControl = new RollYearStepUserControl();

                                   // int Count = UserControlCount + 1;
                                    //RollYearStepUserControl sa = new RollYearStepUserControl();
                                    string sas = "RollYearUserControl" + i;
                                    Control[] ctrl = this.Controls.Find(sas, true);
                                    RollYearStepUserControl rollYearControl = ((TerraScan.RollYearStep.RollYearStepUserControl)(ctrl[0]));
                                   
                                    rollYearControl.Name = "RollYearUserControl" + i;
                                    int IsCurrentStep;
                                    int.TryParse(this.stepDataset.GetRollYearManagement.Rows[i]["IsCurrentStep"].ToString(), out IsCurrentStep);
                                    if (IsCurrentStep > 0)
                                    {
                                        rollYearControl.CurrentStep = true;
                                    }
                                    else
                                    {
                                        rollYearControl.CurrentStep = false;
                                    }
                                    //rollYearControl.CurrentStep = IsCurrentStep;
                                    int IsComplete;
                                    int.TryParse(this.stepDataset.GetRollYearManagement.Rows[i]["IsComplete"].ToString(), out IsComplete);
                                    if (this.stepDataset.GetRollYearManagement.Rows[i]["IsComplete"].Equals(true))
                                    {
                                        rollYearControl.Complete = true;
                                    }
                                    else
                                    {
                                        rollYearControl.Complete = false;
                                    }

                                    rollYearControl.StepNumbertextbox = this.stepDataset.GetRollYearManagement.Rows[i]["Step"].ToString();
                                    rollYearControl.StepDescriptiontextbox = this.stepDataset.GetRollYearManagement.Rows[i]["Description"].ToString();
                                    rollYearControl.StepRunDatetextbox = this.stepDataset.GetRollYearManagement.Rows[i]["RunDate"].ToString();
                                    rollYearControl.StepRunBytextbox = this.stepDataset.GetRollYearManagement.Rows[i]["RunBy"].ToString();
                                    short rollOver;
                                    short.TryParse(this.stepDataset.GetRollYearManagement.Rows[i]["RollOverID"].ToString(), out rollOver);
                                    rollYearControl.RollOverID = rollOver;
                                    rollYearControl.WarningMsg = this.stepDataset.GetRollYearManagement.Rows[i]["WarningText"].ToString();
                                }

                              //  this.StepTimer.Stop();   
                                this.Closeform = false;
                            
                        //    else
                        //    {
                        //        this.StepTimer.Stop();
                        //        this.Closeform = false;
                        //    }
                        
                      
                    }
                    else
                    {
                        this.StepTimer.Stop();
                        this.Closeform = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.StepTimer.Stop();
                this.Closeform = false;
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void LastCompleteMethod()
        {
            short stepYear;
            short.TryParse(this.RollYearCombobox.Text.ToString(), out stepYear);
            this.rollYearDataset = this.form9080Control.WorkItem.F9080_ListRollYearManagement(TerraScanCommon.UserId);
            this.RollYearCombobox.DataSource = this.rollYearDataset.ListRollYearManagement;
            this.RollYearCombobox.ValueMember = this.rollYearDataset.ListRollYearManagement.RollYearColumn.ColumnName;
            this.RollYearCombobox.DisplayMember = this.rollYearDataset.ListRollYearManagement.RollYearColumn.ColumnName;
            DataRow[] rowYear = this.rollYearDataset.ListRollYearManagement.Select("RollYear=" + stepYear);
            if (rowYear.Length > 0)
            {
                this.RollYearCombobox.Text = stepYear.ToString() ;
                this.CompleteTextBox.Text = rowYear[0].ItemArray[1].ToString() ;
                if (string.IsNullOrEmpty(rowYear[0].ItemArray[2].ToString()))
                {
                    this.DateTextBox.Text = string.Empty;
                }
                else
                {
                    this.DateTextBox.Text = rowYear[0].ItemArray[2].ToString();
                }
                

            }
        }

        private void F9080_Resize(object sender, EventArgs e)
        {
            try
            {
                this.ScrollBarVisibility();
                this.ParcelPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.formPanel.Height - 4, this.ParcelPictureBox.Width, "Roll Year Management", 28, 81, 128);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        
    }
}
