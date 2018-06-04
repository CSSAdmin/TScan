// -------------------------------------------------------------------------------------------------
// <copyright file="UserControlCommon.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// Saves the Filter Information
// </summary>
// -------------------------------------------------------------------------------------------------
namespace TerraScan.Common
{
    ///using System;
    ///using System.Collections.Generic;
    ///using System.Text;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Data;

    /// <summary>
    ///  class UserControlCommon
    /// </summary>
    public static class UserControlCommon
    {
        #region enum

        /// <summary>
        /// Enumerator ButtonActionMode
        /// </summary>
        public enum ButtonActionMode
        {
            /// <summary>
            /// New Mode
            /// </summary>
            NewMode = 0,

            /// <summary>
            /// Edit Mode
            /// </summary>
            EditMode = 1,

            /// <summary>
            /// Save
            /// </summary>
            SaveMode = 2,

            /// <summary>
            /// Cancel Mode
            /// </summary>
            CancelMode = 3,

            /// <summary>
            /// Delete Mode
            /// </summary>
            DeleteMode = 4,

            /// <summary>
            /// Open Mode
            /// </summary>
            OpenMode = 5
        }

        /// <summary>
        /// Enumerator ButtonActionType
        /// </summary>
        public enum ButtonActionType
        {
            /// <summary>
            /// Other
            /// </summary>
            Other = 0,

            /// <summary>
            /// New
            /// </summary>
            New = 1,

            /// <summary>
            /// Save
            /// </summary>
            Save = 2,

            /// <summary>
            /// Edit
            /// </summary>
            Edit = 3,

            /// <summary>
            /// Delete
            /// </summary>
            Delete = 4,

            /// <summary>
            /// Cancel
            /// </summary>
            Cancel = 5,

            /// <summary>
            /// Open
            /// </summary>
            Open = 6
        }

        #endregion enum

        #region DataGrid

        /// <summary>
        /// Sets the same property.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="empty">The empty.</param>
        public static void SetSameProperty(DataGridView source, DataGridView empty, int maxRow)
        {
            ////empty.Size = source.Size;
            empty.Location = source.Location;
            empty.Parent = source.Parent;
            empty.Enabled = false;
            SetEmptyGridHeight(empty, maxRow);
        }

        /// <summary>
        /// Used to create a empty row in a Datagrid.
        /// </summary>
        /// <param name="sourceDataTable">The source data table.</param>
        /// <param name="maxRowCount">The max row count.</param>
        /// <returns>returns datatable</returns>
        public static DataTable CreateEmptyRows(DataTable sourceDataTable, int maxRowCount)
        {
            DataRow tempRow;
            if (sourceDataTable.Rows.Count < maxRowCount)
            {
                for (int i = 0; i < maxRowCount; i++)
                {
                    tempRow = sourceDataTable.NewRow();
                    sourceDataTable.Rows.Add(tempRow);
                }
            }

            return sourceDataTable;
        }

        /// <summary>
        /// Sets the height of the grid.
        /// </summary>
        public static void SetGridHeight(DataGridView dataGridView, int maxRow)
        {
            if (((DataTable)dataGridView.DataSource).Rows.Count <= maxRow)
            {
                dataGridView.Height = ((dataGridView.ColumnHeadersHeight - 1) * (((DataTable)dataGridView.DataSource).Rows.Count + 1)) + 1;
            }
            else
            {
                dataGridView.Height = ((dataGridView.ColumnHeadersHeight - 1) * (maxRow + 1)) + 1;
            }
        }

        private static void SetEmptyGridHeight(DataGridView dataGridView, int maxRow)
        {
            if (dataGridView.DataSource != null)
            {
                if (((DataTable)dataGridView.DataSource).Rows.Count > 0)
                {
                    dataGridView.Height = ((dataGridView.ColumnHeadersHeight - 1) * (((DataTable)dataGridView.DataSource).Rows.Count + 1)) + 1;
                }
            }
        }

        /// <summary>
        /// Sets the data grid view position.
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="commentRow">The comment row.</param>
        public static void SetDataGridViewPosition(DataGridView tempDataGridview, int commentRow)
        {
            if (tempDataGridview.Rows.Count > 0 && commentRow >= 0)
            {
                tempDataGridview.Rows[Convert.ToInt32(commentRow)].Selected = true;
                tempDataGridview.CurrentCell = tempDataGridview[0, Convert.ToInt32(commentRow)];
            }
        }        
        #endregion
    }
}
