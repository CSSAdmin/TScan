using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using TerraScan.Common;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using System.Windows.Forms;

namespace TerraScan.UI
{
    public class ExternalComponentService : IService1
    {
        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// </summary>
        /// <param name="shFrmDataContract"></param>
        public void ServiceShowForm(ShowFormDatacontract shFrmDataContract)
        {
            ShellForm.ActiveParentForm.GetExternalShowForm(shFrmDataContract.FormID, shFrmDataContract.Parameter1, shFrmDataContract.Parameter2, shFrmDataContract.Parameter3, shFrmDataContract.Parameter4);
            if (ShellForm.ActiveParentForm.WindowState == FormWindowState.Minimized)
            {
                ShellForm.ActiveParentForm.WindowState = FormWindowState.Maximized;
            }
            return;
        }

        /// <summary>
        /// </summary>
        /// <param name="shFrmDataContract"></param>
        public void ServiceShowFormWithStrings(string formId, string parameter1, string parameter2, string parameter3, string parameter4)
        {
            ShellForm.ActiveParentForm.GetExternalShowForm(formId, parameter1, parameter2, parameter3, parameter4);
            if (ShellForm.ActiveParentForm.WindowState == FormWindowState.Minimized)
            {
                ShellForm.ActiveParentForm.WindowState = FormWindowState.Maximized;
            }
            return;
        }

        /// <summary>
        /// </summary>
        /// <param name="myValue"></param>
        /// <returns></returns>
        public string MyOperation1(string myValue)
        {
            ////terrascanwcf.Infrastructure.Shell.testform frm = new testform();
            ////frm.Show();
            return "Hello: ";
        }

        /// <summary>
        /// </summary>
        /// <param name="dataContractValue"></param>
        /// <returns></returns>
        public string MyOperation2(DataContract1 dataContractValue)
        {
            return "Hello: " + dataContractValue.FirstName;
        }

    }


    [ServiceContract()]
    public interface IService1
    {
        [OperationContract]
        string MyOperation1(string myValue);
        [OperationContract]
        string MyOperation2(DataContract1 dataContractValue);
        [OperationContract]
        void ServiceShowForm(ShowFormDatacontract shFrmDataContract);
        [OperationContract]
        void ServiceShowFormWithStrings(string formId, string parameter1, string parameter2, string parameter3, string parameter4);
    }
    [DataContract]
    public class DataContract1
    {
        string firstName;
        string lastName;


        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        [DataMember]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
    }

    [DataContract]
    public class ShowFormDatacontract
    {
        string formid;
        string parameter1;
        string parameter2;
        string parameter3;
        string parameter4;

        [DataMember]
        public string FormID
        {
            get { return formid; }
            set { formid = value; }
        }
        [DataMember]
        public string Parameter1
        {
            get { return parameter1; }
            set { parameter1 = value; }
        }
        [DataMember]
        public string Parameter2
        {
            get { return parameter2; }
            set { parameter2 = value; }
        }
        [DataMember]
        public string Parameter3
        {
            get { return parameter3; }
            set { parameter3 = value; }
        }
        [DataMember]
        public string Parameter4
        {
            get { return parameter4; }
            set { parameter4 = value; }
        }
    }
}
