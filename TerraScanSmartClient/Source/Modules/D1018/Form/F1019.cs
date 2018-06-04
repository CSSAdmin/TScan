//--------------------------------------------------------------------------------------------
// <copyright file="F1019.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1019.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// Mar-21           Manoj        	    Created// 
//*********************************************************************************/



namespace D1018
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
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities; 
    using TerraScan.Infrastructure.Interface.Constants;   
    using System.Configuration;
    using System.Web.Services.Protocols;
    using D1018;
   
    /// <summary>
    /// Form F1020
    /// </summary>
    public partial class F1019 : BasePage  
    {

        #region variables

        /// <summary>
        /// Created Instance for F9101Controller
        /// </summary>
        private F1019Controller form1019Control= new F1019Controller();

        /// <summary>
        /// variable holds the OwnerId value.
        /// </summary>
        private int ownerId;

        private string payeeID;

        /// <summary>
        /// PaymentEngineData
        /// </summary>
        private PaymentEngineData ownerDetailDataSet = new PaymentEngineData();

        ///<summary>
        /// Owner Detail XML
        /// </summary>
        private string OwnerDetailXML;

        ///<summary>
        ///used to hold OwnerID
        /// </summary>
        private string CurrentOwnerID;

        /// <summary>
        /// Flag to change control's editable property
        /// </summary>
        private bool isEditable;

        #endregion

        #region Constructor

        public F1019(string payeeDetails)
        {
            InitializeComponent();
            //string payeeinfo;
            PaymentEngineData.PayeeDetailDataTable  OwnerDetailDataSet = new PaymentEngineData.PayeeDetailDataTable ();
            try
            {
                string ss = payeeDetails; //"<Root><Table><Test1>Test1</Test1><Test2>Test2</Test2></Table></Root>";
                System.IO.StringReader stringReader = new System.IO.StringReader(ss);
                System.Xml.XmlTextReader textReader = new System.Xml.XmlTextReader(stringReader);
                //System.Xml.XmlReader reader = new System.Xml.XmlReader();
                DataSet ds = new DataSet();
                ds.ReadXml(textReader);

                if (ds.Tables.Count > 0)
                {
                    this.PaidByTextBox.Text = ds.Tables[0].Rows[0][0].ToString();
                    this.Address1TextBox.Text = ds.Tables[0].Rows[0][1].ToString();
                    this.Address2TextBox.Text = ds.Tables[0].Rows[0][2].ToString();
                    this.CityTextBox.Text = ds.Tables[0].Rows[0][3].ToString();
                    this.StateTextBox.Text = ds.Tables[0].Rows[0][4].ToString();
                    this.ZipTextBox.Text = ds.Tables[0].Rows[0][5].ToString();
                    this.CommentTextBox.Text = ds.Tables[0].Rows[0][6].ToString();   
                }
            }
            catch(Exception ex)
            {
            }
             
        }

        public F1019(string payeeDetails, bool isReadOnly)
        {
            InitializeComponent();
            //string payeeinfo;
            PaymentEngineData.PayeeDetailDataTable OwnerDetailDataSet = new PaymentEngineData.PayeeDetailDataTable();
            try
            {
                string ss = payeeDetails; //"<Root><Table><Test1>Test1</Test1><Test2>Test2</Test2></Table></Root>";
                System.IO.StringReader stringReader = new System.IO.StringReader(ss);
                System.Xml.XmlTextReader textReader = new System.Xml.XmlTextReader(stringReader);
                //System.Xml.XmlReader reader = new System.Xml.XmlReader();
                DataSet ds = new DataSet();
                ds.ReadXml(textReader);

                if (ds.Tables.Count > 0)
                {
                    this.PaidByTextBox.Text = ds.Tables[0].Rows[0][0].ToString();
                    this.Address1TextBox.Text = ds.Tables[0].Rows[0][1].ToString();
                    this.Address2TextBox.Text = ds.Tables[0].Rows[0][2].ToString();
                    this.CityTextBox.Text = ds.Tables[0].Rows[0][3].ToString();
                    this.StateTextBox.Text = ds.Tables[0].Rows[0][4].ToString();
                    this.ZipTextBox.Text = ds.Tables[0].Rows[0][5].ToString();
                    this.CommentTextBox.Text = ds.Tables[0].Rows[0][6].ToString().Trim();
                }

                this.IsEditable = isReadOnly;
            }
            catch (Exception ex)
            {
            }

        }

        public F1019()
        {
            InitializeComponent();
            //this.CancelButton = this.MasterCancelButton; 
             
        }
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the mortgage import template controll.
        /// </summary>
        /// <value>The mortgage import template controll.</value>
        [CreateNew]
        public F1019Controller F1019Controll
        {
            get { return this.form1019Control as F1019Controller; }
            set { this.form1019Control = value; }
        }


        public string PayeeID
        {
            get { return this.payeeID; }
            set { this.payeeID = value; }
        }

        /// <summary>
        /// Make control as locked/unlocked
        /// </summary>
        public bool IsEditable
        {
            get
            {
                return this.isEditable;
            }
            set
            {
                this.isEditable = value;
            }
        }

        #endregion

        #region Event Publication
        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        private void PeopleButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                this.Cursor = Cursors.WaitCursor;
                string currentOwnerId = string.Empty;
                Form  paidbyF1019 = new Form();
                paidbyF1019 = this.form1019Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form1019Control.WorkItem);
                if (paidbyF1019 != null)
                {
                    if (paidbyF1019.ShowDialog() == DialogResult.Yes)
                    {
                        this.ownerId = Convert.ToInt32(TerraScanCommon.GetValue(paidbyF1019, "MasterNameOwnerId"));
                        this.ownerDetailDataSet = this.form1019Control.WorkItem.F1019_GetPayeeDetails(this.ownerId); 
                        this.CurrentOwnerID = this.ownerId.ToString();
                        if (this.ownerDetailDataSet.PayeeDetail.Rows.Count > 0)
                        {
                            this.PaidByTextBox.Text = this.ownerDetailDataSet.PayeeDetail.Rows[0]["PaidBy"].ToString();
                            this.Address1TextBox.Text = this.ownerDetailDataSet.PayeeDetail.Rows[0]["Address1"].ToString();
                            this.Address2TextBox.Text = this.ownerDetailDataSet.PayeeDetail.Rows[0]["Address2"].ToString();
                            this.CityTextBox.Text = this.ownerDetailDataSet.PayeeDetail.Rows[0]["City"].ToString();
                            this.StateTextBox.Text = this.ownerDetailDataSet.PayeeDetail.Rows[0]["State"].ToString();
                            this.ZipTextBox.Text = this.ownerDetailDataSet.PayeeDetail.Rows[0]["Zip"].ToString();
                            this.CommentTextBox.Text = this.ownerDetailDataSet.PayeeDetail.Rows[0]["Comment"].ToString();
                        }
                        else
                        {
                            this.PaidByTextBox.Text = string.Empty;
                             this.Address1TextBox.Text = string.Empty;
                            this.Address2TextBox.Text = string.Empty;
                            this.CityTextBox.Text = string.Empty;
                            this.StateTextBox.Text = string.Empty;
                            this.ZipTextBox.Text = string.Empty;
                            this.CommentTextBox.Text = string.Empty; 
                        }
                        
                    }
                }
                
              
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

        private void MasterNameCloseButton_Click(object sender, EventArgs e)
        {
            ///create new datatable for send 
           ///value to Payment Engine
            DataTable tempTable = new DataTable();
            string OwnerItemXml;
            //tempTable = this.ownerDetailDataSet.GetPayment.Clone();
            if (tempTable.Columns.Count <= 0)
            {
              tempTable.Columns.AddRange(new DataColumn[] { new DataColumn("PaidBy"), new DataColumn("Address1"), new DataColumn("Address2"), new DataColumn("City"), new DataColumn("State"), new DataColumn("Zip"),new DataColumn("Comment") });
            }
            ///Column information
           // if (this.ownerDetailDataSet.GetPayment.Rows[0]["paidBy"].ToString() != string.Empty)
            //if(this.ownerDetailDataSet.GetPayment.Rows.Count ==0)
            if (tempTable.Rows.Count == 0)
            {
                DataRow tempRow = tempTable.NewRow();
                tempRow["PaidBy"] = this.PaidByTextBox.Text;  //this.ownerDetailDataSet.GetPayment.Rows[0]["PaidBy"].ToString();
                tempRow["Address1"] = this.Address1TextBox.Text; //this.ownerDetailDataSet.GetPayment.Rows[0]["Address1"].ToString();
                tempRow["Address2"] = this.Address2TextBox.Text; //this.ownerDetailDataSet.GetPayment.Rows[0]["Address2"].ToString();
                tempRow["City"] = this.CityTextBox.Text;  //this.ownerDetailDataSet.GetPayment.Rows[0]["City"].ToString();
                tempRow["State"] = this.StateTextBox.Text; //this.ownerDetailDataSet.GetPayment.Rows[0]["State"].ToString();
                tempRow["Zip"] = this.ZipTextBox.Text;//this.ownerDetailDataSet.GetPayment.Rows[0]["Zip"].ToString();
                tempRow["Comment"] = this.CommentTextBox.Text.Replace("\r","").Trim();  //this.ownerDetailDataSet.GetPayment.Rows[0]["Comment"].ToString();
                //tempRow["PaymentID"] = 0; 
                tempTable.Rows.Add(tempRow);
            }
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(tempTable);
            tempDataSet.Tables[0].TableName = "Table";
            string payeeIDs  = tempDataSet.GetXml();
            this.payeeID = payeeIDs;
           
            this.DialogResult = DialogResult.OK;
            this.Close();  
        
       }

        private void F1019_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.payeeID))
            {
                int payeeDetails;
                //int.TryParse(payeeID, out  payeeDetails);
                //this.ownerDetailDataSet = this.form1019Control.WorkItem.F1019_GetPayeeDetails(payeeDetails);
                this.CurrentOwnerID = payeeID.ToString();
                if (this.ownerDetailDataSet.GetPayment.Rows.Count > 0)
                {
                    this.PaidByTextBox.Text = this.ownerDetailDataSet.GetPayment.Rows[0]["PaidBy"].ToString();
                    this.Address1TextBox.Text = this.ownerDetailDataSet.GetPayment.Rows[0]["Address1"].ToString();
                    this.Address2TextBox.Text = this.ownerDetailDataSet.GetPayment.Rows[0]["Address2"].ToString();
                    this.CityTextBox.Text = this.ownerDetailDataSet.GetPayment.Rows[0]["City"].ToString();
                    this.StateTextBox.Text = this.ownerDetailDataSet.GetPayment.Rows[0]["State"].ToString();
                    this.ZipTextBox.Text = this.ownerDetailDataSet.GetPayment.Rows[0]["Zip"].ToString();
                    this.CommentTextBox.Text = this.ownerDetailDataSet.GetPayment.Rows[0]["Comment"].ToString().Trim();
                }
                else
                {
                    this.PaidByTextBox.Text = string.Empty;
                    this.Address1TextBox.Text = string.Empty;
                    this.Address2TextBox.Text = string.Empty;
                    this.CityTextBox.Text = string.Empty;
                    this.StateTextBox.Text = string.Empty;
                    this.ZipTextBox.Text = string.Empty;
                    this.CommentTextBox.Text = string.Empty;
                }

                // Lock/Unlock controls
                this.EnableControls(IsEditable);
            }
            this.CancelButton = this.CloseButton;
            this.CloseButton.Enabled = true;

            // Lock/Unlock controls
            this.EnableControls(IsEditable);
        }

       
        

        private void CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        private void EnableControls(bool isLock)
        {
            this.PaidByTextBox.LockKeyPress = !isLock;
            this.Address1TextBox.LockKeyPress = !isLock;
            this.Address2TextBox.LockKeyPress = !isLock;
            this.CityTextBox.LockKeyPress = !isLock;
            this.StateTextBox.LockKeyPress = !isLock;
            this.ZipTextBox.LockKeyPress = !isLock;
            this.CommentTextBox.LockKeyPress = !isLock;
            this.PeopleButton.Enabled = isLock;
        }
        
    
    }
}
