
namespace D35100
{
    using System;
    using System.Windows.Forms;
    using TerraScan.BusinessEntities;
    using System.Data;
    using TerraScan.Common;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;

    [SmartPart]
    partial class F3511 : Form
    {
        #region Variable
        /// <summary>
        /// storing neighbourhood ID
        /// </summary>
        private int NBHDID;

        /// <summary>
        /// storing roll year
        /// </summary>
        private int RollYear;

        /// <summary>
        /// storing Neighbourhood type
        /// </summary>
        private long Type;

        /// <summary>
        /// Form3511 Controller
        /// </summary>
        private F3511Controller form3511Controller;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        #endregion

        [CreateNew]
        public F3511Controller F3511Control
        {
            get { return this.form3511Controller as F3511Controller; }
            set { this.form3511Controller = value; }
        }

        public F3511(string NeighbourhoodID, int RollYear, int Type)
        {
            this.NBHDID = Convert.ToInt32(NeighbourhoodID);
            this.RollYear = RollYear;
            this.Type = Type;
            this.InitializeComponent();
        }

        public string CommandResult
        {
            get { return this.commandResult; }
            set { this.commandResult = value; }
        }

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        private void PermissionLock(bool Status)
        {
            //if (this.slicePermissionField.editPermission)
            //{
            //    this.ControlStatus(Status);
            //}
            //else
            //{
                this.ControlStatus(Status);
            //}

        }

        private void ControlStatus(bool ControlLock)
        {
            this.NewNeighborhoodTextBox.LockKeyPress = !ControlLock;
            this.NewNeighborhoodTextBox.Enabled = ControlLock;
        }

        /// <summary>
        /// Form Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F3511_Load(object sender, EventArgs e)
        {
            this.getFormDetailsDataDetails = this.form3511Controller.WorkItem.GetFormDetails(Convert.ToInt32(this.Tag), TerraScanCommon.UserId);
            this.slicePermissionField.openPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName].ToString());
            this.slicePermissionField.newPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName].ToString());
            this.slicePermissionField.deletePermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName].ToString());
            this.slicePermissionField.editPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName].ToString());
            this.PermissionLock(this.slicePermissionField.editPermission);
            if (NewNeighborhoodTextBox.Text.Trim().ToString().Length > 0)
            {
                //NeighborhoodAcceptButton.Enabled = true;
            }
            else
            {
                NeighborhoodAcceptButton.Enabled = false;
            }
        }

        /// <summary>
        /// Neighbourhood Text Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewNeighbourhoodTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NewNeighborhoodTextBox.Text.Trim().ToString().Length > 0 && this.slicePermissionField.editPermission && this.slicePermissionField.newPermission)
            {
                NeighborhoodAcceptButton.Enabled = true;
            }
            else
            {
                NeighborhoodAcceptButton.Enabled = false;
            }
        }

        /// <summary>
        /// Neighbourhood Accept Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NeighborhoodAcceptButton_Click(object sender, EventArgs e)
        {
            if (!this.CheckDuplicateneighborhood())
            {
                MessageBox.Show("This record cannot be saved because a Neighborhood of that Type already exists with the given Name and Roll Year.", ConfigurationWrapper.ApplicationName + " - Duplicate Neighborhood", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.NewNeighborhoodTextBox.Focus();
                return;
            }
            int NewID = this.form3511Controller.WorkItem.CopyNeighbourhood(this.NBHDID, this.NewNeighborhoodTextBox.Text.ToString().Trim());
            this.commandResult = NewID.ToString();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Checks the duplicate district.
        /// </summary>
        /// <returns>duplicate record status</returns>
        private bool CheckDuplicateneighborhood()
        {
            F35100NeighborhoodHeaderData duplicateNeighborhoodHeaderData = new F35100NeighborhoodHeaderData();
            F35100NeighborhoodHeaderData.CheckDuplicateNeighborhoodTableRow dr = duplicateNeighborhoodHeaderData.CheckDuplicateNeighborhoodTable.NewCheckDuplicateNeighborhoodTableRow();
            int errorId = -1;
            int tempNBHDID = 0;
            //int.TryParse(this.NBHDID.ToString().Trim(), out tempNBHDID);
            //if (tempNBHDID > 0)
            //{
            //    dr.NBHDID = this.NBHDID.ToString().Trim();
            //}
            //dr.NBHDID = tempNBHDID.ToString();
            dr.Neighborhood = this.NewNeighborhoodTextBox.Text.Trim();
            dr.RollYear = this.RollYear.ToString();
            dr.Type = this.Type.ToString();
            duplicateNeighborhoodHeaderData.CheckDuplicateNeighborhoodTable.Rows.Add(dr);
            duplicateNeighborhoodHeaderData.CheckDuplicateNeighborhoodTable.AcceptChanges();
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(duplicateNeighborhoodHeaderData.CheckDuplicateNeighborhoodTable.Copy());
            tempDataSet.Tables[0].TableName = "Table";
            //if (string.IsNullOrEmpty(this.NBHDID.ToString().Trim()))
            {
                errorId = this.form3511Controller.WorkItem.DuplicateNeighborhoodHeaderCheck(0, tempDataSet.GetXml());
            }
            //else
            //{
            //  errorId = this.form3511Controller.WorkItem.DuplicateNeighborhoodHeaderCheck(tempNBHDID, tempDataSet.GetXml());
            //}

            if (errorId != -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Neighbourhood Cancel Button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NeighborhoodCancelButton_Click(object sender, EventArgs e)
        {

        }

    }
}
