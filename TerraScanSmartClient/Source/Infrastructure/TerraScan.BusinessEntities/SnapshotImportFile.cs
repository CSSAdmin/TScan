using System.Collections;
using System.Data;
using System;
using System.Windows.Forms;
namespace TerraScan.BusinessEntities {
    
    
    public partial class SnapshotImportFile {

        public void InsertSnapshotImportEntry(IDictionary SnapshotImportDictionary, int lineIndex)
        {
            try
            {
                IDictionaryEnumerator enumerator = SnapshotImportDictionary.GetEnumerator();
                string fieldValue = string.Empty;
                int maxLength = -1;
                string columnName = string.Empty;
                DataRow dr = this.tableSnapshotImportEntry.NewRow();

                while (enumerator.MoveNext())
                {
                    columnName = enumerator.Key.ToString();
                    maxLength = this.tableSnapshotImportEntry.Columns[columnName].MaxLength;
                    fieldValue = Convert.ToString(enumerator.Value);
                    if (maxLength > 0 && fieldValue.Length > maxLength)
                    {
                        fieldValue = fieldValue.Remove(maxLength);
                    }

                    if (fieldValue == "")
                    {

                    }
                    else
                    {
                        dr[columnName] = fieldValue;
                    }
                }

                // dr[this.PermitImportEntry.LineColumn] = lineIndex;

                this.tableSnapshotImportEntry.Rows.Add(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Data given");
            }
        }
    }
}
