using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TerraScan.Common
{
    /// <summary>
    /// This interface defines for the Permission Services
    /// </summary>
    public interface IPermissionService
    {
        void SetPermissions(Form CurrentForm);
        void SetPermissions(UserControl CurrentSmartPart);
    }
}
