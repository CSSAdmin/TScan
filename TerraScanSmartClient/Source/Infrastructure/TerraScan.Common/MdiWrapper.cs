// ---------------------------------------------------------------------
// <copyright file="MdiWrapper.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This wrapper enables application to easily manage the MDI Child forms</summary>
// ---------------------------------------------------------------------
// Author:  Prakash
// Date:    25th Mar 2005
// ---------------------------------------------------------------------
// Change History
// ---------------------------------------------------------------------
// Date             Author      Description
// ----------       ---------   ----------------------------------------
// 28th Mar 2006    Thilak     Created
// ---------------------------------------------------------------------
namespace TerraScan.Common
{
    ////using System;
    ////using System.Collections.Generic;
    ////using System.Text;

    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Text;
    using System.Windows.Forms;
   
    /// <summary>
    /// Provides methods for managing the MDI child forms
    /// </summary>
    public class MdiWrapper : BasePage
    {
        /// <summary>
        /// Static string array that holds all the child form names
        /// </summary>
        ////public static List<string> ChildCollection = new List<string>();
        private static List<string> childCollection = new List<string>();

        /// <summary>
        /// Static string array that holds all the child form text
        /// </summary>
        private static List<string> childNames = new List<string>();

        /// <summary>
        /// string value that holds the currently generated formname
        /// </summary>
        private string currentFormName = string.Empty;

        /// <summary>
        /// string value that holds the currently generated forms DllName
        /// </summary>
        private string formDLLName = string.Empty;

        /// <summary>
        /// string value that holds the currently generated forms DllName
        /// </summary>
        private int formID; 

        /// <summary>
        /// Property to the child form Names
        /// </summary>
        public static List<string> ChildCollection
        {
            get { return childCollection; }

            // set { childCollection = value; }
        }

        /// <summary>
        /// Property to the child form Text
        /// </summary>
        public static List<string> ChildNames
        {
            get { return childNames; }

            // set { childNames = value; }
        }

        /// <summary>
        /// Property to the Current Form Name
        /// </summary>
        /// <value>currentFormName</value>
        public string CurrentFormName
        {
            get { return this.currentFormName; }
            set { this.currentFormName = value; }
        }

        /// <summary>
        /// Property to the Current Form Name
        /// </summary>
        /// <value>currentFormName</value>
        public int FormID
        {
            get { return this.formID; }
            set { this.formID = value; }
        }

        /// <summary>
        /// Property to the FormDLLName
        /// </summary>
        /// <value>FormDLLName</value>
        public string FormDLLName
        {
            get { return this.formDLLName; }
            set { this.formDLLName = value; }
        }

        /// <summary>
        /// Method to check whether a form is already a MDI child form
        /// </summary>
        /// <param name="childName">Form that needs to be validated</param>
        /// <returns>True/False</returns>
        public static bool HasChild(string childName)
        {
            return ChildCollection.Contains(childName);
        }        

        /// <summary>
        /// Gets the index of the child form in the MDIChildren collection
        /// </summary>
        /// <param name="childName">Form that needs to be validated</param>
        /// <returns>An int representing the index</returns>
        public static int GetChildIndex(string childName)
        {
            return ChildCollection.IndexOf(childName);
        }

        /// <summary>
        /// getChildList method which retrieve child collection names
        /// </summary>
        /// <returns>string array which holds the child collection names</returns>
        public static string[] GetChildList()
        {
            string[] childList = new string[childCollection.Count];
            childList = childCollection.ToArray();
            return childList;
        }

        /// <summary>
        /// getChildNames method which retrieve child collection text
        /// </summary>
        /// <returns>string array which holds the child collection text</returns>
        public static string[] GetChildArray()
        {
            string[] childNameList = new string[childNames.Count];
            childNameList = childNames.ToArray();
            return childNameList;
        }

        /// <summary>
        /// Gets the Form object from the MDIchildren collection
        /// </summary>
        /// <param name="childName">Form that needs to be retrieved</param>
        /// <returns>Form object</returns>
        public Form GetChild(string childName)
        {
            return MdiChildren[GetChildIndex(childName)];
        }
        
        /// <summary>
        /// The child form will be captured when it gets loaded
        /// </summary>
        /// <param name="e">OnLoad event args</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ////Add to the collection only if the calling form is a MDI child form

            if (this.IsMdiContainer == false && this.MdiParent != null)
            {
                ChildCollection.Add(this.formID.ToString());
                ChildNames.Add(this.CurrentFormName);
                this.IntimateParent(true);
            }
        }

        /// <summary>
        /// Release the child form index when it is unloaded
        /// </summary>
        /// <param name="e">OnClosed event args</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove from the collection only if the calling form is a MDI child form
            if (this.IsMdiContainer == false && this.MdiParent != null)
            {
                ChildCollection.Remove(this.Name.ToString());
                childNames.Remove(this.CurrentFormName);
                this.IntimateParent(false);
            }
        }

        /// <summary>
        /// Intimate Parent on Every Child Form Load or Unload
        /// </summary>
        /// <param name="active">active form</param>
        private void IntimateParent(bool active)
        {
            // TODO: Direct access of MDIParent should be replaced with some generic code.
         //   MainForm mainForm = (MainForm)this.MdiParent;
           // mainForm.ShowActiveForms(active);
        }
    }
}
