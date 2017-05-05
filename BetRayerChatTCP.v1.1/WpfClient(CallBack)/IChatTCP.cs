﻿using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WpfClient_CallBack_
{
    [ServiceContract]
    public interface IMessageCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnMessageAdded(string message, DateTime timestamp);

        [OperationContract(IsOneWay = true)]
        void SendNames(List<string> names);
    }

    [ServiceContract(CallbackContract = typeof(IMessageCallback), SessionMode = SessionMode.Required)]
    public interface IMessage
    {
        [OperationContract(IsOneWay = true)]
        void AddMessage(string message, string sender);
        [OperationContract]
        bool Subscribe(string name);
        [OperationContract]
        bool Unsubscribe(string name);
    }
}
