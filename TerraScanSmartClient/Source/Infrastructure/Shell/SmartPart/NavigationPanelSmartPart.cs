//--------------------------------------------------------------------------------------------
// <copyright file="NavigationPanelSmartPart.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the NavigationPanelSmartPart.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// Jan-02-2007      Thilak raj        	Panel width changed
// Jan-31-07        Jayanthi Sri        Panel width changed
// Feb-1-07         Jayanthi Sri        Animation Rate Changed for TerrascanXpPanel
//*********************************************************************************/

namespace TerraScan.UI 
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.XPPanel;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Common;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Helper; 
  
    /// <summary>
    /// NavigationPanel SmartPart 
    /// </summary>
    [SmartPart]
    public partial class NavigationPanelSmartPart : UserControl
    {
        #region Private Variables

        /// <summary>
        /// Variable for NavigationPanelSmartPartControll
        /// </summary>
        private NavigationPanelSmartPartController navigationPanelSmartPartControll;

        /// <summary>
        /// LeftPanel object
        /// </summary>
        private TerraScan.XPPanel.TerraScanXPPanel leftNavXPPanel;

        /// <summary>
        /// Link Label object for all Left Pannel controls
        /// </summary>
        private TerraScanLinkLabel pannelLinkLabel;

        /// <summary>
        /// Link Label object for Windows Panel
        /// </summary>
        private TerraScanLinkLabel windowLinkLabel;

        /// <summary>
        /// checkDataSet
        /// </summary>
        //  private DataSet checkDataSet = new DataSet();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor NavigationPanelSmartPart
        /// </summary>
        public NavigationPanelSmartPart()
        {
            InitializeComponent();
        }

        #endregion
        #region Published Events

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// event publication for logout event
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_LogoutEvent, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> LogoutEvent;

        /// <summary>
        /// event publication for ApplicationStatusLinkLabel_Click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ApplicationStatusLinkLabelClickEvent, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<bool>> ApplicationStatusLinkLabelClickEvent;

        /// <summary>
        /// event publication for WindowsLinkLabel click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_WindowsLinkLabelClickEvent, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<UserControl>> WindowsLinkLabelClickEvent;

        [EventPublication(EventTopicNames.ApexLogOutEvent, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<bool>> ApexLogOutEvent;
     
        
        #endregion
        
        #region Properties

        /// <summary>
        /// Property to access controller
        /// </summary>
        [CreateNew]
        public NavigationPanelSmartPartController NavigationPanelSmartPartControll
        {
            get { return this.navigationPanelSmartPartControll as NavigationPanelSmartPartController; }
            set { this.navigationPanelSmartPartControll = value; }
        }

        #endregion

        #region SubScribed Evens

        /// <summary>
        /// Hadles the FormActive Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">DataEvent Argrs</param>
        [EventSubscription(EventTopics.D9001_ShellForm_FormActivate, Thread = ThreadOption.UserInterface)]
        public void FormActivateEvent(object sender, DataEventArgs<string> e)
        {
            // write ur code here
            int locationY = 36;
            string displayName, accessName;
            int form;
            int lblCount = 0;
            int activeMenuCount = 0;

            Control[] tmpPnl = new Control[1];
            tmpPnl = this.xpPanelGroup3.Controls.Find("Windows", true);
            
            ((TerraScanXPPanel)tmpPnl[0]).Controls.Clear();
            bool checkWindowsLinkLabelExpanded = ((TerraScanXPPanel)tmpPnl[0]).IsExpanded;
           
            //((TerraScanXPPanel)tmpPnl[0]).PanelState = XPPanelState.Expanded;

            activeMenuCount = (int)this.navigationPanelSmartPartControll.WorkItem.State["ActiveMenuCount"];
            lblCount = (int)this.navigationPanelSmartPartControll.WorkItem.State["ActiveMenuCount"];

            if (lblCount > 0)
            {
                ((TerraScanXPPanel)tmpPnl[0]).Caption = "Windows  " + "(" + activeMenuCount.ToString() + ")";
                lblCount = (lblCount * 16) + 9;
            }
            else
            {
                ((TerraScanXPPanel)tmpPnl[0]).Caption = "Windows";
                lblCount = (lblCount * 16);
                if (!checkWindowsLinkLabelExpanded)
                {
                    ((TerraScanXPPanel)tmpPnl[0]).PanelState = XPPanelState.Expanded;
                }
            }

            ((TerraScanXPPanel)tmpPnl[0]).PanelHeight = lblCount;

            DataSet menuDataSet = (DataSet)this.navigationPanelSmartPartControll.WorkItem.State["FormItemsDataSet"];
            foreach (DataTable table in menuDataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (int.Equals((int)(row["Active"]), 1))
                    {
                        TerraScanLinkLabel tempLinkLabel = new TerraScanLinkLabel();
                        displayName = row["MenuName"].ToString();
                        accessName = row["FormFile"].ToString();
                        form = Convert.ToInt32(row["Form"]);
                        tempLinkLabel = this.CreateWindowLinkLabel(locationY, displayName, accessName, form);
                        tmpPnl[0].Controls.Add(tempLinkLabel);
                        locationY += 16;
                    }
                }
            }

            if (!checkWindowsLinkLabelExpanded)
            {
                ((TerraScanXPPanel)tmpPnl[0]).PanelState = XPPanelState.Collapsed;
            }

            if (string.Equals(e.Data.ToString(), "0"))
            {
                ////this.SetPanelState();
                ////((TerraScanXPPanel)tmpPnl[0]).PanelState = XPPanelState.Expanded;
            }
        }

        /// <summary>
        /// Handles the SetActiveLinkColor Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">DataEventArgs</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SetActiveLinkColor, Thread = ThreadOption.UserInterface)]
        public void SetActiveLinkColor(object sender, DataEventArgs<string> e)
        {
            string activeLinkName = e.Data.ToString();
            foreach (TerraScanXPPanel terraScanXPPanel in this.xpPanelGroup3.Controls)
            {
                foreach (TerraScanLinkLabel linkLabel in terraScanXPPanel.Controls)
                {
                    if (linkLabel.FormId.ToString() == activeLinkName)
                    {
                        linkLabel.LinkBehavior = LinkBehavior.AlwaysUnderline;
                        linkLabel.LinkColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        linkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
                        linkLabel.LinkColor = System.Drawing.Color.Black;
                    }
                }
            }
        }

        /// <summary>
        /// Called when [on D9001_ shell form_ set debug mode].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.OnD9001_ShellForm_SetDebugMode, ThreadOption.UserInterface)]
        public void OnOnD9001_ShellForm_SetDebugMode(object sender, TerraScan.Infrastructure.Interface.EventArgs<bool> eventArgs)
        {
            if (eventArgs.Data.Equals(true))
            {
                this.UserNameLabel.ForeColor = Color.Red;
            }
            else
            {
                this.UserNameLabel.ForeColor = Color.White;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// creates the terraScanMenu based on userId and applicationId
        /// </summary>
        /// <param name="terraScanMenuDataSet">DataSet Which Holds the terraScanMenu Items</param>
        private void CreateTerraScanMenu(DataSet terraScanMenuDataSet)
        {
            string form;
            string formFile;
            string menuName;
            string menuGroup;
            bool flagWithKeyId;
            int menuItemCount;
            int menuGroupId = -1;
            int permissionOpen = -1;
            for (int tabelCount = 0; tabelCount < terraScanMenuDataSet.Tables.Count; tabelCount++)
            {
                if (terraScanMenuDataSet.Tables[tabelCount].Rows.Count > 0)
                {
                    int locationY = 36;
                    menuGroupId = Convert.ToInt32(terraScanMenuDataSet.Tables[tabelCount].Rows[0]["MenuGroupID"]);
                    menuGroup = terraScanMenuDataSet.Tables[tabelCount].Rows[0]["MenuGroup"].ToString();
                    menuItemCount = terraScanMenuDataSet.Tables[tabelCount].Rows.Count;
                    menuName = terraScanMenuDataSet.Tables[tabelCount].Rows[0]["MenuName"].ToString();
                    if (string.IsNullOrEmpty(menuName.Trim()))
                    {
                        menuItemCount = 0;
                    }
                    ////if(!menuGroup.Equals("NonMenuItem"))
                    if (!menuGroupId.Equals(99))
                    {
                        this.CreatePanel(menuGroup, menuItemCount); // temperory using count
                        //// Calling Method for Inserting Menu

                        for (int itemCount = 0; itemCount < terraScanMenuDataSet.Tables[tabelCount].Rows.Count; itemCount++)
                        {
                            menuName = terraScanMenuDataSet.Tables[tabelCount].Rows[itemCount]["MenuName"].ToString();
                            formFile = terraScanMenuDataSet.Tables[tabelCount].Rows[itemCount]["FormFile"].ToString();
                            ////formFile = terraScanMenuDataSet.Tables[tabelCount].Rows[itemCount]["SmartPartName"].ToString();
                            form = terraScanMenuDataSet.Tables[tabelCount].Rows[itemCount]["Form"].ToString();

                          // flagWithKeyId = Convert.ToBoolean(terraScanMenuDataSet.Tables[tabelCount].Rows[itemCount]["IsKeyOpenDefault"].ToString());
                            if (!string.IsNullOrEmpty(terraScanMenuDataSet.Tables[tabelCount].Rows[itemCount]["IsKeyOpenDefault"].ToString()))
                            {
                                flagWithKeyId = Convert.ToBoolean(terraScanMenuDataSet.Tables[tabelCount].Rows[itemCount]["IsKeyOpenDefault"].ToString());
                            }
                            else
                            {
                                flagWithKeyId = false;
                            }

                            if (!string.IsNullOrEmpty(menuName))
                            {
                                permissionOpen = Convert.ToInt32(terraScanMenuDataSet.Tables[tabelCount].Rows[itemCount]["IsPermissionOpen"]);
                                //// Calling Method for Adding LinkLableControls to Panel
                                this.CreateLinkLable(menuName, form, formFile, locationY, permissionOpen, flagWithKeyId);
                                locationY += 16;
                            }
                        }
                    }
                }
            }
            if (terraScanMenuDataSet.Tables.Count > 0)
            {
                this.CreatePanel("Windows", 0);
            }
        }

        /// <summary>
        /// Sets the Default Property for all panel.
        /// </summary>
        private void SetPanelState()
        {
            foreach (TerraScanXPPanel tempXPPanel in this.xpPanelGroup3.Controls)
            {
                // To send background Color for each panel
               tempXPPanel.PanelState = XPPanelState.Collapsed;
                tempXPPanel.PanelGradient.Color1 = Color.White;
                tempXPPanel.PanelGradient.Color2 = Color.White;
                tempXPPanel.PanelGradient.End = Color.White;
                tempXPPanel.PanelGradient.Start = Color.White;
                tempXPPanel.TextColors = new ColorPair(Color.White);
            }
        }

        /// <summary>
        /// Method to Create Each TerraScanLinkLabel For Active Child Window
        /// </summary>
        /// <param name="locationY">Position of TerraScanLinkLabel Where to Insert</param>
        /// <param name="displayName">Display Name</param>
        /// <param name="accessName">Access Name</param>
        /// <param name="form">The form.</param>
        /// <returns>windowLinkLabel</returns>
        private TerraScanLinkLabel CreateWindowLinkLabel(int locationY, string displayName, string accessName, int form)
        {
            //// Creating a new windowLinkLabel 

            this.windowLinkLabel = new TerraScanLinkLabel();

            ////set the properties of LinkLable

            this.windowLinkLabel.Text = displayName;
            this.windowLinkLabel.Name = accessName;
            this.windowLinkLabel.AccessibleName = accessName;
            this.windowLinkLabel.FormId = form;
            this.windowLinkLabel.PermissionOpen = 1;
            this.windowLinkLabel.AutoSize = true;
            this.windowLinkLabel.ActiveLinkColor = System.Drawing.Color.LightSkyBlue;
            this.windowLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.windowLinkLabel.LinkBehavior = LinkBehavior.HoverUnderline;

            ////this.windowLinkLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.windowLinkLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            this.windowLinkLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.windowLinkLabel.LinkColor = System.Drawing.Color.Black;
            this.windowLinkLabel.Location = new System.Drawing.Point(8, locationY);
            this.windowLinkLabel.Size = new System.Drawing.Size(150, 16);
            //this.windowLinkLabel.TabIndex = 8;
            this.windowLinkLabel.TabStop = false;
            this.windowLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.windowLinkLabel.Click += new EventHandler(this.WindowLinkLabel_Click);
            return this.windowLinkLabel;
        }

        /// <summary>
        /// Creates the link lable inside the Terrascan.XPPanel.
        /// </summary>
        /// <param name="menuName">menuName to Display</param>
        /// <param name="form">form</param>
        /// <param name="formFile">formFile</param>
        /// <param name="locationY">Location Y</param>
        /// <param name="permissionOpen">The permission open.</param>
        private void CreateLinkLable(string menuName, string form, string formFile, int locationY, int permissionOpen, bool flagwithKeyId) //// Creating TerraScanLinkLabel Controls and Setting Properties
        {
            this.pannelLinkLabel = new TerraScanLinkLabel();
            //// Set the properties of LinkLable
            this.pannelLinkLabel.Text = menuName;
            this.pannelLinkLabel.Name = form;
            this.pannelLinkLabel.AccessibleName = formFile;
            this.pannelLinkLabel.FormId = Convert.ToInt32(form);
            this.pannelLinkLabel.PermissionOpen = permissionOpen;
            this.pannelLinkLabel.AutoSize = true;
            this.pannelLinkLabel.Tag = flagwithKeyId;
            this.pannelLinkLabel.ActiveLinkColor = System.Drawing.Color.LightSkyBlue;
            this.pannelLinkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
            this.pannelLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.pannelLinkLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
            this.pannelLinkLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pannelLinkLabel.LinkColor = System.Drawing.Color.Black;
            this.pannelLinkLabel.Location = new System.Drawing.Point(8, locationY);
            this.pannelLinkLabel.Size = new System.Drawing.Size(150, 16);
            //this.pannelLinkLabel.TabIndex = 8;
            this.pannelLinkLabel.TabStop = false;
            this.pannelLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;            
            this.pannelLinkLabel.Click += new EventHandler(this.PannelLinkLabel_Click);
            this.leftNavXPPanel.Controls.Add(this.pannelLinkLabel);
        }

        /// <summary>
        /// Creates the panel.
        /// </summary>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="panelCount">The panel count.</param>
        private void CreatePanel(string nodeName, int panelCount) //// Creating Panel Control and Adding LinkLabels to Panel
        {
            this.leftNavXPPanel = new TerraScan.XPPanel.TerraScanXPPanel();

            //// Setting properties of LeftNavXPPanel Control
            this.leftNavXPPanel.AnimationRate = 0;
            this.leftNavXPPanel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.leftNavXPPanel.Dock = DockStyle.Top;
            this.leftNavXPPanel.AutoScrollMargin = new System.Drawing.Size(0, 10);
            this.leftNavXPPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.leftNavXPPanel.BackColor = System.Drawing.Color.Transparent;
            this.leftNavXPPanel.Caption = nodeName;
            this.leftNavXPPanel.CaptionCornerType = (TerraScan.XPPanel.CornerType)(TerraScan.XPPanel.CornerType.TopLeft | TerraScan.XPPanel.CornerType.TopRight);

            ////this.leftNavXPPanel.CaptionGradient.End = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(80)))), ((int)(((byte)(129)))));

            this.leftNavXPPanel.CaptionGradient.End = System.Drawing.Color.FromArgb((int)(byte)28, (int)(byte)80, (int)(byte)129);

            ////this.leftNavXPPanel.CaptionGradient.Start = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(80)))), ((int)(((byte)(129)))));

            this.leftNavXPPanel.CaptionGradient.Start = System.Drawing.Color.FromArgb((int)(byte)28, (int)(byte)80, (int)(byte)129);
            this.leftNavXPPanel.CaptionGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;

            ////this.leftNavXPPanel.CaptionUnderline = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));

            this.leftNavXPPanel.CaptionUnderline = System.Drawing.Color.FromArgb((int)(byte)255, (int)(byte)255, (int)(byte)255);
            this.leftNavXPPanel.CollapsedGlyphs.Highlight = 3;
            this.leftNavXPPanel.CollapsedGlyphs.ImageSet = this.purpleGlyphsImageSet;
            this.leftNavXPPanel.CollapsedGlyphs.Normal = 2;
            this.leftNavXPPanel.CollapsedGlyphs.Pressed = 3;
            this.leftNavXPPanel.ExpandedGlyphs.Highlight = 1;
            this.leftNavXPPanel.ExpandedGlyphs.ImageSet = this.purpleGlyphsImageSet;
            this.leftNavXPPanel.ExpandedGlyphs.Normal = 0;
            this.leftNavXPPanel.ExpandedGlyphs.Pressed = 1;
            this.leftNavXPPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
            this.leftNavXPPanel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.leftNavXPPanel.HorzAlignment = System.Drawing.StringAlignment.Near;
            this.leftNavXPPanel.ImageItems.Highlight = 3;
            this.leftNavXPPanel.ImageItems.ImageSet = null;
            this.leftNavXPPanel.ImageItems.Normal = 4;
            this.leftNavXPPanel.ImageItems.Pressed = 3;
            this.leftNavXPPanel.Name = nodeName;
            this.leftNavXPPanel.PanelGradient.End = System.Drawing.Color.White;
            this.leftNavXPPanel.PanelGradient.Start = System.Drawing.Color.White;
            this.leftNavXPPanel.PanelGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.leftNavXPPanel.PanelState = TerraScan.XPPanel.XPPanelState.Expanded;
            if (panelCount > 0)
            {
                panelCount = (panelCount * 16) + 44;
            }
            else
            {
                panelCount = (panelCount * 16) + 33;
            }

            this.leftNavXPPanel.Size = new System.Drawing.Size(this.leftNavXPPanel.Width, panelCount);
            this.leftNavXPPanel.PanelState = TerraScan.XPPanel.XPPanelState.Collapsed;

            ////this.leftNavXPPanel.TextColors.Background = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));

            this.leftNavXPPanel.TextColors.Background = System.Drawing.Color.FromArgb((int)(byte)192, (int)(byte)192, (int)(byte)0);
            this.leftNavXPPanel.TextColors.Foreground = System.Drawing.Color.White;
            this.leftNavXPPanel.TextHighlightColors.Background = System.Drawing.Color.Transparent;

            ////this.leftNavXPPanel.TextHighlightColors.Foreground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));

            this.leftNavXPPanel.TextHighlightColors.Foreground = System.Drawing.Color.FromArgb((int)(byte)192, (int)(byte)192, (int)(byte)0);
            this.leftNavXPPanel.VertAlignment = System.Drawing.StringAlignment.Center;
            this.leftNavXPPanel.XPPanelStyle = TerraScan.XPPanel.XPPanelStyle.Custom;
            ////this.leftNavXPPanel.Click += new EventHandler(this.LeftNavXPPanel_Click);
            this.leftNavXPPanel.Expanded += new EventHandler(this.LeftNavXPPanel_Expanded);
            this.leftNavXPPanel.Expanding += new EventHandler(this.LeftNavXPPanel_Expanding);
            this.leftNavXPPanel.Collapsed += new EventHandler(this.LeftNavXPPanel_Collapsed);
            this.leftNavXPPanel.TabStop = false;
            this.leftNavXPPanel.Width = this.xpPanelGroup3.Width;
            ////Added by Jayanthi to set the location of first (Receipts) link panel when scroll has come
            if (this.xpPanelGroup3.Controls.Count == 0)
            {
                this.leftNavXPPanel.Location = new Point(0,0); 
                this.xpPanelGroup3.Controls.Add(this.leftNavXPPanel);
            }
            // Till here 
            else
            {
                this.xpPanelGroup3.Controls.Add(this.leftNavXPPanel);
            }
            
        }

        /// <summary>
        /// Handles the Expanded event of the leftNavXPPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LeftNavXPPanel_Expanded(object sender, EventArgs e)
        {    
            
            xpPanelGroup3.Enabled = true;
            TerraScan.XPPanel.TerraScanXPPanel selectedXPPanel = (TerraScanXPPanel)sender;
            selectedXPPanel.Focus();
            //// Collapse the  other panles
            foreach (TerraScanXPPanel tempXPPanel in this.xpPanelGroup3.Controls)
            {
                if (selectedXPPanel.Name != tempXPPanel.Name)
                {
                    tempXPPanel.PanelState = XPPanelState.Collapsed;
                }
            }
           
        }

        /// <summary>
        /// Handles the Expanded event of the leftNavXPPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LeftNavXPPanel_Collapsed(object sender, EventArgs e)
        {
            TerraScan.XPPanel.TerraScanXPPanel selectedXPPanel = (TerraScanXPPanel)sender;
            selectedXPPanel.Focus();
        }

        /// <summary>
        /// Handles the Expanding event of the leftNavXPPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LeftNavXPPanel_Expanding(object sender, EventArgs e)
        {
            xpPanelGroup3.Enabled = false;
            this.SetPanelState();
            TerraScan.XPPanel.TerraScanXPPanel selectedXPPanel = (TerraScanXPPanel)sender;

            if (selectedXPPanel.IsExpanding)
            {
                selectedXPPanel.TextColors = new ColorPair(Color.FromArgb(168, 176, 19));
            }
            else
            {
                selectedXPPanel.TextColors = new ColorPair(Color.White);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Panel TerraScanLinkLabel Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">eventargs</param>
        private void PannelLinkLabel_Click(object sender, EventArgs e)
        {
            ////xpPanelGroup3.AutoSize = true;
            bool setParameter = false;
            TerraScanLinkLabel linkLabel = new TerraScanLinkLabel();
            linkLabel = ((TerraScanLinkLabel)sender);
            linkLabel.Focus();
            FormInfo formInfo;
            formInfo = TerraScanCommon.GetFormInfo(linkLabel.FormId);

            if ((linkLabel.Tag != null) && bool.TryParse(linkLabel.Tag.ToString(), out setParameter))
            {
                if (setParameter)
                {
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = 999; //999; //-1;       ////purushotham to resolve bug //999;
                }
            }
            bool isFieldUser = TerraScanCommon.IsFieldUser;
            //bool isDatabaseAvailable = ScriptEngine.IsDatabaseAvailable();
            bool isDatabaseAvailable = TerraScanCommon.IsDataBaseAvailable;  
            bool isOnLineMode = WSHelper.IsOnLineMode;
           

            if (isFieldUser && isOnLineMode && linkLabel.FormId == 3230)
            {
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            else if ((isFieldUser && isDatabaseAvailable))
            {
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));                
            }
            else if ((((isFieldUser && isOnLineMode && !isDatabaseAvailable) && linkLabel.FormId != 3230))
                || ((!isFieldUser && isOnLineMode) && linkLabel.FormId == 3230))
                MessageBox.Show(SharedFunctions.GetResourceString("PermissionCheck"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ((!isFieldUser && isOnLineMode) && linkLabel.FormId != 3230)
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

            if (isDatabaseAvailable && isFieldUser)
            {
                WSHelper.IsOnLineMode = false;
            }
            else
            {
                WSHelper.IsOnLineMode = true;
            }
        }

        /// <summary>
        /// Handles the Load Event For the SmartPart
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">eventargs</param>
        private void NavigationPanelSmartPart_Load(object sender, EventArgs e)
        {
            this.UserNameLabel.Text = TerraScanCommon.UserName;
            DataSet terraScanMenuItems = new DataSet();
            ////this.DateTimeLabel.Text = System.DateTime.Now.ToLongDateString();
            this.DateTimeLabel.Text = System.DateTime.Now.ToString("ddd MMMMMMMMM d, yyyy");
            terraScanMenuItems = this.navigationPanelSmartPartControll.Menu; 
            ////.GetMenuItems();
            DataSet formInfoDataSet = new DataSet();
            if (terraScanMenuItems.Tables != null && terraScanMenuItems.Tables.Count > 0 && terraScanMenuItems.Tables[0] != null)
            {
                formInfoDataSet.Tables.Add(terraScanMenuItems.Tables[0].Clone());
            }

            for (int i = 0; i < terraScanMenuItems.Tables.Count; i++)
            {
                formInfoDataSet.Tables[0].Merge(terraScanMenuItems.Tables[i], true);
            }

            TerraScanCommon.TerraScanCachedData = formInfoDataSet;
           
            
            this.CreateTerraScanMenu(terraScanMenuItems);
            ////ToDo:: Need to Remove After Test
            ////navigationPanelSmartPartControll.WorkItem.State["FormPermissions"] = NavigationPanelSmartPartController.GetFormPermissions(TerraScanCommon.UserId);
            this.Resize += new EventHandler(this.NavigationPanelSmartPart_Resize);
            this.xpPanelGroup3.AutoScroll = true;
            ////this.xpPanelGroup2.Size  = new Size(this.Width, this.ParentForm.Height - 24);

        }

        /// <summary>
        /// Handles the Resize event of the NavigationPanelSmartPart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NavigationPanelSmartPart_Resize(object sender, EventArgs e)
        {
           //this.xpPanelGroup2.Size = new Size(this.Width, this.Parent.Height);               
        }
        
        /// <summary>
        /// Handles the WindowLinkLabel Click Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">eventargs</param>
        private void WindowLinkLabel_Click(object sender, EventArgs e)
        {
            TerraScanLinkLabel linkLabel = new TerraScanLinkLabel();
            linkLabel = ((TerraScanLinkLabel)sender);
            FormInfo formInfo = TerraScanCommon.GetFormInfo(linkLabel.FormId);
            WorkItem moduleWorkItem = new WorkItem();
            UserControl childForm = new UserControl();
            string displayFormName = formInfo.formFile.Substring((formInfo.formFile.LastIndexOf(".") + 1), (formInfo.formFile.Length - formInfo.formFile.LastIndexOf(".") - 1));
            moduleWorkItem = (WorkItem)this.navigationPanelSmartPartControll.WorkItem.Items.Get(formInfo.form.ToString());
            if (moduleWorkItem != null)
            {
                childForm = (UserControl)moduleWorkItem.SmartParts.Get(displayFormName);
                if (childForm != null)
                {
                    this.WindowsLinkLabelClickEvent(this, new DataEventArgs<UserControl>(childForm));
                }
            }
            else
            {
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
        }

        /// <summary>
        /// Handles the Click event of the LogOutLinkLabel control.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LogOutLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TerraScanCommon.IsApexAvail = false;  
            this.ApexLogOutEvent(this,new DataEventArgs<bool>(true));
            if (!TerraScanCommon.IsApexAvail)
            {
                this.LogoutEvent(this, new DataEventArgs<string>("Logout"));
            }
            else
            {
                MessageBox.Show("Please close the Apex Application to Logout", "Terrascan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);    
            }

        }

        /// <summary>
        /// Set the tooltip for Display name when the width exceeds the panel's width
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void UserNameLabel_MouseEnter(object sender, EventArgs e)
        {
            ToolTip labelToolTip = new ToolTip();
            Graphics graphics = this.CreateGraphics();
            SizeF widthHeight = graphics.MeasureString(UserNameLabel.Text.Trim(), this.Font);
            if (UserNameLabel.AutoSize)
            {
                int parentx = LogoutPanel.Location.X;
                int childx = UserNameLabel.Location.X;
                int total = childx - parentx;
                int width = UserNameLabel.Size.Width;
                if (widthHeight.Width > LogoutPanel.Width - total)
                {
                    labelToolTip.RemoveAll();
                    labelToolTip.SetToolTip(UserNameLabel, UserNameLabel.Text);
                }
                else
                {
                    labelToolTip.RemoveAll();
                }
            }

            graphics.Dispose();
        }

        /// <summary>
        /// Handles the LinkClicked event of the ApplicationStatusLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        //private void ApplicationStatusLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    try
        //    {
        //        this.Cursor = Cursors.WaitCursor;
        //        bool checkOutProccessed = false;
        //        DataSet configDataSet = new DataSet();
        //        if (!WSHelper.IsOnLineMode)
        //        {
        //            if (MessageBox.Show(SharedFunctions.GetResourceString("SwitchOnlineMode"), SharedFunctions.GetResourceString("FieldUseHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //            {
        //                if (this.ParentForm.ActiveMdiChild != null)
        //                {
        //                    if (MessageBox.Show(SharedFunctions.GetResourceString("CloseAllForms"), SharedFunctions.GetResourceString("FieldUseHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.Yes)
        //                    {
        //                        if (DomainAuthorization.ActiveDirectoryExists(ConfigurationWrapper.DomainNameAdmin))
        //                        {
        //                            // WSHelper.IsOnLineMode = false;
        //                            WSHelper.F9065_UpdateApplicationStatus(TerraScanCommon.CheckOutStatus, false, TerraScanCommon.UserId);
        //                            WSHelper.IsOnLineMode = true;
        //                            this.ApplicationStatusLinkLabelClickEvent(this, new DataEventArgs<bool>(true));
        //                            // Close all the open forms and reload the menu and leftNav from server DB
        //                            this.ApplicationStatusLinkLabel.Text = "On Line";
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Your are not connect to net.");
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (DomainAuthorization.ActiveDirectoryExists(ConfigurationWrapper.DomainNameAdmin))
        //                    {
        //                        WSHelper.IsOnLineMode = false;
        //                        WSHelper.IsOnLineMode = false;
        //                        WSHelper.F9065_UpdateApplicationStatus(checkOutProccessed, true, TerraScanCommon.UserId);
        //                        WSHelper.IsOnLineMode = true;
        //                        this.ApplicationStatusLinkLabelClickEvent(this, new DataEventArgs<bool>(true));
        //                        this.ApplicationStatusLinkLabel.Text = "On Line";
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show(SharedFunctions.GetResourceString("NetworkNotAvailable"), SharedFunctions.GetResourceString("FieldUseHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (MessageBox.Show(SharedFunctions.GetResourceString("SwitchFieldMode"), SharedFunctions.GetResourceString("FieldUseHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //            {
        //                if (this.ParentForm.ActiveMdiChild != null)
        //                {
        //                    if (MessageBox.Show(SharedFunctions.GetResourceString("CloseAllForms"), SharedFunctions.GetResourceString("FieldUseHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.Yes)
        //                    {
        //                        if (ScriptEngine.IsServerAvailable())
        //                        {
        //                            if (ScriptEngine.IsDatabaseAvailable())
        //                            {
        //                                WSHelper.IsOnLineMode = false;

        //                                WSHelper.IsOnLineMode = false;
        //                                WSHelper.F9065_UpdateApplicationStatus(TerraScanCommon.CheckOutStatus, true, TerraScanCommon.UserId);

        //                                this.ApplicationStatusLinkLabelClickEvent(this, new DataEventArgs<bool>(true));
        //                                this.ApplicationStatusLinkLabel.Text = "Field";

        //                            }
        //                            else
        //                            {
        //                                MessageBox.Show("No DB exixts reinstall the application.");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("No sql express instance exists for field manipulation.\n");
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (ScriptEngine.IsServerAvailable())
        //                    {
        //                        if (ScriptEngine.IsDatabaseAvailable())
        //                        {
        //                            WSHelper.IsOnLineMode = false;

        //                            WSHelper.IsOnLineMode = false;
        //                            WSHelper.F9065_UpdateApplicationStatus(checkOutProccessed, false, TerraScanCommon.UserId);

        //                            this.ApplicationStatusLinkLabelClickEvent(this, new DataEventArgs<bool>(true));
        //                            this.ApplicationStatusLinkLabel.Text = "Field";
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show(SharedFunctions.GetResourceString("DoCheckoutProcess"), SharedFunctions.GetResourceString("FieldUseHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show(SharedFunctions.GetResourceString("SqlNotAvailable"), SharedFunctions.GetResourceString("FieldUseHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        //    }
        //    finally
        //    {
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        #endregion
    }
}
