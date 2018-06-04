namespace TerraScan.BusinessEntities {

    using System.Data;

    partial class F11011ExciseStatementData
    {
        /// <summary>
        /// Initializes the excise tax receipt with the default value.
        /// </summary>
        public void InitializeExciseReceipt()
        {
            this.tableGetExciseReceipt.Rows.Clear();
            ////initialize three rows
            for (int i = 0; i < 3; i++)
            {
                DataRow dr = this.tableGetExciseReceipt.NewRow();
                this.tableGetExciseReceipt.Rows.Add(dr);
            }

            this.tableGetExciseReceipt.Rows[0][this.tableGetExciseReceipt.ItemColumn] = "Tax";
            this.tableGetExciseReceipt.Rows[0][this.tableGetExciseReceipt.FeeTypeColumn] = "Technology";
            this.tableGetExciseReceipt.Rows[1][this.tableGetExciseReceipt.ItemColumn] = "Interest";
            this.tableGetExciseReceipt.Rows[1][this.tableGetExciseReceipt.FeeTypeColumn] = "Transaction";
            this.tableGetExciseReceipt.Rows[2][this.tableGetExciseReceipt.ItemColumn] = "Penalty";

            this.tableGetExciseReceipt.AcceptChanges();
        }
    }
}
