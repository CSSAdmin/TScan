// -------------------------------------------------------------------------------------------
// <copyright file="ExceptionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access exception related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;

    #endregion Namespace

    /// <summary>
    /// Main Class for the Exception Component
    /// </summary>
    public static class ExceptionComp
    {
        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="title">The title.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="machineName">Name of the machine.</param>
        /// <param name="appDomainName">Name of the app domain.</param>
        /// <param name="processId">The process id.</param>
        /// <param name="processName">Name of the process.</param>
        /// <param name="managedThreadName">Name of the managed thread.</param>
        /// <param name="win32ThreadId">The win32 thread id.</param>
        /// <param name="message">The message.</param>
        /// <param name="formattedMessage">The formatted message.</param>
        public static void LogException(int eventId, int priority, string severity, string title, DateTime timeStamp, string machineName, string appDomainName, string processId, string processName, string managedThreadName, string win32ThreadId, string message, string formattedMessage)
        {
            Hashtable exceptionDetails = new Hashtable();
            exceptionDetails.Add("eventID", eventId);
            exceptionDetails.Add("priority", priority);
            exceptionDetails.Add("severity", severity);
            exceptionDetails.Add("title", title);
            exceptionDetails.Add("timestamp", timeStamp);
            exceptionDetails.Add("machineName", machineName);
            exceptionDetails.Add("AppDomainName", appDomainName);
            exceptionDetails.Add("ProcessID", processId);
            exceptionDetails.Add("ProcessName", processName);
            exceptionDetails.Add("ThreadName", managedThreadName);
            exceptionDetails.Add("Win32ThreadId", win32ThreadId);
            exceptionDetails.Add("message", message);
            exceptionDetails.Add("formattedmessage", formattedMessage);
            DataProxy.ExecuteSP("f9001_pcins_Log", exceptionDetails);
        }
    }
}
