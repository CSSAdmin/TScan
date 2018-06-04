//--------------------------------------------------------------------------------------------
// <copyright file="ToolBoxSmartPart.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the ToolBoxSmartPart.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// ---------        ---------       Created
// 27 JUL 06		RANJANI JG	    Modified
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
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;

    /// <summary>
    /// ToolBox SmartPart
    /// </summary>
    [SmartPart]
    public partial class ToolBoxSmartPart : PrimaryBaseSmartPart
    {
        /// <summary>
        /// instance for toolBox Entity to access 
        /// </summary>
        private ToolBoxEntity toolBoxEntity = new ToolBoxEntity();        

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ToolBoxSmartPart"/> class.
        /// </summary>
        public ToolBoxSmartPart()
        {
            this.InitializeComponent();
            this.QueryByFormToolStripMenuItem.Click += new EventHandler(this.QueryByFormButton_Click);
        }       

        #region Publications and Subcriptions

        /// <summary>
        /// QueryByFromButtonClick Event
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryByFromButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> QueryByFromButtonClick;

        /// <summary>
        /// ClearFilterButtonClick Event
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_ClearFilterButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> ClearFilterButtonClick;

        /// <summary>
        /// QueryUtilityButtonClick Event
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryUtilityButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> QueryUtilityButtonClick;

        /// <summary>
        /// SnapshotUtilityButtonClick Event
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_SnapshotUtilityButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> SnapshotUtilityButtonClick;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tool box entity.
        /// </summary>
        /// <value>The tool box entity.</value>
        public ToolBoxEntity ToolBoxEntity
        {
            get { return this.toolBoxEntity; }
            set { this.toolBoxEntity = value; }
        }

        /// <summary>
        /// Property for NewButton Enable/Disable
        /// </summary>
        public bool EnableClearFilterButton
        {
            get
            {
                return this.ClearFilterButton.Enabled;
            }

            set
            {
                this.ClearFilterButton.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visible clear filter button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [visible clear filter button]; otherwise, <c>false</c>.
        /// </value>
        public bool VisibleClearFilterButton
        {
            get
            {
                return this.ClearFilterButton.Visible;
            }

            set
            {
                this.ClearFilterButton.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visible query by form button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [visible query by form button]; otherwise, <c>false</c>.
        /// </value>
        public bool VisibleQueryByFormButton
        {
            get
            {
                return this.QueryByFormButton.Visible;
            }

            set
            {
                this.QueryByFormButton.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visible calculator button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [visible calculator button]; otherwise, <c>false</c>.
        /// </value>
        public bool VisibleCalculatorButton
        {
            get
            {
                return this.CalculatorButton.Visible;
            }

            set
            {
                this.CalculatorButton.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visible snapshot utility button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [visible snapshot utility button]; otherwise, <c>false</c>.
        /// </value>
        public bool VisibleSnapshotUtilityButton
        {
            get
            {
                return this.SnapshotUtilityButton.Visible;
            }

            set
            {
                this.SnapshotUtilityButton.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visible query utility button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [visible query utility button]; otherwise, <c>false</c>.
        /// </value>
        public bool VisibleQueryUtilityButton
        {
            get
            {
                return this.QueryUtilityButton.Visible;
            }

            set
            {
                this.QueryUtilityButton.Visible = value;
            }
        }
       
        #endregion

        /// <summary>
        /// Shows the SnapshotUtility form on Snapshot UtilityButton Click.- called from smartpart used this
        /// </summary>
        public void ShowSnapshotUtilityForm()
        {
            object[] optionalParameter = new object[] { this.toolBoxEntity.CurrentFormId, this.toolBoxEntity.SnapshotIdXmlString, this.toolBoxEntity.SnapshotCount };
            ////9051 - snapshot utility form
            Form snapshot = TerraScanCommon.GetForm(9051, optionalParameter, this.toolBoxEntity.ParentWorkItem);

            if (snapshot != null && snapshot.ShowDialog() == DialogResult.Yes)
            {
                this.toolBoxEntity.CalledFormStatus = true;
                this.toolBoxEntity.KeyId = Convert.ToInt32(TerraScanCommon.GetValue(snapshot, "SnapShotId"));
                this.toolBoxEntity.SnapshotName = TerraScanCommon.GetValue(snapshot, "SnapshotName");
                this.toolBoxEntity.SnapshotDescription = TerraScanCommon.GetValue(snapshot, "SnapshotDescription");
            }
            else
            {
                this.toolBoxEntity.CalledFormStatus = false;
            }
        }

        /// <summary>
        /// Shows the queryUtility form on query UtilityButton Click.- called from smartpart used this
        /// </summary>
        public void ShowQueryUtilityForm()
        {
            object[] optionalParameter = new object[] { this.toolBoxEntity.CurrentFormId, this.toolBoxEntity.WhereCondition, this.toolBoxEntity.UserDefinedWhereCondition };
            ////9050 - query utility form
            Form query = TerraScanCommon.GetForm(9050, optionalParameter, this.toolBoxEntity.ParentWorkItem);

            if (query != null && query.ShowDialog() == DialogResult.Yes)
            {
                this.toolBoxEntity.CalledFormStatus = true;
                this.toolBoxEntity.KeyId = Convert.ToInt32(TerraScanCommon.GetValue(query, "QueryId"));
                this.toolBoxEntity.WhereCondition = TerraScanCommon.GetValue(query, "WhereCondition");
                this.toolBoxEntity.UserDefinedWhereCondition = TerraScanCommon.GetValue(query, "UserWhereCondition");
            }
            else
            {
                this.toolBoxEntity.CalledFormStatus = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the QueryByFormButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryByFormButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.QueryByFormButton.Enabled)
                {
                    this.QueryByFormButton.Focus();
                    this.QueryByFromButtonClick(this, new DataEventArgs<Button>(this.QueryByFormButton));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ClearFilterButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearFilterButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearFilterButtonClick(this, new DataEventArgs<Button>(sender as Button));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the QueryUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.toolBoxEntity = new ToolBoxEntity();
                this.QueryUtilityButtonClick(this, new DataEventArgs<Button>(sender as Button));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }    

        /// <summary>
        /// Handles the Click event of the SnapshotUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SnapshotUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.toolBoxEntity = new ToolBoxEntity();
                this.SnapshotUtilityButtonClick(this, new DataEventArgs<Button>(sender as Button));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }        
    }
}
