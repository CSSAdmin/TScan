namespace D3200
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Infragistics.Win;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;

    [SmartPart]
    public partial class F3201 : BaseSmartPart
    {

        #region Member Variables

        /// <summary>
        /// form3201Control Controller
        /// </summary>
        private F3201Controller form3201Control;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Hold form master number
        /// </summary>
        private int masterFormNumber;

        /// <summary>
        ///  used to store parcelId
        /// </summary>
        private int parcelId;

        /// <summary>
        /// The Page Mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Form Label Info string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// Sketch link dataset
        /// </summary>
        private F3201SketchLinkData sketchData = new F3201SketchLinkData();

        /// <summary>
        /// Terragon deatils which type is equals to 1
        /// </summary>
        private F3201SketchLinkData.TerragonDataDataTable terragonParentTable = new F3201SketchLinkData.TerragonDataDataTable();

        /// <summary>
        /// Terragon deatils which type is not equals to 1
        /// </summary>
        private F3201SketchLinkData.TerragonDataDataTable terragonChildTable = new F3201SketchLinkData.TerragonDataDataTable();

        /// <summary>
        /// Polygon deatils which type is equals to 3
        /// </summary>
        private F3201SketchLinkData.PolygonDataDataTable polygonParentTable = new F3201SketchLinkData.PolygonDataDataTable();

        /// <summary>
        /// Polygon deatils which type is not equals to 3
        /// </summary>
        private F3201SketchLinkData.PolygonDataDataTable polygonChildTable = new F3201SketchLinkData.PolygonDataDataTable();

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

        /// <summary>
        /// Save process
        /// </summary>
        private bool isSaveProcess = false;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F3201"/> class.
        /// </summary>
        public F3201()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F3201"/> class.
        /// </summary>
        /// <param name="currentParcel">The current parcel.</param>
        public F3201(int currentParcel)
        {
            this.parcelId = currentParcel;
            InitializeComponent();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the F8052 control.
        /// </summary>
        /// <value>The F8052 control.</value>
        [CreateNew]
        public F3201Controller F3201Control
        {
            get { return this.form3201Control as F3201Controller; }

            set { this.form3201Control = value; }
        }
        #endregion
        
        #region Event Publication

        /// <summary>
        /// event publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        [EventPublication(EventTopicNames.F3201_F32012_FormReload, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> F3201_F32012_FormReload;

        #endregion Event Publication

        #region Event Scbscription

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            try
            {
                if (e.Data == "F" + this.Tag.ToString() && !this.isSaveProcess)
                {
                    this.form3201Control.WorkItem.State[SharedFunctions.GetResourceString("FormStatus")] = this.CheckPageStatus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sends the optional parameters.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SendOptionalParameters, Thread = ThreadOption.UserInterface)]
        public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        {
             object[] optionalParams = e.Data;
             if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.ParentFormId))
             {
                 string parcelID = Convert.ToString(optionalParams[0]);
                 int.TryParse(parcelID, out this.parcelId);
                 if (!string.IsNullOrEmpty(parcelID))
                 {
                     if (this.parcelId > 0)
                     {
                         this.CustomizeLinkedGrid();
                         this.LoadSketchData();
                         this.LoadWorkSpace();
                         this.LinkedGridView.ClearSelection();
                     }
                     else
                     {
                         DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("F9030DefaultAlert"), SharedFunctions.GetResourceString("TerraScanInvalidRecord"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                         if (DialogResult.OK == dialogResult)
                         {
                             this.CustomizeLinkedGrid();
                             this.LoadSketchData();
                             this.LoadWorkSpace();
                             this.LinkedGridView.ClearSelection();
                         }
                         else
                         {
                             this.ParentForm.Close();
                             return;
                         }
                     }
                 }
             }
        }

        #endregion Event Scbscription

        #region Methods

        private void CustomizeTerragonGrid()
        {
            this.UnlinkedTerragonGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.UnlinkedTerragonGrid.DisplayLayout.InterBandSpacing = 0;

            //// To make Band[1] Column Header Visible False
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].ColHeadersVisible = false;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].ColHeadersVisible = false;
           
            // Hide Row selectors in Band[0]
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.False;

            //// Make the Row Indentation to left Corner
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Indentation = -2;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Indentation = -2;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Override.MinRowHeight = 22;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.MinRowHeight = 22;

            //// Change the Row selector Apprance for Band[1] and hide the row selector of band[0]
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowSelectorHeaderAppearance.BorderColor = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.ThemedElementAlpha = Alpha.Default;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.BorderColor = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.BackColor = Color.FromArgb(31, 71, 125);

            // To avoid dark line of border 
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Override.BorderStyleRow = UIElementBorderStyle.None;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.BorderStyleRow = UIElementBorderStyle.None;

            // Font size for band[1]
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowAppearance.FontData.Name = "Arial";
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowAppearance.FontData.SizeInPoints = 8F;

            //// Change the Row RowAppearance
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Override.RowAppearance.BackColor = Color.FromArgb(200, 214, 230);
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Override.RowAppearance.ForeColor = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.SizeInPoints = 8.25F;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Override.RowAppearance.BorderColor = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.Bold = DefaultableBoolean.True;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowAppearance.FontData.SizeInPoints = 8.25F;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor = Color.FromArgb(217, 217, 217);
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowAppearance.ForeColor = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowAppearance.BorderColor = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowAppearance.BorderColor2 = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Override.RowAppearance.BorderAlpha = Alpha.Opaque;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowAppearance.BorderAlpha = Alpha.Opaque;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BorderColor = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BorderColor2 = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Hide;
            this.UnlinkedTerragonGrid.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;

            //// Row selected appearance changed to white
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Override.SelectedCellAppearance.ForeColor = Color.White;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.SelectedCellAppearance.ForeColor = Color.White;

            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Override.SelectedCellAppearance.BackColor = Color.FromArgb(0, 0, 128);
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Override.SelectedCellAppearance.BackColor = Color.FromArgb(0, 0, 128);

            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Columns[this.terragonParentTable.DescriptionColumn.ColumnName].Header.VisiblePosition = 0;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Columns[this.terragonParentTable.TerragonIDColumn.ColumnName].Hidden = true;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Columns[this.terragonParentTable.TypeIDColumn.ColumnName].Hidden = true;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Columns[this.terragonParentTable.GroupIDColumn.ColumnName].Hidden = true;

            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Columns[this.terragonChildTable.DescriptionColumn.ColumnName].Header.VisiblePosition = 0;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Columns[this.terragonChildTable.TerragonIDColumn.ColumnName].Hidden = true;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Columns[this.terragonChildTable.TypeIDColumn.ColumnName].Hidden = true;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Columns[this.terragonChildTable.GroupIDColumn.ColumnName].Hidden = true;

            // Style
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Columns[this.terragonParentTable.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Columns[this.terragonChildTable.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Columns[this.terragonParentTable.DescriptionColumn.ColumnName].CellAppearance.BorderColor = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Columns[this.terragonParentTable.DescriptionColumn.ColumnName].CellAppearance.BorderColor = Color.Black;

            // Make the cell becomes non editable
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Columns[this.terragonChildTable.DescriptionColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            // Align Header Text
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Columns[this.terragonParentTable.DescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[0].Columns[this.terragonParentTable.DescriptionColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Columns[this.terragonChildTable.DescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.UnlinkedTerragonGrid.DisplayLayout.Bands[1].Columns[this.terragonChildTable.DescriptionColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            // Empty Row settings
            this.UnlinkedTerragonGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
            this.UnlinkedTerragonGrid.DisplayLayout.EmptyRowSettings.Style = Infragistics.Win.UltraWinGrid.EmptyRowStyle.AlignWithDataRows;
            this.UnlinkedTerragonGrid.DisplayLayout.EmptyRowSettings.RowAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.UnlinkedTerragonGrid.DisplayLayout.EmptyRowSettings.CellAppearance.BorderColor = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.EmptyRowSettings.EmptyAreaAppearance.BorderColor = Color.Black;
            this.UnlinkedTerragonGrid.DisplayLayout.EmptyRowSettings.EmptyAreaAppearance.BackColor = Color.Black;
            
            this.UnlinkedTerragonGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;

            // For full row selection
            this.UnlinkedTerragonGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.UnlinkedTerragonGrid.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
        }

        /// <summary>
        /// Customizes the polygon grid.
        /// </summary>
        private void CustomizePolygonGrid()
        {
            this.UnlinkedPolygonGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.UnlinkedPolygonGrid.DisplayLayout.InterBandSpacing = 0;

            //// To make Band[1] Column Header Visible False
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].ColHeadersVisible = false;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].ColHeadersVisible = false;

            this.UnlinkedPolygonGrid.DisplayLayout.Appearance.BorderColor = Color.Black;

            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Override.MinRowHeight = 22;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.MinRowHeight = 22;

            // Hide Row selectors in Band[0]
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.False;

            //// Make the Row Indentation to left Corner
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Indentation = -2;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Indentation = -2;

            //// Change the Row selector Apprance for Band[1] and hide the row selector of band[0]
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowSelectorHeaderAppearance.BorderColor = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.ThemedElementAlpha = Alpha.Default;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.BorderColor = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.BackColor = Color.FromArgb(31, 71, 125);
            this.UnlinkedPolygonGrid.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Hide;

            // To avoid dark line of border 
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Override.BorderStyleRow = UIElementBorderStyle.None;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.BorderStyleRow = UIElementBorderStyle.None;

            // Font size for band[1]
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowAppearance.FontData.Name = "Arial";
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowAppearance.FontData.SizeInPoints = 8F;

            //// Change the Row RowAppearance
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Override.RowAppearance.BackColor = Color.FromArgb(200, 214, 230);
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Override.RowAppearance.ForeColor = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.SizeInPoints = 8.25F;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Override.RowAppearance.BorderColor = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.Bold = DefaultableBoolean.True;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowAppearance.FontData.SizeInPoints = 8.25F;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor = Color.FromArgb(217, 217, 217);
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowAppearance.ForeColor = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowAppearance.BorderColor = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowAppearance.BorderColor2 = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Override.RowAppearance.BorderAlpha = Alpha.Opaque;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowAppearance.BorderAlpha = Alpha.Opaque;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BorderColor = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BorderColor2 = Color.Black;

            this.UnlinkedPolygonGrid.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;

            //// Row selected appearance changed to white
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Override.SelectedCellAppearance.ForeColor = Color.White;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.SelectedCellAppearance.ForeColor = Color.White;

            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Override.SelectedCellAppearance.BackColor = Color.FromArgb(0, 0, 128);
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Override.SelectedCellAppearance.BackColor = Color.FromArgb(0, 0, 128);

            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Columns[this.polygonParentTable.DescriptionColumn.ColumnName].Header.VisiblePosition = 0;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Columns[this.polygonParentTable.PolygonIDColumn.ColumnName].Hidden = true;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Columns[this.polygonParentTable.TypeIDColumn.ColumnName].Hidden = true;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Columns[this.polygonParentTable.GroupIDColumn.ColumnName].Hidden = true;

            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Columns[this.polygonChildTable.DescriptionColumn.ColumnName].Header.VisiblePosition = 0;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Columns[this.polygonChildTable.PolygonIDColumn.ColumnName].Hidden = true;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Columns[this.polygonChildTable.TypeIDColumn.ColumnName].Hidden = true;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Columns[this.polygonChildTable.GroupIDColumn.ColumnName].Hidden = true;

            // Style
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Columns[this.polygonParentTable.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Columns[this.polygonChildTable.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Columns[this.polygonParentTable.DescriptionColumn.ColumnName].CellAppearance.BorderColor = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Columns[this.polygonChildTable.DescriptionColumn.ColumnName].CellAppearance.BorderColor = Color.Black;

            // Make the cell becomes non editable
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Columns[this.polygonChildTable.DescriptionColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            // Align Header Text
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Columns[this.polygonParentTable.DescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[0].Columns[this.polygonParentTable.DescriptionColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Columns[this.polygonChildTable.DescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.UnlinkedPolygonGrid.DisplayLayout.Bands[1].Columns[this.polygonChildTable.DescriptionColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            // Empty Row settings
            this.UnlinkedPolygonGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
            this.UnlinkedPolygonGrid.DisplayLayout.EmptyRowSettings.Style = Infragistics.Win.UltraWinGrid.EmptyRowStyle.AlignWithDataRows;
            this.UnlinkedPolygonGrid.DisplayLayout.EmptyRowSettings.RowAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.UnlinkedPolygonGrid.DisplayLayout.EmptyRowSettings.CellAppearance.BorderColor = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.EmptyRowSettings.EmptyAreaAppearance.BorderColor = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.EmptyRowSettings.EmptyAreaAppearance.BackColor = Color.Black;
            this.UnlinkedPolygonGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;

            // For full row selection
            this.UnlinkedPolygonGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.UnlinkedPolygonGrid.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
        }

        /// <summary>
        /// Customizes the linked grid.
        /// </summary>
        private void CustomizeLinkedGrid()
        {
            this.LinkedGridView.RemoveDefaultSelection = true;
            this.LinkedGridView.AllowUserToResizeColumns = false;
            this.LinkedGridView.AllowUserToResizeRows = false;
            this.LinkedGridView.AutoGenerateColumns = false;

            this.TerragonID.DataPropertyName = this.sketchData.LinkedData.TerragonIDColumn.ColumnName;
            this.TerragonTypeID.DataPropertyName = this.sketchData.LinkedData.TerragonTypeIDColumn.ColumnName;
            this.TerragonDescription.DataPropertyName = this.sketchData.LinkedData.TerragonDescriptionColumn.ColumnName;
            this.PolygonID.DataPropertyName = this.sketchData.LinkedData.PolygonIDColumn.ColumnName;
            this.PolygonTypeID.DataPropertyName = this.sketchData.LinkedData.PolygonTypeIDColumn.ColumnName;
            this.PolygonDescription.DataPropertyName = this.sketchData.LinkedData.PolygonDescriptionColumn.ColumnName;
            this.TerragonGroupID.DataPropertyName = this.sketchData.LinkedData.TerragonGroupIDColumn.ColumnName;
            this.PolygonGroupID.DataPropertyName = this.sketchData.LinkedData.PolygonGroupIDColumn.ColumnName;
        }

        /// <summary>
        /// Loads the sketch data.
        /// </summary>
        private void LoadSketchData()
        {
            this.NoteLabel.Text = "Note: A Terragon's Area cannot be modified outside of Sketch once it has been linked with a polygon.";
            this.sketchData = this.form3201Control.WorkItem.F3201_GetSketchLinkData(this.parcelId, TerraScanCommon.UserId);
            this.PopulateGrid();
            this.SetMasterButtonStatus(false);
            this.SetArrowButtonStatus();
            this.LinkedGridView.CurrentCell = null;
            this.isSaveProcess = false;
        }

        /// <summary>
        /// Populates the grid.
        /// </summary>
        private void PopulateGrid()
        {
            try
            {
                this.terragonParentTable.Clear();
                this.terragonChildTable.Clear();
                // Prepare tables for Terragon Grid binding
                // Parent table prepartion for band[0]
                this.sketchData.TerragonData.DefaultView.RowFilter = this.sketchData.TerragonData.TypeIDColumn.ColumnName + " = 1";
                //this.terragonParentTable = (F3201SketchLinkData.TerragonDataDataTable)this.sketchData.TerragonData.DefaultView.ToTable("ParentTable").Copy();
                this.terragonParentTable.Merge(this.sketchData.TerragonData.DefaultView.ToTable().Copy());
                this.terragonParentTable.TableName = "ParentTable";

                // Child table preperation for band[1]
                this.sketchData.TerragonData.DefaultView.RowFilter = this.sketchData.TerragonData.TypeIDColumn.ColumnName + " <> 1";
                //this.terragonChildTable = (F3201SketchLinkData.TerragonDataDataTable)this.sketchData.TerragonData.DefaultView.ToTable("ChildTable").Copy();
                this.terragonChildTable.Merge(this.sketchData.TerragonData.DefaultView.ToTable().Copy());
                this.terragonChildTable.TableName = "ChildTable";
            }
            catch (Exception ex)
            {
            }

            this.sketchData.TerragonData.DefaultView.RowFilter = string.Empty;

            DataSet terragonData = new DataSet();
            terragonData.Tables.Add(this.terragonParentTable.Copy());
            terragonData.Tables.Add(this.terragonChildTable.Copy());
            try
            {
                terragonData.Relations.Add(terragonData.Tables["ParentTable"].Columns["GroupID"], terragonData.Tables["ChildTable"].Columns["GroupID"]);
            }
            catch (Exception ex)
            {
                // TO DO
            }

            this.UnlinkedTerragonGrid.DataMember = this.terragonParentTable.TableName;
            this.UnlinkedTerragonGrid.DataSource = terragonData.Copy();
          
            // Prepare tables for Polygon Grid binding
            this.polygonParentTable.Clear();
            this.polygonChildTable.Clear();
            
            // Parent table prepartion for band[0]
            this.sketchData.PolygonData.DefaultView.RowFilter = this.sketchData.PolygonData.TypeIDColumn.ColumnName + " = 3";
            //this.polygonParentTable = (F3201SketchLinkData.PolygonDataDataTable)this.sketchData.PolygonData.DefaultView.ToTable();
            this.polygonParentTable.Merge(this.sketchData.PolygonData.DefaultView.ToTable().Copy());
            this.polygonParentTable.TableName = "ParentTable";

            // Child table preperation for band[1]
            this.sketchData.PolygonData.DefaultView.RowFilter = this.sketchData.PolygonData.TypeIDColumn.ColumnName + " <> 3";
            //this.polygonChildTable = (F3201SketchLinkData.PolygonDataDataTable)this.sketchData.PolygonData.DefaultView.ToTable();
            this.polygonChildTable.Merge(this.sketchData.PolygonData.DefaultView.ToTable().Copy());
            this.polygonChildTable.TableName = "ChildTable";

            this.sketchData.PolygonData.DefaultView.RowFilter = string.Empty;

            DataSet polygonData = new DataSet();
            polygonData.Tables.Add(this.polygonParentTable.Copy());
            polygonData.Tables.Add(this.polygonChildTable.Copy());
            polygonData.Relations.Add(polygonData.Tables["ParentTable"].Columns["GroupID"], polygonData.Tables["ChildTable"].Columns["GroupID"]);
            this.UnlinkedPolygonGrid.DataMember = this.polygonParentTable.TableName;
            this.UnlinkedPolygonGrid.DataSource = polygonData.Copy();

            // Bind Linked Terragon/ Polygon data
            this.LinkedGridView.NumRowsVisible = 10;
            this.LinkedGridView.AllowEmptyRows = true;
            this.LinkedGridView.DataSource = this.sketchData.LinkedData.DefaultView;

            if (this.LinkedGridView.Rows.Count > 10)
            {
                this.LinkedGridScrollBar.SendToBack();
            }
            else
            {
                this.LinkedGridScrollBar.BringToFront();
            }
        }

        #region ExpandAllParentRows

        /// <summary>
        /// Expands all parent rows.
        /// </summary>
        /// <param name="row">The row.</param>
        private void ExpandAllTerragonParentRows(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            // Expand all the parent row presents Grid to show the child rows
            this.UnlinkedTerragonGrid.ActiveRow = row;
            this.UnlinkedTerragonGrid.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExpandRow, false, false);
            this.UnlinkedTerragonGrid.DisplayLayout.Rows[row.Index].ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            this.UnlinkedTerragonGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None;
        }

        /// <summary>
        /// Expands all parent rows.
        /// </summary>
        /// <param name="row">The row.</param>
        private void ExpandAllPolygonParentRows(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            // Expand all the parent row presents Grid to show the child rows
            this.UnlinkedPolygonGrid.ActiveRow = row;
            this.UnlinkedPolygonGrid.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExpandRow, false, false);
            this.UnlinkedPolygonGrid.DisplayLayout.Rows[row.Index].ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            this.UnlinkedPolygonGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None;
        }

        #endregion ExpandAllParentRows

        /// <summary>
        /// Check the page Status when the Edit is Cancelled
        /// </summary>
        /// <returns>true if no change in page status</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (this.SaveButton.Enabled)
            {
                //dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", SharedFunctions.GetResourceString("FormClose1410"), "", "?"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                dialogResult = MessageBox.Show("Do you want to save the changes to Sketch Link?", ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    this.SaveSketchLink();
                    return true;
                    //return this.SaveMasterConfirm();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form3201Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form3201Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form3201Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }
          
            //this.formLabelInfo[0] = SharedFunctions.GetResourceString("F1410FormHeader");
            this.formLabelInfo[0] = "Sketch Links";
            this.formLabelInfo[1] = string.Empty;

            if (this.form3201Control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
            {
                this.FooterWorkspace.Show(this.form3201Control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
            }
            else
            {
                this.FooterWorkspace.Show(this.form3201Control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
            }

            this.footerSmartPart = (FooterSmartPart)this.form3201Control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
            this.footerSmartPart.ParentWorkItem = this.form3201Control.WorkItem;
            this.footerSmartPart.FormId = "3201";
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

            if (this.sketchData.HeaderDetails.Rows.Count > 0 && !string.IsNullOrEmpty(this.sketchData.HeaderDetails.Rows[0][0].ToString().Trim()))
            {
                string subTitle = this.sketchData.HeaderDetails.Rows[0][0].ToString().Trim();
                string[] headerArray = subTitle.Split('/');
                string rollYear = string.Empty;
                if (headerArray.Length > 1)
                {
                    rollYear = headerArray[headerArray.Length - 1];
                    subTitle = subTitle.Substring(0, subTitle.Length - rollYear.Length);
                }
                //if (!string.IsNullOrEmpty(rollYear.Trim()))
                //{
                //    this.SubHeaderLabel.Text = subTitle.Trim() + " / ";
                //}
                //else
                //{
                    this.SubHeaderLabel.Text = subTitle.Trim();
                //}

                this.SubHeader1Label.Text = rollYear.Trim();
            }
            else
            {
                this.SubHeaderLabel.Text = string.Empty;
                this.SubHeader1Label.Text = string.Empty;
            }

            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Sets the master button status.
        /// </summary>
        /// <param name="isEnable">if set to <c>true</c> [is enable].</param>
        private void SetMasterButtonStatus(bool isEnable)
        {                    
            this.SaveButton.Enabled = isEnable;
            this.CancelButton.Enabled = isEnable;
        }

        /// <summary>
        /// Sets the arrow button status.
        /// </summary>
        private void SetArrowButtonStatus()
        {
            // Set ArrosButton image based on the row selection in both terragon and polygon grid
            if (this.UnlinkedTerragonGrid.ActiveRow != null && this.UnlinkedTerragonGrid.ActiveRow.Band.Index.Equals(1)
                    && this.UnlinkedPolygonGrid.ActiveRow != null && this.UnlinkedPolygonGrid.ActiveRow.Band.Index.Equals(1))
            {
                this.ArrowButton.Enabled = true;
                this.ArrowButton.BackgroundImage = global::D3200.Properties.Resources.BlueArrow;
            }
            else
            {
               // this.ArrowButton.Enabled = false;
                this.ArrowButton.BackgroundImage = global::D3200.Properties.Resources.GrayArrow;
            }
        }

        /// <summary>
        /// Removes the empty rows.
        /// </summary>
        /// <param name="changeSets">The change sets.</param>
        /// <returns>Table with valid data rows</returns>
        private DataTable RemoveEmptyRows(DataTable changeSets)
        {
            // Remove the empty rows presents in table
            changeSets.DefaultView.RowFilter = "EmptyRecord$ = False";
            return changeSets.DefaultView.ToTable();
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F3201 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F3201_Load(object sender, EventArgs e)
        {
            try
            {
                this.CustomizeLinkedGrid();
                this.SetMasterButtonStatus(false);
              //  this.LoadSketchData();
                this.LoadWorkSpace();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the InitializeLayout event of the UnlinkedTerragonGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void UnlinkedTerragonGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeTerragonGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeRow event of the UnlinkedTerragonGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void UnlinkedTerragonGrid_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            try
            {
                if (e.Row.Band.Index.Equals(0))
                {
                    this.ExpandAllTerragonParentRows(e.Row);
                }

                e.Row.Height = 22;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the AfterRowActivate event of the UnlinkedTerragonGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UnlinkedTerragonGrid_AfterRowActivate(object sender, EventArgs e)
        {
            try
            {
                this.SetArrowButtonStatus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeLayout event of the UnlinkedPolygonGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void UnlinkedPolygonGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizePolygonGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeRow event of the UnlinkedPolygonGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void UnlinkedPolygonGrid_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            try
            {
                if (e.Row.Band.Index.Equals(0))
                {
                    this.ExpandAllPolygonParentRows(e.Row);
                }

                e.Row.Height = 22;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the AfterRowActivate event of the UnlinkedPolygonGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UnlinkedPolygonGrid_AfterRowActivate(object sender, EventArgs e)
        {
            try
            {
                this.SetArrowButtonStatus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the LinkedGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void LinkedGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.LinkedGridView.Columns[e.ColumnIndex].Name.Equals("Seperator"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(31, 71, 125);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the LinkedGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void LinkedGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Set RemoveLink button image based on the row selection
                if (this.LinkedGridView.OriginalRowCount > 0 && this.LinkedGridView.Rows[e.RowIndex].Selected)
                {
                    this.RemoveLinkButton.Enabled = true;
                    this.RemoveLinkButton.BackgroundImage = global::D3200.Properties.Resources.RemoveLink_blue;
                }
                else
                {
                    this.RemoveLinkButton.Enabled = true;
                    this.RemoveLinkButton.BackgroundImage = global::D3200.Properties.Resources.RemoveLink_gray;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the HelplinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelplinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString().Trim()))
                {
                    HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadSketchData();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.isSaveProcess = true;
                this.SaveSketchLink();
              
                this.F3201_F32012_FormReload(this, new DataEventArgs<int>(this.parcelId));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.isSaveProcess = false;
            }
        }

        /// <summary>
        /// Saves the sketch link.
        /// </summary>
        private void SaveSketchLink()
        {
            this.isSaveProcess = true;
            //DataTable filteredDatable = this.sketchData.LinkedData.Copy();
            //this.sketchData.LinkedData.Rows.Clear();
            //this.sketchData.LinkedData.Merge(this.RemoveEmptyRows(filteredDatable));
            DataTable filteredDatable = this.RemoveEmptyRows(this.sketchData.LinkedData.Copy());
            string linkedData = TerraScanCommon.GetXmlString(filteredDatable);
            string returnMessage = this.form3201Control.WorkItem.F3201_SaveSketchLinkData(linkedData, this.parcelId, TerraScanCommon.UserId);

            // If the SP returns validation message, show F3299 form
            if (!string.IsNullOrEmpty(returnMessage.Trim()))
            {
                Form errorForm = new Form();
                object[] optionalParameter = new object[] { returnMessage };
                errorForm = this.form3201Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3299, optionalParameter, this.form3201Control.WorkItem);
                if (errorForm != null)
                {
                    errorForm.ShowDialog();
                }
            }
               // this.SetMasterButtonStatus(true);
            //}
            //else
            //{
            this.LoadSketchData();
               // this.SetMasterButtonStatus(false);
                this.isSaveProcess = true;
            //}
        }

        /// <summary>
        /// Handles the Click event of the ArrowButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ArrowButton_Click(object sender, EventArgs e)
        {
            try
            {
                int currentTerraParentIndex = 0;
                int currentPolyParentIndex = 0;
                
                // If the row focus is not properly set in terragon grid
                // Set focus on currently selected row or else the activerow becomes null
                if (this.UnlinkedTerragonGrid.ActiveRow == null)
                {
                    if (this.UnlinkedTerragonGrid.Selected.Rows.Count > 0)
                    {
                        int currentRowIndex = this.UnlinkedTerragonGrid.Selected.Rows[0].Index;

                        if (this.UnlinkedTerragonGrid.Selected.Rows[0].ParentRow != null)
                        {
                            int parentBandIndex = this.UnlinkedTerragonGrid.Selected.Rows[0].ParentRow.Index;
                            this.UnlinkedTerragonGrid.Rows[parentBandIndex].ChildBands[0].Rows[currentRowIndex].Selected = true;
                            this.UnlinkedTerragonGrid.Rows[parentBandIndex].ChildBands[0].Rows[currentRowIndex].Activate();
                            currentTerraParentIndex = parentBandIndex;
                        }
                        else
                        {
                            this.UnlinkedTerragonGrid.Rows[currentRowIndex].Selected = true;
                            this.UnlinkedTerragonGrid.Rows[currentRowIndex].Activate();
                            currentTerraParentIndex = currentRowIndex;
                        }
                    }
                }

                // If the row focus is not properly set in poluygon grid
                // Set focus on currently selected row or else the activerow becomes null
                if (this.UnlinkedPolygonGrid.ActiveRow == null)
                {
                    if (this.UnlinkedPolygonGrid.Selected.Rows.Count > 0)
                    {
                        int currentRowIndex = this.UnlinkedPolygonGrid.Selected.Rows[0].Index;

                        if (this.UnlinkedPolygonGrid.Selected.Rows[0].ParentRow != null)
                        {
                            int parentBandIndex = this.UnlinkedPolygonGrid.Selected.Rows[0].ParentRow.Index;
                            this.UnlinkedPolygonGrid.Rows[parentBandIndex].ChildBands[0].Rows[currentRowIndex].Selected = true;
                            this.UnlinkedPolygonGrid.Rows[parentBandIndex].ChildBands[0].Rows[currentRowIndex].Activate();
                            currentPolyParentIndex = parentBandIndex;
                        }
                        else
                        {
                            this.UnlinkedPolygonGrid.Rows[currentRowIndex].Selected = true;
                            this.UnlinkedPolygonGrid.Rows[currentRowIndex].Activate();
                            currentPolyParentIndex = currentRowIndex;
                        }
                    }
                }

                // Add new row in Linked grid
                if (this.UnlinkedTerragonGrid.ActiveRow != null && this.UnlinkedTerragonGrid.ActiveRow.Band.Index.Equals(1)
                    && this.UnlinkedPolygonGrid.ActiveRow != null && this.UnlinkedPolygonGrid.ActiveRow.Band.Index.Equals(1))
                {
                    currentTerraParentIndex = this.UnlinkedTerragonGrid.ActiveRow.ParentRow.Index;
                    F3201SketchLinkData.LinkedDataRow newRow = this.sketchData.LinkedData.NewLinkedDataRow();
                    if (this.UnlinkedTerragonGrid.ActiveRow.Cells[this.sketchData.TerragonData.TerragonIDColumn.ColumnName].Value != null)
                    {
                        newRow.TerragonID = int.Parse(this.UnlinkedTerragonGrid.ActiveRow.Cells[this.sketchData.TerragonData.TerragonIDColumn.ColumnName].Value.ToString()); 
                    }

                    if (this.UnlinkedTerragonGrid.ActiveRow.Cells[this.sketchData.TerragonData.TypeIDColumn.ColumnName].Value != null)
                    {
                        newRow.TerragonTypeID = int.Parse(this.UnlinkedTerragonGrid.ActiveRow.Cells[this.sketchData.TerragonData.TypeIDColumn.ColumnName].Value.ToString());
                    }

                    if (this.UnlinkedTerragonGrid.ActiveRow.Cells[this.sketchData.TerragonData.DescriptionColumn.ColumnName].Value != null)
                    {
                        newRow.TerragonDescription = this.UnlinkedTerragonGrid.ActiveRow.Cells[this.sketchData.TerragonData.DescriptionColumn.ColumnName].Value.ToString();
                    }
                    else
                    {
                        newRow.TerragonDescription = string.Empty;
                    }

                    if (this.UnlinkedTerragonGrid.ActiveRow.Cells[this.sketchData.TerragonData.GroupIDColumn.ColumnName].Value != null)
                    {
                        newRow.TerragonGroupID = int.Parse(this.UnlinkedTerragonGrid.ActiveRow.Cells[this.sketchData.TerragonData.GroupIDColumn.ColumnName].Value.ToString());
                    }

                    currentPolyParentIndex = this.UnlinkedPolygonGrid.ActiveRow.ParentRow.Index;
                    if (this.UnlinkedPolygonGrid.ActiveRow.Cells[this.sketchData.PolygonData.PolygonIDColumn.ColumnName].Value != null)
                    {
                        newRow.PolygonID = int.Parse(this.UnlinkedPolygonGrid.ActiveRow.Cells[this.sketchData.PolygonData.PolygonIDColumn.ColumnName].Value.ToString());
                    }

                    if (this.UnlinkedPolygonGrid.ActiveRow.Cells[this.sketchData.PolygonData.TypeIDColumn.ColumnName].Value != null)
                    {
                        newRow.PolygonTypeID = int.Parse(this.UnlinkedPolygonGrid.ActiveRow.Cells[this.sketchData.PolygonData.TypeIDColumn.ColumnName].Value.ToString());
                    }

                    if (this.UnlinkedPolygonGrid.ActiveRow.Cells[this.sketchData.PolygonData.DescriptionColumn.ColumnName].Value != null)
                    {
                        newRow.PolygonDescription = this.UnlinkedPolygonGrid.ActiveRow.Cells[this.sketchData.PolygonData.DescriptionColumn.ColumnName].Value.ToString();
                    }
                    else
                    {
                        newRow.PolygonDescription = string.Empty;
                    }

                    if (this.UnlinkedPolygonGrid.ActiveRow.Cells[this.sketchData.PolygonData.GroupIDColumn.ColumnName].Value != null)
                    {
                        newRow.PolygonGroupID = int.Parse(this.UnlinkedPolygonGrid.ActiveRow.Cells[this.sketchData.PolygonData.GroupIDColumn.ColumnName].Value.ToString());
                    }

                    try
                    {
                        DataTable filteredDatable = this.sketchData.LinkedData.Copy();
                        this.sketchData.LinkedData.Rows.Clear();
                        this.sketchData.LinkedData.Merge(this.RemoveEmptyRows(filteredDatable));
                    }
                    catch (Exception ex)
                    {
                    }

                    this.sketchData.LinkedData.Rows.Add(newRow);

                    DataRow[] currentTerragon = this.sketchData.TerragonData.Select(this.sketchData.TerragonData.TerragonIDColumn.ColumnName
                                                                                    + " = " + this.UnlinkedTerragonGrid.ActiveRow.Cells[this.sketchData.TerragonData.TerragonIDColumn.ColumnName].Value.ToString());

                    if (currentTerragon.Length > 0)
                    {
                        this.sketchData.TerragonData.Rows.Remove(currentTerragon[0]);
                    }

                    DataRow[] currentPolygon = this.sketchData.PolygonData.Select(this.sketchData.PolygonData.PolygonIDColumn.ColumnName
                                                                                    + " = " + this.UnlinkedPolygonGrid.ActiveRow.Cells[this.sketchData.PolygonData.PolygonIDColumn.ColumnName].Value.ToString());

                    if (currentPolygon.Length > 0)
                    {
                        this.sketchData.PolygonData.Rows.Remove(currentPolygon[0]);
                    }

                    this.PopulateGrid();

                    this.UnlinkedTerragonGrid.Rows[currentTerraParentIndex].Activate();
                    this.UnlinkedPolygonGrid.Rows[currentPolyParentIndex].Activate();
                    this.UnlinkedTerragonGrid.Rows[currentTerraParentIndex].Selected = true;
                    this.UnlinkedPolygonGrid.Rows[currentPolyParentIndex].Selected = true;

                    this.LinkedGridView.Focus();
                    this.LinkedGridView.ClearSelection();
                    this.LinkedGridView.CurrentCell = null;
                    int currentRowIndex = 0;
                    if (this.LinkedGridView.OriginalRowCount > 0)
                    {
                        currentRowIndex = this.LinkedGridView.OriginalRowCount  - 1; 
                    }

                    TerraScanCommon.SetDataGridViewCellPosition(this.LinkedGridView, currentRowIndex, 2);
                    this.LinkedGridView.CurrentCell.Selected = true;
                    this.LinkedGridView.CurrentRow.Selected = true;

                    this.SetMasterButtonStatus(true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the RemoveLinkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RemoveLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.LinkedGridView.CurrentRow != null)
                {
                    // Add row in Terragon Grid
                    F3201SketchLinkData.TerragonDataRow terragonRow = this.sketchData.TerragonData.NewTerragonDataRow();
                    if (this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.TerragonIDColumn.ColumnName].Value != null)
                    {
                        terragonRow.TerragonID = int.Parse(this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.TerragonIDColumn.ColumnName].Value.ToString());
                    }

                    if (this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.TerragonTypeIDColumn.ColumnName].Value != null)
                    {
                        terragonRow.TypeID = int.Parse(this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.TerragonTypeIDColumn.ColumnName].Value.ToString());
                    }

                    if (this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.TerragonDescriptionColumn.ColumnName].Value != null)
                    {
                        terragonRow.Description = this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.TerragonDescriptionColumn.ColumnName].Value.ToString();
                    }
                    else
                    {
                        terragonRow.Description = string.Empty;
                    }

                    if (this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.TerragonGroupIDColumn.ColumnName].Value != null)
                    {
                        terragonRow.GroupID = int.Parse(this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.TerragonGroupIDColumn.ColumnName].Value.ToString());
                    }

                    this.sketchData.TerragonData.Rows.Add(terragonRow);

                    // Add row in polygon grid
                    F3201SketchLinkData.PolygonDataRow polygonRow = this.sketchData.PolygonData.NewPolygonDataRow();
                    if (this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.PolygonIDColumn.ColumnName].Value != null)
                    {
                        polygonRow.PolygonID = int.Parse(this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.PolygonIDColumn.ColumnName].Value.ToString());
                    }

                    if (this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.PolygonTypeIDColumn.ColumnName].Value != null)
                    {
                        polygonRow.TypeID = int.Parse(this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.PolygonTypeIDColumn.ColumnName].Value.ToString());
                    }

                    if (this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.PolygonDescriptionColumn.ColumnName].Value != null)
                    {
                        polygonRow.Description = this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.PolygonDescriptionColumn.ColumnName].Value.ToString();
                    }
                    else
                    {
                        polygonRow.Description = string.Empty;
                    }

                    if (this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.PolygonGroupIDColumn.ColumnName].Value != null)
                    {
                        polygonRow.GroupID = int.Parse(this.LinkedGridView.CurrentRow.Cells[this.sketchData.LinkedData.PolygonGroupIDColumn.ColumnName].Value.ToString());
                    }

                    this.sketchData.PolygonData.Rows.Add(polygonRow);

                    // Remove current row from Linked Grid
                    DataRow currentRow = this.sketchData.LinkedData.Rows[this.LinkedGridView.CurrentRow.Index];
                    this.sketchData.LinkedData.Rows.Remove(currentRow);

                    // Populate Terragon, Polygon & Linked Grie
                    this.PopulateGrid();

                    // Set focus on current row in Terragon grid
                    if (this.UnlinkedTerragonGrid.Rows.Count > 0)
                    {
                        for (int terraRowIndex = 0; terraRowIndex < this.UnlinkedTerragonGrid.Rows.Count; terraRowIndex++)
                        {
                            if (this.UnlinkedTerragonGrid.Rows[terraRowIndex].ChildBands != null && this.UnlinkedTerragonGrid.Rows[terraRowIndex].ChildBands[0].Rows.Count > 0)
                            {
                                for (int terraChildIndex = 0; terraChildIndex < this.UnlinkedTerragonGrid.Rows[terraRowIndex].ChildBands[0].Rows.Count; terraChildIndex++)
                                {
                                    if (this.UnlinkedTerragonGrid.Rows[terraRowIndex].ChildBands[0].Rows[terraChildIndex].Cells["TerragonID"].Value != null
                                        && this.UnlinkedTerragonGrid.Rows[terraRowIndex].ChildBands[0].Rows[terraChildIndex].Cells["TerragonID"].Value.Equals(terragonRow.TerragonID))
                                    {
                                        this.UnlinkedTerragonGrid.Rows[terraRowIndex].ChildBands[0].Rows[terraChildIndex].Selected = true;
                                        this.UnlinkedTerragonGrid.Rows[terraRowIndex].ChildBands[0].Rows[terraChildIndex].Activate();
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    // Set focus on current row in polygon grid
                    if (this.UnlinkedPolygonGrid.Rows.Count > 0)
                    {
                        for (int polyRowIndex = 0; polyRowIndex < this.UnlinkedPolygonGrid.Rows.Count; polyRowIndex++)
                        {
                            if (this.UnlinkedPolygonGrid.Rows[polyRowIndex].ChildBands != null && this.UnlinkedPolygonGrid.Rows[polyRowIndex].ChildBands[0].Rows.Count > 0)
                            {
                                for (int polyChildIndex = 0; polyChildIndex < this.UnlinkedPolygonGrid.Rows[polyRowIndex].ChildBands[0].Rows.Count; polyChildIndex++)
                                {
                                    if (this.UnlinkedPolygonGrid.Rows[polyRowIndex].ChildBands[0].Rows[polyChildIndex].Cells["PolygonID"].Value != null
                                        && this.UnlinkedPolygonGrid.Rows[polyRowIndex].ChildBands[0].Rows[polyChildIndex].Cells["PolygonID"].Value.Equals(polygonRow.PolygonID))
                                    {
                                        this.UnlinkedPolygonGrid.Rows[polyRowIndex].ChildBands[0].Rows[polyChildIndex].Selected = true;
                                        this.UnlinkedPolygonGrid.Rows[polyRowIndex].ChildBands[0].Rows[polyChildIndex].Activate();
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    // Enable Save/Cancel button
                    this.SetMasterButtonStatus(true);
                    this.LinkedGridView.CurrentCell = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events
    
    }
}
