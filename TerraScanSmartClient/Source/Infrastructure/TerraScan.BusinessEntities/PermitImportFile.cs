using System.Collections;
using System.Data;
using System;
using System.Windows.Forms;
namespace TerraScan.BusinessEntities {
    
    
    public partial class PermitImportFile {
        /// <summary>
        /// Inserts permit import entry record
        /// </summary>
        /// <param name="mortgageImportDictionary">The collection contains columnname and value</param>
        /// <param name="lineIndex">The index of the file line</param>
        public void InsertMortgageImportEntry(IDictionary permitImportDictionary, int lineIndex)
        {
            try
            {
                IDictionaryEnumerator enumerator = permitImportDictionary.GetEnumerator();
                string fieldValue = string.Empty;
                int maxLength = -1;
                string columnName = string.Empty;
                DataRow dr = this.tablePermitImportEntry.NewRow();

                while (enumerator.MoveNext())
                {
                    columnName = enumerator.Key.ToString();
                    maxLength = this.tablePermitImportEntry.Columns[columnName].MaxLength;
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

                this.tablePermitImportEntry.Rows.Add(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Data given");
            }
        }
    }
}
namespace TerraScan.BusinessEntities.PermitImportFileTableAdapters
{
    
    
    public partial class PermitImportFile {


    }
}
