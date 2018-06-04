// -------------------------------------------------------------------------------------------
// <copyright file="ExceptionManager.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access database</summary>
// Release history
// VERSION	DESCRIPTION 
// -------------------------------------------------------------------------------------------
namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Services.Protocols;
    using System.Data.SqlClient;
    using System.Configuration;
    ////using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
    using System.Windows;
    using System.Windows.Forms;
    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

    /// <summary>
    /// ExceptionManager to handle all exception
    /// </summary>
    public static class ExceptionManager
    {
        #region Enum
        /// <summary>
        /// Enumerator Action Type
        /// </summary>
        public enum ActionType
        {
            /// <summary>
            /// CloseApplication 
            /// </summary>
            CloseApplication = 0,

            /// <summary>
            /// CloseCurrentForm
            /// </summary>
            CloseCurrentForm = 1,

            /// <summary>
            /// Display
            /// </summary>
            Display = 2,

            /// <summary>
            /// Just Log
            /// </summary>
            JustLog = 3
        }
        #endregion enum

        /// <summary>
        /// Manages the expection.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="source">The source.</param>
        public static void ManageException(string errorMessage, Exception ex, ActionType actionType, Form source)
        {
            try
            {
                ExceptionPolicy.HandleException(ex, ConfigurationWrapper.UIPolicyName);
                Application.UseWaitCursor = false;
                ShowExceptionViewer(errorMessage, actionType, source);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Manages the expection.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static bool ManageException(Exception ex)
        {
            try
            {
                ExceptionPolicy.HandleException(ex, ConfigurationWrapper.UIPolicyName);
                Application.UseWaitCursor = false;
                string tempErrorMessage = "An exception has occurred, and the application must close.  Do you wish to restart the application?";//ConfigurationWrapper.ExceptionMessage.Replace("\\n", "\n");
                ////MessageBox.Show(tempErrorMessage, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if ((MessageBox.Show(tempErrorMessage, ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No))
                {
                    return false;
                }
            }
            catch (Exception)
            {
            }
            return true;
        }

        /// <summary>
        /// Manages the exception.
        /// </summary>
        /// <param name="errorMessage">The error message to display to user.</param>
        /// <param name="errorLog">The error message to Log in database.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="source">The source.</param>
        public static void ManageException(string errorMessage, string errorLog, ActionType actionType, Form source)
        {
            Exception ex = new Exception(errorLog);
            ExceptionPolicy.HandleException(ex, ConfigurationWrapper.UIPolicyName);
            Application.UseWaitCursor = false;
            ShowExceptionViewer(errorMessage, actionType, source);
        }

        /// <summary>
        /// Manage Expection
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="source">The source.</param>
        public static void ManageException(Exception ex, ActionType actionType, System.Windows.Forms.Form source)
        {
            if (ex != null)
            {
                ExceptionPolicy.HandleException(ex, ConfigurationWrapper.UIPolicyName);
                Application.UseWaitCursor = false;

                //// If Soap Exception override action type to close current Form.
                if (ex.GetType().Equals(new SoapException()))
                {
                    ShowExceptionViewer(ConfigurationWrapper.ExceptionMessage, ActionType.CloseCurrentForm, source);
                }
                else
                {
                    if (actionType != ActionType.JustLog)
                    {
                        ShowExceptionViewer(ConfigurationWrapper.ExceptionMessage, actionType, source);
                    }
                }
            }
        }

        /// <summary>
        /// Shows the exception viewer.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="source">The source.</param>
        private static void ShowExceptionViewer(string errorMessage, ActionType actionType, System.Windows.Forms.Form source)
        {
            try
            {
                string tempErrorMessage = errorMessage.Replace("\\n", "\n");
                MessageBox.Show(tempErrorMessage, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                switch (actionType)
                {
                    case ExceptionManager.ActionType.CloseCurrentForm:
                        {
                            source.Close();
                            break;
                        }

                    case ExceptionManager.ActionType.CloseApplication:
                        {
                            Application.Exit(new System.ComponentModel.CancelEventArgs(true));
                            break;
                        }
                }
            }
            catch (Exception)
            {
            }
            //////ExceptionViewer exceptionViewer = new ExceptionViewer(errorMessage, actionType);
            //////exceptionViewer.Owner = source;
            //////exceptionViewer.ShowDialog();
            //////exceptionViewer.Dispose();
        }
    }
}
