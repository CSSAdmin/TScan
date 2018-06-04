//--------------------------------------------------------------------------------------------
// <copyright file="PaymentEngineUserControl.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Login.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22 Nov 2011      Manoj Kumar P        	    Created
//-----------------------------------------------------------------------------------------------


namespace TerraScan.RollYearStep
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Helper;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.ObjectBuilder;
    using System.Collections;
    using System.Threading;


    /// <summary>
    /// Usercontrol for Roll Year Steps
    /// </summary>
    public partial class RollYearStepUserControl : UserControl
    {
        public struct parameters
        {
            public static int ROllOverId;
            public static bool isCurrent;
            public static bool isComplete;
        }

        #region Member Variables

        /// <summary>
        /// Display Step Number label text
        /// </summary>
        string StepNumberText = string.Empty;

        /// <summary>
        /// Display Step Description label text
        /// </summary>
        string StepDescriptionText = string.Empty;


        /// <summary>
        /// Display Step Run Date label text
        /// </summary>
        string StepRunDateText = string.Empty;
        string StepButtonText = string.Empty;
        /// <summary>
        /// Display Step Run By label text
        /// </summary>
        string StepRunByText = string.Empty;
        /// <summary>
        /// Display Warning Message By label text
        /// </summary>
        string warningMsg = string.Empty;

        bool IsCurrentstep = false;

        bool IsComplete = false;

       

        short rollOverId;

        /// <summary>
        /// formLabel Info
        /// </summary>
        private string[] formLabelInfo = new string[2];

        public delegate void StepClickEventHandler(object sender, short RollOverId, RollYearStepUserControl tempControl);
        public event StepClickEventHandler StepButtonHandler;

        /// <summary>
        /// used for the background worker
        /// </summary>
        private BackgroundWorker bw = new BackgroundWorker();

        private BackgroundWorker rollYearWork = new BackgroundWorker();

        /// <summary>
        /// formLabel Info
        /// </summary>
        private string[] LabelInfo = new string[2];


        #endregion Member Variables


        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RollYearStepUserControl"/> class.
        /// </summary>
        public RollYearStepUserControl()
        {
            this.InitializeComponent();
        }
        #endregion Constructors

        #region properties

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public string WarningMsg
        {
            get
            {
                return this.warningMsg;
            }
            set
            {
                this.warningMsg = value;
            }
        }


        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public bool CurrentStep
        {
            get
            {
                return this.IsCurrentstep;
            }

            set
            {
                this.IsCurrentstep = value;
                if (this.IsCurrentstep)
                {
                    this.StepButton.Text = "Run This Step";
                    this.StepButton.Enabled = true;
                    this.StepButton.BackColor = Color.FromArgb(28, 81, 128);
                    this.StepButtonpanel.BackColor = Color.FromArgb(191, 191, 191);
                    this.StepNumberTextBox.ForeColor = Color.FromArgb(255, 0, 0);
                }
            }
        }

      


        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public short RollOverID
        {
            get
            {
                return this.rollOverId;
            }

            set
            {
                this.rollOverId = value;

            }
        }

        // <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public bool Complete
        {
            get
            {
                return this.IsComplete;
            }

            set
            {
                this.IsComplete = value;
                if (this.IsComplete)
                {
                    this.StepButton.Text = "Step Complete";
                    this.StepButton.Enabled = false;
                    this.StepButton.BackColor = Color.FromArgb(128, 128, 128);
                    this.StepButtonpanel.BackColor = Color.FromArgb(64, 64, 64);
                }
                else if (this.IsCurrentstep)
                {
                    this.StepButton.Text = "Run This Step";
                    this.StepButton.Enabled = true;
                    this.StepButton.BackColor = Color.FromArgb(28, 81, 128);
                    this.StepButtonpanel.BackColor = Color.FromArgb(191, 191, 191);
                }
                else
                {
                    this.StepButton.Text = "";
                    this.StepButton.Enabled = false;
                    this.StepButton.BackColor = Color.FromArgb(28, 81, 128);
                }



            }
        }


        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public string StepNumbertextbox
        {
            get
            {
                return this.StepDescriptionText;
            }

            set
            {
                this.StepDescriptionText = value;
                {
                    if (this.IsCurrentstep && !this.IsComplete)
                    {
                        this.StepNumberTextBox.ForeColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        this.StepNumberTextBox.ForeColor = Color.FromArgb(0, 0, 0);
                    }
                    this.StepNumberTextBox.Text = this.StepDescriptionText;
                }
            }
        }


        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public string StepDescriptiontextbox
        {
            get
            {
                return this.StepDescriptionText;
            }

            set
            {
                this.StepDescriptionText = value;
                {
                    this.DescriptionTextBox.Text = this.StepDescriptionText;
                }
            }
        }


        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public string StepRunDatetextbox
        {
            get
            {
                return this.StepRunDateText;
            }

            set
            {
                this.StepRunDateText = value;
                {
                    this.RunDateTextBox.Text = this.StepRunDateText;
                }
            }
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public string StepButtontext
        {
            get
            {
                return this.StepButtonText;
            }

            set
            {
                this.StepButtonText = value;
                {
                    this.StepButton.Text = this.StepButtonText;
                    if (this.StepButton.Text.Equals("Step Complete"))
                    {
                        this.StepButton.Enabled = false;
                        this.StepButton.BackColor = Color.FromArgb(128, 128, 128);
                        this.StepButtonpanel.BackColor = Color.FromArgb(64, 64, 64);
                        this.StepNumberTextBox.ForeColor = Color.FromArgb(0, 0, 0);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public string StepRunBytextbox
        {
            get
            {
                return this.StepRunByText;
            }

            set
            {
                this.StepRunByText = value;
                {
                    this.StepRunByTextBox.Text = this.StepRunByText;
                }
            }
        }



        #endregion properties


        private void RollYearStepUserControl_Load(object sender, EventArgs e)
        {

        }

        public void StepButton_Click(object sender, EventArgs e)
        {
            if (StepButtonHandler != null)
            {
                if (MessageBox.Show(WarningMsg, "TerraScan T2", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == (DialogResult.Yes))
                {
                    //rollYearWork.WorkerReportsProgress = true;
                    //this.rollYearWork.DoWork += new DoWorkEventHandler(rollYearWork_DoWork);
                    //this.rollYearWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(rollYearWork_RunWorkerCompleted);
                    //this.rollYearWork.RunWorkerAsync();
                    
                    this.StepButton.Text = "Step Running...";
                    this.StepButton.BackColor = Color.FromArgb(38, 38, 38);
                    this.StepButtonpanel.BackColor = Color.FromArgb(115, 115, 115);  
                    short rolloverId = RollOverID;
                    string warningmsg = WarningMsg;
                    this.StepButtonHandler(sender, rolloverId,this);

                    ////this.StepButton.Text = "Step Complete";
                    ////this.StepButton.Enabled = false;
                    ////this.StepButton.BackColor = Color.FromArgb(128, 128, 128);
                    ////this.StepButtonpanel.BackColor = Color.FromArgb(64, 64, 64);
                    ////this.StepNumberTextBox.ForeColor = Color.FromArgb(0, 0, 0);
                }

                // short rolloverId = RollOverID;
                // string warningmsg = WarningMsg;
                // this.StepButton.BackColor = Color.FromArgb(38, 38, 38);
                // this.StepButton.Text = "Step Running...";
                // Thread.Sleep(2000);
                // this.StepButtonHandler(sender, rolloverId, warningmsg);
                // //this.StepButton.BackColor = Color.FromArgb(38, 38, 38);
                // //this.StepButton.Text = "Step Running...";
                // //Thread.Sleep(200000);
                //// this.StepButton.Text = "Step Complete";
                // //Thread CompleteThread = new Thread(StepButtonRunning);
                // //CompleteThread.Start();
                // this.StepButton.Text = "Step Complete";
                // this.StepButton.Enabled = false;
                // this.StepButton.BackColor = Color.FromArgb(128, 128, 128);
                // this.StepButtonpanel.BackColor = Color.FromArgb(64, 64, 64);


            }

        }




        /// <summary>
        /// Handles the RunWorkerCompleted event of the backGroundWork1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void rollYearWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.StepButton.Text = "Step Complete";
                this.StepButton.Enabled = false;
                this.StepButton.BackColor = Color.FromArgb(128, 128, 128);
                this.StepButtonpanel.BackColor = Color.FromArgb(64, 64, 64);
                this.StepNumberTextBox.ForeColor = Color.FromArgb(0, 0, 0);
            }
            catch (Exception ex)
            {
                ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }


        /// <summary>
        /// Handles the DoWork event of the backGroundWork1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        ////private void rollYearWork_DoWork(object sender, DoWorkEventArgs e)
        ////{
         
        ////    //this.StepButton.Text = "Step Running...";
        ////    //this.StepButton.BackColor = Color.FromArgb(64, 64, 64);
        ////    short rolloverId = RollOverID;
        ////    string warningmsg = WarningMsg;
        ////    this.StepButtonHandler(sender, rolloverId);
        ////    //StepButton.Invoke((MethodInvoker)delegate { StepButton.Text = "Step Running..."; StepButton.BackColor = Color.FromArgb(64, 64, 64); });

         
        ////}


    }
}