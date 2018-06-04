//--------------------------------------------------------------------------------------------
// <copyright file="DomainAuthorization.cs" company="Congruent">
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
// 16 Nov 07        Karthikeyan V        Form Created for user authentication.
//*********************************************************************************/

namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.Utilities;
    using System.Configuration;
    using System.Windows.Forms;
    using System.Reflection;
    using System.Data;
    using System.Text.RegularExpressions;
    using TerraScan.Helper;
    using System.Web.Services.Protocols;
    using System.Collections;
    using TerraScan.Common.Reports;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using System.DirectoryServices.ActiveDirectory;
    using System.DirectoryServices.Protocols;
    using System.DirectoryServices;
    using System.Threading;
    using System.Security.Principal;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using Microsoft.Win32;

    /// <summary>
    /// DomainAuthorization
    /// </summary>
    public static class DomainAuthorization
    {
        #region DllImport        

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        ////[DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        ////private unsafe static extern int FormatMessage(int dwFlags, ref IntPtr lpSource,
        ////    int dwMessageId, int dwLanguageId, ref String lpBuffer, int nSize, IntPtr* Arguments);

        ////[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        ////public extern static bool CloseHandle(IntPtr handle);

        ////[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        ////public extern static bool DuplicateToken(IntPtr ExistingTokenHandle,
        ////    int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);

        #endregion

        #region Methods

        /// <summary>
        /// Actives the directory exists.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <returns>bool</returns>
        public static bool ActiveDirectoryExists(string domainName)
        {
            try
            {
                return DirectoryEntry.Exists("LDAP://" + domainName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Caches the user validation.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="domainName">Name of the domain.</param>
        /// <returns>string</returns>
        public static string CacheUserValidation(string userName, string password, string domainName)
        {
            IntPtr tokenHandle = new IntPtr(0);
            IntPtr dupeTokenHandle = new IntPtr(0);

            //This parameter causes LogonUser to create a primary token.
            const int LOGON32_PROVIDER_DEFAULT = 0;
            ////const int LOGON32_LOGON_NETWORK = 3;
            const int LOGON32_LOGON_INTERACTIVE = 2;
            ////const int SecurityImpersonation = 2;

            tokenHandle = IntPtr.Zero;
            dupeTokenHandle = IntPtr.Zero;

             
            // Call LogonUser to obtain a handle to an access token.
            bool returnValue = LogonUser(userName, domainName, password,
                LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT,
                ref tokenHandle);
            string return2 = returnValue.ToString() ; 
             
            if (returnValue)
            {
                return "success";
            }
            else 
            {
                return "Invalid Login Name";
            }
        }

        #endregion
    }
}
