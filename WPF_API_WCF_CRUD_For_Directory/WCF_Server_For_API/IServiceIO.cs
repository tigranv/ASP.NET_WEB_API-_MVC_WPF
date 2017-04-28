﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Server_For_API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceIO
    {
        [OperationContract]
        IEnumerable<string> GetAllFiles(string dirName);


        [OperationContract]
        string Get(string dirName, string fileName);


        [OperationContract]
        void Put(string dirName, string fileName, string value);

        [OperationContract]
        void Post(string dirName, string fileName, string value);

        [OperationContract]
        void Delete(string dirName, string fileName);
        
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WCF_Server_For_API.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
