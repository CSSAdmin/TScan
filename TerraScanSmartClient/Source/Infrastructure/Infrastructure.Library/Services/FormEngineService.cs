//--------------------------------------------------------------------------------------------
// <copyright file="FormEngineService.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the FormEngineService.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// May-20-2009      Sadha Shivudu      	Created
//*********************************************************************************/
namespace TerraScan.Infrastructure.Library.Services
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TerraScan.Infrastructure.Interface.Services;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Common;
    using System.Reflection;
    using Microsoft.Practices.CompositeUI.Services;
    using Microsoft.Practices.CompositeUI.Configuration;

    #endregion Namespace

    /// <summary>
    /// FormEngineService class
    /// </summary>
    public class FormEngineService : IFormEngineService
    {
        #region IFormEngineService Members
        
        /// <summary>
        /// Gets the smart part.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="optionalParameter">The optional parameter.</param>
        /// <param name="parentWorkItem">The parent work item.</param>
        /// <returns>The generated smartpart.</returns>
        public UserControl GetSmartPart(int formId, object[] optionalParameter, WorkItem parentWorkItem)
        {
            string currentWorkItemName = string.Empty;
            UserControl currentSmartPart = new UserControl();
            WorkItem currentWorkItem = new WorkItem();

            //// get the form information like formFile,permissions etc..
            FormInfo formInfo = TerraScanCommon.GetFormInfo(formId);

            if (!string.IsNullOrEmpty(formInfo.formFile))
            {
                currentWorkItemName = formInfo.formFile.Trim() + "WorkItem";
                //// check for the parent workitem contains the current workitem
                if (!parentWorkItem.Items.Contains(currentWorkItemName))
                {
                    ////create the instance of current smartpart.
                    currentSmartPart = CreateSmartPartInstance(formInfo.formFile, optionalParameter, currentWorkItemName, parentWorkItem);
                }
                else
                {
                    currentWorkItem = (WorkItem)parentWorkItem.Items.Get(currentWorkItemName);
                    currentSmartPart = (UserControl)currentWorkItem.Items.Get(formInfo.formFile);
                    currentWorkItem.Terminate();
                    currentSmartPart.Dispose();
                    currentSmartPart = CreateSmartPartInstance(formInfo.formFile, optionalParameter, currentWorkItemName, parentWorkItem);
                }

                return currentSmartPart;
            }
            else
            {
                ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six);
                return null;
            }
        }
        
        /// <summary>
        /// Gets the form.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="optionalParameter">The optional parameter.</param>
        /// <param name="parentWorkItem">The parent work item.</param>
        /// <returns>The generated form</returns>
        public Form GetForm(int formId, object[] optionalParameter, WorkItem parentWorkItem)
        {
            string currentWorkItemName = string.Empty;
            FormInfo formInfo;
            Form currentForm = new Form();
            WorkItem currentWorkItem = new WorkItem();

            //// get the form information like formFile,permissions etc..
            formInfo = TerraScanCommon.GetFormInfo(formId);

            if (!string.IsNullOrEmpty(formInfo.formFile))
            {
                try
                {
                    currentWorkItemName = formInfo.formFile.Trim() + "WorkItem";

                    //// check for the parent workitem contains the current workitem
                    if (!parentWorkItem.Items.Contains(currentWorkItemName))
                    {
                        ////create the instance of current form.
                        currentForm = this.CreateFormInstance(formInfo.formFile, optionalParameter, currentWorkItemName, parentWorkItem);
                    }
                    else
                    {
                        currentWorkItem = (WorkItem)parentWorkItem.Items.Get(currentWorkItemName);
                        currentForm = (Form)currentWorkItem.Items.Get(formInfo.formFile);
                        currentWorkItem.Terminate();
                        currentForm.Dispose();
                        currentForm = this.CreateFormInstance(formInfo.formFile, optionalParameter, currentWorkItemName, parentWorkItem);
                    }

                    return currentForm;
                }
                catch (Exception ex)
                {
                    if (!ex.Message.Equals("Cannot access a disposed object.\r\nObject name: 'Infragistics.Win.UltraWinGrid.UltraGridColumn'."))
                    {
                        ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six, formId);
                    }
                    return null;
                }
            }
            else
            {
                ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six, formId);
                return null;
            }
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Creates the smart part instance.
        /// </summary>
        /// <param name="formFile">The form file.</param>
        /// <param name="optionalParameter">The optional parameter.</param>
        /// <param name="currentWorkItemName">Name of the current work item.</param>
        /// <param name="parentWorkItem">The parent work item.</param>
        /// <returns>The generated smartpart instance.</returns>
        private UserControl CreateSmartPartInstance(string formFile, object[] optionalParameter, string currentWorkItemName, WorkItem parentWorkItem)
        {
            string assemblyName = string.Empty;
            WorkItem currentWorkItem = new WorkItem();
            UserControl currentSmartPart = new UserControl();
            if (!string.IsNullOrEmpty(formFile))
            {
                assemblyName = formFile.Remove(formFile.LastIndexOf("."));
                assemblyName = string.Concat(assemblyName, ".dll");
                //// load the module and get the loaded assembly to create the instance for smartpart and workitem.
                System.Reflection.Assembly assembly = this.LoadModule(assemblyName, parentWorkItem);
                currentWorkItem = (WorkItem)assembly.CreateInstance(currentWorkItemName);
                if (optionalParameter != null)
                {
                    currentSmartPart = (UserControl)assembly.CreateInstance(formFile, true, BindingFlags.CreateInstance, null, optionalParameter, System.Globalization.CultureInfo.CurrentUICulture, null);
                }
                else
                {
                    currentSmartPart = (UserControl)assembly.CreateInstance(formFile);
                }

                //// add the current workitem to the parent workitem
                parentWorkItem.Items.Add(currentWorkItem, currentWorkItemName);
                //// add the current smartpart to the current workitem
                currentWorkItem.Items.Add(currentSmartPart, formFile);
            }

            return currentSmartPart;
        }
        
        /// <summary>
        /// Creates the form instance.
        /// </summary>
        /// <param name="formFile">The form file.</param>
        /// <param name="optionalParameter">The optional parameter.</param>
        /// <param name="currentWorkItemName">Name of the current work item.</param>
        /// <param name="parentWorkItem">The parent work item.</param>
        /// <returns>The generated form instance.</returns>
        private Form CreateFormInstance(string formFile, object[] optionalParameter, string currentWorkItemName, WorkItem parentWorkItem)
        {
            string assemblyName = string.Empty;
            WorkItem currentWorkItem = new WorkItem();
            Form currentForm = new Form();
            if (!string.IsNullOrEmpty(formFile))
            {
                assemblyName = formFile.Remove(formFile.LastIndexOf("."));
                assemblyName = string.Concat(assemblyName, ".dll");
                //// load the module and get the loaded assembly to create the instance for current form and current workitem.
                System.Reflection.Assembly assembly = this.LoadModule(assemblyName, parentWorkItem);
                currentWorkItem = (WorkItem)assembly.CreateInstance(currentWorkItemName);
                if (optionalParameter != null)
                {
                    currentForm = (Form)assembly.CreateInstance(formFile, true, BindingFlags.CreateInstance, null, optionalParameter, System.Globalization.CultureInfo.CurrentUICulture, null);
                }
                else
                {
                    currentForm = (Form)assembly.CreateInstance(formFile);
                }

                //// add the current workitem to the parent workitem
                parentWorkItem.Items.Add(currentWorkItem, currentWorkItemName);
                //// add the current form to the current workitem
                currentWorkItem.Items.Add(currentForm, formFile);
            }

            return currentForm;
        }
        
        /// <summary>
        /// Loads the module.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="moduleWorkItem">The module work item.</param>
        /// <returns>The loaded module assembly.</returns>
        private Assembly LoadModule(string assemblyName, WorkItem moduleWorkItem)
        {
            //// check the assemly in the loaded modules
            if (!CheckModuleLoaded(assemblyName, moduleWorkItem))
            {
                IModuleInfo[] objModuleInfo = new IModuleInfo[1];
                ModuleInfo moduleInfo = new ModuleInfo(assemblyName);
                objModuleInfo[0] = moduleInfo;
                ////load the module and add it to moduleWorkItem using moduleLoaderService
                moduleWorkItem.Services.Get<IModuleLoaderService>().Load(moduleWorkItem, objModuleInfo);
            }

            ////enumerate the loaded modules in the application and get the assembly
            IList<LoadedModuleInfo> listLoadedModules;
            listLoadedModules = moduleWorkItem.Services.Get<IModuleLoaderService>().LoadedModules;
            foreach (LoadedModuleInfo loadedModule in listLoadedModules)
            {
                if (loadedModule.Assembly.ManifestModule.Name.Equals(assemblyName))
                {
                    return loadedModule.Assembly;
                }
            }

            return null;
        }
        
        /// <summary>
        /// Checks the module loaded.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="moduleWorkItem">The module work item.</param>
        /// <returns>The status of loaded module.</returns>
        private bool CheckModuleLoaded(string assemblyName, WorkItem moduleWorkItem)
        {
            IList<LoadedModuleInfo> listLoadedModules;

            ////enumerate the loaded modules and check for the assembly exists
            listLoadedModules = moduleWorkItem.Services.Get<IModuleLoaderService>().LoadedModules;
            foreach (LoadedModuleInfo loadedModule in listLoadedModules)
            {
                if (loadedModule.Assembly.ManifestModule.Name.Equals(assemblyName)) return true;
            }

            return false;
        }

        #endregion
    }
}
