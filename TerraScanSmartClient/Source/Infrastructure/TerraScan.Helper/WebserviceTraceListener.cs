//--------------------------------------------------------------------------------------------
// <copyright file="WebServiceTraceListener.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Login.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    Created
// 04 May 06        Jyothi        Form Created for user authentication.
//*********************************************************************************/
namespace TerraScan.Helper
{
    using System;
    using System.Configuration;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;
    using System.IO;
    using System.Collections;
    using System.Diagnostics;
    using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using WCFService;   
    
    /// <summary>
    /// WebserviceTraceListener
    /// </summary>
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class WebserviceTraceListener : CustomTraceListener
    {
        /// <summary>
        /// TerraScanService
        /// </summary>
        private static SmartClientServiceClient terraScanService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:WebserviceTraceListener"/> class.
        /// </summary>
        public WebserviceTraceListener()
            : base()
        {
            terraScanService = new SmartClientServiceClient();
            //if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["WebServiceUrl"]))
            //{
            //    terraScanService.Url = ConfigurationManager.AppSettings["WebServiceUrl"];
            //}
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="message">Message</param>
        public override void Write(string message)
        {
            this.CallWebService(0, 5, TraceEventType.Information, string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, null, null, message);
        }

        /// <summary>
        /// Override of WriteLine -> calls Write() 
        /// </summary>
        /// <param name="message">message</param>
        public override void WriteLine(string message)
        {
            this.Write(message);
        }

        /// <summary>
        /// TraceData
        /// </summary>
        /// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache"></see> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType"></see> values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">The trace data to emit.</param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (data is LogEntry)
            {
                LogEntry logEntry = data as LogEntry;
              
                this.CallWebService(logEntry);                
            }
            else if (data is string)
            {
                this.Write(data as string);
            }
            else
            {
                base.TraceData(eventCache, source, eventType, id, data);
            }
        }

        /// <summary>
        /// CallWebService
        /// </summary>
        /// <param name="eventId">eventid</param>
        /// <param name="priority">priority</param>
        /// <param name="severity">severity</param>
        /// <param name="title">title</param>
        /// <param name="timeStamp">timeStamp</param>
        /// <param name="machineName">machineName</param>
        /// <param name="appDomainName">appDomainName</param>
        /// <param name="processId">processId</param>
        /// <param name="processName">processName</param>
        /// <param name="managedThreadName">managedThreadName</param>
        /// <param name="win32ThreadId">win32ThreadId</param>
        /// <param name="message">message</param>
        private void CallWebService(int eventId, int priority, TraceEventType severity, string title, DateTime timeStamp, string machineName, string appDomainName, string processId, string processName, string managedThreadName, string win32ThreadId, string message)
        {
            try
            {
                /////TerraScan.WebService.TerraScanService terraScanService = new global::TerraScan.Helper.TerraScan.WebService.TerraScanService();
                terraScanService.LogException(eventId, priority, severity.ToString(), title, timeStamp, machineName, appDomainName, processId, processName, managedThreadName, win32ThreadId, message, message);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// CallWebService
        /// </summary>
        /// <param name="logEntry">logEntry</param>
        private void CallWebService(LogEntry logEntry)
        {
            try
            {
                string formattedMessage;
                if (Formatter != null)
                {
                    formattedMessage = Formatter.Format(logEntry);
                }
                else
                {
                    formattedMessage = logEntry.Message;
                }

                //////TerraScan.WebService.TerraScanService terraScanService = new global::TerraScan.Helper.TerraScan.WebService.TerraScanService();
                terraScanService.LogException(logEntry.EventId, logEntry.Priority, logEntry.Severity.ToString(), logEntry.Title, DateTime.Now, logEntry.MachineName, logEntry.AppDomainName, logEntry.ProcessId, logEntry.ProcessName, logEntry.ManagedThreadName, logEntry.Win32ThreadId, logEntry.Message, formattedMessage);
            }
            catch (Exception)
            {
            }
        }
    }
}
