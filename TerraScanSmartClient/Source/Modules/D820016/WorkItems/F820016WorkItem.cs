using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using System.Data;
using TerraScan.BusinessEntities;
using TerraScan.Helper;

namespace D820016
{
    public partial class F820016WorkItem : WorkItem
    {
        #region Base Methods
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion Base Methods.

        #region CRUD Methods.

        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        public DataSet RdlToCode_FillCombo(string storedProcedureName)
        {
            return WSHelper.RdlToCode_FillCombo(storedProcedureName);
        }

        public DataSet RdlToCode_Get(string getXMLString, string formId)
        {
            return WSHelper.RdlToCode_Get(getXMLString, formId);
        }

        public int RdlToCode_Save(string saveXMLString, string formId)
        {
            return WSHelper.RdlToCode_Save(saveXMLString, formId);
        }

        public void RdlToCode_Delete(string deleteXMLString, string formId)
        {
            WSHelper.RdlToCode_Delete(deleteXMLString, formId);
        }

        #endregion CRUD Methods.
    }
}
