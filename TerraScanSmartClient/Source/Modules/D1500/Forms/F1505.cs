//--------------------------------------------------------------------------------------------
// <copyright file="F1505.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Generic Element Management. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20131118         Manoj P             Created
//20161221          priyadharshini     /* Modified to disable district panel when NE state */
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.UI.Controls;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Utilities;

    public partial class F1505 : Form
    {
        
      #region variables

         /// <summary>
        /// To store the Element Key Id
        /// </summary>
        private string elementKeyId;

        /// <summary>
        /// controller F1503Controller
        /// </summary>
        private F1505Controller form1505Control;

        /// <summary>
        /// dataset contains district management details.
        /// </summary>
        private F15002DistMgmtData districtMgmtDataSet = new F15002DistMgmtData();


        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private F15010ExciseAffidavitData districtSelectionDataSet = new F15010ExciseAffidavitData();

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// Used To StoreExchage RateId
        /// </summary>
        private int exciseRateId;

        /// <summary>
        /// Used To StoreExchage RollYear
        /// </summary>
        private string Yeartext;

        /// <summary>
        /// Used To StoreExchage LinkLabel
        /// </summary>
        private string linkLabel;

        /// <summary>
        /// Used To StoreExchage districtID
        /// </summary>
        private int districtTypeID;


        /// <summary>
        /// Used To StoreExchage activeID
        /// </summary>
        private int activeID;

        /// <summary>
        /// Used To StoreExchage exciserRateID
        /// </summary>
        private int exciserID=0;



        private string exciserRateID;

        /* Modified to disable district panel when NE state */
        /// <summary>
        /// Used To enable/disable exciseTable
        /// </summary>
        private bool isDistrictPanelEnable = false;


        /// <summary>
        /// Used To StoreExchage districtID
        /// </summary>
        private int districtID;

        /// <summary>
        /// Used To StoreExchange exciseId
        /// </summary>
        private string exciseID;

       #endregion variables

      #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1503"/> class.
        /// </summary>
        public F1505()
        {
            InitializeComponent();
        }

        public F1505(string RollYear, int DistrictType, int Active, string Exciselabel, string DistrictTypeText,string ExciseRateId, int DistrictID)
        {
            InitializeComponent();
            this.districtTypeID = DistrictType;
            this.Yeartext = RollYear;
            this.linkLabel = Exciselabel;
            this.activeID = Active;
            this.exciserRateID = ExciseRateId;
            this.districtID = DistrictID;

        }
        
        #endregion Constructor

        #region Property

        /// <summary>
        /// For 1503Control
        /// </summary>
        [CreateNew]
        public F1505Controller Form1505Control
        {
            get { return this.form1505Control as F1505Controller; }
            set { this.form1505Control = value; } 
        }

        /// <summary>
        /// Gets or sets the elementKeyId.
        /// </summary>
        /// <value>The ElementKeyId.</value>
        public string ElementKeyId
        {
            get { return this.elementKeyId; }
            set { this.elementKeyId = value; }
        }

        /// <summary>
        /// Gets or sets the command result.
        /// </summary>
        /// <value>The command result.</value>
        public string CommandResult
        {
            get { return commandResult; }
            set { commandResult = value; }
        }

        #endregion Property

        #region EventPublication

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;



        #endregion EventPublication

        /// <summary>
        /// Initialize the active combo box.
        /// </summary>
        private void InitActiveComboBox()
        {
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            this.ActiveComboBox.DataSource = commonData.ComboBoxDataTable;
            this.ActiveComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.ActiveComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
        }

        /// <summary>
        /// Inits the district type combo box.
        /// </summary>
        private void InitDistrictTypeComboBox()
        {
            this.districtMgmtDataSet = this.form1505Control.WorkItem.F15002_GetDistrictType(TerraScanCommon.UserId);
            this.DistrictTypeCOmbo.DataSource = this.districtMgmtDataSet.F15002_ListDistrictType;
            this.DistrictTypeCOmbo.ValueMember = this.districtMgmtDataSet.F15002_ListDistrictType.DistrictTypeIDColumn.ColumnName;
            this.DistrictTypeCOmbo.DisplayMember = this.districtMgmtDataSet.F15002_ListDistrictType.DistrictTypeColumn.ColumnName;
            byte ratenable;
            if (districtMgmtDataSet.DistrictVisibility.Rows.Count > 0)
            {
                F15002DistMgmtData.DistrictVisibilityRow districtType = (F15002DistMgmtData.DistrictVisibilityRow)this.districtMgmtDataSet.DistrictVisibility.Rows[0];

                if (!districtType.IsIsExciseRateEnabledNull())
                {
                    byte.TryParse(districtType.IsExciseRateEnabled.ToString(), out ratenable);
                    if (ratenable > 0)
                    {
                        this.isDistrictPanelEnable = true;
                    }
                    else
                    {
                        this.isDistrictPanelEnable = false;
                    }
                }
                else
                {
                    this.isDistrictPanelEnable = true;
                }
            }       
        }

        private void ExciseRatePictureBox_Click(object sender, EventArgs e)
        {
            Form districtF1102 = new Form();
            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
               int temprollyear = int.Parse(this.RollYearTextBox.Text);
                if (temprollyear > 0)
                {
                    object[] optionalParameters = new object[] { temprollyear };
                    districtF1102 = this.form1505Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1102, optionalParameters, this.form1505Control.WorkItem);
                }
                else
                {
                    int temrollyear = GetYear();
                    object[] optionalParameters = new object[] { temrollyear };
                    districtF1102 = this.form1505Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1102, optionalParameters, this.form1505Control.WorkItem);

                }
            }
            else
            {
                int temrollyear = GetYear();
                object[] optionalParameters = new object[] { temrollyear };
                districtF1102 = this.form1505Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1102, optionalParameters, this.form1505Control.WorkItem);

            }

            DialogResult districtDialog;
            if (districtF1102 != null)
            {
                districtDialog = districtF1102.ShowDialog();

                if (districtDialog == DialogResult.Yes)
                {
                    try
                    {
                        exciseID = TerraScanCommon.GetValue(districtF1102, "ExciseRateDistrictSelectionId");
                        exciseRateId = Convert.ToInt32(exciseID);
                        this.districtSelectionDataSet = this.form1505Control.WorkItem.F15010_GetDistrictSelection(Convert.ToInt32(this.exciseRateId));
                        if (this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows.Count > 0)
                        {
                            F15010ExciseAffidavitData.ListAffidavitDistrictSelectionRow districtRow = (F15010ExciseAffidavitData.ListAffidavitDistrictSelectionRow)this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0];
                            if (!districtRow.IsDistrictNull())
                            {
                                this.Exciselabel.Text = districtRow.District;
                            }
                            else
                            {
                                this.Exciselabel.Text = string.Empty;
                            }
                        
                        }

                        
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                }
                else if (districtDialog == DialogResult.Ignore)
                {
                    try
                    {
                        FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(1101);
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                }
            }

        }

        // TO IMPLEMENT CO:TFS 8095 – Provide Year default for 1102 when called from 11002.
        /// <summary>
        /// GET THE ROLL YEAR
        /// </summary>
        private int GetYear()
        {
            CommentsData getYearDataSet = new CommentsData();
            getYearDataSet = this.form1505Control.WorkItem.GetConfigDetails("TR_RollYear");
            int tempRollYear = -1;
            int.TryParse(getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString(), out tempRollYear);
            if (tempRollYear.Equals(0))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tempRollYear;
        }

        /* Modified to disable district panel when NE state */
        private void DisableDistrictPanel()
        {
            this.ExciseRatepanel.Enabled = this.isDistrictPanelEnable;
            this.ExciseRatelabel.Visible = this.isDistrictPanelEnable;
            this.ExciseRatePictureBox.Visible = this.isDistrictPanelEnable;
            this.Exciselabel.Visible = this.isDistrictPanelEnable;
        }

        private void F1505_Load(object sender, EventArgs e)
        {
            this.InitActiveComboBox();
            this.InitDistrictTypeComboBox();
            /* Modified to disable district panel when NE state */
            this.DisableDistrictPanel();
            this.DistrictTypeCOmbo.SelectedIndex=this.districtTypeID;
            this.RollYearTextBox.Text= this.Yeartext;
            this.Exciselabel.Text=this.linkLabel;
            this.ActiveComboBox.SelectedIndex=this.activeID;
            if(!string.IsNullOrEmpty(this.DistrictTextBox.Text) && !string.IsNullOrEmpty(this.RollYearTextBox.Text) && !string.IsNullOrEmpty(this.ActiveComboBox.Text))
            {
               this.CreateButton.Enabled = true; 
            }
           else
           {
               this.CreateButton.Enabled = false; 
           }

        }

        private void DistrictTextBox_Leave(object sender, EventArgs e)
        {
           if(!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.RollYearTextBox.Text) && this.ActiveComboBox.SelectedIndex>-1 && this.DistrictTypeCOmbo.SelectedIndex>-1) 
           {
               this.CreateButton.Enabled =true; 
           }
           else
           {
               this.CreateButton.Enabled = false; 
           }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            int rollyear = Convert.ToInt32(this.RollYearTextBox.Text);
            bool isactiv = Convert.ToBoolean(this.ActiveComboBox.SelectedValue);
            if(exciseRateId == 0)
              int.TryParse(this.exciserRateID, out exciseRateId);

            int districtTypeValue = Convert.ToInt32(this.DistrictTypeCOmbo.SelectedValue);
            string districtText = this.DistrictTextBox.Text.Trim();
            string message = this.form1505Control.WorkItem.F1505ExecuteCopyDistrict(this.districtID, districtText, rollyear, this.DescriptionTextBox.Text, isactiv, districtTypeValue, exciseRateId, TerraScanCommon.UserId);
            
            /* Modified to keep popup form open */
            DialogResult dialog= MessageBox.Show(message, "TerraScan - District Copy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (dialog == DialogResult.OK && message.Equals("The district already exists"))
            {
                this.DialogResult = DialogResult.None;
            }         
        }

        /// <summary>
        /// to open the ExciseRate District Form 
        /// </summary>
        /// <param name="sender">exciseRateId</param>
        /// <param name="e"></param>
        private void Exciselabel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11013);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.exciserRateID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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

        private void CancelButton_Click(object sender, EventArgs e)
        {

        }

        private void DistrictTextBox_TextChanged(object sender, EventArgs e)
        {

        }

       


    }
}
