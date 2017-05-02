﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApi_Client_For_WCF.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IServiceIO")]
    public interface IServiceIO {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceIO/GetAllFiles", ReplyAction="http://tempuri.org/IServiceIO/GetAllFilesResponse")]
        string[] GetAllFiles(string dirName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceIO/GetAllFiles", ReplyAction="http://tempuri.org/IServiceIO/GetAllFilesResponse")]
        System.Threading.Tasks.Task<string[]> GetAllFilesAsync(string dirName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceIO/Get", ReplyAction="http://tempuri.org/IServiceIO/GetResponse")]
        string Get(string dirName, string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceIO/Get", ReplyAction="http://tempuri.org/IServiceIO/GetResponse")]
        System.Threading.Tasks.Task<string> GetAsync(string dirName, string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceIO/Put", ReplyAction="http://tempuri.org/IServiceIO/PutResponse")]
        void Put(string dirName, string fileName, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceIO/Put", ReplyAction="http://tempuri.org/IServiceIO/PutResponse")]
        System.Threading.Tasks.Task PutAsync(string dirName, string fileName, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceIO/Post", ReplyAction="http://tempuri.org/IServiceIO/PostResponse")]
        void Post(string dirName, string fileName, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceIO/Post", ReplyAction="http://tempuri.org/IServiceIO/PostResponse")]
        System.Threading.Tasks.Task PostAsync(string dirName, string fileName, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceIO/Delete", ReplyAction="http://tempuri.org/IServiceIO/DeleteResponse")]
        void Delete(string dirName, string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceIO/Delete", ReplyAction="http://tempuri.org/IServiceIO/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(string dirName, string fileName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceIOChannel : WebApi_Client_For_WCF.ServiceReference1.IServiceIO, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceIOClient : System.ServiceModel.ClientBase<WebApi_Client_For_WCF.ServiceReference1.IServiceIO>, WebApi_Client_For_WCF.ServiceReference1.IServiceIO {
        
        public ServiceIOClient() {
        }
        
        public ServiceIOClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceIOClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceIOClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceIOClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string[] GetAllFiles(string dirName) {
            return base.Channel.GetAllFiles(dirName);
        }
        
        public System.Threading.Tasks.Task<string[]> GetAllFilesAsync(string dirName) {
            return base.Channel.GetAllFilesAsync(dirName);
        }
        
        public string Get(string dirName, string fileName) {
            return base.Channel.Get(dirName, fileName);
        }
        
        public System.Threading.Tasks.Task<string> GetAsync(string dirName, string fileName) {
            return base.Channel.GetAsync(dirName, fileName);
        }
        
        public void Put(string dirName, string fileName, string value) {
            base.Channel.Put(dirName, fileName, value);
        }
        
        public System.Threading.Tasks.Task PutAsync(string dirName, string fileName, string value) {
            return base.Channel.PutAsync(dirName, fileName, value);
        }
        
        public void Post(string dirName, string fileName, string value) {
            base.Channel.Post(dirName, fileName, value);
        }
        
        public System.Threading.Tasks.Task PostAsync(string dirName, string fileName, string value) {
            return base.Channel.PostAsync(dirName, fileName, value);
        }
        
        public void Delete(string dirName, string fileName) {
            base.Channel.Delete(dirName, fileName);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(string dirName, string fileName) {
            return base.Channel.DeleteAsync(dirName, fileName);
        }
    }
}