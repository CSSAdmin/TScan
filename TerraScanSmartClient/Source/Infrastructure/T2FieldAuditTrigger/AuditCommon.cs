using System;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Security.Principal;


public partial class Triggers
{
    // Enter existing table or view for the target and uncomment the attribute line
    // [Microsoft.SqlServer.Server.SqlTrigger (Name="AuditCommon", Target="Table1", Event="FOR UPDATE")]
    [SqlTrigger(Name = "AuditCommon", Target = "tTS_Form", Event = "FOR UPDATE, INSERT, DELETE")]
    public static void AuditCommon()
    {
        SqlTriggerContext triggContext = SqlContext.TriggerContext;
        SqlCommand sqlComm = new SqlCommand();
        SqlPipe sqlP = SqlContext.Pipe;
        DataRow insertedRow;
        DataRow deletedRow;
        string tableName;
        string selectedColumns = "";
        string keyColumn = "";
        string keyValue = "";
        int rowsAffected;
        int userID;

        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            try
            {
                conn.Open();
                sqlComm.Connection = conn;
                ////SqlCommand cmd = new SqlCommand("SELECT OBJECT_NAME(resource_associated_entity_id) FROM sys.dm_tran_locks WHERE request_session_id = @@SPID AND resource_type = 'OBJECT'", conn);
                SqlCommand cmd = new SqlCommand("SELECT * INTO T2AuditTriggerForTerraScan FROM Inserted WHERE 1 = 0; SELECT TOP 1 TableName FROM (SELECT TableName, TotalCount FROM (SELECT OBJECT_NAME(object_id) AS TableName ,COUNT(OBJECT_NAME(object_id)) TotalCount FROM sys.columns O JOIN sys.syscolumns T ON O.[name] = T.[name] WHERE OBJECT_NAME(T.id) = 'T2AuditTriggerForTerraScan' AND OBJECT_NAME(object_id) IN (SELECT	OBJECT_NAME(resource_associated_entity_id) FROM sys.dm_tran_locks WHERE request_session_id = @@SPID AND resource_type = 'OBJECT') GROUP	BY OBJECT_NAME(object_id)) AS ObjectsLocked) T2DerivedAudit WHERE TableName NOT IN('T2AuditTriggerForTerraScan') ORDER BY TotalCount DESC; DROP TABLE T2AuditTriggerForTerraScan;", conn);
                tableName = cmd.ExecuteScalar().ToString();
                cmd.CommandText = "SELECT C.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK, INFORMATION_SCHEMA.KEY_COLUMN_USAGE C  WHERE PK.TABLE_NAME = '" + tableName.Trim() + "' AND CONSTRAINT_TYPE = 'PRIMARY KEY' AND C.TABLE_NAME = PK.TABLE_NAME AND C.CONSTRAINT_NAME = PK.CONSTRAINT_NAME";
                if (cmd.ExecuteScalar() != null)
                {
                    keyColumn = cmd.ExecuteScalar().ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(keyColumn))
                    {
                        cmd.CommandText = "SELECT sys.identity_columns.[name] FROM sys.identity_columns WHERE OBJECT_NAME(object_id) ='" + tableName + "'";
                        if (cmd.ExecuteScalar() != null)
                        {
                            keyColumn = cmd.ExecuteScalar().ToString();
                        }
                    }
                }

                cmd.CommandText = "SELECT ISNULL(UpdatedBy, 0) FROM INSERTED"; //"SELECT UserID FROM tTS_User WHERE Name_Net = SUSER_SNAME()";
                if (cmd.ExecuteScalar() != null)
                {
                    userID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                }
                else
                {
                    userID = 0;
                }

                SqlDataAdapter auditAdapter = new SqlDataAdapter("SELECT ColumnName = [name] FROM sys.columns WHERE OBJECT_NAME(object_id) = '" + tableName + "' AND [name] IN(SELECT FieldName FROM tTR_Audit_TableList JOIN tTR_Audit_FieldList ON tTR_Audit_TableList.AuditTableID = tTR_Audit_FieldList.AuditTableID WHERE TableName = '" + tableName + "' AND tTR_Audit_TableList.IsDeleted = 0 AND tTR_Audit_FieldList.IsDeleted = 0) ORDER BY column_id ASC", conn);
                DataTable selectedAudit = new DataTable();
                auditAdapter.Fill(selectedAudit);
                if (selectedAudit.Rows.Count != 0)
                {
                    selectedColumns = "";
                    for (int totalConfColumns = 0; totalConfColumns < selectedAudit.Rows.Count; totalConfColumns++)
                    {
                        selectedColumns = selectedColumns + selectedAudit.Rows[totalConfColumns]["ColumnName"].ToString() + ", ";
                    }

                    selectedColumns = selectedColumns.Trim().Substring(0, selectedColumns.Length - 2);
                    auditAdapter.SelectCommand.CommandText = "SELECT " + keyColumn + "," + selectedColumns + " FROM INSERTED";
                    DataTable insertedTable = new DataTable();
                    auditAdapter.Fill(insertedTable);
                    auditAdapter.SelectCommand.CommandText = "SELECT " + keyColumn + "," + selectedColumns + " FROM DELETED";
                    DataTable deletedTable = new DataTable();
                    auditAdapter.Fill(deletedTable);

                    if (triggContext.TriggerAction == TriggerAction.Update)
                    {
                        rowsAffected = deletedTable.Rows.Count;
                        for (int totalNofRecords = rowsAffected - 1; totalNofRecords >= 0; totalNofRecords--)
                        {
                            insertedRow = insertedTable.Rows[totalNofRecords];
                            deletedRow = deletedTable.Rows[totalNofRecords];
                            keyValue = deletedTable.Rows[totalNofRecords][keyColumn].ToString();
                            foreach (DataColumn column in insertedTable.Columns)
                            {
                                if (!insertedRow[column.Ordinal].Equals(deletedRow[column.Ordinal]))
                                {
                                    sqlComm.CommandText = "INSERT INTO tTR_Audit(TableName, PK, AuditType, FieldName, Old, New, UpdateDateTime, UserID, Comment) SELECT '" + tableName + "', '" + keyValue + "', 'U', '" + column.ColumnName + "', NULLIF('" + deletedRow[column.Ordinal].ToString().Replace("'", "''") + "', ''), NULLIF('" + insertedRow[column.Ordinal].ToString().Replace("'", "''") + "', ''), GETDATE(), " + userID + ", NULL";
                                    sqlP.Send(sqlComm.CommandText);
                                    sqlP.ExecuteAndSend(sqlComm);
                                }
                            }
                        }
                    }
                    else if (triggContext.TriggerAction == TriggerAction.Delete)
                    {
                        rowsAffected = deletedTable.Rows.Count;
                        for (int totalNofRecords = rowsAffected - 1; totalNofRecords >= 0; totalNofRecords--)
                        {
                            deletedRow = deletedTable.Rows[totalNofRecords];
                            keyValue = deletedTable.Rows[totalNofRecords][keyColumn].ToString();
                            foreach (DataColumn column in deletedTable.Columns)
                            {
                                sqlComm.CommandText = "INSERT INTO tTR_Audit(TableName, PK, AuditType, FieldName, Old, New, UpdateDateTime, UserID, Comment) SELECT '" + tableName + "', '" + keyValue + "', 'D', '" + column.ColumnName + "', '" + deletedRow[column.Ordinal].ToString().Replace("'", "''") + "', GETDATE(), " + userID + ", NULL";
                                sqlP.Send(sqlComm.CommandText);
                                sqlP.ExecuteAndSend(sqlComm);
                            }
                        }
                    }
                    else if (triggContext.TriggerAction == TriggerAction.Insert)
                    {
                        rowsAffected = insertedTable.Rows.Count;
                        for (int totalNofRecords = rowsAffected - 1; totalNofRecords >= 0; totalNofRecords--)
                        {
                            insertedRow = insertedTable.Rows[totalNofRecords];
                            keyValue = insertedTable.Rows[totalNofRecords][keyColumn].ToString();
                            //foreach (DataColumn column in insertedTable.Columns)
                            //{
                            sqlComm.CommandText = "INSERT INTO tTR_Audit(TableName, PK, AuditType, FieldName, Old, New, UpdateDateTime, UserID, Comment) SELECT '" + tableName + "', '" + keyValue + "', 'I', '" + keyColumn + "', NULL, '" + keyValue + "', GETDATE(), " + userID + ", NULL";
                            sqlP.Send(keyColumn);
                            sqlP.Send(sqlComm.CommandText);
                            sqlP.ExecuteAndSend(sqlComm);
                            //}
                        }
                    }
                }

                conn.Close();
            }
            catch (InvalidOperationException ex)
            {
                sqlP.Send(ex.ToString());
            }
        }
        // Replace with your own code
        SqlContext.Pipe.Send("Trigger FIRED");
    }
}
