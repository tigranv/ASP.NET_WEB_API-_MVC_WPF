﻿using ServiceClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace WpfClient_CallBack_
{
    public partial class MainWindow : Window, IMessageCallback, IDisposable
    {
        IMessage pipeProxy = null;
        bool flag = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Bt_LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (!flag)
            {
                if (Connect())
                {
                    status.Text = $"{txtUserName.Text} is connected";
                    pipeProxy.SendOnlineUsers();
                    flag = true;
                    Bt_LogIn.Content = "Log Out";
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            else
            {
                status.Text = $"Disconnected";
                flag = false;
                Bt_LogIn.Content = "Log In";
                ListBox_OnlineUsers.ItemsSource = null;
                pipeProxy.Unsubscribe(txtUserName.Text);
                pipeProxy.SendOnlineUsers();
            }   
        }

        private void Bt_Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pipeProxy.AddMessage(messageTextbox.Text, txtUserName.Text);
                messageTextbox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool Connect()
        {
            
            DuplexChannelFactory<IMessage> pipeFactory =
                      new DuplexChannelFactory<IMessage>(
                      new InstanceContext(this),
                      new NetTcpBinding(),
                      new EndpointAddress("net.tcp://localhost:7000/ISubscribe"));
            try
            {
                pipeProxy = pipeFactory.CreateChannel();
                pipeProxy.Subscribe(txtUserName.Text);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
      
        public void OnMessageAdded(string message, DateTime timestamp)
        {
            Dispatcher.Invoke(() =>
            {
                rtbMessages.Text+= message + ": " + timestamp.ToString("hh:mm:ss") + "\n";
            });
        }        

        public void SendNames(ObservableCollection<string> names)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)(() =>
                {
                    ListBox_OnlineUsers.DataContext = names;
                    Binding binding = new Binding();
                    ListBox_OnlineUsers.SetBinding(ItemsControl.ItemsSourceProperty, binding);
                }));
        }

        private void ClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pipeProxy.Unsubscribe(txtUserName.Text);
            pipeProxy.SendOnlineUsers();
        }

        public void Dispose()
        {
            pipeProxy.Unsubscribe(txtUserName.Text);
            pipeProxy.SendOnlineUsers();
        }
    }
}
