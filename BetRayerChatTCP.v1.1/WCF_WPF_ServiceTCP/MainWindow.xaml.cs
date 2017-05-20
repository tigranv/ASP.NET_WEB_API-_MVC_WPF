﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Collections.ObjectModel;
using ServiceClassLibrary;
using System.Windows.Media;
using Google.API.Translate;
using LatinTo_ArmClassLibrary;

namespace WCF_WPF_ServiceTCP
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public partial class MainWindow : Window, IMessage
    {
        private static Dictionary<string, IMessageCallback> subscribers = new Dictionary<string, IMessageCallback>();
        private static ObservableCollection<string> onlinesList = new ObservableCollection<string>();
        public ServiceHost host = null;
        bool flag = false;
        object syncObj = new object();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (!flag)
            {
                host = new ServiceHost(typeof(MainWindow), new Uri("net.tcp://localhost:7000"));
                host.AddServiceEndpoint(typeof(IMessage), new NetTcpBinding(), "ISubscribe");
                host.Open();
                buttonStart.Content = "STOP";
                buttonStart.Background = Brushes.Red;
                labelStatus.Content = "Connected to Port net.tcp://localhost:7000";
                flag = true;   
            }
            else
            {
                host.Close();
                labelStatus.Content = "Disconnected";
                buttonStart.Background = Brushes.ForestGreen;
                buttonStart.Content = "START";
                flag = false;  
            }
        }

        public bool Subscribe(string name)
        {
            try
            {
                IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                if (!subscribers.ContainsKey(name) && !subscribers.ContainsValue(callback))
                {
                    subscribers.Add(name, callback);
                    onlinesList.Add(name);
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }


        public bool Unsubscribe(string name)
        {
            foreach (string c in subscribers.Keys)
            {
                if (name == c)
                {
                    lock (syncObj)
                    {
                        subscribers.Remove(name);
                        onlinesList.Remove(name);
                        foreach (IMessageCallback callback in subscribers.Values)
                        {
                            callback.SendNames(onlinesList);
                        }
                    }
                    return true;
                }
            }
            return false;

            //try
            //{
            //    IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
            //    if (!subscribers.ContainsKey(name) && !subscribers.ContainsValue(callback))
            //    {
            //        subscribers.Remove(name);
            //        onlinesList.Remove(name);     
            //    }
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public void AddMessage(string message, string sender, bool convMode)
        {
            //string language;
            //TranslateClient client = new TranslateClient("");
            //string translate = client.TranslateAndDetect(message, (string)Google.API.Translate.Language.English, out language);
            string messText = convMode?$"{sender} ---> {message.LatToArmConverter1()}": $"{sender} ---> {message}";
            foreach (var item in subscribers)
            {
                if (((ICommunicationObject)item.Value).State == CommunicationState.Opened)
                {
                    item.Value.OnMessageAdded(messText, DateTime.Now);
                }
                else
                {
                    subscribers.Remove(item.Key);
                }
            }

            // old code asunc foreach
            //subscribers.ForEach(delegate (IMessageCallback callback)
            //{
            //    if (((ICommunicationObject)callback).State == CommunicationState.Opened)
            //    {
            //        callback.OnMessageAdded(messText, DateTime.Now);
            //    }
            //    else
            //    {
            //        subscribers.Remove(callback);
            //    }
            //});    
        }
        

        public void SendOnlineUsers()
        {
            foreach (var item in subscribers)
            {
                if (((ICommunicationObject)item.Value).State == CommunicationState.Opened)
                {
                    item.Value.SendNames(onlinesList);
                }
                else
                {
                    subscribers.Remove(item.Key);
                }
            }

            // for old code
            //subscribers.ForEach(delegate (IMessageCallback callback)
            //{
            //    if (((ICommunicationObject)callback).State == CommunicationState.Opened)
            //    {
            //        callback.SendNames(onlinesList);
            //    }
            //    else
            //    {
            //        subscribers.Remove(callback);
            //    }
            //});
        }

        public void AddPrivateMessage(string message, string sender, string reciver)
        {
            string messText = $"{sender} ---> {message}";

            subscribers[reciver].OnPrivateMessageAdded(messText, sender, DateTime.Now);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}
