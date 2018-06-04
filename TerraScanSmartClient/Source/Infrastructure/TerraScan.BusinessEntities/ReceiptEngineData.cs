namespace TerraScan.BusinessEntities 
{
    using System.Data;

    /// <summary>
    /// ReceiptEngineData class file
    /// </summary>
    partial class ReceiptEngineData
    {
        /// <summary>
        /// Initializes the payment items.
        /// </summary>
        public void InitializePaymentItems()
        {
            this.tablePaymentItems.Rows.Clear();

            // initialize three rows
            for (int i = 0; i < 3; i++)
            {
                DataRow dr = this.tablePaymentItems.NewRow();
                this.tablePaymentItems.Rows.Add(dr);
            }

            this.tablePaymentItems.AcceptChanges();
        }

        /// <summary>
        /// Initializes the history grid.
        /// </summary>
        public void InitializeHistoryGrid()
        {
            this.tableListHistoryGrid.Rows.Clear();

            // initialize three rows
            for (int i = 0; i < 3; i++)
            {
                DataRow dr = this.tableListHistoryGrid.NewRow();
                this.tableListHistoryGrid.Rows.Add(dr);
            }

            this.tableListHistoryGrid.AcceptChanges();
        }        
    }
}
