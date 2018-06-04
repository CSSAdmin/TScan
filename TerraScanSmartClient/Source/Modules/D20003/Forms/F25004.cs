//----------------------------------------------------------------------------------
// <copyright file="F25004.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F25004.cs.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		         Description
// ----------		---------		     -------------------------------------------
// 04/05/2006       M.Vijaya Kumar       Created
// 14/05/2009       A.Shanmuga Sundaram  Modified to Implement TSCO# 7167
//*********************************************************************************/

namespace D20003
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;

    /// <summary>
    /// F25004 Class file
    /// </summary>
    public partial class F25004 : Form
    {
        #region Variables

        /// <summary>
        /// Used to store the parcel id on load (always -999 no use)
        /// </summary>
        private int parcelIdonLoad = -999;

        /// <summary>
        /// Usde to store the situsTextBoxValue
        /// </summary>
        private string situsTextBoxValue;

        /// <summary>
        /// Used to store the saveCompleted
        /// </summary>
        private bool saveCompleted;

        /// <summary>
        /// Used to store the currentSitusId
        /// </summary>
        private int currentSitusId;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to store the situsId
        /// </summary>
        private int situsId;

        /// <summary>
        /// used to store the parcelId
        /// </summary>
        private int parcelId;

        /// <summary>
        /// Used to store the unitId
        /// </summary>
        private int unitId;

        /// <summary>
        /// Usde to store the streetId
        /// </summary>
        private int streetId;

        /// <summary>
        /// Used to store the currentStreetId
        /// </summary>
        private int currentStreetId;

        /// <summary>
        /// Used to store the dataOnLoad
        /// </summary>
        private bool dataOnLoad;

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private Hashtable reportFileIdHashTable = new Hashtable();

        /// <summary>
        /// controller F25004Controller
        /// </summary>
        private F25004Controller form25004Control;

        /// <summary>
        /// used to store the streetListManagementData
        /// </summary>
        private F25011StreetListManagementData streetListManagementData = new F25011StreetListManagementData();

        /// <summary>
        /// Used to store situsManagementData
        /// </summary>
        private F25003SitusManagementData situsManagementData = new F25003SitusManagementData();

        /// <summary>
        /// Used to store the listStreetDataTable
        /// </summary>
        private F25003SitusManagementData.ListStreetDataTable listStreetDataTable = new F25003SitusManagementData.ListStreetDataTable();

        /// <summary>
        /// Usde to strore the listUnitTypeDataTable
        /// </summary>
        private F25003SitusManagementData.ListUnitTypeDataTable listUnitTypeDataTable = new F25003SitusManagementData.ListUnitTypeDataTable();

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F25004"/> class.
        /// </summary>
        public F25004()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F25004"/> class.
        /// </summary>
        /// <param name="situsId">The situs id.</param>
        /// <param name="parcelId">The parcel id.</param>
        public F25004(int situsId, int parcelId)
        {
            InitializeComponent();
            this.situsId = situsId;
            this.parcelId = parcelId;
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F25004Control
        /// </summary>
        [CreateNew]
        public F25004Controller Form25004Control
        {
            get { return this.form25004Control as F25004Controller; }
            set { this.form25004Control = value; }
        }

        /// <summary>
        /// Gets or sets the current situs id.
        /// </summary>
        /// <value>The current situs id.</value>
        public int CurrentSitusId
        {
            get { return this.currentSitusId; }
            set { this.currentSitusId = value; }
        }

        #endregion Property

        #region Methods

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.SitusTextBox.MaxLength = this.situsManagementData.ListSitusManagement.SitusColumn.MaxLength;
            this.HouseNumberTextBox.MaxLength = this.situsManagementData.ListSitusManagement.HouseNumberColumn.MaxLength;
            this.UnitNumberTextBox.MaxLength = this.situsManagementData.ListSitusManagement.UnitNumberColumn.MaxLength;
            this.ZipCodeTextBox.MaxLength = this.situsManagementData.ListSitusManagement.ZipCodeColumn.MaxLength;
            this.CityTextBox.MaxLength = this.situsManagementData.ListSitusManagement.CityColumn.MaxLength;

            this.StreetNameComboBox.MaxLength = this.situsManagementData.ListSitusManagement.StreetNameColumn.MaxLength;
            this.UnitTypeComboBox.MaxLength = this.situsManagementData.ListSitusManagement.UnitTypeColumn.MaxLength;
        }

        /// <summary>
        /// Clears the situs edit form.
        /// </summary>
        private void ClearSitusEdit()
        {
            this.SitusTextBox.Text = string.Empty;
            this.HouseNumberTextBox.Text = string.Empty;
            this.UnitTypeComboBox.SelectedIndex = -1;
            this.UnitNumberTextBox.Text = string.Empty;
            this.ZipCodeTextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.XCoordinatesTextBox.Text = string.Empty;
            this.YCoordinatesTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Loads the name of the street.
        /// </summary>
        private void LoadStreetName()
        {
            //DataRow customRow = this.listStreetDataTable.NewRow();
            //this.listStreetDataTable.Clear();
            //customRow[this.listStreetDataTable.StreetIDColumn.ColumnName] = "0";
            //customRow[this.listStreetDataTable.StreetNameColumn.ColumnName] = string.Empty;
            //this.listStreetDataTable.Rows.Add(customRow);
            this.situsManagementData = this.form25004Control.WorkItem.F25003_ListStreet();

            this.listStreetDataTable = this.situsManagementData.ListStreet;

            if (this.listStreetDataTable.Rows.Count > 0)
            {
                this.StreetNameComboBox.DataSource = this.listStreetDataTable;
                this.StreetNameComboBox.DisplayMember = this.listStreetDataTable.StreetNameColumn.ColumnName;
                this.StreetNameComboBox.ValueMember = this.listStreetDataTable.StreetIDColumn.ColumnName;

                if (this.streetId > 0)
                {
                    this.StreetNameComboBox.SelectedValue = this.streetId;
                }
                else
                {
                    this.StreetNameComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Enablesaves the cancel button.
        /// </summary>
        /// <param name="isenable">if set to <c>true</c> [isenable].</param>
        private void EnablesaveCancelButton(bool isenable)
        {
            this.SitusEditSaveButton.Enabled = isenable;
        }

        /// <summary>
        /// Loads the Unit Type Combo Box
        /// </summary>
        private void LoadUnitType()
        {
            DataRow customRow = this.listUnitTypeDataTable.NewRow();
            this.listUnitTypeDataTable.Clear();
            customRow[this.listUnitTypeDataTable.UnitIDColumn.ColumnName] = "0";
            customRow[this.listUnitTypeDataTable.UnitColumn.ColumnName] = string.Empty;
            this.listUnitTypeDataTable.Rows.Add(customRow);

            this.situsManagementData = this.form25004Control.WorkItem.F25003_ListUnitType();

            this.listUnitTypeDataTable.Merge(this.situsManagementData.ListUnitType);

            if (this.listUnitTypeDataTable.Rows.Count > 0)
            {
                this.UnitTypeComboBox.DataSource = this.listUnitTypeDataTable;
                this.UnitTypeComboBox.DisplayMember = this.listUnitTypeDataTable.UnitColumn.ColumnName;
                this.UnitTypeComboBox.ValueMember = this.listUnitTypeDataTable.UnitIDColumn.ColumnName;

                if (this.unitId > 0)
                {
                    this.UnitTypeComboBox.SelectedValue = this.unitId;
                }
                else
                {
                    this.UnitTypeComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// To save the Situs Edit
        /// </summary>
        private void SaveSitusEdit()
        {
            bool isFuturePush = false;
            DialogResult dialogResult;
            dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("SitusPushBeforeSave")), "TerraScan T2– Push Situs to Future Years", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                isFuturePush = true;
            }
            else
            {
                isFuturePush = false;
            }
            decimal tempXCOrdinates;
            decimal tempYCOrdinates;

            //// if (!string.IsNullOrEmpty(this.HouseNumberTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.StreetNameComboBox.Text.Trim())) // && !string.IsNullOrEmpty(this.CityTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.ZipCodeTextBox.Text.Trim()))
            if (!string.IsNullOrEmpty(this.HouseNumberTextBox.Text.Trim()) && this.StreetNameComboBox.SelectedValue != null)
            {
                this.situsManagementData.ListSitusManagement.Rows.Clear();
                F25003SitusManagementData.ListSitusManagementRow dr = this.situsManagementData.ListSitusManagement.NewListSitusManagementRow();
                dr.Situs = this.SitusTextBox.Text.Trim();

                dr.ParcelID = this.parcelId;

                dr.HouseNumber = this.HouseNumberTextBox.Text.Trim();

                if (!string.IsNullOrEmpty(this.StreetNameComboBox.Text.Trim()))
                {
                    dr.StreetID = Convert.ToInt32(this.StreetNameComboBox.SelectedValue);
                }
                else
                {
                    dr.StreetID = 0;
                }

                if (!string.IsNullOrEmpty(this.UnitTypeComboBox.Text.Trim()))
                {
                    dr.UnitID = Convert.ToInt32(this.UnitTypeComboBox.SelectedValue);
                }
                else
                {
                    dr.UnitID = 0;
                }

                dr.UnitNumber = this.UnitNumberTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(this.ZipCodeTextBox.Text.Trim()))
                {
                    dr.ZipCode = this.ZipCodeTextBox.Text.Trim();
                }
                else
                {
                    dr.ZipCode = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.CityTextBox.Text.Trim()))
                {
                    dr.City = this.CityTextBox.Text.Trim();
                }
                else
                {
                    dr.City = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.XCoordinatesTextBox.Text.Trim()))
                {
                    Decimal.TryParse(this.XCoordinatesTextBox.Text.Trim(), out tempXCOrdinates);
                    dr.X_Coord = tempXCOrdinates;
                }

                if (!string.IsNullOrEmpty(this.YCoordinatesTextBox.Text.Trim()))
                {
                    Decimal.TryParse(this.YCoordinatesTextBox.Text.Trim(), out tempYCOrdinates);
                    dr.Y_Coord = tempYCOrdinates;
                }

                this.situsManagementData.ListSitusManagement.Rows.Add(dr);

                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                {
                    this.currentSitusId = this.form25004Control.WorkItem.F25003_SaveListMangement(this.situsId, (Utility.GetXmlString(this.situsManagementData.ListSitusManagement.Copy())), isFuturePush, TerraScanCommon.UserId);
                }
                else
                {
                    this.currentSitusId = this.form25004Control.WorkItem.F25003_SaveListMangement(-999, (Utility.GetXmlString(this.situsManagementData.ListSitusManagement.Copy())), isFuturePush, TerraScanCommon.UserId);
                }

                this.saveCompleted = true;
                this.EnablesaveCancelButton(false);
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), ConfigurationWrapper.ApplicationSave.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.saveCompleted = false;
            }


        }

        /// <summary>
        /// To Call the street List management Form
        /// </summary>
        private void ToCallStreetListMangementForm()
        {
            ////if (this.currentStreetId > 0)
            ////{                    
            FormInfo formInfo;
            formInfo = TerraScanCommon.GetFormInfo(20011);
            formInfo.optionalParameters = new object[1];
            formInfo.optionalParameters[0] = this.currentStreetId;
            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            //// }           
        }

        /// <summary>
        /// Used to Set the Page Mode Type
        /// </summary>
        private void ToSetPageModeType()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                if (this.situsId > 0)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                }
                else
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                }

                ////to Enable the save cancel button
                this.EnablesaveCancelButton(true);
            }
        }

        /// <summary>
        /// Fills the zip code and city text box.
        /// </summary>
        private void FillZipCodeAndCityTextBox()
        {
            ////Added by Biju on 05/May/2010 to implement #6973
            string streetName = "";
            ////Commented by Biju on 06/May/2010 to implement #6973
            ////if (!string.IsNullOrEmpty(this.StreetNameComboBox.Text.Trim()))
            ////Added by Biju on 06/May/2010 to implement #6973
            if (this.StreetNameComboBox.SelectedValue != null && !string.IsNullOrEmpty(this.StreetNameComboBox.SelectedValue.ToString()))
            {
                this.currentStreetId = 0;
                ////this.streetListManagementData.ListStreetManagement.Clear();
                ////this.streetListManagementData = this.form25004Control.WorkItem.F25011_ListMasterStreetList(this.currentStreetId, this.StreetNameComboBox.Text.Trim(), string.Empty);
                ////if (this.streetListManagementData.ListStreetManagement.Rows.Count > 0)
                ////{
                ////    this.currentStreetId = int.Parse(this.streetListManagementData.ListStreetManagement.Rows[0][this.streetListManagementData.ListStreetManagement.StreetIDColumn].ToString());
                ////}
                ////Commented by Biju on 04/May/2010 to implement #6973
                ////this.streetListManagementData.ListStreetManagement.Clear();
                this.currentStreetId = Convert.ToInt32(this.StreetNameComboBox.SelectedValue);
                ////Commented by Biju on 04/May/2010 to implement #6973
                ////this.streetListManagementData = this.form25004Control.WorkItem.F25011_ListMasterStreetList(this.currentStreetId, this.StreetNameComboBox.Text.Trim(), string.Empty);
                ////Added by Biju on 04/May/2010 to implement #6973
                DataRow[] tempDR = this.listStreetDataTable.Select("StreetID=" + this.currentStreetId);
                ////Commented by Biju on 04/May/2010 to implement #6973
                ////if (this.streetListManagementData.ListStreetManagement.Rows.Count > 0)
                ////Added by Biju on 04/May/2010 to implement #6973
                if (tempDR.Length > 0)
                {
                    ////Commented by Biju on 04/May/2010 to implement #6973
                    ////this.ZipCodeTextBox.Text = this.streetListManagementData.ListStreetManagement.Rows[0][this.streetListManagementData.ListStreetManagement.ZipCodeColumn].ToString();
                    ////this.CityTextBox.Text = this.streetListManagementData.ListStreetManagement.Rows[0][this.streetListManagementData.ListStreetManagement.CityColumn].ToString();
                    ////int.TryParse(this.streetListManagementData.ListStreetManagement.Rows[0][this.streetListManagementData.ListStreetManagement.StreetIDColumn].ToString(), out this.currentStreetId);
                    ////Added by Biju on 04/May/2010 to implement #6973
                    this.ZipCodeTextBox.Text = tempDR[0][this.listStreetDataTable.ZipCodeColumn.ColumnName].ToString();
                    this.CityTextBox.Text = tempDR[0][this.listStreetDataTable.CityColumn.ColumnName].ToString();
                    streetName = tempDR[0][this.listStreetDataTable.FullStreetNameColumn.ColumnName].ToString();
                }
                else
                {
                    this.ZipCodeTextBox.Text = string.Empty;
                    this.CityTextBox.Text = string.Empty;
                    this.currentStreetId = 0;
                }
            }
            else
            {
                this.ZipCodeTextBox.Text = string.Empty;
                this.CityTextBox.Text = string.Empty;
                this.currentStreetId = 0;
            }
            ////code added to fix the bug #3904 - > difference in Situs field before & after savings.
            ////khaja Dt.18-Dec-08
            this.SitusEditTextChanged(streetName);
        }

        /// <summary>
        /// Handles the Text changed event of the SitusEditTextBoxs control.
        /// </summary>
        private void SitusEditTextChanged(string streetName)
        {
            if (this.SitusEditSaveButton.Enabled)
            {
                this.situsTextBoxValue = string.Empty;

                if (!string.IsNullOrEmpty(this.HouseNumberTextBox.Text.Trim()))
                {
                    this.situsTextBoxValue = this.HouseNumberTextBox.Text.Trim();
                }
                ////Modified by Biju on 06/May/2010 to implement #6973
                if (this.StreetNameComboBox.SelectedValue != null && !string.IsNullOrEmpty(this.StreetNameComboBox.SelectedValue.ToString()))//// if (!string.IsNullOrEmpty(this.StreetNameComboBox.Text.Trim()))
                {
                    ////Commented by Biju on 04/May/2010 to implement #6973
                    ////this.streetListManagementData.ListStreetManagement.Clear();
                    ////this.streetListManagementData = this.form25004Control.WorkItem.F25011_ListMasterStreetList(this.currentStreetId, this.StreetNameComboBox.Text.Trim(), string.Empty);
                    ////if (this.streetListManagementData.ListStreetManagement.Rows.Count > 0)
                    ////{
                    ////    this.situsTextBoxValue = this.situsTextBoxValue + " " + this.streetListManagementData.ListStreetManagement.Rows[0][this.streetListManagementData.ListStreetManagement.FullStreetNameColumn].ToString();
                    ////   //// this.situsTextBoxValue = this.situsTextBoxValue + " " + this.StreetNameComboBox.Text.Trim();
                    //// }
                    ////Added by Biju on 04/May/2010 to implement #6973
                    if (string.IsNullOrEmpty(streetName))
                    {
                        DataRow[] tempDR = this.listStreetDataTable.Select("StreetID=" + this.StreetNameComboBox.SelectedValue);
                        if (tempDR.Length > 0)
                        {
                            streetName = tempDR[0][this.listStreetDataTable.FullStreetNameColumn.ColumnName].ToString();
                        }

                    }
                    this.situsTextBoxValue = this.situsTextBoxValue + " " + streetName;
                    ////till here
                }

                if (!string.IsNullOrEmpty(this.UnitTypeComboBox.Text.Trim()))
                {
                    this.situsTextBoxValue = this.situsTextBoxValue + " " + this.UnitTypeComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.UnitNumberTextBox.Text.Trim()))
                {
                    this.situsTextBoxValue = this.situsTextBoxValue + " " + this.UnitNumberTextBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.CityTextBox.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(this.situsTextBoxValue))
                    {
                        this.situsTextBoxValue = this.situsTextBoxValue + ", " + this.CityTextBox.Text.Trim();
                    }
                    else
                    {
                        this.situsTextBoxValue = this.situsTextBoxValue + " " + this.CityTextBox.Text.Trim();
                    }
                }

                if (!string.IsNullOrEmpty(this.ZipCodeTextBox.Text.Trim()))
                {
                    this.situsTextBoxValue = this.situsTextBoxValue + " " + this.ZipCodeTextBox.Text.Trim();
                }

                this.SitusTextBox.Text = this.situsTextBoxValue;
                this.situsTextBoxValue = string.Empty;
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F25004 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F25004_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.dataOnLoad = true;
                this.CancelButton = this.SitusEditCancelButton;
                ////this.SaveToolStripMenuItem.Click += new EventHandler(this.SitusEditSaveButton_Click);
                ////Added by Biju on 04/May/2010 to implement #6973
                string situsValue = "";
                this.SetMaxLength();

                if (this.situsId > 0)
                {
                    this.situsManagementData = this.form25004Control.WorkItem.F25003_ListSitusMangement(this.parcelIdonLoad, this.situsId);
                    if (this.situsManagementData.ListSitusManagement.Rows.Count > 0)
                    {
                        this.HouseNumberTextBox.Text = this.situsManagementData.ListSitusManagement.Rows[0][this.situsManagementData.ListSitusManagement.HouseNumberColumn].ToString();
                        ////int.TryParse(this.streetListManagementData.ListStreetManagement.Rows[0][this.streetListManagementData.ListStreetManagement.StreetIDColumn].ToString(), out this.currentStreetId);
                        this.UnitNumberTextBox.Text = this.situsManagementData.ListSitusManagement.Rows[0][this.situsManagementData.ListSitusManagement.UnitNumberColumn].ToString();
                        this.ZipCodeTextBox.Text = this.situsManagementData.ListSitusManagement.Rows[0][this.situsManagementData.ListSitusManagement.ZipCodeColumn].ToString();
                        this.CityTextBox.Text = this.situsManagementData.ListSitusManagement.Rows[0][this.situsManagementData.ListSitusManagement.CityColumn].ToString();

                        ////Commented by Biju on 04/May/2010 to implement #6973
                        ////this.SitusTextBox.Text = this.situsManagementData.ListSitusManagement.Rows[0][this.situsManagementData.ListSitusManagement.SitusColumn].ToString();
                        ////Added by Biju on 04/May/2010 to implement #6973
                        situsValue = this.situsManagementData.ListSitusManagement.Rows[0][this.situsManagementData.ListSitusManagement.SitusColumn].ToString();

                        this.XCoordinatesTextBox.Text = this.situsManagementData.ListSitusManagement.Rows[0][this.situsManagementData.ListSitusManagement.X_CoordColumn].ToString();
                        this.YCoordinatesTextBox.Text = this.situsManagementData.ListSitusManagement.Rows[0][this.situsManagementData.ListSitusManagement.Y_CoordColumn].ToString();
                        int.TryParse(this.situsManagementData.ListSitusManagement.Rows[0][this.situsManagementData.ListSitusManagement.UnitIDColumn].ToString(), out this.unitId);
                        int.TryParse(this.situsManagementData.ListSitusManagement.Rows[0][this.situsManagementData.ListSitusManagement.StreetIDColumn].ToString(), out this.streetId);
                        int.TryParse(this.situsManagementData.ListSitusManagement.Rows[0][this.situsManagementData.ListSitusManagement.StreetIDColumn].ToString(), out this.currentStreetId);
                        this.SitusIDAuditlinkLabel.Enabled = true;
                        this.SitusIDAuditlinkLabel.Text = "tTs_Situs [SitusID] " + this.situsId;
                    }
                    else
                    {
                        this.SitusIDAuditlinkLabel.Text = "tTs_Situs [SitusID] " + "";
                        this.SitusIDAuditlinkLabel.Enabled = false;
                    }
                }
                else
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.ClearSitusEdit();
                    this.SitusIDAuditlinkLabel.Text = "tTs_Situs [SitusID] " + "";
                    this.SitusIDAuditlinkLabel.Enabled = false;
                }

                this.LoadStreetName();
                this.LoadUnitType();
                ////Added by Biju on 04/May/2010 to implement #6973
                this.SitusTextBox.Text = situsValue;
                //// commented to Fix an issue due to New TSCO# 7167
                ////to fill the Zip code and city textbox
                // this.FillZipCodeAndCityTextBox();

                this.EnablesaveCancelButton(false);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.dataOnLoad = false;
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the SitusIDAuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void SitusIDAuditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(90101);
                formInfo.optionalParameters = new object[2];
                formInfo.optionalParameters[0] = this.Tag;
                formInfo.optionalParameters[1] = this.situsId;
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

        /// <summary>
        /// Handles the Click event of the SitusEditSaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SitusEditSaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.SaveSitusEdit();
                if (this.saveCompleted)
                {
                    this.EnablesaveCancelButton(false);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        /// <summary>
        /// Handles the Click event of the SitusEditCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SitusEditCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.EnablesaveCancelButton(false);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the StreetMgmtButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StreetMgmtButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currentStreetId == 0)
                {
                    this.ToCallStreetListMangementForm();
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                ////to check the whether streetId is grether than zero
                if (this.currentStreetId > 0)
                {
                    if (this.SitusEditSaveButton.Enabled)
                    {
                        //// switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, " ?"), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            case DialogResult.Yes:
                                {
                                    try
                                    {
                                        this.SaveSitusEdit();
                                        if (this.saveCompleted)
                                        {
                                            this.ToCallStreetListMangementForm();
                                            this.DialogResult = DialogResult.OK;
                                            this.Close();
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    catch (SoapException ex)
                                    {
                                        ////TODO : Need to find specific exception and handle it.
                                        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                                    }
                                    catch (Exception ex)
                                    {
                                        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                                    }

                                    break;
                                }

                            case DialogResult.No:
                                {
                                    this.ToCallStreetListMangementForm();
                                    this.EnablesaveCancelButton(false);
                                    this.DialogResult = DialogResult.Cancel;
                                    this.Close();
                                    break;
                                }

                            case DialogResult.Cancel:
                                {
                                    return;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        this.ToCallStreetListMangementForm();

                        if (this.currentStreetId > 0)
                        {
                            this.Close();
                        }
                    }
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        /// <summary>
        /// Handles the FormClosing event of the F25004 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F25004_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                {
                    if (e.CloseReason.Equals(CloseReason.UserClosing))
                    {
                        if (this.SitusEditSaveButton.Enabled)
                        {
                            ////switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName + " ?"), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        try
                                        {
                                            this.SaveSitusEdit();
                                            if (this.saveCompleted)
                                            {
                                                this.DialogResult = DialogResult.OK;
                                                e.Cancel = false;
                                            }
                                            else
                                            {
                                                e.Cancel = true;
                                            }
                                        }
                                        catch (SoapException ex)
                                        {
                                            ////TODO : Need to find specific exception and handle it.
                                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                                        }
                                        catch (Exception ex)
                                        {
                                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                                        }

                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        this.DialogResult = DialogResult.Cancel;
                                        e.Cancel = false;
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        e.Cancel = true;
                                        this.DialogResult = DialogResult.Cancel;
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Toes the enable save cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ToEnableSaveCancelButton(object sender, EventArgs e)
        {
            try
            {
                this.ToSetPageModeType();
                this.SitusEditTextChanged("");
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the StreetNameComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StreetNameComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.dataOnLoad)
                {
                    this.ToSetPageModeType();
                    ////to fill the Zip code and city textbox            
                    this.FillZipCodeAndCityTextBox();
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SitusEditSaveButton.Enabled)
                {
                    this.SaveSitusEdit();
                    if (this.saveCompleted)
                    {
                        this.EnablesaveCancelButton(false);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the StreetNameComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void StreetNameComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13 && (this.pageMode == TerraScanCommon.PageModeTypes.New || this.pageMode == TerraScanCommon.PageModeTypes.Edit))
                {
                    ////to fill the Zip code and city textbox            
                    this.FillZipCodeAndCityTextBox();
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the StreetNameComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StreetNameComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Check data loaded or not Bug ID 251
                if (!this.dataOnLoad)
                {
                    this.ToSetPageModeType();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        ////This event is removed by Biju on 05/May/2010 to implement #6973
        /// <summary>
        /// Handles the Leave event of the StreetNameComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StreetNameComboBox_Leave(object sender, EventArgs e)
        {
            ////try
            ////{
            ////    if (this.pageMode == TerraScanCommon.PageModeTypes.New || this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            ////    {
            ////        this.FillZipCodeAndCityTextBox();


            ////    }
            ////}
            ////catch (SoapException soapException)
            ////{
            ////    ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            ////}
            ////catch (Exception ex)
            ////{
            ////    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            ////}
        }

        #endregion Events

        private void ZipCodeLabel_Click(object sender, EventArgs e)
        {

        }
        ////This event is added by Biju on 05/May/2010 to implement #6973
        /// <summary>
        /// Handles the validated event of the StreetNameComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StreetNameComboBox_Validated(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.New || this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    this.FillZipCodeAndCityTextBox();


                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}