//--------------------------------------------------------------------------------------------
// <copyright file="FSLegalGridUserControl.cs" company="Congruent">
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
// 15-Nov-2010      Biju I.G.           Added events to handle cell begin edit
//*********************************************************************************/

namespace TerraScan.FSLegalGrid
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;

    /// <summary>
    /// Usercontrol for FSLegalGrid
    /// </summary>
    public partial class FSLegalGridUserControl : TerraScan.Common.UserControlBasePage
    {
        #region Variables

        /// <summary>
        /// used to set Focus
        /// </summary>
        private bool setFocus = false;

        /// <summary>
        /// used to store focus value
        /// </summary>
        private bool focusTrue = false;

        ///<summary>
        ///tabkey value
        ///</summary>
        private bool tabkey = false;
        /// <summary>
        /// used to store all grids
        /// </summary>
        private DataSet loadFSLegalGridDS = new DataSet();

        /// <summary>
        /// used to store SubsectionGrid Details
        /// </summary>
        private DataTable subsectiondt = new DataTable();

        /// <summary>
        /// used to store NEGrid Details
        /// </summary>
        private DataTable nedt = new DataTable();

        /// <summary>
        /// used to store NWGrid Details
        /// </summary>
        private DataTable nwdt = new DataTable();

        /// <summary>
        /// used to store SWGrid Details
        /// </summary>
        private DataTable swdt = new DataTable();

        /// <summary>
        /// used to store SEGrid Details
        /// </summary>
        private DataTable sedt = new DataTable();

        /// <summary>
        /// used to store CommentsGrid Details
        /// </summary>
        private DataTable cmddt = new DataTable();

        /// <summary>
        /// used to store all Grid Details
        /// </summary>
        private DataSet copydataset = new DataSet();

        /// <summary>
        /// used to fill the dataset
        /// </summary>
        public DataSet filldataset = new DataSet();

        /// <summary>
        /// used to set mulitirow
        /// </summary>
        public bool multirow = false;

        /// <summary>
        /// used to set commentshide
        /// </summary>
        public bool commentshide = false;

        private bool secgridview = false;
        private int currentcolumnindex;
        private int currentrowindex;
        private bool tabindexset = false;

        public bool secgridedit = false;

        private bool commentclickflag = false;

        private int commentcolumnindex;

        private int commentrowindex;

        /// <summary>
        /// yaxisPoint
        /// </summary>
        private int yaxisPoint;

        #endregion

        #region  Event Declaration

        /// <summary>
        /// CommandImageCellClick EventHadler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void EventHandler(Object sender, DataGridViewCellEventArgs e);

        public event EventHandler CommandImageCellClick;

        /// <summary>
        /// EditEventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void EditEventHandler(Object sender, DataGridViewCellEventArgs e);
        public event EditEventHandler SectionGridEdit;

        ////public delegate void EditEventHandler(object sender, DataGridViewCellEventArgs e);
        ////public event EditEventHandler

        /// <summary>
        /// EditEventHandler1
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void EditEventHandler1(Object sender, DataGridViewCellEventArgs e);
        public event EditEventHandler1 NWGridBeginEdit;

        /// <summary>
        /// EditEventHandler2
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void EditEventHandler2(Object sender, DataGridViewCellCancelEventArgs e);
        public event EditEventHandler2 NEGridBeginEdit;

        /// <summary>
        /// EditEventHandler3
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void EditEventHandler3(Object sender, DataGridViewCellEventArgs e);
        public event EditEventHandler3 SWGridBeginEdit;

        /// <summary>
        /// EditEventHandler4
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void EditEventHandler4(Object sender, DataGridViewCellEventArgs e);
        public event EditEventHandler4 SEGridBeginEdit;

        /// <summary>
        /// EditEventHandler5
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void EditEventHandler5(Object sender, DataGridViewCellEventArgs e);
        public event EditEventHandler5 CommentsGridBeginEdit;

        /// <summary>
        /// SectionGridEditEnd EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void SectionGridEditEndEventHandler(Object sender, DataGridViewCellCancelEventArgs e);
        public event SectionGridEditEndEventHandler SectionGridEndEdit;

        /// <summary>
        /// SectionGridEditEnd EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        //public delegate void SectionGridEditEndEventHandler(Object sender, DataGridViewCellCancelEventArgs e);
        //public event SectionGridEditEndEventHandler SectionGridEndEdit;

        public delegate void SectionGridSelectionChangeEventHandler(Object sender, EventArgs e);
        public event SectionGridSelectionChangeEventHandler SectionGridSelectionChangeEvent;

        /* Added By Ramya For BugId 2479 */

        public delegate void SectionGridRowCancelEventHandler(Object sender, DataGridViewRowCancelEventArgs e);
        public event SectionGridRowCancelEventHandler SectionGridRowDeleteEvent;

        /*Till Here */

        public delegate void SectionGridKeyDownEventHandler(object sender, KeyEventArgs e);
        public event SectionGridKeyDownEventHandler SectionGridKeyDownEvent;

        public delegate void SectionGridKeyPressEventHandler(object sender, KeyPressEventArgs e);
        public event SectionGridKeyPressEventHandler SectionKeyPressEvent;

        public delegate void SectionGridPreviewKeyDownEventHandler(object sender, PreviewKeyDownEventArgs e);
        public event SectionGridPreviewKeyDownEventHandler SectionPreviewKeyDownEvent;

        public delegate void SectionPreviewKeyDownEventHandler(object sender, PreviewKeyDownEventArgs e);
        public event SectionPreviewKeyDownEventHandler SectionPKeyDownEvent;

        public delegate void NWGridPreviewKeyDownEventHandler(object sender, PreviewKeyDownEventArgs e);
        public event NWGridPreviewKeyDownEventHandler NWPreviewKeyDownEvent;

        public delegate void NEGridPreviewKeyDownEventHandler(object sender, PreviewKeyDownEventArgs e);
        public event NEGridPreviewKeyDownEventHandler NEPreviewKeyDownEvent;

        public delegate void SWGridPreviewKeyDownEventHandler(object sender, PreviewKeyDownEventArgs e);
        public event SWGridPreviewKeyDownEventHandler SWPreviewKeyDownEvent;

        public delegate void SEGridPreviewKeyDownEventHandler(object sender, PreviewKeyDownEventArgs e);
        public event SEGridPreviewKeyDownEventHandler SEPreviewKeyDownEvent;

        public delegate void SectionGridCheckedStateEventHandler(object sender, EventArgs e);
        public event SectionGridCheckedStateEventHandler SectionCheckedStateChanged;

        public delegate void NEGridKeyPressEventHandler(object sender, KeyPressEventArgs e);
        public event NEGridKeyPressEventHandler NEKeyPressEvent;

        public delegate void NWGridKeyPressEventHandler(object sender, KeyPressEventArgs e);
        public event NWGridKeyPressEventHandler NWKeyPressEvent;

        public delegate void SWGridKeyPressEventHandler(object sender, KeyPressEventArgs e);
        public event SWGridKeyPressEventHandler SWKeyPressEvent;

        public delegate void SEGridKeyPressEventHandler(object sender, KeyPressEventArgs e);
        public event SEGridKeyPressEventHandler SEKeyPressEvent;

        //public delegate void SectionGridKeyPressEventHandler(object sender, KeyPressEventArgs e);
        //public event SectionGridKeyPressEventHandler SectionKeyPressEvent;


        /// <summary>
        /// NEGridEditEnd EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void NEGridEditEndEventHandler(Object sender, DataGridViewCellCancelEventArgs e);
        public event NEGridEditEndEventHandler NEGridEndEdit;
        

        public delegate void NEGridTextChanged(object sender, EventArgs e);
        public event NEGridTextChanged NETextChanged;


        /// <summary>
        /// NWGridEditEnd EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void NWGridEditEndEventHandler(Object sender, DataGridViewCellCancelEventArgs e);
        public event NWGridEditEndEventHandler NWGridEndEdit;

        /// <summary>
        /// SWGridEditEnd EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void SWGridEditEndEventHandler(Object sender, DataGridViewCellCancelEventArgs e);
        public event SWGridEditEndEventHandler SWGridEndEdit;

        /// <summary>
        /// SEGridEditEnd EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void SEGridEditEndEventHandler(Object sender, DataGridViewCellCancelEventArgs e);
        public event SEGridEditEndEventHandler SEGridEndEdit;

        public delegate void SectionGridTextChanged(object sender, EventArgs e);
        public event SectionGridTextChanged SectionTextChanged;

        public delegate void SEGridTextChanged(object sender, EventArgs e);
        public event SEGridTextChanged SETextChanged;

        public delegate void SWGridTextChanged(object sender, EventArgs e);
        public event SWGridTextChanged SWTextChanged;

        public delegate void NWGridTextChanged(object sender, EventArgs e);
        public event NWGridTextChanged NWTextChanged;

        /// <summary>
        /// NEGridKeyUp EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void NEGridKeyUpEventHandler(object sender, KeyEventArgs e);
        public event NEGridKeyUpEventHandler NEGridKeyUp;

        /// <summary>
        /// NWGridKeyUp EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void NWGridKeyUpEventHandler(object sender, KeyEventArgs e);
        public event NWGridKeyUpEventHandler NWGridKeyUp;

        /// <summary>
        ///SEGridKeyUp EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void SEGridKeyUpEventHandler(object sender, KeyEventArgs e);
        public event SEGridKeyUpEventHandler SEGridKeyUp;

        /// <summary>
        /// SWGridKeyUp EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void SWGridKeyUpEventHandler(object sender, KeyEventArgs e);
        public event SWGridKeyUpEventHandler SWGridKeyUp;

        /// <summary>
        /// SectionGridKeyUp EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void SectionGridKeyUpEventHandler(object sender, KeyEventArgs e);
        public event SectionGridKeyUpEventHandler SecGridKeyUp;

        /// <summary>
        /// NEGridEditingControlShowing EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void NEGridEditingControlShowingEventHandler(object sender, DataGridViewEditingControlShowingEventArgs e);
        public event NEGridEditingControlShowingEventHandler NEGridEditingControlShowing;

        /// <summary>
        /// NWGridEditingControlShowing EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void NWGridEditingControlShowingEventHandler(object sender, DataGridViewEditingControlShowingEventArgs e);
        public event NWGridEditingControlShowingEventHandler NWGridEditingControlShowing;

        /// <summary>
        ///SEGridEditingControlShowing EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void SEGridEditingControlShowingEventHandler(object sender, DataGridViewEditingControlShowingEventArgs e);
        public event SEGridEditingControlShowingEventHandler SEGridEditingControlShowing;

        /// <summary>
        /// SWGridEditingControlShowing EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void SWGridEditingControlShowingEventHandler(object sender, DataGridViewEditingControlShowingEventArgs e);
        public event SWGridEditingControlShowingEventHandler SWGridEditingControlShowing;

        /// <summary>
        /// SectionGridEditingControlShowing EventHandler
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        public delegate void SectionGridEditingControlShowingEventHandler(object sender, DataGridViewEditingControlShowingEventArgs e);
        public event SectionGridEditingControlShowingEventHandler SectionGridEditingControlShowing;

        public delegate void SectionGridCellContentClickEventHandler(object sender, DataGridViewCellEventArgs e);
        public event SectionGridCellContentClickEventHandler SectionGridCellContentClick;

        public delegate void SectionGridCellContentDoubleClickEventHandler(object sender, DataGridViewCellEventArgs e);
        public event SectionGridCellContentDoubleClickEventHandler SectionGridCellContentDoubleClick;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FSLegalGridUserControl"/> class.
        /// </summary>
        public FSLegalGridUserControl()
        {
            InitializeComponent();
            //this.SectiondataGridView.CellEndEdit += new DataGridViewCellEventHandler(CommentsdataGridView_CellEndEdit);
            ////this.SectiondataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.SEdataGridView_UserDeletingRow);

            //// this.NWdataGridView.CellBeginEdit +=new DataGridViewCellCancelEventHandler(NWdataGridView_CellBeginEdit);
        }
        #endregion

        #region Property

        ///// <summary>
        ///// Gets or sets the fill grid.
        ///// </summary>
        ///// <value>The fill grid.</value>
        //// public bool SetFoucs
        //// {
        ////    get { return this.setFocus; }
        ////    set { this.setFocus = value; }
        //// }

        /// <summary>
        /// Gets or sets the fill grid.
        /// </summary>
        /// <value>The fill grid.</value>
        public DataSet FillGrid
        {
            get { return this.filldataset; }
            set { this.filldataset = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [multirow setting].
        /// </summary>
        /// <value><c>true</c> if [multirow setting]; otherwise, <c>false</c>.</value>
        public bool MultirowSetting
        {
            get { return this.multirow; }
            set { this.multirow = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [comments hide].
        /// </summary>
        /// <value><c>true</c> if [comments hide]; otherwise, <c>false</c>.</value>
        public bool CommentsHide
        {
            get { return this.CommentsdataGridView.Visible; }
            set { this.multirow = value; }
            ////this.Commentpanel.Visible= value;}
        }

        /// <summary>
        /// Gets the record count indicator.
        /// </summary>
        /// <value>The record count indicator.</value>
        public int RecordCountIndicator
        {
            get
            {
                ////return this.subsectiondt.Rows.Count;
                return this.loadFSLegalGridDS.Tables["SubDivisionTable"].Rows.Count;
            }
        }

        public bool SecGridBoolean
        {
            get { return this.secgridedit; }
            set { this.secgridedit = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resizes the grid.
        /// </summary>
        private void ResizeGrid()
        {

            int recordCount = this.SectiondataGridView.RowCount;
            ////recordCount = 2;

            
            if (recordCount > 1)
            {
                int increment = ((recordCount - 1) * 22);

                ////Added by Biju I.G. on 24/Nov/2009 to implement #4934
                this.SubName.HeaderText = "Subdivision / Section (" + (recordCount-1).ToString() + ")";
                
                ////SubSection/Divsion
                this.sectionpanel.Height = (69 + increment);
                this.SectiondataGridView.Height = (43 + increment);
                this.Line1Label.Height = (69 + increment);

                ////NEPanel
                this.NEpanel.Height = (69 + increment);
                this.NEdataGridView.Height = (43 + increment);
                this.label1.Height = (69 + increment);
                ////NWPanel
                this.NWpanel.Height = (69 + increment);
                this.NWdataGridView.Height = (43 + increment);
                this.label2.Height = (69 + increment);

                ////SWPanel
                this.SWpanel.Height = (69 + increment);
                this.SWdataGridView.Height = (43 + increment);
                this.label3.Height = (69 + increment);
                ////SEPanel
                this.SEpanel.Height = (69 + increment);
                this.SEdataGridView.Height = (43 + increment);
                this.label4.Height = (69 + increment);
                ////Comments Grid
                this.Commentpanel.Height = (69 + increment);
                this.CommentsdataGridView.Height = (43 + increment);

                this.Legalpanel.Height = (97 + increment);
                //// this.Height = (98 + height);
                this.Height = this.Legalpanel.Height - 2;
            }
            else
            {
                ////Added by Biju I.G. on 24/Nov/2009 to implement #4934
                this.SubName.HeaderText = "Subdivision / Section";

                this.sectionpanel.Height = 69;
                this.SectiondataGridView.Height = 43;
                this.Line1Label.Height = 69;
                ////NWPanel
                this.NEpanel.Height = 69;
                this.NEdataGridView.Height = 43;
                this.label1.Height = 69;
                ////NWPanel
                this.NWpanel.Height = 69;
                this.NWdataGridView.Height = 43;
                this.label2.Height = 69;
                ////SWPanel
                this.SWpanel.Height = 69;
                this.SWdataGridView.Height = 43;
                this.label3.Height = 69;
                ////SEPanel
                this.SEpanel.Height = 69;
                this.SEdataGridView.Height = 43;
                this.label4.Height = 69;
                ////Comments Grid
                this.Commentpanel.Height = 69;
                this.CommentsdataGridView.Height = 43;

                this.Legalpanel.Height = 97;
                ////this.Height = (98 + height);
                this.Height = this.Legalpanel.Height;
            }
        }

        /// <summary>
        /// Gets the datas.
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetDatas()
        {
            return this.loadFSLegalGridDS;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        /// <param name="dt">The dt.</param>
        public void fillData(DataTable dt)
        {
            this.loadFSLegalGridDS = new DataSet();
            this.loadFSLegalGrid();
            this.CustomiseDAtaGrid();

            this.SectiondataGridView.DataSource = this.loadFSLegalGridDS.Tables["SubDivisionTable"];

            this.NEdataGridView.DataSource = this.loadFSLegalGridDS.Tables["NEDetailsTable"];
            this.NWdataGridView.DataSource = this.loadFSLegalGridDS.Tables["NWDetailsTable"];
            this.SWdataGridView.DataSource = this.loadFSLegalGridDS.Tables["SWDetailsTable"];
            this.SEdataGridView.DataSource = this.loadFSLegalGridDS.Tables["SEDetailsTable"];
            this.CommentsdataGridView.DataSource = this.loadFSLegalGridDS.Tables["CommentsDetailsTable"];
            (SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).DataSource = dt;
            (SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).ValueMember = "SubID"; //// dt.Columns[0].ColumnName;
            (SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).DisplayMember = "SubName"; //// dt.Columns[1].ColumnName;// ;
            this.SectiondataGridView.Rows[0].Cells[0].Value = 0;
            this.SectiondataGridView.Rows[0].Cells[0].Selected = false;
            this.ResizeGrid();
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="FormDetails">The form details.</param>
        public void fillData(DataTable dt, DataSet FormDetails)
        {
            this.loadFSLegalGridDS.Clear();
            this.loadFSLegalGridDS.Merge(FormDetails);
            try
            {
               // this.NEdataGridView.DataSource = null;
            }
            catch (Exception ex)
            {
            }
            this.CustomiseDAtaGrid1(this.loadFSLegalGridDS);

            (SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).DataSource = dt;
            (SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).ValueMember = "SubID";
            (SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).DisplayMember = "SubName";

            this.SectiondataGridView.DataSource = this.loadFSLegalGridDS.Tables["SubDivisionTable"];
            //this.loadFSLegalGridDS.Tables["NEDetailsTable"].AcceptChanges();
            try
            {
                //if (this.NEdataGridView.Rows.Count == 0 || this.NEdataGridView.Rows.Count > 1)
                //{
                    
                    this.NEdataGridView.DataSource = this.loadFSLegalGridDS.Tables["NEDetailsTable"].DefaultView;
                   
                //}
            }
            catch (Exception ex)
            {
            }
        
            this.NWdataGridView.DataSource = this.loadFSLegalGridDS.Tables["NWDetailsTable"];
            this.CommentsdataGridView.DataSource = this.loadFSLegalGridDS.Tables["CommentsDetailsTable"];

            /*Coded to prevent the autogenerate rows in comments grid on Client issue - kuppu*/
            this.CommentsdataGridView.Columns[1].Visible = false;
            this.CommentsdataGridView.Columns[2].Visible = false;
            this.CommentsdataGridView.Columns[3].Visible = false;

            this.SWdataGridView.DataSource = this.loadFSLegalGridDS.Tables["SWDetailsTable"];
            this.SEdataGridView.DataSource = this.loadFSLegalGridDS.Tables["SEDetailsTable"];
            this.ResizeGrid();
            this.LoadCommentsImage();
            this.commentclickflag = false;
        }

        /* Added By ramya.D for BugId #2479 */
        /// <summary>
        /// deleteRecord
        /// </summary>
        /// <param name="dt">dt</param>
        /// <param name="rowIndex">rowIndex</param>
        public void deleteRecord(DataTable dt, int rowIndex)
        {
            //if (!string.IsNullOrEmpty(this.loadFSLegalGridDS.Tables["SubDivisionTable"].Rows[rowIndex]["SubName"].ToString()))
            if (!string.IsNullOrEmpty(this.loadFSLegalGridDS.Tables["SubDivisionTable"].Rows[rowIndex]["SubID"].ToString()))
            {
                this.loadFSLegalGridDS.Tables["NEDetailsTable"].Rows.RemoveAt(rowIndex);
                this.loadFSLegalGridDS.Tables["NWDetailsTable"].Rows.RemoveAt(rowIndex);
                this.loadFSLegalGridDS.Tables["CommentsDetailsTable"].Rows.RemoveAt(rowIndex);
                this.loadFSLegalGridDS.Tables["SWDetailsTable"].Rows.RemoveAt(rowIndex);
                this.loadFSLegalGridDS.Tables["SEDetailsTable"].Rows.RemoveAt(rowIndex);
                this.loadFSLegalGridDS.Tables["SubDivisionTable"].Rows.RemoveAt(rowIndex);
            }
            else
            {
                this.NEdataGridView.Rows[rowIndex].Selected = false;
                this.SectiondataGridView.Rows[rowIndex].Selected = false;
                this.NWdataGridView.Rows[rowIndex].Selected = false;
                this.SWdataGridView.Rows[rowIndex].Selected = false;
                this.SEdataGridView.Rows[rowIndex].Selected = false;
                this.CommentsdataGridView.Rows[rowIndex].Selected = false;
            }

            this.CustomiseDAtaGrid1(this.loadFSLegalGridDS);

            (SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).DataSource = dt;
            (SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).ValueMember = "SubID";
            (SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).DisplayMember = "SubName";

            this.SectiondataGridView.DataSource = this.loadFSLegalGridDS.Tables["SubDivisionTable"];

            this.NEdataGridView.DataSource = this.loadFSLegalGridDS.Tables["NEDetailsTable"];
            this.NWdataGridView.DataSource = this.loadFSLegalGridDS.Tables["NWDetailsTable"];
            this.CommentsdataGridView.DataSource = this.loadFSLegalGridDS.Tables["CommentsDetailsTable"];

            /*Coded to prevent the autogenerate rows in comments grid on Client issue - kuppu*/
            this.CommentsdataGridView.Columns[1].Visible = false;
            this.CommentsdataGridView.Columns[2].Visible = false;
            this.CommentsdataGridView.Columns[3].Visible = false;

            this.SWdataGridView.DataSource = this.loadFSLegalGridDS.Tables["SWDetailsTable"];
            this.SEdataGridView.DataSource = this.loadFSLegalGridDS.Tables["SEDetailsTable"];
            this.ResizeGrid();
            this.LoadCommentsImage();
            this.SectiondataGridView.CurrentCell = null;

        }

        /*Till Here*/

        /// <summary>
        /// Loads the comments image.
        /// </summary>
        public void LoadCommentsImage()
        {
            this.CommentsdataGridView.AutoGenerateColumns = false;

            if ((this.loadFSLegalGridDS != null) && (this.loadFSLegalGridDS.Tables != null) && (this.loadFSLegalGridDS.Tables["CommentsDetailsTable"] != null))
            {
                for (int i = 0; i < this.loadFSLegalGridDS.Tables["CommentsDetailsTable"].Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(this.loadFSLegalGridDS.Tables["CommentsDetailsTable"].Rows[i][0].ToString()))
                    {
                        this.CommentsdataGridView["Comments", i].Value = Properties.Resources.GREEN_copy;
                    }
                    else
                    {
                        this.CommentsdataGridView["Comments", i].Value = Properties.Resources.RED_copy; //// @"C:\Documents and Settings\kuppusamyb\Desktop\ciamge\C.jpg";
                    }
                }
            }
        }

        /// <summary>
        /// Customises the D ata grid1.
        /// </summary>
        /// <param name="columnDetails">The column details.</param>
        private void CustomiseDAtaGrid1(DataSet columnDetails)
        {
            this.SectiondataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection SectionGridcolumns = this.SectiondataGridView.Columns;
            SectionGridcolumns["SubName"].DataPropertyName = "SubID";
            SectionGridcolumns["Lot"].DataPropertyName = "Lot";
            SectionGridcolumns["LotP"].DataPropertyName = "LotP";
            SectionGridcolumns["Block"].DataPropertyName = "Block";
            SectionGridcolumns["BlockP"].DataPropertyName = "BlockP";

            this.NEdataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection NEcolumns = this.NEdataGridView.Columns;
            NEcolumns["NEFirst"].DataPropertyName = "NEFirst";
            NEcolumns["NWFirst"].DataPropertyName = "NWFirst";
            NEcolumns["SWFirst"].DataPropertyName = "SWFirst";
            NEcolumns["SEFirst"].DataPropertyName = "SEFirst";

            this.NWdataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection NWcolumns = this.NWdataGridView.Columns;
            NWcolumns["NESEC"].DataPropertyName = "NESEC";
            NWcolumns["NWSEC"].DataPropertyName = "NWSEC";
            NWcolumns["SWSEC"].DataPropertyName = "SWSEC";
            NWcolumns["SESEC"].DataPropertyName = "SESEC";

            this.SWdataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection SWcolumns = this.SWdataGridView.Columns;
            SWcolumns["ThirdNE"].DataPropertyName = "ThirdNE";
            SWcolumns["ThirdNW"].DataPropertyName = "ThirdNW";
            SWcolumns["ThirdSW"].DataPropertyName = "ThirdSW";
            SWcolumns["ThirdSE"].DataPropertyName = "ThirdSE";

            this.SEdataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection SEcolumns = this.SEdataGridView.Columns;
            SEcolumns["FourNE"].DataPropertyName = "FourNE";
            SEcolumns["FourNW"].DataPropertyName = "FourNW";
            SEcolumns["FourSW"].DataPropertyName = "FourSW";
            SEcolumns["FourSE"].DataPropertyName = "FourSE";
            this.CommentsdataGridView.AutoGenerateColumns = true;

            if (this.commentshide)
            {
                this.CommentsdataGridView.AutoGenerateColumns = false;
                DataGridViewColumnCollection Commentscolumns = this.CommentsdataGridView.Columns;
                Commentscolumns["Comments"].DataPropertyName = "Comments";
            }
        }

        /// <summary>
        /// Loads the FS legal grid.
        /// </summary>
        private void loadFSLegalGrid()
        {
            this.loadFSLegalGridDS = new DataSet();
            this.subsectiondt = new DataTable("SubDivisionTable");
            this.nedt = new DataTable("NEDetailsTable");
            this.nwdt = new DataTable("NWDetailsTable");
            this.sedt = new DataTable("SEDetailsTable");
            this.swdt = new DataTable("SWDetailsTable");
            this.cmddt = new DataTable("CommentsDetailsTable");

            ////Adding Subsection Column
            if (this.subsectiondt.Columns.Count == 0)
            {
                this.subsectiondt.Columns.Add("SubID", System.Type.GetType("System.Int32"));
                ////  this.subsectiondt.Columns.Add("SubID");
                this.subsectiondt.Columns.Add("Lot");
                this.subsectiondt.Columns.Add("LotP", System.Type.GetType("System.Boolean"));
                this.subsectiondt.Columns.Add("Block");
                this.subsectiondt.Columns.Add("BlockP", System.Type.GetType("System.Boolean"));
                this.subsectiondt.Columns.Add("LglID", System.Type.GetType("System.Int32"));
                this.subsectiondt.Columns.Add("InstID", System.Type.GetType("System.Int32"));
            }

            DataRow subSectionRow;
            subSectionRow = this.subsectiondt.NewRow();
            ////  subSectionRow["SubName"] = 0;

            // Code commented to fix TFS#12303 - Legal/Subdivision should not be defaulting on New
            //subSectionRow["SubID"] = 1;
            subSectionRow["Lot"] = string.Empty;
            subSectionRow["LotP"] = false;
            subSectionRow["Block"] = string.Empty;
            subSectionRow["BlockP"] = false;
            this.subsectiondt.Rows.Add(subSectionRow);
            this.subsectiondt.AcceptChanges();

            ////Adding NE Grid
            if (this.nedt.Columns.Count == 0)
            {
                this.nedt.Columns.Add("NEFirst");
                this.nedt.Columns.Add("NWFirst");
                this.nedt.Columns.Add("SWFirst");
                this.nedt.Columns.Add("SEFirst");
                this.nedt.Columns.Add("LglID", System.Type.GetType("System.Int32"));
                this.nedt.Columns.Add("InstID", System.Type.GetType("System.Int32"));
            }

            DataRow nerow;
            nerow = this.nedt.NewRow();
            nerow["NEFirst"] = string.Empty;
            nerow["NWFirst"] = string.Empty;
            nerow["SWFirst"] = string.Empty;
            nerow["SEFirst"] = string.Empty;
            this.nedt.Rows.Add(nerow);
            this.nedt.AcceptChanges();

            ////Adding NWGrid
            if (this.nwdt.Columns.Count == 0)
            {
                this.nwdt.Columns.Add("NESEC");
                this.nwdt.Columns.Add("NWSEC");
                this.nwdt.Columns.Add("SWSEC");
                this.nwdt.Columns.Add("SESEC");
                this.nwdt.Columns.Add("LglID", System.Type.GetType("System.Int32"));
                this.nwdt.Columns.Add("InstID", System.Type.GetType("System.Int32"));
            }

            DataRow nwrow;
            nwrow = this.nwdt.NewRow();
            nwrow["NESEC"] = string.Empty;
            nwrow["NWSEC"] = string.Empty;
            nwrow["SWSEC"] = string.Empty;
            nwrow["SESEC"] = string.Empty;
            this.nwdt.Rows.Add(nwrow);
            this.nwdt.AcceptChanges();

            ////Adding SW Grid
            if (this.swdt.Columns.Count == 0)
            {
                this.swdt.Columns.Add("ThirdNE");
                this.swdt.Columns.Add("ThirdNW");
                this.swdt.Columns.Add("ThirdSW");
                this.swdt.Columns.Add("ThirdSE");
                this.swdt.Columns.Add("LglID", System.Type.GetType("System.Int32"));
                this.swdt.Columns.Add("InstID", System.Type.GetType("System.Int32"));
            }

            DataRow swrow;
            swrow = this.swdt.NewRow();
            swrow["ThirdNE"] = string.Empty;
            swrow["ThirdNW"] = string.Empty;
            swrow["ThirdSW"] = string.Empty;
            swrow["ThirdSE"] = string.Empty;
            this.swdt.Rows.Add(swrow);
            this.swdt.AcceptChanges();
            ////Adding SE Grid
            if (this.sedt.Columns.Count == 0)
            {
                this.sedt.Columns.Add("FourNE");
                this.sedt.Columns.Add("FourNW");
                this.sedt.Columns.Add("FourSW");
                this.sedt.Columns.Add("FourSE");
                this.sedt.Columns.Add("LglID", System.Type.GetType("System.Int32"));
                this.sedt.Columns.Add("InstID", System.Type.GetType("System.Int32"));
            }

            DataRow serow;
            serow = this.sedt.NewRow();
            serow["FourNE"] = string.Empty;
            serow["FourNW"] = string.Empty;
            serow["FourSW"] = string.Empty;
            serow["FourSE"] = string.Empty;
            this.sedt.Rows.Add(serow);
            this.sedt.AcceptChanges();

            ////Adding Command Grid
            if (this.cmddt.Columns.Count == 0)
            {
                this.cmddt.Columns.Add("Comments");
                this.cmddt.Columns.Add("LglID", System.Type.GetType("System.Int32"));
                this.cmddt.Columns.Add("InstID", System.Type.GetType("System.Int32"));
            }

            DataRow cmdrow;
            cmdrow = this.cmddt.NewRow();
            cmdrow["Comments"] = string.Empty;
            this.cmddt.Rows.Add(cmdrow);
            this.cmddt.AcceptChanges();

            this.loadFSLegalGridDS.Tables.Add(this.subsectiondt);
            this.loadFSLegalGridDS.Tables.Add(this.nedt);
            this.loadFSLegalGridDS.Tables.Add(this.nwdt);
            this.loadFSLegalGridDS.Tables.Add(this.swdt);
            this.loadFSLegalGridDS.Tables.Add(this.sedt);
            this.loadFSLegalGridDS.Tables.Add(this.cmddt);
        }

        /// <summary>
        /// Customises the D ata grid.
        /// </summary>
        private void CustomiseDAtaGrid()
        {
            this.SectiondataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection SectionGridcolumns = this.SectiondataGridView.Columns;
            SectionGridcolumns["SubName"].DataPropertyName = "SubID";
            SectionGridcolumns["Lot"].DataPropertyName = "Lot";
            SectionGridcolumns["LotP"].DataPropertyName = "LotP";
            SectionGridcolumns["Block"].DataPropertyName = "Block";
            SectionGridcolumns["BlockP"].DataPropertyName = "BlockP";
            //(SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).se      
            this.NEdataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection NEcolumns = this.NEdataGridView.Columns;
            NEcolumns["NEFirst"].DataPropertyName = "NEFirst";
            NEcolumns["NWFirst"].DataPropertyName = "NWFirst";
            NEcolumns["SWFirst"].DataPropertyName = "SWFirst";
            NEcolumns["SEFirst"].DataPropertyName = "SEFirst";

            this.NWdataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection NWcolumns = this.NWdataGridView.Columns;
            NWcolumns["NESEC"].DataPropertyName = "NESEC";
            NWcolumns["NWSEC"].DataPropertyName = "NWSEC";
            NWcolumns["SWSEC"].DataPropertyName = "SWSEC";
            NWcolumns["SESEC"].DataPropertyName = "SESEC";

            this.SWdataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection SWcolumns = this.SWdataGridView.Columns;
            SWcolumns["ThirdNE"].DataPropertyName = "ThirdNE";
            SWcolumns["ThirdNW"].DataPropertyName = "ThirdNW";
            SWcolumns["ThirdSW"].DataPropertyName = "ThirdSW";
            SWcolumns["ThirdSE"].DataPropertyName = "ThirdSE";

            this.SEdataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection SEcolumns = this.SEdataGridView.Columns;
            SEcolumns["FourNE"].DataPropertyName = "FourNE";
            SEcolumns["FourNW"].DataPropertyName = "FourNW";
            SEcolumns["FourSW"].DataPropertyName = "FourSW";
            SEcolumns["FourSE"].DataPropertyName = "FourSE";
            // Coding added for the issue 4521.c button should not get display on instrument search engine
            if (!this.multirow)
            {
                this.commentsgridhide();
            }
            this.CommentsdataGridView.AutoGenerateColumns = false;
           
            if (this.commentshide)
            {
                DataGridViewColumnCollection Commentscolumns = this.CommentsdataGridView.Columns;
                Commentscolumns["Comments"].DataPropertyName = "Comments";
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the SectiondataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SectiondataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.multirow)
                {

                    if (((e.RowIndex + 1) == this.SectiondataGridView.Rows.Count) && (e.ColumnIndex == 0))
                    {
                        if (!string.IsNullOrEmpty(this.SectiondataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim())
                            && Convert.ToInt32(this.SectiondataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) > 0)
                        {
                            this.loadFSLegalGridDS.Tables[0].Rows.InsertAt(this.loadFSLegalGridDS.Tables[0].NewRow(), this.loadFSLegalGridDS.Tables[0].Rows.Count);
                            this.loadFSLegalGridDS.Tables[1].Rows.InsertAt(this.loadFSLegalGridDS.Tables[1].NewRow(), this.loadFSLegalGridDS.Tables[1].Rows.Count);
                            this.loadFSLegalGridDS.Tables[2].Rows.InsertAt(this.loadFSLegalGridDS.Tables[2].NewRow(), this.loadFSLegalGridDS.Tables[2].Rows.Count);
                            this.loadFSLegalGridDS.Tables[3].Rows.InsertAt(this.loadFSLegalGridDS.Tables[3].NewRow(), this.loadFSLegalGridDS.Tables[3].Rows.Count);
                            this.loadFSLegalGridDS.Tables[4].Rows.InsertAt(this.loadFSLegalGridDS.Tables[4].NewRow(), this.loadFSLegalGridDS.Tables[4].Rows.Count);
                            this.loadFSLegalGridDS.Tables[5].Rows.InsertAt(this.loadFSLegalGridDS.Tables[5].NewRow(), this.loadFSLegalGridDS.Tables[5].Rows.Count);
                            this.secgridedit = true;
                            //// this.SectiondataGridView.DataSource = this.loadFSLegalGridDS.Tables["SubDivisionTable"];
                            this.ResizeGrid();

                            //// this.loadFSLegalGridDS.Tables[2].AcceptChanges();
                        }
                    }
                }
                //// Checking Lot Column - If it is other than numberic then it will zero
                //// int lot;
                //// if (!string.IsNullOrEmpty(this.SectiondataGridView.Rows[e.RowIndex].Cells[1].Value.ToString().Trim()))
                //// {
                ////    int.TryParse(this.SectiondataGridView.Rows[e.RowIndex].Cells[1].Value.ToString().Trim(), out lot);
                ////    if (lot == 0)
                ////    {
                ////        this.SectiondataGridView.Rows[e.RowIndex].Cells[1].Value = DBNull.Value;  
                ////    }
                //// }
                ////// Checking Block Column - If it is other than numberic then it will zero
                //// int block;
                //// if (!string.IsNullOrEmpty(this.SectiondataGridView.Rows[e.RowIndex].Cells[3].Value.ToString().Trim()))
                //// {
                ////    int.TryParse(this.SectiondataGridView.Rows[e.RowIndex].Cells[3].Value.ToString().Trim(), out block);
                ////    if (block == 0)
                ////    {
                ////        this.SectiondataGridView.Rows[e.RowIndex].Cells[3].Value = DBNull.Value;
                ////    }
                //// }
            }
            catch (Exception ex)
            {

            }

            if (this.SectionGridEndEdit != null)
            {
                this.SectionGridEndEdit(this, new System.Windows.Forms.DataGridViewCellCancelEventArgs(e.ColumnIndex, e.RowIndex));
            }
        }

        /// <summary>
        /// Commentsgridhides this instance.
        /// </summary>
        private void commentsgridhide()
        {
            if (this.commentshide)
            {
                this.Commentpanel.Visible = true;
                this.Commentheaderpanel.Visible = true;
                this.CommentsdataGridView.Visible = true;
                this.commentshide = true;
            }
            else
            {
                this.Commentpanel.Visible = false;
                this.Commentheaderpanel.Visible = false;
                this.CommentsdataGridView.Visible = false;
                this.commentshide = false;
            }
        }

        /// <summary>
        /// Rows the selection.
        /// </summary>
        private void RowSelection()
        {
            if (!string.IsNullOrEmpty(this.SectiondataGridView.Rows[0].Cells[0].Value.ToString().Trim()))
            {
                this.SectiondataGridView.Rows[0].Selected = true;
                this.NEdataGridView.Rows[0].Selected = true;
                this.NWdataGridView.Rows[0].Selected = true;
                this.SEdataGridView.Rows[0].Selected = true;
                this.SWdataGridView.Rows[0].Selected = true;
            }
            else
            {
                this.SectiondataGridView.Rows[0].Selected = false;
                this.NEdataGridView.Rows[0].Selected = false;
                this.NWdataGridView.Rows[0].Selected = false;
                this.SEdataGridView.Rows[0].Selected = false;
                this.SWdataGridView.Rows[0].Selected = false;
            }
        }

        /// <summary>
        /// InvokeScrollEvent
        /// </summary>
        public void InvokeScrollEvent()
        {
            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).Scroll += new ScrollEventHandler(this.FSLegalGridUserControl_Scroll);
                ////((Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).MouseWheel +=new MouseEventHandler(this.FSLegalGridUserControl_MouseWheel); 
            }
        }

        #endregion

        #region Events

        #region FSLegalGridUserControl

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the FSLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FSLegalGridUserControl_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (SectionGridSelectionChangeEvent != null)
            {
                this.SectionGridSelectionChangeEvent(this, e);

            }
        }

        /// <summary>
        /// Handles the KeyDown event of the FSLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FSLegalGridUserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (SectionGridKeyDownEvent != null)
            {
       
                this.SectionGridKeyDownEvent(this, e);
            }
         
        }

        /// <summary>
        /// Handles the KeyPress event of the FSLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void FSLegalGridUserControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SectionKeyPressEvent != null)
            {
                this.SectionKeyPressEvent(sender, e);
            }
            //throw new Exception("The method or operation is not implemented.");
        }

        #endregion FSLegalGridUserControl

        #region SectionGrid

        /// <summary>
        /// Handles the EditingControlShowing event of the SectiondataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void SectiondataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);

            if (this.SectionGridEditingControlShowing != null)
            {
                this.SectionGridEditingControlShowing(this, new System.Windows.Forms.DataGridViewEditingControlShowingEventArgs(e.Control, e.CellStyle));
            }
            ////set color to editing control

            if (e.Control is DataGridViewComboBoxEditingControl)
            {

                secgridview = true;
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).MaxLength = 50;
                ((ComboBox)e.Control).SelectionChangeCommitted -= new System.EventHandler(FSLegalGridUserControl_SelectionChangeCommitted);
                ((ComboBox)e.Control).SelectionChangeCommitted += new System.EventHandler(FSLegalGridUserControl_SelectionChangeCommitted);
                ((ComboBox)e.Control).KeyUp -= new KeyEventHandler(FSLegalGridUserControl_KeyUp); 
                ((ComboBox)e.Control).KeyUp += new KeyEventHandler(FSLegalGridUserControl_KeyUp); 

  
               ((ComboBox)e.Control).KeyDown -= new KeyEventHandler(FSLegalGridUserControl_KeyDown);
                ((ComboBox)e.Control).KeyDown += new KeyEventHandler(FSLegalGridUserControl_KeyDown);
                ((ComboBox)e.Control).KeyPress += new KeyPressEventHandler(FSLegalGridUserControl_KeyPress);
                ((ComboBox)e.Control).PreviewKeyDown += new PreviewKeyDownEventHandler(FSLegalGridUserControl_PreviewKeyDown);
            }



            // ((CheckBox)e.Control).CheckStateChanged += new System.EventHandler(SectionControl_CheckStateChanged);
            e.Control.TextChanged -= new System.EventHandler(FSLegalGridUserControl_TextChanged);
            e.Control.TextChanged += new System.EventHandler(FSLegalGridUserControl_TextChanged);
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(SectionControl_PreviewKeyDown);
            e.Control.KeyPress += new KeyPressEventHandler(SectionControl_KeyPress);
            //e.Control.MouseClick += new MouseEventHandler(SectionControl_MouseClick);
            
            e.Control.MouseLeave += new System.EventHandler(SectionControl_MouseLeave);
        }

        private void FSLegalGridUserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.multirow)
            {
                //if (e.KeyData.Equals(Keys.Enter))
                //{
                //    this.tabkey = true;
                //}
                //else
                //{
                //    this.tabkey = false;
                //}

                if (SectiondataGridView.CurrentCell.GetType() == typeof(DataGridViewComboBoxCell) && e.KeyData.Equals(Keys.Enter))
                {
                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)SectiondataGridView.Rows[SectiondataGridView.CurrentRow.Index].Cells[SectiondataGridView.CurrentCell.ColumnIndex];
                    if ((SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).DataSource != null)
                    {
                        try
                        {
                            DataTable secTion = (DataTable)((SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).DataSource);
                            DataRow[] subDivision = secTion.Select("SubName ='" + SectiondataGridView.CurrentCell.EditedFormattedValue.ToString().Replace("'", "''") + "'");
                            if (subDivision.Length > 0 && Convert.ToInt32(subDivision[0][0].ToString()) >= 0)
                            {
                                cell.Value = Convert.ToInt32(subDivision[0][0].ToString());
                                SectiondataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                
                                if (SectiondataGridView.CurrentRow.Index < SectiondataGridView.Rows.Count - 1)
                                {
                                    SectiondataGridView.Rows[SectiondataGridView.CurrentRow.Index + 1].Selected = true;
                                    SectiondataGridView.CurrentCell = SectiondataGridView.Rows[SectiondataGridView.CurrentRow.Index + 1].Cells[0];
                                    //SectiondataGridView.Rows[SectiondataGridView.CurrentRow.Index + 1].Cells[0].Selected = true;
                                    SectiondataGridView.BeginEdit(true);  
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                }

            }
        }

        private void FSLegalGridUserControl_SelectedValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show("congruent"); 
        }


        private void SectionControl_MouseLeave(object sender, EventArgs e)
        {
            if (SectionCheckedStateChanged != null)
            {
                this.SectionCheckedStateChanged(sender, e);
            }
        }

        private void SectionControl_MouseClick(object sender, MouseEventArgs e)
        {
            //if (SectionCheckedStateChanged != null)
            //{
            //    this.SectionCheckedStateChanged(sender, e);
            //}  
        }

        private void SectionControl_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void SectionControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SectionKeyPressEvent != null)
            {
                this.SectionKeyPressEvent(sender, e);
            }
        }

        private void SectionControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (SectionPKeyDownEvent != null)
            {
                if (e.KeyCode != Keys.Delete)
                {

                    this.SectionPKeyDownEvent(sender, e);
                }

            }
            //else
            //{
            //    if (this.SectiondataGridView[this.SectiondataGridView.CurrentCell.ColumnIndex, this.SectiondataGridView.CurrentCell.RowIndex].Value.ToString() != "")
            //    {
            //        this.SectiondataGridView.AllowUserToDeleteRows = true;
            //        this.NWdataGridView.AllowUserToDeleteRows = true;
            //        this.NEdataGridView.AllowUserToDeleteRows = true;
            //        this.SWdataGridView.AllowUserToDeleteRows = true;
            //        this.SEdataGridView.AllowUserToDeleteRows = true;
            //        this.CommentsdataGridView.AllowUserToDeleteRows = true;
            //    }
            //    else
            //    {
            //        this.SectiondataGridView.AllowUserToDeleteRows = false;
            //        this.NWdataGridView.AllowUserToDeleteRows = false;
            //        this.NEdataGridView.AllowUserToDeleteRows = false;
            //        this.SWdataGridView.AllowUserToDeleteRows = false;
            //        this.SEdataGridView.AllowUserToDeleteRows = false;
            //        this.CommentsdataGridView.AllowUserToDeleteRows = false;
            //    }
            //}
        }

        /// <summary>
        /// Handles the TextChanged event of the FSLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FSLegalGridUserControl_TextChanged(object sender, EventArgs e)
        {
            if (SectionTextChanged != null)
            {
                this.SectionTextChanged(sender, e);
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the FSLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void FSLegalGridUserControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (SectionPreviewKeyDownEvent != null)
            {
                this.SectionPreviewKeyDownEvent(sender, e);
            }

            //throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Handles the KeyDown event of the SectiondataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SectiondataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.Validate())
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            {
                               
                                       
                               if (this.SecGridKeyUp != null)
                                {
                                   

                                  this.SecGridKeyUp(this, new System.Windows.Forms.KeyEventArgs(Keys.Enter));
                                 
                                }

                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                }

                if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the SectiondataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SectiondataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (SectionGridCellContentClick != null)
            //{
            //    this.SectionGridCellContentClick(sender, e);
            //}
            if (SectionKeyPressEvent != null)
            {
                if (e.ColumnIndex.Equals(2) || e.ColumnIndex.Equals(4))
                {
                    KeyPressEventArgs eventArg = new KeyPressEventArgs((char)Keys.Up);
                    this.SectionKeyPressEvent(sender, eventArg);
                }

                if (e.ColumnIndex.Equals(4))
                {
                    // this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                    this.NEdataGridView.CurrentCell = this.NEdataGridView.Rows[e.RowIndex].Cells[0];
                    this.NEdataGridView.Focus();
                    this.NEdataGridView.BeginEdit(true);
                }
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the SectiondataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SectiondataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //this.SectiondataGridView_CellEndEdit(sender, e);
            if (this.CommentsdataGridView.Rows.Count > 0 && this.SectiondataGridView.Rows.Count > 0 && this.NEdataGridView.Rows.Count > 0 && this.NWdataGridView.Rows.Count > 0 && this.SEdataGridView.Rows.Count > 0 && this.SWdataGridView.Rows.Count > 0)
            {
                if (this.CommentsdataGridView.CurrentCell != null)
                {
                    this.CommentsdataGridView.Rows[e.RowIndex].Cells[this.CommentsdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.CommentsdataGridView.CurrentCell = null;
                }

                if (this.NEdataGridView.CurrentCell != null)
                {
                    this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.NEdataGridView.CurrentCell = null;
                }

                if (this.NWdataGridView.CurrentCell != null)
                {
                    this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.NWdataGridView.CurrentCell = null;
                }

                if (this.SEdataGridView.CurrentCell != null)
                {
                    this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////this.SEdataGridView.CurrentCell = null;
                }

                if (this.SWdataGridView.CurrentCell != null)
                {
                    this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////this.SWdataGridView.CurrentCell = null;
                }

                // this.SEdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex].Selected = false;
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the SectiondataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SectiondataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            ////this.SectiondataGridView.Focus();  
            if (this.multirow)
            {

                if (((e.RowIndex + 1) == this.SectiondataGridView.Rows.Count) && (e.ColumnIndex == 0) && SectiondataGridView.CurrentCell.GetType() ==typeof(DataGridViewComboBoxCell))
                {
                    if (!string.IsNullOrEmpty(this.SectiondataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim())
                        && Convert.ToInt32(this.SectiondataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()) > 0)
                    {
                        this.loadFSLegalGridDS.Tables[0].Rows.InsertAt(this.loadFSLegalGridDS.Tables[0].NewRow(), this.loadFSLegalGridDS.Tables[0].Rows.Count);
                        this.loadFSLegalGridDS.Tables[1].Rows.InsertAt(this.loadFSLegalGridDS.Tables[1].NewRow(), this.loadFSLegalGridDS.Tables[1].Rows.Count);
                        this.loadFSLegalGridDS.Tables[2].Rows.InsertAt(this.loadFSLegalGridDS.Tables[2].NewRow(), this.loadFSLegalGridDS.Tables[2].Rows.Count);
                        this.loadFSLegalGridDS.Tables[3].Rows.InsertAt(this.loadFSLegalGridDS.Tables[3].NewRow(), this.loadFSLegalGridDS.Tables[3].Rows.Count);
                        this.loadFSLegalGridDS.Tables[4].Rows.InsertAt(this.loadFSLegalGridDS.Tables[4].NewRow(), this.loadFSLegalGridDS.Tables[4].Rows.Count);
                        this.loadFSLegalGridDS.Tables[5].Rows.InsertAt(this.loadFSLegalGridDS.Tables[5].NewRow(), this.loadFSLegalGridDS.Tables[5].Rows.Count);
                        //this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Focus(); 
                        this.secgridedit = true;

                        //// this.SectiondataGridView.DataSource = this.loadFSLegalGridDS.Tables["SubDivisionTable"];
                        this.ResizeGrid();
                        //// this.loadFSLegalGridDS.Tables[2].AcceptChanges();
                    }
                }
            }
            
            if (this.SectionGridEdit != null)
            {
                this.SectionGridEdit(this, new System.Windows.Forms.DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
                
            }
            //this.SectiondataGridView.Focus();
            //if (secgridview)
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[1];
            //        this.SectiondataGridView.Focus();
            //        this.SectiondataGridView.BeginEdit(true);
            //    }
            //    else if (e.ColumnIndex == 1)
            //    {
            //        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[2];
            //        this.SectiondataGridView.Focus();
            //        this.SectiondataGridView.BeginEdit(true);
            //    }
            //    else if (e.ColumnIndex == 2)
            //    {
            //        currentrowindex = e.RowIndex;
            //        currentcolumnindex = 2;
            //        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[3];
            //        this.SectiondataGridView.Focus();
            //        this.SectiondataGridView.BeginEdit(true);
            //    }
            //    else if (e.ColumnIndex == 3)
            //    {
            //        currentcolumnindex = 3;
            //        currentrowindex = e.RowIndex;
            //        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[4];
            //        this.SectiondataGridView.Focus();
            //        this.SectiondataGridView.BeginEdit(true);
            //    }
            //}
           
        }

        /// <summary>
        /// Handles the DataError event of the SectiondataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void SectiondataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion SectionGrid

        #region NWdataGridView

        /// <summary>
        /// Handles the KeyDown event of the NWdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void NWdataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.Validate())
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            {
                                if (this.NWGridKeyUp != null)
                                {
                                    this.NWGridKeyUp(this, new System.Windows.Forms.KeyEventArgs(Keys.Enter));
                                }

                                break;
                            }

                        default:
                            {
                                break;
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
        /// Handles the EditingControlShowing event of the NWdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void NWdataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
            if (this.NWGridEditingControlShowing != null)
            {
                this.NWGridEditingControlShowing(this, new System.Windows.Forms.DataGridViewEditingControlShowingEventArgs(e.Control, e.CellStyle));
            }

            e.Control.TextChanged += new System.EventHandler(NWControl_TextChanged);
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(NWControl_PreviewKeyDown);
            e.Control.KeyPress += new KeyPressEventHandler(NWControl_KeyPress);
        }

        private void NWControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (NWKeyPressEvent != null && e.KeyChar != (char)Keys.Tab)
            {
                this.NWKeyPressEvent(sender, e);
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the NWControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void NWControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (NWPreviewKeyDownEvent != null)
            {
                this.NWPreviewKeyDownEvent(sender, e);
            }
            else if (NWKeyPressEvent != null && e.KeyCode == Keys.Delete)
            {
                KeyPressEventArgs eventArg = new KeyPressEventArgs((char)Keys.Delete);
                
                this.NWKeyPressEvent(sender, eventArg);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the NWControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NWControl_TextChanged(object sender, EventArgs e)
        {
            if (NWTextChanged != null)
            {
                this.NWTextChanged(sender, e);
            }

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the NWdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void NWdataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (this.NWGridEndEdit != null)
            //{
            //    this.NWGridEndEdit(this, new System.Windows.Forms.DataGridViewCellCancelEventArgs(e.ColumnIndex, e.RowIndex));
            //}
        }

        /// <summary>
        /// Handles the CellValueChanged event of the NWdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void NWdataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.NWGridBeginEdit != null)
            {
                this.NWGridBeginEdit(this, new System.Windows.Forms.DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the NWdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void NWdataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.CommentsdataGridView.Rows.Count > 0 && this.SectiondataGridView.Rows.Count > 0 && this.NEdataGridView.Rows.Count > 0 && this.NWdataGridView.Rows.Count > 0 && this.SEdataGridView.Rows.Count > 0 && this.SWdataGridView.Rows.Count > 0)
            {
                if (this.SectiondataGridView.CurrentCell != null)
                {
                    this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////  this.SectiondataGridView.CurrentCell = null;
                }

                if (this.NEdataGridView.CurrentCell != null)
                {
                    this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.NEdataGridView.CurrentCell = null;
                }

                if (this.SEdataGridView.CurrentCell != null)
                {
                    this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////  this.SEdataGridView.CurrentCell = null;
                }

                if (this.SWdataGridView.CurrentCell != null)
                {
                    this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.SWdataGridView.CurrentCell = null;
                }

                if (this.CommentsdataGridView.CurrentCell != null)
                {
                    this.CommentsdataGridView.Rows[e.RowIndex].Cells[this.CommentsdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.CommentsdataGridView.CurrentCell = null;
                }
            }
        }
        #endregion NWdataGridView

        #region NEdataGridView

        /// <summary>
        /// Handles the EditingControlShowing event of the NEdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void NEdataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
            if (this.NEGridEditingControlShowing != null)
            {
                this.NEGridEditingControlShowing(this, new System.Windows.Forms.DataGridViewEditingControlShowingEventArgs(e.Control, e.CellStyle));
            }
            e.Control.TextChanged += new System.EventHandler(NEControl_TextChanged);
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(NEControl_PreviewKeyDown);
            e.Control.KeyPress += new KeyPressEventHandler(NEControl_KeyPress);
            //TextBox txtbox = e.Control as TextBox;
            //if (txtbox != null)
            //{
            //    txtbox.KeyDown -= new KeyEventHandler(txtbox_KeyDown);
            //    txtbox.KeyDown += new KeyEventHandler(txtbox_KeyDown);
            //}
        }

        private void txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void NEControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (NEKeyPressEvent != null && e.KeyChar != 9)
            {
                this.NEKeyPressEvent(sender, e);
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the NEControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void NEControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (NEPreviewKeyDownEvent != null)
            {
                this.NEPreviewKeyDownEvent(sender, e);
            }
            else if (NEKeyPressEvent != null && e.KeyCode == Keys.Delete)
            {
                KeyPressEventArgs eventArg = new KeyPressEventArgs((char)Keys.Delete);
                this.NEKeyPressEvent(sender, eventArg);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the NEControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NEControl_TextChanged(object sender, EventArgs e)
        {
            if (NETextChanged != null)
            {
                this.NETextChanged(sender, e);
            }

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the NEdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void NEdataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (this.NEGridEndEdit != null)
            //{
            //    this.NEGridEndEdit(this, new System.Windows.Forms.DataGridViewCellCancelEventArgs(e.ColumnIndex, e.RowIndex));
            //}
            //if (e.ColumnIndex.Equals(3))
            //{
            //    this.NEdataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
            //    this.NEdataGridView.CurrentCell = null;
            //}
        }
        private void NEdataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.NEGridBeginEdit != null)
            {
                this.NEGridBeginEdit(this, new System.Windows.Forms.DataGridViewCellCancelEventArgs(e.ColumnIndex, e.RowIndex));
            }
        }
        /// <summary>
        /// Handles the RowEnter event of the NEdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void NEdataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.CommentsdataGridView.Rows.Count > 0 && this.SectiondataGridView.Rows.Count > 0 && this.NEdataGridView.Rows.Count > 0 && this.NWdataGridView.Rows.Count > 0 && this.SEdataGridView.Rows.Count > 0 && this.SWdataGridView.Rows.Count > 0)
            {
                if (this.SectiondataGridView.CurrentCell != null)
                {
                    this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.SectiondataGridView.CurrentCell = null;
                }

                if (this.NWdataGridView.CurrentCell != null)
                {
                    this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////  this.NWdataGridView.CurrentCell = null;
                }

                if (this.SEdataGridView.CurrentCell != null)
                {
                    this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.SEdataGridView.CurrentCell = null;
                }

                if (this.SWdataGridView.CurrentCell != null)
                {
                    this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.SWdataGridView.CurrentCell = null;
                }

                if (this.CommentsdataGridView.CurrentCell != null)
                {
                    this.CommentsdataGridView.Rows[e.RowIndex].Cells[this.CommentsdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.CommentsdataGridView.CurrentCell = null;
                }
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the NEdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void NEdataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.NEGridBeginEdit  != null)
            {
                this.NEGridBeginEdit(this, new System.Windows.Forms.DataGridViewCellCancelEventArgs(e.ColumnIndex, e.RowIndex));
            }
        }
        #endregion NEdataGridView

        #region SWdataGridView

        /// <summary>
        /// Handles the KeyDown event of the SWdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SWdataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.Validate())
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            {
                                if (this.SWGridKeyUp != null)
                                {
                                    this.SWGridKeyUp(this, new System.Windows.Forms.KeyEventArgs(Keys.Enter));
                                }

                                break;
                            }

                        default:
                            {
                                break;
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
        /// Handles the EditingControlShowing event of the SWdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void SWdataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
            if (this.SWGridEditingControlShowing != null)
            {
                this.SWGridEditingControlShowing(this, new System.Windows.Forms.DataGridViewEditingControlShowingEventArgs(e.Control, e.CellStyle));
            }

            e.Control.TextChanged += new System.EventHandler(SWControl_TextChanged);
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(SWControl_PreviewKeyDown);
            e.Control.KeyPress += new KeyPressEventHandler(SWControl_KeyPress);
        }

        private void SWControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SWKeyPressEvent != null && e.KeyChar != 9)
            {
                this.SWKeyPressEvent(sender, e);
            }
        }

        private void SWControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (SWPreviewKeyDownEvent != null)
            {
                this.SWPreviewKeyDownEvent(sender, e);
            }
            else if (SWKeyPressEvent != null && e.KeyCode == Keys.Delete)
            {
                KeyPressEventArgs eventArg = new KeyPressEventArgs((char)Keys.Delete);
                this.SWKeyPressEvent(sender, eventArg);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the SWControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SWControl_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the CellEndEdit event of the SWdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SWdataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (this.SWGridEndEdit != null)
            //{
            //    this.SWGridEndEdit(this, new System.Windows.Forms.DataGridViewCellCancelEventArgs(e.ColumnIndex, e.RowIndex));
            //}
        }

        /// <summary>
        /// Handles the RowEnter event of the SWdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SWdataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.CommentsdataGridView.Rows.Count > 0 && this.SectiondataGridView.Rows.Count > 0 && this.NEdataGridView.Rows.Count > 0 && this.NWdataGridView.Rows.Count > 0 && this.SEdataGridView.Rows.Count > 0 && this.SWdataGridView.Rows.Count > 0)
            {
                if (this.SectiondataGridView.CurrentCell != null)
                {
                    this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////  this.SectiondataGridView.CurrentCell = null;
                }

                if (this.NEdataGridView.CurrentCell != null)
                {
                    this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.NEdataGridView.CurrentCell = null;
                }

                if (this.SEdataGridView.CurrentCell != null)
                {
                    this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////this.SEdataGridView.CurrentCell = null;
                }

                if (this.NWdataGridView.CurrentCell != null)
                {
                    this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.NWdataGridView.CurrentCell = null;
                }

                if (this.CommentsdataGridView.CurrentCell != null)
                {
                    this.CommentsdataGridView.Rows[e.RowIndex].Cells[this.CommentsdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////  this.CommentsdataGridView.CurrentCell = null;
                }
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the SWdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SWdataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.SWGridBeginEdit != null)
            {
                this.SWGridBeginEdit(this, new System.Windows.Forms.DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
            }
        }
        #endregion SWdataGridView

        #region SEdataGridView

        /// <summary>
        /// Handles the KeyDown event of the SEdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SEdataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.Validate())
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            {
                                if (this.SEGridKeyUp != null)
                                {
                                    this.SEGridKeyUp(this, new System.Windows.Forms.KeyEventArgs(Keys.Enter));
                                }

                                break;
                            }

                        default:
                            {
                                break;
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
        /// Handles the EditingControlShowing event of the SEdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void SEdataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
            if (this.SEGridEditingControlShowing != null)
            {
                this.SEGridEditingControlShowing(this, new System.Windows.Forms.DataGridViewEditingControlShowingEventArgs(e.Control, e.CellStyle));
            }
            e.Control.TextChanged += new System.EventHandler(SEControl_TextChanged);
            e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(SEControl_PreviewKeyDown);
            e.Control.KeyPress += new KeyPressEventHandler(SEControl_KeyPress);
        }

        private void SEControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SEKeyPressEvent != null && e.KeyChar != 9)
            {
                this.SEKeyPressEvent(sender, e);
            }
        }

        private void SEControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (SEPreviewKeyDownEvent != null)
            {
                this.SEPreviewKeyDownEvent(sender, e);
            }
            else if (SEKeyPressEvent != null && e.KeyCode == Keys.Delete)
            {
                KeyPressEventArgs eventArg = new KeyPressEventArgs((char)Keys.Delete);
                this.SEKeyPressEvent(sender, eventArg);
            }
            //throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Handles the TextChanged event of the SEControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SEControl_TextChanged(object sender, EventArgs e)
        {
            if (SETextChanged != null)
            {
                this.SETextChanged(sender, e);
            }

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the SEdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SEdataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (this.SEGridEndEdit != null)
            //{
            //    this.SEGridEndEdit(this, new System.Windows.Forms.DataGridViewCellCancelEventArgs(e.ColumnIndex, e.RowIndex));
            //}
        }

        /// <summary>
        /// Handles the RowEnter event of the SEdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SEdataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.CommentsdataGridView.Rows.Count > 0 && this.SectiondataGridView.Rows.Count > 0 && this.NEdataGridView.Rows.Count > 0 && this.NWdataGridView.Rows.Count > 0 && this.SEdataGridView.Rows.Count > 0 && this.SWdataGridView.Rows.Count > 0)
            {
                if (this.SectiondataGridView.CurrentCell != null)
                {
                    this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////    this.SectiondataGridView.CurrentCell = null;
                }

                if (this.NEdataGridView.CurrentCell != null)
                {
                    this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////   this.NEdataGridView.CurrentCell = null;
                }

                if (this.SWdataGridView.CurrentCell != null)
                {
                    this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////   this.SWdataGridView.CurrentCell = null;
                }

                if (this.NWdataGridView.CurrentCell != null)
                {
                    this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////  this.NWdataGridView.CurrentCell = null;
                }

                if (this.CommentsdataGridView.CurrentCell != null)
                {
                    this.CommentsdataGridView.Rows[e.RowIndex].Cells[this.CommentsdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////  this.CommentsdataGridView.CurrentCell = null;
                }
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the SEdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SEdataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.SEGridBeginEdit != null)
            {
                this.SEGridBeginEdit(this, new System.Windows.Forms.DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
            }
        }
        #endregion SEdataGridView

        #region CommentsdataGridView


        /// <summary>
        /// Handles the DataError event of the CommentsdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void CommentsdataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the CommentsdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CommentsdataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.CommentsdataGridView.AutoGenerateColumns = false;
            if (this.CommentsdataGridView.Rows.Count > 0 && this.SectiondataGridView.Rows.Count > 0 && this.NEdataGridView.Rows.Count > 0 && this.NWdataGridView.Rows.Count > 0 && this.SEdataGridView.Rows.Count > 0 && this.SWdataGridView.Rows.Count > 0)
            {
                if (this.SectiondataGridView.CurrentCell != null)
                {
                    this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////  this.SectiondataGridView.CurrentCell = null;
                }

                if (this.NEdataGridView.CurrentCell != null)
                {
                    this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////  this.NEdataGridView.CurrentCell = null;
                }

                if (this.SWdataGridView.CurrentCell != null)
                {
                    this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //// this.SWdataGridView.CurrentCell = null;
                }

                if (this.NWdataGridView.CurrentCell != null)
                {
                    this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////  this.NWdataGridView.CurrentCell = null;
                }

                if (this.SEdataGridView.CurrentCell != null)
                {
                    this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    ////  this.SEdataGridView.CurrentCell = null;
                }
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the CommentsdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CommentsdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //this.commentclickflag = true;
            //if (this.CommandImageCellClick != null)
            //{
            //    this.CommandImageCellClick(this, new System.Windows.Forms.DataGridViewCellEventArgs(CommentsdataGridView.Columns.Count - 1, CommentsdataGridView.CurrentRow.Index));
            //}
        }

        #endregion CommentsdataGridView

        private void SectiondataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        #endregion Events

        /// <summary>
        /// Handles the PreviewKeyDown event of the NEdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void NEdataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (this.Validate())
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            {
                                if (this.NEGridKeyUp != null)
                                {
                                    this.NEGridKeyUp(this, new System.Windows.Forms.KeyEventArgs(Keys.Enter));
                                }

                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        private void SectiondataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SectionGridCellContentDoubleClick != null)
            {
                this.SectionGridCellContentDoubleClick(sender, e);
            }
        }

        private void NEdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*Condition checked by Ramya */
            if (this.NEdataGridView.CurrentCell.ColumnIndex > 0 && this.NEdataGridView.CurrentCell.RowIndex > 0 && e.RowIndex > 0)
            {
                this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                this.NEdataGridView.CurrentCell = this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex];
                this.NEdataGridView.BeginEdit(true);
            }

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        private void NWdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*Condition checked by Ramya */
            if (this.NWdataGridView.CurrentCell.ColumnIndex > 0 && this.NWdataGridView.CurrentCell.RowIndex > 0 && e.RowIndex > 0)
            {
                this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                this.NWdataGridView.CurrentCell = this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex];
                this.NWdataGridView.BeginEdit(true);
            }

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        private void SWdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*Condition checked by Ramya */
            if (this.SWdataGridView.CurrentCell.ColumnIndex > 0 && this.SWdataGridView.CurrentCell.RowIndex > 0 && e.RowIndex > 0)
            {
                this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                this.SWdataGridView.CurrentCell = this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex];
                this.SWdataGridView.BeginEdit(true);
            }

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        private void SEdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*Condition checked by Ramya */
            if (this.SEdataGridView.CurrentCell.ColumnIndex > 0 && this.SEdataGridView.CurrentCell.RowIndex > 0 && e.RowIndex > 0)
            {
                this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                this.SEdataGridView.CurrentCell = this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex];
                this.SEdataGridView.BeginEdit(true);
            }

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(200, this.yaxisPoint);
            }
        }

        private void NEdataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex].Selected = false;

            // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
            // Set Active control as next grid to remove focus from current grid
            if (this.ActiveControl != null && this.ActiveControl.Equals(this.NEdataGridView) && this.NEdataGridView.CurrentCell != null
                && this.NEdataGridView.CurrentCell.ColumnIndex >= 0 && this.NEdataGridView.CurrentCell.RowIndex >= 0)
            {
                this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                this.NEdataGridView.CurrentCell = this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex];
                this.NEdataGridView.BeginEdit(true);
            }

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        private void NWdataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           // this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex].Selected = false;

            // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
            // Set Active control as next grid to remove focus from current grid
            if (this.ActiveControl != null && this.ActiveControl.Equals(this.NWdataGridView) && this.NWdataGridView.CurrentCell != null
                && this.NWdataGridView.CurrentCell.ColumnIndex >= 0 && this.NWdataGridView.CurrentCell.RowIndex >= 0)
            {
                this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                this.NWdataGridView.CurrentCell = this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex];
                this.NWdataGridView.BeginEdit(true);

                //if (this.NWdataGridView.CurrentCell.ColumnIndex == 0)
                //{
                //    this.NEdataGridView.ClearSelection();
                //}
            }

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        private void SWdataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
            // Set Active control as next grid to remove focus from current grid
            if (this.ActiveControl != null && this.ActiveControl.Equals(this.SWdataGridView) && this.SWdataGridView.CurrentCell != null
                && this.SWdataGridView.CurrentCell.ColumnIndex >= 0 && this.SWdataGridView.CurrentCell.RowIndex >= 0)
            {
                this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                this.SWdataGridView.CurrentCell = this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex];
                this.SWdataGridView.BeginEdit(true);
            }
            //this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex].Selected = false;

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        private void SEdataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
            // Set Active control as next grid to remove focus from current grid
            if (this.ActiveControl != null && this.ActiveControl.Equals(this.SEdataGridView) && this.SEdataGridView.CurrentCell != null
                && this.SEdataGridView.CurrentCell.ColumnIndex >= 0 && this.SEdataGridView.CurrentCell.RowIndex >= 0)
            {
                this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                this.SEdataGridView.CurrentCell = this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex];
                this.SEdataGridView.BeginEdit(true);
            }

            //this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex].Selected = false;

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(200, this.yaxisPoint);
            }
        }

        /* Added by Ramya For BudId 2479*/

        /// <summary>
        /// SectiondataGridView_RowHeaderMouseClick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SectiondataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                this.SectiondataGridView.CurrentCell = null;
                this.SectiondataGridView.Rows[e.RowIndex].Selected = true;
                this.NEdataGridView.Rows[e.RowIndex].Selected = true;
                this.NWdataGridView.Rows[e.RowIndex].Selected = true;
                this.SWdataGridView.Rows[e.RowIndex].Selected = true;
                this.SEdataGridView.Rows[e.RowIndex].Selected = true;
                this.CommentsdataGridView.Rows[e.RowIndex].Selected = true;

            }
            for (int j = 0; j <= this.NEdataGridView.Rows.Count - 1; j++)
            {
                if (j != e.RowIndex)
                {
                    this.NEdataGridView.Rows[j].Selected = false;

                    this.NWdataGridView.Rows[j].Selected = false;
                    this.SWdataGridView.Rows[j].Selected = false;
                    this.SEdataGridView.Rows[j].Selected = false;
                    this.CommentsdataGridView.Rows[j].Selected = false;
                }
            }
        }

        private void SectiondataGridView_AllowUserToDeleteRowsChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// SectiondataGridView_UserDeletingRow
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SectiondataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (SectionGridRowDeleteEvent != null)
            {
                this.SectionGridRowDeleteEvent(this, e);
            }
            e.Cancel = true;
        }

        private void SectiondataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                         
                //this.SectiondataGridView_CellEndEdit(sender, e);
                   
                this.currentcolumnindex = e.ColumnIndex;
                this.currentrowindex = e.RowIndex;
                if (e.ColumnIndex == 4)
                {
                   // this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                    this.NEdataGridView.CurrentCell = this.NEdataGridView.Rows[e.RowIndex].Cells[0];
                    this.NEdataGridView.Focus();
                    this.NEdataGridView.BeginEdit(true);
               }
             
                //if (e.ColumnIndex == 1)
                //{
                //    this.SectiondataGridView.Rows[e.RowIndex].Cells[2].Value = true; 
                //}
                //if (e.ColumnIndex == 1)
                //{
                //    this.SectiondataGridView.Rows[e.RowIndex].Cells[2].Value = true; 
                //    //if (this.SectiondataGridView.SectiondataGridView.Rows[e.RowIndex].Cells[2].Value.ToString())
                //    //{
                //    //    this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[3];
                //    //    this.SectiondataGridView.BeginEdit(true);
                //    //}
                //}
            }
            catch (Exception ex)
            {

            }
        }

        private void NEdataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == 3)
                //{
                //   // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
                //    // Set Active control as next grid to remove focus from current grid
                //    //this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                //    //this.ActiveControl = this.NWdataGridView;

                //    this.NWdataGridView.CurrentCell = this.NWdataGridView.Rows[e.RowIndex].Cells[0];
                //    this.NWdataGridView.Focus();
                //    this.NWdataGridView.BeginEdit(true);
                //}
            }
            catch (Exception ex)
            {

            }
        }

        private void NWdataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 3)
            //    {
            //        // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
            //        // Set Active control as next grid to remove focus from current grid
            //        //this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex].Selected = false;
            //        //this.ActiveControl = this.SWdataGridView;

            //        //this.SWdataGridView.CurrentCell = this.SWdataGridView.Rows[e.RowIndex].Cells[0];
            //        //this.SWdataGridView.Focus();
            //        //this.SWdataGridView.BeginEdit(true);
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void SWdataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 3)
            //    {
            //        // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
            //        // Set Active control as next grid to remove focus from current grid
            //        //this.ActiveControl = this.SEdataGridView;
            //        //this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex].Selected = false;

            //        this.SEdataGridView.CurrentCell = this.SEdataGridView.Rows[e.RowIndex].Cells[0];
            //        this.SEdataGridView.Focus();
            //        this.SEdataGridView.BeginEdit(true);
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void SEdataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 3)
            //    {
            //        // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
            //        // Set Active control as next grid to remove focus from current grid
            //        //this.ActiveControl = null;
            //        //this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex].Selected = false;

            //        this.CommentsdataGridView.CurrentCell = this.CommentsdataGridView.Rows[e.RowIndex].Cells[0];
            //        this.CommentsdataGridView.Focus();
            //        this.CommentsdataGridView.BeginEdit(true);
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        /// <summary>
        /// Handles the CellLeave event of the CommentsdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CommentsdataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (CommentsdataGridView.Rows.Count > 1)
                {
                    if (!this.commentclickflag)
                    {
                        if (e.ColumnIndex == 0)
                        {
                            //this.ActiveControl = this.SectiondataGridView;

                            if ((e.RowIndex + 1) <= CommentsdataGridView.Rows.Count - 1)
                            {
                                this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex + 1].Cells[0];
                                this.SectiondataGridView.Focus();
                                this.SectiondataGridView.BeginEdit(true);
                            }
                            else
                            {
                                this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[0].Cells[0];
                                this.SectiondataGridView.Focus();
                                this.SectiondataGridView.Rows[0].Cells[0].Selected = true;
                                // this.SectiondataGridView.BeginEdit(true);
                            }
                        }
                    }
                    this.commentclickflag = false;
                    //else
                    //{
                    //    this.CommentsdataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[0];
                    //    this.CommentsdataGridView.Focus();
                    //    this.CommentsdataGridView.BeginEdit(true);
                    //}
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SectiondataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.SectiondataGridView.CurrentCell.ColumnIndex >= 0 && this.SectiondataGridView.CurrentCell.RowIndex >= 0)
            {
                //this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                //this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex];
               // this.SectiondataGridView.Focus();

                this.SectiondataGridView.BeginEdit(true);
            }

            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        private void SectiondataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            //this.SectiondataGridView_CellEndEdit(sender, e); 
            if (e.ColumnIndex > 0)
            {
                this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex];
                //this.SectiondataGridView.Focus();
                this.SectiondataGridView.BeginEdit(true);
            }
            //if (e.ColumnIndex == 2)
            //{
            //    this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Value = true;
            //}

            this.SectiondataGridView.Rows[e.RowIndex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
 
            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        private void SectiondataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
           
            //if(this.multirow)// && this.tabkey)
            //    {
                if (SectiondataGridView.CurrentCell.GetType() ==typeof(DataGridViewComboBoxCell))
                {
                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)SectiondataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if ((SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).DataSource != null)
                    {
                        try
                        {
                            DataTable secTion = (DataTable)((SectiondataGridView.Columns["SubName"] as DataGridViewComboBoxColumn).DataSource);
                            DataRow[] subDivision = secTion.Select("SubName ='" + e.FormattedValue.ToString().Replace("'", "''") + "'");
                            if (subDivision.Length > 0 && Convert.ToInt32(subDivision[0][0].ToString())>=0)
                            {
                                cell.Value = Convert.ToInt32(subDivision[0][0].ToString());
                                SectiondataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            }
                        }
                        catch (Exception ex)
                        { 
                        }
                    }
                   
                }

                
                // if (((e.RowIndex + 1) == this.SectiondataGridView.Rows.Count) && (e.ColumnIndex == 0))
                //{

                //    if (!string.IsNullOrEmpty(this.SectiondataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()))
                //    {
                //        this.loadFSLegalGridDS.Tables[0].Rows.InsertAt(this.loadFSLegalGridDS.Tables[0].NewRow(), this.loadFSLegalGridDS.Tables[0].Rows.Count);
                //        this.loadFSLegalGridDS.Tables[1].Rows.InsertAt(this.loadFSLegalGridDS.Tables[1].NewRow(), this.loadFSLegalGridDS.Tables[1].Rows.Count);
                //        this.loadFSLegalGridDS.Tables[2].Rows.InsertAt(this.loadFSLegalGridDS.Tables[2].NewRow(), this.loadFSLegalGridDS.Tables[2].Rows.Count);
                //        this.loadFSLegalGridDS.Tables[3].Rows.InsertAt(this.loadFSLegalGridDS.Tables[3].NewRow(), this.loadFSLegalGridDS.Tables[3].Rows.Count);
                //        this.loadFSLegalGridDS.Tables[4].Rows.InsertAt(this.loadFSLegalGridDS.Tables[4].NewRow(), this.loadFSLegalGridDS.Tables[4].Rows.Count);
                //        this.loadFSLegalGridDS.Tables[5].Rows.InsertAt(this.loadFSLegalGridDS.Tables[5].NewRow(), this.loadFSLegalGridDS.Tables[5].Rows.Count);
                //        //this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Focus(); 
                //        this.secgridedit = true;

                //        //// this.SectiondataGridView.DataSource = this.loadFSLegalGridDS.Tables["SubDivisionTable"];
                //        this.ResizeGrid();
                //        //// this.loadFSLegalGridDS.Tables[2].AcceptChanges();
                //    }
                //}
 
            //}
        
            //this.tabkey = false;
            //try

            //{
            //    if (secgridview)
            //    {
            //        if (e.ColumnIndex == 0)
            //        {
            //            this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[1];
            //            this.SectiondataGridView.Focus();
            //            this.SectiondataGridView.BeginEdit(true);
            //        }
            //        else if (e.ColumnIndex == 1)
            //        {
            //            this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[2];
            //            this.SectiondataGridView.Focus();
            //            this.SectiondataGridView.BeginEdit(true);
            //        }
            //        else if (e.ColumnIndex == 2)
            //        {
            //            currentrowindex = e.RowIndex;
            //            currentcolumnindex = 2;
            //            this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[3];
            //            this.SectiondataGridView.Focus();
            //            this.SectiondataGridView.BeginEdit(true);
            //        }
            //        else if (e.ColumnIndex == 3)
            //        {
            //            this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[e.RowIndex].Cells[3];
            //            this.SectiondataGridView.Focus();
            //            this.SectiondataGridView.BeginEdit(true);
            //            currentcolumnindex = 3;
            //        }
            //    }
            //    //if (this.currentrowindex >= 0)
            //    //{
            //    //    if (this.currentcolumnindex == 2)
            //    //    {
            //    //        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[currentrowindex].Cells[currentcolumnindex + 1];
            //    //        this.SectiondataGridView.Focus();
            //    //        this.SectiondataGridView.BeginEdit(true);
            //    //    }
            //    //}
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void SectiondataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.currentrowindex >= 0)
                {
                    if (this.currentcolumnindex == 2)
                    {
                        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[currentrowindex].Cells[currentcolumnindex + 1];
                        this.SectiondataGridView.Focus();
                        this.SectiondataGridView.BeginEdit(true);
                     }
                }

                if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    ((Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                }
            }
            catch (Exception ex)
            {

            }
            //try
            //{
            //    if (this.currentrowindex >= 0)
            //    {
            //        if (this.currentcolumnindex == 2)
            //        {
            //            if (!this.tabindexset)
            //            {
            //                this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[currentrowindex].Cells[currentcolumnindex + 1];
            //                this.SectiondataGridView.Focus();
            //                this.SectiondataGridView.BeginEdit(true);
            //                this.tabindexset = true;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}

        }

        private void SectiondataGridView_TabIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.SectiondataGridView.Rows.Count > 0)
                {
                    if (this.currentcolumnindex == 0)
                    {
                        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[this.currentrowindex].Cells[1];
                        this.SectiondataGridView.Focus();
                        this.SectiondataGridView.CurrentCell.Selected = false;
                        this.SectiondataGridView.BeginEdit(true);
                    }
                    else if (this.currentcolumnindex == 1)
                    {
                        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[this.currentrowindex].Cells[2];
                        this.SectiondataGridView.Focus();
                        this.SectiondataGridView.CurrentCell.Selected = false;
                        this.SectiondataGridView.BeginEdit(true);
                    }
                    else if (this.currentcolumnindex == 2)
                    {
                        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[this.currentrowindex].Cells[3];
                        this.SectiondataGridView.Focus();
                        this.SectiondataGridView.CurrentCell.Selected = false;
                        this.SectiondataGridView.BeginEdit(true);
                    }
                    else if (this.currentcolumnindex == 3)
                    {
                        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[this.currentrowindex].Cells[4];
                        this.SectiondataGridView.Focus();
                        this.SectiondataGridView.CurrentCell.Selected = false;
                        this.SectiondataGridView.BeginEdit(true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SectiondataGridView_ColumnDataPropertyNameChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                if (this.SectiondataGridView.Rows.Count > 0)
                {
                    if (this.currentcolumnindex == 0)
                    {
                        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[this.currentrowindex].Cells[1];
                        this.SectiondataGridView.Focus();
                        this.SectiondataGridView.Rows[this.currentrowindex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                        this.SectiondataGridView.BeginEdit(true);
                    }
                    else if (this.currentcolumnindex == 1)
                    {
                        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[this.currentrowindex].Cells[2];
                        this.SectiondataGridView.Focus();
                        // this.SectiondataGridView.CurrentCell.Selected = false; 
                        this.SectiondataGridView.Rows[this.currentrowindex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                        this.SectiondataGridView.BeginEdit(true);
                    }
                    else if (this.currentcolumnindex == 2)
                    {
                        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[this.currentrowindex].Cells[3];
                        this.SectiondataGridView.Focus();
                        //this.SectiondataGridView.CurrentCell.Selected = false;
                        this.SectiondataGridView.Rows[this.currentrowindex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                        this.SectiondataGridView.BeginEdit(true);
                    }
                    else if (this.currentcolumnindex == 3)
                    {
                        this.SectiondataGridView.CurrentCell = this.SectiondataGridView.Rows[this.currentrowindex].Cells[4];
                        this.SectiondataGridView.Focus();
                        //this.SectiondataGridView.CurrentCell.Selected = false;
                        this.SectiondataGridView.Rows[this.currentrowindex].Cells[this.SectiondataGridView.CurrentCell.ColumnIndex].Selected = false;
                        this.SectiondataGridView.BeginEdit(true);
                    }
                    else if (this.currentcolumnindex == 4)
                    {
                        this.NEdataGridView.CurrentCell = this.NEdataGridView.Rows[this.currentrowindex].Cells[0];
                        this.NEdataGridView.Focus();
                        this.NEdataGridView.Rows[this.currentrowindex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                        this.NEdataGridView.BeginEdit(true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// Handles the MouseDown event of the CommentsdataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void CommentsdataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            //this.commentclickflag = true;
            //if (this.CommandImageCellClick != null)
            //{
            //    this.CommandImageCellClick(this, new System.Windows.Forms.DataGridViewCellEventArgs(CommentsdataGridView.Columns.Count - 1, CommentsdataGridView.CurrentRow.Index));
            //}
        }

        private void CommentsdataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.commentclickflag = true;
            if (this.CommandImageCellClick != null)
            {
                // this.CommandImageCellClick(this, new System.Windows.Forms.DataGridViewCellEventArgs(CommentsdataGridView.Columns.Count - 1, CommentsdataGridView.CurrentRow.Index));
                this.CommandImageCellClick(this, new System.Windows.Forms.DataGridViewCellEventArgs(this.commentcolumnindex, this.commentrowindex));
            }
        }
        //// Added for 4505 by Malliga
        private void CommentsdataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.commentcolumnindex = e.ColumnIndex;
            this.commentrowindex = e.RowIndex;
        }
        ////End Here

        private void FSLegalGridUserControl_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > 5)
            {
                this.yaxisPoint = e.NewValue;
            }
            else
            {
                this.yaxisPoint = 0;

            }
        }

        private void FSLegalGridUserControl_Load(object sender, EventArgs e)
        {
            ((Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).Scroll += new ScrollEventHandler(this.FSLegalGridUserControl_Scroll);
        }

        private void CommentsdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        private void NWdataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Tab))
            {
                // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
                // Set Active control as next grid to remove focus from current grid
                //this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                //this.ActiveControl = this.SWdataGridView;

                //this.SWdataGridView.CurrentCell = this.SWdataGridView.Rows[this.NWdataGridView.CurrentRow.Index].Cells[0];
                //this.SWdataGridView.Focus();
                //this.SWdataGridView.BeginEdit(true);
            }
        }

        private void NEdataGridView_TabKeyPress(Keys e)
        {
            try
            {
                if (this.NEdataGridView.CurrentCell.ColumnIndex == 3)
                {
                    // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
                    // Set Active control as next grid to remove focus from current grid
                    //this.NEdataGridView.Rows[e.RowIndex].Cells[this.NEdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //this.ActiveControl = this.NWdataGridView;
                    
                    this.NWdataGridView.CurrentCell = this.NWdataGridView.Rows[this.NEdataGridView.CurrentRow.Index].Cells[0];
                    this.NWdataGridView.Focus();
                    this.NWdataGridView.BeginEdit(true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void NWdataGridView_TabKeyPress(Keys e)
        {
            try
            {
                if (this.NWdataGridView.CurrentCell.ColumnIndex == 3)
                {
                    // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
                    // Set Active control as next grid to remove focus from current grid
                    //this.NWdataGridView.Rows[e.RowIndex].Cells[this.NWdataGridView.CurrentCell.ColumnIndex].Selected = false;
                    //this.ActiveControl = this.SWdataGridView;

                    this.SWdataGridView.CurrentCell = this.SWdataGridView.Rows[this.NWdataGridView.CurrentRow.Index].Cells[0];
                    this.SWdataGridView.Focus();
                    this.SWdataGridView.BeginEdit(true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SWdataGridView_TabKeyPress(Keys e)
        {
            try
            {
                if (this.SWdataGridView.CurrentCell.ColumnIndex == 3)
                {
                    // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
                    // Set Active control as next grid to remove focus from current grid
                    //this.ActiveControl = this.SEdataGridView;
                    //this.SWdataGridView.Rows[e.RowIndex].Cells[this.SWdataGridView.CurrentCell.ColumnIndex].Selected = false;

                    this.SEdataGridView.CurrentCell = this.SEdataGridView.Rows[this.SWdataGridView.CurrentRow.Index].Cells[0];
                    this.SEdataGridView.Focus();
                    this.SEdataGridView.BeginEdit(true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SEdataGridView_TabKeyPress(Keys e)
        {
            try
            {
                if (this.SEdataGridView.CurrentCell.ColumnIndex == 3)
                {
                    // To Fix issue #12319 (Fields should be highlighted in yellow and the curser should become visible)
                    // Set Active control as next grid to remove focus from current grid
                    //this.ActiveControl = null;
                    //this.SEdataGridView.Rows[e.RowIndex].Cells[this.SEdataGridView.CurrentCell.ColumnIndex].Selected = false;

                    this.CommentsdataGridView.CurrentCell = this.CommentsdataGridView.Rows[this.SEdataGridView.CurrentRow.Index].Cells[0];
                    this.CommentsdataGridView.Focus();
                    this.CommentsdataGridView.BeginEdit(true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SectiondataGridView_TabKeyPress(Keys e)
        {
            try
            {
                if (this.SectiondataGridView.CurrentCell.ColumnIndex == 4)
                {
                    this.NEdataGridView.CurrentCell = this.NEdataGridView.Rows[this.SectiondataGridView.CurrentCell.RowIndex].Cells[0];
                    this.NEdataGridView.Focus();
                    this.NEdataGridView.BeginEdit(true);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
