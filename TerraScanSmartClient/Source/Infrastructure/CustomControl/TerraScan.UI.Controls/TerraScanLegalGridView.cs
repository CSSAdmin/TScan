namespace TerraScan.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Data;

    public partial class TerraScanLegalGridView : System.Windows.Forms.DataGridView
    {
        public TerraScanLegalGridView()
        {
            //InitializeComponent();
        }

        #region delegateDeclaration
        /// <summary>
        /// Declare delegate for Before Row Enter.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        /// <returns>the Boolvalue</returns>
        public delegate void TabKeyPressEventHandler(Keys e);

        #endregion

        #region eventDecalration

        /// <summary>
        /// Declare the event, which is associated with the
        /// delegate BeforeRowEnter(object, DataGridViewCellEventArgs).  
        /// </summary>          
        public event TabKeyPressEventHandler TabKeyPress;

        #endregion

        /// <summary>
        /// Raises the before row enter event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// <returns>the bool value</returns>
        protected virtual void OnTabKeyPress(Keys e)
        {
            // If an event has no subscribers registerd, it will 
            // evaluate to null. The test checks that the value is not
            // null, ensuring that there are subsribers before
            // calling the event itself.
            if (this.TabKeyPress != null)
            {
                this.TabKeyPress(e);  // Notify Subscribers
            }
        }

        //public LegalGridView(IContainer container)
        //{
        //    container.Add(this);

        //    InitializeComponent();
        //}

        /// <summary>
        /// Processes keys, such as the TAB, ESCAPE, RETURN, and ARROW keys, used to control dialog boxes.
        /// </summary>
        /// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"></see> values that represents the key or keys to process.</param>
        /// <returns>
        /// true if the key was processed; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidCastException">The key pressed would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"></see> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"></see> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"></see>.</exception>
        /// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"></see> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"></see> property to true. </exception>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                ////check for editing control to trigger manual navigation
                if (this.EditingControl != null)
                {
                    if (keyData.Equals(Keys.Tab))
                    {
                        this.OnTabKeyPress(keyData);
                        return false;
                    }
                }
            }
            catch
            {
            }

            return base.ProcessDialogKey(keyData);
        }
    }
}
