namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Reflection;

    /// <summary>
    /// TerraScanMessageBox
    /// </summary>
    public partial class TerraScanMessageBox : Form
    {
         #region Variables

        /// <summary>
        /// Header Text of The Form
        /// </summary>
        private string formHeaderText = string.Empty;

        /// <summary>
        /// Error Message is set to Empty
        /// </summary>
        private string errorMessage = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MessageForm class.
        /// </summary>
        /// <param name="errorMsg">the error Message</param>
        /// <param name="headerText">the Header Text</param>
        /// <param name="errorButtons">errorButtons</param>
        /// <param name="errorType">errorType</param>
        public TerraScanMessageBox(string errorMsg, string headerText, MessageBoxButtons errorButtons, MessageBoxIcon errorType)
        {
            this.InitializeComponent();
            this.formHeaderText = headerText;
            this.errorMessage = errorMsg.Replace("\\n", "\n");             
            if (errorButtons.Equals(MessageBoxButtons.OK))
            {
                this.PanelOk.Visible = true;
                this.PanelYesNo.Visible = false;
                this.PanelYesNoCancel.Visible = false;
                this.PanelOkCancel.Visible = false;
            }
            else if (errorButtons.Equals(MessageBoxButtons.YesNo))
            {
                this.PanelOk.Visible = false;
                this.PanelYesNo.Visible = true;
                this.PanelYesNoCancel.Visible = false;
                this.PanelOkCancel.Visible = false;
            }
            else if (errorButtons.Equals(MessageBoxButtons.YesNoCancel))
            {
                this.PanelOk.Visible = false;
                this.PanelYesNo.Visible = false;
                this.PanelYesNoCancel.Visible = true;
                this.PanelOkCancel.Visible = false;
            }
            else if (errorButtons.Equals(MessageBoxButtons.OKCancel))
            {
                this.PanelOk.Visible = false;
                this.PanelYesNo.Visible = false;
                this.PanelYesNoCancel.Visible = false;
                this.PanelOkCancel.Visible = true;
            }
            else
            {
                this.PanelOk.Visible = false;
                this.PanelYesNo.Visible = false;
                this.PanelYesNoCancel.Visible = false;
                this.PanelOkCancel.Visible = false;
            }
            this.MessageLabel.Text = errorMsg;
            if (this.MessageLabel.Height + 50 >= 120)
            {
                this.Height = this.MessageLabel.Height + 100;
            }
            else
            {
                this.MessageLabel.Height = 153;
            }
            this.ErrorTypeIconPictureBox.Image = this.ErrorTypeIconList.Images[errorType.ToString()];
        }

        /// <summary>
        /// TerraScanMessageBox
        /// </summary>
        public TerraScanMessageBox()
        {
            this.InitializeComponent();
        }

        #endregion
        /*
        #region Enums

        /// <summary>
        /// Enumerator ErrorType
        /// </summary>
        public enum ErrorType
        {
            /// <summary>
            /// Type Error = 0 
            /// </summary>
            Error = 0,

            /// <summary>
            /// Type Warning = 1
            /// </summary>
            Warning = 1,

            /// <summary>
            /// Type Information = 2
            /// </summary>
            Information = 2
        }

        /// <summary>
        /// Enumerator ErrorButtons
        /// </summary>
        public enum ErrorButtons
        {
            /// <summary>
            /// ErrorButton Ok = 0 
            /// </summary>
            Ok = 0,

            /// <summary>
            /// ErrorButton YesNo = 1
            /// </summary>
            YesNo = 1,

            /// <summary>
            /// ErrorButton YesNoCancel = 2
            /// </summary>
            YesNoCancel = 2
        }

        #endregion
        */
        #region PublicMethods

        /// <summary>
        /// Method to show the MessageForm as Dailog
        /// </summary>
        /// <param name="errorMsg">the error Message</param>
        /// <param name="headerText">the Header Text</param>
        /// <param name="errorButtons">errorButtons</param>
        /// <param name="errorType">errorType</param>
        /// <returns>the DialogResult value</returns>
        public static DialogResult Show(string errorMsg, string headerText, MessageBoxButtons errorButtons, MessageBoxIcon errorType)
        {
            errorMsg = errorMsg.Replace("\\n", "\n");
            TerraScanMessageBox messageForm = new TerraScanMessageBox(errorMsg, headerText, errorButtons, errorType);
            messageForm.AutoSize = true;
            messageForm.AutoScaleMode = AutoScaleMode.Inherit;
            messageForm.AutoSizeMode = AutoSizeMode.GrowAndShrink; 
            messageForm.StartPosition = FormStartPosition.CenterParent;
           
            messageForm.ShowDialog();
            return messageForm.DialogResult;
        }
        
        #endregion

        /// <summary>
        /// Load event of form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">eventargs</param>
        private void MessageForm_Load(object sender, EventArgs e)
        {
            this.Text = this.formHeaderText.Trim();
            this.MessageLabel.Text = this.errorMessage.Trim();
        }
    }
}