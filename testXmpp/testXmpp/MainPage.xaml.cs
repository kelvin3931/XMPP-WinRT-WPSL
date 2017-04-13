using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XMPP.common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace testXmpp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private XMPP.Client Client;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btn_connect(object sender, RoutedEventArgs e)
        {
            Client = new XMPP.Client();

            Client.Settings.Hostname = "hostname";
            Client.Settings.SSL = false;
            Client.Settings.OldSSL = false;
            Client.Settings.Port = 5222;
            Client.Settings.Id = "id";
            Client.Settings.Password = "password";
            Client.Settings.Account = "account";

            Client.OnReceive += OnReceive;

            Client.Connect();
        }

        private void btn_sendMsg(object sender, RoutedEventArgs e)
        {
            var message = new XMPP.tags.jabber.client.message();
            message.to = "idReceiver";
            message.from = "idSender";
            message.type = XMPP.tags.jabber.client.message.typeEnum.chat;

            var body = new XMPP.tags.jabber.client.body();
            body.Value = "Hello, test in windows 10 env.";
            message.Add(body);

            Client.Send(message);
        }

        private void btn_disconnect(object sender, RoutedEventArgs e)
        {
            Client.Disconnect();
        }

        // OnReceive handler
        private void OnReceive(object sender, TagEventArgs e)
        {
            if (e.tag is XMPP.tags.jabber.client.message)
            {
                ProcessMessage(e.tag as XMPP.tags.jabber.client.message);
            }
        }

        private void ProcessMessage(XMPP.tags.jabber.client.message msg)
        {
            if (msg.body.ToString() != "")
            {
                Debug.WriteLine("message body:" + msg.body);
                Debug.WriteLine("message time:" + msg.Timestamp);
                Debug.WriteLine("to:" + msg.to);
                Debug.WriteLine("from:" + msg.from);
            }
        }
    }
}
