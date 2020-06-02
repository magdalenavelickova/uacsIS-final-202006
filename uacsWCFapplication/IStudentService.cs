using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace uacsWCFapplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IStudentService
    {
        
        [OperationContract]
        Student GetDataUsingDataContract(Student student);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/YourNumber/{number}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetYourNumber(string number);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Student
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool IsActive
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StudentName
        {
            get { return stringValue; }
            set { stringValue = value; }
        }

        [DataMember]
        public string StudentIndex
        {
            get;
            set;
        }

        [DataMember]
        public string StudentEmail
        {
            get;
            set;
        }
    }
}
