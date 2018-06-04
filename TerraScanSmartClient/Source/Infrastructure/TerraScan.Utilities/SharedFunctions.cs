// -------------------------------------------------------------------------------------------
// <copyright file="SharedFunctions.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods shared among various components.</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------
namespace TerraScan.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;
    using System.Text.RegularExpressions;
    using System.Collections;
    using System.Resources;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Windows.Forms;
    using System.Data;
    using System.Globalization;
    using TerraScan.BusinessEntities;
    using System.Xml;
    using System.IO;
    using TerraScan.UI.Controls;  
    using System.Drawing;
    using System.Configuration;

    /// <summary>
    /// Shared Functions is used to have all common method for the current application
    /// </summary>
    public class SharedFunctions
    {
        #region variables

        /// <summary>
        /// Instance of Resource manager class to access information from resource files.
        /// </summary>
        private static ResourceManager resourceManager = new ResourceManager("TerraScan.UI.Localization.Strings", System.Reflection.Assembly.Load("TerraScan.UI.Localization"));

        /// <summary>
        /// RealEstateQueryingField array used to store field names
        /// </summary>
        private static String[] realEstateQueryingField = new String[] { "STATEMENTID", "STATEMENTNUMBER", "ROLLYEAR", "LEVYYEAR", "DISTRICT", "PARCELID", "PARCELNUMBER", "SITUS", "MAPNUMBER", "LEGAL", "OWNERNAME", "TOTALVALUE", "ORIGINALTAX", "TOTALEXEMPTIONS", "TOTALDEDUCTIONS", "TAXABLEVALUE", "TAXBILLED" };

        /// <summary>
        /// QueryingField Dataset
        /// </summary>
        private static F9001QueryingFieldsData queryingFieldDataSet = new F9001QueryingFieldsData();

        /// <summary>
        /// QueryingField DataTable
        /// </summary>
        private static DataTable queryingFieldsDataTable = new DataTable();

        /// <summary>
        /// array which contains and and or
        /// </summary>
        private static String[] andOrOperatorArray = new String[] { "AND", "OR" };

        /// <summary>
        /// array which contains comparision operators
        /// </summary>
        private static String[] comparisionOperatorsArray = new String[] { ">=", "<=", "<>", "=", ">", "<", "!=", "!<", "!>", "LIKE" };

        /// <summary>
        /// array which contains comparision operators
        /// </summary>
        private static String[] comparisionOperatorsCombinations = new String[] { "=", ">=", "<=>", "!=<>" };

        /// <summary>
        /// array which contains operators specific to sql
        /// </summary>
        private static String[] otherOperatorsArray = new String[] { "EXISTS", "IN", "ALL" };

        /// <summary>
        /// array which contains wildcard Characters specific to sql
        /// </summary>
        private static String[] wildCardArray = new String[] { "%", "_", "[", "^", "]" };

        /// <summary>
        /// array which contains wildcard Characters specific to sql
        /// </summary>
        private static String queryOperators = new String(new char[] { '=', '>', '<', '!', ' ' });

        /// <summary>
        /// array which contains wildcard Characters specific to sql
        /// </summary>
        private static String possibleEndString = new String(new char[] { ' ', '(' });

        /// <summary>
        /// array which contains wildcard Characters specific to sql
        /// </summary>
        private static String possibleStartString = new String(new char[] { ' ', ')' });

        #endregion

        #region Enum

        /// <summary>
        /// Possible Validations for the query
        /// </summary>
        public enum PossibleValidation
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,

            /// <summary>
            /// NotValid Query
            /// </summary>
            NotValid,

            /// <summary>
            /// Need DataTyep Change
            /// </summary>
            NeedTypeChange
        }

        #endregion

        #region public methods

        /// <summary>
        /// Gets a string from the ResourceManager, returns the key if not found 
        /// plus optional debug information.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <returns>The value if found, return Keyvalue otherwise returns NULL.</returns>
        /// Commented by Thilak Raj
        public static string GetResourceString(string key)
        {
            string resourceString = string.Empty;

            if (key != null && key.Trim().Length != 0)
            {
                resourceString = resourceManager.GetString(key);

                if (resourceString == null)
                {
                    resourceString = key;
                }
                else
                {
                    resourceString = resourceString.Replace("\\n", "\n");
                }
            }

            return resourceString;
        }
                
        /// <summary>
        /// finds the filter criteria for the given field
        /// </summary>
        /// <param name="whereCondition"> The parsedWhereCondition used to find querycriteria for a field</param>        
        /// <param name="fieldName">the fieldname</param>
        /// <returns> filter criteria for the given field.</returns>
        public static string FindFilterCriteria(string whereCondition, string fieldName)
        {
            string filterCriteria = string.Empty;

            if (!string.IsNullOrEmpty(whereCondition))
            {
                fieldName = fieldName.ToUpper();
                int startIndex = -1;
                int endIndex = -1;

                startIndex = whereCondition.IndexOf(fieldName);
                if (startIndex > -1 && whereCondition.Length > startIndex + fieldName.Length && queryOperators.Contains(whereCondition[startIndex + fieldName.Length].ToString()))
                {
                    endIndex = whereCondition.IndexOf(")", startIndex);
                    if (endIndex > startIndex)
                    {
                        filterCriteria = whereCondition.Substring(startIndex, endIndex - startIndex);
                        filterCriteria = filterCriteria.Replace(fieldName, string.Empty).Trim();
                    }
                }
            }

            return filterCriteria;
        }

        /// <summary>
        /// Sets the query related property.
        /// </summary>
        /// <param name="queryControlArray">The query control array.</param>
        /// <param name="queryByForm">if set to <c>true</c> [query by form].</param>
        /// <param name="userDefinedWhereCondition">The user defined where condition.</param>
        /// <param name="queryControlColor">Color of the query control.</param>
        public static void SetQueryRelatedProperty(TerraScanTextBox[] queryControlArray, bool queryByForm, String userDefinedWhereCondition, Color queryControlColor)
        {
            if (queryControlArray == null)
            {
                return;
            }

            if (userDefinedWhereCondition == null)
            {
                userDefinedWhereCondition = String.Empty;
            }

            ////iterating each control
            foreach (TerraScanTextBox queryControl in queryControlArray)
            {
                if (queryControl != null && queryControl.IsQueryableFileld)
                {
                    if (queryByForm)
                    {
                        queryControl.BackColor = queryControlColor;
                        queryControl.LockKeyPress = false;
                        queryControl.MaxLength = Int16.MaxValue;
                        if (!queryControl.ValidateType.Equals(TerraScanTextBox.ControlvalidationType.Text))
                        {
                            queryControl.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                        }

                        ////setting values
                        if (!String.IsNullOrEmpty(userDefinedWhereCondition))
                        {
                            queryControl.Text = FindFilterCriteria(userDefinedWhereCondition, queryControl.QueryingFileldName);
                        }
                        else
                        {
                            queryControl.Text = String.Empty;
                        }
                    }
                    else
                    {
                        if (queryControlColor.Equals(Color.Empty))
                        {
                            queryControl.SetDefaultBackColor();
                        }
                        else
                        {
                            queryControl.BackColor = queryControlColor;
                        }

                        queryControl.SetMaxLength();
                        queryControl.SetDefaultValidType();                       

                        if (!queryControl.IsEditable)
                        {
                            queryControl.LockKeyPress = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// generate where condition
        /// </summary>
        /// <param name="whereCondition">whereCondition to parse.</param>
        /// <param name="referringDataTable">The referring data table.</param>
        /// <returns>
        /// The Arraylist having the generated sql conditions.
        /// </returns>
        public static ArrayList GenerateUserWhereCondition(string whereCondition, DataTable referringDataTable)
        {
            queryingFieldsDataTable = referringDataTable;
            ArrayList errorList = new ArrayList();
            ArrayList tempArr = new ArrayList();
            ArrayList fieldContentList = new ArrayList();
            string givenString = whereCondition.ToUpper();
            ////givenString = "((statementid ((2* or 3*)and(4*)))AND(statementnumber (<21 or <41) and >12/12/2005) AND (rollyear r* and statementid 2*))".ToUpper();

            int startIndex = 0;
            int endIndex = 0;
            int tempIndex = 0;
            string queryingField = string.Empty;
            string[] braceArray = new string[10];
            string fieldName = string.Empty;
            int fieldIndex = -1;
            string[] userFieldContainer = new string[queryingFieldsDataTable.Columns.Count];
            string conditionalOperator = string.Empty;
            StringCollection sc = new StringCollection();
            bool userConditionSucceed = true;

            if (givenString.Contains("BETWEEN "))
            {
                tempIndex = givenString.IndexOf("BETWEEN ");
                ////differentiate between-and and 'AND' condition - between-and to between-a1nd
                givenString = givenString.Insert(givenString.IndexOf(" AND", tempIndex) + 2, "1");
            }

            string querySubstring = givenString;
            bool pipeSymbolFound = false;
            ////separating fields with braces     

            do
            {
                if (startIndex > -1)
                {
                    startIndex = givenString.IndexOf("(", startIndex);
                }

                if (endIndex > -1)
                {
                    endIndex = givenString.IndexOf(")", endIndex);
                }

                if (startIndex > -1)
                {
                    if (startIndex > endIndex)
                    {
                        startIndex = Convert.ToInt16(sc[sc.Count - 1].Split(new char[] { ' ' })[1]);
                        tempIndex = startIndex % 9;
                        braceArray[tempIndex] = string.Concat(braceArray[tempIndex], " ", startIndex, "~", endIndex);
                        tempIndex = endIndex % 9;
                        braceArray[tempIndex] = string.Concat(braceArray[tempIndex], " ", endIndex, "~", startIndex);

                        startIndex = endIndex;
                        endIndex = endIndex + 1;
                        sc.RemoveAt(sc.Count - 1);
                        continue;
                    }

                    sc.Add("( " + startIndex.ToString());
                    startIndex = startIndex + 1;
                }
                else if (endIndex > -1)
                {
                    if (sc.Count > 0)
                    {
                        startIndex = Convert.ToInt16(sc[sc.Count - 1].Split(new char[] { ' ' })[1]);
                        tempIndex = startIndex % 9;
                        braceArray[tempIndex] = string.Concat(braceArray[tempIndex], " ", startIndex, "~", endIndex);
                        tempIndex = endIndex % 9;
                        braceArray[tempIndex] = string.Concat(braceArray[tempIndex], " ", endIndex, "~", startIndex);

                        startIndex = -1;
                        endIndex = endIndex + 1;
                        sc.RemoveAt(sc.Count - 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            while (startIndex > -1 || endIndex > -1);

            do
            {
                pipeSymbolFound = false;
                startIndex = -1;
                for (int i = queryingFieldsDataTable.Columns.Count - 1; i >= 0; i--)
                {
                    queryingField = queryingFieldsDataTable.Columns[i].ColumnName.ToUpper();
                    tempIndex = querySubstring.LastIndexOf(queryingField);

                    if (tempIndex > -1 && querySubstring.Length > tempIndex + queryingField.Length && queryOperators.Contains(querySubstring[tempIndex + queryingField.Length].ToString()))
                    {
                        if (tempIndex == 0 || " (".Contains(querySubstring[tempIndex - 1].ToString()))
                        {
                            if (startIndex < 0 || tempIndex > startIndex)
                            {
                                startIndex = tempIndex;
                                fieldName = queryingField;
                                fieldIndex = i;
                            }
                        }
                    }
                }

                if (startIndex > -1)
                {
                    tempIndex = querySubstring.IndexOf('|', startIndex);
                    if (tempIndex > startIndex)
                    {
                        querySubstring = querySubstring.Remove(tempIndex);
                        pipeSymbolFound = true;
                    }

                    if (querySubstring.Length != givenString.Length || pipeSymbolFound)
                    {
                        tempIndex = querySubstring.LastIndexOf("OR");
                        if (tempIndex > querySubstring.LastIndexOf("AND"))
                        {
                            if (tempIndex > startIndex)
                            {
                                querySubstring = querySubstring.Remove(tempIndex).Trim();
                            }
                        }
                        else
                        {
                            tempIndex = querySubstring.LastIndexOf("AND");
                            if (tempIndex > startIndex)
                            {
                                querySubstring = querySubstring.Remove(tempIndex).Trim();
                            }
                        }
                    }

                    tempIndex = querySubstring.Length - 1;
                    bool flag = true;
                    do
                    {
                        if (String.Equals(querySubstring[tempIndex].ToString(), "(") || String.Equals(querySubstring[tempIndex].ToString(), ")"))
                        {
                            string[] arrValue = braceArray[tempIndex % 9].Trim().Split(new char[] { ' ' });
                            foreach (string tempValue in arrValue)
                            {
                                if (tempIndex == Convert.ToInt16(tempValue.Split(new char[] { '~' })[0]))
                                {
                                    endIndex = Convert.ToInt16(tempValue.Split(new char[] { '~' })[1]);
                                    if (endIndex > startIndex + 1 && endIndex < querySubstring.Length)
                                    {
                                        ////userConditionSucceed = false;
                                        flag = false;
                                        break;
                                    }

                                    querySubstring = querySubstring.Remove(tempIndex);
                                    tempIndex--;
                                }
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    while (flag);

                    fieldContentList.Add(string.Concat(new string[] { querySubstring.Substring(startIndex), " @", fieldName, " ", startIndex.ToString(), " ", querySubstring.Length.ToString() }));

                    if (userConditionSucceed)
                    {
                        conditionalOperator = " AND ";
                        if (querySubstring.LastIndexOf("OR") > querySubstring.LastIndexOf("AND"))
                        {
                            conditionalOperator = " OR ";
                        }

                        if (!String.IsNullOrEmpty(userFieldContainer[fieldIndex]))
                        {
                            tempIndex = userFieldContainer[fieldIndex].LastIndexOf('@');
                            userFieldContainer[fieldIndex] = String.Concat(new string[] { userFieldContainer[fieldIndex].Remove(tempIndex), userFieldContainer[fieldIndex].Substring(tempIndex + 1), querySubstring.Substring(startIndex).Replace(fieldName, ""), "@", conditionalOperator });
                        }
                        else
                        {
                            userFieldContainer[fieldIndex] = String.Concat(querySubstring.Substring(startIndex), "@", conditionalOperator);
                        }
                    }

                    querySubstring = querySubstring.Remove(startIndex, querySubstring.Length - startIndex).Trim();
                }
            }
            while (startIndex > -1);

            givenString = ProcessWhereCondtionFields(givenString, fieldContentList);

            if (givenString.Contains("BETWEEN "))
            {
                tempIndex = givenString.IndexOf("BETWEEN ");
                ////differentiate between-and and 'AND' condition - between-and to between-a1nd
                givenString = givenString.Remove(givenString.IndexOf(" A1ND", tempIndex) + 2, 1);
            }

            string tempStringValue = string.Empty;
            StringBuilder userWhereCondition = new StringBuilder(string.Empty);
            foreach (string fieldValue in userFieldContainer)
            {
                tempStringValue = fieldValue;
                if (!String.IsNullOrEmpty(fieldValue))
                {
                    if (userWhereCondition.Length > 0)
                    {
                        userWhereCondition.Append(" AND ");
                    }

                    userWhereCondition.Append("(");
                    tempStringValue = tempStringValue.Remove(fieldValue.LastIndexOf("@"));
                    if (tempStringValue.Contains("BETWEEN "))
                    {
                        tempIndex = tempStringValue.IndexOf("BETWEEN ");
                        ////differentiate between-and and 'AND' condition - between-and to between-a1nd
                        tempStringValue = tempStringValue.Remove(tempStringValue.IndexOf(" A1ND", tempIndex) + 2, 1);
                    }

                    userWhereCondition.Append(tempStringValue.Trim());
                    userWhereCondition.Append(")");
                }
            }

            tempArr.Add("");
            tempArr.Add(givenString);
            tempArr.Add(userWhereCondition.ToString());

            return tempArr;
        }

        /// <summary>
        /// Gets the formatted SQL condition.
        /// </summary>
        /// <param name="controlArray">The control array.</param>
        /// <param name="referringDataTable">The referring data table.</param>
        /// <param name="whereClause">The where clause.</param>
        /// <param name="userFriendlyWhereCondition">The user friendly where condition.</param>
        /// <param name="invalidQuery">if set to <c>true</c> [invalid query].</param>
        public static void GetFormattedSqlCondition(TerraScanTextBox[] controlArray, DataTable referringDataTable, StringBuilder whereClause, StringBuilder userFriendlyWhereCondition, ref bool invalidQuery)
        {
            ////used to store parsed where condition
            string returnValue = String.Empty;
            ////checks where condition exisits
            bool previousValueExists = false;
            ////true when query is invalid
            invalidQuery = false;

            ////iterating each control
            for (int i = 0; i < controlArray.Length; i++)
            {
                TerraScanTextBox queryControl = controlArray.GetValue(i) as TerraScanTextBox;

                if (queryControl != null && queryControl.IsQueryableFileld && !String.IsNullOrEmpty(queryControl.Text.Trim()))
                {
                    //// ParseSqlWhereCondition returns string containing parsed query value 
                    returnValue = FormatSqlWhereCondition(queryControl.QueryingFileldName, queryControl.Text.Trim().ToUpper(), referringDataTable.Columns[queryControl.QueryingFileldName].DataType);

                    //// returnValue contains parsed query value for conditions like and, or, <, > etc... 
                    if (!String.IsNullOrEmpty(returnValue))
                    {
                        if (previousValueExists)
                        {
                            whereClause.Append(" AND ");
                        }

                        previousValueExists = true;
                        whereClause.Append("(");
                        whereClause.Append(returnValue);
                        whereClause.Append(")");
                    }
                    else
                    {
                        invalidQuery = true;
                        break;
                    }
                    //// whereCondition - used for requery part
                    if (userFriendlyWhereCondition.Length > 0)
                    {
                        userFriendlyWhereCondition.Append(" AND ");
                    }

                    userFriendlyWhereCondition.Append("(");
                    userFriendlyWhereCondition.Append(queryControl.QueryingFileldName.ToString());
                    userFriendlyWhereCondition.Append(" ");
                    userFriendlyWhereCondition.Append(queryControl.Text.Trim());
                    userFriendlyWhereCondition.Append(")");
                }
            }
        }

        /// <summary>
        /// format given where condition to execute query
        /// </summary>
        /// <param name="fieldName"> The filedname for the condition.</param>
        /// <param name="controlValue"> The value given in the control.</param>
        /// <param name="fieldDataType"> The datatype for the field given.</param>
        /// <returns> The string having the generated sql conditions.</returns>
        public static string FormatSqlWhereCondition(string fieldName, String controlValue, Type fieldDataType)
        {
            bool prevValExists = false;
            bool oroperatorPrevValExists = false;

            fieldName = string.Concat(fieldName, " ");
            string castFieldName = string.Concat("CAST(", fieldName, "AS VARCHAR(20)) ");
            StringBuilder whereClause = new StringBuilder();
            string[] returnValue = new string[5];
            StringBuilder tempwhereClause = new StringBuilder();
            string notOperatorValue = string.Empty;
            PossibleValidation possibleValidation = PossibleValidation.None;

            ////remove single quotes if any
            controlValue = controlValue.Replace("'", " ");

            if (controlValue.Contains("BETWEEN"))
            {
                ////differentiate between-and and 'AND' condition
                controlValue = controlValue.Insert(controlValue.IndexOf(" AND", controlValue.IndexOf("BETWEEN")) + 1, "1");
            }

            string[] andoperatorConditionResult = controlValue.Split(new string[] { " AND " }, StringSplitOptions.RemoveEmptyEntries);
            string tempValue = String.Empty;

            foreach (string andValue in andoperatorConditionResult)
            {
                notOperatorValue = string.Empty;
                if (andValue.Contains(" OR "))
                {
                    oroperatorPrevValExists = false;
                    tempwhereClause.Remove(0, tempwhereClause.Length);
                    string[] oroperatorConditionResult = andValue.Split(new string[] { " OR " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string oroperatorValue in oroperatorConditionResult)
                    {
                        tempValue = oroperatorValue.Trim().ToUpper();
                        FormatSqlWhereConditions(ref tempValue, fieldDataType, out possibleValidation, out notOperatorValue);

                        if (oroperatorPrevValExists)
                        {
                            tempwhereClause.Append(" OR ");
                        }

                        tempwhereClause.Append(notOperatorValue);
                        oroperatorPrevValExists = true;

                        if (possibleValidation.Equals(PossibleValidation.NotValid))
                        {
                            return String.Empty;
                        }
                        else if (possibleValidation.Equals(PossibleValidation.NeedTypeChange))
                        {
                            tempwhereClause.Append(castFieldName);
                        }
                        else
                        {
                            tempwhereClause.Append(fieldName);
                        }

                        tempwhereClause.Append(tempValue);
                    }

                    if (prevValExists)
                    {
                        whereClause.Append(" AND ");
                    }

                    prevValExists = true;
                    whereClause.Append(tempwhereClause);
                }
                else
                {
                    tempValue = andValue.Trim().ToUpper();
                    FormatSqlWhereConditions(ref tempValue, fieldDataType, out possibleValidation, out notOperatorValue);

                    if (prevValExists)
                    {
                        whereClause.Append(" AND ");
                    }

                    whereClause.Append(notOperatorValue);
                    prevValExists = true;
                    if (possibleValidation.Equals(PossibleValidation.NotValid))
                    {
                        return "";
                    }
                    else if (possibleValidation.Equals(PossibleValidation.NeedTypeChange))
                    {
                        whereClause.Append(castFieldName);
                    }
                    else
                    {
                        whereClause.Append(fieldName);
                    }

                    whereClause.Append(tempValue);
                }
            }

            return whereClause.ToString();
        }

        /// <summary>
        /// Parses the user where condition.
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <param name="referringDataTable">The referring data table.</param>
        /// <returns>combination of sql condition and possible validation with</returns>
        public static ArrayList ParseUserWhereCondition(string whereCondition, DataTable referringDataTable)
        {
            queryingFieldsDataTable = referringDataTable;
            ////GenerateUserWhereCondition(whereCondition);
            ArrayList errorList = new ArrayList();
            string givenString = whereCondition.ToUpper();           
            ////givenString = "((statementid 2* or 3* and 4*)AND(statementnumber (12/16/2005..12/25/2005) or 12/12/2005) AND (rollyear r*))".ToUpper();
            int startIndex = -1;
            int endIndex = -1;
            int tempIndex = -1;
            string queryingField = string.Empty;
            string tempValue = string.Empty;

            ////separating fields with braces
            foreach (DataColumn queryingFieldDataColumn in queryingFieldsDataTable.Columns)
            {
                queryingField = queryingFieldDataColumn.ColumnName.ToUpper();
                startIndex = -1;
                do
                {
                    startIndex = givenString.IndexOf(queryingField + " ", startIndex + 1);

                    if (startIndex > -1)
                    {
                        endIndex = -1;
                        foreach (DataColumn tempDataColumn in queryingFieldsDataTable.Columns)
                        {
                            tempValue = tempDataColumn.ColumnName.ToUpper();
                            tempIndex = givenString.IndexOf(tempValue + " ", startIndex + 1);
                            if (tempIndex > -1)
                            {
                                if (endIndex == -1)
                                {
                                    endIndex = tempIndex;
                                }

                                if (endIndex > -1 && tempIndex < endIndex)
                                {
                                    endIndex = tempIndex;
                                }
                            }
                        }

                        if (endIndex > startIndex)
                        {
                            string tempSubString = givenString.Substring(0, endIndex);

                            tempIndex = tempSubString.LastIndexOf("AND");
                            if (tempIndex > 0 && tempSubString.LastIndexOf("AND") > tempSubString.LastIndexOf("OR"))
                            {
                                givenString = givenString.Insert(tempIndex + "AND".Length, " ");
                                endIndex = tempIndex;
                            }
                            else
                            {
                                tempIndex = tempSubString.LastIndexOf("OR");
                                if (tempIndex > 0)
                                {
                                    givenString = givenString.Insert(tempIndex + "OR".Length, " ");
                                    endIndex = tempIndex;
                                }
                            }

                            givenString = givenString.Insert(endIndex, ") ");
                        }
                        else
                        {
                            givenString = givenString.Insert(givenString.Length, ")");
                        }

                        givenString = givenString.Insert(startIndex, "(");
                        startIndex = startIndex + queryingField.Length;
                    }
                }
                while (startIndex > -1);
            }

            givenString = String.Concat("(", givenString, ")");

            ////stores braces and its elements start and end index
            StringCollection sc = new StringCollection();
            ////enumerator for given wherecondition
            CharEnumerator chenumerator = givenString.GetEnumerator();
            int index = 0;
            int openBraceCount = 0;
            int closeBraceCount = 0;

            while (chenumerator.MoveNext())
            {
                switch (chenumerator.Current)
                {
                    case '(':
                        sc.Add(index.ToString() + " (");
                        openBraceCount++;
                        break;
                    case ')':
                        sc.Add(index.ToString() + " )");
                        closeBraceCount++;
                        break;
                }

                index++;
            }

            if ((sc.Count % 2) != 0 || (openBraceCount != closeBraceCount))
            {
                errorList.Add(false);
                return errorList;
            }
            ////temp array
            string[] intermArr = new string[sc.Count];
            ////temporary query string array
            ArrayList queryStringArr = new ArrayList();
            string[] fieldContainer = new string[20];
            fieldContainer.Initialize();
            string samp = String.Empty;
            string querySubString = String.Empty;
            string tempStringValue = string.Empty;
            int errorNo = 0;
            int intermArrIndex = 0;
            int prevStartIndex = 0;
            int prevEndIndex = 0;
            string conditionalOperator = string.Empty;

            for (int i = sc.Count - 1; i >= 0; i--)
            {
                if (sc[i].Substring(sc[i].Length - 1) == ")")
                {
                    intermArr[intermArrIndex++] = sc[i];
                }
                else if (sc[i].Substring(sc[i].Length - 1) == "(")
                {
                    startIndex = int.Parse(sc[i].Split(' ')[0]) + 1;
                    if (intermArrIndex > 0)
                    {
                        endIndex = int.Parse(intermArr[--intermArrIndex].Split(' ')[0]) - 1;
                    }
                    else
                    {
                        endIndex = -1;
                    }

                    if (startIndex > endIndex)
                    {
                        errorNo = 1;
                        break;
                    }

                    querySubString = givenString.Substring(startIndex, (endIndex - startIndex) + 1);

                    ////checks - any field is available in substring
                    for (int fieldIndex = 0; fieldIndex < queryingFieldsDataTable.Columns.Count; fieldIndex++)
                    {
                        conditionalOperator = " AND ";
                        samp = queryingFieldsDataTable.Columns[fieldIndex].ColumnName.ToUpper();
                        if (querySubString.StartsWith(samp))
                        {
                            tempStringValue = givenString.Substring(0, startIndex);
                            if (tempStringValue.LastIndexOf("OR") > tempStringValue.LastIndexOf("AND"))
                            {
                                conditionalOperator = " OR ";
                            }

                            if (!String.IsNullOrEmpty(fieldContainer[fieldIndex]))
                            {
                                fieldContainer[fieldIndex] = String.Concat(new string[] { fieldContainer[fieldIndex].Remove(fieldContainer[fieldIndex].LastIndexOf('@')), fieldContainer[fieldIndex].Split(new char[] { '@' })[1], querySubString.Replace(samp, String.Empty).Trim(), "@", conditionalOperator });
                            }
                            else
                            {
                                fieldContainer[fieldIndex] = String.Concat(querySubString, "@", conditionalOperator);
                            }

                            break;
                        }
                    }

                    if (querySubString.Trim() == "AND" || querySubString.Trim() == "OR")
                    {
                        errorNo = 1;
                        break;
                    }

                    for (int k = 0; k < queryStringArr.Count; k++)
                    {
                        index = queryStringArr[k].ToString().LastIndexOf("@");

                        samp = queryStringArr[k].ToString().Substring(index + 1);
                        prevStartIndex = int.Parse(samp.Split(' ')[0]);
                        prevEndIndex = int.Parse(samp.Split(' ')[1]);
                        if (startIndex < prevStartIndex && prevStartIndex < endIndex && startIndex < prevEndIndex && prevEndIndex < endIndex)
                        {
                            if (querySubString.Contains("(" + queryStringArr[k].ToString().Substring(0, index) + ")"))
                            {
                                querySubString = querySubString.Remove(prevStartIndex - startIndex, queryStringArr[k].ToString().Substring(0, index).Length);
                                querySubString = querySubString.Insert(prevStartIndex - startIndex, k.ToString());
                            }
                        }
                    }

                    queryStringArr.Add(querySubString + "@" + startIndex.ToString() + " " + endIndex.ToString());
                }
            }

            if (errorNo == 1)
            {
                errorList.Add(false);
                return errorList;
            }

            ArrayList tempArr = new ArrayList();

            if (queryStringArr.Count > 0)
            {
                string sampStr = queryStringArr[queryStringArr.Count - 1].ToString();
                tempArr = ParseUserWhereCondition(queryStringArr, queryStringArr.Count - 1, "");
            }

            StringBuilder userWhereCondition = new StringBuilder(string.Empty);
            foreach (string fieldValue in fieldContainer)
            {
                tempStringValue = fieldValue;
                if (!String.IsNullOrEmpty(fieldValue))
                {
                    if (userWhereCondition.Length > 0)
                    {
                        userWhereCondition.Append(" AND ");
                    }

                    userWhereCondition.Append("(");
                    tempStringValue = tempStringValue.Remove(fieldValue.LastIndexOf("@"));
                    userWhereCondition.Append(tempStringValue.Trim());
                    userWhereCondition.Append(")");
                }
            }

            tempArr.Add(userWhereCondition);

            return tempArr;
        }

        /// <summary>
        /// Sets the data grid view position.
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="commentRow">The comment row.</param>
        public static void SetDataGridViewPosition(Control controlName, int commentRow)
        {
            DataGridView tempDataGridview = (DataGridView)controlName;
            if (tempDataGridview.Rows.Count > 0 && commentRow >= 0)
            {
                tempDataGridview.Rows[Convert.ToInt32(commentRow)].Selected = true;
                tempDataGridview.CurrentCell = tempDataGridview[0, Convert.ToInt32(commentRow)];
            }
        }

        /// <summary>
        /// Creates the empty rows.
        /// </summary>
        /// <param name="sourceDataTable">The source data table.</param>
        /// <param name="maxRowCount">The max row count.</param>
        /// <returns>DataTable</returns>
        public static DataTable CreateEmptyRows(DataTable sourceDataTable, int maxRowCount)
        {
            int defaultRowsCount = 0;
            DataRow tempRow;
            if (sourceDataTable.Rows.Count < maxRowCount)
            {
                defaultRowsCount = maxRowCount - sourceDataTable.Rows.Count;
                for (int i = 0; i < defaultRowsCount; i++)
                {
                    tempRow = sourceDataTable.NewRow();
                    for (int j = 0; j < sourceDataTable.Columns.Count; j++)
                    {
                        if (sourceDataTable.Columns[j].DataType.ToString() == "System.Int32")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Int16")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Boolean")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else
                        {
                            tempRow[j] = string.Empty;
                        }
                    }

                    sourceDataTable.Rows.Add(tempRow);
                }
            }

            return sourceDataTable;
        }

        /// <summary>
        /// XMLs the parser.
        /// </summary>
        /// <param name="xmlString">The XML string.</param>
        /// <returns>Returns XmlTextReader</returns>
        public static XmlTextReader XmlParser(string xmlString)
        {
            StringReader stringReader = new StringReader(xmlString);

            System.Xml.XmlTextReader textReader = new System.Xml.XmlTextReader(stringReader);

            return textReader;
        }

        /// <summary>
        /// XMLParser
        /// </summary>
        /// <param name="xmlresultset">xmlresultset</param>
        /// <param name="tagName" >the value in tag</param>
        /// <returns>XmlTextReader</returns>
        public static XmlTextReader XmlParser(string xmlresultset, string tagName)
        {
            StringReader sr = new StringReader(xmlresultset);

            System.Xml.XmlTextReader tr = new System.Xml.XmlTextReader(xmlresultset);

            return tr;

            /* //XmlDocument xdoc = new XmlDocument();
            //xdoc.LoadXml(xmlresultset);

            //XmlNode rootNode = xdoc.GetElementsByTagName(tagName).Item(0);

            //rootNode.Attributes.RemoveAll();

            //IEnumerator resultNodes = rootNode.ChildNodes.GetEnumerator();

            //while (resultNodes.MoveNext())
            //{
            //    IEnumerator node = ((XmlNode)resultNodes.Current).ChildNodes.GetEnumerator();
            //    while (node.MoveNext())
            //    {
            //        ((XmlNode)node.Current).Attributes.RemoveAll();
            //    }
            //}

            //xmlresultset = xdoc.InnerXml;

            //XmlTextReader reader = new XmlTextReader(new StringReader(xmlresultset));
            //reader.WhitespaceHandling = WhitespaceHandling.None;
            //return reader; */
        } 

        #endregion

        #region private methods

        /// <summary>
        /// Frames the sqlcondition based on the input.
        /// </summary>
        /// <param name="inputValue">The input value from the User interface.</param>
        /// <param name="fieldDataType">used to find field datatype</param>
        /// <param name="possibleValidation">The possible validation.</param>
        /// <param name="notConditionalOperator">The not conditional operator.</param>
        /// <returns>
        /// integer value
        /// </returns>
        private static int FormatSqlWhereConditions(ref string inputValue, Type fieldDataType, out PossibleValidation possibleValidation, out string notConditionalOperator)
        {
            string returnValue = inputValue;
            string tempValue = String.Empty;
            possibleValidation = PossibleValidation.None;
            notConditionalOperator = String.Empty;

            if (returnValue.StartsWith("NOT "))
            {
                notConditionalOperator = " NOT ";
                returnValue = returnValue.Remove(0, 3).Trim();
            }

            if (returnValue.StartsWith("LIKE "))
            {
                returnValue = returnValue.Remove(0, 4).Trim();
                if (!fieldDataType.Equals(typeof(string)))
                {
                    possibleValidation = IsValidValue(returnValue, fieldDataType);
                }

                inputValue = string.Concat(notConditionalOperator, " LIKE '", returnValue, "'");
                notConditionalOperator = String.Empty;
                return 1;
            }

            if (returnValue.StartsWith("BETWEEN "))
            {
                returnValue = returnValue.Remove(0, 7).Trim();

                string[] arrValue = returnValue.Split(new string[] { " 1AND" }, StringSplitOptions.RemoveEmptyEntries);
                if (arrValue.Length == 2)
                {
                    inputValue = string.Concat(new string[] { notConditionalOperator, " BETWEEN", " '", arrValue[0].Trim(), "' AND '", arrValue[1].Trim(), "'" });
                    notConditionalOperator = String.Empty;

                    if (!fieldDataType.Equals(typeof(string)))
                    {
                        ////for array 1
                        possibleValidation = IsValidValue(arrValue[0].Trim(), fieldDataType);
                        if (possibleValidation.Equals(PossibleValidation.NotValid))
                        {
                            return 0;
                        }
                        else if (possibleValidation.Equals(PossibleValidation.NeedTypeChange))
                        {
                            possibleValidation = IsValidValue(arrValue[1].Trim(), fieldDataType);

                            if (possibleValidation.Equals(PossibleValidation.NotValid))
                            {
                                return 0;
                            }

                            return 1;
                        }

                        inputValue = inputValue.Replace("'", "");
                    }

                    return 1;
                }
                else
                {
                    possibleValidation = PossibleValidation.NotValid;
                    return 0;
                }
            }

            foreach (string comparisionOperator in comparisionOperatorsCombinations)
            {
                if (returnValue.StartsWith(comparisionOperator[0].ToString()))
                {
                    tempValue = comparisionOperator[0].ToString();
                    returnValue = returnValue.Remove(0, 1);
                    for (int i = 1; i < comparisionOperator.Length; i++)
                    {
                        if (returnValue.StartsWith(comparisionOperator[i].ToString()))
                        {
                            returnValue = returnValue.Remove(0, 1);
                            tempValue = string.Concat(tempValue, comparisionOperator[i]);
                            break;
                        }
                    }

                    if (!fieldDataType.Equals(typeof(string)))
                    {
                        possibleValidation = IsValidValue(returnValue.Trim(), fieldDataType);

                        if (possibleValidation.Equals(PossibleValidation.None))
                        {
                            inputValue = String.Concat(tempValue, " ", returnValue.Trim());
                            return 1;
                        }
                    }

                    inputValue = String.Concat(tempValue, " '", returnValue.Trim(), "'");

                    return 1;
                }
            }

            if (returnValue.StartsWith("!!"))
            {
                returnValue = ReplaceString(returnValue);
                returnValue = returnValue.Replace("!!", "NOT LIKE '%");
                returnValue = returnValue.Insert(returnValue.Length, "%'");
                inputValue = returnValue;
                return 1;
            }

            if (returnValue.StartsWith("!"))
            {
                returnValue = ReplaceString(returnValue);
                returnValue = returnValue.Replace("!", "LIKE '%");
                returnValue = returnValue.Insert(returnValue.Length, "%'");
                inputValue = returnValue;
                return 1;
            }

            if (returnValue.Contains("*") || returnValue.Contains("?"))
            {
                returnValue = ReplaceString(returnValue);
                returnValue = returnValue.Insert(0, "LIKE '");
                returnValue = returnValue.Insert(returnValue.Length, "'");
                inputValue = returnValue;
                return 1;
            }

            if (returnValue == "BLANK")
            {
                returnValue = "= ' '";
                inputValue = returnValue;
                return 1;
            }

            if (returnValue == "NULL")
            {
                returnValue = "IS NULL";
                inputValue = returnValue;
                return 1;
            }

            inputValue = "LIKE '" + returnValue + "'";
            return 1;
        }

        /// <summary>
        /// Gets parsed where condition
        /// </summary>
        /// <param name="queryStringArr"> Queries string array.</param>
        /// <param name="queryStringIndex"> index of queryStringArr.</param>
        /// <param name="fieldName"> The filedname for the condition.</param>
        /// <returns> The Arraylist having the generated sql conditions.</returns>
        private static ArrayList ParseUserWhereCondition(ArrayList queryStringArr, int queryStringIndex, string fieldName)
        {
            ArrayList tempArr = new ArrayList();
            ArrayList resultArr = new ArrayList();
            PossibleValidation possibleValidation = PossibleValidation.None;
            string notOperatorValue = String.Empty;
            resultArr.Add("");
            resultArr.Add("");

            string returnValue = string.Empty;
            string whereClause = string.Empty;
            StringBuilder tempOrConditionValue = new StringBuilder(String.Empty);

            string andValue = String.Empty, oroperatorValue = String.Empty;
            bool prevValExists = false;
            bool oroperatorPrevValExists = false;

            // string _processStr = _givenStr;
            int startIndex = -1;
            int endIndex = -1;
            int tempIndex = -1;

            string sampStr = queryStringArr[queryStringIndex].ToString();
            sampStr = sampStr.Remove(sampStr.LastIndexOf("@")).Trim();

            if (sampStr.Contains("BETWEEN"))
            {
                ////differentiate between-and and 'AND' condition
                sampStr = sampStr.Insert(sampStr.IndexOf("AND", sampStr.IndexOf("BETWEEN") + 1), "1");
            }

            string[] andArrValue = sampStr.Split(new string[] { " AND " }, StringSplitOptions.None);

            for (int i = 0; i < andArrValue.Length; i++)
            {
                startIndex = endIndex = -1;
                andValue = andArrValue[i];
                if (andValue.Contains(" OR "))
                {
                    oroperatorPrevValExists = false;
                    tempOrConditionValue.Remove(0, tempOrConditionValue.Length);
                    string[] oroperatorArrayValue = andValue.Split(new string[] { " OR " }, StringSplitOptions.None);
                    for (int j = 0; j < oroperatorArrayValue.Length; j++)
                    {
                        startIndex = endIndex = -1;
                        oroperatorValue = oroperatorArrayValue[j];

                        for (int fieldCount = 0; fieldCount < queryingFieldsDataTable.Columns.Count; fieldCount++)
                        {
                            if (oroperatorValue.StartsWith(queryingFieldsDataTable.Columns[fieldCount].ColumnName.ToUpper() + " "))
                            {
                                string[] arr = oroperatorValue.Split(' ');
                                fieldName = arr[0];
                                oroperatorValue = string.Join(" ", arr, 1, arr.Length - 1);
                                break;
                            }
                        }

                        startIndex = oroperatorValue.IndexOf("(", startIndex + 1);
                        endIndex = oroperatorValue.IndexOf(")", endIndex + 1);

                        if (startIndex > -1 && endIndex > -1 && startIndex < endIndex)
                        {
                            tempIndex = int.Parse(oroperatorValue.Substring(startIndex + 1, (endIndex - startIndex) - 1));
                            tempArr = ParseUserWhereCondition(queryStringArr, tempIndex, fieldName);

                            if (!string.IsNullOrEmpty(tempArr[0].ToString()))
                            {
                                resultArr[0] = tempArr[0];
                            }

                            if (!string.IsNullOrEmpty(tempArr[1].ToString()))
                            {
                                if (oroperatorPrevValExists)
                                {
                                    tempOrConditionValue.Append(" OR ");
                                }

                                oroperatorPrevValExists = true;
                                tempOrConditionValue.Append("(" + tempArr[1].ToString() + ")");
                            }
                        }
                        else
                        {
                            ////remove single quotes if any
                            oroperatorValue = oroperatorValue.Replace("'", " ").Trim();
                            if (!String.IsNullOrEmpty(fieldName))
                            {
                                FormatSqlWhereConditions(ref oroperatorValue, queryingFieldsDataTable.Columns[fieldName].DataType, out possibleValidation, out notOperatorValue);
                            }

                            if (oroperatorPrevValExists)
                            {
                                tempOrConditionValue.Append(" OR ");
                            }

                            oroperatorPrevValExists = true;
                            tempOrConditionValue.Append(notOperatorValue + fieldName + " " + oroperatorValue);
                            oroperatorArrayValue[j] = String.Concat(notOperatorValue + fieldName + " " + oroperatorValue);
                        }
                    }
                    ////checks for and operator related fields exists
                    if (prevValExists)
                    {
                        resultArr[1] += " AND ";
                    }

                    prevValExists = true;
                    resultArr[1] += tempOrConditionValue.ToString();
                    andArrValue[i] = String.Join(" OR ", oroperatorArrayValue);
                }
                else
                {
                    for (int fieldCount = 0; fieldCount < queryingFieldsDataTable.Columns.Count; fieldCount++)
                    {
                        if (andValue.StartsWith(queryingFieldsDataTable.Columns[fieldCount].ColumnName.ToUpper() + " "))
                        {
                            string[] arr = andValue.Split(' ');
                            fieldName = arr[0];
                            andValue = string.Join(" ", arr, 1, arr.Length - 1);
                            break;
                        }
                    }

                    startIndex = andValue.IndexOf("(", startIndex + 1);
                    endIndex = andValue.IndexOf(")", endIndex + 1);
                    if (startIndex > -1 && endIndex > -1 && startIndex < endIndex)
                    {
                        tempIndex = int.Parse(andValue.Substring(startIndex + 1, (endIndex - startIndex) - 1));
                        tempArr = ParseUserWhereCondition(queryStringArr, tempIndex, fieldName);

                        if (!string.IsNullOrEmpty(tempArr[0].ToString()))
                        {
                            resultArr[0] = tempArr[0];
                        }

                        if (!string.IsNullOrEmpty(tempArr[1].ToString()))
                        {
                            if (prevValExists)
                            {
                                resultArr[1] += " AND ";
                            }

                            prevValExists = true;
                            resultArr[1] += "(" + tempArr[1].ToString() + ")";
                        }
                    }
                    else
                    {
                        ////remove single quotes if any
                        andValue = andValue.Replace("'", " ").Trim();
                        if (!String.IsNullOrEmpty(fieldName))
                        {
                            FormatSqlWhereConditions(ref andValue, queryingFieldsDataTable.Columns[fieldName].DataType, out possibleValidation, out notOperatorValue);
                        }

                        if (prevValExists)
                        {
                            resultArr[1] += " AND ";
                        }

                        prevValExists = true;
                        resultArr[1] += notOperatorValue + fieldName + " " + andValue;
                        andArrValue[i] = notOperatorValue + fieldName + " " + andValue;
                    }
                }
            }

            queryStringArr[queryStringIndex] = string.Join(" AND ", andArrValue);

            return resultArr;
        }
       
        /// <summary>
        /// process field values - fill appropriate fieldname to the field content
        /// </summary>
        /// <param name="whereCondition">the where condition to process</param>
        /// <param name="fieldContentList">field content list contains the fieldvalues</param>
        /// <returns>processed wherecondition</returns>
        private static string ProcessWhereCondtionFields(string whereCondition, ArrayList fieldContentList)
        {
            int startIndex = 0;
            int endIndex = 0;
            int tempIndex = 0;
            string fieldValue = String.Empty;
            string orvalue = string.Empty;
            string andValue = string.Empty;
            string fieldIndexContent = string.Empty;
            string fieldName = string.Empty;

            for (int i = 0; i < fieldContentList.Count; i++)
            {
                fieldValue = fieldContentList[i].ToString();
                tempIndex = fieldValue.LastIndexOf("@");
                fieldIndexContent = fieldValue.Substring(tempIndex + 1);
                fieldName = string.Concat(fieldIndexContent.Split(new char[] { ' ' })[0], " ");
                fieldValue = fieldValue.Remove(tempIndex);                
                fieldValue = fieldValue.Replace(fieldName, " ");

                string[] andoperatorConditionResult = fieldValue.Split(new string[] { "AND" }, StringSplitOptions.RemoveEmptyEntries);

                for (int andValueCount = 0; andValueCount < andoperatorConditionResult.Length; andValueCount++)
                {
                    andValue = andoperatorConditionResult.GetValue(andValueCount).ToString();

                    if (!String.IsNullOrEmpty(andValue.Trim()))
                    {
                        string[] oroperatorConditionResult = andValue.Split(new string[] { "OR" }, StringSplitOptions.RemoveEmptyEntries);

                        if (oroperatorConditionResult.Length > 1)
                        {
                            for (int orvalueCount = 0; orvalueCount < oroperatorConditionResult.Length; orvalueCount++)
                            {
                                tempIndex = 0;
                                orvalue = oroperatorConditionResult.GetValue(orvalueCount).ToString();

                                if (!String.IsNullOrEmpty(orvalue.Trim()))
                                {                                   
                                    if ((orvalue.StartsWith(" ") || orvalue.StartsWith("(")) && (orvalue.EndsWith(" ") || orvalue.EndsWith(")")))
                                    {
                                        orvalue = orvalue.Trim();
                                        do
                                        {
                                            if (!String.Equals(orvalue[tempIndex++].ToString(), "("))
                                            {
                                                break;
                                            }
                                        }
                                        while (true);

                                        if (orvalue.StartsWith("NOT"))
                                        {
                                            tempIndex = tempIndex + 3;
                                        }

                                        orvalue = orvalue.Insert(tempIndex - 1, String.Concat(" ", fieldName));
                                        oroperatorConditionResult.SetValue(string.Concat(orvalue, " "), orvalueCount);
                                    }
                                }
                            }

                            andoperatorConditionResult.SetValue(String.Join(" OR ", oroperatorConditionResult).TrimStart(), andValueCount);
                        }
                        else
                        {
                            tempIndex = 0;                           
                            if ((andValue.StartsWith(" ") || andValue.StartsWith("(")) && (andValue.EndsWith(" ") || andValue.EndsWith(")")))
                            {
                                andValue = andValue.Trim();
                                do
                                {
                                    if (!String.Equals(andValue[tempIndex++].ToString(), "("))
                                    {
                                        break;
                                    }
                                }
                                while (true);

                                if (andValue.StartsWith("NOT"))
                                {
                                    tempIndex = tempIndex + 3;
                                }

                                andValue = andValue.Insert(tempIndex - 1, String.Concat(" ", fieldName));
                                andoperatorConditionResult.SetValue(string.Concat(andValue, " "), andValueCount);
                            }
                        }
                    }

                    fieldValue = String.Join(" AND ", andoperatorConditionResult).TrimStart();
                }

                fieldContentList[i] = String.Concat(fieldValue, "@", fieldIndexContent);
            }

            for (int i = 0; i < fieldContentList.Count; i++)
            {
                tempIndex = fieldContentList[i].ToString().LastIndexOf("@");
                fieldIndexContent = fieldContentList[i].ToString().Substring(tempIndex + 1);
                string[] tempArr = fieldIndexContent.Split(new char[] { ' ' });
                startIndex = Convert.ToInt16(tempArr[1]);
                endIndex = Convert.ToInt16(tempArr[2]);
                whereCondition = whereCondition.Remove(startIndex, endIndex - startIndex);
                whereCondition = whereCondition.Insert(startIndex, fieldContentList[i].ToString().Substring(0, tempIndex - 1));
            }

            return whereCondition;
        }

        /// <summary>
        /// checks whether the given value is valid or not
        /// </summary>
        /// <param name="inputValue">The string to be verified</param>
        /// <param name="dataType">Type of the data.</param>
        /// <returns>returns enum possible validation</returns>
        private static PossibleValidation IsValidValue(string inputValue, Type dataType)
        {
            int parsedValue = 0;
            decimal parseddecimalValue = 0;
            PossibleValidation possiblities = PossibleValidation.None;
            foreach (string wildcardChar in wildCardArray)
            {
                if (inputValue.Contains(wildcardChar))
                {
                    inputValue = inputValue.Replace(wildcardChar, "");
                    possiblities = PossibleValidation.NeedTypeChange;
                }
            }

            if (dataType.Equals(typeof(int)))
            {
                if (!int.TryParse(inputValue, out parsedValue))
                {
                    possiblities = PossibleValidation.NotValid;
                }
            }
            else if (dataType.Equals(typeof(decimal)))
            {
                if (!decimal.TryParse(inputValue, out parseddecimalValue))
                {
                    possiblities = PossibleValidation.NotValid;
                }
            }            

            return possiblities;
        }

        /// <summary>
        /// Replace the occurence of * with % and ? with _
        /// </summary>
        /// <param name="inputValue"> The string to be replaced</param>
        /// <returns> The string with replaced value.</returns>
        private static string ReplaceString(string inputValue)
        {
            if (inputValue.Contains("*") || inputValue.Contains("?"))
            {
                inputValue = inputValue.Replace("*", "%");
                inputValue = inputValue.Replace("?", "_");
            }

            return inputValue;
        }

        #endregion
    }
}
